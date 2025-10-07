Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient

Partial Class Inventario_Inventario_Guia_Transportista
    Inherits System.Web.UI.Page

    Dim obj As New clsInv_Listados

    Public Property vImgPrev As String
        Get
            Return Session("vImgPrev").ToString().Trim()
        End Get
        Set(ByVal value As String)
            Session("vImgPrev") = value
        End Set
    End Property
    Public Property Lista_PersonaBE As List(Of PersonaBE)
        Get
            If Session("Lista_PersonaBE") IsNot Nothing Then
                Return CType(Session("Lista_PersonaBE"), List(Of PersonaBE))
            Else
                Session("Lista_PersonaBE") = New List(Of PersonaBE)()
                Return CType(Session("Lista_PersonaBE"), List(Of PersonaBE))
            End If
        End Get
        Set(ByVal value As List(Of PersonaBE))
            Session("Lista_PersonaBE") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            lblRegistroGuia.Text = ""

            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim fInv As New clsInv_Procesos
        Dim psArticulo As String = ""
        Dim pdCodUbica As Double = 0
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim psCodArticulo As String = ""
        Dim pdSaldo As Double = 0
        Dim ListaArt As String = ""
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Text = ""
        Dim psCodGuia As Double = 0
        Try
            dt = obj.Lista_GuiaTransportista_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
            Flexd.DataSource = dt
            Flexd.DataBind()
            If dt.Rows.Count > 0 Then
                lblRegistroGuia.Text = dt.Rows.Count & " Guías."
            Else
                lblRegistroGuia.Text = "No hay Guías"
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub Flexd_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flexd.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim i As Long = 0
        Dim psIngresar As String = "S"
        Dim dt As New DataTable
        Dim psCodGuia As Double = 0
        Dim pCodArchivo As Double = 0
        Dim Fila As GridViewRow
        Me.Page.Session.Timeout = 1080
        Try ' 
            If e.CommandName = "Cambiar" Then
                txtFecha.Text = FormatoFecha(FechaActual)
                TxtNroGuiaT.Text = Flexd.Rows(Index).Cells(3).Text
                TxtNroGuia.Text = Flexd.Rows(Index).Cells(7).Text
                TxtEstadoActual.Text = Flexd.Rows(Index).Cells(12).Text
                Call LlenaComboItem("TBOPC542", DdlEstado)
                DdlEstado.SelectedValue = Flexd.Rows(Index).Cells(13).Text
                txtCodGuiaT.Text = Flexd.Rows(Index).Cells(2).Text
                txtCodGuia.Text = Flexd.Rows(Index).Cells(6).Text
                vImgPrev = Nothing
                psCodGuia = txtCodGuia.Text
                dt = obj.ListaArchivos_xGuiaRemision(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
                hndQR.Value = txtCodGuia.Text
                divEstado.Visible = True
                divArchivo.Visible = True
                FlexAr.DataSource = Nothing
                FlexAr.DataBind()
                psCodGuia = Flexd.Rows(Index).Cells(6).Text
                dt = obj.ListaArchivos_xGuiaRemision(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
                FlexAr.DataSource = dt
                FlexAr.DataBind()
                dt = Nothing
                For i = 0 To FlexAr.Rows.Count - 1
                    pCodArchivo = FlexAr.Rows(i).Cells(4).Text.Trim
                    dt = obj.ListaDatos_xArchivos(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia, pCodArchivo)
                    If dt.Rows.Count > 0 Then
                        For Each drMenuItem As Data.DataRow In dt.Rows
                            Fila = FlexAr.Rows(i)
                            'FlexTA.Rows(i).Cells(11).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
                            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                            lbl.InnerHtml = "</b><A href='GUIAS/" & Nu(drMenuItem("GUIREMT_ARCHIVO_NOMBRE")) & "'TARGET='_blank'>" & Nu(drMenuItem("GUIREMT_ARCHIVO_NOMBRE")) & "</A>"
                        Next
                    End If
                    dt = Nothing
                Next
            ElseIf e.CommandName = "Archivos" Then
                divArchivo.Visible = True
                FlexAr.DataSource = Nothing
                FlexAr.DataBind()
                psCodGuia = Flexd.Rows(Index).Cells(6).Text
                dt = obj.ListaArchivos_xGuiaRemision(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
                FlexAr.DataSource = dt
                FlexAr.DataBind()
                dt = Nothing
                For i = 0 To FlexAr.Rows.Count - 1
                    pCodArchivo = FlexAr.Rows(i).Cells(4).Text.Trim
                    dt = obj.ListaDatos_xArchivos(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia, pCodArchivo)
                    If dt.Rows.Count > 0 Then
                        For Each drMenuItem As Data.DataRow In dt.Rows
                            Fila = FlexAr.Rows(i)
                            'FlexTA.Rows(i).Cells(11).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
                            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                            lbl.InnerHtml = "</b><A href='GUIAS/" & Nu(drMenuItem("GUIREMT_ARCHIVO_NOMBRE")) & "'TARGET='_blank'>" & Nu(drMenuItem("GUIREMT_ARCHIVO_NOMBRE")) & "</A>"
                        Next
                    End If
                    dt = Nothing
                Next
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub OrdenarItems()
        Dim lst = New List(Of PersonaBE)
        lst.AddRange(Lista_PersonaBE.OrderByDescending(Function(n) n.FECHA_CREACION))
        Lista_PersonaBE = lst
    End Sub

    Private Sub Carga_repFotos()
        'repFotos.DataSource = Lista_PersonaBE
        'repFotos.DataBind()
    End Sub
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim strScript As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Try
            If Not vImgPrev Is Nothing Then
                Dim vArchivo As String = vImgPrev
                Dim ms = New IO.MemoryStream(IO.File.ReadAllBytes(vArchivo))
                Dim tmp = ms.ToArray()
                Dim vDNi = IO.Path.GetFileName(vArchivo)
                Dim vCreacion = IO.File.GetCreationTime(vArchivo)
                Dim item As New ListItem()
                item.Value = vDNi
                item.Text = vDNi
                Dim pCodarchivo As String = ""
                Dim Rs As SqlDataReader
                Cn.Open() : CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(archivo_codigo) FROM TBINV_GUIA_REMISION_TRANSPORTE_ARCHIVO"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pCodarchivo = Nz(Rs(0)) + 1
                    End While
                Else
                    pCodarchivo = 1
                End If
                Rs.Close()

                CmdGlobal.CommandText = " INSERT TBINV_GUIA_REMISION_TRANSPORTE_ARCHIVO (archivo_codigo,EMPRESA_CODIGO,GUIREM_CODIGO, GUIREMT_CODIGO,GUIREMT_ARCHIVO_NOMBRE, GUIREMT_ARCHIVO_FECHA)" _
                                      & " VALUES ( " & pCodarchivo & ",'" & Session("CodEmpresa") & "', " & Nz(txtCodGuia.Text) & " , " & Nz(txtCodGuiaT.Text) & " , '" & vDNi & "', '" & vCreacion & "') "
                CmdGlobal.ExecuteNonQuery()
                Cn.Close()
                vImgPrev = Nothing
                divArchivo.Visible = True
                Dim pCodArchivo1 As Double = 0
                Dim psCodGuia As Double = 0
                Dim Fila As GridViewRow
                Dim dt As New DataTable
                FlexAr.DataSource = Nothing
                FlexAr.DataBind()
                psCodGuia = Nz(txtCodGuia.Text)
                dt = obj.ListaArchivos_xGuiaRemision(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
                FlexAr.DataSource = dt
                FlexAr.DataBind()
                dt = Nothing
                For i = 0 To FlexAr.Rows.Count - 1
                    pCodarchivo = FlexAr.Rows(i).Cells(4).Text.Trim
                    dt = obj.ListaDatos_xArchivos(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia, pCodarchivo)
                    If dt.Rows.Count > 0 Then
                        For Each drMenuItem As Data.DataRow In dt.Rows
                            Fila = FlexAr.Rows(i)
                            'FlexTA.Rows(i).Cells(11).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
                            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                            lbl.InnerHtml = "</b><A href='GUIAS/" & Nu(drMenuItem("GUIREMT_ARCHIVO_NOMBRE")) & "'TARGET='_blank'>" & Nu(drMenuItem("GUIREMT_ARCHIVO_NOMBRE")) & "</A>"
                        Next
                    End If
                    dt = Nothing
                Next
                'Response.Redirect("Inventario/Inventario_Guia_Transportista.aspx")
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Protected Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        divEstado.Visible = False
        divArchivo.Visible = False
    End Sub
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        If TxtEstadoActual.Text = DdlEstado.Text Then lblError.Text = "Debe de seleccionar un estado diferente al actual." : Exit Sub
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim pdPeso As String = "NULL"
        Dim psFechaCampo As String = ""
        Dim psFecha As String = ""
        psFecha = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
        If IsNumeric(txtPeso.Text) Then pdPeso = txtPeso.Text
        If DdlEstado.SelectedValue = "1" Then psFechaCampo = ", GUIREMTD_FECHA_RECEPCION = '" & psFecha & "' "
        If DdlEstado.SelectedValue = "3" Then psFechaCampo = ", GUIREMTD_FECHA_ENTREGADO = '" & psFecha & "' "
        If DdlEstado.SelectedValue = "2" Then psFechaCampo = ", GUIREMTD_FECHA_ENVIO = '" & psFecha & "' "
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " UPDATE TBINV_GUIA_REMISON_TRANSPORTE_DETALLE " _
                              & " SET GUIREMTD_ESTADO = '" & DdlEstado.SelectedValue & "', " _
                              & " GUIREMTD_PESO = " & pdPeso & " " & psFechaCampo & "  " _
                              & " WHERE GUIREMT_CODIGO = " & Nz(txtCodGuiaT.Text) & " AND GUIREM_CODIGO = " & Nz(txtCodGuia.Text)
        CmdGlobal.ExecuteNonQuery()
        Cn.Close()
        divEstado.Visible = False
        btnListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub BtnLeerGmail_Click(sender As Object, e As EventArgs) Handles BtnLeerGmail.Click
        'Dim pop3Client As Pop3Client
        'pop3Client = New Pop3Client
        'pop3Client.Connect("pop.gmail.com", "995", True)
        'pop3Client.Authenticate("tuCorreo", "TuClave")
        'Dim count As Integer = pop3Client.GetMessageCount
        'Dim Migrid As DataTable = New DataTable
        'Migrid.Columns.Add("Número")
        'Migrid.Columns.Add("Enviado Por")
        'Migrid.Columns.Add("Motivo")
        'Migrid.Columns.Add("Fecha")
        'Dim counter As Integer = 0
        'Dim i As Integer = count
        'Do While (i >= 1)
        '    Dim message As Message = pop3Client.GetMessage(i)
        '    Migrid.Rows.Add()
        '    Migrid.Rows((Migrid.Rows.Count - 1))("Número") = i
        '    Migrid.Rows((Migrid.Rows.Count - 1))("Enviado Por") = message.Headers.From.Address
        '    Migrid.Rows((Migrid.Rows.Count - 1))("Motivo") = message.Headers.Subject
        '    Migrid.Rows((Migrid.Rows.Count - 1))("Fecha") = message.Headers.DateSent
        '    counter = counter + 1
        '    i = i - 1
        '    If counter = 20 Then
        '        Exit Do
        '    End If
        'Loop
        'grid.DataSource = Migrid
    End Sub
End Class
