Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.NetworkCredential
Partial Class Menu_Subscriptor
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblTitulo.InnerText = Session("MenuNom")
            Title = Session("MenuNom")
            txtNombre.Text = ""
            txtApepat.Text = ""
            txtApemat.Text = ""
            txtTelefono.Text = ""
            txtEmail.Text = ""
        End If
    End Sub
    Private Sub Limpiar()
        Dim i As Integer = 0
        txtNombre.Text = ""
        txtApepat.Text = ""
        txtApemat.Text = ""
        txtTelefono.Text = ""
        txtEmail.Text = ""
        For i = 1 To chkListado.Items.Count - 1
            chkListado.Items(i).Selected = False
        Next
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        lblError.Text = ""
        Dim UserCodigo As String = ""
        Dim i As Integer = 0
        Dim opc As Integer = 0
        Dim obj As New ModuloSeguridad
        Dim dt As New DataTable
        dt = obj.Verificar_Correo(txtEmail.Text.Trim)
        If dt.Rows.Count > 0 Then
            Call Limpiar()
            lblError.Text = "Ya se le genero un usuario con su Correo Electrónico."
        Else
            UserCodigo = Genera_Codigo_NoPersonal("S")
            If txtNombre.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Nombre."
            If txtApepat.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Apellido Paterno."
            If txtApemat.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Apellido Materno."
            If txtTelefono.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Teléfono."
            If txtEmail.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Correo Electrónico."
            For i = 0 To chkListado.Items.Count - 1
                If chkListado.Items(i).Selected = False Then opc = opc + 1
            Next
            If opc = chkListado.Items.Count - 1 Then lblError.Text = lblError.Text & " <br> - Seleccionar una opción."
            If lblError.Text.Trim <> "" Then
                lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
                Exit Sub
            End If
            lblError.Text = ""
            Try
                Dim obj2 As New Insertar
                obj.Insertar_Subscriptor(UserCodigo, txtNombre.Text.Trim, txtApepat.Text.Trim, txtApemat.Text.Trim, txtTelefono.Text.Trim, txtEmail.Text.Trim)
                obj.Insertar_UserGrpoEmps(UserCodigo, Session("CodGrupoEmpresa"), Session("CodEmpresa"))
                For i = 0 To chkListado.Items.Count - 1
                    If chkListado.Items(i).Selected = True Then
                        If i = 3 Or i = 4 Then obj2.Insertar_RelacionUserLink(UserCodigo, i, IIf(i = 3, 12, 13))
                    End If
                Next
                Envia_Email(UserCodigo, txtEmail.Text.Trim, Right(UserCodigo, 4))
            Catch ex As SqlException
                'lblError.Text = ex.Message
            Catch Ex As Exception
                'lblError.Text = Ex.Message
            Finally
            End Try
            Call Limpiar()
            lblError.Text = "Su Usuario y Contraseña ha sido enviado a su Correo Electrónico."
        End If
        dt = Nothing
    End Sub
    Private Sub Envia_Email(ByVal User As String, ByVal Email As String, ByVal Clave As String)
        Dim correo As New MailMessage()
        Dim smtp As New SmtpClient

        correo.From = New MailAddress("slimorales.27@gmail.com")
        correo.To.Add(Email)
        correo.Subject = "WebCas - Su Usuario"
        correo.Body = "Le recordamos que sus datos de acceso son:<br> Usuario :" & User & "<br> Contraseña: " & Clave
        correo.IsBodyHtml = True

        smtp.Host = "smtp.gmail.com"
        smtp.Port = 25
        smtp.EnableSsl = True
        smtp.Credentials = New System.Net.NetworkCredential("slimorales.27@gmail.com", "elizabeth")

        Try
            smtp.Send(correo)
            'lblError.Text = "Mensaje enviado satisfactoriamente"
        Catch ex As Exception
            lblError.Text = "ERROR: " & ex.Message
        End Try
    End Sub
    Function Genera_Codigo_NoPersonal(ByVal Guardar As String) As String
        Dim obj As New ModuloSeguridad
        Dim dt As New Data.DataTable
        Dim NumCod As Integer
        dt = obj.Consigue_UltimoNUsuario
        If dt.Rows.Count > 0 Then
            For Each drMenuItem As Data.DataRow In dt.Rows
                obj.Ingresar_UltimoNUsuario(Format(drMenuItem("USUARIO") + 1, "0000"))
            Next
        End If
        dt = Nothing
        dt = obj.Extraer_UltimoNUsuario
        If dt.Rows.Count = 0 Then
            If Guardar = "S" Then
                obj.Ingresar_UltimoNUsuario("0002")
            End If
            Genera_Codigo_NoPersonal = "11110001"
        Else
            For Each drMenuItem As Data.DataRow In dt.Rows
                If Guardar = "S" Then
                    NumCod = Nz(drMenuItem("CONUDS_CORR"))
                    drMenuItem("CONUDS_CORR") = Cadena_Num_Corr(NumCod)
                    drMenuItem("CONUDS_CORR") = Val(drMenuItem("CONUDS_CORR")) + 1
                    Genera_Codigo_NoPersonal = "1111" & Format(NumCod, "0000")
                Else
                    NumCod = Nz(drMenuItem("CONUDS_CORR"))
                    Genera_Codigo_NoPersonal = "1111" & Format(NumCod, "0000")
                End If
            Next
        End If
    End Function
End Class
