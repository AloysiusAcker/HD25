
Imports Microsoft.VisualBasic

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Imports System.Data.SqlClient
Imports System.Configuration
Imports WebGestor
Public Class ImagenesGuia
    Private Sub New()
    End Sub

    Public Shared Sub GuardarImagen(nombrearchivo As String, imagen As String(), ByVal psConexion As String)

        Using conn As New SqlConnection(psConexion)
            conn.Open()
            Dim CmdGlobal As New SqlCommand
            Dim Rs As SqlDataReader
            Dim psCodigo As Integer = 1
            CmdGlobal.Connection = conn
            CmdGlobal.CommandText = "SELECT MAX(GUIREM_CODIGO) FROM TBINV_GUIA_REMISION_0001"
            Rs = CmdGlobal.ExecuteReader

            If Rs.HasRows Then
                While Rs.Read
                    psCodigo = CInt(Rs(0)) + 1
                End While
            Else
                MsgBox("La imagen seleccionada no existe")
            End If
            Rs.Close()

            Dim query As String = "UPDATE TBINV_GUIA_REMISION_0001 SET GUIA_IMG_NOMBRE = @name , GUIA_IMG = @imagen " &
                                  "WHERE empresa_codigo = '0001' AND GUIREM_CODIGO = " & psCodigo

            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@name", nombrearchivo)
            Dim imageParam As SqlParameter = cmd.Parameters.Add("@imagen", System.Data.SqlDbType.Image)
            cmd.ExecuteNonQuery()

        End Using

    End Sub

    Public Shared Function GetImagenList(ByVal psConexion As String) As List(Of ImgGuia)
        Dim lista As New List(Of ImgGuia)()

        Using conn As New SqlConnection(psConexion)
            conn.Open()

            Dim query As String = "SELECT GUIREM_CODIGO, GUIA_IMG_NOMBRE " &
                                    "FROM TBINV_GUIA_REMISION_0001"

            Dim cmd As New SqlCommand(query, conn)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim img As New ImgGuia(Convert.ToInt32(reader("GUIREM_CODIGO")), Convert.ToString(reader("GUIA_IMG_NOMBRE")))
                lista.Add(img)

            End While
        End Using

        Return lista

    End Function

    Public Shared Function GetImagenById(GUIREM_CODIGO As Integer, ByVal psConexion As String) As ImgGuia
        Dim img As ImgGuia = Nothing

        Using conn As New SqlConnection(psConexion)
            conn.Open()

            Dim query As String = "SELECT GUIREM_CODIGO, GUIA_IMG_NOMBRE, GUIA_IMG AS Imagen " &
                                    "FROM TBINV_GUIA_REMISION_0001 " &
                                    "WHERE GUIREM_CODIGO = @GUIREM_CODIGO"

            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@GUIREM_CODIGO", GUIREM_CODIGO)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then



                img = New ImgGuia(reader("GUIREM_CODIGO"), Nu(reader("GUIA_IMG_NOMBRE")))
                If Not IsDBNull(reader("Imagen")) Then

                    img.Imagen = DirectCast(reader("Imagen"), Byte())
                End If

            End If


        End Using



        Return img

    End Function


End Class

Public Class ImgGuia
    Public Sub New(GUIREM_CODIGO As Integer, GUIA_IMG_NOMBRE As String)



        Me.GUIREM_CODIGO = GUIREM_CODIGO

        Me.GUIA_IMG_NOMBRE = GUIA_IMG_NOMBRE

    End Sub

    Public Property GUIREM_CODIGO() As Integer
        Get
            Return m_GUIREM_CODIGO
        End Get
        Set
            m_GUIREM_CODIGO = Value
        End Set
    End Property
    Private m_GUIREM_CODIGO As Integer


    Public Property GUIA_IMG_NOMBRE() As String
        Get
            Return m_img_nom
        End Get
        Set
            m_img_nom = Value
        End Set
    End Property
    Private m_img_nom As String

    Public Property Imagen() As Byte()
        Get
            Return m_Imagen
        End Get
        Set
            m_Imagen = Value
        End Set
    End Property
    Private m_Imagen As Byte()

End Class