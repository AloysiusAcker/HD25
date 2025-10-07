Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Catalogo

    Public Function Codigo(ByVal psConexion As String) As String
        Dim TxtCodigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT MAX(ART_CODIGO) FROM TBINV_ARTICULOS"
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

    Public Function Concat_Clasificacion(ByVal psConexion As String, ByVal codClasificacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_CONCAT_CLASIFICACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CLASIFICACION", codClasificacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_CONCAT_CLASIFICACION")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Catalogo(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_LIST_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", psCodEmpresa)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_LIST_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Sit(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_llenarSituacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_llenarSituacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Tipo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_llenarTipoArticulo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_llenarTipoArticulo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Tipo2(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_llenarTipoArticulo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_llenarTipoArticulo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Tipo_Bien(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_LLENAR_COMBO_TIPO_BIEN_ARTICULO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_LLENAR_COMBO_TIPO_BIEN_ARTICULO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Detraccion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_LLENAR_COMBO_DETRACCION_ARTICULO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_LLENAR_COMBO_DETRACCION_ARTICULO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Unidad(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_LlenarUnidMed", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_LlenarUnidMed")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Bus_Articulo(ByVal psConexion As String, ByVal Codigo As String,
                                 ByVal Clasificacion As String, ByVal Descripcion As String,
                                 ByVal Tipo As String, ByVal NuPart As String,
                                 ByVal CodEs As String, ByVal marca As String, ByVal modelo As String, ByVal ListaArt As String,
                                 ByVal ListaMarca As String, ByVal ListaModelo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_BUSCAR_ARTICULOS", Cn)
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
        Dim Dt As New DataTable("PROC_INV_BUSCAR_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Bus_Articulo_xSKU(ByVal psConexion As String, ByVal Codigo As String,
                                     ByVal Clasificacion As String, ByVal Descripcion As String,
                                     ByVal Tipo As String, ByVal NuPart As String,
                                     ByVal CodEs As String, ByVal marca As String, ByVal modelo As String, ByVal ListaArt As String,
                                     ByVal ListaMarca As String, ByVal ListaModelo As String, ByVal psSku As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_BUSCAR_ARTICULOS_xSKU", Cn)
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
        Cmd.Parameters.AddWithValue("@sku", psSku)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_BUSCAR_ARTICULOS_xSKU")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ArticuloxBusqueda(ByVal psConexion As String, ByVal Codigo As Double,
                                            ByVal Clasificacion As String, ByVal Descripcion As String,
                                            ByVal Tipo As String, ByVal NuPart As String,
                                            ByVal CodEs As String, ByVal marca As Double, ByVal modelo As Double, ByVal ListaArt As String,
                                            ByVal ListaMarca As String, ByVal ListaModelo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_BUSCAR_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@V_ART_CODIGO", SqlDbType.Float).Value = Codigo
        Cmd.Parameters.Add("@V_CLAS_NUMERO", SqlDbType.VarChar).Value = Clasificacion
        Cmd.Parameters.Add("@V_DESCRIP", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@V_TIPO", SqlDbType.VarChar).Value = Tipo
        Cmd.Parameters.Add("@V_PARTE", SqlDbType.VarChar).Value = NuPart
        Cmd.Parameters.Add("@V_CODESP", SqlDbType.VarChar).Value = CodEs
        Cmd.Parameters.Add("@V_MAR", SqlDbType.Float).Value = marca
        Cmd.Parameters.Add("@V_MOD", SqlDbType.Float).Value = modelo
        Cmd.Parameters.Add("@ListaArt", SqlDbType.VarChar).Value = ListaArt
        Cmd.Parameters.Add("@ListaMarca", SqlDbType.VarChar).Value = ListaMarca
        Cmd.Parameters.Add("@ListaModelo", SqlDbType.VarChar).Value = ListaModelo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_BUSCAR_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_Inv_BuscarArticuloNombreImagen
    Public Function BuscarArticuloNombreImagen(ByVal psConexion As String, ByVal pCodArt As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_BuscarArticuloNombreImagen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_BuscarArticuloNombreImagen")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ArticuloxUbicacionNuevos(ByVal psConexion As String, ByVal pCodUbicacion As Double, ByVal Descripcion As String, ByVal pdCodArt As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Articulos_NuevosxUbicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.Float).Value = pCodUbicacion
        Cmd.Parameters.Add("@ArtDescripcion", SqlDbType.VarChar).Value = Descripcion '@ArtCodigo
        Cmd.Parameters.Add("@ArtCodigo", SqlDbType.Float).Value = pdCodArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Articulos_NuevosxUbicacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function RegistrarCatalogo(ByVal psConexion As String, ByVal Codigo As Double,
                                      ByVal Tipo As Double, ByVal Clasifi As Double, ByVal codMar As Double,
                                      ByVal codMod As Double, ByVal codModDetalle As Double, ByVal descripcion As String,
                                      ByVal abrev As String, ByVal parte As String, ByVal codEs As String, ByVal uniMe As Double,
                                      ByVal detraccion As Double, ByVal tipoBien As String, ByVal peso As Double, ByVal volumen As Double,
                                      ByVal alto As Double, ByVal ancho As Double, ByVal largo As Double, ByVal psUser As String, ByVal pSku As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_INS_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@V_CODART", SqlDbType.Float).Value = Codigo
        Cmd.Parameters.Add("@V_TIPO", SqlDbType.Float).Value = Tipo
        Cmd.Parameters.Add("@V_CLASIFICACION", SqlDbType.Float).Value = Clasifi
        Cmd.Parameters.Add("@V_ARTMAR_CODIGO", SqlDbType.Float).Value = codMar
        Cmd.Parameters.Add("@V_ARTMOD_CODIGO", SqlDbType.Float).Value = codMod
        Cmd.Parameters.Add("@V_ARTMODET_CODIGO", SqlDbType.Float).Value = codModDetalle
        Cmd.Parameters.Add("@V_DESCRIP", SqlDbType.VarChar).Value = descripcion
        Cmd.Parameters.Add("@V_ART_ABREV", SqlDbType.VarChar).Value = abrev
        Cmd.Parameters.Add("@V_ART_CODEQUNRO", SqlDbType.VarChar).Value = parte
        Cmd.Parameters.Add("@V_ART_CODESP", SqlDbType.VarChar).Value = codEs
        Cmd.Parameters.Add("@V_UNIMED", SqlDbType.Float).Value = uniMe
        Cmd.Parameters.Add("@V_DETRACCION", SqlDbType.Float).Value = detraccion
        Cmd.Parameters.Add("@V_SUNAT", SqlDbType.VarChar).Value = tipoBien
        Cmd.Parameters.Add("@V_PESO", SqlDbType.Float).Value = peso
        Cmd.Parameters.Add("@V_VOLUMEN", SqlDbType.Float).Value = volumen
        Cmd.Parameters.Add("@V_VOL_ALTO", SqlDbType.Float).Value = alto
        Cmd.Parameters.Add("@V_VOL_ANCHO", SqlDbType.Float).Value = ancho
        Cmd.Parameters.Add("@V_VOL_LARGO", SqlDbType.Float).Value = largo
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Sku", SqlDbType.VarChar).Value = pSku
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_INS_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function ActualizaCatalogo(ByVal psConexion As String, ByVal Codigo As Double, ByVal Tipo As Double, ByVal Clasifi As Double,
                                      ByVal codMar As Double, ByVal codMod As Double, ByVal descripcion As String, ByVal abrev As String,
                                      ByVal parte As String, ByVal codEs As String, ByVal uniMe As Double, ByVal detraccion As Double,
                                      ByVal tipoBien As String, ByVal peso As Double, ByVal volumen As Double,
                                      ByVal alto As Double, ByVal ancho As Double, ByVal largo As Double, ByVal psSku As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_UPD_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@V_CODART", SqlDbType.Float).Value = Codigo
        Cmd.Parameters.Add("@V_TIPO", SqlDbType.Float).Value = Tipo
        Cmd.Parameters.Add("@V_CLASIFICACION", SqlDbType.Float).Value = Clasifi
        Cmd.Parameters.Add("@V_ARTMAR_CODIGO", SqlDbType.Float).Value = codMar
        Cmd.Parameters.Add("@V_ARTMOD_CODIGO", SqlDbType.Float).Value = codMod
        Cmd.Parameters.Add("@V_DESCRIP", SqlDbType.VarChar).Value = descripcion
        Cmd.Parameters.Add("@V_ART_ABREV", SqlDbType.VarChar).Value = abrev
        Cmd.Parameters.Add("@V_ART_CODEQUNRO", SqlDbType.VarChar).Value = parte
        Cmd.Parameters.Add("@V_ART_CODESP", SqlDbType.VarChar).Value = codEs
        Cmd.Parameters.Add("@V_UNIMED", SqlDbType.Float).Value = uniMe
        Cmd.Parameters.Add("@V_DETRACCION", SqlDbType.Float).Value = detraccion
        Cmd.Parameters.Add("@V_SUNAT", SqlDbType.VarChar).Value = tipoBien
        Cmd.Parameters.Add("@PESO", SqlDbType.Float).Value = peso
        Cmd.Parameters.Add("@VOLUMEN", SqlDbType.Float).Value = volumen
        Cmd.Parameters.Add("@ALTO", SqlDbType.Float).Value = alto
        Cmd.Parameters.Add("@ANCHO", SqlDbType.Float).Value = ancho
        Cmd.Parameters.Add("@LARGO", SqlDbType.Float).Value = largo
        Cmd.Parameters.Add("@SKU", SqlDbType.VarChar).Value = psSku
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_UPD_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function GuardarImagen_xBien(ByVal psConexion As String, ByVal psSerieNumerar As Double,
                                      ByVal img As Byte(), ByVal nomImg As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_GuardarImagen_xSerie", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_Numerar", SqlDbType.Float).Value = psSerieNumerar
        Cmd.Parameters.Add("@Imagen_Nom", SqlDbType.VarChar).Value = nomImg
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@Imagen", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_GuardarImagen_xSerie")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function GuardarImagen(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal img As Byte(), ByVal nomImg As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_UPD_ARTICULOS_IMAGEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@NOM_IMAGEN", nomImg)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_UPD_ARTICULOS_IMAGEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function EliminarArticulo(ByVal psConexion As String, ByVal codigoArt As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_DEL_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@V_CODART", codigoArt)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_DEL_ARTICULOS")
        Da.Fill(Dt)
        Return Dt

    End Function


    Public Function Buscar_Marca(ByVal psConexion As String, ByVal codigo As Double, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarMarca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@COD_MAR", SqlDbType.Float).Value = codigo
        Cmd.Parameters.Add("@DESCRIP", SqlDbType.VarChar).Value = descripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarMarca")
        Da.Fill(Dt)
        Return Dt
    End Function




    Public Function Buscar_Modelo(ByVal psConexion As String, ByVal codigoMo As Double, ByVal descripcion As String, ByVal codMar As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarModelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@COD_MOD", SqlDbType.Float).Value = codigoMo
        Cmd.Parameters.Add("@DESCRIPC", SqlDbType.VarChar).Value = descripcion
        Cmd.Parameters.Add("@COD_MARC", SqlDbType.Float).Value = codMar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarModelo")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Buscar_Modelo_Detalle(ByVal psConexion As String, ByVal codDetaMo As Double, ByVal descripcion As String, ByVal cod_mod As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscModeloDetalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@cod_detalle", SqlDbType.Float).Value = codDetaMo
        Cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion
        Cmd.Parameters.Add("@cod_modelo", SqlDbType.Float).Value = cod_mod
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscModeloDetalle")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function RegistrarArticuloModeloDetalle(ByVal psConexion As String, ByVal Codigo As String,
                                       ByVal codMod As String, ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_InsertArticuloModeloDetalle]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ARMODE_COD", Codigo)
        Cmd.Parameters.AddWithValue("@ARMOD_CODIGO", codMod)
        Cmd.Parameters.AddWithValue("@ARMOD_DESC", Descripcion)


        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_InsertArticuloModeloDetalle]")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function EliminarArticuloModeloDetalle(ByVal psConexion As String, ByVal codModeloDet As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_EliminarArticuloModeloDetalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ARMODE_COD", codModeloDet)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_EliminarArticuloModeloDetalle")
        Da.Fill(Dt)
        Return Dt

    End Function


    Public Function ActualizarArticuloModeloDetalle(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal codMod As String, ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_InsertArticuloModeloDetalle]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ARMODE_COD", Codigo)
        Cmd.Parameters.AddWithValue("@ARMOD_CODIGO", codMod)
        Cmd.Parameters.AddWithValue("@ARMOD_DESC", Descripcion)


        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_InsertArticuloModeloDetalle]")
        Da.Fill(Dt)
        Return Dt
    End Function







End Class