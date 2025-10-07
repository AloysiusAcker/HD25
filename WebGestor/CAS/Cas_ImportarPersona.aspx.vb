Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Imports System.IO
Imports System.Reflection
Partial Class Cas_ImportarPersona
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblMensajeExportada.Text = ""
            Session("UnaVez") = "1"
        End If
    End Sub
    Protected Sub btnArchTexto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchTexto.Click
        Dim objStreamReader As StreamReader
        Dim strLine As String
        Dim Lineas As Long = 0
        Dim Linea As String = ""
        Dim Oficina As Double = 0
        Dim Puesto As Double = 0
        Dim Territorio As Double = 0
        Dim pCodBanca As Double = 0
        Dim pAntiguedad As Decimal = 0
        Dim obj As New ModuloCas
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Try
            If NomArchivo.HasFile Then
                'Pass the file path and the file name to the StreamReader constructor.
                objStreamReader = New StreamReader(NomArchivo.PostedFile.FileName)
                'Read the first line of text.
                strLine = objStreamReader.ReadLine
                'Continue to read until you reach the end of the file.
                Do While Not strLine Is Nothing
                    'Write the line to the Console window.
                    Console.WriteLine(strLine)
                    'Read the next line.
                    strLine = objStreamReader.ReadLine
                    Linea = strLine
                    Lineas = Lineas + 1
                    dt = obj.CasConsulta_ExistePersona(Trim(Mid(Linea, 102, 7)), "", "", "1",Session("Ruta_Emp"))
                    If dt.Rows.Count > 0 Then
                        For Each dr As Data.DataRow In dt.Rows
                            If Nu(dr("TBCAS_PERSONA_NOMBRE")) <> QuitaComilla(Trim(Mid(Linea, 109, 45))) And Nu(dr("TBCAS_PERSONA_APELLIDOS")) <> QuitaComilla(Trim(Mid(Linea, 154, 45))) Then
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", QuitaComilla(Trim(Mid(Linea, 109, 45))), QuitaComilla(Trim(Mid(Linea, 154, 45))), 0, 0, "", "", "", 0, "", 0, 0, "9",Session("Ruta_Emp"))
                            End If
                            'OFICINA
                            If QuitaComilla(Trim(Mid(Linea, 1, 3))) <> "" And QuitaComilla(Trim(Mid(Linea, 4, 45))) <> "" Then
                                dt2 = obj.CasConsulta_ExisteOficina(Trim(Mid(Linea, 1, 3)),Session("Ruta_Emp"))
                                If dt2.Rows.Count = 0 Then
                                    obj.InsUpd_Oficina(Oficina, Trim(Mid(Linea, 1, 3)), QuitaComilla(Trim(Mid(Linea, 4, 45))), 1, HttpContext.Current.User.Identity.Name, "1",Session("Ruta_Emp"))
                                Else
                                    For Each dr2 As Data.DataRow In dt2.Rows
                                        Oficina = Nz(dr2("TBCAS_OFICINA_CODIGO"))
                                    Next
                                End If
                                dt2 = Nothing
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", Oficina, 0, "", "", "", 0, "", 0, 0, "4",Session("Ruta_Emp"))
                            End If
                            'PUESTO
                            If QuitaComilla(Trim(Mid(Linea, 199, 4))) <> "" And QuitaComilla(Trim(Mid(Linea, 203, 45))) <> "" Then
                                dt2 = obj.CasConsulta_ExistePuetso("", QuitaComilla(Trim(Mid(Linea, 199, 4))), "2",Session("Ruta_Emp"))
                                If dt2.Rows.Count = 0 Then
                                    obj.InsUpd_Puesto(Puesto, QuitaComilla((Mid(Linea, 203, 45))), QuitaComilla((Mid(Linea, 199, 4))), "3",Session("Ruta_Emp"))
                                Else
                                    For Each dr2 As Data.DataRow In dt2.Rows
                                        Puesto = Nz(dr2("PUESTO_CODIGO"))
                                    Next
                                End If
                                dt2 = Nothing
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, Puesto, "", "", "", 0, "", 0, 0, "5",Session("Ruta_Emp"))
                            End If
                            'TERRITORIO
                            If QuitaComilla(Trim(Mid(Linea, 49, 4))) <> "" And QuitaComilla(Trim(Mid(Linea, 53, 45))) <> "" Then
                                dt2 = obj.CasConsulta_ExisteTerritorio(QuitaComilla(Trim(Mid(Linea, 49, 4))),Session("Ruta_Emp"))
                                If dt2.Rows.Count = 0 Then
                                    obj.InsUpd_Territorio(Territorio, QuitaComilla((Mid(Linea, 49, 4))), QuitaComilla((Mid(Linea, 53, 45))), "1",Session("Ruta_Emp"))
                                Else
                                    For Each dr2 As Data.DataRow In dt2.Rows
                                        Territorio = Nz(dr2("TERRI_CODIGO"))
                                    Next
                                End If
                                dt2 = Nothing
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", 0, "", 0, Territorio, "6",Session("Ruta_Emp"))
                            End If
                            If Nu(dr("TBCAS_TELEFONO")) <> Trim(Mid(Linea, 251, 9)) Then
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, Trim(Mid(Linea, 251, 9)), "", "", 0, "", 0, 0, "10",Session("Ruta_Emp"))
                            End If
                            If Nu(dr("TBCAS_ANEXO")) <> Trim(Mid(Linea, 260, 9)) Then
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", Trim(Mid(Linea, 260, 9)), "", 0, "", 0, 0, "11",Session("Ruta_Emp"))
                            End If
                            If Nu(dr("TBCAS_PERSONA_BANCA")) <> Trim(Mid(Linea, 98, 4)) Then
                                pCodBanca = Nz(Trim(Mid(Linea, 98, 4)))
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", pCodBanca, "", 0, 0, "8",Session("Ruta_Emp"))
                            End If
                            If Nu(dr("TBCAS_CORREO_ELECTRONICO")) <> Trim(Mid(Linea, 269, 45)) Then
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", Trim(Mid(Linea, 269, 45)), 0, "", 0, 0, "12",Session("Ruta_Emp"))
                            End If
                            If Nu(dr("TBCAS_PERSONA_FILLER")) <> Trim(Mid(Linea, 314, 37)) Then
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", 0, Trim(Mid(Linea, 314, 37)), 0, 0, "13",Session("Ruta_Emp"))
                            End If
                            If Nu(dr("TBCAS_PERSONA_ANTIGUEDAD")) <> Trim(Mid(Linea, 248, 3)) Then
                                pAntiguedad = Nz(Trim(Mid(Linea, 248, 3)))
                                obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", 0, "", pAntiguedad, 0, "7",Session("Ruta_Emp"))
                            End If
                        Next
                    Else
                        'OFICINA
                        If QuitaComilla(Trim(Mid(Linea, 1, 3))) <> "" And QuitaComilla(Trim(Mid(Linea, 4, 45))) <> "" Then
                            dt2 = obj.CasConsulta_ExisteOficina(Trim(Mid(Linea, 1, 3)),Session("Ruta_Emp"))
                            If dt2.Rows.Count = 0 Then
                                obj.InsUpd_Oficina(Oficina, Trim(Mid(Linea, 1, 3)), QuitaComilla(Trim(Mid(Linea, 4, 45))), 1, HttpContext.Current.User.Identity.Name, "1",Session("Ruta_Emp"))
                            Else
                                For Each dr2 As Data.DataRow In dt2.Rows
                                    Oficina = Nz(dr2("TBCAS_OFICINA_CODIGO"))
                                Next
                            End If
                            dt2 = Nothing
                        End If
                        'PUESTO
                        If QuitaComilla(Trim(Mid(Linea, 199, 4))) <> "" And QuitaComilla(Trim(Mid(Linea, 203, 45))) <> "" Then
                            dt2 = obj.CasConsulta_ExistePuetso("", QuitaComilla(Trim(Mid(Linea, 199, 4))), "2",Session("Ruta_Emp"))
                            If dt2.Rows.Count = 0 Then
                                obj.InsUpd_Puesto(Puesto, QuitaComilla((Mid(Linea, 203, 45))), QuitaComilla((Mid(Linea, 199, 4))), "3",Session("Ruta_Emp"))
                            Else
                                For Each dr2 As Data.DataRow In dt2.Rows
                                    Puesto = Nz(dr2("PUESTO_CODIGO"))
                                Next
                            End If
                            dt2 = Nothing
                        End If
                        'TERRITORIO
                        If QuitaComilla(Trim(Mid(Linea, 49, 4))) <> "" And QuitaComilla(Trim(Mid(Linea, 53, 45))) <> "" Then
                            dt2 = obj.CasConsulta_ExisteTerritorio(QuitaComilla(Trim(Mid(Linea, 49, 4))),Session("Ruta_Emp"))
                            If dt2.Rows.Count = 0 Then
                                obj.InsUpd_Territorio(Territorio, QuitaComilla((Mid(Linea, 49, 4))), QuitaComilla((Mid(Linea, 53, 45))), "1",Session("Ruta_Emp"))
                            Else
                                For Each dr2 As Data.DataRow In dt2.Rows
                                    Territorio = Nz(dr2("TERRI_CODIGO"))
                                Next
                            End If
                            dt2 = Nothing
                        End If
                        pCodBanca = Nz(Trim(Mid(Linea, 98, 4)))
                        pAntiguedad = Nz(Trim(Mid(Linea, 248, 3)))
                        obj.InsUpd_Personas(0, Trim(Mid(Linea, 102, 7)), QuitaComilla(Trim(Mid(Linea, 109, 45))), QuitaComilla(Trim(Mid(Linea, 154, 45))), Oficina, Puesto, Trim(Mid(Linea, 251, 9)), Trim(Mid(Linea, 260, 9)), Trim(Mid(Linea, 269, 45)), pCodBanca, Trim(Mid(Linea, 314, 37)), pAntiguedad, Territorio, "1",Session("Ruta_Emp"))
                    End If
                Loop
                'Close the file.
                objStreamReader.Close()
                'Console.ReadLine()
                lblMensajeExportada.Text = "La actualización de personas terminó."
            Else
                lblMensajeExportada.Text = "No hay archivo que cargar"
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
                    'lblMensajeExportada.Text = "Espere ...."
                    'ARCHIVO = NomArchivo.PostedFile.FileName
                    oXL = CreateObject("Excel.Application")
                    oXL.Visible = False
                    'oWB = oXL.Workbooks.Open("D:\CAS\LISTADO USUARIOS COL VF OCT.xls")
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
                            dt = obj.CasConsulta_ExistePersona(Trim(Mid(oSheet.Cells(i, 6).value, 1, 7)), "", "", "1",Session("Ruta_Emp"))
                            If dt.Rows.Count > 0 Then
                                For Each dr As Data.DataRow In dt.Rows
                                    If Nu(dr("TBCAS_PERSONA_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 7).value, 1, 45))) And Nu(dr("TBCAS_PERSONA_APELLIDOS")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 8).value, 1, 45))) Then
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", QuitaComilla(Trim(Mid(oSheet.Cells(i, 7).value, 1, 45))), QuitaComilla(Trim(Mid(oSheet.Cells(i, 6).value, 1, 45))), 0, 0, "", "", "", 0, "", 0, 0, "9",Session("Ruta_Emp"))
                                    End If
                                    'OFICINA
                                    If QuitaComilla(Trim(Mid(oSheet.Cells(i, 1).value, 1, 3))) <> "" And QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))) <> "" Then
                                        dt2 = obj.CasConsulta_ExisteOficina(Trim(Mid(oSheet.Cells(i, 1).value, 1, 3)),Session("Ruta_Emp"))
                                        If dt2.Rows.Count = 0 Then
                                            obj.InsUpd_Oficina(Oficina, Trim(Mid(oSheet.Cells(i, 1).value, 1, 3)), QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))), 1, HttpContext.Current.User.Identity.Name, "1",Session("Ruta_Emp"))
                                        Else
                                            For Each dr2 As Data.DataRow In dt2.Rows
                                                Oficina = Nz(dr2("TBCAS_OFICINA_CODIGO"))
                                                If Nu(dr2("TBCAS_OFICINA_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))) Then
                                                    obj.InsUpd_Oficina(Oficina, Trim(Mid(oSheet.Cells(i, 1).value, 1, 3)), QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))), 1, HttpContext.Current.User.Identity.Name, "3",Session("Ruta_Emp"))
                                                End If
                                            Next
                                        End If
                                        dt2 = Nothing
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", Oficina, 0, "", "", "", 0, "", 0, 0, "4",Session("Ruta_Emp"))
                                    End If
                                    'PUESTO
                                    If QuitaComilla(Trim(Mid(oSheet.Cells(i, 9).value, 1, 4))) <> "" And QuitaComilla(Trim(Mid(oSheet.Cells(i, 10).value, 1, 45))) <> "" Then
                                        dt2 = obj.CasConsulta_ExistePuetso("", QuitaComilla(Trim(Mid(oSheet.Cells(i, 9).value, 1, 4))), "2",Session("Ruta_Emp"))
                                        If dt2.Rows.Count = 0 Then
                                            obj.InsUpd_Puesto(Puesto, QuitaComilla((Mid(oSheet.Cells(i, 10).value, 1, 45))), QuitaComilla((Mid(oSheet.Cells(i, 9).value, 1, 4))), "3",Session("Ruta_Emp"))
                                        Else
                                            For Each dr2 As Data.DataRow In dt2.Rows
                                                Puesto = Nz(dr2("PUESTO_CODIGO"))
                                                If Nu(dr2("PUESTO_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 10).value, 1, 45))) Then
                                                    obj.InsUpd_Puesto(Puesto, QuitaComilla((Mid(oSheet.Cells(i, 10).value, 1, 45))), QuitaComilla((Mid(oSheet.Cells(i, 9).value, 1, 4))), "4",Session("Ruta_Emp"))
                                                End If
                                            Next
                                        End If
                                        dt2 = Nothing
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, Puesto, "", "", "", 0, "", 0, 0, "5",Session("Ruta_Emp"))
                                    End If
                                    'TERRITORIO
                                    If QuitaComilla(Trim(Mid(oSheet.Cells(i, 3).value, 1, 4))) <> "" And QuitaComilla(Trim(Mid(oSheet.Cells(i, 4).value, 1, 45))) <> "" Then
                                        dt2 = obj.CasConsulta_ExisteTerritorio(QuitaComilla(Trim(Mid(oSheet.Cells(i, 3).value, 1, 4))),Session("Ruta_Emp"))
                                        If dt2.Rows.Count = 0 Then
                                            obj.InsUpd_Territorio(Territorio, QuitaComilla((Mid(oSheet.Cells(i, 3).value, 1, 4))), QuitaComilla((Mid(oSheet.Cells(i, 4).value, 1, 45))), "1",Session("Ruta_Emp"))
                                        Else
                                            For Each dr2 As Data.DataRow In dt2.Rows
                                                Territorio = Nz(dr2("TERRI_CODIGO"))
                                                If Nu(dr2("TERRI_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 4).value, 1, 45))) Then
                                                    obj.InsUpd_Territorio(Territorio, QuitaComilla((Mid(oSheet.Cells(i, 3).value, 1, 4))), QuitaComilla((Mid(oSheet.Cells(i, 4).value, 1, 45))), "2",Session("Ruta_Emp"))
                                                End If
                                            Next
                                        End If
                                        dt2 = Nothing
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", 0, "", 0, Territorio, "6",Session("Ruta_Emp"))
                                    End If
                                    If Nu(dr("TBCAS_TELEFONO")) <> Trim(Mid(oSheet.Cells(i, 12).value, 1, 9)) Then
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, Trim(Mid(oSheet.Cells(i, 12).value, 1, 9)), "", "", 0, "", 0, 0, "10",Session("Ruta_Emp"))
                                    End If
                                    If Nu(dr("TBCAS_ANEXO")) <> Trim(Mid(oSheet.Cells(i, 13).value, 1, 9)) Then
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", Trim(Mid(oSheet.Cells(i, 13).value, 1, 9)), "", 0, "", 0, 0, "11",Session("Ruta_Emp"))
                                    End If
                                    If Nu(dr("TBCAS_PERSONA_BANCA")) <> Trim(Mid(oSheet.Cells(i, 5).value, 1, 4)) Then
                                        pCodBanca = Nz(Trim(Mid(oSheet.Cells(i, 5).value, 1, 4)))
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", pCodBanca, "", 0, 0, "8",Session("Ruta_Emp"))
                                    End If
                                    If Nu(dr("TBCAS_CORREO_ELECTRONICO")) <> Trim(Mid(oSheet.Cells(i, 14).value, 1, 45)) Then
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", Trim(Mid(oSheet.Cells(i, 14).value, 1, 45)), 0, "", 0, 0, "12",Session("Ruta_Emp"))
                                    End If
                                    If Nu(dr("TBCAS_PERSONA_FILLER")) <> Trim(Mid(oSheet.Cells(i, 15).value, 1, 37)) Then
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", 0, Trim(Mid(oSheet.Cells(i, 15).value, 1, 37)), 0, 0, "13",Session("Ruta_Emp"))
                                    End If
                                    If Nu(dr("TBCAS_PERSONA_ANTIGUEDAD")) <> Trim(Mid(oSheet.Cells(i, 11).value, 1, 3)) Then
                                        pAntiguedad = Nz(Trim(Mid(oSheet.Cells(i, 11).value, 1, 3)))
                                        obj.InsUpd_Personas(Nz(dr("TBCAS_PERSONA_CODIGO")), "", "", "", 0, 0, "", "", "", 0, "", pAntiguedad, 0, "7",Session("Ruta_Emp"))
                                    End If
                                Next
                            Else
                                'OFICINA
                                If QuitaComilla(Trim(Mid(oSheet.Cells(i, 1).value, 1, 3))) <> "" And QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))) <> "" Then
                                    dt2 = obj.CasConsulta_ExisteOficina(Trim(Mid(oSheet.Cells(i, 1).value, 1, 3)),Session("Ruta_Emp"))
                                    If dt2.Rows.Count = 0 Then
                                        obj.InsUpd_Oficina(Oficina, Trim(Mid(oSheet.Cells(i, 1).value, 1, 3)), QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))), 1, HttpContext.Current.User.Identity.Name, "1",Session("Ruta_Emp"))
                                    Else
                                        For Each dr2 As Data.DataRow In dt2.Rows
                                            Oficina = Nz(dr2("TBCAS_OFICINA_CODIGO"))
                                            If Nu(dr2("TBCAS_OFICINA_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))) Then
                                                obj.InsUpd_Oficina(Oficina, Trim(Mid(oSheet.Cells(i, 1).value, 1, 3)), QuitaComilla(Trim(Mid(oSheet.Cells(i, 2).value, 1, 45))), 1, HttpContext.Current.User.Identity.Name, "3",Session("Ruta_Emp"))
                                            End If
                                        Next
                                    End If
                                    dt2 = Nothing
                                End If
                                'PUESTO
                                If QuitaComilla(Trim(Mid(oSheet.Cells(i, 9).value, 1, 4))) <> "" And QuitaComilla(Trim(Mid(oSheet.Cells(i, 10).value, 1, 45))) <> "" Then
                                    dt2 = obj.CasConsulta_ExistePuetso("", QuitaComilla(Trim(Mid(oSheet.Cells(i, 9).value, 1, 4))), "2",Session("Ruta_Emp"))
                                    If dt2.Rows.Count = 0 Then
                                        obj.InsUpd_Puesto(Puesto, QuitaComilla((Mid(oSheet.Cells(i, 10).value, 1, 45))), QuitaComilla((Mid(oSheet.Cells(i, 9).value, 1, 4))), "3",Session("Ruta_Emp"))
                                    Else
                                        For Each dr2 As Data.DataRow In dt2.Rows
                                            Puesto = Nz(dr2("PUESTO_CODIGO"))
                                            If Nu(dr2("PUESTO_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 10).value, 1, 45))) Then
                                                obj.InsUpd_Puesto(Puesto, QuitaComilla((Mid(oSheet.Cells(i, 10).value, 1, 45))), QuitaComilla((Mid(oSheet.Cells(i, 9).value, 1, 4))), "4",Session("Ruta_Emp"))
                                            End If
                                        Next
                                    End If
                                    dt2 = Nothing
                                End If
                                'TERRITORIO
                                If QuitaComilla(Trim(Mid(oSheet.Cells(i, 3).value, 1, 4))) <> "" And QuitaComilla(Trim(Mid(oSheet.Cells(i, 4).value, 1, 45))) <> "" Then
                                    dt2 = obj.CasConsulta_ExisteTerritorio(QuitaComilla(Trim(Mid(oSheet.Cells(i, 3).value, 1, 4))),Session("Ruta_Emp"))
                                    If dt2.Rows.Count = 0 Then
                                        obj.InsUpd_Territorio(Territorio, QuitaComilla((Mid(oSheet.Cells(i, 3).value, 1, 4))), QuitaComilla((Mid(oSheet.Cells(i, 4).value, 1, 45))), "1",Session("Ruta_Emp"))
                                    Else
                                        For Each dr2 As Data.DataRow In dt2.Rows
                                            Territorio = Nz(dr2("TERRI_CODIGO"))
                                            If Nu(dr2("TERRI_NOMBRE")) <> QuitaComilla(Trim(Mid(oSheet.Cells(i, 4).value, 1, 45))) Then
                                                obj.InsUpd_Territorio(Territorio, QuitaComilla((Mid(oSheet.Cells(i, 3).value, 1, 4))), QuitaComilla((Mid(oSheet.Cells(i, 4).value, 1, 45))), "2",Session("Ruta_Emp"))
                                            End If
                                        Next
                                    End If
                                    dt2 = Nothing
                                End If
                                pCodBanca = Nz(Trim(Mid(oSheet.Cells(i, 5).value, 1, 4)))
                                pAntiguedad = Nz(Trim(Mid(oSheet.Cells(i, 11).value, 1, 3)))
                                obj.InsUpd_Personas(0, QuitaComilla(Trim(Mid(oSheet.Cells(i, 6).value, 1, 7))), QuitaComilla(Trim(Mid(oSheet.Cells(i, 7).value, 1, 45))), QuitaComilla(Trim(Mid(oSheet.Cells(i, 8).value, 1, 45))), Oficina, Puesto, QuitaComilla(Trim(Mid(oSheet.Cells(i, 12).value, 1, 9))), QuitaComilla(Trim(Mid(oSheet.Cells(i, 13).value, 1, 9))), Trim(Mid(oSheet.Cells(i, 14).value, 1, 45)), pCodBanca, "", pAntiguedad, Territorio, "1",Session("Ruta_Emp"))
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
