Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports System
Partial Class AdminProblemas_Define_Personas
    Inherits System.Web.UI.Page
    Protected Sub cmdListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListar.Click
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Try
            Dim dtListado As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_Personas(Session("Ruta_Emp"), Session("CodEmpresa"))
            Flex.DataSource = dtListado
            Flex.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Title = "Mesa de Ayuda - Registro Personas"
            Try
                Call Cargar_Oficina()
                Call Cargar_Puesto()
                Call Cargar_Territorio()
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Private Sub Cargar_Oficina()
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        cboOficina.Items.Clear()
        Try
            dt = obj.MALista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"))
            cboOficina.DataSource = dt
            cboOficina.DataTextField = "DESCRIPCION"
            cboOficina.DataValueField = "AOFICINA_CODIGO"
            cboOficina.DataBind()
            cboOficina.Items.Add("< Seleccionar >") : cboOficina.SelectedValue = "< Seleccionar >"
            dt = Nothing
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Cargar_Puesto()
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        Try
            cboPuesto.Items.Clear()
            dt = obj.MALista_Puesto(Session("Ruta_Emp"), Session("CodEmpresa"))
            cboPuesto.DataSource = dt
            cboPuesto.DataTextField = "DESCRIPCION"
            cboPuesto.DataValueField = "APUESTO_CODIGO"
            cboPuesto.DataBind()
            cboPuesto.Items.Add("< Seleccionar >") : cboPuesto.SelectedValue = "< Seleccionar >"
            dt = Nothing
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Cargar_Territorio()
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        cboTerrotorio.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT TERRI_CODIGO,TERRI_CODINTERNO+' - '+TERRI_NOMBRE AS DESCRIPCION " _
                              & " FROM dbo.TBADMIN_TERRITORIO WHERE TERRI_SYS_EST = '0' AND EMPRESA_CODIGO ='" & Session("CodEmpresa") & "'"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cboTerrotorio.DataSource = cmdSql.ExecuteReader
            cboTerrotorio.DataTextField = "DESCRIPCION"
            cboTerrotorio.DataValueField = "TERRI_CODIGO"
            cboTerrotorio.DataBind()
            cboTerrotorio.Items.Add("< Seleccionar >") : cboTerrotorio.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Call Limpiar()
        lblEtiqueta.Text = "Ingresar Persona"
        lblError.Text = ""
        btnNuevo.Enabled = False
        lblIngreso.Visible = True
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblIngreso.Visible = True
            Call Limpiar()
            lblEtiqueta.Text = "Edición de Personas"
            txtUsuario.Text = Flex.Rows(Index).Cells(2).Text
            txtCodigo.Text = Flex.Rows(Index).Cells(1).Text
            txtApellidos.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtTelefono.Text = Replace(Flex.Rows(Index).Cells(8).Text, "&nbsp;", "")
            txtAnexo.Text = Replace(Flex.Rows(Index).Cells(9).Text, "&nbsp;", "")
            txtBanca.Text = Replace(Flex.Rows(Index).Cells(15).Text, "&nbsp;", "")
            txtAntiguedad.Text = Replace(Flex.Rows(Index).Cells(14).Text, "&nbsp;", "")
            txtCorreo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If Flex.Rows(Index).Cells(11).Text <> "&nbsp;" Then cboOficina.SelectedValue = Flex.Rows(Index).Cells(11).Text Else cboOficina.SelectedValue = "< Seleccionar >"
            If Flex.Rows(Index).Cells(12).Text <> "&nbsp;" Then cboTerrotorio.SelectedValue = Flex.Rows(Index).Cells(12).Text Else cboTerrotorio.SelectedValue = "< Seleccionar >"
            If Flex.Rows(Index).Cells(13).Text <> "&nbsp;" Then cboPuesto.SelectedValue = Flex.Rows(Index).Cells(13).Text Else cboPuesto.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Private Sub Limpiar()
        lblEtiqueta.Text = ""
        txtUsuario.Text = ""
        txtNombres.Text = ""
        txtApellidos.Text = ""
        txtCorreo.Text = ""
        txtTelefono.Text = ""
        txtAnexo.Text = ""
        txtAntiguedad.Text = ""
        txtBanca.Text = ""
        cboOficina.SelectedValue = "< Seleccionar >"
        cboTerrotorio.SelectedValue = "< Seleccionar >"
        cboPuesto.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        lblEtiqueta.Text = ""
        txtUsuario.Text = ""
        txtNombres.Text = ""
        txtApellidos.Text = ""
        txtCorreo.Text = ""
        txtTelefono.Text = ""
        txtAnexo.Text = ""
        txtAntiguedad.Text = ""
        txtBanca.Text = ""
        cboOficina.SelectedValue = "< Seleccionar >"
        cboTerrotorio.SelectedValue = "< Seleccionar >"
        cboPuesto.SelectedValue = "< Seleccionar >"
        lblIngreso.Visible = False
        btnNuevo.Enabled = True
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Len(txtUsuario.Text) = 0 Then lblError.Text = "Falta ingresar Usuario" : Exit Sub
        If Len(txtNombres.Text) = 0 Then lblError.Text = "Falta ingresar Nombre" : Exit Sub
        If Len(txtApellidos.Text) = 0 Then lblError.Text = "Falta ingresar Apellidos" : Exit Sub
        Dim obj As New clsMesaAyuda
        Dim pCodPersona As Double : pCodPersona = 0
        Dim pAntiguedad As Double : pAntiguedad = 0
        Dim pBanca As Double : pBanca = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Try
            If lblEtiqueta.Text = "Ingresar Persona" Then
                dt = obj.MAConsulta_ExistePersona(txtUsuario.Text.Trim, "", "", "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                If dt.Rows.Count = 0 Then
                    dt2 = obj.MAConsulta_ExistePersona("", txtNombres.Text.Trim, txtApellidos.Text.Trim, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
                    If dt2.Rows.Count = 1 Then
                        If MsgBox("¿El nombre de usuario ya existe, desea ingresar codigo de Usuario.?", vbQuestion + vbYesNo) = vbYes Then
                            obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
                            If cboOficina.SelectedValue <> "< Seleccionar >" Then
                                obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, cboOficina.SelectedValue.Trim, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "4", Session("Ruta_Emp"), Session("CodEmpresa"))
                            End If
                            If cboPuesto.SelectedValue <> "< Seleccionar >" Then
                                obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, cboPuesto.SelectedValue.Trim, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "5", Session("Ruta_Emp"), Session("CodEmpresa"))
                            End If
                            If cboTerrotorio.SelectedValue <> "< Seleccionar >" Then
                                obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, cboTerrotorio.SelectedValue.Trim, "6", Session("Ruta_Emp"), Session("CodEmpresa"))
                            End If
                            If txtAntiguedad.Text.Trim <> "" Then
                                pAntiguedad = txtAntiguedad.Text.Trim
                                obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", pAntiguedad, 0, "7", Session("Ruta_Emp"), Session("CodEmpresa"))
                            End If
                            If txtBanca.Text.Trim <> "0" Or txtBanca.Text.Trim <> "" Then
                                pBanca = txtBanca.Text.Trim
                                obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", pAntiguedad, 0, "8", Session("Ruta_Emp"), Session("CodEmpresa"))
                            End If
                        End If
                    Else
                        obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
                        If cboOficina.SelectedValue <> "< Seleccionar >" Then
                            obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, cboOficina.SelectedValue.Trim, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "4", Session("Ruta_Emp"), Session("CodEmpresa"))
                        End If
                        If cboPuesto.SelectedValue <> "< Seleccionar >" Then
                            obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, cboPuesto.SelectedValue.Trim, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "5", Session("Ruta_Emp"), Session("CodEmpresa"))
                        End If
                        If cboTerrotorio.SelectedValue <> "< Seleccionar >" Then
                            obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, cboTerrotorio.SelectedValue.Trim, "6", Session("Ruta_Emp"), Session("CodEmpresa"))
                        End If
                        If txtAntiguedad.Text.Trim <> "" Then
                            pAntiguedad = txtAntiguedad.Text.Trim
                            obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", pAntiguedad, 0, "7", Session("Ruta_Emp"), Session("CodEmpresa"))
                        End If
                        If txtBanca.Text.Trim <> "0" Or txtBanca.Text.Trim <> "" Then
                            pBanca = txtBanca.Text.Trim
                            obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", pAntiguedad, 0, "8", Session("Ruta_Emp"), Session("CodEmpresa"))
                        End If
                    End If
                    dt2 = Nothing
                Else
                    lblError.Text = "El Codigo de Usuario ya existe." : Exit Sub
                End If
                dt = Nothing
            Else
                pCodPersona = txtCodigo.Text.Trim
                obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "3", Session("Ruta_Emp"), Session("CodEmpresa"))
                If cboOficina.SelectedValue <> "< Seleccionar >" Then
                    obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, cboOficina.SelectedValue.Trim, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "4", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
                If cboPuesto.SelectedValue <> "< Seleccionar >" Then
                    obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, cboPuesto.SelectedValue.Trim, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, 0, "5", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
                If cboTerrotorio.SelectedValue <> "< Seleccionar >" Then
                    obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", 0, cboTerrotorio.SelectedValue.Trim, "6", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
                If txtAntiguedad.Text.Trim <> "0" Or txtAntiguedad.Text.Trim <> "" Then
                    pAntiguedad = txtAntiguedad.Text.Trim
                    obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", pAntiguedad, 0, "7", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
                If txtBanca.Text.Trim <> "0" Or txtBanca.Text.Trim <> "" Then
                    pBanca = txtBanca.Text.Trim
                    obj.MAInsUpd_Personas(pCodPersona, txtUsuario.Text.Trim, txtNombres.Text.Trim, txtApellidos.Text.Trim, 0, 0, txtTelefono.Text.Trim, txtAnexo.Text.Trim, txtCorreo.Text.Trim, pBanca, "", pAntiguedad, 0, "8", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnCancelar_Click(sender, e)
        cmdListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
End Class
