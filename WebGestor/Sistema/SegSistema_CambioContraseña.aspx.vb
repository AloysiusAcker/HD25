Imports WebGestor
Imports System.Data.SqlClient
Partial Class Sistema_SegSistema_CambioContraseña
    Inherits System.Web.UI.Page

    Private Sub Guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guardar.Click
        lblErrorData.Text = ""
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim Ok As String = ""
        If Valida_Cadena(txtClaveN1.Text) = False Then
            lblErrorData.Text = "La nueva clave contiene un caracter no válido."
            Exit Sub
        End If
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "UPDATE TBUSUARI SET USUARI_PASS='" & Trim(txtClaveN1.Text) & "' WHERE USUARI_CODIGO='" & User.Identity.Name & "' AND USUARI_PASS='" & Trim(txtClaveAnt.Text) & "'"
            If cmdSql.ExecuteNonQuery() <= 0 Then
                lblErrorData.Text = "La Contraseña anterior no es correcta. Reintente!"
            Else
                Ok = "1"
            End If
        Catch Ex As SqlException
            lblErrorData.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            lblErrorData.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
        If Ok = "1" Then
            Session("PageMensaje") = "2"
            Session("Mensaje") = "La Contraseña se ha actualizado correctamente !!!"
            Response.Redirect("Segsistema_MensajeOk.aspx")
        End If
    End Sub
    Private Function Valida_Cadena(ByVal Cadena As String) As Boolean
        Dim i As Integer
        Dim Cad As String = Cadena
        Valida_Cadena = True
        For i = 1 To Len(Cadena)
            If Mid(Cadena, i, 1) = "'" Then
                Valida_Cadena = False
                Exit Function
            End If
        Next
    End Function
End Class
