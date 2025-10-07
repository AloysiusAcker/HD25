Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class Encuesta_Des
    Inherits System.Web.UI.Page
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents DIV1 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Div2 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents DIV3 As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents Div4 As System.Web.UI.HtmlControls.HtmlGenericControl

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
            lblNota.Visible = False
            lblResultado.Visible = False
            FlexResultado.Visible = False
            lblMensaje2.Text = ""
            HyperLink1.Visible = False
            HyperLink1.Text = "Continuar con otra " & Session("TipoPrueba")
            lblTitulo.InnerText = "Desarrollo de la " & Session("TipoPrueba") & " Nº " & Session("CodPrueba")
            lblTitulo2.InnerText = "''" & Session("NomPrueba") & "''"
            Flex222.Visible = False : FlexLey.Visible = False
            GuardarRptas.Visible = False : GuardarRptas2.Visible = False
            Cancelar.Visible = False : Cancelar2.Visible = False
            lblTipoRpta.Text = "" : lblTipoRptaCorrecta.Text = "" : lblFormaMarcar.Text = "" : lblFormaResponder.Text = ""
            lbl1.Visible = False : lbl2.Visible = False : lbl3.Visible = False
            lblIns1.Visible = False : lblIns2.Visible = False
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim Rs As SqlDataReader
            Dim bolError As Boolean
            Try
                Cn.Open()
                Dim Sql As String = "SELECT PRUEBA_TIPO_RESPUESTAS,PRUEBA_TIPO_RPTAS_CORRECTAS,PRUEBA_ORGANIZ_PORGRUPO,PRUEBA_ESPECIFIC1,PRUEBA_ESPECIFIC2,PRUEBA_FORMA_MARCAR,PRUEBA_FORMA_RESPONDER, " _
                                  & "PRUEBA_CONTESTAR_TODAS,PRUEBA_PREG_OBLIGAR_RESPONDER,PRUEBA_TIEMPO_HRS,PRUEBA_TIEMPO_MIN " _
                                  & " FROM TBGENERAC_PRUEBA_DEFINE WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (PRUEBA_CODIGO = " & Session("CodPrueba") & ")"
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
                                    lblNota.Visible = True : lblNota.InnerHtml = "&nbsp;<b>Nota : </b>Debe responder todas las preguntas."
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
                'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
            Catch Ex As Exception
                'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
            Finally
                Cn.Close()
            End Try
            If bolError = True Then lblMensaje.Text = "Parámetros inválidos."
        End If
    End Sub
    Private Sub ArmaCuestionario1()
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim i As Integer, ii As Integer, aa As Integer
        Dim Matriz(,) As String
        Dim dt As New DataTable
        'Dim ddv As DataView
        Dim MyDataRow As DataRow
        Dim Fila As DataGridItem
        Dim Pregunta As String, CodPreg As String ', RptaMarcada As String, RptaCorrecta As String
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            Flex1.AutoGenerateColumns = False
            dt.Columns.Add("PREG_CODIGO", GetType(String))
            CmdGlobal.CommandText = "SELECT PREG_CODIGO, PREG_ORDEN FROM TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " WHERE (PRUEBA_CODIGO =" & Session("CodPrueba") & ") AND (PREG_SYS_EST = '0') ORDER BY PREG_ORDEN"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                Flex1.Visible = True
                GuardarRptas.Visible = True : GuardarRptas2.Visible = True
                Cancelar.Visible = True : Cancelar2.Visible = True
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
                                        & " FROM TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " P INNER JOIN TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & " R ON P.PRUEBA_CODIGO = R.PRUEBA_CODIGO AND P.PREG_CODIGO = R.PREG_CODIGO" _
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
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim i As Integer, ii As Integer, aa As Integer
        Dim Matriz(,) As String
        'Dim MatrizMV(,) As String, mv As Integer, Nmv As Integer

        Cn.Open()
        cmdSql.Connection = Cn
        If lblFormaMarcar.Text = "2" Then  'marcacion multiple
            If lblFormaResponder.Text = "1" Then 'responder en check
                Flex.AllowSorting = False
                Flex.AutoGenerateColumns = False
                Flex.Columns(3).Visible = False 'columa option 
                Flex.Columns(4).Visible = True 'columna check
                Flex.DataSource = Arma_Preguntas(cmdSql)
                Flex.DataBind()
            ElseIf lblFormaResponder.Text = "2" Then 'responder en combos
                Flex222.AllowSorting = False
                Flex222.AutoGenerateColumns = False
                For ii = 3 To Flex222.Columns.Count - 1
                    Flex222.Columns(ii).Visible = False
                Next
                Flex222.DataSource = Arma_Preguntas(cmdSql)
                Flex222.DataBind()
                FlexLey.Visible = True
                FlexLey.DataSource = Arma_MenuValores(cmdSql, "", 2)
                FlexLey.DataBind()
            End If
        Else
            Flex.AllowSorting = False
            Flex.AutoGenerateColumns = False
            Flex.Columns(3).Visible = True 'columa option 
            Flex.Columns(4).Visible = False 'columna check
            Flex.DataSource = Arma_Preguntas(cmdSql)
            Flex.DataBind()
        End If
        'armado de las option de respuestas
        cmdSql.CommandText = "SELECT DISTINCT RESP_DESCRIPCION, RESP_CODIGO, RESP_ORDEN FROM TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & "  " _
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
            If lblFormaMarcar.Text = "2" Then  'marcación multiple
                If lblFormaResponder.Text = "1" Then 'responder en check
                    Dim Fila As DataGridItem
                    For i = 0 To Flex.Items.Count - 1 'recorrido de filas
                        Fila = Flex.Items(i)
                        Dim Rptas As CheckBoxList = CType(Fila.FindControl("chkRpta"), CheckBoxList)
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
                ElseIf lblFormaResponder.Text = "2" Then ' responder en combos
                    For aa = 0 To ii - 1 'habilitar las columnas a mostrar
                        Flex222.Columns(aa + 3).Visible = True
                        Flex222.Columns(aa + 3).HeaderText = Matriz(aa, 0).ToString 'descrip rpta
                        Flex222.Columns(aa + 3).ItemStyle.VerticalAlign = VerticalAlign.Middle
                        Flex222.Columns(aa + 3).ItemStyle.HorizontalAlign = HorizontalAlign.Center
                        'Rpta.Value = Matriz(aa, 1).ToString
                        Flex222.DataBind()
                    Next
                    Dim Fila As DataGridItem
                    For i = 0 To Flex222.Items.Count - 1 'recorrido de filas
                        For aa = 0 To ii - 1
                            Fila = Flex222.Items(i)
                            Dim Combo As DropDownList = CType(Fila.FindControl("cbo" & (aa + 1) & ""), DropDownList)
                            If Not Combo Is Nothing Then
                                Combo.Width = Unit.Pixel(40)
                                Combo.Items.Clear()
                                Combo.DataSource = Arma_MenuValores(cmdSql, Matriz(aa, 1).ToString, 1)
                                Combo.DataTextField = "LETRA"
                                Combo.DataValueField = "CODIGO"
                                Combo.DataBind()
                                Combo.ToolTip = "Menú de Valores"
                            End If
                        Next
                    Next
                End If
            Else
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
            End If
        Else
            Rs.Close()
        End If
        Cn.Close()
    End Sub
    Private Function Arma_MenuValores(ByVal cmdSql As SqlCommand, ByVal CodResp As String, ByVal f As Integer) As ICollection
        Dim Rs As SqlDataReader

        Dim dt As New DataTable
        Dim MyDataRow As DataRow

        If f = 1 Then
            dt.Columns.Add(New DataColumn("LETRA", GetType(String)))
            dt.Columns.Add(New DataColumn("CODIGO", GetType(String)))
            MyDataRow = dt.NewRow()
            MyDataRow(0) = "- -"
            MyDataRow(1) = "R" & CodResp
            dt.Rows.Add(MyDataRow)
        Else
            dt.Columns.Add(New DataColumn("C1", GetType(String)))
        End If
        cmdSql.CommandText = "SELECT MVALOR_CODIGO, MVALOR_LETRA, MVALOR_NOMBRE, MVALOR_VALOR_INI, MVALOR_VALOR_FIN " _
                           & " FROM TBGENERAC_PRUEBA_MENU_VALOR WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (MVALOR_SYS_EST = '0') ORDER BY MVALOR_VALOR_INI DESC, MVALOR_VALOR_FIN DESC,MVALOR_LETRA ASC"
        Rs = cmdSql.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                MyDataRow = dt.NewRow()
                If f = 1 Then
                    MyDataRow(0) = Nu(Rs!MVALOR_LETRA)
                    MyDataRow(1) = Nu(Rs!MVALOR_CODIGO)
                Else
                    MyDataRow(0) = "<b>" & Nu(Rs!MVALOR_LETRA) & "</b>&nbsp;&nbsp;&nbsp;" & Nu(Rs!MVALOR_NOMBRE)
                    If Nu(Rs!MVALOR_VALOR_INI) <> "" And Nu(Rs!MVALOR_VALOR_FIN) <> "" Then
                        If Nu(Rs!MVALOR_VALOR_INI) = Nu(Rs!MVALOR_VALOR_FIN) Then
                            MyDataRow(0) = MyDataRow(0) & " (" & Nu(Rs!MVALOR_VALOR_INI) & ")"
                        Else
                            MyDataRow(0) = MyDataRow(0) & " (" & Nu(Rs!MVALOR_VALOR_INI) & " - " & Nu(Rs!MVALOR_VALOR_FIN) & ")"
                        End If
                    End If
                End If
                dt.Rows.Add(MyDataRow)
            End While
        End If
        Rs.Close()
        Arma_MenuValores = New DataView(dt)
    End Function
    Private Function Arma_Preguntas(ByVal cmdSql As SqlCommand) As ICollection
        Dim Rs As SqlDataReader
        Dim dt As New DataTable
        Dim MyDataRow As DataRow

        dt.Columns.Add(New DataColumn("PREG_ORDEN", GetType(Integer)))
        dt.Columns.Add(New DataColumn("PREG_DESCRIPCION", GetType(String)))
        dt.Columns.Add(New DataColumn("PREG_CODIGO", GetType(Integer)))
        cmdSql.CommandText = "SELECT PREG_ORDEN, PREG_DESCRIPCION, PREG_CODIGO FROM TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " WHERE (PREG_SYS_EST = '0') AND (PRUEBA_CODIGO =" & Session("CodPrueba") & ")  ORDER BY PREG_ORDEN"
        Rs = cmdSql.ExecuteReader
        If Rs.HasRows Then
            If lblFormaMarcar.Text = "2" And lblFormaResponder.Text = "2" Then
                Flex222.Visible = True
            Else
                Flex.Visible = True
            End If
            GuardarRptas.Visible = True : GuardarRptas2.Visible = True
            Cancelar.Visible = True : Cancelar2.Visible = True
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
        Dim i As Integer, n As Integer, NSes As Integer, nn As Integer
        Dim Fila As DataGridItem
        lblMensaje.Text = ""
        If lblNroPregCont.Text <> "0" And lblNroPregCont.Text <> "" Then
            If PregContestadas() = False Then Exit Sub
        End If
        'obligar a responder todos los combos por preg cuando se trata de 2 2 2
        Dim Contestada As Boolean
        If lblTipoRpta.Text = "2" Then  'rptas iguales
            If lblFormaMarcar.Text = "2" Then  'marcacion multiple
                If lblFormaResponder.Text = "2" Then 'responder con combos
                    For i = 0 To Flex222.Items.Count - 1 'recorrido de filas
                        Contestada = False
                        For n = 3 To Flex222.Columns.Count - 1
                            If Flex222.Columns(n).Visible = True Then
                                Fila = Flex222.Items(i)
                                Dim Combo As DropDownList = CType(Fila.FindControl("cbo" & (n - 2) & ""), DropDownList)
                                If Not Combo Is Nothing Then
                                    If Left(Combo.SelectedValue, 1) = "R" Then
                                    Else
                                        Contestada = True : Exit For
                                    End If
                                End If
                            End If
                        Next
                        If Contestada = True Then
                            For n = 3 To Flex222.Columns.Count - 1
                                If Flex222.Columns(n).Visible = True Then
                                    Fila = Flex222.Items(i)
                                    Dim Combo As DropDownList = CType(Fila.FindControl("cbo" & (n - 2) & ""), DropDownList)
                                    If Not Combo Is Nothing Then
                                        If Left(Combo.SelectedValue, 1) = "R" Then
                                            lblMensaje.Visible = True
                                            If lblNroPregCont.Text = "0" Or lblNroPregCont.Text <> "T" Then
                                                lblMensaje.Text = "<b>Debe contestar todas las respuestas de la pregunta Nº" & Fila.Cells(0).Text & ".</b>"
                                            Else
                                                lblMensaje.Text = "<b>Debe contestar todas las respuestas.</b>"
                                            End If
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        End If
        Dim Fecha As String = FechaActual()
        Dim Hora As String = HoraActual()
        Dim ValorSys As String = Fecha & Hora & User.Identity.Name
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim Campo As String
        lblMensaje2.Text = ""
        If Session("TipoGrupo") = "3" Then
            Campo = "PD_PERSONAL"
        ElseIf Session("TipoGrupo") = "5" Then
            Campo = "PD_USUARIO"
        Else
            Exit Sub 'FALTA EMPRESA
        End If
        Try
            Cn.Open()
            Cn2.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_GPE_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[w_GPE_" & Session.SessionID & "]"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "CREATE VIEW w_GPE_" & Session.SessionID & " AS SELECT P.PRUEBA_CODIGO,P.PREG_CODIGO, R.RESP_CODIGO " _
                                 & " FROM TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " P INNER JOIN TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & " R ON P.PRUEBA_CODIGO = R.PRUEBA_CODIGO AND P.PREG_CODIGO = R.PREG_CODIGO " _
                                 & " WHERE (P.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (P.PREG_SYS_EST = '0') AND (R.RESP_SYS_EST = '0')"
            cmdSql.ExecuteNonQuery()
            NSes = 0
            cmdSql.CommandText = "SELECT COUNT(PD_SESSIONID) FROM TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") _
                               & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                               & " AND (" & Campo & " = '" & User.Identity.Name & "')"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    NSes = Nz(Rs(0)) + 1
                End While
            Else
                NSes = 1
            End If
            Rs.Close()
            cmdSql.CommandText = "INSERT INTO TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " (PRUEBA_CODIGO, GRUPO_CODIGO, GRUPO_TIPO," & Campo & ", PD_ESTADO, PD_SYS_EST, PD_SYS_CRE,PD_SESSIONID) " _
                                     & "VALUES (" & Session("CodPrueba") & "," & Session("CodGrupo") & ",'" & Session("TipoGrupo") & "','" & User.Identity.Name & "','1','0','" & ValorSys & "','" & NSes & "')"
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "INSERT INTO TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " (PRUEBA_CODIGO,GRUPO_CODIGO, GRUPO_TIPO," & Campo & ", PREG_CODIGO, RESP_CODIGO,DD_SYS_CRE, DD_SYS_EST,PD_SESSIONID) " _
                                   & " SELECT PRUEBA_CODIGO," & Session("CodGrupo") & ",'" & Session("TipoGrupo") & "','" & User.Identity.Name & "', PREG_CODIGO, RESP_CODIGO, '" & ValorSys & "', '0','" & NSes & "' FROM w_GPE_" & Session.SessionID & ""
            cmdSql.ExecuteNonQuery()
            If lblTipoRpta.Text = "1" Then  'rptas diferentes
                With Flex1
                    'se supone q se inserta en blanco
                    'cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_RESPONDIDO=NULL " _
                    '                     & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                    '                     & " AND (" & Campo & "= '" & User.Identity.Name & "') AND PD_SESSIONID='" & NSes & "'"
                    'cmdSql.ExecuteNonQuery()
                    For i = 0 To .Items.Count - 1 'recorrido de filas
                        Fila = .Items(i)
                        Dim CodPreg As Label = CType(Fila.FindControl("Preg"), Label)
                        If CodPreg.Text <> "" Then
                            Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas1"), RadioButtonList)
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then
                                    cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_RESPONDIDO='X' " _
                                                         & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                                                         & " AND (" & Campo & " = '" & User.Identity.Name & "') AND (PREG_CODIGO = " & CodPreg.Text & ") AND (RESP_CODIGO = " & Rpta.Value & ") AND (DD_SYS_EST = '0') AND PD_SESSIONID='" & NSes & "'"
                                    cmdSql.ExecuteNonQuery()
                                End If
                            Next
                        End If
                    Next
                End With
            ElseIf lblTipoRpta.Text = "2" Then  'rptas iguales
                'se supone q se inserta en blanco
                'cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_RESPONDIDO=NULL " _
                '                     & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                '                     & " AND (" & Campo & "='" & User.Identity.Name & "') AND PD_SESSIONID='" & NSes & "'"
                'cmdSql.ExecuteNonQuery()
                If lblFormaMarcar.Text = "2" Then  'marcacion multiple
                    If lblFormaResponder.Text = "1" Then 'responder con check
                        For i = 0 To Flex.Items.Count - 1
                            Fila = Flex.Items(i)
                            Dim Rptas As CheckBoxList = CType(Fila.FindControl("chkRpta"), CheckBoxList)
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then
                                    cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_RESPONDIDO='X' " _
                                                         & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                                                         & " AND (" & Campo & " = '" & User.Identity.Name & "') AND (PREG_CODIGO = " & Fila.Cells(2).Text & ") AND (RESP_CODIGO = " & Rpta.Value & ") AND (DD_SYS_EST = '0') AND PD_SESSIONID='" & NSes & "'"
                                    cmdSql.ExecuteNonQuery()
                                End If
                            Next
                        Next
                    ElseIf lblFormaResponder.Text = "2" Then 'responder con combos
                        Dim CodMenuV As String, CodResp As String
                        For i = 0 To Flex222.Items.Count - 1 'recorrido de filas
                            For n = 3 To Flex222.Columns.Count - 1
                                If Flex222.Columns(n).Visible = True Then
                                    Fila = Flex222.Items(i)
                                    Dim Combo As DropDownList = CType(Fila.FindControl("cbo" & (n - 2) & ""), DropDownList)
                                    If Not Combo Is Nothing Then
                                        CodResp = ""
                                        For nn = 0 To Combo.Items.Count - 1
                                            If Combo.Items(nn).Text = "- -" Then CodResp = Mid(Combo.Items(nn).Value, 2) : Exit For
                                        Next
                                        CodMenuV = ""
                                        'If Combo.Items(Combo.SelectedIndex).Text <> "- -" Then CodMenuV = Combo.Items(Combo.SelectedIndex).Value
                                        If Left(Combo.SelectedValue, 1) = "R" Then
                                        Else
                                            CodMenuV = Combo.SelectedValue
                                        End If
                                        If CodResp <> "" Then
                                            cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_RESPONDIDO='" & IIf(CodMenuV = "", "", "X") & "',MVALOR_CODIGO=" & IIf(CodMenuV = "", "NULL", CodMenuV) & "" _
                                                                 & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                                                                 & " AND (" & Campo & " = '" & User.Identity.Name & "') AND (PREG_CODIGO = " & Fila.Cells(2).Text & ") AND (RESP_CODIGO = " & CodResp & ") AND (DD_SYS_EST = '0') AND PD_SESSIONID='" & NSes & "'"
                                            cmdSql.ExecuteNonQuery()
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                ElseIf lblFormaMarcar.Text = "1" Then 'marcacion unica
                    If lblFormaResponder.Text = "1" Then 'responder con option
                        For i = 0 To Flex.Items.Count - 1
                            Fila = Flex.Items(i)
                            Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas"), RadioButtonList)
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then
                                    cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_RESPONDIDO='X' " _
                                                         & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                                                         & " AND (" & Campo & " = '" & User.Identity.Name & "') AND (PREG_CODIGO = " & Fila.Cells(2).Text & ") AND (RESP_CODIGO = " & Rpta.Value & ") AND (DD_SYS_EST = '0') AND PD_SESSIONID='" & NSes & "'"
                                    cmdSql.ExecuteNonQuery()
                                End If
                            Next
                        Next
                    ElseIf lblFormaResponder.Text = "2" Then 'responder un solo combo

                    End If
                End If
            End If
            'código considerando que se permite marcar una sóla respuesta
            Dim Ppo As Double, Nota As Double
            Nota = -1

            'se supone q se inserta en blanco
            'cmdSql.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_PUNT_PREG_OBTENIDO=NULL,DD_PUNT_PREG_OBTENIDO=NULL,MVALOR_CODIGO=NULL " _
            '                     & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
            '                     & " AND (" & Campo & " = '" & User.Identity.Name & "') AND PD_SESSIONID='" & NSes & "'"
            'cmdSql.ExecuteNonQuery()

            cmdSql2.Connection = Cn2
            If lblTipoRptaCorrecta.Text <> "" And lblFormaMarcar.Text = "1" Then
                'código considerando que se permite marcar una sóla respuesta
                'listar las preguntas respondidas y a la vez q sean las correctas
                cmdSql.CommandText = "SELECT PD.PREG_CODIGO,PD.RESP_CODIGO,(SELECT PREG.RESP_VALOR FROM TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & "  PREG WHERE PREG.PRUEBA_CODIGO=PD.PRUEBA_CODIGO AND PREG.RESP_SYS_EST='0' AND PREG.PREG_CODIGO=PD.PREG_CODIGO AND PREG.RESP_CODIGO=PD.RESP_CODIGO) AS VALOR_RPTA " _
                    & " From TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " PD " _
                    & " WHERE (PD.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (PD.GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (PD.GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                    & " AND (PD." & Campo & " = '" & User.Identity.Name & "') AND (PD.DD_SYS_EST = '0')  AND (PD.DD_RESPONDIDO = 'X')  AND PD_SESSIONID='" & NSes & "' AND (SELECT PREG.RESP_CORRECTA FROM TBGENERAC_PRUEBA_RESPUESTAS_" & Session("CodEmpresa") & "  PREG WHERE PREG.PRUEBA_CODIGO=PD.PRUEBA_CODIGO AND PREG.RESP_SYS_EST='0' AND PREG.PREG_CODIGO=PD.PREG_CODIGO AND PREG.RESP_CODIGO=PD.RESP_CODIGO)='X' ORDER BY PD.PREG_CODIGO, PD.RESP_CODIGO"
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    Nota = 0
                    While Rs.Read
                        Nota = Nota + Nz(Rs!VALOR_RPTA)
                        If Nu(Rs!VALOR_RPTA) <> "" Then Ppo = Nz(Rs!VALOR_RPTA) Else Ppo = -1
                        cmdSql2.CommandText = "UPDATE TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " SET DD_PUNT_PREG_OBTENIDO=" & IIf(Ppo = -1, "NULL", Ppo) & " " _
                                             & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                                             & " AND (" & Campo & "= '" & User.Identity.Name & "') AND (DD_SYS_EST = '0')  AND (PREG_CODIGO = " & Rs!PREG_CODIGO & ") AND PD_SESSIONID='" & NSes & "'"
                        cmdSql2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
            End If
            cmdSql2.CommandText = "UPDATE TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " SET PD_ESTADO='3',PD_FECHA_DESA='" & Fecha & "', PD_HORA_DESA='" & Left(Hora, 4) & "',PD_PRUEBA_NOTA=" & IIf(Nota = -1, "NULL", Nota) & "" _
                                 & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                                 & " AND (" & Campo & " = '" & User.Identity.Name & "') AND (PD_SYS_EST = '0') AND PD_SESSIONID='" & NSes & "'"
            cmdSql2.ExecuteNonQuery()
            cmdSql2.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[w_GPE_" & Session.SessionID & "]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[w_GPE_" & Session.SessionID & "]"
            cmdSql2.ExecuteNonQuery()
            'lblTitulo.Visible = False
            'lblTitulo2.Visible = False
            Flex.Enabled = False
            Flex1.Enabled = False
            Flex222.Enabled = False
            GuardarRptas.Enabled = False : GuardarRptas2.Enabled = False
            Cancelar.Enabled = False : Cancelar2.Enabled = False
            If UCase(Session("TipoPrueba")) = "PRUEBA" Then
                Call Muestra_Resultado(Campo, NSes)
            End If
            lblMensaje2.Text = "El desarrollo de la " & Session("TipoPrueba") & " ya ha sido enviada. Gracias."
            HyperLink1.Visible = True
        Catch Ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn2.Close()
            Cn.Close()
        End Try
    End Sub
    Private Sub Muestra_Resultado(ByVal Campo As String, ByVal NSes As Integer)
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand

        Cn.Open()
        cmdSql.Connection = Cn
        Dim lblNota As String = "", lblNombreEscala As String = ""
        Dim VerPuntTotal As String = "", VerPuntTotal_TipoConver As String = ""
        Dim VerPuntSGrupo As String = "", VerPuntSGrupo_TipoConver As String = ""
        cmdSql.CommandText = "SELECT PRUEBA_PREG_OBLIGAR_RESPONDER,PRUEBA_OBTENER_PUNT_TOTAL,PRUEBA_OBTENER_PUNT_SUBGPO,PRUEBA_PUNT_TOTAL_TIPO_CONVER_RESULT,PRUEBA_PUNT_SUBGPO_TIPO_CONVER_RESULT " _
                              & " FROM TBGENERAC_PRUEBA_DEFINE WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND PRUEBA_CODIGO='" & Session("CodPrueba") & "'"
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
            cmdSql.CommandText = "SELECT PD_PRUEBA_NOTA FROM TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " " _
                & " WHERE (PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
                & " AND (" & Campo & " = '" & User.Identity.Name & "') AND (PD_SYS_EST = '0') AND PD_SESSIONID='" & NSes & "'"
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
                         & " WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (CR_SYS_EST = '0') AND (CR_TIPO_RESULTADO = '1') AND " _
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
            Dim MyDataRow As DataRow
            Dim cmdSql1 As New SqlCommand
            Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
            Dim Rs2 As SqlDataReader
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
            cmdSql.CommandText = "CREATE VIEW w_ObtPunt_SGpo_Preg_" & Session.SessionID & " AS SELECT DD." & Campo & ",DD.PRUEBA_CODIGO,AVG(DD.DD_PUNT_PREG_OBTENIDO) AS PuntajexPreg,P.GPOPREG_CODIGO, DD.PREG_CODIGO " _
                                  & " FROM TBGENERAC_PRUEBA_DESA_DETALLE_" & Session("CodEmpresa") & " DD INNER JOIN TBGENERAC_PRUEBA_PREGUNTAS_" & Session("CodEmpresa") & " P ON DD.PRUEBA_CODIGO = P.PRUEBA_CODIGO AND DD.PREG_CODIGO = P.PREG_CODIGO  " _
                                  & " GROUP BY DD." & Campo & ", DD.PRUEBA_CODIGO,DD.GRUPO_CODIGO, DD.GRUPO_TIPO, DD.DD_SYS_EST,P.PREG_SYS_EST, P.GPOPREG_CODIGO,DD.PREG_CODIGO,PD_SESSIONID  " _
                                  & " HAVING (DD." & Campo & " = '" & User.Identity.Name & "') AND (DD.PRUEBA_CODIGO = " & Session("CodPrueba") & ") AND (DD.GRUPO_CODIGO = " & Session("CodGrupo") & ") AND (DD.GRUPO_TIPO = '" & Session("TipoGrupo") & "')" _
                                  & "  AND PD_SESSIONID='" & NSes & "' AND (DD.DD_SYS_EST = '0') AND (P.PREG_SYS_EST = '0') "
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = "SELECT V." & Campo & ", V.PRUEBA_CODIGO, SUM(V.PuntajexPreg) AS PuntajexSGpo, V.GPOPREG_CODIGO,GP.GPOPREG_NOMBRE " _
                  & " FROM w_ObtPunt_SGpo_Preg_" & Session.SessionID & " V INNER JOIN TBGENERAC_PRUEBA_PREG_AGRUPA GP ON V.PRUEBA_CODIGO = GP.PRUEBA_CODIGO AND V.GPOPREG_CODIGO = GP.GPOPREG_CODIGO WHERE GP.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' " _
                  & " GROUP BY V." & Campo & ", V.PRUEBA_CODIGO,V.GPOPREG_CODIGO, GP.GPOPREG_SYS_EST,GP.GPOPREG_NOMBRE " _
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
                                 & " WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (CR_SYS_EST = '0') AND (CR_TIPO_RESULTADO = '2') AND " _
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
    Private Sub GuardarRptas2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GuardarRptas2.Click
        Call GuardarRptas_Click(sender, e)
    End Sub
    Private Function PregContestadas() As Boolean
        Dim Fila As DataGridItem
        Dim i As Integer, n As Integer
        Dim Respondidas As Integer = 0, PregTotal As Integer = 0
        PregContestadas = True
        If lblTipoRpta.Text = "1" Then  'rptas diferentes
            With Flex1
                For i = 0 To .Items.Count - 1 'recorrido de filas
                    Fila = .Items(i)
                    Dim CodPreg As Label = CType(Fila.FindControl("Preg"), Label)
                    If CodPreg.Text <> "" Then
                        PregTotal = PregTotal + 1
                        Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas1"), RadioButtonList)
                        If Not Rptas Is Nothing Then
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then Respondidas = Respondidas + 1 : Exit For
                            Next
                        End If
                    End If
                Next
            End With
        ElseIf lblTipoRpta.Text = "2" Then  'rptas iguales
            If lblFormaMarcar.Text = "2" Then  'marcacion multiple
                If lblFormaResponder.Text = "1" Then 'responder con check
                    For i = 0 To Flex.Items.Count - 1
                        Fila = Flex.Items(i)
                        Dim Rptas As CheckBoxList = CType(Fila.FindControl("chkRpta"), CheckBoxList)
                        If Not Rptas Is Nothing Then
                            PregTotal = PregTotal + 1
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then Respondidas = Respondidas + 1 : Exit For
                            Next
                        End If
                    Next
                ElseIf lblFormaResponder.Text = "2" Then 'responder con combos
                    For i = 0 To Flex222.Items.Count - 1 'recorrido de filas
                        PregTotal = PregTotal + 1
                        For n = 3 To Flex222.Columns.Count - 1
                            If Flex222.Columns(n).Visible = True Then
                                Fila = Flex222.Items(i)
                                Dim Combo As DropDownList = CType(Fila.FindControl("cbo" & (n - 2) & ""), DropDownList)
                                If Not Combo Is Nothing Then
                                    If Left(Combo.SelectedValue, 1) = "R" Then
                                    Else
                                        Respondidas = Respondidas + 1 : GoTo Sgte_Fila
                                    End If
                                End If
                            End If
                        Next
Sgte_Fila:
                    Next
                End If
            ElseIf lblFormaMarcar.Text = "1" Then 'marcacion unica
                If lblFormaResponder.Text = "1" Then 'responder con option
                    For i = 0 To Flex.Items.Count - 1
                        Fila = Flex.Items(i)
                        Dim Rptas As RadioButtonList = CType(Fila.FindControl("OptRespuestas"), RadioButtonList)
                        If Not Rptas Is Nothing Then
                            PregTotal = PregTotal + 1
                            For n = 0 To Rptas.Items.Count - 1
                                Dim Rpta As ListItem = New ListItem
                                Rpta = Rptas.Items(n)
                                If Rpta.Selected = True Then Respondidas = Respondidas + 1 : Exit For
                            Next
                        End If
                    Next
                ElseIf lblFormaResponder.Text = "2" Then 'responder un solo combo

                End If
            End If
        End If
        If lblNroPregCont.Text = "T" Then  'todas son oblig
            If Respondidas < PregTotal Then
                lblMensaje.Visible = True
                lblMensaje.Text = "<b>Favor de responder todas las preguntas !!!</b>"
                PregContestadas = False
            End If
        Else
            If Respondidas < CInt(lblNroPregCont.Text) Then
                lblMensaje.Visible = True
                If CInt(lblNroPregCont.Text) = PregTotal Then
                    lblMensaje.Text = "<b>Favor de responder todas las preguntas !!!</b>"
                Else
                    lblMensaje.Text = "<b>Debe responder " & lblNroPregCont.Text & " de " & PregTotal & " preguntas obligatoriamente !!!</b>"
                End If
                PregContestadas = False
            End If
        End If
    End Function

    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Flex.SelectedIndexChanged

    End Sub
End Class
