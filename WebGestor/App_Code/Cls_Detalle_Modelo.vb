Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Cls_Detalle_Modelo

    Public Function Filtrar_Descripcion_Detalle_Modelo(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal codModelo As String, ByVal codDetalle As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_TBINV_FILTRAR_DESCRIPCION_DETALLE_MODELO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FILTRODESC", FiltroDesc)
        Cmd.Parameters.AddWithValue("@COD_MODELO", codModelo)
        Cmd.Parameters.AddWithValue("@COD_DETALLE", codDetalle)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_TBINV_FILTRAR_DESCRIPCION_DETALLE_MODELO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CodigoModDetalle(ByVal psConexion As String, ByVal codMod As String) As String
        Dim codigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ARMODE_CODIGO) FROM TBINV_ARTICULO_MODELO_DETALLE WHERE ARTMOD_CODIGO like '" + codMod + "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    codigo = 1 + Rs(0)
                End While
            Else
                codigo = 0
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return codigo
    End Function

    Public Function RegistrarArticuloModeloDetalle(ByVal psConexion As String, ByVal Codigo As String,
                               ByVal codMod As String, ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_InsertArticuloModeloDetalle]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ARMODE_COD", Codigo)
        Cmd.Parameters.AddWithValue("@ARMOD_CODIGO", codMod)
        Cmd.Parameters.AddWithValue("@ARMOD_DESC", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_InsertArticuloModeloDetalle]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function ActualizarArticuloModeloDetalle(ByVal psConexion As String, ByVal Codigo As String,
                                                    ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_ActualizarArticuloModeloDetalle]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ARMODE_COD", Codigo)
        Cmd.Parameters.AddWithValue("@ARMOD_DESC", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_ActualizarArticuloModeloDetalle]")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function EliminarArticuloModeloDetalle(ByVal psConexion As String, ByVal codModeloDet As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_EliminarArticuloModeloDetalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ARMODE_COD", codModeloDet)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_EliminarArticuloModeloDetalle")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function llenarModeloDe(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_Llenar_ModeloDeta]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_Llenar_ModeloDeta]")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function ListarModelo_Detalle(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim cn As New SqlConnection(psConexion)
        Dim cmd As New SqlCommand("[Proc_Listar_Modelo_Detalle]", cn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@COD_MODELO", codigo)
        Dim Da As New SqlDataAdapter(cmd)
        Dim Dt As New DataTable("[Proc_Listar_Modelo_Detalle]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Modelo_Detalle(ByVal psConexion As String, ByVal codDetaMo As String, ByVal descripcion As String, ByVal cod_mod As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscModeloDetalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@cod_detalle", codDetaMo)
        Cmd.Parameters.AddWithValue("@descripcion", descripcion)
        Cmd.Parameters.AddWithValue("@cod_modelo", cod_mod)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscModeloDetalle")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
