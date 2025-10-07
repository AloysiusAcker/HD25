Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Inventario_Inventario_Cargar_Valor_Libro
    Inherits System.Web.UI.Page
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
        Dim obj As New clsInv_Listados
        Dim objInsUpdDel As New clsInv_InsUpdDel
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim pDato As String = ""
        Dim ARCHIVO As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal3 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Dim psSerieNumerar As String = ""
        Dim psPlaca As String = ""
        Dim Serie As String = ""
        Dim Placa As String = ""
        lblMensajeExportada.Text = ""
        Dim Rs As SqlDataReader
        Try
            If Session("UnaVez") = "1" Then
                If NomArchivo.HasFile Then
                    'lblMensajeExportada.Text = "Espere ...."
                    'ARCHIVO = NomArchivo.PostedFile.FileName
                    oXL = CreateObject("Excel.Application")
                    oXL.Visible = False
                    oWB = oXL.Workbooks.Open("D:\HAC-DATA01-COMPARTIDO\CARGA INVENTARIO PRUEBA 1.xls")
                    'oWB = oXL.Workbooks.Open(NomArchivo.PostedFile.FileName)
                    oSheet = oWB.ActiveSheet
                    ' Como prueba inicial solo pintaremos lo que haya en la primera celda

                    For i = 4 To oSheet.Rows.Count
                        If Nz(oSheet.Cells(i, 1).value) = 0 Then
                            Session("UnaVez") = "2"
                            Exit For
                        Else
                            psSerieNumerar = "" : Serie = "" : psPlaca = ""
                            Serie = oSheet.Cells(i, 3).value
                            psPlaca = oSheet.Cells(i, 2).value
                            If psPlaca <> "" Then
                                CmdGlobal.CommandText = "SELECT SERIE_NUMERAR FROM TBINV_ARTICULOS_SERIES_0001 WHERE PLACA_NRO = " & psPlaca
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psSerieNumerar = Nz(Rs(0)) : Placa = " PLACA_NRO = " & psPlaca
                                    End While
                                End If
                                Rs.Close()
                            End If
                            If psSerieNumerar = "" And Serie <> "" Then
                                CmdGlobal.CommandText = "SELECT SERIE_NUMERAR FROM TBINV_ARTICULOS_SERIES_0001 WHERE SERIE_NRO = '" & Serie & "'"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psSerieNumerar = Nz(Rs(0)) : Placa = " SERIE_NRO = " & psPlaca
                                    End While
                                End If
                                Rs.Close()
                            End If
                            If psSerieNumerar <> "" Then
                                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_0001 SET SERIE_VALORRESIDUAL = " & Nz(RTrim(LTrim(oSheet.Cells(i, 4).value))) & " WHERE SERIE_NUMERAR = " & psSerieNumerar
                                CmdGlobal.ExecuteNonQuery()
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
                    lblMensajeExportada.Text = "La actualización de Valor en libro terminó."
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblMensajeExportada.Text = ""
            Session("UnaVez") = "1"
        End If
    End Sub
End Class
