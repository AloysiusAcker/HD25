Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor

Public Class Insertar
    Public Function Insertar_Comentario(ByVal pCodEmpresa As String, ByVal pEmpresa As String,
                                ByVal pPerContacto As String, ByVal pDireccion As String,
                                ByVal pCodPostal As String, ByVal pPais As String,
                                ByVal pProvincia As String, ByVal pDistrito As String,
                                ByVal pTelefono As String, ByVal pEmail As String,
                                ByVal pComentario As String, ByVal pDpto As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Insertar_Comentarios", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Empresa", SqlDbType.VarChar).Value = pEmpresa
        Cmd.Parameters.Add("@PerContacto", SqlDbType.VarChar).Value = pPerContacto
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = pDireccion
        Cmd.Parameters.Add("@CodPostal", SqlDbType.VarChar).Value = pCodPostal
        Cmd.Parameters.Add("@Pais", SqlDbType.VarChar).Value = pPais
        Cmd.Parameters.Add("@Provincia", SqlDbType.VarChar).Value = pProvincia
        Cmd.Parameters.Add("@Distrito", SqlDbType.VarChar).Value = pDistrito
        Cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = pTelefono
        Cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = pEmail
        Cmd.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = pComentario
        Cmd.Parameters.Add("@Dpto", SqlDbType.VarChar).Value = pDpto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Insertar_Comentarios")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_RelacionUserLink(ByVal pUserCodigo As String, ByVal pUserLink As Double,
                                              ByVal pQueHace As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("INS_RELACION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = pUserCodigo
        Cmd.Parameters.Add("@UserLink", SqlDbType.Int).Value = pUserLink
        Cmd.Parameters.Add("@QueHace", SqlDbType.Int).Value = pQueHace
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INS_RELACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Elemento(ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                            ByVal Menucod As Double, ByVal Nombre As String,
                            ByVal NombreHtml As String, ByVal Categoria As Double,
                            ByVal DescripCorta As String, ByVal DescripLarga As String,
                            ByVal UserCodigo As String, ByVal Fecha As String,
                            ByVal Completar1 As String, ByVal Completar2 As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("INS_ELEMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@Menucod", SqlDbType.Int).Value = Menucod
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@NombreHtml", SqlDbType.VarChar).Value = NombreHtml
        Cmd.Parameters.Add("@Categoria", SqlDbType.Int).Value = Categoria
        Cmd.Parameters.Add("@DescripCorta", SqlDbType.VarChar).Value = DescripCorta
        Cmd.Parameters.Add("@DescripLarga", SqlDbType.VarChar).Value = DescripLarga
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = UserCodigo
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = Fecha
        Cmd.Parameters.Add("@Completar1", SqlDbType.VarChar).Value = Completar1
        Cmd.Parameters.Add("@Completar2", SqlDbType.VarChar).Value = Completar2
        'Cmd.Parameters.Add("@Imagen", SqlDbType.Text).Value = pImagen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INS_ELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Modificar_Elemento(ByVal pMenuCodElemento As Double, ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                        ByVal Menucod As Double, ByVal Nombre As String,
                        ByVal NombreHtml As String, ByVal Categoria As Double,
                        ByVal DescripCorta As String, ByVal DescripLarga As String,
                        ByVal UserCodigo As String, ByVal Fecha As String,
                        ByVal Completar1 As String, ByVal Completar2 As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("UPD_ELEMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@MenuCodElemento", SqlDbType.Int).Value = pMenuCodElemento
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@Menucod", SqlDbType.Int).Value = Menucod
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@NombreHtml", SqlDbType.VarChar).Value = NombreHtml
        Cmd.Parameters.Add("@Categoria", SqlDbType.Int).Value = Categoria
        Cmd.Parameters.Add("@DescripCorta", SqlDbType.VarChar).Value = DescripCorta
        Cmd.Parameters.Add("@DescripLarga", SqlDbType.VarChar).Value = DescripLarga
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = UserCodigo
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = Fecha
        Cmd.Parameters.Add("@Completar1", SqlDbType.VarChar).Value = Completar1
        Cmd.Parameters.Add("@Completar2", SqlDbType.VarChar).Value = Completar2
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("UPD_ELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Elemento2(ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                           ByVal Menucod As Double, ByVal Nombre As String,
                           ByVal NombreHtml As String, ByVal Categoria As Double,
                           ByVal DescripCorta As String, ByVal DescripLarga As String,
                           ByVal UserCodigo As String, ByVal Fecha As String,
                           ByVal Completar1 As String, ByVal Completar2 As String, ByVal pImagen As Byte()) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("INS_ELEMENTO2", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@Menucod", SqlDbType.Int).Value = Menucod
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@NombreHtml", SqlDbType.VarChar).Value = NombreHtml
        Cmd.Parameters.Add("@Categoria", SqlDbType.Int).Value = Categoria
        Cmd.Parameters.Add("@DescripCorta", SqlDbType.VarChar).Value = DescripCorta
        Cmd.Parameters.Add("@DescripLarga", SqlDbType.VarChar).Value = DescripLarga
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = UserCodigo
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = Fecha
        Cmd.Parameters.Add("@Completar1", SqlDbType.VarChar).Value = Completar1
        Cmd.Parameters.Add("@Completar2", SqlDbType.VarChar).Value = Completar2
        Cmd.Parameters.Add("@Imagen", SqlDbType.Text).Value = pImagen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INS_ELEMENTO2")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class

