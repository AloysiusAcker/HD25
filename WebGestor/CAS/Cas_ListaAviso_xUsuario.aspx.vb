Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class CAS_Cas_ListaAviso_xUsuario
    Inherits System.Web.UI.Page

    Dim obj As New ModuloCas
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Call LlenaComboItem("TBOPC333", DdlTipo)
                Call LlenaComboItem("TBOPC334", DdlEstado)
                Call LlenaComboItem("TBOPC335", DdlAviso)
                DdlAviso.SelectedValue = "1"
                Call LLenaComboItemTabEsp(DdlAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
                Call DdlAplicativo_SelectedIndexChanged(sender, e)
                DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
                DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
                DdlProducto.Enabled = False
                DdlSubProd.Enabled = False
                DivAviso.Visible = False
                DivAvisoDet.Visible = False
                Call ListaAvisos()
            Catch Ex As SqlException
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Private Sub ListaAvisos()
        Dim pEstUsuario As String = "%"
        Dim pEstAviso As String = "%"
        Dim pTipoAviso As String = "%"
        Dim dt As New DataTable
        pEstUsuario = DdlAviso.SelectedValue.Trim
        If DdlTipo.SelectedValue.Trim <> "< Seleccionar >" Then pTipoAviso = DdlTipo.SelectedValue.Trim
        If DdlEstado.SelectedValue.Trim <> "< Seleccionar >" Then pEstAviso = DdlEstado.SelectedValue.Trim
        LblError.Text = ""
        Dim psCodAplicativo As Double = 0
        Dim psCodProducto As Double = 0
        Dim psCodSubProd As Double = 0
        If DdlAplicativo.SelectedValue <> "< Seleccionar >" Then psCodAplicativo = DdlAplicativo.SelectedValue
        If DdlProducto.SelectedValue <> "< Seleccionar >" Then psCodProducto = DdlProducto.SelectedValue
        If DdlSubProd.SelectedValue <> "< Seleccionar >" Then psCodSubProd = DdlSubProd.SelectedValue
        Try
            Flex.DataSource = obj.CasLista_Aviso(Session("User"), pEstUsuario, pTipoAviso, pEstAviso, "2", Session("Ruta_Emp"), psCodAplicativo, psCodProducto, psCodSubProd, Session("CodEmpresa"))
            Flex.DataBind()
            DivAviso.Visible = True
            dt = Nothing
        Catch Ex As SqlException
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    'Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
    '    Call ListaAvisos()
    'End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblError.Text = ""
        Dim dtlistado As New DataTable
        Dim psNroAviso As Double = 0
        Dim dt As New DataTable
        Dim psRuta As String = ""
        Dim pCodigo As Double = 0
        Dim psEtiqueta As String = "Ver"
        Dim Fila As GridViewRow
        If e.CommandName = "Ok" Then
            psNroAviso = Nz(Flex.Rows(Index).Cells(1).Text.Trim)
            Call ListaAvisos()
            dt = obj.CasConsulta_ExisteAviso(Session("User"), Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                obj.InsUpd_Aviso(psNroAviso, "", "", "", Session("User"), "3", Session("Ruta_Emp"), "", 0, 0, 0, Session("CodEmpresa"))
            End If
            dt = Nothing
            DivAvisoDet.Visible = True
            FlexDetalle.DataSource = obj.CasLista_xAviso(Session("User"), psNroAviso, Session("Ruta_Emp"))
            FlexDetalle.DataBind()
            dt = obj.ListaArchivo_xAviso(Session("CodEmpresa"), Session("Ruta_Emp"), psNroAviso)
            gvArchivo.DataSource = dt
            gvArchivo.DataBind()
            psRuta = "Temas_" & Session("SiglaGrupoEmpresa")
            For i = 0 To gvArchivo.Rows.Count - 1
                pCodigo = gvArchivo.Rows(i).Cells(2).Text.Trim
                dtListado = obj.Aviso_MuestraArchivo_xCodigo(pCodigo, Session("Ruta_Emp"), Session("codEmpresa"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = gvArchivo.Rows(i)
                        Dim lbl As HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='" & psRuta & "\" & Nu(drMenuItem("ARCHIVO")) & "'TARGET='_blank'>" & psEtiqueta & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        End If
    End Sub
    Private Sub DdlAviso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAviso.SelectedIndexChanged
        Dim DT As New DataTable
        DT = Nothing
        FlexDetalle.DataSource = dt
        FlexDetalle.DataBind()
        gvArchivo.DataSource = dt
        gvArchivo.DataBind()
        Call ListaAvisos()
    End Sub
    Private Sub DdlEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlEstado.SelectedIndexChanged

        Dim DT As New DataTable
        DT = Nothing
        FlexDetalle.DataSource = dt
        FlexDetalle.DataBind()
        gvArchivo.DataSource = DT
        gvArchivo.DataBind()
        Call ListaAvisos()
    End Sub
    Private Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged

        Dim DT As New DataTable
        DT = Nothing
        FlexDetalle.DataSource = dt
        FlexDetalle.DataBind()
        gvArchivo.DataSource = DT
        gvArchivo.DataBind()
        Call ListaAvisos()
    End Sub

    Private Sub DdlAplicativo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAplicativo.SelectedIndexChanged
        LblError.Visible = False
        DdlProducto.Items.Clear()
        DdlSubProd.Items.Clear()
        DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
        DdlProducto.Enabled = False
        DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        DdlSubProd.Enabled = False
        Call LLenaComboItemTabEsp(DdlProducto, DdlAplicativo.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If DdlAplicativo.SelectedValue = "< Seleccionar >" Then
            DdlProducto.Enabled = False
            DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
            DdlSubProd.Enabled = False
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        Else
            DdlProducto.Enabled = True
            DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
            DdlSubProd.Enabled = False
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        End If
        Dim DT As New DataTable
        DT = Nothing
        FlexDetalle.DataSource = dt
        FlexDetalle.DataBind()
        gvArchivo.DataSource = DT
        gvArchivo.DataBind()
        Call ListaAvisos()
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub DdlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProducto.SelectedIndexChanged
        LblError.Visible = False
        DdlSubProd.Items.Clear()
        DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        DdlSubProd.Enabled = False
        If DdlProducto.SelectedIndex = -1 Or DdlProducto.Items.Count = 0 Then Exit Sub
        If DdlProducto.Items(DdlProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(DdlSubProd, DdlAplicativo.SelectedValue.Trim, DdlProducto.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If DdlProducto.SelectedValue = "< Seleccionar >" Then
            DdlSubProd.Enabled = False
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        Else
            DdlSubProd.Enabled = True
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        End If
        Dim DT As New DataTable
        DT = Nothing
        FlexDetalle.DataSource = dt
        FlexDetalle.DataBind()
        gvArchivo.DataSource = DT
        gvArchivo.DataBind()
        Call ListaAvisos()
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub DdlSubProd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlSubProd.SelectedIndexChanged

        Dim DT As New DataTable
        DT = Nothing
        FlexDetalle.DataSource = DT
        FlexDetalle.DataBind()
        gvArchivo.DataSource = DT
        gvArchivo.DataBind()
        Call ListaAvisos()
    End Sub
    'Private Sub Flex_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Flex.RowDataBound
    '    Dim imagen As System.Web.UI.WebControls.Image = DirectCast(Flex.FindControl("Image"), System.Web.UI.WebControls.Image)
    '    Dim psNroAviso As Double = 0
    '    Dim dt As New DataTable
    '    'CType(e.Row.Cells(1).Text, Double)
    '    'If psNroAviso <> 0 Then
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        psNroAviso = CType(Nz(e.Row.Cells(1).Text), Double)
    '        dt = obj.CasLista_xAviso(Session("User"), psNroAviso, Session("Ruta_Emp"))
    '        If dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                If Nu(dr("ESTADO")) = "1" Then imagen.ImageUrl = "~/WebGestor/Icono/minus.png"
    '                If Nu(dr("ESTADO")) = "2" Then imagen.ImageUrl = "~/WebGestor/Icono/ok20.png"
    '            Next
    '        End If
    '    End If
    'End Sub
End Class
