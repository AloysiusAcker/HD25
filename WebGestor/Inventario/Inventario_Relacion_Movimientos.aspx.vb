Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Imports System.Math
Partial Class Inventario_Inventario_Relacion_Movimientos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("UnaVez") = "NO"
            Session("UnaVezLista") = "NO"
        End If
    End Sub

    Private Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim fInv As New clsInv_Procesos
        Dim psArticulo As String = ""
        Dim pdCodUbica As Double = 0
        Dim psCodArticulo As String = ""
        Dim pdSaldo As Double = 0
        Dim ListaArt As String = ""
        Dim psFecha As String = ""
        Dim psFechaanterior As String = ""
        Dim psFechaDate As Date = Date.Now
        Dim psFechaFin As String = ""
        Try
            psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
            If TxtFecha.Text = "" Then
                psFecha = "20230101"
            Else
                psFechaDate = DateAdd("d", -1, CDate(TxtFecha.Text))
                psFechaanterior = Mid(psFechaDate, 7, 4) + Mid(psFechaDate, 4, 2) + Mid(psFechaDate, 1, 2)
            End If
            If TxtFechaFin.Text = "" Then
                psFechaFin = FechaActual()
            Else
                psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
            End If
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim cmdSql As New SqlCommand
            Dim pscodArt As Double = 0
            Dim psTipoUbica As String = ""
            Dim psCodUbica As Double = 0
            If cboUbica.SelectedValue <> "< Seleccionar >" Then
                psTipoUbica = cboUbica.SelectedValue
            End If
            If txtUbicaCodigo.Text <> "" Then psCodUbica = Nz(txtUbicaCodigo.Text.Trim)

            Dim sql As String = " select * from  fu_Lista_Movimientos_Cantidades_22 ('" & Session("CodEmpresa") & "', " & pscodArt & ", '" & psTipoUbica & "'," & psCodUbica & " , " & psFecha & ", '" & psFechaFin & "','" & psFechaanterior & "') " _
                              & " order by k_art_volumen,k_art_nombre, k_art_fecha , k_art_tipo_mov "
            Dim Cmd As New SqlCommand(Sql, Cn)
            Dim Da As New SqlDataAdapter(Cmd)
            Dim Dt As New DataTable(sql)
            Da.Fill(Dt)

            Flex.DataSource = Dt
            Flex.DataBind()

            If Dt.Rows.Count > 1 Then
                LblRegistro.Text = "Hay " & Dt.Rows.Count & " registros."
            ElseIf Dt.Rows.Count = 1 Then
                LblRegistro.Text = "Hay 1 registro."
            ElseIf Dt.Rows.Count = 0 Then
                LblRegistro.Text = "Hay 0 registro."
            End If

            Dim pdVolumen As Double = 0
            Dim pdVolumenTotal As Double = 0
            Dim pdCant As Double = 0

            Dim dtArt As New DataTable
            For i = 0 To Flex.Rows.Count - 1
                pdCant = Nz(Flex.Rows(i).Cells(5).Text)
                pdVolumen = Flex.Rows(i).Cells(12).Text
                pdVolumenTotal = pdCant * pdVolumen
                Flex.Rows(i).Cells(7).Text = pdVolumenTotal.ToString("0.##########")
                If pdVolumenTotal = 0 Then Flex.Rows(i).Cells(7).Text = "0.0000000000"
            Next


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub Limpiar_Popup()
        txtBusUbicCodInterno.Value = ""
        txtBusUbicDescripcion.Value = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub
    Private Sub btnUbicCerrar_Click(sender As Object, e As EventArgs) Handles btnUbicCerrar.Click
        Call Limpiar_Popup()
    End Sub

    Private Sub btnBusUbicacion_Click(sender As Object, e As EventArgs) Handles btnBusUbicacion.Click
        lblEtiqUbicacion2.Text = lblEtiqUbicacion.Text
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub
    Protected Sub cboUbica_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUbica.SelectedIndexChanged
        If cboUbica.SelectedValue.Trim = "1" Then
            lblEtiqUbicacion.Text = "Búsqueda de Almacén"
            btnBusUbicacion.Enabled = True
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboUbica.SelectedValue.Trim = "2" Then
            lblEtiqUbicacion.Text = "Búsqueda de Sección"
            btnBusUbicacion.Enabled = True
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboUbica.SelectedValue.Trim = "< Seleccionar >" Then
            lblEtiqUbicacion.Text = "Búsqueda"
            btnBusUbicacion.Enabled = False
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        End If
    End Sub
    Protected Sub btnUbicListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbicListar.Click
        Dim obj As New clsInv_Listados
        Dim pdCodAlmacen As Double = 0
        Try
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            If cboUbica.SelectedValue.Trim = "2" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusUbicCodInterno.Value.Trim, txtBusUbicDescripcion.Value.Trim)
                FlexUbicacion.DataBind()
            ElseIf cboUbica.SelectedValue.Trim = "1" Then
                If txtBusUbicCodInterno.Value.Trim <> "" Then pdCodAlmacen = txtBusUbicCodInterno.Value.Trim
                FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusUbicDescripcion.Value.Trim)
                FlexUbicacion.DataBind()
            End If
        Catch ex As SqlException
            'LblError.Text = ex.Message
        Catch ex As Exception
            'LblError.Text = ex.Message
        End Try
    End Sub

    Private Sub FlexUbicacion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            txtUbicaCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtUbicaDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtUbicaCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
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
        Response.AddHeader("Content-Disposition", "attachment;filename=RelacionMovimientoPesosyVolumenes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = 0
        Dim cn As String = Session("Ruta_Emp")
        Dim psCodArt As String = ""
        Dim dtListado As New DataTable

        If e.CommandName = "Detalle" Then
            Index = Convert.ToInt32(e.CommandArgument)
            txtArtCodigo.Value = Flex.Rows(Index).Cells(1).Text.Trim
            txtArtDescripcion.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtPeso.Text = Flex.Rows(Index).Cells(8).Text.Trim
            txtVolAlto.Text = Flex.Rows(Index).Cells(9).Text.Trim
            txtVolAncho.Text = Flex.Rows(Index).Cells(10).Text.Trim
            txtVolLargo.Text = Flex.Rows(Index).Cells(11).Text.Trim
            txtVolumen.Text = Flex.Rows(Index).Cells(12).Text.Trim
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#miModal').modal('show');", True)
        End If

    End Sub

    Private Sub txtPeso_TextChanged(sender As Object, e As EventArgs) Handles txtPeso.TextChanged
        Dim inputText As String = txtPeso.Text
        Dim value As Double

        If Not Double.TryParse(inputText, value) Then
            ' El valor ingresado no es un número decimal válido
            txtPeso.Text = "0" ' Limpia el TextBox
            ' Puedes mostrar un mensaje de error aquí o tomar otra acción.
        End If
    End Sub

    Private Sub txtVolAlto_TextChanged(sender As Object, e As EventArgs) Handles txtVolAlto.TextChanged
        Dim inputText As String = txtVolAlto.Text
        Dim value As Double

        If Not Double.TryParse(inputText, value) Then
            ' El valor ingresado no es un número decimal válido
            txtVolAlto.Text = "0" ' Limpia el TextBox
            ' Puedes mostrar un mensaje de error aquí o tomar otra acción.
        End If
    End Sub

    Private Sub txtVolAncho_TextChanged(sender As Object, e As EventArgs) Handles txtVolAncho.TextChanged
        Dim inputText As String = txtVolAncho.Text
        Dim value As Double

        If Not Double.TryParse(inputText, value) Then
            ' El valor ingresado no es un número decimal válido
            txtVolAncho.Text = "0" ' Limpia el TextBox
            ' Puedes mostrar un mensaje de error aquí o tomar otra acción.
        End If
    End Sub

    Private Sub txtVolLargo_TextChanged(sender As Object, e As EventArgs) Handles txtVolLargo.TextChanged
        Dim inputText As String = txtVolLargo.Text
        Dim value As Double

        If Not Double.TryParse(inputText, value) Then
            ' El valor ingresado no es un número decimal válido
            txtVolLargo.Text = "0" ' Limpia el TextBox
            ' Puedes mostrar un mensaje de error aquí o tomar otra acción.
        End If
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim pdVolumen As Double = 0
        Dim pdAlto As Double = 0
        Dim pdAncho As Double = 0
        Dim pdLargo As Double = 0
        Dim pdPeso As Double = 0
        Dim pdArtCodigo As Double = 0
        pdArtCodigo = Nz(txtArtCodigo.Value)
        pdAlto = Nz(txtVolAlto.Text)
        pdAncho = Nz(txtVolAncho.Text)
        pdLargo = Nz(txtVolLargo.Text)
        pdVolumen = Nz(txtVolumen.Text)
        pdPeso = Nz(txtPeso.Text)

        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS SET ART_VOLUMEN = " & pdVolumen & "  , " _
                              & " ART_PESO = " & pdPeso & " , ART_VOL_ALTO = " & pdAlto & " , " _
                              & " ART_VOL_ANCHO = " & pdAncho & " , ART_VOL_LARGO = " & pdLargo & "  " _
                              & " WHERE ART_CODIGO = " & pdArtCodigo
        CmdGlobal.ExecuteNonQuery()
        Cn.Close()
        btnListar_Click(sender, e)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#miModal').modal('hide');", True)
    End Sub

    Private Sub txtVolumen_TextChanged(sender As Object, e As EventArgs) Handles txtVolumen.TextChanged
        Dim inputText As String = txtVolumen.Text
        Dim value As Double

        If Not Double.TryParse(inputText, value) Then
            ' El valor ingresado no es un número decimal válido
            txtVolumen.Text = "0" ' Limpia el TextBox
            ' Puedes mostrar un mensaje de error aquí o tomar otra acción.
        End If
    End Sub

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs) Handles BtnCalcular.Click
        Dim pdVolumen As Double = 0
        Dim pdAlto As Double = 0
        Dim pdAncho As Double = 0
        Dim pdLargo As Double = 0
        pdAlto = Nz(txtVolAlto.Text)
        pdAncho = Nz(txtVolAncho.Text)
        pdLargo = Nz(txtVolLargo.Text)

        pdVolumen = pdAlto * pdAncho * pdLargo
        txtVolumen.Text = pdVolumen.ToString("0.##########")

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#miModal').modal('show');", True)
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#miModal').modal('hide');", True)
    End Sub
End Class
