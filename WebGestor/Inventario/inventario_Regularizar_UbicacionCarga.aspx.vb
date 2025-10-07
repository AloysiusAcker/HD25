Imports System.Data
Imports WebGestor
Partial Class Inventario_inventario_Regularizar_UbicacionCarga
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ''
        End If
    End Sub
    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New clsInv_Listados
        Dim objLogis As New clsLogis_Listado
        Dim dt As New DataTable

        Dim codigoalm As Double = 0
        Dim codigo As String = BuscarCodigo.Value.ToString
        Dim descripcion As String = BuscarDescripcion.Value.ToString

        If TituloPopup.Text = "Búsqueda Almacén" Then
            codigoalm = Nz(BuscarCodigo.Value.ToString)
            dt = obj.Lista_BusquedaAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), codigoalm, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
            dt = obj.Lista_BusquedaCentroCosto(Session("Ruta_Emp"), Session("CodEmpresa"), codigo, descripcion)
        End If

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub
    Protected Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            LblUbicaCodigo.Text = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If

        Limpiar_Cajas_Popup()
    End Sub
    Protected Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

        Limpiar_Cajas_Popup()
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaEquipos.DataSource = dt
        GvListaEquipos.DataBind()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New clsInv_Listados
        Dim dt As New DataTable
        Dim pdUbicaCodigo As Double = 0
        Dim psUbicaTipo As String = ""
        If LblUbicaCodigo.Text <> "" Then pdUbicaCodigo = Nz(LblUbicaCodigo.Text)
        If RBAlmacen.Checked = True Then psUbicaTipo = "1"
        If RBCentroC.Checked = True Then psUbicaTipo = "2"
        If RbTodos.Checked = True Then psUbicaTipo = ""
        dt = obj.Regularizar_ListaEquipos_xUbicacionCarga(Session("Ruta_Emp"), Session("CodEmpresa"), psUbicaTipo, pdUbicaCodigo)
        GvListaEquipos.DataSource = dt
        GvListaEquipos.DataBind()
        If dt.Rows.Count > 0 Then
            LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
        ElseIf dt.Rows.Count = 1 Then
            LblRegistro.Text = "Hay 1 registro."
        Else
            LblRegistro.Text = "Hay 0 registro."
        End If
    End Sub

    Private Sub BtnRegularizar_Click(sender As Object, e As EventArgs) Handles BtnRegularizar.Click

        Dim objProceso As New clsInv_Procesos
        Dim pdCodDestino As Double = 0
        Dim pdCodOrigen As Double = 0
        Dim psTipoOrigen As String = ""
        If LblUbicaCodigo.Text <> "" Then pdCodOrigen = Nz(LblUbicaCodigo.Text)
        If RBAlmacen.Checked = True Then psTipoOrigen = "1"
        If RBCentroC.Checked = True Then psTipoOrigen = "2"
        Dim pdCodSalida As Double = 0
        Dim pdSerieNumerar As Double = 0
        Dim pdCodArt As Double = 0
        Dim pdContador As Double = 0
        LblRegRegularizar.Text = "0"
        Dim obj As New clsInv_Listados
        Dim dt As New DataTable
        dt = obj.Regularizar_Diferencia_CeCosto(Session("Ruta_Emp"), Session("CodEmpresa"))
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                psTipoOrigen = Nu(dr("UBICACT_TIPO"))
                pdCodOrigen = Nz(dr("UBICACT_CODIGO"))
                pdCodDestino = Nz(dr("CODIGO_DESTINO"))
                pdSerieNumerar = Nz(dr("SERIE_NUMERAR"))
                pdCodArt = Nz(dr("ARTICULO_CODIGO"))
                If pdCodDestino > 0 And pdCodOrigen > 0 Then
                    If pdCodOrigen <> pdCodDestino Then
                        pdCodSalida = objProceso.Invnetario_Salida_Ingreso_Automatico(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), psTipoOrigen, "2", pdCodOrigen, pdCodDestino, pdSerieNumerar, pdCodArt)
                    End If
                End If
                pdContador = pdContador + 1
                LblRegRegularizar.Text = pdContador
                UpdatePanel4.Update()
            Next
        End If

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la regularización');", True)
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaEquipos.DataSource = dt
        GvListaEquipos.DataBind()
    End Sub

    Private Sub RbTodos_CheckedChanged(sender As Object, e As EventArgs) Handles RbTodos.CheckedChanged

        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaEquipos.DataSource = dt
        GvListaEquipos.DataBind()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LblRegRegularizar.Text = LblRegRegularizar.Text
    End Sub
End Class
