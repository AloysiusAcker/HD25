Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class clsLogis_InsUpDel
    Public Function Insertar_Centro_Costos(ByVal Conexion As String, ByVal EmpresaCod As String,
       ByVal CostoCod As Double, ByVal CodInterno As String, ByVal descr As String,
       ByVal Piso As String, ByVal Direccion As String, ByVal Edificio As String,
       ByVal Ubicacion As String, ByVal Ruc As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBLOGIS_CENTRO_COSTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EmpresaCod
        Cmd.Parameters.Add("@CCOSTO_CODIGO", SqlDbType.Float).Value = CostoCod
        Cmd.Parameters.Add("@CCOSTO_COD_INTERNO", SqlDbType.VarChar).Value = CodInterno
        Cmd.Parameters.Add("@CCOSTO_DESCRIPCION", SqlDbType.VarChar).Value = descr
        Cmd.Parameters.Add("@CCOSTO_PISO", SqlDbType.VarChar).Value = Piso
        Cmd.Parameters.Add("@CCOSTO_DIRECCION", SqlDbType.VarChar).Value = Direccion
        Cmd.Parameters.Add("@CCOSTO_EDIFICIO", SqlDbType.VarChar).Value = Edificio
        Cmd.Parameters.Add("@CCOSTO_UBICACION", SqlDbType.VarChar).Value = Ubicacion
        Cmd.Parameters.Add("@CCOSTO_RUC", SqlDbType.VarChar).Value = Ruc
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBLOGIS_CENTRO_COSTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Costos_Seccion(ByVal Conexion As String, ByVal EmpresaCod As String, ByVal CodSeccion As Double,
                                            ByVal CostoCod As Double, ByVal CodInterno As String, ByVal Descripcion As String,
                                            ByVal Ruc As String, ByVal TipoEstablec As String, ByVal Direccion As String,
                                            ByVal Piso As String, ByVal Edificio As String, ByVal Ubica As String,
                                            ByVal Hall As String, ByVal psTta As String, ByVal psTsi As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBLOGIS_CENTRO_COSTO_SECCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EmpresaCod
        Cmd.Parameters.Add("@CECOSE_CODIGO", SqlDbType.Float).Value = CodSeccion
        Cmd.Parameters.Add("@CCOSTO_CODIGO", SqlDbType.Float).Value = CostoCod
        Cmd.Parameters.Add("@CECOSE_COD_INTERNO", SqlDbType.VarChar).Value = CodInterno
        Cmd.Parameters.Add("@CECOSE_DESCRIPCION", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@CECOSE_RUC", SqlDbType.VarChar).Value = Ruc
        Cmd.Parameters.Add("@CECOSE_TIPO", SqlDbType.VarChar).Value = TipoEstablec
        Cmd.Parameters.Add("@CECOSE_DIRECCION", SqlDbType.VarChar).Value = Direccion
        Cmd.Parameters.Add("@CECOSE_PISO", SqlDbType.VarChar).Value = Piso
        Cmd.Parameters.Add("@CECOSE_EDIFICIO", SqlDbType.VarChar).Value = Edificio
        Cmd.Parameters.Add("@CECOSE_UBICACION", SqlDbType.VarChar).Value = Ubica
        Cmd.Parameters.Add("@CECOSE_HALL", SqlDbType.VarChar).Value = Hall
        Cmd.Parameters.Add("@CECOSE_TTA", SqlDbType.VarChar).Value = psTta
        Cmd.Parameters.Add("@CECOSE_TSI", SqlDbType.VarChar).Value = psTsi
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBLOGIS_CENTRO_COSTO_SECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Update_Seccion(ByVal Conexion As String, ByVal CodEmpresa As String,
                                   ByVal CodSeccion As Double, ByVal CodInterno As String,
                                   ByVal Descripcion As String, ByVal Ruc As String, ByVal Tipo As String,
                                   ByVal Direccion As String, ByVal Piso As String,
                                   ByVal Edificio As String, ByVal Ubicacion As String,
                                   ByVal Hall As String, ByVal psTta As String,
                                   ByVal psTsi As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPLOGIS_UPD_SECCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodSeccion", SqlDbType.Float).Value = CodSeccion
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = CodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@Ruc", SqlDbType.VarChar).Value = Ruc
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = Tipo
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = Direccion
        Cmd.Parameters.Add("@Piso", SqlDbType.VarChar).Value = Piso
        Cmd.Parameters.Add("@Edificio", SqlDbType.VarChar).Value = Edificio
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = Ubicacion
        Cmd.Parameters.Add("@Hall", SqlDbType.VarChar).Value = Hall
        Cmd.Parameters.Add("@Tta", SqlDbType.VarChar).Value = psTta
        Cmd.Parameters.Add("@Tsi", SqlDbType.VarChar).Value = psTsi
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLOGIS_UPD_SECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Update_CentroCostos(ByVal Conexion As String, ByVal CodEmpresa As String,
                                        ByVal CodCosto As Double, ByVal CodInterno As String,
                                        ByVal Descripcion As String, ByVal Piso As String,
                                        ByVal Direccion As String, ByVal Edificio As String,
                                        ByVal Ubicacion As String, ByVal Ruc As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPLOGIS_UPD_CENTRO_COSTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodCosto", SqlDbType.Float).Value = CodCosto
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = CodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@Piso", SqlDbType.VarChar).Value = Piso
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = Direccion
        Cmd.Parameters.Add("@Edificio", SqlDbType.VarChar).Value = Edificio
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = Ubicacion
        Cmd.Parameters.Add("@Ruc", SqlDbType.VarChar).Value = Ruc
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLOGIS_UPD_CENTRO_COSTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
