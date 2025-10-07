Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class clsMesaAyuda
    Public Sub MACargar_Empresa(ByVal cbo As DropDownList, ByVal psConexion As String, ByVal psCodEmpresa As String)
        Dim Cn As New SqlConnection(psConexion)
        cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT AEMP_CODIGO,AEMP_NOMBRE" _
                            & " FROM dbo.TBADMIN_EMPRESA WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "'"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cbo.DataSource = cmdSql.ExecuteReader
            cbo.DataTextField = "AEMP_NOMBRE"
            cbo.DataValueField = "AEMP_CODIGO"
            cbo.DataBind()
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Function ObtenerValorItem(ByVal Valor As String, ByVal Tabla As String, _
                              ByVal Ntb As Integer, ByVal pCodEmpresa As String, _
                              ByVal Conexion As String) As String
        Dim CnTE As New SqlConnection(Conexion)
        Dim Sql As String = ""
        Dim Rs As SqlDataReader
        ObtenerValorItem = ""
        Try
            CnTE.Open()
            If Ntb = 1 Then
                Sql = " SELECT NIVEL1_DESCRIP AS VALOR,NIVEL1_CODIGO AS CODIGO From " & Tabla & " " _
                    & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & pCodEmpresa & "') " _
                    & " and (NIVEL1_CODIGO = " & Valor & ")"
            ElseIf Ntb = 2 Then
                Sql = " SELECT NIVEL2_DESCRIP AS VALOR,NIVEL2_CODIGO AS CODIGO From " & Tabla & " " _
                    & " WHERE (NIVEL2_SYS_EST = '0') AND (EMPRESA_CODIGO='" & pCodEmpresa & "') " _
                    & " and (NIVEL2_CODIGO = " & Valor & ")"
            ElseIf Ntb = 3 Then
                Sql = " SELECT NIVEL3_DESCRIP AS VALOR,NIVEL3_CODIGO AS CODIGO From " & Tabla & " " _
                    & " WHERE (NIVEL3_SYS_EST = '0') AND (EMPRESA_CODIGO='" & pCodEmpresa & "') " _
                    & " and (NIVEL3_CODIGO = " & Valor & ")"
            End If
            Dim cmdSql As New SqlClient.SqlCommand(Sql, CnTE)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    ObtenerValorItem = Nu(Rs("VALOR"))
                End While
            End If
            Rs.Close()
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            CnTE.Close()
        End Try
    End Function
    Public Sub MATipos_Criterio(ByVal pTipo As String, ByVal cbo As DropDownList, ByVal pCodEmpresa As String, ByVal Conexion As String)
        Dim Cn As New SqlConnection(Conexion)
        cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT ADMCRI_DESCRIPCION,ADMCRI_CODIGO FROM TBADMIN_CRITERIOS WHERE " _
                              & " EMPRESA_CODIGO='" & pCodEmpresa & "' AND ADMCRI_SYS_EST='0' AND ADMCRI_TIPO='" & pTipo & "' "
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cbo.DataSource = cmdSql.ExecuteReader
            cbo.DataTextField = "ADMCRI_DESCRIPCION"
            cbo.DataValueField = "ADMCRI_CODIGO"
            cbo.DataBind()
            'cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Function MALista_Criterio(ByVal CodEmpresa As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_CRITERIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_CRITERIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodAplicativo As Double, _
                                       ByVal pCodProducto As Double, ByVal pCodSubProd As Double, _
                                       ByVal Conexion As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_BASEDATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Int).Value = pCodSubProd
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_SubProductos(ByVal pCodEmpresa As String, ByVal pCodProducto As Double, _
                                       ByVal pCodSubProd As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_SUBPRODUCTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_SUBPRODUCTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Productos(ByVal pCodEmpresa As String, ByVal pCodProducto As Double, _
                                      ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_PRODUCTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_PRODUCTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Aplicativo(ByVal pCodEmpresa As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_APLICATIVOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_APLICATIVOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_xProblema(ByVal pCodEmpresa As String, ByVal pCodProblema As Double, _
                                        ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_XPROBLEMA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = pCodProblema
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_XPROBLEMA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_xProblema_Solucion(ByVal pCodEmpresa As String, ByVal pCodProblema As Double, _
                                               ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_XPROBLEMA_SOLUCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = pCodProblema
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_XPROBLEMA_SOLUCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Oficina(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_OFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_PersonalSol(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_PERSONALSOL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_PERSONALSOL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Item(ByVal Conexion As String, ByVal pCodEmpresa As String, _
                                 ByVal pdTipoProb As Double, ByVal psTipoItem As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_ITEMS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoProb", SqlDbType.Float).Value = pdTipoProb
        Cmd.Parameters.Add("@TipoItem", SqlDbType.VarChar).Value = psTipoItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_ITEMS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAUltimo_Item(ByVal Conexion As String, ByVal pCodEmpresa As String) As String
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_ULTIMO_ITEM", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_ULTIMO_ITEM")
        Da.Fill(Dt)
        MAUltimo_Item = ""
        If Dt.Rows.Count = 1 Then
            For Each dr As DataRow In Dt.Rows
                MAUltimo_Item = Nu(dr(0))
            Next
        End If
        Return MAUltimo_Item
    End Function
    Public Function MALista_XTemaAyuda(ByVal pCodigo As Double, ByVal pCodEmpresa As String, _
                                       ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_TEMAAYUDAxCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_TEMAAYUDAxCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Archivo_xProblema(ByVal pCodigo As Double, ByVal pCodEmpresa As String, _
                                       ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_ARCHIVO_PROBLEMA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_ARCHIVO_PROBLEMA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_TemaAyuda(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_TEMAAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_TEMAAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Empresa(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_EMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_EMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Oficina(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_OFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Oficina_xEmpresa(ByVal Conexion As String, ByVal psCodEmpresa As String, _
                                             ByVal pdEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_OFICINA_XEMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Empresa", SqlDbType.Float).Value = pdEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_OFICINA_XEMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Puesto(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_PUESTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_PUESTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Personas(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_PERSONAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_PERSONAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Enlace(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_ENLACE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_ENLACE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Enlace(ByVal pCodigo As Double, ByVal Conexion As String, _
                                    ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_ENLACExCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_ENLACExCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_UsuarioXNivel(ByVal pNivel As String, ByVal pTipoListado As String, _
                                          ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTAUSUARIO_XNIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.VarChar).Value = pNivel
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTAUSUARIO_XNIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MALista_Aviso(ByVal pUser As String, ByVal pEstUsuario As String, _
                                  ByVal pTipoAviso As String, ByVal pEstAviso As String, _
                                  ByVal pTipoListado As String, ByVal Conexion As String, _
                                  ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_LISTA_AVISOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@EstUsuario", SqlDbType.VarChar).Value = pEstUsuario
        Cmd.Parameters.Add("@TipoAviso", SqlDbType.VarChar).Value = pTipoAviso
        Cmd.Parameters.Add("@EstAviso", SqlDbType.VarChar).Value = pEstAviso
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_LISTA_AVISOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    'insertar y update
    Public Function MAInsUpd_Problema(ByVal pCodEmpresa As String, ByVal pTipoProb As Double,
                                ByVal pUser As String, ByVal pPeCodInterno As String,
                                ByVal pRep_Codigo As Double, ByVal pRep_Prioridad As String,
                                ByVal pTipo2 As Double, ByVal pTipo3 As Double,
                                ByVal pRep_Descrip As String, ByVal pTipo As String,
                                ByVal pTipoIngreso As String, ByVal pCodOficina As Double,
                                ByVal pTelefActual As String, ByVal pEstado As String,
                                ByVal pAsignado As String, ByVal pIniciaLlamada As String,
                                ByVal pSeguimiento As String, ByVal pMotivo As String,
                                ByVal pUserRedirec As String, ByVal pAsigTipo As String,
                                ByVal psFechaProb As String, ByVal psHoraProb As String,
                                ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_PROBLEMA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoProb", SqlDbType.Float).Value = pTipoProb
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@PeCodInterno", SqlDbType.VarChar).Value = pPeCodInterno
        Cmd.Parameters.Add("@Rep_Codigo", SqlDbType.Float).Value = pRep_Codigo
        Cmd.Parameters.Add("@Rep_Prioridad", SqlDbType.VarChar).Value = pRep_Prioridad
        Cmd.Parameters.Add("@Tipo2", SqlDbType.Float).Value = pTipo2
        Cmd.Parameters.Add("@Tipo3", SqlDbType.Float).Value = pTipo3
        Cmd.Parameters.Add("@Rep_Descrip", SqlDbType.VarChar).Value = pRep_Descrip
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pCodOficina
        Cmd.Parameters.Add("@TelefActual", SqlDbType.VarChar).Value = pTelefActual
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@Asignado", SqlDbType.VarChar).Value = pAsignado
        Cmd.Parameters.Add("@IniciaLlamada", SqlDbType.VarChar).Value = pIniciaLlamada
        Cmd.Parameters.Add("@Seguimiento", SqlDbType.VarChar).Value = pSeguimiento
        Cmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = pMotivo
        Cmd.Parameters.Add("@UserRedirec", SqlDbType.VarChar).Value = pUserRedirec
        Cmd.Parameters.Add("@AsigTipo", SqlDbType.VarChar).Value = pAsigTipo
        Cmd.Parameters.Add("@FechaProb", SqlDbType.VarChar).Value = psFechaProb
        Cmd.Parameters.Add("@HoraProb", SqlDbType.VarChar).Value = psHoraProb
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_PROBLEMA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_ProblemaDetalle(ByVal pCodEmpresa As String, ByVal pCodIncidente As Double,
                                ByVal pSolucion As String, ByVal pUser As String,
                                ByVal pTipoIngreso As String, ByVal psFechaProb As String,
                                ByVal psHoraProb As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_PROBLEMADETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = pCodIncidente
        Cmd.Parameters.Add("@Solucion", SqlDbType.VarChar).Value = pSolucion
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.Float).Value = pTipoIngreso
        Cmd.Parameters.Add("@FechaProb", SqlDbType.VarChar).Value = psFechaProb
        Cmd.Parameters.Add("@HoraProb", SqlDbType.VarChar).Value = psHoraProb
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_PROBLEMADETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsert_ProblemaDetalle(ByVal pCodEmpresa As String, ByVal Conexion As String,
                                             ByVal pCodProb As Double, ByVal pCodAccion As String,
                                             ByVal pDescripAccion As String, ByVal pCodCausa As String,
                                             ByVal pDescripCausa As String, ByVal pUser As String,
                                             ByVal pEstado As String, ByVal pTipoProb1 As Double,
                                             ByVal pTipoProb2 As Double, ByVal pTipoProb3 As Double,
                                             ByVal psFechaAcc As String, ByVal psHoraAcc As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INS_PROBLEMADETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProb", SqlDbType.Float).Value = pCodProb
        Cmd.Parameters.Add("@CodAccion", SqlDbType.VarChar).Value = pCodAccion
        Cmd.Parameters.Add("@DescripAccion", SqlDbType.VarChar).Value = pDescripAccion
        Cmd.Parameters.Add("@CodCausa", SqlDbType.VarChar).Value = pCodCausa
        Cmd.Parameters.Add("@DescripCausa", SqlDbType.VarChar).Value = pDescripCausa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@TipoProb1", SqlDbType.Float).Value = pTipoProb1
        Cmd.Parameters.Add("@TipoProb2", SqlDbType.Float).Value = pTipoProb2
        Cmd.Parameters.Add("@TipoProb3", SqlDbType.Float).Value = pTipoProb3
        Cmd.Parameters.Add("@FechaAcc", SqlDbType.VarChar).Value = psFechaAcc
        Cmd.Parameters.Add("@HoraAcc", SqlDbType.VarChar).Value = psHoraAcc
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INS_PROBLEMADETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAUpdate_xProblemaNoVisto(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                              ByVal pCodProblema As Double, ByVal psFecha As String,
                                              ByVal psHora As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_PROBLEMA_NOVISTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProblema", SqlDbType.Float).Value = pCodProblema
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = psHora
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_PROBLEMA_NOVISTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAUpdate_ProblemaAsignado(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                              ByVal pCodProblema As Double, ByVal psPerAsignada As String,
                                              ByVal psFecAsignada As String, ByVal psHorAsignada As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_UPD_PROBLEMA_ASIGNADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProblema", SqlDbType.Float).Value = pCodProblema
        Cmd.Parameters.Add("@PerAsignada", SqlDbType.VarChar).Value = psPerAsignada
        Cmd.Parameters.Add("@FecAsignada", SqlDbType.VarChar).Value = psFecAsignada
        Cmd.Parameters.Add("@HorAsignada", SqlDbType.VarChar).Value = psHorAsignada
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_UPD_PROBLEMA_ASIGNADO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsert_ProblemaAsignado(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                              ByVal pCodProblema As Double, ByVal psPerAsignada As String,
                                              ByVal psFecAsignada As String, ByVal psHorAsignada As String,
                                              ByVal psEstado As String, ByVal psFecAsiVisto As String,
                                              ByVal psHorAsiVisto As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INS_PROBLEMA_ASIGNADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProblema", SqlDbType.Float).Value = pCodProblema
        Cmd.Parameters.Add("@PerAsignada", SqlDbType.VarChar).Value = psPerAsignada
        Cmd.Parameters.Add("@FecAsignada", SqlDbType.VarChar).Value = psFecAsignada
        Cmd.Parameters.Add("@HorAsignada", SqlDbType.VarChar).Value = psHorAsignada
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@FecAsiVisto", SqlDbType.VarChar).Value = psFecAsiVisto
        Cmd.Parameters.Add("@HorAsiVisto", SqlDbType.VarChar).Value = psHorAsiVisto
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INS_PROBLEMA_ASIGNADO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsert_Item(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                 ByVal pdTipoProb As Double, ByVal pdCodItem As Double,
                                 ByVal psDescripItem As String, ByVal psTipoItem As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INS_ITEM", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoProb", SqlDbType.Float).Value = pdTipoProb
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = pdCodItem
        Cmd.Parameters.Add("@DescripItem", SqlDbType.VarChar).Value = psDescripItem
        Cmd.Parameters.Add("@TipoItem", SqlDbType.VarChar).Value = psTipoItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INS_ITEM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAUpdate_Item(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                    ByVal pdCodItem As Double, ByVal psDescripItem As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_UPD_ITEM", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = pdCodItem
        Cmd.Parameters.Add("@DescripItem", SqlDbType.VarChar).Value = psDescripItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_UPD_ITEM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_TemaAyuda(ByVal pCodigo As Double, ByVal pClasificacion As String,
                                   ByVal pTipo As String, ByVal pNombreArchivo As String,
                                   ByVal pDescripcion As String, ByVal pUser As String,
                                   ByVal pTipoIngreso As String, ByVal Conexion As String,
                                   ByVal pCodEmpresa As String, ByVal pCodProb As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_TEMAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Clasificacion", SqlDbType.VarChar).Value = pClasificacion
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar).Value = pNombreArchivo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@CodProb", SqlDbType.Float).Value = pCodProb
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_TEMAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodBaseDatos As Double,
                                ByVal pCodAplicativo As Double, ByVal pCodProducto As Double,
                                ByVal pCodSubProd As Double, ByVal pTransaccion As String,
                                ByVal pConsulta As String, ByVal pSolucion As String,
                                ByVal pTipoIngreso As String, ByVal Conexion As String,
                                ByVal psCategoria As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_BASEDATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBaseDatos", SqlDbType.Int).Value = pCodBaseDatos
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Int).Value = pCodSubProd
        Cmd.Parameters.Add("@Transaccion", SqlDbType.VarChar).Value = pTransaccion
        Cmd.Parameters.Add("@Consulta", SqlDbType.VarChar).Value = pConsulta
        Cmd.Parameters.Add("@Solucion", SqlDbType.VarChar).Value = pSolucion
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = psCategoria
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Aviso(ByVal pCodAviso As Double, ByVal pTipoAviso As String,
                                   ByVal pDescrip As String, ByVal pEstadoAviso As String,
                                   ByVal pUser As String, ByVal pTipoIngreso As String,
                                   ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_AVISO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodAviso", SqlDbType.Float).Value = pCodAviso
        Cmd.Parameters.Add("@TipoAviso", SqlDbType.VarChar).Value = pTipoAviso
        Cmd.Parameters.Add("@Descrip", SqlDbType.VarChar).Value = pDescrip
        Cmd.Parameters.Add("@EstadoAviso", SqlDbType.VarChar).Value = pEstadoAviso
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_AVISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_PuclicaAviso(ByVal pCodAviso As Double, ByVal pUser As String,
                                          ByVal pTipoIngreso As String, ByVal Conexion As String,
                                          ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_PUBLICARAVISO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodAviso", SqlDbType.Float).Value = pCodAviso
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_PUBLICARAVISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Enlace(ByVal pCodigo As Double, ByVal pDescripcion As String,
                                    ByVal pUrl As String, ByVal pTipoIngreso As String,
                                    ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_ENLACE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Url", SqlDbType.VarChar).Value = pUrl
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_ENLACE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Empresa(ByVal pCodigo As Double, ByVal pNombre As String,
                                     ByVal pUser As String, ByVal pTipoIngreso As String,
                                     ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_EMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_EMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Oficina(ByVal pCodigo As Double, ByVal pCodigoint As String,
                                     ByVal pNombre As String, ByVal pEmpresa As String,
                                     ByVal pUser As String, ByVal pTipoIngreso As String,
                                     ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_OFICINA", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Codigoint", SqlDbType.VarChar).Value = pCodigoint
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@Empresa", SqlDbType.VarChar).Value = pEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Puesto(ByVal pCodigo As Double, ByVal pNombre As String,
                                    ByVal pCodInterno As String, ByVal pTipoIngreso As String,
                                    ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_PUESTO", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_PUESTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Territorio(ByVal pCodTerritorio As Double, ByVal pCodInterno As String,
                                        ByVal pNombre As String, ByVal pTipoIngreso As String,
                                        ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_TERRITORIO", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Territorio", SqlDbType.Float).Value = pCodTerritorio
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_TERRITORIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Criterio(ByVal pCodEmpresa As String, ByVal pTipo As String,
                                      ByVal pCodigo As Double, ByVal pDescripcion As String,
                                      ByVal pInicia As String, ByVal pTipoIngreso As String,
                                      ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_CRITERIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Inicia", SqlDbType.VarChar).Value = pInicia
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_CRITERIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Grupo(ByVal pCodigo As Double, ByVal pNombre As String,
                                   ByVal User As String, ByVal pTipoIngreso As String,
                                   ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_GRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = User
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_GRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_RelacionGrupo(ByVal pCodGrupo As Double, ByVal pUsuario As String,
                                           ByVal pTipoIngreso As String, ByVal Conexion As String,
                                           ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_RELACIONGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = pCodGrupo
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_RELACIONGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_UsuarioNivel(ByVal pNivel As String, ByVal pUsuario As String,
                                          ByVal pTipoIngreso As String, ByVal Conexion As String,
                                          ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_USUARIONIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pNivel
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_USUARIONIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_ComponentexGrupo(ByVal pCodigo As Double, ByVal CodComponente As Double,
                                              ByVal pTipoIngreso As String, ByVal Conexion As String,
                                              ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_COMPONENTEXGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@CodComponente", SqlDbType.Float).Value = CodComponente
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_COMPONENTEXGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAInsUpd_Personas(ByVal persona_codigo As Double, ByVal persona_Usuario As String,
                                    ByVal persona_nombre As String, ByVal Persona_Apellidos As String,
                                    ByVal oficina As Double, ByVal Puesto As Double,
                                    ByVal telefono As String, ByVal anexo As String,
                                    ByVal correo As String, ByVal CodBanca As Double,
                                    ByVal Filler As String, ByVal Antiguedad As Decimal,
                                    ByVal Territorio As Double, ByVal TipoIngreso As String,
                                    ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_INSUPD_CASPERSONA", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@persona_codigo", SqlDbType.Float).Value = persona_codigo
        Cmd.Parameters.Add("@persona_Usuario", SqlDbType.VarChar).Value = persona_Usuario
        Cmd.Parameters.Add("@persona_nombre", SqlDbType.VarChar).Value = persona_nombre
        Cmd.Parameters.Add("@Persona_Apellidos", SqlDbType.VarChar).Value = Persona_Apellidos
        Cmd.Parameters.Add("@oficina", SqlDbType.Float).Value = oficina
        Cmd.Parameters.Add("@Puesto", SqlDbType.Float).Value = Puesto
        Cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = telefono
        Cmd.Parameters.Add("@anexo", SqlDbType.VarChar).Value = anexo
        Cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo
        Cmd.Parameters.Add("@CodBanca", SqlDbType.Float).Value = CodBanca
        Cmd.Parameters.Add("@Filler", SqlDbType.VarChar).Value = Filler
        Cmd.Parameters.Add("@Antiguedad", SqlDbType.Decimal).Value = Antiguedad
        Cmd.Parameters.Add("@Territorio", SqlDbType.Float).Value = Territorio
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = TipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_INSUPD_CASPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    'CONSULTA
    Public Function MAConsulta_Problema(ByVal Conexion As String, ByVal pCodEmpresa As String, _
                                        ByVal pCodProblema As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_CONSULTA_PROBLEMA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProblema", SqlDbType.Float).Value = pCodProblema
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_CONSULTA_PROBLEMA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_xProblema(ByVal Conexion As String, ByVal pCodEmpresa As String, _
                                         ByVal pCodProblema As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_CONSULTA_XPROBLEMA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProblema", SqlDbType.Float).Value = pCodProblema
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_CONSULTA_XPROBLEMA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExistePersona(ByVal Usuario As String, ByVal persona_nombre As String, _
                                      ByVal Persona_Apellidos As String, ByVal TipoIngreso As String, _
                                              ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEPERSONA", Cn)
        Cmd.CommandTimeout = 50
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = Usuario
        Cmd.Parameters.Add("@persona_nombre", SqlDbType.VarChar).Value = persona_nombre
        Cmd.Parameters.Add("@Persona_Apellidos", SqlDbType.VarChar).Value = Persona_Apellidos
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = TipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteEmpresa(ByVal Nombre As String, ByVal Conexion As String, _
                                             ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEEMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEEMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteOficina(ByVal Codigoint As String, ByVal Conexion As String, _
                                             ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEOFICINA", Cn)
        Cmd.CommandTimeout = 50
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Codigoint", SqlDbType.VarChar).Value = Codigoint
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEOFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExistePuetso(ByVal Nombre As String, ByVal pCodInterno As String, _
                                            ByVal pTipoIngreso As String, ByVal Conexion As String, _
                                            ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEPUESTO", Cn)
        Cmd.CommandTimeout = 50
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEPUESTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteGrupo(ByVal CodGrupo As Double, ByVal Nombre As String, _
                                           ByVal TipoConsulta As String, ByVal Conexion As String, _
                                           ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = CodGrupo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = TipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteUsuario(ByVal Usuario_Accion As String, _
                                             ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Usuario_Accion", SqlDbType.VarChar).Value = Usuario_Accion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteCriterio(ByVal pDescripcion As String, ByVal pTipo As String, _
                                               ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTECRITERIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Dt As New DataTable("ADMIN_EXISTECRITERIO")
        Dim Da As New SqlDataAdapter(Cmd)
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteGrupoxComponente(ByVal CodGrupo As Double, ByVal CodComponente As Double, _
                                                      ByVal TipoConsulta As String, ByVal Conexion As String, _
                                                      ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEGRUPO_XCOMPONENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = CodGrupo
        Cmd.Parameters.Add("@CodComponente", SqlDbType.Float).Value = CodComponente
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = TipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEGRUPO_XCOMPONENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteGrupoxUsuario(ByVal CodGrupo As Double, ByVal pUser As String, _
                                                   ByVal pTipoConsulta As String, ByVal Conexion As String, _
                                                   ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEGRUPO_XUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = CodGrupo
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEGRUPO_XUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteAviso(ByVal pUser As String, ByVal Conexion As String, _
                                           ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEAVISO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEAVISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteN1Usuario(ByVal pUser As String, ByVal Conexion As String, _
                                               ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEUSUARIOxN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEUSUARIOxN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteUsuarioNivel(ByVal pNivel As String, ByVal pUsuario As String, _
                                                   ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTEUSUARIONIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.VarChar).Value = pNivel
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTEUSUARIONIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_CantInc(ByVal pCodEmpresa As String, ByVal pFechaIni As String, _
                                        ByVal pFechaFin As String, ByVal pEstado As String, _
                                        ByVal pImportancia As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_COUNT_INCIDENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@Importancia", SqlDbType.VarChar).Value = pImportancia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_COUNT_INCIDENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteTerritorio(ByVal pCodInterno As String, ByVal Conexion As String, _
                                                ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTETERRITORIO", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTETERRITORIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function MAConsulta_ExisteTemaAyuda(ByVal pNombreArchivo As String, ByVal Conexion As String, _
                                               ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("ADMIN_EXISTETEMAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar).Value = pNombreArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("ADMIN_EXISTETEMAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class