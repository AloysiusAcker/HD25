Imports System
Imports System.IO
Imports System.Text
Namespace Devjoker
    Public Class Principal
        Public Shared Sub Main()
            Dim ge As New GenExcell()
            ge.DoExcell("nuevo_file.html")
            ge.DoExcell("nuevo_file.xls")
        End Sub
    End Class
    Friend Class GenExcell
        Dim w As StreamWriter
        Public Function DoExcell(ByVal ruta As String) As Integer
            Dim fs As New FileStream(ruta, FileMode.Create, FileAccess.ReadWrite)
            w = New StreamWriter(fs)
            EscribeCabecera()
            For i As Integer = 0 To 20 - 1
                EscribeLinea(i)
            Next
            EscribePiePagina()
            w.Close()
            Return 0
        End Function
        Public Sub EscribeCabecera()
            Dim html As New StringBuilder()
            html.Append("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">")
            html.Append("<html>")
            html.Append("  <head>")
            html.Append("<title>www.devjoker.com</title>")
            html.Append("<meta http-equiv=""Content-Type""content=""text/html; charset=UTF-8"" />")
            html.Append("  </head>")
            html.Append("<body>")
            html.Append("<p>")
            html.Append("<table>")
            html.Append("<tr style=""font-weight: bold;font-size: 12px;color: white;"">")
            html.Append("<td></td><td bgcolor=""Blue"">Titulo de la tabla:</td>")
            html.Append("<td bgcolor=""Blue"">Iteración:</td>")
            html.Append("</tr>")
            w.Write(html.ToString())
        End Sub
        Public Sub EscribeLinea(ByVal i As Integer)
            Dim bgColor As String = "", fontColor = ""
            If i Mod 2 = 0 Then
                bgColor = " bgcolor=""LightBlue"" "
                fontColor = " style=""font-size: 10px;color: white;"" "
            End If
            w.Write("<tr ><td ></td><td {2} {3}>Titulo de la celda:{0}</td><td {2} {3}>Valor de la celda: {1}</td></tr>", i.ToString(), i.ToString(), bgColor, fontColor)
        End Sub
        Public Sub EscribePiePagina()
            Dim html As New StringBuilder()
            html.Append("  </table>")
            html.Append("</p>")
            html.Append(" </body>")
            html.Append("</html>")
            w.Write(html.ToString())
        End Sub
    End Class
End Namespace



