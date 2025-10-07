Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Imports System.IO
Imports System.Reflection
Partial Class CallCenter_CallCenter_CargaDatos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblMensajeExportada.Text = ""
            Session("UnaVez") = "1"
        End If
    End Sub
    Protected Sub btnArchExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchExcel.Click
        Dim oXL As Excel.Application
        Dim oWB As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        Dim i As Integer = 0
        Dim Oficina As Double = 0
        Dim Puesto As Double = 0
        Dim Territorio As Double = 0
        Dim pCodBanca As Double = 0
        Dim pAntiguedad As Decimal = 0
        Dim obj As New ModuloCas
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim pDato As String = ""
        Dim ARCHIVO As String = ""
        Try
            If Session("UnaVez") = "1" Then
                If NomArchivo.HasFile Then
                    oXL = CreateObject("Excel.Application")
                    oXL.Visible = False
                    oWB = oXL.Workbooks.Open(NomArchivo.PostedFile.FileName)
                    oSheet = oWB.ActiveSheet
                    ' Como prueba inicial solo pintaremos lo que haya en la primera celda
                    For i = 2 To oSheet.Rows.Count
                        'i = i + 1
                        If Nu(oSheet.Cells(i, 1).value) = "" Then
                            Session("UnaVez") = "2"
                            Exit For
                        End If
                        If Nu(oSheet.Cells(i, 1).value) <> "" Then
                            dt = obj.CasConsulta_ExistePersona(Trim(Mid(oSheet.Cells(i, 6).value, 1, 7)), "", "", "1", Session("Ruta_Emp"))
                            If dt.Rows.Count > 0 Then
                                For Each dr As Data.DataRow In dt.Rows
                                    '
                                Next
                            Else
                                '
                            End If
                        End If
                    Next
                    If Not oWB Is Nothing Then
                        EiminaReferencias(oSheet)
                        oXL.Workbooks(1).Close(False)
                        EiminaReferencias(oWB)
                        oXL.Quit()
                        EiminaReferencias(oXL)
                    End If
                    System.GC.Collect()
                    lblMensajeExportada.Text = "La actualización de personas terminó."
                    Exit Sub
                Else
                    lblMensajeExportada.Text = "No hay archivo que cargar"
                End If
            End If
        Catch Ex As SqlException
            lblMensajeExportada.Visible = True
            lblMensajeExportada.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblMensajeExportada.Visible = True
            lblMensajeExportada.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub EiminaReferencias(ByRef Referencias As Object)
        Try
            Do Until _
                 System.Runtime.InteropServices.Marshal.ReleaseComObject(Referencias) <= 0
            Loop
        Catch
        Finally
            Referencias = Nothing
        End Try
    End Sub
End Class
