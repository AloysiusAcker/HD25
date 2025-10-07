Imports System.Data.SqlClient
Imports System.Data
Imports System.Net
Imports System.Net.Mail
Imports System.Windows.Forms

Public Class Cls_Enviar_Email
    Private Correos As New MailMessage
    Private Envios As New SmtpClient
    Public Function EnviarCorreo(ByVal psConexion As String, ByVal Saludo As String, ByVal Mensaje As String, ByVal Imagen As String, ByVal Despedida As String, ByVal Firma As String,
                            ByVal ImagenFirma As String, ByVal Asunto As String, ByVal Destinatario As String, ByVal Adjunto As String()) As String
        Try
            Correos.To.Clear()
            Correos.Body = ""
            Correos.Subject = ""
            Correos.Body = Saludo + vbCrLf + Mensaje + vbCrLf + Despedida + vbCrLf + Firma
            Correos.BodyEncoding = System.Text.Encoding.UTF8
            'Correos.BodyTransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable 
            Correos.Subject = Asunto
            Correos.IsBodyHtml = False

            Correos.To.Add(Trim(Destinatario))

            If Imagen <> "" Then
                Dim ArchImagen As Net.Mail.Attachment = New Net.Mail.Attachment(Imagen)
                Correos.Attachments.Add(ArchImagen)
            End If

            If Adjunto.Count > 0 Then
                For index = 0 To Adjunto.Count - 1
                    Dim Archivo As Net.Mail.Attachment = New Net.Mail.Attachment(Adjunto(index))
                    Correos.Attachments.Add(Archivo)
                Next
            End If

            If ImagenFirma <> "" Then
                Dim ArchFirma As Net.Mail.Attachment = New Net.Mail.Attachment(ImagenFirma)
                Correos.Attachments.Add(ArchFirma)
            End If

            Correos.From = New MailAddress("selm_03@hotmail.com")
            Envios.Credentials = New System.Net.NetworkCredential("selm_03@hotmail.com", "M4rt1n4Lu1s2920")

            Dim toEmail As String = "slimorales.3129@gmail.com"
            Dim subject As String = "Hola"
            Dim message As String = "hola"


            'Dim smtpClient As New SmtpClient("smtp.live.com")
            'smtpClient.Port = 25
            'smtpClient.EnableSsl = True
            'smtpClient.Credentials = New NetworkCredential("selm_03@hotmail.com", "M4rt1n4Lu1s2920")
            'Dim mail As New MailMessage("selm_03@hotmail.com", toEmail, subject, message)
            'smtpClient.Send(mail)


            ''- - - - Hotmail - - - - 
            'Envios.Host = "smtp.live.com"
            ''Envios.Port = 465

            ''- - - - Yahoo - - - - 
            ''Envios.Host = "smtp.mail.yahoo.com"
            ''Envios.Port = 465                  

            ''Envios.Host = "smtp.gmail.com"
            Envios.Port = 587

            Envios.EnableSsl = True

            Envios.Send(Correos)
            Return "1"
        Catch ex As Exception
            Return "2"
        End Try
    End Function

    Public Function Llenar_Combo_Informacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_INFORMACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_INFORMACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Ultimo_Envio(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_BUSCAR_ULTIMO_ENVIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_BUSCAR_ULTIMO_ENVIO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_Numero_Envios(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_BUSCAR_NUMERO_ENVIOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_BUSCAR_NUMERO_ENVIOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Insertar_Tabla_Correos(ByVal psConexion As String, ByVal NroTicket As String, ByVal Correo As String,
                                           ByVal Fecha As String, ByVal Hora As String, ByVal Estado As String,
                                           ByVal NroVez As String, ByVal TipoCorreo As String, ByVal CorreoCuerpo As String,
                                           ByVal CorreoProviene As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_TABLA_CORREOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NroTicket)
        Cmd.Parameters.AddWithValue("@CORREO", Correo)
        Cmd.Parameters.AddWithValue("@FECHA", Fecha)
        Cmd.Parameters.AddWithValue("@HORA", Hora)
        Cmd.Parameters.AddWithValue("@ESTADO", Estado)
        Cmd.Parameters.AddWithValue("@NROVEZ", NroVez)
        Cmd.Parameters.AddWithValue("@TIPOCORREO", TipoCorreo)
        Cmd.Parameters.AddWithValue("@CORREOCUERPO", CorreoCuerpo)
        Cmd.Parameters.AddWithValue("@CORREOPROVIENE", CorreoProviene)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_TABLA_CORREOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Insertar_Traking_Accion(ByVal psConexion As String, ByVal NroTicket As String, ByVal Fecha As String,
                                            ByVal Hora As String, ByVal Usuario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_TRAKING_ACCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NroTicket)
        Cmd.Parameters.AddWithValue("@FECHA", Fecha)
        Cmd.Parameters.AddWithValue("@HORA", Hora)
        Cmd.Parameters.AddWithValue("@USUARIO", Usuario)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_TRAKING_ACCION")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Insertar_Equipo_Lista_Correo(ByVal psConexion As String, ByVal CodCliente As String, ByVal Fecha As String,
                                                 ByVal Hora As String, ByVal Usuario As String, ByVal Destinatario As String,
                                                 ByVal CorreoCC As String, ByVal Asunto As String, ByVal Cuerpo As String,
                                                 ByVal NroVez As String, ByVal CorreoSysCre As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_EQUIPO_LISTA_CORREO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODCLIENTE", CodCliente)
        Cmd.Parameters.AddWithValue("@FECHA", Fecha)
        Cmd.Parameters.AddWithValue("@HORA", Hora)
        Cmd.Parameters.AddWithValue("@USUARIO", Usuario)
        Cmd.Parameters.AddWithValue("@DESTINATARIO", Destinatario)
        Cmd.Parameters.AddWithValue("@CORREOCC", CorreoCC)
        Cmd.Parameters.AddWithValue("@ASUNTO", Asunto)
        Cmd.Parameters.AddWithValue("@CUERPO", Cuerpo)
        Cmd.Parameters.AddWithValue("@NROVEZ", NroVez)
        Cmd.Parameters.AddWithValue("@CORREO_SYS_CRE", CorreoSysCre)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_EQUIPO_LISTA_CORREO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Empleados(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_EMPLEADOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_EMPLEADOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Documentos(ByVal psConexion As String, ByVal CodUsuario As String, ByVal NroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_DOCUMENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@USUARIO", CodUsuario)
        Cmd.Parameters.AddWithValue("@REFERENCIA", NroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_DOCUMENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Contactos(ByVal psConexion As String, ByVal CodCliente As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_BUSCLIENTES_CONTACTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodCliente", CodCliente)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_BUSCLIENTES_CONTACTOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Cuerpo_Email(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_CUERPO_EMAIL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_CUERPO_EMAIL")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_Campos_Cliente(ByVal psConexion As String, ByVal CodCliente As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_BUSCAR_CAMPOS_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODCLIENTE", CodCliente)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_BUSCAR_CAMPOS_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Traking_Correo(ByVal psConexion As String, ByVal NroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_CORREO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@NroTicket", NroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_CORREO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Traking_Acciones(ByVal psConexion As String, ByVal NroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ACCIONES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodTicket", NroTicket)
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ACCIONES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Traking_Estado(ByVal psConexion As String, ByVal NroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@NroTicket", NroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Combo_Tipo_Correo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_TIPO_CORREO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_TIPO_CORREO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Ayuda_Relacion_Datos(ByVal psConexion As String, ByVal CodRelacion As String, ByVal CodAplicacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_TEMAYUDA_RELACION_DATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodRelacion", CodRelacion)
        Cmd.Parameters.AddWithValue("@CodAplicacion", CodAplicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_TEMAYUDA_RELACION_DATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
