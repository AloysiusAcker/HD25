Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_Relacion_Equipos_Prestados
    Inherits System.Web.UI.Page

    Dim obj As New clsInv_Listados
    Dim objEmp As New ModuloGeneral
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                DdlEstado.Items.Clear()
                Dim lst0 As New ListItem : lst0.Text = "Enviado" : lst0.Value = 0 : DdlEstado.Items.Add(lst0)
                Dim lst1 As New ListItem : lst1.Text = "Prestado" : lst1.Value = 1 : DdlEstado.Items.Add(lst1)
                Dim lst2 As New ListItem : lst2.Text = "Salida de Devolución" : lst2.Value = 2 : DdlEstado.Items.Add(lst2)
                Dim lst3 As New ListItem : lst3.Text = "Devuelto" : lst3.Value = 3 : DdlEstado.Items.Add(lst3)
                Dim lst4 As New ListItem : lst4.Text = "Anulado" : lst4.Value = 4 : DdlEstado.Items.Add(lst4)
                DdlEstado.Items.Add("< Seleccionar >") : DdlEstado.SelectedValue = "1"
            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos:" & ex.Message & "');", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('ha ocurrido un error en la aplicacion:" & ex.Message & "');", True)
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim pCodSalida As Double = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp")
        Dim psMotivo As String = ""
        Dim psEstado As String = ""
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If
        Dim pdEstado As String = ""
        If DdlEstado.SelectedValue <> "< Seleccionar >" Then psEstado = DdlEstado.SelectedValue
        Dim psSerieNro As String = ""
        Dim psPlacaNro As String = ""
        If TxtSerieNro.Text <> "" Then psSerieNro = TxtSerieNro.Text
        If TxtPlacaNro.Text <> "" Then psPlacaNro = TxtPlacaNro.Text
        Try

            accordion.Visible = True
            Dim dt As New DataTable
            dt = obj.Lista_Prestamos(Session("Ruta_Emp"), Session("CodEmpresa"), "", "", psEstado, psSerieNro, psPlacaNro)
            gvListadoEq.DataSource = dt
            gvListadoEq.DataBind()
            If dt.Rows.Count = 0 Then
                LblRegistro.Text = "No se encontrarón registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro.Text = "Hay 1 registro."
            Else
                LblRegistro.Text = "Se encontrarón " & dt.Rows.Count & " registros."
            End If

            dt = obj.Lista_Prestamos_Accesorios(Session("Ruta_Emp"), Session("CodEmpresa"), "", "", psEstado, "", "", "", "", "", "")
            gvAccesorios.DataSource = dt
            gvAccesorios.DataBind()
            If dt.Rows.Count = 0 Then
                LblRegistrosAcc.Text = "No se encontrarón registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistrosAcc.Text = "Hay 1 registro."
            Else
                LblRegistrosAcc.Text = "Se encontrarón " & dt.Rows.Count & " registros."
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos:" & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('ha ocurrido un error en la aplicacion:" & ex.Message & "');", True)
        Finally
        End Try
    End Sub

    Private Sub DdlOrigen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlOrigen.SelectedIndexChanged
        If DdlOrigen.SelectedValue = "1" Then lblEtq_BusDestino.Text = "Busqueda de Almacén" : Session("TipoOrigen") = "Almacen"
        If DdlOrigen.SelectedValue = "2" Then lblEtq_BusDestino.Text = "Busqueda de Centro de Costo" : Session("TipoOrigen") = "CentroCosto"
        Session("TipoBus") = "Origen"
        TxtOrigCodigo.Text = ""
        txtOrigDescripcion.Text = ""
        LblCodOrigen.Text = ""
    End Sub

    Private Sub DdlDestino_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDestino.SelectedIndexChanged
        If DdlDestino.SelectedValue = "1" Then lblEtq_BusDestino.Text = "Busqueda de Almacén" : Session("TipoDestino") = "Almacen"
        If DdlDestino.SelectedValue = "2" Then lblEtq_BusDestino.Text = "Busqueda de Centro de Costo" : Session("TipoDestino") = "CentroCosto"
        Session("TipoBus") = "Destino"
        TxtDestCodigo.Text = ""
        TxtDestDescripcion.Text = ""
        LblCodDestino.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub

    Private Sub btnUbiCerrar_Click(sender As Object, e As EventArgs) Handles btnUbiCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Private Sub FlexUbicacion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            If Session("TipoBus") = "Origen" Then
                LblCodOrigen.Text = ""
                TxtOrigCodigo.Text = ""
                txtOrigDescripcion.Text = ""
                Session("OrigenCodExt") = FlexUbicacion.Rows(Index).Cells(1).Text
                Session("OrigenDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Session("OrigenCodigo") = FlexUbicacion.Rows(Index).Cells(3).Text
                LblCodOrigen.Text = Session("OrigenCodigo")
                TxtOrigCodigo.Text = Session("OrigenCodExt")
                txtOrigDescripcion.Text = Session("OrigenDescrip")
                FlexUbicacion.DataSource = Nothing
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
                FlexUbicacion.DataBind()
            ElseIf Session("TipoBus") = "Destino" Then
                TxtDestCodigo.Text = ""
                TxtDestDescripcion.Text = ""
                LblCodDestino.Text = ""
                Session("DestinoCodExt") = FlexUbicacion.Rows(Index).Cells(1).Text
                Session("DestinoDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Session("DestinoCodigo") = FlexUbicacion.Rows(Index).Cells(3).Text
                TxtDestDescripcion.Text = Session("DestinoDescrip")
                TxtDestCodigo.Text = Session("DestinoCodExt")
                LblCodDestino.Text = Session("DestinoCodigo")
                FlexUbicacion.DataSource = Nothing
                FlexUbicacion.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
            End If
        End If
    End Sub

    Private Sub btnUbiListar_Click(sender As Object, e As EventArgs) Handles btnUbiListar.Click
        Try
            Dim psConexion As String = Session("Ruta_Emp")
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            Dim pdCodAlmacen As Double = 0
            If Session("TipoBus") = "Origen" Then
                If Session("TipoOrigen") = "CentroCosto" Then
                    FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                    FlexUbicacion.DataBind()
                ElseIf Session("TipoOrigen") = "Almacen" Then
                    If txtBusCod.Text = "" Then pdCodAlmacen = 0 Else pdCodAlmacen = txtBusCod.Text
                    FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                    FlexUbicacion.DataBind()
                End If
            ElseIf Session("TipoBus") = "Destino" Then
                If Session("TipoDestino") = "CentroCosto" Then
                    FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                    FlexUbicacion.DataBind()
                ElseIf Session("TipoDestino") = "Almacen" Then
                    If txtBusCod.Text = "" Then pdCodAlmacen = 0 Else pdCodAlmacen = txtBusCod.Text
                    FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                    FlexUbicacion.DataBind()
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        Finally
        End Try
    End Sub

    Private Sub BtnBuscarD_Click(sender As Object, e As EventArgs) Handles BtnBuscarD.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub

    Private Sub BtnBuscarO_Click(sender As Object, e As EventArgs) Handles BtnBuscarO.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub
End Class
