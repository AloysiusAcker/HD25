
Partial Class camara
    Inherits System.Web.UI.Page

    Public Property vImgPrev As String
        Get
            Return Session("vImgPrev").ToString().Trim()
        End Get
        Set(ByVal value As String)
            Session("vImgPrev") = value
        End Set
    End Property
    Public Property Lista_PersonaBE As List(Of PersonaBE)
        Get
            If Session("Lista_PersonaBE") IsNot Nothing Then
                Return CType(Session("Lista_PersonaBE"), List(Of PersonaBE))
            Else
                Session("Lista_PersonaBE") = New List(Of PersonaBE)()
                Return CType(Session("Lista_PersonaBE"), List(Of PersonaBE))
            End If
        End Get
        Set(ByVal value As List(Of PersonaBE))
            Session("Lista_PersonaBE") = value
        End Set
    End Property

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Page.IsPostBack = False Then
            vImgPrev = Nothing

            Dim Lst = New List(Of PersonaBE)
            Dim vCarpeta As String = System.Web.HttpContext.Current.Server.MapPath("uploads/foto")
            Dim vArchivos() As String = IO.Directory.GetFiles(vCarpeta)
            For Each vArchivo As String In vArchivos
                Dim vExt As Boolean = False
                Select Case IO.Path.GetExtension(vArchivo.ToLower())
                    Case ".png"
                        vExt = True
                    Case ".gif"
                        vExt = True
                    Case ".jpg"
                        vExt = True
                End Select
                If vExt Then
                    Dim ms = New System.IO.MemoryStream(IO.File.ReadAllBytes(vArchivo))
                    Dim tmp = ms.ToArray()
                    Dim vDNi = IO.Path.GetFileNameWithoutExtension(vArchivo)
                    Dim vCreacion = IO.File.GetCreationTime(vArchivo)
                    Lst.Add(New PersonaBE() With {
                        .PERSON_C_CODIGO = vDNi,
                        .PERSON_I_FOTO = tmp,
                        .FECHA_CREACION = vCreacion})
                End If
            Next
            Lista_PersonaBE = Lst
            OrdenarItems()
            Carga_repFotos()

            If Not Request.QueryString("p1") Is Nothing Then
                hndQR.Value = Request.QueryString("p1").ToString().Trim()
            Else
                hndQR.Value = ""
            End If
        End If
    End Sub

    Private Sub OrdenarItems()
        Dim lst = New List(Of PersonaBE)
        lst.AddRange(Lista_PersonaBE.OrderByDescending(Function(n) n.FECHA_CREACION))
        Lista_PersonaBE = lst
    End Sub

    Private Sub Carga_repFotos()
        repFotos.DataSource = Lista_PersonaBE
        repFotos.DataBind()
    End Sub
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim strScript As String = ""
        Try
            If Not vImgPrev Is Nothing Then
                Dim vArchivo As String = vImgPrev
                Dim ms = New IO.MemoryStream(IO.File.ReadAllBytes(vArchivo))
                Dim tmp = ms.ToArray()
                Dim vDNi = IO.Path.GetFileNameWithoutExtension(vArchivo)
                Dim vCreacion = IO.File.GetCreationTime(vArchivo)

                Dim lst = Lista_PersonaBE
                Dim oPersonaBE = lst.FirstOrDefault(Function(n) n.PERSON_C_CODIGO = vDNi)
                If oPersonaBE Is Nothing Then
                    lst.Add(New PersonaBE() With {
                                .PERSON_C_CODIGO = vDNi,
                                .PERSON_I_FOTO = tmp,
                                .FECHA_CREACION = vCreacion})
                Else
                    oPersonaBE.PERSON_I_FOTO = tmp
                    oPersonaBE.FECHA_CREACION = vCreacion
                End If
                Lista_PersonaBE = lst

                OrdenarItems()
                Carga_repFotos()
            End If
            vImgPrev = Nothing
            Response.Redirect("camara.aspx") 'limpio la variable DNI request y el hidden QR
        Catch ex As Exception
            strScript = "alert('" & ex.Message.Replace("'", "").Replace(vbCrLf, " ") & "')"
        Finally
            Dim guidKey = Guid.NewGuid()
            ScriptManager.RegisterStartupScript(Me.UpdatePanel1, Me.UpdatePanel1.GetType(), guidKey.ToString(), strScript, True)
        End Try
    End Sub
    Protected Sub repFotos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repFotos.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Fila = CType(e.Item.DataItem, PersonaBE)
            If Not Fila Is Nothing Then
                Dim imgFotos = CType(e.Item.FindControl("imgFotos"), UI.HtmlControls.HtmlImage)
                Dim objDescrip = CType(e.Item.FindControl("objDescrip"), UI.HtmlControls.HtmlGenericControl)
                imgFotos.Src = "camara_.ashx?cod=" & Fila.PERSON_C_CODIGO
                objDescrip.InnerHtml = Fila.PERSON_C_CODIGO
            End If
        End If
    End Sub
End Class
