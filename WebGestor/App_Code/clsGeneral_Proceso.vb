
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsGeneral_Proceso
    Dim objSeg As New ModuloSeguridad
    Public Sub Llena_GrupoEmpresa(ByVal cbo As DropDownList, ByVal psUser As String)
        Dim dt As DataTable
        cbo.Items.Clear()
        dt = objSeg.Lista_GrupoEmpresa(psUser, "1")
        cbo.DataSource = dt
        cbo.DataTextField = "GE_NOMBRE"
        cbo.DataValueField = "GRPOEMPRESA_CODIGO"
        cbo.DataBind()
    End Sub
    Public Sub Llena_Empresa(ByVal psUser As String, ByVal pdCodGrupo As Double,
                             ByVal cbo As DropDownList)
        Dim objSeg As New ModuloSeguridad
        cbo.Items.Clear()
        cbo.DataSource = objSeg.Lista_Empresa(psUser, pdCodGrupo, "1")
        cbo.DataTextField = "GEE_NOMBRE"
        cbo.DataValueField = "EMPRESA_CODIGO"
        cbo.DataBind()
        cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
    End Sub

    Public Function Prc_Ventas_xMes(ByVal psConexion As String, ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_xMes", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@psAño", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_xMes")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Prc_Ventas_xProductos(ByVal psConexion As String, ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_xProductos", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@psAño", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_xProductos")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
