Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Cas_Relacion_Reportes
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                cboEstado.Items.Clear()
                cboImportancia.Items.Clear()
                Call LlenaComboItem("TBOPC323", cboEstado)
                Call Tipos_Criterio("2", cboImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
                txtFechaIni.Text = FormatoFecha(FechaActual)
                txtFechaFin.Text = txtFechaIni.Text.Trim
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub chkEstado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        If chkEstado.Checked = True Then
            cboEstado.Enabled = True : cboEstado.SelectedValue = "< Seleccionar >"
        Else
            cboEstado.Enabled = False : cboEstado.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub chkImport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkImport.CheckedChanged
        If chkImport.Checked = True Then
            cboImportancia.Enabled = True : cboImportancia.SelectedValue = "< Seleccionar >"
        Else
            cboImportancia.Enabled = False : cboImportancia.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnBListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBListar.Click
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim dRow As Data.DataRow
        Dim obj As New ModuloCas
        Dim pCodComponente As Double : pCodComponente = 0
        Dim CantInc As Double = 0
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim i As Integer = 0
        Dim Sql As String : Sql = ""
        Dim pFecIni As String, pFecFin As String : pFecFin = "" : pFecIni = ""
        Sql = ""
        pFecIni = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
        pFecFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        If txtFechaIni.Text = "" And txtFechaFin.Text = "" Then
        Else
            If txtFechaFin.Text = "" And txtFechaIni.Text <> "" Then
                Sql = Sql & " AND (APROB_FECHA_REPORTA='" & pFecIni & "')"
            ElseIf txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then
                Sql = Sql & " AND (APROB_FECHA_REPORTA='" & pFecFin & "')"
            Else
                Sql = Sql & " AND (APROB_FECHA_REPORTA BETWEEN '" & pFecIni & "' AND '" & pFecFin & "')"
            End If
        End If
        If chkImport.Checked = True And cboImportancia.SelectedValue <> "< Seleccionar >" Then Sql = Sql & " AND I.APROB_PRIORIDAD = '" & cboImportancia.SelectedValue.Trim & "'"
        dt.Columns.Add("C1")
        dt.Columns.Add("C2")
        dt.Columns.Add("C3")
        dt.Columns.Add("C4")
        dt.Columns.Add("C5")
        Try
            If cboReporte.SelectedValue = "0" Then
                Flex.Columns(0).HeaderText = "Componente"
                Flex.Columns(1).HeaderText = "Total Inc."
                Flex.Columns(2).HeaderText = ""
                Flex.Columns(3).HeaderText = "%"
                Flex.Columns(4).HeaderText = ""
                Flex.Columns(0).ItemStyle.Width = 445
                Flex.Columns(1).ItemStyle.Width = 50
                Flex.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                Flex.Columns(2).ItemStyle.Width = 0
                Flex.Columns(2).ItemStyle.ForeColor = Drawing.Color.White
                Flex.Columns(3).ItemStyle.Width = 50
                Flex.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Right
                Flex.Columns(3).ItemStyle.ForeColor = Drawing.Color.Black
                Flex.Columns(4).ItemStyle.Width = 0
                Flex.Columns(4).ItemStyle.ForeColor = Drawing.Color.White
                dtListado = obj.CasConsulta_CantInc(Session("CodEmpresa"), pFecIni, pFecFin, "%", "%",Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dtListado.Rows
                        CantInc = CDbl(Nz(dr("Cant")))
                    Next
                End If
                dtListado = Nothing
                dtListado = obj.CasLista_IncxComponente(Session("CodEmpresa"), pFecIni, pFecFin, "%", "%",Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dtListado.Rows
                        dRow = dt.NewRow
                        dRow("C1") = Nu(dr("NIVEL1_DESCRIP"))
                        dRow("C2") = Nu(dr("Cant"))
                        dRow("C3") = Nu(dr("APROB_TIPO"))
                        dRow("C4") = Format((Nz(dr("Cant")) * 100) / CantInc, "#0.00")
                        dRow("C5") = ""
                        dt.Rows.Add(dRow)
                    Next
                End If
            ElseIf cboReporte.SelectedValue = "1" Then
                Flex.Columns(0).HeaderText = "Componentes"
                Flex.Columns(1).HeaderText = "Elementos"
                Flex.Columns(2).HeaderText = ""
                Flex.Columns(3).HeaderText = ""
                Flex.Columns(4).HeaderText = "Total Inc."
                Flex.Columns(0).ItemStyle.Width = 245
                Flex.Columns(1).ItemStyle.Width = 200
                Flex.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                Flex.Columns(2).ItemStyle.Width = 0
                Flex.Columns(2).ItemStyle.ForeColor = Drawing.Color.White
                Flex.Columns(3).ItemStyle.Width = 0
                Flex.Columns(3).ItemStyle.ForeColor = Drawing.Color.White
                Flex.Columns(4).ItemStyle.Width = 50
                Flex.Columns(4).ItemStyle.ForeColor = Drawing.Color.Black
                Flex.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right

                Cn.Open()
                cmdSql.Connection = Cn
                Dim lblNota As String = "", lblNombreEscala As String = ""
                Dim VerPuntTotal As String = "", VerPuntTotal_TipoConver As String = ""
                Dim VerPuntSGrupo As String = "", VerPuntSGrupo_TipoConver As String = ""
                cmdSql.CommandText = " SELECT DISTINCT I.APROB_TIPO, I.APROB_PROBLEMA1, P1.NIVEL1_DESCRIP, " _
                                   & " (SELECT NIVEL2_DESCRIP From dbo.TBESP_CAS2 WHERE (NIVEL2_CODIGO = I.APROB_PROBLEMA1)) AS NOM_PROB1, " _
                                   & " (SELECT DISTINCT COUNT(APROB_TIPO) FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (I.APROB_TIPO = APROB_TIPO) AND (NOT (APROB_USUARIO_REPORTA = '')) AND " _
                                   & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & ") AS CANT,"
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_TIPO) FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (I.APROB_TIPO = APROB_TIPO) AND (APROB_PROBLEMA1 IS NULL) AND (NOT (APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & ") AS CANT2, "
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_PROBLEMA1) FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (I.APROB_TIPO = APROB_TIPO) AND (I.APROB_PROBLEMA1 = APROB_PROBLEMA1) AND (NOT (APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & ") AS CANT3 "
                cmdSql.CommandText = cmdSql.CommandText & " FROM dbo.TBCAS_INCIDENTES AS I INNER JOIN dbo.TBESP_CAS1 AS P1 " _
                    & " ON I.APROB_TIPO = P1.NIVEL1_CODIGO AND I.EMPRESA_CODIGO = P1.EMPRESA_CODIGO " _
                    & " WHERE (I.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (I.APROB_SYS_EST = '0') " _
                    & " AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT P.TBCAS_PERSONA_USUARIO From TBCAS_PERSONA P WHERE (I.APROB_USUARIO_REPORTA = P.TBCAS_PERSONA_USUARIO)) IS NOT NULL)"
                cmdSql.CommandText = cmdSql.CommandText & Sql
                cmdSql.CommandText = cmdSql.CommandText & " ORDER BY P1.NIVEL1_DESCRIP,CANT"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        i = i + 1
                        dRow = dt.NewRow()
                        dRow("C1") = Nu(Rs!NIVEL1_DESCRIP)
                        dRow("C2") = IIf(Nu(Rs!NOM_PROB1) = "", "Sin Elementos", Nu(Rs!NOM_PROB1))
                        dRow("C3") = Nu(Rs!APROB_TIPO)
                        dRow("C4") = Nz(Rs!APROB_PROBLEMA1)
                        dRow("C5") = IIf(Nz(Rs!Cant3) = 0, Nz(Rs!Cant2), Nz(Rs!Cant3))
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
            End If
            dtListado = Nothing
            Flex.DataSource = dt
            Flex.DataBind()
            dt = Nothing
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboReporte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboReporte.SelectedIndexChanged
        If cboReporte.SelectedValue <> "< Seleccionar >" Then
            btnBListar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql2 As New SqlCommand
        Dim Campo As String : Campo = ""
        Dim Sql As String : Sql = ""
        Dim pFecIni As String, pFecFin As String : pFecFin = "" : pFecIni = ""
        Sql = ""
        pFecIni = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
        pFecFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        If txtFechaIni.Text = "" And txtFechaFin.Text = "" Then
        Else
            If txtFechaFin.Text = "" And txtFechaIni.Text <> "" Then
                Campo = " AND (I.APROB_FECHA_REPORTA=I2.APROB_FECHA_REPORTA)"
            ElseIf txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then
                Campo = " AND (I.APROB_FECHA_REPORTA=I2.APROB_FECHA_REPORTA)"
            Else
                Campo = " AND (I.APROB_FECHA_REPORTA=I2.APROB_FECHA_REPORTA)"
            End If
        End If
        If txtFechaIni.Text = "" And txtFechaFin.Text = "" Then
        Else
            If txtFechaFin.Text = "" And txtFechaIni.Text <> "" Then
                Sql = Sql & " AND (APROB_FECHA_REPORTA='" & pFecIni & "')"
            ElseIf txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then
                Sql = Sql & " AND (APROB_FECHA_REPORTA='" & pFecFin & "')"
            Else
                Sql = Sql & " AND (APROB_FECHA_REPORTA BETWEEN '" & pFecIni & "' AND '" & pFecFin & "')"
            End If
        End If
        If chkImport.Checked = True And cboImportancia.SelectedValue <> "< Seleccionar >" Then Sql = Sql & " AND I.APROB_PRIORIDAD = '" & cboImportancia.SelectedValue.Trim & "'"
        Try
            If cboReporte.SelectedValue = "0" Then
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_IncidentesxComponentes]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_IncidentesxComponentes]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = " CREATE VIEW V_IncidentesxComponentes AS SELECT I.APROB_TIPO, P1.NIVEL1_DESCRIP, COUNT(I.APROB_TIPO) AS CANT " _
                                   & " FROM dbo.TBCAS_INCIDENTES AS I INNER JOIN dbo.TBESP_CAS1 AS P1 ON I.APROB_TIPO = P1.NIVEL1_CODIGO AND I.EMPRESA_CODIGO = P1.EMPRESA_CODIGO " _
                                   & " WHERE (I.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (I.APROB_SYS_EST = '0') AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND " _
                                   & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL)"
                If chkEstado.Checked = True And cboEstado.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND I.APROB_ESTADO='" & cboEstado.SelectedValue.Trim & "'"
                If Sql <> "" Then cmdSql.CommandText = cmdSql.CommandText & Sql
                cmdSql.CommandText = cmdSql.CommandText & " GROUP BY I.APROB_TIPO, P1.NIVEL1_DESCRIP"
                cmdSql.ExecuteNonQuery()
                Response.Redirect("CasReporte_IncidentexComponente.aspx")
            End If
            If cboReporte.SelectedValue = "1" Then
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_IncidentesxElementos]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_IncidentesxElementos]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = " CREATE VIEW V_IncidentesxElementos AS SELECT DISTINCT I.APROB_TIPO, I.APROB_PROBLEMA1, P1.NIVEL1_DESCRIP, " _
                                   & " (SELECT NIVEL2_DESCRIP From dbo.TBESP_CAS2 WHERE (NIVEL2_CODIGO = I.APROB_PROBLEMA1)) AS NOM_PROB1, " _
                                   & " (SELECT COUNT(APROB_TIPO) FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (I.APROB_TIPO = APROB_TIPO) AND (NOT (I.APROB_USUARIO_REPORTA = '')) and " _
                                   & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS CANT,"
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_TIPO) FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (I.APROB_TIPO = APROB_TIPO) AND (APROB_PROBLEMA1 IS NULL) AND (NOT (I.APROB_USUARIO_REPORTA = '')) and " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS CANT2, "
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_PROBLEMA1) FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (I.APROB_TIPO = APROB_TIPO) AND (I.APROB_PROBLEMA1 = APROB_PROBLEMA1) AND (NOT (I.APROB_USUARIO_REPORTA = '')) and " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS CANT3 "
                cmdSql.CommandText = cmdSql.CommandText & " FROM dbo.TBCAS_INCIDENTES AS I INNER JOIN dbo.TBESP_CAS1 AS P1 " _
                    & " ON I.APROB_TIPO = P1.NIVEL1_CODIGO AND I.EMPRESA_CODIGO = P1.EMPRESA_CODIGO " _
                    & " WHERE (I.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (I.APROB_SYS_EST = '0') " _
                    & " AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT P.TBCAS_PERSONA_USUARIO From TBCAS_PERSONA P WHERE (I.APROB_USUARIO_REPORTA = P.TBCAS_PERSONA_USUARIO)) IS NOT NULL)"
                If chkEstado.Checked = True And cboEstado.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND I.APROB_ESTADO='" & cboEstado.SelectedValue.Trim & "'"
                If Sql <> "" Then cmdSql.CommandText = cmdSql.CommandText & Sql
                cmdSql.ExecuteNonQuery()
                Cn2.Open()
                cmdSql2.Connection = Cn2
                cmdSql2.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_IncidentesxElementos2]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_IncidentesxElementos2]"
                cmdSql2.ExecuteNonQuery()
                cmdSql2.CommandText = " CREATE VIEW V_IncidentesxElementos2 AS SELECT DISTINCT NIVEL1_DESCRIP, NOM_PROB1, SUM(CANT) AS TC, SUM(CANT2) AS TSE, SUM(CANT3) AS TE" _
                                      & " From dbo.V_IncidentesxElementos GROUP BY NIVEL1_DESCRIP, NOM_PROB1"
                cmdSql2.ExecuteNonQuery()
                Response.Redirect("CasReporte_IncidentexElemento.aspx")
            End If
            If cboReporte.SelectedValue = "2" Then
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_IncidentesTotales]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_IncidentesTotales]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = " CREATE VIEW V_IncidentesTotales AS SELECT DISTINCT "
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_CODIGO) AS cant FROM dbo.TBCAS_INCIDENTES AS I2 WHERE I.APROB_SYS_EST='0'  AND (NOT (I.APROB_USUARIO_REPORTA = ''))  AND " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS cant_incidente,"
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_CODIGO) AS cant FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (APROB_ASIGNADO_TIPO IS NULL) AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS n1, "
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_CODIGO) AS cant FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (NOT (APROB_ASIGNADO_TIPO IS NULL)) AND (NOT (APROB_ESTADO IN ('3', '4', '7', '8', '9'))) AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS n2, "
                cmdSql.CommandText = cmdSql.CommandText & " (SELECT COUNT(APROB_CODIGO) AS cant FROM dbo.TBCAS_INCIDENTES AS I2 WHERE (NOT (APROB_ASIGNADO_TIPO IS NULL)) AND (APROB_ESTADO IN ('3', '4', '7', '8', '9')) AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND " _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I2.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL) " & Sql & " ) AS por_resolver "
                cmdSql.CommandText = cmdSql.CommandText & " FROM dbo.TBCAS_INCIDENTES AS I WHERE (APROB_SYS_EST = '0')  AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND" _
                    & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL)"
                If Sql <> "" Then cmdSql.CommandText = cmdSql.CommandText & Sql
                cmdSql.ExecuteNonQuery()
                Response.Redirect("CasReporte_IncidenteTotales.aspx")
            End If
            If cboReporte.SelectedValue = "3" Then
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_Incidentes_OficinaComponetes]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_Incidentes_OficinaComponetes]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = " CREATE VIEW V_Incidentes_OficinaComponetes AS SELECT I.APROB_CODIGO, I.APROB_FECHA_REPORTA, O.TBCAS_OFICINA_NOMBRE, PRO.NIVEL1_DESCRIP " _
                          & " FROM dbo.TBCAS_INCIDENTES AS I INNER JOIN dbo.TBCAS_PERSONA AS P ON I.APROB_USUARIO_REPORTA = P.TBCAS_PERSONA_USUARIO INNER JOIN " _
                          & " dbo.TBCAS_OFICINAS AS O ON P.TBCAS_OFICINA = O.TBCAS_OFICINA_CODIGO INNER JOIN dbo.TBESP_CAS1 AS PRO ON I.APROB_TIPO = PRO.NIVEL1_CODIGO AND I.EMPRESA_CODIGO = PRO.EMPRESA_CODIGO " _
                          & " WHERE (I.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PRO.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (NOT (I.APROB_USUARIO_REPORTA = '')) AND" _
                          & " ((SELECT TBCAS_PERSONA_USUARIO FROM dbo.TBCAS_PERSONA AS P WHERE (I.APROB_USUARIO_REPORTA = TBCAS_PERSONA_USUARIO)) IS NOT NULL)"
                If Sql <> "" Then cmdSql.CommandText = cmdSql.CommandText & Sql
                cmdSql.ExecuteNonQuery()
                Response.Redirect("CasReporte_IncOficinaxComponente.aspx")
            End If
            If cboReporte.SelectedValue = "4" Then
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_Consulta_Solucion]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_Consulta_Solucion]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = " CREATE VIEW V_Consulta_Solucion AS SELECT TOP 10 '1' AS TIPO," & "'" & Nombre_Mes(Mid(FechaActual, 5, 2), True) & "'" & " AS MES,CARCON_CONSULTA, CARCON_SOLUCION, CONTADOR_WEB_XMES, CONTADOR_XMES," _
                          & " (SELECT NIVEL1_DESCRIP From TBESP_CAS1 WHERE NIVEL1_CODIGO = CARCON_APLICATIVO AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND NIVEL1_SYS_EST = '0') AS APLICATIVO_DESCRIPCION," _
                          & " (SELECT NIVEL2_DESCRIP From TBESP_CAS2 WHERE NIVEL2_CODIGO = CARCON_PRODUCTO AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND NIVEL2_SYS_EST = '0') AS PRODUCTO_DESCRIPCION," _
                          & " (SELECT NIVEL3_DESCRIP From TBESP_CAS3 WHERE NIVEL3_CODIGO = CARCON_SUBPRODUCTO AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND NIVEL3_SYS_EST = '0')" _
                          & " AS SUB_PRODUCTO_DESCRIPCION,(SELECT SUM(CONTADOR_XMES) AS Total_xMes From dbo.TBCAS_CARTERA_CONSULTA" _
                          & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CARCON_SYS_EST = '0')) AS Total," _
                          & " (SELECT SUM(CONTADOR_WEB_XMES) AS Total_Web_xMes From dbo.TBCAS_CARTERA_CONSULTA" _
                          & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CARCON_SYS_EST = '0')) AS Total_Web, CARCON_APLICATIVO, CARCON_PRODUCTO," _
                          & " CARCON_SUBPRODUCTO From dbo.TBCAS_CARTERA_CONSULTA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CARCON_SYS_EST = '0') ORDER BY CONTADOR_XMES DESC"
                cmdSql.ExecuteNonQuery()
                Response.Redirect("CasReporte_ResumenBD.aspx")
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
End Class
