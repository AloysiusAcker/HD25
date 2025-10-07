Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports WebGestor
Imports OfficeOpenXml
Imports System.Math
Partial Class Inventario_Inventario_Resumen_Estadistica
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'BindGridViewPrincipal()
        End If
    End Sub
    Private Sub BindGridViewPrincipal()

        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cmd As New SqlCommand("Prc_Inventario_Resumen_Estadistica", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Resumen_Estadistica")
        Da.Fill(Dt)
        gvEmployeeDetails.DataSource = Dt
        gvEmployeeDetails.DataBind()
        Dim pdTotal As Double = 0
        Dim pdInv As Double = 0
        Dim dtArt As New DataTable
        For i = 0 To gvEmployeeDetails.Rows.Count - 1
            pdTotal = Nz(gvEmployeeDetails.Rows(i).Cells(3).Text)
            pdInv = Nz(gvEmployeeDetails.Rows(i).Cells(4).Text) * 100
            If pdTotal > 0 Then
                gvEmployeeDetails.Rows(i).Cells(6).Text = Round(pdInv / pdTotal, 2)
                gvEmployeeDetails.Rows(i).Cells(6).Text = gvEmployeeDetails.Rows(i).Cells(6).Text & " %"
            Else
                gvEmployeeDetails.Rows(i).Cells(6).Text = gvEmployeeDetails.Rows(i).Cells(6).Text & " %"
            End If
        Next
    End Sub

    Protected Sub gvEmployeeDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim detalleGridView As GridView = TryCast(e.Row.FindControl("gv_Child"), GridView)

            If detalleGridView IsNot Nothing Then
                Dim primaryKey As Integer = Convert.ToInt32(gvEmployeeDetails.DataKeys(e.Row.RowIndex).Values("ID"))
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim Cmd As New SqlCommand("Prc_Lista_Ubicacion_xInventario", Cn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
                Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = primaryKey
                Dim Da As New SqlDataAdapter(Cmd)
                Dim Dt As New DataTable("Prc_Lista_Ubicacion_xInventario")
                Da.Fill(Dt)
                detalleGridView.DataSource = Dt
                detalleGridView.DataBind()
                Dim pdTotal As Double = 0
                Dim pdInv As Double = 0
                Dim dtArt As New DataTable
                For i = 0 To detalleGridView.Rows.Count - 1
                    pdTotal = Nz(detalleGridView.Rows(i).Cells(3).Text)
                    pdInv = Nz(detalleGridView.Rows(i).Cells(4).Text) * 100
                    If pdTotal > 0 Then
                        detalleGridView.Rows(i).Cells(6).Text = Round(pdInv / pdTotal, 2)
                        detalleGridView.Rows(i).Cells(6).Text = detalleGridView.Rows(i).Cells(6).Text & " %"
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        BindGridViewPrincipal()


    End Sub
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim customerId As String = gvEmployeeDetails.DataKeys(e.Row.RowIndex).Value.ToString()
            Dim gvOrders As GridView = TryCast(e.Row.FindControl("gv_Child"), GridView)

            Dim primaryKey As Integer = Convert.ToInt32(gvEmployeeDetails.DataKeys(e.Row.RowIndex).Values("ID"))
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim Cmd As New SqlCommand("Prc_Lista_Ubicacion_xInventario", Cn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
            Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = primaryKey
            Dim Da As New SqlDataAdapter(Cmd)
            Dim Dt As New DataTable("Prc_Lista_Ubicacion_xInventario")
            Da.Fill(Dt)
            gvOrders.DataSource = Dt
            gvOrders.DataBind()
            Dim pdTotal As Double = 0
            Dim pdInv As Double = 0
            Dim dtArt As New DataTable
            For i = 0 To gvOrders.Rows.Count - 1
                pdTotal = Nz(gvOrders.Rows(i).Cells(3).Text)
                pdInv = Nz(gvOrders.Rows(i).Cells(4).Text) * 100
                If pdTotal > 0 Then
                    gvOrders.Rows(i).Cells(6).Text = Round(pdInv / pdTotal, 2)
                    gvOrders.Rows(i).Cells(6).Text = gvOrders.Rows(i).Cells(6).Text & " %"
                Else
                    gvOrders.Rows(i).Cells(6).Text = gvOrders.Rows(i).Cells(6).Text & " %"
                End If
            Next
        End If
    End Sub
    Protected Sub OnRowDataBound2(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim detalleGridView As GridView = TryCast(e.Row.FindControl("gv_Child2"), GridView)
            If detalleGridView IsNot Nothing Then
                Dim primaryKey As Integer = Convert.ToInt32(gvEmployeeDetails.DataKeys(e.Row.RowIndex).Values("DetalleID"))
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim Cmd As New SqlCommand("Prc_Inventario_Resumen_xUbicacion_xIngreso", Cn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
                Cmd.Parameters.Add("@CodUbicacion", SqlDbType.Float).Value = primaryKey
                Dim Da As New SqlDataAdapter(Cmd)
                Dim Dt As New DataTable("Prc_Inventario_Resumen_xUbicacion_xIngreso")
                Da.Fill(Dt)
                detalleGridView.DataSource = Dt
                detalleGridView.DataBind()
            End If
        End If
    End Sub

    Private Sub ListaBienesInventariados()
        Dim dt As New DataTable
        Dim obj As New Cls_Inventario_Verificacion
        Try
            dt = obj.Lista_Inventario_Verificacion(Session("Ruta_Emp"), 0, "", 0)
            GvListaBienes.DataSource = dt
            GvListaBienes.DataBind()
            If dt.Rows.Count > 1 Then lblRegistroInv.Text = "Hay " & dt.Rows.Count & " registros."
            If dt.Rows.Count = 1 Then lblRegistroInv.Text = "Hay 1 registro."
            If dt.Rows.Count = 0 Then lblRegistroInv.Text = "No hay registros."

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try


    End Sub

    Private Sub BtnListaBienes_Click(sender As Object, e As EventArgs) Handles BtnListaBienes.Click
        ListaBienesInventariados()
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Try
            exportarEstados_xHoja()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub exportarEstados_xHoja()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        Dim dt3 As New DataTable()
        Dim dt4 As New DataTable()
        Dim dt5 As New DataTable()
        Dim dt6 As New DataTable()
        Dim dt7 As New DataTable()
        Dim dt8 As New DataTable()
        Dim psCodInv As Double = 0
        Dim objdatos As New Cls_Inventario_Verificacion
        ' Configurar los datos en dt1 y dt2...
        dt1 = objdatos.Lista_Inventario_Verificacion(Session("Ruta_Emp"), 0, "", 0)

        ' Crear el archivo de Excel
        Using excelPackage As New ExcelPackage()
            ' Agregar hojas al archivo de Excel
            Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Bienes Inventariados")

            ' Llenar Hoja1 con los datos de dt1
            worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

            ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=Lista_Bienes_Inventariados.xlsx")
            Response.BinaryWrite(excelPackage.GetAsByteArray())
            Response.End()
        End Using
    End Sub
End Class
