Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inspeccion_Documentos_Ayuda
    Inherits System.Web.UI.Page
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblIngresarFecha.Visible = True
        lblError.Text = ""
        Call LlenaComboItem("TBOPC372", cboTipoIngreso)
        cboTipoIngreso.Items.Add("< Seleccionar >")
        cboTipoIngreso.SelectedValue = "< Seleccionar >"
        Call LlenaComboItem("TBOPC331", cboTipoArchivo)
        cboTipoArchivo.Items.Add("< Seleccionar >")
        cboTipoArchivo.SelectedValue = "< Seleccionar >"
        Call LlenaComboItem("TBOPC413", cboCategoria)
        cboCategoria.Items.Add("< Seleccionar >")
        cboCategoria.SelectedValue = "< Seleccionar >"
        txtNroInspeccion.Text = ""
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblIngresarFecha.Visible = False
            txtFechaIng.Text = ""
            txtFechaFin.Text = ""
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        Dim NroInspecc As Double = 0
        Dim oficina As Double = 0
        Dim i As Integer
        Dim dtListado As New DataTable
        Dim pCodigo As Double
        Dim fila As GridViewRow
        Dim obj As New clsInspeccion_Listado
        lblError.Text = ""
        If txtXInspeccion.Text.Trim <> "" Then NroInspecc = txtXInspeccion.Text.Trim
        If txtXCodOficina.Text.Trim <> "" Then oficina = txtXCodOficina.Text.Trim
        If txtFechaIng.Text.Trim <> "" And txtFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtFechaIng.Text.Trim, 4) + Mid(txtFechaIng.Text.Trim, 4, 2) + Left(txtFechaIng.Text.Trim, 2)
            FechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
        ElseIf txtFechaIng.Text.Trim <> "" And txtFechaFin.Text.Trim = "" Then
            FechaIni = Right(txtFechaIng.Text.Trim, 4) + Mid(txtFechaIng.Text.Trim, 4, 2) + Left(txtFechaIng.Text.Trim, 2)
            FechaFin = Right(txtFechaIng.Text.Trim, 4) + Mid(txtFechaIng.Text.Trim, 4, 2) + Left(txtFechaIng.Text.Trim, 2)
        ElseIf txtFechaIng.Text.Trim = "" And txtFechaFin.Text.Trim = "" Then
            FechaIni = "20100101"
            FechaFin = "21000101"
        ElseIf txtFechaIng.Text.Trim = "" And txtFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
            FechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
        End If
        Try
            dtListado = obj.Listar_Ayuda_General(Session("Ruta_Emp"), oficina, FechaIni, FechaFin, NroInspecc, "", Session("CodEmpresa"), User.Identity.Name)
            Flex.DataSource = dtListado
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
            dtListado = Nothing
            For i = 0 To Flex.Rows.Count - 1
                pCodigo = Flex.Rows(i).Cells(9).Text.Trim
                dtListado = obj.Listar_TemaAyuda(Session("Ruta_Emp"), pCodigo)
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        fila = Flex.Rows(i)
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & Ex.Message
            lblError.Visible = True
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnCancelarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarTA.Click
        Call LimpiarIngreso()
        lblIngresarFecha.Visible = False
    End Sub
    Sub LimpiarIngreso()
        cboTipoIngreso.SelectedValue = "< Seleccionar >"
        cboTipoArchivo.SelectedValue = "< Seleccionar >"
        cboCategoria.SelectedValue = "< Seleccionar >"
        txtFechaIngreso.Text = ""
        txtOficinaRuc.Text = ""
        txtOficinaDescripcion.Text = ""
        txtCodOficina.Text = ""
        txtDocDescrip.Text = ""
        txtNroInspeccion.Text = ""
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Session("TipoBus") = "Ing"
        txtOficinaDescripcion.Text = ""
        txtOficinaRuc.Text = ""
        txtCodOficina.Text = ""
        lblBusCentroCosto.Visible = True
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
    End Sub
    Protected Sub txtOficinaRuc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOficinaRuc.TextChanged
        If txtOficinaRuc.Text = "" Then
            txtOficinaRuc.Text = ""
            txtOficinaDescripcion.Text = ""
            txtCodOficina.Text = ""
        End If
    End Sub
    Protected Sub btnBuscarXOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarXOficina.Click
        txtXCodOficina.Text = ""
        txtXRucOficina.Text = ""
        txtXDesOficina.Text = ""
        lblBusCentroCosto.Visible = True
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
        Session("TipoBus") = "Bus"
    End Sub
    Protected Sub txtXRucOficina_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXRucOficina.TextChanged
        If txtXRucOficina.Text = "" Then
            txtXCodOficina.Text = ""
            txtXRucOficina.Text = ""
            txtXDesOficina.Text = ""
        End If
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            FlexUbicacion.DataSource = obj.Lista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexUbicacion.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiCerrar.Click
        lblBusCentroCosto.Visible = False
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            If Session("TipoBus") = "Bus" Then
                txtXCodOficina.Text = ""
                txtXRucOficina.Text = ""
                txtXDesOficina.Text = ""
                txtXRucOficina.Text = FlexUbicacion.Rows(Index).Cells(1).Text
                txtXDesOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtXCodOficina.Text = FlexUbicacion.Rows(Index).Cells(3).Text
                FlexUbicacion.DataSource = Nothing
                FlexUbicacion.DataBind()
                lblBusCentroCosto.Visible = False
            ElseIf Session("TipoBus") = "Ing" Then
                txtOficinaDescripcion.Text = ""
                txtOficinaRuc.Text = ""
                txtCodOficina.Text = ""
                txtOficinaRuc.Text = FlexUbicacion.Rows(Index).Cells(1).Text
                txtOficinaDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtCodOficina.Text = FlexUbicacion.Rows(Index).Cells(3).Text
                FlexUbicacion.DataSource = Nothing
                FlexUbicacion.DataBind()
                lblBusCentroCosto.Visible = False
            End If
        End If
    End Sub
    Protected Sub FlexUbicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlexUbicacion.SelectedIndexChanged

    End Sub
End Class
