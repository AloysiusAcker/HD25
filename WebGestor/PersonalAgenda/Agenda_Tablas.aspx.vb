Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Agenda_Tablas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
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
        btnPLCancelar_Click(sender, e)
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
        Call Listar_Horarios_Atencion()
    End Sub
    Private Sub Listar_Horarios_Atencion()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim psAño As String = AñoActual(cboEmpresa.SelectedValue.Trim, Session("Ruta_Emp"))
        Dim TipoA As String = ""
        Dim Dia1 As String, NomTipo As String : Dia1 = "" : NomTipo = ""
        Dim Hora1 As String, Hora2 As String : Hora1 = "" : Hora2 = ""
        Dim Hora11 As String, Hora22 As String : Hora11 = "" : Hora22 = ""
        Dim Cadena As String, NomArea As String, CodArea As String
        Dim Personal As String, NomPersonal As String
        Dim Sql As String = ""
        Dim CantFila As Long = 0
        Dim a As Long = 0
        Personal = "" : NomPersonal = "" : Cadena = "" : NomArea = "" : CodArea = ""
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
            Sql = " SELECT (SELECT (PERSON_APEPAT + ' ' + PERSON_APEMAT +', ' + PERSON_NOMBRES) From TBPERSONAL WHERE PERSON_CODIGO = HOR.ATEN_PERSONAL) AS NOMBRESP," _
                & " (SELECT ELEMEN_VALOR From TBCELEMEN WHERE ELEMEN_CODIGO = HOR.ATEN_TIPO AND ELEMEN_TABLA = 'TBOPC202') AS ATIPO, " _
                & " HOR.ATEN_DIA,HOR.ATEN_HOR_INI , HOR.ATEN_HOR_FIN, AREAS.AREA_CODIGO,DA.AREA_NOMBRE,HOR.ATEN_TIPO, HOR.ATEN_AÑO, HOR.ATEN_PERSONAL " _
                & " From TBPERSONAL_HORATENCION HOR INNER JOIN TBPERSONAL_AREAS AREAS " _
                & " ON HOR.ATEN_AREA = AREAS.AREA_CODIGO AND HOR.ATEN_PERSONAL = AREAS.PERSON_PERSONAL " _
                & " INNER JOIN TBPERSONAL_DEFINE_AREA DA ON DA.AREA_CODIGO = AREAS.AREA_CODIGO " _
                & " WHERE (HOR.ATEN_AÑO = '" & psAño & "') AND (HOR.ATEN_SYS_EST = '0') AND (AREAS.PERSON_AREA_SYS_EST = '0') " _
                & " AND (DA.AREA_SYS_EST = '0') AND DA.GRPOEMPRESA_CODIGO = " & cboGrupo.SelectedValue.Trim & " " _
                & " AND DA.EMPRESA_CODIGO = '" & cboEmpresa.SelectedValue.Trim & "' " _
                & " ORDER BY AREA_NOMBRE, NOMBRESP, ATIPO,ATEN_HOR_INI , ATEN_HOR_FIN,ATEN_DIA"
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CantFila = CantFila + 1
                End While
            End If
            Rs.Close()
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    a = a + 1
                    If CodArea <> Nu(Rs!AREA_CODIGO) Then
                        If Personal <> "" Then
                            If Cadena <> "" Then Cadena = Cadena & "; "
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c0") = i
                            drT("c1") = NomArea
                            drT("c2") = NomPersonal
                            drT("c3") = Personal
                            drT("c4") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c5") = NomTipo
                            drT("c6") = TipoA
                            drT("c7") = CodArea
                            dtListado.Rows.Add(drT)
                        End If
                        NomArea = Nu(Rs!AREA_NOMBRE)
                        CodArea = Nu(Rs!AREA_CODIGO)
                        Personal = ""
                        NomPersonal = ""
                        TipoA = ""
                        Cadena = ""
                        NomTipo = ""
                        Dia1 = ""
                        Hora1 = "" : Hora11 = ""
                        Hora2 = "" : Hora22 = ""
                    End If
                    If Personal <> Nu(Rs!ATEN_PERSONAL) Then
                        If Personal <> "" Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c0") = i
                            drT("c1") = NomArea
                            drT("c2") = NomPersonal
                            drT("c3") = Personal
                            drT("c4") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c5") = NomTipo
                            drT("c6") = TipoA
                            drT("c7") = CodArea
                            dtListado.Rows.Add(drT)
                        End If
                        Personal = Nu(Rs!ATEN_PERSONAL)
                        NomPersonal = Nu(Rs!NOMBRESP)
                        TipoA = ""
                        Cadena = ""
                        NomTipo = ""
                        Dia1 = ""
                        Hora1 = ""
                        Hora2 = ""
                    End If
                    If TipoA <> Nu(Rs!ATEN_TIPO) Then
                        If TipoA <> "" Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c0") = i
                            drT("c1") = NomArea
                            drT("c2") = NomPersonal
                            drT("c3") = Personal
                            drT("c4") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c5") = NomTipo
                            drT("c6") = TipoA
                            drT("c7") = CodArea
                            dtListado.Rows.Add(drT)
                        End If
                        NomTipo = Nu(Rs!ATIPO)
                        TipoA = Nu(Rs!ATEN_TIPO)
                        Cadena = ""
                        Dia1 = Nombre_Dia(Nu(Rs!ATEN_DIA), False)
                        'If Hora1 <> Nu(Rs!ATEN_HOR_INI) Or Hora2 <> Nu(Rs!ATEN_HOR_FIN) Then
                        Hora1 = Nu(Rs!ATEN_HOR_INI) : Hora11 = Left(Nu(Rs!ATEN_HOR_INI), 2) & ":" & Right(Nu(Rs!ATEN_HOR_INI), 2)
                        Hora2 = Nu(Rs!ATEN_HOR_FIN) : Hora22 = Left(Nu(Rs!ATEN_HOR_FIN), 2) & ":" & Right(Nu(Rs!ATEN_HOR_FIN), 2)
                        'Else

                        'End If
                        If a = CantFila Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c0") = i
                            drT("c1") = NomArea
                            drT("c2") = NomPersonal
                            drT("c3") = Personal
                            drT("c4") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c5") = NomTipo
                            drT("c6") = TipoA
                            drT("c7") = CodArea
                            dtListado.Rows.Add(drT)
                        End If
                    Else
                        If Hora1 <> Nu(Rs!ATEN_HOR_INI) Or Hora2 <> Nu(Rs!ATEN_HOR_FIN) Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            Dia1 = Comprension_Dias(Dia1)
                            Cadena = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            Hora1 = Nu(Rs!ATEN_HOR_INI) : Hora11 = Left(Nu(Rs!ATEN_HOR_INI), 2) & ":" & Right(Nu(Rs!ATEN_HOR_INI), 2)
                            Hora2 = Nu(Rs!ATEN_HOR_FIN) : Hora22 = Left(Nu(Rs!ATEN_HOR_FIN), 2) & ":" & Right(Nu(Rs!ATEN_HOR_FIN), 2)
                            Dia1 = ""
                        End If
                        If Dia1 <> "" Then Dia1 = Dia1 & ", "
                        Dia1 = Dia1 & Nombre_Dia(Nu(Rs!ATEN_DIA), False)
                        If a = CantFila Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c0") = i
                            drT("c1") = NomArea
                            drT("c2") = NomPersonal
                            drT("c3") = Personal
                            drT("c4") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c5") = NomTipo
                            drT("c6") = TipoA
                            drT("c7") = CodArea
                            dtListado.Rows.Add(drT)
                        End If
                    End If
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
        Call Llenar_Areas()
    End Sub
    Private Sub Llenar_Areas()
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
            cmdGlobal.CommandText = " SELECT AREA_CODIGO,AREA_NOMBRE " _
                                  & " FROM TBPERSONAL_DEFINE_AREA " _
                                  & " WHERE AREA_SYS_EST='0' AND GRPOEMPRESA_CODIGO=" & cboAGrupo.SelectedValue.Trim & " " _
                                  & " AND EMPRESA_CODIGO='" & cboAEmpresa.SelectedValue.Trim & "' " _
                                  & " ORDER BY AREA_NOMBRE"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = i
                    drT("c1") = Nu(Rs!AREA_CODIGO)
                    drT("c2") = Nu(Rs!AREA_NOMBRE)
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
        Call LLenar_Personal_xArea()
    End Sub
    Private Sub LLenar_Personal_xArea()
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
            cmdGlobal.CommandText = " SELECT PERSON_PERSONAL, " _
                                  & " (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO = PERSON_PERSONAL) AS NOMBRE_PERSONAL," _
                                  & " AREA.AREA_CODIGO, AREA_NOMBRE " _
                                  & " FROM TBPERSONAL_AREAS AREA INNER JOIN TBPERSONAL_DEFINE_AREA DA " _
                                  & " ON AREA.AREA_CODIGO = DA.AREA_CODIGO " _
                                  & " WHERE DA.GRPOEMPRESA_CODIGO = '" & cboPLGrupo.SelectedValue.Trim & "' " _
                                  & " AND DA.EMPRESA_CODIGO = '" & cboPLEmpresa.SelectedValue.Trim & "' " _
                                  & " AND AREA_SYS_EST = '0'AND (PERSON_AREA_SYS_EST = '0')  " _
                                  & " ORDER BY DA.AREA_NOMBRE,NOMBRE_PERSONAL"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i
                    drT("c2") = Nu(Rs!PERSON_PERSONAL)
                    drT("c3") = Nu(Rs!NOMBRE_PERSONAL)
                    drT("c4") = Nu(Rs!AREA_CODIGO)
                    drT("c5") = Nu(Rs!AREA_NOMBRE)
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
    Protected Sub btnPCNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        lblEtqNuevo.Visible = True
        Try
            cboHAArea.Items.Clear()
            cboHAPersonal.Items.Clear()
            cboHAMin.SelectedValue = "--"
            FlexHorAtencion.DataSource = Nothing
            FlexHorAtencion.DataBind()
            Call Personal_Area()
            Call Deshabilitar_NuevoHorario()
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            'Cn.Close()
        End Try
    End Sub
    Private Sub Deshabilitar_NuevoHorario()
        lblHAEtq5.Visible = False
        lblHAEtq6.Visible = False
        lblGrillaHorario.Visible = False
        cboHATipo.Visible = False : cboHATipo.Items.Clear()
        btnHACancelar.Visible = False : btnHAGuardar.Visible = False
        FlexHora.DataSource = Nothing
        FlexHora.DataBind()
    End Sub
    Private Sub Habilitar_NuevoHorario()
        lblHAEtq5.Visible = True
        lblHAEtq6.Visible = True
        lblGrillaHorario.Visible = True
        cboHATipo.Visible = True : cboHATipo.Items.Clear()
        btnHACancelar.Visible = True : btnHAGuardar.Visible = True
        FlexHora.DataSource = Nothing
        FlexHora.DataBind()
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
    Protected Sub FlexPC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPC.RowCommand
        '
    End Sub
    Protected Sub btnPCListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Call Listar_Horarios_Atencion()
    End Sub
    Protected Sub btnANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAError.Text = ""
        lblAIngresar.Visible = True
        lblEtq14.Text = "Nueva Area"
        txtANombre.Text = ""
        txtACodigo.Text = ""
    End Sub
    Protected Sub btnAGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAError.Text = ""
        If txtANombre.Text.Trim = "" Then lblAError.Text = "Falta ingresar el Nombre o Descripción del Area" : Exit Sub
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim psACodigo As String = ""
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            If lblEtq14.Text = "Nueva Area" Then
                cmdGlobal.CommandText = "SELECT * FROM TBPERSONAL_DEFINE_AREA WHERE AREA_SYS_EST='0' AND GRPOEMPRESA_CODIGO=" & cboAGrupo.SelectedValue.Trim & " AND EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "' AND upper(AREA_NOMBRE)='" & UCase(txtANombre.Text.Trim) & "'"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        Rs.Close() : Cn.Close()
                        lblAError.Text = "Ya existe una área con el mismo nombre, favor de corregir o cambiarlo."
                        Exit Sub
                    End While
                End If
                Rs.Close()
                cmdGlobal.CommandText = "SELECT MAX(AREA_CODIGO) FROM TBPERSONAL_DEFINE_AREA"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psACodigo = Nz(Rs(0)) + 1
                    End While
                Else
                    psACodigo = "1"
                End If
                Rs.Close()
                cmdGlobal.CommandText = " INSERT INTO TBPERSONAL_DEFINE_AREA(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,AREA_CODIGO, AREA_NOMBRE,AREA_SYS_EST) " _
                                      & " VALUES(" & cboAGrupo.SelectedValue.Trim & ",'" & cboAEmpresa.SelectedValue.Trim & "','" & psACodigo & "','" & txtANombre.Text.Trim & "','0')"
                cmdGlobal.ExecuteNonQuery()
            Else
                psACodigo = txtACodigo.Text.Trim
                cmdGlobal.CommandText = " UPDATE TBPERSONAL_DEFINE_AREA SET AREA_NOMBRE='" & txtANombre.Text.Trim & "' WHERE AREA_CODIGO='" & psACodigo & "'"
                cmdGlobal.ExecuteNonQuery()
            End If
            Call Llenar_Areas()
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
    Protected Sub btnPLAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblPLIngresar.Visible = True
            Call Llenar_Personal_Combo(cboPLGrupo.SelectedValue.Trim, cboPLEmpresa.SelectedValue.Trim)
        Catch ex As SqlException
            lblPLError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPLError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Llenar_Personal_Combo(ByVal psCodGrupo As String, ByVal psCodEmpresa As String)
        Dim Sql As String
        Sql = " SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES AS Nombre,PERSON_CODIGO as Codigo " _
            & " FROM BDGRUPOEMPRESAS.dbo.TBPERSONAL P INNER JOIN BDGRUPOEMPRESAS.dbo.TBPERSONAL_EMPRESAS PE " _
            & " ON P.PERSON_CODIGO=PE.PERSONAL_CODIGO " _
            & " WHERE PERSON_CODEST='00' AND P.PERSON_SYS_EST='0' AND PE.PERSON_SYS_EST='0' " _
            & " AND (EMPRESA_CODIGO='" & psCodEmpresa & "') " _
            & " AND (GRPOEMPRESA_CODIGO=" & psCodGrupo & ") " _
            & " ORDER BY PERSON_APEPAT,PERSON_APEMAT,PERSON_NOMBRES"
        Call Llenar_Combo(cboPLPersonal, Sql)
    End Sub
    Private Sub Marcar_Personal_xArea()
        Dim Check As CheckBox
        Dim i As Integer
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT AREA.AREA_CODIGO,DA.AREA_NOMBRE " _
                                  & " From TBPERSONAL_AREAS AREA INNER JOIN TBPERSONAL_DEFINE_AREA DA " _
                                  & " ON AREA.AREA_CODIGO = DA.AREA_CODIGO " _
                                  & " WHERE DA.GRPOEMPRESA_CODIGO = '" & cboPLGrupo.SelectedValue.Trim & "' " _
                                  & " AND DA.EMPRESA_CODIGO = '" & cboPLEmpresa.SelectedValue.Trim & "' " _
                                  & " AND AREA_SYS_EST = '0' AND (PERSON_AREA_SYS_EST = '0') " _
                                  & " AND (PERSON_PERSONAL = '" & cboPLPersonal.SelectedValue.Trim & "')"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    For i = 0 To FlexPLPersonal.Rows.Count - 1
                        If FlexPLPersonal.Rows(i).Cells(1).Text = Nu(Rs!AREA_CODIGO).ToString Then
                            Check = CType(FlexPLPersonal.Rows(i).Cells(0).FindControl("chkPer"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                End While
            End If
            Rs.Close()
        Catch ex As SqlException
            lblPLError.Text = ex.Message
        Catch ex As Exception
            lblPLError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnPLCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FlexPLPersonal.DataSource = Nothing
        FlexPLPersonal.DataBind()
        lblPLIngresar.Visible = False
        cboPLPersonal.Items.Clear()
    End Sub
    Protected Sub FlexPL_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPL.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblPLError.Text = ""
        Dim psAño As String = AñoActual(cboPLEmpresa.SelectedValue.Trim, Session("Ruta_Emp"))
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        If e.CommandName = "Quitar" Then
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " DELETE FROM  TBPERSONAL_AREAS " _
                                  & " WHERE (AREA_CODIGO =" & FlexPL.Rows(Index).Cells(4).Text & ") " _
                                  & " AND (PERSON_PERSONAL = '" & FlexPL.Rows(Index).Cells(2).Text & "') "
            cmdGlobal.ExecuteNonQuery()
            cmdGlobal.CommandText = " DELETE FROM TBPERSONAL_HORATENCION " _
                                  & " WHERE ATEN_AÑO='" & psAño & "' " _
                                  & " AND ATEN_PERSONAL='" & FlexPL.Rows(Index).Cells(2).Text & "' " _
                                  & " AND ATEN_AREA='" & FlexPL.Rows(Index).Cells(4).Text & "'"
            cmdGlobal.ExecuteNonQuery()
            Call LLenar_Personal_xArea()
        End If
    End Sub
    Protected Sub btnPLListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Call LLenar_Personal_xArea()
    End Sub
    Protected Sub btnPLGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim i As Long = 0
        Dim PerLabora As CheckBox
        Dim psCod As String = ""
        Dim psAño As String = AñoActual(cboPLEmpresa.SelectedValue.Trim, Session("Ruta_Emp"))
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " DELETE FROM  TBPERSONAL_AREAS " _
                                  & " WHERE (PERSON_PERSONAL = '" & cboPLPersonal.SelectedValue.Trim & "') "
            cmdGlobal.ExecuteNonQuery()
            cmdGlobal.CommandText = " DELETE FROM TBPERSONAL_HORATENCION " _
                                  & " WHERE ATEN_AÑO='" & psAño & "' " _
                                  & " AND ATEN_PERSONAL='" & cboPLPersonal.SelectedValue.Trim & "' "
            cmdGlobal.ExecuteNonQuery()
            For i = 0 To FlexPLPersonal.Rows.Count - 1
                PerLabora = FlexPLPersonal.Rows(i).Cells(0).FindControl("chkPer")
                If PerLabora.Checked = True Then
                    cmdGlobal.CommandText = "SELECT MAX(AREA_CODIGO) FROM TBPERSONAL_DEFINE_AREA"
                    Rs = cmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCod = Nz(Rs(0)) + 1
                        End While
                    Else
                        psCod = "1"
                    End If
                    Rs.Close()
                    cmdGlobal.CommandText = " INSERT INTO TBPERSONAL_AREAS(PERSON_AREA_NUM_REG,PERSON_PERSONAL,PERSON_AREA_SYS_EST,AREA_CODIGO) " _
                                          & " VALUES(" & psCod & ",'" & cboPLPersonal.SelectedValue.Trim & "','0'," & FlexPLPersonal.Rows(i).Cells(1).Text & ")"
                    cmdGlobal.ExecuteNonQuery()
                End If
            Next
            Call LLenar_Personal_xArea()
            Call btnPLCancelar_Click(sender, e)
        Catch ex As SqlException
            lblPLError.Text = ex.Message
        Catch ex As Exception
            lblPLError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexPL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub btnAListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Llenar_Areas()
    End Sub
    Protected Sub cboPLPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Call Llenar_Areas_Asignar(FlexPLPersonal, cboPLGrupo.SelectedValue.Trim, cboPLEmpresa.SelectedValue.Trim)
            Call Marcar_Personal_xArea()
        Catch ex As SqlException
            lblPLError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPLError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally

        End Try
    End Sub
    Private Sub Llenar_Areas_Asignar(ByVal psFlex As GridView, ByVal psCodGrupo As String, ByVal psCodEmpresa As String)
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
        cmdGlobal.CommandText = " SELECT AREA_CODIGO,AREA_NOMBRE " _
                              & " FROM TBPERSONAL_DEFINE_AREA " _
                              & " WHERE AREA_SYS_EST='0' AND GRPOEMPRESA_CODIGO=" & cboPLGrupo.SelectedValue.Trim & " " _
                              & " AND EMPRESA_CODIGO='" & cboPLEmpresa.SelectedValue.Trim & "' " _
                              & " ORDER BY AREA_NOMBRE"
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                i = i + 1
                drT = dtListado.NewRow()
                drT("c2") = Nu(Rs!AREA_CODIGO)
                drT("c3") = Nu(Rs!AREA_NOMBRE)
                dtListado.Rows.Add(drT)
            End While
        End If
        Rs.Close()
        psFlex.DataSource = dtListado
        psFlex.DataBind()
    End Sub
    Protected Sub btnHANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Call Habilitar_NuevoHorario()
        btnHAHorario.Enabled = False
        btnHANuevo.Enabled = False
        btnHACerrar.Enabled = False
        Call LlenaComboItem("TBOPC202", cboHATipo)
        Dim i As Long = 0
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim a As Long = 0
        Dim cv As String = ""
        dtListado.Columns.Add("c0")
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c6")
        For i = 1 To 7
            drT = dtListado.NewRow()
            drT("c0") = i
            drT("c1") = Nombre_Dia(i, False)
            drT("c6") = i
            dtListado.Rows.Add(drT)
        Next
        FlexHora.DataSource = dtListado
        FlexHora.DataBind()
        'Call Llenar_GrillaHorario()
    End Sub
    Private Sub Personal_Area()
        Dim Sql As String
        Sql = " SELECT AREA_CODIGO as Codigo,AREA_NOMBRE as Nombre " _
            & " FROM TBPERSONAL_DEFINE_AREA " _
            & " WHERE GRPOEMPRESA_CODIGO = " & cboGrupo.SelectedValue.Trim & " " _
            & " AND EMPRESA_CODIGO = '" & cboEmpresa.SelectedValue.Trim & "' " _
            & " AND AREA_SYS_EST='0'"
        Call Llenar_Combo(cboHAArea, Sql)
    End Sub
    Protected Sub cboHAArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Try
            Call ListaCombo_PersonalxArea()
            cboHAMin.SelectedValue = "--"
            FlexHorAtencion.DataSource = Nothing
            FlexHorAtencion.DataBind()
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            '
        End Try
    End Sub
    Private Sub ListaCombo_PersonalxArea()
        Dim Sql As String
        Sql = " SELECT PERSON_PERSONAL as Codigo," _
            & " (SELECT person_apepat + ' ' + person_apemat + ', ' + person_nombres From TBPERSONAL WHERE person_codigo = PERSON_PERSONAL)  AS Nombre " _
            & " From TBPERSONAL_AREAS " _
            & " WHERE (PERSON_AREA_SYS_EST = '0') " _
            & " AND (AREA_CODIGO = '" & cboHAArea.SelectedValue.Trim & "') " _
            & " ORDER BY Nombre"
        Call Llenar_Combo(cboHAPersonal, Sql)
    End Sub
    Protected Sub cboHAPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psAño As String = AñoActual(cboEmpresa.SelectedValue.Trim, Session("Ruta_Emp"))
        Dim TipoA As String = ""
        Dim Dia1 As String, NomTipo As String : Dia1 = "" : NomTipo = ""
        Dim Hora1 As String, Hora2 As String : Hora1 = "" : Hora2 = ""
        Dim Hora11 As String, Hora22 As String : Hora11 = "" : Hora22 = ""
        Dim Cadena As String, NomArea As String, CodArea As String
        Dim Sql As String = ""
        Dim CantFila As Long = 0
        Dim a As Long = 0
        Dim ColI, Fili As Long : ColI = 0 : Fili = 0
        Cadena = "" : NomArea = "" : CodArea = ""
        Try
            cboHAMin.SelectedValue = "--"
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT NCIT_NRO_CITAS " _
                                  & " FROM TBPERSONAL_HORATENCION_TXC " _
                                  & " WHERE NCIT_AÑO='" & psAño & "' " _
                                  & " AND NCIT_PERSONAL='" & cboHAPersonal.SelectedValue.Trim & "' " _
                                  & " AND NCIT_AREA='" & cboHAArea.SelectedValue.Trim & "'"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If Nu(Rs!NCIT_NRO_CITAS) <> "" Then
                        cboHAMin.SelectedValue = Nu(Rs!NCIT_NRO_CITAS)
                    End If
                End While
            End If
            Rs.Close()
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            Sql = " SELECT ATEN_TIPO,  (SELECT ELEMEN_VALOR  From TBCELEMEN  WHERE ELEMEN_CODIGO = ATEN_TIPO AND  ELEMEN_TABLA = 'TBOPC202') AS ATIPO, " _
                & " (SELECT ELEMEN_VALOR_MINIS  From TBCELEMEN  WHERE ELEMEN_CODIGO = ATEN_TIPO AND  ELEMEN_TABLA = 'TBOPC202') AS ATIPO_COLOR , " _
                & " ATEN_DIA,ATEN_HOR_INI , ATEN_HOR_FIN " _
                & " From TBPERSONAL_HORATENCION " _
                & " WHERE (ATEN_AÑO = '" & psAño & "') AND (ATEN_SYS_EST = '0') " _
                & " AND (ATEN_PERSONAL = '" & cboHAPersonal.SelectedValue.Trim & "') " _
                & " AND ATEN_AREA='" & cboHAArea.SelectedValue.Trim & "'"
            Sql = Sql & " ORDER BY ATIPO,ATEN_HOR_INI , ATEN_HOR_FIN,ATEN_DIA"
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CantFila = CantFila + 1
                End While
            End If
            Rs.Close()
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    a = a + 1
                    If TipoA <> Nu(Rs!ATEN_TIPO) Then
                        If TipoA <> "" Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c1") = i
                            drT("c2") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c3") = NomTipo
                            drT("c4") = TipoA
                            dtListado.Rows.Add(drT)
                        End If
                        NomTipo = Nu(Rs!ATIPO)
                        TipoA = Nu(Rs!ATEN_TIPO)
                        Cadena = ""
                        Dia1 = Nombre_Dia(Nu(Rs!ATEN_DIA), False)
                        If Hora1 <> Nu(Rs!ATEN_HOR_INI) Or Hora2 <> Nu(Rs!ATEN_HOR_FIN) Then
                            Hora1 = Nu(Rs!ATEN_HOR_INI)
                            Hora11 = Left(Nu(Rs!ATEN_HOR_INI), 2) & ":" & Right(Nu(Rs!ATEN_HOR_INI), 2)
                            Hora2 = Nu(Rs!ATEN_HOR_FIN)
                            Hora22 = Left(Nu(Rs!ATEN_HOR_FIN), 2) & ":" & Right(Nu(Rs!ATEN_HOR_FIN), 2)
                        Else

                        End If
                        If a = CantFila Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c1") = i
                            drT("c2") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c3") = NomTipo
                            drT("c4") = TipoA
                            dtListado.Rows.Add(drT)
                        End If
                    Else
                        If Hora1 <> Nu(Rs!ATEN_HOR_INI) Or Hora2 <> Nu(Rs!ATEN_HOR_FIN) Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            Dia1 = Comprension_Dias(Dia1)
                            Cadena = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            Hora1 = Nu(Rs!ATEN_HOR_INI) : Hora11 = Left(Nu(Rs!ATEN_HOR_INI), 2) & ":" & Right(Nu(Rs!ATEN_HOR_INI), 2)
                            Hora2 = Nu(Rs!ATEN_HOR_FIN) : Hora22 = Left(Nu(Rs!ATEN_HOR_FIN), 2) & ":" & Right(Nu(Rs!ATEN_HOR_FIN), 2)
                            Dia1 = ""
                        End If
                        If Dia1 <> "" Then Dia1 = Dia1 & ", "
                        Dia1 = Dia1 & Nombre_Dia(Nu(Rs!ATEN_DIA), False)
                        If a = CantFila Then
                            If Cadena <> "" Then Cadena = Cadena & "; " 'Chr(13)
                            i = i + 1
                            drT = dtListado.NewRow()
                            Dia1 = Comprension_Dias(Dia1)
                            drT("c1") = i
                            drT("c2") = Cadena & Dia1 & " " & Hora11 & " a " & Hora22
                            drT("c3") = NomTipo
                            drT("c4") = TipoA
                            dtListado.Rows.Add(drT)
                        End If
                    End If
                End While
            End If
            Rs.Close()
            FlexHorAtencion.DataSource = dtListado
            FlexHorAtencion.DataBind()
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnHACancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Call Deshabilitar_NuevoHorario()
        btnHAHorario.Enabled = True
        btnHANuevo.Enabled = True
        btnHACerrar.Enabled = True
    End Sub
    Protected Sub btnHACerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        lblEtqNuevo.Visible = False
    End Sub
    Protected Sub btnHAHorario_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
    End Sub
    Protected Sub btnHAGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPLError.Text = ""
        Dim NroMin As Long : NroMin = 0
        Dim i As Long = 0
        Dim ExD As Boolean : ExD = False
        Dim ColI, Fili As Integer, cv As String
        Dim TipoA, Area As String
        Dim cD1 As DropDownList
        Dim cA1 As DropDownList
        Dim cD2 As DropDownList
        Dim cA2 As DropDownList
        Dim psD1, psA1, psD2, psA2 As String
        Dim a As Long = 0
        Dim r As Long = 0
        Dim Variable As String = ""
        psA1 = "" : psD1 = "" : psA2 = "" : psD2 = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs2 As SqlDataReader
        Dim psAño As String = AñoActual(cboEmpresa.SelectedValue.Trim, Session("Ruta_Emp"))
        Try
            If cboHATipo.SelectedValue = "< Seleccionar >" Then lblPLError.Text = "Debe de seleccionar el Tipo de Atención" : Exit Sub
            ExD = False
            For i = 0 To FlexHora.Rows.Count - 1
                cD1 = CType(FlexHora.Rows(i).Cells(2).FindControl("cboD1"), DropDownList)
                cA1 = CType(FlexHora.Rows(i).Cells(3).FindControl("cboA1"), DropDownList)
                cD2 = CType(FlexHora.Rows(i).Cells(4).FindControl("cboD2"), DropDownList)
                cA2 = CType(FlexHora.Rows(i).Cells(5).FindControl("cboA2"), DropDownList)
                If cD1.SelectedValue <> "00:00" And cA1.SelectedValue <> "00:00" Then ExD = True : Exit For
                If cD2.SelectedValue <> "00:00" And cA2.SelectedValue <> "00:00" Then ExD = True : Exit For
            Next
            If ExD = False Then lblPCError.Text = "Debe de ingresar los intervalos de horas de los dias que atenderá el Personal" : Exit Sub
            For i = 1 To FlexHora.Rows.Count - 1
                cD1 = CType(FlexHora.Rows(i).Cells(2).FindControl("cboD1"), DropDownList)
                cA1 = CType(FlexHora.Rows(i).Cells(3).FindControl("cboA1"), DropDownList)
                cD2 = CType(FlexHora.Rows(i).Cells(4).FindControl("cboD2"), DropDownList)
                cA2 = CType(FlexHora.Rows(i).Cells(5).FindControl("cboA2"), DropDownList)
                If (cD1.SelectedValue <> "00:00" And cA1.SelectedValue = "00:00") Or (cD1.SelectedValue = "00:00" And cA1.SelectedValue <> "00:00") Then
                    lblPCError.Text = "Debe de ingresar la hora inicio y la hora termino del Dia " & Nombre_Dia(FlexHora.Rows(i).Cells(6).Text, True) & Chr(13) & "del primer intervalo de tiempo para la atención." : Exit Sub
                ElseIf cD1.SelectedValue <> "00:00" And cA1.SelectedValue <> "00:00" Then
                    NroMin = DateDiff("n", cD1.SelectedValue, cA1.SelectedValue)
                    If NroMin <= 0 Then lblPCError.Text = "Verificar el primer intervalo de tiempo del Dia " & Nombre_Dia(FlexHora.Rows(i).Cells(6).Text, True) & ", deben de existir entre ellos" & Chr(13) & "por lo menos un minuto y la hora de inicio no debe ser menor a la hora termino" : Exit Sub
                End If
                If (cD2.SelectedValue <> "00:00" And cA2.SelectedValue = "00:00") Or (cD2.SelectedValue = "00:00" And cA2.SelectedValue <> "00:00") Then
                    lblPCError.Text = "Debe de ingresar la hora inicio y la hora termino del Dia " & Nombre_Dia(FlexHora.Rows(i).Cells(6).Text, True) & Chr(13) & "de existir un segundo intervalo de tiempo de atención." : Exit Sub
                ElseIf cD2.SelectedValue <> "00:00" And cA2.SelectedValue <> "00:00" Then
                    NroMin = DateDiff("n", cD2.SelectedValue.Trim, cA2.SelectedValue.Trim.Trim)
                    If NroMin <= 0 Then lblPCError.Text = "Verificar el segundo intervalo de tiempo del Dia " & Nombre_Dia(FlexHora.Rows(i).Cells(6).Text, True) & ", deben de existir entre ellos" & Chr(13) & "por lo menos un minuto y la hora de inicio no debe ser menor a la hora termino" : Exit Sub
                End If
            Next
            TipoA = cboHATipo.SelectedValue.Trim
            Area = cboHAArea.SelectedValue.Trim
            For i = 1 To FlexHora.Rows.Count - 1
                If FlexHora.Rows(i).Cells(0).Text <> "" Then
                    ColI = Val(FlexHora.Rows(i).Cells(6).Text) + 1
                    cD1 = CType(FlexHora.Rows(i).Cells(2).FindControl("cboD1"), DropDownList)
                    cA1 = CType(FlexHora.Rows(i).Cells(3).FindControl("cboA1"), DropDownList)
                    cD2 = CType(FlexHora.Rows(i).Cells(4).FindControl("cboD2"), DropDownList)
                    cA2 = CType(FlexHora.Rows(i).Cells(5).FindControl("cboA2"), DropDownList)
                    psD1 = Left(cD1.SelectedValue.Trim, 2) & Right(cD1.SelectedValue.Trim, 2)
                    psA1 = Left(cA1.SelectedValue.Trim, 2) & Right(cA1.SelectedValue.Trim, 2)
                    psD2 = Left(cD2.SelectedValue.Trim, 2) & Right(cD2.SelectedValue.Trim, 2)
                    psA2 = Left(cA2.SelectedValue.Trim, 2) & Right(cA2.SelectedValue.Trim, 2)
                    For a = Val(psD1) To Val(psA1) - 5 Step 5
                        Variable = a
                        cv = Llenar_Ceros(Variable, 4)
                        If Val(Right(cv, 2)) <= 55 Then
                            'Fili = 0
                            'For r = 1 To FlexSemanal.Rows - 1
                            '    If FlexSemanal.TextMatrix(r, 0) = Left(cv, 2) & ":" & Right(cv, 2) Then Fili = r : Exit For
                            'Next
                            'If Fili <> 0 Then
                            '    If FlexSemanal.TextMatrix(Fili, ColI) = "A" Then
                            '        MsgBox("Se ha encontrado que el primer intervalo de tiempo del Dia " & Nombre_Dia(.TextMatrix(i, 6), True) & " se encuentran asignadas a una de sus otras" & Chr(13) & "Areas de Trabajo; cambiar los intervalos de tiempo o el Día a espacios libres para poder guardar." & Chr(13) & Chr(13) & "Para ver su horario semanal haga click en « Hor. Semanal »", vbExclamation, Me.Caption) : Call Limpiar_Hor_Semanal() : FlexHor.SetFocus() : Exit Sub
                            '    ElseIf FlexSemanal.TextMatrix(Fili, ColI) <> "" And FlexSemanal.TextMatrix(Fili, ColI) <> TipoA Then
                            '        MsgBox("Se ha encontrado que el primer intervalo de tiempo del Dia " & Nombre_Dia(.TextMatrix(i, 6), True) & " se encuentran asignadas a otro" & Chr(13) & "tipo de Atención; cambiar los intervalos de tiempo o el Día a espacios libres para poder guardar." & Chr(13) & Chr(13) & "Para ver su horario semanal haga click en « Hor. Semanal »", vbExclamation, Me.Caption) : Call Limpiar_Hor_Semanal() : FlexHor.SetFocus() : Exit Sub
                            '    ElseIf FlexSemanal.TextMatrix(Fili, ColI) = "" Then
                            '        FlexSemanal.TextMatrix(Fili, ColI) = "x1"
                            '    End If
                            'End If
                        End If
                    Next
                    If cD2.SelectedValue.Trim <> "00:00" And cA2.SelectedValue.Trim <> "00:00" Then
                        For a = Val(psD2) To Val(psA2) - 5 Step 5
                            Variable = a
                            cv = Llenar_Ceros(Variable, 4)
                            If Val(Right(cv, 2)) <= 55 Then
                                Fili = 0
                                'For r = 1 To FlexSemanal.Rows - 1
                                '    If FlexSemanal.TextMatrix(r, 0) = Left(cv, 2) & ":" & Right(cv, 2) Then Fili = r : Exit For
                                'Next
                                'If Fili <> 0 Then
                                '    If FlexSemanal.TextMatrix(Fili, ColI) = "A" Then
                                '        MsgBox("Se ha encontrado que el segundo intervalo de tiempo del Dia " & Nombre_Dia(.TextMatrix(i, 6), True) & " se encuentran asignadas a una de sus otras" & Chr(13) & "Areas de Trabajo; cambiar los intervalos de tiempo o el Día a espacios libres para poder guardar." & Chr(13) & Chr(13) & "Para ver su horario semanal haga click en « Hor. Semanal »", vbExclamation, Me.Caption) : Call Limpiar_Hor_Semanal() : FlexHor.SetFocus() : Exit Sub
                                '    ElseIf FlexSemanal.TextMatrix(Fili, ColI) = "x1" Then
                                '        MsgBox("Se ha encontrado que el segundo intervalo de tiempo del Dia " & Nombre_Dia(.TextMatrix(i, 6), True) & " se encuentran asignadas" & Chr(13) & "al primer intervalo de tiempo; cambiar los intervalos de tiempo a horas libres para" & Chr(13) & "poder guardar." & Chr(13) & "Para ver su horario semanal haga click en « Hor. Semanal »", vbExclamation, Me.Caption) : Call Limpiar_Hor_Semanal() : FlexHor.SetFocus() : Exit Sub
                                '    ElseIf FlexSemanal.TextMatrix(Fili, ColI) <> "" And FlexSemanal.TextMatrix(Fili, ColI) <> TipoA Then
                                '        MsgBox("Se ha encontrado que el primer intervalo de tiempo del Dia " & Nombre_Dia(.TextMatrix(i, 6), True) & " se encuentran asignadas" & Chr(13) & "a otro tipo de Atención; cambiar los intervalos de tiempo o el Día a espacios libres para" & Chr(13) & "poder guardar." & Chr(13) & "Para ver sus horarios semanales haga click en « Hor. Semanal »", vbExclamation, Me.Caption) : Call Limpiar_Hor_Semanal() : FlexHor.SetFocus() : Exit Sub
                                '    Else
                                '        FlexSemanal.TextMatrix(Fili, ColI) = "x2"
                                '    End If
                                'End If
                            End If
                        Next
                    End If
                End If
            Next
            Cn.Open() : cmdGlobal.Connection = Cn
            Cn2.Open() : cmdGlobal2.Connection = Cn2
            'cmdGlobal.CommandText = "SELECT * FROM TBPERSONAL_HORATENCION " _
            '    & " WHERE ATEN_AÑO='" & psAño & "' AND ATEN_PERSONAL='" & cboHAPersonal.SelectedValue.Trim & "' " _
            '    & " AND ATEN_TIPO='" & TipoA & "' AND ATEN_SYS_EST='0' AND ATEN_AREA='" & Area & "'"
            'Rs = cmdGlobal.ExecuteReader
            'If Rs.HasRows Then
            '    While Rs.Read
            '        If MsgBox("Se ha encontrado que existe un Horario definido para la " & cboTipoAten & "." & Chr(13) & "¿Desea borrar lo existente y guardar lo nuevo?", vbQuestion + vbYesNo, Me.Caption) = vbYes Then
            '            cmdGlobal2.CommandText = "DELETE FROM TBPERSONAL_HORATENCION WHERE ATEN_AÑO='" & psAño & "' AND ATEN_PERSONAL='" & cboHAPersonal.SelectedValue.Trim & "' AND ATEN_TIPO='" & TipoA & "' AND ATEN_AREA='" & Area & "'" ' AND GRPOEMPRESA_CODIGO = '" & CodGrupoEmpresa & "' AND EMPRESA_CODIGO = '" & SistCodEmpresa & "'"
            '            cmdGlobal.ExecuteNonQuery()
            '            For i = 1 To FlexHora.Rows.Count - 1
            '                If FlexHora.Rows(i).Cells(0).Text <> "" Then
            '                    cD1 = CType(FlexHora.Rows(i).Cells(2).FindControl("cboD1"), DropDownList)
            '                    cA1 = CType(FlexHora.Rows(i).Cells(3).FindControl("cboA1"), DropDownList)
            '                    cD2 = CType(FlexHora.Rows(i).Cells(4).FindControl("cboD2"), DropDownList)
            '                    cA2 = CType(FlexHora.Rows(i).Cells(5).FindControl("cboA2"), DropDownList)
            '                    psD1 = Left(cD1.SelectedValue.Trim, 2) & Right(cD1.SelectedValue.Trim, 2)
            '                    psA1 = Left(cA1.SelectedValue.Trim, 2) & Right(cA1.SelectedValue.Trim, 2)
            '                    psD2 = Left(cD2.SelectedValue.Trim, 2) & Right(cD2.SelectedValue.Trim, 2)
            '                    psA2 = Left(cA2.SelectedValue.Trim, 2) & Right(cA2.SelectedValue.Trim, 2)
            '                    cmdGlobal2.CommandText = " INSERT INTO TBPERSONAL_HORATENCION(ATEN_AÑO, ATEN_PERSONAL, ATEN_TIPO, ATEN_DIA,ATEN_HOR_INI, ATEN_HOR_FIN, ATEN_SYS_EST,ATEN_AREA) " _
            '                                           & " VALUES('" & psAño & "','" & cboHAPersonal.SelectedValue.Trim & "','" & TipoA & "','" & FlexHora.Rows(i).Cells(6).Text & "','" & psD1 & "','" & psA1 & "','0','" & Area & "')"
            '                    cmdGlobal2.ExecuteNonQuery()
            '                    If cD2.SelectedValue.Trim <> "00:00" And cA2.SelectedValue.Trim <> "00:00" Then
            '                        cmdGlobal2.CommandText = " INSERT INTO TBPERSONAL_HORATENCION(ATEN_AÑO, ATEN_PERSONAL, ATEN_TIPO, ATEN_DIA,ATEN_HOR_INI, ATEN_HOR_FIN, ATEN_SYS_EST,ATEN_AREA) " _
            '                                               & " VALUES('" & psAño & "','" & cboHAPersonal.SelectedValue.Trim & "','" & TipoA & "','" & FlexHora.Rows(i).Cells(6).Text & "','" & psD2 & "','" & psA2 & "','0','" & Area & "')"
            '                        cmdGlobal2.ExecuteNonQuery()
            '                    End If
            '                End If
            '            Next
            '        Else
            '            Exit Sub
            '        End If
            '    End While
            'Else
            cmdGlobal.CommandText = "DELETE FROM TBPERSONAL_HORATENCION WHERE ATEN_AÑO='" & psAño & "' AND ATEN_PERSONAL='" & cboHAPersonal.SelectedValue.Trim & "' AND ATEN_TIPO='" & TipoA & "' AND ATEN_AREA='" & Area & "'" ' AND GRPOEMPRESA_CODIGO = '" & CodGrupoEmpresa & "' AND EMPRESA_CODIGO = '" & SistCodEmpresa & "'"
            cmdGlobal.ExecuteNonQuery()
            For i = 0 To FlexHora.Rows.Count - 1
                cD1 = CType(FlexHora.Rows(i).Cells(2).FindControl("cboD1"), DropDownList)
                cA1 = CType(FlexHora.Rows(i).Cells(3).FindControl("cboA1"), DropDownList)
                cD2 = CType(FlexHora.Rows(i).Cells(4).FindControl("cboD2"), DropDownList)
                cA2 = CType(FlexHora.Rows(i).Cells(5).FindControl("cboA2"), DropDownList)
                psD1 = Left(cD1.SelectedValue.Trim, 2) & Right(cD1.SelectedValue.Trim, 2)
                psA1 = Left(cA1.SelectedValue.Trim, 2) & Right(cA1.SelectedValue.Trim, 2)
                psD2 = Left(cD2.SelectedValue.Trim, 2) & Right(cD2.SelectedValue.Trim, 2)
                psA2 = Left(cA2.SelectedValue.Trim, 2) & Right(cA2.SelectedValue.Trim, 2)
                If cD1.SelectedValue.Trim <> "00:00" And cA1.SelectedValue.Trim <> "00:00" Then
                    cmdGlobal2.CommandText = " INSERT INTO TBPERSONAL_HORATENCION(ATEN_AÑO, ATEN_PERSONAL, ATEN_TIPO, ATEN_DIA,ATEN_HOR_INI, ATEN_HOR_FIN, ATEN_SYS_EST,ATEN_AREA) " _
                                           & " VALUES('" & psAño & "','" & cboHAPersonal.SelectedValue.Trim & "','" & TipoA & "','" & FlexHora.Rows(i).Cells(6).Text & "','" & psD1 & "','" & psA1 & "','0','" & Area & "')"
                    cmdGlobal2.ExecuteNonQuery()
                End If
                If cD2.SelectedValue.Trim <> "00:00" And cA2.SelectedValue.Trim <> "00:00" Then
                    cmdGlobal2.CommandText = " INSERT INTO TBPERSONAL_HORATENCION(ATEN_AÑO, ATEN_PERSONAL, ATEN_TIPO, ATEN_DIA,ATEN_HOR_INI, ATEN_HOR_FIN, ATEN_SYS_EST,ATEN_AREA) " _
                                           & " VALUES('" & psAño & "','" & cboHAPersonal.SelectedValue.Trim & "','" & TipoA & "','" & FlexHora.Rows(i).Cells(6).Text & "','" & psD2 & "','" & psA2 & "','0','" & Area & "')"
                    cmdGlobal2.ExecuteNonQuery()
                End If
            Next
            'End If
            cboHAPersonal_SelectedIndexChanged(sender, e)
            Call Listar_Horarios_Atencion()
            Call Deshabilitar_NuevoHorario()
            btnHANuevo.Enabled = True : btnHACerrar.Enabled = True : btnHAHorario.Enabled = True
        Catch ex As SqlException
            lblPCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblPCError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Function Llenar_GrillaHorario() As DataTable
        Dim i As Long = 0
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim dtCombo As New DataTable
        Dim drCombo As DataRow
        Dim a As Long = 0
        Dim cv As String = ""
        Dim Variable As String = ""
        Llenar_GrillaHorario = Nothing
        Try
            dtCombo.Columns.Add("c0")
            For a = 600 To 2350 Step 5
                Variable = a
                cv = Llenar_Ceros(Variable, 4)
                If Val(Right(cv, 2)) > 0 And Val(Right(cv, 2)) <= 55 Then
                    drCombo = dtCombo.NewRow()
                    drCombo("c0") = Formatohora(cv)
                    dtCombo.Rows.Add(drCombo)
                End If
                If Val(Right(cv, 2)) = 55 Then
                    drCombo = dtCombo.NewRow()
                    drCombo("c0") = Llenar_Ceros(Val(Left(cv, 2)) + 1, 2) + ":" + "00"
                    dtCombo.Rows.Add(drCombo)
                End If
            Next
            drCombo = dtCombo.NewRow()
            drCombo("c0") = "23:55"
            dtCombo.Rows.Add(drCombo)
            drCombo = dtCombo.NewRow()
            drCombo("c0") = "00:00"
            dtCombo.Rows.Add(drCombo)
            For a = 5 To 550 Step 5
                Variable = a
                cv = Llenar_Ceros(Variable, 4)
                If Val(Right(cv, 2)) > 0 And Val(Right(cv, 2)) <= 55 Then
                    drCombo = dtCombo.NewRow()
                    drCombo("c0") = FormatoHora(cv)
                    dtCombo.Rows.Add(drCombo)
                End If
                If Val(Right(cv, 2)) = 55 Then
                    drCombo = dtCombo.NewRow()
                    drCombo("c0") = Llenar_Ceros(Val(Left(cv, 2)) + 1, 2) + ":" + "00"
                    dtCombo.Rows.Add(drCombo)
                End If
            Next
            drT = dtCombo.NewRow()
            drT("c0") = "05:55"
            dtCombo.Rows.Add(drT)
            Return dtCombo
        Catch ex As SqlException
            lblPLError.Text = ex.Message
        Catch ex As Exception
            lblPLError.Text = ex.Message
        Finally
            '
        End Try
    End Function
    Protected Sub FlexHora_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles FlexHora.RowDataBound
        lblPLError.Text = ""
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cD1 As DropDownList = DirectCast(e.Row.FindControl("cboD1"), DropDownList)
                Dim cA1 As DropDownList = DirectCast(e.Row.FindControl("cboA1"), DropDownList)
                Dim cD2 As DropDownList = DirectCast(e.Row.FindControl("cboD2"), DropDownList)
                Dim cA2 As DropDownList = DirectCast(e.Row.FindControl("cboA2"), DropDownList)
                cD1.ClearSelection() : cA1.ClearSelection()
                cD2.ClearSelection() : cA2.ClearSelection()
                If cD1 IsNot DBNull.Value Then
                    Me.prcCargarCombo(cD1)
                End If
                If cA1 IsNot DBNull.Value Then
                    Me.prcCargarCombo(cA1)
                End If
                If cD2 IsNot DBNull.Value Then
                    Me.prcCargarCombo(cD2)
                End If
                If cA2 IsNot DBNull.Value Then
                    Me.prcCargarCombo(cA2)
                End If
                cD1.SelectedValue = "00:00"
                cA1.SelectedValue = "00:00"
                cD2.SelectedValue = "00:00"
                cA2.SelectedValue = "00:00"
            End If
        Catch ex As SqlException
            lblPLError.Text = ex.Message
        Catch ex As Exception
            lblPLError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Public Sub prcCargarCombo(ByVal cboCombo As DropDownList)
        lblPLError.Text = ""
        Dim dtDatosParaCargarElCombo As DataTable = Llenar_GrillaHorario()
        cboCombo.DataSource = dtDatosParaCargarElCombo
        cboCombo.DataTextField = "c0"
        cboCombo.DataValueField = "c0"
        cboCombo.DataBind()
    End Sub
End Class

