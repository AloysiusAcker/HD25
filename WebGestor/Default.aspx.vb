Imports System.Data.SqlClient
Imports WebGestor
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Protected Sub cmdEntrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEntrar.Click
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim CmdGlobal_GpEmp As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim nDiasExp As Short, nDiasContados As Short
        Dim ApePat As String, ApeMat As String, Nombres As String
        Session("CodGrupoEmpresa") = ""
        Session("SiglaGrupoEmpresa") = ""
        Session("CodEmpresa") = ""
        Session("User") = ""
        lblMensajeUsuario.Visible = True
        lblMensajeUsuario.Text = ""
        lblMensajeLogin.Text = ""
        Dim TipoGrupo As String = ""
        Session("TipoGrupo") = ""
        If txtUsuario.Text.Trim = "" Then lblMensajeLogin.Text = "<br> - Ingresar Usuario"
        If txtClave.Text.Trim = "" Then lblMensajeLogin.Text = lblMensajeLogin.Text & "<br> - Ingresar Contraseña"
        If lblMensajeLogin.Text.Trim <> "" Then
            lblMensajeLogin.Text = lblMensajeLogin.Text
            Exit Sub
        End If
        Try
            Cn.Open()
            CmdGlobal_GpEmp.Connection = Cn
            If Left(txtUsuario.Text, 4) = "1111" Then
                CmdGlobal_GpEmp.CommandText = " SELECT USUARI_ESTADO,USUARI_PASS,USUARI_TIPO_EXP,USUARI_DIAS_EXP,USUARI_FECPASS,USUARI_FECINI,USUARI_FECFIN," _
                                            & " USUARI_DIAHORACC,USUARI_ACCFER,USUARI_PERCED,USUARI_APEPAT,USUARI_APEMAT,USUARI_NOMBRES,USUARI_CODIGO,PERSON_COD_INTERNO,USUARI_NUMPASS " _
                                            & " FROM TBUSUARI WHERE USUARI_CODIGO='" & txtUsuario.Text & "' AND USUARI_SYS_EST='0'"
            Else
                CmdGlobal_GpEmp.CommandText = " SELECT USUARI_ESTADO,USUARI_PASS,USUARI_TIPO_EXP,USUARI_DIAS_EXP,USUARI_FECPASS,USUARI_FECINI,USUARI_FECFIN," _
                                            & " USUARI_DIAHORACC,USUARI_ACCFER,USUARI_PERCED,USUARI_APEPAT,USUARI_APEMAT,USUARI_NOMBRES,USUARI_CODIGO,PERSON_COD_INTERNO,USUARI_NUMPASS " _
                                            & " FROM TBUSUARI WHERE PERSON_COD_INTERNO='" & txtUsuario.Text & "' AND USUARI_SYS_EST='0' "
            End If
            CmdGlobal_GpEmp.Connection = Cn
            Rs = CmdGlobal_GpEmp.ExecuteReader()
            If Rs.HasRows = False Then
                lblMensajeUsuario.ForeColor = System.Drawing.Color.Red
                lblMensajeUsuario.Text = "Código de usuario indicado no está registrado en el sistema"
            Else
                While Rs.Read()
                    If Rs.GetString(0) = "S" Then
                        If Nu(Rs!USUARI_PASS) <> "" Then
                            If Rs.GetString(1) = txtClave.Text Then
                                If Nu(Rs.GetString(2)) = "" Then
                                    nDiasExp = 0 : nDiasContados = 0
                                    lblMensajeUsuario.ForeColor = System.Drawing.Color.Red
                                    lblMensajeUsuario.Text = "La contraseña del Usuario indicado no tiene tipo de expiración"
                                ElseIf Nu(Rs.GetString(2)) = "1" Then
                                    nDiasExp = 0 : nDiasContados = -1
                                Else
                                    nDiasExp = IIf(Nu(Rs.GetString(3)) = "1", 0, CInt(Rs.GetString(3)))
                                    nDiasContados = DateDiff("d", FormatoFecha(Nu(Rs.GetString(4))), FormatoFecha(FechaActual))
                                End If
                                If nDiasExp = 0 And nDiasContados = 0 Then
                                Else
                                    If nDiasContados < nDiasExp Then
                                        If (CLng(FechaActual()) >= CLng(Nu(Rs(5)))) And (CLng(FechaActual()) <= CLng(Nu(Rs(6)))) Then
                                            ApePat = Rs.GetString(10)
                                            ApeMat = Rs.GetString(11)
                                            Nombres = Rs.GetString(12)
                                            lblMensajeUsuario.Visible = False
                                            lblMensajeUsuario.ForeColor = System.Drawing.Color.Blue
                                            lblMensajeUsuario.Text = "Bienvenido : " & ApePat & " " & ApeMat & " " & Nombres
                                            'txtUsuario.Text = Nu(Rs!PERSON_COD_INTERNO)
                                            txtUsuario.Text = Nu(Rs!USUARI_CODIGO)
                                            TipoGrupo = IIf(Nu(Rs!USUARI_PERCED) = "S", "3", "5") '3 PERSONAL, 5 USUARIOS EXT, FALTARIA EMP
                                        Else
                                            lblMensajeUsuario.ForeColor = System.Drawing.Color.Red
                                            lblMensajeUsuario.Text = "El Usuario ingresado, no puede accesar al sistema. Ha caducado su usuario."
                                        End If
                                    Else
                                        lblMensajeUsuario.ForeColor = System.Drawing.Color.Red
                                        lblMensajeUsuario.Text = "La contraseña ha expirado. Es necesario cambiar la contraseña para poder luego ingresar el sistema."
                                    End If
                                End If
                            Else
                                lblMensajeUsuario.ForeColor = System.Drawing.Color.Red
                                lblMensajeUsuario.Text = "Contraseña equivocada."
                            End If

                        Else
                            If (Nu(Rs!USUARI_PASS) = "" Or Nu(Rs!USUARI_PASS) = "") And Nu(Rs!USUARI_NUMPASS) = "0" Then
                                ApePat = Rs.GetString(10)
                                ApeMat = Rs.GetString(11)
                                Nombres = Rs.GetString(12)
                                lblMensajeUsuario.Visible = False
                                lblMensajeUsuario.ForeColor = System.Drawing.Color.Blue
                                lblMensajeUsuario.Text = "Bienvenido : " & ApePat & " " & ApeMat & " " & Nombres
                                txtUsuario.Text = Nu(Rs!PERSON_COD_INTERNO)
                                Session("User") = txtUsuario.Text
                                Session("UserNombre") = lblMensajeUsuario.Text
                                Session("UserNombreInicio") = Mid(lblMensajeUsuario.Text, 14)
                                Session("Codigo") = Nu(Rs!USUARI_CODIGO)

                            End If
                        End If
                    Else
                        lblMensajeUsuario.ForeColor = System.Drawing.Color.Red
                        lblMensajeUsuario.Text = "Usuario no tiene acceso al Sistema"
                    End If
                End While
            End If
            Rs.Close()
            Cn.Close()
            If lblMensajeUsuario.Visible = False Then
                Session("UserNombre") = lblMensajeUsuario.Text
                Session("UserFirmado") = "S"
                Session("TipoGrupo") = TipoGrupo
                Session("User") = txtUsuario.Text
                Session("UserNombreInicio") = Mid(lblMensajeUsuario.Text, 14)
                FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, False)
                Response.Redirect("PaginaPrincipal.aspx")
            Else
                Session("UserFirmado") = "N"
            End If
        Catch Ex As SqlException
            lblMensajeUsuario.Visible = True
            lblMensajeUsuario.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblMensajeUsuario.Visible = True
            lblMensajeUsuario.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'btnPopup.Attributes.Add("onclick", "window.open('IngresarPassword.aspx',null,'left=500, top=200, height=200, width= 350, status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');")
    End Sub
    Protected Sub txtUsuario_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsuario.TextChanged
        'Session("User") = txtUsuario.Text
    End Sub
    Protected Sub txtClave_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtClave.TextChanged
        'Session("Pass") = txtClave.Text
    End Sub
End Class
