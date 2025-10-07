Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class Encuestas_Realizadas
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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            If User.Identity.Name = "" Or Session("TipoGrupo") Is Nothing Then
                Session.Clear()
                FormsAuthentication.SignOut()
                Response.Redirect("TerminaSesion.aspx")
                Exit Sub
            End If
            Info.Visible = False
            Tabla.Visible = True
            Call Carga_Enc_Realizadas(True)
        End If
    End Sub
    Private Sub Carga_Enc_Realizadas(ByVal Primera As Boolean)
        lblMensaje.Text = ""
        Tabla.DataSource = Carga_Enc_Realizadas2()
        Tabla.DataBind()
        If Primera = True Then
            If Tabla.Items.Count < 7 Then Tabla.AllowPaging = False Else Tabla.AllowPaging = True
            Tabla.DataBind()
        End If
        Dim Fila As DataGridItem
        Dim i As Integer
        With Tabla
            For i = 0 To .Items.Count - 1 'recorrido de filas
                Fila = .Items(i)
                Dim tb As DataGrid = CType(Fila.FindControl("Flex2"), DataGrid)
                If Not tb Is Nothing Then
                    'If .Items(i).Cells(1).Text = "Encuesta" And .Items(i).Cells(4).Text = "2" Then Boton.Visible = True Else Boton.Visible = False
                    'tb.Rows(0).Cells(0).InnerHtml = Det_Enc_Realizadas(.Items(i).Cells(2).Text)
                    'tb.Columns.Clear()
                    tb.Height = Unit.Percentage(100)
                    tb.DataSource = Det_Enc_Realizadas(.Items(i).Cells(2).Text)
                    tb.DataBind()
                    Dim Fila2 As DataGridItem
                    Dim i2 As Integer
                    For i2 = 0 To tb.Items.Count - 1 'recorrido de filas
                        Fila2 = tb.Items(i2)
                        Dim cmd As LinkButton = CType(Fila2.FindControl("cmdVer"), LinkButton)
                        If Not cmd Is Nothing Then
                            If .Items(i).Cells(1).Text = "Prueba" And (.Items(i).Cells(5).Text = "S" Or .Items(i).Cells(6).Text = "S") Then cmd.Enabled = True Else cmd.Enabled = False
                        End If
                    Next
                End If
            Next
        End With
    End Sub
    Sub Tabla_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        Tabla.CurrentPageIndex = e.NewPageIndex
        Call Carga_Enc_Realizadas(False)
    End Sub
    Private Function Det_Enc_Realizadas(ByVal CodPrueba As String) As ICollection
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim Campo As String, i As Integer = 0
        If Session("TipoGrupo") = "3" Then
            Campo = "PD_PERSONAL"
        ElseIf Session("TipoGrupo") = "5" Then
            Campo = "PD_USUARIO"
        Else
            Exit Function 'FALTA EMPRESA
        End If
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add("C1", GetType(String))
        dt.Columns.Add("C2", GetType(String))
        dt.Columns.Add("C3", GetType(String))
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            'no filtramos grupo codigo, por la razon q puede estar en varios grupos d su mismo tipo
            cmdSql.CommandText = "SELECT PD_FECHA_DESA, PD_HORA_DESA, DEF.PRUEBA_TIPO,PD_SESSIONID FROM TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " PD INNER JOIN TBGENERAC_PRUEBA_DEFINE DEF ON PD.PRUEBA_CODIGO = DEF.PRUEBA_CODIGO " _
                                & " WHERE (PD_SYS_EST = '0') AND (" & Campo & " = '" & User.Identity.Name & "') AND (PD.GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (PD.PRUEBA_CODIGO = '" & CodPrueba & "') AND (DEF.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PD_ESTADO='3')" _
                                & " ORDER BY PD_FECHA_DESA DESC, PD_HORA_DESA DESC"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    dr = dt.NewRow()
                    dr(0) = FormatoFecha(Nu(Rs!PD_FECHA_DESA)) & " " & FormatoHora(Nu(Rs!PD_HORA_DESA))
                    dr(1) = Nu(Rs!PD_SESSIONID)
                    dr(2) = CodPrueba
                    dt.Rows.Add(dr)
                End While
            End If
            Det_Enc_Realizadas = New DataView(dt)
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Function Carga_Enc_Realizadas2() As ICollection
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim cmdSql As New SqlCommand
        Dim Campo As String, i As Integer
        If Session("TipoGrupo") = "3" Then
            Campo = "PD_PERSONAL"
        ElseIf Session("TipoGrupo") = "5" Then
            Campo = "PD_USUARIO"
        Else
            Exit Function 'FALTA EMPRESA
        End If
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add("C1", GetType(String))
        dt.Columns.Add("C2", GetType(String))
        dt.Columns.Add("C3", GetType(String))
        dt.Columns.Add("C4", GetType(String))
        dt.Columns.Add("C5", GetType(String))
        dt.Columns.Add("C6", GetType(String))
        dt.Columns.Add("C7", GetType(String))
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            'no filtramos grupo codigo, por la razon q puede estar en varios grupos d su mismo tipo
            cmdSql.CommandText = "SELECT DISTINCT PD.PRUEBA_CODIGO, DEF.PRUEBA_TIPO, DEF.PRUEBA_NOMBRE,GRUPO_CODIGO,PRUEBA_OBTENER_PUNT_TOTAL,PRUEBA_OBTENER_PUNT_SUBGPO " _
                                & " FROM  TBGENERAC_PRUEBA_DESARROLLO_" & Session("CodEmpresa") & " PD INNER JOIN TBGENERAC_PRUEBA_DEFINE DEF ON PD.PRUEBA_CODIGO = DEF.PRUEBA_CODIGO " _
                                & " WHERE (PD." & Campo & " = '" & User.Identity.Name & "') AND (PD.GRUPO_TIPO = '" & Session("TipoGrupo") & "') AND (DEF.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')  AND (PD_ESTADO='3') AND (DEF.PRUEBA_SYS_EST = '0') ORDER BY PD.PRUEBA_CODIGO"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    dr = dt.NewRow()
                    dr(0) = i.ToString
                    dr(1) = IIf(Nu(Rs!PRUEBA_TIPO) = "1", "Prueba", "Encuesta")
                    dr(2) = Format(Nz(Rs!PRUEBA_CODIGO), "0000")
                    dr(3) = Nu(Rs!PRUEBA_NOMBRE)
                    dr(4) = Nu(Rs!GRUPO_CODIGO)
                    dr(5) = Nu(Rs!PRUEBA_OBTENER_PUNT_TOTAL)
                    dr(6) = Nu(Rs!PRUEBA_OBTENER_PUNT_SUBGPO)
                    dt.Rows.Add(dr)
                End While
            Else
                lblMensaje.Text = "No se han encontrado Pruebas o Encuestas realizadas."
            End If
            Carga_Enc_Realizadas2 = New DataView(dt)
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Sub Tabla_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Tabla.ItemCommand
        If e.Item.Cells.Count < 3 Then Exit Sub
        lblMensaje.Text = "" : lblMensaje.Visible = False
        If e.CommandName = "Desarrollar" Then
            Session("CodPrueba") = e.Item.Cells(2).Text
            Session("TipoPrueba") = e.Item.Cells(1).Text
            Session("NomPrueba") = e.Item.Cells(3).Text
            Session("CodGrupo") = e.Item.Cells(4).Text
            Response.Redirect("Encuesta_Des.aspx")
        End If
    End Sub
    Sub Flex2_VerResultados(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim i As Integer
        With Tabla
            For i = 0 To .Items.Count - 1
                If .Items(i).Cells(2).Text = e.Item.Cells(2).Text Then
                    Info.Visible = True
                    lblResultado.Visible = False
                    FlexResultado.Visible = False
                    lbl1.InnerHtml = "&nbsp;<b>Tipo :</b> " & .Items(i).Cells(1).Text
                    lbl2.InnerHtml = "&nbsp;<b>Nº :</b> " & .Items(i).Cells(2).Text
                    lbl3.InnerHtml = "&nbsp;<b>Nombre :</b> " & .Items(i).Cells(3).Text
                    lbl4.InnerHtml = "&nbsp;<b>Fecha y Hora :</b> " & e.Item.Cells(0).Text
                    Call Muestra_Resultado(.Items(i).Cells(2).Text, .Items(i).Cells(4).Text, e.Item.Cells(1).Text)
                    Exit Sub
                End If
            Next
        End With
    End Sub
    Private Sub CerrarResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarResult.Click
        Info.Visible = False
    End Sub
    Private Sub Muestra_Resultado(ByVal CodPrueba As String, ByVal CodGrupo As String, ByVal NSes As String)
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand

        Dim Campo As String
        If Session("TipoGrupo") = "3" Then
            Campo = "PD_PERSONAL"
        ElseIf Session("TipoGrupo") = "5" Then
            Campo = "PD_USUARIO"
        Else
            Exit Sub 'FALTA EMPRESA
        End If

        Cn.Open()
        cmdSql.Connection = Cn
        Dim lblNota As String = "", lblNombreEscala As String = ""
        Dim VerPuntTotal As String = "", VerPuntTotal_TipoConver As String = ""
        Dim VerPuntSGrupo As String = "", VerPuntSGrupo_TipoConver As String = ""
        cmdSql.CommandText = "SELECT PRUEBA_OBTENER_PUNT_TOTAL,PRUEBA_OBTENER_PUNT_SUBGPO,PRUEBA_PUNT_TOTAL_TIPO_CONVER_RESULT,PRUEBA_PUNT_SUBGPO_TIPO_CONVER_RESULT " _
                              & " FROM TBGENERAC_PRUEBA_DEFINE WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND PRUEBA_CODIGO='" & CodPrueba & "'"
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
                & " WHERE (PRUEBA_CODIGO = " & CodPrueba & ") AND (GRUPO_CODIGO = " & CodGrupo & ") AND (GRUPO_TIPO = '" & Session("TipoGrupo") & "') " _
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
                         & " (CR_TIPO_CONVERSION = '1') AND (PRUEBA_CODIGO = " & CodPrueba & ") AND " _
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
                                  & " HAVING (DD." & Campo & " = '" & User.Identity.Name & "') AND (DD.PRUEBA_CODIGO = " & CodPrueba & ") AND (DD.GRUPO_CODIGO = " & CodGrupo & ") AND (DD.GRUPO_TIPO = '" & Session("TipoGrupo") & "')" _
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
                                 & " (CR_TIPO_CONVERSION = '1') AND (PRUEBA_CODIGO = " & CodPrueba & ") AND (GPOPREG_CODIGO='" & Nu(Rs!GPOPREG_CODIGO) & "') AND " _
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
