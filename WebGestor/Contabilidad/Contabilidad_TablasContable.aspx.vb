Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient  
Partial Class Contabilidad_TablasContable
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Private Sub LlenaAsiento()
        Dim obj As New clsCont_Listados
        Try
            FlexAsiento.DataSource = obj.Cont_ListaAsientos(Session("CodEmpresa"), cboAño.SelectedValue.Trim, "No", "0", Session("Ruta_Emp"))
            FlexAsiento.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaDocumento()
        Dim obj As New clsCont_Listados
        Try
            FlexDoc.DataSource = obj.Cont_ListaDocumentos(Session("CodEmpresa"), cboAñoDoc.SelectedValue.Trim, Session("Ruta_Emp"))
            FlexDoc.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaCCosto()
        Dim obj As New clsCont_Listados
        Try
            FlexCC.DataSource = obj.Cont_ListaCentroCostos(Session("CodEmpresa"), cboAñoCC.SelectedValue.Trim, "", Session("Ruta_Emp"))
            FlexCC.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaFlujoCaja()
        Dim obj As New clsCont_Listados
        Try
            FlexFC.DataSource = obj.Cont_ListaFlujoCaja(Session("CodEmpresa"), cboAñoFC.SelectedValue.Trim, Session("Ruta_Emp"))
            FlexFC.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            cboAño.Items.Clear()
            Call LlenaAno(cboAño)
            cboAño.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboAño.SelectedValue = CInt(Left(FechaActual, 4))
            cboAño.Focus()
            Call LlenaAsiento()
        End If
        If Ficha.ActiveTabIndex = 1 Then
            cboAñoDoc.Items.Clear()
            Call LlenaAno(cboAñoDoc)
            cboAñoDoc.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboAñoDoc.SelectedValue = CInt(Left(FechaActual, 4))
            cboAñoDoc.Focus()
            Call LlenaDocumento()
        End If
        If Ficha.ActiveTabIndex = 2 Then
            cboAñoCC.Items.Clear()
            Call LlenaAno(cboAñoCC)
            cboAñoCC.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboAñoCC.SelectedValue = CInt(Left(FechaActual, 4))
            cboAñoCC.Focus()
            Call LlenaCCosto()
        End If
        If Ficha.ActiveTabIndex = 3 Then
            cboAñoFC.Items.Clear()
            Call LlenaAno(cboAñoFC)
            cboAñoFC.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboAñoFC.SelectedValue = CInt(Left(FechaActual, 4))
            cboAñoFC.Focus()
            Call LlenaFlujoCaja()
        End If
        If Ficha.ActiveTabIndex = 4 Then
            cboAñoTC.Items.Clear()
            Call LlenaAno(cboAñoTC)
            cboAñoTC.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Lista_TC()
            Call TiempoPerson(True)
        End If
        If Ficha.ActiveTabIndex = 5 Then
            cboAñoPP.Items.Clear()
            Call LlenaAno(cboAñoPP)
            cboAñoPP.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Call LlenaComboItem("TBOPC014", cboTipoPer)
            Call FechaInicio()
            Call NroPeriodos()
            Call Lista_Periodo()
        End If
    End Sub
    Private Sub FechaInicio()
        Dim obj As New clsCont_Listados
        Dim dt As DataTable
        Try
            dt = obj.Cont_FechaInicio(Session("CodEmpresa"), Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    txtFechaInicio.Text = FormatoFecha(Nu(drMenuItem("EMP_PER_INICIO")))
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub NroPeriodos()
        Dim obj As New clsCont_Listados
        Dim dt As DataTable
        Try
            dt = obj.Cont_ListaPeriodos(Session("CodEmpresa"), cboAñoPP.Text, "NO", "", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    txtNPeriodo.Text = Nu(drMenuItem("PER_NRO_PERIODOS"))
                    cboTipoPer.SelectedValue = Nu(drMenuItem("PER_TIPO_PERIODO"))
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAño.SelectedIndexChanged
        Call LlenaAsiento()
    End Sub
    Protected Sub cboAñoDoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAñoDoc.SelectedIndexChanged
        Call LlenaDocumento()
    End Sub
    Protected Sub FlexAsiento_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexAsiento.PageIndexChanging
        lblError.Text = ""
        FlexAsiento.PageIndex = e.NewPageIndex
        Call LlenaAsiento()
    End Sub
    Protected Sub FlexDoc_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexDoc.PageIndexChanging
        lblError.Text = ""
        FlexDoc.PageIndex = e.NewPageIndex
        Call LlenaDocumento()
    End Sub
    Protected Sub cboAñoCC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAñoCC.SelectedIndexChanged
        Call LlenaCCosto()
    End Sub
    Protected Sub FlexCC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexCC.PageIndexChanging
        lblError.Text = ""
        FlexCC.PageIndex = e.NewPageIndex
        Call LlenaCCosto()
    End Sub
    Protected Sub FlexFC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexFC.PageIndexChanging
        lblError.Text = ""
        FlexFC.PageIndex = e.NewPageIndex
        Call LlenaFlujoCaja()
    End Sub
    Protected Sub cboAñoFC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAñoFC.SelectedIndexChanged
        Call LlenaFlujoCaja()
    End Sub
    Protected Sub btnANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraIngreso.Visible = True
        cboAño.Enabled = False
        lblAEtiqueta.Text = "Nuevo Tipo de Asiento"
        btnANuevo.Enabled = False
    End Sub
    Protected Sub btnAGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim obj As New clsCont_Listados
        lblAError.Text = ""
        If Len(Trim(txtACodigo.Text)) = 0 Then lblAError.Text = "Falta el ingreso Código del Tipo de Asiento." : Exit Sub
        If Len(Trim(txtACodigo.Text)) = 1 Then lblAError.Text = "El Código del Tipo de Asiento debe ser a partir de 2 digitos a 4." : Exit Sub
        If Len(Trim(txtADescripcion.Text)) = 0 Then lblAError.Text = "Falta el ingreso de la descripción del Tipo de Asiento." : Exit Sub
        If Len(Trim(txtAPrefijo.Text)) = 0 Then lblAError.Text = "Falta el ingreso del prefijo para los ingresos de los Comprobantes" : Exit Sub
        lblError.Text = ""
        If lblAEtiqueta.Text = "Nuevo Tipo de Asiento" Then
            dt = obj.Cont_ExisteAsiento(Session("CodEmpresa"), cboAño.Text, UCase(Trim(txtACodigo.Text)), "1", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblAError.Text = "El Código ingresado del Asiento ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing

            dt = obj.Cont_ExisteAsiento(Session("CodEmpresa"), cboAño.Text, UCase(Trim(txtADescripcion.Text)), "2", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblAError.Text = "La descripción ingresada del Asiento ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing

            dt = obj.Cont_ExisteAsiento(Session("CodEmpresa"), cboAño.Text, UCase(Trim(txtAPrefijo.Text)), "3", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblAError.Text = "El prefijo ingresado del Asiento ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing

            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " INSERT INTO TBASIENTOS(ASIENTO_EMPRESA,ASIENTO_AÑO,ASIENTO_CODIGO,ASIENTO_DESCRIPCION,ASIENTO_SYS_EST,ASIENTO_PREFIJO) VALUES('" & Session("CodEmpresa") & "','" & cboAño.Text & "','" & Trim(Trim(txtACodigo.Text)) & "','" & Trim(txtADescripcion.Text) & "','0','" & UCase(Trim(txtAPrefijo.Text)) & "')"
            CmdGlobal.ExecuteNonQuery()
            Cn.Close()
        ElseIf lblAEtiqueta.Text = "Edición Tipo de Asiento" Then
            If UCase(lblNombre.Text) <> UCase(txtADescripcion.Text) Then
                dt = obj.Cont_ExisteAsiento(Session("CodEmpresa"), cboAño.Text, UCase(Trim(txtADescripcion.Text)), "2", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblAError.Text = "La descripción ingresada del Asiento ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = Nothing
            End If
            If UCase(lblpref.Text) <> UCase(txtAPrefijo.Text) Then
                dt = obj.Cont_ExisteAsiento(Session("CodEmpresa"), cboAño.Text, UCase(Trim(txtAPrefijo.Text)), "3", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblAError.Text = "El prefijo ingresado del Asiento ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = Nothing
            End If
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " UPDATE TBASIENTOS SET ASIENTO_DESCRIPCION='" & Trim(txtADescripcion.Text) & "',ASIENTO_PREFIJO='" & UCase(Trim(txtAPrefijo.Text)) & "' WHERE ASIENTO_SYS_EST='0' AND " _
                                  & " ASIENTO_EMPRESA='" & Session("CodEmpresa") & "' AND ASIENTO_AÑO='" & cboAño.Text & "' AND ASIENTO_CODIGO='" & Trim(txtACodigo.Text) & "'"
            CmdGlobal.ExecuteNonQuery()
            Cn.Close()
        End If
        Call LlenaAsiento()
        btnACancelar_Click(sender, e)
    End Sub
    Protected Sub btnACancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraIngreso.Visible = False
        lblAEtiqueta.Text = ""
        lblNombre.Text = ""
        lblpref.Text = ""
        lblAError.Text = ""
        txtACodigo.Text = ""
        txtADescripcion.Text = ""
        txtAPrefijo.Text = ""
        cboAño.Enabled = True
        txtACodigo.ReadOnly = False
        btnANuevo.Enabled = True
    End Sub
    Protected Sub FlexAsiento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexAsiento.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblAError.Text = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        If e.CommandName = "Editar" Then
            Cn.Open() : CmdGlobal.Connection = Cn
            If FlexAsiento.Rows(Index).Cells(1).Text = 99 Then lblAError.Text = "No podrá editar el Tipo de Asiento." : Exit Sub
            If Existe_Tabla("TBCOMPROB_" & Session("CodEmpresa") & cboAño.Text & "", Session("Ruta_Emp")) = False Then lblAError.Text = "Error, no se ha encontrado la tabla de los Comprobantes del año y de la empresa" : Exit Sub
            CmdGlobal.CommandText = "SELECT * FROM TBCOMPROB_" & Session("CodEmpresa") & cboAño.Text & " WHERE (COMPROB_ASIENTO_CODIGO = '" & FlexAsiento.Rows(Index).Cells(1).Text & "') AND (COMPROB_SYS_EST = '0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then lblAError.Text = "No puede editarse el tipo de Asiento, se encuentra en uso(Ingresos de los Comprobantes)." : Exit Sub
            Rs.Close() : Cn.Close()
            FraIngreso.Visible = True
            lblAEtiqueta.Text = "Edición Tipo de Asiento"
            cboAño.Enabled = False
            txtACodigo.Text = FlexAsiento.Rows(Index).Cells(1).Text
            txtACodigo.ReadOnly = True
            txtADescripcion.Text = FlexAsiento.Rows(Index).Cells(2).Text
            lblNombre.Text = FlexAsiento.Rows(Index).Cells(2).Text
            txtAPrefijo.Text = FlexAsiento.Rows(Index).Cells(3).Text
            lblpref.Text = FlexAsiento.Rows(Index).Cells(3).Text
        End If
    End Sub
    Protected Sub btnDNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraDIngreso.Visible = True
        cboAñoDoc.Enabled = False
        lblDEtiqueta.Text = "Nuevo Documento Contable"
        btnDNuevo.Enabled = False
    End Sub
    Protected Sub btnDCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraDIngreso.Visible = False
        lblDEtiqueta.Text = ""
        lblDDescripcion.Text = ""
        lblDError.Text = ""
        txtDCodigo.Text = ""
        txtDDescripcion.Text = ""
        cboAñoDoc.Enabled = True
        txtDCodigo.ReadOnly = False
        btnDNuevo.Enabled = True
    End Sub
    Protected Sub FlexDoc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexDoc.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblDError.Text = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        If e.CommandName = "Editar" Then
            Cn.Open() : CmdGlobal.Connection = Cn
            If Existe_Tabla("TBCOMPROB_" & Session("CodEmpresa") & cboAñoDoc.Text & "", Session("Ruta_Emp")) = False Then lblAError.Text = "Error, no se ha encontrado la tabla de los Comprobantes del año y de la empresa" : Exit Sub
            CmdGlobal.CommandText = "SELECT * FROM TBCOMPROB_" & Session("CodEmpresa") & cboAñoDoc.Text & " WHERE (COMPROB_DOC_CODIGO = '" & FlexDoc.Rows(Index).Cells(1).Text & "') AND (COMPROB_SYS_EST = '0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then lblDError.Text = "No puede editarse el Documento Contable, se encuentra en uso(Ingresos de los Comprobantes)." : Exit Sub
            Rs.Close() : Cn.Close()
            FraDIngreso.Visible = True
            lblDEtiqueta.Text = "Edición Documento Contable"
            cboAñoDoc.Enabled = False
            txtDCodigo.Text = FlexDoc.Rows(Index).Cells(1).Text
            txtDCodigo.ReadOnly = True
            txtDDescripcion.Text = FlexDoc.Rows(Index).Cells(2).Text
            lblDDescripcion.Text = FlexDoc.Rows(Index).Cells(2).Text
        End If
    End Sub
    Protected Sub btnDGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim obj As New clsCont_Listados
        lblDError.Text = ""
        If Len(Trim(txtDCodigo.Text)) = 0 Then lblDError.Text = "Falta el Ingreso del Código del Documento." : Exit Sub
        If Len(Trim(txtDCodigo.Text)) = 1 Then lblDError.Text = "El Código del Documento debe ser a partir de 2 digitos a 4." : Exit Sub
        If Len(Trim(txtDDescripcion.Text)) = 0 Then lblDError.Text = "Falta el Ingreso del Nombre del Documento Contable." : Exit Sub
        lblDError.Text = ""
        If lblDEtiqueta.Text = "Nuevo Documento Contable" Then
            dt = obj.Cont_ExisteDocumentos(Session("CodEmpresa"), cboAñoDoc.Text, UCase(Trim(txtDCodigo.Text)), "1", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblDError.Text = "El Código ingresado del documento ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing
            dt = obj.Cont_ExisteDocumentos(Session("CodEmpresa"), cboAñoDoc.Text, UCase(Trim(txtDDescripcion.Text)), "2", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblDError.Text = "El Nombre ingresado del documento ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " INSERT INTO TBDOCUMENTOS(DOC_EMPRESA,DOC_AÑO,DOC_CODIGO,DOC_DOCUMENTO,DOC_SYS_EST) VALUES('" & Session("CodEmpresa") & "','" & cboAñoDoc.Text & "','" & Trim(Trim(txtDCodigo.Text)) & "','" & Trim(txtDDescripcion.Text) & "','0')"
            CmdGlobal.ExecuteNonQuery()
            Cn.Close()
        ElseIf lblDEtiqueta.Text = "Edición Documento Contable" Then
            If UCase(lblDDescripcion.Text) <> UCase(txtDDescripcion.Text) Then
                dt = obj.Cont_ExisteDocumentos(Session("CodEmpresa"), cboAñoDoc.Text, UCase(Trim(txtDDescripcion.Text)), "2", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblDError.Text = "El Nombre ingresado del documento ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = Nothing
            End If
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " UPDATE TBDOCUMENTOS SET DOC_DOCUMENTO='" & Trim(txtDDescripcion.Text) & "' WHERE DOC_SYS_EST='0' AND " _
                                  & " DOC_EMPRESA='" & Session("CodEmpresa") & "' AND DOC_AÑO='" & cboAñoDoc.Text & "' AND DOC_CODIGO='" & Trim(txtDCodigo.Text) & "'"
            CmdGlobal.ExecuteNonQuery()
            Cn.Close()
        End If
        Call LlenaDocumento()
        btnDCancelar_Click(sender, e)
    End Sub
    Protected Sub btnFNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraFIngreso.Visible = True
        cboAñoFC.Enabled = False
        lblFEtiqueta.Text = "Nuevo Flujo Caja"
        optFTipo.SelectedValue = 0
        btnFNuevo.Enabled = False
    End Sub
    Protected Sub btnFCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraFIngreso.Visible = False
        lblFEtiqueta.Text = ""
        lblFDescripcion.Text = ""
        lblFError.Text = ""
        txtFCodigo.Text = ""
        txtFDescripcion.Text = ""
        cboAñoFC.Enabled = True
        txtFCodigo.ReadOnly = False
        btnDNuevo.Enabled = True
    End Sub
    Protected Sub btnFGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New DataTable
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim tipo As String
        lblFError.Text = ""
        If Len(Trim(txtFCodigo.Text)) = 0 Then lblFError.Text = "Falta ingresar Codigo Interno de Flujo Caja." : Exit Sub
        If Len(Trim(txtFCodigo.Text)) > 3 Then lblFError.Text = "El Codigo Interno de Flujo Caja debe tener 3 dígitos." : Exit Sub
        If Len(Trim(txtFDescripcion.Text)) = 0 Then lblFError.Text = "Falta ingresar Descripción de Flujo Caja." : Exit Sub
        lblFError.Text = ""
        If optFTipo.SelectedValue = 0 Then tipo = "I" Else tipo = "E"
        If lblFEtiqueta.Text = "Nuevo Flujo Caja" Then
            dt = obj.Cont_ExisteFlujoCaja(Session("CodEmpresa"), cboAñoFC.Text, UCase(Trim(txtFCodigo.Text)), "1", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblFError.Text = "El Código Interno de Flujo Caja ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing
            dt = obj.Cont_ExisteFlujoCaja(Session("CodEmpresa"), cboAñoFC.Text, UCase(Trim(txtFDescripcion.Text)), "2", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then lblFError.Text = "La decsripción ingresado de Flujo Caja ya existe, por favor verificar o cambiarlo" : Exit Sub
            dt = Nothing
            obj2.Cont_InsUpd_FlujoCaja(Session("CodEmpresa"), cboAñoFC.Text, Trim(txtFCodigo.Text), Trim(txtFDescripcion.Text), tipo, "1", 0, Session("Ruta_Emp"))
        ElseIf lblFEtiqueta.Text = "Editar Flujo Caja" Then
            If UCase(lblFDescripcion.Text) <> UCase(txtFDescripcion.Text) Then
                dt = obj.Cont_ExisteFlujoCaja(Session("CodEmpresa"), cboAñoFC.Text, UCase(Trim(txtFDescripcion.Text)), "2", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblFError.Text = "La decsripción ingresado de Flujo Caja ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = Nothing
            End If
            obj2.Cont_InsUpd_FlujoCaja(Session("CodEmpresa"), cboAñoFC.Text, Trim(txtFCodigo.Text), Trim(txtFDescripcion.Text), tipo, "2", CDbl(lblFCodigo.Text), Session("Ruta_Emp"))
        End If
        Call LlenaFlujoCaja()
        btnFCancelar_Click(sender, e)
    End Sub
    Protected Sub FlexFC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexFC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblFError.Text = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        If e.CommandName = "Editar" Then
            Cn.Open() : CmdGlobal.Connection = Cn
            If Existe_Tabla("TBCOMPROB_" & Session("CodEmpresa") & cboAñoFC.Text & "", Session("Ruta_Emp")) = False Then lblFError.Text = "Error, no se ha encontrado la tabla de los Comprobantes del año y de la empresa" : Exit Sub
            CmdGlobal.CommandText = "SELECT * FROM TBCOMPROB_" & Session("CodEmpresa") & cboAñoFC.Text & " WHERE (COMPROB_FLUJOCAJA = '" & FlexFC.Rows(Index).Cells(1).Text & "') AND (COMPROB_SYS_EST = '0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then lblFError.Text = "No puede editarse el Documento Contable, se encuentra en uso(Ingresos de los Comprobantes)." : Exit Sub
            Rs.Close() : Cn.Close()
            FraFIngreso.Visible = True
            lblFEtiqueta.Text = "Editar Flujo Caja"
            cboAñoFC.Enabled = False
            lblFCodigo.Text = FlexFC.Rows(Index).Cells(1).Text
            txtFCodigo.Text = FlexFC.Rows(Index).Cells(2).Text
            txtFCodigo.ReadOnly = True
            txtFDescripcion.Text = FlexFC.Rows(Index).Cells(3).Text
            lblFDescripcion.Text = FlexFC.Rows(Index).Cells(3).Text
            If FlexFC.Rows(Index).Cells(4).Text = "I" Then
                optFTipo.SelectedValue = 0
            Else
                optFTipo.SelectedValue = 1
            End If
        End If
    End Sub
    Protected Sub btnCCNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraCCIngreso.Visible = True
        cboAñoCC.Enabled = False
        lblCCEtiqueta.Text = "Nuevo Centro de Costo Contable"
        txtCCOrden.Enabled = True
        txtCCMascara.Text = "__.__.__.__"
        btnCCNuevo.Enabled = False
    End Sub
    Protected Sub btnCCCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraCCIngreso.Visible = False
        lblCCEtiqueta.Text = ""
        lblCCError.Text = ""
        lblCCDescripcion.Text = ""
        lblCCTieneHijos.Text = ""
        lblCCCuenta1.Text = ""
        lblCCCuenta2.Text = ""
        lblCCCuenta3.Text = ""
        lblCCCuenta4.Text = ""
        lblCCNivel.Text = ""
        lblCCNroNiveles.Text = "4"
        lblCCOrden.Text = ""
        lblCCTieneHijos.Text = ""
        txtCCCodigo.Text = ""
        txtCCDescripcion.Text = ""
        txtCCMascara.Text = ""
        txtCCOrden.Text = ""
        cboAñoCC.Enabled = True
        txtCCCodigo.ReadOnly = False
        btnCCNuevo.Enabled = True
    End Sub
    Protected Sub btnCCGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New DataTable
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim Resp As String
        Dim pNivelOrden As String : pNivelOrden = ""
        Dim pCodigo As Double
        If optCCNivel.SelectedValue = 0 Then pNivelOrden = "P"
        If optCCNivel.SelectedValue = 1 Then pNivelOrden = "S"
        If optCCNivel.SelectedValue = 2 Then pNivelOrden = "R"
        Try
Seguir:
            Resp = Verificador_Cuenta(txtCCOrden.Text)
            If Resp = "1" Then
                txtCCOrden.Text = Rellenar_Ceros_CtaContable(txtCCOrden.Text) : GoTo Seguir
            End If
            If Resp = "2" Then
                If lblCCEtiqueta.Text = "Nuevo Centro de Costo Contable" Or (lblCCEtiqueta.Text = "Editar Centro de Costo Contable" And (lblCCOrden.Text <> txtCCOrden.Text)) Then lblCCError.Text = "El orden ingresado ya existe, por favor verificar o cambiar el orden" : Exit Sub
            End If
            If Resp = "5" Then lblCCError.Text = "El orden ingresado NO es válido, no puede ser cero," & Chr(13) & "por favor verificar o cambiar el orden" : Exit Sub
            If Resp = "6" Then lblCCError.Text = "El orden ingresado NO es válida, no puede saltear un nivel, un nivel no puede" & Chr(13) & "comenzar de cero, por favor verificar o cambiar el orden" : Exit Sub
            If Descomponer_Orden() <> "" Then
                Exit Sub
            End If
            If optCCNivel.SelectedValue <> 0 And optCCNivel.SelectedValue <> 1 And optCCNivel.SelectedValue <> 2 Then lblCCError.Text = "Falta seleccionar el Nivel" : Exit Sub
            If lblCCNivel.Text = "1" And optCCNivel.SelectedValue <> 0 Then
                lblCCError.Text = "El Centro de Costo es nivel principal."
            End If
            If lblCCNivel.Text <> "1" And optCCNivel.SelectedValue = 0 Then
                lblCCError.Text = "El orden del Centro de Costo no puede ser PRINCIPAL"
                Exit Sub
            End If
            If lblCCEtiqueta.Text = "Editar Centro de Costo Contable" Then
                If lblCCTieneHijos.Text = "S" And optCCNivel.SelectedValue = 2 Then lblCCError.Text = "El orden del Centro de Costo debe ser un Sub-Centro" : Exit Sub
            End If
            'If Len(Trim(txtCCCodigo.Text)) = 0 Then lblCCError.Text = "Falta el Ingreso del Código del Documento." : Exit Sub
            If Len(Trim(txtCCDescripcion.Text)) = 0 Then lblCCError.Text = "Falta el Ingreso del Nombre del Documento Contable." : Exit Sub
            lblCCError.Text = ""
            If lblCCEtiqueta.Text = "Nuevo Centro de Costo Contable" Then
                If Resp = "2" Then lblCCError.Text = "El orden del organigrama del Centro de Costo ingresada ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = obj.Cont_ExisteCentroCostos(Session("CodEmpresa"), cboAñoCC.Text, Trim(txtCCOrden.Text), "", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblCCError.Text = "El Código ingresado del documento ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = Nothing
                dt = obj.Cont_ExisteCentroCostos(Session("CodEmpresa"), cboAñoDoc.Text, UCase(Trim(txtCCDescripcion.Text)), "2", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblCCError.Text = "El Nombre ingresado del documento ya existe, por favor verificar o cambiarlo" : Exit Sub
                dt = Nothing
                obj2.Cont_InsUpd_CentroCosto(Session("CodEmpresa"), cboAñoCC.Text, Trim(txtCCOrden.Text), UCase(Trim(txtCCDescripcion.Text)), pNivelOrden, "1", 0, Trim(lblCCNivel.Text), Session("Ruta_Emp"))
            ElseIf lblCCEtiqueta.Text = "Editar Centro de Costo Contable" Then
                pCodigo = txtCCCodigo.Text
                If UCase(lblCCDescripcion.Text) <> UCase(txtCCDescripcion.Text) Then
                    dt = obj.Cont_ExisteCentroCostos(Session("CodEmpresa"), cboAñoCC.Text, UCase(Trim(txtCCDescripcion.Text)), "2", Session("Ruta_Emp"))
                    If dt.Rows.Count > 0 Then lblCCError.Text = "El Nombre ingresado del documento ya existe, por favor verificar o cambiarlo" : Exit Sub
                    dt = Nothing
                    obj2.Cont_InsUpd_CentroCosto(Session("CodEmpresa"), cboAñoCC.Text, Trim(txtCCOrden.Text), UCase(Trim(txtCCDescripcion.Text)), pNivelOrden, "2", pCodigo, Trim(lblCCNivel.Text), Session("Ruta_Emp"))
                End If
            End If
            Call LlenaCCosto()
            btnCCCancelar_Click(sender, e)
        Catch ex As SqlException
            lblCCError.Text = ex.Message
        Catch ex As Exception
            lblCCError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexCC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexCC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs2 As SqlDataReader
        Dim Cad As String
        Dim i As Integer
        lblCCError.Text = ""
        If e.CommandName = "Editar" Then
            Cn.Open() : CmdGlobal.Connection = Cn
            If Existe_Tabla("TBCOMPROB_" & Session("CodEmpresa") & cboAñoCC.Text & "", Session("Ruta_Emp")) = False Then lblCCError.Text = "Error, no se ha encontrado la tabla de los Comprobantes del año y de la empresa" : Exit Sub
            CmdGlobal.CommandText = "SELECT * FROM TBCOMPROB_" & Session("CodEmpresa") & cboAñoCC.Text & " WHERE (COMPROB_CENTRO_COSTO = '" & FlexCC.Rows(Index).Cells(4).Text & "') AND (COMPROB_SYS_EST = '0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then lblCCError.Text = "No puede editarse el Centro de Costo, se encuentra en uso(Ingresos de los Comprobantes)." : Exit Sub
            Rs.Close() : Cn.Close()
            FraCCIngreso.Visible = True
            lblCCEtiqueta.Text = "Editar Centro de Costo Contable"
            cboAñoCC.Enabled = False
            txtCCCodigo.ReadOnly = True
            txtCCOrden.Text = FlexCC.Rows(Index).Cells(1).Text
            txtCCDescripcion.Text = FlexCC.Rows(Index).Cells(2).Text
            lblCCDescripcion.Text = FlexCC.Rows(Index).Cells(2).Text
            txtCCCodigo.Text = FlexCC.Rows(Index).Cells(4).Text
            If FlexCC.Rows(Index).Cells(3).Text = "P" Then
                optCCNivel.SelectedValue = 0
            ElseIf FlexCC.Rows(Index).Cells(3).Text = "S" Then
                optCCNivel.SelectedValue = 1
            ElseIf FlexCC.Rows(Index).Cells(3).Text = "R" Then
                optCCNivel.SelectedValue = 2
            End If
            txtCCOrden_TextChanged(sender, e)
            Cad = ""
            For i = 1 To Val(lblCCNivel.Text)
                If Cad <> "" Then Cad = Cad & "."
                If i = 1 Then Cad = Cad & lblCCCuenta1.Text
                If i = 2 Then Cad = Cad & lblCCCuenta2.Text
                If i = 3 Then Cad = Cad & lblCCCuenta3.Text
                If i = 4 Then Cad = Cad & lblCCCuenta4.Text
            Next
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            CmdGlobal2.CommandText = "SELECT * FROM TBCENTROCOSTOS WHERE CCOSTO_EMPRESA='" & Session("CodEmpresa") & "' AND CCOSTO_AÑO='" & cboAñoCC.Text & "' " _
            & " AND LEFT(CCOSTO_ORGANIGRAMA," & Len(Cad) & ")='" & Cad & "' AND CCOSTO_CODIGO<>'" & Trim(txtCCCodigo.Text) & "' AND CCOSTO_SYS_EST='0'"
            Rs2 = CmdGlobal2.ExecuteReader
            If Rs2.HasRows Then lblCCTieneHijos.Text = "S"
            Rs2.Close() : Cn2.Close()
            If lblCCTieneHijos.Text = "S" Then txtCCOrden.Enabled = False Else txtCCOrden.Enabled = True
        End If
    End Sub
    Protected Sub txtCCOrden_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCOrden.TextChanged
        Dim Resp As String
Seguir:
        Resp = Verificador_Cuenta(txtCCOrden.Text)
        If Resp = "1" Then
            txtCCOrden.Text = Rellenar_Ceros_CtaContable(txtCCOrden.Text) : GoTo Seguir
        End If
        If Resp = "2" Then
            If lblFEtiqueta.Text = "Nuevo Centro de Costo Contable" Or (lblCCEtiqueta.Text = "Editar Centro de Costo" And (lblCCOrden.Text <> txtCCOrden.Text)) Then lblCCError.Text = "El orden ingresado ya existe, por favor verificar o cambiar el orden" : Exit Sub
        End If
        If Resp = "5" Then lblCCError.Text = "El orden ingresado NO es válido, no puede ser cero," & Chr(13) & "por favor verificar o cambiar el orden" : Exit Sub
        If Resp = "6" Then lblCCError.Text = "El orden ingresado NO es válida, no puede saltear un nivel, un nivel no puede" & Chr(13) & "comenzar de cero, por favor verificar o cambiar el orden" : Exit Sub
        If Descomponer_Orden() <> "" Then
            Exit Sub
        End If
    End Sub
    Private Function Verificador_Cuenta(ByVal CadVerif As String) As String
        Dim ii As Integer, aa As Integer
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Cad As String
        Verificador_Cuenta = ""
        For ii = 1 To Len(CadVerif)
            If Trim(Mid(CadVerif, ii, 1)) = "_" Then
                Verificador_Cuenta = "1" : Exit Function
            End If
        Next
        'VERIFICAR QUE LA CUENTA NO SALTEE NINGÚN NIVEL Y NO SEA DE CEROS
        Cad = ""
        For ii = 1 To Len(CadVerif)
            If Trim(Mid(CadVerif, ii, 1)) = "." Then
            Else
                Cad = Cad & Trim(Mid(CadVerif, ii, 1))
            End If
        Next
        If Val(Cad) = 0 Then Verificador_Cuenta = "5" : Exit Function
        Cad = "" : aa = 0
        For ii = 1 To Len(CadVerif)
            If Trim(Mid(CadVerif, ii, 1)) = "." Then
                If Val(Cad) = 0 Then aa = ii : Exit For
                Cad = ""
            Else
                Cad = Cad & Trim(Mid(CadVerif, ii, 1))
            End If
        Next
        If aa <> 0 Then
            Cad = ""
            For ii = ii - 1 To Len(CadVerif)
                If Trim(Mid(CadVerif, ii, 1)) = "." Then
                    If Val(Cad) > 0 Then Verificador_Cuenta = "6" : Exit Function
                    Cad = ""
                Else
                    Cad = Cad & Trim(Mid(CadVerif, ii, 1))
                    If Len(CadVerif) = ii Then
                        If Val(Cad) > 0 Then Verificador_Cuenta = "6" : Exit Function
                    End If
                End If
            Next
        End If
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT * FROM TBCENTROCOSTOS WHERE CCOSTO_EMPRESA='" & Session("CodEmpresa") & "' AND CCOSTO_SYS_EST='0' AND CCOSTO_AÑO='" & cboAñoCC.Text & "' AND CCOSTO_ORGANIGRAMA='" & CadVerif & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            If lblCCEtiqueta.Text = "Nuevo Centro de Costo Contable" Then Verificador_Cuenta = "2"
        End If
        Rs.Close() : Cn.Close()
    End Function
    Public Function Rellenar_Ceros_CtaContable(ByVal cCuenta As String) As String
        Dim iix As Integer
        Rellenar_Ceros_CtaContable = cCuenta
Avanza:
        For iix = 1 To Len(cCuenta)
            If Trim(Mid(Rellenar_Ceros_CtaContable, iix, 1)) = "_" Or Len(Trim(Mid(Rellenar_Ceros_CtaContable, iix, 1))) = 0 Then
                Rellenar_Ceros_CtaContable = Left(Rellenar_Ceros_CtaContable, iix - 1) & "0" & Mid(Rellenar_Ceros_CtaContable, iix + 1)
                GoTo Avanza
            End If
        Next
    End Function
    Private Function Descomponer_Orden() As String
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim a, i As Integer
        Dim Cad, Cad2, CuentaSup As String
        Descomponer_Orden = ""
        a = 1
        lblCCCuenta1.Text = ""
        For i = 1 To Len(txtCCOrden.Text)
            If Mid(txtCCOrden.Text, i, 1) = "." Then
                a = a + 1
                If a = 1 Then lblCCCuenta1.Text = ""
                If a = 2 Then lblCCCuenta2.Text = ""
                If a = 3 Then lblCCCuenta3.Text = ""
                If a = 4 Then lblCCCuenta4.Text = ""
            Else
                If a = 1 Then lblCCCuenta1.Text = lblCCCuenta1.Text & Mid(txtCCOrden.Text, i, 1)
                If a = 2 Then lblCCCuenta2.Text = lblCCCuenta2.Text & Mid(txtCCOrden.Text, i, 1)
                If a = 3 Then lblCCCuenta3.Text = lblCCCuenta3.Text & Mid(txtCCOrden.Text, i, 1)
                If a = 4 Then lblCCCuenta4.Text = lblCCCuenta4.Text & Mid(txtCCOrden.Text, i, 1)
            End If
        Next
        lblCCNivel.Text = ""
        For i = Val(lblCCNroNiveles.Text) To 1 Step -1
            If i = 1 Then If Val(lblCCCuenta1.Text) > 0 Then lblCCNivel.Text = i : Exit For
            If i = 2 Then If Val(lblCCCuenta2.Text) > 0 Then lblCCNivel.Text = i : Exit For
            If i = 3 Then If Val(lblCCCuenta3.Text) > 0 Then lblCCNivel.Text = i : Exit For
            If i = 4 Then If Val(lblCCCuenta4.Text) > 0 Then lblCCNivel.Text = i : Exit For
        Next
        If lblCCNivel.Text = "" Or lblCCNivel.Text = "1" Then
        Else
            Cad = "" : Cad2 = "" : CuentaSup = "" : a = 0
            For i = 1 To Len(txtCCOrden.Text)
                If Mid(txtCCOrden.Text, i, 1) = "." Then
                    If Cad2 <> "" Then Cad2 = Cad2 & "."
                    Cad2 = Cad2 & Cad
                    a = a + 1
                    CuentaSup = CompletarCeros(Cad2)
                    If Existe_Orden(CuentaSup) = False Then
                        If a = Val(lblCCNivel.Text) Then
                            GoTo Seguir
                        Else
                            Descomponer_Orden = "1"
                            lblCCError.Text = "El orden que intenta guardar no es válida, para que existe el orden " & txtCCOrden.Text & Chr(13) & "debe primero existir su orden superior; es decir, el orden " & CuentaSup & Chr(13) & "Favor de verificar o cambiar el orden." : Exit Function
                        End If
                    Else
                        Cad = "" : CuentaSup = ""
                    End If
                Else
                    Cad = Cad & Mid(txtCCOrden.Text, i, 1)
                End If
            Next
Seguir:
            CuentaSup = ""
            CuentaSup = Ubicar_Cuenta_Nro_Nivel(txtCCOrden.Text, Val(lblCCNivel.Text) - 1)
            CuentaSup = CompletarCeros(CuentaSup)
            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT CCOSTO_NIVEL_ORDEN FROM TBCENTROCOSTOS WHERE CCOSTO_EMPRESA='" & Session("CodEmpresa") & "' AND CCOSTO_AÑO='" & cboAñoCC.Text & "' AND CCOSTO_SYS_EST='0' AND CCOSTO_ORGANIGRAMA='" & CuentaSup & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If UCase(Nu(Rs!CCOSTO_NIVEL_ORDEN)) = "R" Then
                        Descomponer_Orden = "2"
                        lblCCError.Text = "El Orden superior del orden ingresado es " & CuentaSup & Chr(13) & "que posee el Nivel REGISTRO y por lo tanto NO puede" & Chr(13) & "agregar el Centro de Costo."
                    End If
                End While
            Else
                Descomponer_Orden = "1"
                lblCCError.Text = "El Orden que intenta guardar no es válida"
            End If
            Rs.Close() : Cn.Close()
        End If
        If lblCCNivel.Text = "1" Then optCCNivel.SelectedValue = 0
    End Function
    Private Function CompletarCeros(ByVal Cadena As String) As String
        Dim iix As Integer
        CompletarCeros = Cadena
        For iix = Len(Cadena) + 1 To Len(txtCCMascara.Text)
            If Trim(Mid(txtCCMascara.Text, iix, 1)) = "_" Then
                CompletarCeros = CompletarCeros & "0"
            ElseIf Trim(Mid(txtCCMascara.Text, iix, 1)) = "." Then
                CompletarCeros = CompletarCeros & "."
            End If
        Next
    End Function
    Private Function Existe_Orden(ByVal cCuenta As String) As Boolean
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Existe_Orden = False
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT * FROM TBCENTROCOSTOS WHERE CCOSTO_EMPRESA='" & Session("CodEmpresa") & "' AND CCOSTO_AÑO='" & cboAñoCC.Text & "' AND CCOSTO_SYS_EST='0' AND CCOSTO_ORGANIGRAMA='" & cCuenta & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then Existe_Orden = True
        Rs.Close() : Cn.Close()
    End Function
    Private Function Ubicar_Cuenta_Nro_Nivel(ByVal Cuenta As String, ByVal NroNivel As Integer) As String
        Dim ii As Integer, ip As Integer
        ip = 0
        Ubicar_Cuenta_Nro_Nivel = ""
        For ii = 1 To Len(Cuenta)
            If Mid(Cuenta, ii, 1) = "." Then ip = ip + 1
            If ip = NroNivel Then
                Ubicar_Cuenta_Nro_Nivel = Left(Cuenta, ii) : Exit Function
            End If
        Next
    End Function
    Private Sub Lista_TC()
        Call Carga_Lista_Tipo_Cambio(cboAñoTC.Text, cboMesTC.SelectedValue.Trim)
    End Sub
    Private Sub Carga_Lista_Tipo_Cambio(ByVal Aññ As String, ByVal Mm As String)
        Dim Dias As Integer
        Dim Fecha
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim dt As DataTable
        Dim i As Integer = 0
        Dias = 0
        Try
            dt = obj.Cont_ExisteAñoMes(Aññ, Mm, Session("Ruta_Emp"))
            If dt.Rows.Count = 0 Then
                Fecha = 31 & "/" & Mm & "/" & Aññ
                If IsDate(Fecha) = True Then
                    Dias = 31
                    GoTo INSERTA
                Else
                    Fecha = 30 & "/" & Mm & "/" & Aññ
                    If IsDate(Fecha) = True Then
                        Dias = 30
                        GoTo INSERTA
                    Else
                        Fecha = 29 & "/" & Mm & "/" & Aññ
                        If IsDate(Fecha) = True Then
                            Dias = 29
                            GoTo INSERTA
                        Else
                            Fecha = 28 & "/" & Mm & "/" & Aññ
                            Dias = 28
                            GoTo INSERTA
                        End If
                    End If
                End If
INSERTA:
                For i = 1 To Dias
                    Fecha = Aññ & Mm & Format(i, "00")
                    obj2.Cont_InsUpd_TipoCambio(Fecha, "", HttpContext.Current.User.Identity.Name, 0, 0, "1", Session("Ruta_Emp"))
                Next
            End If
            dt = Nothing
            FlexTC.DataSource = obj.Cont_ListaTipoCambio(Aññ, Mm, Session("Ruta_Emp"))
            FlexTC.DataBind()
        Catch ex As SqlException
            lblTCError.Text = ex.Message
        Catch ex As Exception
            lblTCError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAñoTC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAñoTC.SelectedIndexChanged
        Call Lista_TC()
    End Sub
    Protected Sub cboMesTC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMesTC.SelectedIndexChanged
        Call Lista_TC()
    End Sub
    Protected Sub FlexTC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTC.PageIndexChanging
        lblTCError.Text = ""
        FlexTC.PageIndex = e.NewPageIndex
        Call Lista_TC()
    End Sub
    Protected Sub FlexTC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblTCError.Text = ""
        If e.CommandName = "Editar" Then
            lblIngresoTC.Visible = True
            lblEtiquetaTC.Text = "Edición Tipo de Cambio"
            cboAñoTC.Enabled = False
            cboMesTC.Enabled = False
            txtFecha.Text = FlexTC.Rows(Index).Cells(1).Text
            txtTCCompra.Text = FlexTC.Rows(Index).Cells(2).Text
            txtTCVenta.Text = FlexTC.Rows(Index).Cells(3).Text
            FlexTC.Enabled = False
        End If
        Call TiempoPerson(True)
    End Sub
    Protected Sub btntcCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoTC.Visible = False
        lblEtiquetaTC.Text = ""
        cboAñoTC.Enabled = True
        cboMesTC.Enabled = True
        FlexTC.Enabled = True
        Call TiempoPerson(True)
    End Sub
    Protected Sub btnTCGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New DataTable
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim Fecha As String = ""
        Dim pCompra As Decimal = 0
        Dim pVenta As Decimal = 0
        lblFError.Text = ""
        If lblEtiquetaTC.Text = "Edición Tipo de Cambio" Then
            pCompra = txtTCCompra.Text.Trim
            pVenta = txtTCVenta.Text.Trim
            Fecha = Right(txtFecha.Text.Trim, 4) & Mid(txtFecha.Text.Trim, 4, 2) & Left(txtFecha.Text.Trim, 2)
            obj2.Cont_InsUpd_TipoCambio(Fecha, "", HttpContext.Current.User.Identity.Name, pCompra, pVenta, "2", Session("Ruta_Emp"))
        End If
        Call Lista_TC()
        btntcCancelar_Click(sender, e)
    End Sub
    Private Sub TiempoPerson(Optional ByVal Completo As Boolean = False)
        Dim myConnection_FA As New SqlClient.SqlConnection(Ruta_Ng)
        Dim myCommand_FA As New SqlClient.SqlCommand("SELECT GETDATE()", myConnection_FA)
        Dim myReader_Fa As SqlClient.SqlDataReader
        Dim Fecha As String = ""
        myConnection_FA.Open()
        myReader_Fa = myCommand_FA.ExecuteReader()
        While myReader_Fa.Read()
            txtFechaSistema.Text = Format(Day(myReader_Fa.GetDateTime(0)), "00") & "/" & Format(Month(myReader_Fa.GetDateTime(0)), "00") & "/" & Format(Year(myReader_Fa.GetDateTime(0)), "0000")
            If Completo = False Then
                txtHoraSistema.Text = Format(Hour(myReader_Fa.GetDateTime(0)), "00") + ":" + Format(Minute(myReader_Fa.GetDateTime(0)), "00")
            Else
                txtHoraSistema.Text = Format(Hour(myReader_Fa.GetDateTime(0)), "00") + ":" + Format(Minute(myReader_Fa.GetDateTime(0)), "00") + ":" + Format(Second(myReader_Fa.GetDateTime(0)), "00")
            End If
        End While
        myReader_Fa.Close()
        myConnection_FA.Close()
    End Sub
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        Call TiempoPerson(True)
    End Sub
    Protected Sub cboAñoPP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Lista_Periodo()
        Call FechaInicio()
        Call NroPeriodos()
    End Sub
    Private Sub Lista_Periodo()
        Dim obj As New clsCont_Listados
        Try
            FlexPP.DataSource = obj.Cont_ListaPeriodos(Session("CodEmpresa"), cboAñoPP.Text, "NO", "", Session("Ruta_Emp"))
            FlexPP.DataBind()
        Catch ex As SqlException
            lblPPError.Text = ex.Message
        Catch ex As Exception
            lblPPError.Text = ex.Message
        Finally
        End Try
        Dim i As Integer
        For i = 0 To FlexPP.Rows.Count - 1
            If FlexPP.Rows(i).Cells(12).Text.Trim = "S" Then
                FlexPP.Rows(i).BackColor = Drawing.Color.DarkGreen
                FlexPP.Rows(i).Cells(10).ForeColor = Drawing.Color.DarkGreen
                FlexPP.Rows(i).Cells(11).ForeColor = Drawing.Color.DarkGreen
                FlexPP.Rows(i).Cells(12).ForeColor = Drawing.Color.DarkGreen
            Else
                FlexPP.Rows(i).BackColor = Drawing.Color.White
                FlexPP.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                FlexPP.Rows(i).Cells(11).ForeColor = Drawing.Color.White
                FlexPP.Rows(i).Cells(12).ForeColor = Drawing.Color.White
            End If
        Next
    End Sub
    Protected Sub FlexPP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPP.PageIndexChanging
        lblPPError.Text = ""
        FlexPP.PageIndex = e.NewPageIndex
        Call Lista_Periodo()
    End Sub
    Protected Sub FlexPP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblPPError.Text = ""
        Dim obj As New clsCont_InsUpdDel
        Dim pFechaIni As String = ""
        Dim pFechaFin As String = ""
        pFechaIni = Right(FlexPP.Rows(Index).Cells(7).Text, 4) & Mid(FlexPP.Rows(Index).Cells(7).Text, 4, 2) & Left(FlexPP.Rows(Index).Cells(7).Text, 2)
        pFechaFin = Right(FlexPP.Rows(Index).Cells(8).Text, 4) & Mid(FlexPP.Rows(Index).Cells(8).Text, 4, 2) & Left(FlexPP.Rows(Index).Cells(8).Text, 2)
        If e.CommandName = "PActual" Then
            If FlexPP.Rows(Index).Cells(10).Text = "0" Then
                lblPPError.Text = "Un periodo cerrado no puede estar como periodo actual a que no sea" & Chr(13) & "el último periodo." : Exit Sub
            Else
                If MsgBox("Saber el periodo actual brinda ayuda en el momento de ingresar" & Chr(13) & _
                          "comprobantes o mostrar los reportes de contabilidad." & Chr(13) & Chr(13) & _
                          "¿Confirma que desea establecer como Periodo Actual?", vbQuestion + vbYesNo, "Periodo Actual") = vbYes Then
                    obj.Cont_InsUpd_Periodo(Session("CodEmpresa"), cboAñoPP.Text.Trim, "", "", FlexPP.Rows(Index).Cells(5).Text.Trim, "", pFechaIni, pFechaIni, "S", "3", Session("Ruta_Emp"))
                    obj.Cont_InsUpd_Periodo(Session("CodEmpresa"), cboAñoPP.Text.Trim, "", "", FlexPP.Rows(Index).Cells(5).Text.Trim, "", pFechaIni, pFechaFin, "N", "4", Session("Ruta_Emp"))
                End If
            End If
        ElseIf e.CommandName = "CerrarP" Then
            If FlexPP.Rows(Index).Cells(10).Text = "1" Then
                If MsgBox("Al cerrar un periodo implica no poder ingresar más comprobantes." & Chr(13) & Chr(13) & "¿Confirma que desea Cerrar el Periodo?", vbQuestion + vbYesNo, "Cierre de Periodo") = vbYes Then
                    obj.Cont_InsUpd_Periodo(Session("CodEmpresa"), cboAñoPP.Text.Trim, "", "", FlexPP.Rows(Index).Cells(5).Text.Trim, "", pFechaIni, pFechaIni, "", "6", Session("Ruta_Emp"))
                End If
            ElseIf FlexPP.Rows(Index).Cells(10).Text = "0" Then
                lblPPError.Text = "El periodo ya se encuentra cerrado." : Exit Sub
            Else
                lblPPError.Text = "El periodo no indica ningún ingreso de comprobante, por lo tanto" & Chr(13) & "no puede abrir ni cerrar el periodo." : Exit Sub
            End If
        ElseIf e.CommandName = "AbrirP" Then
            If FlexPP.Rows(Index).Cells(10).Text = "0" Then
                If MsgBox("Al abrir un periodo implica poder ingresar más comprobantes." & Chr(13) & Chr(13) & "¿Confirma que desea Abrir el Periodo?", vbQuestion + vbYesNo, "Abrir Periodo") = vbYes Then
                    obj.Cont_InsUpd_Periodo(Session("CodEmpresa"), cboAñoPP.Text.Trim, "", "", FlexPP.Rows(Index).Cells(5).Text.Trim, "", pFechaIni, pFechaIni, "", "5", Session("Ruta_Emp"))
                End If
            ElseIf FlexPP.Rows(Index).Cells(10).Text = "1" Then
                lblPPError.Text = "El periodo ya se encuentra abierto." : Exit Sub
            Else
                lblPPError.Text = "El periodo no indica ningún ingreso de comprobante, por lo tanto" & Chr(13) & "no puede abrir ni cerrar el periodo." : Exit Sub
            End If
        ElseIf e.CommandName = "Eliminar" Then
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal2 As New SqlCommand
            Dim Rs As SqlDataReader
            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT * FROM TBCOMPROB_" & Session("CodEmpresa") & cboAñoPP.Text & " V WHERE COMPROB_SYS_EST = '0'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                lblPPError.Text = "No se puede eliminar la definición de periodos, existen comprobantes ingresados." : Exit Sub
            Else
                If MsgBox("¿Confirma eliminar la definición de periodos sin opción de recuperarlo?.", vbQuestion + vbYesNo, "Eliminar Periodo") = vbYes Then
                    obj.Cont_InsUpd_Periodo(Session("CodEmpresa"), cboAñoPP.Text.Trim, "", "", FlexPP.Rows(Index).Cells(5).Text.Trim, "", pFechaIni, pFechaIni, "", "7", Session("Ruta_Emp"))
                    Cn2.Open()
                    CmdGlobal2.Connection = Cn2
                    CmdGlobal2.CommandText = "DELETE FROM TBCOMPROB_" & Session("CodEmpresa") & cboAñoPP.Text
                    CmdGlobal2.ExecuteNonQuery()
                    Cn2.Close()
                Else
                End If
            End If
            Rs.Close() : Cn.Close()
        End If
        Call Lista_Periodo()
    End Sub
End Class
