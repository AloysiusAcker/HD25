Imports System.Data.SqlClient
Imports System.Data

Imports OfficeOpenXml
Imports WebGestor
Partial Class Ventas_Ventas_Registro_Oportunidades
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Call Carga_Vendedor(DdlVendedor)
            DdlVendedor.SelectedValue = "< Seleccionar >"
            Call Carga_Vendedor(DDlRVendedor)
            DDlRVendedor.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC429", DDlReque)
            DdlDpto.Items.Clear()
            DdlProv.Items.Clear()
            DdlDist.Items.Clear()
            DdlDpto.Enabled = True
            DdlProv.Items.Add("< Seleccionar >") : DdlProv.SelectedValue = "< Seleccionar >"
            DdlProv.Enabled = False
            DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
            DdlDist.Enabled = False
            Call LlenaComboItem("TBOPC006", DdlPais)
            Call LlenaComboItem("TBOPC002", DdlDpto)

            Call LlenaComboItem("TBOPC444", DdlSeguiTipo)
            Call LlenaComboItem("TBOPC445", DdlProxAcc)
        End If
    End Sub
    Private Sub Carga_Vendedor(ByVal Ddl As DropDownList)

        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String = ""
        Cn.Open()
        Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
        Sql = " SELECT PERSON_VENDEDOR,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES FROM BDGRUPOEMPRESAS.DBO.TBPERSONAL WHERE PERSON_VENDEDOR = PERSON_CODIGO AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND PERSON_SYS_EST='0' ) AS VENDEDOR " _
            & " FROM TBVENDEDOR WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
        cmdSql = New SqlClient.SqlCommand(Sql, Cn)
        Ddl.DataSource = cmdSql.ExecuteReader
        Ddl.DataTextField = "VENDEDOR"
        Ddl.DataValueField = "PERSON_VENDEDOR"
        Ddl.DataBind()
        Ddl.Items.Add("< Seleccionar >") : DdlVendedor.SelectedValue = "< Seleccionar >"

    End Sub
    Private Sub ChkFecha_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFecha.CheckedChanged
        If ChkFecha.Checked = True Then
            TxtFecha.Text = "" : TxtFecha.Enabled = True
            TxtFechaFin.Text = "" : TxtFechaFin.Enabled = True
        Else
            TxtFecha.Text = "" : TxtFecha.Enabled = False
            TxtFechaFin.Text = "" : TxtFechaFin.Enabled = False
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim dt As New DataTable
        Dim objAg As New ClsVentas_Listados
        dt = Nothing
        Dim psVendedor As String = ""
        If DdlVendedor.SelectedValue <> "< Seleccionar >" Then
            psVendedor = DdlVendedor.SelectedValue
        End If

        Dim psFechaIni As String = "20240101"
        Dim psfechafin As String = "21001231"
        If TxtFecha.Text <> "" Then
            psFechaIni = Right(TxtFecha.Text, 4) & Mid(TxtFecha.Text, 4, 2) & Left(TxtFecha.Text, 2)
        End If
        If TxtFechaFin.Text <> "" Then
            psfechafin = Right(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 4, 2) & Left(TxtFechaFin.Text, 2)
        End If
        GvListaOportunidades.DataSource = dt
        GvListaOportunidades.DataBind()
        Try
            dt = objAg.Ventas_ListaOportunidades(Session("Ruta_Emp"), Session("CodEmpresa"), psFechaIni, psfechafin, psVendedor)
            GvListaOportunidades.DataSource = dt
            GvListaOportunidades.DataBind()

            If dt.Rows.Count = 0 Then
                lblRegistro.Text = "No hay registros"
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro.Text = "Hay 1 registro"
            ElseIf dt.Rows.Count > 1 Then
                lblRegistro.Text = "Hay " & dt.Rows.Count & " registros"
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try

    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim dt As New DataTable
        Dim objAg As New ClsVentas_Listados
        dt = Nothing
        Dim psVendedor As String = ""
        If DdlVendedor.SelectedValue <> "< Seleccionar >" Then
            psVendedor = DdlVendedor.SelectedValue
        End If

        Dim psFechaIni As String = "20240101"
        Dim psfechafin As String = "21001231"
        If TxtFecha.Text <> "" Then
            psFechaIni = Right(TxtFecha.Text, 4) & Mid(TxtFecha.Text, 4, 2) & Left(TxtFecha.Text, 2)
        End If
        If TxtFechaFin.Text <> "" Then
            psfechafin = Right(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 4, 2) & Left(TxtFechaFin.Text, 2)
        End If
        ' Configurar los datos en dt1 y dt2...
        Try
            dt = objAg.Ventas_ListaOportunidades(Session("Ruta_Emp"), Session("CodEmpresa"), psFechaIni, psfechafin, psVendedor)
            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Oportunidades")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Oportunidades.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub
    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        TxtFechaReg.Text = FormatoFecha(FechaActual)
        Call Limpiar()
        Dim pdCodOportunidad As Integer = 1
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT isnull(MAX(OPP_CODIGO),0) FROM TBVENTAS_OPORTUNIDADES"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                pdCodOportunidad = 1 + Rs(0)
            End While
        End If
        Rs.Close()
        txtNroReg.Value = Llenar_Ceros(pdCodOportunidad, 4)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalOportunidad').modal('show');", True)
    End Sub
    Private Sub BtnECerrar_Click(sender As Object, e As EventArgs) Handles BtnECerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalOportunidad').modal('hide');", True)
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

    Private Sub BtnEGuardar_Command(sender As Object, e As CommandEventArgs) Handles BtnEGuardar.Command
        Dim dt As New DataTable()
        Dim objAg As New ClsVentas_Listados
        Dim psRequerimiento As String = ""
        If DDlReque.SelectedValue <> "< Seleccionar >" Then psRequerimiento = DDlReque.SelectedValue
        Dim psVendedor As String = ""
        If DDlRVendedor.SelectedValue <> "< Seleccionar >" Then psVendedor = DDlRVendedor.SelectedValue
        Dim psPais As String = ""
        Dim psDpto As String = ""
        Dim psProv As String = ""
        Dim psDist As String = ""
        If DdlPais.SelectedValue <> "< Seleccionar >" Then psPais = DdlPais.SelectedValue
        If DdlDpto.SelectedValue <> "< Seleccionar >" Then psDpto = Llenar_Ceros(DdlDpto.SelectedValue, 6)
        If DdlProv.SelectedValue <> "< Seleccionar >" Then psProv = Llenar_Ceros(DdlProv.SelectedValue, 6)
        If DdlDist.SelectedValue <> "< Seleccionar >" Then psDist = Llenar_Ceros(DdlDist.SelectedValue, 6)
        Dim psFechaReg As String = ""
        If TxtFechaReg.Text <> "" Then
            psFechaReg = Right(TxtFechaReg.Text, 4) & Mid(TxtFechaReg.Text, 4, 2) & Left(TxtFechaReg.Text, 2)
        End If
        Try
            If TxtRazonScial.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la Razón Social.')", True)
                'ElseIf TxtRUC.Value = "" Then
                '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar RUC.')", True)
            ElseIf TxtCApePat.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el apellido del contacto.')", True)
            ElseIf TxtCNombres.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la nombre del contacto.')", True)
            ElseIf DDlRVendedor.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('seleccionar vendedor.')", True)
            ElseIf DDlReque.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar requerimiento.')", True)
            ElseIf TxtRDetalle.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar detalle del requerimiento.')", True)
            Else
                objAg.Ventas_InsertarOportunidades(Session("Ruta_Emp"), Session("CodEmpresa"), txtNroReg.Value, psRequerimiento, psFechaReg, TxtRDetalle.Value, psVendedor, TxtRUC.Value, TxtRazonScial.Value, TxtDireccion.Value, psPais, psDpto, psProv, psDist, TxtCApePat.Value, TxtCApeMat.Value, TxtCNombres.Value, TxtCEmail.Text, TxtCTelef.Text, TxtCTelef2.Text, Session("User"))
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalOportunidad').modal('hide');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub
    Private Sub GvListaOportunidades_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaOportunidades.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psEmpleador As String = ""
        Dim psCodOportunidad As Double = 0
        Dim dtDatos As New DataTable
        Dim obj As New ClsVentas_Listados
        dtDatos = Nothing
        GvSeguimiento.DataSource = dtDatos
        GvSeguimiento.DataBind()
        If e.CommandName = "Detalle" Then
            psCodOportunidad = Llenar_Ceros(GvListaOportunidades.Rows(Index).Cells(2).Text, 4)
            psEmpleador = GvListaOportunidades.Rows(Index).Cells(5).Text
            LblTituloModal.Text = "Seguimiento de la Oportunidad Nro. " & psCodOportunidad
            dtDatos = Nothing
            dtDatos = obj.Ventas_ListaSeguimiento_xOportunidad(Session("Ruta_Emp"), Session("CodEmpresa"), psCodOportunidad)
            GvSeguimiento.DataSource = dtDatos
            GvSeguimiento.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
        End If
        If e.CommandName = "Ingresar" Then
            psCodOportunidad = Llenar_Ceros(GvListaOportunidades.Rows(Index).Cells(2).Text, 4)
            dtDatos = obj.Ventas_Lista_xOportunidad(Session("Ruta_Emp"), Session("CodEmpresa"), psCodOportunidad)
            If dtDatos.Rows.Count > 0 Then
                For Each dr As DataRow In dtDatos.Rows
                    TxtOpoNro.Value = Llenar_Ceros(Nu(dr("OPP_CODIGO")), 4)
                    TxtOpoFecha.Value = FormatoFecha(Nu(dr("OPP_FECHA")))
                    TxtOpoRuc.Value = Nu(dr("OPP_CLIENTE_RUC"))
                    TxtOpoRazonSocial.Value = Nu(dr("OPP_CLIENTE_RAZON"))
                    TxtOpoDireccion.Value = Nu(dr("OPP_CLIENTE_DIRECCION"))
                    TxtOpoRequerimiento.Value = Nu(dr("REQUERIMIENTO"))
                    TxtOpoDetalle.Value = Nu(dr("OPP_COMENTARIO"))
                    TxtOpoContacto.Value = Nu(dr("OPP_CLIENTE_CONTACTO_NOMBRES")) & " " & Nu(dr("OPP_CLIENTE_CONTACTO_APELLIDOS")) & " " & Nu(dr("OPP_CLIENTE_CONTACTO_APEMAT"))
                    TxtOpoEmail.Value = Nu(dr("OPP_CLIENTE_CONTACTO_EMAIL"))
                    TxtOpoTelef.Value = Nu(dr("OPP_CLIENTE_CONTACTO_TELEFONO_1"))
                    TxtOpoTelef2.Value = Nu(dr("OPP_CLIENTE_CONTACTO_TELEFONO_2"))
                Next
            End If
            TxtSeguiFecha.Text = FormatoFecha(FechaActual)
            TxtFechaAcc.Text = FormatoFecha(FechaActual)
            TxtHoraAcc.Text = FormatoHora(HoraActual)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalSeguimiento').modal('show');", True)
        End If
    End Sub

    Private Sub BtnCerrarSeg_Click(sender As Object, e As EventArgs) Handles BtnCerrarSeg.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub
    Private Sub Limpiar()
        TxtRUC.Value = ""
        TxtRazonScial.Value = ""
        TxtCApeMat.Value = ""
        TxtCApePat.Value = ""
        TxtCEmail.Text = ""
        TxtCNombres.Value = ""
        TxtCTelef.Text = ""
        TxtCTelef2.Text = ""
        TxtDireccion.Value = ""
        TxtRDetalle.Value = ""
        DdlDist.SelectedValue = "< Seleccionar >"
        DdlPais.SelectedValue = "< Seleccionar >"
        DdlDpto.SelectedValue = "< Seleccionar >"
        DdlProv.SelectedValue = "< Seleccionar >"
        DDlReque.SelectedValue = "< Seleccionar >"
        DDlRVendedor.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub BtnBuscaRuc_Click(sender As Object, e As EventArgs) Handles BtnBuscaRuc.Click
        Dim psCodCliente As Double = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        If TxtRUC.Value <> "" Then
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT PERSONA_RUC, PERSONA_RAZON_SOCIAL,PERSONA_CODIGO, PERSONA_DIRECCION, " _
                                  & "PERSONA_PAIS,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC006' AND ELEMEN_CODIGO = PERSONA_PAIS) AS PPAIS," _
                                  & "PERSONA_DPTO,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC002' AND ELEMEN_CODIGO = PERSONA_DPTO) AS PDPTO," _
                                  & "PERSONA_PROV,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC003' AND ELEMEN_CODIGO = PERSONA_PROV) AS PPROV," _
                                  & "PERSONA_DIST,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) AS PDIST " _
                                  & " From TBDATA_PERSONAS " _
                                  & " WHERE (PERSONA_SYS_EST = '0') AND (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PERSONA_TIPO = '1') "
            If TxtRUC.Value <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND (PERSONA_RUC = '" & TxtRUC.Value & "')"
            If TxtRazonScial.Value <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND (PERSONA_RAZON_SOCIAL = '" & TxtRazonScial.Value & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    TxtRUC.Value = Nu(Rs!PERSONA_RUC)
                    TxtRazonScial.Value = Nu(Rs!PERSONA_RAZON_SOCIAL)
                    TxtDireccion.Value = Nu(Rs!PERSONA_DIRECCION)
                    psCodCliente = Nu(Rs!PERSONA_CODIGO)
                    If Nu(Rs!PERSONA_PAIS) <> "" Then
                        DdlPais.SelectedValue = Nu(Rs!PERSONA_PAIS)
                    End If
                    If Nu(Rs!PERSONA_DPTO) <> "" Then
                        DdlDpto.SelectedValue = Nu(Rs!PERSONA_DPTO)
                        DdlDpto_SelectedIndexChanged(sender, e)
                    End If
                    If Nu(Rs!PERSONA_PROV) <> "" Then
                        DdlProv.SelectedValue = Nu(Rs!PERSONA_PROV)
                        DdlProv_SelectedIndexChanged(sender, e)
                    End If
                    If Nu(Rs!PERSONA_DIST) <> "" Then
                        DdlDist.SelectedValue = Nu(Rs!PERSONA_DIST)
                    End If
                End While
            End If
            Rs.Close()
        End If
    End Sub

    Private Sub BtnNuevo_Command(sender As Object, e As CommandEventArgs) Handles BtnNuevo.Command

    End Sub

    Private Sub BtnSeguiCerrar_Click(sender As Object, e As EventArgs) Handles BtnSeguiCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalSeguimiento').modal('hide');", True)
    End Sub

    Private Sub ChkProxAccion_CheckedChanged(sender As Object, e As EventArgs) Handles ChkProxAccion.CheckedChanged
        If ChkProxAccion.Checked = True Then
            DivAccion.Visible = True
            DdlProxAcc.SelectedValue = "< Seleccionar >"
            TxtFechaAcc.Text = FormatoFecha(FechaActual)
            TxtHoraAcc.Text = FormatoHora(HoraActual)
        Else
            DivAccion.Visible = False
            DdlProxAcc.SelectedValue = "< Seleccionar >"
            TxtFechaAcc.Text = FormatoFecha(FechaActual)
            TxtHoraAcc.Text = FormatoHora(HoraActual)
        End If
    End Sub

    Private Sub Limpiar_seguimeinto()
        DdlSeguiTipo.SelectedValue = "< Seleccionar >"
        TxtSeguiDescripcion.InnerText = ""
        DdlProxAcc.SelectedValue = "< Seleccionar >"
        DivAccion.Visible = False
        ChkProxAccion.Checked = False
    End Sub

    Private Sub BtnSeguiGuardar_Click(sender As Object, e As EventArgs) Handles BtnSeguiGuardar.Click
        '

        Dim dt As New DataTable()
        Dim objAg As New ClsVentas_Listados
        Dim psRequerimiento As String = ""
        Dim psTipoSegui As String = ""
        Dim psFechaSegui As String = ""
        If DdlSeguiTipo.SelectedValue <> "< Seleccionar >" Then psTipoSegui = DdlSeguiTipo.SelectedValue
        If TxtSeguiFecha.Text <> "" Then
            psFechaSegui = Right(TxtSeguiFecha.Text, 4) & Mid(TxtSeguiFecha.Text, 4, 2) & Left(TxtSeguiFecha.Text, 2)
        End If
        Dim psProxAcc As String = ""
        Dim psFechaAcc As String = ""
        Dim psHoraAcc As String = ""
        If ChkProxAccion.Checked = True Then
            If DdlProxAcc.SelectedValue <> "< Seleccionar >" Then psProxAcc = DdlProxAcc.SelectedValue
            If TxtFechaAcc.Text <> "" Then
                psFechaAcc = Right(TxtFechaAcc.Text, 4) & Mid(TxtFechaAcc.Text, 4, 2) & Left(TxtFechaAcc.Text, 2)
            End If
            If TxtHoraAcc.Text <> "" Then
                psHoraAcc = Left(TxtHoraAcc.Text, 2) & Mid(TxtHoraAcc.Text, 4, 2)
            End If
        End If
            Try
            If TxtSeguiDescripcion.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Detalle del seguimiento.')", True)
            ElseIf DdlProxAcc.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('seleccionar Tipo de Seguimiento.')", True)
            ElseIf ChkProxAccion.Checked = True And DdlProxAcc.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('seleccionar proxima acción.')", True)
            Else
                objAg.Ventas_Insert_OportunidadSeguimiento(Session("Ruta_Emp"), Session("CodEmpresa"), Nz(TxtOpoNro.Value), psFechaSegui, "", psTipoSegui, TxtSeguiDescripcion.InnerText, Session("User"), psFechaAcc, psHoraAcc, psProxAcc)
            End If
            Call Limpiar_seguimeinto()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalSeguimiento').modal('hide');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub
End Class
