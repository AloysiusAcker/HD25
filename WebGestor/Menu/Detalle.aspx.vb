Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class Detalle
    Inherits System.Web.UI.Page
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            Dim Rs As SqlDataReader
            Dim Rs2 As SqlDataReader
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim Cn2 As New SqlConnection(Ruta_GrEmp)
            Dim cmdSql As New SqlCommand
            Dim cmdSql2 As New SqlCommand
            Dim dt As New DataTable
            Dim dtSigla As New DataTable
            Dim MyDataRow As DataRow
            Dim FechaServer As String = FechaActual()
            dt.Columns.Add(New DataColumn("C1"))
            dt.Columns.Add(New DataColumn("C2"))
            dt.Columns.Add(New DataColumn("C3"))
            lblTitulo.InnerText = Session("MenuNom")
            Title = Session("MenuNom")
            Dim UsarFechaIni As Boolean = False
            Dim UsarFechaFin As Boolean = False
            Dim Entrar As Boolean = False
            Dim VerDetalle As Boolean = False, LinkVerDListo As Boolean = False
            Try
                lblMensaje.Visible = False
                Dim pdCodGrupo As Double = 0
                Dim Sigla As String = ""
                Dim objSeg As New ModuloSeguridad
                pdCodGrupo = Session("CodGrupoEmpresa")
                dtSigla = objSeg.Obtener_Sigla(pdCodGrupo)
                If dtSigla.Rows.Count > 0 Then
                    For Each dr As DataRow In dtSigla.Rows
                        Sigla = Nu(dr("GE_CODIGO")) & Nu(dr("GE_PREFIJO"))
                    Next
                End If
                dtSigla = Nothing
                Cn.Open()
                Cn2.Open()
                cmdSql.Connection = Cn
                cmdSql2.Connection = Cn2
                'ConfigurationSettings.AppSettings("CodGE") -> en servipruen, Session("CodGrupoEmpresa")-> en soporte online
                cmdSql.CommandText = "SELECT *,(SELECT COUNT(COMENT_CODIGO) FROM TBMENU_ITEMS_COMENT X WHERE X.ELEMENTO_CODIGO=T.ELEMENTO_CODIGO AND X.COMENT_SYS_EST='0') AS NCOMENT " _
                                   & " FROM TBMENU_ITEMS_ELEMENTOS T WHERE GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND ITEM_CODIGO=" & Session("MenuCod") & " AND ELEMENTO_SYS_EST='0' AND ELEMENTO_CODIGO=" & Session("MenuCodElement") & ""
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then 'obtener contador de rptas para la matriz
                    While Rs.Read
                        MyDataRow = dt.NewRow()
                        MyDataRow(0) = Nu(Rs!ELEMENTO_CODIGO)
                        MyDataRow(1) = Nu(Rs!ELEMENTO_AGREG_COMENT)
                        MyDataRow(2) = Nz(Rs!NCOMENT)
                        dt.Rows.Add(MyDataRow)
                    End While
                Else
                    lblMensaje.Visible = True
                    lblMensaje.Text = "No hay registro que mostrar."
                End If
                Rs.Close()
                Lista.DataSource = New DataView(dt)
                Lista.DataBind()

                Dim i As Integer, a As Integer, cl As Integer
                Dim Fila As DataGridItem
                With Lista
                    For i = 0 To .Items.Count - 1
                        Fila = .Items(i)
                        'OCULTAR
                        For a = 1 To 15
                            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("L" & a & ""), System.Web.UI.HtmlControls.HtmlGenericControl)
                            If Not lbl Is Nothing Then lbl.Visible = False
                        Next
                        Dim CeldaImg As System.Web.UI.HtmlControls.HtmlTableCell = CType(Fila.FindControl("FraImg"), System.Web.UI.HtmlControls.HtmlTableCell)
                        'If Not CeldaImg Is Nothing Then CeldaImg.Visible = False
                        Dim lblx As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("lblComent"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        If Not lblx Is Nothing Then lblx.Visible = False
                        Dim Img As System.Web.UI.WebControls.Image = CType(Fila.FindControl("Img"), System.Web.UI.WebControls.Image)
                        If Not Img Is Nothing Then Img.Visible = False
                        Dim Link2 As ImageButton = CType(Fila.FindControl("BAddC"), ImageButton)
                        If Not Link2 Is Nothing Then Link2.Visible = False
                        Dim Link3 As ImageButton = CType(Fila.FindControl("BVerC"), ImageButton)
                        If Not Link3 Is Nothing Then Link3.Visible = False
                        'MOSTRAR
                        cl = 0
                        cmdSql.CommandText = "SELECT CAMPO_ETIQUETA, CAMPO_NOMBRE " _
                                          & " FROM TBMENU_ITEMS_CAMPOS WHERE (ITEM_CODIGO = " & Session("MenuCod") & ") AND (CAMPO_SYS_EST = '0') ORDER BY CAMPO_ORDEN"
                        Rs = cmdSql.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                If Nu(Rs!CAMPO_NOMBRE) <> "ELEMENTO_FECHA2" And Nu(Rs!CAMPO_NOMBRE) <> "ELEMENTO_FECHA3" Then 'ESTOS CAMPOS SON PARA WHERE Y NO PARA MOSTRAR
                                    If Existe_Campo("TBMENU_ITEMS_ELEMENTOS", Nu(Rs!CAMPO_NOMBRE)) = True Then
                                        'Session("MenuCampos") = Session("MenuCampos") & Nu(Rs!CAMPO_NOMBRE) & ","
                                        If Nu(Rs!CAMPO_NOMBRE) = "" Then
                                            'If Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_IMAGEN" Then
                                            'If Not Img Is Nothing Then
                                            '    'CeldaImg.Width = "540px"
                                            '    'CeldaImg.DataBind()
                                            '    Img.Visible = True
                                            '    Img.ImageUrl = "" : Img.Width = Unit.Empty : Img.Height = Unit.Empty : Img.DataBind()
                                            '    Img.ImageUrl = "ArchModMenu/Imagenes/m" & Fila.Cells(0).Text & ".jpg"
                                            '    'Img.ImageUrl = Server.MapPath("~\Menu\ArchModMenu\Imagenes\m" & Fila.Cells(0).Text & ".jpg")
                                            '    Img.DataBind()
                                            '    'If Img.Width = Unit.Pixel(540) Then Img.Width = Unit.Pixel(540)
                                            'End If
                                        Else
                                            cl = cl + 1
                                            cmdSql2.CommandText = " SELECT " & Nu(Rs!CAMPO_NOMBRE) & ", ELEMENTO_IMAGEN_NOMBRE, " _
                                                                & " (SELECT CATEG_NOMBRE FROM TBMENU_ITEMS_CATEGORIA WHERE CATEG_CODIGO=ELEMENTO_CATEGORIA) AS CATEG, " _
                                                                & " (SELECT PERSON_APEPAT+' '+PERSON_APEMAT+' '+PERSON_NOMBRES AS NOMBRESP FROM TBPERSONAL WHERE PERSON_CODIGO=ELEMENTO_USUARIO) AS NOMBRESP, " _
                                                                & " (SELECT USUARI_APEPAT+' '+USUARI_APEMAT+' '+USUARI_NOMBRES AS NOMBRESU From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO=ELEMENTO_USUARIO)AS NOMBRESU " _
                                                                & " FROM TBMENU_ITEMS_ELEMENTOS WHERE ELEMENTO_CODIGO='" & Fila.Cells(0).Text & "' AND GRPOEMPRESA_CODIGO = '" & Session("CodGrupoEmpresa") & "' AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                                            Rs2 = cmdSql2.ExecuteReader
                                            If Rs2.HasRows Then
                                                While Rs2.Read
                                                    Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("L" & cl & ""), System.Web.UI.HtmlControls.HtmlGenericControl)
                                                    If Not lbl Is Nothing Then
                                                        If Nu(Rs2(Rs!CAMPO_NOMBRE)) <> "" Then
                                                            lbl.Visible = True
                                                            If Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_CATEGORIA" Then
                                                                lbl.InnerHtml = "<b>" & Nu(Rs!CAMPO_ETIQUETA) & " : </b>" & Nu(Rs2!CATEG)
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_IMAGEN" Then
                                                                If Not Img Is Nothing Then
                                                                    Img.Visible = True
                                                                    Img.ImageUrl = "" : Img.DataBind()
                                                                    Img.ImageUrl = "~/MenuWeb/Imagenes_" & Sigla & "/" & Nu(Rs2!ELEMENTO_IMAGEN_NOMBRE)
                                                                    Img.DataBind()
                                                                End If
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_ARCHIVO_NOMBRE" Then
                                                                'lbl.InnerHtml = "<img src='ArchModMenu/Iconos/downlanim.gif'/>&nbsp;<A href='ArchModMenu/Dowloads/" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "'TARGET='_blank'>" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "</A>"
                                                                lbl.InnerHtml = "<img src='ArchModMenu/Iconos/downlanim.gif'/><A href='~/MenuWeb/Archivos_" & Sigla & "/" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "' TARGET='_blank'>" & Nu(Rs2!ELEMENTO_ARCHIVO_NOMBRE) & "</A>"
                                                            ElseIf InStr(1, Nu(Rs!CAMPO_NOMBRE), "FECHA") > 0 Then
                                                                lbl.InnerHtml = FormatoFecha(Nu(Rs2(Rs!CAMPO_NOMBRE)))
                                                            ElseIf InStr(1, Nu(Rs!CAMPO_NOMBRE), "LINK") > 0 Then
                                                                lbl.InnerHtml = "<b>" & Nu(Rs!CAMPO_ETIQUETA) & " : </b><img src='ArchModMenu/Iconos/web.gif'/>&nbsp;<A href='http://" & Nu(Rs2(Rs!CAMPO_NOMBRE)) & "'TARGET='_blank'>" & Nu(Rs2(Rs!CAMPO_NOMBRE)) & "</A>"
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_USUARIO" Then
                                                                lbl.InnerHtml = "<b>" & Nu(Rs!CAMPO_ETIQUETA) & " : </b>" & IIf(Nu(Rs2!NOMBRESP) = "", Nu(Rs2!NOMBRESU), Nu(Rs2!NOMBRESP))
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_NOMBRE" Then
                                                                lbl.InnerHtml = Nu(Rs2(Rs!CAMPO_NOMBRE))
                                                            ElseIf Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_DESCRIP_CORTA" Or Nu(Rs!CAMPO_NOMBRE) = "ELEMENTO_DESCRIP_LARGA" Then
                                                                lbl.InnerHtml = Nu(Rs2(Rs!CAMPO_NOMBRE))
                                                            Else
                                                                lbl.InnerHtml = "<b>" & Nu(Rs!CAMPO_ETIQUETA) & " : </b>" & Nu(Rs2(Rs!CAMPO_NOMBRE))
                                                            End If
                                                        End If
                                                    End If
                                                End While
                                            End If
                                            Rs2.Close()
                                        End If
                                    End If
                                End If
                            End While
                        End If
                        Rs.Close()
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
                            'ELEMENTO_NRO_VISTO,ELEMENTO_AGREG_COMENT,ELEMENTO_CODIGO
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
End Class