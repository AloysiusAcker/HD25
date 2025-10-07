Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class Inv_Definiciones_Tablas
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Listar_Ubicaciones()
            Llenar_Combo_Marca()
            Llenar_Combo_Proyecto()
            TabContainer1.ActiveTabIndex = 0
            TabContainer1.ActiveTab.Enabled = True
            Llenar_Combo_Tipo()
            Llenar_Combo_Ubicacion()
        End If
    End Sub

    '---- CÓDIGO DE LOS ALMACENES ----'
    Protected Sub Listar_Almacenes()
        Dim obj As New Cls_Almacenes
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim descrip As String = TxtDescAlmacen.Text
        dt = obj.Lista_Almacenes(psconexion, descrip)
        GvListaAlmacen.DataSource = dt
        GvListaAlmacen.DataBind()
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListarAlmacen.Click
        Listar_Almacenes()
    End Sub

    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevoAlmacen.Click
        Dim obj As New Cls_Almacenes
        Dim psconexion As String = Session("Ruta_Emp")
        Ocultar_Mostrar_Almacen(True)
        BtnAgregarAlmacen.Text = "Agregar"
        TxtCodAlmacen.Text = obj.Codigo2(psconexion)
        DdlDpto.Items.Clear()
        DdlProv.Items.Clear()
        DdlDist.Items.Clear()
        DdlDpto.Enabled = True
        DdlProv.Items.Add("< Seleccionar >") : DdlProv.SelectedValue = "< Seleccionar >"
        DdlProv.Enabled = False
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        DdlDist.Enabled = False
        Call LlenaComboItem("TBOPC002", DdlDpto)
        If TxtCodAlmacen.Text = 0 Then
            TxtCodAlmacen.Text = 1
        End If
        BtnNuevoAlmacen.Enabled = False
        divNuevoAlm.Visible = True
        divEditarAlm.Visible = False
        'LblEtiquetaDato.Text = "Nuevo Almacén"
    End Sub

    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('hide');", True)
    End Sub

    Protected Sub Ocultar_Mostrar_Almacen(ByVal vf As Boolean)
        TxtCodigoCCAyuda.Text = ""
        TxtCodigoCCSAyuda.Text = ""
        'LblEtiq1.Visible = vf
        TxtDescripcionAlmacen.Visible = vf : TxtDescripcionAlmacen.Text = ""
        TxtCodAlmacen.Text = ""
        'LblEtiq2.Visible = vf
        DdlTipoAlmacen.Visible = vf : DdlTipoAlmacen.SelectedValue = "< Seleccionar >"
        LblEtiq6.Visible = vf
        TxtDireccionAlmacen.Visible = vf : TxtDireccionAlmacen.Text = ""
        'LblEtiq3.Visible = vf
        TxtCCCodigoAlmacen.Visible = vf : TxtCCCodigoAlmacen.Text = ""
        BtnBuscarCC.Visible = vf
        TxtCCDescripcionAlmacen.Visible = vf : TxtCCDescripcionAlmacen.Text = ""
        LblEtiq4.Visible = vf : LblEtiq19.Visible = vf
        LblEtiq5.Visible = vf : LblEtiq20.Visible = vf
        BtnBuscarCCS.Visible = vf
        TxtCCSDescripcionAlmacen.Visible = vf : TxtCCSDescripcionAlmacen.Text = ""
        TxtCCSCodigoAlmacen.Visible = vf : TxtCCSCodigoAlmacen.Text = ""
        LblEtiq7.Visible = vf
        DdlUbicacionAlmacen.Visible = vf : DdlUbicacionAlmacen.SelectedValue = "< Seleccionar >"
        LblEtiq8.Visible = vf : LblEtiq18.Visible = vf
        DdlBajaAlmacen.Visible = vf : DdlBajaAlmacen.SelectedValue = "< Seleccionar >"
        LblEtiq9.Visible = vf : DdlDpto.Visible = vf
        LblEtiq10.Visible = vf : DdlDist.Visible = vf
        LblEtiq11.Visible = vf : DdlProv.Visible = vf
        LblEtiq12.Visible = vf : LblEtiq15.Visible = vf
        LblEtiq13.Visible = vf : LblEtiq16.Visible = vf
        LblEtiq14.Visible = vf : LblEtiq17.Visible = vf
        DdlModoAlmacen.Visible = vf : DdlModoAlmacen.SelectedValue = "< Seleccionar >"
        BtnAgregarAlmacen.Visible = vf
        BtnCancelarAlmacen.Visible = vf
        'LblEtiquetaDato.Visible = vf
        divNuevoAlm.Visible = False
        divEditarAlm.Visible = False
    End Sub

    Protected Sub Llenar_Combo_Tipo()
        Call LlenaComboItem("TBOPC374", DdlTipoAlmacen)
    End Sub

    Protected Sub Llenar_Combo_Ubicacion()
        Dim obj As New Cls_Almacenes
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Departamento(psconexion)
        DdlUbicacionAlmacen.DataSource = dt
        DdlUbicacionAlmacen.DataValueField = "ELEMENTO_CODUNICO"
        DdlUbicacionAlmacen.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlUbicacionAlmacen.DataBind()
        DdlUbicacionAlmacen.Items.Add("< Seleccionar >")
        DdlUbicacionAlmacen.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub BtnAgregarAlmacen_Click(sender As Object, e As EventArgs) Handles BtnAgregarAlmacen.Click
        Dim obj As New Cls_Almacenes
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = TxtCodAlmacen.Text
        Dim descripcion As String = TxtDescripcionAlmacen.Text.Trim.ToString
        Dim planta As String = DdlUbicacionAlmacen.SelectedValue.ToString
        Dim codCCS As String = TxtCodigoCCSAyuda.Text.ToString
        Dim direccion As String = TxtDireccionAlmacen.Text.Trim.ToString
        Dim tipo As String = DdlTipoAlmacen.SelectedValue.Trim.ToString
        Dim modo As String = DdlModoAlmacen.SelectedValue.ToString
        Dim baja As String = DdlBajaAlmacen.SelectedValue.ToString
        Dim psDpto As String = DdlDpto.SelectedValue.Trim.ToString
        Dim psProv As String = DdlProv.SelectedValue.ToString
        Dim psDistrito As String = DdlDist.SelectedValue.ToString
        Dim dt As DataTable


        If descripcion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
        ElseIf direccion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Dirección');", True)
        ElseIf tipo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Tipo');", True)
        ElseIf codCCS.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Sección');", True)
        ElseIf planta.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Ubicación');", True)
        ElseIf baja.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un campo valido -De Baja-');", True)
        ElseIf modo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Modo');", True)
        ElseIf psDpto.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione departamento');", True)
        ElseIf psProv.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione provincia');", True)
        ElseIf psDistrito.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione distrito');", True)
        Else
            If BtnAgregarAlmacen.Text = "Agregar" Then
                dt = obj.Filtrar_Descripcion_Almacen(psconexion, UCase(descripcion), codigo)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
                End If
                obj.Registra_Almacen(psconexion, codigo, descripcion, planta, codCCS, direccion, tipo, modo, baja, psDpto, psProv, psDistrito)
            ElseIf BtnAgregarAlmacen.Text = "Actualizar" Then
                obj.Actualiza_Almacen(psconexion, codigo, descripcion, planta, codCCS, direccion, tipo, modo, baja, psDpto, psProv, psDistrito)
            End If
            BtnAgregarAlmacen.Text = ""
            Ocultar_Mostrar_Almacen(False)
            Listar_Almacenes()
            BtnNuevoAlmacen.Enabled = True
        End If
    End Sub

    Protected Sub BtnCancelarAlmacen_Click(sender As Object, e As EventArgs) Handles BtnCancelarAlmacen.Click
        Ocultar_Mostrar_Almacen(False)
        BtnAgregarAlmacen.Text = ""
        BtnNuevoAlmacen.Enabled = True
    End Sub

    Protected Sub GvListaAlmacen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaAlmacen.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Almacenes
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        Dim dtCombos As DataTable
        Dim ayudaTabla As GridView = New GridView()

        Try
            DdlDpto.Items.Clear()
            DdlProv.Items.Clear()
            DdlDist.Items.Clear()
            DdlDpto.Enabled = True
            DdlProv.Items.Add("< Seleccionar >") : DdlProv.SelectedValue = "< Seleccionar >"
            DdlProv.Enabled = False
            DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
            DdlDist.Enabled = False
            Call LlenaComboItem("TBOPC002", DdlDpto)

            If e.CommandName = "EditaAlmacen" Then
                Ocultar_Mostrar_Almacen(True)
                'LblEtiquetaDato.Text = "Editar Almacén"
                divEditarAlm.Visible = True
                divNuevoAlm.Visible = False
                TxtCodAlmacen.Text = GvListaAlmacen.Rows(Index).Cells(3).Text
                TxtDescripcionAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtDireccionAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtCCCodigoAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtCCDescripcionAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtCCSCodigoAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtCCSDescripcionAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")

                dtCombos = obj.Lista_Almacenes_Combos(cn, GvListaAlmacen.Rows(Index).Cells(3).Text)

                If dtCombos IsNot Nothing Then
                    Dim dbRow As DataRow = dtCombos.Rows(0)
                    DdlUbicacionAlmacen.SelectedValue = dbRow(0).ToString
                    DdlTipoAlmacen.SelectedValue = dbRow(1).ToString
                    DdlModoAlmacen.SelectedValue = dbRow(2).ToString
                    DdlBajaAlmacen.SelectedValue = dbRow(3).ToString
                    TxtCodigoCCSAyuda.Text = dbRow(4).ToString
                    TxtCodigoCCAyuda.Text = dbRow(5).ToString
                    If dbRow(7).ToString <> "" Then
                        DdlDpto.SelectedValue = dbRow(7).ToString : DdlDpto_SelectedIndexChanged(sender, e)
                        DdlProv.SelectedValue = dbRow(9).ToString : DdlProv_SelectedIndexChanged(sender, e)
                        DdlDist.SelectedValue = dbRow(11).ToString
                    End If
                End If
                BtnAgregarAlmacen.Text = "Actualizar"
            ElseIf e.CommandName = "EliminaAlmacen" Then
                Dim dt As New DataTable
                dt = obj.Elimina_Almacen(cn, GvListaAlmacen.Rows(Index).Cells(3).Text)
                If dt.Rows.Count > 0 Then
                    Mensaje.Text = "No se puede eliminar. El Almacen esta siendo utilizado."
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensaje').modal('show');", True)
                Else
                    Listar_Almacenes()
                    Ocultar_Mostrar_Almacen(False)
                End If
            ElseIf e.CommandName = "Relacion" Then
                TxtMCodAlmacen.Text = GvListaAlmacen.Rows(Index).Cells(3).Text
                TextMAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaAlmacen.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                Dim objSeg As New ModuloSeguridad
                gvUsuario.DataSource = objSeg.Listar_Usuarios_SinAdm(Ruta_Ng)
                gvUsuario.DataBind()
                Call Marcar_Usuario()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUsuario').modal('show');", True)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub Marcar_Usuario()
        Try
            Dim Check As CheckBox
            Dim obj As New clsInv_Listados
            Dim dt As DataTable
            Dim pdCodAlmacen As Double = 0
            Dim i As Integer = 0
            pdCodAlmacen = TxtMCodAlmacen.Text
            dt = obj.Lista_Usuario_xAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen)
            For Each dr As Data.DataRow In dt.Rows
                For i = 0 To gvUsuario.Rows.Count - 1
                    If gvUsuario.Rows(i).Cells(2).Text = dr("USUARI_CODIGO").ToString Then
                        Check = CType(gvUsuario.Rows(i).Cells(1).FindControl("chkUsuario"), CheckBox)
                        Check.Checked = True
                        Check.Enabled = False
                        GoTo Despues
                    End If
                Next
Despues:
            Next
            dt = Nothing
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnRelacionCerrar_Click(sender As Object, e As EventArgs) Handles BtnRelacionCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUsuario').modal('hide');", True)
    End Sub
    Private Sub BtnSi_Click(sender As Object, e As EventArgs) Handles BtnSi.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensaje').modal('hide');", True)
    End Sub
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim obj As New Cls_Almacenes
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codC As String = ""
        Dim codS As String = BuscarCodigo.Value.ToString
        Dim descripcion As String = BuscarDescripcion.Value.Trim.ToString
        If TituloPopup.Text = "Buscar Centro de Costos" Then
            codC = BuscarCodigo.Value.ToString
            dt = obj.Buscar_Ceco(psconexion, codC, descripcion)
        Else
            codC = TxtCodigoCCAyuda.Text.ToString
            If codC = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione Centro de Costo');", True)
            End If
            dt = obj.Buscar_Cecose(psconexion, codC, codS, descripcion)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Protected Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Almacenes
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        Dim codACC As String = TxtCodigoCCAyuda.Text.ToString

        If e.CommandName = "Aceptar" And TituloPopup.Text = "Buscar Centro de Costos" Then
            TxtCCCodigoAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtCCDescripcionAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtCodigoCCAyuda.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Limpiar_Popup()
        Else
            TxtCCSCodigoAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtCCSDescripcionAlmacen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtCodigoCCSAyuda.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Limpiar_Popup()
        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Limpiar_Popup()
    End Sub

    Private Sub BtnBuscarCC_Click(sender As Object, e As EventArgs) Handles BtnBuscarCC.Click
        TituloPopup.Text = "Buscar Centro de Costos"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
    End Sub

    Private Sub BtnBuscarCCS_Click(sender As Object, e As EventArgs) Handles BtnBuscarCCS.Click
        TituloPopup.Text = "Buscar Centro de Costos Sección"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
    End Sub




    '---- CÓDIGO DE LAS MARCAS ----'

    '-- LISTAR MARCAS --'
    Protected Sub Listar_Marcas()
        Dim obj As New Cls_Marcas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim desc As String = TxtDescMarca.Text
        dt = obj.Lista_Marcas(psconexion, desc)
        GvListaMarcas.DataSource = dt
        GvListaMarcas.DataBind()
    End Sub

    '-- LLENAR COMBO MARCAS --'
    Protected Sub Llenar_Combo_Marca()
        Dim obj As New Cls_Marcas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Marcas(psconexion, "")
        DdlMarca.DataSource = dt
        DdlMarca.DataValueField = "ARTMAR_CODIGO"
        DdlMarca.DataTextField = "ARTMAR_DESCRIPCION"
        DdlMarca.DataBind()
    End Sub

    '-- OCULTAR O MOSTRAR LOS LABEL'S, TEXTBOX'S Y BUTTON'S DE LAS MARCAS --'
    Protected Sub Ocultar_Mostrar_Marcas(ByVal vf As Boolean)
        TxtCodigoMarca.Visible = vf
        TxtDescripcionMarca.Visible = vf
        LblCodigoMarca.Visible = vf
        LblDescripcionMarca.Visible = vf
        BtnCancelarMarca.Visible = vf
        BtnAgregarMarca.Visible = vf
        divMarcaNuevo.Visible = False
        divMarcaEdit.Visible = False
    End Sub

    '-- LIMPIAR LOS TEXTBOX'S DE LAS MARCAS --'
    Protected Sub Limpiar_Cajas_Marca()
        TxtCodigoMarca.Text = ""
        TxtDescripcionMarca.Text = ""
    End Sub

    Protected Sub BtnListarMarca_Click(sender As Object, e As EventArgs) Handles BtnListarMarca.Click
        Listar_Marcas()
    End Sub

    Protected Sub BtnNuevaMarca_Click(sender As Object, e As EventArgs) Handles BtnNuevaMarca.Click
        Dim obj As New Cls_Marcas
        Dim cn As String = Session("Ruta_Emp")
        Ocultar_Mostrar_Marcas(True)
        Limpiar_Cajas_Marca()
        TxtCodigoMarca.Text = obj.CodigoMarca(cn)
        If TxtCodigoMarca.Text = 0 Then
            TxtCodigoMarca.Text = 1
        End If
        BtnAgregarMarca.Text = "Agregar"
        divMarcaNuevo.Visible = True
        divMarcaEdit.Visible = False
        BtnNuevaMarca.Enabled = False
    End Sub

    Protected Sub BtnGrabarMarca_Click(sender As Object, e As EventArgs) Handles BtnAgregarMarca.Click
        Dim obj As New Cls_Marcas
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = TxtCodigoMarca.Text
        Dim descripcion As String = TxtDescripcionMarca.Text.Trim
        Dim dt As DataTable
        dt = obj.Filtrar_Descripcion_Marca(psconexion, UCase(descripcion), codigo)
        If descripcion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
        ElseIf dt.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
        Else
            If BtnAgregarMarca.Text = "Agregar" Then
                obj.Registra_Marca(psconexion, codigo, descripcion)
            ElseIf BtnAgregarMarca.Text = "Actualizar" Then
                obj.Actualiza_Marca(psconexion, codigo, descripcion)
            End If
            Ocultar_Mostrar_Marcas(False)
            Listar_Marcas()
            BtnNuevaMarca.Enabled = True
        End If
    End Sub

    Protected Sub BtnCancelarMarca_Click(sender As Object, e As EventArgs) Handles BtnCancelarMarca.Click
        Limpiar_Cajas_Marca()
        Ocultar_Mostrar_Marcas(False)
        BtnNuevaMarca.Enabled = True
    End Sub

    Protected Sub GvListaMarcas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaMarcas.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Marcas
        Dim obj1 As New Cls_Modelo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = GvListaMarcas.Rows(Index).Cells(3).Text
        Dim dt As New DataTable
        If e.CommandName = "DetalleMarca" Then
            dt = obj1.Lista_Marcas_Modelo(psconexion, codigo)
            GvListaModelo.DataSource = dt
            GvListaModelo.DataBind()
            Llenar_Combo_Marca()
            BtnNuevoModelo.Visible = True
            DdlMarca.SelectedValue = codigo
            TabContainer1.ActiveTabIndex = 2
            TabContainer1.Enabled = True
        ElseIf e.CommandName = "EliminaMarca" Then
            obj.Eliminar_Marca(psconexion, Replace(GvListaMarcas.Rows(Index).Cells(3).Text, "&nbsp;", ""))
            Listar_Marcas()
            Ocultar_Mostrar_Marcas(False)
            Limpiar_Cajas_Marca()
        ElseIf e.CommandName = "EditaMarca" Then
            Ocultar_Mostrar_Marcas(True)
            TxtCodigoMarca.Text = Replace(GvListaMarcas.Rows(Index).Cells(3).Text, "&nbsp;", "")
            TxtDescripcionMarca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaMarcas.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            BtnAgregarMarca.Text = "Actualizar"
            divMarcaNuevo.Visible = False
            divMarcaEdit.Visible = True
        End If
    End Sub

    '--------------------- MODELO ----------------------'
    Protected Sub BtnNuevoModelo_Click(sender As Object, e As EventArgs) Handles BtnNuevoModelo.Click
        Dim obj As New Cls_Modelo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codMarca As String = DdlMarca.SelectedValue.ToString
        Ocultar_Mostrar_Modelo(True)
        codigoModelo.Value = obj.CodigoModelo(psconexion, codMarca)
        If codigoModelo.Value = 0 Then
            codigoModelo.Value = 1
        End If
        BtnAgregarModelo.Text = "Agregar"
        TxtDescripcionModelo.Value = ""
        BtnNuevoModelo.Enabled = False
    End Sub

    Protected Sub Listar_Modelo()
        Dim obj As New Cls_Modelo
        Dim codMarca As String = DdlMarca.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")
        Dim dt As DataTable
        dt = obj.Lista_Marcas_Modelo(psconexion, codMarca)
        GvListaModelo.DataSource = dt
        GvListaModelo.DataBind()
        DdlMarca.Enabled = True
    End Sub

    Protected Sub Ocultar_Mostrar_Modelo(ByVal vf As Boolean)
        TxtDescripcionModelo.Visible = vf
        LblDescripcionModelo.Visible = vf
        BtnCancelarModelo.Visible = vf
        BtnAgregarModelo.Visible = vf
    End Sub

    Protected Sub BtnGrabarModelo_Click(sender As Object, e As EventArgs) Handles BtnAgregarModelo.Click
        Dim obj As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codMarca As String = DdlMarca.SelectedValue.ToString
        Dim codMod As String = codigoModelo.Value.ToString
        Dim descripcion As String = TxtDescripcionModelo.Value.Trim.ToString
        Dim dt As New DataTable

        dt = obj.Filtrar_Descripcion_Modelo(psconexion, UCase(descripcion), codMarca, codMod)
        If descripcion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
        ElseIf dt.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
        Else
            If BtnAgregarModelo.Text = "Agregar" Then
                obj.Agregar_Marcas_Modelo(psconexion, codMod, codMarca, descripcion)
            ElseIf BtnAgregarModelo.Text = "Actualizar" Then
                obj.Actualizar_Marcas_Modelo(psconexion, codMod, codMarca, descripcion)
            End If
            Ocultar_Mostrar_Modelo(False)
            Listar_Modelo()
            TxtDescripcionModelo.Value = ""
            codigoModelo.Value = ""
            BtnNuevoModelo.Enabled = True
        End If
    End Sub

    Private Sub BtnListarModelo_Click(sender As Object, e As EventArgs) Handles BtnListarModelo.Click
        Listar_Modelo()
    End Sub

    Protected Sub BtnCancelarModelo_Click(sender As Object, e As EventArgs) Handles BtnCancelarModelo.Click
        Ocultar_Mostrar_Modelo(False)
        DdlMarca.Enabled = True
        BtnNuevoModelo.Enabled = True
    End Sub

    Protected Sub GvListaModelo_SelectedIndexChanged(sender As Object, e As GridViewCommandEventArgs) Handles GvListaModelo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Modelo
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As DataTable

        If e.CommandName = "EditaModelo" Then
            Ocultar_Mostrar_Modelo(True)
            DdlMarca.SelectedValue = GvListaModelo.Rows(Index).Cells(3).Text
            codigoModelo.Value = GvListaModelo.Rows(Index).Cells(4).Text
            TxtDescripcionModelo.Value = GvListaModelo.Rows(Index).Cells(5).Text
            DdlMarca.Enabled = False
            BtnAgregarModelo.Text = "Actualizar"
        ElseIf e.CommandName = "EliminaModelo" Then
            dt = obj.Eliminar_Marcas_Modelo(cn, GvListaModelo.Rows(Index).Cells(4).Text, GvListaModelo.Rows(Index).Cells(3).Text)
            Dim dbRow As DataRow = dt.Rows(0)
            If dbRow(0) = "1" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar el modelo porque está en uso');", True)
            Else
                dt = obj.Lista_Marcas_Modelo(cn, GvListaModelo.Rows(Index).Cells(3).Text)
                GvListaModelo.DataSource = dt
                GvListaModelo.DataBind()
                Ocultar_Mostrar_Modelo(False)
            End If
        ElseIf e.CommandName = "DetalleModelo" Then
            txtCodigoMo.Text = GvListaModelo.Rows(Index).Cells(4).Text
            txtNomModelo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaModelo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Ocultar_Mostrar_Detalle_Modelo(False)
            txtCodigoDetaMo.Text = ""
            Listar_ModeloDetalle()
            btnNuevoDetalle.Enabled = True
        End If
    End Sub

    '--------------------- DETALLE -------------------'
    Protected Sub Limpiar_Cajas_Detalle_Modelo()
        txtDescripcionDetalle.Text = ""
        txtCodigoDetaMo.Text = ""
    End Sub

    Protected Sub Ocultar_Mostrar_Detalle_Modelo(ByVal vf As Boolean)
        txtDescripcionDetalle.Visible = vf
        lbldescripDetalle.Visible = vf
        btnAgregarDetalle.Visible = vf
        btnCancelarDetalle.Visible = vf
        lblCodModDet.Visible = vf
        txtCodigoDetaMo.Visible = vf
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregarDetalle.Click
        Dim obj As New Cls_Detalle_Modelo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codMod As String = txtCodigoMo.Text.ToString
        Dim codModDetalle As String = txtCodigoDetaMo.Text.ToString
        Dim descripcion As String = txtDescripcionDetalle.Text.Trim.ToString
        Dim dt As New DataTable

        dt = obj.Filtrar_Descripcion_Detalle_Modelo(psconexion, UCase(descripcion), codMod, codModDetalle)
        If descripcion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
        ElseIf dt.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
        Else
            If btnAgregarDetalle.Text = "Agregar" Then
                obj.RegistrarArticuloModeloDetalle(psconexion, codModDetalle, codMod, descripcion)
            ElseIf btnAgregarDetalle.Text = "Actualizar" Then
                obj.ActualizarArticuloModeloDetalle(psconexion, codModDetalle, descripcion)
            End If
            Listar_ModeloDetalle()
            Limpiar_Cajas_Detalle_Modelo()
            Ocultar_Mostrar_Detalle_Modelo(False)
            btnNuevoDetalle.Enabled = True
        End If
    End Sub

    Protected Sub Listar_ModeloDetalle()
        Dim obj As New Cls_Detalle_Modelo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim cod As String = txtCodigoMo.Text
        dt = obj.ListarModelo_Detalle(psconexion, cod)
        GvListaDetalle.DataSource = dt
        GvListaDetalle.DataBind()
    End Sub

    Private Sub BtnNuevoDetalle_Click(sender As Object, e As EventArgs) Handles btnNuevoDetalle.Click
        Dim obj As New Cls_Detalle_Modelo
        Dim cn As String = Session("Ruta_Emp")
        Dim codigoModelo As String = txtCodigoMo.Text.ToString

        Limpiar_Cajas_Detalle_Modelo()
        If codigoModelo <> "" Then
            txtCodigoDetaMo.Text = obj.CodigoModDetalle(cn, codigoModelo)
            If txtCodigoDetaMo.Text = 0 Then
                txtCodigoDetaMo.Text = 1
            End If
            Ocultar_Mostrar_Detalle_Modelo(True)
            btnAgregarDetalle.Text = "Agregar"
            btnNuevoDetalle.Enabled = False
        End If
    End Sub

    Protected Sub btnCancelarDetalle_Click(sender As Object, e As EventArgs) Handles btnCancelarDetalle.Click
        Ocultar_Mostrar_Detalle_Modelo(False)
        Limpiar_Cajas_Detalle_Modelo()
        btnNuevoDetalle.Enabled = True
    End Sub

    Protected Sub GvListaDetalle_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaDetalle.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Detalle_Modelo
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable

        If e.CommandName = "EditaDetalle" Then
            txtCodigoDetaMo.Text = Replace(GvListaDetalle.Rows(Index).Cells(2).Text, "&nbsp;", "")
            txtDescripcionDetalle.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaDetalle.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtCodigoDetaMo.Enabled = False
            txtCodigoMo.Enabled = False
            Ocultar_Mostrar_Detalle_Modelo(True)
            btnAgregarDetalle.Text = "Actualizar"
        ElseIf e.CommandName = "EliminaDetalle" Then
            obj.EliminarArticuloModeloDetalle(cn, Replace(GvListaDetalle.Rows(Index).Cells(2).Text, "&nbsp;", ""))
            Listar_ModeloDetalle()
        End If
    End Sub


    '--------------------- PROPIETARIO ---------------------'

    '-- LIMPIAR LOS TEXTBOX'S DE LOS PROPIETARIOS --'
    Protected Sub Limpiar_Cajas_Propietarios()
        TxtCodigoPropietario.Text = ""
        TxtDescripcionPropietario.Text = ""
        DdlPlacabilidadPropietario.SelectedValue = "< Seleccionar >"
        TxtPlacaInicial.Text = ""
        TxtPlacaFinal.Text = ""
    End Sub

    Protected Sub Cajas_Placa(ByVal vf As Boolean, ByVal placaI As String, ByVal placaF As String)
        TxtPlacaInicial.Enabled = vf
        TxtPlacaFinal.Enabled = vf
        TxtPlacaInicial.Text = placaI
        TxtPlacaFinal.Text = placaF
    End Sub

    '-- OCULTAR O MOSTRAR LOS LABEL'S, TEXTBOX'S Y BUTTON'S DE LOS PROPIETARIOS --'
    Protected Sub Ocultar_Mostrar_Propietarios(ByVal vf As Boolean)
        LblCodigoPropietario.Visible = vf
        LblDescripcionPropietario.Visible = vf
        LblPlacabilidadPropietario.Visible = vf
        TxtCodigoPropietario.Visible = vf
        TxtDescripcionPropietario.Visible = vf
        DdlPlacabilidadPropietario.Visible = vf
        LblPlacaInicial.Visible = vf
        TxtPlacaInicial.Visible = vf
        LblPlacaFinal.Visible = vf
        TxtPlacaFinal.Visible = vf
        BtnAgregarPropietario.Visible = vf
        BtnCancelarPropietario.Visible = vf
    End Sub

    '-- LISTAR PROPIETARIOS --'
    Protected Sub Listar_Propietarios()
        Dim obj As New Cls_Propietario
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim descrip As String = TxtDescPropietario.Text.Trim
        dt = obj.Lista_PropXDesc(psconexion, descrip)
        GvListaPropietario.DataSource = dt
        GvListaPropietario.DataBind()
    End Sub

    Protected Sub BtnListarPropietario_Click(sender As Object, e As EventArgs) Handles BtnListarPropietario.Click
        Listar_Propietarios()
    End Sub

    Protected Sub BtnNuevoPropietario_Click(sender As Object, e As EventArgs) Handles BtnNuevoPropietario.Click
        Dim obj As New Cls_Propietario
        Dim cn As String = Session("Ruta_Emp")
        Limpiar_Cajas_Propietarios()
        TxtCodigoPropietario.Text = obj.Codigo2(cn)
        If TxtCodigoPropietario.Text = 0 Then
            TxtCodigoPropietario.Text = 1
        End If
        Ocultar_Mostrar_Propietarios(True)
        BtnAgregarPropietario.Text = "Agregar"
        Cajas_Placa(False, "", "")
        BtnNuevoPropietario.Enabled = False
    End Sub

    Private Sub DdlPlacabilidadPropietario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlPlacabilidadPropietario.SelectedIndexChanged
        If DdlPlacabilidadPropietario.SelectedValue = "S" Then
            Cajas_Placa(True, TxtPlacaInicial.Text.ToString, TxtPlacaFinal.Text.ToString)
        Else
            Cajas_Placa(False, TxtPlacaInicial.Text.ToString, TxtPlacaFinal.Text.ToString)
        End If
    End Sub

    Protected Sub BtnAgregarPropietario_Click(sender As Object, e As EventArgs) Handles BtnAgregarPropietario.Click
        Dim obj As New Cls_Propietario
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = TxtCodigoPropietario.Text
        Dim descripcion As String = TxtDescripcionPropietario.Text.Trim.ToString
        Dim placabilidad As String = DdlPlacabilidadPropietario.SelectedValue.ToString
        Dim placaInicial As String = TxtPlacaInicial.Text.Trim.ToString
        Dim placaFinal As String = TxtPlacaFinal.Text.Trim.ToString

        dt = obj.Filtrar_Descripcion_Propietario(psconexion, UCase(descripcion), codigo)
        If descripcion = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
        ElseIf dt.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
        ElseIf placabilidad = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione la Placabilidad');", True)
        ElseIf placabilidad = "S" And placaInicial = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Placa Inicial');", True)
        ElseIf placabilidad = "S" And placaFinal = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Placa Final');", True)
        Else
            Try
                If placabilidad = "S" And (Convert.ToInt32(placaFinal) < Convert.ToInt32(placaInicial)) Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La Placa Final debe ser mayor que la Placa Inicial');", True)
                Else
                    If BtnAgregarPropietario.Text = "Agregar" Then
                        If placabilidad = "N" Then
                            placaInicial = ""
                            placaFinal = ""
                        Else
                            placaInicial = Convert.ToInt32(placaInicial)
                            placaFinal = Convert.ToInt32(placaFinal)
                        End If
                        obj.RegistrarPropietario(psconexion, codigo, descripcion, placabilidad)
                    ElseIf BtnAgregarPropietario.Text = "Actualizar" Then
                        If placabilidad = "N" Then
                            placaInicial = ""
                            placaFinal = ""
                        Else
                            placaInicial = Convert.ToInt32(placaInicial)
                            placaFinal = Convert.ToInt32(placaFinal)
                        End If
                        obj.ActualizaPropietario(psconexion, codigo, descripcion, placabilidad)
                    End If
                    obj.Agregar_Actualizar_Placa_TipoBien(psconexion, codigo, placaInicial, placaFinal)
                    Limpiar_Cajas_Propietarios()
                    Ocultar_Mostrar_Propietarios(False)
                    Listar_Propietarios()
                    Cajas_Placa(False, "", "")
                    BtnNuevoPropietario.Enabled = True
                End If
            Catch ex As FormatException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La placa inicial y la placa final deben ser números');", True)
            End Try
        End If
    End Sub

    Protected Sub BtnCancelarPropietario_Click(sender As Object, e As EventArgs) Handles BtnCancelarPropietario.Click
        Ocultar_Mostrar_Propietarios(False)
        Limpiar_Cajas_Propietarios()
        Cajas_Placa(False, "", "")
        BtnNuevoPropietario.Enabled = True
    End Sub

    Protected Sub GvListaPropietario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaPropietario.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Propietario
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable

        If e.CommandName = "EditaPropietario" Then
            Ocultar_Mostrar_Propietarios(True)
            TxtCodigoPropietario.Text = Replace(GvListaPropietario.Rows(Index).Cells(2).Text, "&nbsp;", "")
            TxtDescripcionPropietario.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaPropietario.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If Replace(GvListaPropietario.Rows(Index).Cells(4).Text, "&nbsp;", "") = "" Then
                DdlPlacabilidadPropietario.SelectedValue = "< Seleccionar >"
            Else
                DdlPlacabilidadPropietario.SelectedValue = Replace(GvListaPropietario.Rows(Index).Cells(4).Text, "&nbsp;", "")
            End If
            If Replace(GvListaPropietario.Rows(Index).Cells(4).Text, "&nbsp;", "") = "S" Then
                Cajas_Placa(True, Replace(GvListaPropietario.Rows(Index).Cells(5).Text, "&nbsp;", ""), Replace(GvListaPropietario.Rows(Index).Cells(6).Text, "&nbsp;", ""))
            ElseIf Replace(GvListaPropietario.Rows(Index).Cells(4).Text, "&nbsp;", "") = "" Then
            Else
                Cajas_Placa(False, Replace(GvListaPropietario.Rows(Index).Cells(5).Text, "&nbsp;", ""), Replace(GvListaPropietario.Rows(Index).Cells(6).Text, "&nbsp;", ""))
            End If
            BtnAgregarPropietario.Text = "Actualizar"
        ElseIf e.CommandName = "EliminaPropietario" Then
            dt = obj.EliminaPropietario(cn, Replace(GvListaPropietario.Rows(Index).Cells(2).Text, "&nbsp;", ""))
            Dim dbRow As DataRow = dt.Rows(0)
            If dbRow(0) = "1" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar el Propietario');", True)
            ElseIf dbRow(0) = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Propietario eliminado');", True)
                obj.Eliminar_Placa_TipoBien(cn, Replace(GvListaPropietario.Rows(Index).Cells(2).Text, "&nbsp;", ""))
                Limpiar_Cajas_Propietarios()
                Listar_Propietarios()
                Ocultar_Mostrar_Propietarios(False)
            End If
        End If
    End Sub

    '--------------------------- PROYECTO --------------------------'
    Protected Sub Listar_Proyectos()
        Dim obj As New Cls_Proyectos
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim año As String = DdlAño.Text
        dt = obj.Lista_Proyectos(psconexion, año)
        GridView_Proyectos.DataSource = dt
        GridView_Proyectos.DataBind()
    End Sub

    Protected Sub BtnListar_Proyectos_Click(sender As Object, e As EventArgs) Handles btnListar_Proyectos.Click
        Listar_Proyectos()
    End Sub

    Protected Sub Limpiar_Cajas()
        txtCodigo_Proy.Text = ""
        txtDescripcion_Proy.Text = ""
    End Sub

    Protected Sub Ocultar_Visible(ByVal vf As Boolean)
        txtAño.Visible = vf
        txtCodigo_Proy.Visible = vf
        txtDescripcion_Proy.Visible = vf
        LblAño_Proy.Visible = vf
        LblCodigo_Proy.Visible = vf
        LblDescripción_Proy.Visible = vf
        BtnCancelar_Proyectos.Visible = vf
        BtnGrabar_Proyectos.Visible = vf
    End Sub

    Protected Sub BtnGrabar_Proyectos_Click(sender As Object, e As EventArgs) Handles BtnGrabar_Proyectos.Click
        Dim obj As New Cls_Proyectos
        Dim psconexion As String = Session("Ruta_Emp")
        Dim año As String = txtAño.Text.Trim.ToString
        Dim codigo As String = txtCodigo_Proy.Text.ToString
        Dim descripcion As String = txtDescripcion_Proy.Text.Trim.ToString
        Dim dt As DataTable

        dt = obj.Filtrar_Descripcion_Proyecto(psconexion, UCase(descripcion), codigo)
        Try
            If año.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese un Año');", True)
            ElseIf Convert.ToInt32(año) < (Date.Now.Year - 2) Or Convert.ToInt32(año) > (Date.Now.Year + 10) Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese un Año entre " + (Date.Now.Year - 2).ToString + " y " + (Date.Now.Year + 10).ToString + "');", True)
            ElseIf descripcion.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
            ElseIf dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
            Else
                If BtnGrabar_Proyectos.Text = "Grabar" Then
                    obj.Registra_Proyecto(psconexion, año, codigo, descripcion)
                ElseIf BtnGrabar_Proyectos.Text = "Actualizar" Then
                    obj.Actualiza_Proyecto(psconexion, año, codigo, descripcion)
                End If
                Ocultar_Visible(False)
                DdlAño.SelectedValue = txtAño.Text
                Listar_Proyectos()
                Llenar_Combo_Proyecto()
                btnNuevo_Proyectos.Enabled = True
            End If
        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Año debe ser número');", True)
        End Try
    End Sub

    Protected Sub BtnCancelar_Proyectos_Click(sender As Object, e As EventArgs) Handles BtnCancelar_Proyectos.Click
        Ocultar_Visible(False)
        Limpiar_Cajas()
        btnNuevo_Proyectos.Enabled = True
    End Sub

    Protected Sub GridView_Proyectos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView_Proyectos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Proyectos
        Dim dt As New DataTable
        Dim cn As String = Session("Ruta_Emp")

        If e.CommandName = "Editar" Then
            Ocultar_Visible(True)
            txtAño.Text = Replace(GridView_Proyectos.Rows(Index).Cells(3).Text, "&nbsp;", "")
            txtCodigo_Proy.Text = Replace(GridView_Proyectos.Rows(Index).Cells(2).Text, "&nbsp;", "")
            txtDescripcion_Proy.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GridView_Proyectos.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            BtnGrabar_Proyectos.Text = "Actualizar"
        ElseIf e.CommandName = "Eliminar" Then
            dt = obj.Eliminar_Proyecto(cn, Replace(GridView_Proyectos.Rows(Index).Cells(2).Text, "&nbsp;", ""), Replace(GridView_Proyectos.Rows(Index).Cells(3).Text, "&nbsp;", ""))
            Dim dbRow As DataRow = dt.Rows(0)
            If dbRow(0) = "1" Then
                MensajeProyecto.Text = "No se puede eliminar. El Proyeccto esta siendo utilizado."
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeProyecto').modal('show');", True)
            ElseIf dbRow(0) = "2" Then
                Llenar_Combo_Proyecto()
                Listar_Proyectos()
                Ocultar_Visible(False)
                Limpiar_Cajas()
            End If
        End If
    End Sub

    Private Sub BtnPOk_Click(sender As Object, e As EventArgs) Handles BtnPOk.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeProyecto').modal('hide');", True)
    End Sub

    Protected Sub Llenar_Combo_Proyecto()
        Dim obj As New Cls_Proyectos
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Combo(psconexion)
        DdlAño.DataSource = dt
        DdlAño.DataMember = "proyecto_AÑO"
        DdlAño.DataTextField = "proyecto_AÑO"
        DdlAño.DataBind()
    End Sub

    Protected Sub BtnNuevoProyecto_Click(sender As Object, e As EventArgs) Handles btnNuevo_Proyectos.Click
        Dim obj As New Cls_Proyectos
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        Limpiar_Cajas()
        txtCodigo_Proy.Text = obj.CodigoProy(cn)
        If txtCodigo_Proy.Text = 0 Then
            txtCodigo_Proy.Text = 1
        End If
        Ocultar_Visible(True)
        BtnGrabar_Proyectos.Text = "Grabar"
        btnNuevo_Proyectos.Enabled = False
    End Sub

    '--------------------- UBICACIONES ---------------------'
    Protected Sub Ocultar_Visible_Ubicaciones(ByVal vf As Boolean)
        TxtCodigo_Ubicaciones.Visible = vf
        TxtDescripcion_Ubicaciones.Visible = vf
        LblCodigo.Visible = vf
        LblDescripción.Visible = vf
        BtnCancelar_Ubicaciones.Visible = vf
        BtnGrabar_Ubicaciones.Visible = vf
        DdlTipo.Visible = vf
        Label4.Visible = vf
        Label5.Visible = vf
        Label6.Visible = vf
    End Sub

    Protected Sub Limpiar_Cajas_Ubicaciones()
        TxtCodigo_Ubicaciones.Text = ""
        TxtDescripcion_Ubicaciones.Text = ""
        DdlTipo.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub Listar_Ubicaciones()
        Dim obj As New Cls_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Define_Ubicaciones(psconexion)
        GridView_Ubicaciones.DataSource = dt
        GridView_Ubicaciones.DataBind()
    End Sub

    Private Sub GridView1_Ubicaciones_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView_Ubicaciones.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable
        Dim psTipo As String = ""

        If e.CommandName = "Editar" Then
            Ocultar_Visible_Ubicaciones(True)
            TxtCodigo_Ubicaciones.Text = Replace(GridView_Ubicaciones.Rows(Index).Cells(2).Text, "&nbsp;", "")
            TxtDescripcion_Ubicaciones.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GridView_Ubicaciones.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            psTipo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GridView_Ubicaciones.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            BtnGrabar_Ubicaciones.Text = "Actualizar"
            If psTipo <> "" Then DdlTipo.SelectedValue = psTipo
        ElseIf e.CommandName = "Eliminar" Then
            dt = obj.Eliminar_Ubicaciones(cn, Replace(GridView_Ubicaciones.Rows(Index).Cells(2).Text, "&nbsp;", ""), Replace(GridView_Ubicaciones.Rows(Index).Cells(3).Text, "&nbsp;", ""))
            If dt.Rows.Count > 0 Then
                MensajeUbic.Text = "No se puede eliminar. La Ubicacion esta siendo utilizado."
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeUbic').modal('show');", True)
            Else
                Listar_Ubicaciones()
                Ocultar_Visible_Ubicaciones(False)
                Limpiar_Cajas_Ubicaciones()
            End If
        End If
    End Sub

    Protected Sub Nuevo_Ubicaciones_Click(sender As Object, e As EventArgs) Handles BtnNuevo_Ubicaciones.Click
        Dim obj As New Cls_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        Limpiar_Cajas_Ubicaciones()
        TxtCodigo_Ubicaciones.Text = obj.Codigo_Ubicaciones(cn)
        If TxtCodigo_Ubicaciones.Text = 0 Then
            TxtCodigo_Ubicaciones.Text = 1
        End If
        DdlTipo.SelectedValue = "< Seleccionar >"
        Ocultar_Visible_Ubicaciones(True)
        BtnGrabar_Ubicaciones.Text = "Grabar"
        BtnNuevo_Ubicaciones.Enabled = False
    End Sub

    Protected Sub BtnCancelar_Ubicaciones_Click(sender As Object, e As EventArgs) Handles BtnCancelar_Ubicaciones.Click
        Ocultar_Visible_Ubicaciones(False)
        Limpiar_Cajas_Ubicaciones()
        BtnNuevo_Ubicaciones.Enabled = True
    End Sub

    Protected Sub BtnGrabar_Ubicaciones_Click(sender As Object, e As EventArgs) Handles BtnGrabar_Ubicaciones.Click
        Dim obj As New Cls_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = 0
        If TxtCodigo_Ubicaciones.Text <> "" Then codigo = Nz(TxtCodigo_Ubicaciones.Text)
        Dim descripcion As String = TxtDescripcion_Ubicaciones.Text.Trim.ToString
        Dim dt As DataTable
        Dim psTipo As String = ""
        If DdlTipo.SelectedValue <> "< Seleccionar >" Then
            psTipo = DdlTipo.SelectedValue
        End If

        dt = obj.Filtrar_Descripcion_Ubicacion(psconexion, UCase(descripcion), codigo)
        If dt.Rows.Count > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe la Descripción');", True)
        ElseIf descripcion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Descripción');", True)
        ElseIf DdlTipo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione Tipo');", True)
        Else
            If BtnGrabar_Ubicaciones.Text = "Grabar" Then
                obj.Registra_Ubicaciones(psconexion, codigo, descripcion, psTipo)
            ElseIf BtnGrabar_Ubicaciones.Text = "Actualizar" Then
                obj.Actualiza_Ubicaciones(psconexion, codigo, descripcion, psTipo)
            End If
            Ocultar_Visible_Ubicaciones(False)
            Limpiar_Cajas_Ubicaciones()
            Listar_Ubicaciones()
            BtnNuevo_Ubicaciones.Enabled = True
        End If
    End Sub

    Private Sub BtnUOk_Click(sender As Object, e As EventArgs) Handles BtnUOk.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeUbic').modal('hide');", True)
    End Sub

    Private Sub DdlDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDpto.SelectedIndexChanged
        DdlProv.Items.Clear()
        DdlDist.Items.Clear()
        DdlProv.Enabled = False
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        DdlDist.Enabled = False
        If DdlDpto.SelectedIndex = -1 Or DdlDpto.Items.Count = 0 Then Exit Sub
        If DdlDpto.Items(DdlDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlProv, Left(DdlDpto.SelectedValue, 2), "PR")
        If DdlDpto.SelectedValue <> "< Seleccionar >" Then DdlProv.Enabled = True
    End Sub

    Private Sub DdlProv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProv.SelectedIndexChanged
        DdlDist.Items.Clear()
        DdlDist.Enabled = False
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        If DdlProv.SelectedIndex = -1 Or DdlProv.Items.Count = 0 Then Exit Sub
        If DdlProv.Items(DdlProv.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlDist, Left(DdlDpto.SelectedValue, 2) + Mid(DdlProv.SelectedValue, 3, 2), "DS")
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        If DdlProv.SelectedValue <> "< Seleccionar >" Then DdlDist.Enabled = True
    End Sub

    Private Sub gvUsuario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUsuario.RowCommand
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim pCodAlmacen As Double = 0
            Dim Check As CheckBox
            Dim pCodUsuario As String = ""
            pCodAlmacen = TxtMCodAlmacen.Text
            If e.CommandName = "Quitar" Then
                Dim obj As New clsInv_Listados
                obj.Delete_UsuarioAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), pCodAlmacen, gvUsuario.Rows(Index).Cells(2).Text)
                Check = CType(gvUsuario.Rows(Index).Cells(1).FindControl("chkUsuario"), CheckBox)
                If Check.Checked = True And Check.Enabled = False Then
                    Check.Checked = False : Check.Enabled = True
                End If
                Call Marcar_Usuario()
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        Finally
            '
        End Try
    End Sub

    Private Sub BtnRelacionGuardar_Click(sender As Object, e As EventArgs) Handles BtnRelacionGuardar.Click
        Try
            Dim Check As CheckBox
            Dim obj As New clsInv_Listados
            Dim pdCodAlmacen As Double = 0
            Dim i As Integer = 0
            Dim pCodUsuario As String = ""
            pdCodAlmacen = TxtMCodAlmacen.Text
            For i = 0 To gvUsuario.Rows.Count - 1
                Check = CType(gvUsuario.Rows(i).Cells(1).FindControl("chkUsuario"), CheckBox)
                If Check.Checked = True And Check.Enabled = True Then
                    pCodUsuario = gvUsuario.Rows(i).Cells(2).Text
                    obj.Insertar_UsuarioAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, pCodUsuario)
                End If
            Next
            Call Marcar_Usuario()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        Finally
            '
        End Try
    End Sub
End Class