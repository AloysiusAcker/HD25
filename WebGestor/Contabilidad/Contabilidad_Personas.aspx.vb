Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Partial Class Contabilidad_Personas
    Inherits System.Web.UI.Page

    Private Sub ConsultarRUC()
        'Dim ruc As String = txtRuc.Text.Trim()

        'If Not String.IsNullOrEmpty(ruc) Then
        '    Dim url As String = "https://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias"
        '    Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        '    request.Method = "POST"
        '    request.ContentType = "application/x-www-form-urlencoded"

        '    Dim postData As String = "nroRuc=" & ruc
        '    Dim byteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(postData)
        '    request.ContentLength = byteArray.Length

        '    Using dataStream As Stream = request.GetRequestStream()
        '        dataStream.Write(byteArray, 0, byteArray.Length)
        '    End Using

        '    Try
        '        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        '        Using reader As New StreamReader(response.GetResponseStream())
        '            Dim html As String = reader.ReadToEnd()
        '            Dim doc As New HtmlDocument()
        '            doc.LoadHtml(html)

        '            ' Ejemplo: Extraer el nombre o razón social
        '            Dim razonSocialNode As HtmlNode = doc.DocumentNode.SelectSingleNode("//input[@id='txtRazonSocial']")
        '            Dim razonSocial As String = If(razonSocialNode IsNot Nothing, razonSocialNode.GetAttributeValue("value", "No encontrado"), "No encontrado")

        '            txtRazonSocial.Text = "<pre>Razón Social: " & razonSocial & "</pre>"
        '        End Using
        '    Catch ex As Exception
        '        litResultado.Text = "Error al consultar el RUC: " & ex.Message
        '    End Try
        'Else
        '    litResultado.Text = "Por favor, ingrese un RUC válido."
        'End If
    End Sub


    Private Sub Datos_Sunat()
        Try
            Dim cokkie As CookieContainer = New CookieContainer()
            Dim myurl As String = "http://api.sunat.cloud/ruc/" + txtRuc.Text.Trim()
            Dim myWebRequest As HttpWebRequest = WebRequest.Create(myurl)
            myWebRequest.CookieContainer = cokkie
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim myhttpWebResponse As HttpWebResponse = myWebRequest.GetResponse()
            Dim myStream As Stream = myhttpWebResponse.GetResponseStream()
            Dim myStreamReader As New StreamReader(myStream)
            Dim xDat As String = ""
            Dim pos As Integer = 0

            If txtRuc.Text.Length() <> 11 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El RUC debe ser de 11 dígitos');", True)
                txtRazonSocial.Text = ""
                txtDireccion.Text = ""
            Else
                Dim c As Int32 = 0
                Dim psNumero As Integer
                txtRuc.Text = Convert.ToInt64(txtRuc.Text.ToString())
                If txtRuc.Text.Substring(0, 2) = "10" Then
                    While (myStreamReader.EndOfStream = False)
                        txtRuc.Text = txtRuc.Text.Trim()
                        xDat = myStreamReader.ReadLine()
                        pos = pos + 1
                        psNumero = InStr(1, xDat, ":")
                        c = xDat.Length()
                        Select Case pos
                            Case 3
                                txtRazonSocial.Text = IIf(IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString()).ToString() = "-", "", IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString()).ToString())
                                Exit Select
                            Case 9
                                txtNomComercial.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 5
                                txtFechaInicioActividades.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 8
                                txtEstadoContribuyente.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 11
                                txtDireccion.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                        End Select
                    End While
                ElseIf txtRuc.Text.Substring(0, 2) = "20" Then
                    While (myStreamReader.EndOfStream = False)
                        txtRuc.Text = txtRuc.Text.Trim()
                        xDat = myStreamReader.ReadLine()
                        pos = pos + 1
                        psNumero = InStr(1, xDat.ToString, ":")
                        c = xDat.Length()
                        Select Case pos
                            Case 3
                                txtRazonSocial.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 9
                                txtNomComercial.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 5
                                txtFechaInicioActividades.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 8
                                txtEstadoContribuyente.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                            Case 11
                                txtDireccion.Text = IIf(xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString() = "-", "", xDat.Substring(psNumero + 2, (c - 2) - (psNumero + 2)).ToString())
                                Exit Select
                        End Select
                    End While
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe ingresar un RUC que comience con 10 o 20');", True)
                End If
                If pos < 5 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('RUC no existe');", True)
                    txtRazonSocial.Text = ""
                    txtDireccion.Text = ""
                End If
            End If
        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El RUC es solo números');", True)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                lblError.Text = ""
                cboTipoPer.Items.Clear()
                cboTipoCliente.Items.Clear() : cboTipoCliente.Enabled = False
                cboPais.Items.Clear()
                Call LlenaComboItem("TBOPC001", cboTipoPer) : cboTipoPer.Items.Add("< Seleccionar >") : cboTipoPer.SelectedValue = "< Seleccionar >"
                Call LlenaComboItem("TBOPC361", cboTipoCliente) : cboTipoCliente.Items.Add("< Seleccionar >") : cboTipoCliente.SelectedValue = "< Seleccionar >"
                Call LlenaComboItem("TBOPC006", cboPais) : cboPais.Items.Add("< Seleccionar >") : cboPais.SelectedValue = "< Seleccionar >"
                If cboPais.Items.Count > 0 Then cboPais.SelectedValue = "51" : cboPais_SelectedIndexChanged(sender, e)

                '---------------------------------------------------------------------'
                Dim obj As New Cls_Cliente
                Dim dt As New DataTable
                Dim codPersona As String = Convert.ToString(Request.QueryString("WpkDi"))
                Dim ruc As String = Convert.ToString(Request.QueryString("KllPd0s"))
                Dim cif As String = Convert.ToString(Request.QueryString("Ni830dHuciPLO"))
                Dim razo As String = Convert.ToString(Request.QueryString("093008roijfoiJ_sfF"))
                Dim codCliente As String = Convert.ToString(Request.QueryString("Lpoeh58FJIJS0lk"))
                Dim nroTicket As String = Convert.ToString(Request.QueryString("KJoi09jdJA90dIW"))
                Dim dbRow As DataRow
                If codPersona <> "" Or ruc <> "" Or cif <> "" Or razo <> "" Or codCliente <> "" Then
                    If nroTicket = "" Then
                        Session("Redireccion") = "CLIENTE"
                    Else
                        Session("Redireccion") = "TICKET"
                        txtNroTicket.Text = nroTicket
                    End If
                    If codPersona <> "" Or ruc <> "" Then
                        dt = obj.Lista_Datos_Clientes(Session("Ruta_Emp"), codPersona, ruc)
                        If dt.Rows.Count > 0 Then
                            dbRow = dt.Rows(0)
                            cboTipoPer.Items.Clear()
                            Call LlenaComboItem("TBOPC001", cboTipoPer) : cboTipoPer.Items.Add("< Seleccionar >") : cboTipoPer.SelectedValue = "< Seleccionar >"
                            cboTipoPer_SelectedIndexChanged(sender, e)
                            cboTipoCliente.Items.Clear() : cboTipoCliente.Enabled = False
                            Call LlenaComboItem("TBOPC361", cboTipoCliente) : cboTipoCliente.Items.Add("< Seleccionar >") : cboTipoCliente.SelectedValue = "< Seleccionar >"
                            cboPais.Items.Clear() : cboPais.Items.Add("< Seleccionar >") : cboPais.SelectedValue = "< Seleccionar >"
                            Call LlenaComboItem("TBOPC006", cboPais)
                            If cboPais.Items.Count > 0 Then cboPais.SelectedValue = "51" : cboPais_SelectedIndexChanged(sender, e)
                            If cboDpto.Items.Count > 0 Then cboDpto.SelectedValue = "150000" : cboDpto_SelectedIndexChanged(sender, e)
                            If cboProv.Items.Count > 0 Then cboProv.SelectedValue = "150100" : cboProv_SelectedIndexChanged(sender, e)
                            Call LlenaComboItem("TBOPC472", CboRubro)
                            Call LlenaComboItem("TBOPC527", CboSectorEconomico)
                            txtCodPersona.Text = codPersona.ToString()
                            txtCertInscrip.Text = Nu(dbRow("PERSONA_CERT_INSCR"))
                            txtContacto.Text = Nu(dbRow("PERSONA_NOMBRE_CONTACTO"))
                            txtDireccion.Text = Nu(dbRow("PERSONA_DIRECCION"))
                            txtCorreo.Text = Nu(dbRow("PERSONA_EMAIL"))
                            txtNombres.Text = Nu(dbRow("PERSONA_NOMBRES"))
                            txtRazonSocial.Text = Nu(dbRow("PERSONA_RAZON_SOCIAL"))
                            txtRuc.Text = Nu(dbRow("PERSONA_RUC"))
                            txtPagWeb.Text = Nu(dbRow("PERSONA_WEB"))
                            txtTelef1.Text = Nu(dbRow("PERSONA_TELF1"))
                            If Nu(dbRow("PERSONA_TELF2")) <> "" Then txtTelef2.Text = Nu(dbRow("PERSONA_TELF2"))
                            cboTipoPer.SelectedValue = Nu(dbRow("PERSONA_TIPO")) : cboTipoPer_SelectedIndexChanged(sender, e)
                            If cboTipoPer.SelectedValue = "1" And Nu(dbRow("PERSONA_TIPO_CLIENTE")) <> "" Then cboTipoCliente.SelectedValue = Nu(dbRow("PERSONA_TIPO_CLIENTE"))
                            If Nu(dbRow("PERSONA_PAIS")) <> "" Then cboPais.SelectedValue = Nu(dbRow("PERSONA_PAIS")) : cboPais_SelectedIndexChanged(sender, e)
                            If Nu(dbRow("PERSONA_DPTO")) <> "" Then cboDpto.SelectedValue = Nu(dbRow("PERSONA_DPTO")) : cboDpto_SelectedIndexChanged(sender, e)
                            If Nu(dbRow("PERSONA_PROV")) <> "" Then cboProv.SelectedValue = Nu(dbRow("PERSONA_PROV")) : cboProv_SelectedIndexChanged(sender, e)
                            If Nu(dbRow("PERSONA_DIST")) <> "" Then cboDist.SelectedValue = Nu(dbRow("PERSONA_DIST"))
                            If Nu(dbRow("PERSONA_RUBRO")) <> "" Then CboRubro.SelectedValue = Nu(dbRow("PERSONA_RUBRO"))
                            If Nu(dbRow("PERSONA_CUII")) <> "" Then CboSectorEconomico.SelectedValue = Nu(dbRow("PERSONA_CUII"))
                            If Nu(dbRow("PERSONA_CEPRO")) <> "" Then CboSectorEconomico.SelectedValue = Nu(dbRow("PERSONA_CEPRO"))
                            If dt.Rows.Count > 0 Then
                                dt = obj.Lista_Contacto_Personas(Session("Ruta_Emp"), codPersona, "%")
                                dbRow = dt.Rows(0)
                                txtApepat.Text = Nu(dbRow("CONTACTO_APEPAT"))
                                txtApemat.Text = Nu(dbRow("CONTACTO_APEPAT"))
                                txtDniContacto.Text = Nu(dbRow("CONTACTO_DOC_NRO"))
                                txtNombres.Text = Nu(dbRow("CONTACTO_NOMBRES"))
                            End If
                            FraIngreso.Visible = True
                            lblEtiqueta.Text = "Editar Persona"
                            Session("Nuevo_Reg") = "Persona"
                            Exit Sub
                        End If
                    End If
                    If cif <> "" Or razo <> "" Then
                        dt = obj.Lista_Clientes(Session("Ruta_Emp"), razo, cif)
                        If dt.Rows.Count > 0 Then
                            dbRow = dt.Rows(0)
                            cboTipoPer.Items.Clear()
                            Call LlenaComboItem("TBOPC001", cboTipoPer) : cboTipoPer.Items.Add("< Seleccionar >") : cboTipoPer.SelectedValue = "< Seleccionar >"
                            cboTipoPer_SelectedIndexChanged(sender, e)
                            cboTipoCliente.Items.Clear() : cboTipoCliente.Enabled = False
                            Call LlenaComboItem("TBOPC361", cboTipoCliente) : cboTipoCliente.Items.Add("< Seleccionar >") : cboTipoCliente.SelectedValue = "< Seleccionar >"
                            cboPais.Items.Clear() : cboPais.Items.Add("< Seleccionar >") : cboPais.SelectedValue = "< Seleccionar >"
                            Call LlenaComboItem("TBOPC006", cboPais)
                            If cboPais.Items.Count > 0 Then cboPais.SelectedValue = "51" : cboPais_SelectedIndexChanged(sender, e)
                            If cboDpto.Items.Count > 0 Then cboDpto.SelectedValue = "150000" : cboDpto_SelectedIndexChanged(sender, e)
                            If cboProv.Items.Count > 0 Then cboProv.SelectedValue = "150100" : cboProv_SelectedIndexChanged(sender, e)
                            Call LlenaComboItem("TBOPC528", CboRubro)
                            Call LlenaComboItem("TBOPC527", CboSectorEconomico)
                            txtRazonSocial.Text = Nu(dbRow("TBTICKET_CLIENTE_NOMBRE"))
                            txtRuc.Text = Nu(dbRow("TBTICKET_CLIENTE_CIF"))
                            txtDireccion.Text = Nu(dbRow("TBTICKET_CLIENTE_DIRECCION"))
                            dt = obj.Lista_Contacto_Clientes(Session("Ruta_Emp"), codCliente)
                            If dt.Rows.Count > 0 Then
                                dbRow = dt.Rows(0)
                                txtDniContacto.Text = Nu(dbRow("TBTICKET_CONTACTO_DNI"))
                                txtApepat.Text = Nu(dbRow("TBTICKET_CONTACTO_APEPAT"))
                                txtApemat.Text = Nu(dbRow("TBTICKET_CONTACTO_APEMAT"))
                                txtNombres.Text = Nu(dbRow("TBTICKET_CONTACTO_NOMBRES"))
                            End If
                            FraIngreso.Visible = True
                            lblEtiqueta.Text = "Editar Persona"
                            Session("Nuevo_Reg") = "Cliente"
                            Exit Sub
                        End If
                    End If
                Else
                    btnListar_Click(sender, e)
                End If
            Catch ex As SqlException
                lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
            Catch ex As Exception
                lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New ClsCont_Listados
        Try
            lblError.Text = ""
            Flex.DataSource = obj.Cont_ListaPersonas(Session("CodEmpresa"), Session("Ruta_Emp"))
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        btnListar_Click(sender, e)
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            Try
                Session("Aplicar_Acciones") = "No"
                Dim codCliente As String = Convert.ToString(Request.QueryString("Lpoeh58FJIJS0lk"))
                If codCliente <> "" Then
                    Response.Redirect("~/Contabilidad/Contabilidad_Personas.aspx")
                End If
                Cn.Open() : CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT PERSONA_CERT_INSCR,PERSONA_CODIGO, PERSONA_TIPO,(SELECT ELEMEN_VALOR From BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC001' AND ELEMEN_CODIGO = PERSONA_TIPO) AS PTIPO,PERSONA_TIPO_CLIENTE," _
                                      & " PERSONA_RUC, PERSONA_RAZON_SOCIAL, PERSONA_APEPAT, PERSONA_APEMAT, PERSONA_NOMBRES, PERSONA_DIRECCION, PERSONA_RUBRO, PERSONA_CUII, " _
                                      & " PERSONA_PAIS,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC006' AND ELEMEN_CODIGO = PERSONA_PAIS) AS PPAIS," _
                                      & " PERSONA_DPTO,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC002' AND ELEMEN_CODIGO = PERSONA_DPTO) AS PDPTO," _
                                      & " PERSONA_PROV,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC003' AND ELEMEN_CODIGO = PERSONA_PROV) AS PPROV," _
                                      & " PERSONA_DIST,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) AS PDIST," _
                                      & " PERSONA_EMAIL,PERSONA_EMAIL2,PERSONA_WEB,PERSONA_WEB2,PERSONA_PROVEE, PERSONA_NOMBRE_CONTACTO,PERSONA_TELF1, PERSONA_TELF2, PERSONA_TELF_OF,PERSONA_ANEXO_OF, PERSONA_TELF_CELULAR,PERSONA_FAX1, PERSONA_FAX2, " _
                                      & " PERSONA_CATEGORIA,(SELECT ELEMEN_VALOR From BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC005' AND ELEMEN_CODIGO = PERSONA_CATEGORIA) AS PCATEG " _
                                      & " From TBDATA_PERSONAS WHERE (PERSONA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (PERSONA_CODIGO='" & Flex.Rows(Index).Cells(11).Text.Trim & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        txtCodPersona.Text = Flex.Rows(Index).Cells(11).Text.Trim
                        txtApepat.Text = Nu(Rs!PERSONA_APEPAT)
                        txtApemat.Text = Nu(Rs!PERSONA_APEMAT)
                        txtCertInscrip.Text = Nu(Rs!PERSONA_CERT_INSCR)
                        txtContacto.Text = Nu(Rs!PERSONA_NOMBRE_CONTACTO)
                        txtDireccion.Text = Nu(Rs!PERSONA_DIRECCION)
                        txtCorreo.Text = Nu(Rs!PERSONA_EMAIL)
                        txtNombres.Text = Nu(Rs!PERSONA_NOMBRES)
                        txtRazonSocial.Text = Nu(Rs!PERSONA_RAZON_SOCIAL)
                        txtRuc.Text = Nu(Rs!PERSONA_RUC)
                        txtPagWeb.Text = Nu(Rs!PERSONA_WEB)
                        txtTelef1.Text = Nu(Rs!PERSONA_TELF1)
                        If Nu(Rs!PERSONA_TELF2) <> "" Then txtTelef2.Text = Nu(Rs!PERSONA_TELF2)
                        cboTipoPer.SelectedValue = Nu(Rs!PERSONA_TIPO) : cboTipoPer_SelectedIndexChanged(sender, e)
                        If cboTipoPer.SelectedValue = "1" And Nu(Rs!PERSONA_TIPO_CLIENTE) <> "" Then cboTipoCliente.SelectedValue = Nu(Rs!PERSONA_TIPO_CLIENTE)
                        If Nu(Rs!PERSONA_PAIS) <> "" Then cboPais.SelectedValue = Nu(Rs!PERSONA_PAIS) : cboPais_SelectedIndexChanged(sender, e)
                        If Nu(Rs!PERSONA_DPTO) <> "" Then cboDpto.SelectedValue = Nu(Rs!PERSONA_DPTO) : cboDpto_SelectedIndexChanged(sender, e)
                        If Nu(Rs!PERSONA_PROV) <> "" Then cboProv.SelectedValue = Nu(Rs!PERSONA_PROV) : cboProv_SelectedIndexChanged(sender, e)
                        If Nu(Rs!PERSONA_DIST) <> "" Then cboDist.SelectedValue = Nu(Rs!PERSONA_DIST)
                        Call LlenaComboItem("TBOPC472", CboRubro)
                        Call LlenaComboItem("TBOPC527", CboSectorEconomico)
                        If Nu(Rs!PERSONA_CUII) <> "" Then CboSectorEconomico.SelectedValue = Nu(Rs!PERSONA_CUII)
                        If Nu(Rs!PERSONA_RUBRO) <> "" Then CboRubro.SelectedValue = Nu(Rs!PERSONA_RUBRO)
                    End While
                End If
                Rs.Close() : Cn.Close()
                Session("Nuevo_Reg") = "N"
                FraIngreso.Visible = True
                lblEtiqueta.Text = "Editar Persona"
            Catch ex As SqlException
                lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
            Catch ex As Exception
                lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("Aplicar_Acciones") = "No"
            lblError.Text = ""
            Session("Nuevo_Reg") = "S"
            FraIngreso.Visible = True
            lblEtiqueta.Text = "Nueva Persona"
            cboTipoPer.Items.Clear()
            Call LlenaComboItem("TBOPC001", cboTipoPer) : cboTipoPer.Items.Add("< Seleccionar >") : cboTipoPer.SelectedValue = "< Seleccionar >"
            cboTipoPer_SelectedIndexChanged(sender, e)
            cboTipoCliente.Items.Clear() : cboTipoCliente.Enabled = False
            Call LlenaComboItem("TBOPC361", cboTipoCliente) : cboTipoCliente.Items.Add("< Seleccionar >") : cboTipoCliente.SelectedValue = "< Seleccionar >"
            cboPais.Items.Clear() : cboPais.Items.Add("< Seleccionar >") : cboPais.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC006", cboPais)
            If cboPais.Items.Count > 0 Then cboPais.SelectedValue = "51" : cboPais_SelectedIndexChanged(sender, e)
            If cboDpto.Items.Count > 0 Then cboDpto.SelectedValue = "150000" : cboDpto_SelectedIndexChanged(sender, e)
            If cboProv.Items.Count > 0 Then cboProv.SelectedValue = "150100" : cboProv_SelectedIndexChanged(sender, e)
            Call LlenaComboItem("TBOPC528", CboRubro)
            Call LlenaComboItem("TBOPC527", CboSectorEconomico) 'sector economico
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        FraIngreso.Visible = False
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim St1 As String, St2 As String, St3 As String
        Dim lsPais As String = ""
        Dim dt As DataTable
        Dim obj As New ClsCont_Listados
        Dim obj2 As New ClsCont_InsUpdDel
        Dim obj3 As New Cls_Cliente
        Dim obj4 As New Cls_Relacion_Ticket
        Dim pCodigo As Double = 0
        Dim pTipoCliente As String = ""
        Dim psFormaPago As String = ""
        Dim i As Integer = 0
        lblError.Text = ""
        Dim ValorSys As String = ""
        Dim CmdGlobal As New SqlCommand
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Cn.Open() : CmdGlobal.Connection = Cn

        If Session("Nuevo_Reg") = "Cliente" Or Session("Nuevo_Reg") = "Persona" Then
            Dim codCliente As String = Convert.ToString(Request.QueryString("Lpoeh58FJIJS0lk"))
            Dim categoria As String = ""
            Dim ruc As String = txtRuc.Text.ToString()
            Dim razSocial As String = txtRazonSocial.Text.ToString()
            Dim perCodigo As String = ""
            Dim apePat As String = txtApepat.Text.ToString()
            Dim apeMat As String = txtApemat.Text.ToString()
            Dim nombres As String = txtNombres.Text.ToString()
            Dim nomContacto As String = txtContacto.Text.ToString()
            Dim tipo As String = cboTipoPer.SelectedValue.ToString()
            Dim tipoCli As String = cboTipoCliente.SelectedValue.ToString()
            Dim provee As String = ""
            Dim direccion As String = txtDireccion.Text.ToString()
            Dim pais As String = cboPais.SelectedValue.ToString()
            Dim dpto As String = cboDpto.SelectedValue.ToString()
            Dim prov As String = cboProv.SelectedValue.ToString()
            Dim dist As String = cboDist.SelectedValue.ToString()
            Dim email1 As String = ""
            Dim email2 As String = ""
            Dim web1 As String = txtPagWeb.Text.ToString()
            Dim web2 As String = ""
            Dim telfOf As String = ""
            Dim anexoOf As String = ""
            Dim telfCelular As String = ""
            Dim fax1 As String = ""
            Dim fax2 As String = ""
            Dim sysMod As String = ""
            Dim certInscr As String = txtCertInscrip.Text.ToString()
            Dim pago As String = cboFormaPago.Text.ToString()
            Dim cepro As String = CboSectorEconomico.SelectedValue.ToString()
            Dim accion As String = ""
            Dim respSolucion As String = ""
            Dim codSistema As String = "0"
            Dim diasCredito As String = "0"
            Dim extranjero As String = ""
            Dim padron As String = ""
            Dim referencia As String = "1"
            Dim fechaNac As String = ""
            Dim urbanizacion As String = ""
            Dim comercial As String = txtNomComercial.Text.ToString()
            Dim estSunat As String = txtEstadoContribuyente.Text.ToString()
            Dim rubro As String = CboRubro.SelectedValue.ToString()
            Dim provCod As String = ""
            Dim proviene As String = ""
            Dim cuii As String = ""
            Dim codPersona As String = Convert.ToString(Request.QueryString("WpkDi"))
            Dim telf1 As String = IIf(txtTelef1.Text.Trim = "___-____", "", txtTelef1.Text.Trim).ToString()
            Dim telf2 As String = IIf(txtTelef2.Text.Trim = "___-____", "", txtTelef2.Text.Trim).ToString()
            ValorSys = FechaActual() & HoraActual() & Session("User")
            If ruc.Length() <> 11 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El ruc debe ser de 11 dígitos');", True)
            ElseIf razSocial.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Razón Social');", True)
            ElseIf apePat.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese el Apellido Paterno');", True)
            ElseIf apeMat.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese el Apellido Materno');", True)
            ElseIf nombres.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese el Nombre');", True)
            ElseIf tipo.Equals("< Seleccionar >") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Tipo de Persona');", True)
            Else
                If Session("Nuevo_Reg") = "Cliente" Then
                    obj3.Agregar_Persona_Cliente(Session("Ruta_Emp"), codCliente, categoria, ruc, razSocial, perCodigo, apePat, apeMat, nombres, nomContacto, tipo, tipoCli,
                                                 provee, direccion, pais, dpto, prov, dist, email1, email2, web1, web2, telf1, telf2, telfOf, anexoOf, telfCelular, fax1,
                                                 fax2, sysMod, ValorSys, certInscr, pago, cepro, accion, respSolucion, codSistema, diasCredito, extranjero, padron, referencia,
                                                 fechaNac, urbanizacion, comercial, estSunat, rubro, provCod, proviene, cuii, Session("User"))
                ElseIf Session("Nuevo_Reg") = "Persona" Then
                    obj3.Actualizar_Persona_Cliente(Session("Ruta_Emp"), codCliente, categoria, ruc, razSocial, perCodigo, apePat, apeMat, nombres, nomContacto, tipo, tipoCli,
                                                 provee, direccion, pais, dpto, prov, dist, email1, email2, web1, web2, telf1, telf2, telfOf, anexoOf, telfCelular, fax1,
                                                 fax2, sysMod, ValorSys, certInscr, pago, cepro, accion, respSolucion, codSistema, diasCredito, extranjero, padron, referencia,
                                                 fechaNac, urbanizacion, comercial, estSunat, rubro, provCod, proviene, cuii, Session("User"), codPersona)
                End If
                If Session("Redireccion") = "TICKET" Then
                    obj3.Cambiar_Accion_Cliente(Session("Ruta_Emp"), codCliente, "1", FechaActual().ToString(), HoraActual().ToString(), Session("User"), "1", codCliente)
                    Response.Redirect("~/CRM/CRM_Tiempo_Estado_Ticket.aspx")
                ElseIf Session("Redireccion") = "CLIENTE" Then
                    obj4.Insertar_Acciones_Ticket(Session("Ruta_Emp"), "1", txtNroTicket.Text, FechaActual().ToString(), HoraActual().ToString(), Session("User"), txtNroTicket.Text)
                    Response.Redirect("~/CRM/CRM_Relacion_Ticket.aspx")
                End If
            End If
        Else
            Try
                ValorSys = FechaActual() & HoraActual() & Session("User")
                If RbDni.Checked = True Then
                    If Len(txtRuc.Text) <> 8 Then lblError.Text = lblError.Text & "<br> - Ingresar DNI de 8 digitos."
                Else
                    If Len(txtRuc.Text) <> 11 Then lblError.Text = lblError.Text & "<br> - Ingresar RUC de 11 digitos."
                End If
                'If cboTipoPer.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Tipo de Persona"
                'If cboTipoPer.SelectedValue = "2" Or cboTipoPer.SelectedValue = "1" Then
                '    If CboTipo1.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & "<br> - Seleccionar Tipo "
                'End If
                If cboTipoPer.SelectedValue = "1" And cboTipoCliente.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Tipo de Cliente"
                'If cboTipoPer.SelectedValue = "1" And cboFormaPago.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & "<br> - Seleccionar forma de pago"
                If Rbruc.Checked = True And Len(Trim(txtRazonSocial.Text)) = 0 And CboTipo1.SelectedValue = "2" Then lblError.Text = lblError.Text & "<br> - Ingresar Nombre ó Razón Social."
                'If Rbruc.Checked = True And Len(Trim(txtDniContacto.Text)) = 0 Then lblError.Text = lblError.Text & "<br> - Ingresar el DNI del contacto."
                'If Rbruc.Checked = True And Len(txtDniContacto.Text) <> 8 Then lblError.Text = lblError.Text & "<br> - Ingresar el DNI del contacto de 8 dígitos."
                If Session("Nuevo_Reg") = "N" And RbDni.Checked = True Then
                    If Len(Trim(txtApepat.Text)) = 0 Then lblError.Text = lblError.Text & "<br> - Ingresar Apellido Paterno del contacto."
                    If Len(Trim(txtApemat.Text)) = 0 Then lblError.Text = lblError.Text & "<br> - Ingresar Apellido Materno del contacto."
                    If Len(Trim(txtNombres.Text)) = 0 Then lblError.Text = lblError.Text & "<br> - Ingresar Nombres del contacto."
                End If
                If cboProv.SelectedValue <> "< Seleccionar >" And cboProv.Text <> "" Then St2 = cboProv.SelectedValue Else St2 = ""
                If cboDist.SelectedValue <> "< Seleccionar >" And cboDist.Text <> "" Then St1 = cboDist.SelectedValue Else St1 = ""
                If cboDpto.SelectedValue <> "< Seleccionar >" And cboDpto.Text <> "" And cboDpto.SelectedValue <> "< Seleccionar >" Then St3 = cboDpto.SelectedValue Else St3 = ""
                If cboPais.SelectedValue <> "< Seleccionar >" And cboPais.Text <> "" And cboPais.SelectedValue <> "< Seleccionar >" Then lsPais = cboPais.Text Else lsPais = ""
                'If CboSectorEconomico.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Sector Económico"
                'If CboRubro.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Rubro"

                If lblError.Text.Trim <> "" Then
                    lblError.Text = "Se han encontrado las sgtes. observaciones: " & lblError.Text
                    Exit Sub
                End If
                If cboTipoPer.SelectedValue <> "< Seleccionar >" Then pTipoCliente = cboTipoCliente.SelectedValue.Trim Else pTipoCliente = ""
                If cboFormaPago.SelectedValue <> "(Seleccionar)" Then psFormaPago = cboFormaPago.SelectedValue.Trim Else psFormaPago = ""
                If Len(Trim(txtRazonSocial.Text)) = 0 Then txtRazonSocial.Text = txtApepat.Text.Trim + " " + txtApemat.Text.Trim + " " + txtNombres.Text.Trim
                Dim psSectorEc As String = ""
                If CboSectorEconomico.SelectedValue <> "< Seleccionar >" Then psSectorEc = CboSectorEconomico.SelectedValue.Trim Else psSectorEc = ""
                Dim psRubro As String = ""
                If CboRubro.SelectedValue <> "< Seleccionar >" Then psRubro = CboRubro.SelectedValue.Trim Else psRubro = ""
                Dim psDniContacto As String = ""
                If Rbruc.Checked = True Then psDniContacto = txtDniContacto.Text.Trim
                If RbDni.Checked = True Then psDniContacto = txtRuc.Text.Trim
                Dim psGrabaContacto As String = "N"
                If Session("Nuevo_Reg") = "S" Then psGrabaContacto = "S"
                If Session("Nuevo_Reg") = "S" Then
                    dt = obj.Cont_ExistePersonas(Session("CodEmpresa"), txtRuc.Text.Trim, cboTipoPer.SelectedValue.Trim, "1", Session("Ruta_Emp"))
                    If dt.Rows.Count > 0 Then lblError.Text = "El RUC y el Tipo de Persona que intenta guardar ya se encuentra definido," & Chr(13) & "verifique o cambie el RUC ó elija otro Tipo de Persona." : Exit Sub
                    dt = Nothing
                    dt = obj.Cont_ExistePersonas(Session("CodEmpresa"), txtRuc.Text.Trim, cboTipoPer.SelectedValue.Trim, "1", Session("Ruta_Emp"))
                    If dt.Rows.Count > 0 Then
                        For Each dr As Data.DataRow In dt.Rows
                            For i = 0 To Flex.Rows.Count - 1
                                If UCase(Trim(txtRazonSocial.Text.Trim)) = UCase(dr("PERSONA_RAZON_SOCIAL").ToString) Then
                                    GoTo ABC
                                Else
                                    lblError.Text = "Si intenta agregar a una misma persona con diferentes tipos de persona," & Chr(13) & "aparte del RUC, la razón social debe ser la misma, por favor corregir." : Exit Sub
                                End If
                            Next
                        Next
                        dt = Nothing
                    Else
ABC:
                        dt = Nothing
                        dt = obj.Cont_ExistePersonas(Session("CodEmpresa"), txtRuc.Text.Trim, "", "2", Session("Ruta_Emp"))
                        If dt.Rows.Count > 0 Then
                            GoTo DEF
                        Else
DEF:
                            dt = Nothing
                            obj2.Cont_InsUpd_Personas(0, txtRuc.Text.Trim, txtRazonSocial.Text.Trim, txtApepat.Text.Trim, txtApemat.Text.Trim, txtNombres.Text.Trim, "",
                                                      cboTipoPer.SelectedValue.Trim, pTipoCliente, "", txtDireccion.Text.Trim, lsPais, St3, St2, St1, txtCorreo.Text.Trim, txtPagWeb.Text.Trim, Session("CodEmpresa"),
                                                      "", "", IIf(txtTelef1.Text.Trim = "___-____", "", txtTelef1.Text.Trim), IIf(txtTelef2.Text.Trim = "___-____", "", txtTelef2.Text.Trim), "", "", "", "", "",
                                                      txtCertInscrip.Text.Trim, "", "1", psFormaPago, Session("Ruta_Emp"), psSectorEc, psRubro, psDniContacto, txtContacto.Text.Trim, psGrabaContacto, ValorSys)
                        End If
                    End If
                Else
                    pCodigo = txtCodPersona.Text
                    obj2.Cont_InsUpd_Personas(pCodigo, txtRuc.Text.Trim, txtRazonSocial.Text.Trim, txtApepat.Text.Trim, txtApemat.Text.Trim, txtNombres.Text.Trim, "",
                                              cboTipoPer.SelectedValue.Trim, pTipoCliente, "", txtDireccion.Text.Trim, lsPais, St3, St2, St1, txtCorreo.Text.Trim, txtPagWeb.Text.Trim, Session("CodEmpresa"),
                                              "", "", IIf(txtTelef1.Text.Trim = "___-____", "", txtTelef1.Text.Trim), IIf(txtTelef2.Text.Trim = "___-____", "", txtTelef2.Text.Trim), "", "", "", "", "",
                                              txtCertInscrip.Text.Trim, "", "2", psFormaPago, Session("Ruta_Emp"), psSectorEc, psRubro, psDniContacto, txtContacto.Text.Trim, psGrabaContacto, ValorSys)
                End If
                btnRegresar_Click(sender, e)
                btnListar_Click(sender, e)
            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
            Finally
            End Try
        End If
    End Sub
    Protected Sub cboPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPais.SelectedIndexChanged
        Try
            lblError.Text = ""
            cboDpto.Items.Clear()
            cboProv.Items.Clear()
            cboDist.Items.Clear()
            cboDpto.Enabled = False
            cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
            cboProv.Enabled = False
            cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
            cboDist.Enabled = False
            If cboPais.SelectedValue = "< Seleccionar >" Then Exit Sub
            If cboPais.SelectedIndex = -1 Or cboPais.Items.Count = 0 Then Exit Sub
            If cboPais.SelectedValue = "51" Then
                Call LlenaComboItem("TBOPC002", cboDpto)
                cboDpto.Items.Add("< Seleccionar >") : cboDpto.SelectedValue = "< Seleccionar >"
                cboDpto.Enabled = True
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDpto.SelectedIndexChanged
        cboProv.Items.Clear()
        cboDist.Items.Clear()
        cboProv.Enabled = False
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        cboDist.Enabled = False
        If cboDpto.SelectedIndex = -1 Or cboDpto.Items.Count = 0 Then Exit Sub
        If cboDpto.Items(cboDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", cboProv, Left(cboDpto.SelectedValue, 2), "PR")
        cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
        If cboDpto.SelectedValue <> "< Seleccionar >" Then cboProv.Enabled = True
    End Sub
    Protected Sub cboProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProv.SelectedIndexChanged
        cboDist.Items.Clear()
        cboDist.Enabled = False
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        If cboProv.SelectedIndex = -1 Or cboProv.Items.Count = 0 Then Exit Sub
        If cboProv.Items(cboProv.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", cboDist, Left(cboDpto.SelectedValue, 2) + Mid(cboProv.SelectedValue, 3, 2), "DS")
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        If cboProv.SelectedValue <> "< Seleccionar >" Then cboDist.Enabled = True
    End Sub

    Protected Sub cboTipoPer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoPer.SelectedIndexChanged
        If cboTipoPer.SelectedValue.Trim = "1" Then
            cboTipoCliente.Enabled = True : cboTipoCliente.SelectedValue = "< Seleccionar >"
            CboTipo1.Enabled = True : CboTipo1.SelectedValue = "(Seleccionar)"
            Rbruc.Enabled = True : RbDni.Enabled = True : Rbruc.Checked = True
            cboFormaPago.Enabled = True : cboFormaPago.SelectedValue = "1"
        ElseIf cboTipoPer.SelectedValue.Trim = "2" Then
            cboTipoCliente.Enabled = False : cboTipoCliente.SelectedValue = "< Seleccionar >"
            CboTipo1.Enabled = False : CboTipo1.SelectedValue = "(Seleccionar)"
            Rbruc.Enabled = True : RbDni.Enabled = True : Rbruc.Checked = True
            cboFormaPago.Enabled = True : cboFormaPago.SelectedValue = "(Seleccionar)"
        Else
            cboTipoCliente.Enabled = False : cboTipoCliente.SelectedValue = "< Seleccionar >"
            CboTipo1.Enabled = False : CboTipo1.SelectedValue = "(Seleccionar)"
            Rbruc.Enabled = True : RbDni.Enabled = True : Rbruc.Checked = True
            cboFormaPago.Enabled = False : cboFormaPago.SelectedValue = "(Seleccionar)"
        End If
        txtApepat.Enabled = True
        txtApemat.Enabled = True
        txtNombres.Enabled = True
        Rbruc.Enabled = True
        RbDni.Enabled = True
    End Sub
    Protected Sub CboTipo1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTipo1.SelectedIndexChanged
        If Session("Nuevo_Reg") = "N" Then Exit Sub
        If CboTipo1.SelectedValue = "(Seleccionar)" Then Exit Sub
        If CboTipo1.SelectedValue = 1 Then
            txtRazonSocial.Text = ""
            txtRazonSocial.Enabled = False : txtApepat.Enabled = True : txtApemat.Enabled = True : txtNombres.Enabled = True
        Else
            txtRazonSocial.Enabled = True : txtApepat.Enabled = False : txtApemat.Enabled = False : txtNombres.Enabled = False
            txtApepat.Text = "" : txtApemat.Text = "" : txtNombres.Text = ""
        End If
    End Sub

    'Private Sub BtnSunat_Click(sender As Object, e As EventArgs) Handles BtnSunat.Click
    '    'Extraer_Datos_Sunat(txtRuc.Text)

    '    'Datos_Sunat()
    'End Sub
    Private Sub BtnSunat_Click(sender As Object, e As EventArgs) Handles BtnSunat.Click
        Dim ruc As String = txtRuc.Text ' Asume un TextBox para ingresar el RUC
        'Dim servicio As New SunatService()

        Try
            'Dim htmlRespuesta As String = Await servicio.ConsultarRucPublicoAsync(ruc)
            'Dim datos As String = servicio.ExtraerDatos(htmlRespuesta)
            'Dim datos1 As String = ExtraerDatos(htmlRespuesta)
            'txtDireccion.Text = datos1
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Datos: " & datos1 & "')", True)
            Datos_Sunat()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        End Try
    End Sub

    Public Function ExtraerDatos(html As String) As String
        'Dim doc As New HtmlAgilityPack.HtmlDocument()
        'doc.LoadHtml(html)


        '' Buscar nodos de los datos
        'Dim razonSocialNode = doc.DocumentNode.SelectSingleNode("//td[contains(text(), 'Nombre o Razón Social')]/following-sibling::td")
        'Dim direccionNode = doc.DocumentNode.SelectSingleNode("//td[contains(text(), 'Dirección')]/following-sibling::td")

        '' Verificación explícita para evitar nulos
        'Dim razonSocial As String = "No disponible"
        'If razonSocialNode IsNot Nothing Then
        '    razonSocial = razonSocialNode.InnerText.Trim()
        'End If

        'Dim direccion As String = "No disponible"
        'If direccionNode IsNot Nothing Then
        '    direccion = direccionNode.InnerText.Trim()
        'End If


        'Return "Razón Social: {razonSocial}{vbNewLine}Dirección: {direccion}"
    End Function

    Private Sub RbDni_CheckedChanged(sender As Object, e As EventArgs) Handles RbDni.CheckedChanged
        If RbDni.Checked = True Then
            txtApepat.Enabled = True
            txtApemat.Enabled = True
            txtNombres.Enabled = True
            txtRazonSocial.Enabled = True
            txtRuc.MaxLength = 8
            CboTipo1.SelectedValue = "1"
            txtDniContacto.Enabled = False
        End If
    End Sub

    Private Sub Rbruc_CheckedChanged(sender As Object, e As EventArgs) Handles Rbruc.CheckedChanged
        If Rbruc.Checked = True Then
            txtApepat.Enabled = True
            txtApemat.Enabled = True
            txtNombres.Enabled = True
            txtRazonSocial.Enabled = True
            txtRuc.MaxLength = 11
            CboTipo1.SelectedValue = "2"
            txtDniContacto.Enabled = True
        End If
    End Sub

End Class
