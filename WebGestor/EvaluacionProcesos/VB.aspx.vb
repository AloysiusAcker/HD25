Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Data

Partial Class EvaluacionProcesos_VB
    Inherits System.Web.UI.Page

    Protected Sub FindCoordinates(sender As Object, e As EventArgs)

        'Dim url As String = "http://maps.google.com/maps/api/geocode/xml?address=" + txtLocation.Text + "&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"
        Dim url As String = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + txtLocation.Text + "&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"

        Dim request As WebRequest = WebRequest.Create(url)
        Using response As WebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                Dim dsResult As New DataSet()
                dsResult.ReadXml(reader)
                Dim dtCoordinates As New DataTable()
                dtCoordinates.Columns.AddRange(New DataColumn(3) {New DataColumn("Id", GetType(Integer)), New DataColumn("Address", GetType(String)), New DataColumn("Latitude", GetType(String)), New DataColumn("Longitude", GetType(String))})
                For Each row As DataRow In dsResult.Tables("result").Rows
                    Dim geometry_id As String = dsResult.Tables("geometry").[Select]("result_id = " + row("result_id").ToString())(0)("geometry_id").ToString()
                    Dim location As DataRow = dsResult.Tables("location").[Select](Convert.ToString("geometry_id = ") & geometry_id)(0)
                    dtCoordinates.Rows.Add(row("result_id"), row("formatted_address"), location("lat"), location("lng"))
                Next
                GridView1.DataSource = dtCoordinates
                GridView1.DataBind()
            End Using
        End Using
    End Sub

End Class
