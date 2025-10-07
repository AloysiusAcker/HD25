Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System
Imports System.Data
Partial Class Sistema_SegSistema_NuevoUsuario
    Inherits System.Web.UI.Page
    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        lblErrorData.Text = ""
        lblEtq31.Visible = False
        lblEtq32.Visible = False
        lblEtq33.Visible = False
        Dim NumReg As Integer = 0
        Dim Fecha As String = FechaActual()
        Dim Hora As String = HoraActual()
        Dim ValorSys As String = Fecha & Hora & User.Identity.Name
        Dim Cn As New SqlConnection(Configuration.ConfigurationManager.AppSettings("cnGrupoEmp"))
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim bolError As Boolean = False
        Dim dFechaNac As String = Right(txtFechaNac.Text, 4) + Mid(txtFechaNac.Text, 4, 2) + Left(txtFechaNac.Text, 2)
        If Busca_Duplicado_Usuario() = True Then lblEtq31.Visible = True
        If Valida_Cadena(txtClaveN1.Text) = False Then lblEtq32.Visible = True
        If txtClaveN1.Text.Trim <> txtClaveN2.Text.Trim Then lblEtq33.Visible = True
        If lblEtq31.Visible = True Or lblEtq32.Visible = True Or lblEtq33.Visible = True Then Exit Sub
        If txtUsuario.Text.Trim = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar Usuario."
        If txtClaveN1.Text.Trim = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar Contraseña."
        If txtClaveN2.Text.Trim = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Confirmar Contraseña."
        If txtApePat.Text = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar su Apellido Paterno."
        If txtNombres.Text = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar su Nombre."
        If txtEmail.Text.Trim = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar su Email."
        If txtFechaNac.Text = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar su Fecha de Nacimiento."
        If cboDoc.SelectedValue = "< Seleccionar >" Then lblErrorData.Text = lblErrorData.Text & "<br> - Seleccionar Documento."
        If txtNroDoc.Text = "" Then lblErrorData.Text = lblErrorData.Text & "<br> - Ingresar su Nro. de Documento."
        If cboSexo.SelectedValue = "< Seleccionar >" Then lblErrorData.Text = lblErrorData.Text & "<br> - Seleccionar Sexo."
        If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblErrorData.Text = lblErrorData.Text & "<br> - Seleccionar Empresa."
        If cboOficina.SelectedValue = "< Seleccionar >" Then lblErrorData.Text = lblErrorData.Text & "<br> - Seleccionar Oficina."
        If cboPuesto.SelectedValue = "< Seleccionar >" Then lblErrorData.Text = lblErrorData.Text & "<br> - Seleccionar Puesto."
        Dim CodPersonal As String = Genera_Codigo_NoPersonal("S")
        If CodPersonal = "" Then Exit Sub
        If lblErrorData.Text.Trim <> "" Then
            lblErrorData.Text = "Corregir las sgtes. observaciones: " & lblErrorData.Text.Trim
            Exit Sub
        End If
        Dim dDoc As String = IIf(cboDoc.SelectedValue = "< Seleccionar >", "NULL", cboDoc.SelectedValue.Trim)
        Dim dSexo As String = IIf(cboSexo.SelectedValue = "< Seleccionar >", "NULL", cboSexo.SelectedValue.Trim)
        Dim dEstCiv As String = IIf(cboEstadoCivil.SelectedValue = "< Seleccionar >", "NULL", cboEstadoCivil.SelectedValue.Trim)
        Dim dPais As String = IIf(cboPais.SelectedValue = "< Seleccionar >", "NULL", cboPais.SelectedValue.Trim)
        Dim dDpto As String, dProv As String, dDist As String
        If cboDpto.Enabled = False Then
            dDpto = "NULL"
        Else
            dDpto = IIf(cboDpto.SelectedValue = "< Seleccionar >", "NULL", cboDpto.SelectedValue.Trim)
        End If
        If cboProv.Enabled = False Then
            dProv = "NULL"
        Else
            dProv = IIf(cboProv.SelectedValue = "< Seleccionar >", "NULL", cboProv.SelectedValue.Trim)
        End If
        If cboDist.Enabled = False Then
            dDist = "NULL"
        Else
            dDist = IIf(cboDist.SelectedValue = "< Seleccionar >", "NULL", cboDist.SelectedValue.Trim)
        End If
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            Cn.ChangeDatabase("BDSeguridadGrupoEmps")
            cmdSql.CommandText = "INSERT INTO TBUSUARI (USUARI_CODIGO,USUARI_SYS_EST,USUARI_SYS_CRE) VALUES('" & CodPersonal & "','0','" & ValorSys & "')"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = " UPDATE TBUSUARI SET USUARI_PERCED = 'N',USUARI_PASS = '" & Trim(txtClaveN1.Text) & "',USUARI_FECPASS = '" & Fecha & "',USUARI_ESTADO = 'S',USUARI_ESTASOCIADO = '01', " _
                               & " USUARI_NIVEL = '11',USUARI_FECINI = '" & Fecha & "',USUARI_FECFIN = '" & Format(CInt(Left(Fecha, 4)) + 10, "0000") & "0101'," _
                               & " USUARI_DIAHORACC = 'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX', " _
                               & " USUARI_ACCFER = 'S',USUARI_NUMPASS = '1',USUARI_APEPAT = '" & Trim(txtApePat.Text) & "',USUARI_APEMAT = '" & Trim(txtApeMat.Text) & "',USUARI_NOMBRES = '" & Trim(txtNombres.Text) & "'," _
                               & " USUARI_TIPO_EXP = '1',USUARI_CORREO='" & Trim(txtEmail.Text) & "',USUARI_TIPDOCIDE =" & dDoc & ",USUARI_CODDOCIDE = '" & txtNroDoc.Text.Trim & "', " _
                               & " USUARI_SEXO = " & dSexo & ", USUARI_FECHANAC = '" & dFechaNac & "', USUARI_DIRECCION = '" & txtDireccion.Text.Trim & "', USUARI_PAIS = " & dPais & ", " _
                               & " USUARI_DPTO = " & dDpto & ", USUARI_PROV = " & dProv & ", USUARI_DIST = " & dDist & ", PERSON_COD_INTERNO = '" & txtUsuario.Text.Trim & "', " _
                               & " USUARI_EMPRESA = '" & cboEmpresa.Items(cboEmpresa.SelectedIndex).Text & "', USUARI_EMPRESA_OFICINA = '" & cboOficina.Items(cboOficina.SelectedIndex).Text & "', USUARI_EMPRESA_PUESTO = '" & cboPuesto.Items(cboPuesto.SelectedIndex).Text & "' " _
                               & " WHERE USUARI_CODIGO='" & CodPersonal & "'"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "INSERT INTO TBUSUARI_GRPOEMPS(USUARI_CODIGO,GRPOEMPRESA_CODIGO,EMPRESA_CODIGO) VALUES('" & CodPersonal & "'," & Configuration.ConfigurationManager.AppSettings("CodGE") & ", '" & Configuration.ConfigurationManager.AppSettings("CodEmpresa") & "')"
            cmdSql.ExecuteNonQuery()
            Dim CodPerfil As String = Configuration.ConfigurationManager.AppSettings("CodPerfil0001")
            Dim cmdSql2 As New SqlCommand("SELECT * FROM TBPERFIL WHERE PERFIL_CODUNICO='" & CodPerfil & "' AND PERFIL_SYS_EST='0'", Cn)
            Rs = cmdSql2.ExecuteReader
            If Rs.HasRows Then
                Rs.Close()
                cmdSql.CommandText = "INSERT INTO TBUSUPER( PERFIL_CODUNICO, USUPER_CODUSU, USUPER_SYS_CRE, USUPER_SYS_EST) " _
                                   & "VALUES('" & CodPerfil & "','" & CodPersonal & "','" & ValorSys & "','0')"
                cmdSql.ExecuteNonQuery()
            End If
            Dim obj As New clsMesaAyuda
            obj.MAInsUpd_Personas(0, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApePat.Text.Trim & " " & txtApeMat.Text.Trim, cboOficina.SelectedValue.Trim, cboPuesto.SelectedValue.Trim, "", "", txtEmail.Text.Trim, 0, "", 0, 0, "14", Configuration.ConfigurationManager.AppSettings("cnEmpresa"), Configuration.ConfigurationManager.AppSettings("CodEmpresa"))
        Catch Ex As SqlException
            bolError = True
            lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            bolError = True
            lblErrorData.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        Call User_Pass()
        If Not bolError Then
            Session("PageMensaje") = "4"
            Session("Mensaje") = "Usuario registrado correctamente!!!!!" & "<br> Revise su email su usuario y contraseña le ha sido enviado."
            Response.Redirect("SegSistema_MensajeOk.aspx")
        End If
    End Sub
    Private Sub Envia_Email(ByVal Usuario As String, ByVal Clave As String)
        Dim i As Integer = 0
        Dim correo As New MailMessage()
        Me.Page.Session.Timeout = 1080
        correo.From = New MailAddress(Trim(txtEmail.Text))
        correo.To.Add(Trim(txtEmail.Text))
        'correo.CC.Add(Trim(txtEmail.Text))
        correo.Subject = "Información del Usuario"
        correo.Body = "Le recordamos que sus datos de acceso son: Usuario " & Usuario & "; Contraseña " & Clave

        Dim smtp As New SmtpClient
        smtp.Host = "smtp.gmail.com"
        smtp.Port = 25
        smtp.EnableSsl = True
        smtp.Credentials = New System.Net.NetworkCredential("soporte.tecnico.tecnologias@gmail.com", "hacc2010")

        Try
            smtp.Send(correo)
        Catch Ex As Exception
            i = 1
            lblErrorData.Text = "Se ha producido un error en la aplicación: " & "<br>" & Ex.Message
        End Try
    End Sub
    Private Sub User_Pass()
        Dim Clave As String = ""
        Dim Email As String = ""
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim i As Integer, Ok As Boolean = False
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT PERSON_COD_INTERNO,USUARI_PASS FROM TBUSUARI WHERE USUARI_CORREO='" & Trim(txtEmail.Text) & "'"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                i = 0
                Do While Rs.Read
                    i = i + 1
                Loop
                Rs.Close()
                Rs = cmdSql.ExecuteReader
                Do While Rs.Read
                    Clave = Nu(Rs!USUARI_PASS)
                    Email = Nu(Rs!PERSON_COD_INTERNO)
                    Ok = True
                Loop
            End If
        Catch Ex As SqlException
            lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorData.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        If Ok = True Then
            Call Envia_Email(Email, Clave)
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                cboDpto.Enabled = False
                cboProv.Enabled = False
                cboDist.Enabled = False
                Call LlenaComboItem("TBOPC019", cboSexo)
                Call LlenaComboItem("TBOPC020", cboEstadoCivil)
                Call LlenaComboItem("TBOPC006", cboPais)
                Call LlenaComboItem("tbopc023", cboDoc)
                cboDpto.Items.Add("< Seleccionar >") : cboDpto.SelectedValue = "< Seleccionar >"
                cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
                cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
                cboOficina.Items.Add("< Seleccionar >") : cboOficina.SelectedValue = "< Seleccionar >"
                Call Cargar_Puesto()
                Call Cargar_Empresa()
            Catch Ex As SqlException
                lblErrorData.Visible = True
                lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblErrorData.Visible = True
                lblErrorData.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Private Sub Cargar_Empresa()
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        cboEmpresa.Items.Clear()
        Try
            dt = obj.MALista_Empresa(Configuration.ConfigurationManager.AppSettings("cnEmpresa"), Configuration.ConfigurationManager.AppSettings("CodEmpresa"))
            cboEmpresa.DataSource = dt
            cboEmpresa.DataTextField = "AEMP_NOMBRE"
            cboEmpresa.DataValueField = "AEMP_CODIGO"
            cboEmpresa.DataBind()
            cboEmpresa.Items.Add("< Seleccionar >") : cboEmpresa.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlException
            lblErrorData.Visible = True
            lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorData.Visible = True
            lblErrorData.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Cargar_Oficina()
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        cboOficina.Items.Clear()
        Dim pdEmpresa As Double = cboEmpresa.SelectedValue.Trim
        Try
            dt = obj.MALista_Oficina_xEmpresa(Configuration.ConfigurationManager.AppSettings("cnEmpresa"), Configuration.ConfigurationManager.AppSettings("CodEmpresa"), pdEmpresa)
            cboOficina.DataSource = dt
            cboOficina.DataTextField = "AOFICINA_NOMBRE"
            cboOficina.DataValueField = "AOFICINA_CODIGO"
            cboOficina.DataBind()
            cboOficina.Items.Add("< Seleccionar >") : cboOficina.SelectedValue = "< Seleccionar >"
            dt = Nothing
        Catch Ex As SqlException
            lblErrorData.Visible = True
            lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorData.Visible = True
            lblErrorData.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Cargar_Puesto()
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        Try
            cboPuesto.Items.Clear()
            dt = obj.MALista_Puesto(Configuration.ConfigurationManager.AppSettings("cnEmpresa"), Configuration.ConfigurationManager.AppSettings("CodEmpresa"))
            cboPuesto.DataSource = dt
            cboPuesto.DataTextField = "APUESTO_NOMBRE"
            cboPuesto.DataValueField = "APUESTO_CODIGO"
            cboPuesto.DataBind()
            cboPuesto.Items.Add("< Seleccionar >") : cboPuesto.SelectedValue = "< Seleccionar >"
            dt = Nothing
        Catch Ex As SqlException
            lblErrorData.Visible = True
            lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorData.Visible = True
            lblErrorData.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Function Busca_Duplicado_Email() As Boolean
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            Dim cmdSql As New SqlCommand("SELECT * From TBUSUARI WHERE USUARI_CORREO='" & Trim(txtEmail.Text) & "' AND USUARI_SYS_EST='0'", Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then Busca_Duplicado_Email = True
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Function Busca_Duplicado_Usuario() As Boolean
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            Dim cmdSql As New SqlCommand("SELECT * From TBUSUARI WHERE PERSON_COD_INTERNO='" & txtUsuario.Text.Trim & "' AND USUARI_SYS_EST='0'", Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then Busca_Duplicado_Usuario = True
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Function Valida_Cadena(ByVal Cadena As String) As Boolean
        Dim i As Integer
        Dim Cad As String = Cadena
        Valida_Cadena = True
        For i = 1 To Len(Cadena)
            If Mid(Cadena, i, 1) = "'" Then
                Valida_Cadena = False
                Exit Function
            End If
        Next
    End Function
    Protected Sub cboPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPais.SelectedIndexChanged
        If cboPais.Items.Count = 0 Then Exit Sub
        cboDpto.Items.Clear()
        If cboPais.SelectedValue = "51" Then
            cboDpto.Enabled = True
            cboProv.Enabled = False
            cboDist.Enabled = False
            Call LlenaComboItem("tbopc002", cboDpto)
        Else
            cboDpto.Enabled = False
            cboProv.Enabled = False
            cboDist.Enabled = False
        End If
    End Sub
    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        cboProv.Items.Clear()
        cboDist.Items.Clear()
        If cboPais.Items.Count = 0 Then Exit Sub
        If cboDpto.Items.Count = 0 Then Exit Sub
        If cboDpto.SelectedValue <> "< Seleccionar >" Then Call LlenaComboItem2("TBOPC003", cboProv, Left(Format(CLng(cboDpto.Items(cboDpto.SelectedIndex).Value), "000000"), 2), "PR")
        cboProv.Enabled = True
        cboDist.Enabled = False
    End Sub
    Protected Sub cboProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProv.SelectedIndexChanged
        cboDist.Items.Clear()
        If cboDpto.Items.Count = 0 Then Exit Sub
        If cboProv.Items.Count = 0 Then Exit Sub
        If cboProv.SelectedValue <> "< Seleccionar >" And cboDpto.SelectedIndex <> -1 Then Call LlenaComboItem2("TBOPC004", cboDist, Left(Format(CLng(cboDpto.Items(cboDpto.SelectedIndex).Value), "000000"), 2) + Mid(Format(CLng(cboProv.Items(cboProv.SelectedIndex).Value), "000000"), 3, 2), "DS")
        cboDist.Enabled = True
    End Sub
    Protected Sub cboDoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDoc.SelectedIndexChanged
        If cboDoc.SelectedValue <> "< Seleccionar >" Then
            txtNroDoc.Enabled = True
            txtNroDoc.Text = ""
        Else
            txtNroDoc.Enabled = False
            txtNroDoc.Text = ""
        End If
    End Sub
    Protected Sub cboEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmpresa.SelectedIndexChanged
        If cboEmpresa.SelectedValue.Trim <> "< Seleccionar >" Then
            Call Cargar_Oficina()
        Else
            cboOficina.Items.Clear()
            cboOficina.Items.Add("< Seleccionar >") : cboOficina.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub txtUsuario_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsuario.TextChanged
        'If txtUsuario.Text.Trim <> "" Then
        '    If Busca_Duplicado_Usuario() = True Then lblEtq31.Visible = True Else lblEtq31.Visible = False
        'End If
    End Sub
    Protected Sub txtClaveN1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClaveN1.TextChanged
        'If txtClaveN1.Text.Trim <> "" Then
        '    If Valida_Cadena(txtClaveN1.Text) = False Then lblEtq32.Visible = True Else lblEtq32.Visible = False
        'End If
    End Sub
    Protected Sub txtClaveN2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClaveN2.TextChanged
        'If txtClaveN2.Text.Trim <> "" Then
        '    If txtClaveN1.Text.Trim <> txtClaveN2.Text.Trim Then lblEtq33.Visible = True Else lblEtq33.Visible = False
        'End If
    End Sub
End Class
