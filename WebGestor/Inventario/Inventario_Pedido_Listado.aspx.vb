Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports System.IO
Imports ClosedXML.Excel
Partial Class Inventario_Inventario_Pedido_Listado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Call LlenaComboItem("TBOPC552", DdlTipoPedido)
        End If
    End Sub

    Private Sub BtnLeer_Click(sender As Object, e As EventArgs) Handles BtnLeer.Click
        LeerExcelHojaxHoja()
        BtnListar_Click(sender, e)
    End Sub

    Private Sub LeerExcelHojaxHoja()
        If FileUpload1.HasFile Then
            Try
                ' Guardar el archivo en el servidor
                Dim filePath As String = Path.Combine(Server.MapPath("~/UploadedFiles/"), FileUpload1.FileName)
                FileUpload1.SaveAs(filePath)

                ' Leer el archivo Excel
                Using package As New ExcelPackage(New FileInfo(filePath))
                    Dim allData As New DataTable()

                    ' Leer cada hoja del archivo Excel
                    For Each worksheet As ExcelWorksheet In package.Workbook.Worksheets
                        ' Leer los datos de la hoja
                        Dim sheetData As DataTable = GetDataTableFromWorksheet(worksheet)

                    Next
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Termino la carga.');", True)
                End Using
            Catch ex As Exception
                ' Manejar errores
                ' Aquí puedes agregar un manejo de errores más detallado
                Response.Write("Error: " & ex.Message)
            End Try
        Else
            Response.Write("Please select a file to upload.")
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New clsInv_Listados
        Try
            Dim ptipoPedido As String = ""
            If DdlTipoPedido.SelectedValue <> "< Seleccionar >" Then
                ptipoPedido = DdlTipoPedido.SelectedValue
            End If
            Dim dt As DataTable
            dt = obj.Lista_Pedidos(Session("Ruta_Emp"), ptipoPedido)
            Flex.DataSource = dt
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
        'Lista_Pedidos
    End Sub

    Private Sub DdlTipoPedido_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoPedido.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub

    Private Function GetDataTableFromWorksheet(worksheet As ExcelWorksheet) As DataTable
        Dim CnA As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CnB As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobalA As New SqlCommand
        Dim CmdGlobalB As New SqlCommand
        Dim Rs As SqlDataReader
        CnA.Open() : CmdGlobalA.Connection = CnA
        CnB.Open() : CmdGlobalB.Connection = CnB
        Dim psTicket1 As String = ""
        Dim psTarea2 As String = ""
        Dim psUsuario3 As String = ""
        Dim pRegistro4 As String = ""
        Dim psCC5 As String = ""
        Dim psDescripción6 As String = ""
        Dim psOC7 As String = ""
        Dim psCantidad8 As String = ""
        Dim psTipoAtencion9 As String = ""
        Dim psNombreHoja As String = ""
        Dim psMotivoRecojo As String = ""
        Dim psPlaca As String = ""
        Dim psSerie As String = ""
        Dim psEstado As String = ""
        Dim psEnvio As String = ""
        Dim psObservacion As String = ""
        Dim psCCDescripcion As String = ""

        Dim ValorSys As String = Session("User") & FechaActual() & HoraActual()
        Dim pdRegistro As Double = 0

        Dim dt As New DataTable(worksheet.Name)
        Dim hasHeader As Boolean = True ' Ajusta esto según si tu hoja tiene encabezados o no

        ' Crear columnas en DataTable
        For col As Integer = 1 To worksheet.Dimension.End.Column
            dt.Columns.Add(If(hasHeader, worksheet.Cells(1, col).Text, "Column" & col))
        Next

        ' Agregar filas al DataTable
        Dim startRow As Integer = If(hasHeader, 2, 1)
        For row As Integer = startRow To worksheet.Dimension.End.Row
            Dim newRow As DataRow = dt.NewRow()
            psNombreHoja = worksheet.Name
            For col As Integer = 1 To worksheet.Dimension.End.Column
                newRow(col - 1) = worksheet.Cells(row, col).Text
            Next
            dt.Rows.Add(newRow)
        Next

        For Each dr As DataRow In dt.Rows
            CmdGlobalA.CommandText = "SELECT MAX(ISNULL(INVPEDIDO_CODIGO,0)) FROM TBINV_PEDIDO "
            Rs = CmdGlobalA.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdRegistro = Nz(Rs(0)) + 1
                End While
            Else
                pdRegistro = 1
            End If
            Rs.Close()

            If UCase(psNombreHoja) = "REQUERIMIENTO" Then
                psTicket1 = Nu(dr(0))
                psTarea2 = Nu(dr(1))
                psUsuario3 = Nu(dr(2))
                pRegistro4 = Nu(dr(3))
                psCC5 = Nu(dr(4))
                psDescripción6 = Nu(dr(5))
                psOC7 = Nu(dr(6))
                psCantidad8 = Nu(dr(7))
                psTipoAtencion9 = Nu(dr(8))
                If psTicket1 <> "" And psTarea2 <> "" Then

                    CmdGlobalA.CommandText = "SELECT * FROM TBINV_PEDIDO WHERE INVPEDIDO_NRO_TICKET =  '" & psTicket1 & "' AND  INVPEDIDO_TAREA = '" & psTarea2 & "'"
                    Rs = CmdGlobalA.ExecuteReader
                    If Rs.HasRows Then
                        Rs.Close()
                    Else
                        Rs.Close()
                        CmdGlobalA.CommandText = " INSERT INTO TBINV_PEDIDO ( EMPRESA_CODIGO, INVPEDIDO_CODIGO, INVPEDIDO_REG_FECHA, INVPEDIDO_REG_HORA, " _
                                   & " INVPEDIDO_REG_USUARIO, INVPEDIDO_REG_TIPO, INVPEDIDO_NRO_TICKET, INVPEDIDO_TAREA,  " _
                                   & " INVPEDIDO_USUARIO, INVPEDIDO_USUARIO_NOMBRE, INVPEDIDO_CCOSTO, INVPEDIDO_NRO_OC, " _
                                   & " INVPEDIDO_ARTICULO_CANT, INVPEDIDO_ARTICULO_NOMBRE, INVPEDIDO_TIPO_ATENCION_NOMBRE, INVPEDIDO_SYS_CRE, INVPEDIDO_SYS_EST)" _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & pdRegistro & ", '" & FechaActual() & "', '" & HoraActual() & "', " _
                                   & " '" & Session("User") & "', '2', '" & psTicket1 & "', '" & psTarea2 & "', " _
                                   & " '" & pRegistro4 & "', '" & psUsuario3 & "', '" & psCC5 & "', '" & psOC7 & "', " _
                                   & " " & psCantidad8 & ", '" & psDescripción6 & "', '" & psTipoAtencion9 & "', '" & ValorSys & "', '0')"
                        CmdGlobalA.ExecuteNonQuery()
                    End If
                End If
            ElseIf UCase(psNombreHoja) = "RECOJO" Then '1
                psMotivoRecojo = Nu(dr(0))
                psTicket1 = Nu(dr(1))
                psTarea2 = Nu(dr(2))
                psCCDescripcion = Nu(dr(3))
                psOC7 = Nu(dr(4))
                psCantidad8 = Nu(dr(5))
                psDescripción6 = Nu(dr(6))
                psPlaca = Nu(dr(7))
                If psDescripción6 <> "" And psPlaca <> "" Then
                    If psPlaca <> "-" Then CmdGlobalA.CommandText = "SELECT * FROM TBINV_PEDIDO WHERE INVPEDIDO_NRO_TICKET =  '" & psTicket1 & "' AND  INVPEDIDO_TAREA = '" & psTarea2 & "' AND INVPEDIDO_ARTICULO_NOMBRE = '" & psDescripción6 & "' AND INVPEDIDO_PLACA =" & Nz(psPlaca) & " "
                    If psPlaca = "-" Then CmdGlobalA.CommandText = "SELECT * FROM TBINV_PEDIDO WHERE INVPEDIDO_NRO_TICKET =  '" & psTicket1 & "' AND  INVPEDIDO_TAREA = '" & psTarea2 & "' AND INVPEDIDO_ARTICULO_NOMBRE = '" & psDescripción6 & "' "

                    Rs = CmdGlobalA.ExecuteReader
                    If Rs.HasRows Then
                        Rs.Close()
                    Else
                        Rs.Close()

                        CmdGlobalA.CommandText = " INSERT INTO TBINV_PEDIDO ( EMPRESA_CODIGO, INVPEDIDO_CODIGO, INVPEDIDO_REG_FECHA, INVPEDIDO_REG_HORA, " _
                                   & " INVPEDIDO_REG_USUARIO, INVPEDIDO_REG_TIPO, INVPEDIDO_NRO_TICKET, INVPEDIDO_TAREA,  INVPEDIDO_MOTIVO_RECOJO,  " _
                                   & " INVPEDIDO_CCOSTO, INVPEDIDO_CCOSTO_NOMBRE,  " _
                                   & " INVPEDIDO_ARTICULO_CANT, INVPEDIDO_ARTICULO_NOMBRE, INVPEDIDO_SYS_CRE, INVPEDIDO_SYS_EST)" _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & pdRegistro & ", '" & FechaActual() & "', '" & HoraActual() & "', " _
                                   & " '" & Session("User") & "', '1', '" & psTicket1 & "', '" & psTarea2 & "', '" & psMotivoRecojo & "'," _
                                   & " '" & psCC5 & "', '" & psCCDescripcion & "', " _
                                   & " " & psCantidad8 & ", '" & psDescripción6 & "', '" & ValorSys & "', '0')"
                        CmdGlobalA.ExecuteNonQuery()
                        If psPlaca <> "" And psPlaca <> "-" Then
                            CmdGlobalA.CommandText = " UPDATE TBINV_PEDIDO SET INVPEDIDO_PLACA  = " & Nz(psPlaca) & " WHERE INVPEDIDO_CODIGO= " & pdRegistro
                            CmdGlobalA.ExecuteNonQuery()
                        End If
                    End If
                End If
            ElseIf UCase(psNombreHoja) = "INCIDENCIAS" Then '3
                psTicket1 = Nu(dr(0))
                psTarea2 = Nu(dr(1))
                psCCDescripcion = Nu(dr(2))
                psCC5 = Nu(dr(3))
                psCantidad8 = Nu(dr(4))
                psDescripción6 = Nu(dr(5))
                psPlaca = Nu(dr(6))
                psSerie = Nu(dr(7))
                psEstado = Nu(dr(8))
                psEnvio = Nu(dr(9))
                psObservacion = Nu(dr(10))
                If psTicket1 <> "" And psTarea2 <> "" Then
                    CmdGlobalA.CommandText = "SELECT * FROM TBINV_PEDIDO WHERE INVPEDIDO_NRO_TICKET =  '" & psTicket1 & "' AND  INVPEDIDO_TAREA = '" & psTarea2 & "'"
                    Rs = CmdGlobalA.ExecuteReader
                    If Rs.HasRows Then
                        Rs.Close()
                    Else
                        Rs.Close()
                        CmdGlobalA.CommandText = " INSERT INTO TBINV_PEDIDO ( EMPRESA_CODIGO, INVPEDIDO_CODIGO, INVPEDIDO_REG_FECHA, INVPEDIDO_REG_HORA, " _
                                   & " INVPEDIDO_REG_USUARIO, INVPEDIDO_REG_TIPO, INVPEDIDO_NRO_TICKET, INVPEDIDO_TAREA,  " _
                                   & " INVPEDIDO_CCOSTO, INVPEDIDO_CCOSTO_NOMBRE,  " _
                                   & " INVPEDIDO_ARTICULO_CANT, INVPEDIDO_ARTICULO_NOMBRE, INVPEDIDO_SYS_CRE, INVPEDIDO_SYS_EST, " _
                                   & " INVPEDIDO_SERIE,  INVPEDIDO_ENVIO,  INVPEDIDO_OBSERVACION, INVPEDIDO_ESTADO)" _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & pdRegistro & ", '" & FechaActual() & "', '" & HoraActual() & "', " _
                                   & " '" & Session("User") & "', '3', '" & psTicket1 & "', '" & psTarea2 & "', " _
                                   & " '" & psCC5 & "', '" & psCCDescripcion & "', 1, '" & psDescripción6 & "',  '" & ValorSys & "', '0', " _
                                   & " '" & psSerie & "', '" & psEnvio & "', '" & psObservacion & "', '" & psEstado & "') "
                        CmdGlobalA.ExecuteNonQuery()
                        If psPlaca <> "" And psPlaca <> "-" And psPlaca <> "S/N" Then
                            CmdGlobalA.CommandText = " UPDATE TBINV_PEDIDO SET INVPEDIDO_PLACA  = " & Nz(psPlaca) & " WHERE INVPEDIDO_CODIGO= " & pdRegistro
                            CmdGlobalA.ExecuteNonQuery()
                        End If
                    End If
                End If
            End If
        Next

        Return dt
    End Function
End Class
