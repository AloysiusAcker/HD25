Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            Dim obj As New Listados
            Dim dt As New Data.DataTable
            lblTitulo.InnerText = Session("MenuNom")
            Title = Session("MenuNom")
            lblMensaje.Visible = False
            lblCat.Visible = False : cboCat.Visible = False
            lblItem.Visible = False : cboElemento.Visible = False
            If User.Identity.Name = "" Then
                btnNuevo.Visible = False
            Else
                dt = obj.Autoriza_IngElemento(User.Identity.Name, Session("MenuCod"))
                If dt.Rows.Count > 0 Then btnNuevo.Visible = True
                dt = Nothing
            End If
            Dim Rs As SqlDataReader
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim cmdSql As New SqlCommand
            Try
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "SELECT * FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') AND (CAMPO_NOMBRE='ELEMENTO_CATEGORIA')"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    lblCat.Visible = True : cboCat.Visible = True
                    lblItem.Visible = True : lblItem.Text = "&nbsp;" & Session("MenuNom") & " : &nbsp;" : cboElemento.Visible = True
                End If
                Rs.Close()
                cboCat.Items.Clear()
                If cboCat.Visible = True Then
                    cmdSql.CommandText = " SELECT CATEG_CODIGO, CATEG_NOMBRE From TBMENU_ITEMS_CATEGORIA" _
                                       & " WHERE (GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & ") AND (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (ITEM_CODIGO =" & Session("MenuCod") & ") AND (CATEG_SYS_EST = '0') ORDER BY CATEG_NOMBRE"
                    cboCat.DataSource = cmdSql.ExecuteReader
                    cboCat.DataTextField = "CATEG_NOMBRE"
                    cboCat.DataValueField = "CATEG_CODIGO"
                    cboCat.DataBind()
                End If
                Dim Item As New ListItem
                Item.Text = "[Todas las Categorías]"
                Item.Value = "0"
                cboCat.Items.Add(Item)
                If cboCat.Items.Count = 1 Then
                    cboCat.SelectedIndex = 0
                Else
                    Dim i As Integer
                    For i = 0 To cboCat.Items.Count
                        If cboCat.Items(i).Value = 0 Then
                            cboCat.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
                Call cboCat_SelectedIndexChanged(sender, e)
            Catch Ex As SqlException
                'lblMensaje.Visible = True
                'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
            Catch Ex As Exception
                'lblMensaje.Visible = True
                'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
            Finally
                Cn.Close()
            End Try
        End If
    End Sub
    Private Function Existe_Campo(ByVal NTabla As String, ByVal nCampo As String) As Boolean
        Dim RE As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Cn.Open()
        cmdSql.Connection = Cn
        Existe_Campo = False
        cmdSql.CommandText = "SELECT * FROM INFORMATION_SCHEMA.Columns WHERE (TABLE_NAME = N'" & NTabla & "') AND (COLUMN_NAME = '" & nCampo & "')"
        RE = cmdSql.ExecuteReader
        If RE.HasRows Then Existe_Campo = True
        Cn.Close()
    End Function
    Private Sub cboCat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCat.SelectedIndexChanged
        cboElemento.Items.Clear()
        If cboElemento.Visible = True Then
            Dim Itemx As New ListItem
            Itemx.Text = "[Todos]"
            Itemx.Value = "0"
            cboElemento.Items.Add(Itemx)
            Call Carga_ComboElementos()
            If cboElemento.Items.Count = 1 Then
                cboElemento.SelectedIndex = 0
            Else
                Dim i As Integer
                For i = 0 To cboElemento.Items.Count
                    If cboElemento.Items(i).Value = 0 Then
                        cboElemento.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        End If
        Call Carga_Grilla(True, "")
    End Sub
    Private Sub Carga_ComboElementos()
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql As New SqlCommand
        Dim FechaServer As String = FechaActual()
        Dim UsarFechaIni As Boolean = False
        Dim UsarFechaFin As Boolean = False
        Dim Entrar As Boolean = False

        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT * FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') AND (CAMPO_NOMBRE='ELEMENTO_FECHA2')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then UsarFechaIni = True
            Rs.Close()
            cmdSql.CommandText = "SELECT * FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') AND (CAMPO_NOMBRE='ELEMENTO_FECHA3')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then UsarFechaFin = True
            Rs.Close()
            cmdSql.CommandText = "SELECT * " _
                               & " FROM TBMENU_ITEMS_ELEMENTOS T WHERE GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND ITEM_CODIGO=" & Session("MenuCod") & " AND ELEMENTO_SYS_EST='0'"
            If cboCat.SelectedValue <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (ELEMENTO_CATEGORIA=" & cboCat.SelectedValue & ")"
            cmdSql.CommandText = cmdSql.CommandText & " ORDER BY ELEMENTO_NOMBRE"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then 'obtener contador de rptas para la matriz
                While Rs.Read
                    Entrar = False
                    If UsarFechaIni = False And UsarFechaFin = False Then
                        Entrar = True
                    ElseIf UsarFechaIni = True And UsarFechaFin = False Then
                        If Nu(Rs!ELEMENTO_FECHA2) = "" Or (Nu(Rs!ELEMENTO_FECHA2) <= FechaServer) Then Entrar = True
                    ElseIf UsarFechaIni = False And UsarFechaFin = True Then
                        If Nu(Rs!ELEMENTO_FECHA3) = "" Or (Nu(Rs!ELEMENTO_FECHA3) >= FechaServer) Then Entrar = True
                    ElseIf UsarFechaIni = True And UsarFechaFin = True Then
                        If Nu(Rs!ELEMENTO_FECHA2) = "" And Nu(Rs!ELEMENTO_FECHA3) = "" Then
                            Entrar = True
                        ElseIf Nu(Rs!ELEMENTO_FECHA2) <> "" And Nu(Rs!ELEMENTO_FECHA3) = "" Then
                            If Nu(Rs!ELEMENTO_FECHA2) <= FechaServer Then Entrar = True
                        ElseIf Nu(Rs!ELEMENTO_FECHA2) = "" And Nu(Rs!ELEMENTO_FECHA3) <> "" Then
                            If Nu(Rs!ELEMENTO_FECHA3) >= FechaServer Then Entrar = True
                        Else
                            If Nu(Rs!ELEMENTO_FECHA2) <= FechaServer And Nu(Rs!ELEMENTO_FECHA3) >= FechaServer Then Entrar = True
                        End If
                    End If
                    If Entrar = True Then
                        Dim Item As New ListItem
                        Item.Text = Nu(Rs!ELEMENTO_NOMBRE)
                        Item.Value = Nu(Rs!ELEMENTO_CODIGO)
                        cboElemento.Items.Add(Item)
                    End If
                End While
            End If
            Rs.Close()
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Function Carga_Grilla_Source(ByVal CodElement As String) As ICollection
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql As New SqlCommand
        Dim FechaServer As String = FechaActual()
        Dim UsarFechaIni As Boolean = False
        Dim UsarFechaFin As Boolean = False
        Dim Entrar As Boolean = False

        Dim dt As New DataTable
        Dim MyDataRow As DataRow
        dt.Columns.Add(New DataColumn("C1"))
        dt.Columns.Add(New DataColumn("C2"))
        dt.Columns.Add(New DataColumn("C3"))
        dt.Columns.Add(New DataColumn("C4"))
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT * FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') AND (CAMPO_NOMBRE='ELEMENTO_FECHA2')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then UsarFechaIni = True
            Rs.Close()
            cmdSql.CommandText = "SELECT * FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') AND (CAMPO_NOMBRE='ELEMENTO_FECHA3')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then UsarFechaFin = True
            Rs.Close()
            cmdSql.CommandText = "SELECT *,(SELECT COUNT(COMENT_CODIGO) FROM TBMENU_ITEMS_COMENT X WHERE X.ELEMENTO_CODIGO=T.ELEMENTO_CODIGO AND X.COMENT_SYS_EST='0') AS NCOMENT " _
                               & " FROM TBMENU_ITEMS_ELEMENTOS T WHERE GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND ITEM_CODIGO=" & Session("MenuCod") & " AND ELEMENTO_SYS_EST='0'"
            If cboCat.SelectedValue <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (ELEMENTO_CATEGORIA=" & cboCat.SelectedValue & ")"
            If CodElement <> "" And CodElement <> "0" Then cmdSql.CommandText = cmdSql.CommandText & " AND (ELEMENTO_CODIGO=" & CodElement & ")"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then 'obtener contador de rptas para la matriz
                While Rs.Read
                    Entrar = False
                    If UsarFechaIni = False And UsarFechaFin = False Then
                        Entrar = True
                    ElseIf UsarFechaIni = True And UsarFechaFin = False Then
                        If Nu(Rs!ELEMENTO_FECHA2) = "" Or (Nu(Rs!ELEMENTO_FECHA2) <= FechaServer) Then Entrar = True
                    ElseIf UsarFechaIni = False And UsarFechaFin = True Then
                        If Nu(Rs!ELEMENTO_FECHA3) = "" Or (Nu(Rs!ELEMENTO_FECHA3) >= FechaServer) Then Entrar = True
                    ElseIf UsarFechaIni = True And UsarFechaFin = True Then
                        If Nu(Rs!ELEMENTO_FECHA2) = "" And Nu(Rs!ELEMENTO_FECHA3) = "" Then
                            Entrar = True
                        ElseIf Nu(Rs!ELEMENTO_FECHA2) <> "" And Nu(Rs!ELEMENTO_FECHA3) = "" Then
                            If Nu(Rs!ELEMENTO_FECHA2) <= FechaServer Then Entrar = True
                        ElseIf Nu(Rs!ELEMENTO_FECHA2) = "" And Nu(Rs!ELEMENTO_FECHA3) <> "" Then
                            If Nu(Rs!ELEMENTO_FECHA3) >= FechaServer Then Entrar = True
                        Else
                            If Nu(Rs!ELEMENTO_FECHA2) <= FechaServer And Nu(Rs!ELEMENTO_FECHA3) >= FechaServer Then Entrar = True
                        End If
                    End If
                    If Entrar = True Then
                        MyDataRow = dt.NewRow()
                        MyDataRow(0) = Nu(Rs!ELEMENTO_CODIGO)
                        MyDataRow(1) = Nu(Rs!ELEMENTO_AGREG_COMENT)
                        MyDataRow(2) = Nz(Rs!NCOMENT)
                        MyDataRow(3) = Nu(Rs!ELEMENTO_ARCHIVO_NOMBRE) '
                        dt.Rows.Add(MyDataRow)
                    End If
                End While
                Carga_Grilla_Source = New DataView(dt)
            Else
                lblMensaje.Visible = True
                lblMensaje.Text = "No hay registros que mostrar."
            End If
            Rs.Close()
        Catch Ex As SqlException
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
    End Function
    Private Sub Carga_Grilla(ByVal PriVez As Boolean, ByVal CodElement As String)
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim obj As New Listados
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim dt As New DataTable
        Dim VerDetalle As Boolean = False, LinkVerDListo As Boolean = False, VerDetalle2 As Boolean = False
        lblMensaje.Visible = False
        Try
            Dim pdCodGrupo As Double = 0
            Dim Sigla As String = ""
            Dim objSeg As New ModuloSeguridad
            pdCodGrupo = Session("CodGrupoEmpresa")
            dt = objSeg.Obtener_Sigla(pdCodGrupo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Sigla = Nu(dr("GE_CODIGO")) & Nu(dr("GE_PREFIJO"))
                Next
            End If
            dt = Nothing
            Lista.DataSource = Carga_Grilla_Source(CodElement)
            Lista.DataBind()
            If PriVez = True Then
                If Lista.Items.Count < 5 Then Lista.AllowPaging = False Else Lista.AllowPaging = True
                Lista.DataBind()
            End If
            Cn.Open()
            Cn2.Open()
            cmdSql.Connection = Cn
            cmdSql2.Connection = Cn2
            cmdSql.CommandText = "SELECT * FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') AND " _
                              & "(CAMPO_NOMBRE<>'ELEMENTO_CATEGORIA' AND CAMPO_NOMBRE<>'ELEMENTO_NOMBRE' AND CAMPO_NOMBRE<>'ELEMENTO_DESCRIP_CORTA' AND CAMPO_NOMBRE<>'ELEMENTO_LINK1' AND CAMPO_NOMBRE<>'ELEMENTO_FECHA1' AND CAMPO_NOMBRE<>'ELEMENTO_USUARIO' AND CAMPO_NOMBRE<>'ELEMENTO_IMAGEN' AND CAMPO_NOMBRE<>'ELEMENTO_ARCHIVO_NOMBRE' AND CAMPO_NOMBRE<>'ELEMENTO_FECHA2' AND CAMPO_NOMBRE<>'ELEMENTO_FECHA3' )"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then VerDetalle = True
            Rs.Close()
            Dim i As Integer, a As Integer, cl As Integer
            Dim Fila As DataGridItem
            With Lista
                For i = 0 To .Items.Count - 1
                    VerDetalle2 = VerDetalle : LinkVerDListo = False
                    Fila = .Items(i)
                    'OCULTAR
                    For a = 1 To 7
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("L" & a & ""), System.Web.UI.HtmlControls.HtmlGenericControl)
                        If Not lbl Is Nothing Then lbl.InnerText = "" : lbl.Visible = False : lbl.Attributes("FONT-WEIGHT") = "Normal"
                        Dim LinkB As LinkButton = CType(Fila.FindControl("b" & a & ""), LinkButton)
                        If Not LinkB Is Nothing Then LinkB.Text = "" : LinkB.Visible = False
                    Next
                    Dim lblx As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("lblComent"), System.Web.UI.HtmlControls.HtmlGenericControl)
                    If Not lblx Is Nothing Then lblx.Visible = False
                    Dim Img As ImageButton = CType(Fila.FindControl("Img"), ImageButton)
                    If Not Img Is Nothing Then Img.Visible = False
                    Dim Link1 As LinkButton = CType(Fila.FindControl("BVerD"), LinkButton)
                    If Not Link1 Is Nothing Then Link1.Visible = False
                    Dim Link2 As ImageButton = CType(Fila.FindControl("BAddC"), ImageButton)
                    If Not Link2 Is Nothing Then Link2.Visible = False
                    Dim Link3 As ImageButton = CType(Fila.FindControl("BVerC"), ImageButton)
                    If Not Link3 Is Nothing Then Link3.Visible = False
                    Dim Link4 As LinkButton = CType(Fila.FindControl("BModD"), LinkButton)
                    If Not Link4 Is Nothing Then
                        If User.Identity.Name = "" Then
                            Link4.Visible = False
                        Else
                            Link4.Visible = False
                            dt = obj.Autoriza_IngElemento(User.Identity.Name, Session("MenuCod"))
                            If dt.Rows.Count > 0 Then Link4.Visible = True
                            dt = Nothing
                        End If
                    End If
                    'MOSTRAR
                    cl = 0
                    cmdSql.CommandText = "SELECT CAMPO_ETIQUETA, CAMPO_NOMBRE " _
                                      & " FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') ORDER BY CAMPO_ORDEN"
                    Rs = cmdSql.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            If Nu(Rs!CAMPO_NOMBRE) <> "ELEMENTO_FECHA2" And Nu(Rs!CAMPO_NOMBRE) <> "ELEMENTO_FECHA3" Then 'ESTOS CAMPOS SON PARA WHERE Y NO PARA MOSTRAR
                                If Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_NOMBRE" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_DESCRIP_CORTA" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_LINK1" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_FECHA1" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_USUARIO" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_IMAGEN" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_ARCHIVO_NOMBRE" Then  'solo estos campos son permitidos, los demas seria ver detalles 'Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_CATEGORIA" Or
                                    If Existe_Campo("TBMENU_ITEMS_ELEMENTOS", Nu(Rs!CAMPO_NOMBRE)) = True Then
                                        If Nu(Rs!CAMPO_NOMBRE) = "" Then
                                            'If Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_IMAGEN" Then
                                            'If Not Img Is Nothing Then
                                            '    Img.Visible = True
                                            '    Img.ImageUrl = "" : Img.DataBind()
                                            '    Img.ImageUrl = "~/MenuWeb/Imagenes_" & Sigla & "/" & Nu(Rs!ELEMENTO_IMAGEN_NOMBRE)
                                            '    'Img.ImageUrl = "ArchModMenu/Imagenes/m" & Fila.Cells(0).Text & ".jpg"
                                            '    ''Img.ImageUrl = Server.MapPath("~\Menu\ArchModMenu\Imagenes\m" & Fila.Cells(0).Text & ".jpg")
                                            '    Img.DataBind()
                                            '    'Img.Visible = True
                                            '    'Img.ImageUrl = "" : Img.Width = Unit.Empty : Img.Height = Unit.Empty : Img.DataBind()
                                            '    'Img.ImageUrl = "ArchModMenu/Imagenes/m" & Fila.Cells(0).Text & ".jpg"
                                            '    'Img.DataBind()
                                            'End If
                                        Else
                                            cl = cl + 1
                                            cmdSql2.CommandText = " SELECT " & Nu(Rs!CAMPO_NOMBRE) & ",ELEMENTO_IMAGEN_NOMBRE,ELEMENTO_NOMBRE2, " _
                                                                & " (SELECT CATEG_NOMBRE FROM TBMENU_ITEMS_CATEGORIA WHERE CATEG_CODIGO=ELEMENTO_CATEGORIA) AS CATEG, " _
                                                                & " (SELECT USUARI_APEPAT+' '+USUARI_APEMAT+' '+USUARI_NOMBRES AS NOMBRESU From  BDSeguridadGrupoEmps.DBO.TBUSUARI WHERE USUARI_CODIGO=ELEMENTO_USUARIO)AS NOMBRESU " _
                                                                & " FROM TBMENU_ITEMS_ELEMENTOS WHERE ELEMENTO_CODIGO='" & Fila.Cells(0).Text & "' AND GRPOEMPRESA_CODIGO = '" & Session("CodGrupoEmpresa") & "' AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                                            Rs2 = cmdSql2.ExecuteReader
                                            If Rs2.HasRows Then
                                                While Rs2.Read
                                                    If Nu(Rs2(Rs!CAMPO_NOMBRE)) <> "" Then
                                                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("L" & cl & ""), System.Web.UI.HtmlControls.HtmlGenericControl)
                                                        If Not lbl Is Nothing Then
                                                            lbl.Visible = True
                                                            If Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_NOMBRE" Then
                                                                If Nu(Rs2!ELEMENTO_NOMBRE2) <> "" Then
                                                                    lbl.InnerHtml = Nu(Rs2!ELEMENTO_NOMBRE2)
                                                                Else
                                                                    lbl.InnerHtml = Nu(Rs2!ELEMENTO_NOMBRE)
                                                                End If
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_IMAGEN" Then
                                                                If Not Img Is Nothing Then
                                                                    Img.Visible = True
                                                                    Img.ImageUrl = "" : Img.DataBind()
                                                                    Img.ImageUrl = "~/MenuWeb/Imagenes_" & Sigla & "/" & Nu(Rs2!ELEMENTO_IMAGEN_NOMBRE)
                                                                    Img.DataBind()
                                                                End If
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_ARCHIVO_NOMBRE" Then
                                                                'lbl.InnerHtml = "<img src='ArchModMenu/Iconos/new.gif'/><A href='ArchModMenu/Dowloads/" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "' TARGET='_blank'>" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "</A>"
                                                                lbl.InnerHtml = "<img src='ArchModMenu/Iconos/new.gif'/><A href='~/MenuWeb/Archivos_" & Sigla & "/" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "' TARGET='_blank'>" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "</A>"
                                                                'lbl.InnerHtml = "<img src='ArchModMenu/Iconos/new.gif'/><A href='ArchModMenu/Dowloads/" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "'Detalle.aspx?CodElement={0}) %>'>" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "</A>"
                                                                lbl.InnerHtml = "<img src='ArchModMenu/Iconos/downlanim.gif'/>&nbsp;" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE)
                                                                Dim LinkB As LinkButton = CType(Fila.FindControl("b" & cl & ""), LinkButton)
                                                                If Not LinkB Is Nothing Then
                                                                    'LinkB.Visible = True
                                                                    'LinkB.Text = "Descargar..."
                                                                    'LinkB.CommandName = "eDowLoad"
                                                                End If
                                                            ElseIf InStr(1, Nu(Rs!CAMPO_NOMBRE), "FECHA") > 0 Then
                                                                lbl.InnerHtml = FormatoFecha(Nu(Rs2(Rs!CAMPO_NOMBRE)))

                                                                'ElseIf InStr(1, Nu(Rs!CAMPO_NOMBRE), "LINK") > 0 Then
                                                                '    lbl.InnerHtml = "<b>" & Nu(Rs!CAMPO_ETIQUETA) & " : </b><img src='ArchModMenu/Iconos/web.gif'/>&nbsp;<A href='http://" & Nu(Rs2(Rs!CAMPO_NOMBRE)) & "'TARGET='_blank'>" & Nu(Rs2(Rs!CAMPO_NOMBRE)) & "</A>"

                                                            ElseIf InStr(1, Nu(Rs!CAMPO_NOMBRE), "LINK") > 0 Then
                                                                lbl.InnerHtml = "<img src='ArchModMenu/Iconos/web.gif'/>&nbsp;<A href='" & Nu(Rs2(Rs!CAMPO_NOMBRE)) & "' TARGET='_blank'>" & Nu(Rs2(Rs!CAMPO_NOMBRE)) & "</A>"
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_USUARIO" Then
                                                                lbl.InnerHtml = "<b>" & Nu(Rs!CAMPO_ETIQUETA) & " : </b>" & Nu(Rs2!NOMBRESU)
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_DESCRIP_CORTA" Then
                                                                lbl.InnerHtml = Nu(Rs2(Rs!CAMPO_NOMBRE))
                                                                If VerDetalle2 = True And LinkVerDListo = False Then
                                                                    Dim LinkB As LinkButton = CType(Fila.FindControl("b" & cl & ""), LinkButton)
                                                                    If Not LinkB Is Nothing Then
                                                                        LinkB.Visible = True
                                                                        LinkB.Text = "Leer&nbsp;&nbsp;más..."
                                                                        LinkB.CommandName = "eVerD"
                                                                        LinkVerDListo = True
                                                                    End If
                                                                End If
                                                            Else
                                                                lbl.InnerHtml = Nu(Rs2(Rs!CAMPO_NOMBRE))
                                                            End If
                                                        End If
                                                    End If
                                                End While
                                            End If
                                            Rs2.Close()
                                        End If
                                    End If
                                End If
                            End If
                        End While
                    End If
                    Rs.Close()
                    If Not Img Is Nothing Then
                        Dim lbL As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("L2"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        If Not lbL Is Nothing Then
                            If Img.Visible = True Then lbL.Style("WIDTH") = "380px" ': lbL.DataBind()
                        End If
                    End If
                    If VerDetalle2 = True And LinkVerDListo = False Then
                        Dim LinkX As LinkButton = CType(Fila.FindControl("BVerD"), LinkButton)
                        If Not LinkX Is Nothing Then LinkX.Visible = True
                    End If
                    If Fila.Cells(1).Text = "S" Or CInt(Fila.Cells(2).Text) > 0 Then
                        Dim lblx1 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("lblComent"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        If Not lblx1 Is Nothing Then lblx.Visible = True : lblx.InnerHtml = "Comentarios: " & Fila.Cells(2).Text
                        If Fila.Cells(1).Text = "S" Then
                            Dim Linkx2 As ImageButton = CType(Fila.FindControl("BAddC"), ImageButton)
                            If Not Linkx2 Is Nothing Then Linkx2.Visible = True
                        End If
                        If CInt(Fila.Cells(2).Text) > 0 Then
                            Dim Linkx3 As ImageButton = CType(Fila.FindControl("BVerC"), ImageButton)
                            If Not Linkx3 Is Nothing Then Linkx3.Visible = True
                        End If
                        'ELEMENTO_NRO_VISTO,
                    End If
                Next
            End With
        Catch Ex As SqlException
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
            Cn2.Close()
        End Try
    End Sub
    Private Sub Lista_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Lista.ItemCommand
        If e.CommandName = "eVerD" Then
            'Session("MenuCod") = Session("MenuCod")
            'Session("MenuNom") = Session("MenuNom")
            Session("MenuCodElement") = e.Item.Cells(0).Text
            Response.Redirect("Detalle.aspx")
        ElseIf e.CommandName = "eModD" Then
            Session("Modificar") = "S"
            Session("MenuCodElement") = e.Item.Cells(0).Text
            Response.Redirect("IngresarElemento.aspx")
        ElseIf e.CommandName = "eAddC" Then

        ElseIf e.CommandName = "eVerC" Then

        ElseIf e.CommandName = "eDowLoad" Then

        Else
        End If
    End Sub
    Private Sub Lista_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Lista.PageIndexChanged
        Lista.CurrentPageIndex = e.NewPageIndex
        Call Carga_Grilla(False, "")
    End Sub
    Private Sub cboElemento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboElemento.SelectedIndexChanged
        If cboElemento.SelectedIndex = -1 Then Exit Sub
        Call Carga_Grilla(True, cboElemento.SelectedValue)
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("Modificar") = "N"
        Response.Redirect("IngresarElemento.aspx")
    End Sub
End Class
