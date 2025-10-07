Imports System.IO
Imports ClosedXML.Excel
Imports System.Data
Imports OfficeOpenXml
Partial Class Inventario_Inventario_ExportarExcel
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Genera el archivo Excel

        Dim valor As String = Request.QueryString("parametro") 'Nro recepcion
        Dim valor2 As String = Request.QueryString("parametro2") 'proveedor


        Dim dt As New DataTable
        Dim pdCodRecep As Double = valor
        Dim NomArch As String = ""
        dt = obj.Lista_Recepcion_Series_Exportar(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecep)
        NomArch = valor & " " & valor2



        Using excelPackage As New ExcelPackage()
            ' Agregar hojas al archivo de Excel
            Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("LECTURA")

            ' Llenar Hoja1 con los datos de dt1
            worksheet1.Cells("A1").LoadFromDataTable(dt, True)

            ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=" & NomArch & ".xlsx")
            Response.BinaryWrite(excelPackage.GetAsByteArray())
            Response.End()
        End Using


    End Sub
End Class
