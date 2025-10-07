Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class Cls_Eventos

    Public Function Lista_Eventos(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Eventos_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Eventos_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_XCodEventos(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pCodEvento As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Eventos_xCodigo]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pCodEvento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Eventos_xCodigo]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Eventos(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal pUser As String,
                                     ByVal pEvFechaIni As String, ByVal pEvFechaFin As String, ByVal pEveHoraIni As String,
                                     ByVal pEveHoraFin As String, ByVal pEveTipo As String, ByVal pEveNombre As String,
                                     ByVal pEveObjetivo As String, ByVal pEveDescripcion As String, ByVal pEveContacto As String,
                                     ByVal pEveContactoTel As String, ByVal pEveResponsable As String, ByVal pEveDireccion As String,
                                     ByVal pEvePais As String, ByVal pEveDpto As String, ByVal pEveProv As String, ByVal pEveDist As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Eventos_Insertar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@EventoFechaIni", SqlDbType.VarChar).Value = pEvFechaIni
        Cmd.Parameters.Add("@EventoFechaFin", SqlDbType.VarChar).Value = pEvFechaFin
        Cmd.Parameters.Add("@EventoHoraIni", SqlDbType.VarChar).Value = pEveHoraIni
        Cmd.Parameters.Add("@EventoHoraFin", SqlDbType.VarChar).Value = pEveHoraFin
        Cmd.Parameters.Add("@EventoTipo", SqlDbType.VarChar).Value = pEveTipo
        Cmd.Parameters.Add("@EventoNombre", SqlDbType.VarChar).Value = pEveNombre
        Cmd.Parameters.Add("@EventoObjetivo", SqlDbType.VarChar).Value = pEveObjetivo
        Cmd.Parameters.Add("@EventoDescripcion", SqlDbType.VarChar).Value = pEveDescripcion
        Cmd.Parameters.Add("@EventoContacto", SqlDbType.VarChar).Value = pEveContacto
        Cmd.Parameters.Add("@EventoContactoTel", SqlDbType.VarChar).Value = pEveContactoTel
        Cmd.Parameters.Add("@EventoResponsable", SqlDbType.VarChar).Value = pEveResponsable
        Cmd.Parameters.Add("@EventoDireccion", SqlDbType.VarChar).Value = pEveDireccion
        Cmd.Parameters.Add("@eventoPais", SqlDbType.VarChar).Value = pEvePais
        Cmd.Parameters.Add("@EventoDpto", SqlDbType.VarChar).Value = pEveDpto
        Cmd.Parameters.Add("@EventoProv", SqlDbType.VarChar).Value = pEveProv
        Cmd.Parameters.Add("@EventoDist", SqlDbType.VarChar).Value = pEveDist
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Eventos_Insertar")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualizar_Eventos(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal pUser As String,
                                       ByVal pEvFechaIni As String, ByVal pEvFechaFin As String, ByVal pEveHoraIni As String,
                                       ByVal pEveHoraFin As String, ByVal pEveTipo As String, ByVal pEveNombre As String,
                                       ByVal pEveObjetivo As String, ByVal pEveDescripcion As String, ByVal pEveContacto As String,
                                       ByVal pEveContactoTel As String, ByVal pEveResponsable As String, ByVal pCodEvento As Integer,
                                       ByVal pEveDireccion As String, ByVal pEvePais As String, ByVal pEveDpto As String,
                                       ByVal pEveProv As String, ByVal pEveDist As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Eventos_Update", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@EventoFechaIni", SqlDbType.VarChar).Value = pEvFechaIni
        Cmd.Parameters.Add("@EventoFechaFin", SqlDbType.VarChar).Value = pEvFechaFin
        Cmd.Parameters.Add("@EventoHoraIni", SqlDbType.VarChar).Value = pEveHoraIni
        Cmd.Parameters.Add("@EventoHoraFin", SqlDbType.VarChar).Value = pEveHoraFin
        Cmd.Parameters.Add("@EventoTipo", SqlDbType.VarChar).Value = pEveTipo
        Cmd.Parameters.Add("@EventoNombre", SqlDbType.VarChar).Value = pEveNombre
        Cmd.Parameters.Add("@EventoObjetivo", SqlDbType.VarChar).Value = pEveObjetivo
        Cmd.Parameters.Add("@EventoDescripcion", SqlDbType.VarChar).Value = pEveDescripcion
        Cmd.Parameters.Add("@EventoContacto", SqlDbType.VarChar).Value = pEveContacto
        Cmd.Parameters.Add("@EventoContactoTel", SqlDbType.VarChar).Value = pEveContactoTel
        Cmd.Parameters.Add("@EventoResponsable", SqlDbType.VarChar).Value = pEveResponsable
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pCodEvento
        Cmd.Parameters.Add("@EventoDireccion", SqlDbType.VarChar).Value = pEveDireccion
        Cmd.Parameters.Add("@eventoPais", SqlDbType.VarChar).Value = pEvePais
        Cmd.Parameters.Add("@EventoDpto", SqlDbType.VarChar).Value = pEveDpto
        Cmd.Parameters.Add("@EventoProv", SqlDbType.VarChar).Value = pEveProv
        Cmd.Parameters.Add("@EventoDist", SqlDbType.VarChar).Value = pEveDist
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Eventos_Update")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Ultimo_Evento(ByVal psConexion As String) As String
        Dim pdUltimoEvento As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT isnull(MAX(EVENTO_CODIGO),0) FROM TBEVENTOS"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdUltimoEvento = 1 + Rs(0)
                End While
            Else
                pdUltimoEvento = 1
            End If
            Rs.Close()
        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try
        Return pdUltimoEvento
    End Function
    Public Function Lista_Participantes_xEventos(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pEvento As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evento_Participante_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pEvento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evento_Participante_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Participantes_xEventos(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pEvento As Integer,
                                                    ByVal pCodPersonal As String, ByVal pFecha As String, ByVal pHoraIng As String,
                                                    ByVal pHoraSal As String, ByVal pUser As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Evento_Participante_Ins]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pEvento
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@HoraIng", SqlDbType.VarChar).Value = pHoraIng
        Cmd.Parameters.Add("@HoraSal", SqlDbType.VarChar).Value = pHoraSal
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Evento_Participante_Ins]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ultimo_Permiso(ByVal psConexion As String) As String
        Dim pdUltimoPermiso As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT isnull(MAX(PERMISO_REGISTRO),0) FROM TBPERSONAL_PERMISOS"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdUltimoPermiso = 1 + Rs(0)
                End While
            Else
                pdUltimoPermiso = 1
            End If
            Rs.Close()
        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try
        Return pdUltimoPermiso
    End Function
    Public Function Lista_Permisos(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Permisos_Lista]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Permisos_Lista]")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Insertar_Permisos(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal pUser As String,
                                      ByVal pPermisoTipo As String, ByVal pPermisoPersonal As String, ByVal pPermisoFechaIni As String,
                                      ByVal pPermisoFechaFin As String, ByVal pPermisoHoraIni As String, ByVal pPermisoHoraFin As String,
                                      ByVal pMotivo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Permisos_Insertar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@PermisoTipo", SqlDbType.VarChar).Value = pPermisoTipo
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pPermisoPersonal
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pPermisoFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pPermisoFechaFin
        Cmd.Parameters.Add("@HoraIni", SqlDbType.VarChar).Value = pPermisoHoraIni
        Cmd.Parameters.Add("@HoraFin", SqlDbType.VarChar).Value = pPermisoHoraFin '
        Cmd.Parameters.Add("@PermisoMotivo", SqlDbType.VarChar).Value = pMotivo
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Permisos_Insertar")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Permisos_Delete
    Public Function Eliminar_Permisos(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pCodPermiso As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Permisos_Delete]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodPermiso", SqlDbType.Int).Value = pCodPermiso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Permisos_Delete]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eventos_xParticipantes(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pCodEvento As Integer, ByVal pUser As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Eventos_Lista_xParticipantes]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pCodEvento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Eventos_Lista_xParticipantes]")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function EventosDatos_xCodigo(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pCodEvento As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Eventos_Datos_xCodigo]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pCodEvento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Eventos_Datos_xCodigo]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Eliminar_Participantes(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pCodEvento As Integer,
                                           ByVal pCodPersonal As String, ByVal pFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Evento_Participante_Delete]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodEvento", SqlDbType.Int).Value = pCodEvento
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Evento_Participante_Delete]")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class