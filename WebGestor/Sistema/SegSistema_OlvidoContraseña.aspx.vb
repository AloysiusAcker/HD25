Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports WebGestor
Partial Class Sistema_SegSistema_OlvidoContraseña
    Inherits System.Web.UI.Page
    Private Sub Enviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Enviar.Click
        lblMsg.Text = ""
        Dim Clave As String = ""
        Dim Email As String = ""
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim i As Integer, Ok As Boolean = False
        If txtEmail.Text.Trim = "" Then lblMsg.Text = "Ingresar Email." : Exit Sub
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
                If i > 1 Then
                    lblMsg.Text = "Ha ocurrido un error se ha encontrado más un usuario con el mismo correo."
                Else
                    Rs.Close()
                    Rs = cmdSql.ExecuteReader
                    Do While Rs.Read
                        Clave = Nu(Rs!USUARI_PASS)
                        Email = Nu(Rs!PERSON_COD_INTERNO)
                        Ok = True
                    Loop
                End If
            Else
                lblMsg.Text = "Usuario no registrado."
            End If
        Catch Ex As SqlException
            lblMsg.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblMsg.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        If Ok = True Then
            Call Envia_Email(Email, Clave)
        End If
    End Sub
    Private Sub Envia_Email(ByVal Usuario As String, ByVal Clave As String)
        Dim i As Integer = 0

        Dim correo As New MailMessage()
        Me.Page.Session.Timeout = 1080
        correo.From = New MailAddress(Trim(txtEmail.Text))
        correo.To.Add(Trim(txtEmail.Text))
        correo.CC.Add(Trim(txtEmail.Text))
        correo.Subject = "Recordar Contraseña"
        correo.Body = "Le recordamos que sus datos de acceso son: Usuario " & Usuario & "; Contraseña " & Clave

        Dim smtp As New SmtpClient
        smtp.Host = "smtp.gmail.com"
        smtp.Port = 25
        smtp.EnableSsl = True
        smtp.Credentials = New System.Net.NetworkCredential("soporte.tecnico.tecnologias@gmail.com", "hacc2010")

        Try
            smtp.Send(correo)
        Catch ehttp As System.Web.HttpException
            i = 1
            lblMsg.Text = "Se ha producido un error."
        End Try
        If i = 0 Then
            Session("PageMensaje") = "3"
            Session("Mensaje") = "Revise su email su contraseña de acceso le ha sido enviado !!!"
            Response.Redirect("SegSistema_MensajeOk.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtEmail.Text = ""
        End If
    End Sub
End Class
