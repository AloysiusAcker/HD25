Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor


Public Class CRM_Tiempo_Estado_Ticket
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Barra_Progreso()
            LLenar_Combo_Dias()
            LLenar_Combo_Horas()
            LLenar_Combo_Minutos()
            Call LlenaComboItem("TBOPC473", DdlTipoProceso)
            Call LlenaComboItem("TBOPC473", DdlTipoProcesoESTADOCLIENTE)
            Call LlenaComboItem("TBOPC473", DdlTipoProcesoRELACION)
            Call LlenaComboItem("TBOPC473", DdlTipoProcesoESTADO)

            Call LlenaComboItem("TBOPC480", DdlEstadoESTADOCLIENTE)

            Llenar_Combo_Peticion()

            Call LlenaComboItem("TBOPC475", DdlEstadoPROCESOESTADO)

            Call LlenaComboItem("TBOPC520", DdlAccionMAA)

            Call LlenaComboItem("TBOPC480", DdlEstadoCliente)
            Call LlenaComboItem("TBOPC480", DdlEstadoTiempoEstadoCliente)
            Call LlenaComboItem("TBOPC480", DdlAccionesCliente)
            Call LlenaComboItem("TBOPC480", DdlAccionesTiempoEstadoCliente)
        End If
    End Sub

    '
    '
    'TIEMPO ESTADO TICKET
    '
    '

    Protected Sub Lista_Estado_Tiempo()
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Estado_Tiempo(psconexion)
        GvListaEstadoTiempo.DataSource = dt
        GvListaEstadoTiempo.DataBind()

        LblTotalEstadosTiempoL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalEstadosTiempo.Visible = True
        LblTotalEstadosTiempoL.Visible = True
    End Sub
    Private Sub BtnListarRelaciónT_Click(sender As Object, e As EventArgs) Handles BtnListarRelaciónT.Click
        System.Threading.Thread.Sleep(1000)
        Lista_Estado_Tiempo()
    End Sub
    Private Sub BtnEstadoSiguiente_Click(sender As Object, e As EventArgs) Handles BtnEstadoSiguiente.Click
        LblIngreso.Visible = True
        LblIngreso.Text = "Ingresar Estado Relación"
        LblTipoProceso.Visible = True
        DdlTipoProceso.Visible = True
        DdlTipoProceso.Enabled = True
        LblEstadoTicket.Visible = True
        DdlEstadoTicket.Visible = True
        DdlEstadoTicket.Enabled = True
        LblEstadoRelacion.Visible = True
        DdlEstadoRelacion.Visible = True
        BtnGuardarEstadoRelacion.Visible = True
        BtnCancelarEstadoRelacion.Visible = True
        LblDuracion.Visible = False
        LblDias.Visible = False
        DdlDias.Visible = False
        LblHoras.Visible = False
        DdlHoras.Visible = False
        LblMinutos.Visible = False
        DdlMinutos.Visible = False
        BtnActualizarTiempo.Visible = False
        BtnCancelarTiempo.Visible = False
        DdlTipoProceso.SelectedValue = "< Seleccionar >"
        DdlEstadoRelacion.SelectedValue = "< Seleccionar >"
        DdlEstadoTicket.SelectedValue = "< Seleccionar >"
        DdlDias.SelectedValue = "--"
        DdlHoras.SelectedValue = "--"
        DdlMinutos.SelectedValue = "--"
    End Sub

    Private Sub BtnGuardarEstadoRelacion_Click(sender As Object, e As EventArgs) Handles BtnGuardarEstadoRelacion.Click
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstadoTicket As String = DdlEstadoTicket.SelectedValue.ToString
        Dim EstadoRelacion As String = DdlEstadoRelacion.SelectedValue.ToString
        Dim dt As DataTable
        If EstadoTicket.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un Estado Ticket');", True)
        ElseIf EstadoRelacion.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Estado Relación');", True)
        ElseIf EstadoTicket.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Estado Ticket');", True)
        ElseIf EstadoRelacion.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Estado Relación');", True)
        Else
            dt = obj.Insertar_Estado_Relacion(psconexion, EstadoTicket, EstadoRelacion)
            Dim dvRow As DataRow = dt.Rows(0)
            If dvRow(0) = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe relación en la tabla');", True)
            Else
                Lista_Estado_Tiempo()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se guardó la relación correctamente');", True)
            End If
        End If
    End Sub
    Private Sub GvListaEstadoTiempo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaEstadoTiempo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Estado_Ticket
        Dim psconexion As String = Session("Ruta_Emp")
        Dim ticketEstado As String = Replace(GvListaEstadoTiempo.Rows(Index).Cells(2).Text, "&nbsp;", "")
        Dim estadoRelacion As String = Replace(GvListaEstadoTiempo.Rows(Index).Cells(6).Text, "&nbsp;", "")
        Dim tiempo As String = Replace(GvListaEstadoTiempo.Rows(Index).Cells(4).Text, "&nbsp;", "")
        Dim dias As String = "0"
        Dim horas As String = "0"
        Dim minutos As String = "0"
        Dim dt As New DataTable

        If GvListaEstadoTiempo.Rows(Index).Cells(4).Text <> "&nbsp;" Then
            dias = tiempo.Substring(0, 2).ToString.Trim
            horas = tiempo.Substring(8, 2).ToString.Trim
            minutos = tiempo.Substring(17, 2).ToString.Trim
        End If

        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As New SqlCommand
        cmd.CommandText = "select PROCESO_CODIGO from TBTICKET_RELACION_PROCESO_ESTADO WHERE ESTADO_CODIGO = " + ticketEstado
        cmd.CommandType = CommandType.Text
        cmd.Connection = cn
        cn.Open()
        Dim TbTicket As New DataTable
        TbTicket.Load(cmd.ExecuteReader())
        cn.Close()

        If e.CommandName = "BtnEliminarRelacion" Then
            If Replace(GvListaEstadoTiempo.Rows(Index).Cells(6).Text, "&nbsp;", "").Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El estado no tiene relación');", True)
            Else
                dt = obj.Eliminar_Relacion(psconexion, ticketEstado, estadoRelacion)
                GvListaEstadoTiempo.DataSource = dt
                GvListaEstadoTiempo.DataBind()
            End If

        ElseIf e.CommandName = "BtnEditarTiempo" Then
            LblIngreso.Visible = True
            LblIngreso.Text = "Editar Tiempo"
            LblTipoProceso.Visible = True
            DdlTipoProceso.Visible = True
            DdlTipoProceso.Enabled = True
            LblEstadoTicket.Visible = True
            DdlEstadoTicket.Visible = True
            DdlEstadoTicket.Enabled = True
            LblDias.Visible = True
            DdlDias.Visible = True
            LblHoras.Visible = True
            DdlHoras.Visible = True
            LblMinutos.Visible = True
            DdlMinutos.Visible = True
            LblDuracion.Visible = True
            BtnActualizarTiempo.Visible = True
            BtnCancelarTiempo.Visible = True
            BtnGuardarEstadoRelacion.Visible = False
            BtnCancelarEstadoRelacion.Visible = False
            LblEstadoRelacion.Visible = True
            DdlEstadoRelacion.Visible = True
            DdlEstadoRelacion.Enabled = True
            DdlTipoProceso.SelectedValue = "< Seleccionar >"
            DdlTipoProceso.SelectedValue = "1"
            DdlEstadoTicket.Items.Clear()
            DdlEstadoTicket.Items.Add("< Seleccionar >") : DdlEstadoTicket.SelectedValue = "< Seleccionar >"
            DdlEstadoRelacion.Items.Clear()
            DdlEstadoRelacion.Items.Add("< Seleccionar >") : DdlEstadoRelacion.SelectedValue = "< Seleccionar >"
            DdlDias.SelectedValue = "--"
            DdlHoras.SelectedValue = "--"
            DdlMinutos.SelectedValue = "--"
            Dim e1 As New EventArgs
            DdlTipoProceso_SelectedIndexChanged(sender, e1)
            DdlEstadoTicket.SelectedValue = ticketEstado
            DdlEstadoRelacion.SelectedValue = estadoRelacion
            DdlDias.SelectedValue = CInt(dias)
            DdlHoras.SelectedValue = CInt(horas)
            DdlMinutos.SelectedValue = CInt(minutos)
        End If
    End Sub

    Private Sub BtnCancelarEstadoRelacion_Click(sender As Object, e As EventArgs) Handles BtnCancelarEstadoRelacion.Click
        LblIngreso.Visible = False
        LblIngreso.Text = ""
        LblTipoProceso.Visible = False
        DdlTipoProceso.Visible = False
        LblEstadoTicket.Visible = False
        DdlEstadoTicket.Visible = False
        LblEstadoRelacion.Visible = False
        DdlEstadoRelacion.Visible = False
        BtnGuardarEstadoRelacion.Visible = False
        BtnCancelarEstadoRelacion.Visible = False
        DdlTipoProceso.SelectedValue = "< Seleccionar >"
        DdlEstadoTicket.SelectedValue = "< Seleccionar >"
        DdlEstadoRelacion.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub BtnCancelarTiempo_Click(sender As Object, e As EventArgs) Handles BtnCancelarTiempo.Click
        LblIngreso.Visible = False
        LblIngreso.Text = ""
        LblTipoProceso.Visible = False
        DdlTipoProceso.Visible = False
        LblEstadoTicket.Visible = False
        DdlEstadoTicket.Visible = False
        BtnCancelarTiempo.Visible = False
        BtnActualizarTiempo.Visible = False
        LblDias.Visible = False
        DdlDias.Visible = False
        LblHoras.Visible = False
        DdlHoras.Visible = False
        LblMinutos.Visible = False
        DdlMinutos.Visible = False
        LblDuracion.Visible = False
        DdlTipoProceso.SelectedValue = "< Seleccionar >"
        DdlEstadoTicket.SelectedValue = "< Seleccionar >"
        DdlEstadoRelacion.SelectedValue = "< Seleccionar >"
        DdlDias.SelectedValue = "--"
        DdlHoras.SelectedValue = "--"
        DdlMinutos.SelectedValue = "--"
    End Sub
    Protected Sub Llenar_Combo_Tipo_Procesos()
        Dim obj As New Cls_Estado_Ticket
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Tipo_Procesos(psconexion)
        DdlTipoProceso.DataSource = dt
        DdlTipoProceso.DataValueField = "ELEMEN_CODIGO"
        DdlTipoProceso.DataTextField = "ELEMEN_VALOR"
        DdlTipoProceso.DataBind()
        DdlTipoProceso.Items.Add("< Seleccionar >")
        DdlTipoProceso.SelectedValue = "< Seleccionar >"
        DdlTipoProcesoESTADOCLIENTE.DataSource = dt
        DdlTipoProcesoESTADOCLIENTE.DataValueField = "ELEMEN_CODIGO"
        DdlTipoProcesoESTADOCLIENTE.DataTextField = "ELEMEN_VALOR"
        DdlTipoProcesoESTADOCLIENTE.DataBind()
        DdlTipoProcesoESTADOCLIENTE.Items.Add("< Seleccionar >")
        DdlTipoProcesoESTADOCLIENTE.SelectedValue = "< Seleccionar >"
        DdlTipoProcesoRELACION.DataSource = dt
        DdlTipoProcesoRELACION.DataValueField = "ELEMEN_CODIGO"
        DdlTipoProcesoRELACION.DataTextField = "ELEMEN_VALOR"
        DdlTipoProcesoRELACION.DataBind()
        DdlTipoProcesoRELACION.Items.Add("< Seleccionar >")
        DdlTipoProcesoRELACION.SelectedValue = "< Seleccionar >"
        DdlTipoProcesoESTADO.DataSource = dt
        DdlTipoProcesoESTADO.DataValueField = "ELEMEN_CODIGO"
        DdlTipoProcesoESTADO.DataTextField = "ELEMEN_VALOR"
        DdlTipoProcesoESTADO.DataBind()
        DdlTipoProcesoESTADO.Items.Add("< Seleccionar >")
        DdlTipoProcesoESTADO.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub LLenar_Combo_Dias()
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        DdlDias.Items.Clear()
        DdlDiasTiempoEstadoCliente.Items.Clear()
        For index = 0 To 90
            DdlDias.Items.Add(index)
            DdlDiasTiempoEstadoCliente.Items.Add(index)
        Next
        DdlDias.Items.Add("--")
        DdlDias.SelectedValue = "--"
        DdlDiasTiempoEstadoCliente.Items.Add("--")
        DdlDiasTiempoEstadoCliente.SelectedValue = "--"
    End Sub
    Protected Sub LLenar_Combo_Horas()
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        DdlHoras.Items.Clear()
        DdlHorasTiempoEstadoCliente.Items.Clear()
        For index = 0 To 24
            DdlHoras.Items.Add(index)
            DdlHorasTiempoEstadoCliente.Items.Add(index)
        Next
        DdlHoras.Items.Add(("--"))
        DdlHoras.SelectedValue = "--"
        DdlHorasTiempoEstadoCliente.Items.Add(("--"))
        DdlHorasTiempoEstadoCliente.SelectedValue = "--"
    End Sub
    Protected Sub LLenar_Combo_Minutos()
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        DdlMinutos.Items.Clear()
        DdlMinutosTiempoEstadoCliente.Items.Clear()
        For index = 0 To 59
            DdlMinutos.Items.Add(index)
            DdlMinutosTiempoEstadoCliente.Items.Add(index)
        Next
        DdlMinutos.Items.Add("--")
        DdlMinutos.SelectedValue = "--"
        DdlMinutosTiempoEstadoCliente.Items.Add("--")
        DdlMinutosTiempoEstadoCliente.SelectedValue = "--"
    End Sub
    Private Sub DdlTipoProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoProceso.SelectedIndexChanged
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim codigo As String = DdlTipoProceso.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")

        If codigo = "< Seleccionar >" Then
            DdlEstadoTicket.Items.Clear()
            DdlEstadoTicket.Items.Add("< Seleccionar >") : DdlEstadoTicket.SelectedValue = "< Seleccionar >"
            DdlEstadoRelacion.Items.Clear()
            DdlEstadoRelacion.Items.Add("< Seleccionar >") : DdlEstadoRelacion.SelectedValue = "< Seleccionar >"
        Else

            DdlEstadoTicket.Items.Clear()
            DdlEstadoRelacion.Items.Clear()

            dt = obj.Llenar_Combo_Estado_Ticket(psconexion, codigo)
            If DdlEstadoTicket.Items.Count > 0 Then

            End If
            DdlEstadoTicket.DataSource = dt
            DdlEstadoTicket.DataValueField = "ELEMEN_CODIGO"
            DdlEstadoTicket.DataTextField = "ELEMEN_VALOR"
            DdlEstadoTicket.DataBind()
            DdlEstadoTicket.Items.Add("< Seleccionar >")
            DdlEstadoTicket.SelectedValue = "< Seleccionar >"
            DdlEstadoRelacion.DataSource = dt
            DdlEstadoRelacion.DataValueField = "ELEMEN_CODIGO"
            DdlEstadoRelacion.DataTextField = "ELEMEN_VALOR"
            DdlEstadoRelacion.DataBind()
            DdlEstadoRelacion.Items.Add("< Seleccionar >")
            DdlEstadoRelacion.SelectedValue = "< Seleccionar >"

        End If

    End Sub
    Private Sub BtnActualizarTiempo_ClickBtnActualizarTiempo_Click(sender As Object, e As EventArgs) Handles BtnActualizarTiempo.Click
        Dim obj As New Cls_Estado_Ticket
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim ticket As String = DdlEstadoTicket.SelectedValue.ToString
        Dim dias As String = DdlDias.SelectedValue.ToString
        Dim horas As String = DdlHoras.SelectedValue.ToString
        Dim minutos As String = DdlMinutos.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")

        If dias = "--" Then dias = "00"
        If horas = "--" Then horas = "00"
        If minutos = "--" Then minutos = "00"

        If CInt(dias) < 10 Then dias = "0" + dias
        If CInt(horas) < 10 Then horas = "0" + horas
        If CInt(minutos) < 10 Then minutos = "0" + minutos

        If dias = "00" And horas = "00" And minutos = "00" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione el tiempo que va a ingresar');", True)
        Else
            Dim total As Integer = (CInt(dias) * 1440) + (CInt(horas) * 60) + CInt(minutos)
            dt = obj.Actualizar_Estado_Tiempo(psconexion, ticket, dias, horas, minutos, total)
            BtnCancelarTiempo_Click(sender, e)
            BtnListarRelaciónT_Click(sender, e)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se actualizó el tiempo correctamente');", True)
        End If
    End Sub


    '
    '
    'CLIENTE
    '
    '
    Protected Sub Barra_Progreso()
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim dtAyuda As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim dbRow As DataRow
        Dim porcentaje As Integer
        Dim tablaAyuda As New DataTable
        Dim grid As New GridView

        dt = obj.Contar_Clientes(psconexion, "%")
        dbRow = dt.Rows(0)
        Dim total As Integer = CInt(dbRow(0))
        TotalClientesP.InnerText = dbRow(0).ToString

        dtAyuda = obj.Llenar_Estado(psconexion)

        For index = 1 To 2
            Dim dc As New DataColumn("Fila" + CStr(index), Type.GetType("System.String"))
            tablaAyuda.Columns.Add(dc)
        Next

        Dim cs = New StringBuilder()
        cs.Append("")
        LiteralAyuda.Text = cs.ToString()
        Dim c As Integer = 1
        For index = 1 To dtAyuda.Rows.Count
            dt = obj.Contar_Clientes(psconexion, index)
            dbRow = dt.Rows(0)
            porcentaje = (dbRow(0) / total) * 100

            cs.Append("<div class='form-group' style='height: 30px'>")
            cs.Append("<p class='col-lg-5 control-label'>" + dbRow(1).ToString + "</p>")
            cs.Append("<p class='control-label-2' style='width:15px'>" + dbRow(0).ToString + "</p>")
            cs.Append("<div Class='col-lg-6'>")
            cs.Append("<div Class='progress' style='height: 25px;'>")
            If c = 1 Then
                cs.Append("<div class='progress-bar progress-bar-success' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 2
            ElseIf c = 2 Then
                cs.Append("<div class='progress-bar progress-bar-danger' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 3
            ElseIf c = 3 Then
                cs.Append("<div class='progress-bar progress-bar-info' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 4
            ElseIf c = 4 Then
                cs.Append("<div class='progress-bar progress-bar-warning' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 5
            ElseIf c = 5 Then
                cs.Append("<div class='progress-bar progress-bar-success progress-bar-striped' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 6
            ElseIf c = 6 Then
                cs.Append("<div class='progress-bar progress-bar-danger progress-bar-striped' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 7
            ElseIf c = 7 Then
                cs.Append("<div class='progress-bar progress-bar-info progress-bar-striped' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 8
            ElseIf c = 8 Then
                cs.Append("<div class='progress-bar progress-bar-warning progress-bar-striped' role='progressbar' runat='server' style='width:" + CStr(porcentaje) + "%;'>")
                c = 1
            End If
            cs.Append("</div>")
            cs.Append("</div>")
            cs.Append("</div>")
            cs.Append("</div>")
        Next

        LiteralAyuda.Text = cs.ToString()
    End Sub

    Sub Listar_Usuarios()
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Usuarios(psconexion)
        GvCarteraMasiva.DataSource = dt
        GvCarteraMasiva.DataBind()
    End Sub

    Sub Listar_Clientes()
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Clientes(psconexion, "%", "%")
        TablaClientes.Style.Add("height", "500px")
        TablaClientes.Style.Add("width", "1000px")
        TablaClientes.Style.Add("overflow", "auto")
        TablaClientes.Style.Add("padding-left", "15px")
        TablaClientes.Style.Add("margin-left", "15px")
        GvListaClientes.DataSource = dt
        GvListaClientes.DataBind()

        TotalClientesL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalClientesL.Visible = True
        TotalClientesL.Visible = True

        Barra_Progreso()
    End Sub

    Sub Llenar_Combos_Cliente()
        Dim obj As New Cls_Cliente
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Llenar_Estado(psconexion)
        DdlEstadoCliente.DataSource = dt
        DdlEstadoCliente.DataValueField = "ELEMEN_CODIGO"
        DdlEstadoCliente.DataTextField = "ELEMEN_VALOR"
        DdlEstadoCliente.DataBind()
        DdlEstadoCliente.Items.Add("< Seleccionar >")
        DdlEstadoCliente.SelectedValue = "< Seleccionar >"

        DdlEstadoTiempoEstadoCliente.DataSource = dt
        DdlEstadoTiempoEstadoCliente.DataValueField = "ELEMEN_CODIGO"
        DdlEstadoTiempoEstadoCliente.DataTextField = "ELEMEN_VALOR"
        DdlEstadoTiempoEstadoCliente.DataBind()
        DdlEstadoTiempoEstadoCliente.Items.Add("< Seleccionar >")
        DdlEstadoTiempoEstadoCliente.SelectedValue = "< Seleccionar >"


        dt = obj.Llenar_Acciones(psconexion)
        DdlAccionesCliente.DataSource = dt
        DdlAccionesCliente.DataValueField = "ELEMEN_CODIGO"
        DdlAccionesCliente.DataTextField = "ELEMEN_VALOR"
        DdlAccionesCliente.DataBind()
        DdlAccionesCliente.Items.Add("< Seleccionar >")
        DdlAccionesCliente.SelectedValue = "< Seleccionar >"

        DdlAccionesTiempoEstadoCliente.DataSource = dt
        DdlAccionesTiempoEstadoCliente.DataValueField = "ELEMEN_CODIGO"
        DdlAccionesTiempoEstadoCliente.DataTextField = "ELEMEN_VALOR"
        DdlAccionesTiempoEstadoCliente.DataBind()
        DdlAccionesTiempoEstadoCliente.Items.Add("< Seleccionar >")
        DdlAccionesTiempoEstadoCliente.SelectedValue = "< Seleccionar >"
    End Sub

    Sub Ocultar_Mostrar_Clientes(ByVal vf As Boolean)
        LblCIFCliente.Visible = False
        TxtCIFCliente.Visible = False
        LblCodGPSCliente.Visible = False
        TxtCodGPSCliente.Visible = False
        LblAdquiraCliente.Visible = False
        TxtAdquiraCliente.Visible = False
        LblFechaCliente.Visible = False
        TxtFechaCliente.Visible = False
        LblNombreCliente.Visible = False
        TxtNombreCliente.Visible = False
        LblGMICliente.Visible = False
        TxtGMICliente.Visible = False
        LblTelefono2Cliente.Visible = False
        TxtTelefono2Cliente.Visible = False
        LblDireccionCliente.Visible = False
        TxtDireccionCliente.Visible = False
        LblTelefono3Cliente.Visible = False
        TxtTelefono3Cliente.Visible = False
        LblCiudadCliente.Visible = False
        TxtCiudadCliente.Visible = False
        LblProvinciaCliente.Visible = False
        TxtProvinciaCliente.Visible = False
        LblPaisCliente.Visible = False
        TxtPaisCliente.Visible = False
        LblCodPostalCliente.Visible = False
        TxtCodPostalCliente.Visible = False
        LblTelefonoEfectivoCliente.Visible = False
        TxtTelefonoEfectivoCliente.Visible = False
        LblOCCliente.Visible = False
        TxtOCCliente.Visible = False
        LblModoFacturacionCliente.Visible = False
        TxtModoFacturacionCliente.Visible = False
        LblGrupoCliente.Visible = False
        TxtGrupoCliente.Visible = False
        LblCargoContactoCliente.Visible = False
        TxtCargoContactoCliente.Visible = False
        LblModoHojaEntradaCliente.Visible = False
        TxtModoHojaEntradaCliente.Visible = False
        LblSociedadCliente.Visible = False
        TxtSociedadCliente.Visible = False
        LblEmailCliente.Visible = False
        TxtEmailCliente.Visible = False
        LblNombreNegociadorCliente.Visible = False
        TxtNombreNegociadorCliente.Visible = False
        LblExtranjeroCliente.Visible = False
        TxtExtranjeroCliente.Visible = False
        LblGrupoABCCliente.Visible = False
        TxtGrupoABCCliente.Visible = False
        LblTelefonoNCliente.Visible = False
        TxtTelefonoNCliente.Visible = False
        LblOkComprasCliente.Visible = False
        TxtOkComprasCliente.Visible = False
        LblAccionesCliente.Visible = False
        DdlAccionesCliente.Visible = False
        LblEstadoCliente.Visible = False
        DdlEstadoCliente.Visible = False
        If Session("AyudaCliente").ToString = "CambiarEstado" Then
            TituloAgregarCliente.Visible = vf
            LblEstadoCliente.Visible = vf
            DdlEstadoCliente.Visible = vf
            BtnGuardarCliente.Visible = vf
            BtnCancelarCliente.Visible = vf
            BtnAgregarCliente.Enabled = True
            TxtNombreCliente.Enabled = False
            TxtCIFCliente.Enabled = False
        ElseIf Session("AyudaCliente").ToString = "AplicarAcciones" Then
            TituloAgregarCliente.Visible = vf
            LblAccionesCliente.Visible = vf
            DdlAccionesCliente.Visible = vf
            BtnGuardarCliente.Visible = vf
            BtnCancelarCliente.Visible = vf
            BtnAgregarCliente.Enabled = True
            TxtNombreCliente.Enabled = False
            TxtCIFCliente.Enabled = False
        ElseIf Session("AyudaCliente").ToString = "Agregar" Then
            TituloAgregarCliente.Visible = vf
            LblCIFCliente.Visible = vf
            TxtCIFCliente.Visible = vf
            LblCodGPSCliente.Visible = vf
            TxtCodGPSCliente.Visible = vf
            LblAdquiraCliente.Visible = vf
            TxtAdquiraCliente.Visible = vf
            LblFechaCliente.Visible = vf
            TxtFechaCliente.Visible = vf
            LblNombreCliente.Visible = vf
            TxtNombreCliente.Visible = vf
            LblGMICliente.Visible = vf
            TxtGMICliente.Visible = vf
            LblTelefono2Cliente.Visible = vf
            TxtTelefono2Cliente.Visible = vf
            LblDireccionCliente.Visible = vf
            TxtDireccionCliente.Visible = vf
            LblTelefono3Cliente.Visible = vf
            TxtTelefono3Cliente.Visible = vf
            LblCiudadCliente.Visible = vf
            TxtCiudadCliente.Visible = vf
            LblProvinciaCliente.Visible = vf
            TxtProvinciaCliente.Visible = vf
            LblPaisCliente.Visible = vf
            TxtPaisCliente.Visible = vf
            LblCodPostalCliente.Visible = vf
            TxtCodPostalCliente.Visible = vf
            LblTelefonoEfectivoCliente.Visible = vf
            TxtTelefonoEfectivoCliente.Visible = vf
            LblOCCliente.Visible = vf
            TxtOCCliente.Visible = vf
            LblModoFacturacionCliente.Visible = vf
            TxtModoFacturacionCliente.Visible = vf
            LblGrupoCliente.Visible = vf
            TxtGrupoCliente.Visible = vf
            LblCargoContactoCliente.Visible = vf
            TxtCargoContactoCliente.Visible = vf
            LblModoHojaEntradaCliente.Visible = vf
            TxtModoHojaEntradaCliente.Visible = vf
            LblSociedadCliente.Visible = vf
            TxtSociedadCliente.Visible = vf
            LblEmailCliente.Visible = vf
            TxtEmailCliente.Visible = vf
            LblNombreNegociadorCliente.Visible = vf
            TxtNombreNegociadorCliente.Visible = vf
            LblExtranjeroCliente.Visible = vf
            TxtExtranjeroCliente.Visible = vf
            LblGrupoABCCliente.Visible = vf
            TxtGrupoABCCliente.Visible = vf
            LblTelefonoNCliente.Visible = vf
            TxtTelefonoNCliente.Visible = vf
            LblOkComprasCliente.Visible = vf
            TxtOkComprasCliente.Visible = vf
            BtnGuardarCliente.Visible = vf
            BtnCancelarCliente.Visible = vf
            TxtNombreCliente.Enabled = True
            TxtCIFCliente.Enabled = True
            If vf Then
                BtnAgregarCliente.Enabled = False
            Else
                BtnAgregarCliente.Enabled = True
            End If
        End If
    End Sub

    Sub Limpiar_Cajas_Agregar_Cliente()
        TxtCIFCliente.Text = ""
        TxtCodGPSCliente.Text = ""
        TxtCodGPSCliente.Text = ""
        TxtAdquiraCliente.Text = ""
        TxtFechaCliente.Value = ""
        TxtNombreCliente.Text = ""
        TxtGMICliente.Text = ""
        TxtTelefono2Cliente.Text = ""
        TxtDireccionCliente.Text = ""
        TxtTelefono3Cliente.Text = ""
        TxtCiudadCliente.Text = ""
        TxtProvinciaCliente.Text = ""
        TxtPaisCliente.Text = ""
        TxtCodPostalCliente.Text = ""
        TxtTelefonoEfectivoCliente.Text = ""
        TxtOCCliente.Text = ""
        TxtModoFacturacionCliente.Text = ""
        TxtGrupoCliente.Text = ""
        TxtCargoContactoCliente.Text = ""
        TxtModoHojaEntradaCliente.Text = ""
        TxtSociedadCliente.Text = ""
        TxtEmailCliente.Text = ""
        TxtNombreNegociadorCliente.Text = ""
        TxtExtranjeroCliente.Text = ""
        TxtGrupoABCCliente.Text = ""
        TxtTelefonoNCliente.Text = ""
        TxtOkComprasCliente.Text = ""
    End Sub

    Private Sub BtnBuscarCliente_Click(sender As Object, e As EventArgs) Handles BtnBuscarCliente.Click
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim ruc As String = TxtRucClienteBuscar.Text.ToString()
        Dim razSocial As String = TxtRazonSocialClienteBuscar.Text.ToString()
        dt = obj.Lista_Clientes(Session("Ruta_emp"), razSocial, ruc)
        TablaClientes.Style.Add("height", "500px")
        TablaClientes.Style.Add("width", "1000px")
        TablaClientes.Style.Add("overflow", "auto")
        TablaClientes.Style.Add("padding-left", "15px")
        TablaClientes.Style.Add("margin-left", "15px")
        GvListaClientes.DataSource = dt
        GvListaClientes.DataBind()

        TotalClientesL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalClientesL.Visible = True
        TotalClientesL.Visible = True

        Barra_Progreso()
    End Sub

    Private Sub BtnAgregarCliente_Click(sender As Object, e As EventArgs) Handles BtnAgregarCliente.Click
        Session("AyudaCliente") = "Agregar"
        Limpiar_Cajas_Agregar_Cliente()
        Ocultar_Mostrar_Clientes(True)
        TituloAgregarCliente.Text = "Agregar Cliente"
        BtnGuardarCliente.Text = "Guardar"
        TxtFechaCliente.Value = DateTime.Now.ToString("yyyy-MM-dd")
    End Sub

    Private Sub BtnCancelarCLIENTE_Click(sender As Object, e As EventArgs) Handles BtnCancelarCliente.Click
        Ocultar_Mostrar_Clientes(False)
        Limpiar_Cajas_Agregar_Cliente()
    End Sub

    Private Sub BtnListarCliente_Click(sender As Object, e As EventArgs) Handles BtnListarCliente.Click
        System.Threading.Thread.Sleep(1000)
        Listar_Clientes()
    End Sub

    Private Sub BtnGuardarCliente_Click(sender As Object, e As EventArgs) Handles BtnGuardarCliente.Click
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        '-----------------------------------------------------------------------'
        Dim codCliente As String = CodClienteAyuda.Text.Trim.ToString
        Dim codAsignado As String = CodAsignadoAyuda.Text.Trim.ToString
        Dim codPersona As String = CodPersonaAyuda.Text.Trim.ToString
        Dim rucCliente As String = RucClienteAyuda.Text.Trim.ToString
        Dim cifCliente As String = CifClienteAyuda.Text.Trim.ToString
        Dim razoCliente As String = RazSoClienteAyuda.Text.Trim.ToString
        Dim accion As String = DdlAccionesCliente.SelectedValue.ToString
        Dim estado As String = DdlEstadoCliente.SelectedValue.ToString
        '-----------------------------------------------------------------------'
        '-----------------------------------------------------------------------'
        Dim cif As String = TxtCIFCliente.Text.Trim.ToString
        Dim gps As String = TxtCodGPSCliente.Text.Trim.ToString
        Dim adquira As String = TxtAdquiraCliente.Text.Trim.ToString
        Dim fechaAdquira As String = TxtFechaCliente.Value.ToString
        Dim fecha() As String
        Dim nombre As String = TxtNombreCliente.Text.Trim.ToString
        Dim gmi As String = TxtGMICliente.Text.Trim.ToString
        Dim telf2 As String = TxtTelefono2Cliente.Text.Trim.ToString
        Dim direccion As String = TxtDireccionCliente.Text.Trim.ToString
        Dim telf3 As String = TxtTelefono3Cliente.Text.Trim.ToString
        Dim ciudad As String = TxtCiudadCliente.Text.Trim.ToString
        Dim provincia As String = TxtProvinciaCliente.Text.Trim.ToString
        Dim pais As String = TxtPaisCliente.Text.Trim.ToString
        Dim codPostal As String = TxtCodPostalCliente.Text.Trim.ToString
        Dim telfE As String = TxtTelefonoEfectivoCliente.Text.Trim.ToString
        Dim modoFacturacion As String = TxtModoFacturacionCliente.Text.Trim.ToString
        Dim grupo As String = TxtGrupoCliente.Text.Trim.ToString
        Dim oc As String = TxtOCCliente.Text.Trim.ToString
        Dim modoEntrada As String = TxtModoHojaEntradaCliente.Text.Trim.ToString
        Dim sociedad As String = TxtSociedadCliente.Text.Trim.ToString
        Dim cargoContacto As String = TxtCargoContactoCliente.Text.Trim.ToString
        Dim nomNegociador As String = TxtNombreNegociadorCliente.Text.Trim.ToString
        Dim emailNegociador As String = TxtEmailCliente.Text.Trim.ToString
        Dim telfNegociador As String = TxtTelefonoNCliente.Text.Trim.ToString
        Dim extranjero As String = TxtExtranjeroCliente.Text.Trim.ToString
        Dim grupoABC As String = TxtGrupoABCCliente.Text.Trim.ToString
        Dim okCompras As String = TxtOkComprasCliente.Text.Trim.ToString

        If BtnGuardarCliente.Text = "Guardar" Or BtnGuardarCliente.Text = "Actualizar" Then
            If nombre.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese el Nombre');", True)
            ElseIf cif.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese el CIF');", True)
            Else
                fecha = Regex.Split(fechaAdquira, "-")
                fechaAdquira = fecha(0) + fecha(1) + fecha(2)
                If BtnGuardarCliente.Text = "Guardar" Then
                    dt = obj.Registra_Cliente(psconexion, cif, gps, adquira, fechaAdquira, nombre, gmi, telf2, direccion, telf3, ciudad, provincia, pais, codPostal, telfE, modoFacturacion,
                                              grupo, oc, modoEntrada, sociedad, cargoContacto, nomNegociador, emailNegociador, telfNegociador, extranjero, grupoABC, okCompras)
                    Dim dbRow As DataRow = dt.Rows(0)
                    If dbRow(0).ToString = "2" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El CIF del Cliente ya existe');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresado correctamente');", True)
                        Ocultar_Mostrar_Clientes(False)
                        Listar_Clientes()
                    End If
                ElseIf BtnGuardarCliente.Text = "Actualizar" Then
                    dt = obj.Actualizar_Cliente(psconexion, codCliente, cif, gps, adquira, fechaAdquira, nombre, gmi, telf2, direccion, telf3, ciudad, provincia, pais, codPostal, telfE, modoFacturacion,
                                              grupo, oc, modoEntrada, sociedad, cargoContacto, nomNegociador, emailNegociador, telfNegociador, extranjero, grupoABC, okCompras)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Actualizado correctamente');", True)
                    Ocultar_Mostrar_Clientes(False)
                    Listar_Clientes()
                End If
            End If
        ElseIf BtnGuardarCliente.Text = "Cambiar Estado" Then
            If estado = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione el Estado');", True)
            Else
                obj.Cambiar_Estado_Cliente(psconexion, codCliente, estado, FechaActual().ToString(), HoraActual().ToString(), Session("User"), "")
                Ocultar_Mostrar_Clientes(False)
                Listar_Clientes()
            End If
        ElseIf BtnGuardarCliente.Text = "Aplicar" Then
            If accion = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione la Acción');", True)
            ElseIf accion = "1" Then
                Response.Redirect("~/Contabilidad/Contabilidad_Personas.aspx?WpkDi=" + codPersona + "&KllPd0s=" + rucCliente + "&Ni830dHuciPLO=" + cifCliente +
                                  "&093008roijfoiJ_sfF=" + razoCliente + "&Lpoeh58FJIJS0lk=" + codCliente + "&KJoi09jdJA90dIW=")
            ElseIf accion = "15" Or accion = "7" Then
                If codPersona = "" Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Debe actualizar datos del Cliente');", True)
                Else
                    If accion = "15" Then
                        Response.Redirect("~/CallCenter/CallCenter_Pantalla_Operador.aspx?WpkDi=" + "LLE" + "&Lpoeh58FJIJS0lk=" + codPersona + "&Ni830dHuciPLO=" + codCliente)
                    ElseIf accion = "7" Then
                        Response.Redirect("~/CallCenter/CallCenter_Pantalla_Operador.aspx?WpkDi=" + "LLS" + "&Lpoeh58FJIJS0lk=" + codPersona + "&Ni830dHuciPLO=" + codCliente)
                    End If
                End If
            ElseIf accion = "6" Then
                If codAsignado = "" Then
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Debe Asignar un Personal');", True)
                Else
                    Dim obj1 As New Cls_Agenda_Citas
                    Dim dt1 As New DataTable
                    dt1 = obj1.Buscar_Personal(Session("Ruta_emp"), codAsignado)
                    Dim dbRow As DataRow = dt1.Rows(0)
                    If dbRow(0).ToString() = "NO" Then
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('El Personal Asignado no tiene Horarios para Citas');", True)
                    Else
                        Response.Redirect("~/PersonalAgenda/Agenda_Citas.aspx?8JAsd0hfiuF=" + codAsignado + "&Ni830dHuciPLO=" + dbRow(0).ToString())
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub GvListaClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaClientes.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim codCliente As String = GvListaClientes.Rows(Index).Cells(16).Text.ToString
        '-------------------------------------------------------------------------------------------------------'
        Dim codPersona As String = Replace(GvListaClientes.Rows(Index).Cells(46).Text.ToString, "&nbsp;", "")
        Dim rucC As String = Replace(GvListaClientes.Rows(Index).Cells(6).Text.ToString, "&nbsp;", "")
        Dim cifC As String = Replace(GvListaClientes.Rows(Index).Cells(6).Text.ToString, "&nbsp;", "")
        Dim razoC As String = Replace(GvListaClientes.Rows(Index).Cells(7).Text.ToString, "&nbsp;", "")
        Dim codAsignado As String = Replace(GvListaClientes.Rows(Index).Cells(19).Text.ToString, "&nbsp;", "")
        Dim estado As String = GvListaClientes.Rows(Index).Cells(17).Text.ToString
        '-------------------------------------------------------------------------------------------------------'
        Dim gps As String = Replace(GvListaClientes.Rows(Index).Cells(8).Text.ToString, "&nbsp;", "")
        Dim grupo As String = Replace(GvListaClientes.Rows(Index).Cells(9).Text.ToString, "&nbsp;", "")
        Dim sociedad As String = Replace(GvListaClientes.Rows(Index).Cells(10).Text.ToString, "&nbsp;", "")
        Dim gmi As String = Replace(GvListaClientes.Rows(Index).Cells(11).Text.ToString, "&nbsp;", "")
        Dim entrada As String = Replace(GvListaClientes.Rows(Index).Cells(12).Text.ToString, "&nbsp;", "")
        Dim facturacion As String = Replace(GvListaClientes.Rows(Index).Cells(13).Text.ToString, "&nbsp;", "")
        Dim adquira As String = Replace(GvListaClientes.Rows(Index).Cells(14).Text.ToString, "&nbsp;", "")
        Dim fechaAdquira As String = Replace(GvListaClientes.Rows(Index).Cells(15).Text.ToString, "&nbsp;", "")
        Dim telefono2 As String = Replace(GvListaClientes.Rows(Index).Cells(21).Text.ToString, "&nbsp;", "")
        Dim telefono3 As String = Replace(GvListaClientes.Rows(Index).Cells(22).Text.ToString, "&nbsp;", "")
        Dim telefonoE As String = Replace(GvListaClientes.Rows(Index).Cells(23).Text.ToString, "&nbsp;", "")
        Dim direccion As String = Replace(GvListaClientes.Rows(Index).Cells(24).Text.ToString, "&nbsp;", "")
        Dim ciudad As String = Replace(GvListaClientes.Rows(Index).Cells(25).Text.ToString, "&nbsp;", "")
        Dim provincia As String = Replace(GvListaClientes.Rows(Index).Cells(26).Text.ToString, "&nbsp;", "")
        Dim codPostal As String = Replace(GvListaClientes.Rows(Index).Cells(27).Text.ToString, "&nbsp;", "")
        Dim pais As String = Replace(GvListaClientes.Rows(Index).Cells(28).Text.ToString, "&nbsp;", "")
        Dim oc As String = Replace(GvListaClientes.Rows(Index).Cells(29).Text.ToString, "&nbsp;", "")
        Dim cargo As String = Replace(GvListaClientes.Rows(Index).Cells(30).Text.ToString, "&nbsp;", "")
        Dim extranjero As String = Replace(GvListaClientes.Rows(Index).Cells(31).Text.ToString, "&nbsp;", "")
        Dim grupoABC As String = Replace(GvListaClientes.Rows(Index).Cells(32).Text.ToString, "&nbsp;", "")
        Dim nomNegociador As String = Replace(GvListaClientes.Rows(Index).Cells(33).Text.ToString, "&nbsp;", "")
        Dim emailNegociador As String = Replace(GvListaClientes.Rows(Index).Cells(34).Text.ToString, "&nbsp;", "")
        Dim telfNegociador As String = Replace(GvListaClientes.Rows(Index).Cells(35).Text.ToString, "&nbsp;", "")
        Dim okCompras As String = Replace(GvListaClientes.Rows(Index).Cells(36).Text.ToString, "&nbsp;", "")
        Dim psconexion As String = Session("Ruta_Emp")

        If e.CommandName = "EliminarCliente" Then
            dt = obj.Elimina_Cliente(psconexion, codCliente)
            Dim dbRow As DataRow = dt.Rows(0)
            If dbRow(0).ToString = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar Cliente');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Eliminado correctamente');", True)
            End If
            Listar_Clientes()
        ElseIf e.CommandName = "TrackingCliente" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTracking').modal('show');", True)
            dt = obj.Lista_Tracking_Clientes(psconexion, codCliente)
            GvTrackingEstados.DataSource = dt
            GvTrackingEstados.DataBind()
            dt = obj.Lista_Tracking_Acciones_Clientes(psconexion, codCliente)
            GvTrackingAcciones.DataSource = dt
            GvTrackingAcciones.DataBind()
        ElseIf e.CommandName = "CambiarEstadoCliente" Then
            Session("AyudaCliente") = "CambiarEstado"
            TituloAgregarCliente.Text = "Actualizar Estado"
            BtnGuardarCliente.Text = "Cambiar Estado"
            Ocultar_Mostrar_Clientes(True)
            TxtCIFCliente.Text = cifC
            LblCIFCliente.Visible = True
            TxtCIFCliente.Visible = True
            TxtNombreCliente.Text = razoC
            LblNombreCliente.Visible = True
            TxtNombreCliente.Visible = True
            CodClienteAyuda.Text = codCliente.ToString()
            DdlEstadoCliente.SelectedValue = estado
        ElseIf e.CommandName = "EditarCliente" Then
            Session("AyudaCliente") = "Agregar"
            Limpiar_Cajas_Agregar_Cliente()
            Ocultar_Mostrar_Clientes(True)
            TxtCIFCliente.Text = cifC
            TxtNombreCliente.Text = razoC
            TxtCodGPSCliente.Text = gps
            TxtGrupoCliente.Text = grupo
            TxtSociedadCliente.Text = sociedad
            TxtGMICliente.Text = gmi
            TxtModoHojaEntradaCliente.Text = entrada
            TxtModoFacturacionCliente.Text = facturacion
            TxtAdquiraCliente.Text = adquira
            TxtFechaCliente.Value = fechaAdquira
            TxtTelefono2Cliente.Text = telefono2
            TxtTelefono3Cliente.Text = telefono3
            TxtTelefonoEfectivoCliente.Text = telefonoE
            TxtDireccionCliente.Text = direccion
            TxtCiudadCliente.Text = ciudad
            TxtProvinciaCliente.Text = provincia
            TxtCodPostalCliente.Text = codPostal
            TxtPaisCliente.Text = pais
            TxtOCCliente.Text = oc
            TxtCargoContactoCliente.Text = cargo
            TxtExtranjeroCliente.Text = extranjero
            TxtGrupoABCCliente.Text = grupoABC
            TxtNombreNegociadorCliente.Text = nomNegociador
            TxtEmailCliente.Text = emailNegociador
            TxtTelefonoNCliente.Text = telfNegociador
            TxtOkComprasCliente.Text = okCompras
            CodClienteAyuda.Text = codCliente
            TituloAgregarCliente.Text = "Editar Cliente"
            BtnGuardarCliente.Text = "Actualizar"
        ElseIf e.CommandName = "AplicarAccionesCliente" Then
            Session("AyudaCliente") = "AplicarAcciones"
            TituloAgregarCliente.Text = "Aplicar Acciones"
            BtnGuardarCliente.Text = "Aplicar"
            Ocultar_Mostrar_Clientes(True)
            TxtCIFCliente.Text = cifC
            LblCIFCliente.Visible = True
            TxtCIFCliente.Visible = True
            TxtNombreCliente.Text = razoC
            LblNombreCliente.Visible = True
            TxtNombreCliente.Visible = True
            CodPersonaAyuda.Text = codPersona.ToString()
            CodClienteAyuda.Text = codCliente.ToString()
            CodAsignadoAyuda.Text = codAsignado.ToString()
            RucClienteAyuda.Text = rucC.ToString()
            CifClienteAyuda.Text = cifC.ToString()
            RazSoClienteAyuda.Text = razoC.ToString()
            dt = obj.Llenar_Acciones_Cliente(psconexion, codCliente)
            DdlAccionesCliente.Items.Clear()
            DdlAccionesCliente.DataSource = dt
            DdlAccionesCliente.DataValueField = "ELEMEN_CODIGO"
            DdlAccionesCliente.DataTextField = "ELEMEN_VALOR"
            DdlAccionesCliente.DataBind()
            DdlAccionesCliente.Items.Add("< Seleccionar >")
            DdlAccionesCliente.SelectedValue = "< Seleccionar >"
        End If
    End Sub

    Private Sub BtnCerrarTracking_Click(sender As Object, e As EventArgs) Handles BtnCerrarTracking.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTracking').modal('hide');", True)
    End Sub

    Private Sub BtnAsignarCarteraMasivaCliente_Click(sender As Object, e As EventArgs) Handles BtnAsignarCarteraMasivaCliente.Click
        Listar_Usuarios()

        Dim check As CheckBox
        For i = 0 To GvListaClientes.Rows.Count - 1
            check = CType(GvListaClientes.Rows(i).Cells(0).FindControl("Check"), CheckBox)
            If check.Checked = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCarteraMasiva').modal('show');", True)
                Exit For
            End If
            If i = GvListaClientes.Rows.Count - 1 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar a un Cliente como mínimo');", True)
            End If
        Next
    End Sub

    Private Sub GvCarteraMasiva_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvCarteraMasiva.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codCliente As String = ""
        Dim codAsignado As String = GvCarteraMasiva.Rows(Index).Cells(1).Text.ToString
        Dim fecha As String = FechaActual()
        Dim horaActual As DateTime = TimeOfDay
        Dim hora As String = ""
        If CInt(horaActual.Hour.ToString) < 10 Then hora = "0" + horaActual.Hour.ToString
        If CInt(horaActual.Minute.ToString) < 10 Then hora += "0" + horaActual.Minute.ToString
        If CInt(horaActual.Hour.ToString) >= 10 Then hora = horaActual.Hour.ToString
        If CInt(horaActual.Minute.ToString) >= 10 Then hora += horaActual.Minute.ToString

        Dim check As CheckBox
        If e.CommandName = "Aceptar" Then
            For i = 0 To GvListaClientes.Rows.Count - 1
                check = CType(GvListaClientes.Rows(i).Cells(0).FindControl("Check"), CheckBox)
                If check.Checked = True Then
                    codCliente = GvListaClientes.Rows(i).Cells(16).Text.ToString
                    obj.Cambiar_Estado_Cliente(psconexion, codCliente, "3", fecha, hora, Session("User"), codAsignado)
                End If
            Next

            For i = 0 To GvListaClientes.Rows.Count - 1
                check = CType(GvListaClientes.Rows(i).Cells(0).FindControl("Check"), CheckBox)
                check.Checked = False
            Next

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCarteraMasiva').modal('hide');", True)
            Listar_Clientes()
        End If
    End Sub

    Private Sub BtnCerrarCarteraMasiva_Click(sender As Object, e As EventArgs) Handles BtnCerrarCarteraMasiva.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCarteraMasiva').modal('hide');", True)
    End Sub

    Private Sub BtnDeseleccionarCliente_Click(sender As Object, e As EventArgs) Handles BtnDeseleccionarCliente.Click
        Dim check As CheckBox
        Dim c As Integer = 0
        For i = 0 To GvListaClientes.Rows.Count - 1
            check = CType(GvListaClientes.Rows(i).Cells(0).FindControl("Check"), CheckBox)
            If check.Checked = True Then
                check.Checked = False
                c = 1
            End If
        Next

        If c = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Los Clientes están sin seleccionar');", True)
        End If
    End Sub

    Private Sub BtnExportarExcelCliente_Click(sender As Object, e As EventArgs) Handles BtnExportarExcelCliente.Click
        If GvListaClientes.Rows.Count() <= 0 Or GvListaClientes.Rows.Count = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay lista de Clientes');", True)
        Else
            Exportar_Excel_Cliente()
        End If
    End Sub

    Private Sub Exportar_Excel_Cliente()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Dim tablaAyuda As New DataTable
        Dim grid As New GridView

        For index = 6 To GvListaClientes.Columns.Count - 1
            Dim dc As New DataColumn(GvListaClientes.Columns(index).HeaderText, Type.GetType("System.String"))
            tablaAyuda.Columns.Add(dc)
        Next

        For index = 0 To GvListaClientes.Rows.Count - 1
            Dim drG As DataRow = tablaAyuda.NewRow
            For index1 = 6 To GvListaClientes.Columns.Count - 1
                drG.Item(GvListaClientes.Columns(index1).HeaderText) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaClientes.Rows(index).Cells(index1).Text.Trim.ToString, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Next
            tablaAyuda.Rows.Add(drG)
        Next

        grid.DataSource = tablaAyuda
        grid.DataBind()

        grid.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grid)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=LISTADO_DE_CLIENTES.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    '
    '
    'CONTACTOS DEL CLIENTE
    '
    '
    Private Sub BtnAgregarContacto_Click(sender As Object, e As EventArgs) Handles BtnAgregarContacto.Click
        LblTituloAgregarContacto.Visible = True
        LblTituloAgregarContacto.Text = "Agregar Contacto"
        LblCliente.Visible = True
        txtCIFClienteMOD.Visible = True
        BtnBuscaCliente.Visible = True
        TxtRZClienteMOD.Visible = True
        LblApePaterno.Visible = True
        TxtApePaterno.Visible = True
        LblApeMaterno.Visible = True
        TxtApeMaterno.Visible = True
        LblNombres.Visible = True
        txtNombres.Visible = True
        LblTelefono.Visible = True
        TxtTelefono.Visible = True
        LblCelular.Visible = True
        TxtCelular.Visible = True
        LblEmail.Visible = True
        TxtEmail.Visible = True
        BtnGuardarContacto.Visible = True
        BtnCancelarContacto.Visible = True
        BtnGuardarContacto.Text = "Guardar"
        Limpiar_Cajas_Agregar_Contactos()
    End Sub
    Private Sub BtnCancelarContacto_Click(sender As Object, e As EventArgs) Handles BtnCancelarContacto.Click
        LblTituloAgregarContacto.Visible = False
        LblCliente.Visible = False
        txtCIFClienteMOD.Visible = False
        BtnBuscaCliente.Visible = False
        TxtRZClienteMOD.Visible = False
        LblApePaterno.Visible = False
        TxtApePaterno.Visible = False
        LblApeMaterno.Visible = False
        TxtApeMaterno.Visible = False
        LblNombres.Visible = False
        txtNombres.Visible = False
        LblTelefono.Visible = False
        TxtTelefono.Visible = False
        LblCelular.Visible = False
        TxtCelular.Visible = False
        LblEmail.Visible = False
        TxtEmail.Visible = False
        BtnGuardarContacto.Visible = False
        BtnCancelarContacto.Visible = False
        Limpiar_Cajas_Agregar_Contactos()
    End Sub
    Protected Sub Limpiar_Cajas_Agregar_Contactos()
        txtCIFClienteMOD.Text = ""
        TxtRZClienteMOD.Text = ""
        TxtApePaterno.Text = ""
        TxtApeMaterno.Text = ""
        txtNombres.Text = ""
        TxtTelefono.Text = ""
        TxtCelular.Text = ""
        TxtEmail.Text = ""
    End Sub


    Protected Sub Lista_Contactos_Clientes()
        Dim obj As New Cls_Contactos_Cliente
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codCliente As String = "%"
        Dim Contacto As String = "%"
        If TxtCliente.Text.ToString <> "" Then
            codCliente = TxtCliente.Text.ToString
        End If
        If TxtApPaterno.Text.ToString <> "" Then
            Contacto = TxtApPaterno.Text.ToString
        End If
        dt = obj.Lista_Contactos_Clientes(psconexion, codCliente, Contacto)
        TablaContactosClientes.Style.Add("height", "500px")
        TablaContactosClientes.Style.Add("width", "1000px")
        TablaContactosClientes.Style.Add("overflow", "auto")
        TablaContactosClientes.Style.Add("padding-left", "18px")
        TablaContactosClientes.Style.Add("margin-left", "18px")
        GvListaContactosClientes.DataSource = dt
        GvListaContactosClientes.DataBind()

        LblTotalContactosClientesL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalContactosClientes.Visible = True
        LblTotalContactosClientesL.Visible = True
    End Sub
    Protected Sub Lista_Cliente()
        Dim obj As New Cls_Contactos_Cliente
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim RazonSocial As String = "%"
        Dim CIF As String = "%"
        If TxtCIF.Value.ToString <> "" Then
            CIF = TxtCIF.Value.ToString
        End If
        If TxtRazonSocial.Value.ToString <> "" Then
            RazonSocial = TxtRazonSocial.Value.ToString
        End If
        dt = obj.Lista_Clientes(psconexion, RazonSocial, CIF)
        GvBuscarClienteModal.DataSource = dt
        GvBuscarClienteModal.DataBind()

        LblTotalClientesMML.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalClientesMM.Visible = True
        LblTotalClientesMML.Visible = True
    End Sub
    Private Sub BtnListarContacto_Click(sender As Object, e As EventArgs) Handles BtnListarContacto.Click
        Lista_Contactos_Clientes()
    End Sub
    Private Sub BtnListarClienteModal_Click(sender As Object, e As EventArgs) Handles BtnListarClienteModal.Click
        Lista_Cliente()
    End Sub
    Private Sub BtnBuscaCliente_Click(sender As Object, e As EventArgs) Handles BtnBuscaCliente.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaCliente').modal('show');", True)
        Limpiar_Cajas_Buscar_Cliente()
    End Sub

    Private Sub BtnCerrarClienteModal_Click(sender As Object, e As EventArgs) Handles BtnCerrarClienteModal.Click
        Limpiar_Cajas_Buscar_Cliente()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaCliente').modal('hide');", True)
    End Sub

    Private Sub GvBuscarClienteModal_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarClienteModal.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtCIFClienteMOD.Text = GvBuscarClienteModal.Rows(Index).Cells(1).Text.ToString
            TxtRZClienteMOD.Text = GvBuscarClienteModal.Rows(Index).Cells(2).Text.ToString
            LblCodCLIENTE.Text = GvBuscarClienteModal.Rows(Index).Cells(3).Text.ToString
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaCliente').modal('hide');", True)

        End If
    End Sub
    Protected Sub Limpiar_Cajas_Buscar_Cliente()
        TxtRazonSocial.Value = ""
        TxtCIF.Value = ""
        GvBuscarClienteModal.DataSource = Nothing
        GvBuscarClienteModal.DataBind()
    End Sub
    Private Sub BtnGuardarContacto_Click(sender As Object, e As EventArgs) Handles BtnGuardarContacto.Click
        Dim obj As New Cls_Contactos_Cliente
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim CodCliente As String = LblCodCLIENTE.Text.ToString
        Dim ApePaterno As String = TxtApePaterno.Text.ToString
        Dim ApeMaterno As String = TxtApeMaterno.Text.ToString
        Dim Nombres As String = txtNombres.Text.ToString
        Dim Telefono As String = TxtTelefono.Text.ToString
        Dim Celular As String = TxtCelular.Text.ToString
        Dim Email As String = TxtEmail.Text.ToString
        Dim Codigo As String = LblCodContacto.Text.ToString
        Try
            If CodCliente.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Cliente');", True)
            ElseIf ApePaterno.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Apellido Paterno');", True)
            ElseIf Nombres.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese un Nombre');", True)
            ElseIf Email.Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese un Email');", True)
            Else
                If Telefono <> "" Then
                    Telefono = Convert.ToInt32(Telefono)
                End If
                If Celular <> "" Then
                    Celular = Convert.ToInt32(Celular)
                End If
                If BtnGuardarContacto.Text = "Guardar" Then
                    obj.Ingresar_Contactos(psconexion, CodCliente, UCase(ApePaterno), UCase(ApeMaterno), UCase(Nombres), Telefono, Celular, Email)
                ElseIf BtnGuardarContacto.Text = "Actualizar" Then
                    obj.Actualizar_Contactos(psconexion, CodCliente, UCase(ApePaterno), UCase(ApeMaterno), UCase(Nombres), Telefono, Celular, Email, Codigo)
                End If
                Lista_Contactos_Clientes()
                Limpiar_Cajas_Agregar_Contactos()
            End If
        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Teléfono y Celular debe ser en números');", True)
        End Try
    End Sub

    Private Sub GvListaContactosClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaContactosClientes.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Actualizar" Then
            LblCodContacto.Text = GvListaContactosClientes.Rows(Index).Cells(1).Text.ToString
            txtCIFClienteMOD.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(2).Text.ToString, "&nbsp;", "")
            TxtRZClienteMOD.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(3).Text.ToString, "&nbsp;", "")
            TxtApePaterno.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(4).Text.ToString, "&nbsp;", "")
            TxtApeMaterno.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(5).Text.ToString, "&nbsp;", "")
            txtNombres.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(6).Text.ToString, "&nbsp;", "")
            TxtTelefono.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(7).Text.ToString, "&nbsp;", "")
            TxtCelular.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(8).Text.ToString, "&nbsp;", "")
            TxtEmail.Text = Replace(GvListaContactosClientes.Rows(Index).Cells(9).Text.ToString, "&nbsp;", "")
            LblCodCLIENTE.Text = GvListaContactosClientes.Rows(Index).Cells(10).Text.ToString
            LblTituloAgregarContacto.Visible = True
            LblTituloAgregarContacto.Text = "Editar Contacto"
            LblCliente.Visible = True
            txtCIFClienteMOD.Visible = True
            BtnBuscaCliente.Visible = True
            TxtRZClienteMOD.Visible = True
            LblApePaterno.Visible = True
            TxtApePaterno.Visible = True
            LblApeMaterno.Visible = True
            TxtApeMaterno.Visible = True
            LblNombres.Visible = True
            txtNombres.Visible = True
            LblTelefono.Visible = True
            TxtTelefono.Visible = True
            LblCelular.Visible = True
            TxtCelular.Visible = True
            LblEmail.Visible = True
            TxtEmail.Visible = True
            BtnGuardarContacto.Visible = True
            BtnCancelarContacto.Visible = True
            BtnGuardarContacto.Text = "Actualizar"
        End If
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Dim tablaAyuda As New DataTable
        Dim grid As New GridView

        For index = 2 To GvListaContactosClientes.Columns.Count - 2
            Dim dc As New DataColumn(GvListaContactosClientes.Columns(index).HeaderText, Type.GetType("System.String"))
            tablaAyuda.Columns.Add(dc)
        Next

        For index = 0 To GvListaContactosClientes.Rows.Count - 1
            Dim drG As DataRow = tablaAyuda.NewRow
            For index1 = 2 To GvListaContactosClientes.Columns.Count - 2
                drG.Item(GvListaContactosClientes.Columns(index1).HeaderText) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaContactosClientes.Rows(index).Cells(index1).Text.Trim.ToString, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Next
            tablaAyuda.Rows.Add(drG)
        Next

        grid.DataSource = tablaAyuda
        grid.DataBind()

        grid.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(grid)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Lista_de_Contactos_Clientes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Private Sub BtnExportarExcel_Click(sender As Object, e As EventArgs) Handles BtnExportarExcel.Click
        If GvListaContactosClientes.Rows.Count() <= 0 Or GvListaContactosClientes.Rows.Count = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay lista de Contactos - Clientes');", True)
        Else
            Exportar_Excel()
        End If
    End Sub

    '
    '
    'PROCESO PETICIÓN
    '
    '
    Protected Sub Lista_Proceso_Peticion()
        Dim obj As New Cls_Proceso_Peticion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Proceso_Peticion(psconexion)
        GvListaProcesoPeticion.DataSource = dt
        GvListaProcesoPeticion.DataBind()

        LblTotalPeticionL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalPeticion.Visible = True
        LblTotalPeticionL.Visible = True
    End Sub

    Private Sub BtnListarProcesos_Click(sender As Object, e As EventArgs) Handles BtnListarProcesos.Click
        Lista_Proceso_Peticion()
    End Sub

    Private Sub BtnAgregarRelacionPP_Click(sender As Object, e As EventArgs) Handles BtnAgregarRelacionPP.Click
        LblTituloAgregarRelacion.Visible = True
        LblTipoProcesoRELACION.Visible = True
        DdlTipoProcesoRELACION.Visible = True
        LblTipoPeticionRELACION.Visible = True
        DdlTipoPeticionRELACION.Visible = True
        BtnGuardarAgregarRelacionPP.Visible = True
        BtnCancelarAgregarRelacionPP.Visible = True
    End Sub

    Private Sub BtnCancelarAgregarRelacionPP_Click(sender As Object, e As EventArgs) Handles BtnCancelarAgregarRelacionPP.Click
        LblTituloAgregarRelacion.Visible = False
        LblTipoProcesoRELACION.Visible = False
        DdlTipoProcesoRELACION.Visible = False
        LblTipoPeticionRELACION.Visible = False
        DdlTipoPeticionRELACION.Visible = False
        BtnGuardarAgregarRelacionPP.Visible = False
        BtnCancelarAgregarRelacionPP.Visible = False
    End Sub
    Protected Sub Llenar_Combo_Peticion()
        Dim obj As New Cls_Proceso_Peticion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Peticion(psconexion)
        DdlTipoPeticionRELACION.DataSource = dt
        DdlTipoPeticionRELACION.DataValueField = "NIVEL1_CODIGO"
        DdlTipoPeticionRELACION.DataTextField = "PETICION"
        DdlTipoPeticionRELACION.DataBind()
        DdlTipoPeticionRELACION.Items.Add("< Seleccionar >")
        DdlTipoPeticionRELACION.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub BtnGuardarAgregarRelacionPP_Click(sender As Object, e As EventArgs) Handles BtnGuardarAgregarRelacionPP.Click
        Dim obj As New Cls_Proceso_Peticion
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim TipoPeticion As String = DdlTipoPeticionRELACION.SelectedValue.ToString
        Dim TipoProceso As String = DdlTipoProcesoRELACION.SelectedValue.ToString
        Dim dt As DataTable

        If TipoPeticion.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un Tipo de Petición');", True)
        ElseIf TipoProceso.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Tipo de Proceso');", True)
        Else
            dt = obj.Insertar_Proceso_Peticion(psconexion, TipoPeticion, TipoProceso)
            Dim dvRow As DataRow = dt.Rows(0)
            If dvRow(0) = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe relación en la tabla');", True)
            End If
            Lista_Proceso_Peticion()
        End If
    End Sub
    Private Sub GvListaProcesoPeticion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaProcesoPeticion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Proceso_Peticion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim TipoPeticion As String = GvListaProcesoPeticion.Rows(Index).Cells(1).Text
        Dim TipoProceso As String = GvListaProcesoPeticion.Rows(Index).Cells(4).Text
        Dim dt As New DataTable
        If e.CommandName = "QuitarRelacion" Then
            dt = obj.Eliminar_Relacion_Proceso_Peticion(psconexion, TipoPeticion, TipoProceso)
            Lista_Proceso_Peticion()
        End If
    End Sub
    '
    '
    'PROCESO ESTADO
    '
    '
    Protected Sub Lista_Proceso_Estado()
        Dim obj As New Cls_Proceso_Estado
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Proceso_Estado(psconexion)
        GvListaProcesoEstado.DataSource = dt
        GvListaProcesoEstado.DataBind()

        LblTotalEstadoL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalEstado.Visible = True
        LblTotalEstadoL.Visible = True
    End Sub
    Private Sub BtnListarEstados_Click(sender As Object, e As EventArgs) Handles BtnListarEstados.Click
        Lista_Proceso_Estado()
    End Sub
    Private Sub BtnAgregarRelacionPE_Click(sender As Object, e As EventArgs) Handles BtnAgregarRelacionPE.Click
        LblTituloAgregarRelacionPE.Visible = True
        LblTipoProcesoESTADO.Visible = True
        DdlTipoProcesoESTADO.Visible = True
        LblEstadoPROCESOESTADO.Visible = True
        DdlEstadoPROCESOESTADO.Visible = True
        BtnGuardarAgregarRelacionPE.Visible = True
        BtnCancelarAgregarRelacionPE.Visible = True
    End Sub

    Private Sub BtnCancelarAgregarRelacionPE_Click(sender As Object, e As EventArgs) Handles BtnCancelarAgregarRelacionPE.Click
        LblTituloAgregarRelacionPE.Visible = False
        LblTipoProcesoESTADO.Visible = False
        DdlTipoProcesoESTADO.Visible = False
        LblEstadoPROCESOESTADO.Visible = False
        DdlEstadoPROCESOESTADO.Visible = False
        BtnGuardarAgregarRelacionPE.Visible = False
        BtnCancelarAgregarRelacionPE.Visible = False
    End Sub

    Protected Sub Llenar_Combo_Estado_Procesos()
        Dim obj As New Cls_Proceso_Estado
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Estado_Procesos(psconexion)
        DdlEstadoPROCESOESTADO.DataSource = dt
        DdlEstadoPROCESOESTADO.DataValueField = "ELEMEN_CODIGO"
        DdlEstadoPROCESOESTADO.DataTextField = "ELEMEN_VALOR"
        DdlEstadoPROCESOESTADO.DataBind()
        DdlEstadoPROCESOESTADO.Items.Add("< Seleccionar >")
        DdlEstadoPROCESOESTADO.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Accion()
        Dim obj As New Cls_Proceso_Estado
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Accion(psconexion)
        DdlAccionMAA.DataSource = dt
        DdlAccionMAA.DataValueField = "ELEMEN_CODIGO"
        DdlAccionMAA.DataTextField = "ELEMEN_VALOR"
        DdlAccionMAA.DataBind()
        DdlAccionMAA.Items.Add("< Seleccionar >")
        DdlAccionMAA.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub BtnGuardarAgregarRelacionPE_Click(sender As Object, e As EventArgs) Handles BtnGuardarAgregarRelacionPE.Click
        Dim obj As New Cls_Proceso_Estado
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstadoCodigo As String = DdlEstadoPROCESOESTADO.SelectedValue.ToString
        Dim ProcesoCodigo As String = DdlTipoProcesoESTADO.SelectedValue.ToString
        Dim dt As DataTable

        If ProcesoCodigo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un Tipo de Proceso');", True)
        ElseIf EstadoCodigo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Estado');", True)
        Else
            dt = obj.Insertar_Proceso_Estado(psconexion, EstadoCodigo, ProcesoCodigo)
            Dim dvRow As DataRow = dt.Rows(0)
            If dvRow(0) = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe relación en la tabla');", True)
            End If
            Lista_Proceso_Estado()
        End If
    End Sub
    Private Sub GvListaProcesoEstado_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaProcesoEstado.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Proceso_Estado
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstadoCodigo As String = GvListaProcesoEstado.Rows(Index).Cells(2).Text
        Dim ProcesoCodigo As String = GvListaProcesoEstado.Rows(Index).Cells(6).Text
        Dim dt As New DataTable
        If e.CommandName = "AsignarAcciones" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
            TxtProcesoMAA.Text = GvListaProcesoEstado.Rows(Index).Cells(3).Text
            TxtEstadoMAA.Text = GvListaProcesoEstado.Rows(Index).Cells(4).Text
            LblCodESTADO.Text = GvListaProcesoEstado.Rows(Index).Cells(2).Text
            LblCodPROCESO.Text = GvListaProcesoEstado.Rows(Index).Cells(6).Text
        ElseIf e.CommandName = "QuitarRelacion" Then
            dt = obj.Eliminar_Relacion(psconexion, EstadoCodigo, ProcesoCodigo)
            Lista_Proceso_Estado()
        End If
    End Sub

    Private Sub BtnCancelarAccion_Click(sender As Object, e As EventArgs) Handles BtnCancelarAccion.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('hide');", True)
    End Sub
    Private Sub BtnGuardarAccion_Click(sender As Object, e As EventArgs) Handles BtnGuardarAccion.Click
        Dim obj As New Cls_Proceso_Estado
        Dim objCn As New Cls_Proceso_Estado
        Dim psconexion As String = Session("Ruta_Emp")
        Dim TicketEstado As String = LblCodESTADO.Text.ToString
        Dim TicketProceso As String = LblCodPROCESO.Text.ToString
        Dim TicketAccion As String = DdlAccionMAA.SelectedValue.ToString

        Dim dt As DataTable
        dt = obj.Insertar_Accion(psconexion, Session("CodEmpresa"), TicketEstado, TicketProceso, TicketAccion)
        Dim dvRow As DataRow = dt.Rows(0)
        If dvRow(0) = "2" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe relación en la tabla');", True)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('hide');", True)
        Lista_Proceso_Estado()
    End Sub

    '
    '
    'TIEMPO ESTADO CLIENTE
    '
    '

    Sub Listar_Tiempo_Estado_Cliente()
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Tiempo_Estado_Clientes(psconexion)
        GvListaTiempoEstadoCliente.DataSource = dt
        GvListaTiempoEstadoCliente.DataBind()

        TotalTiempoEstadosL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalTiempoEstadosL.Visible = True
        TotalTiempoEstadosL.Visible = True
    End Sub

    Sub Ocultar_Mostrar_Tiempo_Estado_Cliente(ByVal vf As Boolean)
        If Session("AyudaCliente") = "IngresarTiempoEstadoCliente" Or Session("AyudaCliente") = "EditarTiempoEstadoCliente" Then
            DdlEstadoTiempoEstadoCliente.SelectedValue = "< Seleccionar >"
            DdlDiasTiempoEstadoCliente.SelectedValue = "--"
            DdlHorasTiempoEstadoCliente.SelectedValue = "--"
            DdlMinutosTiempoEstadoCliente.SelectedValue = "--"
            TituloAgregarTiempoEstadoCliente.Visible = vf
            LblDuracionTiempoEstadoCliente.Visible = vf
            DdlDiasTiempoEstadoCliente.Visible = vf
            DdlHorasTiempoEstadoCliente.Visible = vf
            DdlMinutosTiempoEstadoCliente.Visible = vf
            LblDiasTiempoEstadoCliente.Visible = vf
            LblHorasTiempoEstadoCliente.Visible = vf
            LblMinutosTiempoEstadoCliente.Visible = vf
            BtnGuardarTiempoEstadoCliente.Visible = vf
            BtnCancelarTiempoEstadoCliente.Visible = vf
            LblEstadoTiempoEstadoCliente.Visible = vf
            DdlEstadoTiempoEstadoCliente.Visible = vf
            vf = False
            LblAccionesTiempoEstadoCliente.Visible = vf
            DdlAccionesTiempoEstadoCliente.Visible = vf
            BtnAsignarAccionesTiempoEstadoCliente.Enabled = True
        ElseIf Session("AyudaCliente") = "AsignarTiempoEstadoCliente" Then
            DdlEstadoTiempoEstadoCliente.SelectedValue = "< Seleccionar >"
            DdlAccionesTiempoEstadoCliente.SelectedValue = "< Seleccionar >"
            TituloAgregarTiempoEstadoCliente.Visible = vf
            LblAccionesTiempoEstadoCliente.Visible = vf
            DdlAccionesTiempoEstadoCliente.Visible = vf
            LblEstadoTiempoEstadoCliente.Visible = vf
            DdlEstadoTiempoEstadoCliente.Visible = vf
            BtnGuardarTiempoEstadoCliente.Visible = vf
            BtnCancelarTiempoEstadoCliente.Visible = vf
            If vf Then
                BtnAsignarAccionesTiempoEstadoCliente.Enabled = False
            Else
                BtnAsignarAccionesTiempoEstadoCliente.Enabled = True
            End If
            vf = False
            LblDuracionTiempoEstadoCliente.Visible = vf
            DdlDiasTiempoEstadoCliente.Visible = vf
            DdlHorasTiempoEstadoCliente.Visible = vf
            DdlMinutosTiempoEstadoCliente.Visible = vf
            LblDiasTiempoEstadoCliente.Visible = vf
            LblHorasTiempoEstadoCliente.Visible = vf
            LblMinutosTiempoEstadoCliente.Visible = vf
        End If
    End Sub


    Private Sub BtnListarTiempoEstadoCliente_Click(sender As Object, e As EventArgs) Handles BtnListarTiempoEstadoCliente.Click
        System.Threading.Thread.Sleep(1000)
        Listar_Tiempo_Estado_Cliente()
    End Sub

    Private Sub BtnAsignarAccionesTiempoEstadoCliente_Click(sender As Object, e As EventArgs) Handles BtnAsignarAccionesTiempoEstadoCliente.Click
        TituloAgregarTiempoEstadoCliente.Text = "Asignar Tiempo Estado"
        Session("AyudaCliente") = "AsignarTiempoEstadoCliente"
        BtnGuardarTiempoEstadoCliente.Text = "Guardar"
        Ocultar_Mostrar_Tiempo_Estado_Cliente(True)
    End Sub

    Private Sub BtnGuardarTiempoEstadoCliente_Click(sender As Object, e As EventArgs) Handles BtnGuardarTiempoEstadoCliente.Click
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim dbRow As DataRow
        Dim psconexion As String = Session("Ruta_Emp")
        Dim estado As String = DdlEstadoTiempoEstadoCliente.SelectedValue.ToString
        Dim accion As String = DdlAccionesTiempoEstadoCliente.SelectedValue.ToString
        Dim dia As String = DdlDiasTiempoEstadoCliente.SelectedValue.ToString
        Dim hora As String = DdlHorasTiempoEstadoCliente.SelectedValue.ToString
        Dim minuto As String = DdlMinutosTiempoEstadoCliente.SelectedValue.ToString

        If BtnGuardarTiempoEstadoCliente.Text = "Guardar" Then
            If estado.Equals("< Seleccionar >") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un estado');", True)
            ElseIf accion.Equals("< Seleccionar >") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una acción');", True)
            Else
                dt = obj.Registra_Estado_Accion_Cliente(psconexion, estado, accion)
                dbRow = dt.Rows(0)
                If dbRow(0).ToString = "2" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La acción ya está registrada en el estado');", True)
                ElseIf dbRow(0).ToString = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Acción registrada correctamente');", True)
                    Ocultar_Mostrar_Tiempo_Estado_Cliente(False)
                    Listar_Tiempo_Estado_Cliente()
                End If
            End If
        ElseIf BtnGuardarTiempoEstadoCliente.Text = "Ingresar Tiempo" Or BtnGuardarTiempoEstadoCliente.Text = "Editar Tiempo" Then
            If estado.Equals("< Seleccionar >") Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un estado');", True)
            ElseIf DdlDiasTiempoEstadoCliente.SelectedValue = "--" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un día');", True)
            ElseIf DdlHorasTiempoEstadoCliente.SelectedValue = "--" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un hora');", True)
            ElseIf DdlMinutosTiempoEstadoCliente.SelectedValue = "--" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un minuto');", True)
            Else
                If CInt(dia) < 10 Then dia = "0" + dia
                If CInt(hora) < 10 Then hora = "0" + hora
                If CInt(minuto) < 10 Then minuto = "0" + minuto
                Dim total As Integer = (CInt(dia) * 1440) + (CInt(hora) * 60) + CInt(minuto)
                If BtnGuardarTiempoEstadoCliente.Text = "Ingresar Tiempo" Then
                    dt = obj.Agregar_Tiempo_Estado_Cliente(psconexion, estado, dia, hora, minuto, total)
                    dbRow = dt.Rows(0)
                    If dbRow(0).ToString = "2" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Estado ya tiene un tiempo registrado');", True)
                    ElseIf dbRow(0).ToString = "1" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Registrado correctamente');", True)
                        Ocultar_Mostrar_Tiempo_Estado_Cliente(False)
                        Listar_Tiempo_Estado_Cliente()
                    End If
                ElseIf BtnGuardarTiempoEstadoCliente.Text = "Editar Tiempo" Then
                    dt = obj.Editar_Tiempo_Estado_Cliente(psconexion, estado, dia, hora, minuto, total)
                    dbRow = dt.Rows(0)
                    If dbRow(0).ToString = "2" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Estado no tiene un tiempo registrado');", True)
                    ElseIf dbRow(0).ToString = "1" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Modificado Correctamente');", True)
                        Ocultar_Mostrar_Tiempo_Estado_Cliente(False)
                        Listar_Tiempo_Estado_Cliente()
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub BtnCancelarTiempoEstadoCliente_Click(sender As Object, e As EventArgs) Handles BtnCancelarTiempoEstadoCliente.Click
        Ocultar_Mostrar_Tiempo_Estado_Cliente(False)
    End Sub

    Private Sub BtnIngresarTiempoEstadoCliente_Click(sender As Object, e As EventArgs) Handles BtnIngresarTiempoEstadoCliente.Click
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Session("AyudaCliente") = "IngresarTiempoEstadoCliente"
        TituloAgregarTiempoEstadoCliente.Text = "Ingresar Tiempo Estado"
        BtnGuardarTiempoEstadoCliente.Text = "Ingresar Tiempo"
        Ocultar_Mostrar_Tiempo_Estado_Cliente(True)
    End Sub

    Private Sub GvListaTiempoEstadoCliente_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaTiempoEstadoCliente.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim estado As String = GvListaTiempoEstadoCliente.Rows(Index).Cells(5).Text
        Dim accion As String = Replace(GvListaTiempoEstadoCliente.Rows(Index).Cells(4).Text, "&nbsp;", "")
        Dim tiempo As String = Replace(GvListaTiempoEstadoCliente.Rows(Index).Cells(3).Text, "&nbsp;", "")
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")

        If e.CommandName = "EditarTiempo" Then
            If tiempo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay tiempo para modificar');", True)
            Else
                Dim dias As String = tiempo.Substring(0, 2).ToString.Trim
                Dim horas As String = tiempo.Substring(13, 2).ToString.Trim
                Dim minutos As String = tiempo.Substring(22, 2).ToString.Trim
                Session("AyudaCliente") = "EditarTiempoEstadoCliente"
                TituloAgregarTiempoEstadoCliente.Text = "Editar Tiempo Estado"
                BtnGuardarTiempoEstadoCliente.Text = "Editar Tiempo"
                Ocultar_Mostrar_Tiempo_Estado_Cliente(True)
                DdlEstadoTiempoEstadoCliente.SelectedValue = CInt(estado)
                DdlEstadoTiempoEstadoCliente.Enabled = False
                DdlDiasTiempoEstadoCliente.SelectedValue = CInt(dias)
                DdlHorasTiempoEstadoCliente.SelectedValue = CInt(horas)
                DdlMinutosTiempoEstadoCliente.SelectedValue = CInt(minutos)
            End If
        ElseIf e.CommandName = "EliminarAccionTiempo" Then
            If accion = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay acciones para eliminar');", True)
            Else
                LblCodTiempoEstadoCliente.Text = estado
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAccionesTiempoEstado').modal('show');", True)
                dt = obj.Listar_Acciones_XEstado(psconexion, estado)
                GvAccionesXEstado.DataSource = dt
                GvAccionesXEstado.DataBind()
            End If
        End If
    End Sub

    Private Sub BtnCerrarAccionesEstado_Click(sender As Object, e As EventArgs) Handles BtnCerrarAccionesEstado.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAccionesTiempoEstado').modal('hide');", True)
        BtnListarTiempoEstadoCliente_Click(sender, e)
    End Sub

    Private Sub GvAccionesXEstado_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvAccionesXEstado.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Cliente
        Dim dt As New DataTable
        Dim estado As String = LblCodTiempoEstadoCliente.Text
        Dim accion As String = GvAccionesXEstado.Rows(Index).Cells(2).Text
        Dim psconexion As String = Session("Ruta_Emp")

        If e.CommandName = "Eliminar" Then
            obj.Elimina_Acciones_XEstado(psconexion, estado, accion)
            dt = obj.Listar_Acciones_XEstado(psconexion, estado)
            GvAccionesXEstado.DataSource = dt
            GvAccionesXEstado.DataBind()
        End If
    End Sub

    '
    '
    'PROCESO ESTADO CLIENTE
    '
    '

    Protected Sub Lista_Estado_Cliente()
        Dim obj As New Cls_Estado_Cliente
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Estado_Cliente(psconexion)
        GvListaEstadoCliente.DataSource = dt
        GvListaEstadoCliente.DataBind()

        LblTotalEstadoClienteL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalEstadoCliente.Visible = True
        LblTotalEstadoClienteL.Visible = True
    End Sub
    Private Sub GvListaEstadoCliente_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaEstadoCliente.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Estado_Cliente
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstadoCodigo As String = GvListaEstadoCliente.Rows(Index).Cells(1).Text
        Dim ProcesoCodigo As String = GvListaEstadoCliente.Rows(Index).Cells(4).Text
        Dim dt As New DataTable
        If e.CommandName = "QuitarRelacion" Then
            dt = obj.Eliminar_Relacion(psconexion, EstadoCodigo, ProcesoCodigo)
            Lista_Estado_Cliente()
        End If
    End Sub
    Private Sub BtnAgregarRelacionCliente_Click(sender As Object, e As EventArgs) Handles BtnAgregarRelacionCliente.Click
        LblTituloAgregarRelacionESTADOCLIENTE.Visible = True
        LblTipoProcesoESTADOCLIENTE.Visible = True
        DdlTipoProcesoESTADOCLIENTE.Visible = True
        LblEstadoESTADOCLIENTE.Visible = True
        DdlEstadoESTADOCLIENTE.Visible = True
        BtnGuardarAgregarRelacionEC.Visible = True
        BtnCancelarAgregarRelacionEC.Visible = True
        Limpiar_Cajas_Agregar_Contactos_EC()
        DdlEstadoPROCESOESTADO.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub BtnListarEstadoCliente_Click(sender As Object, e As EventArgs) Handles BtnListarEstadoCliente.Click
        Lista_Estado_Cliente()
    End Sub
    Private Sub BtnCancelarAgregarRelacionEC_Click(sender As Object, e As EventArgs) Handles BtnCancelarAgregarRelacionEC.Click
        LblTituloAgregarRelacionESTADOCLIENTE.Visible = False
        LblTipoProcesoESTADOCLIENTE.Visible = False
        DdlTipoProcesoESTADOCLIENTE.Visible = False
        LblEstadoESTADOCLIENTE.Visible = False
        DdlEstadoESTADOCLIENTE.Visible = False
        BtnGuardarAgregarRelacionEC.Visible = False
        BtnCancelarAgregarRelacionEC.Visible = False
        DdlEstadoPROCESOESTADO.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Estado()
        Dim obj As New Cls_Estado_Cliente
        Dim dt As New DataTable

        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Estado(psconexion)
        DdlEstadoESTADOCLIENTE.DataSource = dt
        DdlEstadoESTADOCLIENTE.DataValueField = "ELEMEN_CODIGO"
        DdlEstadoESTADOCLIENTE.DataTextField = "ELEMEN_VALOR"
        DdlEstadoESTADOCLIENTE.DataBind()
        DdlEstadoESTADOCLIENTE.Items.Add("< Seleccionar >")
        DdlEstadoESTADOCLIENTE.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Limpiar_Cajas_Agregar_Contactos_EC()
        DdlTipoProcesoESTADOCLIENTE.SelectedValue = "< Seleccionar >"
        DdlEstadoESTADOCLIENTE.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub BtnGuardarAgregarRelacionEC_Click(sender As Object, e As EventArgs) Handles BtnGuardarAgregarRelacionEC.Click
        Dim obj As New Cls_Estado_Cliente
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstadoCodigo As String = DdlEstadoESTADOCLIENTE.SelectedValue.ToString
        Dim ProcesoCodigo As String = DdlTipoProcesoESTADOCLIENTE.SelectedValue.ToString
        Dim dt As DataTable

        If EstadoCodigo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un Estado');", True)
        ElseIf ProcesoCodigo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Tipo de Proceso');", True)
        Else
            dt = obj.Insertar_Estado_Relacion(psconexion, EstadoCodigo, ProcesoCodigo)
            Dim dvRow As DataRow = dt.Rows(0)
            If dvRow(0) = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe relación en la tabla');", True)
            End If
            Lista_Estado_Cliente()
            Limpiar_Cajas_Agregar_Contactos_EC()
        End If
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click

        Dim NroTabla As String = "4"
        Dim var_Tabla1 As String = ""
        Dim var_Tabla2 As String = ""
        Dim var_Tabla3 As String = ""
        Response.Redirect("~/Sistema/SegSistem_Exportar_Datos.aspx?parametro=" & Server.UrlEncode(NroTabla) & "&var_Tabla1=" & Server.UrlEncode(var_Tabla1) & "&var_Tabla2=" & Server.UrlEncode(var_Tabla2) & "&var_Tabla3=" & Server.UrlEncode(var_Tabla3))

    End Sub
End Class