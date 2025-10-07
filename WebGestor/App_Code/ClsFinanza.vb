Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Public Class ClsFinanza
    'Prc_Finanza_Lista_IngresosEgresos
    Public Function Filtrar_Descripcion_Almacen(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal psAño As String,
                                                ByVal psModulo As String, ByVal psFechaIni As String, ByVal psFechaFin As String,
                                                ByVal psPersona As Double, ByVal psMoneda As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Finanza_Lista_IngresosEgresos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@psAño", SqlDbType.VarChar).Value = psAño
        Cmd.Parameters.Add("@psModulo", SqlDbType.VarChar).Value = psModulo
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@psFechaFin", SqlDbType.VarChar).Value = psFechaFin
        Cmd.Parameters.Add("@psPersona", SqlDbType.Float).Value = psPersona
        Cmd.Parameters.Add("@psMoneda", SqlDbType.VarChar).Value = psMoneda
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Finanza_Lista_IngresosEgresos")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
