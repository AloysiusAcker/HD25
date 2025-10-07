Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_
    Inherits System.Web.UI.Page
    Dim ObjLista As New clsInv_Listados
    Dim ObjProceso As New clsInv_Procesos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnOpen.Attributes.Add("OnClick", "window.open('Inventario_Emergente.aspx',null,'height=600,width=500');")
            DivInv.Visible = False
            Call Llena_Inventario(DdlInventario)

            Session("Tabla") = Nothing
            'If Session("Lista") = "Si" Then
            '    BtnIngresarEq_Click(sender, e)
            '    If Session("Tabla").Rows.Count > 0 Then
            '        lblRegistroRe.Text = "Hay " & Session("Tabla").Rows.Count & " equipo."
            '        gvLista.DataSource = Session("Tabla")
            '        gvLista.DataBind()
            '        lblRegistroRe.Text = "Hay " & gvLista.Rows.Count & " equipo."
            '    Else
            '        gvLista.DataSource = Nothing
            '        gvLista.DataBind()
            '        lblRegistroRe.Text = "No hay equipos."
            '    End If
            'End If
            'Session("Tabla") = Nothing
            Session("Lista") = ""
            Session("Editar") = ""
            Dim Rs As SqlDataReader
            Dim drArt As DataRow
            Dim dt As DataTable
            Dim dtArt As New DataTable
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Cn.Open() : CmdGlobal.Connection = Cn

            If Existe_Tabla("V_TBINV_LISTASERIE_" & Session("User"), Session("Ruta_Emp")) = False Then
                CmdGlobal.CommandText = " CREATE TABLE [DBO].[V_TBINV_LISTASERIE_" & Session("User") & "] ([CORRELATIVO] [FLOAT], [ART_CODIGO] [FLOAT], [SERIE_NUMERAR] [FLOAT], [SERIE_NRO] [VARCHAR] (150), [PLACA_NRO] [FLOAT], [BIEN_NUEVO] [VARCHAR] (1), [PLACA_TSG] [VARCHAR] (1), [CANTIDAD] [FLOAT]) On [PRIMARY]"
                CmdGlobal.ExecuteNonQuery()
            End If
            dtArt.Columns.Add("c1")
            dtArt.Columns.Add("c2")
            dtArt.Columns.Add("c3")
            dtArt.Columns.Add("c4")
            dtArt.Columns.Add("c5")
            dtArt.Columns.Add("c6")
            dtArt.Columns.Add("c7")
            dtArt.Columns.Add("c8")
            dtArt.Columns.Add("c9")
            dtArt.Columns.Add("c10")
            dtArt.Columns.Add("c11")
            dtArt.Columns.Add("c12")
            dtArt.Columns.Add("c13")
            dtArt.Columns.Add("c14")
            Dim psCodReg As String = ""

            CmdGlobal.CommandText = " Select SERIE_NUMERAR,SERIE_NRO, PLACA_NRO, A.ART_CODIGO, A.ART_DESCRIPCION,ART_CODEQUIVA,BIEN_NUEVO,PLACA_TSG, CANTIDAD " _
                                  & " FROM V_TBINV_LISTASERIE_" & Session("User") & " As U INNER JOIN  TBINV_ARTICULOS As A On A.ART_CODIGO = U.ART_CODIGO " _
                                  & " ORDER BY CORRELATIVO DESC"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    dt = ObjLista.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), Nu(Rs("SERIE_NRO")), Nz(Rs("PLACA_NRO")))
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            drArt = dtArt.NewRow()
                            drArt("c1") = Nu(dr("COD_ARTICULO"))
                            drArt("c2") = Nu(dr("ART_DESCRIPCION"))
                            drArt("c3") = Nu(dr("SERIE_NRO"))
                            drArt("c4") = Nu(dr("PLACA_NRO"))
                            drArt("c5") = Nu(dr("TIPOBIEN"))
                            drArt("c6") = Nu(dr("TIPO_UBICACION"))
                            drArt("c7") = Nu(dr("COD_ALMACEN"))
                            drArt("c8") = Nu(dr("ALMACEN_NOMBRE"))
                            drArt("c9") = Nu(dr("UBICACT_CODIGO"))
                            drArt("c10") = Nu(dr("SERIE_NUMERAR"))
                            drArt("c11") = Nu(dr("UBICACT_TIPO"))
                            drArt("c12") = ""
                            drArt("c13") = Nu(dr("SERIE_CUSTODIA_CCOSTO"))
                            drArt("c14") = Nu(dr("ART_TIPO"))
                            dtArt.Rows.Add(drArt)
                        Next
                    End If
                End While
            End If

            gvLista.DataSource = dtArt
            gvLista.DataBind()

            'Session("psSerieNumerar") = ""
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Private Sub Llena_Inventario(ByVal combo As DropDownList)
        'Lista_Ubicaciones
        Dim obj As New clsInv_Listados
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.Lista_Inventario2(Session("Ruta_Emp"), Session("CodEmpresa"))
        combo.DataTextField = "INVENT_DESCRIPCION"
        combo.DataValueField = "INVENT_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
    End Sub
    Public Property Tabla As DataTable
        Get
            If Session("Tabla") Is Nothing Then
                Session("Tabla") = New DataTable()
                Return CType(Session("Tabla"), DataTable)
            Else
                Return CType(Session("Tabla"), DataTable)
            End If
        End Get
        Set(value As DataTable)
            Session("Tabla") = value
        End Set
    End Property
    Protected Sub BtnIngresarEq_Click(sender As Object, e As EventArgs) Handles BtnIngresarEq.Click
        DivInv.Visible = True
        lblError.Text = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "Select MAX(INVENT_CODIGO) FROM TBINVENTARIO "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtInvCod.Text = Nz(Rs(0)) + 1
                End While
            Else
                txtInvCod.Text = 1
            End If
            Rs.Close()
            txtInvCod.Text = Llenar_Ceros(txtInvCod.Text, 4)
            txtInvNombre.Text = ""
            txtPlaca.Text = ""
            txtNroSerie.Text = ""
            lblError.Text = ""
            lblRegistroRe.Text = ""
            txtDUbicacion.Text = ""
            txtDCodigo.Text = ""
            txtDDescripcion.Text = ""
            btnOpen.Visible = False
            BtnNo.Visible = False
            gvLista.DataSource = Nothing
            gvLista.DataBind()
            BtnInvBuscar.Visible = False
            DdlInventario.Visible = True
            Label2.Visible = True
            Panel1.Visible = False
            Me.Page.Session.Timeout = 1080

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub optUbicacionD_SelectedIndexChanged(sender As Object, e As EventArgs) Handles optUbicacionD.SelectedIndexChanged

        txtDUbicacion.Text = ""
        txtDCodigo.Text = ""
        txtDDescripcion.Text = ""
        lblError.Text = ""
        lblRegistroRe.Text = ""
        Me.Page.Session.Timeout = 1080
        If optUbicacionD.SelectedValue = "0" Then
            btnUbica.Enabled = False
        ElseIf optUbicacionD.SelectedValue = "1" Then
            lblBusUbica.Text = "Busqueda de Almacén"
            btnUbica.Enabled = True
        ElseIf optUbicacionD.SelectedValue = "2" Then
            lblBusUbica.Text = "Busqueda de Centro de Costos"
            btnUbica.Enabled = True
        End If
    End Sub

    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiCerrar.Click
        ModalPopupExtender2.Hide()
        txtDUbicacion.Text = ""
        txtDCodigo.Text = ""
        txtDDescripcion.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            FlexUbicacion.DataBind()
            If optUbicacionD.SelectedValue.Trim = "2" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            ElseIf optUbicacionD.SelectedValue.Trim = "1" Then
                If txtBusCod.Text.Trim <> "" Then pdCodAlmacen = txtBusCod.Text.Trim
                FlexUbicacion.DataSource = obj.Lista_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            End If
            Me.Page.Session.Timeout = 1080
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            Me.Page.Session.Timeout = 1080
            txtDUbicacion.Text = ""
            txtDCodigo.Text = ""
            txtDDescripcion.Text = ""
            txtDCodigo.Text = FlexUbicacion.Rows(Index).Cells(1).Text
            txtDDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click
        Dim obj As New clsInv_Listados
        Dim dt As DataTable = Nothing
        Dim psNroPlaca As Double = 0
        Dim dtArt As New DataTable
        lblError.Text = ""
        lblError.Text = ""
        btnOpen.Visible = False
        BtnNo.Visible = False

        If gvLista.Rows.Count > 0 Then
            For i = 0 To gvLista.Rows.Count - 1
                If txtPlaca.Text.Trim = gvLista.Rows(i).Cells(4).Text Then
                    lblError.Text = "Ya se ingreso el equipo." : Exit Sub
                End If
            Next
        End If

        Try
            If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
            dt = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroSerie.Text.Trim, psNroPlaca)
            If dt.Rows.Count > 0 Then
                If txtPlaca.Text <> "" Or txtNroSerie.Text <> "" Then
                    Session("NroSerie") = txtNroSerie.Text
                    Session("NorPlaca") = txtPlaca.Text
                    Session("Nuevo") = "No"
                    lblRegistroRe.Text = "¿Deseas verificar los datos del activo?"
                    btnOpen.Visible = True
                    BtnNo.Visible = True
                    Exit Sub
                End If
            Else
                If txtPlaca.Text <> "" Or txtNroSerie.Text <> "" Then
                    Session("NroSerie") = txtNroSerie.Text
                    Session("NorPlaca") = txtPlaca.Text
                    Session("Nuevo") = "Si"
                    Session("psSerieNumerar") = ""
                    lblRegistroRe.Text = "No se encontró el equipo.¿Desea ingresarlo?"
                    btnOpen.Visible = True
                    BtnNo.Visible = True
                    Exit Sub
                End If
            End If
            txtPlaca.Text = ""
            txtNroSerie.Text = ""
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub

    Private Sub Agregar_Activo()
        Dim obj As New clsInv_Listados
        Dim dt As DataTable = Nothing
        Dim psNroPlaca As Double = 0
        Dim drArt As DataRow
        Dim dtArt As New DataTable
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        lblError.Text = ""
        dtArt.Columns.Add("c1")
        dtArt.Columns.Add("c2")
        dtArt.Columns.Add("c3")
        dtArt.Columns.Add("c4")
        dtArt.Columns.Add("c5")
        dtArt.Columns.Add("c6")
        dtArt.Columns.Add("c7")
        dtArt.Columns.Add("c8")
        dtArt.Columns.Add("c9")
        dtArt.Columns.Add("c10")
        dtArt.Columns.Add("c11")
        dtArt.Columns.Add("c12")
        dtArt.Columns.Add("c13")
        dtArt.Columns.Add("c14")
        lblError.Text = ""
        btnOpen.Visible = False
        BtnNo.Visible = False

        If gvLista.Rows.Count > 0 Then
            For i = 0 To gvLista.Rows.Count - 1
                If txtPlaca.Text.Trim = gvLista.Rows(i).Cells(4).Text Then
                    lblError.Text = "Ya se ingreso el equipo." : Exit Sub
                End If
            Next
        End If

        If gvLista.Rows.Count > 0 Then
            For i = 0 To gvLista.Rows.Count - 1
                drArt = dtArt.NewRow()
                drArt("c1") = gvLista.Rows(i).Cells(1).Text
                drArt("c2") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c3") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c4") = gvLista.Rows(i).Cells(4).Text
                drArt("c5") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c6") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c7") = gvLista.Rows(i).Cells(7).Text
                drArt("c8") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c9") = gvLista.Rows(i).Cells(9).Text
                drArt("c10") = gvLista.Rows(i).Cells(10).Text
                drArt("c11") = gvLista.Rows(i).Cells(11).Text
                drArt("c12") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c13") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                drArt("c14") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvLista.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                dtArt.Rows.Add(drArt)
            Next
        End If
        Session("Tabla") = dtArt
        Dim pscodReg As String = ""
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
            dt = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroSerie.Text.Trim, psNroPlaca)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    drArt = dtArt.NewRow()
                    drArt("c1") = Nu(dr("COD_ARTICULO"))
                    drArt("c2") = Nu(dr("ART_DESCRIPCION"))
                    drArt("c3") = Nu(dr("SERIE_NRO"))
                    drArt("c4") = Nu(dr("PLACA_NRO"))
                    drArt("c5") = Nu(dr("TIPOBIEN"))
                    drArt("c6") = Nu(dr("TIPO_UBICACION"))
                    drArt("c7") = Nu(dr("COD_ALMACEN"))
                    drArt("c8") = Nu(dr("ALMACEN_NOMBRE"))
                    drArt("c9") = Nu(dr("UBICACT_CODIGO"))
                    drArt("c10") = Nu(dr("SERIE_NUMERAR"))
                    drArt("c11") = Nu(dr("UBICACT_TIPO"))
                    drArt("c12") = ""
                    drArt("c13") = Nu(dr("SERIE_CUSTODIA_CCOSTO"))
                    drArt("c14") = Nu(dr("ART_TIPO"))
                    dtArt.Rows.Add(drArt)

                    CmdGlobal.CommandText = " Select max(CORRELATIVO) FROM V_TBINV_LISTASERIE_" & Session("User")
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            pscodReg = Nz(Rs(0)) + 1
                        End While
                    Else
                        pscodReg = 1
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = " Select * FROM V_TBINV_LISTASERIE_" & Session("User") & " WHERE SERIE_NUMERAR = " & Nu(dr("SERIE_NUMERAR"))
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        Rs.Close()
                    Else
                        Rs.Close()
                        CmdGlobal.CommandText = " INSERT INTO V_TBINV_LISTASERIE_" & Session("User") & " (CORRELATIVO, ART_CODIGO,SERIE_NUMERAR, SERIE_NRO, CANTIDAD) " _
                                              & " VALUES (" & pscodReg & "," & Nu(dr!COD_ARTICULO) & "," & Nu(dr!Serie_Numerar) & ", '" & Nu(dr!Serie_Nro) & "',1) "
                        CmdGlobal.ExecuteNonQuery()
                        If Nu(Rs!placa_Nro) <> "" Then
                    CmdGlobal.CommandText = " UPDATE V_TBINV_LISTASERIE_" & Session("User") & " SET PLACA_NRO = " & Nu(dr!placa_Nro) & " WHERE CORRELATIVO = " & pscodReg
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If
            Next
            End If
            If dtArt.Rows.Count > 0 Then
                lblRegistroRe.Text = "Hay " & dtArt.Rows.Count & " equipo."
                gvLista.DataSource = dtArt
                gvLista.DataBind()
                lblRegistroRe.Text = "Hay " & gvLista.Rows.Count & " equipo."
            Else
                gvLista.DataSource = dt
                gvLista.DataBind()
                lblRegistroRe.Text = "No hay equipos."
            End If
            txtPlaca.Text = ""
            txtNroSerie.Text = ""
            Session("NroSerie") = txtNroSerie.Text
            Session("NorPlaca") = txtPlaca.Text
            lblRegistroRe.Text = ""
            btnOpen.Visible = True
            BtnNo.Visible = True
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub
    Private Sub Llenar_Detalle()
        Dim obj As New clsInv_Listados
        Dim dt As DataTable = Nothing
        Dim psNroPlaca As Double = 0
        Dim drArt As DataRow
        Dim dtArt As New DataTable
        lblError.Text = ""
        dtArt.Columns.Add("c1")
        dtArt.Columns.Add("c2")
        dtArt.Columns.Add("c3")
        dtArt.Columns.Add("c4")
        dtArt.Columns.Add("c5")
        dtArt.Columns.Add("c6")
        dtArt.Columns.Add("c7")
        dtArt.Columns.Add("c8")
        dtArt.Columns.Add("c9")
        dtArt.Columns.Add("c10")
        dtArt.Columns.Add("c11")
        dtArt.Columns.Add("c12")
        dtArt.Columns.Add("c13")
        dtArt.Columns.Add("c14")
        lblError.Text = ""

        Try
            'Lista_Inventario_UbicDetalle
            Dim dt2 As DataTable
            dt2 = obj.Lista_Inventario_UbicDetalle(Session("Ruta_Emp"), Session("CodEmpresa"), txtInvCodUbic.Text)
            If dt2.Rows.Count > 0 Then
                For Each dr2 As DataRow In dt2.Rows
                    txtNroSerie.Text = Nu(dr2("INVDET_SERIE_NRO"))
                    dt = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroSerie.Text.Trim, psNroPlaca)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If Nu(dr2("INVDET_SERIE_NUMERAR")) = Nu(dr("SERIE_NUMERAR")) Then
                                drArt = dtArt.NewRow()
                                drArt("c1") = Nu(dr("COD_ARTICULO"))
                                drArt("c2") = Nu(dr("ART_DESCRIPCION"))
                                drArt("c3") = Nu(dr("SERIE_NRO"))
                                drArt("c4") = Nu(dr("PLACA_NRO"))
                                drArt("c5") = Nu(dr("TIPOBIEN"))
                                drArt("c6") = Nu(dr("TIPO_UBICACION"))
                                drArt("c7") = Nu(dr("COD_ALMACEN"))
                                drArt("c8") = Nu(dr("ALMACEN_NOMBRE"))
                                drArt("c9") = Nu(dr("UBICACT_CODIGO"))
                                drArt("c10") = Nu(dr("SERIE_NUMERAR"))
                                drArt("c11") = Nu(dr("UBICACT_TIPO"))
                                drArt("c12") = "X"
                                drArt("c13") = Nu(dr("SERIE_CUSTODIA_CCOSTO"))
                                drArt("c14") = Nu(dr("ART_TIPO"))
                                dtArt.Rows.Add(drArt)
                            End If
                        Next
                    End If
                Next
            End If
            gvLista.DataSource = Nothing
            gvLista.DataBind()
            If dtArt.Rows.Count > 0 Then
                lblRegistroRe.Text = "Hay " & dtArt.Rows.Count & " equipo."
                gvLista.DataSource = dtArt
                gvLista.DataBind()
            End If
            txtPlaca.Text = ""
            txtNroSerie.Text = ""
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub
    Private Sub gvLista_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLista.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Dim drArt As DataRow
        Dim psSerieNumerar As String = ""
        Dim i As Long = 0
        Dim dtArt As New DataTable
        Dim dt As DataTable
        dt = Nothing
        dtArt.Columns.Add("c1")
        dtArt.Columns.Add("c2")
        dtArt.Columns.Add("c3")
        dtArt.Columns.Add("c4")
        dtArt.Columns.Add("c5")
        dtArt.Columns.Add("c6")
        dtArt.Columns.Add("c7")
        dtArt.Columns.Add("c8")
        dtArt.Columns.Add("c9")
        dtArt.Columns.Add("c10")
        dtArt.Columns.Add("c11")
        dtArt.Columns.Add("c12")
        dtArt.Columns.Add("c13")
        dtArt.Columns.Add("c14")
        Try
            If e.CommandName = "Quitar" Then
                psSerieNumerar = gvLista.Rows(Index).Cells(10).Text
                For i = 0 To gvLista.Rows.Count - 1
                    If gvLista.Rows(i).Cells(10).Text <> psSerieNumerar Then
                        drArt = dtArt.NewRow()
                        drArt("c1") = gvLista.Rows(i).Cells(1).Text
                        drArt("c2") = gvLista.Rows(i).Cells(2).Text
                        drArt("c3") = gvLista.Rows(i).Cells(3).Text
                        drArt("c4") = gvLista.Rows(i).Cells(4).Text
                        drArt("c5") = gvLista.Rows(i).Cells(5).Text
                        drArt("c6") = gvLista.Rows(i).Cells(6).Text
                        drArt("c7") = gvLista.Rows(i).Cells(7).Text
                        drArt("c8") = gvLista.Rows(i).Cells(8).Text
                        drArt("c9") = gvLista.Rows(i).Cells(9).Text
                        drArt("c10") = gvLista.Rows(i).Cells(10).Text
                        drArt("c11") = gvLista.Rows(i).Cells(11).Text
                        drArt("c12") = gvLista.Rows(i).Cells(12).Text
                        drArt("c13") = gvLista.Rows(i).Cells(13).Text
                        drArt("c14") = gvLista.Rows(i).Cells(14).Text
                        dtArt.Rows.Add(drArt)
                    End If
                Next
                If dtArt.Rows.Count > 0 Then
                    lblRegistroRe.Text = "Hay " & dtArt.Rows.Count & " equipo."
                    gvLista.DataSource = dtArt
                    gvLista.DataBind()
                Else
                    gvLista.DataSource = dt
                    gvLista.DataBind()
                    lblRegistroRe.Text = "No hay equipos."
                End If
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Finally
        End Try
    End Sub
    Protected Sub BtnGrabarInv_Click(sender As Object, e As EventArgs) Handles BtnGrabarInv.Click
        lblError.Text = ""
        lblError.ForeColor = System.Drawing.Color.Red
        If DdlInventario.Text = "" Then lblError.Text = "Seleccionar inventario" : Exit Sub
        If gvLista.Rows.Count = 0 Then lblError.Text = "Ingresar equipos al inventario." : Exit Sub
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim ValorSys As String = ""
        Dim psCodInv As String = ""
        Dim psCodInvUbic As String = ""
        Try
            ValorSys = Session("User") + FechaActual() + HoraActual()
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2


            Dim i As Long = 0
            If Session("Editar") = "" Then
                CmdGlobal2.CommandText = "SELECT MAX(INVENTUBIC_CODIGO) FROM TBINVENTARIO_UBICACIONES "
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        psCodInvUbic = Nz(Rs2(0)) + 1
                    End While
                Else
                    psCodInvUbic = 1
                End If
                Rs2.Close()

                For i = 0 To gvLista.Rows.Count - 1
                    If gvLista.Rows(i).Cells(12).Text = "" Or gvLista.Rows(i).Cells(12).Text = "&nbsp;" Then
                        CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_UBICACIONES WHERE INVENTUBIC_NRO = '" & DdlInventario.SelectedValue & "' AND (INVENTUBIC_UBIC_TIPO = '2') " _
                                          & " AND (INVENTUBIC_UBIC_CODIGO = '" & gvLista.Rows(i).Cells(13).Text & "') AND (INVENTUBIC_SYS_EST = '0') AND (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psCodInvUbic = Nu(Rs!INVENTUBIC_CODIGO)
                            End While
                            Rs.Close()
                        Else
                            Rs.Close()
                            CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_UBICACIONES (EMPRESA_CODIGO, INVENTUBIC_CODIGO, INVENTUBIC_NRO, INVENTUBIC_UBIC_TIPO, " _
                                      & " INVENTUBIC_UBIC_CODIGO,INVENTUBIC_RESPONSABLE, INVENTUBIC_ESTADO, INVENTUBIC_SYS_EST, INVENTUBIC_SYS_CRE)" _
                                      & " VALUES ('" & Session("CodEmpresa") & "'," & psCodInvUbic & "," & DdlInventario.SelectedValue & ",'2'," _
                                      & " " & gvLista.Rows(i).Cells(13).Text & ", '', '2', '0', '" & ValorSys & "')"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                        CmdGlobal.CommandText = " DELETE FROM TBINVENTARIO_DETALLE WHERE INVDET_SERIE_NUMERAR='" & gvLista.Rows(i).Cells(10).Text & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO, INVDET_INVENTUBIC_CODIGO, INVDET_FECHA, INVDET_ART_CODIGO, " _
                                              & " INVDET_SERIE_NUMERAR, INVDET_SERIE_NRO, INVDET_ART_TIPO, INVDET_CANTIDAD, INVDET_CANT_HAY, " _
                                              & " INVDET_UBIC_TIPO, INVDET_UBIC_CODIGO, INVDET_ESTADO_INGRESO, INVDET_ESTADO_INVENTARIO, " _
                                              & " INVDET_SYS_EST, INVDET_SYS_CRE ) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "', " & psCodInvUbic & ", '" & FechaActual() & "'," & gvLista.Rows(i).Cells(1).Text & ", " _
                                              & " " & gvLista.Rows(i).Cells(10).Text & ", '" & gvLista.Rows(i).Cells(3).Text & "', '" & gvLista.Rows(i).Cells(14).Text & "', 1,1," _
                                              & " '" & gvLista.Rows(i).Cells(11).Text & "', " & gvLista.Rows(i).Cells(9).Text & ", '1', '1', '0' ,'" & ValorSys & "')"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    'SALIDA E INGRESO AUTOMATICO
                    Call ObjProceso.Salida_Ingreso_Automatico(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), gvLista.Rows(i).Cells(11).Text,
                                                              "2", gvLista.Rows(i).Cells(9).Text, gvLista.Rows(i).Cells(13).Text, gvLista.Rows(i).Cells(10).Text,
                                                              gvLista.Rows(i).Cells(1).Text)
                Next
                i = 0
            End If
            lblError.Text = "Inventario Nro. " & psCodInv & " de la ubicación " & DdlInventario.Text & " se realizó con exito"
            lblError.ForeColor = System.Drawing.Color.Maroon
            DivInv.Visible = False
            Me.Page.Session.Timeout = 1080
            Session("Editar") = ""
        Catch ex As SqlException
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        End Try
    End Sub
    Protected Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        Call Agregar_Activo()
        lblRegistroRe.Text = "Hay " & gvLista.Rows.Count & " equipos."
        btnOpen.Visible = False
        BtnNo.Visible = False
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnInvCerrar_Click(sender As Object, e As EventArgs) Handles btnInvCerrar.Click
        ModalPopupExtender1.Hide()
        txtInvCod.Text = ""
        txtInvNombre.Text = ""
        txtDUbicacion.Text = ""
        gvListaInv.DataSource = Nothing
        gvListaInv.DataBind()
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnInvListar_Click(sender As Object, e As EventArgs) Handles btnInvListar.Click
        'Lista_Inventario
        Try
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            gvListaInv.DataSource = obj.Lista_Inventario(Session("Ruta_Emp"), Session("CodEmpresa"))
            gvListaInv.DataBind()
            Me.Page.Session.Timeout = 1080
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnInvSeguir_Click(sender As Object, e As EventArgs) Handles BtnInvSeguir.Click
        DivInv.Visible = True
        lblError.Text = ""
        Try
            Session("Editar") = "s"
            DdlInventario.Visible = False
            Label2.Visible = False
            txtInvCod.Text = ""
            txtInvNombre.Text = ""
            txtPlaca.Text = ""
            txtNroSerie.Text = ""
            lblError.Text = ""
            lblRegistroRe.Text = ""
            txtDUbicacion.Text = ""
            txtDCodigo.Text = ""
            txtDDescripcion.Text = ""
            btnOpen.Visible = False
            BtnNo.Visible = False
            gvLista.DataSource = Nothing
            gvLista.DataBind()
            BtnInvBuscar.Visible = True

            Panel1.Visible = True
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub BtnInvBuscar_Click(sender As Object, e As EventArgs) Handles BtnInvBuscar.Click
        '
    End Sub
    Private Sub gvListaInv_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvListaInv.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtDUbicacion.Text = ""
            txtDCodigo.Text = ""
            txtDDescripcion.Text = ""
            txtInvCodUbic.Text = gvListaInv.Rows(Index).Cells(7).Text
            optUbicacionD.SelectedValue = gvListaInv.Rows(Index).Cells(6).Text
            txtInvCod.Text = gvListaInv.Rows(Index).Cells(1).Text
            txtInvNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaInv.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaInv.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaInv.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDUbicacion.Text = gvListaInv.Rows(Index).Cells(5).Text
            gvListaInv.DataSource = Nothing
            gvListaInv.DataBind()
            Me.Page.Session.Timeout = 1080
            Call Llenar_Detalle()
            gvLista.Visible = True
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Private Sub BtnGrabarInv_Disposed(sender As Object, e As EventArgs) Handles BtnGrabarInv.Disposed

    End Sub

End Class
