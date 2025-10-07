Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_Carga_Individual
    Inherits System.Web.UI.Page
    Dim oFuncInv As New clsInv_Procesos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnOpen.Attributes.Add("OnClick", "window.open('Inventario_Emergente.aspx',null,'height=400,width=480');")
            BtnIngresar.Attributes.Add("OnClick", "window.open('Inventario_Emergente.aspx',null,'height=400,width=480');")
            Dim obj As New clsInv_Listados
            lblError.Text = ""
            'btnListar_Click(sender, e)
            optUbicacionD.SelectedIndex = "1"
            btnOpen.Visible = False
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Cn.Open() : CmdGlobal.Connection = Cn
            If Existe_Tabla("V_INV_GENERAR_RECEP", Session("Ruta_Emp")) = False Then
                CmdGlobal.CommandText = " CREATE TABLE V_INV_GENERAR_RECEP (SERIE_NUMERAR float, SERIE_USER VARCHAR(8)) "
                CmdGlobal.ExecuteNonQuery()
            End If
            CmdGlobal.CommandText = " DELETE FROM V_INV_GENERAR_RECEP WHERE SERIE_USER = '" & Session("User") & "'  " : CmdGlobal.ExecuteNonQuery()
            txtPlaca.Text = Session("NorPlaca")
            txtNroSerie.Text = Session("NroSerie")
            lblError.Text = Session("Mensaje")
            If txtPlaca.Text <> "" Or txtNroSerie.Text <> "" Then btnListar_Click(sender, e)
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click

        Dim obj As New clsInv_Listados
        Dim objProceso As New clsInv_Procesos
        lblError.Text = ""
        lblRegistro.Text = ""

        Dim pCodArt As Integer = 0
        Dim TipoLista As String = "0"
        Dim dt As DataTable
        Dim psNroPlaca As Double = 0
        dt = Nothing
        lblRegistro.Text = ""
        btnOpen.Visible = False
        BtnNo.Visible = False
        Flex.DataSource = dt
        Flex.DataBind()
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)

        Session("NroSerie") = txtNroSerie.Text
        Session("NorPlaca") = txtPlaca.Text

        If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        If txtNroSerie.Text.Trim = "" And txtPlaca.Text.Trim = "" Then
            lblError.Text = "Seleccionar al menos una busqueda." : Exit Sub
        End If
        Try
            dt = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroSerie.Text.Trim, psNroPlaca)
            If dt.Rows.Count = 0 Then
                lblRegistro.Text = "No se encontraron registros."
                If txtPlaca.Text <> "" Or txtNroSerie.Text <> "" Then
                    Session("NroSerie") = txtNroSerie.Text
                    Session("NorPlaca") = txtPlaca.Text
                    lblRegistro.Text = "No se encontró el equipo.¿Desea ingresarlo?"
                    btnOpen.Visible = True
                    BtnNo.Visible = True
                End If
            Else
                Flex.DataSource = dt
                Flex.DataBind()
                FlexDetalle.DataSource = dt
                FlexDetalle.DataBind()
                lblRegistro.Text = "Se encontrarón " & dt.Rows.Count & " registros."
                Call CargarDatosFaltantes()
                Flex.PageSize = 1000
                Dim dtActividades As DataTable
                Dim lblDS As TextBox = FlexDetalle.Rows(14).Cells(0).FindControl("lblSerieNumerar")
                dtActividades = obj.Lista_Equipos_aGenerar(Session("Ruta_Emp"), Session("CodEmpresa"), lblDS.Text)
                If dtActividades.Rows.Count > 0 Then
                    For Each drAct2 As DataRow In dtActividades.Rows
                        Dim ddlDEst As DropDownList = FlexDetalle.Rows(10).Cells(0).FindControl("ddlDEstado")
                        Dim ddlDZon As DropDownList = FlexDetalle.Rows(9).Cells(0).FindControl("ddlDZona")
                        Dim txtDObservacion As TextBox = FlexDetalle.Rows(11).Cells(0).FindControl("txtDObs")
                        Dim txtDFechaFin As TextBox = FlexDetalle.Rows(12).Cells(0).FindControl("txtDFecha")
                        ddlDEst.SelectedValue = Nu(drAct2("SERIE_ESTADO_EQUIPO"))
                        txtDObservacion.Text = Nu(drAct2("OBS"))
                        txtDFechaFin.Text = Nu(drAct2("FECHA"))
                        ddlDZon.SelectedValue = Nu(drAct2("SERIE_ZONA"))
                    Next
                End If
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub CargarDatosFaltantes()
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim pdCodDet As Double = 0
        Dim dtActividades As New DataTable
        Dim dt As New DataTable
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Dim psCantEval As Integer = 0
        Dim obj As New clsInv_Listados
        Try
            For i = 0 To Flex.Rows.Count - 1
                dtActividades = obj.Lista_Equipos_aGenerar(Session("Ruta_Emp"), Session("CodEmpresa"), Flex.Rows(i).Cells(17).Text)
                If dtActividades.Rows.Count > 0 Then
                    For Each drAct2 As DataRow In dtActividades.Rows
                        Dim ddlEst As DropDownList = Flex.Rows(i).Cells(14).FindControl("ddlEstado")
                        Dim ddlZon As DropDownList = Flex.Rows(i).Cells(13).FindControl("ddlZona")
                        Dim txtObservacion As TextBox = Flex.Rows(i).Cells(15).FindControl("txtObs")
                        Dim txtFechaFin As TextBox = Flex.Rows(i).Cells(16).FindControl("txtFecha")
                        Dim txtVol As TextBox = Flex.Rows(i).Cells(18).FindControl("txtVolumen")
                        ddlEst.SelectedValue = Nu(drAct2("SERIE_ESTADO_EQUIPO"))
                        txtObservacion.Text = Nu(drAct2("OBS"))
                        txtFechaFin.Text = Nu(drAct2("FECHA"))
                        ddlZon.SelectedValue = Nu(drAct2("SERIE_ZONA"))
                        txtVol.Text = Nu(drAct2("SERIE_VOLUMEN"))
                    Next
                End If
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Dim objProceso As New clsInv_Procesos
        lblError.Text = ""
        Dim pCodArt As Integer = 0
        Dim TipoLista As String = "0"
        Dim psNroPlaca As Double = 0
        Dim pdCodAlmacen As Double = 0
        Dim psTipoBien As String = "%"
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        Dim pdAntiguedad As Int16 = 0

        If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        Try
            Flex.PageIndex = e.NewPageIndex
            Flex.DataSource = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroSerie.Text.Trim, psNroPlaca)
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Flex.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Flex)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=STOCK.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Private Sub Exportar_Excel2(ByVal dt As DataTable)
        Dim StwWriter As New System.IO.StringWriter
        Dim htwWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(StwWriter)
        Dim dgGrid As DataGrid = New DataGrid
        dgGrid.DataSource = dt
        dgGrid.HeaderStyle.Font.Bold = True
        dgGrid.DataBind()
        dgGrid.RenderControl(htwWriter)
        Response.ContentType = "application/vnd.ms-excel"
        Me.EnableViewState = False
        Response.Write(StwWriter.ToString)
        Response.End()
    End Sub

    Private Sub Flex_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Flex.RowDataBound
        Dim dt As New DataTable
        Dim psCantEval As Integer = 0
        Dim Colum As Integer = 0
        Dim i As Integer = 0
        Dim obj As New clsInv_Listados
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddlEst As DropDownList = DirectCast(e.Row.FindControl("ddlEstado"), DropDownList)
                Dim ddlZon As DropDownList = DirectCast(e.Row.FindControl("ddlZona"), DropDownList)
                'Dim ddlAlm As DropDownList = DirectCast(e.Row.FindControl("ddlAlmacenD"), DropDownList)
                ddlEst.ClearSelection() : Call LlenaComboItem("TBOPC532", ddlEst)
                Dim psCodAlmacen As Double = 0
                If txtDUbicacion.Text <> "" Then psCodAlmacen = txtDUbicacion.Text
                ddlZon.ClearSelection()
                If psCodAlmacen <> 0 Then
                    Call Llenar_Zona(ddlZon, psCodAlmacen)
                End If
            End If

                Me.Page.Session.Timeout = 1080
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Llenar_Zona(ByVal combo As DropDownList, ByVal psCodAlm As Double)
        Dim obj As New clsInv_Listados
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.ListarZona_xAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), psCodAlm)
        combo.DataTextField = "NOMBRE"
        combo.DataValueField = "AZONA_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim i As Long = 0
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Dim psIngresar As String = "S"
        Try ' 
            If e.CommandName = "Enviar" Then
                If txtDUbicacion.Text = "" Then lblError.Text = "Ingresar la ubicación de Destino." : Exit Sub
                Dim ddlEst As DropDownList = Flex.Rows(Index).Cells(14).FindControl("ddlEstado")
                Dim ddlZon As DropDownList = Flex.Rows(Index).Cells(13).FindControl("ddlZona")
                Dim txtObservacion As TextBox = Flex.Rows(Index).Cells(15).FindControl("txtObs")
                Dim txtFechaFin As TextBox = Flex.Rows(Index).Cells(16).FindControl("txtFecha")
                Dim txtVol As TextBox = Flex.Rows(Index).Cells(18).FindControl("txtVolumen")
                'Dim ddlDestino As DropDownList = Flex.Rows(Index).Cells(18).FindControl("ddlAlmacenD")
                If ddlEst.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar el estado del equipo." : Exit Sub
                If ddlZon.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar la zona de ubicacion." : Exit Sub
                If ddlEst.SelectedValue = "3" And txtObservacion.Text = "" Then lblError.Text = "Ingresar cambio por hacer." : Exit Sub
                If ddlEst.SelectedValue = "4" And txtObservacion.Text = "" Then lblError.Text = "Ingresar al responsable del equipo." : Exit Sub
                If ddlEst.SelectedValue = "4" And txtFechaFin.Text = "" Then lblError.Text = "Ingresar hasta que fecha permanece en el almacén." : Exit Sub
                Dim psDestino As String = ""
                If txtDUbicacion.Text <> "" Then psDestino = txtDUbicacion.Text
                Call Ingreso_Equipo_AAlmacen(Flex.Rows(Index).Cells(17).Text, ddlEst.SelectedValue, ddlZon.SelectedValue, txtObservacion.Text.Trim, txtFechaFin.Text.Trim, psDestino, txtVol.Text.Trim, "I", optUbicacionD.SelectedValue.Trim)
            ElseIf e.CommandName = "Agregar" Then
                For i = 0 To FlexRecep.Rows.Count - 1
                    If FlexRecep.Rows(i).Cells(16).Text = Flex.Rows(Index).Cells(17).Text Then
                        lblError.Text = "El equipo ya se encuentra agregado " : psIngresar = "N"
                    End If
                Next
                If psIngresar = "S" Then
                    Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                    Dim CmdGlobal As New SqlCommand
                    Cn.Open() : CmdGlobal.Connection = Cn
                    CmdGlobal.CommandText = " INSERt INTO V_INV_GENERAR_RECEP (SERIE_NUMERAR, SERIE_USER) VALUES (" & Flex.Rows(Index).Cells(17).Text & ",'" & Session("User") & "') " : CmdGlobal.ExecuteNonQuery()
                    Cn.Close()
                End If
                dt = obj.Lista_Equipos_aRecepcionar(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"))
                FlexRecep.DataSource = dt
                FlexRecep.DataBind()
                If dt.Rows.Count > 0 Then
                    lblRegistroRe.Text = dt.Rows.Count & " equipos a recepcionar"
                Else
                    lblRegistroRe.Text = "No hay equipos a recepcionar"
                End If
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Function Ingreso_Equipo_AAlmacen(ByVal psSerieCodigo As String, ByVal psEstado As String, ByVal psZona As String, ByVal psObs As String,
                                     ByVal psFecha As String, ByVal psDestino As String, ByVal psVolumen As String, ByVal psViene As String,
                                     ByVal psTipoDestino As String) As String
        lblError.Text = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim ValorSys As String = ""
        Dim psCodCECosto As String = ""
        Dim psCodSeccion As String = ""
        Dim psCodArt As String = ""
        Dim psSerieNumerar As String = psSerieCodigo
        Dim psSerieNro As String = ""
        Dim psPlacaNro As String = ""
        Dim psT As String = ""
        Dim psFechaAdq As String = ""
        Dim lblNroMovimiento As String = ""
        Dim StockAc As Double = 0
        Dim cant As Double : cant = 0
        Dim i As Long = 0
        Dim psTipoOrigen As String = ""
        Dim lblCodAlmacen As String = ""
        Dim lblCodDespacho As String = ""
        Dim psCodDestino As String = ""
        Ingreso_Equipo_AAlmacen = ""
        If psDestino <> "" Then psCodDestino = psDestino
        StockAc = 0
        ValorSys = Session("User") + FechaActual() + HoraActual()
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Dim psRecepcion As String = ""
        Dim drArt As DataRow
        Dim dtArt As New DataTable
        i = 0
        CmdGlobal2.CommandText = " SELECT SERIE_NRO, SERIE_NUMERAR, UBICACT_TIPO, UBICACT_CODIGO,ARTICULO_CODIGO " _
                        & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE SERIE_NUMERAR = " & psSerieNumerar
        Rs2 = CmdGlobal2.ExecuteReader
        If Rs2.HasRows Then
            While Rs2.Read
                lblCodAlmacen = Nu(Rs2!UBICACT_CODIGO)
                psTipoOrigen = Nu(Rs2!UBICACT_TIPO)
                psSerieNumerar = Nu(Rs2!Serie_Numerar)
                psCodArt = Nu(Rs2!ARTICULO_CODIGO)
                psRecepcion = ""
                dtArt.Columns.Add("Art_Codigo")
                dtArt.Columns.Add("Art_Cant_xRec")
                dtArt.Columns.Add("Art_Tipo")
                dtArt.Columns.Add("Art_Garantia") 'fecha yyyymmdd
                dtArt.Columns.Add("TiempoGarantia")
                dtArt.Columns.Add("UnidadGarantia")
                drArt = dtArt.NewRow()
                drArt("Art_Codigo") = psCodArt
                drArt("Art_Cant_xRec") = 1
                drArt("Art_Tipo") = "88"
                drArt("Art_Garantia") = ""
                drArt("TiempoGarantia") = ""
                drArt("UnidadGarantia") = ""
                dtArt.Rows.Add(drArt)
                If psTipoOrigen = psTipoDestino And lblCodAlmacen = psCodDestino Then
                Else
                    If psTipoOrigen = "1" Then
                        '-----------------------SALIDA DE ALMACEN
                        CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                lblCodDespacho = Nz(Rs(0)) + 1
                            End While
                        Else
                            lblCodDespacho = 1
                        End If
                        Rs.Close()
                        CmdGlobal3.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                               & " ALMACEN_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                               & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                               & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoDestino & "'," _
                                               & " " & psCodDestino & ",'2','0',1,1,0,1," & lblCodAlmacen & "," _
                                               & " '" & FechaActual() & "','" & HoraActual() & "','20','" & ValorSys & "')"
                        CmdGlobal3.ExecuteNonQuery()
                        CmdGlobal3.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','0',NULL,'20','N')"
                        CmdGlobal3.ExecuteNonQuery()
                        CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                        CmdGlobal3.ExecuteNonQuery()
                        'STOCK
                        StockAc = 0
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                        & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                                CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                             & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal3.ExecuteNonQuery()
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

                        Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, "20", psCodArt, psTipoOrigen, lblCodAlmacen, psTipoDestino, psCodDestino, "Por Inventario", "2", FormatoFecha(FechaActual), 1)

                        CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                      & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & lblCodAlmacen & "', " _
                                                      & " '" & psCodArt & "','1','" & ValorSys & "','3','20','" & FechaActual() & "','0','" & lblCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                        CmdGlobal.ExecuteNonQuery()
                        '--------------------------recepcion en ccosto O ALMACEN
                        CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
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
                                CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                                             & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                             & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                            CmdGlobal3.ExecuteNonQuery()
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
                        Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, "20", psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, lblCodAlmacen, "Por Inventario", "1", FormatoFecha(FechaActual), 1)

                        CmdGlobal3.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                               & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                               & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                               & " '" & psCodArt & "','1','" & ValorSys & "','3','20','" & FechaActual() & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "','" & lblCodAlmacen & "')"
                        CmdGlobal3.ExecuteNonQuery()
                        CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                        CmdGlobal3.ExecuteNonQuery()
                        CmdGlobal3.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                                      & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'20','0','" & ValorSys & "','" & FechaActual() & "','1','" & lblCodDespacho & "')"
                        CmdGlobal3.ExecuteNonQuery()

                        If psViene = "I" Then
                            psRecepcion = oFuncInv.Guarda_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodAlmacen, 1, "5", 1, psCodDestino,
                                                      "20", FechaActual(), lblCodDespacho.Trim, 1, 1, "", HttpContext.Current.User.Identity.Name, "", "", dtArt, psTipoOrigen, psTipoDestino, "S")
                            CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR, SERIE_ORIG_TIPO, SERIE_ORIG_CODIGO, salida_codigo) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "', " & psRecepcion & ", " & psSerieNumerar & ", '" & psTipoOrigen & "', " & lblCodAlmacen & ", " & lblCodDespacho & ")"
                            CmdGlobal.ExecuteNonQuery()
                        End If

                    ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
                        CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                lblCodDespacho = Nz(Rs(0)) + 1
                            End While
                        Else
                            lblCodDespacho = 1
                        End If
                        Rs.Close()

                        CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                                          & " ALMACEN_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                                          & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL) " _
                                                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoDestino & "'," _
                                                          & " " & psCodDestino & ",'2','0',1,0,1,'" & lblCodAlmacen & "'," _
                                                          & " '" & FechaActual() & "','" & HoraActual() & "','20')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO) " _
                                                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','N','0','20')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                        CmdGlobal.ExecuteNonQuery()

                        'STOCK
                        CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                                CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                             & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal3.ExecuteNonQuery()
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

                        Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, "20", psCodArt, psTipoOrigen, lblCodAlmacen, "1", psCodDestino, "Por Inventario", "2", FormatoFecha(FechaActual), 1)

                        CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                      & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & lblCodAlmacen & "', " _
                                                      & " '" & psCodArt & "','1','" & ValorSys & "','3','20','" & FechaActual() & "','0','" & lblCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                        CmdGlobal.ExecuteNonQuery()
                        '--------------------------recepcion en ccosto O ALMACEN
                        CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET  SET RECIBIDA_OK ='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO='M' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA  SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='3',OSAL_CANT_REC='1',OSAL_CANT_FALT_REC='0' WHERE OSAL_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        CmdGlobal.ExecuteNonQuery()
                        'STOCK
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                        & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                                CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                  & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                            CmdGlobal3.ExecuteNonQuery()
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

                        Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, "20", psCodArt, "1", psCodDestino, psTipoOrigen, lblCodAlmacen, "Por Inventario", "1", FormatoFecha(FechaActual), 1)

                        CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                      & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                                      & " '" & psCodArt & "','1','" & ValorSys & "','3','20','" & FechaActual() & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "','" & lblCodAlmacen & "')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                                      & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'20','0','" & ValorSys & "','" & FechaActual() & "','2','" & lblCodDespacho & "')"
                        CmdGlobal.ExecuteNonQuery()
                        If psViene = "I" Then
                            psRecepcion = oFuncInv.Guarda_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodAlmacen, 1, "5", 1, psCodDestino,
                                                      "20", FechaActual(), lblCodDespacho.Trim, 1, 1, "", HttpContext.Current.User.Identity.Name, "", "", dtArt, psTipoOrigen, "1", "S")
                            CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR, SERIE_ORIG_TIPO, SERIE_ORIG_CODIGO, salida_codigo ) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "', " & psRecepcion & ", " & psSerieNumerar & ", '" & psTipoOrigen & "', " & lblCodAlmacen & ", " & lblCodDespacho & ")"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    End If
                End If
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_CARGADO_INV = '2' WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_EQUIPO = '" & psEstado & "', SERIE_VOLUMEN = '" & psVolumen & "', " _
                                      & " SERIE_RESPONSABLE_OBSERVACION = '" & psObs & "', SERIE_CUSTODIA_FECHAFIN = '" & psFecha & "', SERIE_ZONA = " & psZona & " " _
                                      & " WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                Ingreso_Equipo_AAlmacen = lblCodDespacho
            End While
        Else
            Exit Function
        End If
        Cn.Close()
        Cn.Dispose()
        Cn2.Close()
        Cn2.Dispose()
        Cn3.Close()
        Cn3.Dispose()
        lblError.Text = "TERMINO EL INGRESO DE EQUIPOS A ALMACEN DE DESUSO"
    End Function

    Private Sub FlexDetalle_DataBound(sender As Object, e As EventArgs) Handles FlexDetalle.DataBound
        Dim dt As New DataTable
        Dim psCantEval As Integer = 0
        Dim Colum As Integer = 0
        Dim i As Integer = 0
        Dim obj As New clsInv_Listados
        Try
            Dim ddlEst As DropDownList = FlexDetalle.FindControl("ddlDEstado")
            ddlEst.ClearSelection()
            Call LlenaComboItem("TBOPC532", ddlEst)
            Dim ddlZon As DropDownList = FlexDetalle.FindControl("ddlDZona")
            Dim psCodAlmacen As Double = 0
            ddlZon.ClearSelection()
            If txtDUbicacion.Text <> "" Then psCodAlmacen = txtDUbicacion.Text
            If psCodAlmacen <> 0 Then
                Call Llenar_Zona(ddlZon, psCodAlmacen)
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FlexDetalle_PageIndexChanging(sender As Object, e As DetailsViewPageEventArgs) Handles FlexDetalle.PageIndexChanging
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Dim objProceso As New clsInv_Procesos
        lblError.Text = ""
        Dim pCodArt As Integer = 0
        Dim TipoLista As String = "0"
        Dim psNroPlaca As Double = 0
        Dim pdCodAlmacen As Double = 0
        Dim psTipoBien As String = "%"
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        Dim pdAntiguedad As Int16 = 0
        If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        Try
            FlexDetalle.PageIndex = e.NewPageIndex
            FlexDetalle.DataSource = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroSerie.Text.Trim, psNroPlaca)
            FlexDetalle.DataBind()
            Dim dtActividades As DataTable
            Dim lblDS As TextBox = FlexDetalle.Rows(14).Cells(e.NewPageIndex).FindControl("lblSerieNumerar")
            dtActividades = obj.Lista_Equipos_aGenerar(Session("Ruta_Emp"), Session("CodEmpresa"), lblDS.Text)
            If dtActividades.Rows.Count > 0 Then
                For Each drAct2 As DataRow In dtActividades.Rows
                    Dim ddlDEst As DropDownList = FlexDetalle.Rows(10).Cells(e.NewPageIndex).FindControl("ddlDEstado")
                    Dim ddlDZon As DropDownList = FlexDetalle.Rows(9).Cells(e.NewPageIndex).FindControl("ddlDZona")
                    Dim txtDObservacion As TextBox = FlexDetalle.Rows(11).Cells(e.NewPageIndex).FindControl("txtDObs")
                    Dim txtDFechaFin As TextBox = FlexDetalle.Rows(12).Cells(e.NewPageIndex).FindControl("txtDFecha")

                    ddlDEst.SelectedValue = Nu(drAct2("SERIE_ESTADO"))
                    txtDObservacion.Text = Nu(drAct2("OBS"))
                    txtDFechaFin.Text = Nu(drAct2("FECHA"))
                    ddlDZon.SelectedValue = Nu(drAct2("SERIE_ZONA"))
                Next
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub FlexDetalle_ItemCommand(sender As Object, e As DetailsViewCommandEventArgs) Handles FlexDetalle.ItemCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If txtDUbicacion.Text = "" Then lblError.Text = "Seleccionar ubicación de destino." : Exit Sub
        If e.CommandName = "Enviar" Then
            Try
                Dim ddlEst As DropDownList = FlexDetalle.Rows(10).Cells(Index).FindControl("ddlDEstado")
                Dim ddlZon As DropDownList = FlexDetalle.Rows(9).Cells(Index).FindControl("ddlDZona")
                Dim txtObservacion As TextBox = FlexDetalle.Rows(11).Cells(Index).FindControl("txtDObs")
                Dim txtFechaFin As TextBox = FlexDetalle.Rows(12).Cells(Index).FindControl("txtDFecha")
                Dim lblS As TextBox = FlexDetalle.Rows(14).Cells(Index).FindControl("lblSerieNumerar")
                If ddlEst.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar el estado del equipo." : Exit Sub
                If ddlZon.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar la zona de ubicacion." : Exit Sub
                If ddlEst.SelectedValue = "3" And txtObservacion.Text = "" Then lblError.Text = "Ingresar cambio por hacer." : Exit Sub
                If ddlEst.SelectedValue = "4" And txtObservacion.Text = "" Then lblError.Text = "Ingresar al responsable del equipo." : Exit Sub
                If ddlEst.SelectedValue = "4" And txtFechaFin.Text = "" Then lblError.Text = "Ingresar hasta que fecha permanece en el almacén." : Exit Sub
                Dim psDstino As String = ""
                If txtDUbicacion.Text <> "" Then psDstino = txtDUbicacion.Text
                Call Ingreso_Equipo_AAlmacen(lblS.Text, ddlEst.SelectedValue, ddlZon.SelectedValue, txtObservacion.Text.Trim, txtFechaFin.Text.Trim, psDstino, "", "I", optUbicacionD.SelectedValue.Trim)
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
            End Try
        End If
    End Sub

    Private Sub txtNroSerie_TextChanged(sender As Object, e As EventArgs) Handles txtNroSerie.TextChanged
        Call btnListar_Click(sender, e)
    End Sub

    Private Sub txtPlaca_TextChanged(sender As Object, e As EventArgs) Handles txtPlaca.TextChanged
        Call btnListar_Click(sender, e)
    End Sub
    Protected Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        lblRegistro.Text = ""
        btnOpen.Visible = False
        BtnNo.Visible = False
    End Sub

    Private Sub FlexRecep_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexRecep.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Try
            If e.CommandName = "Quitar" Then
                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Cn.Open() : CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " DELETE FROM V_INV_GENERAR_RECEP WHERE SERIE_USER = '" & Session("User") & "' and SERIE_NUMERAR = (" & FlexRecep.Rows(Index).Cells(16).Text & ") " : CmdGlobal.ExecuteNonQuery()
                Cn.Close()
                dt = obj.Lista_Equipos_aRecepcionar(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"))
                FlexRecep.DataSource = dt
                FlexRecep.DataBind()
                If dt.Rows.Count > 0 Then
                    lblRegistroRe.Text = dt.Rows.Count & " equipos a recepcionar"
                Else
                    lblRegistroRe.Text = "No hay equipos a recepcionar"
                End If
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub BtnIngresarEq_Click(sender As Object, e As EventArgs) Handles BtnIngresarEq.Click
        If FlexRecep.Rows.Count = 0 Then lblError.Text = "No hay equipos a recepcionar." : Exit Sub
        Dim psRecepcion As String = ""
        Dim drArt As DataRow
        Dim dtArt As New DataTable
        Dim dtArtSer As New DataTable
        lblError.Text = ""
        Dim i As Long = 0
        Dim a As Int16 = 0
        dtArt.Columns.Add("Art_Codigo")
        dtArt.Columns.Add("Art_Cant_xRec")
        dtArt.Columns.Add("Art_Tipo")
        dtArt.Columns.Add("Art_Garantia") 'fecha yyyymmdd
        dtArt.Columns.Add("TiempoGarantia")
        dtArt.Columns.Add("UnidadGarantia")
        dtArt.Columns.Add("ORIG_TIPO")
        dtArt.Columns.Add("ORIG_CODIGO")
        dtArt.Columns.Add("COD_SALIDA")
        dtArt.Columns.Add("SERIE_NUMERAR")
        Dim psDestino As String = ""
        Dim psCodSalida As String = ""
        If txtDUbicacion.Text = "" Then lblError.Text = "Seleccionar ubicación de destino." : Exit Sub
        If txtDUbicacion.Text <> "" Then psDestino = txtDUbicacion.Text
        For i = 0 To FlexRecep.Rows.Count - 1
            a = a + 1
            psCodSalida = ""
            Dim ddlEst As DropDownList = FlexRecep.Rows(i).Cells(14).FindControl("ddlRecEstado")
            Dim ddlZon As DropDownList = FlexRecep.Rows(i).Cells(13).FindControl("ddlRecZona")
            Dim txtObservacion As TextBox = FlexRecep.Rows(i).Cells(13).FindControl("txtRecObs")
            Dim txtFechaFin As TextBox = FlexRecep.Rows(i).Cells(15).FindControl("txtRecFecha")
            Dim txtVol As TextBox = FlexRecep.Rows(i).Cells(17).FindControl("txtRecVolumen")
            If ddlEst.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar el estado del equipo." : Exit Sub
            If ddlZon.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar la zona de ubicacion." : Exit Sub
            If ddlEst.SelectedValue = "3" And txtObservacion.Text = "" Then lblError.Text = "Ingresar cambio por hacer." : Exit Sub
            If ddlEst.SelectedValue = "4" And txtObservacion.Text = "" Then lblError.Text = "Ingresar al responsable del equipo." : Exit Sub
            If ddlEst.SelectedValue = "4" And txtFechaFin.Text = "" Then lblError.Text = "Ingresar hasta que fecha permanece en el almacén." : Exit Sub

            psCodSalida = Ingreso_Equipo_AAlmacen(FlexRecep.Rows(i).Cells(16).Text, ddlEst.SelectedValue, ddlZon.SelectedValue, txtObservacion.Text.Trim, txtFechaFin.Text.Trim, psDestino, txtVol.Text.Trim, "V", optUbicacionD.SelectedValue.Trim)
            If FlexRecep.Rows(i).Cells(6).Text <> optUbicacionD.SelectedValue.Trim And FlexRecep.Rows(i).Cells(7).Text <> txtDUbicacion.Text Then
                drArt = dtArt.NewRow()
                drArt("Art_Codigo") = FlexRecep.Rows(i).Cells(1).Text
                drArt("Art_Cant_xRec") = 1
                drArt("Art_Tipo") = "88"
                drArt("Art_Garantia") = ""
                drArt("TiempoGarantia") = ""
                drArt("UnidadGarantia") = ""
                drArt("ORIG_TIPO") = IIf(FlexRecep.Rows(i).Cells(6).Text = "Oficina", "2", "1")
                drArt("ORIG_CODIGO") = FlexRecep.Rows(i).Cells(18).Text
                drArt("SERIE_NUMERAR") = FlexRecep.Rows(i).Cells(16).Text
                drArt("COD_SALIDA") = psCodSalida
                dtArt.Rows.Add(drArt)
            End If
        Next
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        psRecepcion = oFuncInv.Guarda_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), 0, 1, "5", 1, psDestino,
                                                "5", FechaActual(), "", 1, FlexRecep.Rows.Count, "", HttpContext.Current.User.Identity.Name, "", "", dtArt, "", optUbicacionD.SelectedValue.Trim, "V")
        Session("CodRecep") = psRecepcion
        lblError.Text = "Se genero la recepción nro. " & psRecepcion
        lblRegistro.Text = ""
        lblRegistroRe.Text = ""
        BtnNo.Visible = False
        btnOpen.Visible = False
        Response.Redirect("../Inventario/Inventario_Carga_Individual.aspx")

    End Sub

    Private Sub FlexRecep_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles FlexRecep.RowDataBound
        Dim dt As New DataTable
        Dim psCantEval As Integer = 0
        Dim Colum As Integer = 0
        Dim i As Integer = 0
        Dim obj As New clsInv_Listados
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddlEst As DropDownList = DirectCast(e.Row.FindControl("ddlRecEstado"), DropDownList)
                Dim ddlZon As DropDownList = DirectCast(e.Row.FindControl("ddlRecZona"), DropDownList)
                'Dim ddlAlm As DropDownList = DirectCast(e.Row.FindControl("ddlAlmacenD"), DropDownList)
                ddlEst.ClearSelection() : Call LlenaComboItem("TBOPC532", ddlEst)
                Dim psCodAlmacen As Double = ""
                If txtDUbicacion.Text <> "" Then psCodAlmacen = txtDUbicacion.Text
                ddlZon.ClearSelection()
                If psCodAlmacen <> 0 Then
                    Call Llenar_Zona(ddlZon, psCodAlmacen)
                End If
            End If
                Me.Page.Session.Timeout = 1080
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiCerrar.Click
        ModalPopupExtender2.Hide()
        txtDUbicacion.Text = ""
        txtDCodigo.Text = ""
        txtDDescripcion.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            FlexUbicacion.DataBind()
            If optUbicacionD.SelectedValue.Trim = "2" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            ElseIf optUbicacionD.SelectedValue.Trim = "1" Then
                If txtBusCod.Text.Trim <> "" Then pdCodAlmacen = txtBusCod.Text.Trim
                FlexUbicacion.DataSource = obj.Lista_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            End If
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtDUbicacion.Text = ""
            txtDCodigo.Text = ""
            txtDDescripcion.Text = ""
            txtDCodigo.Text = FlexUbicacion.Rows(Index).Cells(1).Text
            txtDDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub optUbicacionD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optUbicacionD.SelectedIndexChanged

        txtDUbicacion.Text = ""
        txtDCodigo.Text = ""
        txtDDescripcion.Text = ""
        lblError.Text = ""
        lblRegistro.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        If optUbicacionD.SelectedValue = "0" Then
            btnUbica.Enabled = False
        ElseIf optUbicacionD.SelectedValue = "1" Then
            lblBusUbica.Text = "Busqueda de Almacén"
            btnUbica.Enabled = True
        ElseIf optUbicacionD.SelectedValue = "2" Then
            lblBusUbica.Text = "Busqueda de Centro de Costos"
            btnUbica.Enabled = True
        End If
    End Sub
    Protected Sub txtDCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtDCodigo.TextChanged
        txtDUbicacion.Text = ""
        txtDDescripcion.Text = ""
    End Sub
End Class
