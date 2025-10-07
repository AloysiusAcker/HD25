
Partial Class EvaluacionProcesos_CapturarImagen
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("Mycon").ToString())
            Dim cmd As SqlCommand = New SqlCommand("Usp_InsertImageDT", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@UserImagename", Nothing)
            cmd.Parameters.AddWithValue("@UserImagePath", Nothing)
            cmd.Parameters.AddWithValue("@UserID", 0)
            cmd.Parameters.AddWithValue("@QueryID", 2)
            Dim ds As DataSet = New DataSet()
            Dim da As SqlDataAdapter = New SqlDataAdapter()
            da.SelectCommand = cmd
            da.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                img.ImageUrl = ds.Tables(0).Rows(0)("UserImagePath").ToString()
            End If
        End If
    End Sub

    Private Sub Linkbutton1_Click(sender As Object, e As EventArgs) Handles Linkbutton1.Click
        Dim url As String = "/WebCam/Captureimage.aspx"
        Dim s As String = "window.open('" & url & "', 'popup_window', 'width=900,height=460,left=100,top=100,resizable=no');"
        ClientScript.RegisterStartupScript(Me.[GetType](), "script", s, True)
    End Sub
End Class
