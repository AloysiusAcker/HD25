Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Conciliados
    Public Function Lista_Equipos_Conciliados(ByVal psConexion As String, ByVal codEmpresa As String,
                                              ByVal EstInventario As String, ByVal EstConciliacion As String,
                                              ByVal NroSerie As String,
                                              ByVal NroPlaca As Double,
                                              ByVal CodArticulo As String, ByVal DesArticulo As String,
                                              ByVal Descripcion As String,
                                              ByVal TipoUbicacion As String, ByVal CodUbicacion As Double,
                                              ByVal CodArea As Double, ByVal TipoLista As String,
                                              ByVal TipoListaArea As String,
                                              ByVal Clasifi As String, ByVal CodRelacionador As String, ByVal CodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[SPINV_LISTA_EQUIPOS_CONCILIADOS]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa)
        Cmd.Parameters.AddWithValue("@Inventariado", EstInventario)
        Cmd.Parameters.AddWithValue("@Conciliado", EstConciliacion)
        Cmd.Parameters.AddWithValue("@NroSerie", NroSerie)
        Cmd.Parameters.AddWithValue("@NroPlaca", NroPlaca)
        Cmd.Parameters.AddWithValue("@CodArticulo", CodArticulo)
        Cmd.Parameters.AddWithValue("@ArtDescripcion", Descripcion + DesArticulo)
        Cmd.Parameters.AddWithValue("@TipoUbicacion", TipoUbicacion) 'RADIOBUTTONS
        Cmd.Parameters.AddWithValue("@CodigoUbicacion", CodUbicacion) 'AREA
        Cmd.Parameters.AddWithValue("@CodArea", CodArea) 'Ubicacion
        Cmd.Parameters.AddWithValue("@TipoLista", TipoLista)
        Cmd.Parameters.AddWithValue("@TipoListaArea", TipoListaArea)
        Cmd.Parameters.AddWithValue("@NumeroClas", Clasifi)
        Cmd.Parameters.AddWithValue("@CodRelacionador", CodRelacionador)
        Cmd.Parameters.AddWithValue("@CodInventario", CodInventario)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[SPINV_LISTA_EQUIPOS_CONCILIADOS]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_Conciliar_ListaNoInventariado(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                             ByVal psCodInvUbica As Double, ByVal pdCodArt As Double, ByVal psNombreArt As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario_Conciliacion_NoInventariados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.VarChar).Value = pCodInventario
        Cmd.Parameters.Add("@CodInvUbic", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = psNombreArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario_Conciliacion_NoInventariados")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function ListaCantidadesXActivos(ByVal psConexion As String, ByVal codEmpresa As String,
                                              ByVal EstInventario As String, ByVal EstConciliacion As String,
                                              ByVal NroSerie As String, ByVal NroPlaca As Double,
                                              ByVal CodArticulo As Double, ByVal DesArticulo As String,
                                              ByVal Descripcion As String, ByVal TipoUbicacion As String, ByVal CodUbicacion As Double,
                                              ByVal CodArea As Double, ByVal TipoLista As String,
                                              ByVal TipoListaArea As String,
                                              ByVal Clasifi As String, ByVal CodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_INVENTARIO_CANTIDADESxACTIVOS]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa)
        Cmd.Parameters.AddWithValue("@Inventariado", EstInventario)
        Cmd.Parameters.AddWithValue("@Conciliado", EstConciliacion)
        Cmd.Parameters.AddWithValue("@NroSerie", NroSerie)
        Cmd.Parameters.AddWithValue("@NroPlaca", NroPlaca)
        Cmd.Parameters.AddWithValue("@CodArticulo", CodArticulo)
        Cmd.Parameters.AddWithValue("@ArtDescripcion", Descripcion + DesArticulo)
        Cmd.Parameters.AddWithValue("@TipoUbicacion", TipoUbicacion)
        Cmd.Parameters.AddWithValue("@CodigoUbicacion", CodUbicacion)
        Cmd.Parameters.AddWithValue("@CodArea", CodArea)
        Cmd.Parameters.AddWithValue("@TipoLista", TipoLista)
        Cmd.Parameters.AddWithValue("@TipoListaArea", TipoListaArea)
        Cmd.Parameters.AddWithValue("@NumeroClas", Clasifi)
        Cmd.Parameters.AddWithValue("@CodInventario", CodInventario)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_INVENTARIO_CANTIDADESxACTIVOS]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Equipos_No_Inventariados(ByVal psConexion As String, ByVal codEmpresa As String,
                                                   ByVal CodArticulo As String, ByVal DesArticulo As String,
                                                   ByVal Descripcion As String, ByVal TipoLista As String,
                                                   ByVal NroSerie As String, ByVal CodInventario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[SPINV_LISTA_EQUIPOS_NOINVENTARIADO]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa)
        Cmd.Parameters.AddWithValue("@CodArticulo", CodArticulo)
        Cmd.Parameters.AddWithValue("@ArtDescripcion", Descripcion + DesArticulo)
        Cmd.Parameters.AddWithValue("@TipoLista", TipoLista)
        Cmd.Parameters.AddWithValue("@NroSerie", NroSerie)
        Cmd.Parameters.AddWithValue("@CodInventario", CodInventario)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[SPINV_LISTA_EQUIPOS_CONCILIADOS]")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Lista_Inventario_Verificacion_xEstado
    Public Function Lista_Resumen(ByVal psConexion As String, ByVal CodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Inventario_Resumen]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@CodInventario", CodInventario)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Inventario_Resumen]")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Lista_Resumen_xUbicacion(ByVal psConexion As String, ByVal CodInventario As Double, ByVal pdCodUbicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Inventario_Resumen_xUbicacion]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@CodInventario", CodInventario)
        Cmd.Parameters.AddWithValue("@CodUbicacion", pdCodUbicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Inventario_Resumen_xUbicacion]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Combo_Inventario(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Estado_Inventario(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Estado_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Estado_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Estado_Conciliacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Estado_Conciliacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Estado_Conciliacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Ubicacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Bus_Conciliados(ByVal psConexion As String, ByVal NroSerie As String,
                                    ByVal NroPlaca As String, ByVal CodArticulo As String,
                                    ByVal ArtDescripcion As String, ByVal TipoUbicacion As String,
                                    ByVal CodigoUbicacion As String, ByVal CodArea As String,
                                    ByVal TipoLista As String, ByVal TipoListaArea As String,
                                    ByVal NumeroClas As String, ByVal CodRelacionador As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_BuscarConciliados]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "")
        Cmd.Parameters.AddWithValue("@Inventariado", "")
        Cmd.Parameters.AddWithValue("@Conciliado", "")
        Cmd.Parameters.AddWithValue("@NroSerie", NroSerie)
        Cmd.Parameters.AddWithValue("@NroPlaca", NroPlaca)
        Cmd.Parameters.AddWithValue("@CodArticulo", CodArticulo)
        Cmd.Parameters.AddWithValue("@ArtDescripcion", ArtDescripcion)
        Cmd.Parameters.AddWithValue("@TipoUbicacion", TipoUbicacion)
        Cmd.Parameters.AddWithValue("@CodigoUbicacion", CodigoUbicacion)
        Cmd.Parameters.AddWithValue("@CodArea", CodArea)
        Cmd.Parameters.AddWithValue("@TipoLista", TipoLista)
        Cmd.Parameters.AddWithValue("@TipoListaArea", TipoListaArea)
        Cmd.Parameters.AddWithValue("@NumeroClas", NumeroClas)
        Cmd.Parameters.AddWithValue("@CodRelacionador", CodRelacionador)
        Cmd.Parameters.AddWithValue("@CodInventario", "0")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_BuscarConciliados]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Bus_Articulo(ByVal psConexion As String, ByVal Codigo As String,
                                 ByVal Clasificacion As String, ByVal Descripcion As String,
                                 ByVal Tipo As String, ByVal NuPart As String,
                                 ByVal CodEs As String, ByVal marca As String, ByVal modelo As String, ByVal ListaArt As String,
                                 ByVal ListaMarca As String, ByVal ListaModelo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarArticulos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure

        Cmd.Parameters.AddWithValue("@V_ART_CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@V_CLAS_NUMERO", Clasificacion)
        Cmd.Parameters.AddWithValue("@V_DESCRIP", Descripcion)
        Cmd.Parameters.AddWithValue("@V_TIPO", Tipo)
        Cmd.Parameters.AddWithValue("@V_PARTE", NuPart)
        Cmd.Parameters.AddWithValue("@V_CODESP", CodEs)
        Cmd.Parameters.AddWithValue("@V_MAR", marca)
        Cmd.Parameters.AddWithValue("@V_MOD", modelo)
        Cmd.Parameters.AddWithValue("@ListaArt", ListaArt)
        Cmd.Parameters.AddWithValue("@ListaMarca ", ListaMarca)
        Cmd.Parameters.AddWithValue("@ListaModelo", ListaModelo)


        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarArticulos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Almacenes_Inventario(ByVal psConexion As String, ByVal codigo As String,
                                               ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Almacenes_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Almacenes_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_CentroC_Inventario(ByVal psConexion As String, ByVal codigo As String,
                                               ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_CentroC_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_CentroC_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Serie_Numerar(ByVal psConexion As String, ByVal NroPlaca As String, ByVal NroSerie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Buscar_Serie_Numerar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NRO_PLACA", NroPlaca)
        Cmd.Parameters.AddWithValue("@NRO_SERIE", NroSerie)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Buscar_Serie_Numerar")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Actualizar_Estado_Inventario(ByVal psConexion As String, ByVal SerieNum As String, ByVal Conciliados As String, ByVal Estado As String, ByVal psCodUbicInv As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Cambiar_Estado_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@SERIE_NUMERAR", SerieNum)
        Cmd.Parameters.AddWithValue("@CONCILIADOS", Conciliados)
        Cmd.Parameters.AddWithValue("@ESTADO", Estado)
        '@CodUbicInv
        Cmd.Parameters.AddWithValue("@CodUbicInv", psCodUbicInv)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Cambiar_Estado_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Conciliar_Equipos(ByVal psConexion As String, ByVal SerieNum As String, CodArticulo As String,
                                      ByVal CodRelacionador As String, ByVal NroSerie As String,
                                    ByVal Combo As String, ByVal SerieNumNoInv As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Conciliar_Equipos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@SERIE_NUMERAR", SerieNum)
        Cmd.Parameters.AddWithValue("@CodArticulo", CodArticulo)
        Cmd.Parameters.AddWithValue("@CodRelacionador", CodRelacionador)
        Cmd.Parameters.AddWithValue("@COMBO", Combo)
        Cmd.Parameters.AddWithValue("@SerienumerarNoinv", SerieNumNoInv)
        Cmd.Parameters.AddWithValue("@CodUbicInv", 1)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Conciliar_Equipos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Marca(ByVal psConexion As String, ByVal codigo As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarMarca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MAR", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIP", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarMarca")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Modelo(ByVal psConexion As String, ByVal codigoMo As String, ByVal descripcion As String, ByVal codMar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarModelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MOD", codigoMo)
        Cmd.Parameters.AddWithValue("@DESCRIPC", descripcion)
        Cmd.Parameters.AddWithValue("@COD_MARC", codMar)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarModelo")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
