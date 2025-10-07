Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_Relacion_Salidas_Almacent
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objEmp As New ModuloGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                DdlEstado.Items.Clear()
                Dim lst1 As New ListItem : lst1.Text = "Generada" : lst1.Value = 1 : DdlEstado.Items.Add(lst1)
                Dim lst2 As New ListItem : lst2.Text = "Enviada" : lst2.Value = 2 : DdlEstado.Items.Add(lst2)
                Dim lst3 As New ListItem : lst3.Text = "Recibida Ok" : lst3.Value = 3 : DdlEstado.Items.Add(lst3)
                Dim lst4 As New ListItem : lst4.Text = "Recibida No Ok" : lst4.Value = 4 : DdlEstado.Items.Add(lst4)
                Dim lst5 As New ListItem : lst5.Text = "Generada sin serie" : lst5.Value = 5 : DdlEstado.Items.Add(lst5)
                Dim lst6 As New ListItem : lst6.Text = "Anulada" : lst6.Value = 6 : DdlEstado.Items.Add(lst6)
                Dim lst7 As New ListItem : lst7.Text = "Recibida Sin Placa" : lst7.Value = 7 : DdlEstado.Items.Add(lst7)
                Dim lst8 As New ListItem : lst8.Text = "Todas las Generadas" : lst8.Value = -1 : DdlEstado.Items.Add(lst8)
                Dim lst9 As New ListItem : lst9.Text = "Todas las Enviadas" : lst9.Value = -2 : DdlEstado.Items.Add(lst9)
                Dim lst10 As New ListItem : lst10.Text = "Todas las Recibidass" : lst10.Value = -3 : DdlEstado.Items.Add(lst10)
                Dim lst11 As New ListItem : lst11.Text = "Todas las Salidas" : lst11.Value = -4 : DdlEstado.Items.Add(lst11)
                DdlEstado.Items.Add("< Seleccionar >") : DdlEstado.SelectedValue = "< Seleccionar >"
                Call Carga_Motivos()
            Catch Ex As SqlException
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Private Sub Carga_Motivos()
        Dim psConexion As String = Session("Ruta_Emp")
        Dim Cn As New SqlConnection(psConexion)
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        DdlMotivo.Items.Clear()
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = " SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO" _
                               & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '1') AND (MAINSA_UBICACION2 IN (1,2,3,4,5,6)) ORDER BY MOTIVO_TRASLADO"
            Rs = cmdSql.ExecuteReader()
            DdlMotivo.DataSource = Rs
            DdlMotivo.DataTextField = "MOTIVO_TRASLADO"
            DdlMotivo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
            DdlMotivo.DataBind()

            DdlMotivo.Items.Add("< Seleccionar >")
            DdlMotivo.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch Ex As Exception
            LblError.Text = Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim pCodSalida As Double = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp")
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        Dim psMotivo As String = ""
        Dim psEstado As String = ""
        psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If

        Try
            If DdlMotivo.SelectedValue <> "< Seleccionar >" Then
                psMotivo = DdlMotivo.SelectedValue
            End If
            If DdlEstado.SelectedValue <> "< Seleccionar >" Then
                psEstado = DdlEstado.SelectedValue
            End If

            If TxtNroSalida.Text <> "" Then pCodSalida = Nz(TxtNroSalida.Text)
            gridSalida.DataSource = obj.Lista_SalidaAlmacen(psConexion, Session("CodEmpresa"), pCodSalida, psFecha, psFechaFin, psMotivo, psEstado)
            gridSalida.DataBind()
            LblRegistro.Text = "Se encontrarón " & gridSalida.Rows.Count & " registros."

        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub gridSalida_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridSalida.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psCodSalida As Double = 0
        Dim dtDatos As New DataTable
        dtDatos = Nothing
        gridSalidaEq.DataSource = dtDatos
        gridSalidaEq.DataBind()
        gridSalidaAcc.DataSource = dtDatos
        gridSalidaAcc.DataBind()
        If e.CommandName = "Detalle" Then
            psCodSalida = gridSalida.Rows(Index).Cells(2).Text
            LblTituloModal.Text = "Salida Nro. " & psCodSalida
            dtDatos = obj.Lista_DetalleSinSeries_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "1")
            If dtDatos.Rows.Count > 0 Then
                gridSalidaAcc.DataSource = dtDatos
                gridSalidaAcc.DataBind()
            End If
            'Label2
            If dtDatos.Rows.Count > 0 Then
                Label2.Visible = True
            Else
                Label2.Visible = False
            End If
            dtDatos = Nothing

            dtDatos = obj.Lista_Detalle_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "1")
            gridSalidaEq.DataSource = dtDatos
            gridSalidaEq.DataBind()

            If dtDatos.Rows.Count > 0 Then
                LblEtiq35.Visible = True
            Else
                LblEtiq35.Visible = False
            End If
            dtDatos = Nothing
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
        End If
        If e.CommandName = "Eliminar" Then
            Session("AnulUnaVez") = "1"
            psCodSalida = gridSalida.Rows(Index).Cells(2).Text
            Session("AnulCodSalida") = gridSalida.Rows(Index).Cells(2).Text
            lblCodEstado.Text = gridSalida.Rows(Index).Cells(14).Text
            Session("AnulCodEstado") = gridSalida.Rows(Index).Cells(14).Text
            lblCodsalida.Text = psCodSalida
            txtAnulacion.Text = ""
            Dim psOrdenVenta As String = ""
            LblTituloModalAnul.Text = "Salida Nro. " & Llenar_Ceros(psCodSalida, 6)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAnulacion').modal('show');", True)
        End If
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub

    Private Sub BtnRegularizar_Click(sender As Object, e As EventArgs) Handles BtnRegularizar.Click
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        'Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim Cn4 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Rs3 As SqlDataReader
        Cn.Open()
        Cn2.Open()
        Cn3.Open()
        Cn4.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal2.Connection = Cn2
        CmdGlobal3.Connection = Cn3
        CmdGlobal4.Connection = Cn4
        Dim pd_Secuencia_Accion As Double = 0
        Dim psNroticket As Double = 0
        Dim psCodSalida As Double = 0
        Dim obj As New clsInv_Procesos
        Try

            CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND not DESP_TICKET is null order by DESP_CODIGO desc"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodSalida = Nz(Rs!DESP_CODIGO)
                    psNroticket = Nz(Rs!DESP_TICKET)
                    CmdGlobal2.CommandText = "SELECT TICKET_CODIGO FROM TBTICKET_TRAKING_ACCION WHERE ACCION_REFERENCIA = " & psCodSalida & " and  TICKET_CODIGO=" & psNroticket
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows = False Then
                        CmdGlobal3.CommandText = "SELECT MAX(ACCION_SECUENCIA) FROM TBTICKET_TRAKING_ACCION WHERE TICKET_CODIGO=" & psNroticket
                        Rs3 = CmdGlobal3.ExecuteReader
                        If Rs3.HasRows Then
                            While Rs3.Read
                                pd_Secuencia_Accion = Format(Nz(Rs3(0)) + 1, "000")
                            End While
                        Else
                            pd_Secuencia_Accion = "1"
                        End If
                        Rs3.Close()
                        CmdGlobal3.CommandText = " INSERT INTO TBTICKET_TRAKING_ACCION ( TICKET_CODIGO, ACCION_SECUENCIA, ACCION_CODIGO, ACCION_FECHA, ACCION_HORA, ACCION_USER, ACCION_REFERENCIA) " _
                                              & " VALUES (" & Nz(psNroticket) & ", " & Nz(pd_Secuencia_Accion) & ", '18', '" & Nu(Rs!DESP_FECHA) & "', '" & HoraActual(True) & "', '" & Session("User") & "', " & Nz(psCodSalida) & ")"
                        CmdGlobal3.ExecuteNonQuery()
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la regularizacion de traking');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos:" & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('ha ocurrido un error en la aplicacion:" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub BtnAnularCerrar_Click(sender As Object, e As EventArgs) Handles BtnAnularCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAnulacion').modal('hide');", True)
    End Sub

    Protected Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        Dim psOrdenVenta As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs1 As SqlDataReader
        Dim psCodigoDestino As String = ""
        Dim fn As New clsInv_Procesos
        Dim psMensaje As String = ""


        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        If Session("AnulUnaVez") = "1" Then
            If txtAnulacion.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el motivo de la anulación.');", True)
            Else
                Session("AnulUnaVez") = "2"
                If lblCodEstado.Text = "3" Then

                    CmdGlobal.CommandText = " SELECT * " _
                                        & " FROM TBINV_ALMACEN_DESPACHO D INNER JOIN TBINV_ALMACEN_DESPACHO_DET DD ON D.DESP_CODIGO = DD.DESP_CODIGO " _
                                        & " WHERE D.DESP_CODIGO = " & lblCodsalida.Text & " AND D.DESP_SYS_EST ='0' AND D.EMPRESA_CODIGO ='" & Session("CodEmpresa") & "'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            If Nu(Rs!DESP_TIPODESTINO) = "1" Then
                                psCodigoDestino = Nu(Rs!ALMACEN_CODIGO_DESTINO)
                            ElseIf Nu(Rs!DESP_TIPODESTINO) = "2" Then
                                psCodigoDestino = Nu(Rs!CECOSE_CODIGO_DESTINO)
                            ElseIf Nu(Rs!DESP_TIPODESTINO) = "3" Then
                                psCodigoDestino = Nu(Rs!PROVEEDOR_CODIGO_DESTINO)
                            ElseIf Nu(Rs!DESP_TIPODESTINO) = "4" Then
                                psCodigoDestino = Nu(Rs!EQUIPO_CODIGO_DESTINO)
                            ElseIf Nu(Rs!DESP_TIPODESTINO) = "5" Then
                                psCodigoDestino = Nu(Rs!PERSONA_CODIGO_DESTINO)
                            ElseIf Nu(Rs!DESP_TIPODESTINO) = "6" Then
                                psCodigoDestino = Nu(Rs!CLIENTE_CODIGO_DESTINO)
                            End If
                            If Nu(Rs!DESP_TIPODESTINO) <> "5" Then
                                CmdGlobal2.CommandText = " SELECT * FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " " _
                                                       & " WHERE UBICACT_TIPO = '" & Nu(Rs!DESP_TIPODESTINO) & "' AND UBICACT_CODIGO = " & psCodigoDestino & ""
                                Rs1 = CmdGlobal2.ExecuteReader
                                If Rs1.HasRows Then
                                    While Rs1.Read
                                        psMensaje = "No se puede anular la salida N° " & Nu(Rs!DESP_CODIGO) & " Uno de los equipos no se encuentra en la ubicación de destino."
                                    End While
                                End If
                                Rs1.Close()
                            End If
                        End While
                    End If
                    Rs.Close()
                    If psMensaje <> "" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('" & psMensaje & "');", True)
                    Else
                        fn.Anular_SalidasRecibidas(lblCodsalida.Text, txtAnulacion.Text, Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"))
                        'Call Actualizar_CantOV_Elimina() 'falta proceso

                        CmdGlobal.CommandText = " SELECT DESP_CODIGO, OVENTA_CODIGO FROM TBVENTAS_ORDENVENTA_SALIDA  WHERE DESP_CODIGO = " & lblCodsalida.Text
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psOrdenVenta = Nu(Rs!OVENTA_CODIGO)
                            End While
                        End If
                        Rs.Close()
                        fn.Actualizar_Estado_Orden_Venta(psOrdenVenta, Session("Ruta_Emp"))
                        BtnListar_Click(sender, e)
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAnulacion').modal('hide');", True)
                        txtAnulacion.Text = ""
                    End If
                Else
                    If lblCodEstado.Text = "1" Or lblCodEstado.Text = "2" Or lblCodEstado.Text = "5" Then
                        fn.Anular_Salida(lblCodsalida.Text, txtAnulacion.Text, Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), lblCodEstado.Text)
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAnulacion').modal('hide');", True)
                    Else
                        psMensaje = "No se puede anular la Salida porque ya ha sido " & lblCodsalida.Text
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('" & psMensaje & "');", True)
                    End If
                End If
            End If
        End If
    End Sub


End Class
