Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Agenda_Citas

    Public Function Buscar_Personal(ByVal psConexion As String, ByVal codPersonal As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_BUSCA_PERSONAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_PERSONAL", codPersonal)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_BUSCA_PERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
