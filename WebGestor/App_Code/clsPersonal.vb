Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class ClsPersonal


    Public Function _Asistencia_GuardarImagen(ByVal Codigo As String, ByVal psFechaReg As String, ByVal img As Byte()) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_EntSal_Insertar_Foto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Personalcod", SqlDbType.VarChar).Value = Codigo
        Cmd.Parameters.Add("@FechaReg", SqlDbType.VarChar).Value = psFechaReg
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@PersonalFoto", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EntSal_Insertar_Foto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GuardarImagen(ByVal psConexion As String, ByVal Codigo As String, ByVal img As Byte()) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Personal_Insertar_Foto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Personalcod", SqlDbType.VarChar).Value = Codigo
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@PersonalFoto", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Personal_Insertar_Foto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Verificar_Personal(ByVal pCodPersonal As String, ByVal psTipoConsulta As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("VERIFICA_PERSONAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = psTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("VERIFICA_PERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_PermisoHoraEntSal(ByVal pCodPersonal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTA_PERMISOHORAENTSAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTA_PERMISOHORAENTSAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_HoraEntSal(ByVal pCodPersonal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("LISTA_HORAENTSAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTA_HORAENTSAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_PermisoHoraIngreso(ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                            ByVal pCodPersonal As String, ByVal pFechaAct As String,
                            ByVal pHorPerIng As String, ByVal pContarTipo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("UPDATE_HORAINGPERMISO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@FechaAct", SqlDbType.VarChar).Value = pFechaAct
        Cmd.Parameters.Add("@HorPerIng", SqlDbType.VarChar).Value = pHorPerIng
        Cmd.Parameters.Add("@ContarTipo", SqlDbType.VarChar).Value = pContarTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("UPDATE_HORAINGPERMISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_HoraSalida(ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                            ByVal pCodPersonal As String, ByVal pFechaAct As String,
                            ByVal pHoraSalida As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("UPDATE_HORASALIDA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@FechaAct", SqlDbType.VarChar).Value = pFechaAct
        Cmd.Parameters.Add("@HoraSalida", SqlDbType.VarChar).Value = pHoraSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("UPDATE_HORASALIDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_PermisoHoraSalida(ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                            ByVal pCodPersonal As String, ByVal pFechaAct As String,
                            ByVal pHorPerSal As String, ByVal pContarTipo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("INSERTAR_HORASALPERMISO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@FechaAct", SqlDbType.VarChar).Value = pFechaAct
        Cmd.Parameters.Add("@HorPerSal", SqlDbType.VarChar).Value = pHorPerSal
        Cmd.Parameters.Add("@ContarTipo", SqlDbType.VarChar).Value = pContarTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INSERTAR_HORASALPERMISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_HoraIngreso(ByVal CodGrupoEmpresa As Double, ByVal CodEmpresa As String,
                            ByVal pCodPersonal As String, ByVal pFechaAct As String,
                            ByVal pHoraIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("INSERTAR_HORAINGRESO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@FechaAct", SqlDbType.VarChar).Value = pFechaAct
        Cmd.Parameters.Add("@HoraIngreso", SqlDbType.VarChar).Value = pHoraIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INSERTAR_HORAINGRESO")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
