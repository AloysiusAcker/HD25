Imports System.Data.SqlClient
Imports WebGestor
Partial Class SegSistema_Mant_Usuarios
    Inherits System.Web.UI.Page

    'Dim cu As New CUFiltros
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Private Sub Listar()
        Try
            Dim obj As New ModuloSeguridad
            Flex.DataSource = obj.Listar_Usuarios
            Flex.SelectedIndex = -1
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub

    Protected Sub FlexP_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlexP.Disposed

    End Sub
    Protected Sub FlexP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexP.PageIndexChanging
        lblError.Text = ""
        FlexP.PageIndex = e.NewPageIndex
        Call Listar_Personal()
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        'lblPError.Text = ""
        'If FlexP.Rows(Index).Cells(2).Text = "SI" Then lblPError.Text = "El personal escogido ya se encuentra registrado como usuario del sistema." : Exit Sub
        If e.CommandName = "Aceptar" Then
            If FlexP.Rows(Index).Cells(1).Text <> "&nbsp;" Then txtCodigoPers.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexP.Rows(Index).Cells(3).Text <> "&nbsp;" Then txtApepat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexP.Rows(Index).Cells(4).Text <> "&nbsp;" Then txtApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexP.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexP.Rows(Index).Cells(7).Text <> "&nbsp;" Then txtNroDoc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexP.Rows(Index).Cells(8).Text <> "&nbsp;" Then lblCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexP.Rows(Index).Cells(9).Text <> "&nbsp;" Then cboTipoDoc.SelectedValue = UCase(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"))

        End If
    End Sub
    Protected Sub cboPerSN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPerSN.SelectedIndexChanged
        If cboPerSN.SelectedValue = "SI" Then
            txtApepat.Text = "" : txtApeMat.Enabled = False
            txtApeMat.Text = "" : txtApepat.Enabled = False
            lblCodInterno.Text = "" : lblCodInterno.Enabled = True
            txtNombres.Text = "" : txtNombres.Enabled = False
            txtEmail.Text = "" : txtEmail.Enabled = False
            txtNroDoc.Text = "" : txtNroDoc.Enabled = False
            cboNacionalidad.SelectedValue = "< Seleccionar >"
            cboTipoDoc.SelectedValue = "< Seleccionar >"
            btnBuscar.Enabled = True
            btnGrabar.Enabled = True
            btnCancelar.Enabled = True
        ElseIf cboPerSN.SelectedValue = "NO" Then
            txtCodigoPers.Text = Genera_Codigo_NoPersonal("N")
            txtApepat.Text = "" : txtApeMat.Enabled = True
            txtApeMat.Text = "" : txtApepat.Enabled = True
            lblCodInterno.Text = "" : lblCodInterno.Enabled = True
            txtNombres.Text = "" : txtNombres.Enabled = True
            txtEmail.Text = "" : txtEmail.Enabled = True
            txtNroDoc.Text = "" : txtNroDoc.Enabled = True
            cboNacionalidad.SelectedValue = "< Seleccionar >"
            cboTipoDoc.SelectedValue = "< Seleccionar >"
            btnBuscar.Enabled = False
            btnGrabar.Enabled = True
            btnCancelar.Enabled = True
            txtApepat.Focus()
        End If
    End Sub
    Private Sub Listar_Personal()
        Try
            Dim obj As New ClsControlPersonal
            FlexP.DataSource = obj.Listar_Personal(Session("CodGrupoEmpresa"), Session("CodEmpresa"))
            FlexP.SelectedIndex = -1
            FlexP.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblError.Text = ""
        fraDatosPersonales.Visible = True
        cboPerSN.Enabled = True
        Dim FechaServer As String : FechaServer = FechaActual()
        Dim Año As Integer : Año = Left(FechaServer, 4) + 1
        Dim FechaFin As String
        cboPerSN.Enabled = True
        cboPerSN_SelectedIndexChanged(sender, e)
        Call LlenaComboItem("tbopc006", cboNacionalidad)
        Call LlenaComboItem("tbopc009", cboTipoDoc)
        cboNacionalidad.Items.Add("< Seleccionar >") : cboNacionalidad.SelectedValue = "< Seleccionar >"
        cboTipoDoc.Items.Add("< Seleccionar >") : cboTipoDoc.SelectedValue = "< Seleccionar >"
        txtFechaIni.Text = FormatoFecha(FechaServer)
        FechaFin = Año & Mid(FechaServer, 5, 4)
        txtFechaFin.Text = FormatoFecha(FechaFin)
        lbl1.Text = "Nuevo Usuario: Datos Personales"
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim FechaIni As String
        Dim FechaFin As String
        Dim dt As New Data.DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        LblError.Text = ""
        FechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        FechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)
        If lblCodInterno.Text = "" Then lblError.Text = " <br> - Ingresar Código Interno del Usuario."
        If txtCodigoPers.Text = "" Then lblError.Text = " <br> - Ingresar Usuario."
        If txtApepat.Text = "" Then lblError.Text = " <br> - Ingresar Apellido Paterno."
        'If txtApeMat.Text = "" Then lblError.Text = " <br> - Ingresar Apellido Materno."
        If txtNombres.Text = "" Then lblError.Text = " <br> - Ingresar Nombres."
        If lblError.Text <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        End If
        Dim objSeg As New ModuloSeguridad
        Dim Horario As String
        Horario = ""
        Try
            If lbl1.Text = "Nuevo Usuario: Datos Personales" Then
                dt = objSeg.Verifica_CodInterno_Usuario(txtCodigoPers.Text.Trim)
                If dt.Rows.Count > 0 Then
                    lblError.Text = "El Usuario ya existe."
                    Exit Sub
                End If
                If cboPerSN.SelectedValue = "NO" Then
                    txtCodigoPers.Text = Genera_Codigo_NoPersonal("S")
                End If
            End If
            If lbl1.Text = "Nuevo Usuario: Datos Personales" Then
                objSeg.InsUpd_Usuarios(IIf(cboPerSN.SelectedValue.Trim = "SI", "S", "N"), txtCodigoPers.Text.Trim, lblCodInterno.Text.Trim, txtApepat.Text.Trim, txtApeMat.Text.Trim, txtNombres.Text.Trim, FechaIni, FechaFin, "1")
                CmdGlobal.CommandText = " UPDATE dbo.TBUSUARI SET	USUARI_CORREO =  '" & txtEmail.Text & "', USUARI_DOCIDENAC = '" & cboNacionalidad.SelectedValue & "', " _
                                      & " USUARI_TIPDOCIDE = '" & Llenar_Ceros(cboTipoDoc.SelectedValue, 2) & "', USUARI_CODDOCIDE = '" & txtNroDoc.Text & "' " _
                                      & " WHERE USUARI_CODIGO = '" & txtCodigoPers.Text.Trim & "' AND USUARI_PERCED = '" & IIf(cboPerSN.SelectedValue.Trim = "SI", "S", "N") & "' " _
                                      & " And USUARI_ESTADO = 'S' And USUARI_SYS_EST = '0'"
                CmdGlobal.ExecuteNonQuery()
                For i = 1 To 7
                    For j = 1 To 24
                        Horario = Horario & "X"
                    Next
                Next
                CmdGlobal.CommandText = " UPDATE TBUSUARI SET USUARI_NIVEL = '11', USUARI_ESTASOCIADO='01', USUARI_ACCFER='N', " _
                                      & " USUARI_DIAHORACC='" & Horario & "', USUARI_NUMPASS = '1', USUARI_PASS = '" & Right(txtCodigoPers.Text.Trim, 4) & "' " _
                                      & " WHERE USUARI_CODIGO='" & txtCodigoPers.Text.Trim & "' "
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBUSUARI_GRPOEMPS (USUARI_CODIGO,GRPOEMPRESA_CODIGO,EMPRESA_CODIGO) " _
                      & " VALUES('" & txtCodigoPers.Text.Trim & "'," & Session("CodGrupoEmpresa") & ",'" & Session("CodEmpresa") & "')"
                CmdGlobal.ExecuteNonQuery()
                'CmdGlobal.CommandText = "INSERT INTO TBUSUPER(USUPER_CODUSU, PERFIL_CODUNICO,USUPER_SYS_CRE, USUPER_SYS_EST) " _
                '         & " VALUES('" & txtCodigoPers.Text.Trim & "','" & 100 & "','" & Session("User") & FechaActual() & HoraActual() & "','0')"
                'CmdGlobal.ExecuteNonQuery()

            ElseIf lbl1.Text = "Editar Usuario: Datos Personales" Then
                objSeg.InsUpd_Usuarios(IIf(cboPerSN.SelectedValue.Trim = "SI", "S", "N"), txtCodigoPers.Text.Trim, lblCodInterno.Text.Trim, txtApepat.Text.Trim, txtApeMat.Text.Trim, txtNombres.Text.Trim, FechaIni, FechaFin, "2")
                CmdGlobal.CommandText = " UPDATE dbo.TBUSUARI SET USUARI_CORREO =  '" & txtEmail.Text & "', USUARI_DOCIDENAC = '" & cboNacionalidad.SelectedValue & "', " _
                                      & " USUARI_TIPDOCIDE = '" & Llenar_Ceros(cboTipoDoc.SelectedValue, 2) & "', USUARI_CODDOCIDE = '" & txtNroDoc.Text & "' " _
                                      & " WHERE USUARI_CODIGO = '" & txtCodigoPers.Text.Trim & "' AND USUARI_PERCED = '" & IIf(cboPerSN.SelectedValue.Trim = "SI", "S", "N") & "' " _
                                      & " And USUARI_ESTADO = 'S' And USUARI_SYS_EST = '0'"
                CmdGlobal.ExecuteNonQuery()
            End If
            btnCancelar_Click(sender, e)
            Call Listar()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Listar()
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        '
        If e.CommandName = "Editar" Then
            Call LlenaComboItem("tbopc006", cboNacionalidad)
            Call LlenaComboItem("tbopc009", cboTipoDoc)
            cboNacionalidad.Items.Add("< Seleccionar >") : cboNacionalidad.SelectedValue = "< Seleccionar >"
            cboTipoDoc.Items.Add("< Seleccionar >") : cboTipoDoc.SelectedValue = "< Seleccionar >"
            cboPerSN.SelectedValue = IIf(Flex.Rows(Index).Cells(8).Text = "S", "SI", "NO")
            cboPerSN_SelectedIndexChanged(sender, e)
            lbl1.Text = "Editar Usuario: Datos Personales"
            txtCodigoPers.Text = Flex.Rows(Index).Cells(3).Text
            lblCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtApepat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtFechaIni.Text = Flex.Rows(Index).Cells(9).Text
            txtFechaFin.Text = Flex.Rows(Index).Cells(10).Text
            If txtCodigoPers.Text = "11119999" Then
                txtApepat.Enabled = False : txtApeMat.Enabled = False : txtNombres.Enabled = False
            Else
                txtApepat.Enabled = True : txtApeMat.Enabled = True : txtNombres.Enabled = True
            End If
            CmdGlobal.CommandText = "Select *,(Select elemen_VALOR from bdgrupoempresas.dbo.tbcelemen where elemen_codigo=USUARI_DOCIDENAC And elemen_tabla='tbopc006') AS USU_NACID, " _
                                    & "(Select elemen_VALOR from bdgrupoempresas.dbo.tbcelemen where elemen_codigo=USUARI_TIPDOCIDE and elemen_tabla='tbopc023') AS USU_TIPID,USUARI_CORREO " _
                                    & " FROM TBUSUARI WHERE USUARI_CODIGO='" & txtCodigoPers.Text & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If Nu(Rs!USUARI_DOCIDENAC) <> "" Then
                        cboNacionalidad.SelectedValue = Nu(Rs!USUARI_DOCIDENAC)
                    End If
                    If Nu(Rs!USUARI_TIPDOCIDE) <> "" Then
                        cboTipoDoc.SelectedValue = Nu(Rs!USUARI_TIPDOCIDE)
                    End If
                    txtNroDoc.Text = Nu(Rs!USUARI_CODDOCIDE)
                    txtEmail.Text = Nu(Rs!USUARI_CORREO)
                End While
            End If
            Rs.Close()
            fraDatosPersonales.Visible = True
        ElseIf e.CommandName = "Asignar" Then
            txtCodUsuarioPU.Text = Flex.Rows(Index).Cells(3).Text
            txtUsuarioPU.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") & ", " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&#209;", "Ñ"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        ElseIf e.CommandName = "Empresa" Then
            txtCodUsuarioAE.Text = Flex.Rows(Index).Cells(3).Text
            txtUsuarioAE.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") & ", " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&#209;", "Ñ"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblError.Text = ""
        cboPerSN.SelectedValue = "< Seleccionar >"
        fraDatosPersonales.Visible = True
        txtApepat.Text = "" : txtApeMat.Enabled = False
        txtApeMat.Text = "" : txtApepat.Enabled = False
        lblCodInterno.Text = "" : lblCodInterno.Enabled = False
        txtNombres.Text = "" : txtNombres.Enabled = False
        txtEmail.Text = "" : txtEmail.Enabled = False
        txtNroDoc.Text = "" : txtNroDoc.Enabled = False
        cboNacionalidad.SelectedValue = "< Seleccionar >"
        cboTipoDoc.SelectedValue = "< Seleccionar >"
        btnBuscar.Enabled = False
        txtCodigoPers.Text = ""
        btnGrabar.Enabled = False
        BtnCancelar.Enabled = False
        fraDatosPersonales.Visible = False
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Listar()
            Call Listar_Personal()
            Call LlenaComboItem("tbopc006", cboNacionalidad)
            Call LlenaComboItem("tbopc009", cboTipoDoc)
            cboNacionalidad.SelectedValue = "< Seleccionar >"
            cboTipoDoc.SelectedValue = "< Seleccionar >"
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call ListaPerfiles()
            Ficha.Height = 370
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
        End If
        If Ficha.ActiveTabIndex = 2 Then
            Call ListaGrupoEmpresa()
            Ficha.Height = 370
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
        End If
    End Sub
    Private Sub ListaGrupoEmpresa()
        Try
            Dim obj As New ClsControlPersonal
            Dim objSeg As New ModuloSeguridad
            Dim dt As New Data.DataTable
            FlexAE.DataSource = objSeg.Lista_GrupoEmpresa_xUsuario(txtCodUsuarioAE.Text.Trim)
            FlexAE.DataBind()
            Session("EsPersonal") = False
            lblAEUser.Text = "Empresas que el usuario " & HttpContext.Current.User.Identity.Name & " tiene acceso :"
            If Existe_Tabla("TBPERSONAL", Ruta_GrEmp) = True And Left(txtCodUsuarioAE.Text.Trim, 4) <> "1111" Then
                dt = obj.Existe_Personal(txtCodUsuarioAE.Text.Trim, "1")
                If dt.Rows.Count > 0 Then
                    Session("EsPersonal") = True
                    lblAEUser.Text = "Empresas que el usuario " & HttpContext.Current.User.Identity.Name & " tiene acceso y que el personal pertenece:"
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Private Sub ListaPerfiles()
        Try
            Dim obj As New ModuloSeguridad
            FlexPU.DataSource = obj.Lista_Perfiles(txtCodUsuarioPU.Text.Trim, "1")
            FlexPU.SelectedIndex = -1
            FlexPU.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub FlexPU_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPU.PageIndexChanging
        lblError.Text = ""
        FlexPU.PageIndex = e.NewPageIndex
        Call ListaPerfiles()
    End Sub
    Protected Sub FlexPU_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPU.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Quitar" Then
            Try
                lblPUError.Text = ""
                Dim obj As New ModuloSeguridad
                obj.InsUpd_PerfilxUsuarios(FlexPU.Rows(Index).Cells(9).Text, txtCodUsuarioPU.Text.Trim, "2")
            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
            Finally
            End Try
            Call ListaPerfiles()
        End If
    End Sub
    Protected Sub btnPUGuardar_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPUGuardar.Click
        Try
            lblPUError.Text = ""
            Dim CodPerfil As Double = 0
            Dim objSeg As New ModuloSeguridad
            Dim dt As Data.DataTable
            If cboGrpoEmp.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Seleccionar Grupo Empresa" : Exit Sub
            If cboEmp.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Seleccionar Empresa" : Exit Sub
            If cboModInteg.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Seleccionar Módulo de Integración" : Exit Sub
            If cboPerfil.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Seleccionar el Perfil" : Exit Sub
            CodPerfil = cboPerfil.SelectedValue.Trim
            dt = objSeg.Existe_PerfilxUsuario(txtCodUsuarioPU.Text, CodPerfil, "2")
            If dt.Rows.Count > 0 Then
                lblPUError.Text = "El usuario ya tiene asignado el perfil." : Exit Sub
            Else
                objSeg.InsUpd_PerfilxUsuarios(CodPerfil, txtCodUsuarioPU.Text.Trim, "1")
            End If
            btnPUCancelar_Click(sender, e)
            Call ListaPerfiles()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub btnPUAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPUAsignar.Click
        Try
            lblPUError.Text = ""
            Ficha.Height = 450
            lblAsignarPerfil.Visible = True
            Dim objSeg As New ModuloSeguridad
            cboGrpoEmp.Items.Clear() : cboEmp.Items.Clear() : cboModInteg.Items.Clear() : cboPerfil.Items.Clear()
            cboGrpoEmp.DataSource = objSeg.Lista_GrupoEmpresa(txtCodUsuarioPU.Text.Trim, "1")
            cboGrpoEmp.DataTextField = "GE_NOMBRE"
            cboGrpoEmp.DataValueField = "GRPOEMPRESA_CODIGO"
            cboGrpoEmp.DataBind()
            cboGrpoEmp.Items.Add("< Seleccionar >") : cboGrpoEmp.SelectedValue = "< Seleccionar >"
            cboEmp.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
            cboModInteg.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
            cboPerfil.Items.Add("< Seleccionar >") : cboPerfil.SelectedValue = "< Seleccionar >"
            cboGrpoEmp_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub btnPUCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPUCancelar.Click
        lblAsignarPerfil.Visible = False
        Ficha.Height = 370
        lblPUError.Text = ""
        cboGrpoEmp.Items.Clear() : cboEmp.Items.Clear() : cboModInteg.Items.Clear() : cboPerfil.Items.Clear()
        cboGrpoEmp.Items.Add("< Seleccionar >") : cboGrpoEmp.SelectedValue = "< Seleccionar >"
        cboEmp.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
        cboModInteg.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
        cboPerfil.Items.Add("< Seleccionar >") : cboPerfil.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboGrpoEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrpoEmp.SelectedIndexChanged
        Try
            lblPUError.Text = ""
            Dim objSeg As New ModuloSeguridad
            Dim CodGrupoEmp As Double
            If cboGrpoEmp.SelectedValue = "< Seleccionar >" Then
                cboEmp.Enabled = False : cboModInteg.Enabled = False : cboPerfil.Enabled = False
            Else
                CodGrupoEmp = cboGrpoEmp.SelectedValue.Trim
                cboEmp.Items.Clear() : cboModInteg.Items.Clear() : cboPerfil.Items.Clear()
                cboEmp.DataSource = objSeg.Lista_Empresa(txtCodUsuarioPU.Text.Trim, CodGrupoEmp, "1")
                cboEmp.DataTextField = "GEE_NOMBRE"
                cboEmp.DataValueField = "EMPRESA_CODIGO"
                cboEmp.DataBind()
                cboModInteg.DataSource = objSeg.Lista_ModuloIntegracion("2", CodGrupoEmp)
                cboModInteg.DataTextField = "MODINTEG_NOMBRE"
                cboModInteg.DataValueField = "MODINTEG_CODIGO"
                cboModInteg.DataBind()
                cboEmp.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
                cboModInteg.Items.Add("< Seleccionar >") : cboModInteg.SelectedValue = "< Seleccionar >"
                cboPerfil.Items.Add("< Seleccionar >") : cboPerfil.SelectedValue = "< Seleccionar >"
                cboEmp.Enabled = True : cboModInteg.Enabled = True : cboPerfil.Enabled = False
                cboModInteg_SelectedIndexChanged(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub cboModInteg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboModInteg.SelectedIndexChanged
        Try
            lblPUError.Text = ""
            Dim objSeg As New ModuloSeguridad
            Dim CodGrupoEmp As Double
            Dim CodModInteg As Double
            If cboModInteg.SelectedValue = "< Seleccionar >" Then
                cboPerfil.Enabled = False
            Else
                CodGrupoEmp = cboGrpoEmp.SelectedValue.Trim
                CodModInteg = cboModInteg.SelectedValue.Trim
                cboPerfil.Items.Clear()
                cboPerfil.DataSource = objSeg.Lista_PerfilxModIntegracion(cboEmp.SelectedValue.Trim, CodGrupoEmp, CodModInteg)
                cboPerfil.DataTextField = "PERFIL_DES"
                cboPerfil.DataValueField = "PERFIL_CODUNICO"
                cboPerfil.DataBind()
                cboPerfil.Items.Add("< Seleccionar >") : cboPerfil.SelectedValue = "< Seleccionar >"
                cboPerfil.Enabled = True
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub btnAEAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAEAsignar.Click
        Try
            lblAEEtiqueta.Text = "Agregar Acceso"
            Ficha.Height = 480
            lblAccesoEmpresa.Visible = True
            Dim objSeg As New ModuloSeguridad
            Dim dt As New Data.DataTable
            Dim dt2 As New Data.DataTable
            cboAEGrpoEmp.Items.Clear() : cboAEEmp.Items.Clear()
            If Session("EsPersonal") = False Then
                If txtCodUsuarioAE.Text.Trim = "11119999" Then
                    cboAEGrpoEmp.DataSource = objSeg.Lista_GrupoEmpresa("", "2")
                Else
                    'lista grupo empresas que el usuario user tiene acceso
                    cboAEGrpoEmp.DataSource = objSeg.Lista_GrupoEmpresa(txtCodUsuarioAE.Text.Trim, "1")
                End If
                cboAEGrpoEmp.DataTextField = "GE_NOMBRE"
                cboAEGrpoEmp.DataValueField = "GEECOD"
                cboAEGrpoEmp.DataBind()
            Else
                'cuando se trate de un personal de empresa sólo puede tener acceso a las empresas q el user tiene acceso y q el personal pertence
                dt = objSeg.Lista_GrupoEmpresa(txtCodUsuarioAE.Text.Trim, "1")
                dt2 = objSeg.Lista_GrupoEmpresa(txtCodUsuarioAE.Text.Trim, "3")
                If dt2.Rows.Count = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se ha podido encontrar en que empresa o empresas pertenece el personal.')", True)
                End If
                If dt.Rows.Count > 0 And dt2.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        For Each dr2 As Data.DataRow In dt2.Rows
                            If dr("GEECOD") = dr2("GRPOEMPRESA_CODIGO") Then
                                Dim Item As New ListItem
                                Item.Text = dr("GE_NOMBRE")
                                Item.Value = dr("GEECOD")
                                cboAEGrpoEmp.Items.Add(Item) : Exit For
                            End If
                        Next
                    Next
                End If
                dt = Nothing
                dt2 = Nothing
            End If
            cboAEGrpoEmp.Items.Add("< Seleccionar >") : cboAEGrpoEmp.SelectedValue = "< Seleccionar >"
            cboAEEmp.Items.Add("< Seleccionar >") : cboAEEmp.SelectedValue = "< Seleccionar >"
            cboAEGrpoEmp_SelectedIndexChanged(sender, e)
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub cboAEGrpoEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAEGrpoEmp.SelectedIndexChanged
        Try
            Dim objSeg As New ModuloSeguridad
            Dim dt As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim CodGrupoEmp As Double = 0
            If cboAEGrpoEmp.SelectedValue = "< Seleccionar >" Then
                cboAEEmp.Enabled = False
            Else
                CodGrupoEmp = cboAEGrpoEmp.SelectedValue.Trim
                If Session("EsPersonal") = False Then
                    If txtCodUsuarioAE.Text.Trim = "11119999" Then
                        cboAEEmp.DataSource = objSeg.Lista_Empresa("", CodGrupoEmp, "2")
                    Else
                        cboAEEmp.DataSource = objSeg.Lista_Empresa(txtCodUsuarioAE.Text.Trim, CodGrupoEmp, "4")
                    End If
                    cboAEEmp.DataTextField = "GEE_NOMBRE"
                    cboAEEmp.DataValueField = "GEE_CODIGO"
                    cboAEEmp.DataBind()
                    cboAEEmp.Items.Add("< Seleccionar >") : cboAEEmp.SelectedValue = "< Seleccionar >"
                    cboAEEmp.Enabled = True
                Else
                    cboAEEmp.Items.Clear()
                    'cuando se trate de un personal de empresa sólo puede tener acceso a las empresas q el user tiene acceso y q el personal pertence
                    dt = objSeg.Lista_Empresa(HttpContext.Current.User.Identity.Name, CodGrupoEmp, "4")
                    'empresas que el personal pertenece
                    dt2 = objSeg.Lista_Empresa(txtCodUsuarioAE.Text.Trim, CodGrupoEmp, "3")
                    If dt2.Rows.Count = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se ha podido encontrar en que empresa o empresas pertenece el personal.')", True)
                    End If
                    If dt.Rows.Count > 0 And dt2.Rows.Count > 0 Then
                        For Each dr As Data.DataRow In dt.Rows
                            For Each dr2 As Data.DataRow In dt2.Rows
                                If dr("GEE_CODIGO") = dr2("GEE_CODIGO") Then
                                    Dim Item As New ListItem
                                    Item.Text = dr("GEE_NOMBRE")
                                    Item.Value = dr("GEE_CODIGO")
                                    cboAEEmp.Items.Add(Item) : Exit For
                                End If
                            Next
                        Next
                    End If
                    dt = Nothing
                    dt2 = Nothing
                    cboAEEmp.Items.Add("< Seleccionar >") : cboAEEmp.SelectedValue = "< Seleccionar >"
                    cboAEEmp.Enabled = True
                End If
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub btnAECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAECancelar.Click
        lblAccesoEmpresa.Visible = False
        Ficha.Height = 370
        cboAEGrpoEmp.Items.Clear() : cboAEEmp.Items.Clear()
        cboAEGrpoEmp.Items.Add("< Seleccionar >") : cboAEGrpoEmp.SelectedValue = "< Seleccionar >"
        cboAEEmp.Items.Add("< Seleccionar >") : cboAEEmp.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnAEGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim objSeg As New ModuloSeguridad
            Dim dt As New Data.DataTable
            Dim CodGrupo As Double = 0
            If cboAEGrpoEmp.SelectedValue.Trim = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Falta seleccionar Grupo Empresa.')", True)
            ElseIf cboAEEmp.SelectedValue.Trim = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Falta seleccionar Empresa.')", True)
            Else
                CodGrupo = cboAEGrpoEmp.SelectedValue.Trim
                dt = objSeg.Existe_UsuarioxGrpoEmp(txtCodUsuarioAE.Text.Trim, CodGrupo, cboAEEmp.SelectedValue.Trim)
                If dt.Rows.Count Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La definición dada ya existe.')", True)
                Else
                    dt = Nothing
                    objSeg.Insertar_UserGrpoEmps(txtCodUsuarioAE.Text.Trim, CodGrupo, cboAEEmp.SelectedValue.Trim)
                    btnAECancelar_Click(sender, e)
                    Call ListaGrupoEmpresa()
                    Ficha.Height = 370
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Protected Sub cboGrpoEmp_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrpoEmp.SelectedIndexChanged

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPersonal').modal('show');", True)
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPersonal').modal('hide');", True)
    End Sub

    Private Sub btnAERegresar_Click(sender As Object, e As EventArgs) Handles btnAERegresar.Click
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha_ActiveTabChanged(sender, e)
        Ficha.Height = 550
        FlexPU.DataSource = Nothing
        FlexPU.DataBind()
        txtCodUsuarioAE.Text = ""
        txtUsuarioAE.Text = ""
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha_ActiveTabChanged(sender, e)
        Ficha.Height = 550
        FlexPU.DataSource = Nothing
        FlexPU.DataBind()
        lblAsignarPerfil.Visible = False
        txtCodUsuarioPU.Text = ""
        txtUsuarioPU.Text = ""
    End Sub
End Class

