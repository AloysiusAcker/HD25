Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class ControlVisitas_Registro
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Private Sub Llena_Combos_Iniciales()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            cboPtoControl.Items.Clear()
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT PC.GRPOEMPRESA_CODIGO,PC.PCONTROL_CODIGO, PC.PCONTROL_PISO,PC.PCONTROL_UBICACION,PC.PCONTROL_DESCRIPCION, " _
                                  & " PCONTROL_DESCRIPCION +' PISO '+ PCONTROL_PISO+' '+PCONTROL_UBICACION AS pto_nombre " _
                                  & " FROM TBPUNTOSCONTROL PC WHERE (PC.PCONTROL_SYS_EST = '0')   " _
                                  & " AND PC.GRPOEMPRESA_CODIGO='" & Session("CodGrupoEmpresa") & "'  ORDER BY PCONTROL_DESCRIPCION"
            Rs = cmdGlobal.ExecuteReader
            cboPtoControl.DataSource = Rs
            cboPtoControl.DataTextField = "pto_nombre"
            cboPtoControl.DataValueField = "PCONTROL_CODIGO"
            cboPtoControl.DataBind()
            Rs.Close()
            cboPtoControl.Items.Add("< Seleccionar >") : cboPtoControl.SelectedValue = "< Seleccionar >"

            cmdGlobal.CommandText = " SELECT PERSON_CONTROLA_CODIGO,P.PERSON_APEPAT + ' ' + P.PERSON_APEMAT + ', ' + P.PERSON_NOMBRES AS NOMBRESP" _
                                  & " FROM TBPERSONAL_CONTROLA PC INNER JOIN TBPERSONAL P ON PC.PERSON_CONTROLA_CODIGO = P.PERSON_CODIGO" _
                                  & " WHERE (P.PERSON_SYS_EST = '0') AND  GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " " _
                                  & " ORDER BY NOMBRESP"
            Rs = cmdGlobal.ExecuteReader
            cboPControla.DataSource = Rs
            cboPControla.DataTextField = "NOMBRESP"
            cboPControla.DataValueField = "PERSON_CONTROLA_CODIGO"
            cboPControla.DataBind()
            Rs.Close()
            cboPControla.Items.Add("< Seleccionar >") : cboPControla.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally

        End Try
    End Sub
    Private Sub Llenar_Puntos_Control()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim Rs2 As SqlClient.SqlDataReader
        Try
            cboRVPControla.Items.Clear()
            Cn2.Open()
            cmdGlobal2.Connection = Cn
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT PC.GRPOEMPRESA_CODIGO,PC.PCONTROL_CODIGO, PC.PCONTROL_PISO,PC.PCONTROL_UBICACION,PC.PCONTROL_DESCRIPCION, " _
                                  & " PCONTROL_DESCRIPCION +' PISO '+ PCONTROL_PISO+' '+PCONTROL_UBICACION AS pto_nombre " _
                                  & " FROM TBPUNTOSCONTROL PC WHERE (PC.PCONTROL_SYS_EST = '0')   " _
                                  & " AND PC.GRPOEMPRESA_CODIGO='" & Session("CodGrupoEmpresa") & "'  ORDER BY PCONTROL_DESCRIPCION"
            Rs = cmdGlobal.ExecuteReader
            cboRVPtoControl.DataSource = Rs
            cboRVPtoControl.DataTextField = "pto_nombre"
            cboRVPtoControl.DataValueField = "PCONTROL_CODIGO"
            cboRVPtoControl.DataBind()
            Rs.Close()
            cboRVPtoControl.Items.Add("< Seleccionar >") : cboRVPtoControl.SelectedValue = "< Seleccionar >"

            cmdGlobal2.CommandText = " SELECT PERSON_CONTROLA_CODIGO,P.PERSON_APEPAT + ' ' + P.PERSON_APEMAT + ', ' + P.PERSON_NOMBRES AS NOMBRESP" _
                                  & " FROM TBPERSONAL_CONTROLA PC INNER JOIN TBPERSONAL P ON PC.PERSON_CONTROLA_CODIGO = P.PERSON_CODIGO" _
                                  & " WHERE (P.PERSON_SYS_EST = '0') AND  GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " " _
                                  & " ORDER BY NOMBRESP"
            Rs2 = cmdGlobal2.ExecuteReader
            cboRVPControla.DataSource = Rs2
            cboRVPControla.DataTextField = "NOMBRESP"
            cboRVPControla.DataValueField = "PERSON_CONTROLA_CODIGO"
            cboRVPControla.DataBind()
            Rs2.Close()
            cboRVPControla.Items.Add("< Seleccionar >") : cboRVPControla.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Llena_Combos_Iniciales()
            txtFecha.Text = FormatoFecha(FechaActual)
        End If
        If Ficha.ActiveTabIndex = 1 Then
            lblRVError.Text = ""
            cboRVisitante.Items.Clear()
            cboRVisitante.Items.Add("< Seleccionar >")
            cboRVisitante.SelectedValue = "< Seleccionar >"
            txtRVCodPersonaControl.Text = cboPControla.SelectedValue.Trim
            lblRVPtoControl.Text = cboPtoControl.SelectedValue.Trim
            txtRVFecha.Text = txtFecha.Text
            Call Llenar_Puntos_Control()
            Call Llenar_Agencia()
            cboRVPtoControl.SelectedValue = cboPtoControl.SelectedValue.Trim
            cboRVPtoControl_SelectedIndexChanged(sender, e)
            cboRVPControla.SelectedValue = cboPControla.SelectedValue.Trim
            optRV.SelectedValue = "Personal en General" : optRV_SelectedIndexChanged(sender, e)
            Call LlenaComboItem("TBOPC009", cboRVTipoDoc)
            Call LlenaComboItem("TBOPC215", cboRVTipoVisita)
            cboRVTipoVisita.SelectedValue = "< Seleccionar >"
            txtBusApePat.Text = ""
            FlexP.DataSource = Nothing
            FlexP.DataBind()
            txtRVAsunto.Text = ""
            txtRVHora.Text = "__:__"
            txtRVNroSerie.Text = ""
            txtRVMarca.Text = ""
            txtRVDescripcion.Text = ""
            txtRVCodEquipo.Text = ""
            FlexEquipos.DataSource = Nothing
            FlexEquipos.DataBind()
        End If
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FlexEquipo.DataSource = Nothing
        FlexEquipo.DataBind()
        Call Listar_Registros()
    End Sub
    Private Sub Listar_Registros()
        lblError.Text = ""
        If cboPtoControl.SelectedValue = "< Seleccionar >" Then Exit Sub
        Try
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim cmdGlobal As New SqlCommand
            Dim Rs As SqlClient.SqlDataReader
            Dim psFecha As String = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
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
            dtListado.Columns.Add("c8")
            dtListado.Columns.Add("c9")
            dtListado.Columns.Add("c10")
            dtListado.Columns.Add("c11")
            dtListado.Columns.Add("c12")
            dtListado.Columns.Add("c13")
            dtListado.Columns.Add("c14")
            dtListado.Columns.Add("c15")
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = "SELECT VISITA_CODIGO,PCONTROL_CODIGO,PERSONAL_REGISTRA_ENTRADA,PERSONAL_REGISTRA_SALIDA,VISITA_FECHA_FIN,TARJETA_VISITA," _
                & " (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_REGISTRA_ENTRADA) AS NOMBRES_RE," _
                & " (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_REGISTRA_SALIDA) AS NOMBRES_RS," _
                & " VISITA_TIPOMOV, VISITA_FECHA,VISITA_TIPO_VISITANTE," _
                & " PERSONAL_CODIGO,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_CODIGO) AS NOMBRESP," _
                & " VISITA_APEPAT + ' ' + VISITA_APEMAT + ', ' + VISITA_NOMBRES AS NOMBRESV, VISITA_EMPRESA, VISITA_TIPODOCIDE," _
                & " (SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC009' AND ELEMEN_CODIGO = VISITA_TIPODOCIDE) AS TIPODOCIDE," _
                 & " (SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC216' AND ELEMEN_CODIGO = VISITA_ESTADO) AS ESTADOVISIT," _
                & " VISITA_NRODOCIDE, VISITA_TIPO,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC215' AND ELEMEN_CODIGO = VISITA_TIPO) AS TIPOVISIT," _
                & " VISITA_ASUNTO , VISITA_HORA_ENTRADA, VISITA_HORA_SALIDA,VISITA_ESTADO," _
                & " (SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO= VISITA_TIPOPERSONA AND ELEMEN_TABLA='TBOPC001') AS TIPOPERSONA, " _
                & " (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_QUE_VISITA) AS NOMBRESAQ " _
                & " FROM TBVISITAS WHERE (VISITA_SYS_EST = '0')  AND (VISITA_FECHA= '" & psFecha & "')"
            If cboRegistro.SelectedValue <> "0" Then
                If cboRegistro.SelectedValue = 3 Then
                    cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (VISITA_TIPOMOV='" & cboRegistro.SelectedValue.Trim & "')"
                Else
                    cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (VISITA_TIPOMOV='" & cboRegistro.SelectedValue.Trim & "' OR VISITA_TIPOMOV='3')"
                End If
            End If
            cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY VISITA_HORA_ENTRADA, VISITA_HORA_SALIDA,VISITA_TIPO_VISITANTE,NOMBRESP,NOMBRESV"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = i
                    If Nu(Rs!VISITA_TIPOMOV) = "1" Then
                        drT("c1") = "ENTRADA"
                        drT("c2") = Left(Nu(Rs!VISITA_HORA_ENTRADA), 2) & ":" & Right(Nu(Rs!VISITA_HORA_ENTRADA), 2)
                        drT("c3") = ""
                    ElseIf Nu(Rs!VISITA_TIPOMOV) = "2" Then
                        drT("c1") = "SALIDA"
                        drT("c2") = ""
                        drT("c3") = Left(Nu(Rs!VISITA_HORA_SALIDA), 2) & ":" & Right(Nu(Rs!VISITA_HORA_SALIDA), 2)
                    ElseIf Nu(Rs!VISITA_TIPOMOV) = "3" Then
                        drT("c1") = "ENT./SAL."
                        drT("c2") = Left(Nu(Rs!VISITA_HORA_ENTRADA), 2) & ":" & Right(Nu(Rs!VISITA_HORA_ENTRADA), 2)
                        drT("c3") = Left(Nu(Rs!VISITA_HORA_SALIDA), 2) & ":" & Right(Nu(Rs!VISITA_HORA_SALIDA), 2)
                    End If
                    drT("c4") = Nu(Rs!TARJETA_VISITA)
                    If Nu(Rs!VISITA_TIPO_VISITANTE) = "1" Then
                        drT("c5") = Nu(Rs!NOMBRESP)
                    Else
                        drT("c5") = Nu(Rs!NOMBRESV) & "  (" & Nu(Rs!tipopersona) & ")"
                        drT("c6") = Nu(Rs!TIPODOCIDE) & " Nº " & Nu(Rs!VISITA_NRODOCIDE)
                        drT("c7") = Nu(Rs!VISITA_EMPRESA)
                    End If
                    drT("c8") = Nu(Rs!NOMBRESAQ)
                    drT("c9") = Nu(Rs!TIPOVISIT)
                    drT("c10") = Nu(Rs!NOMBRES_RE)
                    drT("c11") = Nu(Rs!NOMBRES_RS)
                    drT("c12") = Nu(Rs!ESTADOVISIT)
                    drT("c13") = Nu(Rs!VISITA_CODIGO)
                    drT("c14") = FormatoFecha(Nu(Rs!VISITA_FECHA))
                    drT("c15") = Nu(Rs!VISITA_ESTADO)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Flex.DataSource = dtListado
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub optRV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRVError.Text = ""
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        If optRV.SelectedValue = "Personal en General" Then
            FlexP.DataSource = Nothing
            FlexP.DataBind()
            lblRV3.Enabled = True
            cboRVTipoPer.Enabled = True
            lblRV8.Enabled = True
            txtRVEmpresa.Enabled = True
            cboRVTipoDoc.Enabled = True
            Call LlenaComboItem("TBOPC001", cboRVTipoPer)
            cboRVTipoPer.SelectedValue = "< Seleccionar >"
            lblRV8.Text = "Empresa"
            txtRVApePat.Text = "" : txtRVApePat.ReadOnly = False
            txtRVApeMat.Text = "" : txtRVApeMat.ReadOnly = False
            txtRVNombres.Text = "" : txtRVNombres.ReadOnly = False
            txtRVEmpresa.Text = "" : txtRVEmpresa.ReadOnly = False
            txtRVNroDoc.Text = "" : txtRVNroDoc.ReadOnly = False
            txtRVIDato.Text = ""
            txtRVCodDato.Text = ""
            'txtBusPerApePat.Text = txtRVApePat.Text
            'Call Lista_Persona()
            lblBP1.Text = "Listado de Personas"
            txtRVNroSerie.Text = ""
            txtRVMarca.Text = ""
            txtRVDescripcion.Text = ""
            txtRVCodEquipo.Text = ""
            Call LlenaComboItem("TBOPC001", cboBusTipoPer)
            cboBusTipoPer.SelectedValue = "< Seleccionar >"
        Else
            Call LlenaComboItem("TBOPC001", cboRVTipoPer)
            cboRVTipoPer.SelectedValue = "< Seleccionar >"
            cboRVTipoPer.Enabled = False
            txtRVEmpresa.ReadOnly = True
            cboRVTipoDoc.Enabled = False
            lblRV8.Text = "Código"
            txtRVApePat.Text = "" : txtRVApePat.ReadOnly = True
            txtRVApeMat.Text = "" : txtRVApeMat.ReadOnly = True
            txtRVNombres.Text = "" : txtRVNombres.ReadOnly = True
            txtRVEmpresa.Text = "" : txtRVEmpresa.ReadOnly = True
            txtRVNroDoc.Text = "" : txtRVNroDoc.ReadOnly = True
            FlexP.DataSource = Nothing
            FlexP.DataBind()
            'Call Listar_Personal()
            lblBP1.Text = "Listado del Personal"
            txtRVNroSerie.Text = ""
            txtRVMarca.Text = ""
            txtRVDescripcion.Text = ""
            txtRVCodEquipo.Text = ""
            cboBusTipoPer.Items.Add("< Seleccionar >")
            cboBusTipoPer.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnRVRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Call Listar_Registros()
        cboBusTipoPer.SelectedValue = "< Seleccionar >"
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
    End Sub
    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        txtRVIDato.Text = ""
        txtRVCodDato.Text = ""
        If cboPtoControl.SelectedValue.Trim = "< Seleccionar >" Then lblError.Text = "<br> - Falta seleccionar el punto de control al cual se registrará el movimiento."
        If cboPControla.SelectedValue.Trim = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Falta seleccionar el Personal de Control que registrará el movimiento."
        If lblError.Text <> "" Then
            lblError.Text = "Existen las sgtes. observaciones: " & lblError.Text
            Exit Sub
        End If
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub cboRV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRVError.Text = ""
        cboRVisitante.Enabled = False
        lblRV13.Visible = False
        txtRVHSalida.Visible = False
        optRV.SelectedValue = "Personal en General"
        optRV_SelectedIndexChanged(sender, e)
        cboRVPersonal.SelectedValue = "< Seleccionar >"
        txtRVAsunto.Text = ""
        cboRVTipoVisita.SelectedValue = "< Seleccionar >"
        cboRVTipoDoc.SelectedValue = "< Seleccionar >"
        txtRVHora.Enabled = True
        txtRVHora.Text = FormatoHora(HoraActual)
        txtRVNroSerie.Text = ""
        txtRVMarca.Text = ""
        txtRVDescripcion.Text = ""
        txtRVCodEquipo.Text = ""
        FlexEquipos.DataSource = Nothing
        FlexEquipos.DataBind()
        cboBusTipoPer.SelectedValue = "< Seleccionar >"
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        If cboRV.SelectedValue = "1" Then
            cboRVisitante.Items.Clear()
            cboRVisitante.Items.Add("< Seleccionar >")
            cboRVisitante.SelectedValue = "< Seleccionar >"
            optRV.Enabled = True
            lblRV12.Text = "Hora de Entrada"
        ElseIf cboRV.SelectedValue = "2" Then
            cboRVisitante.Items.Clear()
            cboRVisitante.Items.Add("< Seleccionar >")
            cboRVisitante.SelectedValue = "< Seleccionar >"
            optRV.Enabled = True
            lblRV12.Text = "Hora de Salida"
        ElseIf cboRV.SelectedValue = "3" Then
            cboRVisitante.Items.Clear()
            optRV.Enabled = False
            lblRV13.Visible = True
            txtRVHSalida.Visible = True
            lblRV12.Text = "Hora de Entrada"
            lblRV13.Text = "Hora de Salida"
            txtRVHora.Text = FormatoHora(HoraActual)
            txtRVHSalida.Text = FormatoHora(HoraActual)
            cboRVisitante.Enabled = True
            Call Lista_Visitante()
        End If
    End Sub
    Private Sub Lista_Visitante()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        lblRVError.Text = ""
        Try
            cboRVisitante.Items.Clear()
            Dim psFecha As String = Right(txtRVFecha.Text, 4) + Mid(txtRVFecha.Text, 4, 2) + Left(txtRVFecha.Text, 2)
            'listar todos los registros tipo entrada para colocar la salida
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = "SELECT VISITA_CODIGO,VISITA_TIPO_VISITANTE, PERSONAL_CODIGO, " _
                & " (CASE VISITA_TIPO_VISITANTE " _
                & " WHEN '1' THEN (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES  FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_CODIGO) " _
                & " WHEN '2' THEN VISITA_APEPAT + ' ' + VISITA_APEMAT + ', ' + VISITA_NOMBRES + ' (' + " _
                & " (SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO= VISITA_TIPOPERSONA AND ELEMEN_TABLA='TBOPC001') + ') (' + " _
                & " (SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC009' AND ELEMEN_CODIGO = VISITA_TIPODOCIDE) +' N° '+ VISITA_NRODOCIDE + ')' " _
                & " END) AS VISITANTE " _
                & " FROM TBVISITAS V WHERE (VISITA_SYS_EST = '0') AND (PCONTROL_CODIGO =" & cboPtoControl.SelectedValue.Trim & ") AND (VISITA_FECHA = '" & psFecha & "') AND (VISITA_TIPOMOV = '1')"
            Rs = cmdGlobal.ExecuteReader
            cboRVisitante.DataSource = Rs
            cboRVisitante.DataTextField = "VISITANTE"
            cboRVisitante.DataValueField = "VISITA_CODIGO"
            cboRVisitante.DataBind()
            Rs.Close()
            cboRVisitante.Items.Add("< Seleccionar >") : cboRVisitante.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblRVError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblRVError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnRVGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim Rs2 As SqlClient.SqlDataReader
        Dim TipoVisita As String = ""
        Dim AQuien As String = ""
        Dim psCodVisita As String = ""
        lblRVError.Text = ""
        If cboRV.SelectedValue = "Seleccionar" Then lblRVError.Text = "<br> - Falta seleccionar el tipo de registro."
        If cboRVisitante.Enabled = True And cboRVisitante.SelectedValue = "< Seleccionar >" Then lblRVError.Text = lblRVError.Text & "<br> - Falta seleccionar al Visitante que le registrará la Hora de Salida."
        If optRV.SelectedValue = "Personal que Labora" Then
            If txtRVEmpresa.Text = "" Then lblRVError.Text = lblRVError.Text & "<br> - Falta seleccionar el personal que labora."
            If cboRVTipoVisita.SelectedValue <> "< Seleccionar >" Then
                TipoVisita = cboRVTipoVisita.SelectedValue.Trim
            Else
                TipoVisita = ""
            End If
            If cboRVPersonal.SelectedValue <> "< Seleccionar >" Then
                AQuien = cboRVPersonal.SelectedValue.Trim
            Else
                AQuien = ""
            End If
        Else
            If cboRVTipoPer.SelectedValue = "< Seleccionar >" Then lblRVError.Text = lblRVError.Text & "<br> - Falta seleccionar tipo de persona que es el visitante."
            If txtRVApePat.Text.Trim = "" Then lblRVError.Text = lblRVError.Text & "<br> - Falta ingresar el Apellido de la Persona visitante."
            If txtRVNombres.Text.Trim = "" Then lblRVError.Text = lblRVError.Text & "<br> - Falta ingresar el o los Nombres de la Persona visitante."
            If cboRVTipoDoc.SelectedValue = "< Seleccionar >" Then lblRVError.Text = lblRVError.Text & "<br> - Falta seleccionar el Documento de Identidad de la Persona visitante."
            If txtRVNroDoc.Text.Trim = "" Then lblRVError.Text = lblRVError.Text & "<br> - Falta ingresar el Nº de Documento de Identidad de la Persona visitante."
            If cboRVTipoVisita.SelectedValue = "< Seleccionar >" Then lblRVError.Text = lblRVError.Text & "<br> - Falta ingresar la visita de que tipo es."
            If cboRVPersonal.SelectedValue = "< Seleccionar >" Then lblRVError.Text = lblRVError.Text & "<br> - Falta seleccionar al Personal que visita."
            TipoVisita = cboRVTipoVisita.SelectedValue.Trim
            AQuien = cboRVPersonal.SelectedValue.Trim
        End If
        If lblRVError.Text <> "" Then
            lblError.Text = "Existe las sgtes. observaciones: " & lblError.Text
            Exit Sub
        End If
        Try
            Dim ValorSys As String = FechaActual() + HoraActual() + HttpContext.Current.User.Identity.Name
            Dim psFecha As String = Right(txtRVFecha.Text, 4) + Mid(txtRVFecha.Text, 4, 2) + Left(txtRVFecha.Text, 2)
            Cn.Open() : Cn2.Open()
            cmdGlobal.Connection = Cn : cmdGlobal2.Connection = Cn2
            If cboRV.SelectedValue.Trim = "1" Or cboRV.SelectedValue.Trim = "2" Then
                cmdGlobal.CommandText = "SELECT MAX(VISITA_CODIGO) FROM TBVISITAS"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodVisita = Nz(Rs(0)) + 1
                    End While
                Else
                    psCodVisita = 1
                End If
                Rs.Close()
                cmdGlobal.CommandText = "INSERT INTO TBVISITAS(VISITA_CODIGO, PCONTROL_CODIGO, VISITA_TIPOMOV,VISITA_SYS_EST,VISITA_SYS_CRE,VISITA_FECHA, TARJETA_VISITA)" _
                                   & "VALUES(" & psCodVisita & "," & LblRVPtoControl.Text.Trim & "," _
                                   & "'" & CboRV.SelectedValue.Trim & "','0','" & ValorSys & "','" & psFecha & "', '" & TxtTarjeta.Text.Trim & "')"
                cmdGlobal.ExecuteNonQuery()
                If cboRV.SelectedValue.Trim = "1" Then
                    cmdGlobal.CommandText = "UPDATE TBVISITAS SET PCONTROL_CODIGO='" & lblRVPtoControl.Text.Trim & "',VISITA_HORA_ENTRADA='" & Left(txtRVHora.Text, 2) + Right(txtRVHora.Text, 2) & "',VISITA_HORA_SALIDA=NULL,PERSONAL_REGISTRA_ENTRADA='" & txtRVCodPersonaControl.Text.Trim & "',VISITA_ESTADO='2' WHERE VISITA_CODIGO=" & psCodVisita & ""
                    cmdGlobal.ExecuteNonQuery()
                Else
                    cmdGlobal.CommandText = "UPDATE TBVISITAS SET VISITA_HORA_SALIDA='" & Left(txtRVHora.Text, 2) + Right(txtRVHora.Text, 2) & "',VISITA_HORA_ENTRADA=NULL,PERSONAL_REGISTRA_SALIDA='" & txtRVCodPersonaControl.Text.Trim & "',VISITA_ESTADO='3' WHERE VISITA_CODIGO=" & psCodVisita
                    cmdGlobal.ExecuteNonQuery()
                End If
            ElseIf cboRV.SelectedValue.Trim = "3" Then
                psCodVisita = txtRVCodVisita.Text.Trim
                cmdGlobal.CommandText = "UPDATE TBVISITAS SET VISITA_TIPOMOV='3',VISITA_HORA_ENTRADA='" & Left(TxtRVHora.Text, 2) + Right(TxtRVHora.Text, 2) & "',VISITA_HORA_SALIDA='" & Left(TxtRVHSalida.Text, 2) + Right(TxtRVHSalida.Text, 2) & "',PERSONAL_REGISTRA_SALIDA='" & TxtRVCodPersonaControl.Text.Trim & "',VISITA_ESTADO='3' WHERE VISITA_CODIGO=" & psCodVisita
                cmdGlobal.ExecuteNonQuery()
            End If
            If optRV.SelectedValue.Trim = "Personal que Labora" Then
                cmdGlobal.CommandText = "UPDATE TBVISITAS SET VISITA_TIPO_VISITANTE='1',PERSONAL_CODIGO='" & txtRVEmpresa.Text.Trim & "' WHERE VISITA_CODIGO=" & psCodVisita
                cmdGlobal.ExecuteNonQuery()
            ElseIf cboRV.SelectedValue.Trim <> "3" Then
                cmdGlobal.CommandText = "UPDATE TBVISITAS SET VISITA_TIPO_VISITANTE='2',VISITA_APEPAT='" & txtRVApePat.Text.Trim & "',VISITA_APEMAT='" & txtRVApeMat.Text.Trim & "',VISITA_NOMBRES='" & txtRVNombres.Text.Trim & "',VISITA_EMPRESA = '" & txtRVEmpresa.Text.Trim & "', " _
                                      & "VISITA_NRODOCIDE='" & txtRVNroDoc.Text.Trim & "',VISITA_TIPODOCIDE='" & cboRVTipoDoc.SelectedValue.Trim & "',VISITA_TIPOPERSONA='" & cboRVTipoPer.SelectedValue.Trim & "'" _
                                      & "WHERE VISITA_CODIGO=" & psCodVisita
                cmdGlobal.ExecuteNonQuery()
            End If
            If cboRV.SelectedValue.Trim <> "3" Then
                cmdGlobal.CommandText = "UPDATE TBVISITAS SET VISITA_TIPO='" & TipoVisita & "', VISITA_ASUNTO='" & txtRVAsunto.Text.Trim & "',PERSONAL_QUE_VISITA='" & AQuien & "' WHERE VISITA_CODIGO=" & psCodVisita
                cmdGlobal.ExecuteNonQuery()
            End If
            Dim psCodDato As String = ""
            If cboRV.SelectedValue.Trim <> "3" And optRV.SelectedValue.Trim <> "Personal que Labora" Then
                If txtRVIDato.Text.Trim = "" Then
                    cmdGlobal.CommandText = " SELECT MAX(DATOPER_CODIGO) FROM TBVISITAS_DATAPERSONA "
                    Rs = cmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodDato = Nz(Rs(0)) + 1
                        End While
                    Else
                        psCodDato = 1
                    End If
                    Rs.Close()
                    cmdGlobal.CommandText = " INSERT INTO TBVISITAS_DATAPERSONA (DATOPER_CODIGO, DATOPER_TIPO, DATOPER_TIPO_DOC, DATOPER_NRO_DOC, " _
                                          & " DATOPER_EMPRESA, DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES, DATOPER_SYS_EST, DATOPER_SYS_CRE)  VALUES " _
                                          & " (" & psCodDato & ", '" & cboRVTipoPer.SelectedValue.Trim & "', '" & cboRVTipoDoc.SelectedValue.Trim & "', '" & txtRVNroDoc.Text.Trim & "', " _
                                          & " '" & txtRVEmpresa.Text.Trim & "', '" & txtRVApePat.Text.Trim & "', '" & txtRVApeMat.Text.Trim & "', '" & txtRVNombres.Text.Trim & "', '0', '" & ValorSys & "')"
                    cmdGlobal.ExecuteNonQuery()
                End If
                If txtRVCodDato.Text.Trim <> "" Then
                    cmdGlobal.CommandText = " SELECT * FROM TBVISITAS_DATAPERSONA " _
                        & " WHERE DATOPER_CODIGO = " & txtRVCodDato.Text.Trim & " AND DATOPER_SYS_EST = '0' " _
                        & " AND DATOPER_TIPO = '" & cboRVTipoPer.SelectedValue.Trim & "' " _
                        & " AND DATOPER_TIPO_DOC = '" & cboRVTipoDoc.SelectedValue.Trim & "' " '_
                    Rs = cmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            cmdGlobal2.CommandText = " UPDATE TBVISITAS_DATAPERSONA SET " _
                                                  & " DATOPER_EMPRESA = '" & txtRVEmpresa.Text.Trim & "', " _
                                                  & " DATOPER_APEPAT = '" & txtRVApePat.Text.Trim & "', " _
                                                  & " DATOPER_APEMAT = '" & txtRVApeMat.Text.Trim & "', " _
                                                  & " DATOPER_NOMBRES = '" & txtRVNombres.Text.Trim & "', " _
                                                  & " DATOPER_NRO_DOC = '" & txtRVNroDoc.Text.Trim & "' " _
                                                  & " WHERE DATOPER_CODIGO = " & txtRVCodDato.Text.Trim & " AND DATOPER_SYS_EST = '0' " _
                                                  & " AND DATOPER_TIPO_DOC = '" & cboRVTipoDoc.SelectedValue.Trim & "' " _
                                                  & " AND DATOPER_TIPO = '" & cboRVTipoPer.SelectedValue.Trim & "'"
                            cmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        cmdGlobal2.CommandText = " SELECT MAX(DATOPER_CODIGO) FROM TBVISITAS_DATAPERSONA "
                        Rs2 = cmdGlobal2.ExecuteReader
                        If Rs2.HasRows Then
                            While Rs2.Read
                                psCodDato = Nz(Rs2(0)) + 1
                            End While
                        Else
                            psCodDato = 1
                        End If
                        Rs2.Close()
                        cmdGlobal2.CommandText = " INSERT INTO TBVISITAS_DATAPERSONA (DATOPER_CODIGO, DATOPER_TIPO, DATOPER_TIPO_DOC, DATOPER_NRO_DOC, " _
                                              & " DATOPER_EMPRESA, DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES, DATOPER_SYS_EST, DATOPER_SYS_CRE)  VALUES " _
                                              & " (" & psCodDato & ", '" & cboRVTipoPer.SelectedValue.Trim & "', '" & cboRVTipoDoc.SelectedValue.Trim & "', '" & txtRVNroDoc.Text.Trim & "', " _
                                              & " '" & txtRVEmpresa.Text.Trim & "', '" & txtRVApePat.Text.Trim & "', '" & txtRVApeMat.Text.Trim & "', '" & txtRVNombres.Text.Trim & "', '0', '" & ValorSys & "')"
                        cmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()
                End If
            End If
            Dim i As Integer = 0
            Dim psCodEquipo As String = ""
            Dim psNroSerie As String = ""
            Dim psMarca As String = ""
            Dim psDescripcion As String = ""
            If FlexEquipos.Rows.Count > 0 And cboRV.SelectedValue.Trim <> "3" Then
                For i = 0 To FlexEquipos.Rows.Count - 1
                    psCodEquipo = ""
                    If Replace(FlexEquipos.Rows(i).Cells(4).Text, "&nbsp;", "") = "" Then
                        cmdGlobal.CommandText = " SELECT MAX(EQ_CODIGO) FROM TBVISITAS_EQUIPO "
                        Rs = cmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psCodEquipo = Nz(Rs(0)) + 1
                            End While
                        Else
                            psCodEquipo = 1
                        End If
                        Rs.Close()
                        psNroSerie = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        psMarca = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        psDescripcion = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        cmdGlobal.CommandText = " INSERT INTO TBVISITAS_EQUIPO (EMPRESA_CODIGO, EQ_CODIGO, EQ_SERIE, EQ_MARCA, EQ_DESCRIPCION, EQ_SYS_EST, EQ_SYS_CRE) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "'," & psCodEquipo & ", '" & psNroSerie & "', '" & psMarca & "', '" & psDescripcion & "', '0', '" & ValorSys & "')"
                        cmdGlobal.ExecuteNonQuery()
                        cmdGlobal.CommandText = " INSERT INTO TBVISITAS_XEQUIPO (EMPRESA_CODIGO, VISITA_CODIGO, EQ_CODIGO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "'," & psCodVisita & ", " & psCodEquipo & ")"
                        cmdGlobal.ExecuteNonQuery()
                    Else
                        psCodEquipo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        cmdGlobal.CommandText = " INSERT INTO TBVISITAS_XEQUIPO (EMPRESA_CODIGO, VISITA_CODIGO, EQ_CODIGO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "'," & psCodVisita & ", " & psCodEquipo & ")"
                        cmdGlobal.ExecuteNonQuery()
                    End If
                Next
            End If
            btnRVRegresar_Click(sender, e)
        Catch ex As SqlException
            lblRVError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblRVError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboRVisitante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim Rs2 As SqlClient.SqlDataReader
        Try
            optRV.Enabled = False
            txtRVHora.Enabled = False
            If cboRVisitante.SelectedValue = "< Seleccionar >" Then
                optRV.SelectedValue = "Personal en General"
                optRV_SelectedIndexChanged(sender, e)
                cboRVPersonal.SelectedValue = "< Seleccionar >"
                txtRVAsunto.Text = ""
                cboRVTipoVisita.SelectedValue = "< Seleccionar >"
                cboRVTipoDoc.SelectedValue = "< Seleccionar >"
                txtRVHora.Enabled = True
                txtRVHora.Text = FormatoHora(HoraActual)
            Else
                txtRVCodVisita.Text = cboRVisitante.SelectedValue.Trim
                Cn.Open() : Cn2.Open()
                cmdGlobal.Connection = Cn : cmdGlobal2.Connection = Cn2
                cmdGlobal.CommandText = " SELECT PCONTROL_CODIGO, VISITA_TIPO_VISITANTE,PERSONAL_CODIGO, VISITA_CODIGO, " _
                                      & " (SELECT PERSON_APEPAT FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_CODIGO) AS PER_APEPAT, " _
                                      & " (SELECT PERSON_APEMAT FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_CODIGO) AS PER_APEMAT, " _
                                      & " (SELECT PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO = PERSONAL_CODIGO) AS PER_NOMBRES, " _
                                      & " VISITA_TIPOPERSONA,VISITA_APEPAT , VISITA_APEMAT, VISITA_NOMBRES, VISITA_EMPRESA, " _
                                      & " VISITA_TIPODOCIDE, VISITA_NRODOCIDE, VISITA_TIPO, VISITA_ASUNTO, VISITA_HORA_ENTRADA,PERSONAL_QUE_VISITA" _
                                      & " FROM TBVISITAS V WHERE (VISITA_SYS_EST = '0') AND (VISITA_CODIGO = " & cboRVisitante.SelectedValue.Trim & ")"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        If Nu(Rs!VISITA_TIPO_VISITANTE) = "1" Then
                            optRV.SelectedValue = "Personal que Labora"
                            optRV_SelectedIndexChanged(sender, e)
                            txtRVApePat.Text = Nu(Rs!PER_APEPAT)
                            txtRVApeMat.Text = Nu(Rs!PER_APEMAT)
                            txtRVNombres.Text = Nu(Rs!PER_NOMBRES)
                            txtRVEmpresa.Text = Nu(Rs!PERSONAL_CODIGO)
                        Else
                            optRV.SelectedValue = "Personal en General"
                            optRV_SelectedIndexChanged(sender, e)
                            cboRVTipoPer.SelectedValue = Nu(Rs!VISITA_TIPOPERSONA)
                            txtRVApeMat.Text = Nu(Rs!VISITA_APEMAT)
                            txtRVApePat.Text = Nu(Rs!VISITA_APEPAT)
                            txtRVNombres.Text = Nu(Rs!VISITA_NOMBRES)
                            txtRVEmpresa.Text = Nu(Rs!VISITA_EMPRESA)
                            txtRVNroDoc.Text = Nu(Rs!VISITA_NRODOCIDE)
                            cboRVTipoDoc.SelectedValue = Nu(Rs!VISITA_TIPODOCIDE)
                        End If
                        cboRVPersonal.SelectedValue = Nz(Rs!PERSONAL_QUE_VISITA)
                        txtRVAsunto.Text = Nu(Rs!VISITA_ASUNTO)
                        If Nu(Rs!VISITA_TIPO) = "" Then
                            cboRVTipoVisita.SelectedValue = "< Seleccionar >"
                        Else
                            cboRVTipoVisita.SelectedValue = Nz(Rs!VISITA_TIPO)
                        End If
                        txtRVHora.Enabled = True
                        txtRVHora.Text = Left(Nu(Rs!VISITA_HORA_ENTRADA), 2) & ":" & Right(Nu(Rs!VISITA_HORA_ENTRADA), 2)
                        txtRVNroSerie.Text = ""
                        txtRVMarca.Text = ""
                        txtRVDescripcion.Text = ""
                        txtRVCodEquipo.Text = ""
                        Dim dtListado As New DataTable
                        Dim drT As DataRow
                        Dim i As Long = 0
                        dtListado.Columns.Add("c1")
                        dtListado.Columns.Add("c2")
                        dtListado.Columns.Add("c3")
                        dtListado.Columns.Add("c4")
                        dtListado.Columns.Add("c5")
                        cmdGlobal2.CommandText = " SELECT  VE.EQ_CODIGO, EQ_SERIE, EQ_MARCA, EQ_DESCRIPCION " _
                                  & " FROM TBVISITAS_EQUIPO VE INNER JOIN TBVISITAS_XEQUIPO VXE " _
                                  & " ON VE.EQ_CODIGO = VXE.EQ_CODIGO " _
                                  & " WHERE VXE.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                  & " AND VE.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                  & " AND EQ_SYS_EST = '0' AND VXE.VISITA_CODIGO = " & Nu(Rs!VISITA_CODIGO)
                        Rs2 = cmdGlobal2.ExecuteReader
                        If Rs2.HasRows Then
                            While Rs2.Read
                                i = i + 1
                                drT = dtListado.NewRow()
                                drT("c1") = i
                                drT("c2") = Nu(Rs2!EQ_SERIE)
                                drT("c3") = Nu(Rs2!EQ_MARCA)
                                drT("c4") = Nu(Rs2!EQ_DESCRIPCION)
                                drT("c5") = Nu(Rs2!EQ_CODIGO)
                                dtListado.Rows.Add(drT)
                            End While
                        End If
                        Rs2.Close()
                        FlexEquipos.DataSource = dtListado
                        FlexEquipos.DataBind()
                    End While
                End If
                Rs.Close()
            End If
        Catch ex As SqlException
            lblRVError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblRVError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub Listar_Personal()
        Dim psCodGrupo As Double = 0
        psCodGrupo = Session("CodGrupoEmpresa")
        Try
            Dim obj As New clsControlPersonal
            FlexP.DataSource = obj.Listar_Personal("0", "00", txtBusApePat.Text.Trim, "", "", psCodGrupo, Session("CodEmpresa"), "1")
            FlexP.SelectedIndex = -1
            FlexP.DataBind()
        Catch ex As SqlException
            lblRVError.Text = ex.Message
        Catch Ex As Exception
            lblRVError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Lista_Persona()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("PERSON_CODIGO")
            dtListado.Columns.Add("TIPO_PER")
            dtListado.Columns.Add("PERSON_APEPAT")
            dtListado.Columns.Add("PERSON_APEMAT")
            dtListado.Columns.Add("PERSON_NOMBRES")
            dtListado.Columns.Add("EMPRESA")
            dtListado.Columns.Add("TIPO_DOC")
            dtListado.Columns.Add("PERSON_NUMDOCIDE")
            dtListado.Columns.Add("TIPO_CODDOC")
            dtListado.Columns.Add("TIPO_CODPER")
            Cn.Open() : Cn2.Open()
            cmdGlobal.Connection = Cn : cmdGlobal2.Connection = Cn2
            cmdGlobal.CommandText = " SELECT DATOPER_CODIGO, DATOPER_TIPO, DATOPER_EMPRESA , DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES, DATOPER_TIPO_DOC, DATOPER_NRO_DOC, " _
                & " (SELECT ELEMEN_VALOR From dbo.TBCELEMEN WHERE (ELEMEN_CODIGO = DP.DATOPER_TIPO) AND (ELEMEN_TABLA = 'TBOPC001')) AS TIPOPERSONA, " _
                & " (SELECT ELEMEN_VALOR FROM dbo.TBCELEMEN WHERE (ELEMEN_TABLA = 'TBOPC009') AND (ELEMEN_CODIGO = DP.DATOPER_TIPO_DOC)) AS TIPODOCIDE " _
                & " FROM dbo.TBVISITAS_DATAPERSONA AS DP WHERE (DATOPER_SYS_EST = '0') "
            If cboBusTipoPer.SelectedValue <> "< Seleccionar >" Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND DATOPER_TIPO = '" & cboBusTipoPer.SelectedValue.Trim & "'"
            If txtBusApePat.Text.Trim <> "" Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND UPPER(DATOPER_APEPAT) LIKE '" & UCase(txtBusApePat.Text.Trim) & "%'"
            cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY DATOPER_EMPRESA , DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("PERSON_CODIGO") = Nu(Rs!DATOPER_CODIGO)
                    drT("TIPO_PER") = Nu(Rs!tipopersona)
                    drT("PERSON_APEPAT") = Nu(Rs!DATOPER_APEPAT)
                    drT("PERSON_APEMAT") = Nu(Rs!DATOPER_APEMAT)
                    drT("PERSON_NOMBRES") = Nu(Rs!DATOPER_NOMBRES)
                    drT("EMPRESA") = Nu(Rs!DATOPER_EMPRESA)
                    drT("TIPO_DOC") = Nu(Rs!TIPODOCIDE)
                    drT("PERSON_NUMDOCIDE") = Nu(Rs!DATOPER_NRO_DOC)
                    drT("TIPO_CODDOC") = Nu(Rs!DATOPER_TIPO_DOC)
                    drT("TIPO_CODPER") = Nu(Rs!DATOPER_TIPO)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexP.DataSource = dtListado
            FlexP.DataBind()
        Catch ex As SqlException
            lblRVError.Text = ex.Message
        Catch Ex As Exception
            lblRVError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        'lblPError.Text = ""
        'If FlexP.Rows(Index).Cells(2).Text = "SI" Then lblPError.Text = "El personal escogido ya se encuentra registrado como usuario del sistema." : Exit Sub
        If e.CommandName = "Aceptar" Then
            If optRV.SelectedValue = "Personal en General" Then
                If FlexP.Rows(Index).Cells(1).Text <> "&nbsp;" Then txtRVCodDato.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(3).Text <> "&nbsp;" Then txtRVApePat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(4).Text <> "&nbsp;" Then txtRVApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtRVNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(8).Text <> "&nbsp;" Then txtRVNroDoc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(6).Text <> "&nbsp;" Then txtRVEmpresa.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(9).Text <> "&nbsp;" Then CboRVTipoDoc.SelectedValue = Funciones.Llenar_Ceros(UCase(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")), 2)
                If FlexP.Rows(Index).Cells(10).Text <> "&nbsp;" Then cboRVTipoPer.SelectedValue = UCase(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"))
                txtRVIDato.Text = "N"
                ModalPopupExtender1.Hide()
                cboBusTipoPer.SelectedValue = "< Seleccionar >"
                txtBusApePat.Text = ""
                FlexP.DataSource = Nothing
                FlexP.DataBind()
            ElseIf optRV.SelectedValue = "Personal que Labora" Then
                If FlexP.Rows(Index).Cells(1).Text <> "&nbsp;" Then txtRVEmpresa.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(3).Text <> "&nbsp;" Then txtRVApePat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(4).Text <> "&nbsp;" Then txtRVApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtRVNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(8).Text <> "&nbsp;" Then txtRVNroDoc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtRVIDato.Text = ""
                If FlexP.Rows(Index).Cells(9).Text <> "&nbsp;" Then cboRVTipoDoc.SelectedValue = UCase(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"))
                ModalPopupExtender1.Hide()
                cboBusTipoPer.SelectedValue = "< Seleccionar >"
                txtBusApePat.Text = ""
                FlexP.DataSource = Nothing
                FlexP.DataBind()
            End If
        End If
    End Sub
    Protected Sub btnRVBusEq_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT  EQ_CODIGO, EQ_SERIE, EQ_MARCA, EQ_DESCRIPCION " _
                                  & " FROM TBVISITAS_EQUIPO " _
                                  & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                  & " AND EQ_SYS_EST = '0' AND EQ_SERIE = '" & txtRVNroSerie.Text.Trim & "'"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtRVNroSerie.Text = Nu(Rs!EQ_SERIE)
                    txtRVMarca.Text = Nu(Rs!EQ_MARCA)
                    txtRVDescripcion.Text = Nu(Rs!EQ_DESCRIPCION)
                    txtRVCodEquipo.Text = Nu(Rs!EQ_CODIGO)
                End While
            Else
                txtRVMarca.Text = ""
                txtRVDescripcion.Text = ""
                txtRVCodEquipo.Text = ""
            End If
        Catch ex As SqlException
            lblRVError.Text = ex.Message
        Catch Ex As Exception
            lblRVError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnRVIngEquipo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim i As Long = 0
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c2")
        dtListado.Columns.Add("c3")
        dtListado.Columns.Add("c4")
        dtListado.Columns.Add("c5")
        For i = 0 To FlexEquipos.Rows.Count - 1
            i = i + 1
            drT = dtListado.NewRow()
            drT("c1") = i
            drT("c2") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("c3") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("c4") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("c5") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEquipos.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            dtListado.Rows.Add(drT)
        Next
        i = i + 1
        drT = dtListado.NewRow()
        drT("c1") = i
        drT("c2") = txtRVNroSerie.Text
        drT("c3") = txtRVMarca.Text
        drT("c4") = txtRVDescripcion.Text
        drT("c5") = txtRVCodEquipo.Text
        dtListado.Rows.Add(drT)
        FlexEquipos.DataSource = dtListado
        FlexEquipos.DataBind()
        txtRVNroSerie.Text = ""
        txtRVMarca.Text = ""
        txtRVDescripcion.Text = ""
        txtRVCodEquipo.Text = ""
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            If e.CommandName = "Equipo" Then
                Cn.Open()
                cmdGlobal.Connection = Cn
                Dim dtListado As New DataTable
                Dim drT As DataRow
                Dim i As Long = 0
                dtListado.Columns.Add("c1")
                dtListado.Columns.Add("Nro_Serie")
                dtListado.Columns.Add("Marca")
                dtListado.Columns.Add("Descripcion")
                cmdGlobal.CommandText = " SELECT  VE.EQ_CODIGO, EQ_SERIE, EQ_MARCA, EQ_DESCRIPCION " _
                                  & " FROM TBVISITAS_EQUIPO VE INNER JOIN TBVISITAS_XEQUIPO VXE " _
                                  & " ON VE.EQ_CODIGO = VXE.EQ_CODIGO " _
                                  & " WHERE VXE.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                  & " AND VE.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                  & " AND EQ_SYS_EST = '0' AND VXE.VISITA_CODIGO = " & Flex.Rows(Index).Cells(14).Text
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        i = i + 1
                        drT = dtListado.NewRow()
                        drT("c1") = i
                        drT("Nro_Serie") = Nu(Rs!EQ_SERIE)
                        drT("Marca") = Nu(Rs!EQ_MARCA)
                        drT("Descripcion") = Nu(Rs!EQ_DESCRIPCION)
                        dtListado.Rows.Add(drT)
                    End While
                End If
                FlexEquipo.DataSource = dtListado
                FlexEquipo.DataBind()
            End If
        Catch ex As SqlException
            lblRVError.Text = ex.Message
        Catch Ex As Exception
            lblRVError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnBPCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        cboBusTipoPer.SelectedValue = "< Seleccionar >"
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub btnBPListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If optRV.SelectedValue = "Personal en General" Then
            Call Lista_Persona()
            ModalPopupExtender1.Show()
        Else
            Call Listar_Personal()
            ModalPopupExtender1.Show()
        End If
    End Sub

    Private Sub Llenar_Agencia()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        lblRVError.Text = ""
        Dim psCodEmpresa As String = ""
        psCodEmpresa = Session("CodEmpresa")
        Try
            cboRVAgencia.Items.Clear()
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = "SELECT A.AGENCIA_CODIGO, AGENCIA_NOMBRE+' PISO '+AGENCIA_PISO+' OF. '+ AGENCIA_OFICINA AS AGENCIA " _
                                  & " FROM TBAGENCIAS A INNER JOIN TBPUNTOSCONTROL_AGENCIA PA ON PA.AGENCIA_CODIGO = A.AGENCIA_CODIGO " _
                                  & " AND PA.GRPOEMPRESA_CODIGO = A.GRPOEMPRESA_CODIGO AND PA.EMPRESA_CODIGO = A.EMPRESA_CODIGO " _
                                  & " WHERE AGENCIA_SYS_EST='0' AND A.GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & " " _
                                  & " AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' " _
                                  & " AND PCONTROL_CODIGO = '" & cboRVPtoControl.SelectedValue.Trim & "'"
            Rs = cmdGlobal.ExecuteReader
            cboRVAgencia.DataSource = Rs
            cboRVAgencia.DataTextField = "AGENCIA"
            cboRVAgencia.DataValueField = "AGENCIA_CODIGO"
            cboRVAgencia.DataBind()
            Rs.Close()
            cboRVAgencia.Items.Add("< Seleccionar >") : cboRVAgencia.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblRVError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblRVError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub

    Protected Sub CboRVAgencia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboRVAgencia.SelectedIndexChanged
        Try
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim cmdGlobal As New SqlCommand
            Dim Rs As SqlClient.SqlDataReader
            Dim psCodEmpresa As String = ""
            psCodEmpresa = Session("CodEmpresa")
            CboRVPersonal.Items.Clear()
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT PL.PERSONAL_CODIGO,P.PERSON_APEPAT + ' ' + P.PERSON_APEMAT + ', ' + P.PERSON_NOMBRES AS NOMBRESP,PERSON_ANEXO1_emp " _
                                  & " FROM TBPERSONAL_XAGENCIA PL INNER JOIN TBPERSONAL P ON PL.PERSONAL_CODIGO = P.PERSON_CODIGO " _
                                  & " INNER JOIN TBPERSONAL_EMPRESAS PE ON P.PERSON_CODIGO=PE.PERSONAL_CODIGO " _
                                  & " WHERE PE.GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & "  " _
                                  & " AND PE.EMPRESA_CODIGO = '" & psCodEmpresa & "' " _
                                  & " AND PL.AGENCIA_CODIGO = '" & CboRVAgencia.SelectedValue.Trim & "'"
            Rs = cmdGlobal.ExecuteReader
            CboRVPersonal.DataSource = Rs
            CboRVPersonal.DataTextField = "NOMBRESP"
            CboRVPersonal.DataValueField = "PERSONAL_CODIGO"
            CboRVPersonal.DataBind()
            Rs.Close()
            CboRVPersonal.Items.Add("< Seleccionar >") : CboRVPersonal.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            LblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            LblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub

    Private Sub CboRVPtoControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CboRVPtoControl.SelectedIndexChanged
        Call Llenar_Agencia()
    End Sub

    Private Sub TxtRVNroDoc_TextChanged(sender As Object, e As EventArgs) Handles TxtRVNroDoc.TextChanged

        If TxtRVNroDoc.Text = "" Then Exit Sub
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader

        TxtRVApePat.Text = ""
        TxtRVApeMat.Text = ""
        TxtRVNombres.Text = ""
        TxtRVEmpresa.Text = ""
        'CboRVTipoDoc.SelectedValue = "01"
        Try
            Cn.Open()
            cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT DATOPER_CODIGO, DATOPER_TIPO, DATOPER_TIPO_DOC, DATOPER_NRO_DOC, DATOPER_EMPRESA, DATOPER_APEPAT, DATOPER_APEMAT, " _
                & " DATOPER_NOMBRES From dbo.TBVISITAS_DATAPERSONA " _
                & " WHERE (DATOPER_SYS_EST = '0') and UPPER(DATOPER_NRO_DOC) ='" & UCase(TxtRVNroDoc.Text) & "'"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    TxtRVApePat.Text = Nu(Rs!DATOPER_APEPAT)
                    TxtRVApeMat.Text = Nu(Rs!DATOPER_APEMAT)
                    TxtRVNombres.Text = Nu(Rs!DATOPER_NOMBRES)
                    TxtRVEmpresa.Text = Nu(Rs!DATOPER_EMPRESA)
                    TxtRVNroDoc.Text = Nu(Rs!DATOPER_NRO_DOC)
                    CboRVTipoDoc.SelectedValue = Funciones.Llenar_Ceros(Nu(Rs!DATOPER_TIPO_DOC), 2)
                    CboRVTipoPer.SelectedValue = Nu(Rs!DATOPER_TIPO)
                End While
            End If
        Catch ex As SqlException
            LblRVError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            LblRVError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally

        End Try

    End Sub
End Class
