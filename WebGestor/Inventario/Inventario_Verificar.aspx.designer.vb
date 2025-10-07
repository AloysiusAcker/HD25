'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Inventario_Verificar
    
    '''<summary>
    '''Control UpdatePanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control LblInventario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblInventario As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control DdlInventario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DdlInventario As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control LblUbicacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblUbicacion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RBAlmacen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RBAlmacen As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control RBCentroC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RBCentroC As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control RBUbicaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RBUbicaciones As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control LblCodigo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblCodigo As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtCodigo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodigo As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control BtnBusca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBusca As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control TxtDescripcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDescripcion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control TxtCodigoAyudaUbicacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodigoAyudaUbicacion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control TxtCodigoAyuda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodigoAyuda As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control BtnListar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnListar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnIniciarVerificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnIniciarVerificacion As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control LblNroPlaca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblNroPlaca As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtNroPlaca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtNroPlaca As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control BtnCancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCancelar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control LblNroSerie.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblNroSerie As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtNroSerie.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtNroSerie As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control TxtMensajeVerificar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtMensajeVerificar As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control UpdatePanel3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel3 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control GvListaVerificarInventario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvListaVerificarInventario As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control GvListaVerificarInventarioOtros.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvListaVerificarInventarioOtros As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control GvListaVerificarInventarioNuevos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvListaVerificarInventarioNuevos As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control UpdatePanel2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel2 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control TituloPopup.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TituloPopup As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control UpdatePanel9.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel9 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control BuscarDescripcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BuscarDescripcion As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BuscarCodigo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BuscarCodigo As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnCerrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCerrar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control upSetSession.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents upSetSession As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control GvBusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvBusqueda As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control GvBusquedaU.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvBusquedaU As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control GvBusquedaM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvBusquedaM As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control UpdatePanel7.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel7 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control TituloPregunta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TituloPregunta As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control BtnSi.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnSi As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnNo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnNo As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control TituloBuscarArticulos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TituloBuscarArticulos As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control UpdatePanel8.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel8 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control TxtCodArticuloBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodArticuloBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control TxtClasificacionBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtClasificacionBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscaClasificacionBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaClasificacionBA As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control LblCodClasificacionBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblCodClasificacionBA As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtDescripcionBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDescripcionBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control DdlTipoBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DdlTipoBA As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control TxtNumParteBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtNumParteBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control TxtCodEspecificoBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodEspecificoBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control TxtMarcaBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtMarcaBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscaMarcaBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaMarcaBA As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control LblCodMarcaBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblCodMarcaBA As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtModeloBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtModeloBA As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscaModeloBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaModeloBA As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control LblCodModeloBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblCodModeloBA As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control BtnBuscarBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscarBA As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnCerrarBA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCerrarBA As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control UpdatePanel5.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel5 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control GvBuscarArticulos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvBuscarArticulos As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control TituloClasificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TituloClasificacion As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control BtnBuscaClasificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaClasificacion As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnCerrarClasificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCerrarClasificacion As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control UpdatePanel11.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel11 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control TrvClasificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TrvClasificacion As Global.System.Web.UI.WebControls.TreeView
    
    '''<summary>
    '''Control UpdatePanel4.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel4 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control TituloArticulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TituloArticulo As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control UpdatePanel6.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel6 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control TxtPlacaNroM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtPlacaNroM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control TxtSerieNroM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtSerieNroM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control DdlEstadoM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DdlEstadoM As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control BtnAgregarArticulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnAgregarArticulo As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control TxtCodRelacionadoM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodRelacionadoM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnCerrarArticulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCerrarArticulo As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control DdlResponsableM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DdlResponsableM As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control TxtCodArticuloM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodArticuloM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscaArticuloM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaArticuloM As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control TxtDescArticuloM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDescArticuloM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control LblArticuloM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblArticuloM As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control RBAlmacenArea.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RBAlmacenArea As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control RBCentroCArea.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RBCentroCArea As Global.System.Web.UI.WebControls.RadioButton
    
    '''<summary>
    '''Control TxtCodAreaM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodAreaM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscaAreaM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaAreaM As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control TxtDescAreaM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDescAreaM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control LblCodAreaM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblCodAreaM As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtCodUbicacionM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtCodUbicacionM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control BtnBuscaUbicacionM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBuscaUbicacionM As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control TxtDescUbicacionM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtDescUbicacionM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control LblCodUbicacionM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblCodUbicacionM As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control TxtObservacionM.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtObservacionM As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control TxtBuscarArticulo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtBuscarArticulo As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control TxtBuscarSerie.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtBuscarSerie As Global.System.Web.UI.HtmlControls.HtmlInputText
    
    '''<summary>
    '''Control GvArticulo1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvArticulo1 As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control GvArticulo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GvArticulo2 As Global.System.Web.UI.WebControls.GridView
End Class
