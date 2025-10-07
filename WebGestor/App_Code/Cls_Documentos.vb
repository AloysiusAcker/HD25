Imports System.Data
Imports System.Data.SqlClient

Public Class Cls_Documentos
    Public Function Lista_Documentos(ByVal psConexion As String, ByVal Usuario As String, ByVal NroTicket As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_DOC_LIST_DOCUMENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = Usuario
        Cmd.Parameters.Add("@REFERENCIA", SqlDbType.Float).Value = NroTicket
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_LIST_DOCUMENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_Documento(ByVal psConexion As String, ByVal Usuario As String, ByVal pCodDocumento As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_DOC_BUSCAR_XCODIGO]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodDocumento", SqlDbType.Float).Value = pCodDocumento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_DOC_BUSCAR_XCODIGO]")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Codigo(ByVal psConexion As String) As String
        Dim TxtCodigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT MAX(TEMA_AYUDA_CODIGO) FROM TBTEMA_AYUDA_GENERAL"
            Rs = CmdGlobal.ExecuteReader

            If Rs.HasRows Then
                While Rs.Read
                    TxtCodigo = 1 + Rs(0)
                End While
            Else
                TxtCodigo = 1
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return TxtCodigo
    End Function
    Public Function Lista_Aplicacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_DOC_LLENAR_COMBO_APLICACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_LLENAR_COMBO_APLICACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Tipo_Ingreso(ByVal psConexion As String, ByVal codAplicacion As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_DOC_LLENAR_COMBO_TIPO_INGRESO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_APLICACION", codAplicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_LLENAR_COMBO_TIPO_INGRESO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ActualizaDocumentos(ByVal psConexion As String, ByVal codigo As String, ByVal aplicacion As String, ByVal tipoIngreso As String,
                                        ByVal fechaIngreso As String, ByVal clasif1 As String, ByVal clasif2 As String,
                                        ByVal clasif3 As String, ByVal nivelAcceso As String, ByVal nombreDoc As String,
                                        ByVal referencia As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_DOC_UPD_DOCUMENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TA_AYUDA_CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@TA_APLICACION_DESCRIPCION", aplicacion)
        Cmd.Parameters.AddWithValue("@TIPOINGRESO", tipoIngreso)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_FECHA_INGRESO", fechaIngreso)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_CLASIFN1", clasif1)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_CLASIFN2", clasif2)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_CLASIFN3", clasif3)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_NIVEL_ACCESO", nivelAcceso)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_NOMBRE_DOC", nombreDoc)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_REFERENCIA", referencia)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_DESCRIPCION", descripcion)

        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_UPD_DOCUMENTOS")
        Da.Fill(Dt)
        Return Dt

    End Function
    Public Function Eliminar_Documentos(ByVal psConexion As String, ByVal codigoDocumento As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_DOC_DEL_DOCUMENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodDocumento", codigoDocumento)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_DEL_DOCUMENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Registrar_Documentos(ByVal psConexion As String, ByVal clasifn1 As String,
                                         ByVal clasifn2 As String, ByVal clasifn3 As String,
                                         ByVal nomDoc As String, ByVal descDoc As String, ByVal usuario As String,
                                         ByVal fecIngreso As String, ByVal sysCre As String, ByVal tipoIngreso As String,
                                         ByVal nivelAcceso As String, ByVal aplicacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_DOC_INS_DOCUMENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_CLASIFN1", clasifn1)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_CLASIFN2", clasifn2)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_CLASIFN3", clasifn3)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_NOMBRE_DOC", nomDoc)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_DESCRIPCION", descDoc)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_USUARIO", usuario)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_FECHA_INGRESO", fecIngreso)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_SYS_CRE", sysCre)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_TIPO_INGRESO", tipoIngreso)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_NIVEL_ACCESO", nivelAcceso)
        Cmd.Parameters.AddWithValue("@TEMA_AYUDA_APLICACION", aplicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_INS_DOCUMENTOS")
        Da.Fill(Dt)
        Return Dt

    End Function
    '
    Public Function Insertar_Documento(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psClasif1 As String, ByVal psClasif2 As String, ByVal psClasif3 As String, ByVal psDocNombre As String,
                                       ByVal psDocDescrip As String, ByVal psUser As String, ByVal psFecha As String, ByVal psValorSistema As String, ByVal psTipoIngreso As String, ByVal psNivel As String,
                                       ByVal psCodTablaRelacion As String, ByVal psCodModulo As String, ByVal psAplicacion As Double, ByVal pspscodArt As Double, ByVal psCodReferencia As Double,
                                       ByVal psIngreso As String, ByVal psCodDocumento As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ins_Documentos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Clasif1", SqlDbType.VarChar).Value = psClasif1
        Cmd.Parameters.Add("@Clasif2", SqlDbType.VarChar).Value = psClasif2
        Cmd.Parameters.Add("@Clasif3", SqlDbType.VarChar).Value = psClasif3
        Cmd.Parameters.Add("@DocNombre", SqlDbType.VarChar).Value = psDocNombre
        Cmd.Parameters.Add("@DocDescrip", SqlDbType.VarChar).Value = psDocDescrip
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@ValorSistema", SqlDbType.VarChar).Value = psValorSistema
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = psTipoIngreso
        Cmd.Parameters.Add("@Nivel", SqlDbType.VarChar).Value = psNivel
        Cmd.Parameters.Add("@CodTablaRelacion", SqlDbType.VarChar).Value = psCodTablaRelacion
        Cmd.Parameters.Add("@CodModulo", SqlDbType.VarChar).Value = psCodModulo
        Cmd.Parameters.Add("@Aplicacion", SqlDbType.Float).Value = psAplicacion
        Cmd.Parameters.Add("@pscodArt", SqlDbType.Float).Value = pspscodArt
        Cmd.Parameters.Add("@CodReferencia", SqlDbType.Float).Value = psCodReferencia
        Cmd.Parameters.Add("@Ingreso", SqlDbType.VarChar).Value = psIngreso
        Cmd.Parameters.Add("@CodDocumento", SqlDbType.Float).Value = psCodDocumento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ins_Documentos")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function DocConsulta_ExisteTemaAyuda(ByVal pNombreArchivo As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_DOC_EXISTETEMAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar).Value = pNombreArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_DOC_EXISTETEMAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
