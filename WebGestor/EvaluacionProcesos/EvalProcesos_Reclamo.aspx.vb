Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web
Partial Class EvaluacionProcesos_EvalProcesos_Reclamo
    Inherits System.Web.UI.Page
    Dim ObjProceso As New ClsEval_Proceso
    Dim ObjCont As New clsCont_Listados
    Dim ObjGeneral As New ModuloGeneral

    'Sub RightAreaMenu_ItemClick(source As Object, e As MenuItemEventArgs) Handles RightAreaMenu.ItemClick
    '    If e.Item.Name = "SignOutItem" Then
    '        Response.Redirect("~/")
    '    End If
    'End Sub
    'Sub ApplicationMenu_ItemDataBound(source As Object, e As MenuItemEventArgs) Handles ApplicationMenu.ItemDataBound
    '    e.Item.Image.Url = String.Format("Css_Gestor/Imagenes/{0}.svg", e.Item.Text)
    '    e.Item.Image.UrlSelected = String.Format("Css_Gestor/Imagenes/{0}-white.svg", e.Item.Text)
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFecha.Text = FormatoFecha(FechaActual)
            txtFechaRpta.Text = FormatoFecha(FechaActual)
            txtHora.Text = FormatoHora(HoraActual)
            Call Llenar_Oficina()
            lblError.Text = ""
            Dim dt As DataTable
            dt = ObjGeneral.Datos_Empresa(Session("Ruta_Emp"), Session("CodEmpresa"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtRuc.Text = dr("Emp_ruc")
                    txtRazonSocial.Text = dr("Emp_nombre")
                Next
            End If
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Dim Rs As SqlDataReader
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT MAX(RECLAMO_REG_NUM) FROM TBEVALUACION_PROCESO_RECLAMO "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    txtCodReclamo.Text = Nz(Rs(0)) + 1
                End While
            Else
                txtCodReclamo.Text = "1"
            End If
            Rs.Close()
        End If
    End Sub
    Private Sub Llenar_Oficina()
        DdlTienda.Items.Clear()
        DdlTienda.DataSource = ObjProceso.Evaluacion_ListaRelacion_OficinaXDM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), Session("User"))
        DdlTienda.DataTextField = "c3"
        DdlTienda.DataValueField = "c4"
        DdlTienda.DataBind()
        DdlTienda.Items.Add("< Seleccionar >")
        DdlTienda.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Cerrar0_Click(sender As Object, e As EventArgs) Handles Cerrar0.Click
        'Response.Redirect("~/PaginaPrincipal.aspx")
        Response.Redirect("EvalProcesos_ReclamoLista.aspx") 'EvalProcesos_ReclamoLista
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        lblError.Text = ""
        Dim dt As New DataTable
        Dim CodOficina As Double = 0
        Dim CodPersona As Double = 0
        Dim psResponsable As String = ""
        Dim psFecha As String = ""
        Dim psFechaRpta As String = ""
        Dim psFechaReg As String = ""
        Dim psHoraReg As String = ""
        Dim psValorsys As String = ""
        Dim psCodReclamo As String = ""
        Dim psUser As String = Session("User")
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            If DdlTienda.SelectedValue = "< Seleccionar >" Then lblError.Text = "Debe elegir una tienda" : Exit Sub
            If txtNombres.Text = "" Then lblError.Text = "Debe ingresar el nombre del consumidor" : Exit Sub
            If txtApellidos.Text = "" Then lblError.Text = "Debe ingresar los apellidos del consumidor" : Exit Sub
            If txtNombres.Text = "" Then lblError.Text = "Debe ingresar el domicilio del consumidor" : Exit Sub
            If txtDni.Text = "" Then lblError.Text = "Debe ingresar el domicilio del consumidor" : Exit Sub
            If txtTelef.Text = "" Then lblError.Text = "Debe ingresar el teléfono del consumidor" : Exit Sub
            If txtEmail.Text = "" Then lblError.Text = "Debe ingresar el correo electrónico del consumidor" : Exit Sub
            If DdlTipo.SelectedValue = "< Seleccionar >" Then lblError.Text = "Debe seleccionar el bien contratado." : Exit Sub
            If txtDescripcion.Text = "" Then lblError.Text = "Ingresar la descripción del motivo del reclamo." : Exit Sub
            If optTipo.SelectedIndex = -1 Then lblError.Text = "Seleccionar reclamo o queja." : Exit Sub
            If txtDetalle.Text = "" Then lblError.Text = "Ingresar el detalle del reclamo y pedido." : Exit Sub

            Cn.Open()
            CmdGlobal.Connection = Cn
            psFechaRpta = Right(txtFechaRpta.Text, 4) & Mid(txtFechaRpta.Text, 4, 2) & Left(txtFechaRpta.Text, 2)
            psFecha = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
            psHoraReg = Left(txtHora.Text, 2) & Right(txtHora.Text, 2)
            psValorsys = psUser & FechaActual() & HoraActual()

            psCodReclamo = txtCodReclamo.Text

            CmdGlobal.CommandText = " INSERT INTO TBEVALUACION_PROCESO_RECLAMO (  EMPRESA_CODIGO, RECLAMO_REG_NUM, RECLAMO_REF_FECHA, RECLAMO_REF_HORA, " _
                                  & " RECLAMO_REF_USER, RECLAMO_FECHA, RCELAMO_HORA, RECLAMO_TIENDA, RECLAMO_NOMBRES, RECLAMO_APELLIDOS, " _
                                  & " RECLAMO_DOMICILIO, RECLAMO_DNI, RECLAMO_TELEFONO, RECLAMO_EMAIL, RECLAMO_BIEN,  RECLAMO_DESCRIPCION, " _
                                  & " RECLAMO_TIPO, RECLAMO_DETALLE, RECLAMO_OBS, RECLAMO_FECHA_RPTA, RCELAMO_SYS_EST, RECLAMO_SYS_CRE, RECLAMO_ESTADO )  " _
                                  & " VALUES ('" & Session("CodEmpresa") & "', " & psCodReclamo & ", '" & FechaActual() & "', '" & HoraActual() & "', " _
                                  & " '" & Session("User") & "', '" & psFecha & "', '" & psHoraReg & "', '" & DdlTienda.SelectedValue & "', '" & LTrim(RTrim(txtNombres.Text)) & "', '" & LTrim(RTrim(txtApellidos.Text)) & "', " _
                                  & " '" & LTrim(RTrim(txtDomicilio.Text)) & "', '" & LTrim(RTrim(txtDni.Text)) & "', '" & LTrim(RTrim(txtTelef.Text)) & "', '" & LTrim(RTrim(txtEmail.Text)) & "', '" & DdlTipo.Text & "', '" & LTrim(RTrim(txtDescripcion.Text)) & "', " _
                                  & " '" & optTipo.SelectedValue & "', '" & LTrim(RTrim(txtDetalle.Text)) & "', '" & LTrim(RTrim(txtObs.Text)) & "', '" & psFechaRpta & "', '0', '" & psValorsys & "','1')"
            CmdGlobal.ExecuteNonQuery()

            Cn.Close()

            lblError.Text = "Su reclamo es el Nro. " & Llenar_Ceros(psCodReclamo, 4)
            BtnGuardar.Enabled = False
            Response.Redirect("EvalProcesos_Reclamo.aspx")
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Response.Redirect("EvalProcesos_Reclamo.aspx")
    End Sub
End Class
