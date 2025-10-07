Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Relacion_Ticket
    Public Function Actualizar_Estado_Ticket(ByVal psConexion As String, ByVal Estado As String,
                                        ByVal Fecha As String, ByVal Hora As String,
                                        ByVal Codigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_ESTADO_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = Estado
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = Fecha
        Cmd.Parameters.Add("@HORA", SqlDbType.VarChar).Value = Hora
        Cmd.Parameters.Add("@CODIGO", SqlDbType.Float).Value = Codigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_ESTADO_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Traking_Correos_Enviados(ByVal psConexion As String, ByVal nroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_CORREO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@NroTicket", nroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_CORREO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Traking_Acciones(ByVal psConexion As String, ByVal nroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ACCIONES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@CodTicket", nroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ACCIONES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Procedimientos(ByVal psConexion As String, ByVal tipoTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_PROCEDIMIENTOS_A_SEGUIR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TIPO_TICKET", tipoTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_PROCEDIMIENTOS_A_SEGUIR")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Tareas(ByVal psConexion As String, ByVal codTarea As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_TAREAS_A_SEGUIR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_TAREA", codTarea)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_TAREAS_A_SEGUIR")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Mostrar_Ticket(ByVal psConexion As String, ByVal nroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_MOSTRAR_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NRO_TICKET", nroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_MOSTRAR_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Relacion_Estado_Ticket_Cliente(ByVal psConexion As String, ByVal TIcketEstado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_RELACION_ESTADO_TICKET_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@psTicket_Estado", SqlDbType.VarChar).Value = TIcketEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_RELACION_ESTADO_TICKET_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Atualizar_Estado_Ticket_Cliente(ByVal psConexion As String, ByVal EstadoCliente As String,
                                                    ByVal EstadoCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_ESTADO_TICKET_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ESTADOCLIENTE", SqlDbType.VarChar).Value = EstadoCliente
        Cmd.Parameters.Add("@ESTADOCODIGO", SqlDbType.Float).Value = EstadoCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_ESTADO_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Actualizar_Traking_Estado_Ticket(ByVal psConexion As String, ByVal HoraFin As String,
                                        ByVal FechaFin As String, ByVal AprobCodigo As Double,
                                        ByVal EstadoCliente As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_TRAKING_ESTADO_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@HORAFIN", SqlDbType.VarChar).Value = HoraFin
        Cmd.Parameters.Add("@FECHAFIN", SqlDbType.VarChar).Value = FechaFin
        Cmd.Parameters.Add("@APROBCODIGO", SqlDbType.Float).Value = AprobCodigo
        Cmd.Parameters.Add("@ESTADOCLIENTE", SqlDbType.VarChar).Value = EstadoCliente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_TRAKING_ESTADO_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_Accion_Traking_Estado_Cliente(ByVal psConexion As String, ByVal NumTicket As Double,
                                        ByVal FechaRegistro As String, ByVal HoraRegistro As String, ByVal EstadoRegistro As String,
                                        ByVal UsuarioRegistro As String, ByVal RegistroObservacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_ACCION_TRAKING_ESTADO_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NUMTICKET", SqlDbType.Float).Value = NumTicket
        Cmd.Parameters.Add("@FECHAREGISTRO", SqlDbType.VarChar).Value = FechaRegistro
        Cmd.Parameters.Add("@HORAREGISTRO", SqlDbType.VarChar).Value = HoraRegistro
        Cmd.Parameters.Add("@ESTADOREGISTRO", SqlDbType.VarChar).Value = EstadoRegistro
        Cmd.Parameters.Add("@USUARIOREGISTRO", SqlDbType.VarChar).Value = UsuarioRegistro
        Cmd.Parameters.Add("@REGISTROOBSERVACION", SqlDbType.VarChar).Value = RegistroObservacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_ACCION_TRAKING_ESTADO_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Ingresar_Traking_Accion_Estado(ByVal psConexion As String, ByVal NumTicket As Double,
                                                   ByVal FechaAccion As String, ByVal HoraAccion As String,
                                                   ByVal AccionUser As String, ByVal AccionReferencia As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_TRAKING_ACCION_ESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NUMTICKET", SqlDbType.Float).Value = NumTicket
        Cmd.Parameters.Add("@FECHAACCION", SqlDbType.VarChar).Value = FechaAccion
        Cmd.Parameters.Add("@HORAACCION", SqlDbType.VarChar).Value = HoraAccion
        Cmd.Parameters.Add("@ACCIONUSER", SqlDbType.VarChar).Value = AccionUser
        Cmd.Parameters.Add("@ACCIONREFERENCIA", SqlDbType.Float).Value = AccionReferencia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_TRAKING_ACCION_ESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    '
    '
    'ACCIONES VARIOS
    '
    '
    '

    Public Function Lista_Acciones_Varios(ByVal psConexion As String, ByVal NroTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_ACCIONES_VARIOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NroTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_ACCIONES_VARIOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_Acciones_Varias(ByVal psConexion As String, ByVal NumTicket As String, ByVal Correlativo As String, ByVal AccionCod As String,
                                        ByVal AccionDescripcion As String, ByVal Canal As String, ByVal UsuarioAccion As String,
                                        ByVal FechaAccion As String, ByVal HoraAccion As String, ByVal HoraFinAccion As String,
                                        ByVal TicketContacto As String, ByVal TicketEstado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_ACCIONES_VARIAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NUMTICKET", NumTicket)
        Cmd.Parameters.AddWithValue("@CORRELATIVO", Correlativo)
        Cmd.Parameters.AddWithValue("@ACCIONCOD", AccionCod)
        Cmd.Parameters.AddWithValue("@ACCIONDESCRIPCION", AccionDescripcion)
        Cmd.Parameters.AddWithValue("@CANAL", Canal)
        Cmd.Parameters.AddWithValue("@USUARIOACCION", UsuarioAccion)
        Cmd.Parameters.AddWithValue("@FECHAACCION", FechaAccion)
        Cmd.Parameters.AddWithValue("@HORAACCION", HoraAccion)
        Cmd.Parameters.AddWithValue("@HORAFINACCION", HoraFinAccion)
        Cmd.Parameters.AddWithValue("@TICKETCONTACTO", TicketContacto)
        Cmd.Parameters.AddWithValue("@TICKETESTADO", TicketEstado)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_ACCIONES_VARIAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Canal(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_CANAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_CANAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Accion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ACCION_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ACCION_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Contacto(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_CONTACTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_CONTACTO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Codigo(ByVal psConexion As String, ByVal CodCliente As String) As String
        Dim TxtCorrelativoAccion As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT COUNT(*) FROM TBTICKET_DETALLE WHERE TICKET_CODIGO = " & CodCliente
            Rs = CmdGlobal.ExecuteReader

            If Rs.HasRows Then
                While Rs.Read
                    TxtCorrelativoAccion = 1 + Rs(0)
                End While
            Else
                TxtCorrelativoAccion = 1
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return TxtCorrelativoAccion
    End Function

    Public Function Reabrir_Ticket(ByVal psConexion As String, ByVal FechaReabierto As String, ByVal HoraReabierto As String,
                                        ByVal Usuario As String, ByVal EstadoFecha As String, ByVal EstadHora As String,
                                        ByVal Motivo As String, ByVal NumTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_REABRIR_TICKET_CAMBIAR_ESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FECHAREABIERTO", FechaReabierto)
        Cmd.Parameters.AddWithValue("@HORAREABIERTO", HoraReabierto)
        Cmd.Parameters.AddWithValue("@USUARIO", Usuario)
        Cmd.Parameters.AddWithValue("@ESTADOFECHA", EstadoFecha)
        Cmd.Parameters.AddWithValue("@ESTADOHORA", EstadHora)
        Cmd.Parameters.AddWithValue("@MOTIVO", Motivo)
        Cmd.Parameters.AddWithValue("@NUMTICKET", NumTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_REABRIR_TICKET_CAMBIAR_ESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Usuarios_Ticket(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_USUARIOS_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_USUARIOS_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Asignar_Ticket(ByVal psConexion As String, ByVal NumTicket As String,
                                   ByVal Usuario As String, ByVal FechaReabierto As String,
                                   ByVal FechaVisto As String, ByVal HoraReabierto As String,
                                   ByVal HoraVisto As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_UPD_ASIGNAR_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NumTicket)
        Cmd.Parameters.AddWithValue("@USUARIO", Usuario)
        Cmd.Parameters.AddWithValue("@FECHAASIGNADO", FechaReabierto)
        Cmd.Parameters.AddWithValue("@FECHAVISTO", FechaVisto)
        Cmd.Parameters.AddWithValue("@HORAASIGNADO", HoraReabierto)
        Cmd.Parameters.AddWithValue("@HORAVISTO", HoraVisto)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_UPD_ASIGNAR_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Insertar_Asignacion(ByVal psConexion As String, ByVal NumTicket As String,
                                        ByVal FechaReg As String, ByVal HoraReg As String,
                                        ByVal RegUsuario As String, ByVal Asesor As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_ASIGNACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NumTicket)
        Cmd.Parameters.AddWithValue("@FECHAREG", FechaReg)
        Cmd.Parameters.AddWithValue("@HORAREG", HoraReg)
        Cmd.Parameters.AddWithValue("@REGUSUARIO", RegUsuario)
        Cmd.Parameters.AddWithValue("@ASESOR", Asesor)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_ASIGNACION")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Insertar_Acciones_Ticket(ByVal psConexion As String, ByVal NumTicket As String,
                                             ByVal Accion As String,
                                             ByVal FechaReg As String, ByVal HoraReg As String,
                                             ByVal AccionUser As String, ByVal Referencia As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_ACCIONES_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NumTicket)
        Cmd.Parameters.AddWithValue("@ACCION", Accion)
        Cmd.Parameters.AddWithValue("@FECHAREG", FechaReg)
        Cmd.Parameters.AddWithValue("@HORAREG", HoraReg)
        Cmd.Parameters.AddWithValue("@ACCIONUSER", AccionUser)
        Cmd.Parameters.AddWithValue("@REFERENCIA", Referencia)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_ACCIONES_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Campos_Ticket(ByVal psConexion As String, ByVal NumTicket As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_BUSCAR_CAMPOS_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NROTICKET", NumTicket)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_BUSCAR_CAMPOS_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Ticket_Reprogramado(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TICKET_REPROGRAMADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TICKET_REPROGRAMADO")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
