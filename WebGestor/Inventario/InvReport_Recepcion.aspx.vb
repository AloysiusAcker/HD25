Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports WebGestor
Partial Class Inventario_InvReport_Recepcion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Session("CodEmpresa") = "0001"
            'ReportViewer1.ProcessingMode = ProcessingMode.Local
            'ReportViewer1.LocalReport.ReportPath = "Inventario\Report_Recepcion.rdlc"
            'Dim Informe_Recepcion = GetData()
            'Dim datasource As New ReportDataSource("Informe_Recepcion", Informe_Recepcion)
            'ReportViewer1.LocalReport.DataSources.Clear()
            'ReportViewer1.LocalReport.DataSources.Add(datasource)
        End If
    End Sub

    Private Function GetData() As DataTable
        Dim conString As String = Session("Ruta_Emp")
        Dim cmd As New SqlCommand("Prc_Reporte_Recepcion")
        Using con As New SqlConnection(conString)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
                cmd.Parameters.Add("@CodRecep", SqlDbType.Float).Value = Session("CodRecep")
                sda.SelectCommand = cmd
                Dim dt As New DataTable
                sda.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

End Class
