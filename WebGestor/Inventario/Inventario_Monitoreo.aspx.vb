Imports System.Data.SqlClient
Imports System.Data
Imports OfficeOpenXml
Imports System.Math
Imports WebGestor
Partial Class Inventario_Inventario_Monitoreo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim ValorLatitud As String = objUbicacion.ObtenerValorLatitud
        'Dim ValorLongitud As String = objUbicacion.ObtenerValorLongitud

        If Not Page.IsPostBack Then
            Llenar_Combos()
            lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss")
            DdlInventario.Items.Add("< Seleccionar >")
            DdlInventario.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC242", DdlEstado)
            DdlEstado.SelectedValue = "2"
        End If
    End Sub
    Protected Sub Llenar_Combos()
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Try
            dt = obj.Llenar_Combo_Inventario(Session("Ruta_Emp"))
            DdlInventario.DataSource = dt
            DdlInventario.DataValueField = "INVENT_CODIGO"
            DdlInventario.DataTextField = "INVENT_DESC"
            DdlInventario.DataBind()


            LstInventario.DataSource = dt
            LstInventario.DataValueField = "INVENT_CODIGO"
            LstInventario.DataTextField = "INVENT_DESC"
            LstInventario.DataBind()
            Call Estilo_ListBox()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub '
    Private Sub Estilo_ListBox()

        For Each item As ListItem In LstInventario.Items
            item.Attributes.CssStyle.Add("display", "flex")
            'item.Attributes.CssStyle.Add("align-items", "center")
            item.Attributes.CssStyle.Add("padding", "3px")
            item.Attributes.CssStyle.Add("margin", "0")
            item.Attributes.CssStyle.Add("cursor", "pointer")
        Next
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try

            Dim obj As New Cls_Inventario
            Dim objUbic As New Cls_Inventario_Ubicacion
            Dim objSeg As New ModuloSeguridad
            Dim pdCodInv As Double = 0
            Dim pdCodUbicInv As Double = 0
            Dim dtDatos As New DataTable
            Dim dt As New DataTable
            Dim dtSeg As New DataTable
            Dim i As Long = 0
            Dim pdCodInvUbicacion As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            lblRegistro2.Text = ""
            Dim psPersonal As String = ""
            If TxtUbicaCodigoInv.Text <> "" Then
                pdCodUbicInv = Nz(TxtUbicaCodigoInv.Text)
            End If
            Dim psFecha As String = ""
            Dim psFechaFin As String = ""
            If TxtFecha.Text <> "" Then
                psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
            End If
            If TxtFechaFin.Text = "" Then
                psFechaFin = psFecha
            Else
                psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
            End If
            Dim psEstado As String = ""
            If DdlEstado.SelectedValue <> "< Seleccionar >" Then
                psEstado = DdlEstado.SelectedValue
            End If
            Dim psCodInventario As String = ""

            Dim selectedItems As New List(Of String)
            For Each item As ListItem In LstInventario.Items
                If item.Selected = True Then
                    If psCodInventario <> "" Then psCodInventario = psCodInventario & ","
                    psCodInventario = psCodInventario & item.Value
                End If
            Next
            Dim pdSumaPlacado As Double = 0
            dt = obj.Lista_Inventario_Monitoreo_xOficina3(Session("Ruta_Emp"), pdCodUbicInv, psFecha, psFechaFin, psEstado, psCodInventario)
            GvResumenCostos.Visible = False
            gvListaxUsuario.Visible = False
            Dim pdAvance As Double = 0
            Dim psPerfil400 As Boolean = False
            dtSeg = objSeg.Usuarios_Perfil400(Session("User"))
            If dtSeg.Rows.Count > 0 Then psPerfil400 = True

            If psPerfil400 = False Then
                GvResumenCostos.Visible = True
                GvResumenCostos.DataSource = dt
                GvResumenCostos.DataBind()
                If GvResumenCostos.Rows.Count > 0 Then
                    For i = 0 To GvResumenCostos.Rows.Count - 1
                        pdCodInvUbicacion = GvResumenCostos.Rows(i).Cells(19).Text
                        psPersonal = ""
                        dtDatos = objUbic.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), pdCodInvUbicacion)
                        If dtDatos.Rows.Count > 0 Then
                            For Each dr As DataRow In dtDatos.Rows
                                If psPersonal <> "" Then psPersonal = psPersonal & "," & "<br/>"
                                psPersonal = psPersonal & Nu(dr("nombre"))
                            Next
                        End If '11+12+13=15
                        GvResumenCostos.Rows(i).Cells(16).Text = psPersonal
                        pdAvance = GvResumenCostos.Rows(i).Cells(6).Text
                        GvResumenCostos.Rows(i).Cells(6).Text = Round(pdAvance, 2) & " %"
                    Next
                End If
            Else
                gvListaxUsuario.Visible = True
                gvListaxUsuario.DataSource = dt
                gvListaxUsuario.DataBind()
                If gvListaxUsuario.Rows.Count > 0 Then
                    For i = 0 To gvListaxUsuario.Rows.Count - 1
                        pdCodInvUbicacion = gvListaxUsuario.Rows(i).Cells(15).Text
                        psPersonal = ""
                        dtDatos = objUbic.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), pdCodInvUbicacion)
                        If dtDatos.Rows.Count > 0 Then
                            For Each dr As DataRow In dtDatos.Rows
                                If psPersonal <> "" Then psPersonal = psPersonal & "," & "<br/>"
                                psPersonal = psPersonal & Nu(dr("nombre"))
                            Next
                        End If
                        gvListaxUsuario.Rows(i).Cells(12).Text = psPersonal
                        pdAvance = gvListaxUsuario.Rows(i).Cells(6).Text
                        gvListaxUsuario.Rows(i).Cells(6).Text = Round(pdAvance, 2) & " %"
                    Next
                End If
            End If


            If dt.Rows.Count > 1 Then
                lblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro2.Text = "No hay registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro2.Text = "Hay 1 registro."

            End If

            dt = Nothing

            dt = obj.Lista_Inventario_Monitoreo_Resumen(Session("Ruta_Emp"), pdCodUbicInv, psFecha, psFechaFin, psEstado, psCodInventario)

            gvResumen.DataSource = dt
            gvResumen.DataBind()

            If gvResumen.Rows.Count > 0 Then
                For i = 0 To gvResumen.Rows.Count - 1
                    pdAvance = gvResumen.Rows(i).Cells(1).Text
                    gvResumen.Rows(i).Cells(1).Text = Round(pdAvance, 2) & " %"
                    pdAvance = gvResumen.Rows(i).Cells(3).Text
                    gvResumen.Rows(i).Cells(3).Text = Round(pdAvance, 2) & " %"
                    pdAvance = gvResumen.Rows(i).Cells(5).Text
                    gvResumen.Rows(i).Cells(5).Text = Round(pdAvance, 2) & " %"
                    pdAvance = gvResumen.Rows(i).Cells(7).Text
                    gvResumen.Rows(i).Cells(7).Text = Round(pdAvance, 2) & " %"
                Next
            End If

            Estilo_ListBox()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        BtnListar_Click(sender, e)
        Call Estilo_ListBox()
    End Sub

    Private Sub GvResumenCostos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvResumenCostos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pdCodUbica As Double = 0
        Dim pdCodUbicaInv As Double = 0
        Dim dt As New DataTable
        If e.CommandName = "Detalle" Then
            Dim objUbic As New Cls_Inventario_Ubicacion
            pdCodUbicaInv = Nz(GvResumenCostos.Rows(Index).Cells(16).Text)
            dt = objUbic.Inventario_Ubicacion_xCodigo(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodUbicaInv)
            For Each dr As DataRow In dt.Rows
                pdCodUbica = Nz(dr("INVENTUBIC_UBIC_CODIGO"))
            Next
            If pdCodUbica > 0 Then
                Call NuevoRegistroSinAcceso(pdCodUbica)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalSinAcceso').modal('show');", True)
        End If
    End Sub

    Private Sub NuevoRegistroSinAcceso(ByVal pCodUbicaInv As Double)
        Dim obj As New Cls_Inventario
        Dim pdNroObs As Double = 0
        Dim dt As New DataTable
        Dim pdCodUbica As Double = 0
        pdCodUbica = pCodUbicaInv
        dt = Nothing
        GvSinAcceso.DataSource = dt
        GvSinAcceso.DataBind()
        dt = obj.Lista_Ubicaciones_SinAcceso(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodUbica)
        If dt.Rows.Count > 0 Then
            GvSinAcceso.DataSource = dt
            GvSinAcceso.DataBind()
        End If
        dt = Nothing
    End Sub

    Private Sub BtnSinAcceso_Cerrar_Click(sender As Object, e As EventArgs) Handles BtnSinAcceso_Cerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalSinAcceso').modal('hide');", True)
    End Sub

    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        Call Estilo_ListBox()
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim objU As New Cls_Inventario_Ubicacion
        Dim objMa As New Cls_Marcas
        Dim objMo As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim inventario As Double = 0
        Dim codigo As Double = 0
        Dim CodInterno As String = ""
        Dim descripcion As String = ""
        Dim codMarca As String = ""

        Try

            CodInterno = BuscarCodigo.Value.ToString
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                inventario = DdlInventario.SelectedValue.ToString
            End If
            descripcion = BuscarDescripcion.Value.ToString
            If TituloPopup.Text = "Búsqueda Almacén" Then
                codigo = Nz(BuscarCodigo.Value.ToString)
                dt = obj.Listar_Almacenes_Inventario_Verificacion(Session("Ruta_Emp"), inventario, codigo, descripcion)
            ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
                dt = obj.Listar_CentroC_Inventario_Verificacion(Session("Ruta_Emp"), inventario, CodInterno, descripcion)
            End If

            GvBusqueda.DataSource = dt
            GvBusqueda.DataBind()
            Call Estilo_ListBox()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try

    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

        Limpiar_Cajas_Popup()
        Call Estilo_ListBox()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtUbicaCodigo.Text = GvBusqueda.Rows(Index).Cells(3).Text
            TxtUbicaCodigoInv.Text = GvBusqueda.Rows(Index).Cells(4).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()
        Call Estilo_ListBox()
    End Sub

    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub
    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        TxtUbicaCodigo.Text = ""
        TxtUbicaCodigoInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        lblRegistro2.Text = ""
        BtnBusca.Enabled = True
        Dim dt As New DataTable
        dt = Nothing
        GvResumenCostos.DataSource = dt
        GvResumenCostos.DataBind()
        Call Estilo_ListBox()
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        TxtUbicaCodigo.Text = ""
        TxtUbicaCodigoInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        BtnBusca.Enabled = True
        Dim dt As New DataTable
        dt = Nothing
        lblRegistro2.Text = ""
        GvResumenCostos.DataSource = dt
        GvResumenCostos.DataBind()
        Call Estilo_ListBox()
    End Sub

    Private Sub RBTodos_CheckedChanged(sender As Object, e As EventArgs) Handles RBTodos.CheckedChanged
        TxtUbicaCodigo.Text = ""
        TxtUbicaCodigoInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        BtnBusca.Enabled = False
        Dim dt As New DataTable
        dt = Nothing
        lblRegistro2.Text = ""
        GvResumenCostos.DataSource = dt
        GvResumenCostos.DataBind()
        Call Estilo_ListBox()
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        '

        Dim obj As New Cls_Inventario
        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim objSeg As New ModuloSeguridad
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dtDatos As New DataTable
        Dim dt As New DataTable
        Dim dtSeg As New DataTable
        Dim i As Long = 0
        Dim pdCodInvUbicacion As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInv = DdlInventario.SelectedValue
        End If
        lblRegistro2.Text = ""
        Dim psPersonal As String = ""
        If TxtUbicaCodigoInv.Text <> "" Then
            pdCodUbicInv = Nz(TxtUbicaCodigoInv.Text)
        End If
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        If TxtFecha.Text <> "" Then
            psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        End If
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If
        Dim psEstado As String = ""
        If DdlEstado.SelectedValue <> "< Seleccionar >" Then
            psEstado = DdlEstado.SelectedValue
        End If
        Dim psCodInventario As String = ""

        Dim selectedItems As New List(Of String)
        For Each item As ListItem In LstInventario.Items
            If item.Selected = True Then
                If psCodInventario <> "" Then psCodInventario = psCodInventario & ","
                psCodInventario = psCodInventario & item.Value
            End If
        Next
        Dim pdSumaPlacado As Double = 0
        dt = obj.Lista_Inventario_Monitoreo_xOficina_Exportar(Session("Ruta_Emp"), pdCodUbicInv, psFecha, psFechaFin, psEstado, psCodInventario)


        Using excelPackage As New ExcelPackage()
            ' Agregar hojas al archivo de Excel
            Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Bienes Inventariados")

            ' Llenar Hoja1 con los datos de dt1
            worksheet1.Cells("A1").LoadFromDataTable(dt, True)

            ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=Lista_Bienes_Inventariados.xlsx")
            Response.BinaryWrite(excelPackage.GetAsByteArray())
            Response.End()
        End Using

    End Sub

End Class
