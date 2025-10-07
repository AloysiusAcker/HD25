Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Partial Class Inventario_Inventario_Define_CentroCostos
    Inherits System.Web.UI.Page
    Dim obj As New clsLogis_Listado
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblCCError.Text = ""
            LblSecError.Text = ""
            DdlEstablecimiento.Items.Clear()
            Call LlenaComboItem("TBOPC327", DdlEstablecimiento)
            DdlSecEstablecimiento.Items.Clear()
            Call LlenaComboItem("TBOPC327", DdlSecEstablecimiento)
            Dim objSeg As New ModuloSeguridad
            Dim dt As New DataTable
            DdlResponsable.Items.Clear()
            dt = objSeg.Listar_Usuarios_SinAdm(Ruta_Ng)
            DdlResponsable.DataSource = dt
            DdlResponsable.DataTextField = "NOMBRES"
            DdlResponsable.DataValueField = "USUARI_CODIGO"
            DdlResponsable.DataBind()
            DdlResponsable.Items.Add("< Seleccionar >")
            DdlResponsable.SelectedValue = "< Seleccionar >"
            Ficha.ActiveTabIndex = 1 : Ficha.Enabled = False
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Private Sub ListarCC_Seccion()
        Dim dt As New DataTable
        dt = Nothing
        Dim psCodCC As Double = 0
        LblSecError.Text = ""
        Try
            psCodCC = Nz(LblCodCentroCosto.Text)
            dt = obj.Lista_Centro_Costos_Seccion(Session("Ruta_Emp"), Session("Codempresa"), "", "", psCodCC)
            GvSeccion.DataSource = dt
            GvSeccion.DataBind()
            LblRegistro.Text = ""
            If dt.Rows.Count = 1 Then
                LblRegistro.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count > 1 Then
                LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
            End If
        Catch ex As SqlException
            LblSecError.Text = ex.Message
        Catch Ex As Exception
            LblSecError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub ListarCC()
        Dim dt As New DataTable
        dt = Nothing
        LblCCError.Text = ""
        Dim psDescripcion As String = ""
        If TxtBuscarCC.Text.Trim <> "" Then
            psDescripcion = TxtBuscarCC.Text.Trim
        End If

        Try
            dt = obj.Lista_Centro_Costos(Session("Ruta_Emp"), Session("Codempresa"), "", psDescripcion)
            GvCC.DataSource = dt
            GvCC.DataBind()
            LblCCRegistro.Text = ""
            If dt.Rows.Count = 1 Then
                LblCCRegistro.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count > 1 Then
                LblCCRegistro.Text = "Hay " & dt.Rows.Count & " registros."
            End If
        Catch ex As SqlException
            LblCCError.Text = ex.Message
        Catch Ex As Exception
            LblCCError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub BtnNuevaCC_Click(sender As Object, e As EventArgs) Handles BtnNuevaCC.Click
        FichaNuevo.Visible = True
        divNuevoCC.Visible = True
        divEditarCC.Visible = False
        TxtCodInterno.Text = ""
        TxtDescripcion.Text = ""
        TxtDireccion.Text = ""
        TxtEdificio.Text = ""
        TxtPiso.Text = ""
        TxtRuc.Text = ""
        TxtUbicacion.Text = ""
        DdlDptoCC.Items.Clear()
        DdlProvCC.Items.Clear()
        DdlDistCC.Items.Clear()
        DdlDptoCC.Enabled = True
        DdlProvCC.Items.Add("< Seleccionar >") : DdlProvCC.SelectedValue = "< Seleccionar >"
        DdlProvCC.Enabled = False
        DdlDistCC.Items.Add("< Seleccionar >") : DdlDistCC.SelectedValue = "< Seleccionar >"
        DdlDistCC.Enabled = False
        Call LlenaComboItem("TBOPC002", DdlDptoCC)
        DdlEstablecimiento.SelectedValue = "< Seleccionar >"
        BtnNuevaCC.Enabled = False
        BtnCCCancelar.Visible = True
        BtnCCGuardar.Visible = True
    End Sub
    Protected Sub BtnListarCC_Click(sender As Object, e As EventArgs) Handles BtnListarCC.Click
        Call ListarCC()
    End Sub
    Protected Sub BtnCCCancelar_Click(sender As Object, e As EventArgs) Handles BtnCCCancelar.Click
        FichaNuevo.Visible = False
        divNuevoCC.Visible = False
        divEditarCC.Visible = False
        DdlEstablecimiento.SelectedValue = "< Seleccionar >"
        TxtCodInterno.Text = ""
        TxtDescripcion.Text = ""
        TxtDireccion.Text = ""
        TxtEdificio.Text = ""
        TxtPiso.Text = ""
        TxtRuc.Text = ""
        BtnNuevaCC.Enabled = True
        BtnCCCancelar.Visible = False
        BtnCCGuardar.Visible = False
    End Sub

    Private Sub GvCC_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvCC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim codigo As Double = Nz(GvCC.Rows(Index).Cells(6).Text)
        Dim psCodInterno As String = (GvCC.Rows(Index).Cells(4).Text)
        Dim psDescripcion As String = (GvCC.Rows(Index).Cells(5).Text)
        Dim dt As New DataTable
        DdlEstablecimiento.SelectedValue = "< Seleccionar >"
        If e.CommandName = "Seccion" Then
            LblCodCentroCosto.Text = codigo
            TxtSecCodInterno.Text = psCodInterno
            TxtSecDescripcion.Text = psDescripcion
            BtnNuevo.Visible = True
            BtnNuevo.Enabled = True
            Call ListarCC_Seccion()
            Ficha.ActiveTabIndex = 0
            Ficha.ActiveTabIndex = 1
            Ficha.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        ElseIf e.CommandName = "Editar" Then
            FichaNuevo.Visible = True
            divNuevoCC.Visible = False
            divEditarCC.Visible = True
            TxtCodInterno.Text = psCodInterno
            TxtDescripcion.Text = psDescripcion
            LblCodCC.Text = codigo
            DdlDptoCC.Items.Clear()
            DdlProvCC.Items.Clear()
            DdlDistCC.Items.Clear()
            DdlDptoCC.Enabled = True
            DdlProvCC.Items.Add("< Seleccionar >") : DdlProvCC.SelectedValue = "< Seleccionar >"
            DdlProvCC.Enabled = False
            DdlDistCC.Items.Add("< Seleccionar >") : DdlDistCC.SelectedValue = "< Seleccionar >"
            DdlDistCC.Enabled = False
            Call LlenaComboItem("TBOPC002", DdlDptoCC)
            dt = obj.Busca_Centro_Costos_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), codigo)
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    If Nz(drow("CCOSTO_TIPO")) > 0 Then DdlEstablecimiento.SelectedValue = Nz(drow("CCOSTO_TIPO"))
                    TxtDireccion.Text = Nu(drow("CCOSTO_DIRECCION"))
                    TxtEdificio.Text = Nu(drow("CCOSTO_EDIFICIO"))
                    TxtPiso.Text = Nu(drow("CCOSTO_PISO"))
                    TxtRuc.Text = Nu(drow("CCOSTO_RUC"))
                    TxtUbicacion.Text = Nu(drow("CCOSTO_UBICACION"))
                    If drow("CCOSTO_DPTO").ToString <> "" Then
                        DdlDptoCC.SelectedValue = Nu(drow("CCOSTO_DPTO")) : DdlDptoCC_SelectedIndexChanged(sender, e)
                        If drow("CCOSTO_PROVINCIA").ToString <> "" Then
                            DdlProvCC.SelectedValue = drow("CCOSTO_PROVINCIA")
                            DdlProvCC_SelectedIndexChanged(sender, e)
                            If drow("CCOSTO_DISTRITO").ToString <> "" Then DdlDistCC.SelectedValue = drow("CCOSTO_DISTRITO")
                        End If
                    End If
                Next
            End If
            BtnNuevaCC.Enabled = False
            BtnCCCancelar.Visible = True
            BtnCCGuardar.Visible = True
        ElseIf e.CommandName = "Eliminar" Then
            dt = obj.Eliminar_Centro_Costos_Seccion(Session("Ruta_Emp"), Session("CodEmpresa"), codigo, 0)
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    If Nu(drow("Mensaje")) = "S" Then
                        Mensaje.Text = "No se puede eliminar el Centro de Costos " & psDescripcion & " tiene Seccion."
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensaje').modal('show');", True)
                Next
            End If
            dt = Nothing
            Call ListarCC()
        End If
    End Sub

    Private Sub Ficha_ActiveTabChanged(sender As Object, e As EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha.Enabled = True
            BtnSecCancelar_Click(sender, e)
            BtnCCCancelar_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.Enabled = True
            BtnSecCancelar_Click(sender, e)
            BtnCCCancelar_Click(sender, e)
        End If
    End Sub
    Protected Sub BtnCCGuardar_Click(sender As Object, e As EventArgs) Handles BtnCCGuardar.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs2 As SqlDataReader
        Dim psExiste As String = ""
        Dim psCodCC As String = ""
        Dim ps_dpto As String = "NULL"
        Dim ps_prov As String = "NULL"
        Dim ps_distr As String = "NULL"
        Dim ps_Establecimiento As String = "NULL"
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Cn3.Open() : CmdGlobal3.Connection = Cn3
            LblCCError.Text = ""
            If DdlEstablecimiento.SelectedValue <> "< Seleccionar >" Then ps_Establecimiento = DdlEstablecimiento.SelectedValue
            If DdlDptoCC.SelectedValue <> "< Seleccionar >" Then ps_dpto = DdlDptoCC.SelectedValue
            If DdlProvCC.SelectedValue <> "< Seleccionar >" Then ps_prov = DdlProvCC.SelectedValue
            If DdlDistCC.SelectedValue <> "< Seleccionar >" Then ps_distr = DdlDistCC.SelectedValue
            If TxtCodInterno.Text.Trim = "" Then LblCCError.Text = LblCCError.Text & "<br> - Ingresar Codigo Interno del Centro de Costos."
            If TxtDescripcion.Text.Trim = "" Then LblCCError.Text = LblCCError.Text & "<br> - Ingresar la descripcion del Centro de Costos."
            If DdlEstablecimiento.SelectedValue = "< Seleccionar >" Then LblCCError.Text = LblCCError.Text & "<br> - Seleccionar tipo de establecimiento."
            If divNuevoCC.Visible = True Then
                CmdGlobal2.CommandText = " SELECT CCOSTO_CODIGO FROM TBLOGIS_CENTRO_COSTOS " _
                                       & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND UPPER(CCOSTO_COD_INTERNO)='" & UCase(TxtCodInterno.Text) & "' AND (CCOSTO_SYS_EST = '0')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        LblCCError.Text = LblCCError.Text & "<br> - El Codigo Interno ingresado ya existe."
                    End While
                End If
                Rs2.Close()
                CmdGlobal2.CommandText = " SELECT CCOSTO_CODIGO FROM TBLOGIS_CENTRO_COSTOS WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND UPPER(CCOSTO_DESCRIPCION)='" & UCase(Trim(TxtDescripcion.Text)) & "' AND (CCOSTO_SYS_EST = '0')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        LblCCError.Text = LblCCError.Text & "<br> - La descripcion ingresada ya existe."
                    End While
                End If
                Rs2.Close()
                If LblCCError.Text <> "" Then
                    LblCCError.Text = LblCCError.Text
                    Exit Sub
                End If

                CmdGlobal2.CommandText = "SELECT MAX(CCOSTO_CODIGO) FROM TBLOGIS_CENTRO_COSTOS WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        psCodCC = Nz(Rs2(0)) + 1
                    End While
                Else
                    psCodCC = "1"
                End If
                Rs2.Close()

                CmdGlobal2.CommandText = "INSERT INTO TBLOGIS_CENTRO_COSTOS(EMPRESA_CODIGO, CCOSTO_CODIGO,CCOSTO_COD_INTERNO, CCOSTO_DESCRIPCION,CCOSTO_SYS_EST,CCOSTO_PISO,CCOSTO_DIRECCION,CCOSTO_EDIFICIO,CCOSTO_UBICACION,CCOSTO_ACTIVO,CCOSTO_TIPO,CCOSTO_RUC,CCOSTO_DPTO,CCOSTO_PROVINCIA,CCOSTO_DISTRITO) " _
                                       & "VALUES('" & Session("CodEmpresa") & "'," & psCodCC & ",'" & Trim(TxtCodInterno.Text) & "','" & Trim(TxtDescripcion.Text) & "','0','" & Trim(TxtPiso.Text) & "','" & Trim(TxtDireccion.Text) & "','" & Trim(TxtEdificio.Text) & "','" & Trim(TxtUbicacion.Text) & "','S','" & DdlEstablecimiento.SelectedValue & "','" & Trim(TxtRuc.Text) & "'," & ps_dpto & "," & ps_prov & "," & ps_distr & ")"
                CmdGlobal2.ExecuteNonQuery()
                Cn2.Close()
            End If
            If divEditarCC.Visible = True Then
                psCodCC = LblCodCC.Text
                CmdGlobal.CommandText = "UPDATE TBLOGIS_CENTRO_COSTOS SET CCOSTO_COD_INTERNO='" & TxtCodInterno.Text & "', " _
                        & " CCOSTO_DESCRIPCION='" & TxtDescripcion.Text & "', " _
                        & " CCOSTO_PISO ='" & TxtPiso.Text & "', " _
                        & " CCOSTO_DIRECCION ='" & TxtDireccion.Text & "', " _
                        & " CCOSTO_EDIFICIO='" & TxtEdificio.Text & "', " _
                        & " CCOSTO_UBICACION='" & TxtUbicacion.Text & "', " _
                        & " CCOSTO_TIPO = " & ps_Establecimiento & "," _
                        & " CCOSTO_RUC='" & TxtRuc.Text & "', " _
                        & " CCOSTO_DPTO=" & ps_dpto & ", " _
                        & " CCOSTO_PROVINCIA=" & ps_prov & ", " _
                        & " CCOSTO_DISTRITO=" & ps_distr & " " _
                        & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND CCOSTO_CODIGO=" & psCodCC
                CmdGlobal.ExecuteNonQuery()
            End If
            BtnCCCancelar_Click(sender, e)
            BtnListarCC_Click(sender, e)
        Catch ex As SqlException
            LblCCError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            LblCCError.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        End Try
    End Sub
    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        DivSeccion.Visible = True
        BtnNuevo.Enabled = False
        BtnSecCancelar.Visible = True
        BtnSecGuardar.Visible = True
        TxtSecCodInt.Text = ""
        TxtSecDescrip.Text = ""
        TxtSecDireccion.Text = ""
        LblRegistro.Text = ""
        TxtSecEdificio.Text = ""
        TxtSecHall.Text = ""
        TxtSecPiso.Text = ""
        TxtSecRuc.Text = ""
        TxtSecUbicacion.Text = ""
        dvNuevaSec.Visible = True
        dvEditarSec.Visible = False
        DdlResponsable.SelectedValue = "< Seleccionar >"
        DdlModo.SelectedValue = "1"
        DdlSecEstablecimiento.SelectedValue = "< Seleccionar >"
        DdlDpto.Items.Clear()
        DdlProv.Items.Clear()
        DdlDist.Items.Clear()
        DdlDpto.Enabled = True
        DdlProv.Items.Add("< Seleccionar >") : DdlProv.SelectedValue = "< Seleccionar >"
        DdlProv.Enabled = False
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        DdlDist.Enabled = False
        Call LlenaComboItem("TBOPC002", DdlDpto)
    End Sub
    Protected Sub BtnSecCancelar_Click(sender As Object, e As EventArgs) Handles BtnSecCancelar.Click
        DivSeccion.Visible = False
        dvNuevaSec.Visible = False
        dvEditarSec.Visible = False
        BtnNuevo.Enabled = True
        BtnSecCancelar.Visible = False
        BtnSecGuardar.Visible = False
    End Sub
    Protected Sub BtnSecGuardar_Click(sender As Object, e As EventArgs) Handles BtnSecGuardar.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs2 As SqlDataReader
        Dim psExiste As String = ""
        Dim psCodCC As String = ""
        Dim ps_dpto As String = "NULL"
        Dim ps_prov As String = "NULL"
        Dim ps_distr As String = "NULL"
        Dim ps_Modo As String = "NULL"
        Dim ps_Establecimiento As String = "NULL"
        Try
            If DdlDpto.SelectedValue <> "< Seleccionar >" Then ps_dpto = DdlDpto.SelectedValue
            If DdlProv.SelectedValue <> "< Seleccionar >" Then ps_prov = DdlProv.SelectedValue
            If DdlDist.SelectedValue <> "< Seleccionar >" Then ps_distr = DdlDist.SelectedValue
            If DdlModo.SelectedValue <> "< Seleccionar >" Then ps_Modo = DdlModo.SelectedValue
            If DdlSecEstablecimiento.SelectedValue <> "< Seleccionar >" Then ps_Establecimiento = DdlSecEstablecimiento.SelectedValue
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Cn3.Open() : CmdGlobal3.Connection = Cn3
            LblSecError.Text = ""
            If TxtSecCodInt.Text.Trim = "" Then LblSecError.Text = LblSecError.Text & "<br> - Ingresar Codigo Interno de la Seccion."
            If TxtSecDescrip.Text.Trim = "" Then LblSecError.Text = LblSecError.Text & "<br> - Ingresar la descripcion de la Seccion."
            'If DdlResponsable.SelectedValue = "< Seleccionar >" Then LblSecError.Text = LblSecError.Text & "<br> - Seleccionar Responsable."
            If DdlModo.SelectedValue = "< Seleccionar >" Then LblSecError.Text = LblSecError.Text & "<br> - Seleccionar Modo."
            If dvNuevaSec.Visible = True Then
                CmdGlobal2.CommandText = " SELECT CECOSE_CODIGO FROM TBLOGIS_CENTRO_COSTO_SECCION " _
                                   & " WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND UPPER(CECOSE_COD_INTERNO)='" & UCase(TxtSecCodInt.Text) & "' AND (CECOSE_SYS_EST = '0')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        LblSecError.Text = LblSecError.Text & "<br> - El Codigo Interno ingresado ya existe."
                    End While
                End If
                Rs2.Close()

                CmdGlobal2.CommandText = " SELECT CECOSE_CODIGO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND UPPER(CECOSE_DESCRIPCION)='" & UCase(Trim(TxtSecDescrip.Text)) & "' AND (CECOSE_SYS_EST = '0')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        LblSecError.Text = LblSecError.Text & "<br> - La descripcion ingresada ya existe."
                    End While
                End If
                Rs2.Close()
                If LblSecError.Text <> "" Then
                    LblSecError.Text = LblSecError.Text
                    Exit Sub
                End If

                CmdGlobal2.CommandText = "SELECT MAX(CECOSE_CODIGO) FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        psCodCC = Nz(Rs2(0)) + 1
                    End While
                Else
                    psCodCC = "1"
                End If
                Rs2.Close()
                CmdGlobal2.CommandText = "INSERT INTO TBLOGIS_CENTRO_COSTO_SECCION(EMPRESA_CODIGO,CECOSE_CODIGO,CECOSE_COD_INTERNO,CECOSE_DESCRIPCION,CECOSE_SYS_EST,CCOSTO_CODIGO,CECOSE_PISO,CECOSE_DIRECCION,CECOSE_EDIFICIO,CECOSE_UBICACION,CECOSE_HALL,CECOSE_ACTIVO,CECOSE_TIPO,CECOSE_RUC,CECOSE_MODO_RECIBIR,CECOSE_DPTO,CECOSE_PROVINCIA,CECOSE_DISTRITO) " _
                                & "VALUES('" & Session("CodEmpresa") & "'," & psCodCC & ",'" & Trim(TxtSecCodInt.Text) & "','" & Trim(TxtSecDescrip.Text) & "','0','" & LblCodCentroCosto.Text & "','" & Trim(TxtSecPiso.Text) & "','" & Trim(TxtSecDireccion.Text) & "','" & Trim(TxtSecEdificio.Text) & "','" & Trim(TxtSecUbicacion.Text) & "','" & Trim(TxtSecHall.Text) & "','S'," & ps_Establecimiento & ",'" & Trim(TxtSecRuc.Text) & "'," & ps_Modo & "," & ps_dpto & "," & ps_prov & ", " & ps_distr & ")"
                CmdGlobal2.ExecuteNonQuery()
            End If
            If dvEditarSec.Visible = True Then
                psCodCC = LblCodSec.Text
                CmdGlobal.CommandText = "UPDATE TBLOGIS_CENTRO_COSTO_SECCION SET CECOSE_COD_INTERNO='" & TxtSecCodInt.Text & "',CECOSE_DESCRIPCION='" & TxtSecDescrip.Text & "'," _
                        & " CECOSE_TIPO = " & ps_Establecimiento & ", CECOSE_RUC='" & TxtSecRuc.Text & "', CECOSE_MODO_RECIBIR = " & ps_Modo & ", " _
                        & " CECOSE_PISO ='" & TxtSecPiso.Text & "', CECOSE_DIRECCION='" & TxtSecDireccion.Text & "', CECOSE_EDIFICIO='" & TxtSecEdificio.Text & "', " _
                        & " CECOSE_UBICACION='" & TxtSecUbicacion.Text & "',CECOSE_HALL='" & TxtSecHall.Text & "',CECOSE_DPTO=" & ps_dpto & ",CECOSE_PROVINCIA=" & ps_prov & ",CECOSE_DISTRITO=" & ps_distr & " " _
                        & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND CECOSE_CODIGO=" & psCodCC
                CmdGlobal.ExecuteNonQuery()
            End If
            If DdlResponsable.SelectedValue <> "< Seleccionar >" Then
                CmdGlobal2.CommandText = " UPDATE TBLOGIS_CENTRO_COSTO_SECCION " _
                              & " SET CECOSE_USUARIO_AUTORIZA = '" & DdlResponsable.SelectedValue & "' " _
                              & " WHERE CECOSE_CODIGO = " & psCodCC
                CmdGlobal2.ExecuteNonQuery()
            End If
            Cn2.Close()
            BtnSecCancelar_Click(sender, e)
            Call ListarCC_Seccion()
        Catch ex As SqlException
            LblSecError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            LblSecError.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        End Try
    End Sub

    Private Sub GvSeccion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvSeccion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim codigo As Double = Nz(LblCodCentroCosto.Text)
        Dim psCodInterno As String = (GvSeccion.Rows(Index).Cells(3).Text)
        Dim psDescripcion As String = (GvSeccion.Rows(Index).Cells(4).Text)
        Dim psCodSec As Double = Nz(GvSeccion.Rows(Index).Cells(5).Text)
        Dim dt As New DataTable
        DdlSecEstablecimiento.SelectedValue = "< Seleccionar >"
        DdlModo.SelectedValue = "< Seleccionar >"
        DdlResponsable.SelectedValue = "< Seleccionar >"
        If e.CommandName = "Eliminar" Then
            dt = obj.Eliminar_Centro_Costos_Seccion(Session("Ruta_Emp"), Session("CodEmpresa"), 0, psCodSec)
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    If Nu(drow("Mensaje")) = "S" And Nu(drow("Mensaje2")) = "S" And Nu(drow("Mensaje3")) = "S" Then
                        Mensaje.Text = "No se puede eliminar la Seccion " & psDescripcion & " porque tiene Recepcion y Salidad de Equipos."
                    ElseIf Nu(drow("Mensaje")) = "S" And Nu(drow("Mensaje2")) = "S" And Nu(drow("Mensaje3")) = "" Then
                        Mensaje.Text = "No se puede eliminar la Seccion " & psDescripcion & " porque tiene Recepcion de Equipos."
                    ElseIf Nu(drow("Mensaje")) = "S" And Nu(drow("Mensaje2")) = "" And Nu(drow("Mensaje3")) = "" Then
                        Mensaje.Text = "No se puede eliminar la Seccion " & psDescripcion & " porque tiene Recepcion de Equipos."
                    ElseIf Nu(drow("Mensaje")) = "" And Nu(drow("Mensaje2")) = "S" And Nu(drow("Mensaje3")) = "S" Then
                        Mensaje.Text = "No se puede eliminar la Seccion " & psDescripcion & " porque tiene Recepcion y Salida de Equipos."
                    ElseIf Nu(drow("Mensaje")) = "" And Nu(drow("Mensaje2")) = "" And Nu(drow("Mensaje3")) = "S" Then
                        Mensaje.Text = "No se puede eliminar la Seccion " & psDescripcion & " porque tiene Salida de Equipos."
                    Else
                        Mensaje.Text = "No se puede eliminar la Seccion " & psDescripcion
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeSec').modal('show');", True)
                Next
            End If
            dt = Nothing
            Call ListarCC_Seccion()
        ElseIf e.CommandName = "Editar" Then
            dvNuevaSec.Visible = False
            dvEditarSec.Visible = True
            DivSeccion.Visible = True
            LblCodSec.Text = psCodSec
            TxtSecCodInt.Text = psCodInterno
            TxtSecDescrip.Text = psDescripcion
            DdlDpto.Items.Clear()
            DdlProv.Items.Clear()
            DdlDist.Items.Clear()
            DdlDpto.Enabled = True
            DdlProv.Items.Add("< Seleccionar >") : DdlProv.SelectedValue = "< Seleccionar >"
            DdlProv.Enabled = False
            DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
            DdlDist.Enabled = False
            Call LlenaComboItem("TBOPC002", DdlDpto)
            dt = obj.Busca_Centro_Costos_Seccion_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), codigo, psCodSec)
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    If Nz(drow("CECOSE_TIPO")) > 0 Then DdlSecEstablecimiento.SelectedValue = Nz(drow("CECOSE_TIPO"))
                    TxtSecDireccion.Text = Nu(drow("CECOSE_DIRECCION"))
                    TxtSecEdificio.Text = Nu(drow("CECOSE_EDIFICIO"))
                    TxtSecPiso.Text = Nu(drow("CECOSE_PISO"))
                    TxtSecRuc.Text = Nu(drow("CECOSE_RUC"))
                    TxtSecUbicacion.Text = Nu(drow("CECOSE_UBICACION"))
                    TxtSecHall.Text = Nu(drow("CECOSE_HALL")) ' 
                    If Nz(drow("CECOSE_USUARIO_AUTORIZA")) > 0 Then
                        DdlResponsable.SelectedValue = Nz(drow("CECOSE_USUARIO_AUTORIZA"))
                    End If
                    If Nz(drow("CECOSE_MODO_RECIBIR")) > 0 Then DdlModo.SelectedValue = Nz(drow("CECOSE_MODO_RECIBIR"))
                    If drow("XDPTO").ToString <> "" Then
                        DdlDpto.SelectedValue = Nu(drow("XDPTO")) : DdlDpto_SelectedIndexChanged(sender, e)
                        If drow("XPROV").ToString <> "" Then
                            DdlProv.SelectedValue = drow("XPROV")
                            DdlProv_SelectedIndexChanged(sender, e)
                            If drow("XDIST").ToString <> "" Then DdlDist.SelectedValue = drow("XDIST")
                        End If
                    End If
                Next
            End If
            BtnNuevo.Enabled = False
            BtnSecCancelar.Visible = True
            BtnSecGuardar.Visible = True
        End If
    End Sub
    Private Sub BtnSi_Click(sender As Object, e As EventArgs) Handles BtnSi.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensaje').modal('hide');", True)
    End Sub

    Private Sub BtnCerrarSec_Click(sender As Object, e As EventArgs) Handles BtnCerrarSec.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeSec').modal('hide');", True)
    End Sub
    Private Sub DdlDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDpto.SelectedIndexChanged
        DdlProv.Items.Clear()
        DdlDist.Items.Clear()
        DdlProv.Enabled = False
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        DdlDist.Enabled = False
        If DdlDpto.SelectedIndex = -1 Or DdlDpto.Items.Count = 0 Then Exit Sub
        If DdlDpto.Items(DdlDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlProv, Left(DdlDpto.SelectedValue, 2), "PR")
        If DdlDpto.SelectedValue <> "< Seleccionar >" Then DdlProv.Enabled = True
    End Sub

    Private Sub DdlProv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProv.SelectedIndexChanged
        DdlDist.Items.Clear()
        DdlDist.Enabled = False
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        If DdlProv.SelectedIndex = -1 Or DdlProv.Items.Count = 0 Then Exit Sub
        If DdlProv.Items(DdlProv.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlDist, Left(DdlDpto.SelectedValue, 2) + Mid(DdlProv.SelectedValue, 3, 2), "DS")
        DdlDist.Items.Add("< Seleccionar >") : DdlDist.SelectedValue = "< Seleccionar >"
        If DdlProv.SelectedValue <> "< Seleccionar >" Then DdlDist.Enabled = True
    End Sub

    Private Sub DdlDptoCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDptoCC.SelectedIndexChanged
        DdlProvCC.Items.Clear()
        DdlDistCC.Items.Clear()
        DdlProvCC.Enabled = False
        DdlDistCC.Items.Add("< Seleccionar >") : DdlDistCC.SelectedValue = "< Seleccionar >"
        DdlDistCC.Enabled = False
        If DdlDptoCC.SelectedIndex = -1 Or DdlDptoCC.Items.Count = 0 Then Exit Sub
        If DdlDptoCC.Items(DdlDptoCC.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlProvCC, Left(DdlDptoCC.SelectedValue, 2), "PR")
        If DdlDptoCC.SelectedValue <> "< Seleccionar >" Then DdlProvCC.Enabled = True
    End Sub

    Private Sub DdlProvCC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProvCC.SelectedIndexChanged
        DdlDistCC.Items.Clear()
        DdlDistCC.Enabled = False
        DdlDistCC.Items.Add("< Seleccionar >") : DdlDistCC.SelectedValue = "< Seleccionar >"
        If DdlProvCC.SelectedIndex = -1 Or DdlProvCC.Items.Count = 0 Then Exit Sub
        If DdlProvCC.Items(DdlProvCC.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlDistCC, Left(DdlDptoCC.SelectedValue, 2) + Mid(DdlProvCC.SelectedValue, 3, 2), "DS")
        DdlDistCC.Items.Add("< Seleccionar >") : DdlDistCC.SelectedValue = "< Seleccionar >"
        If DdlProvCC.SelectedValue <> "< Seleccionar >" Then DdlDistCC.Enabled = True
    End Sub
End Class
