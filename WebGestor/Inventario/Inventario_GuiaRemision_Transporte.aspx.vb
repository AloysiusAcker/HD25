Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Inventario_Inventario_GuiaRemision_Transporte
    Inherits System.Web.UI.Page

    Dim obj As New clsInv_Listados

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            lblRegistroGuia.Text = ""

        End If
    End Sub
    Protected Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim fInv As New clsInv_Procesos
        Dim psArticulo As String = ""
        Dim pdCodUbica As Double = 0
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim psCodArticulo As String = ""
        Dim pdSaldo As Double = 0
        Dim ListaArt As String = ""
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Text = ""
        Dim psCodGuia As Double = 0
        Try
            dt = obj.Lista_GuiaTransportista_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
            Flexd.DataSource = dt
            Flexd.DataBind()
            If dt.Rows.Count > 0 Then
                lblRegistroGuia.Text = dt.Rows.Count & " Guías."
            Else
                lblRegistroGuia.Text = "No hay Guías"
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub Flexd_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flexd.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim i As Long = 0
        Dim psIngresar As String = "S"
        Try ' 
            If e.CommandName = "Cambiar" Then
                divEstado.Visible = True
                txtFecha.Text = FormatoFecha(FechaActual)
                TxtNroGuiaT.Text = Flexd.Rows(Index).Cells(2).Text
                TxtNroGuia.Text = Flexd.Rows(Index).Cells(6).Text
                TxtEstadoActual.Text = Flexd.Rows(Index).Cells(11).Text
                Call LlenaComboItem("TBOPC542", DdlEstado)
                DdlEstado.SelectedValue = Flexd.Rows(Index).Cells(12).Text
                txtCodGuiaT.Text = Flexd.Rows(Index).Cells(1).Text
                txtCodGuia.Text = Flexd.Rows(Index).Cells(5).Text

                vImgPrev = Nothing

                Dim Lst = New List(Of PersonaBE)
                Dim vCarpeta As String = System.Web.HttpContext.Current.Server.MapPath("uploads/guias")
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

                'If Not Request.QueryString("p1") Is Nothing Then
                '    hndQR.Value = Request.QueryString("p1").ToString().Trim()
                'Else
                '    hndQR.Value = ""
                'End If
                hndQR.Value = TxtNroGuia.Text

            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
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
            ScriptManager.RegisterStartupScript(Me.UpdatePanel2, Me.UpdatePanel2.GetType(), guidKey.ToString(), strScript, True)
        End Try
    End Sub
    Protected Sub repFotos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repFotos.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Fila = CType(e.Item.DataItem, PersonaBE)
            If Not Fila Is Nothing Then
                Dim imgFotos = CType(e.Item.FindControl("imgFotos"), UI.HtmlControls.HtmlImage)
                Dim objDescrip = CType(e.Item.FindControl("objDescrip"), UI.HtmlControls.HtmlGenericControl)
                imgFotos.Src = "Foto_.ashx?cod=" & Fila.PERSON_C_CODIGO
                objDescrip.InnerHtml = Fila.PERSON_C_CODIGO
            End If
        End If
    End Sub


    Protected Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        divEstado.Visible = False
    End Sub
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If TxtEstadoActual.Text = DdlEstado.Text Then lblError.Text = "Debe de seleccionar un estado diferente al actual." : Exit Sub
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim pdPeso As String = "NULL"
        Dim psFechaCampo As String = ""
        Dim psFecha As String = ""
        psFecha = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
        If IsNumeric(txtPeso.Text) Then pdPeso = txtPeso.Text
        If DdlEstado.SelectedValue = "1" Then psFechaCampo = ", GUIREMTD_FECHA_RECEPCION = '" & psFecha & "' "
        If DdlEstado.SelectedValue = "3" Then psFechaCampo = ", GUIREMTD_FECHA_ENTREGADO = '" & psFecha & "' "
        If DdlEstado.SelectedValue = "2" Then psFechaCampo = ", GUIREMTD_FECHA_ENVIO = '" & psFecha & "' "
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " UPDATE TBINV_GUIA_REMISON_TRANSPORTE_DETALLE " _
                              & " SET GUIREMTD_ESTADO = '" & DdlEstado.SelectedValue & "', " _
                              & " GUIREMTD_PESO = " & pdPeso & " " & psFechaCampo & "  " _
                              & " WHERE GUIREMT_CODIGO = " & Nz(txtCodGuiaT.Text) & " AND GUIREM_CODIGO = " & Nz(txtCodGuia.Text)
        CmdGlobal.ExecuteNonQuery()
        Cn.Close()
        divEstado.Visible = False
        btnListar_Click(sender, e)
    End Sub
End Class


