Imports WebGestor
Imports System.Data
Partial Class Default4
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not Page.IsPostBack Then 'solo carga una ve
            Call CargarDatos()
            If Flex.Rows.Count = 1 Then
                NomServer = Flex.Rows(0).Cells(2).Text
                Ruta_GrEmp = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False; POOLING=FALSE;initial catalog=BDGrupoEmpresas"
                Ruta_Ng = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDSeguridadGrupoEmps"
                strConexion = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDGEmpresa3TC"
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    Private Sub CargarDatos()
        Using dataset As Data.DataSet = New Data.DataSet()
            dataset.ReadXml(Server.MapPath("XMLServidor.xml"))
            Flex.DataSource = dataset
            Flex.DataBind()
        End Using
    End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Entrar" Then
            NomServer = Flex.Rows(Index).Cells(2).Text
            Session("NombreServidor") = Flex.Rows(Index).Cells(1).Text
            Session("SiglaEmpresa") = Flex.Rows(Index).Cells(3).Text
            Ruta_GrEmp = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False; POOLING=FALSE;initial catalog=BDGrupoEmpresas"
            Ruta_Ng = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDSeguridadGrupoEmps"
            strConexion = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDGEmpresa" & Session("SiglaEmpresa")
            Response.Redirect("Default.aspx")
        End If
    End Sub
End Class
