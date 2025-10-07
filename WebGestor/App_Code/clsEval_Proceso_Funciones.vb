
Imports System.Data
Public Class ClsEval_Proceso_Funciones
    Dim objGrupoEmp As New ModuloGeneral
    Dim ObjProceso As New ClsEval_Proceso
    Public Sub CargarDM(ByVal ddl As DropDownList, ByVal psCodCargo As Double, ByVal CodEmpresa As String, ByVal psCodGrupoEmp As Double)
        ddl.Items.Clear() 'Listar_Usuarios
        ddl.DataSource = objGrupoEmp.Lista_Personal_xCargo(psCodGrupoEmp, CodEmpresa, psCodCargo)
        ddl.DataTextField = "NOMBRE_PERSONAL"
        ddl.DataValueField = "PERSON_CODIGO"
        ddl.DataBind()
    End Sub

    Public Sub Llenar_Proceso(ByVal ddl As DropDownList, ByVal CodEmpresa As String, ByVal psConexion As String)
        ddl.Items.Clear()
        ddl.DataSource = ObjProceso.Lista_Proceso(CodEmpresa, psConexion)
        ddl.DataTextField = "NombreProceso"
        ddl.DataValueField = "CodProceso"
        ddl.DataBind()
        ddl.Items.Add("< Seleccionar >")
        ddl.SelectedValue = "< Seleccionar >"
    End Sub
    Public Sub CargarCargoPersonal(ByVal ddl As DropDownList, ByVal CodEmpresa As String, ByVal psCodGrupoEmp As Double)
        ddl.Items.Clear() 'Listar_Usuarios
        ddl.DataSource = objGrupoEmp.Lista_Cargo(psCodGrupoEmp, CodEmpresa)
        ddl.DataTextField = "CARGO_NOMBRE"
        ddl.DataValueField = "CARGO_CODIGO"
        ddl.DataBind()
        ddl.Items.Add("< Seleccionar >")
        ddl.SelectedValue = "< Seleccionar >"
    End Sub
    Public Sub Llenar_Oficina(ByVal ddlOf As DropDownList, ByVal CodEmpresa As String, ByVal psCodGrupoEmp As Double, ByVal psConexion As String, ByVal psCodDM As String)
        ddlOf.Items.Clear()
        ddlOf.DataSource = ObjProceso.Evaluacion_ListaRelacion_OficinaXDM(CodEmpresa, psConexion, psCodGrupoEmp, psCodDM)
        ddlOf.DataTextField = "c3"
        ddlOf.DataValueField = "c4"
        ddlOf.DataBind()
    End Sub

    Public Sub Llenar_Proceso_Check(ByVal chk As CheckBoxList, ByVal CodEmpresa As String, ByVal psConexion As String)
        Dim dt As DataTable
        chk.Items.Clear()
        dt = ObjProceso.Lista_Proceso(CodEmpresa, psConexion)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim item As New ListItem() With {
                .Text = dr("NombreProceso").ToString(),
                .Value = dr("CodProceso").ToString()}
                chk.Items.Add(item)
            Next
        End If
    End Sub

    'Lista_TipoEvaluacion
    Public Sub Llenar_TipoEval(ByVal ddlOf As DropDownList, ByVal CodEmpresa As String, ByVal psConexion As String)
        ddlOf.Items.Clear()
        ddlOf.DataSource = ObjProceso.Lista_TipoEvaluacion(CodEmpresa, psConexion)
        ddlOf.DataTextField = "TIPOEVAL_DESCRIPCION"
        ddlOf.DataValueField = "TIPOEVAL_CODIGO"
        ddlOf.DataBind()
        ddlOf.Items.Add("< Seleccionar >")
        ddlOf.SelectedValue = "< Seleccionar >"
    End Sub

End Class
