Imports System.Data.SqlClient
Imports System.Data
Public Class CRM_Relacion_con_Procesos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combo_Proceso()
            Llenar_Combo_Nivel1()
        End If
    End Sub
    Protected Sub Lista_Relacion_Proceso()
        Dim obj As New Cls_Relacion_con_Procesos
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Lista_Relacion_Proceso(psconexion)
        GvListaRelacionProcesos.DataSource = dt
        GvListaRelacionProcesos.DataBind()

        LblTotalRelacionProcesosL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalRelacionProcesos.Visible = True
        LblTotalRelacionProcesosL.Visible = True
    End Sub
    Private Sub BtnRelacionar_Click(sender As Object, e As EventArgs) Handles BtnRelacionar.Click
        LblRelacionProcesos.Visible = True
        LblNivel1.Visible = True
        DdlNivel1.Visible = True
        ChkNivel2.Visible = True
        DdlNivel2.Visible = True
        ChkNivel3.Visible = True
        DdlNivel3.Visible = True
        BtnGrabar.Visible = True
        LblProceso.Visible = True
        DdlProceso.Visible = True
        BtnCancelar.Visible = True
        Limpiar_Cajas_Relacion_Procesos()
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        LblRelacionProcesos.Visible = False
        LblNivel1.Visible = False
        DdlNivel1.Visible = False
        ChkNivel2.Visible = False
        DdlNivel2.Visible = False
        ChkNivel3.Visible = False
        DdlNivel3.Visible = False
        BtnGrabar.Visible = False
        LblProceso.Visible = False
        DdlProceso.Visible = False
        BtnCancelar.Visible = False
        Limpiar_Cajas_Relacion_Procesos()
    End Sub
    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        System.Threading.Thread.Sleep(1000)
        Lista_Relacion_Proceso()
    End Sub
    Protected Sub Llenar_Combo_Proceso()
        Dim obj As New Cls_Relacion_con_Procesos
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Proceso(psconexion)
        DdlProceso.DataSource = dt
        DdlProceso.DataValueField = "PROCESO_CODIGO"
        DdlProceso.DataTextField = "NOMBRE"
        DdlProceso.DataBind()
        DdlProceso.Items.Add("< Seleccionar >")
        DdlProceso.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Nivel1()
        Dim obj As New Cls_Relacion_con_Procesos
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Nivel1(psconexion)
        DdlNivel1.DataSource = dt
        DdlNivel1.DataValueField = "NIVEL1_CODIGO"
        DdlNivel1.DataTextField = "NIVEL1"
        DdlNivel1.DataBind()
        DdlNivel1.Items.Add("< Seleccionar >")
        DdlNivel1.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub DdlNivel1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlNivel1.SelectedIndexChanged
        Dim obj As New Cls_Relacion_con_Procesos
        Dim dt As New DataTable
        Dim codigo As String = DdlNivel1.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")

        If codigo = "< Seleccionar >" Then
            DdlNivel2.Items.Clear()
        ElseIf ChkNivel2.Checked = True Then
            dt = obj.Llenar_Combo_Nivel2(psconexion, codigo)
            DdlNivel2.DataSource = dt
            DdlNivel2.DataValueField = "NIVEL2_CODIGO"
            DdlNivel2.DataTextField = "NIVEL2"
            DdlNivel2.DataBind()
            DdlNivel2.Items.Add("< Seleccionar >")
            DdlNivel2.SelectedValue = "< Seleccionar >"
            DdlNivel3.SelectedValue = "< Seleccionar >"
        End If
    End Sub

    Private Sub DdlNivel2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlNivel2.SelectedIndexChanged
        Dim obj As New Cls_Relacion_con_Procesos
        Dim dt As New DataTable
        Dim codigo As String = DdlNivel2.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")

        If codigo = "< Seleccionar >" Then
            DdlNivel3.Items.Clear()
        ElseIf ChkNivel3.Checked = True Then
            dt = obj.Llenar_Combo_Nivel3(psconexion, codigo)
            DdlNivel3.DataSource = dt
            DdlNivel3.DataValueField = "NIVEL3_CODIGO"
            DdlNivel3.DataTextField = "NIVEL3"
            DdlNivel3.DataBind()
            DdlNivel3.Items.Add("< Seleccionar >")
            DdlNivel3.SelectedValue = "< Seleccionar >"
        End If
    End Sub

    Private Sub BtnGrabar_Click(sender As Object, e As EventArgs) Handles BtnGrabar.Click
        Dim obj As New Cls_Relacion_con_Procesos
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Nivel1 As String = DdlNivel1.SelectedValue.ToString
        Dim Nivel2 As String = DdlNivel2.SelectedValue.ToString
        Dim Nivel3 As String = DdlNivel3.SelectedValue.ToString
        Dim Proceso As String = DdlProceso.SelectedValue.ToString
        Dim dt As DataTable

        If Nivel1.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Nivel');", True)
        ElseIf Nivel2.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Nivel');", True)
        ElseIf Nivel3.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Nivel');", True)
        ElseIf Proceso.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Proceso');", True)

        Else
            If Nivel2.Equals("") Then Nivel2 = 0
            If Nivel3.Equals("") Then Nivel3 = 0
            dt = obj.Insertar_Estado_Relacion_Procesos(psconexion, Nivel1, Nivel2, Nivel3, Proceso)
                    Dim dvRow As DataRow = dt.Rows(0)
                    If dvRow(0) = "2" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe relación en la tabla');", True)
                    End If
                    Lista_Relacion_Proceso()
                End If
    End Sub

    Private Sub ChkNivel2_CheckedChanged(sender As Object, e As EventArgs) Handles ChkNivel2.CheckedChanged
        If ChkNivel2.Checked = True Then
            DdlNivel2.Enabled = True
        Else
            DdlNivel2.Enabled = False
            DdlNivel2.Items.Clear()
        End If
    End Sub

    Private Sub ChkNivel3_CheckedChanged(sender As Object, e As EventArgs) Handles ChkNivel3.CheckedChanged
        If ChkNivel3.Checked = True Then
            DdlNivel3.Enabled = True
        Else
            DdlNivel3.Enabled = False
            DdlNivel3.Items.Clear()
        End If
    End Sub

    Private Sub GvListaRelacionProcesos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaRelacionProcesos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Relacion_con_Procesos
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Nivel1 As String = GvListaRelacionProcesos.Rows(Index).Cells(0).Text
        Dim Nivel2 As String = GvListaRelacionProcesos.Rows(Index).Cells(2).Text
        Dim Nivel3 As String = GvListaRelacionProcesos.Rows(Index).Cells(8).Text
        Dim Proceso As String = GvListaRelacionProcesos.Rows(Index).Cells(3).Text
        Dim dt As New DataTable

        If e.CommandName = "QuitarRelacion" Then
            dt = obj.Eliminar_Estado_Relacion_Procesos(psconexion, Nivel1, Nivel2, Nivel3, Proceso)
            Lista_Relacion_Proceso()
        End If
    End Sub
    Protected Sub Limpiar_Cajas_Relacion_Procesos()
        DdlNivel1.SelectedValue = "< Seleccionar >"
        ChkNivel2.Checked = False
        DdlNivel2.Enabled = False
        DdlNivel2.Items.Clear()
        ChkNivel3.Checked = False
        DdlNivel3.Enabled = False
        DdlNivel3.Items.Clear()
        DdlProceso.SelectedValue = "< Seleccionar >"
    End Sub
End Class