Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class ClsMenuWeb_Consultas
    Public Function Lista_Parrafo(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_PARRAFO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.VarChar).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_PARRAFO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Items(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_ITEMS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_ITEMS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_MenuItems(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_SOLOITEMS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_SOLOITEMS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Categoris_xItems(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                           ByVal pdCodItem As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_CATEGORIA_XITEM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = pdCodItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_CATEGORIA_XITEM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ElementosMenuItems(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                             ByVal pdCodItem As Double, ByVal pdCodCategoria As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_ELEMENTOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = pdCodItem
        Cmd.Parameters.Add("@CodCategoria", SqlDbType.Float).Value = pdCodCategoria
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_ELEMENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Auspiciador(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_USPICIADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.VarChar).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_USPICIADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Campos_ItemElemento(ByVal psCampo As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_CAMPOS_ITEMELEMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Campo", SqlDbType.VarChar).Value = psCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_CAMPOS_ITEMELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Campos_xItems(ByVal pdCodItem As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_CAMPOS_XITEM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = pdCodItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_CAMPOS_XITEM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Parrafo(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                ByVal psTitulo As String, ByVal psDescripcion As String,
                                ByVal pdCodigo As Double, ByVal psTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_PARRAFO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.VarChar).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Titulo", SqlDbType.VarChar).Value = psTitulo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pdCodigo
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = psTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_PARRAFO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Campos_XItemElemento(ByVal psCodigo As Double, ByVal psCampo As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_CAMPOS_ITEMELEMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = psCodigo
        Cmd.Parameters.Add("@Campo", SqlDbType.VarChar).Value = psCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_CAMPOS_ITEMELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ItemCategoria(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_CATEGORIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ItemAUtilizar(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_ITEMAUTILIZAR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_ITEMAUTILIZAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ItemVerificar(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String,
                                        ByVal pdCodItem As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_VERIFICAR_ITEM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = pdCodItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_VERIFICAR_ITEM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ItemGenerales() As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_LIS_ITEMS_GENERALES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_LIS_ITEMS_GENERALES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_Categoria(ByVal psCategoria As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_EXISTE_CATEGORIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Categoria", SqlDbType.Float).Value = psCategoria
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_EXISTE_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_UltimoCodigoMenu() As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_NEWCODIGO_ITEMS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_NEWCODIGO_ITEMS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Busca_ItemxNombre(ByVal psNombre As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_BUSCA_ITEMSXNOMBRE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_BUSCA_ITEMSXNOMBRE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Item(ByVal psCodigo As Double, ByVal psNombre As String,
                             ByVal psPagina As String, ByVal psOrden As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_ITEMS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = psCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@Pagina", SqlDbType.VarChar).Value = psPagina
        Cmd.Parameters.Add("@Orden", SqlDbType.Float).Value = psOrden
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_ITEMS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Item(ByVal psCodigo As Double, ByVal psNombre As String,
                             ByVal psPagina As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_UPD_ITEMS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = psCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@Pagina", SqlDbType.VarChar).Value = psPagina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_BUSCA_ITEMSXNOMBRE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_ItemCampo(ByVal psCodigo As Double, ByVal psOrden As Double,
                                  ByVal psNombre As String, ByVal psEtiqueta As String,
                                  ByVal psObligatorio As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_ITEMSCAMPO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = psCodigo
        Cmd.Parameters.Add("@Orden", SqlDbType.Float).Value = psOrden
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@Etiqueta", SqlDbType.VarChar).Value = psEtiqueta
        Cmd.Parameters.Add("@Obligatorio", SqlDbType.VarChar).Value = psObligatorio
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_ITEMSCAMPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_ItemCampo(ByVal psCodigo As Double, ByVal psEtiqueta As String,
                                  ByVal psObligatorio As String, ByVal psNombre As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_UPD_ITEMSCAMPO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = psCodigo
        Cmd.Parameters.Add("@Etiqueta", SqlDbType.VarChar).Value = psEtiqueta
        Cmd.Parameters.Add("@Obligatorio", SqlDbType.VarChar).Value = psObligatorio
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_UPD_ITEMSCAMPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Categoria(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                  ByVal psCodItem As Double, ByVal psNombre As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_CATEGORIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = psCodItem
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Auspiciador(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                    ByVal psNombre As String, ByVal psDescripcion As String,
                                    ByVal psLink As String, ByVal psImagen As String,
                                    ByVal psExtension As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_AUSPICIADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@Link", SqlDbType.VarChar).Value = psLink
        Cmd.Parameters.Add("@Imagen", SqlDbType.VarChar).Value = psImagen
        Cmd.Parameters.Add("@Extension", SqlDbType.VarChar).Value = psExtension
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_AUSPICIADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Auspiciador(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                    ByVal psNombre As String, ByVal psDescripcion As String,
                                    ByVal psLink As String, ByVal pdCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_AUSPICIADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@Link", SqlDbType.VarChar).Value = psLink
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pdCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_AUSPICIADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Auspiciador(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                    ByVal pdCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_DEL_AUSPICIADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.VarChar).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pdCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_DEL_AUSPICIADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Elemento_Menu(ByVal pdGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                      ByVal pdCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_DEL_ELEMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.VarChar).Value = pdGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pdCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_DEL_ELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_ImagenAuspiciador(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                          ByVal pdCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_AUSPICIADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pdCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_AUSPICIADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Categoria(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                  ByVal psCodCategoria As Double, ByVal psNombre As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_UPD_CATEGORIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCategoria", SqlDbType.Float).Value = psCodCategoria
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_UPD_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Categoria(ByVal psCategoria As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_DEL_CATEGORIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Categoria", SqlDbType.Float).Value = psCategoria
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_DEL_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_ItemsAUtilizar(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                       ByVal psCodItem As Double, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_ITEMAUTILIZAR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = psCodItem
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_ITEMAUTILIZAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_ItemsAUtilizar(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                       ByVal psCodItem As Double, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_UPD_ITEMAUTILIZAR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = psCodItem
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_UPD_ITEMAUTILIZAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_ItemsAUtilizar(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                       ByVal psCodItem As Double, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_DEL_ITEMAUTILIZAR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = psCodItem
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_DEL_ITEMAUTILIZAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Elementos(ByVal psCodGrupoEmpresa As Double, ByVal psCodEmpresa As String,
                                  ByVal psCodItem As Double, ByVal psCodCategoria As Double,
                                  ByVal psNombre As String, ByVal psNombreHtml As String,
                                  ByVal psDescripcion As String, ByVal psDetalle As String,
                                  ByVal psPagina1 As String, ByVal psPagina2 As String,
                                  ByVal psImagenNombre As String, ByVal psFecha1 As String,
                                  ByVal psFecha2 As String, ByVal psFecha3 As String,
                                  ByVal psCompletar1 As String, ByVal psCompletar2 As String,
                                  ByVal psCompletar3 As String, ByVal psCompletar4 As String,
                                  ByVal psCompletar5 As String, ByVal psArchivo As String,
                                  ByVal psUsuario As String, ByVal psComentario As String,
                                  ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_MENUWEB_INS_ELEMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodItem", SqlDbType.Float).Value = psCodItem
        Cmd.Parameters.Add("@CodCategoria", SqlDbType.Float).Value = psCodCategoria
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@NombreHtml", SqlDbType.VarChar).Value = psNombreHtml
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = psDetalle
        Cmd.Parameters.Add("@Pagina1", SqlDbType.VarChar).Value = psPagina1
        Cmd.Parameters.Add("@Pagina2", SqlDbType.VarChar).Value = psPagina2
        Cmd.Parameters.Add("@ImagenNombre", SqlDbType.VarChar).Value = psImagenNombre
        Cmd.Parameters.Add("@Fecha1", SqlDbType.VarChar).Value = psFecha1
        Cmd.Parameters.Add("@Fecha2", SqlDbType.VarChar).Value = psFecha2
        Cmd.Parameters.Add("@Fecha3", SqlDbType.VarChar).Value = psFecha3
        Cmd.Parameters.Add("@Completar1", SqlDbType.VarChar).Value = psCompletar1
        Cmd.Parameters.Add("@Completar2", SqlDbType.VarChar).Value = psCompletar2
        Cmd.Parameters.Add("@Completar3", SqlDbType.VarChar).Value = psCompletar3
        Cmd.Parameters.Add("@Completar4", SqlDbType.VarChar).Value = psCompletar4
        Cmd.Parameters.Add("@Completar5", SqlDbType.VarChar).Value = psCompletar5
        Cmd.Parameters.Add("@ArchivoNombre", SqlDbType.VarChar).Value = psArchivo
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUsuario
        Cmd.Parameters.Add("@Comentario", SqlDbType.VarChar).Value = psComentario
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_MENUWEB_INS_ELEMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class