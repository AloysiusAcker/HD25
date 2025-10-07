Imports System
Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports OfficeOpenXml
Partial Class Inventario_Invnetario_Relacion_EquiposConciliados
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combos()
            DdlInventario.SelectedValue = "< Seleccionar >"
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
            DdlInventario.Items.Add("< Seleccionar >")
            DdlInventario.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodUbicaInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvLista.DataSource = dt
        GvLista.DataBind()
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodUbicaInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvLista.DataSource = dt
        GvLista.DataBind()
    End Sub
    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
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
        If TituloPopup.Text = "Busca Sección de Centro de Costo" Or TituloPopup.Text = "Busca Almacén" Or TituloPopup.Text = "Busca Ubicaciones" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        Else
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

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            TxtCodUbica.Text = GvBusqueda.Rows(Index).Cells(3).Text
            TxtCodUbicaInv.Text = GvBusqueda.Rows(Index).Cells(4).Text
            Session("CodSeccion") = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            Dim obj As New Cls_Inventario
            Dim dt As New DataTable
            Dim pdCodInvUbica As Double = 0
            Dim pdCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            If TxtCodUbicaInv.Text <> "" Then
                pdCodInvUbica = TxtCodUbicaInv.Text
            End If
            dt = obj.Invenatrio_Conciliar_Listas(Session("Ruta_Emp"), pdCodInvUbica, 0, pdCodInv)
            GvLista.DataSource = dt
            GvLista.DataBind()
            If dt.Rows.Count > 1 Then
                lblRegistro3.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro3.Text = "Hay 1 registro."
            Else
                lblRegistro3.Text = "No hay registros."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "';", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "';", True)
        End Try
    End Sub
    Private Sub BtnExportarConciliados_Click(sender As Object, e As EventArgs) Handles BtnExportarConciliados.Click


        Try
            Dim obj As New Cls_Inventario
            Dim dt As New DataTable
            Dim pdCodInvUbica As Double = 0
            Dim pdCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            If TxtCodUbicaInv.Text <> "" Then
                pdCodInvUbica = TxtCodUbicaInv.Text
            End If
            dt = obj.Invenatrio_Conciliar_Listas_Exportar(Session("Ruta_Emp"), pdCodInvUbica, 0, pdCodInv)



            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("BienesConciliados")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=BienesConciliados.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()


            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
End Class
