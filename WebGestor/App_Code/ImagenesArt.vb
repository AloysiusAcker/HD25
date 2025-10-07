Imports Microsoft.VisualBasic

Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Imports System.Data.SqlClient
Imports System.Configuration
Imports WebGestor
Public Class ImagenesArt
    Private Sub New()
    End Sub

    Public Shared Sub GuardarImagen(nombrearchivo As String, imagen As String(), ByVal psConexion As String)

        Using conn As New SqlConnection(psConexion)
            conn.Open()
            Dim CmdGlobal As New SqlCommand
            Dim Rs As SqlDataReader
            Dim psCodigo As Integer = 1
            CmdGlobal.Connection = conn
            CmdGlobal.CommandText = "SELECT MAX(ART_CODIGO) FROM TBINV_ARTICULOS"
            Rs = CmdGlobal.ExecuteReader

            If Rs.HasRows Then
                While Rs.Read
                    psCodigo = CInt(Rs(0)) + 1
                End While



            Else
                MsgBox("La imagen seleccionada no existe")
            End If
            Rs.Close()

            Dim query As String = "INSERT INTO TBINV_ARTICULOS (empresa_codigo,ART_IMG_NOM, ART_IMG, ART_CODIGO, art_sys_est, art_tipo , art_clasificacion) " &
                                    "VALUES ('0001',@name, @imagen," & psCodigo & ",'0','88',73)"

            Dim cmd As New SqlCommand(query, conn)


            cmd.Parameters.AddWithValue("@name", nombrearchivo)

            Dim imageParam As SqlParameter = cmd.Parameters.Add("@imagen", System.Data.SqlDbType.Image)



            cmd.ExecuteNonQuery()
        End Using

    End Sub

    Public Shared Function GetImagenList(ByVal psConexion As String) As List(Of Imagenes)
        Dim lista As New List(Of Imagenes)()

        Using conn As New SqlConnection(psConexion)
            conn.Open()

            Dim query As String = "SELECT ART_CODIGO, ART_IMG_NOM " &
                                    "FROM TBINV_ARTICULOS"

            Dim cmd As New SqlCommand(query, conn)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                Dim img As New Imagenes(Convert.ToInt32(reader("ART_CODIGO")), Convert.ToString(reader("ART_IMG_NOM")))
                lista.Add(img)

            End While
        End Using

        Return lista

    End Function

    Public Shared Function GetImagenById(ART_CODIGO As Integer, ByVal psConexion As String) As Imagenes
        Dim img As Imagenes = Nothing

        Using conn As New SqlConnection(psConexion)
            conn.Open()

            Dim query As String = "SELECT ART_CODIGO, ART_IMG_NOM, ART_IMG AS Imagen " &
                                    "FROM TBINV_ARTICULOS " &
                                    "WHERE ART_CODIGO = @ART_CODIGO"

            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ART_CODIGO", ART_CODIGO)

            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then



                img = New Imagenes(reader("ART_CODIGO"), Nu(reader("ART_IMG_NOM")))
                If Not IsDBNull(reader("Imagen")) Then

                    img.Imagen = DirectCast(reader("Imagen"), Byte())
                End If

            End If


        End Using



        Return img

    End Function


End Class

Public Class Imagenes
    Public Sub New(ART_CODIGO As Integer, ART_IMG_NOM As String)



        Me.ART_CODIGO = ART_CODIGO

        Me.ART_IMG_NOM = ART_IMG_NOM

    End Sub

    Public Property ART_CODIGO() As Integer
        Get
            Return m_ART_CODIGO
        End Get
        Set
            m_ART_CODIGO = Value
        End Set
    End Property
    Private m_ART_CODIGO As Integer


    Public Property ART_IMG_NOM() As String
        Get
            Return m_img_nom
        End Get
        Set
            m_img_nom = Value
        End Set
    End Property
    Private m_img_nom As String


    Public Property ART_DESCRIPCION() As String
        Get
            Return m_ART_DESCRIPCION
        End Get
        Set
            m_ART_DESCRIPCION = Value
        End Set
    End Property
    Private m_ART_DESCRIPCION As String
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
