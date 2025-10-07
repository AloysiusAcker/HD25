Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Contactos_Cliente
    Public Function Lista_Contactos_Clientes(ByVal psConexion As String, ByVal codCliente As String, ByVal Contacto As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_GTP_LISTA_CLIENTES_CONTACTOS]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@Cliente", codCliente)
        Cmd.Parameters.AddWithValue("@Contacto", Contacto)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_GTP_LISTA_CLIENTES_CONTACTOS]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Clientes(ByVal psConexion As String, ByVal RazonSocial As String, ByVal CIF As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_GTP_LIST_CLIENTES]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@RAZONSOCIAL", RazonSocial)
        Cmd.Parameters.AddWithValue("@CIF", CIF)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_GTP_LIST_CLIENTES]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_Contactos(ByVal psConexion As String, ByVal CodCliente As String, ByVal ApePaterno As String,
                                       ByVal ApeMaterno As String, ByVal Nombres As String, ByVal Telefono As String,
                                       ByVal Celular As String, ByVal Email As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_GTP_INSERTAR_CONTACTOS]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO_CLIENTE", CodCliente)
        Cmd.Parameters.AddWithValue("@APE_PATERNO", ApePaterno)
        Cmd.Parameters.AddWithValue("@APE_MATERNO", ApeMaterno)
        Cmd.Parameters.AddWithValue("@NOMBRES", Nombres)
        Cmd.Parameters.AddWithValue("@TELEFONO", Telefono)
        Cmd.Parameters.AddWithValue("@CELULAR", Celular)
        Cmd.Parameters.AddWithValue("@EMAIL", Email)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_GTP_INSERTAR_CONTACTOS]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Actualizar_Contactos(ByVal psConexion As String, ByVal CodCliente As String, ByVal ApePaterno As String,
                                       ByVal ApeMaterno As String, ByVal Nombres As String, ByVal Telefono As String,
                                       ByVal Celular As String, ByVal Email As String, ByVal Codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ACTUALIZAR_CONTACTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO_CLIENTE", CodCliente)
        Cmd.Parameters.AddWithValue("@APE_PATERNO", ApePaterno)
        Cmd.Parameters.AddWithValue("@APE_MATERNO", ApeMaterno)
        Cmd.Parameters.AddWithValue("@NOMBRES", Nombres)
        Cmd.Parameters.AddWithValue("@TELEFONO", Telefono)
        Cmd.Parameters.AddWithValue("@CELULAR", Celular)
        Cmd.Parameters.AddWithValue("@EMAIL", Email)
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ACTUALIZAR_CONTACTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
