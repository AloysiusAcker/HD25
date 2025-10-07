Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports WebGestor
Imports OfficeOpenXml
Partial Class Inventario_Inventario_Lista_BienesInventariado
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Llenar_Combos()
            Call Llena_Ubicacion(ddlUbicacion)
            ddlUbicacion.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Private Sub Llena_Ubicacion(ByVal combo As DropDownList)
        'Lista_Ubicaciones
        Dim obj As New clsInv_Listados
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.Lista_Ubicaciones(Session("Ruta_Emp"), Session("CodEmpresa"))
        combo.DataTextField = "Ubicacion"
        combo.DataValueField = "UBICACION_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
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
            DdlInventario.Items.Add("< Seleccionar >")
            DdlInventario.SelectedValue = "< Seleccionar >"

            'dt = obj.Llenar_Combo_Estado(Session("Ruta_Emp"))
            'DdlEstadoM.DataSource = dt
            'DdlEstadoM.DataValueField = "ELEMEN_CODIGO"
            'DdlEstadoM.DataTextField = "ELEMEN_VALOR"
            'DdlEstadoM.DataBind()
            'DdlEstadoM.Items.Add("< Seleccionar >")
            'DdlEstadoM.SelectedValue = "< Seleccionar >"


            'dt = objC.Lista_Tipo(Session("Ruta_Emp"))
            'DdlTipoBA.DataSource = dt
            'DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
            'DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
            'DdlTipoBA.DataBind()
            'DdlTipoBA.Items.Add("< Seleccionar >")
            'DdlTipoBA.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
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
        Dim inventario As String = ""
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
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            txtCodCecose.Text = GvBusqueda.Rows(Index).Cells(3).Text
            txtCodInvUbicacion.Text = GvBusqueda.Rows(Index).Cells(4).Text
            Session("CodSeccion") = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()

    End Sub

    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dt As New DataTable
        LblContador.Text = ""
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Try

            Listar_Inventario_Verificacion()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub
    Private Sub Listar_Inventario_Verificacion()
        LblContador.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing
        Dim pd_NroInventario As Double = 0

        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pd_NroInventario = DdlInventario.SelectedValue
        End If

        Dim pdCodInvUbica As Double = 0
        Dim pdUbicaCodigo As Double = 0
        pdCodInvUbica = Nz(txtCodInvUbicacion.Text.ToString)
        Dim dtO As New DataTable
        dtO = Nothing
        Dim codigo As String = txtCodInvUbicacion.Text.ToString

        Dim tipo As String = ""
        Dim ubicacion As String = txtCodCecose.Text.ToString
        pdUbicaCodigo = Nz(txtCodCecose.Text.ToString)
        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        End If
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0

        Dim pdPlacaNro As Double = 0
        If Nz(TxtNroPlaca.Text) > 0 Then
            pdPlacaNro = Nz(TxtNroPlaca.Text)
        End If

        Try
            GvListarTodo.DataSource = Nothing
            GvListarTodo.DataBind()
            dt = obj.ListaBienes_Inventariados(psconexion, pdCodInvUbica, tipo, pdUbicaCodigo, pdPlacaNro, pd_NroInventario)
            gvListaTop5.DataSource = dt
            gvListaTop5.DataBind()
            If dt.Rows.Count > 1 Then
                LblContador.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblContador.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                LblContador.Text = "Hay 0 registro."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        txtCodCecose.Text = ""
        txtCodInvUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""

        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListarTodo.DataSource = dt
        GvListarTodo.DataBind()
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        txtCodCecose.Text = ""
        txtCodInvUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""

        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListarTodo.DataSource = dt
        GvListarTodo.DataBind()
    End Sub

    Private Sub RBTodos_CheckedChanged(sender As Object, e As EventArgs) Handles RBTodos.CheckedChanged
        txtCodCecose.Text = ""
        txtCodInvUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""

        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListarTodo.DataSource = dt
        GvListarTodo.DataBind()
    End Sub

    Private Sub gvListaTop5_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvListaTop5.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pdPlaca As Double = 0
        Dim pdInvUbica As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim pdSerieNumerar As Double = 0
        Cn.Open() : CmdGlobal.Connection = Cn
        If e.CommandName = "Desactivar" Then
            pdSerieNumerar = gvListaTop5.Rows(Index).Cells(12).Text
            pdInvUbica = gvListaTop5.Rows(Index).Cells(11).Text
            pdPlaca = gvListaTop5.Rows(Index).Cells(6).Text
            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_ACTIVO = '1' WHERE INVDET_SERIE_NUMERAR = " & pdSerieNumerar & " AND INVDET_INVENTUBIC_CODIGO =  " & pdInvUbica
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_ESTADO_ACTIVO = '1' WHERE VERIF_SERIE_NUMERAR = " & pdSerieNumerar & " AND INVENTUBIC_CODIGO =  " & pdInvUbica
            CmdGlobal.ExecuteNonQuery()
        End If
        BtnListar_Click(sender, e)
    End Sub

    Private Sub BtnListarTodos_Click(sender As Object, e As EventArgs) Handles BtnListarTodos.Click
        LblContador.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing
        Dim pd_NroInventario As Double = 0

        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pd_NroInventario = DdlInventario.SelectedValue
        End If

        Dim pdCodInvUbica As Double = 0
        Dim pdUbicaCodigo As Double = 0
        pdCodInvUbica = Nz(txtCodInvUbicacion.Text.ToString)
        Dim dtO As New DataTable
        dtO = Nothing
        Dim codigo As String = txtCodInvUbicacion.Text.ToString

        Dim tipo As String = ""
        Dim ubicacion As String = txtCodCecose.Text.ToString
        pdUbicaCodigo = Nz(txtCodCecose.Text.ToString)
        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        End If
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0

        Dim pdPlacaNro As Double = 0
        If Nz(TxtNroPlaca.Text) > 0 Then
            pdPlacaNro = Nz(TxtNroPlaca.Text)
        End If
        Dim psFechaIni As String = ""
        Dim psfechafin As String = ""
        If TxtFechaIni.Text <> "" Then
            psFechaIni = Right(TxtFechaIni.Text, 4) & Mid(TxtFechaIni.Text, 4, 2) & Left(TxtFechaIni.Text, 2)
        End If
        If TxtFechaFin.Text <> "" Then
            psfechafin = Right(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 4, 2) & Left(TxtFechaFin.Text, 2)
        End If
        Try

            gvListaTop5.DataSource = Nothing
            gvListaTop5.DataBind()

            dt = obj.ListaBienes_TodosInventariados(psconexion, pdCodInvUbica, tipo, pdUbicaCodigo, pdPlacaNro, psFechaIni, psfechafin, pd_NroInventario)
            GvListarTodo.DataSource = dt
            GvListarTodo.DataBind()

            If dt.Rows.Count > 1 Then
                LblContador.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblContador.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                LblContador.Text = "Hay 0 registro."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0

        Dim pd_NroInventario As Double = 0

        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pd_NroInventario = DdlInventario.SelectedValue
        End If

        Dim psFechaMov As String = ""
        Try
            LblContador.Text = ""
            Dim obj As New Cls_Inventario_Verificacion
            Dim dt As New DataTable
            dt = Nothing

            Dim pdCodInvUbica As Double = 0
            Dim pdUbicaCodigo As Double = 0
            pdCodInvUbica = Nz(txtCodInvUbicacion.Text.ToString)
            Dim dtO As New DataTable
            dtO = Nothing
            Dim codigo As String = txtCodInvUbicacion.Text.ToString

            Dim tipo As String = ""
            Dim ubicacion As String = txtCodCecose.Text.ToString
            pdUbicaCodigo = Nz(txtCodCecose.Text.ToString)
            If RBAlmacen.Checked Then
                tipo = "1"
            ElseIf RBCentroC.Checked Then
                tipo = "2"
            End If
            gvListaTop5.DataSource = dt
            gvListaTop5.DataBind()
            Dim psconexion As String = Session("Ruta_Emp")
            Dim pdCodArt As Double = 0
            Dim pdCodInv As Double = 0
            Dim pdCodUbicInv As Double = 0

            Dim pdPlacaNro As Double = 0
            If Nz(TxtNroPlaca.Text) > 0 Then
                pdPlacaNro = Nz(TxtNroPlaca.Text)
            End If
            Dim psFechaIni As String = ""
            Dim psfechafin As String = ""
            If TxtFechaIni.Text <> "" Then
                psFechaIni = Right(TxtFechaIni.Text, 4) & Mid(TxtFechaIni.Text, 4, 2) & Left(TxtFechaIni.Text, 2)
            End If
            If TxtFechaFin.Text <> "" Then
                psfechafin = Right(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 4, 2) & Left(TxtFechaFin.Text, 2)
            End If

            dt1 = obj.ListaBienes_TodosInventariados(psconexion, pdCodInvUbica, tipo, pdUbicaCodigo, pdPlacaNro, psFechaIni, psfechafin, pd_NroInventario)


            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("BienesInventariados")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)
                Dim numberColumn = worksheet1.Column(17) ' 3 es el índice de la columna C, ajusta según tu necesidad
                numberColumn.Style.Numberformat.Format = "0"
                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=BienesInventariados.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()


            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnListaNoEncontrados_Click(sender As Object, e As EventArgs) Handles BtnListaNoEncontrados.Click
        Dim obj As New Cls_Inventario
        Dim pdCodInventario As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dt As New DataTable
        LblContador.Text = ""
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Try

            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInventario = DdlInventario.SelectedValue
            End If
            If Nz(txtCodInvUbicacion.Text.ToString) <> 0 Then
                pdCodUbicInv = Nz(txtCodInvUbicacion.Text.ToString)
            End If
            GvListarTodo.DataSource = Nothing
            GvListarTodo.DataBind()
            gvListaTop5.DataSource = Nothing
            gvListaTop5.DataBind()
            dt = obj.Inventario_Bienes_NoEncontrados(Session("Ruta_Emp"), pdCodInventario, pdCodUbicInv)
            GvNoEncontrados.DataSource = dt
            GvNoEncontrados.DataBind()
            If dt.Rows.Count > 1 Then
                LblContador.Text = "Hay " & dt.Rows.Count & " bienes no encontrados."
            ElseIf dt.Rows.Count = 1 Then
                LblContador.Text = "Hay 1 bien no encontrado."
            ElseIf dt.Rows.Count = 0 Then
                LblContador.Text = "Hay 0 registro."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub BtnExportaNE_Click(sender As Object, e As EventArgs) Handles BtnExportaNE.Click

        Dim obj As New Cls_Inventario
        Dim pdCodInventario As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dt As New DataTable
        LblContador.Text = ""
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Try

            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInventario = DdlInventario.SelectedValue
            End If
            If Nz(txtCodInvUbicacion.Text.ToString) <> 0 Then
                pdCodUbicInv = Nz(txtCodInvUbicacion.Text.ToString)
            End If
            GvListarTodo.DataSource = Nothing
            GvListarTodo.DataBind()
            gvListaTop5.DataSource = Nothing
            gvListaTop5.DataBind()
            dt = obj.Inventario_Bienes_NoEncontrados_Exportar(Session("Ruta_Emp"), pdCodInventario, pdCodUbicInv)

            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("BienesNoInventariados")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)
                Dim numberColumn = worksheet1.Column(17) ' 3 es el índice de la columna C, ajusta según tu necesidad
                numberColumn.Style.Numberformat.Format = "0"
                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Bienes_No_Inventariados.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub
End Class
