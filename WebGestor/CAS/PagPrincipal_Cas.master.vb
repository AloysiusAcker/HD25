Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class PagPrincipal_Cas
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'lblFecha.InnerText = Format(CDate(FormatoFecha(FechaActual())), "dddd, dd 'de' MMMM 'de' yyyy")
        If Session("UserFirmado") = "N" Or Session("UserFirmado") Is Nothing Then
            Inicio.Visible = True
            Cerrar.Visible = False
            btnCambioPass.Visible = False
        Else
            Cerrar.Visible = True
            Inicio.Visible = False
            btnCambioPass.Visible = True
            lblAgrup.InnerText = IIf(Session("NombreGrupoEmpresa") <> "", Session("NombreGrupoEmpresa") & " - " & Session("NombreEmpresa") & " - " & Session("NombreServidor"), "")
        End If
    End Sub

End Class