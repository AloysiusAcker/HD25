Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Contenido_Correo
    Public Function Lista_Contenido_Correo(ByVal psConexion As String, ByVal Tipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_CONTENIDO_EMAIL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TIPO", Tipo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_CONTENIDO_EMAIL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Tipo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_TIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_TIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Tipo(ByVal psConexion As String, ByVal Nombre As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INGRESAR_TIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NOMBRE", Nombre)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INGRESAR_TIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Elimina_Cliente(ByVal psConexion As String, ByVal Codigo As String, ByVal Nombre As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ELIMINAR_TIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@NOMBRE", Nombre)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ELIMINAR_TIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Email(ByVal psConexion As String, ByVal Tipo As String, ByVal Nombre As String,
                                   ByVal Asunto As String, ByVal Saludo As String, ByVal Cuerpo As String,
                                   ByVal NombreImagen As String, ByVal Despedida As String, ByVal Firma As String,
                                   ByVal NombreFirmaImagen As String, ByVal Imagen As Byte(), ByVal FirmaImagen As Byte()) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INGRESAR_EMAIL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TIPO", Tipo)
        Cmd.Parameters.AddWithValue("@NOMBRE", Nombre)
        Cmd.Parameters.AddWithValue("@ASUNTO", Asunto)
        Cmd.Parameters.AddWithValue("@SALUDO", Saludo)
        Cmd.Parameters.AddWithValue("@CUERPO", Cuerpo)
        Cmd.Parameters.AddWithValue("@NOMBREIMAGEN", NombreImagen)
        Cmd.Parameters.AddWithValue("@DESPEDIDA", Despedida)
        Cmd.Parameters.AddWithValue("@FIRMA", Firma)
        Cmd.Parameters.AddWithValue("@NOMBREFIRMAIMAGEN", NombreFirmaImagen)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = Imagen
        Dim imageFirmaParam As SqlParameter = Cmd.Parameters.Add("@FIRMAIMAGEN", System.Data.SqlDbType.Image)
        imageFirmaParam.Value = FirmaImagen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INGRESAR_EMAIL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Actualizar_Email(ByVal psConexion As String, ByVal Tipo As String, ByVal Nombre As String,
                                   ByVal Asunto As String, ByVal Saludo As String, ByVal Cuerpo As String,
                                   ByVal NombreImagen As String, ByVal Despedida As String, ByVal Firma As String,
                                   ByVal NombreFirmaImagen As String, ByVal Imagen As Byte(), ByVal FirmaImagen As Byte(),
                                   ByVal Correlativo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ACTUALIZAR_EMAIL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TIPO", Tipo)
        Cmd.Parameters.AddWithValue("@NOMBRE", Nombre)
        Cmd.Parameters.AddWithValue("@ASUNTO", Asunto)
        Cmd.Parameters.AddWithValue("@SALUDO", Saludo)
        Cmd.Parameters.AddWithValue("@CUERPO", Cuerpo)
        Cmd.Parameters.AddWithValue("@NOMBREIMAGEN", NombreImagen)
        Cmd.Parameters.AddWithValue("@DESPEDIDA", Despedida)
        Cmd.Parameters.AddWithValue("@FIRMA", Firma)
        Cmd.Parameters.AddWithValue("@NOMBREFIRMAIMAGEN", NombreFirmaImagen)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = Imagen
        Dim imageFirmaParam As SqlParameter = Cmd.Parameters.Add("@FIRMAIMAGEN", System.Data.SqlDbType.Image)
        imageFirmaParam.Value = FirmaImagen
        Cmd.Parameters.AddWithValue("@CORRELATIVO", Correlativo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ACTUALIZAR_EMAIL")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
