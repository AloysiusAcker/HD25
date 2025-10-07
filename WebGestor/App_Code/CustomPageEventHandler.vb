Imports Microsoft.VisualBasic
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class CustomPageEventHandler
    Inherits PdfPageEventHelper
    Private headerX As Single = 36
    Private headerY As Single = 800

    ' Coordenadas del pie de página
    Private footerX As Single = 36
    Private footerY As Single = 20

    ' Variable que deseas pasar a OnEndPage
    Private _miVariable As String

    ' Constructor que inicializa la variable
    Public Sub New(miVariable As String)
        _miVariable = miVariable
    End Sub

    ' Evento que se dispara al terminar de escribir en una página
    Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
        ' Aquí puedes agregar tu pie de página en cada página
        Dim headerTable As New PdfPTable(1)
        headerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
        headerTable.DefaultCell.Border = PdfPCell.NO_BORDER
        headerTable.AddCell(New Phrase("INFORME INVENTARIADO FÍSICO " & UCase(_miVariable), New Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC, BaseColor.GRAY)))
        headerTable.WriteSelectedRows(0, -1, 36, 830, writer.DirectContent)

        ' Usar las coordenadas definidas
        Dim footerTable As New PdfPTable(1)
        footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
        footerTable.DefaultCell.Border = PdfPCell.NO_BORDER
        footerTable.AddCell(New Phrase("TECNOLOGÍAS Y SISTEMAS	                                                                                                                                  " & UCase(_miVariable), New Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC, BaseColor.GRAY)))
        footerTable.WriteSelectedRows(0, -1, 36, 20, writer.DirectContent)

    End Sub
End Class
