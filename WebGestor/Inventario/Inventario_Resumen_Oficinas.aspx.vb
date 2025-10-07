Imports System.Data.SqlClient
Imports System.Data
Imports OfficeOpenXml
Imports WebGestor
Partial Class Inventario_Inventario_Resumen_Oficinas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim ValorLatitud As String = objUbicacion.ObtenerValorLatitud
        'Dim ValorLongitud As String = objUbicacion.ObtenerValorLongitud

        If Not Page.IsPostBack Then
            Llenar_Combos()
            DdlInventario.Items.Add("< Seleccionar >")
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

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            Dim obj As New Cls_Inventario
            Dim objUbic As New Cls_Inventario_Ubicacion
            Dim pdCodInv As Double = 0
            Dim pdCodUbicInv As Double = 0
            Dim dtDatos As New DataTable
            Dim dt As New DataTable
            Dim i As Long = 0
            Dim pdCodInvUbicacion As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            Dim psPersonal As String = ""
            If pdCodInv = 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Inventario.')", True)
            Else

                dt = obj.Lista_Inventario_Resumen_Costos(Session("Ruta_Emp"), pdCodInv)

                GvResumenCostos.DataSource = dt
                GvResumenCostos.DataBind()

                If GvResumenCostos.Rows.Count > 0 Then
                    For i = 0 To GvResumenCostos.Rows.Count - 1
                        pdCodInvUbicacion = GvResumenCostos.Rows(i).Cells(3).Text
                        psPersonal = ""
                        dtDatos = objUbic.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), pdCodInvUbicacion)
                        For Each dr As DataRow In dtDatos.Rows
                            If psPersonal <> "" Then psPersonal = psPersonal & "," & "<br/>"
                            psPersonal = psPersonal & Nu(dr("nombre"))
                        Next
                        GvResumenCostos.Rows(i).Cells(12).Text = psPersonal
                        psPersonal = ""
                        dtDatos = objUbic.Inventario_Ubicaciones_Personal_Verifiacion(Session("Ruta_Emp"), pdCodInvUbicacion)
                        For Each dr As DataRow In dtDatos.Rows
                            If psPersonal <> "" Then psPersonal = psPersonal & "," & "<br/>"
                            psPersonal = psPersonal & Nu(dr("nombre"))
                        Next
                        GvResumenCostos.Rows(i).Cells(13).Text = psPersonal
                        GvResumenCostos.Rows(i).Cells(19).Text = CalcularDiferenciaDeTiempo(pdCodInvUbicacion)
                    Next
                End If


                If dt.Rows.Count > 1 Then
                    lblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 0 Then
                    lblRegistro2.Text = "No hay registros."
                ElseIf dt.Rows.Count = 1 Then
                    lblRegistro2.Text = "Hay 1 registro."

                End If

            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub
    Function CalcularDiferenciaDeTiempo(ByVal pdCodInvUbica As Double) As String
        ' Definir las fechas y horas
        CalcularDiferenciaDeTiempo = ""
        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim dtDatos As New DataTable

        Dim psFechaIni As String = ""
        Dim psfechafin As String = ""
        'Inventario_Ubicaciones_HorayFecha_Verifiacion
        ' Calcular la diferencia de tiempo

        dtDatos = objUbic.Inventario_Ubicaciones_HorayFecha_Verifiacion(Session("Ruta_Emp"), pdCodInvUbica)
        For Each dr As DataRow In dtDatos.Rows
            psFechaIni = Nu(dr("fecha_ini")) & " " & Nu(dr("hora_ini"))
            psfechafin = Nu(dr("fecha_fin")) & " " & Nu(dr("hora_fin"))
        Next
        If psFechaIni <> "" And psfechafin <> "" Then
            Dim fechaInicio As Date = Date.ParseExact(psFechaIni, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture)
            Dim fechaFin As Date = Date.ParseExact(psfechafin, "yyyy/MM/dd HH:mm", System.Globalization.CultureInfo.InvariantCulture)

            Dim diferencia As TimeSpan = fechaFin - fechaInicio
            ' Mostrar la diferencia de tiempo en días, horas y minutos
            Dim dias As Integer = diferencia.Days
            Dim horas As Integer = diferencia.Hours
            Dim minutos As Integer = diferencia.Minutes

            ' Mostrar el resultado
            Console.WriteLine("La diferencia de tiempo es: " & dias & " días, " & horas & " horas y " & minutos & " minutos.")
            If dias > 0 Then
                CalcularDiferenciaDeTiempo = dias & " días, " & horas & " horas y " & minutos & " minutos."
            ElseIf dias = 0 And horas > 0 Then
                CalcularDiferenciaDeTiempo = horas & " horas y " & minutos & " minutos."
            ElseIf dias = 0 And horas = 0 And minutos > 0 Then
                CalcularDiferenciaDeTiempo = minutos & " minutos."
            End If
        End If
        Return CalcularDiferenciaDeTiempo
    End Function
    Private Sub Exportar()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        GvResumenCostos.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(GvResumenCostos)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ResumenCosto2.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click


        Try
            Dim obj As New Cls_Inventario
            Dim pdCodInv As Double = 0
            Dim pdCodUbicInv As Double = 0
            Dim dt As New DataTable

            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If

            If pdCodInv = 0 Then

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Inventario.')", True)
            Else

                dt = obj.Lista_Inventario_Resumen_Costos(Session("Ruta_Emp"), pdCodInv)
            End If

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("ResumenCosto")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=ResumenCosto.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnExportar2_Click(sender As Object, e As EventArgs) Handles BtnExportar2.Click

        Call Exportar()
    End Sub
End Class
