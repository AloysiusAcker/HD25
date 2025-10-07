Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Documentacion_Documento_TemasAyuda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
            Dim valor2 As String = Request.QueryString("param2")
            Session("TicketNro") = NroTicket
            Session("NombrePag") = valor2
            Ocultar_Visible(False)
            Llenar_Combos()
            If Session("TicketNro") <> "" Then
                BtnNuevoTema_Click(sender, e)
            End If
        End If
    End Sub
    Protected Sub Listar_Documentos()
        Dim obj As New Cls_Documentos
        Dim dt As New DataTable
        Dim objTema As New clsInspeccion_Listado
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Documentos(psconexion, Session("User"), 0)
        GvListaArticulos.DataSource = dt
        GvListaArticulos.DataBind()
        'Dim fila As GridViewRow
        'Dim pCodigo As Double = 0
        'For i = 0 To GvListaArticulos.Rows.Count - 1
        '    pCodigo = GvListaArticulos.Rows(i).Cells(9).Text.Trim
        '    dt = objTema.Listar_TemaAyuda(Session("Ruta_Emp"), pCodigo)
        '    If dt.Rows.Count > 0 Then
        '        For Each drMenuItem As Data.DataRow In dt.Rows
        '            fila = GvListaArticulos.Rows(i)
        '            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
        '            lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "</A>"
        '        Next
        '    End If
        '    dt = Nothing
        'Next
    End Sub
    Protected Sub Llenar_Combos()
        Dim obj As New Cls_Documentos
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Aplicacion(psconexion)
        DdlAplicacion.DataSource = dt
        DdlAplicacion.DataValueField = "TA_APLICACION_CODIGO"
        DdlAplicacion.DataTextField = "TA_APLICACION_DESCRIPCION"
        DdlAplicacion.DataBind()
        DdlAplicacion.Items.Add("< Seleccionar >")
        DdlAplicacion.SelectedValue = "< Seleccionar >"
        DdlTipoIngreso.Items.Add("< Seleccionar >")
        DdlTipoIngreso.SelectedValue = "< Seleccionar >"
        Call LLenaComboItemTabEsp(DdlClasificacion1, "", "", "TBESP_TEM1", "TBESP_TEM2", "TBESP_TEM3", 1, "0001", Session("Ruta_emp"))

        Call LlenaComboItem("tbopc382", DdlNivelAcceso)

    End Sub

    Protected Sub BtnListarDocumentos_Click(sender As Object, e As EventArgs) Handles BtnListarDocumentos.Click
        Listar_Documentos()
        Ocultar_Visible(False)
    End Sub

    'Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
    '    Dim obj As New Cls_Documentos
    '    Dim psconexion As String = Session("Ruta_Emp")
    '    Dim codDoc As String = txtCodDoc.Text
    '    Dim aplicacion As String = DdlAplicacion.SelectedValue
    '    Dim tipoIngreso As String = DdlTipoIngreso.SelectedValue
    '    Dim clasif1 As String = DdlClasificacion1.SelectedValue
    '    Dim clasif2 As String = DdlClasificacion2.SelectedValue
    '    Dim clasif3 As String = DdlClasificacion3.SelectedValue
    '    Dim nivelAcceso As String = DdlNivelAcceso.SelectedValue
    '    Dim nomDoc As String = FileUpload1.FileName.ToString
    '    Dim ticket As String = txtTicket.Text
    '    Dim descripcion As String = txtDescricion.InnerText.ToString
    '    Dim Fecha As String = TxtFecha.Value.ToString



    '    If tipoIngreso = "< Seleccionar >" Then tipoIngreso = ""
    '    If clasif3 = "< Seleccionar >" Then clasif3 = ""
    '    If clasif2 = "< Seleccionar >" Then clasif2 = ""

    '    If aplicacion.Equals("< Seleccionar >") Then
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Aplicacion');", True)
    '    ElseIf Fecha = "" Then
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Fecha');", True)
    '    ElseIf clasif1.Equals("< Seleccionar >") Then
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Clasificacion');", True)
    '    ElseIf nivelAcceso.Equals("< Seleccionar >") Then
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Nivel de Acceso');", True)
    '    ElseIf descripcion.Equals("") Then
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar una descripción');", True)
    '    ElseIf nomDoc.Equals("") Then
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Archivo');", True)
    '    Else
    '        Dim anio As String = Fecha.Substring(0, 4)
    '        Dim mes As String = Fecha.Substring(5, 2)
    '        Dim dia As String = Fecha.Substring(8, 2)
    '        Fecha = anio + mes + dia

    '        If btnGuardar.Text = "Actualizar" Then
    '            obj.ActualizaDocumentos(psconexion, codDoc, aplicacion, tipoIngreso, Fecha,
    '                               clasif1, clasif2, clasif3,
    '                                DdlNivelAcceso.SelectedValue, nomDoc, ticket, descripcion)
    '            Ocultar_Visible(False)
    '            Listar_Documentos()
    '        ElseIf btnGuardar.Text = "Guardar" Then
    '            obj.Registrar_Documentos(psconexion, clasif1, clasif2,
    '                                        clasif3, nomDoc, txtDescricion.InnerText,
    '                                         Session("User"), Fecha, FechaActual() & HoraActual() & Session("User"),
    '                                         DdlTipoIngreso.SelectedValue, DdlNivelAcceso.SelectedValue, DdlAplicacion.SelectedValue)
    '            Ocultar_Visible(False)
    '            Listar_Documentos()
    '        End If
    '    End If

    'End Sub

    Protected Sub Ocultar_Visible(ByVal vf As Boolean)
        lblAplicacion.Visible = vf
        lblCodigoDocumento.Visible = vf
        lblTipoIngreso.Visible = vf
        lblFecha.Visible = vf
        Label1.Visible = vf
        Label2.Visible = vf
        lblClasificacion.Visible = vf
        lvlNivelAcceso.Visible = vf
        lvlCargarDodumento.Visible = vf
        ' lblRuta.Visible = vf
        lblDescripcion.Visible = vf
        ' FileNombre.Visible = vf
        FileUpload1.Visible = vf

        txtCodDoc.Visible = vf
        DdlAplicacion.Visible = vf
        DdlTipoIngreso.Visible = vf
        TxtFecha.Visible = vf
        DdlClasificacion1.Visible = vf
        DdlClasificacion2.Visible = vf
        DdlClasificacion3.Visible = vf
        DdlNivelAcceso.Visible = vf
        'txtNombreDocumento.Visible = vf
        RbTicket.Visible = vf
        txtTicket.Visible = vf
        'txtRuta.Visible = vf
        txtDescricion.Visible = vf
        btnGuardar.Visible = vf
        btnCancelar.Visible = vf
        btnAbrirBandeja.Visible = vf
        btnSMS.Visible = vf
    End Sub

    Private Sub BtnNuevoTema_Click(sender As Object, e As EventArgs) Handles BtnNuevoTema.Click
        Dim obj As New Cls_Documentos
        Dim cn As String = Session("Ruta_Emp")
        Session("Clasificacion") = ""
        txtCodDoc.Text = obj.Codigo(cn)
        Ocultar_Visible(True)
        btnGuardar.Text = "Guardar"
        Limpiar_Cajas1()

        TxtFecha.Value = DateTime.Now.ToString("yyyy-MM-dd")
        If Session("TicketNro") <> "" Then
            RbTicket.Checked = True
            RbTicket_CheckedChanged(sender, e)
            txtTicket.Text = Session("TicketNro")
        End If
    End Sub

    Private Sub DdlAplicacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAplicacion.SelectedIndexChanged
        Dim obj As New Cls_Documentos
        Dim dt As New DataTable
        Dim psAplicacion As Integer = 0
        If DdlAplicacion.SelectedValue <> "< Seleccionar >" Then
            psAplicacion = DdlAplicacion.SelectedValue
        End If
        Dim psconexion As String = Session("Ruta_Emp")
        DdlTipoIngreso.Items.Clear()
        dt = obj.Lista_Tipo_Ingreso(psconexion, DdlAplicacion.SelectedValue)
        DdlTipoIngreso.DataSource = dt
        DdlTipoIngreso.DataValueField = "TA_INGRESO_CODIGO"
        DdlTipoIngreso.DataTextField = "TA_INGRESO_DESCRIPCION"
        DdlTipoIngreso.DataBind()
        DdlTipoIngreso.Items.Add("< Seleccionar >")
        DdlTipoIngreso.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub DdlClasificacion1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlClasificacion1.SelectedIndexChanged
        Call LLenaComboItemTabEsp(DdlClasificacion2, DdlClasificacion1.SelectedValue, "", "TBESP_TEM1", "TBESP_TEM2", "TBESP_TEM3", 2, "0001", Session("Ruta_emp"))
        If DdlClasificacion1.SelectedValue <> "" Then
            DdlClasificacion3.SelectedValue = "< Seleccionar >"
        End If
    End Sub

    Private Sub DdlClasificacion2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlClasificacion2.SelectedIndexChanged
        Call LLenaComboItemTabEsp(DdlClasificacion3,
                                  DdlClasificacion1.SelectedValue,
                                  DdlClasificacion2.SelectedValue,
                                  "TBESP_TEM1", "TBESP_TEM2", "TBESP_TEM3", 3, "0001", Session("Ruta_emp"))
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        If Session("NombrePag") = "CRM/CRM_Relacion_Ticket.aspx.aspx" Then
            Session("TicketNro") = ""
            Session("NombrePag") = ""
            Response.Redirect("~/CRM/CRM_Relacion_Ticket.aspx.aspx")
        Else
            Ocultar_Visible(False)
            Limpiar_Cajas()
        End If
    End Sub
    Protected Sub Limpiar_Cajas()
        txtCodDoc.Text = ""
        DdlAplicacion.SelectedValue = "< Seleccionar >"
        DdlTipoIngreso.SelectedValue = "< Seleccionar >"
        DdlClasificacion1.SelectedValue = "< Seleccionar >"
        DdlClasificacion2.SelectedValue = "< Seleccionar >"
        DdlClasificacion3.SelectedValue = "< Seleccionar >"
        DdlNivelAcceso.SelectedValue = "< Seleccionar >"
        '  txtNombreDocumento.Text = ""
        'txtRuta.Text = ""
        txtDescricion.InnerText = ""
        txtTicket.Text = ""
    End Sub
    Protected Sub Limpiar_Cajas1()

        DdlAplicacion.SelectedValue = "< Seleccionar >"
        DdlTipoIngreso.SelectedValue = "< Seleccionar >"
        DdlClasificacion1.SelectedValue = "< Seleccionar >"
        DdlClasificacion2.SelectedValue = "< Seleccionar >"
        DdlClasificacion3.SelectedValue = "< Seleccionar >"
        DdlNivelAcceso.SelectedValue = "< Seleccionar >"
        ' txtNombreDocumento.Text = ""
        'txtRuta.Text = ""
        txtDescricion.InnerText = ""

    End Sub
    Protected Function VerDatos(ByVal codigo As String) As DataTable
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As New SqlCommand
        cmd.CommandText = "select " &
                        " TEMA_AYUDA_APLICACION," &
                        " TEMA_AYUDA_TIPOINGRESO," &
                        " SUBSTRING(TEMA_AYUDA_FECHA_INGRESO,1,4)+'-'+SUBSTRING(TEMA_AYUDA_FECHA_INGRESO,5,2)+'-'+SUBSTRING(TEMA_AYUDA_FECHA_INGRESO,7,2)," &
                        " TEMA_AYUDA_CLASIFN1," &
                        " TEMA_AYUDA_CLASIFN2," &
                        " TEMA_AYUDA_CLASIFN3," &
                        " TEMA_AYUDA_NIVEL_ACCESO," &
                        " TEMA_AYUDA_NOMBRE_DOC," &
                        " TEMA_AYUDA_REFERENCIA," &
                        " TEMA_AYUDA_DESCRIPCION" &
                        " from TBTEMA_AYUDA_GENERAL where TEMA_AYUDA_CODIGO=" + codigo
        cmd.CommandType = CommandType.Text
        cmd.Connection = cn
        cn.Open()
        Dim imagen As New DataTable
        imagen.Load(cmd.ExecuteReader())
        cn.Close()

        Return imagen
    End Function

    Protected Sub GvListaArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Documentos
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable
        Dim nomImg As String = FileUpload1.FileName.ToString
        If e.CommandName = "Editar" Then
            Ocultar_Visible(True)
            txtCodDoc.Text = GvListaArticulos.Rows(Index).Cells(9).Text
            'txtNombreDocumento.Text = GvListaArticulos.Rows(Index).Cells(6).Text
            txtDescricion.InnerText = GvListaArticulos.Rows(Index).Cells(7).Text
            dt = VerDatos(txtCodDoc.Text)
            Dim datos As DataRow = dt.Rows(0)
            If Nu(datos(0)) <> "" Then DdlAplicacion.SelectedValue = datos(0)
            DdlAplicacion_SelectedIndexChanged(sender, e)
            If Nu(datos(1)) <> "" Then DdlTipoIngreso.SelectedValue = datos(1)
            If Nu(datos(2)) <> "" Then TxtFecha.Value = datos(2)
            If Nu(datos(3)) <> "" Then DdlClasificacion1.SelectedValue = datos(3)
            DdlClasificacion1_SelectedIndexChanged(sender, e)
            If Nu(datos(4)) <> "" Then DdlClasificacion2.SelectedValue = datos(4)
            DdlClasificacion2_SelectedIndexChanged(sender, e)
            If Nu(datos(5)) <> "" Then DdlClasificacion3.SelectedValue = Nu(datos(5))
            If Nu(datos(6)) <> "" Then DdlNivelAcceso.SelectedValue = datos(6)
            If Nu(datos(8)) <> "" Then txtTicket.Text = datos(8)
            btnGuardar.Text = "Actualizar"

        ElseIf e.CommandName = "Eliminar" Then
            dt = obj.Eliminar_Documentos(cn, GvListaArticulos.Rows(Index).Cells(9).Text)
            Dim dbRow As DataRow = dt.Rows(0)
            If dbRow(0) = "1" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar el documento');", True)
            Else
                Listar_Documentos()
                Ocultar_Visible(False)
                Limpiar_Cajas()
            End If
        ElseIf e.CommandName = "VerArchivo" Then
            ' Obtener el nombre del archivo desde la fila seleccionada
            Dim nombreArchivo As String = GvListaArticulos.Rows(Index).Cells(6).Text
            Try
                ' Aquí puedes abrir el archivo como lo desees, por ejemplo:
                ' Puedes redirigir a una página que muestre el archivo, o descargarlo, etc.
                ' Por ejemplo, si deseas abrir un archivo PDF en una nueva ventana:
                Response.ContentType = "application/pdf"
                Response.AppendHeader("Content-Disposition", "inline; filename=" & nombreArchivo)
                Response.TransmitFile(Server.MapPath("~/Documentacion/Temas/" & nombreArchivo))
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            Catch ex As Exception
                ' Manejar errores aquí
                Response.Write("Error al abrir el archivo: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub BtnFiltro_Click(sender As Object, e As EventArgs) Handles BtnFiltro.Click
        'Process.Start("G:\WebGestorInv\WebGestorInv\ArchivoDocumento\cuentas octubre.xlsx")
    End Sub

    Private Sub RbTicket_CheckedChanged(sender As Object, e As EventArgs) Handles RbTicket.CheckedChanged
        If RbTicket.Checked = True Then
            txtTicket.Text = ""
            txtTicket.Enabled = True
        End If
    End Sub

End Class
