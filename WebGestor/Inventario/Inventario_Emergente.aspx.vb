Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_Emergente
    Inherits System.Web.UI.Page
    Dim ObjInv As New clsInv_Listados
    Dim oFuncInv As New clsInv_Procesos
    Private psSerieNumerar As Double
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            DdlEstado.ClearSelection() : Call LlenaComboItem("TBOPC532", DdlEstado)
            Dim psCodAlmacen As String = "6"
            If txtUbicacion.Text <> "" Then psCodAlmacen = txtUbicacion.Text
            'DdlZona.ClearSelection() : Call Llenar_Zona(DdlZona, psCodAlmacen)
            DdlResponsable.ClearSelection() : Call Llena_Responsable(DdlResponsable)
            DdlUbicacion.ClearSelection() : Call Llena_Ubicacion(DdlUbicacion)
            txtSerie.Text = Session("NroSerie")
            txtPlaca.Text = Session("NorPlaca")
            psSerieNumerar = 0
            Dim psPlaca As Double
            psPlaca = Nz(txtPlaca.Text)
            Dim dt As DataTable
            dt = ObjInv.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtSerie.Text, psPlaca)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Session("psSerieNumerar") = Nz(dr("SERIE_NUMERAR"))
                Next
            End If
            dt = Nothing
            If Nz(Session("psSerieNumerar")) <> 0 Then psSerieNumerar = Session("psSerieNumerar")
            dt = ObjInv.Datos_Equipo_xSerie(Session("Ruta_Emp"), Session("CodEmpresa"), "", psSerieNumerar, "")
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    psSerieNumerar = Nz(dr("SERIE_NUMERAR"))
                    txtPlaca.Text = Nz(dr("PLACA_NRO"))
                    txtSerie.Text = Nu(dr("SERIE_NRO"))
                    optUbicacion.SelectedValue = Nu(dr("UBICACT_TIPO"))
                    txtUbicacion.Text = Nu(dr("UBICACT_CODIGO"))
                    txtUbiCodigo.Text = Nu(dr("COD_ALMACEN"))
                    txtUbiDescripcion.Text = Nu(dr("ALMACEN_NOMBRE"))
                    txtCodArt.Text = Nu(dr("COD_ARTICULO"))
                    txtCodArtAnt.Text = Nu(dr("COD_ARTICULO"))
                    txtNomArt.Text = Nu(dr("ART_DESCRIPCION"))
                    DdlEstado.SelectedValue = Nu(dr("SERIE_ESTADO_EQUIPO"))
                    DdlResponsable.SelectedValue = Nu(dr("SERIE_RESPONSABLE"))
                    DdlUbicacion.SelectedValue = Nu(dr("SERIE_AREA"))
                    txtCodRelacionado.Text = Nu(dr("SERIE_COD_RELACIONADO"))
                    txtObs.Text = Nu(dr("SERIE_RESPONSABLE_OBSERVACION"))
                Next
            End If
            dt = Nothing
            Call LlenaMarca(DdlMarca, Session("Ruta_Emp"), Session("CodEmpresa"))
            Call oFuncInv.Carga_Tabla_Info_Inv("4", DdlTipoArt, Session("Ruta_Emp"), Session("CodEmpresa"))
            Call LlenaBien(DdlTipoBien, Session("Ruta_Emp"), Session("CodEmpresa"))
        End If
    End Sub
    Private Sub LlenaBien(ByVal cbo As DropDownList, ByVal psConexion As String, ByVal psCodEmpresa As String)
        Dim Cn As New SqlConnection(psConexion)
        cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT  RIGHT('000' + CONVERT(VARCHAR(3), COD_BIEN), 3) + ' - ' +DESC_BIEN as BIEN, COD_BIEN  FROM TBTIPO_BIEN_SUNAT WHERE SYS_EST='0' ORDER BY DESC_BIEN "
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cbo.DataSource = cmdSql.ExecuteReader
            cbo.DataTextField = "BIEN"
            cbo.DataValueField = "COD_BIEN"
            cbo.DataBind()
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub LlenaMarca(ByVal cbo As DropDownList, ByVal psConexion As String, ByVal psCodEmpresa As String)
        Dim Cn As New SqlConnection(psConexion)
        cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT  ARTMAR_CODIGO, ARTMAR_DESCRIPCION " _
                              & " FROM TBINV_ARTICULO_MARCA WHERE ARTMAR_SYS_EST = '0' AND EMPRESA_CODIGO = '" & psCodEmpresa & "' "
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cbo.DataSource = cmdSql.ExecuteReader
            cbo.DataTextField = "ARTMAR_DESCRIPCION"
            cbo.DataValueField = "ARTMAR_CODIGO"
            cbo.DataBind()
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub LlenaModelo(ByVal cbo As DropDownList, ByVal psCodMarca As String, ByVal psConexion As String, ByVal psCodEmpresa As String)
        Dim Cn As New SqlConnection(psConexion)
        cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT  ARTMAR_CODIGO, ARTMOD_CODIGO, ARTMOD_DESCRIPCION " _
                              & " FROM TBINV_ARTICULO_MODELO " _
                              & " WHERE ARTMOD_SYS_EST = '0' AND EMPRESA_CODIGO = '" & psCodEmpresa & "' " _
                              & " AND ARTMAR_CODIGO = " & psCodMarca & " "
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cbo.DataSource = cmdSql.ExecuteReader
            cbo.DataTextField = "ARTMOD_DESCRIPCION"
            cbo.DataValueField = "ARTMOD_CODIGO"
            cbo.DataBind()
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblErrorNArt.Text = ex.Message
        Catch ex As Exception
            lblErrorNArt.Text = ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub optUbicacion_SelectedIndexChanged1(sender As Object, e As EventArgs) Handles optUbicacion.SelectedIndexChanged
        txtUbicacion.Text = ""
        txtUbiCodigo.Text = ""
        txtUbiDescripcion.Text = ""
        lblError.Text = ""
        gridCentroCosto.DataSource = Nothing
        gridCentroCosto.DataBind()
        If optUbicacion.SelectedValue = "0" Then
            btnUbica.Enabled = False
        ElseIf optUbicacion.SelectedValue = "1" Then
            lblBusUbica.Text = "Busqueda de Almacén"
            btnUbica.Enabled = True
        ElseIf optUbicacion.SelectedValue = "2" Then
            lblBusUbica.Text = "Busqueda de Centro de Costos"
            btnUbica.Enabled = True
        End If
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

    Private Sub Llena_Responsable(ByVal combo As DropDownList)
        'Listar_Personal
        Dim obj As New ModuloSeguridad
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.Listar_Personal(Session("CodGrupoEmpresa"), Session("CodEmpresa"))
        combo.DataTextField = "NOMBRE_PERSONAL"
        combo.DataValueField = "PERSON_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
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
    Protected Sub Cerrar_Click(sender As Object, e As EventArgs) Handles Cerrar.Click
        'Dim obj As New clsInv_Listados
        'Dim dt As DataTable = Nothing
        'Dim psNroPlaca As Double = 0
        'Dim drArt As DataRow
        'Dim dtArt As New DataTable
        'lblError.Text = ""
        'dtArt.Columns.Add("c1")
        'dtArt.Columns.Add("c2")
        'dtArt.Columns.Add("c3")
        'dtArt.Columns.Add("c4")
        'dtArt.Columns.Add("c5")
        'dtArt.Columns.Add("c6")
        'dtArt.Columns.Add("c7")
        'dtArt.Columns.Add("c8")
        'dtArt.Columns.Add("c9")
        'dtArt.Columns.Add("c10")
        'dtArt.Columns.Add("c11")
        'dtArt.Columns.Add("c12")
        'dtArt.Columns.Add("c13")
        'dtArt.Columns.Add("c14")
        'psNroPlaca = txtPlaca.Text
        'If Session("ListaGrilla") = "Llena" Then
        '    If Session("Tabla").Rows.Count > 0 Then
        '        For i = 0 To Session("Tabla").Rows.Count - 1
        '            drArt = dtArt.NewRow()
        '            drArt("c1") = Session("Tabla").Rows(i).Cells(1).Text
        '            drArt("c2") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c3") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c4") = Session("Tabla").Rows(i).Cells(4).Text
        '            drArt("c5") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c6") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c7") = Session("Tabla").Rows(i).Cells(7).Text
        '            drArt("c8") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c9") = Session("Tabla").Rows(i).Cells(9).Text
        '            drArt("c10") = Session("Tabla").Rows(i).Cells(10).Text
        '            drArt("c11") = Session("Tabla").Rows(i).Cells(11).Text
        '            drArt("c12") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c13") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            drArt("c14") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("Tabla").Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        '            dtArt.Rows.Add(drArt)
        '        Next
        '    End If
        'End If

        'dt = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtSerie.Text.Trim, psNroPlaca)
        'If dt.Rows.Count > 0 Then
        '    For Each dr As DataRow In dt.Rows
        '        drArt = dtArt.NewRow()
        '        drArt("c1") = Nu(dr("COD_ARTICULO"))
        '        drArt("c2") = Nu(dr("ART_DESCRIPCION"))
        '        drArt("c3") = Nu(dr("SERIE_NRO"))
        '        drArt("c4") = Nu(dr("PLACA_NRO"))
        '        drArt("c5") = Nu(dr("TIPOBIEN"))
        '        drArt("c6") = Nu(dr("TIPO_UBICACION"))
        '        drArt("c7") = Nu(dr("COD_ALMACEN"))
        '        drArt("c8") = Nu(dr("ALMACEN_NOMBRE"))
        '        drArt("c9") = Nu(dr("UBICACT_CODIGO"))
        '        drArt("c10") = Nu(dr("SERIE_NUMERAR"))
        '        drArt("c11") = Nu(dr("UBICACT_TIPO"))
        '        drArt("c12") = ""
        '        drArt("c13") = Nu(dr("SERIE_CUSTODIA_CCOSTO"))
        '        drArt("c14") = Nu(dr("ART_TIPO"))
        '        dtArt.Rows.Add(drArt)
        '    Next
        'End If
        ''Session("Tabla") = dtArt
        Response.Write("<script>window.close();</script>")
    End Sub
    Protected Sub btnListaCC_Click(sender As Object, e As EventArgs) Handles btnListaCC.Click
        lblError.Text = ""
        Try
            Dim obj As New clsInv_Listados
            gridCentroCosto.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            gridCentroCosto.DataBind()
            If optUbicacion.SelectedValue.Trim = "2" Then
                gridCentroCosto.DataSource = obj.Lista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"), txtNCodigo.Text.Trim, txtNDescripcion.Text.Trim)
                gridCentroCosto.DataBind()
            ElseIf optUbicacion.SelectedValue.Trim = "1" Then
                If txtNCodigo.Text.Trim <> "" Then pdCodAlmacen = txtNCodigo.Text.Trim
                gridCentroCosto.DataSource = obj.Lista_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, txtNDescripcion.Text.Trim)
                gridCentroCosto.DataBind()
            End If
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnCerrarCC_Click(sender As Object, e As EventArgs) Handles btnCerrarCC.Click
        ModalPopupExtender1.Hide()
        gridCentroCosto.DataSource = Nothing
        gridCentroCosto.DataBind()
    End Sub

    Protected Sub BtnListaArt_Click(sender As Object, e As EventArgs) Handles BtnListaArt.Click
        Try
            Dim obj As New clsInv_Listados
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            lblErrorArt.Text = ""
            If txtBusArtC.Text.Trim <> "" Then pdCodArt = txtBusArtC.Text.Trim
            FlexArt.DataSource = obj.BuscarX_Articulos(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodArt, txtBusArtD.Text.Trim, "")
            FlexArt.DataBind()
            lblRegArt.Text = "Se encontrarón " & FlexArt.Rows.Count & " registros."
            ModalPopupExtender3.Show()
        Catch ex As SqlException
            lblErrorArt.Text = ex.Message
        Catch ex As Exception
            lblErrorArt.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnCerrarArt_Click(sender As Object, e As EventArgs) Handles BtnCerrarArt.Click
        ModalPopupExtender3.Hide()
        txtBusArtC.Text = ""
        txtBusArtD.Text = ""
        lblErrorArt.Text = ""
        FlexArt.DataSource = Nothing
        FlexArt.DataBind()
        lblRegArt.Text = ""
    End Sub

    Private Sub gridCentroCosto_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridCentroCosto.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "sel_detalle" Then
            txtUbicacion.Text = ""
            txtUbiCodigo.Text = ""
            txtUbiDescripcion.Text = ""
            txtUbiCodigo.Text = gridCentroCosto.Rows(Index).Cells(1).Text
            txtUbiDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridCentroCosto.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridCentroCosto.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            gridCentroCosto.DataSource = Nothing
            gridCentroCosto.DataBind()
            Dim psCodAlmacen As String = ""
            If txtUbicacion.Text <> "" Then psCodAlmacen = txtUbicacion.Text
            'ddlZona.ClearSelection() : Call Llenar_Zona(ddlZona, psCodAlmacen)
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Private Sub FlexArt_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexArt.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "sel_detalle" Then
            txtNomArt.Text = ""
            txtCodArt.Text = ""
            txtCodArt.Text = FlexArt.Rows(Index).Cells(1).Text
            txtNomArt.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexArt.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexArt.DataSource = Nothing
            FlexArt.DataBind()
            ModalPopupExtender3.Hide()
        End If
    End Sub
    Protected Sub Guardar_Click(sender As Object, e As EventArgs) Handles Guardar.Click

        lblError.Text = ""
        If txtUbicacion.Text = "" Then lblError.Text = "<br> - Seleccionar la ubicación a ingresar."
        If txtCodArt.Text = "" Then lblError.Text = "<br> - Seleccionar el articulo del equipo."
        If lblError.Text <> "" Then
            lblError.Text = lblError.Text
            Exit Sub
        End If

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psCodCECosto As String = ""
        Dim psCodSeccion As String = ""
        Dim psCodArt As String = ""
        Dim psSerieNumerar2 As String = ""
        Dim ValorSys As String = ""
        Dim psSerieNro As String = ""
        Dim psPlacaNro As String = ""
        Dim psZona As String = ""
        Dim psUbicacion As String = ""
        Dim psCodRecep As String = ""
        Dim psEstado As String = ""
        Dim psFecha As String = ""
        Dim lblNroMovimiento As String = ""
        Dim StockAc As Double = 0
        Dim cant As Double = 0
        Dim psRegularizar As String = ""
        StockAc = 0
        ValorSys = ""
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Cn3.Open() : CmdGlobal3.Connection = Cn3
            cant = 1
            psCodArt = txtCodArt.Text
            psSerieNro = txtSerie.Text
            psPlacaNro = txtPlaca.Text
            'If DdlZona.SelectedValue <> "< Seleccionar >" Then
            '    psZona = DdlZona.SelectedValue
            'End If
            If DdlEstado.SelectedValue <> "< Seleccionar >" Then
                psEstado = DdlEstado.SelectedValue
            End If
            If DdlUbicacion.SelectedValue <> "< Seleccionar >" Then
                psUbicacion = DdlUbicacion.SelectedValue
            End If
            psFecha = txtFecha.Text

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

            If Session("Nuevo") = "Si" Then
                If psPlacaNro <> "" Then
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE PLACA_NRO = " & psPlacaNro
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        Rs.Close()
                        lblError.Text = "LA PLACA YA ESTA INGRESADO." : Exit Sub
                    Else
                        Rs.Close()
                    End If
                End If
                If psSerieNro <> "" And psPlacaNro = "" Then
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE SERIE_NRO = " & psSerieNro
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        Rs.Close()
                        lblError.Text = "LA SERIE YA ESTA INGRESADO." : Exit Sub
                    Else
                        Rs.Close()
                    End If
                End If
                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,   " _
                                  & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG,  RECEP_NRO_ITEM, RECEP_ESTADO, " _
                                  & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO,  RECEP_TIPODESTINO) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecep & "," & txtUbicacion.Text & ", " _
                                  & " '" & FechaActual() & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "',1,'2'," _
                                  & " '0','" & ValorSys & "'," & cant & "," & cant & ",0,0,'N','20','','1', '" & optUbicacion.SelectedValue & "')"
                CmdGlobal.ExecuteNonQuery()

                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                          & " RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecep & ",'1'," & psCodArt & ",'" & cant & "','" & cant & "'," _
                                          & " 0,0,'2','0','20','N')"
                CmdGlobal.ExecuteNonQuery()

                CmdGlobal.CommandText = "SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa")
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psSerieNumerar2 = Nz(Rs(0)) + 1
                    End While
                Else
                    psSerieNumerar2 = 1
                End If
                Rs.Close()

                If psPlacaNro = "" And psSerieNro = "" Then psRegularizar = "A"
                If psPlacaNro = "" And psSerieNro <> "" Then
                    psPlacaNro = psSerieNro
                    psRegularizar = "P"
                ElseIf psPlacaNro <> "" And psSerieNro = "" Then
                    psSerieNro = psPlacaNro
                    psRegularizar = "S"
                Else
                    psSerieNro = psSerieNumerar2
                    psPlacaNro = psSerieNumerar2
                End If
                Session("psSerieNumerar") = psSerieNumerar2
                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " (SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO, SERIE_NRO, SERIE_SOBRANTE," _
                                      & " UBICACT_TIPO, UBICACT_CODIGO, UBICACT_SYS, SERIE_SYS_CRE, SERIE_SYS_EST, SERIE_NUEVO, ALTIBI_CODIGO, SERIE_INGRESO,PROVEEDOR, SERIE_ESTADO, SERIE_ESTADO_EQUIPO, SERIE_CUSTODIA_FECHAFIN, PLACA_NRO, SERIE_RESPONSABLE_OBSERVACION, SERIE_REGULARIZAR)" _
                                      & " VALUES ('" & psSerieNumerar2 & "'," & psCodRecep & ",'" & psCodArt & "','" & psSerieNro & "','N', " _
                                      & " '" & optUbicacion.SelectedValue & "','" & txtUbicacion.Text & "','" & ValorSys & "','" & ValorSys & "','0','S','1','1','0','0', '" & psEstado & "','" & psFecha & "', " & psPlacaNro & ",'" & txtObs.Text & "', '" & psRegularizar & "' )"
                CmdGlobal.ExecuteNonQuery()

                If psZona <> "" Then
                    CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "  SET SERIE_ZONA = " & psZona & "  WHERE SERIE_NUMERAR= " & psSerieNumerar2
                    CmdGlobal.ExecuteNonQuery()
                End If

                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL) " _
                                      & " VALUES(" & psSerieNumerar2 & ",'" & optUbicacion.SelectedValue & "','" & txtUbicacion.Text & "','20','0','" & ValorSys & "','" & FechaActual() & "','3'," & psCodRecep & ")"
                CmdGlobal.ExecuteNonQuery()

                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & txtUbicacion.Text & ") AND (UBICACT_TIPO='" & optUbicacion.SelectedValue & "') " _
                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                        CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & txtUbicacion.Text & ") AND (UBICACT_TIPO='" & optUbicacion.SelectedValue & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                Else
                    CmdGlobal2.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                          & " VALUES(" & txtUbicacion.Text & ",'" & optUbicacion.SelectedValue & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
                Rs.Close()

                CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR) " _
                                                  & " VALUES ('" & Session("CodEmpresa") & "', " & psCodRecep & ", " & psSerieNumerar & ")"
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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep, "20", psCodArt, optUbicacion.SelectedValue, txtUbicacion.Text, "", 0, "Por Inventario", "1", FormatoFecha(FechaActual), 1)

                CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS) " _
                                      & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','2','" & psCodSeccion & "', " _
                                      & " '" & psCodArt & "','1','" & ValorSys & "','3','20','" & FechaActual() & "','0'," & psCodRecep & ")"
                CmdGlobal.ExecuteNonQuery()
                'Session("CodRecep") = psCodRecep
                'Session("Mensaje") = "EL EQUIPO HA SIDO INGRESADO CON RECEPCION NRO. " & psCodRecep

            End If
            CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET PLACA_NRO = " & txtPlaca.Text & ", SERIE_VALIDADO ='0', SERIE_ESTADO_INVENTARIO = '1', SERIE_CONCILIADO = '2', " _
                                      & " SERIE_NRO = '" & txtSerie.Text & "', SERIE_RESPONSABLE_OBSERVACION = '" & txtObs.Text & "' where SERIE_NUMERAR = " & Session("psSerieNumerar")
            CmdGlobal.ExecuteNonQuery()

            If chkRegularizar.Checked = True Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET ARTICULO_CODIGO = " & txtCodArt.Text & ", SERIE_ARTICULO_ANTERIOR = " & txtCodArtAnt.Text & ", SERIE_VALIDADO ='1', SERIE_DATOS_AVALIDAR = '3', SERIE_DATOS_DESCRIPCION = '" & txtNomArt.Text & "' " _
                                          & " where SERIE_NUMERAR = " & Session("psSerieNumerar")
                CmdGlobal.ExecuteNonQuery()
            End If
            If txtCodRelacionado.Text <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_CONCILIADO = '1', SERIE_COD_RELACIONADO = '" & txtCodRelacionado.Text & "' " _
                                          & " WHERE SERIE_NUMERAR = " & Session("psSerieNumerar")
                CmdGlobal.ExecuteNonQuery()
            End If
            If DdlEstado.SelectedValue <> "< Seleccionar >" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                          & " SERIE_ESTADO_EQUIPO = '" & DdlEstado.SelectedValue & "' " _
                                          & " WHERE SERIE_NUMERAR = " & Session("psSerieNumerar")
                CmdGlobal.ExecuteNonQuery()
            End If
            If DdlResponsable.SelectedValue <> "< Seleccionar >" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                          & " SERIE_RESPONSABLE = '" & DdlResponsable.SelectedValue & "' " _
                                          & " WHERE SERIE_NUMERAR = " & Session("psSerieNumerar")
                CmdGlobal.ExecuteNonQuery()
            End If
            If txtUbicacion.Text <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                          & " SERIE_CUSTODIA_CCOSTO = " & txtUbicacion.Text & " " _
                                          & " WHERE SERIE_NUMERAR = " & Session("psSerieNumerar")
                CmdGlobal.ExecuteNonQuery()
            End If
            If DdlUbicacion.SelectedValue <> "< Seleccionar >" And DdlUbicacion.SelectedValue <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                          & " SERIE_AREA = " & DdlUbicacion.SelectedValue & " " _
                                          & " WHERE SERIE_NUMERAR = " & Session("psSerieNumerar")
                CmdGlobal.ExecuteNonQuery()
            End If
            Session("Lista") = "Si"
            Dim scriptCerrar As String = "window.close();"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CerrarVentana", scriptCerrar, True)
            'Response.Write("<script>window.close();</script>")
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub BtnNuevoart_Click(sender As Object, e As EventArgs) Handles BtnNuevoart.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psCodArt As String = ""
        Try
            CmdGlobal.CommandText = "SELECT Max(ART_CODIGO) FROM TBINV_ARTICULOS "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodArt = Nz(Rs(0)) + 1
                End While
            Else
                psCodArt = 1
            End If
            Rs.Close()
            txtNCodArt.Text = psCodArt
            txtNDescripcionArt.Text = ""
            txtCodClasif.Text = ""
            txtAbreviatura.Text = ""
            txtNroParte.Text = ""
            DdlTipoArt.SelectedValue = "< Seleccionar >"
            DdlTipoBien.SelectedValue = "< Seleccionar >"
            DdlMarca.SelectedValue = "< Seleccionar >"
            DdlModelo.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblErrorNArt.Text = ex.Message
        Catch ex As Exception
            lblErrorNArt.Text = ex.Message
        End Try
    End Sub
    Private Sub DdlMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlMarca.SelectedIndexChanged
        DdlModelo.ClearSelection()
        If DdlMarca.SelectedValue <> "< Seleccionar >" Then Call LlenaModelo(DdlModelo, DdlMarca.SelectedValue, Session("Ruta_Emp"), Session("CodEmpresa"))
    End Sub
End Class
