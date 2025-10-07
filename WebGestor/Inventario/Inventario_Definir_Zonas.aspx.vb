Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient

Partial Class Inventario_Inventario_Definir_Zonas
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim oFunc As New clsCont_Funciones
    Dim oFuncInv As New clsInv_Procesos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            obj.Llena_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), DdlBusAlmacen, Session("User"))
            obj.Llena_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), DdlAlmacen, Session("User"))
        End If

    End Sub
    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            Dim pdCodalmacen As Double = 0
            If DdlBusAlmacen.SelectedValue <> "< Seleccionar >" Then
                pdCodalmacen = Nz(DdlBusAlmacen.SelectedValue)
            End If

            Dim dt As DataTable
            dt = obj.Inventario_Almacen_ListaZonas(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodalmacen)
            GvZonas.DataSource = dt
            GvZonas.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub BtnIngZona_Click(sender As Object, e As EventArgs) Handles BtnIngZona.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalZona').modal('show');", True)
    End Sub

    Private Sub DdlAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAlmacen.SelectedIndexChanged
        Try
            Dim pdCodAlmacen As Double = 0
            Dim pdCodZona As Double = 0
            Dim dt As New DataTable
            Dim pdCodRegistro As Double = 0
            If DdlAlmacen.SelectedValue <> "< Seleccionar >" Then
                pdCodAlmacen = DdlAlmacen.SelectedValue
            End If
            If pdCodAlmacen = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar almacén.')", True)
            Else
                dt = obj.Inventario_Zona_UltimoRegistro(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen)
                For Each dr As DataRow In dt.Rows
                    pdCodZona = Nz(dr("cod_zona"))
                    pdCodRegistro = Nz(dr("cod_registro"))
                Next
                LblModalZona.Text = "Ingresar Zona"
                pdCodZona = pdCodZona + 1
                pdCodRegistro = pdCodRegistro + 1
                TxtZona.Text = Llenar_Ceros(pdCodZona, 3)
                TxtZonaNombre.Text = "ZONA " & Nz(TxtZona.Text)
                LblCodRegistro.Text = pdCodRegistro
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try

    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        TxtZona.Text = ""
        TxtZonaNombre.Text = ""
        DdlAlmacen.SelectedValue = "< Seleccionar >"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalZona').modal('hide');", True)
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Try
            Dim dt As New DataTable
            Dim pdCodAlmacen As Double = 0
            Dim pdCodzona As Double = 0
            Dim psZonaNombre As String = ""
            Dim pdCodRegistro As Double = 0
            If TxtZona.Text <> "" Then pdCodzona = Nz(TxtZona.Text)
            If TxtZonaNombre.Text <> "" Then psZonaNombre = TxtZonaNombre.Text
            If DdlAlmacen.SelectedValue <> "< Seleccionar >" Then pdCodAlmacen = DdlAlmacen.SelectedValue
            If LblCodRegistro.Text <> "" Then pdCodRegistro = Nz(LblCodRegistro.Text)
            If pdCodAlmacen = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar almacén.')", True)
            ElseIf pdCodzona = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Codigo Zona.')", True)
            ElseIf psZonaNombre = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Descripción Zona.')", True)
            Else
                dt = obj.Inventario_Zona_Insertar(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, pdCodzona, psZonaNombre, pdCodRegistro)
            End If
            '
            TxtZona.Text = ""
            TxtZonaNombre.Text = ""
            DdlAlmacen.SelectedValue = "< Seleccionar >"
            LblCodRegistro.Text = ""
            BtnListar_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalZona').modal('hide');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub GvZonas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvZonas.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pdZona As Double = 0
        Dim pdCodAlmacen As Double = 0
        Dim pdCodRegistroZona As Double = 0
        Dim dt As New DataTable
        Dim pdCodRack As Double = 0

        If e.CommandName = "Eliminar" Then
            LblModalZona.Text = "Editar Zona"
            pdCodAlmacen = Nz(GvZonas.Rows(Index).Cells(2).Text)
            pdZona = Nz(GvZonas.Rows(Index).Cells(4).Text)
            obj.Inventario_Zona_Delete(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, pdZona)
            BtnListar_Click(sender, e)
        End If
        If e.CommandName = "Rack" Then
            LblModalZona.Text = "Editar Zona"
            pdCodAlmacen = Nz(GvZonas.Rows(Index).Cells(2).Text)
            pdZona = Nz(GvZonas.Rows(Index).Cells(4).Text)
            TxtRackZona.Text = GvZonas.Rows(Index).Cells(4).Text
            TxtRackZonaNombre.Text = GvZonas.Rows(Index).Cells(5).Text
            TxtRackAlmacen.Text = GvZonas.Rows(Index).Cells(2).Text
            TxtRackAlmacenNombre.Text = GvZonas.Rows(Index).Cells(3).Text
            pdCodRegistroZona = Nz(GvZonas.Rows(Index).Cells(6).Text)
            LblCodRegistroZona.Text = pdCodRegistroZona
            dt = obj.Inventario_Rack_UltimoRegistro(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodAlmacen, pdCodRegistroZona)
            For Each dr As DataRow In dt.Rows
                pdCodRack = Nz(dr(0)) + 1
            Next
            TxtRack.Text = Llenar_Ceros(pdCodRack, 3)
            TxtRackNombre.Text = "RACK " & TxtRack.Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalRack').modal('show');", True)
        End If
        If e.CommandName = "Detalle" Then
            pdCodAlmacen = Nz(GvZonas.Rows(Index).Cells(2).Text)
            pdZona = Nz(GvZonas.Rows(Index).Cells(4).Text)
            TxtDetZona.Text = GvZonas.Rows(Index).Cells(4).Text
            TxtDetZonaDescripcion.Text = GvZonas.Rows(Index).Cells(5).Text
            TxtDetAlmacen.Text = GvZonas.Rows(Index).Cells(2).Text & " - " & GvZonas.Rows(Index).Cells(3).Text
            pdCodRegistroZona = Nz(GvZonas.Rows(Index).Cells(6).Text)
            LblCodRegistroZona.Text = pdCodRegistroZona
            dt = obj.Inventario_Rack_Relacion_xZona(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRegistroZona)
            GvRack.DataSource = dt
            GvRack.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalRackDetalle').modal('show');", True)
        End If
    End Sub

    Private Sub BtnRackCerrar_Click(sender As Object, e As EventArgs) Handles BtnRackCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalRack').modal('hide');", True)
    End Sub

    Private Sub BtnRackGuardar_Click(sender As Object, e As EventArgs) Handles BtnRackGuardar.Click
        Try
            Dim dt As New DataTable
            Dim pdCodAlmacen As Double = 0
            Dim pdCodzona As Double = 0
            Dim pdCodZonaCorrelativo As Double = 0
            Dim psZonaNombre As String = ""
            Dim pdCodRack As Double = 0
            Dim psRackNombre As String = ""
            Dim pdCantNivel As Double = 0
            Dim pdCantColumn As Double = 0
            Dim psValorsys As String = ""
            psValorsys = Session("User") & FechaActual() & HoraActual()
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Dim Rs As SqlDataReader
            Dim Nivel As Double = 0
            Dim Col As Double = 0
            Dim pdRegistro As Double = 0
            Cn.Open()
            CmdGlobal.Connection = Cn
            If TxtRackZona.Text <> "" Then pdCodzona = Nz(TxtRackZona.Text)
            If TxtRackZonaNombre.Text <> "" Then psZonaNombre = TxtRackZonaNombre.Text
            If TxtRackAlmacen.Text <> "" Then pdCodAlmacen = Nz(TxtRackAlmacen.Text)
            If TxtRack.Text <> "" Then pdCodRack = Nz(TxtRack.Text)
            If TxtRackNombre.Text <> "" Then psRackNombre = TxtRackNombre.Text
            If TxtRackNivel.Text <> "" Then pdCantNivel = Nz(TxtRackNivel.Text)
            If TxtRackCol.Text <> "" Then pdCantColumn = Nz(TxtRackCol.Text)
            If LblCodRegistroZona.Text <> "" Then pdCodZonaCorrelativo = Nz(LblCodRegistroZona.Text)
            If pdCodAlmacen = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar almacén.')", True)
            ElseIf pdCodzona = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Codigo Zona.')", True)
            ElseIf psZonaNombre = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Descripción Zona.')", True)
            ElseIf pdCodRack = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Codigo Rack.')", True)
            ElseIf psRackNombre = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Descripción del Rack.')", True)
            ElseIf pdCantNivel = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Cant. Niveles.')", True)
            ElseIf pdCantColumn = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Cant. Columnas')", True)
            Else
                pdRegistro = 0

                CmdGlobal.CommandText = " SELECT MAX(ALMAREA_CORRELATIVO) FROM TBINV_ALMACENES_AREAS   "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    Do While Rs.Read
                        pdRegistro = Nz(Rs(0)) + 1
                    Loop
                Else
                    pdRegistro = 1
                End If
                Rs.Close()

                CmdGlobal.CommandText = " INSERT INTO  TBINV_ALMACENES_AREAS (EMPRESA_CODIGO, ALMAREA_CORRELATIVO, ALMZONA_CODIGO, ALMAREA_CODIGO, ALMAREA_NIVEL, ALMAREA_ESTADO, " _
                                      & " ALMAREA_OCUPADO, ALMAREA_DISPONIBLE, ALMAREA_SYS_EST, ALMAREA_SYS_CRE, ALMACEN_CODIGO, ALMAREA_NOMBRE,ALMAREA_COLUMNA) " _
                                      & " VALUES ('" & Session("CodEmpresa") & "', " & pdRegistro & ", " & pdCodZonaCorrelativo & ", " & pdCodRack & ", " & pdCantNivel & ", '0', " _
                                      & " 0,1,'0','" & psValorsys & "', " & pdCodAlmacen & ", '" & psRackNombre & "', " & pdCantColumn & ") "
                CmdGlobal.ExecuteNonQuery()

                For Nivel = 1 To pdCantNivel
                    For Col = 1 To pdCantColumn
                        CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACENES_AREAS_NIVELES ( EMPRESA_CODIGO, ALMAREA_CORRELATIVO, ALMAREA_CODIGO, ALMAREA_NIVEL_CODIGO, ALMAREA_NIVEL_SYS_EST, " _
                                              & " ALMAREA_NIVEL_UBICACION,  ALMAREA_NIVEL_OCUPADO, ALMAREA_NIVEL_DISPONIBLE, ALMAREA_NIVEL_SYS_CRE )" _
                                              & " VALUES ('" & Session("CodEmpresa") & "', " & pdRegistro & " , " & pdCodRack & ", " & Nivel & ",'0', " & Col & ", 0,1,'" & psValorsys & "') "
                        CmdGlobal.ExecuteNonQuery()
                    Next
                Next
            End If
            TxtRackZona.Text = ""
            TxtRackZonaNombre.Text = ""
            TxtRackAlmacen.Text = ""
            TxtRackAlmacenNombre.Text = ""
            TxtRack.Text = ""
            TxtRackNombre.Text = ""
            TxtRackCol.Text = ""
            TxtRackNivel.Text = ""
            BtnListar_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalRack').modal('hide');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalRackDetalle').modal('hide');", True)
    End Sub
End Class
