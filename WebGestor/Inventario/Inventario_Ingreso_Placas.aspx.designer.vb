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


Partial Public Class Inventario_Ingreso_Placas
    
    '''<summary>
    '''Control UpdatePanel2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel2 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control Ficha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Ficha As Global.AjaxControlToolkit.TabContainer
    
    '''<summary>
    '''Control PnLista.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PnLista As Global.AjaxControlToolkit.TabPanel
    
    '''<summary>
    '''Control Label1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label1 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control DdlAlmacén.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DdlAlmacén As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control DropDownList1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DropDownList1 As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control BtnListar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnListar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control GridView_Lista_Recepción.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GridView_Lista_Recepción As Global.System.Web.UI.WebControls.GridView
    
    '''<summary>
    '''Control PnIngreso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PnIngreso As Global.AjaxControlToolkit.TabPanel
    
    '''<summary>
    '''Control Label2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Label2 As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control txtNumRecepcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNumRecepcion As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control UpdatePanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel
    
    '''<summary>
    '''Control BtnIngreso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnIngreso As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnBorrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnBorrar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnCerrar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCerrar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnGenerar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnGenerar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control BtnCancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnCancelar As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Control FramePlaca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents FramePlaca As Global.System.Web.UI.HtmlControls.HtmlGenericControl
    
    '''<summary>
    '''Control DdlTipoPlaca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DdlTipoPlaca As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Control ChkMarcarTodo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ChkMarcarTodo As Global.System.Web.UI.WebControls.CheckBox
    
    '''<summary>
    '''Control LblUltima.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblUltima As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control UltimaPlaca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UltimaPlaca As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control LblIniciar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblIniciar As Global.System.Web.UI.WebControls.Label
    
    '''<summary>
    '''Control IniciarPlaca.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents IniciarPlaca As Global.System.Web.UI.WebControls.TextBox
    
    '''<summary>
    '''Control GridView_Detalle_Recepción.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GridView_Detalle_Recepción As Global.System.Web.UI.WebControls.GridView
End Class
