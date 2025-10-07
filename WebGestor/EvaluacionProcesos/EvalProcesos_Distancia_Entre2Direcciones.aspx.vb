Imports WebGestor
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Data
Partial Class EvaluacionProcesos_EvalProcesos_Distancia_Entre2Direcciones
    Inherits System.Web.UI.Page

    Dim ObjProceso As New ClsEval_Proceso
    Dim FnProceso As New clsEval_Proceso_Funciones
    Dim objGrupoEmp As New ModuloGeneral
    Dim objSeg As New ModuloSeguridad
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblError.Text = ""
            DdlTipoDistancia.SelectedValue = "1"
            Call FnProceso.CargarDM(DdlPersonal, 10, Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            DdlPersonal.Items.Add("< Seleccionar >") : DdlPersonal.SelectedValue = "< Seleccionar >"
            Call Lista_Oficinatodo(DdlOficina)
            DdlOficina.Items.Add("< Seleccionar >") : DdlOficina.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC548", DdlEstado)
            Call FnProceso.CargarCargoPersonal(DdlCargo, Session("CodEmpresa"), Session("CodGrupoEmpresa"))
        End If
    End Sub
    Private Sub Lista_Oficinatodo(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        ddl.DataSource = objSeg.Listar_Oficina(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
        ddl.DataTextField = "OFICINA_NOMBRE"
        ddl.DataValueField = "OFICINA_CODIGO"
        ddl.DataBind()
        ddl.Items.Add("< Total Sistema >")
        ddl.SelectedValue = "< Total Sistema >"
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim dt As New DataTable
        Dim pdLatitud As Double = 0 ' -12.0772581
        Dim pdLongitud As Double = 0 ' -77.0500754
        Dim psEstado As String = ""
        Dim pdCargo As Double = 0
        LblError.Text = ""
        Dim psDm As String = ""
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
        If TxtLatitud.Text <> "0" And TxtLatitud.Text <> "" Then pdLatitud = TxtLatitud.Text
        If TxtLongitud.Text <> "0" And TxtLongitud.Text <> "" Then pdLongitud = TxtLongitud.Text
        If DdlCargo.SelectedValue <> "< Seleccionar >" Then pdCargo = DdlCargo.SelectedValue
        If DdlEstado.SelectedValue <> "< Seleccionar >" Then psEstado = DdlEstado.SelectedValue
        Try
            If DdlTipoDistancia.SelectedValue = "1" Then
                dt = ObjProceso.Lista_Oficina_DistanciaxPersonal(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), pdLatitud, pdLongitud, "K")
            Else
                'pdLatitud = -12.101114
                'pdLongitud = -76.971692
                If pdCargo = 0 And psEstado = "" Then
                    dt = ObjProceso.Lista_Personal_DistanciaxOficina(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), pdLatitud, pdLongitud, "K")
                Else
                    dt = ObjProceso.Lista_Personal_DistanciaxOficina_Filtro(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), pdLatitud, pdLongitud, "K", pdCargo, psEstado)
                End If
            End If
            Flex.DataSource = dt
            Flex.DataBind()
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub DdlPersonal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlPersonal.SelectedIndexChanged
        Dim dt As New DataTable
        Dim pdLatitud As Double = 0
        Dim pdLongitud As Double = 0
        LblError.Text = ""
        Dim psDm As String = ""
        dt = Nothing
        If DdlPersonal.SelectedValue <> "< Seleccionar >" Then
            dt = objGrupoEmp.Obtener_DatosPersonal(Session("CodGrupoEmpresa"), Session("CodEmpresa"), DdlPersonal.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each drow As Data.DataRow In dt.Rows
                    TxtDireccion.Text = Nu(drow("PERSON_DOM_DIRECCION"))
                    TxtLatitud.Text = Nu(drow("PERSON_LATITUD"))
                    TxtLongitud.Text = Nu(drow("PERSON_LONGITUD"))
                Next
            End If
        End If
    End Sub
    Protected Sub DdlTipoDistancia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoDistancia.SelectedIndexChanged
        If DdlTipoDistancia.SelectedValue = "1" Then
            DdlOficina.Enabled = False
            DdlPersonal.Enabled = True
            DdlCargo.Enabled = False
            DdlEstado.Enabled = False
            DdlOficina.SelectedValue = "< Seleccionar >"
            DdlPersonal.SelectedValue = "< Seleccionar >"
            DdlCargo.SelectedValue = "< Seleccionar >"
            DdlEstado.SelectedValue = "< Seleccionar >"
            TxtDireccion.Text = ""
            TxtLatitud.Text = ""
            TxtLongitud.Text = ""
        Else
            DdlOficina.Enabled = True
            DdlCargo.Enabled = True
            DdlEstado.Enabled = True
            DdlPersonal.Enabled = False
            DdlPersonal.SelectedValue = "< Seleccionar >"
            DdlOficina.SelectedValue = "< Seleccionar >"
            DdlCargo.SelectedValue = "< Seleccionar >"
            DdlEstado.SelectedValue = "< Seleccionar >"
            TxtDireccion.Text = ""
            TxtLatitud.Text = ""
            TxtLongitud.Text = ""
        End If
    End Sub

    Private Sub DdlOficina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlOficina.SelectedIndexChanged
        Dim dt As New DataTable
        Dim pdLatitud As Double = 0
        Dim pdLongitud As Double = 0
        LblError.Text = ""
        Dim psDm As String = ""
        dt = Nothing
        If DdlOficina.SelectedValue <> "< Seleccionar >" Then
            dt = objGrupoEmp.Obtener_DatosOficina(Session("CodGrupoEmpresa"), Session("CodEmpresa"), DdlOficina.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each drow As Data.DataRow In dt.Rows
                    TxtDireccion.Text = Nu(drow("OFICINA_DIRECCION"))
                    TxtLatitud.Text = Nu(drow("OFICINA_LATITUD"))
                    TxtLongitud.Text = Nu(drow("OFICINA_LONGITUD"))
                Next
            End If
        End If
    End Sub
End Class
