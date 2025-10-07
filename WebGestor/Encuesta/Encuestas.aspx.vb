Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class Encuestas
    Inherits System.Web.UI.Page
    Dim NoMouse As Boolean = False
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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            If User.Identity.Name = "" Or Session("TipoGrupo") Is Nothing Then
                Session.Clear()
                FormsAuthentication.SignOut()
                Response.Redirect("TerminaSesion.aspx")
                Exit Sub
            End If
            Tabla.Height = Unit.Empty
            lblMensaje.Text = "" : lblMensaje.Visible = False
            lblTitulo.InnerText = "Lista de Pruebas y Encuestas"
            If Session("TipoGrupo") = "3" Then lblTitulo.InnerText = "Lista de Pruebas y Encuestas - Personas" 'PERSONAL
            If Session("TipoGrupo") = "5" Then lblTitulo.InnerText = "Lista de Pruebas y Encuestas - Usuarios Externos"
            If Session("TipoGrupo") = "7" Then lblTitulo.InnerText = "Lista de Pruebas y Encuestas - Empresas"
            'Tabla.Visible = False
            ''If Session("TipoGrupo") = "" Then
            ''    lblMensaje.Text = "Ha terminado el tiempo de su sesión"
            ''    Exit Sub
            ''End If
            'Dim Cn As New SqlConnection(Ruta_Ng)
            'Dim Rs As SqlDataReader
            'Dim bolError As String = ""
            'Try
            '    Cn.Open()
            '    Dim Sql As String = "SELECT UE.EMPRESA_CODIGO, E.EMP_NOMBRE " _
            '                      & " FROM TBUSUARI_GRPOEMPS UE INNER JOIN BDGEmpresa3TG.dbo.TBEMPRESAS E ON UE.EMPRESA_CODIGO = E.EMP_CODIGO" _
            '                      & " WHERE (UE.GRPOEMPRESA_CODIGO = 3) AND (UE.USUARI_CODIGO = '" & User.Identity.Name & "') AND (E.EMP_SYS_EST = '0') AND (E.EMP_VALIDA = 'S') AND  (E.EMP_WEB_ENC_FIRMA = 'S') ORDER BY E.EMP_NOMBRE"
            '    Dim cmdSql As New SqlCommand(Sql, Cn)
            '    Rs = cmdSql.ExecuteReader
            '    If Rs.HasRows Then
            '        bolError = "2"
            '        While Rs.Read
            '            Dim Item As New ListItem
            '            Item.Value = Nu(Rs!EMPRESA_CODIGO)
            '            Item.Text = Nu(Rs!EMP_NOMBRE)
            '            'cboAgrupacion.Items.Add(Item)
            '        End While
            '    Else
            '        bolError = "1"
            '    End If
            'Catch Ex As SqlException
            '    'lblMensaje.Visible = True
            '    'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
            'Catch Ex As Exception
            '    'lblMensaje.Visible = True
            '    'lblMensaje.Text = "Ha ocurrido un error la Aplicacion :<br>" & ex.Message
            'Finally
            '    Cn.Close()
            'End Try
            'If bolError = "1" Then
            '    lblMensaje.Visible = True
            '    lblMensaje.Text = "Parámetros inválidos."
            'ElseIf bolError = "2" Then
            '    If Session("CodEmpresa") = "" And Session("CodEmpresa2") = "" Then
            '        Session("CodEmpresa") = Session("CodEmpresa")
            '        Session("CodEmpresa2") = Session("CodEmpresa")
            '        Session("NomAgrup") = "Agrupación " & Session("NombreGrupoEmpresa")
            '    ElseIf Session("CodEmpresa") <> "" And Session("CodEmpresa2") <> "" Then
            '        Session("CodEmpresa") = Session("CodEmpresa")
            '    End If
            Call Funcion_CboAgrup()
            'End If
        End If
    End Sub
    Private Sub Funcion_CboAgrup()
        Tabla.Visible = True
        Tabla.DataSource = Carga_Encuestas()
        Tabla.DataBind()
        If Tabla.Items.Count < 10 Then Tabla.AllowPaging = False Else Tabla.AllowPaging = True
        Tabla.DataBind()
        Dim Fila As DataGridItem
        Dim i As Integer
        With Tabla
            For i = 0 To .Items.Count - 1 'recorrido de filas
                Fila = .Items(i)
                Dim Boton As LinkButton = CType(Fila.FindControl("Ver"), LinkButton)
                If Not Boton Is Nothing Then 'RESULTADO DE ENCUESTA SOLO PARA RPTAS IGUALES
                    If .Items(i).Cells(1).Text = "Encuesta" And .Items(i).Cells(4).Text = "2" Then Boton.Visible = True Else Boton.Visible = False
                End If
            Next
        End With
    End Sub
    'Private Sub cboAgrupacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAgrupacion.SelectedIndexChanged
    '    'lblMensaje.Text = "" : lblMensaje.Visible = False
    '    'lblMensaje2.Text = ""
    '    'If NoMouse = True Then Exit Sub
    '    If cboAgrupacion.SelectedIndex = -1 Then Exit Sub
    '    Session("CodEmpresa") = cboAgrupacion.Items(cboAgrupacion.SelectedIndex).Value
    '    Session("CodEmpresa2") = Session("CodEmpresa")
    '    Session("NomAgrup") = "Agrupación " & cboAgrupacion.Items(cboAgrupacion.SelectedIndex).Text
    '    Response.Redirect("encuestas.aspx")
    'End Sub
    Sub Tabla_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblMensaje.Text = "" : lblMensaje.Visible = False
        lblMensaje2.Text = ""
        Tabla.CurrentPageIndex = e.NewPageIndex
        Tabla.DataSource = Carga_Encuestas()
        Tabla.DataBind()
        Dim Fila As DataGridItem
        Dim i As Integer
        With Tabla
            For i = 0 To .Items.Count - 1 'recorrido de filas
                Fila = .Items(i)
                Dim Boton As LinkButton = CType(Fila.FindControl("Ver"), LinkButton)
                If Not Boton Is Nothing Then 'RESULTADO DE ENCUESTA SOLO PARA RPTAS IGUALES
                    If .Items(i).Cells(1).Text = "Encuesta" And .Items(i).Cells(4).Text = "2" Then Boton.Visible = True Else Boton.Visible = False
                End If
            Next
        End With
    End Sub
    Private Function Carga_Encuestas() As ICollection
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim bolError As Boolean
        Dim i As Integer, Fecha As String = FechaActual()

        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dv As DataView

        dt.Columns.Add("C1", GetType(String))
        dt.Columns.Add("C2", GetType(String))
        dt.Columns.Add("C3", GetType(String))
        dt.Columns.Add("C4", GetType(String))
        dt.Columns.Add("C5", GetType(String))
        Try
            Cn.Open()
            Dim Sql As String = "SELECT DISTINCT D.EMPRESA_CODIGO, D.PRUEBA_CODIGO, D.PRUEBA_TIPO, D.PRUEBA_NOMBRE,D.PRUEBA_TIPO_RESPUESTAS," _
                                & " PRUEBA_PUBLI_TIENE,PRUEBA_PUBLI_FECINI,PRUEBA_PUBLI_FECFIN " _
                                & " FROM TBGENERAC_PRUEBA_DEFINE D INNER JOIN TBGENERAC_PRUEBA_GRUPOS_" & Session("CodEmpresa") & " G ON D.PRUEBA_CODIGO = G.PRUEBA_CODIGO" _
                                & " WHERE (D.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (D.PRUEBA_SYS_EST = '0') AND (G.GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (G.GRUPO_SYS_EST = '0') ORDER BY D.PRUEBA_CODIGO"
            Dim cmdSql As New SqlCommand(Sql, Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If Nu(Rs!PRUEBA_PUBLI_TIENE) = "" Or Nu(Rs!PRUEBA_PUBLI_TIENE) = "N" Or (Nu(Rs!PRUEBA_PUBLI_TIENE) = "S" And (Fecha >= Nu(Rs!PRUEBA_PUBLI_FECINI) And Fecha <= Nu(Rs!PRUEBA_PUBLI_FECFIN))) Then
                        i = i + 1
                        dr = dt.NewRow()
                        dr(0) = i.ToString
                        dr(1) = IIf(Nu(Rs!PRUEBA_TIPO) = "1", "Prueba", "Encuesta")
                        dr(2) = Format(Nz(Rs!PRUEBA_CODIGO), "0000")
                        dr(3) = Nu(Rs!PRUEBA_NOMBRE)
                        dr(4) = Nu(Rs!PRUEBA_TIPO_RESPUESTAS)
                        dt.Rows.Add(dr)
                    End If
                End While
            Else
                bolError = True
            End If
            Carga_Encuestas = New DataView(dt)
        Catch Ex As SqlException
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = True Then lblMensaje.Visible = True : lblMensaje.Text = "No se encontraron Pruebas y Encuestas."
    End Function
    Private Sub Tabla_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Tabla.ItemCommand
        If e.Item.Cells.Count < 3 Then Exit Sub
        lblMensaje.Text = "" : lblMensaje.Visible = False
        lblMensaje2.Text = ""
        Session("CodPrueba") = e.Item.Cells(2).Text
        Session("TipoPrueba") = e.Item.Cells(1).Text
        Session("NomPrueba") = e.Item.Cells(3).Text
        Session("CodGrupo") = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim bolError As String = ""
        Dim Campo As String
        If Session("TipoGrupo") = "3" Then
            Campo = "PD_PERSONAL"
        ElseIf Session("TipoGrupo") = "5" Then
            Campo = "PD_USUARIO"
        Else
            Exit Sub 'FALTA EMPRESA
        End If
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT GRUPO_CODIGO FROM TBGENERAC_PRUEBA_GRUPOS_" & Session("CodEmpresa") & " WHERE (PRUEBA_CODIGO =" & e.Item.Cells(2).Text & ") AND (GRUPO_TIPO='" & Session("TipoGrupo") & "') AND (GRUPO_SYS_EST = '0') ORDER BY GRUPO_CODIGO"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    bolError = "1"
                    Session("CodGrupo") = Nu(Rs!GRUPO_CODIGO)
                    Exit While
                End While
                Rs.Close()
                If e.CommandName = "Desarrollar" Then
                    cmdSql.CommandText = "SELECT * FROM TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " WHERE (PRUEBA_CODIGO =" & e.Item.Cells(2).Text & ") AND (" & Campo & "='" & User.Identity.Name & "') AND (GRUPO_CODIGO='" & Session("CodGrupo") & "')" 'AND (PD_SESSIONID = '" & Session.SessionID & "')
                    Rs = cmdSql.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            bolError = ""
                            lblMensaje2.Text = "La " & Session("TipoPrueba") & " ya ha sido desarrollada!!!, favor de escoger otra que no haya desarrollado."
                            Rs.Close()
                            Exit Try
                        End While
                    End If
                    Rs.Close()
                End If
            Else
                bolError = "2"
            End If
        Catch Ex As SqlException
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = "2" Then
            lblMensaje.Text = "Parámetros inválidos."
        ElseIf bolError = "1" Then
            If e.CommandName = "Desarrollar" Then
                Response.Redirect("Encuesta_Des.aspx")
            ElseIf e.CommandName = "VerResultados" Then
                Response.Redirect("VerResultEnc.aspx")
            End If
        End If
    End Sub
End Class
