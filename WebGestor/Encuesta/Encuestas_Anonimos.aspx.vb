Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class Encuestas_Anonimos
    Inherits System.Web.UI.Page
    Public NoMouse As Boolean
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
        If NoMouse = True Then Exit Sub
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            Tabla.Height = Unit.Empty
            lblMensaje.Text = ""
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
        End If
    End Sub
    Sub Tabla_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblMensaje.Text = ""
        lblMensaje2.Text = ""
        Tabla.CurrentPageIndex = e.NewPageIndex
        lblMensaje.Text = ""
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
        Dim Cn As New SqlConnection(strConexion)
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
            Dim Sql As String = "SELECT DISTINCT D.EMPRESA_CODIGO, D.PRUEBA_CODIGO, D.PRUEBA_TIPO, D.PRUEBA_NOMBRE,D.PRUEBA_TIPO_RESPUESTAS, " _
                                & " PRUEBA_PUBLI_TIENE,PRUEBA_PUBLI_FECINI,PRUEBA_PUBLI_FECFIN " _
                                & " FROM TBGENERAC_PRUEBA_DEFINE D INNER JOIN TBGENERAC_PRUEBA_GRUPOS_0001 G ON D.PRUEBA_CODIGO = G.PRUEBA_CODIGO" _
                                & " WHERE (D.EMPRESA_CODIGO = '0001') AND (D.PRUEBA_SYS_EST = '0') AND (G.GRUPO_TIPO = '6') AND (G.GRUPO_SYS_EST = '0') ORDER BY D.PRUEBA_CODIGO"
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
                Carga_Encuestas = New DataView(dt)
            Else
                bolError = True
            End If
        Catch Ex As SqlException
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = True Then lblMensaje.Text = "No se encontraron Pruebas y Encuestas."
    End Function
    Private Sub Tabla_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Tabla.ItemCommand
        If e.Item.Cells.Count < 3 Then Exit Sub
        lblMensaje.Text = ""
        lblMensaje2.Text = ""
        Session("CodPrueba") = e.Item.Cells(2).Text
        Session("TipoPrueba") = e.Item.Cells(1).Text
        Session("NomPrueba") = e.Item.Cells(3).Text
        Session("CodGrupo") = ""
        Dim Cn As New SqlConnection(strConexion)
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim bolError As String = ""
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            If e.CommandName = "Desarrollar" Then
                cmdSql.CommandText = "SELECT * FROM TBGENERAC_PRUEBA_DESARROLLO_0001 WHERE (PRUEBA_CODIGO =" & e.Item.Cells(2).Text & ") AND (PD_SESSIONID = '" & Session.SessionID & "') ORDER BY GRUPO_CODIGO"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblMensaje2.Text = "La " & Session("TipoPrueba") & " ya ha sido desarrollada!!!, favor de escoger otra que no haya desarrollado."
                        Rs.Close()
                        Cn.Close()
                        Exit Try
                    End While
                End If
                Rs.Close()
            End If
            cmdSql.CommandText = "SELECT GRUPO_CODIGO FROM TBGENERAC_PRUEBA_GRUPOS_0001 WHERE (PRUEBA_CODIGO =" & e.Item.Cells(2).Text & ") AND (GRUPO_TIPO='6') AND (GRUPO_SYS_EST = '0') ORDER BY GRUPO_CODIGO"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    bolError = "1"
                    Session("CodGrupo") = Nu(Rs!GRUPO_CODIGO)
                    Exit While
                End While
            Else
                bolError = "2"
            End If
        Catch Ex As SqlException
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = "2" Then
            lblMensaje.Text = "Parámetros inválidos."
        ElseIf bolError = "1" Then
            If e.CommandName = "Desarrollar" Then
                Response.Redirect("Encuesta_Anonimos_Des.aspx")
            ElseIf e.CommandName = "VerResultados" Then
                Response.Redirect("VerResultEncAno.aspx")
            End If
        End If
    End Sub
End Class
