Imports System.Data
Imports WebGestor
Imports System.Configuration
Imports System.Data.SqlClient
Partial Class Cas_Relacion_Estadisticas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'LLENA EL COMBO CON LOS REPORTES
            cboReportes.Items.Add("< Seleccionar >")
            cboReportes.Items.Add("POR OFICINA")
            cboReportes.Items.Add("POR CARGOS")
            cboReportes.SelectedValue = "< Seleccionar >"
            Call Cargar_Oficina()
            Call LlenaMes(cboMes, True)
            cboMes.Items.Add("< Seleccionar >") : cboMes.SelectedValue = "< Seleccionar >"
            Call LlenaAno(cboAño)
            cboAño.SelectedValue = Left(FechaActual, 4)
        End If
    End Sub
    Private Sub Cargar_Oficina()
        Dim dt As New DataTable
        Dim obj As New ModuloCas
        cboOficina.Items.Clear()
        Try
            dt = obj.CasLista_Oficina(Session("Ruta_Emp"))
            cboOficina.DataSource = dt
            cboOficina.DataTextField = "DESCRIPCION"
            cboOficina.DataValueField = "TBCAS_OFICINA_CODIGO"
            cboOficina.DataBind()
            cboOficina.Items.Add("< Seleccionar >") : cboOficina.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
            'Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnVistaPrevia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVistaPrevia.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql2 As New SqlCommand
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql3 As New SqlCommand
        Dim Campo As String : Campo = ""
        Dim Sql As String : Sql = ""
        Dim pFecIni As String, pFecFin As String : pFecFin = "" : pFecIni = ""
        Sql = ""
        'pFecIni = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
        'pFecFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        'If txtFechaIni.Text = "" And txtFechaFin.Text = "" Then
        'Else
        '    If txtFechaFin.Text = "" And txtFechaIni.Text <> "" Then
        '        Campo = " AND (I.APROB_FECHA_REPORTA=I2.APROB_FECHA_REPORTA)"
        '    ElseIf txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then
        '        Campo = " AND (I.APROB_FECHA_REPORTA=I2.APROB_FECHA_REPORTA)"
        '    Else
        '        Campo = " AND (I.APROB_FECHA_REPORTA=I2.APROB_FECHA_REPORTA)"
        '    End If
        'End If
        'If txtFechaIni.Text = "" And txtFechaFin.Text = "" Then
        'Else
        '    If txtFechaFin.Text = "" And txtFechaIni.Text <> "" Then
        '        Sql = Sql & " AND (APROB_FECHA_REPORTA='" & pFecIni & "')"
        '    ElseIf txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then
        '        Sql = Sql & " AND (APROB_FECHA_REPORTA='" & pFecFin & "')"
        '    Else
        '        Sql = Sql & " AND (APROB_FECHA_REPORTA BETWEEN '" & pFecIni & "' AND '" & pFecFin & "')"
        '    End If
        'End If
        If cboReportes.SelectedValue = "POR OFICINA" Then
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_Cas_TematicasXOficinas]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_Cas_TematicasXOficinas]"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = " CREATE VIEW V_Cas_TematicasXOficinas AS SELECT SUBSTRING(I.APROB_FECHA_REPORTA, 1, 4) AS AÑO, SUBSTRING(I.APROB_FECHA_REPORTA, 5, 2) AS MES, P.TBCAS_OFICINA," _
                               & " (SELECT TBCAS_OFICINA_NOMBRE FROM dbo.TBCAS_OFICINAS WHERE (TBCAS_OFICINA_CODIGO = P.TBCAS_OFICINA) AND (TBCAS_SYS_EST = '0')) AS OFICINA," _
                               & " (SELECT NIVEL1_DESCRIP FROM dbo.TBESP_CAS1 WHERE (I.APROB_TIPO = NIVEL1_CODIGO)) AS PRODUCTO, COUNT(I.APROB_CODIGO) AS CANT" _
                               & " FROM dbo.TBCAS_INCIDENTES AS I INNER JOIN dbo.TBCAS_PERSONA AS P ON I.APROB_USUARIO_REPORTA = P.TBCAS_PERSONA_USUARIO" _
                               & " WHERE I.APROB_SYS_EST='0' AND P.TBCAS_SYS_EST='0'"
            cmdSql.CommandText = cmdSql.CommandText & " GROUP BY I.APROB_TIPO, P.TBCAS_OFICINA, I.APROB_FECHA_REPORTA"
            cmdSql.ExecuteNonQuery()
            Cn2.Open()
            cmdSql2.Connection = Cn2
            cmdSql2.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_CasReporte_OfxProducto]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_CasReporte_OfxProducto]"
            cmdSql2.ExecuteNonQuery()
            cmdSql2.CommandText = " CREATE VIEW V_CasReporte_OfxProducto AS SELECT AÑO, MES, OFICINA, PRODUCTO, COUNT(CANT) AS TOTAL" _
                                & " FROM dbo.V_Cas_TematicasXOficinas" _
                                & " WHERE (AÑO = '" & cboAño.SelectedValue.Trim & "')"
            If cboOficina.SelectedValue <> "< Seleccionar >" Then cmdSql2.CommandText = cmdSql2.CommandText & " AND TBCAS_OFICINA='" & cboOficina.SelectedValue.Trim & "'"
            If cboMes.SelectedValue <> "< Seleccionar >" Then cmdSql2.CommandText = cmdSql2.CommandText & " AND MES='" & cboMes.SelectedValue.Trim & "'"
            cmdSql2.CommandText = cmdSql2.CommandText & " GROUP BY AÑO, MES, PRODUCTO, OFICINA"
            cmdSql2.ExecuteNonQuery()
            Cn3.Open()
            cmdSql3.Connection = Cn3
            cmdSql3.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_CasReporte_EMOficina]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_CasReporte_EMOficina]"
            cmdSql3.ExecuteNonQuery()
            cmdSql3.CommandText = " CREATE VIEW V_CasReporte_EMOficina AS SELECT AÑO, MES, OFICINA, COUNT(CANT) AS TOTAL" _
                                & " FROM dbo.V_Cas_TematicasXOficinas " _
                                & " WHERE (AÑO = '" & cboAño.SelectedValue.Trim & "') "
            If cboOficina.SelectedValue <> "< Seleccionar >" Then cmdSql3.CommandText = cmdSql3.CommandText & " AND TBCAS_OFICINA='" & cboOficina.SelectedValue.Trim & "'"
            cmdSql3.CommandText = cmdSql3.CommandText & " GROUP BY OFICINA, AÑO, MES"
            cmdSql3.ExecuteNonQuery()
            Response.Redirect("Cas_Rep_RegistroTematicasXOficina.aspx")
        End If
        If cboReportes.SelectedValue = "POR CARGOS" Then
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_Cas_CARGOSCONTACTANINICIAL]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_Cas_CARGOSCONTACTANINICIAL]"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = " CREATE VIEW V_Cas_CARGOSCONTACTANINICIAL AS SELECT (SELECT PUESTO_NOMBRE FROM TBCAS_PUESTO WHERE (PUESTO_CODIGO = P.TBCAS_PUESTO) and (puesto_sys_est='0')) AS PUESTO," _
                               & " COUNT(TBCAS_PUESTO) AS TOTAL,SUBSTRING(I.APROB_FECHA_REPORTA, 1, 4) AS AÑO, SUBSTRING(I.APROB_FECHA_REPORTA, 5, 2) AS MES " _
                               & " FROM TBCAS_PERSONA AS P INNER JOIN TBCAS_INCIDENTES AS I ON P.TBCAS_PERSONA_USUARIO=I.APROB_USUARIO_REPORTA" _
                               & " WHERE I.APROB_SYS_EST='0' AND P.TBCAS_SYS_EST='0'"
            'If Sql <> "" Then cmdSql.CommandText = cmdSql.CommandText & Sql
            cmdSql.CommandText = cmdSql.CommandText & " GROUP BY I.APROB_FECHA_REPORTA,TBCAS_PUESTO"
            cmdSql.ExecuteNonQuery()
            Cn2.Open()
            cmdSql2.Connection = Cn2
            cmdSql2.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[C_CAS_CARGOSCONTACTAN]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[C_CAS_CARGOSCONTACTAN]"
            cmdSql2.ExecuteNonQuery()
            cmdSql2.CommandText = " CREATE VIEW C_CAS_CARGOSCONTACTAN AS SELECT AÑO, MES, PUESTO, COUNT(TOTAL) AS CANT" _
                                & " FROM dbo.V_Cas_CARGOSCONTACTANINICIAL " _
                                & " WHERE (AÑO = '" & cboAño.SelectedValue.Trim & "') "
            If cboMes.SelectedValue <> "< Seleccionar >" Then cmdSql2.CommandText = cmdSql2.CommandText & " AND MES='" & cboMes.SelectedValue.Trim & "'"
            cmdSql2.CommandText = cmdSql2.CommandText & " GROUP BY PUESTO, TOTAL, MES, AÑO"
            cmdSql2.ExecuteNonQuery()
            Cn3.Open()
            cmdSql3.Connection = Cn3
            cmdSql3.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_CAS_DESGLOSECARGOSXOFICINA]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_CAS_DESGLOSECARGOSXOFICINA]"
            cmdSql3.ExecuteNonQuery()
            cmdSql3.CommandText = " CREATE VIEW V_CAS_DESGLOSECARGOSXOFICINA AS SELECT SUBSTRING(I.APROB_FECHA_REPORTA, 1, 4) AS AÑO, SUBSTRING(I.APROB_FECHA_REPORTA, 5, 2) AS MES," _
                                & " (SELECT TBCAS_OFICINA_NOMBRE FROM dbo.TBCAS_OFICINAS WHERE (TBCAS_OFICINA_CODIGO = P.TBCAS_OFICINA) AND (TBCAS_SYS_EST = '0')) AS OFICINA, P.TBCAS_OFICINA," _
                                & " (SELECT PUESTO_NOMBRE FROM dbo.TBCAS_PUESTO WHERE (PUESTO_CODIGO = P.TBCAS_PUESTO)) AS PUESTO, COUNT(P.TBCAS_PUESTO) AS TOTAL" _
                                & " FROM dbo.TBCAS_PERSONA AS P INNER JOIN dbo.TBCAS_INCIDENTES AS I ON P.TBCAS_PERSONA_USUARIO = I.APROB_USUARIO_REPORTA" _
                                & " WHERE (SUBSTRING(I.APROB_FECHA_REPORTA, 1, 4) = '" & cboAño.SelectedValue.Trim & "') "
            If cboMes.SelectedValue <> "< Seleccionar >" Then cmdSql3.CommandText = cmdSql3.CommandText & " AND SUBSTRING(I.APROB_FECHA_REPORTA, 5, 2)='" & cboMes.SelectedValue.Trim & "'"
            If cboOficina.SelectedValue <> "< Seleccionar >" Then cmdSql3.CommandText = cmdSql3.CommandText & " AND TBCAS_OFICINA='" & cboOficina.SelectedValue.Trim & "'"
            cmdSql3.CommandText = cmdSql3.CommandText & " GROUP BY I.APROB_FECHA_REPORTA, P.TBCAS_PUESTO, P.TBCAS_OFICINA"
            cmdSql3.ExecuteNonQuery()
            Response.Redirect("Cas_Rep_PorCargos.aspx")
        End If
    End Sub
    Protected Sub cboReportes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboReportes.SelectedIndexChanged
        If cboReportes.SelectedValue = "POR OFICINA" Then
            cboOficina.Enabled = True
            cboOficina.SelectedValue = "< Seleccionar >"
        ElseIf cboReportes.SelectedValue = "POR CARGOS" Then
            cboOficina.Enabled = True
            cboOficina.SelectedValue = "< Seleccionar >"
        End If
    End Sub
End Class
