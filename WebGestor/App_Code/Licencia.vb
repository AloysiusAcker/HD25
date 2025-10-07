Imports Microsoft.VisualBasic
Imports System.Data
Namespace WebGestor
    Public Module Licencia
        Function Nro_Licencias(ByVal piNro As Integer) As String
            Dim msn As String : msn = ""
            If piNro >= 60 Then msn = "Ha superado el nro de Licencias"
            Nro_Licencias = msn
        End Function
    End Module
End Namespace
