Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Math
Imports OfficeOpenXml
Imports System.IO
Partial Class Inventario_Inventario_Estadistica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim obj As New Cls_Inventario_Verificacion
            Dim dt As New DataTable
            Try
                dt = obj.Llenar_Combo_Inventario(Session("Ruta_Emp"))
                DdlInventario.DataSource = dt
                DdlInventario.DataValueField = "INVENT_CODIGO"
                DdlInventario.DataTextField = "INVENT_DESC"
                DdlInventario.DataBind()

                DdlInventario.Items.Add("< Seleccionar >") : DdlInventario.SelectedValue = "< Seleccionar >"


            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
            Finally
            End Try
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        dt = Nothing
        gvUbicaciones.DataSource = dt
        gvUbicaciones.DataBind()
        Dim pdCodInventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInventario = Nz(DdlInventario.SelectedValue)
        End If
        lblRegistro.Text = "No hay registros."
        Dim i As Long = 0
        Dim pdTotal As Double = 0
        Dim pdCantTotal As Double = 0
        Dim pdCant As Double = 0

        If pdCodInventario = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Inventario');", True)
        Else
            dt = obj.Lista_Inventario_Ubicacion_Estadistica(Session("Ruta_Emp"), pdCodInventario)
            If dt.Rows.Count > 0 Then
                gvUbicaciones.DataSource = dt
                gvUbicaciones.DataBind()
                If dt.Rows.Count > 1 Then lblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
                If dt.Rows.Count = 1 Then lblRegistro.Text = "Hay 1 registro."
            End If
            For i = 0 To gvUbicaciones.Rows.Count - 1
                pdCantTotal = Nz(gvUbicaciones.Rows(i).Cells(3).Text)
                pdCant = Nz(gvUbicaciones.Rows(i).Cells(5).Text)
                If pdCantTotal > 0 Then
                    pdTotal = (pdCant * 100) / pdCantTotal
                    gvUbicaciones.Rows(i).Cells(8).Text = Round(pdTotal, 2)
                    gvUbicaciones.Rows(i).Cells(8).Text = gvUbicaciones.Rows(i).Cells(8).Text & " %"
                    pdCant = gvUbicaciones.Rows(i).Cells(7).Text
                    pdTotal = (pdCant * 100) / pdCantTotal
                    gvUbicaciones.Rows(i).Cells(10).Text = Round(pdTotal, 2)
                    gvUbicaciones.Rows(i).Cells(10).Text = gvUbicaciones.Rows(i).Cells(10).Text & " %"
                End If
                pdCantTotal = Nz(gvUbicaciones.Rows(i).Cells(4).Text)
                If pdCantTotal > 0 Then
                    pdCant = gvUbicaciones.Rows(i).Cells(6).Text
                    pdTotal = (pdCant * 100) / pdCantTotal
                    gvUbicaciones.Rows(i).Cells(9).Text = Round(pdTotal, 2)
                    gvUbicaciones.Rows(i).Cells(9).Text = gvUbicaciones.Rows(i).Cells(9).Text & " %"
                End If
            Next
        End If

    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click

        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvUbicaciones.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvUbicaciones)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Inv_Estadistica.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()

    End Sub

    Private Sub Exportar_BienesInvOk()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If

        Dim psFechaMov As String = ""
        Try

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.Inventariados_Ok(Session("Ruta_Emp"), psCodInv, "", "")

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Inventariado_Ok")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Inventariado_Ok.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
    Private Sub Exportar_BienesNuevos()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If

        Dim psFechaMov As String = ""
        Try

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.Inventariados_Nuevos(Session("Ruta_Emp"), psCodInv)

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Inventariado_Nuevos")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Inventariado_Nuevos.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
    Private Sub BtnExportarInvOk_Click(sender As Object, e As EventArgs) Handles BtnExportarInvOk.Click
        Exportar_BienesInvOk()
    End Sub

    Private Sub BtnExportarNuevos_Click(sender As Object, e As EventArgs) Handles BtnExportarNuevos.Click
        Exportar_BienesNuevos()
    End Sub
End Class
