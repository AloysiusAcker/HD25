Imports System.Data.SqlClient
Imports System.Data


Public Class ClsGtp_Listados
    Public Function GTP_ListaClientes(ByVal psConexion As String, ByVal psNombreCliente As String,
                                      ByVal psEstado As String, ByVal psAsignado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_CLIENTES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = psNombreCliente
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@Asignado", SqlDbType.VarChar).Value = psAsignado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_CLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_GTP_Lista_EstadoTickect
    Public Function GTP_ListaTickect(ByVal psConexion As String, ByVal psCodEstado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Tickect", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEstado", SqlDbType.VarChar).Value = psCodEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Tickect")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GTP_ListaEstadoTickect(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_GTP_Lista_EstadoTickect", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_GTP_Lista_EstadoTickect")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GTP_Lista_BusClientes(ByVal psConexion As String, ByVal psNombreCliente As String,
                                      ByVal psRuc As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_BusCliente", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = psNombreCliente
        Cmd.Parameters.Add("@Ruc", SqlDbType.VarChar).Value = psRuc
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_BusCliente")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GTP_ListaClientes_Top1(ByVal psConexion As String, ByVal psNombreCliente As String,
                                      ByVal psEstado As String, ByVal psAsignado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_CLIENTES_TOP1", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = psNombreCliente
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@Asignado", SqlDbType.VarChar).Value = psAsignado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_CLIENTES_TOP1")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GTP_ListaContactos_xCliente(ByVal psConexion As String, ByVal psCodCliente As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_CONTACTOS_xCLIENTE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodCliente", SqlDbType.VarChar).Value = psCodCliente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_CONTACTOS_xCLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GTP_Datos_Contacto(ByVal psConexion As String, ByVal psCodContacto As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_CLIENTE_CONTACTOS_DATOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodContacto", SqlDbType.VarChar).Value = psCodContacto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_CLIENTE_CONTACTOS_DATOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function GTP_Lista_AccionesxTicket(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal psCodTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ACCIONES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodTicket", SqlDbType.Float).Value = psCodTicket
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ACCIONES")
        Da.Fill(Dt)
        Return Dt
    End Function
    'GTP_CuadroMando_Lista1
    Public Function GTP_CuadroMando_1(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodProceso As Double, ByVal psAsesor As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("GTP_CuadroMando_Lista1", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = pdCodProceso
        Cmd.Parameters.Add("@Asesor", SqlDbType.VarChar).Value = psAsesor
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("GTP_CuadroMando_Lista1")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GTP_CuadroMando_5(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodProceso As Double, ByVal psAsesor As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("GTP_CuadroMando_Lista5", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = pdCodProceso
        Cmd.Parameters.Add("@Asesor", SqlDbType.VarChar).Value = psAsesor
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("GTP_CuadroMando_Lista5")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CRM_Tracking_Acciones(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ACCIONES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodTicket", SqlDbType.Float).Value = pdCodTicket
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ACCIONES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CRM_Tracking_Correos(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_CORREO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@NroTicket", SqlDbType.Float).Value = pdCodTicket
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_CORREO")
        Da.Fill(Dt)
        Return Dt
    End Function
    'PRC_GTPLISTA_TRAKING_LLAMADA
    Public Function CRM_Tracking_Llamadas(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_LLAMADA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodTicket", SqlDbType.Float).Value = pdCodTicket
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_LLAMADA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CRM_Tracking_Asignados(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ASIGNADOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodTicket", SqlDbType.Float).Value = pdCodTicket
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ASIGNADOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CRM_Tracking_Estados(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_TRAKING_ESTADO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@NroTicket", SqlDbType.Float).Value = pdCodTicket
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_TRAKING_ESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
