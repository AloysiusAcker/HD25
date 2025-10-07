
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports a = System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Collections
Imports System.Net.Sockets
Imports System.Diagnostics

'http:'www.csharphelp.com/archives2/archive450.html
Public Class Pop3Exception
    ' TODO: *** Comprobar si es Inherits o Implements ***
    Inherits System.ApplicationException

    Public Sub New(str As String)
        MyBase.New(str)

    End Sub

End Class

Public Class Pop3Message
    Public number As Long
    Public bytes As Long
    Public retrieved As Boolean
    Public message As String
End Class



'Public Class Pop3
'    ' TODO: *** Comprobar si es Inherits o Implements ***
'    Inherits System.Net.Sockets.TcpClient

'    Public Sub Connect(server As String, username As String, password As String)
'        Dim message As String
'        Dim response As String
'        Connect(server, 110)
'        response = response()
'        If response.Substring(0, 3) <> " & OK" Then
'            Throw New Pop3Exception(response)
'        End If
'        message = "USER " & username & "" & vbCrLf & ""
'        Write(message)
'        response = response()
'        If response.Substring(0, 3) <> " & OK" Then
'            Throw New Pop3Exception(response)
'        End If
'        message = "PASS " & password & "" & vbCrLf & ""
'        Write(message)
'        response = response()
'        If response.Substring(0, 3) <> " & OK" Then
'            Throw New Pop3Exception(response)
'        End If

'    End Sub
'    Public Sub Disconnect()
'        Dim message As String
'        Dim response As String
'        message = "QUIT" & vbCrLf & ""
'        Write(message)
'        response = response()
'        If response.Substring(0, 3) <> " & OK" Then
'            Throw New Pop3Exception(response)
'        End If

'    End Sub

'    Public Function List() As ArrayList
'        Dim message As String
'        Dim response As String
'        Dim retval As New ArrayList()
'        message = "LIST" & vbCrLf & ""
'        Write(message)
'        response = response()
'        If response.Substring(0, 3) <> " & OK" Then
'            Throw New Pop3Exception(response)
'        End If

'        While True
'            response = response()
'            If response = "." & vbCrLf & "" Then
'                Return retval
'            Else
'                Dim msg As New Pop3Message()
'                Char() seps =
'                Dim "c As "
'            End If

'            String() values = response.Split(seps)
'            msg.number = Int32.Parse(values(0))
'            msg.bytes = Int32.Parse(values(1))
'            msg.retrieved = False
'            retval.Add(msg)
'            Continue While
'        End While
'    End Function

'End Class


'Public Function Retrieve(rhs As Pop3Message) As Pop3Message
'    Dim message As String
'    Dim response As String
'    Dim msg As New Pop3Message()
'    msg.bytes = rhs.bytes
'    msg.number = rhs.number
'    message = "RETR " & rhs.number & "" & vbCrLf & ""
'    Write(message)
'    response = response()
'    If response.Substring(0, 3) <> " & OK" Then
'        Throw New Pop3Exception(response)
'    End If
'    msg.retrieved = True
'    While True
'        response = response()
'        If response = "." & vbCrLf & "" Then
'            break
'        Else
'            msg.message += response
'        End If

'    End While
'End Function
'Return msg

'Public Sub Delete(rhs As Pop3Message)
'    Dim message As String
'    Dim response As String
'    message = "DELE " & rhs.number & "" & vbCrLf & ""
'    Write(message)
'    response = response()
'    If response.Substring(0, 3) <> " & OK" Then
'        Throw New Pop3Exception(response)
'    End If

'End Sub

'Private Sub Write(message As String)

'    Dim en As System.Text.ASCIIEncoding = New System.Text.ASCIIEncoding()
'    Dim WriteBuffer(0 To 1024 - 1) As Byte
'    WriteBuffer = en.GetBytes(message)
'    Dim stream As NetworkStream = GetStream()
'    stream.Write(WriteBuffer, 0, WriteBuffer.Length)
'    Debug.WriteLine("WRITE:" & message)
'End Sub


'Private Function Response() As String

'    Dim enc As System.Text.ASCIIEncoding = New System.Text.ASCIIEncoding()
'    Dim serverbuff(0 To 1024 - 1) As Byte
'    Dim stream As NetworkStream = GetStream()
'    Dim count As Integer = 0
'    While True

'        Dim buff(0 To 2 - 1) As Byte
'        Dim bytes As Integer = stream.Read(buff, 0, 1)
'        If bytes = 1 Then
'            serverbuff(count) = buff(0)
'            count += 1
'            If buff(0) = "·"c Then
'                break
'            End If

'        Else
'            break
'        End If

'    End While
'End Function

'Dim retval As String = enc.GetString(serverbuff, 0, count)
'Debug.WriteLine("READ:" & retval)
'Return retval