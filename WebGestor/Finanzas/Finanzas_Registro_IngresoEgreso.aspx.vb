Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Finanzas_Finanzas_Registro_IngresoEgreso
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'BtnNuevo.Attributes.Add("OnClick", "window.open('Finanzas_Registro.aspx',null,'height=600,width=500');")

        If Not Page.IsPostBack Then
            Call LlenaComboItem("TBOPC393", DdlModulo)
            Call LlenaComboItem("TBOPC015", DdlMoneda)
            DdlMoneda.SelectedValue = "2"
            Call LlenaAno(DdlAño)
            DdlAño.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            TxtFecha.Text = FormatoFecha(FechaActual())
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        'Filtrar_Descripcion_Almacen
        Dim obj As New ClsFinanza
        Dim dt As New DataTable
        dt = Nothing
        Dim psMoneda As String = ""
        If DdlMoneda.SelectedValue <> "< Seleccionar >" Then
            psMoneda = DdlMoneda.SelectedValue
        End If
        Dim psModulo As String = ""
        If DdlModulo.SelectedValue <> "< Seleccionar >" Then
            psModulo = DdlModulo.SelectedValue
        End If
        Dim psAño As String = ""
        If DdlAño.SelectedValue <> "< Seleccionar >" Then
            psAño = DdlAño.SelectedValue
        End If

        Dim psPeriodoActual As String = ""
        psPeriodoActual = Left(FechaActual, 6)

        Dim psFechaIni As String = ""
        Dim psfechafin As String = ""
        If TxtFecha.Text <> "" Then
            psFechaIni = Right(TxtFecha.Text, 4) & Mid(TxtFecha.Text, 4, 2) & Left(TxtFecha.Text, 2)
        End If
        If TxtFecha.Text <> "" And TxtFechaFin.Text = "" Then
            psfechafin = Right(TxtFecha.Text, 4) & Mid(TxtFecha.Text, 4, 2) & Left(TxtFecha.Text, 2)
        End If
        If TxtFecha.Text <> "" And TxtFechaFin.Text <> "" Then
            psfechafin = Right(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 4, 2) & Left(TxtFechaFin.Text, 2)
        End If
        Dim pdPersona As Double = 0
        GvFinanza.DataSource = dt
        GvFinanza.DataBind()
        Try

            dt = obj.Filtrar_Descripcion_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), psAño, psModulo, psFechaIni, psfechafin, pdpersona, psMoneda)
            GvFinanza.DataSource = dt
            GvFinanza.DataBind()

            If dt.Rows.Count = 0 Then
                lblRegistro.Text = "No hay registros"
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro.Text = "Hay 1 registro"
            ElseIf dt.Rows.Count > 1 Then
                lblRegistro.Text = "Hay " & dt.Rows.Count & " registros"
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Response.Redirect("~/Finanzas/Finanzas_Registro.aspx")

    End Sub
End Class
