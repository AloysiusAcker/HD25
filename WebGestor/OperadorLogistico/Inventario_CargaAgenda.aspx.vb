Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Imports System.IO
Imports System.Reflection
Partial Class Inventario_CargaAgenda
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

                    For i = 4 To oSheet.Rows.Count
                        If oSheet.Cells(i, 1).value = "" Then
                            Session("UnaVez") = "2"
                            Exit For
                        Else
                            If oSheet.Cells(i, 6).value <> "" And oSheet.Cells(i, 19).value <> "" Then
                                objInsUpdDel.Ins_Telefonica_Agenda(Session("Ruta_Emp"), Session("CodEmpresa"), FechaActual, QuitaComilla(Trim(oSheet.Cells(i, 19).value)), QuitaComilla(Trim(oSheet.Cells(i, 5).value)), QuitaComilla(Trim(oSheet.Cells(i, 6).value)), _
                                                          QuitaComilla(Trim(oSheet.Cells(i, 7).value)), QuitaComilla(Trim(oSheet.Cells(i, 8).value)), QuitaComilla(Trim(oSheet.Cells(i, 9).value)), QuitaComilla(Trim(oSheet.Cells(i, 10).value)), _
                                                          QuitaComilla(Trim(oSheet.Cells(i, 11).value)), QuitaComilla(Trim(oSheet.Cells(i, 14).value)), QuitaComilla(Trim(oSheet.Cells(i, 16).value)), QuitaComilla(Trim(oSheet.Cells(i, 21).value)), _
                                                          QuitaComilla(Trim(oSheet.Cells(i, 22).value)), QuitaComilla(Trim(oSheet.Cells(i, 23).value)), QuitaComilla(Trim(oSheet.Cells(i, 24).value)), QuitaComilla(Trim(oSheet.Cells(i, 25).value)), _
                                                          QuitaComilla(Trim(oSheet.Cells(i, 26).value)), QuitaComilla(Trim(oSheet.Cells(i, 27).value)), QuitaComilla(Trim(oSheet.Cells(i, 28).value)), QuitaComilla(Trim(oSheet.Cells(i, 29).value)), QuitaComilla(Trim(oSheet.Cells(i, 30).value)), QuitaComilla(Trim(oSheet.Cells(i, 31).value)))

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
                    lblMensajeExportada.Text = "La actualización de la agenda terminó."
                    Call Actualizar_Pedidos()
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
    Private Sub Actualizar_Pedidos()
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim dt3 As DataTable
        Dim obj As New clsInv_Listados
        Dim objInsUpdDel As New clsInspeccion_InsUpdDel
        Dim objInsUpdDelInv As New clsInv_InsUpdDel
        Dim pDia As String : pDia = ""
        Dim pCodPersona As Double : pCodPersona = 0
        Try
            dt = obj.Lista_AgendaTelef(Session("Ruta_Emp"), Session("CodEmpresa"))
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows

                    dt3 = obj.Devuelve_Telefonica_Personas(Session("Ruta_Emp"), Session("CodEmpresa"), _
                                                 pCodPersona, Nu(drMenuItem("ARCHIVO_DOCTIPO")), Nu(drMenuItem("ARCHIVO_DOCNRO")), _
                                                 Nu(drMenuItem("ARCHIVO_CLIENTE")), Nu(drMenuItem("ARCHIVO_DIRECCION")), _
                                                 Nu(drMenuItem("ARCHIVO_CIUDADZONAL")), Nu(drMenuItem("ARCHIVO_DISTRITO")), _
                                                 Nu(drMenuItem("ARCHIVO_TELEFONO_FIJO")), Nu(drMenuItem("ARCHIVO_TELEFONO_CONTACTO")), _
                                                 Nu(drMenuItem("ARCHIVO_PERSONA_CONTACTO")), Nu(drMenuItem("ARCHIVO_REFERENCIA")), _
                                                 Nu(drMenuItem("ARCHIVO_CELULAR_CONTACTO")))
                    If dt3.Rows.Count = 1 Then
                        For Each drCodPer As Data.DataRow In dt3.Rows
                            pCodPersona = Nz(drCodPer("PER_CODIGO"))
                        Next
                    Else
                        dt2 = obj.Ultima_CodPersona(Session("Ruta_Emp"))
                        If dt2.Rows.Count > 0 Then
                            For Each drUlPer As Data.DataRow In dt2.Rows
                                pCodPersona = Nz(drUlPer("CODIGO")) + 1
                            Next
                        Else
                            pCodPersona = 1
                        End If
                        dt2 = Nothing
                        'insertar persona
                        objInsUpdDelInv.Ins_Telefonica_Personas(Session("Ruta_Emp"), Session("CodEmpresa"), _
                                                     pCodPersona, Nu(drMenuItem("ARCHIVO_DOCTIPO")), Nu(drMenuItem("ARCHIVO_DOCNRO")), _
                                                     Nu(drMenuItem("ARCHIVO_CLIENTE")), Nu(drMenuItem("ARCHIVO_DIRECCION")), _
                                                     Nu(drMenuItem("ARCHIVO_CIUDADZONAL")), Nu(drMenuItem("ARCHIVO_DISTRITO")), _
                                                     Nu(drMenuItem("ARCHIVO_TELEFONO_FIJO")), Nu(drMenuItem("ARCHIVO_TELEFONO_CONTACTO")), _
                                                     Nu(drMenuItem("ARCHIVO_PERSONA_CONTACTO")), Nu(drMenuItem("ARCHIVO_REFERENCIA")), _
                                                     Nu(drMenuItem("ARCHIVO_CELULAR_CONTACTO")))
                    End If
                    dt3 = Nothing
                    pDia = Left(Nu(drMenuItem("ARCHIVO_FECHA_AGENDA")), 1)
                    If Len(pDia) = 1 Then pDia = "0" + pDia Else pDia = pDia
                    objInsUpdDelInv.Ins_Telefonica_Pedido(Session("Ruta_Emp"), Session("CodEmpresa"), _
                                               pCodPersona, Nu(drMenuItem("ARCHIVO_NROPEDIDO")), _
                                               Right(Nu(drMenuItem("ARCHIVO_FECHA_AGENDA")), 4) & pDia & Mid(Nu(drMenuItem("ARCHIVO_FECHA_AGENDA")), 3, 2), _
                                               Nu(drMenuItem("ARCHIVO_PERSONA_CONTACTO")), Nu(drMenuItem("ARCHIVO_REFERENCIA")), Nu(drMenuItem("ARCHIVO_TELEFONO_FIJO")), Nu(drMenuItem("ARCHIVO_TELEFONO_CONTACTO")), _
                                               Nz(drMenuItem("cantEq")), Nu(drMenuItem("ARCHIVO_FRANJAHORARIA")), HttpContext.Current.User.Identity.Name)
                    'obj.Upd_TelefonicaDatos(Session("Ruta_Emp"), Session("CodEmpresa"), Nu(drMenuItem("ARCHIVO_NROPEDIDO")))
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
    End Sub
End Class
