Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Propietario
    Public Function Filtrar_Descripcion_Propietario(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_TBINV_FILTRAR_DESCRIPCION_PROPIETARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FILTRODESC", FiltroDesc)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_TBINV_FILTRAR_DESCRIPCION_PROPIETARIO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Codigo2(ByVal psConexion As String) As String
        Dim TxtCodigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ALTIBI_CODIGO) FROM TBINV_ALMACEN_TIPOBIEN"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    TxtCodigo = 1 + Rs(0)
                End While
            Else
                TxtCodigo = 1
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return TxtCodigo
    End Function


    Public Function Lista_PropXDesc(ByVal psConexion As String, ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_lispordescrip", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@descrip", Descripcion)

        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_lispordescrip")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function RegistrarPropietario(ByVal psConexion As String, ByVal Codigo As String,
                                        ByVal Descripcion As String, ByVal Placabilidad As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_agregarPropietario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@cod", Codigo)
        Cmd.Parameters.AddWithValue("@descrip", Descripcion)
        Cmd.Parameters.AddWithValue("@placa_via", Placabilidad)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_agregarPropietario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function ActualizaPropietario(ByVal psConexion As String, ByVal Codigo As String,
                                        ByVal Descripcion As String, ByVal Placabilidad As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("proc_actualizarbien", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@cod", Codigo)
        Cmd.Parameters.AddWithValue("@descrip", Descripcion)
        Cmd.Parameters.AddWithValue("@placa_via", Placabilidad)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("proc_actualizarbien")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function EliminaPropietario(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Propietario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Eliminar_Propietario")
        Da.Fill(dt)
        Return dt
    End Function

    Public Function Agregar_Actualizar_Placa_TipoBien(ByVal psConexion As String, ByVal codigo As String,
                                           ByVal placaInicial As String, ByVal placaFinal As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Actualizar_Placa_TipoBien", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@PLACA_INICIAL", placaInicial)
        Cmd.Parameters.AddWithValue("@PLACA_FINAL", placaFinal)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Agregar_Actualizar_Placa_TipoBien")
        Da.Fill(dt)
        Return dt
    End Function

    Public Function Eliminar_Placa_TipoBien(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Placa_TipoBien", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Eliminar_Placa_TipoBien")
        Da.Fill(dt)
        Return dt
    End Function

End Class
