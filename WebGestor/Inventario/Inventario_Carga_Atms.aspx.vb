Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports OfficeOpenXml

Partial Class Inventario_Inventario_Carga_Atms
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub
    Protected Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click

        Try

            If fileUpload.HasFile Then
                Dim fileExtension As String = Path.GetExtension(fileUpload.FileName)
                If fileExtension = ".xlsx" Then
                    Dim filePath As String = Server.MapPath("~/Uploads/") & fileUpload.FileName
                    fileUpload.SaveAs(filePath)
                    ReadExcelFile(filePath)
                Else
                    ' Mostrar un mensaje de error si el archivo no es un archivo Excel válido.
                    ' Puedes usar una etiqueta de Bootstrap para mostrar el mensaje de error.
                End If
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Termino la carga.');", True)
                BtnListar_Click(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
            'Session("UnaVez") = "2"
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
            'Session("UnaVez") = "2"
        End Try
    End Sub

    Private Sub ReadExcelFile(filePath As String)

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim ValorSys As String = ""
        Dim Rs As SqlDataReader

        Dim pdPlaca As Double = 0
        Dim psSerieNumerar As Double = 0
        Dim psSerie As String = ""
        Dim pdTermAtm As Double = 0

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        Dim colAtm As Long = 0
        Dim colPlaca As Long = 0
        colAtm = 1
        colPlaca = 8
        filaIni = 2
        filafin = 1916
        Dim valor As String = String.Empty

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2

        ' Leer el archivo Excel
        Using package As New ExcelPackage(New FileInfo(filePath))
            'Using package As New ExcelPackage(New FileInfo(rutaArchivo))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin
                    If worksheet.Cells(row, colAtm).Value IsNot Nothing Then

                        Dim celda As ExcelRange = worksheet.Cells(row, colAtm)

                        If celda.Value IsNot Nothing Then
                            valor = celda.Value.ToString()
                            pdTermAtm = valor
                        End If

                    End If
                    If worksheet.Cells(row, colPlaca).Value IsNot Nothing Then

                        Dim celda2 As ExcelRange = worksheet.Cells(row, colPlaca)

                        If celda2.Value IsNot Nothing Then
                            valor = celda2.Value.ToString()
                            pdPlaca = valor
                        End If

                    End If

                    If pdPlaca <> 0 Then
                        CmdGlobal.CommandText = "SELECT SERIE_NUMERAR FROM TBINV_ARTICULOS_SERIES_0001 WHERE PLACA_NRO = " & pdPlaca
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psSerieNumerar = Nz(Rs(0))
                            End While
                        End If
                        Rs.Close()
                        If psSerieNumerar > 0 Then
                            CmdGlobal.CommandText = "  UPDATE TBINV_ARTICULOS_SERIES_0001 SET SERIE_ATM_NROTERMINAL = " & pdTermAtm & "  " _
                                                & " WHERE SERIE_NUMERAR = " & psSerieNumerar
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    End If
                Next
            End If
        End Using





        'Using package As New ExcelPackage(New FileInfo(filePath))
        'Dim worksheet = package.Workbook.Worksheets(0)

        'Dim startRow As Integer = worksheet.Dimension.Start.Row
        'Dim startCol As Integer = worksheet.Dimension.Start.Column
        'Dim endRow As Integer = worksheet.Dimension.End.Row
        'Dim endCol As Integer = worksheet.Dimension.End.Column

        '' Recorrer todas las filas y columnas del archivo Excel.
        'For row As Integer = startRow To endRow
        '    For col As Integer = startCol To endCol
        '        Dim cellValue As Object = worksheet.Cells(row, col).Text

        '        CmdGlobal.CommandText = "   "
        '        Rs = CmdGlobal.ExecuteReader
        '        If Rs.HasRows Then
        '            While Rs.Read

        '            End While
        '        End If
        '        Rs.Close()
        '        ' Aquí puedes procesar cada valor de celda, por ejemplo, mostrarlo en una etiqueta o almacenarlo en una lista.
        '    Next
        'Next
        'End Using
    End Sub

    Private Sub CargarExcel()
        Dim oXL As Excel.Application
        Dim oWB As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim ValorSys As String = ""
        Dim Rs As SqlDataReader
        Dim pdPlaca As Double = 0
        Dim psSerieNumerar As Double = 0
        Dim psSerie As String = ""
        Dim pdTermAtm As Double = 0

        Try
            If Session("UnaVez") = "1" Then
                If fileUpload.HasFile Then
                    oXL = CreateObject("Excel.Application")
                    oXL.Visible = False
                    'oWB = oXL.Workbooks.Open("D:\HAC-DATA01-COMPARTIDO\CARGA INVENTARIO PRUEBA 1.xls")
                    oWB = oXL.Workbooks.Open(fileUpload.PostedFile.FileName)
                    oSheet = oWB.ActiveSheet

                    For i = 2 To oSheet.Rows.Count
                        If Nz(oSheet.Cells(i, 1).value) = 0 Then
                            Session("UnaVez") = "2"
                            Exit For
                        Else

                            psSerie = QuitaComilla(Nu(oSheet.Cells(i, 7).value))
                            pdPlaca = Nz(oSheet.Cells(i, 8).value)
                            pdTermAtm = Nz(oSheet.Cells(i, 1).value)

                            If pdPlaca <> 0 Then
                                CmdGlobal.CommandText = "SELECT SERIE_NUMERAR FROM TBINV_ARTICULOS_SERIES_0001 WHERE PLACA_NRO = " & pdPlaca
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psSerieNumerar = Nz(Rs(0))
                                    End While
                                End If
                                Rs.Close()
                                CmdGlobal.CommandText = "  UPDATE TBINV_ARTICULOS_SERIES_0001 SET SERIE_ATM_NROTERMINAL = " & pdTermAtm & "  " _
                                                      & " WHERE SERIE_NUEMRAR = " & psSerieNumerar
                                CmdGlobal.ExecuteNonQuery()
                            End If
                        End If
                    Next

                End If
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try

    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dta As New DataTable
        dta = Nothing
        gvListaAtms.DataSource = dta
        gvListaAtms.DataBind()

        Try

            Dim objConn As New SqlConnection(Session("Ruta_Emp"))

            Dim objComand As New SqlCommand(" SELECT SERIE_NUMERAR, SERIE_NRO, PLACA_NRO, ART_CODIGO, ART_CODEQUIVA, ART_DESCRIPCION, SERIE_ATM_NROTERMINAL FROM TBINV_ARTICULOS_SERIES_0001 S INNER JOIN TBINV_ARTICULOS ART ON ART.ART_CODIGO = S.ARTICULO_CODIGO  WHERE NOT SERIE_ATM_NROTERMINAL IS NULL  ", objConn)

            Dim da As New SqlDataAdapter(objComand)
            Dim dt As New DataTable()
            da.Fill(dt)

            gvListaAtms.DataSource = dt
            gvListaAtms.DataBind()

            If dt.Rows.Count > 0 Then
                lblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro2.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro2.Text = "Hay 0 registro."
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try




    End Sub
End Class
