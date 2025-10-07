Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Data
Imports WebGestor
Partial Class MProyRight
    Inherits System.Web.UI.UserControl
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
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            MyDataGrid.DataSource = Carga_Detalle_Derecha()
            MyDataGrid.DataBind()
            If MyDataGrid.Items.Count < 4 Then MyDataGrid.AllowPaging = False Else MyDataGrid.AllowPaging = True
            MyDataGrid.DataBind()
        End If
    End Sub
    Private Function Carga_Detalle_Derecha() As ICollection
        Carga_Detalle_Derecha = Nothing
        Dim i As Long = 0
        Dim n As Long = 0
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql As New SqlCommand
        Dim obj As New Listados
        Dim dt As New Data.DataTable
        Dim nPag As String = ""
        Dim nTPag As String = ""
        Dim Existe As Integer = 0
        Dim dt2 As New DataTable
        Dim MyDataRow As DataRow
        dt.Columns.Add(New DataColumn("AUSPI_CODIGO"))
        dt.Columns.Add(New DataColumn("AUSPI_NOMBRE"))
        dt.Columns.Add(New DataColumn("AUSPI_DESCRIP"))
        dt.Columns.Add(New DataColumn("AUSPI_LINK"))
        dt.Columns.Add(New DataColumn("IMAGEN"))
        Dim sUrl As String = MyBase.Request.FilePath
        Try
            Cn2.Open()
            cmdSql.Connection = Cn2
            'ConfigurationSettings.AppSettings("CodGE") -> en servipruen, Session("CodGrupoEmpresa")-> en soporte online
            cmdSql.CommandText = "SELECT *,('../MenuWeb/ImagesAusp_" & Session("SiglaGrupoEmpresa") & "/'+AUSPI_IMAGEN_NOMBRE) AS IMAGEN1," _
                               & "('../MenuWeb/ImagesAusp_" & Session("SiglaGrupoEmpresa") & "/'+AUSPI_IMAGEN_NOMBRE) AS IMAGEN2 FROM TBWAUSPICIADORES where AUSPI_SYS_EST='0' AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND " _
                               & " EMPRESA_CODIGO='" & Session("CodEmpresa") & "' ORDER BY AUSPI_CODIGO"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    MyDataRow = dt.NewRow()
                    MyDataRow(0) = Nu(Rs!AUSPI_CODIGO)
                    MyDataRow(1) = Nu(Rs!AUSPI_NOMBRE)
                    MyDataRow(2) = Nu(Rs!AUSPI_DESCRIP)
                    MyDataRow(3) = Nu(Rs!AUSPI_LINK)
                    dt2 = obj.Listar_Pagina(Session("CodEmpresa"), Session("CodGrupoEmpresa"), 0, "S")
                    If dt2.Rows.Count > 0 Then
                        For Each drMenuItem As Data.DataRow In dt2.Rows
                            nTPag = HttpContext.Current.Request.ApplicationPath & "/Menu/" & Nu(drMenuItem("ITEM_PAGINA"))
                            If sUrl = nTPag Then Existe = Existe + 1
                        Next
                    End If
                    dt2 = Nothing 'HttpContext.Current.Request.ApplicationPath & "/Menu/_Default.aspx" Or sUrl = HttpContext.Current.Request.ApplicationPath & "/Menu/Detalle.aspx"
                    If sUrl = HttpContext.Current.Request.ApplicationPath & "/Menu/Detalle.aspx" Then Existe = Existe + 1
                    If sUrl = HttpContext.Current.Request.ApplicationPath & "/Menu/IngresarElemento.aspx" Then Existe = Existe + 1
                    n = 0
                    If Existe > 0 Then
                        For i = 1 To Len(sUrl)
                            If Mid(sUrl, i, 1) = "/" Then n = n + 1
                        Next
                        If n > 2 Then
                            MyDataRow(4) = Nu(Rs!IMAGEN2)
                        ElseIf n = 2 Then
                            MyDataRow(4) = Mid(Nu(Rs!IMAGEN2), 4)
                        End If
                    Else
                        For i = 1 To Len(sUrl)
                            If Mid(sUrl, i, 1) = "/" Then n = n + 1
                        Next
                        If n > 2 Then
                            MyDataRow(4) = Nu(Rs!IMAGEN1)
                        ElseIf n = 2 Then
                            MyDataRow(4) = Mid(Nu(Rs!IMAGEN1), 4)
                        End If
                    End If
                    dt.Rows.Add(MyDataRow)
                End While
                Carga_Detalle_Derecha = New DataView(dt)
            End If
            Rs.Close()
        Catch Ex As SqlException
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn2.Close()
        End Try
    End Function
    Private Sub MyDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles MyDataGrid.PageIndexChanged
        MyDataGrid.CurrentPageIndex = e.NewPageIndex
        MyDataGrid.DataSource = Carga_Detalle_Derecha()
        MyDataGrid.DataBind()
    End Sub
    Protected Sub MyDataGrid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyDataGrid.SelectedIndexChanged
        Dim ps As String = ""
        ps = MyDataGrid.SelectedItem.Cells(3).Text
        Label1.Text = ps
    End Sub
End Class


