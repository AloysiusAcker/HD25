Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Datos_del_Bien
    Public Function Ingresar_Equipo(ByVal psConexion As String, ByVal NroSerie As String, ByVal NroPlaca As String,
                                              ByVal SerieResponsableObservacion As String, ByVal SerieEstado As String,
                                    ByVal SerieResponsable As String, ByVal SerieArea As String, ByVal SerieNumerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_INGRESAR_EQUIPO]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NroSerie", NroSerie)
        Cmd.Parameters.AddWithValue("@NroPlaca", NroPlaca)
        Cmd.Parameters.AddWithValue("@SerieResponsableObservacion", SerieResponsableObservacion)
        Cmd.Parameters.AddWithValue("@SerieEstado", SerieEstado)
        Cmd.Parameters.AddWithValue("@SerieResponsable", SerieResponsable)
        Cmd.Parameters.AddWithValue("@SerieArea", SerieArea)
        Cmd.Parameters.AddWithValue("@SerieNumerar", SerieNumerar)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_LISTA_EQUIPOS_BUSCA]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Datos_del_Bien(ByVal psConexion As String, ByVal codEmpresa As String,
                                         ByVal NroSerie As String, ByVal NroPlaca As String,
                                         ByVal CodArea As String, ByVal DesArticulo As String,
                                         ByVal Descripcion As String, ByVal CodRelacionador As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[PRC_LISTA_EQUIPOS_BUSCA]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa)
        Cmd.Parameters.AddWithValue("@NroSerie", NroSerie)
        Cmd.Parameters.AddWithValue("@NroPlaca", NroPlaca)
        Cmd.Parameters.AddWithValue("@Articulo", Descripcion + DesArticulo)
        Cmd.Parameters.AddWithValue("@CodRelacionado", CodRelacionador)
        Cmd.Parameters.AddWithValue("@CodArea", CodArea)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[PRC_LISTA_EQUIPOS_BUSCA]")
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
    Public Function Llenar_Combo_Ubicacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Personal(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Personal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Personal")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Estado(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Estado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Estado")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_Serie_Numerar(ByVal psConexion As String, ByVal placa As String,
                                                  ByVal serie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Buscar_Serie_Numerar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NRO_PLACA", placa)
        Cmd.Parameters.AddWithValue("@NRO_SERIE", serie)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Buscar_Serie_Numerar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cargar_Articulos(ByVal psConexion As String, ByVal numerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_VERIFICAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", "0001")
        Cmd.Parameters.AddWithValue("@ArtDescripcion", "")
        Cmd.Parameters.AddWithValue("@SerieNumerar", numerar)
        Cmd.Parameters.AddWithValue("@SerieNRO", "")
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_VERIFICAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cargar_Articulos1(ByVal psConexion As String, ByVal numerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Articulos_Tabla1", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NUMERAR", numerar)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Articulos_Tabla1")
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
