Imports System
Imports System.Web.Hosting
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_Dashboard
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load, Me.Load
        ' ... 
        ASPxDashboard1.SetConnectionStringsProvider(New MyDataSourceWizardConnectionStringsProvider())
        '            #Region "#DashboardStorage"

        Dim dashboardFileStorage As New DashboardFileStorage("~/App_Data/Dashboards")
        ASPxDashboard1.SetDashboardStorage(dashboardFileStorage)
        '#End Region ' #DashboardStorage

        '#Region "#DataSourceStorage"
        Dim sqlDataSource As New DashboardSqlDataSource("SQL Data Source", "sqlConnection")
        Dim countriesQuery As SelectQuery = SelectQueryFluentBuilder.
                AddTable("Countries").
                SelectColumns("Country", "Latitude", "Longitude", "Year", "EnergyType", "Production", "Import").
                Build("Countries")
        sqlDataSource.Queries.Add(countriesQuery)

        Dim dataSourceStorage As New DataSourceInMemoryStorage()
        dataSourceStorage.RegisterDataSource("sqlDataSource1", sqlDataSource.SaveToXml())
        ASPxDashboard1.SetDataSourceStorage(dataSourceStorage)
        '            #End Region ' #DataSourceStorage
    End Sub
    Protected Sub ASPxDashboard1_ConfigureDataConnection(ByVal sender As Object,
                                                             ByVal e As ConfigureDataConnectionWebEventArgs)
        If e.ConnectionName = "ConnectionSql" Then
            e.ConnectionParameters = New MsSqlConnectionParameters(NomServer, "BDGEmpresa" & Session("SiglaGrupoEmpresa"), "sa", "", MsSqlAuthorizationType.SqlServer)
        End If
        If e.ConnectionName = "sqlConnection" Then
            Dim databasePath As String = HostingEnvironment.MapPath("~/App_Data/DashboardEnergyStatictics.xml")
            e.ConnectionParameters = New XmlFileConnectionParameters(databasePath)
        End If
    End Sub
End Class
