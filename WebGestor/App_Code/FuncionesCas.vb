Imports System.Data
Imports System.Data.SqlClient
Namespace WebGestor
    Public Module FuncionesCas
        Public Sub Tipos_Criterio(ByVal pTipo As String, ByVal cbo As DropDownList, ByVal pCodEmpresa As String, ByVal Conexion As String)
            Dim Cn As New SqlConnection(Conexion)
            cbo.Items.Clear()
            Try
                Cn.Open()
                Dim Sql As String = " SELECT CASCRI_DESCRIPCION,CASCRI_CODIGO FROM TBCAS_CRITERIOS WHERE " _
                                  & " EMPRESA_CODIGO='" & pCodEmpresa & "' AND CASCRI_SYS_EST='0' AND CASCRI_TIPO='" & pTipo & "' "
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                cbo.DataSource = cmdSql.ExecuteReader
                cbo.DataTextField = "CASCRI_DESCRIPCION"
                cbo.DataValueField = "CASCRI_CODIGO"
                cbo.DataBind()
                cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                Cn.Close()
            End Try
        End Sub
        Public Sub Cargar_Empresa(ByVal cbo As DropDownList, ByVal Conexion As String)
            Dim Cn As New SqlConnection(Conexion)
            cbo.Items.Clear()
            Try
                Cn.Open()
                Dim Sql As String = " SELECT TBCAS_EMPRESA_CODIGO,TBCAS_EMPRESA_NOMBRE" _
                                & " FROM dbo.TBCAS_EMPRESA"
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                cbo.DataSource = cmdSql.ExecuteReader
                cbo.DataTextField = "TBCAS_EMPRESA_NOMBRE"
                cbo.DataValueField = "TBCAS_EMPRESA_CODIGO"
                cbo.DataBind()
                cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                Cn.Close()
            End Try
        End Sub
        Public Sub LLenaComboItemTabEsp(ByVal cbo As DropDownList, ByVal Valor1 As String, ByVal Valor2 As String,
                                     ByVal Tb1 As String, ByVal Tb2 As String, ByVal Tb3 As String, ByVal Ntb As Integer, ByVal pCodEmpresa As String, ByVal Conexion As String)
            Dim CnTE As New SqlConnection(Conexion)
            Dim cmdGlobalTE As New SqlCommand
            Dim RsTE As SqlClient.SqlDataReader
            Dim Sql As String = ""
            Try
                CnTE.Open() : cmdGlobalTE.Connection = CnTE
                cbo.Items.Clear()
                If Ntb = 1 Then
                    Sql = "SELECT NIVEL1_DESCRIP AS VALOR,NIVEL1_CODIGO AS CODIGO From " & Tb1 & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & pCodEmpresa & "')"
                    Sql = Sql & " ORDER BY NIVEL1_DESCRIP"
                    Dim cmdSql As New SqlClient.SqlCommand(Sql, CnTE)
                    cbo.DataSource = cmdSql.ExecuteReader
                    cbo.DataTextField = "VALOR"
                    cbo.DataValueField = "CODIGO"
                    cbo.DataBind()
                ElseIf Ntb = 2 Then
                    Sql = "SELECT TB2.NIVEL2_DESCRIP AS VALOR, TB2.NIVEL2_CODIGO AS CODIGO fROM " & Tb2 & " TB2 INNER JOIN " & Tb1 & " TB1 ON TB1.EMPRESA_CODIGO=TB2.EMPRESA_CODIGO AND Tb2.NIVEL1_CODIGO = Tb1.NIVEL1_CODIGO " _
                    & "WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL1_CODIGO = " & Valor1 & ") AND (TB2.NIVEL2_SYS_EST = '0') AND (TB1.EMPRESA_CODIGO='" & pCodEmpresa & "')"
                    Sql = Sql & " ORDER BY TB2.NIVEL2_DESCRIP"
                ElseIf Ntb = 3 Then
                    Sql = "SELECT TB3.NIVEL3_DESCRIP AS VALOR, TB3.NIVEL3_CODIGO AS CODIGO FROM " & Tb2 & " TB2 INNER JOIN " & Tb1 & " TB1 ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                    & "INNER JOIN " & Tb3 & " TB3 ON TB2.EMPRESA_CODIGO=TB3.EMPRESA_CODIGO AND  Tb2.NIVEL2_CODIGO = Tb3.NIVEL2_CODIGO WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0') AND " _
                    & "(TB3.NIVEL3_SYS_EST = '0') AND (TB2.NIVEL1_CODIGO = " & Valor1 & ") AND (TB2.NIVEL2_CODIGO = " & Valor2 & ") AND (TB1.EMPRESA_CODIGO='" & pCodEmpresa & "')"
                    Sql = Sql & " ORDER BY TB3.NIVEL3_DESCRIP"
                End If
                If Ntb <> 1 Then
                    cmdGlobalTE.CommandText = Sql
                    RsTE = cmdGlobalTE.ExecuteReader
                    If RsTE.HasRows Then
                        While RsTE.Read
                            Dim Item As New ListItem
                            Item.Text = Nu(RsTE!VALOR).ToString
                            Item.Value = Nu(RsTE!CODIGO).ToString
                            cbo.Items.Add(Item)
                        End While
                    End If
                    RsTE.Close()
                End If
                cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                CnTE.Close()
            End Try
        End Sub
        Public Sub Cargar_Componente(ByVal cbo As DropDownList, ByVal Conexion As String)
            Dim Cn As New SqlConnection(Conexion)
            cbo.Items.Clear()
            Try
                Cn.Open()
                Dim Sql As String = " SELECT NIVEL1_CODIGO,NIVEL1_DESCRIP" _
                                  & " FROM dbo.TBESP_CAS1"
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                cbo.DataSource = cmdSql.ExecuteReader
                cbo.DataTextField = "NIVEL1_DESCRIP"
                cbo.DataValueField = "NIVEL1_CODIGO"
                cbo.DataBind()
                cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                Cn.Close()
            End Try
        End Sub
        Public Sub Cargar_Grupo(ByVal cbo As DropDownList, ByVal Conexion As String)
            Dim Cn As New SqlConnection(Conexion)
            cbo.Items.Clear()
            Try
                Cn.Open()
                Dim Sql As String = " SELECT GRUPO_COD,GRUPO_NOMBRE" _
                                  & " FROM dbo.TBCAS_GRUPO WHERE GRUPO_SYS_EST = '0'"
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                cbo.DataSource = cmdSql.ExecuteReader
                cbo.DataTextField = "GRUPO_NOMBRE"
                cbo.DataValueField = "GRUPO_COD"
                cbo.DataBind()
                cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                Cn.Close()
            End Try
        End Sub
        Public Sub AvisosPublicados(ByVal txt As TextBox, ByVal pUser As String, ByVal Conexion As String)
            Dim obj As New ModuloCas
            Dim dt As DataTable
            dt = obj.CasConsulta_ExisteAviso(pUser, Conexion)
            If dt.Rows.Count > 0 Then
                txt.Text = "   Hay Aviso"
                txt.BorderColor = Drawing.Color.Green
                txt.BackColor = Drawing.Color.Green
                txt.ForeColor = Drawing.Color.White
            Else
                txt.Text = "No hay Aviso"
                txt.BorderColor = Drawing.Color.Red
                txt.BackColor = Drawing.Color.Red
                txt.ForeColor = Drawing.Color.White
            End If
            dt = Nothing
        End Sub
        Function Devuelve_NewCodOficina() As Double
            Dim CodOficina As Double = 0
            Dim Cn As New SqlConnection(strConexion)
            Dim Cmd As New SqlCommand("TBCAS_NEWCOD_OFICINA", Cn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim Da As New SqlDataAdapter(Cmd)
            Dim Dt As New DataTable("TBCAS_NEWCOD_OFICINA")
            Da.Fill(Dt)
            If Dt.Rows.Count = 1 Then
                For Each dr As Data.DataRow In Dt.Rows
                    CodOficina = Nz(dr(0)) + 1
                Next
            Else
                CodOficina = 1
            End If
            Return CodOficina
        End Function
        Function Lista_Solucion(ByVal pCodEmpresa As String, ByVal pCodInc As Double, ByVal pEstado As String, ByVal pSeguimiento As String, ByVal Conexion As String) As DataTable
            Dim obj As New ModuloCas
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim dtListado As New DataTable
            Dim dRow As Data.DataRow
            Lista_Solucion = Nothing
            Try
                dt.Columns.Add("FECHA")
                dt.Columns.Add("HORA")
                dt.Columns.Add("DESCRIPCION")
                dt.Columns.Add("NOMBRE")
                dt.Columns.Add("FECHA_ACCION")
                dt.Columns.Add("FECHAFIN_SEG")
                dtListado = obj.CasLista_IncidenteDetalle(pCodEmpresa, pCodInc, Conexion)
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        dRow = dt.NewRow
                        dRow("FECHA") = FormatoFecha(Nu(drMenuItem("DPROB_FECHA_ACCION")))
                        dRow("HORA") = FormatoHora(Nu(drMenuItem("DPROB_HORA_ACCION")))
                        dRow("DESCRIPCION") = Nu(drMenuItem("DPROB_ACCION_DESCRIPCION"))
                        If pEstado = "5" Or pEstado = "6" Then
                            dt2 = obj.CasConsulta_ExisteGrupo(Nz(drMenuItem("DPROB_USUARIO_ACCION")), "", "1", Conexion)
                            If dt2.Rows.Count = 1 Then
                                For Each dr As Data.DataRow In dt2.Rows
                                    dRow("NOMBRE") = Nu(dr("GRUPO_NOMBRE"))
                                Next
                            End If
                            dt2 = Nothing
                            If IsDBNull(dRow("NOMBRE")) Then
                                dt2 = obj.CasConsulta_ExisteUsuario(Nu(drMenuItem("DPROB_USUARIO_ACCION")))
                                If dt2.Rows.Count > 0 Then
                                    For Each dr As Data.DataRow In dt2.Rows
                                        dRow("NOMBRE") = " " + Nu(dr("USUARI_CODIGO")) + " - " + Nu(dr("NOMBRESU"))
                                    Next
                                End If
                                dt2 = Nothing
                            End If
                        ElseIf pEstado = "2" Or pEstado = "10" Then
                            dt2 = obj.CasConsulta_ExisteUsuario(Nu(drMenuItem("DPROB_USUARIO_ACCION")))
                            If dt2.Rows.Count > 0 Then
                                For Each dr As Data.DataRow In dt2.Rows
                                    dRow("NOMBRE") = " " + Nu(dr("USUARI_CODIGO")) + " - " + Nu(dr("NOMBRESU"))
                                Next
                            End If
                            dt2 = Nothing
                        End If
                        If pSeguimiento = "1" Or pSeguimiento = "2" Then
                            dRow("FECHA_ACCION") = FormatoFecha(Nu(drMenuItem("DPROB_FECHA_ACCION")))
                            dRow("FECHAFIN_SEG") = FormatoFecha(Nu(drMenuItem("INC_SEGUIMIENTO_FECHAFIN")))
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
                dtListado = Nothing
                Return dt
            Catch Ex As SqlException
            Catch Ex As Exception
            Finally
            End Try
        End Function
    End Module
End Namespace
