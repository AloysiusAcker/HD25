Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Pantalla_Operador

    Public Function codigoInterno(ByVal psConexion As String) As String
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT CONCAT('0000',MAX(CALL_NUMREG) + 1) FROM TBCALLCENTER_DATOS_VARIOS"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    Return (Rs(0))
                End While
            Else
                Return "0001"
            End If
            Rs.Close()
        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try
        Return "0001"
    End Function

    Public Function Listar_Llamadas_Anteriores(ByVal psConexion As String, ByVal codCliente As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LIST_LLAMADAS_ANTERIORES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LIST_LLAMADAS_ANTERIORES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Combo_Respuesta(ByVal psConexion As String, ByVal contacto As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_RESPUESTA_XCONTACTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CONTACTO", contacto)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_RESPUESTA_XCONTACTO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Registrar_Llamada(ByVal psConexion As String, ByVal codCentral As String, ByVal fecha As String,
                                      ByVal hora As String, ByVal user As String, ByVal operador As String,
                                      ByVal fecCarga As String, ByVal fecActualizar As String, ByVal fecLlamada As String,
                                      ByVal horaLlamada As String, ByVal estCartera As String, ByVal estado As String,
                                      ByVal estAtencion As String, ByVal estOperacion As String, ByVal vecesLlamada As String,
                                      ByVal fecProceso As String, ByVal tipoPersona As String, ByVal ruc As String,
                                      ByVal razSocial As String, ByVal direccion As String, ByVal telefono As String,
                                      ByVal codPostal As String, ByVal codEstado As String, ByVal codDpto As String,
                                      ByVal codProvincia As String, ByVal codDistrito As String, ByVal ubigeo As String,
                                      ByVal callFono1 As String, ByVal callFono2 As String, ByVal callFono3 As String,
                                      ByVal callFono4 As String, ByVal callFono5 As String, ByVal procServidor As String,
                                      ByVal tipoNegocio As String, ByVal estProceso As String, ByVal persContacto As String,
                                      ByVal fecCompromiso As String, ByVal fecAllamar As String, ByVal horaAllamar As String,
                                      ByVal telfQllamar As String, ByVal nombrePersona As String, ByVal observacion As String,
                                      ByVal horaInicio As String, ByVal horaFin As String, ByVal codRespuesta As String,
                                      ByVal telfAyuda As String, ByVal telfNoExiste As String, ByVal telfNuevo As String,
                                      ByVal acccion As String, ByVal codCliente As String, ByVal nroTicket As String,
                                      ByVal nomAccion As String, ByVal correo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_LLAMADA_ENTRANTE_SALIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CENTRAL", codCentral)
        Cmd.Parameters.AddWithValue("@FECHA", fecha)
        Cmd.Parameters.AddWithValue("@HORA", hora)
        Cmd.Parameters.AddWithValue("@USER", user)
        Cmd.Parameters.AddWithValue("@OPERADOR", operador)
        Cmd.Parameters.AddWithValue("@FEC_CARGA", fecCarga)
        Cmd.Parameters.AddWithValue("@FEC_ACTUALIZAR", fecActualizar)
        Cmd.Parameters.AddWithValue("@FEC_LLAMADA", fecLlamada)
        Cmd.Parameters.AddWithValue("@HORA_LLAMADA", horaLlamada)
        Cmd.Parameters.AddWithValue("@EST_CARTERA", estCartera)
        Cmd.Parameters.AddWithValue("@ESTADO", estado)
        Cmd.Parameters.AddWithValue("@EST_ATENCION", estAtencion)
        Cmd.Parameters.AddWithValue("@EST_OPERACION", estOperacion)
        Cmd.Parameters.AddWithValue("@VECES_LLAMADA", vecesLlamada)
        Cmd.Parameters.AddWithValue("@FEC_PROCESO", fecProceso)
        Cmd.Parameters.AddWithValue("@TIPO_PERSONA", tipoPersona)
        Cmd.Parameters.AddWithValue("@RUC", ruc)
        Cmd.Parameters.AddWithValue("@RAZON_SOCIAL", razSocial)
        Cmd.Parameters.AddWithValue("@DIRECCION", direccion)
        Cmd.Parameters.AddWithValue("@TELEFONO", telefono)
        Cmd.Parameters.AddWithValue("@COD_POSTAL", codPostal)
        Cmd.Parameters.AddWithValue("@COD_ESTADO", codEstado)
        Cmd.Parameters.AddWithValue("@COD_DPTO", codDpto)
        Cmd.Parameters.AddWithValue("@COD_PROVINCIA", codProvincia)
        Cmd.Parameters.AddWithValue("@COD_DISTRITO", codDistrito)
        Cmd.Parameters.AddWithValue("@UBIGEO", ubigeo)
        Cmd.Parameters.AddWithValue("@CALL_FONO1", callFono1)
        Cmd.Parameters.AddWithValue("@CALL_FONO2", callFono2)
        Cmd.Parameters.AddWithValue("@CALL_FONO3", callFono3)
        Cmd.Parameters.AddWithValue("@CALL_FONO4", callFono4)
        Cmd.Parameters.AddWithValue("@CALL_FONO5", callFono5)
        Cmd.Parameters.AddWithValue("@PROC_SERVIDOR", procServidor)
        Cmd.Parameters.AddWithValue("@TIPO_NEGOCIO", tipoNegocio)
        Cmd.Parameters.AddWithValue("@EST_PROCESO", estProceso)
        Cmd.Parameters.AddWithValue("@PERS_CONTACTO", persContacto)
        Cmd.Parameters.AddWithValue("@FEC_COMPROMISO", fecCompromiso)
        Cmd.Parameters.AddWithValue("@HORA_ALLAMAR", horaAllamar)
        Cmd.Parameters.AddWithValue("@FEC_ALLAMAR", fecAllamar)
        Cmd.Parameters.AddWithValue("@TELF_QLLAMAR", telfQllamar)
        Cmd.Parameters.AddWithValue("@NOMBRE_PERSONA", nombrePersona)
        Cmd.Parameters.AddWithValue("@OBSERVACION", observacion)
        Cmd.Parameters.AddWithValue("@HORA_INICIO", horaInicio)
        Cmd.Parameters.AddWithValue("@HORA_FIN", horaFin)
        Cmd.Parameters.AddWithValue("@COD_RESPUESTA", codRespuesta)
        Cmd.Parameters.AddWithValue("@TELF_AYUDA", telfAyuda)
        Cmd.Parameters.AddWithValue("@TELF_NOEXISTE", telfNoExiste)
        Cmd.Parameters.AddWithValue("@TELF_NUEVO", telfNuevo)
        Cmd.Parameters.AddWithValue("@ACCION", acccion)
        Cmd.Parameters.AddWithValue("@COD_CLIENTE", codCliente)
        Cmd.Parameters.AddWithValue("@NRO_TICKET", nroTicket)
        Cmd.Parameters.AddWithValue("@NOM_ACCION", nomAccion)
        Cmd.Parameters.AddWithValue("@CORREO", correo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_LLAMADA_ENTRANTE_SALIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
