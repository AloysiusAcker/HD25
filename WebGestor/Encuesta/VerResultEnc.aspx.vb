Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Data
Imports WebGestor
Partial Class VerResultEnc
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
            lblTitulo.InnerText = "Resultado de " & Session("TipoPrueba") & " Nº " & Session("CodPrueba")
            lblTitulo2.InnerText = "''" & Session("NomPrueba") & "''"
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
            Dim Rs As SqlDataReader
            Dim Rs2 As SqlDataReader
            Dim cmdSql As New SqlCommand
            Dim cmdSql2 As New SqlCommand
            Dim bolError As String = ""
            Dim a As Integer, b As Integer, c As Integer, TipoCampoGrupo As String
            lblTotalEnc.Visible = False
            Tabla.Columns.Clear()
            Tabla.AutoGenerateColumns = True
            If Session("TipoGrupo") = "3" Then
                TipoCampoGrupo = "PD_PERSONAL"
            ElseIf Session("TipoGrupo") = "5" Then
                TipoCampoGrupo = "PD_USUARIO"
            Else
                Exit Sub 'FALTA EMPRESA
            End If
            Try
                Cn.Open()
                Cn2.Open()
                cmdSql.Connection = Cn
                cmdSql2.Connection = Cn2
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_Result_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsTable') = 1) drop Table [dbo].[w_Result_" & Session.SessionID & "]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "CREATE TABLE [dbo].[w_Result_" & Session.SessionID & "] ([ENCUESTA_CODIGO] [float] NULL ,[AGRUPADO] [varchar] (50) NULL ,[PREGUNTA_ORDEN] [float] NULL ,[PREGUNTA_NOMBRE] [varchar] (200) NULL ,[RESPUESTA_ORDEN] [varchar] (5) NULL ,[RESPUESTA_NOMBRE] [varchar] (200) NULL ,[VALOR] [float] NULL ,[TOTAL_ENCUESTADOS] [float] NULL ,[TOTAL_DESARROLLADOS] [float] NULL ,[TOTAL_SIN_DESARROLLAR] [float] NULL ,[ENCABEZADO_ORDEN] [float] NULL ,[ENCABEZADO] [varchar] (100) NULL ) ON [PRIMARY]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "SELECT R.PRUEBA_CODIGO, P.PREG_ORDEN, R.PREG_CODIGO, P.PREG_DESCRIPCION, R.RESP_ORDEN, R.RESP_CODIGO, R.RESP_DESCRIPCION " _
                   & " FROM TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & " R INNER JOIN TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " P ON R.PRUEBA_CODIGO = P.PRUEBA_CODIGO AND R.PREG_CODIGO = P.PREG_CODIGO " _
                   & " WHERE (R.PRUEBA_CODIGO =" & Session("CodPrueba") & ") AND (P.PREG_SYS_EST = '0') AND (R.RESP_SYS_EST = '0') ORDER BY P.PREG_ORDEN, R.RESP_ORDEN"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        a = 0
                        'cmdSql2.CommandText = "SELECT PREG_CODIGO, RESP_CODIGO, DD_RESPONDIDO FROM TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " " _
                        '      & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (PREG_CODIGO = " & Nz(Rs!PREG_CODIGO) & ") AND (RESP_CODIGO = " & Nz(Rs!RESP_CODIGO) & ") AND (DD_RESPONDIDO = 'X') AND (DD_SYS_EST='0')"
                        cmdSql2.CommandText = "SELECT COUNT(DD_RESPONDIDO) FROM TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " " _
                              & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (PREG_CODIGO = " & Nz(Rs!PREG_CODIGO) & ") AND (RESP_CODIGO = " & Nz(Rs!RESP_CODIGO) & ") AND (DD_RESPONDIDO = 'X') AND (DD_SYS_EST='0')"
                        Rs2 = cmdSql2.ExecuteReader
                        If Rs2.HasRows Then
                            While Rs2.Read
                                'a = a + 1
                                a = Rs2(0)
                            End While
                        End If
                        Rs2.Close()
                        cmdSql2.CommandText = "INSERT INTO w_Result_" & Session.SessionID & "(ENCUESTA_CODIGO, AGRUPADO, PREGUNTA_ORDEN,PREGUNTA_NOMBRE, RESPUESTA_ORDEN,RESPUESTA_NOMBRE, VALOR,ENCABEZADO_ORDEN,ENCABEZADO) " _
                                              & "VALUES(" & Session("CodPrueba") & ",NULL," & Nz(Rs!PREG_ORDEN) & ",'" & Left(Nu(Rs!PREG_DESCRIPCION), 200) & "','" & Nu(Rs!RESP_ORDEN) & "','" & Left(Nu(Rs!RESP_DESCRIPCION), 200) & "'," & a & ",1,'RESPUESTAS')"
                        cmdSql2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
                cmdSql.CommandText = "SELECT PREG_ORDEN, PREG_DESCRIPCION,PRUEBA_CODIGO,PREG_CODIGO FROM TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " P WHERE (PREG_SYS_EST = '0') AND (PRUEBA_CODIGO =" & Session("CodPrueba") & ") ORDER BY PREG_ORDEN"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        a = 0
                        cmdSql2.CommandText = "SELECT DD.PREG_CODIGO, MAX(DD.DD_RESPONDIDO) AS VALOR, D." & TipoCampoGrupo & ", D.PD_ESTADO " _
                             & "FROM TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " DD INNER JOIN TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " D ON DD.PRUEBA_CODIGO = D.PRUEBA_CODIGO AND " _
                             & "Dd.GRUPO_CODIGO = d.GRUPO_CODIGO And Dd.GRUPO_TIPO = d.GRUPO_TIPO And Dd." & TipoCampoGrupo & " = d." & TipoCampoGrupo & " " _
                             & "WHERE (DD.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (DD.GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (DD.GRUPO_CODIGO =" & Session("CodGrupo") & ") AND (D.PD_SYS_EST = '0') AND (DD.DD_SYS_EST = '0') " _
                             & "GROUP BY DD.PREG_CODIGO, D." & TipoCampoGrupo & ",D.PD_ESTADO " _
                             & "HAVING (DD.PREG_CODIGO =" & Nz(Rs!PREG_CODIGO) & ") AND (D.PD_ESTADO = '3') AND (MAX(DD.DD_RESPONDIDO) IS NULL OR MAX(DD.DD_RESPONDIDO) = '')"
                        Rs2 = cmdSql2.ExecuteReader
                        If Rs2.HasRows Then
                            While Rs2.Read
                                a = a + 1
                            End While
                        End If
                        Rs2.Close()
                        cmdSql2.CommandText = "INSERT INTO w_Result_" & Session.SessionID & "(ENCUESTA_CODIGO, AGRUPADO, PREGUNTA_ORDEN,PREGUNTA_NOMBRE, RESPUESTA_ORDEN,RESPUESTA_NOMBRE, VALOR,ENCABEZADO_ORDEN) " _
                                              & "VALUES(" & Session("CodPrueba") & ",NULL," & Nz(Rs!PREG_ORDEN) & ",'" & Left(Nu(Rs!PREG_DESCRIPCION), 200) & "','ZZ','Sin Responder'," & a & ",2)"
                        cmdSql2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
                a = 0
                cmdSql.CommandText = "SELECT * FROM TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " WHERE (PRUEBA_CODIGO =" & Session("CodPrueba") & ") AND (GRUPO_CODIGO =" & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (PD_SYS_EST = '0') AND (PD_ESTADO = '3')"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        a = a + 1
                    End While
                End If
                Rs.Close()
                cmdSql.CommandText = "UPDATE w_Result_" & Session.SessionID & " SET TOTAL_DESARROLLADOS=" & a
                cmdSql.ExecuteNonQuery()
                Dim dt As New DataTable
                Dim dr As DataRow
                Dim dv As DataView
                Dim Sql As String
                cmdSql.CommandText = "SELECT DISTINCT RESPUESTA_ORDEN, RESPUESTA_NOMBRE  FROM w_Result_" & Session.SessionID & "  ORDER BY RESPUESTA_ORDEN"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    a = 0
                    dt.Columns.Add(New DataColumn("#", GetType(String)))
                    dt.Columns.Add(New DataColumn("Pregunta", GetType(String)))
                    While Rs.Read
                        a = a + 1
                        If Nu(Rs!RESPUESTA_ORDEN) = "ZZ" Then
                            dt.Columns.Add(New DataColumn(Nu(Rs!RESPUESTA_NOMBRE), GetType(String)))
                        Else
                            dt.Columns.Add(New DataColumn("Respondieron ''" & Nu(Rs!RESPUESTA_NOMBRE) & "''", GetType(String)))
                        End If
                        Sql = Sql & "SUM(CASE WHEN RESPUESTA_ORDEN = '" & Nu(Rs!RESPUESTA_ORDEN) & "' THEN VALOR ELSE NULL END) AS RE" & a & ","
                    End While
                    Rs.Close()
                    cmdSql.CommandText = "SELECT " & Sql & "  PREGUNTA_ORDEN, PREGUNTA_NOMBRE,TOTAL_DESARROLLADOS FROM w_Result_" & Session.SessionID & " GROUP BY PREGUNTA_ORDEN, PREGUNTA_NOMBRE,TOTAL_DESARROLLADOS"
                    Rs = cmdSql.ExecuteReader
                    While Rs.Read
                        lblTotalEnc.Visible = True
                        lblTotalEnc.Text = "Personas que realizaron la Encuesta : " & Nz(Rs!TOTAL_DESARROLLADOS)
                        b = b + 1
                        dr = dt.NewRow()
                        dr(0) = "<b>" & b.ToString & ".- "
                        dr(1) = Nu(Rs!PREGUNTA_NOMBRE)
                        For c = 1 To a
                            dr(c + 1) = Nu(Rs("RE" & c & ""))
                        Next
                        dt.Rows.Add(dr)
                    End While
                    Rs.Close()
                    cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_Result_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsTable') = 1) drop Table [dbo].[w_Result_" & Session.SessionID & "]"
                    cmdSql.ExecuteNonQuery()
                    With Tabla
                        .DataSource = New DataView(dt)
                        .DataBind()
                        For b = 0 To .Items.Count - 1
                            For a = 0 To .Items(b).Cells.Count - 1
                                If a <= 1 Then 'COLUMNAS # Y PREGUNTA
                                    .Items(b).Cells(a).VerticalAlign = VerticalAlign.Middle
                                    .Items(b).Cells(a).HorizontalAlign = HorizontalAlign.Left
                                Else
                                    .Items(b).Cells(a).Width = Unit.Pixel(80)
                                    .Items(b).Cells(a).VerticalAlign = VerticalAlign.Middle
                                    .Items(b).Cells(a).HorizontalAlign = HorizontalAlign.Center
                                End If
                            Next
                        Next
                    End With
                Else
                    bolError = "2"
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
                Cn2.Close()
            End Try
            If bolError = "2" Then
                lblMensaje.Visible = True
                lblMensaje.Text = "Parámetros inválidos."
            ElseIf bolError = "1" Then

            End If
        End If
    End Sub
End Class
