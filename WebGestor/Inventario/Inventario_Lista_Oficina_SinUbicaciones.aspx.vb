Imports System
Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports OfficeOpenXml
Partial Class Inventario_Inventario_Lista_Oficina_SinUbicaciones
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combos(DdlUbicacion)
        End If
    End Sub

    Protected Sub Llenar_Combos(ByVal combo As DropDownList)
        Try
            Dim obj As New clsInv_Listados
            combo.Items.Clear() 'Listar_Usuarios
            combo.DataSource = obj.Lista_Ubicaciones2(Session("Ruta_Emp"), Session("CodEmpresa"), "2")
            combo.DataTextField = "Ubicacion"
            combo.DataValueField = "UBICACION_CODIGO"
            combo.DataBind()
            combo.Items.Add("< Seleccionar >")
            combo.SelectedValue = "< Seleccionar >"

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
            Dim dt As New DataTable
            Dim pdCodInvUbica As Double = 0
            Dim pdCodUbicacion As Double = 0
            If DdlUbicacion.SelectedValue <> "< Seleccionar >" Then
                pdCodUbicacion = DdlUbicacion.SelectedValue
            End If
            If pdCodUbicacion = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Seleccionar Ubicación';", True)


            Else
                dt = obj.Invenatrio_Lista_Oficina_SinUbicacion(Session("Ruta_Emp"), pdCodUbicacion)
                GvLista.DataSource = dt
                GvLista.DataBind()
                If dt.Rows.Count > 1 Then
                    lblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 1 Then
                    lblRegistro.Text = "Hay 1 registro."
                Else
                    lblRegistro.Text = "No hay registros."
                End If
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "';", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "';", True)
        End Try
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click


        Try
            Dim obj As New Cls_Inventario
            Dim dt As New DataTable
            Dim pdCodInvUbica As Double = 0
            Dim pdCodUbicacion As Double = 0
            If DdlUbicacion.SelectedValue <> "< Seleccionar >" Then
                pdCodUbicacion = DdlUbicacion.SelectedValue
            End If
            If pdCodUbicacion = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Seleccionar Ubicación';", True)


            Else
                dt = obj.Invenatrio_Lista_Oficina_SinUbicacion(Session("Ruta_Emp"), pdCodUbicacion)

                ' Crear el archivo de Excel
                Using excelPackage As New ExcelPackage()
                    ' Agregar hojas al archivo de Excel
                    Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("ListaOficinas")

                    ' Llenar Hoja1 con los datos de dt1
                    worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                    ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                    Response.Clear()
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment; filename=ListaOficinas.xlsx")
                    Response.BinaryWrite(excelPackage.GetAsByteArray())
                    Response.End()


                End Using
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
End Class
