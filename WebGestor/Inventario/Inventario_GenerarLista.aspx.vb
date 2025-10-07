Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inventario_Inventario_GenerarLista
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
            lblError.Text = ""
            lblRegEnviar.Text = ""
            'btnListar_Click(sender, e)
            Dim d As Int16
            For d = 1 To 40
                Dim Item As New ListItem
                Item.Text = d
                Item.Value = d
                DdlAntiguedad.Items.Add(Item)
            Next
            DdlAntiguedad.Items.Add("< Seleccionar >")
            DdlAntiguedad.SelectedValue = "< Seleccionar >"
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Cn.Open() : CmdGlobal.Connection = Cn
            If Existe_Tabla("V_INV_GENERAR_LISTA", Session("Ruta_Emp")) = False Then
                CmdGlobal.CommandText = " CREATE TABLE V_INV_GENERAR_LISTA (SERIE_NUMERAR float) "
                CmdGlobal.ExecuteNonQuery()
            End If
            CmdGlobal.CommandText = " DELETE FROM V_INV_GENERAR_LISTA" : CmdGlobal.ExecuteNonQuery()
        End If
    End Sub
    Private Sub Guardar_Marcado()
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Try
            If Existe_Tabla("V_INV_GENERAR_LISTA", Session("Ruta_Emp")) = False Then
                CmdGlobal.CommandText = " CREATE TABLE V_INV_GENERAR_LISTA (SERIE_NUMERAR float) "
                CmdGlobal.ExecuteNonQuery()
            End If
            CmdGlobal2.CommandText = " DELETE FROM V_INV_GENERAR_LISTA" : CmdGlobal2.ExecuteNonQuery()
            Dim Check As CheckBox
            For i = 0 To FlexLista.Rows.Count - 1
                Check = FlexLista.Rows(i).Cells(0).FindControl("chkMar")
                If Check.Checked = True And Check.Enabled = True Then
                    CmdGlobal2.CommandText = " INSERT INTO V_INV_GENERAR_LISTA (SERIE_NUMERAR) VALUES (" & FlexLista.Rows(i).Cells(12).Text & ")"
                    CmdGlobal2.ExecuteNonQuery()
                End If
            Next
            For i = 0 To Flex.Rows.Count - 1
                Check = Flex.Rows(i).Cells(0).FindControl("chkMar")
                If Check.Checked = True And Check.Enabled = True Then
                    CmdGlobal2.CommandText = " INSERT INTO V_INV_GENERAR_LISTA (SERIE_NUMERAR) VALUES (" & Flex.Rows(i).Cells(12).Text & ")"
                    CmdGlobal2.ExecuteNonQuery()
                End If
            Next
            dt = obj.Lista_Equipos_aGenerar(Session("Ruta_Emp"), Session("CodEmpresa"))
            FlexLista.DataSource = dt
            FlexLista.DataBind()
            For i = 0 To FlexLista.Rows.Count - 1
                Check = FlexLista.Rows(i).Cells(0).FindControl("chkMar")
                Check.Checked = True
            Next
            lblRegEnviar.Text = " Lista de Equipos a Generar : " & dt.Rows.Count & " Registros."
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click

        Dim obj As New clsInv_Listados
        Dim objProceso As New clsInv_Procesos
        lblError.Text = ""
        Dim pCodArt As Integer
        Dim TipoLista As String
        Dim pdAntiguedad As Int16 = 0
        Dim psTipoBien As String = "%"
        Dim pdCodAlmacen As Double = 0
        Dim psNroPlaca As Double = 0
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        If txtCodArt.Text.Trim <> "" Then
            pCodArt = txtCodArt.Text.Trim : TipoLista = "1"
        Else
            pCodArt = 0 : TipoLista = "0"
        End If
        If DdlTipo.SelectedValue <> "< Todos >" Then
            psTipoBien = DdlTipo.SelectedValue
        End If
        If DdlAntiguedad.SelectedValue <> "< Seleccionar >" Then
            pdAntiguedad = DdlAntiguedad.SelectedValue
        End If
        If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        If txtUbicacion.Text.Trim <> "" Then pdCodAlmacen = txtUbicacion.Text.Trim Else pdCodAlmacen = 0
        Try
            Call Guardar_Marcado()
            Flex.DataSource = obj.Lista_Equipos_aTratar(Session("Ruta_Emp"), Session("CodEmpresa"), pCodArt, optUbicacion.SelectedValue.Trim, pdCodAlmacen, TipoLista, txtNroSerie.Text.Trim, psTipoBien, pdAntiguedad, psNroPlaca)
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
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
        Dim pCodArt As Integer
        Dim TipoLista As String
        Dim psNroPlaca As Double = 0
        Dim pdCodAlmacen As Double = 0
        Dim psTipoBien As String = "%"
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        If txtCodArt.Text.Trim <> "" Then
            pCodArt = txtCodArt.Text.Trim : TipoLista = "1"
        Else
            pCodArt = 0 : TipoLista = "0"
        End If
        If DdlTipo.SelectedValue <> "< Todos >" Then
            psTipoBien = DdlAntiguedad.SelectedValue
        End If
        Dim pdAntiguedad As Int16 = 0
        If DdlAntiguedad.SelectedValue <> "< Seleccionar >" Then
            pdAntiguedad = DdlTipo.SelectedValue
        End If

        If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        If txtUbicacion.Text.Trim <> "" Then pdCodAlmacen = txtUbicacion.Text.Trim Else pdCodAlmacen = 0
        Try
            Flex.PageIndex = e.NewPageIndex
            Flex.DataSource = obj.Lista_Equipos_aTratar(Session("Ruta_Emp"), Session("CodEmpresa"), pCodArt, optUbicacion.SelectedValue.Trim, pdCodAlmacen, TipoLista, txtNroSerie.Text.Trim, psTipoBien, pdAntiguedad, psNroPlaca)
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexArt_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexArt.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtNomArt.Text = ""
            txtCodArt.Text = ""
            txtCodArt.Text = FlexArt.Rows(Index).Cells(1).Text
            txtNomArt.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexArt.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            ModalPopupExtender1.Hide()
            txtPArtCodigo.Text = ""
            txtPArtDescripcion.Text = ""
            lblRegArt.Text = ""
            txtUbiDescripcion.Text = ""
            FlexArt.DataSource = Nothing
            FlexArt.DataBind()
        End If
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
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        '
    End Sub
    Protected Sub btnListarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarArt.Click
        Try
            Dim obj As New clsInv_Listados
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            lblErrorArt.Text = ""
            If txtPArtCodigo.Text.Trim <> "" Then pdCodArt = txtPArtCodigo.Text.Trim
            FlexArt.DataSource = obj.BuscarX_Articulos(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodArt, txtPArtDescripcion.Text.Trim, "")
            FlexArt.DataBind()
            lblRegArt.Text = "Se encontrarón " & FlexArt.Rows.Count & " registros."
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            lblErrorArt.Text = ex.Message
        Catch ex As Exception
            lblErrorArt.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub optUbicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiCodigo.Text = ""
        txtUbiDescripcion.Text = ""
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
    Protected Sub txtUbiCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiDescripcion.Text = ""
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtUbicacion.Text = ""
            txtUbiCodigo.Text = ""
            txtUbiDescripcion.Text = ""
            txtUbiCodigo.Text = FlexUbicacion.Rows(Index).Cells(1).Text
            txtUbiDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            FlexUbicacion.DataBind()
            If optUbicacion.SelectedValue.Trim = "2" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            ElseIf optUbicacion.SelectedValue.Trim = "1" Then
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
    Protected Sub btnCerrarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Hide()
        txtPArtCodigo.Text = ""
        txtPArtDescripcion.Text = ""
        lblErrorArt.Text = ""
        FlexArt.DataSource = Nothing
        FlexArt.DataBind()
        lblRegArt.Text = ""
    End Sub
    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender2.Hide()
        txtUbicacion.Text = ""
        txtUbiCodigo.Text = ""
        txtUbiDescripcion.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
    End Sub
    Private Sub FlexArt_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles FlexArt.PageIndexChanging
        lblError.Text = ""
        Dim pCodArt As Integer
        If txtPArtCodigo.Text.Trim <> "" Then pCodArt = txtPArtCodigo.Text.Trim Else pCodArt = 0
        Dim obj As New clsInv_Listados
        FlexArt.PageIndex = e.NewPageIndex
        FlexArt.DataSource = obj.BuscarX_Articulos(Session("Ruta_Emp"), Session("CodEmpresa"), pCodArt, txtPArtDescripcion.Text.Trim, "")
        FlexArt.DataBind()
    End Sub
    Protected Sub chkMarcar_CheckedChanged(sender As Object, e As EventArgs) Handles chkMarcar.CheckedChanged
        Dim Check As CheckBox
        If chkMarcar.Checked = True Then
            For i = 0 To Flex.Rows.Count - 1
                Check = Flex.Rows(i).Cells(0).FindControl("chkMar")
                Check.Checked = True
            Next
        Else
            For i = 0 To Flex.Rows.Count - 1
                Check = Flex.Rows(i).Cells(0).FindControl("chkMar")
                Check.Checked = False
            Next
        End If
    End Sub
    Protected Sub BtnGenerarLista_Click(sender As Object, e As EventArgs) Handles BtnGenerarLista.Click
        Dim i As Integer = 0
        Dim a As Integer : a = 0
        Dim n As Long = 0
        Dim ValorSys As String = ""
        Dim pdCodReg As String = "1"
        Dim Rs As SqlDataReader
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        lblError.Text = ""
        Dim Check As CheckBox
        For i = 0 To FlexLista.Rows.Count - 1
            Check = FlexLista.Rows(i).Cells(1).FindControl("chkMar")
            If Check.Checked = True And Check.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then
            For i = 0 To Flex.Rows.Count - 1
                Check = Flex.Rows(i).Cells(1).FindControl("chkMar")
                If Check.Checked = True And Check.Enabled = True Then a = 1 : Exit For
            Next
        End If
        If a = 0 Then lblError.Text = "Debe de marcar al menos un equipo."
        If lblError.Text <> "" Then
            Exit Sub
        End If
        Cn.Open() : Cn2.Open() : Cn3.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3
        lblError.Text = ""
        Dim psContador As Integer = 0
        Try
            CmdGlobal.CommandText = " SELECT MAX(EQATRATAR_CODIGO) " _
                                   & " FROM  TBINV_EQUIPOSLISTA_ATRATAR "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdCodReg = Nz(Rs(0)) + 1
                End While
            End If
            Rs.Close()
            CmdGlobal2.CommandText = " INSERT INTO  TBINV_EQUIPOSLISTA_ATRATAR (EMPRESA_CODIGO, EQATRATAR_CODIGO, EQATRATAR_REG_FECHA, EQATRATAR_REG_HORA, " _
                                   & " EQATRATAR_REG_USER, EQATARTAR_ESTADO, EQATRATAR_CANT, EQATRATAR_SYS_EST) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & pdCodReg & ", '" & FechaActual() & "', '" & HoraActual() & "', " _
                                   & " '" & Session("User") & "', '1', 0, '0') "
            CmdGlobal2.ExecuteNonQuery()
            For i = 0 To FlexLista.Rows.Count - 1
                Check = FlexLista.Rows(i).Cells(0).FindControl("chkMar")
                If Check.Checked = True And Check.Enabled = True Then
                    psContador = psContador + 1
                    CmdGlobal3.CommandText = " INSERT INTO TBINV_EQUIPOSLISTA_ATRATAR_DET (EMPRESA_CODIGO, EQATRATAR_CODIGO, SERIE_NUMERAR) " _
                                           & " VALUES ('" & Session("CodEmpresa") & "'," & pdCodReg & ", " & FlexLista.Rows(i).Cells(12).Text & ")"
                    CmdGlobal3.ExecuteNonQuery()
                End If
            Next
            For i = 0 To Flex.Rows.Count - 1
                Check = Flex.Rows(i).Cells(0).FindControl("chkMar")
                If Check.Checked = True And Check.Enabled = True Then
                    psContador = psContador + 1
                    CmdGlobal3.CommandText = " INSERT INTO TBINV_EQUIPOSLISTA_ATRATAR_DET (EMPRESA_CODIGO, EQATRATAR_CODIGO, SERIE_NUMERAR) " _
                                           & " VALUES ('" & Session("CodEmpresa") & "'," & pdCodReg & ", " & Flex.Rows(i).Cells(12).Text & ")"
                    CmdGlobal3.ExecuteNonQuery()
                End If
            Next
            CmdGlobal.CommandText = " update TBINV_EQUIPOSLISTA_ATRATAR set  EQATRATAR_CANT = " & psContador & " where EQATRATAR_CODIGO = " & pdCodReg
            CmdGlobal.ExecuteNonQuery()
            If Existe_Tabla("V_INV_GENERAR_LISTA", Session("Ruta_Emp")) = False Then
                CmdGlobal.CommandText = " CREATE TABLE V_INV_GENERAR_LISTA (SERIE_NUMERAR float) "
                CmdGlobal.ExecuteNonQuery()
            End If
            CmdGlobal.CommandText = " DELETE FROM V_INV_GENERAR_LISTA" : CmdGlobal.ExecuteNonQuery()
            Flex.DataSource = Nothing
            Flex.DataBind()
            FlexLista.DataSource = Nothing
            FlexLista.DataBind()
            lblRegistro.Text = ""
            lblRegEnviar.Text = ""
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
End Class
