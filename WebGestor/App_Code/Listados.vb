Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class Listados
    Public Function Listar_Pagina(ByVal pCodEmpresa As String, ByVal pCodGrupoEmpresa As Double, ByVal pMenuCod As Double, ByVal pTodo As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Lista_PaginaMenuSelect", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = pCodGrupoEmpresa
        Cmd.Parameters.Add("@MenuCod", SqlDbType.Int).Value = pMenuCod
        Cmd.Parameters.Add("@Todo", SqlDbType.VarChar).Value = pTodo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Lista_PaginaMenuSelect")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Provincia(ByVal pCodDpto As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTAR_PROVINCIAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodDepartamento", SqlDbType.VarChar).Value = pCodDpto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTAR_PROVINCIAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Distrito(ByVal pCodDpto As String, ByVal pCodProvincia As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTAR_DISTRITOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodDepartamento", SqlDbType.VarChar).Value = pCodDpto
        Cmd.Parameters.Add("@CodProvincia", SqlDbType.VarChar).Value = pCodProvincia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTAR_DISTRITOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Autoriza_IngElemento(ByVal pUserCodigo As String, ByVal pMenuCod As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Permite_IngresarElemento", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = pUserCodigo
        Cmd.Parameters.Add("@MenuCod", SqlDbType.VarChar).Value = pMenuCod
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Permite_IngresarElemento")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Campos(ByVal pMenuCod As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTAR_CAMPOSAINGRESAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodMenu", SqlDbType.Int).Value = pMenuCod
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTAR_CAMPOSAINGRESAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Categoria(ByVal pCodGrupoEmpresa As Double, ByVal pCodEmpresa As String, ByVal pCodMenu As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTAR_CATEGORIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = pCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodMenu", SqlDbType.Int).Value = pCodMenu
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTAR_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Elemento(ByVal pMenuCodElemento As Double, ByVal pCodGrupoEmpresa As Double, ByVal pCodEmpresa As String, ByVal pCodMenu As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTAR_ELEMENTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@MenuCodElemento", SqlDbType.Int).Value = pMenuCodElemento
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = pCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@MenuCod", SqlDbType.Int).Value = pCodMenu
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTAR_ELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ultimo_Elemento() As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("ULTIMO_ELEMENTO", Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ULTIMO_ELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_CodMenu(ByVal pNombreMenu As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Devolver_CodMenuSelect", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NomMenu", SqlDbType.VarChar).Value = pNombreMenu
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Devolver_CodMenuSelect")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
