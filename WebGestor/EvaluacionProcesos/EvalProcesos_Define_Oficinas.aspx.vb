Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports WebGestor
Imports System.Net
Imports System.Text
Partial Class EvaluacionProcesos_EvalProcesos_Define_Oficinas
    Inherits System.Web.UI.Page
    Dim ObjSeg As New ModuloSeguridad
    Dim objProceso As New ClsEval_Proceso

    Protected Sub FindCoordinates(sender As Object, e As EventArgs)

        'Dim url As String = "http://maps.google.com/maps/api/geocode/xml?address=" + txtLocation.Text + "&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"
        Dim url As String = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + TxtDireccion.Text + "&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"

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
                    TxtLatitud.Text = location("lat")
                    TxtLongitud.Text = location("lng")
                Next
                'GridView1.DataSource = dtCoordinates
                'GridView1.DataBind()
            End Using
        End Using
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call LlenaComboItem("TBOPC549", DdlTipo)
            Call LlenaCheckBoxItem("TBOPC550", ChkCanales)
            Call LlenaComboItem("TBOPC002", DdlDpto)
            BtnListar_Click(sender, e)
        End If
    End Sub

    Private Sub DdlDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDpto.SelectedIndexChanged
        DdlProvincia.Items.Clear()
        DdlDistrito.Items.Clear()
        DdlProvincia.Enabled = False
        DdlDistrito.Items.Add("< Seleccionar >") : DdlDistrito.SelectedValue = "< Seleccionar >"
        DdlDistrito.Enabled = False
        If DdlDpto.SelectedIndex = -1 Or DdlDpto.Items.Count = 0 Then Exit Sub
        If DdlDpto.Items(DdlDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlProvincia, Left(DdlDpto.SelectedValue, 2), "PR")
        DdlProvincia.Items.Add("< Seleccionar >") : DdlProvincia.SelectedValue = "< Seleccionar >"
        If DdlDpto.SelectedValue <> "< Seleccionar >" Then DdlProvincia.Enabled = True
    End Sub

    Private Sub DdlProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProvincia.SelectedIndexChanged
        DdlDistrito.Items.Clear()
        DdlDistrito.Enabled = False
        DdlDistrito.Items.Add("< Seleccionar >") : DdlDistrito.SelectedValue = "< Seleccionar >"
        If DdlProvincia.SelectedIndex = -1 Or DdlProvincia.Items.Count = 0 Then Exit Sub
        If DdlProvincia.Items(DdlProvincia.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlDistrito, Left(DdlDpto.SelectedValue, 2) + Mid(DdlProvincia.SelectedValue, 3, 2), "DS")
        DdlDistrito.Items.Add("< Seleccionar >") : DdlDistrito.SelectedValue = "< Seleccionar >"
        If DdlProvincia.SelectedValue <> "< Seleccionar >" Then DdlDistrito.Enabled = True
    End Sub
    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim dt As New DataTable
        LblError.Text = ""
        Try
            dt = ObjSeg.Listar_Oficina(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            Flex.DataSource = dt
            Flex.DataBind()
            DivFlex.Visible = True
            LblRegistro.Text = "Se encontraron " & dt.Rows.Count & " oficinas."
        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        Finally
        End Try
    End Sub
    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        DivDatosOficina.Visible = True
        BtnListar.Enabled = False
        BtnNuevo.Enabled = False
        BtnGuardar.Visible = True
        BtnCancelar.Visible = True
        btnSearch.Visible = True
        TxtCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtDireccion.Text = ""
        TxtLatitud.Text = ""
        TxtLongitud.Text = ""
        LblCodigo.Text = ""
        LblError.Text = ""
        DdlTipo.SelectedValue = "< Seleccionar >"
        DdlDpto.SelectedValue = "150000"
        DdlDpto_SelectedIndexChanged(sender, e)
        For i = 0 To ChkCanales.Items.Count - 1
            ChkCanales.Items(i).Selected = False
        Next
        DivFlex.Visible = False
        Dim dt As New DataTable
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        DivDatosOficina.Visible = False
        BtnListar.Enabled = True
        BtnNuevo.Enabled = True
        BtnGuardar.Visible = False
        BtnCancelar.Visible = False
        btnSearch.Visible = False
        BtnListar_Click(sender, e)
        Call Limpiar_Campos(sender, e)
    End Sub

    Private Sub Limpiar_Campos(sender As Object, e As EventArgs)
        TxtCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtDireccion.Text = ""
        TxtLatitud.Text = ""
        TxtLongitud.Text = ""
        LblCodigo.Text = ""
        LblError.Text = ""
        DdlTipo.SelectedValue = "< Seleccionar >"
        DdlDpto.SelectedValue = "150000"
        DdlDpto_SelectedIndexChanged(sender, e)
        For i = 0 To ChkCanales.Items.Count - 1
            ChkCanales.Items(i).Selected = False
        Next
    End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        'BuscarDatos_xOficina
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        dt = Nothing
        LblError.Text = ""
        Try
            If e.CommandName = "Editar" Then
                dt = ObjSeg.BuscarDatos_xOficina(Session("CodEmpresa"), Session("CodGrupoEmpresa"), Flex.Rows(Index).Cells(2).Text)
                If dt.Rows.Count > 0 Then
                    DivDatosOficina.Visible = True
                    BtnGuardar.Visible = True
                    BtnCancelar.Visible = True
                    BtnListar.Enabled = False
                    BtnNuevo.Enabled = False
                    btnSearch.Visible = True
                    Call Limpiar_Campos(sender, e)

                    For Each drow As DataRow In dt.Rows
                        LblCodigo.Text = Nu(drow("CODIGO"))
                        TxtCodigo.Text = Nu(drow("CODIGO_INTERNO"))
                        TxtDescripcion.Text = Nu(drow("NOMBRES"))
                        TxtDireccion.Text = Nu(drow("DIRECCION"))
                        TxtLatitud.Text = Nu(drow("LATITUD"))
                        TxtLongitud.Text = Nu(drow("LONGITUD"))
                        If Nu(drow("OFICINA_TIPO")) <> "" Then DdlTipo.SelectedValue = Nu(drow("OFICINA_TIPO"))
                        If Nu(drow("OFICINA_DEPTO")) <> "" Then
                            DdlDpto.SelectedValue = Nu(drow("OFICINA_DEPTO"))
                            DdlDpto_SelectedIndexChanged(sender, e)
                        End If
                        If Nu(drow("OFICINA_PROV")) <> "" Then
                            DdlProvincia.SelectedValue = Nu(drow("OFICINA_PROV"))
                            DdlProvincia_SelectedIndexChanged(sender, e)
                        End If
                        If Nu(drow("OFICINA_DIST")) <> "" Then DdlDistrito.SelectedValue = Nu(drow("OFICINA_DIST"))
                    Next
                End If
                dt = Nothing

                For i = 0 To ChkCanales.Items.Count - 1
                    dt = ObjSeg.Existe_CanalxOficina(LblCodigo.Text, ChkCanales.Items(i).Value)
                    If dt.Rows.Count = 1 Then
                        ChkCanales.Items(i).Selected = True
                    End If
                Next

            End If
        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        Finally
        End Try
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If TxtCodigo.Text = "" Then LblError.Text = "Ingresar el codigo interno de la oficina." : Exit Sub
        If TxtDescripcion.Text = "" Then LblError.Text = "Ingresar el nombre de la oficina." : Exit Sub
        If DdlTipo.SelectedValue = "< Seleccionar >" Then LblError.Text = "Seleccionar el tipo de oficina." : Exit Sub
        If TxtDireccion.Text = "" Then LblError.Text = "Ingresar la direccion de la oficina." : Exit Sub
        If DdlDpto.SelectedValue = "< Seleccionar >" Then LblError.Text = "Seleccionar el departamento de la oficina." : Exit Sub
        If DdlProvincia.SelectedValue = "< Seleccionar >" Then LblError.Text = "Seleccionar la provincia de la oficina." : Exit Sub
        If DdlDistrito.SelectedValue = "< Seleccionar >" Then LblError.Text = "Seleccionar el distrito de la oficina." : Exit Sub
        Dim pCodOficina As Double = 0
        Dim dt As New DataTable
        Dim psCodOficina As Double = 0
        Dim psCanal As String = ""
        Dim i As Integer = 0
        Dim CountCanal As Integer = 0
        For i = 0 To ChkCanales.Items.Count - 1
            If ChkCanales.Items(i).Selected = True Then CountCanal = CountCanal + 1
        Next
        If CountCanal = 0 Then LblError.Text = "Seleccionar al menos un canal." : Exit Sub
        Dim OficinaExiste As String = "NO"
        Try
            If LblCodigo.Text <> "" Then pCodOficina = Nz(LblCodigo.Text)
            If pCodOficina = 0 Then
                dt = ObjSeg.Existe_Oficina(Session("CodGrupoEmpresa"), Session("CodEmpresa"), TxtCodigo.Text, TxtDescripcion.Text)
                If dt.Rows.Count > 0 Then
                    OficinaExiste = "SI"
                End If
                dt = Nothing
                If OficinaExiste = "SI" Then LblError.Text = "Existe una oficina con el mismo nombre." : Exit Sub
            End If
            ObjSeg.InsUpd_Oficina(Session("CodEmpresa"), Session("CodGrupoEmpresa"), pCodOficina, TxtCodigo.Text, TxtDescripcion.Text,
                                  TxtDireccion.Text, DdlDpto.SelectedValue, DdlProvincia.SelectedValue, DdlDistrito.SelectedValue,
                                  TxtLatitud.Text, TxtLongitud.Text, DdlTipo.SelectedValue, "")

            If pCodOficina = 0 Then
                dt = ObjSeg.Existe_Oficina(Session("CodGrupoEmpresa"), Session("CodEmpresa"), TxtCodigo.Text, TxtDescripcion.Text)
                If dt.Rows.Count > 0 Then
                    For Each drow As DataRow In dt.Rows
                        pCodOficina = Nu(drow("OFICINA_CODIGO"))
                    Next
                End If
            End If

            ObjSeg.Delete_CanalxOficina(pCodOficina)
            For i = 0 To ChkCanales.Items.Count - 1
                If ChkCanales.Items(i).Selected = True Then
                    dt = ObjSeg.Existe_CanalxOficina(pCodOficina, ChkCanales.Items(i).Value)
                    If dt.Rows.Count = 0 Then
                        ObjSeg.Insert_OficinaCanal(pCodOficina, ChkCanales.Items(i).Value)
                    End If
                End If
            Next
            BtnCancelar_Click(sender, e)
            LblMensaje.Text = "Datos guardados."

        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        Finally
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        'OnClick="FindCoordinates" 
        'Dim url As String = "http://maps.google.com/maps/api/geocode/xml?address=" + txtLocation.Text + "&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"
        Dim url As String = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + TxtDireccion.Text + "&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"

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
                    TxtLatitud.Text = location("lat")
                    TxtLongitud.Text = location("lng")
                Next
                'GridView1.DataSource = dtCoordinates
                'GridView1.DataBind()
            End Using
        End Using
    End Sub
End Class
