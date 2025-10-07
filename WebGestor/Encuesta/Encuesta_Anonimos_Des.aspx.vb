Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class Encuesta_Anonimos_Des
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
            'If User.Identity.Name = "" Then
            '    Response.Redirect("Default.aspx")
            '    Exit Sub
            'End If
            lblResultado.Visible = False
            FlexResultado.Visible = False
            lblMensaje2.Text = ""
            HyperLink1.Visible = False
            lblTitulo.InnerText = "Desarrollo de la " & Session("TipoPrueba") & " Nº " & Session("CodPrueba")
            lblTitulo2.InnerText = "''" & Session("NomPrueba") & "''"
            Flex.Visible = False
            Flex1.Visible = False
            GuardarRptas.Visible = False
            Cancelar.Visible = False
            lblTipoRpta.Text = "" : lblTipoRptaCorrecta.Text = "" : lblFormaMarcar.Text = "" : lblFormaResponder.Text = ""
            lbl1.Visible = False : lbl2.Visible = False : lbl3.Visible = False
            lblIns1.Visible = False : lblIns2.Visible = False
            If Session("CodEmpresa") = "" Then
                lblMensaje.Text = "Se ha interrumpido la operación, intentar de nuevo, yendo a inicio y luego a Anónimo."
                Exit Sub
            End If
            Dim Cn As New SqlConnection(strConexion)
            Dim Rs As SqlDataReader
            Dim bolError As Boolean
            Try
                Cn.Open()
                Dim Sql As String = "SELECT PRUEBA_TIPO_RESPUESTAS,PRUEBA_TIPO_RPTAS_CORRECTAS,PRUEBA_ORGANIZ_PORGRUPO,PRUEBA_ESPECIFIC1,PRUEBA_ESPECIFIC2," _
                                  & " PRUEBA_FORMA_MARCAR,PRUEBA_FORMA_RESPONDER,PRUEBA_TIEMPO_HRS,PRUEBA_TIEMPO_MIN,PRUEBA_PREG_OBLIGAR_RESPONDER,PRUEBA_CONTESTAR_TODAS " _
                                  & " FROM TBGENERAC_PRUEBA_DEFINE WHERE (EMPRESA_CODIGO='0001') AND (PRUEBA_CODIGO = " & Session("CodPrueba") & ")"
                Dim cmdSql As New SqlCommand(Sql, Cn)
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        If Not Rs.IsDBNull(0) Then
                            If Rs!PRUEBA_TIPO_RESPUESTAS = "" Then
                            ElseIf Rs!PRUEBA_TIPO_RESPUESTAS = "1" Then
                                lblTipoRpta.Text = "1"
                                lblTipoRptaCorrecta.Text = "1" 'Nu(Rs!PRUEBA_TIPO_RPTAS_CORRECTAS)  'en este caso debe ser oblig. 1 (1 sóla rpta correcta)
                                lblFormaMarcar.Text = "1" 'obligatorio
                                lblFormaResponder.Text = "1" 'obligatorio
                            ElseIf Rs!PRUEBA_TIPO_RESPUESTAS = "2" Then
                                lblTipoRpta.Text = "2"
                                lblTipoRptaCorrecta.Text = Nu(Rs!PRUEBA_TIPO_RPTAS_CORRECTAS)
                                lblFormaMarcar.Text = Nu(Rs!PRUEBA_FORMA_MARCAR)
                                lblFormaResponder.Text = Nu(Rs!PRUEBA_FORMA_RESPONDER)
                            End If
                            If Nu(Rs!PRUEBA_ESPECIFIC1) <> "" Or Nu(Rs!PRUEBA_ESPECIFIC2) <> "" Then
                                lbl1.Visible = True
                                If Nu(Rs!PRUEBA_ESPECIFIC1) <> "" Then lblIns1.Visible = True : lbl2.Visible = True : lblIns1.InnerText = Nu(Rs!PRUEBA_ESPECIFIC1)
                                If Nu(Rs!PRUEBA_ESPECIFIC2) <> "" Then lblIns2.Visible = True : lbl3.Visible = True : lblIns2.InnerText = Nu(Rs!PRUEBA_ESPECIFIC2)
                            End If
                            If Nu(Rs!PRUEBA_TIEMPO_HRS) <> "" And Nu(Rs!PRUEBA_TIEMPO_MIN) <> "" Then
                            Else
                                If Nu(Rs!PRUEBA_PREG_OBLIGAR_RESPONDER) = "0" Then
                                    lblNroPregCont.Text = "0"
                                ElseIf Nu(Rs!PRUEBA_CONTESTAR_TODAS) = "N" Then
                                    lblNroPregCont.Text = "0"
                                ElseIf Nu(Rs!PRUEBA_CONTESTAR_TODAS) = "S" Then
                                    lblNroPregCont.Text = "T" 'TODAS
                                    lblNota.Visible = True : lblNota.InnerHtml = "&nbsp;<b>Nota : </b>Debe contestar todas las preguntas."
                                Else
                                    lblNroPregCont.Text = Nu(Rs!PRUEBA_PREG_OBLIGAR_RESPONDER)
                                End If
                            End If
                        End If
                    End While
                    Cn.Close()
                    If lblTipoRpta.Text = "" Or lblTipoRptaCorrecta.Text = "" Then
                        lblMensaje.Text = "Parámetros inválidos."
                    ElseIf lblTipoRpta.Text = "1" Then
                        Call ArmaCuestionario1()
                    ElseIf lblTipoRpta.Text = "2" Then
                        Call ArmaCuestionario2()
                    End If
                    Exit Sub
                Else
                    bolError = True
                End If
            Catch Ex As SqlException
                lblMensaje.Text = "Ha ocurrido un error" ' en el registro de la Base de Datos:<br>" & ex.Message
            Catch Ex As Exception
                lblMensaje.Text = "Ha ocurrido un error" ' la Aplicacion:<br>" & ex.Message
            Finally
                Cn.Close()
            End Try
            If bolError = True Then lblMensaje.Text = "Parámetros inválidos."
        End If
    End Sub
    Private Sub ArmaCuestionario1()
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(strConexion)
        Dim CmdGlobal As New SqlCommand
        Dim i As Integer, ii As Integer, aa As Integer
        Dim Matriz(,) As String
        Dim dt As New DataTable
        'Dim dv As DataView
        Dim MyDataRow As DataRow
        Dim Fila As DataGridItem
        Dim Pregunta As String, CodPreg As String ', RptaMarcada As String, RptaCorrecta As String
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            Flex1.AutoGenerateColumns = False
            dt.Columns.Add("PREG_CODIGO", GetType(String))
            CmdGlobal.CommandText = "SELECT PREG_CODIGO, PREG_ORDEN FROM TBGENERAC_PRUEBA_PREGUNTAS_0001 WHERE (PRUEBA_CODIGO =" & Session("CodPrueba") & ") AND (PREG_SYS_EST = '0') ORDER BY PREG_ORDEN"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                Flex1.Visible = True
                GuardarRptas.Visible = True
                Cancelar.Visible = True
                While Rs.Read
                    MyDataRow = dt.NewRow()
                    MyDataRow(0) = Rs!PREG_CODIGO
                    dt.Rows.Add(MyDataRow)
                End While
            End If
            Rs.Close()
            Flex1.DataSource = New DataView(dt)
            Flex1.DataBind()
            For i = 0 To Flex1.Items.Count - 1 'recorrido de filas
                ii = 0 : Pregunta = "" : CodPreg = "" ': RptaMarcada = "" : RptaCorrecta = ""
                Fila = Flex1.Items(i)
                CmdGlobal.CommandText = "SELECT P.PREG_CODIGO, P.PREG_DESCRIPCION, P.PREG_ORDEN, R.RESP_CODIGO, R.RESP_DESCRIPCION, R.RESP_ORDEN" _
                                        & " FROM TBGENERAC_PRUEBA_PREGUNTAS_0001 P INNER JOIN TBGENERAC_PRUEBA_RESPUESTAS_0001 R ON P.PRUEBA_CODIGO = R.PRUEBA_CODIGO AND P.PREG_CODIGO = R.PREG_CODIGO" _
                                        & " WHERE (P.PREG_SYS_EST = '0') AND (P.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (R.RESP_SYS_EST = '0') AND (P.PREG_CODIGO = " & Fila.Cells(0).Text & ") " _
                                        & " ORDER BY R.RESP_ORDEN"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then 'obtener contador de rptas para la matriz
                    While Rs.Read
                        ii = ii + 1
                        Pregunta = "<b>" & (i + 1).ToString & ".-  </b> " & Nu(Rs!PREG_DESCRIPCION)
                        CodPreg = Nu(Rs!PREG_CODIGO)
                        'If Nu(Rs!RESP_CORRECTA) = "X" Then RptaCorrecta = Nu(Rs!RESP_CODIGO)
                    End While
                End If
                Rs.Close()
                Rs = CmdGlobal.ExecuteReader 'colocar en la matriz las respuestas
                If Rs.HasRows Then
                    ReDim Matriz(ii - 1, 1)
                    aa = -1
                    While Rs.Read
                        aa = aa + 1
                        Matriz(aa, 0) = Nu(Rs!RESP_ORDEN) & ".  " & Nu(Rs!RESP_DESCRIPCION)
                        Matriz(aa, 1) = Rs!RESP_CODIGO
                    End While
                    Dim lblPreg As Label = CType(Fila.FindControl("lblPregunta"), Label)
                    lblPreg.Text = Pregunta
                    Dim lblPreg2 As Label = CType(Fila.FindControl("Preg"), Label)
                    lblPreg2.Text = CodPreg
                    lblPreg2.Visible = False
                    Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas1"), RadioButtonList)
                    Rptas.Items.Clear() 'borra las opciones de la fila
                    For aa = 0 To ii - 1
                        Dim Rpta As ListItem = New ListItem
                        Rpta.Text = Matriz(aa, 0).ToString
                        Rpta.Value = Matriz(aa, 1).ToString
                        Rpta.Selected = False
                        Rptas.Items.Add(Rpta)
                    Next
                End If
                Rs.Close()
            Next
            'Call Muestra_Resultado()
        Catch Ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error" ' en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            lblMensaje.Text = "Ha ocurrido un error" ' la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub ArmaCuestionario2()
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(strConexion)
        Dim i As Integer, ii As Integer, aa As Integer
        Dim Matriz(,) As String
        Dim cmdSql As New SqlCommand
        Cn.Open()
        cmdSql.Connection = Cn
        Flex.DataSource = Arma_Preguntas(cmdSql)
        Flex.DataBind()
        'armado de las option de respuestas
        cmdSql.CommandText = "SELECT DISTINCT RESP_DESCRIPCION, RESP_CODIGO, RESP_ORDEN FROM TBGENERAC_PRUEBA_RESPUESTAS_0001  " _
                             & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (RESP_SYS_EST = '0') ORDER BY RESP_ORDEN"
        Rs = cmdSql.ExecuteReader
        If Rs.HasRows Then 'obtener contador de rptas para la matriz
            ii = 0
            While Rs.Read
                ii = ii + 1
            End While
        End If
        Rs.Close()
        Rs = cmdSql.ExecuteReader 'colocar en la matriz las respuestas
        If Rs.HasRows Then
            ReDim Matriz(ii - 1, 1)
            i = -1
            While Rs.Read
                i = i + 1
                Matriz(i, 0) = Rs!RESP_DESCRIPCION
                Matriz(i, 1) = Rs!RESP_CODIGO
            End While
            Rs.Close()
            Dim Fila As DataGridItem
            For i = 0 To Flex.Items.Count - 1 'recorrido de filas
                Fila = Flex.Items(i)
                Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas"), RadioButtonList)
                If Not Rptas Is Nothing Then
                    Rptas.Items.Clear() 'borra las opciones de la fila
                    For aa = 0 To ii - 1
                        Dim Rpta As ListItem = New ListItem
                        Rpta.Text = Matriz(aa, 0).ToString
                        Rpta.Value = Matriz(aa, 1).ToString
                        Rpta.Selected = False
                        Rptas.Items.Add(Rpta)
                    Next
                End If
            Next
        Else
            Rs.Close()
        End If
        Cn.Close()
        'Call Muestra_Resultado()
    End Sub
    Private Function Arma_Preguntas(ByVal cmdSql As SqlCommand) As ICollection
        Dim Rs As SqlDataReader
        'Dim dv As DataView
        Dim dt As New DataTable
        Dim MyDataRow As DataRow
        Flex.AllowSorting = False
        Flex.AutoGenerateColumns = False
        dt.Columns.Add(New DataColumn("PREG_ORDEN", GetType(Integer)))
        dt.Columns.Add(New DataColumn("PREG_DESCRIPCION", GetType(String)))
        dt.Columns.Add(New DataColumn("PREG_CODIGO", GetType(Integer)))
        cmdSql.CommandText = "SELECT PREG_ORDEN, PREG_DESCRIPCION, PREG_CODIGO FROM TBGENERAC_PRUEBA_PREGUNTAS_0001 WHERE (PREG_SYS_EST = '0') AND (PRUEBA_CODIGO =" & Session("CodPrueba") & ")  ORDER BY PREG_ORDEN"
        Rs = cmdSql.ExecuteReader
        If Rs.HasRows Then
            Flex.Visible = True
            GuardarRptas.Visible = True
            Cancelar.Visible = True
            While Rs.Read
                MyDataRow = dt.NewRow()
                MyDataRow(0) = Rs!PREG_ORDEN
                MyDataRow(1) = Rs!PREG_DESCRIPCION
                MyDataRow(2) = Rs!PREG_CODIGO
                dt.Rows.Add(MyDataRow)
            End While
        End If
        Rs.Close()
        Arma_Preguntas = New DataView(dt)
    End Function
    Private Sub GuardarRptas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarRptas.Click
        Dim Fecha As String = FechaActual()
        Dim Hora As String = HoraActual()
        Dim ValorSys As String = Fecha & Hora & User.Identity.Name
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(strConexion)
        Dim Cn2 As New SqlConnection(strConexion)
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim NumAnonimo As Double
        Dim i As Integer, n As Integer
        Dim Fila As DataGridItem
        lblMensaje2.Text = ""
        Try
            Cn.Open()
            Cn2.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_GPE_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[w_GPE_" & Session.SessionID & "]"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "CREATE VIEW w_GPE_" & Session.SessionID & " AS SELECT P.PRUEBA_CODIGO,P.PREG_CODIGO, R.RESP_CODIGO " _
                                 & " FROM TBGENERAC_PRUEBA_PREGUNTAS_0001 P INNER JOIN TBGENERAC_PRUEBA_RESPUESTAS_0001 R ON P.PRUEBA_CODIGO = R.PRUEBA_CODIGO AND P.PREG_CODIGO = R.PREG_CODIGO " _
                                 & " WHERE (P.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (P.PREG_SYS_EST = '0') AND (R.RESP_SYS_EST = '0')"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "SELECT MAX(PD_NUM_ANONIMO) FROM TBGENERAC_PRUEBA_DESARROLLO_0001"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    NumAnonimo = Nz(Rs(0)) + 1
                End While
            Else
                NumAnonimo = 1
            End If
            Rs.Close()
            cmdSql.CommandText = "INSERT INTO TBGENERAC_PRUEBA_DESARROLLO_0001 (PRUEBA_CODIGO, GRUPO_CODIGO, GRUPO_TIPO,PD_NUM_ANONIMO, PD_ESTADO, PD_SYS_EST, PD_SYS_CRE,PD_SESSIONID) " _
                                     & "VALUES (" & Session("CodPrueba") & "," & Session("CodGrupo") & ",'6','" & NumAnonimo & "','1','0','" & ValorSys & "','" & Session.SessionID & "')"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "INSERT INTO TBGENERAC_PRUEBA_DESA_DETALLE_0001 (PRUEBA_CODIGO,GRUPO_CODIGO, GRUPO_TIPO,PD_NUM_ANONIMO, PREG_CODIGO, RESP_CODIGO,DD_SYS_CRE, DD_SYS_EST,PD_SESSIONID) " _
                                   & " SELECT PRUEBA_CODIGO," & Session("CodGrupo") & ",'6','" & NumAnonimo & "', PREG_CODIGO, RESP_CODIGO, '" & ValorSys & "', '0','" & Session.SessionID & "' FROM w_GPE_" & Session.SessionID & ""
            cmdSql.ExecuteNonQuery()
            If lblTipoRpta.Text = "1" Then  'rptas diferentes
                With Flex1
                    cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_0001 SET DD_RESPONDIDO=NULL " _
                                         & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                         & " AND (PD_NUM_ANONIMO= '" & NumAnonimo & "')"
                    cmdSql.ExecuteNonQuery()
                    For i = 0 To .Items.Count - 1 'recorrido de filas
                        Fila = .Items(i)
                        Dim CodPreg As Label = CType(Fila.FindControl("Preg"), Label)
                        If CodPreg.Text <> "" Then
                            Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas1"), RadioButtonList)
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then
                                    cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_0001 SET DD_RESPONDIDO='X' " _
                                                         & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                                         & " AND (PD_NUM_ANONIMO = '" & NumAnonimo & "') AND (PREG_CODIGO = " & CodPreg.Text & ") AND (RESP_CODIGO = " & Rpta.Value & ") AND (DD_SYS_EST = '0')"
                                    cmdSql.ExecuteNonQuery()
                                End If
                            Next
                        End If
                    Next
                End With
            ElseIf lblTipoRpta.Text = "2" Then  'rptas iguales
                With Flex
                    cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_0001 SET DD_RESPONDIDO=NULL " _
                                         & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                         & " AND (PD_NUM_ANONIMO='" & NumAnonimo & "')"
                    cmdSql.ExecuteNonQuery()
                    For i = 0 To .Items.Count - 1 'recorrido de filas
                        Fila = .Items(i)
                        Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas"), RadioButtonList)
                        For n = 0 To Rptas.Items.Count - 1
                            Dim Rpta As ListItem = New ListItem
                            Rpta = Rptas.Items(n)
                            If Rpta.Selected = True Then
                                cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_0001 SET DD_RESPONDIDO='X' " _
                                                     & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                                     & " AND (PD_NUM_ANONIMO = '" & NumAnonimo & "') AND (PREG_CODIGO = " & Fila.Cells(2).Text & ") AND (RESP_CODIGO = " & Rpta.Value & ") AND (DD_SYS_EST = '0')"
                                cmdSql.ExecuteNonQuery()
                            End If
                        Next
                    Next
                End With
            End If
            'código considerando que se permite marcar una sóla respuesta
            Dim Ppo As Double, Nota As Double
            Nota = -1
            cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_0001 SET DD_PUNT_PREG_OBTENIDO=NULL " _
                                 & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                 & " AND (PD_NUM_ANONIMO = '" & NumAnonimo & "')"
            cmdSql.ExecuteNonQuery()
            cmdSql2.Connection = Cn2
            If lblTipoRptaCorrecta.Text <> "" Then
                'listar las preguntas respondidas y a la vez q sean las correctas
                cmdSql.CommandText = "SELECT PD.PREG_CODIGO,PD.RESP_CODIGO,(SELECT PREG.RESP_VALOR FROM TBGENERAC_PRUEBA_RESPUESTAS_0001  PREG WHERE PREG.PRUEBA_CODIGO=PD.PRUEBA_CODIGO AND PREG.RESP_SYS_EST='0' AND PREG.PREG_CODIGO=PD.PREG_CODIGO AND PREG.RESP_CODIGO=PD.RESP_CODIGO) AS VALOR_RPTA " _
                    & " From TBGENERAC_PRUEBA_DESA_DETALLE_0001 PD " _
                    & " WHERE (PD.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (PD.GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (PD.GRUPO_TIPO = '6') " _
                    & " AND (PD.PD_NUM_ANONIMO = '" & NumAnonimo & "') AND (PD.DD_SYS_EST = '0')  AND (PD.DD_RESPONDIDO = 'X') AND (SELECT PREG.RESP_CORRECTA FROM TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & "  PREG WHERE PREG.PRUEBA_CODIGO=PD.PRUEBA_CODIGO AND PREG.RESP_SYS_EST='0' AND PREG.PREG_CODIGO=PD.PREG_CODIGO AND PREG.RESP_CODIGO=PD.RESP_CODIGO)='X' ORDER BY PD.PREG_CODIGO, PD.RESP_CODIGO"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    Nota = 0
                    While Rs.Read
                        Nota = Nota + Nz(Rs!VALOR_RPTA)
                        If Nu(Rs!VALOR_RPTA) <> "" Then Ppo = Nz(Rs!VALOR_RPTA) Else Ppo = -1
                        cmdSql2.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_0001 SET DD_PUNT_PREG_OBTENIDO=" & IIf(Ppo = -1, "NULL", Ppo) & " " _
                                             & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                             & " AND (PD_NUM_ANONIMO= '" & NumAnonimo & "') AND (DD_SYS_EST = '0')  AND (PREG_CODIGO = " & Rs!PREG_CODIGO & ")"
                        cmdSql2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
            End If
            cmdSql2.CommandText = "UPDATE TBGENERAC_PRUEBA_DESARROLLO_0001 SET PD_ESTADO='3',PD_FECHA_DESA='" & Fecha & "', PD_HORA_DESA='" & Left(Hora, 4) & "',PD_PRUEBA_NOTA=" & IIf(Nota = -1, "NULL", Nota) & "" _
                                 & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                                 & " AND (PD_NUM_ANONIMO = '" & NumAnonimo & "') AND (PD_SYS_EST = '0')"
            cmdSql2.ExecuteNonQuery()
            cmdSql2.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_GPE_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[w_GPE_" & Session.SessionID & "]"
            cmdSql2.ExecuteNonQuery()
            'lblTitulo.Visible = False
            'lblTitulo2.Visible = False
            Flex.Enabled = False
            Flex1.Enabled = False
            GuardarRptas.Enabled = False
            Cancelar.Enabled = False
            If UCase(Session("TipoPrueba")) = "PRUEBA" Then
                Call Muestra_Resultado(NumAnonimo)
            End If
            lblMensaje2.Text = "El desarrollo de la " & Session("TipoPrueba") & " ya ha sido enviada. Gracias."
            HyperLink1.Visible = True
        Catch Ex As SqlException
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn2.Close()
            Cn.Close()
        End Try
    End Sub
    Private Sub Muestra_Resultado(ByVal NumAnonimo As Double)
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(strConexion)
        Dim cmdSql As New SqlCommand

        Cn.Open()
        cmdSql.Connection = Cn
        Dim lblNota As String = "", lblNombreEscala As String = ""
        Dim VerPuntTotal As String = "", VerPuntTotal_TipoConver As String = ""
        Dim VerPuntSGrupo As String = "", VerPuntSGrupo_TipoConver As String = ""
        cmdSql.CommandText = "SELECT PRUEBA_PREG_OBLIGAR_RESPONDER,PRUEBA_OBTENER_PUNT_TOTAL,PRUEBA_OBTENER_PUNT_SUBGPO,PRUEBA_PUNT_TOTAL_TIPO_CONVER_RESULT,PRUEBA_PUNT_SUBGPO_TIPO_CONVER_RESULT " _
                              & " FROM TBGENERAC_PRUEBA_DEFINE WHERE (EMPRESA_CODIGO='0001') AND PRUEBA_CODIGO='" & Session("CodPrueba") & "'"
        Rs = cmdSql.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                VerPuntTotal = Nu(Rs!PRUEBA_OBTENER_PUNT_TOTAL)
                VerPuntTotal_TipoConver = Nu(Rs!PRUEBA_PUNT_TOTAL_TIPO_CONVER_RESULT)
                VerPuntSGrupo = Nu(Rs!PRUEBA_OBTENER_PUNT_SUBGPO)
                VerPuntSGrupo_TipoConver = Nu(Rs!PRUEBA_PUNT_SUBGPO_TIPO_CONVER_RESULT)
            End While
        End If
        Rs.Close()
        If VerPuntTotal = "S" Then
            lblResultado.Visible = True
            lblResultado.InnerText = ""
            lblNota = ""
            cmdSql.CommandText = "SELECT PD_PRUEBA_NOTA FROM TBGENERAC_PRUEBA_DESARROLLO_0001 " _
                & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '6') " _
                & " AND (PD_NUM_ANONIMO = '" & NumAnonimo & "') AND (PD_SYS_EST = '0')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNota = Nu(Rs(0))
                End While
            End If
            Rs.Close()
            If lblNota <> "" Then
                If VerPuntTotal_TipoConver = "1" Then
                    cmdSql.CommandText = "SELECT CR_ESCALA_NOMBRE FROM TBGENERAC_PRUEBA_CONVER_RESULT " _
                         & " WHERE (EMPRESA_CODIGO='0001') AND (CR_SYS_EST = '0') AND (CR_TIPO_RESULTADO = '1') AND " _
                         & " (CR_TIPO_CONVERSION = '1') AND (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND " _
                         & " (CR_ESCALA_VALOR_INI <= " & lblNota & ") AND (CR_ESCALA_VALOR_FIN >=" & lblNota & ")"
                    Rs = cmdSql.ExecuteReader
                    If Rs.HasRows = True Then
                        While Rs.Read
                            lblNombreEscala = Nu(Rs(0))
                        End While
                    End If
                    Rs.Close()
                Else
                    'Sql = "SELECT CR_CODIGO_EVAL_PSICO FROM TBGENERAC_PRUEBA_CONVER_RESULT WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (PRUEBA_CODIGO =" & lblCodPrueba & ") " _
                    '     & " AND (CR_TIPO_RESULTADO = '1') AND (CR_TIPO_CONVERSION = '2') AND (CR_SYS_EST = '0')"
                    'Rs3.Open(Sql, Cn, adOpenKeyset, adLockOptimistic)
                    'If Rs3.RecordCount > 0 Then
                    '    Sql = "SELECT PERCT_CATEGORIA, PERCT_PERCENTIL From TBEVAL_PSICOP_CONVER_TOTAL WHERE (PERCT_COD_GRUPO='" & Nu(Rs3!CR_CODIGO_EVAL_PSICO) & "') AND " _
                    '       & "((PERCT_PUNTAJE1 = " & lblNota & ") OR (PERCT_PUNTAJE2 = " & lblNota & ") OR (" & lblNota & " >= PERCT_PUNTAJE1 AND " & lblNota & " <= PERCT_PUNTAJE2))"
                    '    Rs2.Open(Sql, Cn, adOpenKeyset, adLockOptimistic)
                    '    If Rs2.RecordCount > 0 Then
                    '        lblNombreEscala = "Calificación : " & Nu(Rs2!PERCT_PERCENTIL) & "     Categoría : " & Nu(Rs2!PERCT_CATEGORIA)
                    '    End If
                    '    Rs2.Close()
                    'End If
                    'Rs3.Close()
                End If
                lblResultado.InnerHtml = "Resultado Final : " & lblNota
                If lblNombreEscala <> "" Then lblResultado.InnerText = lblResultado.InnerText & " - " & lblNombreEscala
            End If
        End If
        If VerPuntSGrupo = "S" Then
            If VerPuntSGrupo_TipoConver <> "1" Then Cn.Close() : Exit Sub
            FlexResultado.Visible = True
            FlexResultado.Columns.Clear()
            FlexResultado.DataBind()
            Dim dt As New DataTable
            'Dim dv As DataView
            Dim MyDataRow As DataRow
            Dim cmdSql1 As New SqlClient.SqlCommand
            Dim Cn2 As New SqlConnection(strConexion)
            Dim Rs2 As SqlClient.SqlDataReader
            Dim i As Integer
            If VerPuntSGrupo_TipoConver = "1" Then
                dt.Columns.Add("#", GetType(String))
                dt.Columns.Add("Agrupación de Preguntas", GetType(String))
                dt.Columns.Add("Resultado", GetType(String))
                dt.Columns.Add("Escala Literal", GetType(String))
            End If
            Cn2.Open()
            cmdSql1.Connection = Cn2
            cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_ObtPunt_SGpo_Preg_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[w_ObtPunt_SGpo_Preg_" & Session.SessionID & "]"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "CREATE VIEW w_ObtPunt_SGpo_Preg_" & Session.SessionID & " AS SELECT DD.PD_NUM_ANONIMO,DD.PRUEBA_CODIGO,AVG(DD.DD_PUNT_PREG_OBTENIDO) AS PuntajexPreg,P.GPOPREG_CODIGO, DD.PREG_CODIGO " _
                                  & " FROM TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " DD INNER JOIN TBGENERAC_PRUEBA_PREGUNTAS_0004 P ON DD.PRUEBA_CODIGO = P.PRUEBA_CODIGO AND DD.PREG_CODIGO = P.PREG_CODIGO  " _
                                  & " GROUP BY DD.PD_NUM_ANONIMO, DD.PRUEBA_CODIGO,DD.GRUPO_CODIGO, DD.GRUPO_TIPO, DD.DD_SYS_EST,P.PREG_SYS_EST, P.GPOPREG_CODIGO,DD.PREG_CODIGO  " _
                                  & " HAVING (DD.PD_NUM_ANONIMO = '" & NumAnonimo & "') AND (DD.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (DD.GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (DD.GRUPO_TIPO = '6') AND (DD.DD_SYS_EST = '0') AND (P.PREG_SYS_EST = '0') "
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "SELECT V.PD_NUM_ANONIMO, V.PRUEBA_CODIGO, SUM(V.PuntajexPreg) AS PuntajexSGpo, V.GPOPREG_CODIGO,GP.GPOPREG_NOMBRE " _
                  & " FROM w_ObtPunt_SGpo_Preg_" & Session.SessionID & " V INNER JOIN TBGENERAC_PRUEBA_PREG_AGRUPA GP ON V.PRUEBA_CODIGO = GP.PRUEBA_CODIGO AND V.GPOPREG_CODIGO = GP.GPOPREG_CODIGO WHERE GP.EMPRESA_CODIGO='0004' " _
                  & " GROUP BY V.PD_NUM_ANONIMO, V.PRUEBA_CODIGO,V.GPOPREG_CODIGO, GP.GPOPREG_SYS_EST,GP.GPOPREG_NOMBRE " _
                  & " HAVING (GP.GPOPREG_SYS_EST = '0') ORDER BY GP.GPOPREG_NOMBRE"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                i = 0
                While Rs.Read
                    MyDataRow = dt.NewRow()
                    i = i + 1
                    MyDataRow(0) = i.ToString
                    MyDataRow(1) = Nu(Rs!GPOPREG_NOMBRE)
                    MyDataRow(2) = IIf(Nu(Rs!PuntajexSGpo) = "", "", Nz(Rs!PuntajexSGpo))
                    If MyDataRow(2) <> "" Then
                        If VerPuntSGrupo_TipoConver = "1" Then
                            cmdSql1.CommandText = "SELECT CR_ESCALA_NOMBRE FROM TBGENERAC_PRUEBA_CONVER_RESULT " _
                                 & " WHERE (EMPRESA_CODIGO='0001') AND (CR_SYS_EST = '0') AND (CR_TIPO_RESULTADO = '2') AND " _
                                 & " (CR_TIPO_CONVERSION = '1') AND (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GPOPREG_CODIGO='" & Nu(Rs!GPOPREG_CODIGO) & "') AND " _
                                 & " (CR_ESCALA_VALOR_INI <= " & MyDataRow(2) & ") AND (CR_ESCALA_VALOR_FIN >=" & MyDataRow(2) & ")"
                            Rs2 = cmdSql1.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    MyDataRow(3) = Nu(Rs2(0))
                                End While
                            End If
                            Rs2.Close()
                        Else
                            'codigo no valido, se relaciona con el modulo sicopedagogico
                            'Sql = "SELECT CR_CODIGO_EVAL_PSICO, CR_CODIGO_HAB FROM TBGENERAC_PRUEBA_CONVER_RESULT " _
                            '    & " WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (PRUEBA_CODIGO = " & lblCodPrueba & ") AND (CR_TIPO_RESULTADO = '2') AND (CR_TIPO_CONVERSION = '2') " _
                            '    & " AND (GPOPREG_CODIGO = " & Nu(Rs1!GPOPREG_CODIGO) & ") AND (CR_SYS_EST = '0')"
                            'Rs3.Open(Sql, Cn, adOpenKeyset, adLockOptimistic)
                            'If Rs3.RecordCount > 0 Then
                            '    Sql = "SELECT PERC_CATEGORIA, PERC_PERCENTIL From TBEVAL_PSICOP_CONVERSION WHERE (PERC_GRUPO_HAB='" & Nu(Rs3!CR_CODIGO_EVAL_PSICO) & "') AND " _
                            '        & "(PERC_HAB = '" & Nu(Rs3!CR_CODIGO_HAB) & "') AND " _
                            '        & "((PERC_PUNTAJE1 = '" & .TextMatrix(.Rows - 1, 2) & "') OR (PERC_PUNTAJE2 = '" & .TextMatrix(.Rows - 1, 2) & "') OR " _
                            '        & " ('" & .TextMatrix(.Rows - 1, 2) & "' >= PERC_PUNTAJE1 AND '" & .TextMatrix(.Rows - 1, 2) & "' <= PERC_PUNTAJE2))"
                            '    Rs2.Open(Sql, Cn, adOpenKeyset, adLockOptimistic)
                            '    If Rs2.RecordCount > 0 Then
                            '        .TextMatrix(.Rows - 1, 3) = Nu(Rs2!PERC_PERCENTIL)
                            '        .TextMatrix(.Rows - 1, 4) = Nu(Rs2!PERC_CATEGORIA)
                            '    End If
                            '    Rs2.Close()
                            'End If
                            'Rs3.Close()
                        End If
                    End If
                    dt.Rows.Add(MyDataRow)
                End While
            End If
            Rs.Close()
            cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_ObtPunt_SGpo_Preg_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[w_ObtPunt_SGpo_Preg_" & Session.SessionID & "]"
            cmdSql.ExecuteNonQuery()
            Cn2.Close()
            FlexResultado.DataSource = New DataView(dt)
            FlexResultado.DataBind()
        End If
        Cn.Close()
    End Sub
End Class
