Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class ControlVisitas_ControlVisitas_Puntos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        Dim pdCodGrupo As Double = 0
        If Session("CodGrupoEmpresa") <> "" Then
            pdCodGrupo = Session("CodGrupoEmpresa")
        End If
        lblPCError.Text = ""
        lblAError.Text = ""
        lblPLError.Text = ""
        lblPeCError.Text = ""
        btnPCCancelar_Click(sender, e)
        btnPLCancelar_Click(sender, e)
        btnPeCCancelar_Click(sender, e)
        btnACancelar_Click(sender, e)
        If Ficha.ActiveTabIndex = 0 Then
            Try
                Call Llenar_Combo_Grupo(cboGrupo)
                cboGrupo.SelectedValue = pdCodGrupo
                cboGrupo_SelectedIndexChanged(sender, e)
            Catch ex As SqlException
                lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
            Catch Ex As Exception
                lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
            End Try
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Try
                Call Llenar_Combo_Grupo(cboPLGrupo)
                cboPLGrupo.SelectedValue = pdCodGrupo
                cboPLGrupo_SelectedIndexChanged(sender, e)
            Catch ex As SqlException
                lblPLError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
            Catch Ex As Exception
                lblPLError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
            End Try
        End If
        If Ficha.ActiveTabIndex = 2 Then
            Try
                Call Llenar_Combo_Grupo(cboPeCGrupo)
                cboPeCGrupo.SelectedValue = pdCodGrupo
                cboPeCGrupo_SelectedIndexChanged(sender, e)
            Catch ex As SqlException
                lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
            Catch Ex As Exception
                lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
            End Try
        End If
        If Ficha.ActiveTabIndex = 3 Then
            Try
                Call Llenar_Combo_Grupo(cboAGrupo)
                cboAGrupo.SelectedValue = pdCodGrupo
                cboAGrupo_SelectedIndexChanged(sender, e)
            Catch ex As SqlException
                lblAError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
            Catch Ex As Exception
                lblAError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
            End Try
        End If
    End Sub
    Private Sub Llenar_Combo_Grupo(ByVal cbo As DropDownList)
        Dim objSeg As New ModuloSeguridad
        cbo.Items.Clear()
        cbo.DataSource = objSeg.Lista_GrupoEmpresa(HttpContext.Current.User.Identity.Name, "1")
        cbo.DataTextField = "GE_NOMBRE"
        cbo.DataValueField = "GRPOEMPRESA_CODIGO"
        cbo.DataBind()
    End Sub
    Private Sub Llenar_Combo_Empresa(ByVal cbo As DropDownList, ByVal psCodGrupo As String)
        Dim objSeg As New ModuloSeguridad
        Dim CodGrupoEmp As Double
        CodGrupoEmp = psCodGrupo
        cbo.Items.Clear()
        cbo.DataSource = objSeg.Lista_Empresa(HttpContext.Current.User.Identity.Name, CodGrupoEmp, "1")
        cbo.DataTextField = "GEE_NOMBRE"
        cbo.DataValueField = "EMPRESA_CODIGO"
        cbo.DataBind()
    End Sub
    Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        lblPCError.Text = ""
        Try
            Call Llenar_Combo_Empresa(cboEmpresa, cboGrupo.SelectedValue.Trim)
            cboEmpresa.SelectedValue = Session("CodEmpresa")
            If cboEmpresa.SelectedValue <> "< Seleccionar >" Then cboEmpresa_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmpresa.SelectedIndexChanged
        lblPCError.Text = ""
        Call LLenar_PuntosControl()
    End Sub
    Private Sub LLenar_PuntosControl()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("c0")
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            dtListado.Columns.Add("c5")
            dtListado.Columns.Add("c6")
            dtListado.Columns.Add("c7")
            cmdGlobal.CommandText = " SELECT RIGHT('000' + CONVERT(VARCHAR(5), PC.PCONTROL_CODIGO), 3) AS PTO_CODIGO,PC.PCONTROL_CODIGO, PC.AGENCIA_CODIGO, " _
                                  & " AG.AGENCIA_NOMBRE, AG.AGENCIA_CODIGO, PC.PCONTROL_PISO,PC.PCONTROL_UBICACION,PC.PCONTROL_DESCRIPCION " _
                                  & " FROM TBPUNTOSCONTROL PC INNER JOIN TBAGENCIAS AG ON PC.AGENCIA_CODIGO = AG.AGENCIA_CODIGO " _
                                  & " WHERE (PC.PCONTROL_SYS_EST = '0') AND (AG.AGENCIA_SYS_EST = '0') " _
                                  & " AND PC.GRPOEMPRESA_CODIGO=" & cboGrupo.SelectedValue.Trim & " " _
                                  & " AND PC.EMPRESA_CODIGO='" & cboEmpresa.SelectedValue.Trim & "' " _
                                  & " ORDER BY AGENCIA_NOMBRE,PCONTROL_PISO "
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = i
                    drT("c1") = Nu(Rs!PTO_CODIGO)
                    drT("c2") = Nu(Rs!AGENCIA_NOMBRE)
                    drT("c3") = Nu(Rs!PCONTROL_PISO)
                    drT("c4") = Nu(Rs!PCONTROL_UBICACION)
                    drT("c5") = Nu(Rs!PCONTROL_DESCRIPCION)
                    drT("c6") = Nu(Rs!AGENCIA_CODIGO)
                    drT("c7") = Nu(Rs!PCONTROL_CODIGO)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexPC.DataSource = dtListado
            FlexPC.DataBind()
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboAGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAGrupo.SelectedIndexChanged
        lblAError.Text = ""
        Try
            Call Llenar_Combo_Empresa(cboAEmpresa, cboAGrupo.SelectedValue.Trim)
            cboAEmpresa.SelectedValue = Session("CodEmpresa")
            If cboAEmpresa.SelectedValue <> "< Seleccionar >" Then cboAEmpresa_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblAError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblAError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAEmpresa.SelectedIndexChanged
        lblAError.Text = ""
        Call Llenar_Agencias()
    End Sub
    Private Sub Llenar_Agencias()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("c0")
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            cmdGlobal.CommandText = " SELECT RIGHT('00' + CONVERT(VARCHAR(5), AGENCIA_CODIGO), 2) AS AGE_CODIGO, AGENCIA_CODIGO, AGENCIA_NOMBRE " _
                                  & " FROM TBAGENCIAS " _
                                  & " WHERE AGENCIA_SYS_EST='0' " _
                                  & " AND GRPOEMPRESA_CODIGO=" & cboAGrupo.SelectedValue.Trim & " " _
                                  & " AND EMPRESA_CODIGO='" & cboAEmpresa.SelectedValue.Trim & "' " _
                                  & " ORDER BY AGENCIA_NOMBRE"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = i
                    drT("c1") = Nu(Rs!AGE_CODIGO)
                    drT("c2") = Nu(Rs!AGENCIA_NOMBRE)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexA.DataSource = dtListado
            FlexA.DataBind()
        Catch ex As SqlException
            lblAError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblAError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboPLGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPLGrupo.SelectedIndexChanged
        lblPLError.Text = ""
        Try
            Call Llenar_Combo_Empresa(cboPLEmpresa, cboPLGrupo.SelectedValue.Trim)
            cboEmpresa.SelectedValue = Session("CodEmpresa")
            If cboPLEmpresa.SelectedValue <> "< Seleccionar >" Then cboPLEmpresa_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblPLError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPLError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboPLEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPLEmpresa.SelectedIndexChanged
        lblPLError.Text = ""
        Call LLenar_Personal_Labora()
    End Sub
    Private Sub LLenar_Personal_Labora()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            dtListado.Columns.Add("c5")
            dtListado.Columns.Add("c6")
            cmdGlobal.CommandText = " SELECT RIGHT('000' + CONVERT(VARCHAR(5), PPL.PCONTROL_CODIGO), 3) AS PTO_CODIGO, PPL.PCONTROL_CODIGO, A.AGENCIA_NOMBRE,PC.PCONTROL_PISO, PC.PCONTROL_UBICACION,PPL.PERSON_LABORA_CODIGO,P.PERSON_APEPAT + ' ' + P.PERSON_APEMAT + ', ' + P.PERSON_NOMBRES AS NOMBRESP" _
                                  & " FROM TBPTOCONTROL_PERSONALLAB PPL INNER JOIN TBPUNTOSCONTROL PC ON PPL.PCONTROL_CODIGO = PC.PCONTROL_CODIGO INNER JOIN TBPERSONAL P ON PPL.PERSON_LABORA_CODIGO = P.PERSON_CODIGO" _
                                  & " INNER JOIN TBAGENCIAS A ON PC.AGENCIA_CODIGO = A.AGENCIA_CODIGO INNER JOIN TBPERSONAL_EMPRESAS PE ON P.PERSON_CODIGO=PE.PERSONAL_CODIGO WHERE (P.PERSON_SYS_EST = '0') AND " _
                                  & " (PC.PCONTROL_SYS_EST = '0') AND (A.AGENCIA_SYS_EST = '0') AND PE.GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & " AND PE.EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "'" _
                                  & " ORDER BY A.AGENCIA_NOMBRE,PC.PCONTROL_PISO, PC.PCONTROL_UBICACION,NOMBRESP"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i
                    drT("c2") = Nu(Rs!PERSON_LABORA_CODIGO)
                    drT("c3") = Nu(Rs!NOMBRESP)
                    drT("c4") = Nu(Rs!PTO_CODIGO)
                    drT("c5") = Nu(Rs!AGENCIA_NOMBRE) & " " & Nu(Rs!PCONTROL_PISO) & " " & Nu(Rs!PCONTROL_UBICACION)
                    drT("c6") = Nu(Rs!PCONTROL_CODIGO)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexPL.DataSource = dtListado
            FlexPL.DataBind()
        Catch ex As SqlException
            lblPLError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPLError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboPeCGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeCGrupo.SelectedIndexChanged
        lblPeCError.Text = ""
        Try
            Call Llenar_Combo_Empresa(cboPeCEmpresa, cboPeCGrupo.SelectedValue.Trim)
            cboEmpresa.SelectedValue = Session("CodEmpresa")
            If cboPeCEmpresa.SelectedValue <> "< Seleccionar >" Then cboPeCEmpresa_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblPeCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPeCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboPeCEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeCEmpresa.SelectedIndexChanged
        lblPeCError.Text = ""
        Call LLenar_Personal_Controla()
    End Sub
    Private Sub LLenar_Personal_Controla()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            cmdGlobal.CommandText = " SELECT PERSON_CONTROLA_CODIGO,P.PERSON_APEPAT + ' ' + P.PERSON_APEMAT + ', ' + P.PERSON_NOMBRES AS NOMBRESP" _
                                  & " FROM TBPERSONAL_CONTROLA PC INNER JOIN TBPERSONAL P ON PC.PERSON_CONTROLA_CODIGO = P.PERSON_CODIGO INNER JOIN TBPERSONAL_EMPRESAS PE" _
                                  & " ON PE.PERSONAL_CODIGO=P.PERSON_CODIGO AND PC.GRPOEMPRESA_CODIGO = PE.GRPOEMPRESA_CODIGO WHERE (P.PERSON_SYS_EST = '0') AND PC.GRPOEMPRESA_CODIGO=" & cboPeCGrupo.SelectedValue.Trim & " AND " _
                                  & " PC.EMPRESA_CODIGO='" & cboPeCEmpresa.SelectedValue.Trim & "' ORDER BY NOMBRESP"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i
                    drT("c2") = Nu(Rs!PERSON_CONTROLA_CODIGO)
                    drT("c3") = Nu(Rs!NOMBRESP)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexPeC.DataSource = dtListado
            FlexPeC.DataBind()
        Catch ex As SqlException
            lblPeCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPeCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnPCNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPCError.Text = ""
        lblPCIngresar.Visible = True
        lblEtq9.Text = "Nuevo Punto de Control"
        txtPCCodigo.Text = ""
        txtPCPiso.Text = ""
        txtPCDescripcion.Text = ""
        txtPCUbicacion.Text = ""
        Call Llenar_AgenciaCombo()
    End Sub
    Private Sub Llenar_Combo(ByVal cbo As DropDownList, ByVal Sql As String)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        cbo.Items.Clear()
        Cn.Open()
        cmdGlobal.Connection = Cn
        cmdGlobal.CommandText = Sql
        Rs = cmdGlobal.ExecuteReader
        cbo.DataSource = Rs
        cbo.DataTextField = "Nombre"
        cbo.DataValueField = "Codigo"
        cbo.DataBind()
        Rs.Close()
        cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Llenar_AgenciaCombo()
        Dim Sql As String
        Try
            Sql = " SELECT AGENCIA_NOMBRE AS NOMBRE, AGENCIA_CODIGO AS CODIGO" _
                & " FROM TBAGENCIAS " _
                & " WHERE AGENCIA_SYS_EST='0' " _
                & " AND GRPOEMPRESA_CODIGO=" & cboGrupo.SelectedValue.Trim & " " _
                & " AND EMPRESA_CODIGO='" & cboEmpresa.SelectedValue.Trim & "' "
            Call Llenar_Combo(cboPCAgencia, Sql)
        Catch ex As SqlException
            lblPeCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPeCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnPCGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboPCAgencia.SelectedValue = "< Seleccionar >" Then lblPCError.Text = "<br> - Falta seleccionar la agencia."
        If txtPCPiso.Text.Trim = "" Then lblPCError.Text = lblPCError.Text & "<br> - Falta ingresar el piso del punto de control."
        If txtPCUbicacion.Text.Trim = "" Then lblPCError.Text = lblPCError.Text & "<br> - Falta ingresar la ubicación del punto de control."
        If lblPCError.Text <> "" Then
            lblPCError.Text = "Existen las sgtes. observaciones: " & lblPCError.Text
            Exit Sub
        End If
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim psCodigo As String = ""
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            If lblEtq9.Text = "Nuevo Punto de Control" Then
                cmdGlobal.CommandText = "SELECT MAX(PCONTROL_CODIGO) FROM TBPUNTOSCONTROL"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodigo = Nz(Rs(0)) + 1
                    End While
                Else
                    psCodigo = "1"
                End If
                Rs.Close()
                cmdGlobal.CommandText = " INSERT INTO TBPUNTOSCONTROL(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,PCONTROL_CODIGO,PCONTROL_SYS_EST) " _
                                      & " VALUES('" & cboGrupo.SelectedValue.Trim & "','" & cboEmpresa.SelectedValue.Trim & "'," & psCodigo & ",'0')"
                cmdGlobal.ExecuteNonQuery()
            End If
            If psCodigo = "" Then psCodigo = txtPCCodigo.Text.Trim
            cmdGlobal.CommandText = " UPDATE TBPUNTOSCONTROL SET AGENCIA_CODIGO = '" & cboPCAgencia.SelectedValue.Trim & "', PCONTROL_PISO='" & txtPCPiso.Text.Trim & "'," _
                                  & " PCONTROL_UBICACION = '" & txtPCUbicacion.Text.Trim & "', PCONTROL_DESCRIPCION = '" & txtPCDescripcion.Text.Trim & "' " _
                                  & " WHERE PCONTROL_CODIGO = '" & psCodigo & "' AND GRPOEMPRESA_CODIGO = " & cboGrupo.SelectedValue.Trim & " " _
                                  & " AND EMPRESA_CODIGO='" & cboEmpresa.SelectedValue.Trim & "' "
            cmdGlobal.ExecuteNonQuery()
            Call btnPCCancelar_Click(sender, e)
            Call LLenar_PuntosControl()
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnPCCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPCError.Text = ""
        lblPCIngresar.Visible = False
        lblEtq9.Text = ""
        txtPCCodigo.Text = ""
        txtPCPiso.Text = ""
        txtPCDescripcion.Text = ""
        txtPCUbicacion.Text = ""
        cboPCAgencia.Items.Clear()
    End Sub
    Protected Sub FlexPC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblPCError.Text = ""
        If e.CommandName = "Editar" Then
            lblEtq9.Text = "Editar Punto de Control"
            txtPCCodigo.Text = "" : txtPCPiso.Text = ""
            txtPCDescripcion.Text = "" : txtPCUbicacion.Text = ""
            Call Llenar_AgenciaCombo()
            If FlexPC.Rows(Index).Cells(4).Text <> "&nbsp;" Then txtPCPiso.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexPC.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexPC.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtPCUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexPC.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexPC.Rows(Index).Cells(6).Text <> "&nbsp;" Then txtPCDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexPC.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexPC.Rows(Index).Cells(7).Text <> "&nbsp;" Then txtPCCodigo.Text = FlexPC.Rows(Index).Cells(8).Text
            If FlexPC.Rows(Index).Cells(8).Text <> "&nbsp;" Then cboPCAgencia.SelectedValue = FlexPC.Rows(Index).Cells(7).Text
            lblPCIngresar.Visible = True
        End If
    End Sub
    Protected Sub btnPCListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call LLenar_PuntosControl()
    End Sub
    Protected Sub btnANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAError.Text = ""
        lblAIngresar.Visible = True
        lblEtq14.Text = "Nueva Agencia"
        txtANombre.Text = ""
        txtACodigo.Text = ""
    End Sub
    Protected Sub btnAGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAError.Text = ""
        If txtANombre.Text.Trim = "" Then lblAError.Text = "Falta ingresar el Nombre o Descripción de la Agencia" : Exit Sub
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim psACodigo As String = ""
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            If lblEtq14.Text = "Nueva Agencia" Then
                cmdGlobal.CommandText = "SELECT MAX(AGENCIA_CODIGO) FROM TBAGENCIAS"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psACodigo = Nz(Rs(0)) + 1
                    End While
                Else
                    psACodigo = "1"
                End If
                Rs.Close()
                cmdGlobal.CommandText = " INSERT INTO TBAGENCIAS(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,AGENCIA_CODIGO,AGENCIA_NOMBRE,AGENCIA_SYS_EST) " _
                                      & " VALUES(" & cboAGrupo.SelectedValue.Trim & ",'" & cboAEmpresa.SelectedValue.Trim & "','" & psACodigo & "','" & txtANombre.Text.Trim & "','0')"
                cmdGlobal.ExecuteNonQuery()
            Else
                psACodigo = txtACodigo.Text.Trim
                cmdGlobal.CommandText = " UPDATE TBAGENCIAS SET AGENCIA_NOMBRE='" & txtANombre.Text.Trim & "' WHERE AGENCIA_CODIGO='" & psACodigo & "'"
                cmdGlobal.ExecuteNonQuery()
            End If
            Call Llenar_Agencias()
            Call btnACancelar_Click(sender, e)
        Catch ex As SqlException
            lblAError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblAError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnACancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAError.Text = ""
        lblAIngresar.Visible = False
        lblEtq14.Text = ""
        txtANombre.Text = ""
        txtACodigo.Text = ""
    End Sub
    Protected Sub FlexA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblAError.Text = ""
        If e.CommandName = "Editar" Then
            lblEtq14.Text = "Editar Agencia"
            txtANombre.Text = ""
            txtACodigo.Text = ""
            If FlexA.Rows(Index).Cells(2).Text <> "&nbsp;" Then txtACodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexA.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If FlexA.Rows(Index).Cells(3).Text <> "&nbsp;" Then txtANombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexA.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            lblAIngresar.Visible = True
        End If
    End Sub
    Protected Sub btnPeCAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            FlexPCPersonal.DataSource = Nothing
            FlexPCPersonal.DataBind()
            lblPeCIngresar.Visible = True
            Call Llenar_Personal(FlexPCPersonal, cboPeCGrupo.SelectedValue.Trim, cboPeCEmpresa.SelectedValue.Trim)
            Call Marcar_Personal_Controla()
        Catch ex As SqlException
            lblPeCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPeCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally

        End Try
    End Sub
    Private Sub Llenar_Personal(ByVal psFlex As GridView, ByVal psCodGrupo As String, ByVal psCodEmpresa As String)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Cn.Open()
        cmdGlobal.Connection = Cn
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim i As Long = 0
        dtListado.Columns.Add("c2")
        dtListado.Columns.Add("c3")
        cmdGlobal.CommandText = " SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES as NOMBRESP,PERSON_CODIGO,(SELECT PERSON_CONTROLA_CODIGO FROM TBPERSONAL_CONTROLA PC WHERE PERSON_CONTROLA_CODIGO=PERSON_CODIGO AND (PE.GRPOEMPRESA_CODIGO = GRPOEMPRESA_CODIGO) AND  (PE.EMPRESA_CODIGO = EMPRESA_CODIGO)) AS CONTROLA " _
                              & " FROM TBPERSONAL P INNER JOIN TBPERSONAL_EMPRESAS PE ON PE.PERSONAL_CODIGO=P.PERSON_CODIGO WHERE PERSON_CODEST='00' AND P.PERSON_SYS_EST='0' " _
                              & " AND GRPOEMPRESA_CODIGO=" & psCodGrupo & " AND EMPRESA_CODIGO='" & psCodEmpresa & "' ORDER BY 1,2,3"
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                i = i + 1
                drT = dtListado.NewRow()
                drT("c2") = Nu(Rs!PERSON_CODIGO)
                drT("c3") = Nu(Rs!NOMBRESP)
                dtListado.Rows.Add(drT)
            End While
        End If
        Rs.Close()
        psFlex.DataSource = dtListado
        psFlex.DataBind()
    End Sub
    Private Sub Marcar_Personal_Controla()
        Dim Check As CheckBox
        Dim i As Integer
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT PERSON_CONTROLA_CODIGO " _
                                  & " FROM TBPERSONAL_CONTROLA PC " _
                                  & " WHERE (GRPOEMPRESA_CODIGO = " & cboPeCGrupo.SelectedValue.Trim & ") " _
                                  & " AND  (EMPRESA_CODIGO = '" & cboPeCEmpresa.SelectedValue.Trim & "')"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    For i = 0 To FlexPCPersonal.Rows.Count - 1
                        If FlexPCPersonal.Rows(i).Cells(1).Text = Nu(Rs!PERSON_CONTROLA_CODIGO).ToString Then
                            Check = CType(FlexPCPersonal.Rows(i).Cells(0).FindControl("chkPer"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                End While
            End If
            Rs.Close()
        Catch ex As SqlException
            lblPeCError.Text = ex.Message
        Catch ex As Exception
            lblPeCError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnPeCGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim i As Long = 0
        Dim PerControla As CheckBox
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = "DELETE FROM TBPERSONAL_CONTROLA WHERE GRPOEMPRESA_CODIGO='" & cboPeCGrupo.SelectedValue.Trim & "' AND EMPRESA_CODIGO='" & cboPeCEmpresa.SelectedValue.Trim & "'"
            cmdGlobal.ExecuteNonQuery()
            For i = 0 To FlexPCPersonal.Rows.Count - 1
                PerControla = FlexPCPersonal.Rows(i).Cells(0).FindControl("chkPer")
                If PerControla.Checked = True Then
                    cmdGlobal.CommandText = "INSERT INTO TBPERSONAL_CONTROLA(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,PERSON_CONTROLA_CODIGO) " _
                                          & "VALUES('" & cboPeCGrupo.SelectedValue.Trim & "','" & cboPeCEmpresa.SelectedValue.Trim & "','" & FlexPCPersonal.Rows(i).Cells(1).Text & "')"
                    cmdGlobal.ExecuteNonQuery()
                End If
            Next
            Call LLenar_Personal_Controla()
            Call btnPeCCancelar_Click(sender, e)
        Catch ex As SqlException
            lblPeCError.Text = ex.Message
        Catch ex As Exception
            lblPeCError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnPeCCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FlexPCPersonal.DataSource = Nothing
        FlexPCPersonal.DataBind()
        lblPeCIngresar.Visible = False
    End Sub
    Protected Sub btnPLAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLIngresar.Visible = True
        Call Llenar_PtoControl_Combo()
    End Sub
    Private Sub Llenar_PtoControl_Combo()
        Dim Sql As String
        Try
            Sql = " SELECT PC.PCONTROL_CODIGO as codigo, PC.AGENCIA_CODIGO, AG.AGENCIA_NOMBRE+' '+PC.PCONTROL_PISO+' '+PC.PCONTROL_UBICACION as nombre,PC.PCONTROL_DESCRIPCION " _
                & " FROM TBPUNTOSCONTROL PC INNER JOIN TBAGENCIAS AG ON PC.AGENCIA_CODIGO = AG.AGENCIA_CODIGO WHERE (PC.PCONTROL_SYS_EST = '0') AND (AG.AGENCIA_SYS_EST = '0')" _
                & " AND PC.GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & " AND " _
                & " PC.EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "' " _
                & " ORDER BY AGENCIA_NOMBRE,PCONTROL_PISO"
            Call Llenar_Combo(cboPLPtoControl, Sql)
        Catch ex As SqlException
            lblPeCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPeCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboPLPtoControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPLPtoControl.SelectedIndexChanged
        Try
            Call Llenar_Personal(FlexPLPersonal, cboPLGrupo.SelectedValue.Trim, cboPLEmpresa.SelectedValue.Trim)
            Call Marcar_Personal_xPtoControl()
        Catch ex As SqlException
            lblPLError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPLError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally

        End Try
    End Sub
    Private Sub Marcar_Personal_xPtoControl()
        Dim Check As CheckBox
        Dim i As Integer
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT PERSON_LABORA_CODIGO " _
                                  & " FROM TBPTOCONTROL_PERSONALLAB " _
                                  & " WHERE (PCONTROL_CODIGO =" & cboPLPtoControl.SelectedValue.Trim & ") " _
                                  & " AND GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & "  " _
                                  & " AND EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "'"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    For i = 0 To FlexPLPersonal.Rows.Count - 1
                        If FlexPLPersonal.Rows(i).Cells(1).Text = Nu(Rs!PERSON_LABORA_CODIGO).ToString Then
                            Check = CType(FlexPLPersonal.Rows(i).Cells(0).FindControl("chkPer"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                End While
            End If
            Rs.Close()
        Catch ex As SqlException
            lblPeCError.Text = ex.Message
        Catch ex As Exception
            lblPeCError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnPLCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FlexPLPersonal.DataSource = Nothing
        FlexPLPersonal.DataBind()
        lblPLIngresar.Visible = False
        cboPLPtoControl.Items.Clear()
    End Sub
    Protected Sub FlexPL_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPL.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblPLError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        If e.CommandName = "Quitar" Then
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " DELETE FROM TBPTOCONTROL_PERSONALLAB " _
                                  & " WHERE (PCONTROL_CODIGO =" & FlexPL.Rows(Index).Cells(6).Text & ") " _
                                  & " AND (PERSON_LABORA_CODIGO = '" & FlexPL.Rows(Index).Cells(2).Text & "') " _
                                  & " AND (GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & ") " _
                                  & " AND (EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "') "
            cmdGlobal.ExecuteNonQuery()
            Call LLenar_Personal_Labora()
        End If
    End Sub
    Protected Sub btnPLListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Call LLenar_Personal_Labora()
    End Sub
    Protected Sub FlexPeC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPeC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblPeCError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        If e.CommandName = "Quitar" Then
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " DELETE FROM TBPERSONAL_CONTROLA " _
                                  & " WHERE (PERSON_CONTROLA_CODIGO = '" & FlexPeC.Rows(Index).Cells(2).Text & "') " _
                                  & " AND (GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & ") " _
                                  & " AND (EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "') "
            cmdGlobal.ExecuteNonQuery()
            Call LLenar_Personal_Controla()
        End If
    End Sub
    Protected Sub btnPLGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim i As Long = 0
        Dim PerLabora As CheckBox
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " DELETE FROM TBPTOCONTROL_PERSONALLAB " _
                                  & " WHERE (PCONTROL_CODIGO =" & cboPLPtoControl.SelectedValue.Trim & ") " _
                                  & " AND (GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & ") " _
                                  & " AND (EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "') "
            cmdGlobal.ExecuteNonQuery()
            For i = 0 To FlexPLPersonal.Rows.Count - 1
                PerLabora = FlexPLPersonal.Rows(i).Cells(0).FindControl("chkPer")
                If PerLabora.Checked = True Then
                    cmdGlobal.CommandText = " INSERT INTO TBPTOCONTROL_PERSONALLAB(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,PCONTROL_CODIGO,PERSON_LABORA_CODIGO) " _
                                          & " VALUES('" & cboPLGrupo.SelectedValue.Trim & "','" & cboPLEmpresa.SelectedValue.Trim & "','" & cboPLPtoControl.SelectedValue.Trim & "','" & FlexPLPersonal.Rows(i).Cells(1).Text & "')"
                    cmdGlobal.ExecuteNonQuery()
                End If
            Next
            Call LLenar_Personal_Labora()
            Call btnPLCancelar_Click(sender, e)
        Catch ex As SqlException
            lblPeCError.Text = ex.Message
        Catch ex As Exception
            lblPeCError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexPL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub btnAListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class

