Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Inventario_Inventario_Clasificacion

    Inherits System.Web.UI.Page

    Protected Function Eliminar() As DataTable
        Dim obj As New Cls_Clasificacion
        Dim codigo As String = LblCodClas.Text.ToString
        Dim n As Integer = CInt(Nivel.Text.ToString)
        Dim nA As Integer = CInt(Nivel.Text.ToString) - 1
        Dim n1 As String = Nivel1.Text.ToString
        Dim dt As DataTable
        If n = 2 Then
            dt = obj.NodosHijos1(Session("Ruta_Emp"), n1, n)
        ElseIf n = 11 Then
            dt = New DataTable
        Else
            dt = obj.NodosHijos(Session("Ruta_Emp"), n1, n, nA, codigo)
        End If
        Dim contador As Integer = dt.Rows.Count
        If contador > 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar');", True)
        Else
            Dim conn As New SqlConnection(Session("Ruta_Emp"))
            Dim cmd As SqlCommand = New SqlCommand("PROC_INV_DEL_CLASIFICACION", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@CODIGO", codigo)
            Dim Da As New SqlDataAdapter(cmd)
            Dim DTable As New DataTable("PROC_INV_DEL_CLASIFICACION")
            Da.Fill(DTable)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Eliminado');", True)
            Ocultar_Mostrar(False)
            Limpiar_Seleccion()
            obj.PopularRootLevel(Session("Ruta_Emp"))
        End If
        Return dt
    End Function

    Protected Function Grabar() As DataTable
        Dim descripcion As String = TxtDescripcion.Text.ToString
        Dim n As String = Nivel.Text.ToString
        Dim n1 As String = Nivel1.Text.ToString
        Dim n2 As String = Nivel2.Text.ToString
        Dim n3 As String = Nivel3.Text.ToString
        Dim n4 As String = Nivel4.Text.ToString
        Dim n5 As String = Nivel5.Text.ToString
        Dim n6 As String = Nivel6.Text.ToString
        Dim n7 As String = Nivel7.Text.ToString
        Dim n8 As String = Nivel8.Text.ToString
        Dim n9 As String = Nivel9.Text.ToString
        Dim n10 As String = Nivel10.Text.ToString
        Dim conn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As SqlCommand = New SqlCommand("PROC_INV_INS_CLASIFICACION", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        cmd.Parameters.AddWithValue("@NIVEL", n)
        cmd.Parameters.AddWithValue("@NIVEL1", n1)
        cmd.Parameters.AddWithValue("@NIVEL2", n2)
        cmd.Parameters.AddWithValue("@NIVEL3", n3)
        cmd.Parameters.AddWithValue("@NIVEL4", n4)
        cmd.Parameters.AddWithValue("@NIVEL5", n5)
        cmd.Parameters.AddWithValue("@NIVEL6", n6)
        cmd.Parameters.AddWithValue("@NIVEL7", n7)
        cmd.Parameters.AddWithValue("@NIVEL8", n8)
        cmd.Parameters.AddWithValue("@NIVEL9", n9)
        cmd.Parameters.AddWithValue("@NIVEL10", n10)
        Dim Da As New SqlDataAdapter(cmd)
        Dim Dt As New DataTable("PROC_INV_INS_CLASIFICACION")
        Da.Fill(Dt)
        Return Dt
    End Function

    Protected Function Actualizar() As DataTable
        Dim descripcion As String = LblClasificacion.Text.ToString
        Dim codigo As String = LblCodClas.Text.ToString
        Dim n As Integer = CInt(Nivel.Text.ToString) - 1
        Dim n1 As String = Nivel1.Text.ToString
        Dim conn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As SqlCommand = New SqlCommand("PROC_INV_UPD_CLASIFICACION", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        cmd.Parameters.AddWithValue("@CODIGO", codigo)
        cmd.Parameters.AddWithValue("@NIVEL", n)
        cmd.Parameters.AddWithValue("@NIVEL1", n1)
        Dim Da As New SqlDataAdapter(cmd)
        Dim Dt As New DataTable("PROC_INV_UPD_CLASIFICACION")
        Da.Fill(Dt)
        Return Dt
    End Function


    Protected Sub Ocultar_Mostrar(ByVal vf As Boolean)
        TxtDescripcion.Visible = vf
        LblDescripción.Visible = vf
        BtnCancelar.Visible = vf
        BtnGrabar.Visible = vf
        TxtDescripcion.Text = ""
    End Sub

    Sub Limpiar_Seleccion()
        Nivel.Text = "1"
        Nivel1.Text = ""
        Nivel2.Text = ""
        Nivel3.Text = ""
        Nivel4.Text = ""
        Nivel5.Text = ""
        Nivel6.Text = ""
        Nivel7.Text = ""
        Nivel8.Text = ""
        Nivel9.Text = ""
        Nivel10.Text = ""
        trvClasificacion.SelectedNode.Selected = False
        LblClasificacion.Text = ""
        LblCodClas.Text = ""
        LblClasificacion.Enabled = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Listar_Nivel_1()
        End If
    End Sub

    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnAregarSubNivel.Click
        If LblCodClas.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione Nivel');", True)
        Else
            Ocultar_Mostrar(True)
            BtnGrabar.Text = "Guardar"
        End If
    End Sub

    Private Sub BtnGrabar_Click(sender As Object, e As EventArgs) Handles BtnGrabar.Click
        Dim obj As New Cls_Clasificacion
        Dim descripcionG As String = TxtDescripcion.Text.ToString
        Dim descripcionA As String = LblClasificacion.Text.ToString
        Dim n As String = Nivel.Text.ToString
        Dim dt As DataTable
        Dim dbRow As DataRow

        If BtnGrabar.Text = "Guardar" Then
            If descripcionG = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una descripción');", True)
            ElseIf n = "11" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede agregar más de 10 niveles');", True)
            Else
                dt = Grabar()
                dbRow = dt.Rows(0)
                If dbRow(0) = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La descripción ya existe');", True)
                ElseIf dbRow(0) = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Registrado Correctamente');", True)
                    Ocultar_Mostrar(False)
                    Limpiar_Seleccion()
                    Listar_Nivel_1()
                End If
            End If
        ElseIf BtnGrabar.Text = "Actualizar" Then
            If descripcionA = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una descripción');", True)
            Else
                dt = Actualizar()
                dbRow = dt.Rows(0)
                If dbRow(0) = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La descripción ya existe');", True)
                ElseIf dbRow(0) = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Actualizado Correctamente');", True)
                    Ocultar_Mostrar(False)
                    Limpiar_Seleccion()
                    Listar_Nivel_1()
                End If
            End If
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        If BtnGrabar.Text = "Actualizar" Then
            Limpiar_Seleccion()
        End If
        Ocultar_Mostrar(False)
    End Sub

    Sub Listar_Nivel_1()
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
    End Sub

    Private Sub trvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles trvClasificacion.TreeNodePopulate
        Dim obj As New Cls_Clasificacion
        Dim dt As DataTable = obj.NumeroNodo(Session("Ruta_Emp"), CInt(e.Node.Value))
        Dim dbRow As DataRow = dt.Rows(0)
        Dim nivelPrincipal As Integer = CInt(dbRow(1).ToString)
        Dim nodo As Integer = CInt(dbRow(0).ToString) + 1
        Dim nodoAyuda As Integer = CInt(dbRow(0).ToString)
        Dim codigo As Integer = CInt(e.Node.Value)
        If nodo = 2 Then
            dt = obj.NodosHijos1(Session("Ruta_Emp"), nivelPrincipal, nodo)
            obj.NodosPopulares(dt, e.Node.ChildNodes)
        Else
            dt = obj.NodosHijos(Session("Ruta_Emp"), nivelPrincipal, nodo, nodoAyuda, codigo)
            obj.NodosPopulares(dt, e.Node.ChildNodes)
        End If
    End Sub

    Protected Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        Dim obj As New Cls_Clasificacion
        trvClasificacion.SelectedNode.Selected = True
        LblCodClas.Text = trvClasificacion.SelectedNode.Text
        Dim psPosicion As Long = InStr(LblCodClas.Text, "-")
        LblClasificacion.Text = Mid(LblCodClas.Text, psPosicion + 2)
        LblCodClasAyuda.Text = LblCodClas.Text.Substring(0, psPosicion - 2)
        LblCodClas.Text = trvClasificacion.SelectedValue.ToString
        Dim dt As DataTable = obj.NumeroNodo(Session("Ruta_Emp"), CInt(trvClasificacion.SelectedValue))
        Dim dbRow As DataRow = dt.Rows(0)
        Nivel.Text = (CInt(dbRow(0).ToString) + 1)
        Nivel1.Text = dbRow(1).ToString
        Nivel2.Text = dbRow(2).ToString
        Nivel3.Text = dbRow(3).ToString
        Nivel4.Text = dbRow(4).ToString
        Nivel5.Text = dbRow(5).ToString
        Nivel6.Text = dbRow(6).ToString
        Nivel7.Text = dbRow(7).ToString
        Nivel8.Text = dbRow(8).ToString
        Nivel9.Text = dbRow(9).ToString
        Nivel10.Text = dbRow(10).ToString
        If Nivel.Text = "2" Then Nivel2.Text = LblCodClasAyuda.Text
        If Nivel.Text = "3" Then Nivel3.Text = LblCodClasAyuda.Text
        If Nivel.Text = "4" Then Nivel4.Text = LblCodClasAyuda.Text
        If Nivel.Text = "5" Then Nivel5.Text = LblCodClasAyuda.Text
        If Nivel.Text = "6" Then Nivel6.Text = LblCodClasAyuda.Text
        If Nivel.Text = "7" Then Nivel7.Text = LblCodClasAyuda.Text
        If Nivel.Text = "8" Then Nivel8.Text = LblCodClasAyuda.Text
        If Nivel.Text = "9" Then Nivel9.Text = LblCodClasAyuda.Text
        If Nivel.Text = "10" Then Nivel10.Text = LblCodClasAyuda.Text
        Ocultar_Mostrar(False)
        LblClasificacion.Enabled = False
    End Sub

    Private Sub BtnAgregarNivel_Click(sender As Object, e As EventArgs) Handles BtnAgregarNivel.Click
        Ocultar_Mostrar(True)
        Limpiar_Seleccion()
        BtnGrabar.Text = "Guardar"
    End Sub

    Private Sub BtnEditar_Click(sender As Object, e As EventArgs) Handles BtnEditar.Click
        If LblClasificacion.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione el Nivel');", True)
        Else
            LblClasificacion.Enabled = True
            BtnGrabar.Visible = True
            BtnCancelar.Visible = True
            BtnGrabar.Text = "Actualizar"
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles BtnEliminar.Click
        If LblClasificacion.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione el Nivel');", True)
        Else
            Eliminar()
            Listar_Nivel_1()
        End If
    End Sub
End Class
