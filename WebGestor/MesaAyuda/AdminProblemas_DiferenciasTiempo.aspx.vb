Imports System.Data.SqlClient

Partial Class AdminProblemas_DiferenciasTiempo
    Inherits System.Web.UI.Page
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '
    End Sub
    Private Sub BTNVER_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNVER.Click
        'Introducir aquí el código de usuario para inicializar la página
        'CONEXIÓN
        Dim conexion As New SqlConnection(Session("Ruta_Emp"))
        conexion.Open()
        'cargamos los datos de los productos
        'Dim comando As New SqlCommand("select * from TBADMIN_PROBLEMAS", conexion)
        Dim comando As New SqlCommand("select * from V_REPORTE_PROB", conexion)
        Dim datos As SqlDataReader = comando.ExecuteReader
        'enlazamos los datos del sqlreader
        Grdestadoprob.DataSource = datos
        Grdestadoprob.DataBind()
        'cerramos  el sqldatareader y liberamos el objeto
        datos.Close()
        comando.Dispose()
        Dim codigo As Integer = 0
        'si existe el parametros ID...
        If Not Request.Params("id") Is Nothing Then
            '... entonces tomamos su ID
            codigo = CInt(Request.Params("id"))
            'cargamos los productos ofrecidos
            Dim comandodetalle As New SqlCommand( _
            "SELECT * FROM V_PROBLEMA_DET WHERE codigo=" & _
            codigo, conexion)
            Dim datosdetalle As SqlDataReader = comandodetalle.ExecuteReader
            'enlazamos sel al nuevo sqldatareasder al datagrid
            grddetalle.DataSource = datosdetalle
            grddetalle.DataBind()
            'cerramos el nuevo datareader y leberamos el objeto sqlcommand
            datosdetalle.Close()
            comandodetalle.Dispose()
        End If
        conexion.Close()
    End Sub
End Class
