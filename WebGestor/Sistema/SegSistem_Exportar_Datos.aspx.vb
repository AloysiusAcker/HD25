Imports System.Data
Imports WebGestor
Imports OfficeOpenXml
Imports System.Data.SqlClient
Partial Class Sistema_SegSistem_Exportar_Datos
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Genera el archivo Excel

        Dim valor As String = Request.QueryString("parametro")
        Dim var_Tabla1 As String = Request.QueryString("var_Tabla1")
        Dim var_Tabla2 As String = Request.QueryString("var_Tabla2")
        Dim var_Tabla3 As String = Request.QueryString("var_Tabla3")

        Dim dt As New DataTable

        Dim NomArch As String = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim i As Integer = 0
        Dim Da As SqlDataAdapter
        If valor <> "" Then

            Cn.Open()
            CmdGlobal.Connection = Cn
            If valor = "1" Then
                CmdGlobal.CommandText = " SELECT NIVEL1_DESCRIP as [Nivel 1], NIVEL1_CODIGO as [Cod Nivel 1] From " & var_Tabla1 & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') ORDER BY NIVEL1_DESCRIP"
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            End If
            If valor = "2" Then
                CmdGlobal.CommandText = " SELECT TB1.NIVEL1_DESCRIP as [Nivel 1], TB2.NIVEL2_DESCRIP as [Nivel 2],TB2.NIVEL1_CODIGO as [Cod Nivel 1],TB2.NIVEL2_CODIGO as [Cod Nivel 2] " _
                                          & " FROM " & var_Tabla2 & " TB2 INNER JOIN " & var_Tabla1 & " TB1 " _
                                          & " ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO And TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                          & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0')  " _
                                          & " AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                          & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP"
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            End If
            If valor = "3" Then
                CmdGlobal.CommandText = " SELECT TB1.NIVEL1_DESCRIP as [Nivel 1], TB2.NIVEL2_DESCRIP as [Nivel 2],TB3.NIVEL3_DESCRIP as [Nivel 3], TB3.NIVEL3_NS_DHM as [Nivel de Servicio], TB2.NIVEL1_CODIGO as [Cod Nivel 1],TB2.NIVEL2_CODIGO as [Cod Nivel 2] , TB3.NIVEL3_CODIGO as [Cod Nivel 3] " _
                                          & " FROM " & var_Tabla2 & " TB2 INNER JOIN " & var_Tabla1 & " TB1 ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                          & " INNER JOIN " & var_Tabla3 & " TB3 ON TB2.EMPRESA_CODIGO=TB3.EMPRESA_CODIGO AND TB2.NIVEL2_CODIGO = TB3.NIVEL2_CODIGO " _
                                          & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0') AND (TB3.NIVEL3_SYS_EST = '0')  AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                          & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP, TB3.NIVEL3_DESCRIP "
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            End If

            If valor = "4" Then

                CmdGlobal.CommandText = " SELECT (SELECT ELEMEN_VALOR FROM BDGrupoEmpresas.dbo.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC473' AND ELEMEN_CODIGO = PROCESO_CODIGO) AS [PROCESO], NIVEL1_DESCRIP as [Petición],  " _
                                    & " TB2.NIVEL2_DESCRIP as [Nivel 2],TB3.NIVEL3_DESCRIP as [Nivel 3], PROCESO_CODIGO, tb1.NIVEL1_CODIGO as [Cod Nivel 1], TB2.NIVEL2_CODIGO as [Cod Nivel 2], TB3.NIVEL3_CODIGO as [Cod Nivel 3] from dbo.TBTICKET_RELACION_PROCESO_GTP1 AS A INNER JOIN TBESP_GTP1 AS TB1 ON TB1.NIVEL1_CODIGO = A.GTP1_CODIGO INNER JOIN TBESP_GTP2 TB2 ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                    & " INNER JOIN TBESP_GTP3 TB3 ON TB2.EMPRESA_CODIGO=TB3.EMPRESA_CODIGO AND TB2.NIVEL2_CODIGO = TB3.NIVEL2_CODIGO " _
                                    & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0') AND (TB3.NIVEL3_SYS_EST = '0')  AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                    & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP, TB3.NIVEL3_DESCRIP "
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            End If

            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Datos")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                If valor <> "4" Then Response.AddHeader("content-disposition", "attachment; filename=Tablas_Especiales.xlsx")
                If valor = "4" Then Response.AddHeader("content-disposition", "attachment; filename=Tablas_Proceso_Peticion.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using
        End If

    End Sub
End Class
