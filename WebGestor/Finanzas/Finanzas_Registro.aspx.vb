Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Finanzas_Finanzas_Registro
    Inherits System.Web.UI.Page
    Dim FunCont As New clsCont_Funciones
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Call LlenaComboItem("TBOPC393", DdlModulo)
            Call LlenaComboItem("TBOPC392", DdlTipoMov)
            Call LlenaComboItem("TBOPC015", DdlMoneda)
            DdlMoneda.SelectedValue = "2"
            Call LlenaAno(DdlAño)
            DdlAño.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            TxtFechaReg.Text = FormatoFecha(FechaActual())
            TxtCompra.Text = "Compra : " & FunCont.Hallar_Valor_Compra(Session("Ruta_Emp"), FechaActual)
            TxtVenta.Text = " Venta : " & FunCont.Hallar_Valor_Venta(Session("Ruta_Emp"), FechaActual)
            'Call LlenaComboItem("TBOPC413", cboDetraccion)
            'Call LlenaComboItem("TBOPC413", cboAutoDetrac)
            Call FunCont.Llena_TipoDocumento(Session("Ruta_Emp"), Session("CodEmpresa"), DdlAño.SelectedValue, DdlDoc)
            Call FunCont.Llena_TipoDocumento(Session("Ruta_Emp"), Session("CodEmpresa"), DdlAño.SelectedValue, DdlDocRef)
            Call FunCont.Finanza_ListaCaja(Session("Ruta_Emp"), DdlCaja)
            Call Carga_Cuenta_Banco
            Call Carga_CentroCosto
            Call Cargar_TipoBien
        End If

    End Sub
    Private Sub Lista_caja()

    End Sub
    Private Sub Carga_Cuenta_Banco()

    End Sub
    Private Sub Carga_CentroCosto()

    End Sub
    Private Sub Cargar_TipoBien()

    End Sub
End Class
