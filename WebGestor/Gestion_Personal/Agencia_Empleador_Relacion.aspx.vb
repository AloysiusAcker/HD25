Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports OfficeOpenXml
Partial Class Agencia_Empleador_Relacion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Call LlenaComboItemBox("TBOPC499", ChkLisEstado)
            Call LlenaComboItem("TBOPC019", DdlSexo)
            Call LlenaComboItem("TBOPC020", DdlEstCivil)
            Call LlenaComboItem("TBOPC009", DdlEDocTipo)
            Call LlenaComboItem("TBOPC491", DdlETipo)
            Call LlenaComboItem("TBOPC499", DdlEEstado)
            Call LlenaComboItem("TBOPC490", DdlESeEntero)
            DdlSexo.SelectedValue = "< Seleccionar >"
            DdlEstCivil.SelectedValue = "< Seleccionar >"
            DdlEDpto.Items.Clear()
            DdlEProv.Items.Clear()
            DdlEDist.Items.Clear()
            DdlEDpto.Enabled = True
            DdlEProv.Items.Add("< Seleccionar >") : DdlEProv.SelectedValue = "< Seleccionar >"
            DdlEProv.Enabled = False
            DdlEDist.Items.Add("< Seleccionar >") : DdlEDist.SelectedValue = "< Seleccionar >"
            DdlEDist.Enabled = False
            Call LlenaComboItem("TBOPC002", DdlEDpto)
        End If
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
        Dim obj As New Cls_Inventario_Verificacion
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dt As New DataTable
        Dim objAg As New ClsAgencia
        dt = Nothing
        Dim psEstado As String = ""
        Dim psEstadoTodo As String = ""
        For Each item As ListItem In ChkLisEstado.Items
            If item.Selected Then
                If psEstado <> "" Then psEstado = psEstado & ", "
                psEstado = psEstado & item.Value
            End If
        Next
        If psEstado = "" Then
            psEstado = "1"
        End If
        Dim psSexo As String = ""
        If DdlSexo.SelectedValue <> "< Seleccionar >" Then
            psSexo = DdlSexo.SelectedValue
        End If
        Dim psEstcivil As String = ""
        If DdlEstCivil.SelectedValue <> "< Seleccionar >" Then
            psEstcivil = DdlEstCivil.SelectedValue
        End If
        Dim psFechaIni As String = "20240101"
        Dim psfechafin As String = "21001231"
        If TxtFecha.Text <> "" Then
            psFechaIni = Right(TxtFecha.Text, 4) & Mid(TxtFecha.Text, 4, 2) & Left(TxtFecha.Text, 2)
        End If
        If TxtFechaFin.Text <> "" Then
            psfechafin = Right(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 4, 2) & Left(TxtFechaFin.Text, 2)
        End If
        GvListaEmpleadores.DataSource = dt
        GvListaEmpleadores.DataBind()
        Try

            dt = objAg.Agencia_ListaEmpleadores(Session("Ruta_Emp"), psSexo, psFechaIni, psfechafin, psEstcivil, TxtApellido.Text, TxtNrodoc.Text, psEstado)
            GvListaEmpleadores.DataSource = dt
            GvListaEmpleadores.DataBind()

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
        Dim dt As New DataTable()
        Dim objAg As New ClsAgencia
        Dim psEstado As String = ""
        For Each item As ListItem In ChkLisEstado.Items
            If item.Selected Then
                If psEstado <> "" Then psEstado = psEstado & ", "
                psEstado = psEstado & item.Value
            End If
        Next
        Dim psSexo As String = ""
        If DdlSexo.SelectedValue <> "< Seleccionar >" Then
            psSexo = DdlSexo.SelectedValue
        End If
        Dim psEstcivil As String = ""
        If DdlEstCivil.SelectedValue <> "< Seleccionar >" Then
            psEstcivil = DdlEstCivil.SelectedValue
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
        dt = objAg.Agencia_ListaEmpleadores(Session("Ruta_Emp"), psSexo, "", "", psEstcivil, TxtApellido.Text, TxtNrodoc.Text, psEstado)
        ' Crear el archivo de Excel
        Using excelPackage As New ExcelPackage()
            ' Agregar hojas al archivo de Excel
            Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Empleadores")

            ' Llenar Hoja1 con los datos de dt1
            worksheet1.Cells("A1").LoadFromDataTable(dt, True)

            ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=ListaEmpleadores.xlsx")
            Response.BinaryWrite(excelPackage.GetAsByteArray())
            Response.End()
        End Using
    End Sub
    Private Sub Limpiar()
        TxtETelefono.Text = ""
        GvTelefonos.DataSource = Nothing
        GvTelefonos.DataBind()
        txtEDocNro.Value = ""
        TxtEApeMat.Value = ""
        TxtEApePat.Value = ""
        TxtEDireccion.Text = ""
        TxtEEmail.Value = ""
        TxtENombres.Value = ""
        TxtERazonSocial.Value = ""
        TxtERecomienda.Value = ""
        DdlEDocTipo.SelectedValue = "< Seleccionar >"
        DdlETipo.SelectedValue = "< Seleccionar >"
        DdlEEstado.SelectedValue = "< Seleccionar >"
        DdlESeEntero.SelectedValue = "< Seleccionar >"
        DdlEstCivil.SelectedValue = "< Seleccionar >"
        DdlSexo.SelectedValue = "< Seleccionar >"
        DdlEDpto.Items.Clear()
        DdlEProv.Items.Clear()
        DdlEDist.Items.Clear()
        DdlEDpto.Enabled = True
        DdlEProv.Items.Add("< Seleccionar >") : DdlEProv.SelectedValue = "< Seleccionar >"
        DdlEProv.Enabled = False
        DdlEDist.Items.Add("< Seleccionar >") : DdlEDist.SelectedValue = "< Seleccionar >"
        DdlEDist.Enabled = False
        Call LlenaComboItem("TBOPC002", DdlEDpto)
        DdlEEstado.SelectedValue = "1"
        TxtEFechaReg.Value = FormatoFecha(FechaActual)
    End Sub
    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Call Limpiar()
        Session("Nuevo") = "Si"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalEmpleador').modal('show');", True)
    End Sub

    Private Sub BtnECerrar_Click(sender As Object, e As EventArgs) Handles BtnECerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalEmpleador').modal('hide');", True)

    End Sub

    Private Sub GvListaEmpleadores_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaEmpleadores.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psEmpleador As String = ""
        Dim psCodEmpleador As Double = 0
        Dim dtDatos As New DataTable
        Dim objAg As New ClsAgencia
        dtDatos = Nothing
        GvRequerimiento.DataSource = dtDatos
        GvRequerimiento.DataBind()
        If e.CommandName = "Reque" Then
            psCodEmpleador = GvListaEmpleadores.Rows(Index).Cells(1).Text
            psEmpleador = GvListaEmpleadores.Rows(Index).Cells(4).Text
            LblTituloModal.Text = "Requerimiento del Empleador " & psEmpleador
            dtDatos = Nothing
            dtDatos = objAg.Agencia_ListaRequerimiento_xEmpleador(Session("Ruta_Emp"), psCodEmpleador)
            GvRequerimiento.DataSource = dtDatos
            GvRequerimiento.DataBind()
            If dtDatos.Rows.Count > 1 Then
                LblRegistroDetalle.Text = "Hay " & dtDatos.Rows.Count & " registros. "
            ElseIf dtDatos.Rows.Count = 0 Then
                LblRegistroDetalle.Text = "No hay registros. "
            ElseIf dtDatos.Rows.Count = 1 Then
                LblRegistroDetalle.Text = "Hay 1 registro. "
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
        End If
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub

    Private Sub DdlEDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlEDpto.SelectedIndexChanged
        DdlEProv.Items.Clear()
        DdlEDist.Items.Clear()
        DdlEProv.Enabled = False
        DdlEDist.Items.Add("< Seleccionar >") : DdlEDist.SelectedValue = "< Seleccionar >"
        DdlEDist.Enabled = False
        If DdlEDpto.SelectedIndex = -1 Or DdlEDpto.Items.Count = 0 Then Exit Sub
        If DdlEDpto.Items(DdlEDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlEProv, Left(DdlEDpto.SelectedValue, 2), "PR")
        If DdlEDpto.SelectedValue <> "< Seleccionar >" Then DdlEProv.Enabled = True
    End Sub

    Private Sub DdlEProv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlEProv.SelectedIndexChanged
        DdlEDist.Items.Clear()
        DdlEDist.Enabled = False
        DdlEDist.Items.Add("< Seleccionar >") : DdlEDist.SelectedValue = "< Seleccionar >"
        If DdlEProv.SelectedIndex = -1 Or DdlEProv.Items.Count = 0 Then Exit Sub
        If DdlEProv.Items(DdlEProv.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlEDist, Left(DdlEDpto.SelectedValue, 2) + Mid(DdlEProv.SelectedValue, 3, 2), "DS")
        DdlEDist.Items.Add("< Seleccionar >") : DdlEDist.SelectedValue = "< Seleccionar >"
        If DdlEProv.SelectedValue <> "< Seleccionar >" Then DdlEDist.Enabled = True
    End Sub

    Private Sub BtnEGuardar_Click(sender As Object, e As EventArgs) Handles BtnEGuardar.Click
        '
        Dim dt As New DataTable()
        Dim objAg As New ClsAgencia
        Dim psTelefonos As String = ""
        Dim psDocTipo As String = ""
        Dim psEstado As String = ""
        Dim psSeEntero As String = ""
        Dim psDpto As String = ""
        Dim psProv As String = ""
        Dim psDist As String = ""
        Dim psTipoDirec As String = ""
        Dim psFechaNac As String = ""
        Dim psSexo As String = ""
        Dim psEstCivil As String = ""
        If DdlEDocTipo.SelectedValue <> "< Seleccionar >" Then psDocTipo = DdlEDocTipo.SelectedValue
        If DdlEEstado.SelectedValue <> "< Seleccionar >" Then psEstado = DdlEEstado.SelectedValue
        If DdlESeEntero.SelectedValue <> "< Seleccionar >" Then psSeEntero = DdlESeEntero.SelectedValue
        If DdlEDpto.SelectedValue <> "< Seleccionar >" Then psDpto = DdlEDpto.SelectedValue
        If DdlEProv.SelectedValue <> "< Seleccionar >" Then psProv = DdlEProv.SelectedValue
        If DdlEDist.SelectedValue <> "< Seleccionar >" Then psDist = DdlEDist.SelectedValue
        If DdlETipo.SelectedValue <> "< Seleccionar >" Then psTipoDirec = DdlETipo.SelectedValue

        If GvTelefonos.Rows.Count > 0 Then
            For iRow = 0 To GvTelefonos.Rows.Count - 1
                If psTelefonos <> "" Then psTelefonos = psTelefonos & ","
                psTelefonos = psTelefonos & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTelefonos.Rows(iRow).Cells(1).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Next
        End If

        Dim psCodEmpleado As Double = 0

        Try
            If TxtEApePat.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el apellido paterno del empleador')", True)
            ElseIf TxtENombres.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el nombre del empleador')", True)
            Else
                If Session("Nuevo") = "Si" Then
                    dt = objAg.Agencia_Insert_Empleador(Session("Ruta_Emp"), TxtEApePat.Value, TxtEApeMat.Value, TxtENombres.Value, TxtERazonSocial.Value, Session("User"))
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            psCodEmpleado = dr(0)
                        Next
                    End If
                End If
                objAg.Agencia_Update_Empleador(Session("Ruta_Emp"), psCodEmpleado, TxtEApePat.Value, TxtEApeMat.Value, TxtENombres.Value, TxtERazonSocial.Value, Session("User"), psFechaNac, psEstCivil, psDpto, psProv, psDist, TxtEDireccion.Text, psTipoDirec, psSexo, "", "", "", "", "", psSeEntero, TxtERecomienda.Value, "", 0, "", 0, "", 0, "", "", "", "", "", "", "", 0, 0, 0, 0, 0, 0, "", 0, "", psTelefonos, psDocTipo, txtEDocNro.Value)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalEmpleador').modal('hide');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub BtnETelef_Click(sender As Object, e As EventArgs) Handles BtnETelef.Click
        Dim drT As DataRow
        Dim iRow As Double = 0
        Dim dt As New DataTable
        dt.Columns.Add("Telefono", GetType(String))
        If GvTelefonos.Rows.Count > 0 Then
            For iRow = 0 To GvTelefonos.Rows.Count - 1
                drT = dt.NewRow()
                drT("Telefono") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTelefonos.Rows(iRow).Cells(1).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                dt.Rows.Add(drT)
            Next
        End If
        drT = dt.NewRow()
        drT("Telefono") = TxtETelefono.Text
        dt.Rows.Add(drT)
        GvTelefonos.DataSource = dt
        GvTelefonos.DataBind()
        TxtETelefono.Text = ""
    End Sub

    Private Sub GvTelefonos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvTelefonos.RowCommand
        Dim drT As DataRow
        Dim iRow As Double = 0
        Dim dt As New DataTable
        dt.Columns.Add("Telefono", GetType(String))
        If GvTelefonos.Rows.Count > 0 Then
            For iRow = 0 To GvTelefonos.Rows.Count - 1
                drT = dt.NewRow()
                drT("Telefono") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTelefonos.Rows(iRow).Cells(1).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                dt.Rows.Add(drT)
            Next
        End If
        If e.CommandName = "Quitar" Then
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' Asegúrate de que rowIndex esté dentro del rango válido de filas.
            If rowIndex >= 0 AndAlso rowIndex < dt.Rows.Count Then
                dt.Rows.RemoveAt(rowIndex) ' Elimina la fila del DataTable.
                GvTelefonos.DataSource = dt ' Vuelve a vincular el GridView para reflejar el cambio.
                GvTelefonos.DataBind()
            End If
        End If
    End Sub
End Class
