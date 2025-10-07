Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Partial Class Inventario_Inventario_Recepcion_Registrar
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objProceso As New clsInv_Procesos
    Dim oFunc As New clsCont_Funciones
    Dim oFuncInv As New clsInv_Procesos
    Dim objCat As New Cls_Catalogo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
                If NroTicket <> "" Then
                    Session("TicketNro") = NroTicket
                Else
                    Session("TicketNro") = String.Empty
                End If
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 0
                txtFecRegistra.Text = FormatoFecha(FechaActual)
                txtFecRecepcion.Text = FormatoFecha(FechaActual)
                txtHoraRegistra.Text = FormatoHora(HoraActual)
                txtUserRegistra.Text = Mid(Session("UserNombre"), 14)
                obj.Llena_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), cboAlmacen, Session("User"))
                cboAlmacen.SelectedValue = "< Seleccionar >"
                obj.Llena_Motivo_Ing(Session("Ruta_Emp"), Session("CodEmpresa"), cboMotivo)
                cboMotivo.SelectedValue = "< Seleccionar >"
                obj.Llena_Propietario(Session("Ruta_Emp"), Session("CodEmpresa"), cboPropietario)
                cboPropietario.SelectedValue = "< Seleccionar >"
                obj.Llena_AñoProyecto(Session("Ruta_Emp"), Session("CodEmpresa"), cboAño)
                cboAño.SelectedValue = "< Seleccionar >"
                cboProyecto.Items.Add("< Seleccionar >") : cboProyecto.SelectedValue = "< Seleccionar >"
                Call LlenaComboItem("TBOPC062", cboTipoDoc)
                Call LLenar_TipoaArticulo()
                cboPropietario.SelectedValue = 1
            Catch Ex As SqlException
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Private Sub LLenar_TipoaArticulo()
        Dim dt As New DataTable
        dt = Nothing
        dt = objCat.Lista_Tipo(Session("Ruta_Emp"))
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< Seleccionar >")
        DdlTipoBA.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAño.SelectedIndexChanged
        If cboAño.SelectedValue <> "< Seleccionar >" Then
            Call obj.Llena_Proyecto(Session("Ruta_Emp"), Session("CodEmpresa"), cboProyecto, cboAño.SelectedValue.Trim)
        Else
            cboProyecto.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            lblError.Text = ""
            If cboMotivo.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar Motivo a recepcionar los artículos."
            If cboMotivo.SelectedValue = "27" Then
                If cboAlmacen.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar el Almacén a recepcionar los artículos."
                If cboMotivo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Motivo a recepcionar los artículos."
                If cboPropietario.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar al Propietario."
            Else
                If cboAlmacen.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar el Almacén a recepcionar los artículos."
                If cboMotivo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Motivo a recepcionar los artículos."
                If cboPropietario.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar al Propietario."
                'If cboProyecto.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar el Proyecto a ingresar."
                If cboTipoDoc.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar el Tipo de Documento."
                If txtProvCodigo.Text.Trim = "" Then lblError.Text = lblError.Text & "<br> - Seleccionar el Proveedor."
                If txtNroDoc.Text.Trim = "" And txtSerieDoc.Text.Trim = "" Then lblError.Text = lblError.Text & "<br> - Falta ingresar la Serie y/o Numeración del Documento."
            End If
            If optIngreso.SelectedIndex = -1 Then lblError.Text = lblError.Text & "<br> - Seleccionar si va ingresar las cantidades y/o series de los items."
            Dim sFecha As Date
            Dim DiasFuturoRegistrarFecha As Integer
            Dim pdFechaRecep As Long = 0
            Dim pdFechaRango As Long = 0
            Dim TotArt As Long = 0
            Dim i As Long = 0
            Dim lsNroOCompra As String
            'DiasFuturoRegistrarFecha = Val(ParametroTBOPC("TBOPC219", "3"))
            sFecha = DateAdd("d", DiasFuturoRegistrarFecha, FormatoFecha(FechaActual))
            If txtFecRecepcion.Text <> "" Then
                pdFechaRecep = Right(txtFecRecepcion.Text, 4) + Mid(txtFecRecepcion.Text, 4, 2) + Left(txtFecRecepcion.Text, 2)
                pdFechaRango = Right(sFecha, 4) + Mid(sFecha, 4, 2) + Left(sFecha, 2)
            Else
                lblError.Text = lblError.Text & "<br> - Ingresar Fecha de recepción."
            End If
            If pdFechaRecep > pdFechaRango And DiasFuturoRegistrarFecha > 0 Then lblError.Text = lblError.Text & "<br> - La Fecha de Recepción solo puede ser " & DiasFuturoRegistrarFecha & " dias a futuro."
            'If txtReferencia.Text.Trim = "" Then lblError.Text = lblError.Text & "<br> - Ingresar la Referencia"
            With FlexItem
                If .Rows.Count = 0 Then lblError.Text = lblError.Text & "<br> - No hay detalle de recepción que guardar."
                For i = 0 To .Rows.Count - 1
                    Dim psCant As TextBox
                    psCant = CType(FlexItem.Rows(i).Cells(6).FindControl("txtCant"), TextBox)
                    If psCant.Text.Trim = "" And psCant.Text.Trim <> "0" Then
                        lblError.Text = lblError.Text & "<br> - Al Item Nº " & .Rows(i).Cells(1).Text & " falta ingresarle la cantidad recibida.<br> NOTA: Todos los artículos a recibir deben tener cantidades."
                        Exit Sub
                    Else
                        TotArt = TotArt + CDbl(psCant.Text)
                    End If
                Next
                lsNroOCompra = txtNroOC.Text.Trim
            End With
            i = 0
            If lblError.Text <> "" Then
                lblError.Text = lblError.Text
                Exit Sub
            End If
            Dim dtArt As New DataTable
            Dim drArt As DataRow
            dtArt.Columns.Add("Art_Codigo")
            dtArt.Columns.Add("Art_Cant_xRec")
            dtArt.Columns.Add("Art_Tipo")
            dtArt.Columns.Add("Art_Garantia") 'fecha yyyymmdd
            dtArt.Columns.Add("TiempoGarantia")
            dtArt.Columns.Add("UnidadGarantia")
            dtArt.Columns.Add("ORIG_TIPO")
            dtArt.Columns.Add("COD_sALIDA")
            If FlexItem.Rows.Count > 0 Then
                For i = 0 To FlexItem.Rows.Count - 1
                    Dim psCant As TextBox
                    Dim psGarantia As TextBox
                    psCant = CType(FlexItem.Rows(i).Cells(7).FindControl("txtCant"), TextBox)
                    psGarantia = CType(FlexItem.Rows(i).Cells(8).FindControl("txtGarantia"), TextBox)
                    drArt = dtArt.NewRow()
                    drArt("Art_Codigo") = FlexItem.Rows(i).Cells(2).Text
                    drArt("Art_Cant_xRec") = psCant.Text
                    drArt("Art_Tipo") = FlexItem.Rows(i).Cells(11).Text
                    drArt("Art_Garantia") = Right(psGarantia.Text, 4) + Mid(psGarantia.Text, 4, 2) + Left(psGarantia.Text, 2)
                    drArt("TiempoGarantia") = ""
                    drArt("UnidadGarantia") = ""
                    drArt("ORIG_TIPO") = ""
                    drArt("COD_sALIDA") = ""
                    dtArt.Rows.Add(drArt)
                Next
            End If
            Dim psRecepcion As String = ""
            Dim psProyecto As String = ""
            If cboProyecto.SelectedValue.Trim <> "< Seleccionar >" Then
                psProyecto = cboProyecto.SelectedValue.Trim
            End If
            psRecepcion = objProceso.Guarda_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), txtProvCodigo.Text.Trim, cboPropietario.SelectedValue.Trim,
                                                      cboTipoDoc.SelectedValue.Trim, psProyecto, cboAlmacen.SelectedValue.Trim,
                                                      cboMotivo.SelectedValue.Trim, pdFechaRecep, txtNroOC.Text.Trim, FlexItem.Rows.Count, TotArt,
                                                      txtReferencia.Text.Trim, HttpContext.Current.User.Identity.Name, txtSerieDoc.Text.Trim, txtNroDoc.Text, dtArt, "3", "1", "R")


            If Session("TicketNro") <> "" And psRecepcion <> "" Then
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim psNroTicket As String = ""
                Dim psConexion As String = ""
                psConexion = Session("Ruta_Emp")
                psNroTicket = Session("TicketNro")
                objProceso.Guardar_RelacionTicket(psConexion, psNroTicket, "17", Nz(psRecepcion), Session("User"))
                Cn.Open() : CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_TICKET = " & psNroTicket & " WHERE RECEP_CODIGO = " & Nz(psRecepcion)
                CmdGlobal.ExecuteNonQuery()
                Cn.Close()
            End If
            If optIngreso.SelectedIndex = "0" Then
                Exportar.Visible = False
                lblErrort.Text = ""
                txtIngCodAlmacen.Text = cboAlmacen.SelectedValue.Trim
                txtIngRecepcion.Text = Llenar_Ceros(psRecepcion, 6)
                txtIngAlmacen.Text = cboAlmacen.Items(cboAlmacen.SelectedIndex).Text
                txtIngProveedor.Text = txtProvNombre.Text
                Dim dt As New DataTable
                Dim pdCodRecep As Double = txtIngRecepcion.Text
                dt = obj.Lista_Recepcion_Item(Session("Ruta_Emp"), Session("CodEmpresa"), psRecepcion, "S")
                FlexItemSerie.DataSource = dt
                FlexItemSerie.DataBind()
                If dt.Rows.Count > 0 Then
                    Exportar.Visible = True
                Else
                    Exportar.Visible = False
                End If
                dt = Nothing
                dt = obj.Lista_Recepcion_Item(Session("Ruta_Emp"), Session("CodEmpresa"), psRecepcion, "N")
                FlexItemAcc.DataSource = dt
                FlexItemAcc.DataBind()
                FlexItemSerie.DataBind()
                dt = Nothing
                txtIngFecha.Text = txtFecRecepcion.Text
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha.ActiveTabIndex = 1 : Ficha.TabIndex = "1"
                If FlexItemAcc.Rows.Count > 0 Then
                    ChkRecibirAcc.Visible = True
                    BtnGuardarAccCant.Visible = True
                    FlexItemAcc.Visible = True
                End If
            Else
                Response.Redirect("Inventario_Recepcion_Registrar.aspx")
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        End Try
    End Sub
    Protected Sub FlexItemSerie_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexItemSerie.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "IngSerie" Then
            Dim pdCodRecepcion As Double = txtIngRecepcion.Text
            Dim pdCodArt As Double = FlexItemSerie.Rows(Index).Cells(2).Text
            Dim dt As New DataTable
            txtIngArtCodigo.Text = FlexItemSerie.Rows(Index).Cells(2).Text
            txtIngArticulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItemSerie.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            dt = obj.Lista_Recepcion_Item_Serie(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion, pdCodArt)
            FlexSeries.DataSource = dt
            FlexSeries.DataBind()
            Call Llenar_NroSeries()
            dt = Nothing
            BtnGuardarS.Enabled = True
            IngSeries.Visible = True
            BtnGuardarS.Enabled = True
            BtnBorrar.Enabled = True
            Exportar.Visible = True
        End If
    End Sub

    Protected Sub btnGuardarS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardarS.Click
        Dim n As Long, NoSaves As Long
        Dim ValorSys As String
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        'Dim Rs As SqlDataReader
        ValorSys = ""
        NoSaves = 0
        For n = 0 To FlexSeries.Rows.Count - 1
            Dim txtNroSerie As TextBox = FlexSeries.Rows(n).Cells(0).FindControl("txtSerie")
            If txtNroSerie.Text <> "" Then NoSaves = 1 : Exit For
            Dim txtNroPlaca As TextBox = FlexSeries.Rows(n).Cells(1).FindControl("txtPlaca")
            If txtNroPlaca.Text <> "" Then NoSaves = 1 : Exit For
        Next
        'abrir base datos
        Cn.Open() : Cn2.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
        '....
        If NoSaves = 0 Then lblError.Text = "Ingresar Nro. de Series." : Exit Sub
        NoSaves = 0
        For n = 0 To FlexSeries.Rows.Count - 1
            Dim txtNroSerie As TextBox = FlexSeries.Rows(n).Cells(0).FindControl("txtSerie")
            Dim txtNroPlaca As TextBox = FlexSeries.Rows(n).Cells(0).FindControl("txtPlaca")
            'psNroSerie = CType(FlexItemSerie.Rows(n).Cells(0).FindControl("txtSerie"), TextBox)
            If txtNroSerie.Text <> "" Then 'SERIE NORMAL
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_NRO='" & txtNroSerie.Text & "',SYS_SERIE='" & ValorSys & "' WHERE SERIE_NUMERAR=" & FlexSeries.Rows(n).Cells(2).Text
                CmdGlobal.ExecuteNonQuery()
                If txtNroPlaca.Text <> "" Then
                    CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET PLACA_NRO='" & txtNroPlaca.Text & "',SYS_SERIE='" & ValorSys & "' WHERE SERIE_NUMERAR=" & FlexSeries.Rows(n).Cells(2).Text
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If
        Next
        BtnGuardarS.Enabled = False
        FlexSeries.DataSource = Nothing
        FlexSeries.DataBind()
        txtIngArticulo.Text = ""
        txtIngArtCodigo.Text = ""
        Call Calculo_Cantidades_Recibidas()
    End Sub

    Private Sub Llenar_NroSeries()
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim pdCodDet As Double = 0
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Try
            Cn.Open() : cmdSql.Connection = Cn
            For i = 0 To FlexSeries.Rows.Count - 1
                Dim txtNroSerie As TextBox = FlexSeries.Rows(i).Cells(0).FindControl("txtSerie")
                Dim txtNroPlaca As TextBox = FlexSeries.Rows(i).Cells(0).FindControl("txtPlaca")
                cmdSql.CommandText = " SELECT SERIE_NRO, PLACA_NRO " _
                                   & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "  " _
                                   & " WHERE SERIE_NUMERAR = " & FlexSeries.Rows(i).Cells(2).Text
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        txtNroSerie.Text = Nu(Rs("SERIE_NRO"))
                        txtNroPlaca.Text = Nu(Rs("PLACA_NRO"))
                    End While
                End If
                Rs.Close()
            Next
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Calculo_Cantidades_Recibidas()
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim i As Long = 0
        Dim Crecant As Integer
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        CmdGlobal.CommandText = "SELECT ARTICULO_CODIGO," _
             & " SUM(CASE WHEN SERIE_NRO <> '' AND (NOT (SERIE_NRO IS NULL))THEN 1 ELSE 0 END) AS CREC," _
             & " SUM(CASE WHEN SERIE_NRO = '' OR  (SERIE_NRO IS NULL) THEN 1 ELSE 0 END) AS CFREC," _
             & " SUM(CASE WHEN SERIE_NRO <> '' AND (NOT (SERIE_NRO IS NULL) AND SERIE_SOBRANTE = 'S') THEN 1 ELSE 0 END) AS CSOB, " _
             & " SUM(CASE WHEN UBICACT_TIPO <> '' AND (NOT(UBICACT_TIPO IS NULL)) THEN 1 ELSE 0 END) AS CING" _
             & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "  S INNER JOIN TBINV_ARTICULOS A ON ARTICULO_CODIGO=ART_CODIGO AND A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
             & " WHERE (SERIE_SYS_EST = '0') AND NOT(A.ART_TIPO = '87')  GROUP BY RECEP_CODIGO, ARTICULO_CODIGO HAVING (RECEP_CODIGO =" & txtIngRecepcion.Text.Trim & ")"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                CmdGlobal2.CommandText = "SELECT RECEPD_CANT_REC,RECEPD_CANT_ING From dbo.TBINV_ALMACEN_RECEPCION_DET Where (RECEP_CODIGO = " & txtIngRecepcion.Text.Trim & ") And (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & " )"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        Crecant = Nz(Rs!CREC) - Nz(Rs2!RECEPD_CANT_ING)
                    End While
                End If
                Rs2.Close()
                CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_REC=" & Nz(Rs!CREC) & ",RECEPD_CANT_FALT_REC=" & Nz(Rs!CFREC) & ",RECEPD_CANT_SOBR=" & Nz(Rs!CSOB) & ",RECEPD_CANT_RECPARCIAL=" & Nz(Crecant) & ",RECEPD_CANT_ING=" & Nz(Rs!CING) &
                                        " WHERE ARTICULO_CODIGO=" & Nu(Rs!ARTICULO_CODIGO) & "   AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND RECEP_CODIGO=" & txtIngRecepcion.Text.Trim
                CmdGlobal2.ExecuteNonQuery()
            End While
        End If
        Rs.Close()
        'CANTIDADES LOCALES
        CmdGlobal.CommandText = "SELECT SUM(RECEPD_CANT_REC) AS CREC,SUM(RECEPD_CANT_FALT_REC) AS CFREC,SUM(RECEPD_CANT_SOBR) AS CSOB FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO=" & txtIngRecepcion.Text.Trim & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_CANT_REC=" & Nz(Rs!CREC) & ",RECEP_CANT_FALT_REC=" & Nz(Rs!CFREC) & ",RECEP_CANT_SOBR=" & Nz(Rs!CSOB) &
                                        " WHERE RECEP_CODIGO=" & txtIngRecepcion.Text.Trim & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                CmdGlobal2.ExecuteNonQuery()
            End While
        End If
        Rs.Close()
        'CONTADOR POR ARTICULO Q SON CON SERIE
        For i = 0 To FlexItemSerie.Rows.Count - 1
            CmdGlobal.CommandText = "SELECT RECEPD_CANT_REC,RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING FROM TBINV_ALMACEN_RECEPCION_DET WHERE ARTICULO_CODIGO=" & FlexItemSerie.Rows(i).Cells(2).Text & " AND RECEP_CODIGO=" & txtIngRecepcion.Text.Trim & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    FlexItemSerie.Rows(i).Cells(5).Text = Nz(Rs!RECEPD_CANT_REC)
                    FlexItemSerie.Rows(i).Cells(6).Text = Nz(Rs!RECEPD_CANT_FALT_REC)
                End While
            Else
                FlexItemSerie.Rows(i).Cells(5).Text = "0"
                FlexItemSerie.Rows(i).Cells(6).Text = FlexItemSerie.Rows(i).Cells(4).Text
            End If
            Rs.Close()
        Next
        'CONTADOR POR ARTICULO Q NO NECESITA SERIE
        For i = 0 To FlexItemAcc.Rows.Count - 1
            CmdGlobal.CommandText = "SELECT RECEPD_CANT_REC,RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR FROM TBINV_ALMACEN_RECEPCION_DET WHERE ARTICULO_CODIGO=" & FlexItemAcc.Rows(i).Cells(1).Text & " AND RECEP_CODIGO=" & txtIngRecepcion.Text.Trim & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    FlexItemAcc.Rows(i).Cells(5).Text = Nz(Rs!RECEPD_CANT_REC)
                    FlexItemAcc.Rows(i).Cells(6).Text = Nz(Rs!RECEPD_CANT_FALT_REC)
                End While
            Else
                FlexItemAcc.Rows(i).Cells(5).Text = "0"
                FlexItemAcc.Rows(i).Cells(6).Text = FlexItemAcc.Rows(i).Cells(4).Text
            End If
            Rs.Close()
        Next
    End Sub
    Protected Sub btnGuardarAccCant_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardarAccCant.Click
        Dim ia As Integer
        Dim objUpd As New clsInv_InsUpdDel
        Dim pdCodRecep As Double = txtIngRecepcion.Text.Trim
        Dim pdCodArt As Double = 0
        Dim pdCantRec As Double = 0
        Dim pdCantFalta As Double = 0
        Dim pdCantParcial As Double = 0
        Dim pdCantSob As Double = 0
        If FlexItemAcc.Rows.Count = 0 Then lblError.Text = "No hay Accesorios que recibir." : Exit Sub
        For ia = 0 To FlexItemAcc.Rows.Count - 1
            pdCodArt = FlexItemAcc.Rows(ia).Cells(1).Text.Trim
            pdCantRec = FlexItemAcc.Rows(ia).Cells(5).Text.Trim
            pdCantFalta = FlexItemAcc.Rows(ia).Cells(6).Text.Trim
            objUpd.Upd_CantAccesorio(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecep,
                                     pdCodArt, pdCantRec, pdCantFalta, pdCantParcial, pdCantSob)
        Next
        Call Calculo_Cantidades_Recibidas()
    End Sub
    Protected Sub ChkRecibirAcc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRecibirAcc.CheckedChanged
        Dim ia As Integer
        If FlexItemAcc.Rows.Count = 0 Then lblError.Text = "No hay Accesorios que recibir." : Exit Sub
        For ia = 0 To FlexItemAcc.Rows.Count - 1
            If ChkRecibirAcc.Checked = True Then
                FlexItemAcc.Rows(ia).Cells(5).Text = FlexItemAcc.Rows(ia).Cells(4).Text
                FlexItemAcc.Rows(ia).Cells(6).Text = "0"
            Else
                FlexItemAcc.Rows(ia).Cells(5).Text = "0"
                FlexItemAcc.Rows(ia).Cells(6).Text = FlexItemAcc.Rows(ia).Cells(4).Text
            End If
        Next
    End Sub
    Protected Sub btnEjecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEjecutar.Click
        lblErrort.Text = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim ValorSys As String = "", TotalArt As Long = 0
        Dim EstRecep As String = "", Msm As String = ""
        Dim NOCompra As String = ""
        Dim TipoOperac As String = ""
        Dim NomOperac As String = ""
        Dim PlacaNro As Long = 0
        Dim pProveedor As String = ""
        Dim pValorVenta As String = ""
        Dim psFechaRecep As String = Right(txtIngFecha.Text, 4) + Mid(txtIngFecha.Text, 4, 2) + Left(txtIngFecha.Text, 2)
        Dim dt As DataTable
        Dim pdCodRecepcion As Double = 0
        pdCodRecepcion = Nz(txtIngRecepcion.Text.Trim)
        Dim psTipoDoc As String = ""
        Dim psPrefijo As String = ""
        Dim psMotivo As String = ""
        Dim psAlmacen As String = ""
        Dim a As Long = 0
        Dim psUSer As String = User.Identity.Name
        Dim psMotivoNombre As String = ""
        Dim psProveedor As String = ""
        Dim psCotizCompra As String = ""
        Dim psCotizacion As String = ""
        Dim psNroDocumento As String = ""
        Dim psTipoDocumento As String = ""
        Dim pdCantxRec As Double = 0
        Dim psDocSerie As String = ""
        Dim psDocSerieNro As String = ""
        '--
        Try

            pValorVenta = oFunc.Hallar_Valor_Venta(Session("Ruta_Emp"), psFechaRecep)
            dt = obj.Lista_xCodRecepcion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    psTipoDoc = Nu(dr("RECEP_TIPODOC"))
                    psPrefijo = Left(Nu(dr("RECEP_DOC_SERIE")), 2)
                    psMotivo = Nu(dr("RECEP_MOTIVO_GRAL"))
                    psAlmacen = Nu(dr("ALMACEN_CODIGO"))
                    psMotivoNombre = Nu(dr("MOTIVO_GRAL"))
                    psCotizCompra = Nu(dr("RECEP_COTIZOCOMPRA")) '19
                    psProveedor = Nu(dr("RECEP_PROVEEDOR")) '18
                    psCotizacion = Nu(dr("NCOTIZACION")) '20
                    psNroDocumento = Nu(dr("RECEP_DOC_NUMERACION")) '24
                    psTipoDocumento = Nu(dr("RECEP_TIPODOC"))
                    pdCantxRec = Nz(dr("RECEP_CANT_XREC"))
                    psDocSerie = Nu(dr("RECEP_DOC_SERIE")) & "-" & psNroDocumento
                    psDocSerieNro = Nu(dr("RECEP_DOC_SERIE"))
                    NOCompra = Nu(dr("RECEP_NRO_OC"))
                Next
            End If
            dt = Nothing
            txtGuiaSerie.Text = psDocSerieNro
            txtGuiaNro.Text = psNroDocumento
            'If pValorVenta = "" Or pValorVenta = "0.0000" Then lblErrort.Text = lblErrort.Text & "<br> - Ingresar Tipo de Cambio para Ingresar Costos de la Orden de Compra."
            If psTipoDoc = "1" And psPrefijo = "OC" Then
                If txtGuiaSerie.Text.Trim = "" And txtGuiaNro.Text.Trim = "" Then lblErrort.Text = lblErrort.Text & "<br> - Debe ingresar el Nº de Guía de Remisión."
            End If
            ValorSys = ""
            Dim Guia As String = ""
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Cn3.Open() : CmdGlobal3.Connection = Cn3

            CmdGlobal.CommandText = " SELECT RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_PROVEEDOR " _
                              & " FROM TBINV_ALMACEN_RECEPCION WHERE RECEP_CODIGO=" & pdCodRecepcion & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    TotalArt = Nz(Rs!RECEP_CANT_REC)
                    pProveedor = Nu(Rs!RECEP_PROVEEDOR)
                    If TotalArt <= 0 Then lblErrort.Text = lblErrort.Text & "<br> - No hay cantidad de artículos recibidos, no puede continuar con el proceso."
                    If Nz(Rs!RECEP_CANT_FALT_REC) = 0 And Nz(Rs!RECEP_CANT_SOBR) = 0 Then EstRecep = "2"
                    If Nz(Rs!RECEP_CANT_FALT_REC) > 0 And Nz(Rs!RECEP_CANT_SOBR) = 0 Then EstRecep = "3" : Msm = "por la razón de tener faltantes."
                    If Nz(Rs!RECEP_CANT_FALT_REC) = 0 And Nz(Rs!RECEP_CANT_SOBR) > 0 Then EstRecep = "3" : Msm = "por la razón de tener sobrantes."
                    If Nz(Rs!RECEP_CANT_FALT_REC) > 0 And Nz(Rs!RECEP_CANT_SOBR) > 0 Then EstRecep = "3" : Msm = "por la razón de tener faltantes y sobrantes."
                End While
            Else
                Exit Sub
            End If
            Rs.Close()
            'datos para el movimiento
            Dim objCont As New clsCont_Funciones
            Dim psAño As String = ""
            Dim psPeriodo As String = ""
            Dim psCodTrans As String = ""
            psAño = objCont.AñoSistema(Session("Ruta_Emp"), Session("CodEmpresa"))
            psPeriodo = ""
            CmdGlobal.CommandText = "SELECT PER_PERIODO FROM TBPERIODIFICACION WHERE (PER_EMPRESA = '" & Session("CodEmpresa") & "') AND (PER_AÑO = '" & psAño & "') AND (PER_ACTUAL = 'S') AND (PER_SYS_EST = '0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psPeriodo = Nu(Rs!PER_PERIODO)
                End While
            End If
            Rs.Close()
            If psPeriodo = "" Then lblErrort.Text = lblErrort.Text & "<br> - No se ha podido encontrar periodo contable actual."
            psCodTrans = ""
            If lblErrort.Text <> "" Then
                lblErrort.Text = lblErrort.Text
                Exit Sub
            End If
            If psMotivo = "8" And IsNumeric(psNroDocumento) = True Then
                Call oFuncInv.Actualizar_CostoArticulo(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion, "1", "8", "1", psAlmacen, pValorVenta, Right(psDocSerie, 6))
            End If
            Dim psVale As String = ""
            Dim psCodTipoTrans As String = "0"
            Dim psCodMov As String = ""
            Dim psIngresoNumerar As String = ""
            Dim psNroMovimiento As String = ""
            Dim CodOrdenN As String = ""
            Select Case psMotivo
                Case "1" : TipoOperac = "TRANS_INGXCAMBIO" : NomOperac = "Ingreso x Cambio"
                Case "2" : TipoOperac = "TRANS_INGXMANTE" : NomOperac = "Ingreso x Reparación"
                Case "3" : TipoOperac = "TRANS_INGXCAMBIO" : NomOperac = "Ingreso x Devolución"
                Case "4" : TipoOperac = "TRANS_INGXDEMOS" : NomOperac = "Ingreso x Demostración"
                Case "5" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Traslado"
                Case "6" : TipoOperac = "TRANS_INGXPRESTA" : NomOperac = "Ingreso x Prestamo"
                Case "7" : TipoOperac = "TRANS_INGXDEVOL" : NomOperac = "ngreso x Devolución" '--
                Case "8" : TipoOperac = "TRANS_INGXCOMPRA" : NomOperac = "Ingreso x Compra"
                Case "9" : TipoOperac = "TRANS_INGXCOMPRA" : NomOperac = "Ingreso x Donación"
                Case "10" : TipoOperac = "TRANS_INGXCOMPRA" : NomOperac = "Ingreso x Alquiler"
                Case "11" : TipoOperac = "TRANS_INGXPRESTA" : NomOperac = "Ingreso x Respaldo"
                Case "12" : TipoOperac = "TRANS_INGXCAMBIO" : NomOperac = "Ingreso de un Equipo"
                Case "13" : TipoOperac = "TRANS_INGXMANTE" : NomOperac = "Ingreso x Devolución por Reparación"
                Case "14" : TipoOperac = "TRANS_INGXCAMBIO" : NomOperac = "Ingreso x Averia"
                Case "15" : TipoOperac = "TRANS_INGXDEMOS" : NomOperac = "Ingreso x Devolución por Amortización"
                Case "16" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Cambio por Proveedor"
                Case "17" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Demostracion"
                Case "18" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Baja"
                Case "20" : TipoOperac = "TRANS_INGXPRESTA" : NomOperac = "Ingreso x Inventario"
                Case "21" : TipoOperac = "TRANS_INGXDEVOL" : NomOperac = "Ingreso x Componente" '--
                Case "22" : TipoOperac = "TRANS_INGXCOMPRA" : NomOperac = "Ingreso x Anulación"
                Case "23" : TipoOperac = "TRANS_INGXCOMPRA" : NomOperac = "Ingreso x Regularización"
                Case "24" : TipoOperac = "TRANS_INGXCOMPRA" : NomOperac = "Ingreso x Devolucion en Mantenimiento en Proveedor"
                Case "25" : TipoOperac = "TRANS_INGXPRESTA" : NomOperac = "Ingreso x Devolución Definitiva a Proveedor"
                Case "27" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Traslado"
                Case "29" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Importación"
                Case "30" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Traslado"
                Case "31" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Fabricación"
                Case "32" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Nacionalización"
                Case "41" : TipoOperac = "TRANS_INGXTRANSF" : NomOperac = "Ingreso x Distribución"
            End Select
            CmdGlobal.CommandText = "SELECT TRANS_CODIGO,TRANS_DESCRIPCION FROM TBINV_TRANSACCIONES_ALMACEN WHERE TRANS_SYS_EST='0' AND " _
            & " TRANS_TIPO='" & psCodTipoTrans & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND " & TipoOperac & "='S' ORDER BY TRANS_CODIGO"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    psCodTrans = Nu(Rs!TRANS_CODIGO)
                End While
            End If
            Rs.Close()
            If psCodTrans = "" Then lblError.Text = lblError.Text & "<br> - No se ha podido hallar transacción " & NomOperac & "."
            psVale = ""
            CmdGlobal.CommandText = "SELECT MAX(M.MOVAL_NRO_VALE) FROM TBINV_MOVIMIENTOS_ALMACEN M INNER JOIN TBINV_TRANSACCIONES_ALMACEN T ON M.TRANS_CODIGO = T.TRANS_CODIGO AND M.EMPRESA_CODIGO=T.EMPRESA_CODIGO " _
        & " WHERE (T.TRANS_TIPO = '" & psCodTipoTrans & "') AND (M.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (ALMACEN_CODIGO='" & txtIngCodAlmacen.Text.Trim & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    psVale = Llenar_Ceros(Nu(Nz(Rs(0)) + 1), 8)
                End While
            Else
                psVale = "00000001"
            End If
            Rs.Close()
            CmdGlobal.CommandText = "SELECT MAX(MOVAL_CODIGO) FROM TBINV_MOVIMIENTOS_ALMACEN WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    psCodMov = Format(Nz(Rs(0)) + 1, "00000000")
                End While
            Else
                psCodMov = "00000001"
            End If
            Rs.Close()
            Dim psCodTransD As String = ""
            CmdGlobal.CommandText = "SELECT TRANSD_CODIGO,TRANSD_VALOR FROM TBINV_TRANS_ALMACEN_DETALLE WHERE (TRANSD_DETALLE = '2') AND " _
        & " (TRANS_CODIGO = " & psCodTrans & ") AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' ORDER BY TRANSD_CODIGO"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    psCodTransD = Nu(Rs!TRANSD_CODIGO)
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTOS_ALMACEN(EMPRESA_CODIGO, MOVAL_CODIGO,ALMACEN_CODIGO,MOVAL_SYS_EST,MOVAL_SYS_CRE) " _
                              & "VALUES('" & Session("CodEmpresa") & "'," & psCodMov & ",'" & txtIngCodAlmacen.Text.Trim & "','0','" & ValorSys & "')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "UPDATE TBINV_MOVIMIENTOS_ALMACEN SET CONTABLE_AÑO='" & psAño & "',CONTABLE_PERIODO=" & psPeriodo & "," _
                              & "TRANS_CODIGO=" & psCodTrans & ",MOVAL_NRO_VALE='" & psVale & "',MOVAL_FECHA='" & FechaActual() & "'," _
                              & "MOVAL_SYS_MOD='" & ValorSys & "',MOVAL_TOTAL_ART=" & TotalArt & " WHERE MOVAL_CODIGO=" & psCodMov & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            CmdGlobal.ExecuteNonQuery()
            'INSERTAR O ACTUALIZAR EL DETALLE ARTICULOS
            a = 0
            CmdGlobal.CommandText = "SELECT ARTICULO_CODIGO,RECEPD_CANT_REC,RECEPD_CANT_ING FROM TBINV_ALMACEN_RECEPCION_DET WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RECEP_CODIGO =" & pdCodRecepcion & ") AND (RECEPD_SYS_EST = '0')  ORDER BY RECEPD_ITEM"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    If CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING)) > 0 Then
                        'GUARDADA MOV ALMACEN
                        a = a + 1
                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOV_ALMACEN_ARTICULOS WHERE (MOVAL_CODIGO =" & psCodMov & ") AND (ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (MOVALA_SYS_EST='0')"
                        Rs2 = CmdGlobal2.ExecuteReader
                        If Rs2.HasRows = True Then
                            CmdGlobal3.CommandText = "UPDATE TBINV_MOV_ALMACEN_ARTICULOS SET MOVALA_ART_CANTIDAD=" & Nz(Rs2!MOVALA_ART_CANTIDAD) + CDbl(Nz(Rs!RECEPD_CANT_REC)) & ",MOVALA_ART_ORDEN=" & a & " WHERE (MOVAL_CODIGO =" & psCodMov & ") AND (ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (MOVALA_SYS_EST='0')"
                            CmdGlobal3.ExecuteNonQuery()
                        Else
                            CmdGlobal3.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_ARTICULOS(MOVAL_CODIGO, ARTICULO_CODIGO,MOVALA_ART_CANTIDAD, MOVALA_ART_ORDEN,EMPRESA_CODIGO,MOVALA_SYS_EST) " _
                                              & "VALUES(" & psCodMov & "," & Nz(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!RECEPD_CANT_REC) & "," & a & ",'" & Session("CodEmpresa") & "','0')"
                            CmdGlobal3.ExecuteNonQuery()
                        End If
                        Rs2.Close()
                        Dim StockAc As Double
                        CmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & txtIngCodAlmacen.Text.Trim & ") AND (UBICACT_TIPO='1') " _
                                          & " AND (ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs2 = CmdGlobal2.ExecuteReader
                        If Rs2.HasRows = True Then
                            While Rs2.Read
                                StockAc = Nz(Rs2!SAA_STOCK_ACTUAL)
                                If psCodTipoTrans = "0" Then  'INGRESO
                                    StockAc = StockAc + CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING))
                                Else 'SALIDA
                                    StockAc = StockAc - CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING))
                                End If
                                CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & txtIngCodAlmacen.Text.Trim & ") AND (UBICACT_TIPO='1')" _
                                                   & " AND (ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO, ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                              & "VALUES('1'," & psAlmacen & "," & Nz(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!RECEPD_CANT_REC) & ",'0','" & Session("CodEmpresa") & "')"
                            CmdGlobal3.ExecuteNonQuery()
                        End If
                        Rs2.Close()
                        CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_ING = " & Nz(Rs!RECEPD_CANT_REC) & "  " _
                                          & " WHERE RECEP_CODIGO=" & pdCodRecepcion & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND ARTICULO_CODIGO=" & Nu(Rs!ARTICULO_CODIGO) & " AND (RECEPD_SYS_EST = '0')"
                        CmdGlobal2.ExecuteNonQuery()
                        'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                        If CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING)) > 0 Then
                            If psMotivo = "8" Then 'COMPRA
                                CmdGlobal2.CommandText = "SELECT MAX(INGXC_NUMERAR) FROM TBINV_MOV_ALMACEN_INGXCOMPRA2 WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs2 = CmdGlobal2.ExecuteReader
                                If Rs2.HasRows = True Then
                                    While Rs2.Read
                                        psIngresoNumerar = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    psIngresoNumerar = "1"
                                End If
                                Rs2.Close()
                                CmdGlobal3.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_INGXCOMPRA2(EMPRESA_CODIGO,INGXC_NUMERAR,RECEP_CODIGO," _
                                                & "ART_CODIGO, CANT_RECIBIDA,ALMACEN_MOV_CODIGO,USUARIO,FECHA,HORA) " _
                                                & "VALUES('" & Session("CodEmpresa") & "'," & psIngresoNumerar & "," & pdCodRecepcion & "," _
                                                & Nz(Rs!ARTICULO_CODIGO) & "," & CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING)) & "," & psCodMov & ",'" & psUSer & "','" & FechaActual() & "','" & HoraActual() & "')"
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            CmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            Rs2 = CmdGlobal2.ExecuteReader
                            If Rs2.HasRows = True Then
                                While Rs2.Read
                                    psNroMovimiento = Nz(Rs2(0)) + 1
                                End While
                            Else
                                psNroMovimiento = "00000001"
                            End If
                            Rs2.Close()
                            '1: INGRESO, 2:SALIDA
                            Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion, psMotivo, Nu(Rs!ARTICULO_CODIGO), "1", txtIngCodAlmacen.Text.Trim, "3", psProveedor, "1", "1", txtIngFecha.Text.Trim, CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING)))
                            CmdGlobal2.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                               & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','1','1','" & txtIngCodAlmacen.Text.Trim & "','3','" & psProveedor & "','" & pdCodRecepcion & "','" & Nz(Rs!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs!RECEPD_CANT_REC) - Nz(Rs!RECEPD_CANT_ING)) & "','" & ValorSys & "','" & EstRecep & "','" & psMotivo & "', '" & psFechaRecep & "','0')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                    End If
                End While
            End If
            Rs.Close()
            Dim EsAduana As Boolean
            EsAduana = False
            CmdGlobal.CommandText = "SELECT ALMACEN_MOVIL FROM TBINV_ALMACENES WHERE ALMACEN_CODIGO='" & txtIngCodAlmacen.Text.Trim & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND ALMACEN_SYS_EST='0' and almacen_movil='2'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    EsAduana = True
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " SELECT ARTICULO_CODIGO,SERIE_NUMERAR, SERIE_NRO " _
            & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") &
              " WHERE (RECEP_CODIGO =" & pdCodRecepcion & ") AND (SERIE_SYS_EST = '0')" ' AND (SERIE_NRO <> '' AND NOT (SERIE_NRO IS NULL))"
            If EsAduana = False Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND (SERIE_NRO <> '' AND NOT (SERIE_NRO IS NULL)) AND (UBICACT_TIPO IS NULL) AND (UBICACT_CODIGO IS NULL)"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    CmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & txtIngCodAlmacen.Text.Trim & ",UBICACT_SYS='" & ValorSys & "'" _
                                      & " WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                    CmdGlobal2.ExecuteNonQuery()
                    CmdGlobal2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL) " _
                                      & "VALUES(" & Nz(Rs!Serie_Numerar) & ",'1'," & txtIngCodAlmacen.Text.Trim & ",'0','0','" & ValorSys & "','" & FechaActual() & "','3'," & pdCodRecepcion & ")"
                    CmdGlobal2.ExecuteNonQuery()
                    If psMotivo = "2" Then 'MANTENIMIENTO: TIPO(0 INGRESO,1 SALIDA); TIPO_DOC(R ORECEPCION, D ODESPACHO, A OSALIDA); TIPO_ORIGEN(0 SIN ORIGEN,1 ALMACEN, 2 CCOSTO); MANT_ESTADO(1 POR REVISAR,2 REVISADO)
                        CmdGlobal2.CommandText = "SELECT MAX(MANT_CODIGO) FROM TBINV_MOV_ALMACEN_MANTENIMIENTO_" & Session("CodEmpresa") & " "
                        Rs2 = CmdGlobal2.ExecuteReader
                        If Rs2.HasRows = True Then
                            While Rs2.Read
                                psIngresoNumerar = Nz(Rs(0)) + 1
                            End While
                        Else
                            psIngresoNumerar = "1"
                        End If
                        Rs2.Close()
                        CmdGlobal2.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_MANTENIMIENTO_" & Session("CodEmpresa") & " (MANT_CODIGO, MANT_TIPO, MANT_DOCUME_TIPO, MANT_DOCUME_CODIGO, ALMACEN_DESTINO, MANT_ORIGEN_TIPO," _
                                          & "MANT_ORIGEN_CODIGO, SERIE_NUMERAR,MANT_FECHA_ING, MANT_HORA_ING, MANT_USUARIO_ING, MANT_SYS_EST,MANT_ESTADO) VALUES(" _
                                          & psIngresoNumerar & ",'0','R'," & pdCodRecepcion & "," & txtIngCodAlmacen.Text.Trim & ",'0'," _
                                          & "NULL," & Nz(Rs!Serie_Numerar) & ",'" & FechaActual() & "','" & HoraActual() & "','" & psUSer & "','0','1')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " SELECT SERIE_NUMERAR, SERIE_NRO FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON A.ART_CODIGO = S.ARTICULO_CODIGO AND A.ART_SYS_EST ='0'  " _
                              & " WHERE (RECEP_CODIGO =" & pdCodRecepcion & ") AND (SERIE_SYS_EST = '0') AND (SERIE_NRO IS NULL) AND (A.ART_TIPO='88')  AND (UBICACT_TIPO IS NULL) AND (UBICACT_CODIGO IS NULL)"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    CmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & txtIngCodAlmacen.Text.Trim & ",UBICACT_SYS='" & ValorSys & "'" _
                                      & " WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                    CmdGlobal2.ExecuteNonQuery()
                    CmdGlobal2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL) " _
                                      & "VALUES(" & Nz(Rs!Serie_Numerar) & ",'1'," & txtIngCodAlmacen.Text.Trim & ",'0','0','" & ValorSys & "','" & FechaActual() & "','3'," & pdCodRecepcion & ")"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET " _
                              & " RECEP_ESTADO='" & EstRecep & "', " _
                              & " RECEP_FECHA_REC = '" & psFechaRecep & "', " _
                              & " RECEP_SYS_REC = '" & ValorSys & "' " _
                              & " WHERE RECEP_CODIGO=" & pdCodRecepcion & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            CmdGlobal.ExecuteNonQuery()
            If Existe_Tabla("TBIMPORTACION_SEGUIMIENTO", Session("Ruta_Emp")) = True And Existe_Tabla("TBIMPORTACION_NACIONALIZAR_DEPOSITO", Session("Ruta_Emp")) = True Then
                CmdGlobal.CommandText = " SELECT ORDEN_NACIONALIZACION FROM TBIMPORTACION_SEGUIMIENTO WHERE RECEP_CODIGO =" & pdCodRecepcion
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows = True Then
                    While Rs.Read
                        CodOrdenN = Nu(Rs!ORDEN_NACIONALIZACION)
                    End While
                End If
                Rs.Close()
                If CodOrdenN <> "" Then
                    CmdGlobal.CommandText = " UPDATE TBIMPORTACION_NACIONALIZAR_DEPOSITO SET ORDENNAC_ESTADO='3' " _
                                      & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND ORDENNAC_SYS_EST='0' AND " _
                                      & " ORDENNAC_ESTADO='2' AND ORDENNAC_CODIGO=" & CodOrdenN & " AND DESP_CODIGO =" & pdCodRecepcion
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If
            Dim TodoCoti As Integer : TodoCoti = 0
            Dim CantCoti As Integer : CantCoti = 0
            a = 0
            If Existe_Tabla("TBVENTAS_ORDENCOMPRA", Session("Ruta_Emp")) = True Then
                If psCotizCompra <> "" And psCotizacion <> "" Then
                    CmdGlobal.CommandText = "UPDATE TBVENTAS_ORDENCOMPRA SET ESTADO_RECEP='" & EstRecep & "' WHERE ORDEN_COMPRA=" & psCotizCompra & " AND NRO_COTIZACION =  " & psCotizacion & " AND NRO_RECEPCION=" & pdCodRecepcion
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " SELECT ORDEN_COMPRA,NRO_RECEPCION,ESTADO_RECEP FROM TBVENTAS_ORDENCOMPRA WHERE NRO_COTIZACION =  " & psCotizacion
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows = True Then
                        While Rs.Read
                            CantCoti = a + 1
                            If Nu(Rs!ESTADO_RECEP) = "2" Then TodoCoti = TodoCoti + 1
                        End While
                    End If
                    Rs.Close()
                    If TodoCoti = CantCoti Then
                        CmdGlobal.CommandText = "UPDATE TBVENTAS_COTIZACION SET ESTADO = '7' WHERE NRO_COTIZACION=  " & psCotizacion & "  AND ESTADO='6'"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                End If
            End If
            If Existe_Tabla("TBLOGIS_ORDENES_COMPRA", Session("Ruta_Emp")) = True Then Call CambiaEstadoRequisYPedidoYOCompra(pdCodRecepcion)
            CmdGlobal.CommandText = "DELETE FROM TBINV_MOV_ALMACEN_REFERENCIA WHERE MOVAL_CODIGO=" & psCodMov & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            CmdGlobal.ExecuteNonQuery()
            If psCodTransD <> "" Then
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_REFERENCIA(MOVAL_CODIGO, TRANS_CODIGO, TRANS_REF_CODIGO,MOVALREF_VALOR,EMPRESA_CODIGO,MOVALREF_SYS_EST) " _
                                  & "VALUES(" & psCodMov & "," & psCodTrans & "," & psCodTransD & ",'" & pdCodRecepcion & "','" & Session("CodEmpresa") & "','0')"
                CmdGlobal.ExecuteNonQuery()
            End If
            '''''''GENERAR SALIDA A PRODUCCION
            'Dim pGenProduc As Boolean : pGenProduc = False
            'Dim psOCompra As String = ""
            'Dim psMonedaOC As String = ""
            'If Existe_Tabla("TBLOGIS_ORDENES_COMPRA", Session("Ruta_Emp")) = True Then
            '    CmdGlobal.CommandText = " SELECT DISTINCT OC.OCOMPRA_CODIGO , OC.OCOMPRA_NUMERAR, RD.DESTINO_TIPO, RD.DESTINO_CODIGO, A.ART_TIPO, A.ART_CODIGO, OC.OCOMPRA_ORDENTRABAJO, R.RECEP_CANT_XREC, R.RECEP_CANT_REC, R.RECEP_CANT_FALT_REC " _
            '      & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
            '      & " dbo.TBLOGIS_ORDENES_COMPRA OC ON R.RECEP_COTIZOCOMPRA = OC.OCOMPRA_CODIGO AND  R.EMPRESA_CODIGO = OC.EMPRESA_CODIGO INNER JOIN " _
            '      & " dbo.TBLOGIS_ORDENES_COMPRA_DETALLE OCD ON OC.OCOMPRA_NUMERAR = OCD.OCOMPRA_NUMERAR AND OC.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO INNER JOIN " _
            '      & " dbo.TBLOGIS_REQUISICION_DETALLE RD ON OCD.OCOMPRAD_REQUISD_NUMERAR = RD.REQUISD_NUMERAR AND  OCD.EMPRESA_CODIGO = RD.EMPRESA_CODIGO INNER JOIN " _
            '      & " dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON RD.DESTINO_CODIGO = S.SERIE_NUMERAR INNER JOIN " _
            '      & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
            '      & " WHERE (RD.DESTINO_TIPO = '4') AND (OC.OCOMPRA_CODIGO = " & Nz(NOCompra) & ") AND (A.ART_TIPO = 89) AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND " _
            '      & " (RD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (OCD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (OC.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (R.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
            '      & " AND (R.RECEP_SYS_EST = '0') AND (OC.OCOMPRA_SYS_EST = '0') AND (RD.REQUISD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (A.ART_SYS_EST = '0') "
            '    Rs = CmdGlobal.ExecuteReader
            '    If Rs.HasRows = True Then pGenProduc = True
            '    Rs.Close()
            '    If pGenProduc = True Then Call oFuncInv.GenerarSalidaProduccion(Session("Ruta_Emp"), Session("CodEmpresa"), "", 0, txtIngCodAlmacen.Text.Trim, psUSer)
            '    ''''''Cambiar estado a la orden de compra de autorizada a recibida en almacen
            '    If psTipoDocumento = "1" Then psOCompra = Nz(NOCompra)
            '    If psOCompra <> "" Then
            '        CmdGlobal.CommandText = "UPDATE TBLOGIS_ORDENES_COMPRA SET OCOMPRA_ESTADO='6' " _
            '                          & "WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OCOMPRA_ESTADO = '1' AND OCOMPRA_NUMERAR=" & Nz(NOCompra)
            '        CmdGlobal.ExecuteNonQuery()
            '    End If
            'End If
            ''''''ASIENTO CONTABLE COSTO VENTA
            Dim psBaseImponible As String = ""
            Dim pdTotal As Double = 0
            Dim pMoneda As String = ""
            Dim psGenera As Boolean : psGenera = False
            Dim PsFaltaRecep As Boolean : PsFaltaRecep = False
            Dim psestOk As Boolean : psestOk = False
            Dim CantP As Boolean : CantP = False
            Dim i As Long = 0
            CmdGlobal.CommandText = " SELECT  EMPRESA_CODIGO, RECEP_CODIGO, RECEP_ESTADO From dbo.TBINV_ALMACEN_RECEPCION WHERE (RECEP_CODIGO = " & pdCodRecepcion & ") AND (RECEP_ESTADO = '2') AND (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then psestOk = True
            Rs.Close()
            CmdGlobal.CommandText = "SELECT SUM(RECEPD_CANT_RECPARCIAL) AS CPARCIAL FROM dbo.TBINV_ALMACEN_RECEPCION_DET AS D WHERE (RECEP_CODIGO =" & pdCodRecepcion & ") AND (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    If pdCantxRec = CDbl(Nz(Rs!CPARCIAL)) Then CantP = True
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " SELECT RECEPD_CANT_FALT_REC From dbo.TBINV_ALMACEN_RECEPCION_DET " _
            & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND RECEP_CODIGO = " & pdCodRecepcion & " AND  (RECEPD_CANT_RECPARCIAL > 0)"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then PsFaltaRecep = True
            Rs.Close()
            CmdGlobal.CommandText = " SELECT RECEP_CODIGO,RECEP_CONASIENTO,RECEP_ESTADO FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO ='" & Session("CodEmpresa") & "' and RECEP_CODIGO=" & pdCodRecepcion
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    If (Nu(Rs!RECEP_CONASIENTO) <> "SI") Then psGenera = True
                End While
            End If
            Rs.Close()
            If PsFaltaRecep = True And CantP = False Then
                CmdGlobal.CommandText = " SELECT D.OCOMPRAD_ARTICULO, D.OCOMPRAD_PRECIO_UNIT,  C.OCOMPRA_MONEDA " _
                & " FROM dbo.TBLOGIS_ORDENES_COMPRA AS C INNER JOIN dbo.TBLOGIS_ORDENES_COMPRA_DETALLE AS D ON C.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND c.OCOMPRA_NUMERAR = d.OCOMPRA_NUMERAR " _
                & " INNER JOIN TBLOGIS_ORDENES_COMPRA_RECEPCION OCR ON C.OCOMPRA_NUMERAR = OCR.OCOMPRA_NUMERAR " _
                & " WHERE (OCR.RECEP_CODIGO = " & pdCodRecepcion & ") AND (C.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows = True Then
                    While Rs.Read
                        CmdGlobal.CommandText = " SELECT RECEPD_CANT_RECPARCIAL From dbo.TBINV_ALMACEN_RECEPCION_DET " _
                        & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RECEP_CODIGO = " & pdCodRecepcion & ") AND (ARTICULO_CODIGO = " & (Rs!OCOMPRAD_ARTICULO) & ") AND (RECEPD_CANT_RECPARCIAL > 0) "
                        Rs2 = CmdGlobal2.ExecuteReader
                        If Rs2.HasRows = True Then
                            While Rs2.Read
                                pMoneda = Nu(Rs!OCOMPRA_MONEDA)
                                pdTotal = pdTotal + CDbl(Nz(Rs2!RECEPD_CANT_RECPARCIAL)) * CDbl(Nz(Rs!OCOMPRAD_PRECIO_UNIT))
                            End While
                        End If
                        Rs2.Close()
                    End While
                End If
                Rs.Close()
            Else
                CmdGlobal.CommandText = " SELECT OC.OCOMPRA_MONEDA,OC.OCOMPRA_NUMERAR, OC.OCOMPRA_RECEP_CODIGO, OCM.OCOMPRAD_ARTICULO, OCM.ORDEN, OCM.ETIQUETA, OCM.VALOR" _
                & " FROM dbo.TBLOGIS_ORDENES_COMPRA AS OC INNER JOIN dbo.TBLOGIS_ORDENES_COMPRA_DET_MONTOS AS OCM " _
                & " ON OC.EMPRESA_CODIGO = OCM.EMPRESA_CODIGO AND OC.OCOMPRA_NUMERAR = OCM.OCOMPRA_NUMERAR" _
                & " INNER JOIN TBLOGIS_ORDENES_COMPRA_RECEPCION OCR ON OCR.OCOMPRA_NUMERAR = OC.OCOMPRA_NUMERAR " _
                & " Where (OCR.RECEP_CODIGO = " & pdCodRecepcion & ") And (OCM.Orden = 4)"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows = True Then
                    While Rs.Read
                        pMoneda = Nu(Rs!OCOMPRA_MONEDA)
                        pdTotal = pdTotal + CDbl(Nz(Rs!Valor))
                    End While
                End If
                Rs.Close()
            End If
            psBaseImponible = Format(pdTotal, "0.00")
            If pdTotal > 0 Then
                'Call oFunc.Generar_Comprobante("3", txtGuiaSerie.Text.Trim & " - " & txtGuiaNro.Text.Trim, psProveedor, "09", pMoneda, 0, 0, psBaseImponible, pValorVenta, txtIngFecha.Text.Trim, "", txtIngFecha.Text.Trim, Session("SiglaGrupoEmpresa"), "COSTO POR COMPRA", "16", "I", "", "", "", "", "", "", "", "", "", txtIngFecha.Text.Trim, "", "", "", "", "", "", "", "", psAño, "", "", "", pdCodRecepcion)
                CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_CONASIENTO='SI' " _
                                  & " WHERE RECEP_CODIGO=" & pdCodRecepcion
                CmdGlobal.ExecuteNonQuery()
            End If
            Dim pCostoSol As Double : pCostoSol = 0
            Dim pCostoDol As Double : pCostoDol = 0
            If Existe_Tabla("TBPRECIOS_COSTOSARTICULOS", Session("Ruta_Emp")) = True And (EstRecep = "2" Or EstRecep = "3") Then
                CmdGlobal.CommandText = " DELETE FROM TBPRECIOS_COSTOSARTICULOS WHERE RECEP_CODIGO = " & pdCodRecepcion & " "
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " SELECT OC.OCOMPRA_RECEP_CODIGO, OCD.OCOMPRAD_ARTICULO, OCD.OCOMPRAD_PRECIO_UNIT, OC.OCOMPRA_MONEDA " _
                & " FROM dbo.TBLOGIS_ORDENES_COMPRA AS OC INNER JOIN dbo.TBLOGIS_ORDENES_COMPRA_DETALLE AS OCD ON " _
                & " OC.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO AND OC.OCOMPRA_NUMERAR = OCD.OCOMPRA_NUMERAR " _
                & " WHERE (OC.OCOMPRA_RECEP_CODIGO = " & pdCodRecepcion & ") AND (OC.OCOMPRA_SYS_EST = '0') " _
                & " AND (OC.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (OCD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows = True Then
                    While Rs.Read
                        For i = 0 To FlexItemAcc.Rows.Count - 1
                            If FlexItemAcc.Rows(i).Cells(1).Text = Format(Nu(Rs!OCOMPRAD_ARTICULO), "00000000") Then
                                If Nu(Rs!OCOMPRA_MONEDA) = "1" Then
                                    pCostoSol = Nz(Rs!OCOMPRAD_PRECIO_UNIT) * pValorVenta
                                    pCostoDol = Nz(Rs!OCOMPRAD_PRECIO_UNIT)
                                ElseIf Nu(Rs!OCOMPRA_MONEDA) = "2" Then
                                    pCostoSol = Nz(Rs!OCOMPRAD_PRECIO_UNIT)
                                    pCostoDol = Nz(Rs!OCOMPRAD_PRECIO_UNIT) / pValorVenta
                                End If
                                CmdGlobal.CommandText = " INSERT INTO TBPRECIOS_COSTOSARTICULOS (EMPRESA_CODIGO ,RECEP_CODIGO,ARTICULO_CODIGO,PRECIO_FECHA,PRECIO_COSTO_S,PRECIO_COSTO_D,TIPO_CAMBIO_VENTA,TIPO_MONEDA) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCodRecepcion & "," & Nu(Rs!OCOMPRAD_ARTICULO) & ",'" & psFechaRecep & "'," & Format(pCostoSol, "0.000000") & "," & Format(pCostoDol, "0.000000") & "," & pValorVenta & ",'" & Nu(Rs!OCOMPRA_MONEDA) & "')"
                                CmdGlobal.ExecuteNonQuery()
                                Exit For
                            End If
                        Next
                        For i = 0 To FlexItemSerie.Rows.Count - 1
                            If FlexItemSerie.Rows(i).Cells(1).Text = Format(Nu(Rs!OCOMPRAD_ARTICULO), "00000000") Then
                                If Nu(Rs!OCOMPRA_MONEDA) = "1" Then
                                    pCostoSol = Nz(Rs!OCOMPRAD_PRECIO_UNIT) * pValorVenta
                                    pCostoDol = Nz(Rs!OCOMPRAD_PRECIO_UNIT)
                                ElseIf Nu(Rs!OCOMPRA_MONEDA) = "2" Then
                                    pCostoSol = Nz(Rs!OCOMPRAD_PRECIO_UNIT)
                                    pCostoDol = Nz(Rs!OCOMPRAD_PRECIO_UNIT) / pValorVenta
                                End If
                                CmdGlobal.CommandText = " INSERT INTO TBPRECIOS_COSTOSARTICULOS (EMPRESA_CODIGO ,RECEP_CODIGO,ARTICULO_CODIGO,PRECIO_FECHA,PRECIO_COSTO_S,PRECIO_COSTO_D,TIPO_CAMBIO_VENTA,TIPO_MONEDA) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCodRecepcion & "," & Nu(Rs!OCOMPRAD_ARTICULO) & ",'" & psFechaRecep & "'," & Format(pCostoSol, "0.000000") & "," & Format(pCostoDol, "0.000000") & "," & pValorVenta & ",'" & Nu(Rs!OCOMPRA_MONEDA) & "')"
                                CmdGlobal.ExecuteNonQuery()
                                Exit For
                            End If
                        Next
                    End While
                End If
                Rs.Close()
            End If
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True

            BtnRegresar2_Click(sender, e)
            Call Limpiar_Controles()
        Catch ex As SqlException
            lblErrort.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            lblErrort.Text = "Ha ocurrido un error en la aplicacion:" & ex.Message
        End Try
    End Sub
    Private Sub Limpiar_Controles()
        FlexItem.DataSource = Nothing
        FlexItem.DataBind()
        cboAlmacen.SelectedValue = "< Seleccionar >"
        cboMotivo.SelectedValue = "< Seleccionar >"
        cboPropietario.SelectedValue = "< Seleccionar >"
        cboProyecto.SelectedValue = "< Seleccionar >"
        cboAño.SelectedValue = "< Seleccionar >"
        cboTipoDoc.SelectedValue = "< Seleccionar >"
        txtFecRegistra.Text = FormatoFecha(FechaActual)
        txtHoraRegistra.Text = FormatoHora(HoraActual)
        txtUserRegistra.Text = Mid(Session("UserNombre"), 14)
        txtGuiaNro.Text = ""
        txtGuiaSerie.Text = ""
        txtIngAlmacen.Text = ""
        txtIngArtCodigo.Text = ""
        txtIngArticulo.Text = ""
        txtIngCodAlmacen.Text = ""
        txtIngFecha.Text = ""
        txtIngProveedor.Text = ""
        txtIngRecepcion.Text = ""
        txtNroDoc.Text = ""
        txtNroOC.Text = ""
        txtNroRecepcion.Text = ""
        txtProvCodigo.Text = ""
        txtProvNombre.Text = ""
        txtProvRuc.Text = ""
        txtRazonSocialTipoPers.Text = ""
        txtReferencia.Text = ""
        txtRucTipoPers.Text = ""
        txtSerieDoc.Text = ""
        FlexItemAcc.DataSource = Nothing
        FlexItemAcc.DataBind()
        FlexItemSerie.DataSource = Nothing
        FlexItemSerie.DataBind()
        FlexSeries.DataSource = Nothing
        FlexSeries.DataBind()
        FlexTipoPers.DataSource = Nothing
        FlexTipoPers.DataBind()
        lblError.Text = ""
        lblErrort.Text = ""
    End Sub
    Private Sub CambiaEstadoRequisYPedidoYOCompra(ByVal NRecepcion As String)
        Dim CnA As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CnB As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CnC As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobalA As New SqlCommand
        Dim CmdGlobalB As New SqlCommand
        Dim CmdGlobalC As New SqlCommand
        Dim RsOC As SqlDataReader
        Dim RsOR As SqlDataReader
        Dim Cantidad As Long
        Dim psUSer As String = User.Identity.Name
        CnA.Open() : CmdGlobalA.Connection = CnA
        CnB.Open() : CmdGlobalB.Connection = CnB
        CnC.Open() : CmdGlobalC.Connection = CnC
        CmdGlobalA.CommandText = " SELECT OCR.OCOMPRA_NUMERAR, RD.ARTICULO_CODIGO, RD.RECEPD_CANT_REC, RD.RECEPD_CANT_FALT_REC" _
            & " FROM TBINV_ALMACEN_RECEPCION R INNER JOIN TBINV_ALMACEN_RECEPCION_DET RD ON R.EMPRESA_CODIGO = RD.EMPRESA_CODIGO AND R.RECEP_CODIGO = RD.RECEP_CODIGO " _
            & " INNER JOIN TBLOGIS_ORDENES_COMPRA_RECEPCION OCR ON OCR.RECEP_CODIGO = R.RECEP_CODIGO and R.EMPRESA_CODIGO = ocR.EMPRESA_CODIGO" _
            & " WHERE (OCr.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (OCR.RECEP_CODIGO =" & NRecepcion & ")"
        RsOR = CmdGlobalA.ExecuteReader
        If RsOR.HasRows Then
            While RsOR.Read
                Cantidad = Nz(RsOR!RECEPD_CANT_REC)
                CmdGlobalB.CommandText = "SELECT OCOMPRAD_REQUISD_NUMERAR,OCD.OCOMPRAD_ARTICULO,OCD.OCOMPRAD_CANTIDAD " _
                    & " FROM TBLOGIS_ORDENES_COMPRA OC INNER JOIN TBLOGIS_ORDENES_COMPRA_DETALLE OCD ON OC.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO AND OC.OCOMPRA_NUMERAR = OCD.OCOMPRA_NUMERAR" _
                    & " WHERE (OC.OCOMPRA_NUMERAR = " & Nz(RsOR!OCOMPRA_NUMERAR) & ") AND (OCD.OCOMPRAD_ARTICULO = '" & Nz(RsOR!ARTICULO_CODIGO) & "') ORDER BY OCOMPRAD_REQUISD_NUMERAR"
                RsOC = CmdGlobalB.ExecuteReader
                If RsOC.HasRows Then
                    While RsOC.Read()
                        If Cantidad >= CLng(Nz(RsOC!OCOMPRAD_CANTIDAD)) And Cantidad > 0 Then
                            'CAMBIAR DE 5(REQUIS CON O/C) A ESTADO 6(REQUIS ATENDIDA)
                            CmdGlobalC.CommandText = "UPDATE TBLOGIS_REQUISICION_DETALLE SET REQUISD_ESTADO='6',REQUISD_ESTADO_USUARIO='" & psUSer & "', REQUISD_ESTADO_FECHA='" & FechaActual() & "',REQUISD_ESTADO_HORA='" & HoraActual() & "'" _
                                                  & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND REQUISD_NUMERAR=" & Nz(RsOC!OCOMPRAD_REQUISD_NUMERAR) & " AND REQUISD_ESTADO ='5'"
                            CmdGlobalC.ExecuteNonQuery()
                            'CAMBIAR DE 11(PEDIDO CON O/C) A ESTADO 12(PEDIDO POR DESPACHAR)
                            CmdGlobalC.CommandText = "UPDATE TBLOGIS_PEDIDO_DETALLE SET PEDIDOD_FECHA_ATENDIDA='" & FechaActual() & "', PEDIDOD_HORA_ATENDIDA='" & HoraActual() & "', PEDIDOD_ESTADO='12' " _
                                                  & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PEDIDOD_REQUISD_NUMERAR = " & Nz(RsOC!OCOMPRAD_REQUISD_NUMERAR) & ") AND PEDIDOD_ESTADO='11'"
                            CmdGlobalC.ExecuteNonQuery()

                            CmdGlobalC.CommandText = "UPDATE TBLOGIS_ORDENES_COMPRA_DETALLE SET ALMACEN_CANT_RECIBIDA_TOTAL=" & CLng(Nz(RsOC!OCOMPRAD_CANTIDAD)) & ",ALMACEN_ESTADO_ENTREGA='2'" _
                                                  & " WHERE (OCOMPRA_NUMERAR = " & Nz(RsOR!OCOMPRA_NUMERAR) & ") AND (OCOMPRAD_ARTICULO = '" & Nz(RsOR!ARTICULO_CODIGO) & "') AND (OCOMPRAD_REQUISD_NUMERAR=" & Nz(RsOC!OCOMPRAD_REQUISD_NUMERAR) & ")"
                            CmdGlobalC.ExecuteNonQuery()
                        ElseIf Cantidad < CLng(Nz(RsOC!OCOMPRAD_CANTIDAD)) And Cantidad > 0 Then
                            'CAMBIAR DE 5(REQUIS CON O/C) A ESTADO 6(REQUIS ATENDIDA)
                            CmdGlobalC.CommandText = "UPDATE TBLOGIS_REQUISICION_DETALLE SET REQUISD_ESTADO='10',REQUISD_ESTADO_USUARIO='" & psUSer & "', REQUISD_ESTADO_FECHA='" & FechaActual() & "',REQUISD_ESTADO_HORA='" & HoraActual() & "'" _
                                                  & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND REQUISD_NUMERAR=" & Nz(RsOC!OCOMPRAD_REQUISD_NUMERAR) & " AND REQUISD_ESTADO='5'"
                            CmdGlobalC.ExecuteNonQuery()
                            'CAMBIAR DE 11(PEDIDO CON O/C) A ESTADO 12(PEDIDO POR DESPACHAR)
                            CmdGlobalC.CommandText = "UPDATE TBLOGIS_PEDIDO_DETALLE SET PEDIDOD_FECHA_ATENDIDA='" & FechaActual() & "', PEDIDOD_HORA_ATENDIDA='" & HoraActual() & "', PEDIDOD_ESTADO='12' " _
                                                  & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PEDIDOD_REQUISD_NUMERAR = " & Nz(RsOC!OCOMPRAD_REQUISD_NUMERAR) & ") AND PEDIDOD_ESTADO='11'"
                            CmdGlobalC.ExecuteNonQuery()
                            CmdGlobalC.CommandText = "UPDATE TBLOGIS_ORDENES_COMPRA_DETALLE SET ALMACEN_CANT_RECIBIDA_TOTAL=ALMACEN_CANT_RECIBIDA_TOTAL+" & CLng(Cantidad) & ",ALMACEN_ESTADO_ENTREGA='2'" _
                                                  & " WHERE (OCOMPRA_NUMERAR = " & Nz(RsOR!OCOMPRA_NUMERAR) & ") AND (OCOMPRAD_ARTICULO = '" & Nz(RsOR!ARTICULO_CODIGO) & "') AND (OCOMPRAD_REQUISD_NUMERAR=" & RsOC!OCOMPRAD_REQUISD_NUMERAR & ")"
                            CmdGlobalC.ExecuteNonQuery()
                        End If
                        Cantidad = Cantidad - CLng(Nz(RsOC!OCOMPRAD_CANTIDAD))
                    End While
                End If
                RsOC.Close()
            End While
            Dim estado As String = ""
            CmdGlobalB.CommandText = "SELECT SUM(ALMACEN_CANT_RECIBIDA_TOTAL) AS CANTREC, SUM(OCOMPRAD_CANTIDAD) AS CANTXREC " _
                & " From TBLOGIS_ORDENES_COMPRA_DETALLE WHERE (OCOMPRA_NUMERAR = " & Nz(RsOR!OCOMPRA_NUMERAR) & ")"
            RsOC = CmdGlobalB.ExecuteReader
            If RsOC.HasRows Then
                If Nz(RsOC!Cantrec) = Nz(RsOC!CantXRec) Then estado = "2"
                If Nz(RsOC!Cantrec) = 0 And Nz(RsOC!CantXRec) > 0 Then estado = "1" '--> debe darse nunca el caso
                If Nz(RsOC!Cantrec) < Nz(RsOC!CantXRec) Then estado = "3"
                CmdGlobalC.CommandText = "UPDATE TBLOGIS_ORDENES_COMPRA SET OCOMPRA_ESTADO_ING_ALMACEN='" & estado & "' WHERE OCOMPRA_NUMERAR=" & Nz(RsOR!OCOMPRA_NUMERAR)
                CmdGlobalC.ExecuteNonQuery()
            End If
            RsOC.Close()
        End If
        RsOR.Close()
        CnA.Close()
        CnB.Close()
        CnC.Close()
    End Sub
    Protected Sub BtnRegresar2_Click(sender As Object, e As EventArgs) Handles BtnRegresar2.Click
        Response.Redirect("Inventario_Recepcion_Registrar.aspx")
    End Sub

    Private Sub FlexItem_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexItem.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Quitar" Then
            FlexItem.Rows(Index).Visible = False
        End If
    End Sub
    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Call Limpiar_Controles()
    End Sub

    Private Sub btnBus_Click(sender As Object, e As EventArgs) Handles btnBus.Click
        lblEtq_BusDestino.Text = "Busqueda de Proveedores"
        txtProvCodigo.Text = ""
        txtProvRuc.Text = ""
        txtProvNombre.Text = ""
        FlexTipoPers.DataSource = Nothing
        FlexTipoPers.DataBind()
        txtRucTipoPers.Text = ""
        txtRazonSocialTipoPers.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub
    Protected Sub btnListaProveedor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListaProveedor.Click
        Try
            Dim psConexion As String = Session("Ruta_Emp")
            Dim obj As New clsInv_Listados
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            FlexTipoPers.DataSource = obj.Lista_Proveedor(psConexion, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTipoPers.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtProvCodigo.Text = ""
            txtProvRuc.Text = ""
            txtProvNombre.Text = ""
            Session("DestinoCodExt") = FlexTipoPers.Rows(Index).Cells(1).Text
            Session("DestinoDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            Session("DestinoCodigo") = FlexTipoPers.Rows(Index).Cells(3).Text
            txtProvNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("DestinoDescrip"), "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            txtProvRuc.Text = Session("DestinoCodExt")
            txtProvCodigo.Text = Session("DestinoCodigo")
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
        End If
    End Sub
    Protected Sub btnCerrar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar2.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        TxtSku.Value = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Try
            Dim obj As New Cls_Catalogo
            Dim objCn As New Cls_Conexion
            Dim objListaInv As New Cls_Inventario_Verificacion
            Dim dt As New DataTable
            Dim psListaArt As String = "1"
            Dim psListaMarca As String = "1"
            Dim psListaModelo As String = "1"
            Dim psconexion As String = Session("Ruta_Emp")
            Dim pdCodArt As Double = 0
            If TxtCodArticuloBA.Value <> "" Then
                pdCodArt = Nz(TxtCodArticuloBA.Value.ToString)
            End If
            Dim clasificacion As String = ""
            Dim psDescripcion As String = TxtDescripcionBA.Value.ToString
            Dim tipo As String = DdlTipoBA.SelectedValue.ToString
            Dim numPart As String = TxtNumParteBA.Value.ToString
            Dim especifico As String = TxtCodEspecificoBA.Value.ToString
            Dim psSku As String = ""
            Dim marca As Double = 0
            Dim modelo As Double = 0
            Dim pdCodUbicacion As Double = 0

            If marca <> 0 Then psListaMarca = ""
            If modelo <> 0 Then psListaModelo = ""
            If pdCodArt <> 0 Then psListaArt = ""
            If tipo = "< Seleccionar >" Then tipo = ""

            Dim psCodArtSku As String = ""

            If TxtSku.Value <> "" Then
                psSku = TxtSku.Value
            End If

            Dim drT As DataRow
            Dim dtColum As New DataTable


            dtColum.Columns.Add("ART_CODIGO")
            dtColum.Columns.Add("ART_CODEQUIVA")
            dtColum.Columns.Add("ART_DESCRIPCION")
            dtColum.Columns.Add("TIPO_ART")
            dtColum.Columns.Add("ART_TIPO")
            dtColum.Columns.Add("ART_SKU")

            If psSku <> "" Then

                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim CmdGlobal2 As New SqlCommand
                Cn.Open() : CmdGlobal.Connection = Cn
                Cn2.Open() : CmdGlobal2.Connection = Cn2
                Dim Rs As SqlDataReader

                CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS WHERE UPPER(ART_SKU) = '" & UCase(psSku) & "'  "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodArtSku = Nu(Rs("ART_CODIGO"))
                        psDescripcion = Nu(Rs("ART_DESCRIPCION"))
                        TxtDescripcionBA.Value = Nu(Rs("ART_DESCRIPCION"))
                    End While
                End If
                Rs.Close()
                If psCodArtSku = "" Then

                    CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS_IMAGENES WHERE ART_SKU = '" & psSku & "'  "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psDescripcion = Nu(Rs("ART_DESCRIPCION"))
                            TxtDescripcionBA.Value = Nu(Rs("ART_DESCRIPCION"))
                        End While
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS WHERE UPPER(ART_DESCRIPCION) = '" & UCase(TxtDescripcionBA.Value) & "'  "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodArtSku = Nu(Rs("ART_CODIGO"))
                            CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS SET ART_SKU = '" & psSku & "' WHERE ART_CODIGO =  " & psCodArtSku
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()
                End If


            End If

            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, clasificacion, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            If dt.Rows.Count > 0 Then
                For Each drDato As DataRow In dt.Rows
                    drT = dtColum.NewRow()
                    drT("ART_CODIGO") = Nu(drDato("ART_CODIGO"))
                    drT("ART_CODEQUIVA") = Nu(drDato("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drDato("ART_DESCRIPCION"))
                    drT("TIPO_ART") = Nu(drDato("TIPO_ART"))
                    drT("ART_TIPO") = Nu(drDato("ART_TIPO"))
                    drT("ART_SKU") = Nu(drDato("ART_SKU"))
                    dtColum.Rows.Add(drT)
                Next
            End If

            GvBuscarArticulos.DataSource = dtColum
            GvBuscarArticulos.DataBind()
            If dtColum.Rows.Count > 1 Then
                LblCantArtReg.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dtColum.Rows.Count = 1 Then
                LblCantArtReg.Text = "Hay 1 registro."
            ElseIf dtColum.Rows.Count = 0 Then
                LblCantArtReg.Text = "No hay registro."
            End If

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub GvBuscarArticulos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            Dim psArtCodigo As Double = 0
            psArtCodigo = GvBuscarArticulos.Rows(Index).Cells(1).Text
            TxtCodArticuloBA.Value = ""
            TxtDescripcionBA.Value = ""
            GvBuscarArticulos.DataSource = Nothing
            GvBuscarArticulos.DataBind()
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            Dim a As Long = 0
            dtListado.Columns.Add("c0")
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            dtListado.Columns.Add("SKU")
            dtListado.Columns.Add("c5")
            dtListado.Columns.Add("c6")
            dtListado.Columns.Add("c7")
            If FlexItem.Rows.Count > 0 Then
                For i = 0 To FlexItem.Rows.Count - 1
                    a = a + 1
                    drT = dtListado.NewRow()
                    Dim psCant As TextBox
                    Dim psGarantia As TextBox
                    psCant = CType(FlexItem.Rows(i).Cells(6).FindControl("txtCant"), TextBox)
                    psGarantia = CType(FlexItem.Rows(i).Cells(7).FindControl("txtGarantia"), TextBox)
                    drT("c0") = Llenar_Ceros(a, 3)
                    drT("c1") = FlexItem.Rows(i).Cells(2).Text
                    drT("c2") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItem.Rows(i).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    drT("c3") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItem.Rows(i).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    drT("c4") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItem.Rows(i).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    drT("SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItem.Rows(i).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    drT("c5") = FlexItem.Rows(i).Cells(8).Text
                    CType(FlexItem.Rows(i).Cells(6).FindControl("txtCant"), TextBox).Text = psCant.Text
                    CType(FlexItem.Rows(i).Cells(7).FindControl("txtGarantia"), TextBox).Text = psGarantia.Text
                    drT("c6") = "S"
                    drT("c7") = FlexItem.Rows(i).Cells(11).Text
                    dtListado.Rows.Add(drT)
                Next
            End If
            Dim dt As New DataTable
            dt = obj.Lista_ArtxCodigo(Session("Ruta_Emp"), Session("Codempresa"), psArtCodigo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    a = a + 1
                    drT = dtListado.NewRow()
                    drT("c0") = Llenar_Ceros(a, 3)
                    drT("c1") = Nu(dr("COD_ARTICULO"))
                    drT("c2") = Nu(dr("ART_CODEQUIVA"))
                    drT("c3") = Nu(dr("ART_DESCRIPCION"))
                    drT("c4") = Nu(dr("TIPO"))
                    drT("c5") = Nu(dr("ART_CODIGO"))
                    drT("c6") = "S"
                    drT("c7") = Nu(dr("ART_TIPO"))
                    drT("SKU") = Nu(dr("ART_SKU"))
                    dtListado.Rows.Add(drT)
                Next
            End If
            FlexItem.DataSource = dtListado
            FlexItem.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
            Limpiar_Cajas_Buscar_Articulos()
        End If
    End Sub
    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        TituloPopupp.Text = "Busca Clasificaciones"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub
    Private Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click
        TituloPopup.Text = "Busca Marcas"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaMarca_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarca.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = BuscarCodigo.Value.ToString
        Dim codMarca As String = ""
        Dim CodModelo As String = ""
        Dim descripcion As String = BuscarDescripcion.Value.ToString

        If TituloPopup.Text = "Búsqueda de Marcas" Or TituloPopup.Text = "Busca Marcas" Then
            dt = obj.Buscar_Marca(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda de Modelo" Or TituloPopup.Text = "Busca Modelos" Then
            If TituloPopup.Text = "Busca Modelos" Then
                codMarca = LblCodMarcaBA.Text.ToString
            End If
            If codMarca = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Marca');", True)
            Else
                dt = obj.Buscar_Modelo(psconexion, codigo, descripcion, codMarca)
                If dt.Rows.Count() = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Modelos de la Marca seleccionada');", True)
                End If
            End If
        ElseIf TituloPopup.Text = "Búsqueda de Detalle del Modelo" Then
            If CodModelo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Modelo');", True)
            Else
                dt = obj.Buscar_Modelo_Detalle(psconexion, codigo, descripcion, CodModelo)
                If dt.Rows.Count() = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Detalles del Modelo seleccionado');", True)
                End If
            End If
        End If
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub btnModalBuscarClas_Click(sender As Object, e As EventArgs) Handles btnModalBuscarClas.Click
        PopularRootLevel()
    End Sub

    Private Sub trvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles trvClasificacion.TreeNodePopulate
        Dim obj As New Cls_Clasificacion
        Dim dt As DataTable = obj.NumeroNodo(Session("Ruta_Emp"), CInt(e.Node.Value))
        Dim dbRow As DataRow = dt.Rows(0)
        Dim nivelPrincipal As Integer = CInt(dbRow(1).ToString)
        Dim nodo As Integer = CInt(dbRow(0).ToString) + 1
        Dim nodoAyuda As Integer = CInt(dbRow(0).ToString)
        Dim codigo As Integer = CInt(e.Node.Value)
        If nodo = 2 Then
            dt = obj.NodosHijos1(Session("Ruta_Emp"), nivelPrincipal, nodo)
            NodosPopulares(dt, e.Node.ChildNodes)
        Else
            dt = obj.NodosHijos(Session("Ruta_Emp"), nivelPrincipal, nodo, nodoAyuda, codigo)
            NodosPopulares(dt, e.Node.ChildNodes)
        End If
    End Sub

    Private Sub PopularRootLevel()
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" Select CLAS_CODIGO As CODIGO, " +
                                        " CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " +
                                        " (SELECT count(clas_codigo) " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE CLAS_COD_NIVEL=1 and clas_sys_est = '0' ORDER BY CLAS_NUMERACION", objConn)
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()

        da.Fill(dt)
        NodosPopulares(dt, trvClasificacion.Nodes)
    End Sub

    Private Sub NodosPopulares(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        nodes.Clear()
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("clasificacion").ToString()
            tn.Value = dr("CODIGO").ToString()
            nodes.Add(tn)
            tn.PopulateOnDemand = (CInt(dr("CountHijos")) > 0)
        Next
    End Sub

    Protected Sub BtnBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBorrar.Click
        Dim pdCodRecep As Double = txtIngRecepcion.Text
        Dim objDel As New clsInv_InsUpdDel
        objDel.Del_Series(Session("Ruta_Emp"), Session("CodEmpresa"), 0, pdCodRecep)
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim valor As String = txtIngRecepcion.Text.Trim
        Dim valor2 As String = txtIngProveedor.Text.Trim
        Response.Redirect("~/Inventario/Inventario_ExportarExcel.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))
    End Sub

    Private Sub btnGuardarExcel_Click(sender As Object, e As EventArgs) Handles btnGuardarExcel.Click
        If FileUpload1.HasFile Then
            Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim filePath As String = Server.MapPath("~/Uploads/" & fileName)
            If fileName = txtIngRecepcion.Text & " " & txtIngProveedor.Text & ".xlsx" Then
                FileUpload1.SaveAs(filePath)
                LoadExcelData(filePath)
                If Session("SeriesRepetidas") <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('En el archivo hay series repetidas.');", True)
                Else
                    Call Llenar_NroSeries()
                    Dim ia As Integer
                    Dim objUpd As New clsInv_InsUpdDel
                    Dim pdCodRecep As Double = txtIngRecepcion.Text.Trim
                    Dim pdCodArt As Double = 0
                    Dim pdCantRec As Double = 0
                    Dim pdCantFalta As Double = 0
                    Dim pdCantParcial As Double = 0
                    Dim pdCantSob As Double = 0
                    If FlexItemAcc.Rows.Count = 0 Then lblError.Text = "No hay Accesorios que recibir." : Exit Sub
                    For ia = 0 To FlexItemSerie.Rows.Count - 1
                        pdCodArt = FlexItemSerie.Rows(ia).Cells(2).Text.Trim
                        pdCantRec = FlexItemSerie.Rows(ia).Cells(5).Text.Trim
                        pdCantFalta = FlexItemSerie.Rows(ia).Cells(6).Text.Trim
                        objUpd.Upd_CantAccesorio(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecep,
                                                 pdCodArt, pdCantRec, pdCantFalta, pdCantParcial, pdCantSob)
                    Next
                    Call Calculo_Cantidades_Recibidas()
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('El nombre del archivo no coincide con el Nro. de Recepción ni con el Proveedor.');", True)
            End If
        End If
    End Sub
    Private Sub LoadExcelData(ByVal filePath As String)
        Dim CnA As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CnB As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobalA As New SqlCommand
        Dim CmdGlobalB As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psUSer As String = User.Identity.Name
        CnA.Open() : CmdGlobalA.Connection = CnA
        CnB.Open() : CmdGlobalB.Connection = CnB
        Dim psSerieRepetidas As String = ""
        Dim Serie As String = ""
        Dim Placa As Double = 0
        Dim Serie_Numerar As Double = 0
        Dim ValorSys As String = psUSer & FechaActual() & HoraActual()

        Dim dt As New DataTable
        Using workbook As New XLWorkbook(filePath)
            Dim worksheet As IXLWorksheet = workbook.Worksheets.FirstOrDefault()

            If worksheet IsNot Nothing Then
                ' Verificar si la hoja contiene filas
                If worksheet.RowsUsed().Any() Then
                    ' Añadir columnas
                    Dim headerRow = worksheet.Row(1)
                    For Each cell As IXLCell In headerRow.CellsUsed()
                        dt.Columns.Add(cell.Value.ToString())
                    Next

                    ' Añadir filas
                    For Each row As IXLRow In worksheet.RowsUsed().Skip(1)
                        Dim dataRow As DataRow = dt.NewRow()
                        Dim i As Integer = 0
                        For Each cell As IXLCell In row.CellsUsed()
                            dataRow(i) = cell.Value.ToString()
                            i += 1
                        Next
                        dt.Rows.Add(dataRow)
                    Next
                End If
            End If
        End Using

        For Each dr As DataRow In dt.Rows
            Serie = Nu(dr("NRO_SERIE"))
            Placa = Nz(dr("NRO_PLACA"))
            Serie_Numerar = Nz(dr("SERIE_NUMERAR"))
            CmdGlobalA.CommandText = " SELECT SERIE_NUMERAR FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE SERIE_NRO='" & Serie & "'"
            Rs = CmdGlobalA.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psSerieRepetidas = psSerieRepetidas & "<br/>" & Serie
                End While
                Rs.Close()
            Else
                Rs.Close()
                If Placa > 0 And Serie = "" Then Serie = Placa
                CmdGlobalA.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_NRO='" & Serie & "',SYS_SERIE='" & ValorSys & "' WHERE SERIE_NUMERAR=" & Serie_Numerar
                CmdGlobalA.ExecuteNonQuery()
                If Placa > 0 Then
                    CmdGlobalA.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET PLACA_NRO=" & Placa & " WHERE SERIE_NUMERAR=" & Serie_Numerar
                    CmdGlobalA.ExecuteNonQuery()
                End If
            End If
        Next

        If psSerieRepetidas <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Existen Serie repetidas : " & psSerieRepetidas & "');", True)

        End If
    End Sub

    Private Sub BtnNuevoBA_Click(sender As Object, e As EventArgs) Handles BtnNuevoBA.Click
        Dim obj As New Cls_Catalogo
        Dim psCodClasif As Double = 0
        Dim pdCodArt As Double = 0
        Dim pdTipoArt As Double = 0
        Try
            If DdlTipoBA.SelectedValue <> "< Seleccionar >" Then
                pdTipoArt = Nz(DdlTipoBA.SelectedValue)
            End If
            pdCodArt = obj.Codigo(Session("Ruta_Emp"))
            If lblCodClas.Text <> "" Then psCodClasif = lblCodClas.Text
            Dim psArtDescripcion As String = ""
            If TxtDescripcionBA.Value <> "" Then psArtDescripcion = TxtDescripcionBA.Value
            If psArtDescripcion = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar descripción del bien.');", True)
            ElseIf psCodClasif = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Clasificación.');", True)
            ElseIf pdTipoArt = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Tipo.');", True)
            Else
                obj.RegistrarCatalogo(Session("Ruta_Emp"), pdCodArt, pdTipoArt, psCodClasif, 0, 0, 0, psArtDescripcion, Left(psArtDescripcion, 19), TxtNumParteBA.Value, "", 34, 0, "", 0, 0, 0, 0, 0, Session("User"), TxtSku.Value)
            End If

            BtnNuevoBA.Visible = True
            BtnBuscarBA_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        trvClasificacion.SelectedNode.Selected = True
        TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
        Dim psNumero As Integer = 0
        lblCodClas.Text = trvClasificacion.SelectedValue
        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub

End Class
