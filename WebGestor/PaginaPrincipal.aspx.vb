Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class PaginaPrincipal
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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Not Page.IsPostBack Then 'solo carga una vez
            cboEmpresa.Items.Clear()
            cboEmpresa.Visible = False
            lblEtiq1.Visible = False
            lblMensaje.Visible = False
            Dim Cn As New SqlConnection(Ruta_Ng)
            Dim Rs As SqlDataReader
            Dim bolError As String = ""
            If Session("UserFirmado") = "S" Then
                Try
                    Cn.Open()
                    Dim Sql As String = "SELECT GE.GE_NOMBRE + ' -» ' + GEE.GEE_NOMBRE AS NOMEMPRESA, UE.EMPRESA_CODIGO + '.' + (CASE WHEN LEN(CAST(UE.GRPOEMPRESA_CODIGO AS VARCHAR))= 1 THEN '0' + CAST(UE.GRPOEMPRESA_CODIGO AS VARCHAR) ELSE CAST(UE.GRPOEMPRESA_CODIGO AS VARCHAR) END)+ '.' + CAST(UE.GRPOEMPRESA_CODIGO AS VARCHAR) + GE.GE_PREFIJO AS CODIF " _
                                      & " FROM TBUSUARI_GRPOEMPS UE INNER JOIN BDGrupoEmpresas.dbo.TBGRUPOEMPRESAS GE ON UE.GRPOEMPRESA_CODIGO = GE.GE_CODIGO " _
                                      & " INNER JOIN BDGrupoEmpresas.dbo.TBGRUPOEMP_EMPRESAS GEE ON UE.GRPOEMPRESA_CODIGO = GEE.GE_CODIGO AND UE.EMPRESA_CODIGO = GEE.GEE_CODIGO" _
                                      & " WHERE (UE.USUARI_CODIGO = '" & User.Identity.Name & "') AND (GE.GE_SYS_EST = '0') AND (GEE.GEE_SYS_EST = '0')  ORDER BY GE.GE_NOMBRE, GEE.GEE_NOMBRE  "
                    Dim cmdSql As New SqlCommand(Sql, Cn)
                    Rs = cmdSql.ExecuteReader
                    If Rs.HasRows Then
                        bolError = "2"
                        Rs.Close()
                        cboEmpresa.DataSource = cmdSql.ExecuteReader
                        cboEmpresa.DataTextField = "NOMEMPRESA"
                        cboEmpresa.DataValueField = "CODIF"
                        cboEmpresa.DataBind()
                    Else
                        bolError = "1"
                    End If
                Catch Ex As SqlException
                    'lblMensaje.Visible = True
                    'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
                Catch Ex As Exception
                    'lblMensaje.Visible = True
                    'lblMensaje.Text = "Ha ocurrido un error la Aplicacion :<br>" & ex.Message
                Finally
                    Cn.Close()
                End Try
            Else
                bolError = "3"
            End If
            If bolError = "1" Then
                lblMensaje.Visible = True
                lblMensaje.Text = "Parámetros inválidos."
            ElseIf bolError = "2" Then
                If cboEmpresa.Items.Count > 1 Then cboEmpresa.Visible = True : lblEtiq1.Visible = True
                If Session("CodEmpresa") = "" Then
                    cboEmpresa.SelectedIndex = 0
                    Session("CodGrupoEmpresa") = Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Value, 6, 2)
                    Session("SiglaGrupoEmpresa") = Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Value, 9)
                    Session("CodEmpresa") = Left(cboEmpresa.Items(cboEmpresa.SelectedIndex).Value, 4)
                    Dim i As Integer = InStr(1, cboEmpresa.Items(cboEmpresa.SelectedIndex).Text, "»")
                    Session("NombreGrupoEmpresa") = Trim(Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Text, 1, i - 2))
                    Session("NombreEmpresa") = Trim(Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Text, i + 1))
                    Session("Ruta_Emp") = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;POOLING=FALSE;initial catalog=BDGEmpresa" & Session("SiglaGrupoEmpresa")
                    BDEmpresa = "BDGEmpresa" & Session("SiglaGrupoEmpresa")
                Else
                    'cboEmpresa.Visible = True
                    'lblEtiq1.Visible = True
                    Dim i As Integer
                    For i = 0 To cboEmpresa.Items.Count - 1
                        If cboEmpresa.Items(i).Value = Session("CodEmpresa") & "." & Session("CodGrupoEmpresa") & "." & Session("SiglaGrupoEmpresa") Then cboEmpresa.SelectedIndex = i : Exit For
                    Next
                End If
                Call Carga_Detalle_Centro()
                Me.Page.Session.Timeout = 1080
            Else
                cboEmpresa.SelectedIndex = 0
                Session("CodGrupoEmpresa") = 3
                Session("SiglaGrupoEmpresa") = "3TG"
                Session("CodEmpresa") = "0006"
                BDEmpresa = "BDEmpresa" & Session("SiglaGrupoEmpresa")
                Call Carga_Detalle_Centro()
                Me.Page.Session.Timeout = 1080
            End If
        End If
    End Sub
    Private Sub Carga_Detalle_Centro()
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql As New SqlCommand

        Dim dt As New DataTable
        Dim MyDataRow As DataRow
        dt.Columns.Add(New DataColumn("PARRAFO_TITULO"))
        dt.Columns.Add(New DataColumn("PARRAFO_DESCRIP"))
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT * FROM TBWINICIO_PARRAFOS where PARRAFO_SYS_EST='0' AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' ORDER BY PARRAFO_CODIGO"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    MyDataRow = dt.NewRow()
                    MyDataRow(0) = Nu(Rs!PARRAFO_TITULO)
                    MyDataRow(1) = Nu(Rs!PARRAFO_DESCRIP)
                    dt.Rows.Add(MyDataRow)
                End While
                MyDataList.DataSource = New DataView(dt)
                MyDataList.DataBind()
            End If
            Rs.Close()
        Catch Ex As SqlException
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            'lblMensaje.Visible = True
            'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub cboEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmpresa.SelectedIndexChanged
        If cboEmpresa.SelectedIndex = -1 Then Exit Sub
        Session("CodGrupoEmpresa") = Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Value, 6, 2)
        Session("SiglaGrupoEmpresa") = Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Value, 9)
        Session("CodEmpresa") = Left(cboEmpresa.Items(cboEmpresa.SelectedIndex).Value, 4)
        Dim i As Integer = InStr(1, cboEmpresa.Items(cboEmpresa.SelectedIndex).Text, "»")
        Session("NombreGrupoEmpresa") = Trim(Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Text, 1, i - 2))
        Session("NombreEmpresa") = Trim(Mid(cboEmpresa.Items(cboEmpresa.SelectedIndex).Text, i + 1))
        Session("Ruta_Emp") = "workstation id=;packet size=4096;user id=sa;data source=" & NomServer & ";persist security info=False;initial catalog=BDGEmpresa" & Session("SiglaGrupoEmpresa")
        Response.Redirect("PaginaPrincipal.aspx")
    End Sub
End Class
