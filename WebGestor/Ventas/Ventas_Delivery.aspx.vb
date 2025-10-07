Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports WebGestor
Imports System.Math
Imports System.Net
Partial Class Ventas_Ventas_Delivery
    Inherits System.Web.UI.Page
    Dim ObjSeg As New ModuloSeguridad
    Dim ObjVentas As New ClsVentas_Listados
    Dim ObjInv As New clsInv_Listados
    Dim ObjCont As New clsCont_Funciones
    Dim ObjContList As New clsCont_Listados
    Dim objContInsUpd As New ClsCont_InsUpdDel
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call LlenaComboItem("tbopc514", DdlFormaPago)
            Call LlenaComboItem("tbopc515", DdlTipoTarj)
            Call LlenaComboItem("TBOPC002", DdlDpto)
            DdlDpto.SelectedValue = "150000" : DdlDpto_SelectedIndexChanged(sender, e)
            ObjVentas.Cargar_TipoDocumento(DdlTipoDoc, Session("CodEmpresa"), Session("Ruta_Emp"), AñoActual(Session("CodEmpresa"), Session("Ruta_Emp")))
            Call Limpiar(sender, e)
        End If
    End Sub
    Private Sub DdlDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDpto.SelectedIndexChanged
        DdlProvincia.Items.Clear()
        DdlDistrito.Items.Clear()
        DdlProvincia.Enabled = False
        DdlDistrito.Items.Add("< Seleccionar >") : DdlDistrito.SelectedValue = "< Seleccionar >"
        DdlDistrito.Enabled = False
        If DdlDpto.SelectedIndex = -1 Or DdlDpto.Items.Count = 0 Then Exit Sub
        If DdlDpto.Items(DdlDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlProvincia, Left(DdlDpto.SelectedValue, 2), "PR")
        DdlProvincia.Items.Add("< Seleccionar >") : DdlProvincia.SelectedValue = "< Seleccionar >"
        If DdlDpto.SelectedValue = "150000" Then DdlProvincia.SelectedValue = "150100"
        If DdlDpto.SelectedValue <> "< Seleccionar >" Then DdlProvincia.Enabled = True
    End Sub

    Private Sub DdlProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProvincia.SelectedIndexChanged
        DdlDistrito.Items.Clear()
        DdlDistrito.Enabled = False
        DdlDistrito.Items.Add("< Seleccionar >") : DdlDistrito.SelectedValue = "< Seleccionar >"
        If DdlProvincia.SelectedIndex = -1 Or DdlProvincia.Items.Count = 0 Then Exit Sub
        If DdlProvincia.Items(DdlProvincia.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlDistrito, Left(DdlDpto.SelectedValue, 2) + Mid(DdlProvincia.SelectedValue, 3, 2), "DS")
        DdlDistrito.Items.Add("< Seleccionar >") : DdlDistrito.SelectedValue = "< Seleccionar >"
        If DdlProvincia.SelectedValue <> "< Seleccionar >" Then DdlDistrito.Enabled = True
    End Sub

    Private Sub DdlFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlFormaPago.SelectedIndexChanged
        If DdlFormaPago.SelectedValue = "1" Then
            LblEtq_20.Visible = True : LblEtq_20.Text = "Efectivo"
            TxtEfectivo.Text = "0.00" : TxtEfectivo.ReadOnly = False : TxtEfectivo.Visible = True
            DdlTipoTarj.Visible = False
        Else
            TxtEfectivo.Text = "0.00" : TxtEfectivo.ReadOnly = False : TxtEfectivo.Visible = False
            DdlTipoTarj.Visible = True
            LblEtq_20.Visible = True : LblEtq_20.Text = "Tipo"
        End If
    End Sub

    Private Sub DdlTipoDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoDoc.SelectedIndexChanged
        If DdlTipoDoc.SelectedValue = "01" Then
            TxtBusRuc.Text = ""
            txtBusRazon.Text = ""
            lblFCodCliente.Text = ""
            btnBusRuc.Enabled = True
        End If
    End Sub

    Private Sub TxtTelefono_TextChanged(sender As Object, e As EventArgs) Handles TxtTelefono.TextChanged
        Try
            LblError.Text = ""
            Call Consultar_Telef_Dni(sender, e, TxtTelefono.Text)
        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        End Try
    End Sub

    Private Sub Consultar_Telef_Dni(sender As Object, e As EventArgs, ByVal psTelefono As String, Optional psNroDni As String = "")
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsT As SqlDataReader

        Cn.Open() : Cn2.Open() : Cn3.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3

        Dim ProvP As String = ""
        Dim DistP As String = ""

        TxtFecNac.Text = FormatoFecha(FechaActual)

        If psTelefono = "" And psNroDni = "" Then LblError.Text = "Ingresar una busqueda por teléfono o DNI." : Exit Sub

        Dim psCodPersona As String : psCodPersona = ""
        Dim psCargarTelefono As String : psCargarTelefono = ""

CargarNuevamente:


        CmdGlobal.CommandText = " SELECT PERSONA_CODIGO,PERSONA_EXTRANJERO, PERSONA_REFERENCIA, PERSONA_DIAS_CREDITO,PERSONA_TIPO,(SELECT ELEMEN_VALOR From BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC001' AND ELEMEN_CODIGO = PERSONA_TIPO) AS PTIPO,PERSONA_TIPO_CLIENTE," _
            & " PERSONA_RUC, PERSONA_RAZON_SOCIAL, PERSONA_APEPAT, PERSONA_APEMAT, PERSONA_NOMBRES, PERSONA_DIRECCION,PERSONA_FORMA_PAGO, PERSONA_EMAIL, PERSONA_FECHANAC, " _
            & " PERSONA_PAIS,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC006' AND ELEMEN_CODIGO = PERSONA_PAIS) AS PPAIS," _
            & " PERSONA_DPTO,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC002' AND ELEMEN_CODIGO = PERSONA_DPTO) AS PDPTO," _
            & " PERSONA_PROV,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC003' AND ELEMEN_CODIGO = PERSONA_PROV) AS PPROV," _
            & " PERSONA_DIST,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) AS PDIST," _
            & " PERSONA_EMAIL,PERSONA_EMAIL2,PERSONA_WEB,PERSONA_WEB2,PERSONA_PROVEE, PERSONA_NOMBRE_CONTACTO,PERSONA_TELF1, PERSONA_TELF2, PERSONA_TELF_OF,PERSONA_ANEXO_OF, PERSONA_TELF_CELULAR,PERSONA_FAX1, PERSONA_FAX2, " _
            & " PERSONA_CATEGORIA,(SELECT ELEMEN_VALOR From BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC005' AND ELEMEN_CODIGO = PERSONA_CATEGORIA) AS PCATEG " _
            & " From TBDATA_PERSONAS WHERE (PERSONA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
        If psCodPersona <> "" Then
            CmdGlobal.CommandText = CmdGlobal.CommandText & " AND PERSONA_CODIGO = " & psCodPersona & ""
        Else
            If psTelefono <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND (PERSONA_TELF1 = '" & psTelefono & "')"
            If psNroDni <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND (PERSONA_RUC = '" & psNroDni & "')"
        End If
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows = True Then
            While Rs.Read
                TxtApePat.Text = Nu(Rs!PERSONA_APEPAT)
                TxtApeMat.Text = Nu(Rs!PERSONA_APEMAT)
                TxtNombres.Text = Nu(Rs!PERSONA_NOMBRES)
                TxtRuc.Text = Nu(Rs!PERSONA_RUC)
                TxtRazonSocial.Text = Nu(Rs!PERSONA_RAZON_SOCIAL)
                lblCodCliente.Text = Nu(Rs!PERSONA_CODIGO)
                TxtBusRuc.Text = Nu(Rs!PERSONA_RUC)
                txtBusRazon.Text = Nu(Rs!PERSONA_RAZON_SOCIAL)
                lblFCodCliente.Text = Nu(Rs!PERSONA_CODIGO)
                TxtEmail.Text = Nu(Rs!PERSONA_EMAIL)
                CmdGlobal2.CommandText = " SELECT DATAPERSONA_N_CODIGO, DIRECCION_N_CODIGO, DIRECCION_C_DESCRIPCION, DIRECCION_C_PAIS, DIRECCION_C_DPTO, " _
                                        & " DIRECCION_C_PROV , DIRECCION_C_DIST, DIRECCION_C_PRINCIPAL, DIRECCION_C_TELEFONO, DIRECCION_C_REFERENCIA, " _
                                        & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC006' AND ELEMEN_CODIGO = DIRECCION_C_PAIS) AS PPAIS," _
                                        & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC002' AND ELEMEN_CODIGO = DIRECCION_C_DPTO) AS PDPTO," _
                                        & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC003' AND ELEMEN_CODIGO = DIRECCION_C_PROV) AS PPROV," _
                                        & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = DIRECCION_C_DIST) AS PDIST" _
                                        & " From TBDATA_PERSONAS_DIRECCION " _
                                        & " WHERE DATAPERSONA_N_CODIGO = " & Nz(Rs!PERSONA_CODIGO)
                If TxtTelefono.Text = "" Then CmdGlobal2.CommandText = CmdGlobal2.CommandText & " and DIRECCION_C_DESCRIPCION = '" & Nu(Rs!PERSONA_DIRECCION) & "'"
                If TxtTelefono.Text <> "" Then CmdGlobal2.CommandText = CmdGlobal2.CommandText & " and DIRECCION_C_TELEFONO = '" & TxtTelefono.Text & "'"
                RsT = CmdGlobal2.ExecuteReader
                If RsT.HasRows = True Then
                    While RsT.Read
                        TxtTelefono.Text = Nu(RsT!DIRECCION_C_TELEFONO)
                        TxtDireccion.Text = Nu(RsT!DIRECCION_C_DESCRIPCION)
                        TxtReferencia.Text = Nu(RsT!DIRECCION_C_REFERENCIA)
                        DdlDistrito.Items.Clear()
                        DdlProvincia.Items.Clear()
                        If Nu(RsT!DIRECCION_C_DPTO) <> "" Then DdlDpto.SelectedValue = Nu(RsT!DIRECCION_C_DPTO)
                        If DdlDpto.SelectedValue <> "< Seleccionar >" Then DdlDpto_SelectedIndexChanged(sender, e)
                        ProvP = Nu(RsT("DIRECCION_C_PROV"))
                        DistP = Nu(RsT("DIRECCION_C_DIST"))
                    End While
                End If
                RsT.Close()
                If Nu(Rs!PERSONA_FECHANAC) <> "" Then TxtFecNac.Text = FormatoFecha(Nu(Rs!PERSONA_FECHANAC))
                If ProvP = "" And DistP = "" Then
                    TxtDireccion.Text = Nu(Rs!PERSONA_DIRECCION)
                    TxtReferencia.Text = Nu(Rs!PERSONA_REFERENCIA)
                    DdlDistrito.Items.Clear()
                    DdlProvincia.Items.Clear()
                    If Nu(Rs!PERSONA_DPTO) <> "" Then DdlDpto.SelectedValue = Nu(Rs!PERSONA_DPTO)
                    ProvP = Nu(Rs("PERSONA_PROV"))
                    DistP = Nu(Rs("PERSONA_DIST"))
                End If
                If DdlDpto.SelectedValue <> "< Seleccionar >" Then DdlDpto_SelectedIndexChanged(sender, e)
                If ProvP <> "" Then DdlProvincia.SelectedValue = ProvP
                If DdlProvincia.SelectedValue <> "< Seleccionar >" Then DdlProvincia_SelectedIndexChanged(sender, e)
                If DistP <> "" Then DdlDistrito.SelectedValue = DistP
            End While
            Rs.Close()
        Else
            Rs.Close()
            CmdGlobal3.CommandText = " SELECT TOP 1 PERSONA_CODIGO FROM TBVENTAS_DELIVERY WHERE DELIVERY_TELEFONO = '" & psTelefono & "' "
            Rs = CmdGlobal3.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    psCodPersona = Nu(Rs!PERSONA_CODIGO) : psCargarTelefono = "NO"
                End While
            End If
            Rs.Close()
            If psCodPersona <> "" Then GoTo CargarNuevamente
            If psTelefono <> "" Then LblError.Text = "No se encuentra el teléfono." : Call Limpiar(sender, e) : Exit Sub
            If psNroDni <> "" Then LblError.Text = "No se encuentra la persona." : Call Limpiar(sender, e) : TxtRuc.Text = psNroDni : Exit Sub
        End If
    End Sub
    Private Sub Limpiar(sender As Object, e As EventArgs)
        TxtRuc.Text = ""
        lblCodCliente.Text = ""
        TxtApeMat.Text = ""
        TxtApePat.Text = ""
        TxtNombres.Text = ""
        TxtDireccion.Text = ""
        DdlDpto.SelectedValue = "150000" : DdlDpto_SelectedIndexChanged(sender, e)
        DdlProvincia.SelectedValue = "150100" : DdlProvincia_SelectedIndexChanged(sender, e)
        TxtReferencia.Text = ""
        TxtRazonSocial.Text = ""
        TxtEfectivo.Text = "0"
        TxtTimeAprox.Text = "0"
        DdlFormaPago.SelectedValue = "< Seleccionar >"
        DdlTipoTarj.SelectedValue = "< Seleccionar >"
        DdlTipoDoc.SelectedValue = "< Seleccionar >"
        'txtObs.Text = ""
        TxtDCant.Text = 0
        TxtBusArt.Text = ""
        TxtBusArtCodigo.Text = ""
        TxtDTotal.Text = "0.00"
        TxtDSubTotal.Text = "0.00"
        TxtDIgv.Text = "0.00"
        TxtEmail.Text = ""
        TxtFecNac.Text = FormatoFecha(FechaActual)
        TxtBusRuc.Text = ""
        txtBusRazon.Text = ""
        lblFCodCliente.Text = ""

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " select max(delivery_codigo) from tbventas_delivery "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows = True Then
            While Rs.Read
                TxtNroDelivery.Text = Llenar_Ceros(Nz(Rs(0)) + 1, 5)
            End While
        Else
            TxtNroDelivery.Text = "00001"
        End If
        Rs.Close()
        TxtDFecha.Text = FormatoFecha(FechaActual)

        Dim dt As New DataTable
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
        GvBusArt.DataSource = dt
        GvBusArt.DataBind()
        GvStock.DataSource = dt
        GvStock.DataBind()
        LblMensajeError.Text = ""
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Call Limpiar(sender, e)
    End Sub

    Private Sub BtnBuscarArt_Click(sender As Object, e As EventArgs) Handles BtnBuscarArt.Click
        Dim pdCodArticulo As Double = 0
        Dim dt As New DataTable
        dt = Nothing
        LblError.Text = ""
        Try
            GvBusArt.DataSource = dt
            GvBusArt.DataBind()
            Call Lista_Clasificaciones_Activas()
            If TxtBusArtCodigo.Text <> "" Then pdCodArticulo = TxtBusArtCodigo.Text
            dt = ObjInv.Inv_Lista_Articulos_xCodigo_xDescripcion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodArticulo, TxtBusArt.Text)
            GvBusArt.DataSource = dt
            GvBusArt.DataBind()
            DivFlex.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('show');", True)
        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        Finally
        End Try
    End Sub

    Private Sub Lista_Clasificaciones_Activas()
        Dim psCodClas As String = ""
        Dim SqlClas As String = ""

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn4 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn5 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn6 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim CmdGlobal5 As New SqlCommand
        Dim CmdGlobal6 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Rs3 As SqlDataReader
        Cn5.Open() : Cn6.Open() : CmdGlobal5.Connection = Cn5 : CmdGlobal6.Connection = Cn6
        Cn.Open() : Cn2.Open() : Cn3.Open() : Cn4.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3 : CmdGlobal4.Connection = Cn4

        Try

            If Existe_Tabla("[V_TBINV_CLASIF_ACTIVAS]", Session("Ruta_Emp")) = False Then
                CmdGlobal5.CommandText = " CREATE TABLE [V_TBINV_CLASIF_ACTIVAS] ([CLAS_CODIGO] [FLOAT] NOT NULL) "
                CmdGlobal5.ExecuteNonQuery()
            End If

            CmdGlobal6.CommandText = " DELETE FROM V_TBINV_CLASIF_ACTIVAS "
            CmdGlobal6.ExecuteNonQuery()

            CmdGlobal.CommandText = " SELECT  CLAS_CODIGO FROM TBVENTAS_ACTIVAR_CLASIFICACION  "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    CmdGlobal2.CommandText = " SELECT CLAS_COD_NIVEL From TBINV_ARTICULO_CLASIFICACION Where (CLAS_CODIGO = " & Nu(Rs!CLAS_CODIGO) & ")"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows = True Then
                        While Rs2.Read
                            CmdGlobal3.CommandText = " SELECT CLAS_CODIGO From TBINV_ARTICULO_CLASIFICACION Where (CLAS_NIVEL" & Nu(Rs2!CLAS_COD_NIVEL) & " = " & Nu(Rs!CLAS_CODIGO) & ")"
                            Rs3 = CmdGlobal3.ExecuteReader
                            If Rs3.HasRows = True Then
                                While Rs3.Read
                                    CmdGlobal4.CommandText = " INSERT INTO V_TBINV_CLASIF_ACTIVAS (CLAS_CODIGO) VALUES (" & Nu(Rs3!CLAS_CODIGO) & ") "
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            End If
                            Rs3.Close()
                        End While
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()

        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        Finally
        End Try

    End Sub

    Private Sub GvBusArt_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArt.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dtListado As New DataTable
        Dim dtStock As New DataTable
        Dim psPrecioVenta As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn4 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim StockActual As Double : StockActual = 0
        Dim cant As Double : cant = 0
        Dim a As Integer = 0
        dtListado.Columns.Add("ART_DESCRIPCION")
        dtListado.Columns.Add("ART_CODIGO")
        dtListado.Columns.Add("STOCK_ACTUAL") '
        dtListado.Columns.Add("CANT")
        dtListado.Columns.Add("PRECIO")
        dtListado.Columns.Add("TOTAL")
        dtListado.Columns.Add("PRECIO_SINIGV")
        dtListado.Columns.Add("PRECIO_IGV")
        dtListado.Columns.Add("ART_COMPUESTO")

        dtStock.Columns.Add("ART_DESCRIPCION")
        dtStock.Columns.Add("ART_CODIGO")
        dtStock.Columns.Add("STOCK_ACTUAL") '
        dtStock.Columns.Add("STOCK_UTILIZADO")
        dt = Nothing
        LblMensajeError.Text = ""
        LblError.Text = ""
        LblMensajeError2.Text = ""
        Dim pdCodart As Double = 0
        Try
            psPrecioVenta = ObjCont.Hallar_Valor_Venta(Session("Ruta_Emp"), FechaActual)
            If e.CommandName = "Seleccionar" Then
                pdCodart = GvBusArt.Rows(index).Cells(2).Text
                For i = 0 To Flex.Rows.Count - 1
                    If Flex.Rows(i).Cells(2).Text = GvBusArt.Rows(index).Cells(2).Text Then
                        LblMensajeError2.Text = "Articulo ya Ingresado" : Exit Sub
                    End If
                Next

                Dim drT As DataRow
                Dim drS As DataRow

                For i = 0 To Flex.Rows.Count - 1
                    Dim txtCantidad As TextBox = Flex.Rows(i).Cells(4).FindControl("txtCant")
                    drT = dtListado.NewRow()
                    drT("ART_DESCRIPCION") = Flex.Rows(i).Cells(1).Text
                    drT("ART_CODIGO") = Flex.Rows(i).Cells(2).Text
                    drT("STOCK_ACTUAL") = Flex.Rows(i).Cells(3).Text
                    drT("CANT") = txtCantidad.Text ' Flex.Rows(i).Cells(4).Text
                    drT("PRECIO") = Flex.Rows(i).Cells(5).Text
                    drT("TOTAL") = Flex.Rows(i).Cells(6).Text
                    drT("PRECIO_SINIGV") = Flex.Rows(i).Cells(7).Text
                    drT("PRECIO_IGV") = Flex.Rows(i).Cells(8).Text
                    drT("ART_COMPUESTO") = Flex.Rows(i).Cells(9).Text
                    dtListado.Rows.Add(drT)
                Next

                drT = dtListado.NewRow()
                drT("ART_DESCRIPCION") = GvBusArt.Rows(index).Cells(1).Text
                drT("ART_CODIGO") = GvBusArt.Rows(index).Cells(2).Text
                drT("ART_COMPUESTO") = GvBusArt.Rows(index).Cells(5).Text

                StockActual = ObjVentas.Obtener_StockActual_xCodArt(Session("Ruta_Emp"), pdCodart)
                If StockActual > 0 Then
                    If GvBusArt.Rows(index).Cells(5).Text = "SI" Then
                        drT("CANT") = "1"
                    Else
                        drT("STOCK_ACTUAL") = StockActual
                        drT("CANT") = "1"
                        a = 2
                        For i = 0 To GvStock.Rows.Count - 1
                            If GvStock.Rows(i).Cells(1).Text = Nz(drT("ART_CODIGO")) Then
                                cant = GvStock.Rows(i).Cells(2).Text : a = 1 : Exit For
                            Else
                                a = 2
                            End If
                        Next

                        If StockActual - cant > 0 And a = 2 Then
                            drS = dtStock.NewRow()
                            drS("ART_DESCRIPCION") = GvBusArt.Rows(index).Cells(1).Text
                            drS("ART_CODIGO") = GvBusArt.Rows(index).Cells(2).Text
                            drS("STOCK_ACTUAL") = StockActual
                            dtStock.Rows.Add(drS)
                            GvStock.DataSource = dtStock
                            GvStock.DataBind()
                        ElseIf StockActual > 0 Then
                            drT("STOCK_ACTUAL") = StockActual - cant
                        Else
                            If GvBusArt.Rows(index).Cells(6).Text <> 92 Then
                                drT("ART_DESCRIPCION") = ""
                                drT("ART_CODIGO") = ""
                                LblError.Text = "Articulo no se encuentra en Stock" : Exit Sub
                            ElseIf GvBusArt.Rows(index).Cells(6).Text = 92 Then
                                drT("STOCK_ACTUAL") = 0
                            End If
                        End If
                    End If
                Else

                End If

                Dim Moneda As String = "2"
                Dim pMoneda As String = ""
                Dim pVenta As Double = 0
                Dim pVentaIgv As Double = 0
                Dim pdPrecioCosto As Double = 0
                Dim pdPrecioIgv As Double = 0
                Dim pdPrecioSinIgv As Double = 0
                Dim pdPrecioTotal As Double = 0
                Dim pdValorIgv As Double = 0
                pdValorIgv = ObjVentas.Obtener_ValorIgv(Ruta_GrEmp)

                dt2 = ObjVentas.PrecioVenta_xCodArt(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodart)

                If dt2.Rows.Count > 0 Then
                    For Each drPrecio As DataRow In dt2.Rows
                        Moneda = "2"
                        pMoneda = Nu(drPrecio!PRECIO_MONEDA)
                        pVenta = Nz(drPrecio!PRECIO_VENTA)
                        pVentaIgv = Nz(drPrecio!PRECIO_VENTA_IGV)
                        If Moneda = "1" Then
                            If pMoneda = "1" Then
                                If Nz(psPrecioVenta) = 0 And pVenta = 0 Then LblMensajeError2.Text = "No Existe Tipo de Cambio." : Exit Sub
                            Else
                                If Nz(psPrecioVenta) = 0 Then LblMensajeError2.Text = "No Existe Tipo de Cambio." : Exit Sub
                            End If
                        Else
                            If pMoneda = "1" Then If Nz(psPrecioVenta) = 0 Then LblMensajeError2.Text = "No Existe Tipo de Cambio." : Exit Sub
                        End If
                        If pVentaIgv = 0 Then
                            pdPrecioCosto = PrecioCosto(Moneda, pMoneda, pVenta, psPrecioVenta)
                        Else
                            pdPrecioCosto = PrecioCosto(Moneda, pMoneda, pVentaIgv, psPrecioVenta)
                        End If
                        drT("PRECIO") = pdPrecioCosto
                        pdPrecioSinIgv = Round(pdPrecioCosto / (pdValorIgv + 1), 2)
                        drT("PRECIO_SINIGV") = Round(pdPrecioCosto / (pdValorIgv + 1), 2)
                        drT("PRECIO_IGV") = Round(pdPrecioCosto - pdPrecioSinIgv, 2)
                        drT("TOTAL") = Round(pdPrecioCosto * CDbl(Nz(drT("CANT"))), 2)
                    Next
                Else
                    LblMensajeError2.Text = "Articulo no tiene Precio." : Exit Sub
                End If

                dtListado.Rows.Add(drT)
                Flex.DataSource = dtListado
                Flex.DataBind()
                If dtListado.Rows.Count = 1 Then LblRegistro.Text = "1 Producto"
                If dtListado.Rows.Count > 1 Then LblRegistro.Text = dtListado.Rows.Count & " Productos"

                drT = dtListado.NewRow()

                Call Calculo_Totales
            End If

            dt = Nothing

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('hide');", True)
            GvBusArt.DataSource = dt
            GvBusArt.DataBind()

        Catch ex As SqlException
            LblMensajeError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblMensajeError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        End Try
    End Sub
    Private Sub Calculo_Totales()
        Dim Total As Double : Total = 0
        Dim SubTotal As Double : SubTotal = 0
        Dim Igv As Double : Igv = 0
        Dim psCant As Double : psCant = 0
        Dim psColCant As String = ""
        With Flex
            For i = 0 To .Rows.Count - 1
                psColCant = "txtCant"
                Dim txtCant As TextBox = .Rows(i).Cells(4).FindControl(psColCant)
                Total = Total + CDbl(Nz(.Rows(i).Cells(6).Text))
                SubTotal = SubTotal + (CDbl(Nz(.Rows(i).Cells(7).Text)) * CDbl(Nz(txtCant.Text)))
                Igv = Igv + (CDbl(Nz(.Rows(i).Cells(8).Text)) * CDbl(Nz(txtCant.Text)))
                psCant = psCant + Nz(txtCant.Text) ' Nz(.Rows(i).Cells(4).Text)
            Next
        End With
        TxtDTotal.Text = Round(Total, 2)
        TxtDSubTotal.Text = Round(SubTotal, 2)
        TxtDIgv.Text = Round(Igv, 2)
        TxtDCant.Text = psCant
    End Sub
    Private Sub Btnagregar_Click(sender As Object, e As EventArgs) Handles Btnagregar.Click
        TituloPopupp.Text = "Búsqueda de Productos"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('show');", True)
    End Sub

    Private Sub BtnCerrarModal_Click(sender As Object, e As EventArgs) Handles BtnCerrarModal.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('hide');", True)
    End Sub

    Protected Sub txtCant_TextChanged(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        Dim psCantEval As Integer = 0
        Dim Colum As Integer = 0
        Dim i As Integer = 0
        LblMensajeError.Text = ""
        Dim precioTotal As Double = 0
        Dim psColCant As String = ""
        Dim Total As Double : Total = 0

        Dim textBox As TextBox = CType(sender, TextBox)
        Dim cantidad As String = (CType((sender), TextBox)).Text.Trim()
        Dim currentRow As GridViewRow = CType((CType(sender, TextBox)).Parent.Parent, GridViewRow)

        Try

            If cantidad > 0 Then
                If Nz(cantidad) <= Nz(currentRow.Cells(3).Text) Then
                    Total= CDbl(currentRow.Cells(5).Text) * CDbl(Nz(cantidad))

                    currentRow.Cells(6).Text = Round(Total, 2)

                    Call Calculo_Totales()
                Else
                    LblMensajeError.Text = "La cantidad a ingresar debe ser menor al stock."
                End If
            End If
        Catch ex As SqlException
            LblMensajeError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblMensajeError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        End Try

    End Sub
    Function PrecioCosto(ByVal monedacot As String, ByVal Moneda As String, ByVal Precio As Double, ByVal TipoCambio As String) As Double
        PrecioCosto = 0
        If monedacot = "1" Then
            If Moneda = "1" Then
                PrecioCosto = Precio
            Else
                PrecioCosto = Precio / Nz(TipoCambio)
            End If
        Else
            If Moneda = "1" Then
                PrecioCosto = Precio * Nz(TipoCambio)
            Else
                PrecioCosto = Precio
            End If
        End If
        PrecioCosto = Round(PrecioCosto, 2)
        Return PrecioCosto
    End Function

    Private Sub BtnRegistrar_Click(sender As Object, e As EventArgs) Handles BtnRegistrar.Click

        Dim Cod As String = ""
        Dim psTipoDoc As String : psTipoDoc = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim psFecNacimiento As String = ""
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim RsG As SqlDataReader
        Dim RsG2 As SqlDataReader

        If TxtTelefono.Text = "" Then LblMensajeError.Text = "Falta ingresar teléfono" : Exit Sub
        If RbDniRuc.SelectedValue = "0" Then
            If TxtApePat.Text = "" Then LblMensajeError.Text = "Falta ingresar Apellido Paterno" : Exit Sub
            If TxtApeMat.Text = "" Then LblMensajeError.Text = "Falta ingresar Apellido Materno" : Exit Sub
            If TxtNombres.Text = "" Then LblMensajeError.Text = "Falta ingresar Nombre" : Exit Sub
        ElseIf RbDniRuc.SelectedValue = "1" Then
            If TxtRazonSocial.Text = "" Then LblMensajeError.Text = "Falta ingresar la Razón Social" : Exit Sub
        End If
        If TxtDireccion.Text = "" Then LblMensajeError.Text = "Falta ingresar dirección" : Exit Sub
        If TxtRuc.Text = "" Then LblMensajeError.Text = "Falta ingresar Número de DNI" : Exit Sub
        If DdlTipoDoc.SelectedValue = "< Seleccionar >" Then LblMensajeError.Text = "Seleccionar tipo de documento." : Exit Sub
        If DdlFormaPago.SelectedValue = "< Seleccionar >" Then LblMensajeError.Text = "Seleccionar forma de pago." : Exit Sub
        If DdlFormaPago.SelectedValue = "2" Then
            If DdlTipoTarj.SelectedValue = "< Seleccionar >" Then LblMensajeError.Text = "Seleccionar tipo de tarjeta." : Exit Sub
        ElseIf DdlFormaPago.SelectedValue = "1" Then
            If Nz(TxtEfectivo.Text) = 0 Then LblMensajeError.Text = "Falta ingresar monto de efectivo" : Exit Sub
        End If
        If Flex.Rows.Count = 0 Then LblMensajeError.Text = "Falta ingresar detalle" : Exit Sub
        Dim ValorSys As String = ""

        Dim St1 As String = "", St2 As String = "", St3 As String = ""
        Dim psRazonsocial As String = ""
        Dim psTipoTarjeta As String
        If RbDniRuc.SelectedValue = "0" Then psRazonsocial = TxtApePat.Text & " " & TxtApeMat.Text & " " & Trim(TxtNombres.Text)
        If RbDniRuc.SelectedValue = "1" Then psRazonsocial = TxtRazonSocial.Text
        If DdlProvincia.SelectedValue <> "< Seleccionar >" Then St1 = "'" & DdlProvincia.SelectedValue & "'" Else St1 = "NULL"
        If DdlDistrito.SelectedValue <> "< Seleccionar >" Then St2 = "'" & DdlDistrito.SelectedValue & "'" Else St2 = "NULL"
        If DdlDpto.SelectedValue <> "< Seleccionar >" Then St3 = "'" & DdlDpto.SelectedValue & "'" Else St3 = "NULL"
        If DdlTipoTarj.SelectedValue <> "< Seleccionar >" Then psTipoTarjeta = "'" & DdlTipoTarj.SelectedValue & "'" Else psTipoTarjeta = "NULL"

        Dim dt As New DataTable

        Try

            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3

            psTipoDoc = DdlTipoDoc.Text

            If psTipoDoc = "01" And lblFCodCliente.Text = "" Then
                LblMensajeError.Text = "Ingresar el RUC a facturar." : Exit Sub
            End If

            dt = ObjContList.Cont_ExistePersonas(Session("CodEmpresa"), TxtRuc.Text, "1", "1", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lblCodCliente.Text = Nu(dr("PERSONA_CODIGO"))
                Next
            End If
            dt = Nothing


            If lblCodCliente.Text = "" Then

                dt = ObjContList.Obtener_UltimaPersona(Session("CodEmpresa"), Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        lblCodCliente.Text = Nz(dr(0)) + 1
                    Next
                Else
                    lblCodCliente.Text = "1"
                End If
                dt = Nothing

                CmdGlobal.CommandText = " INSERT INTO TBDATA_PERSONAS(EMPRESA_CODIGO, PERSONA_CODIGO, PERSONA_RUC,PERSONA_TIPO, PERSONA_SYS_EST,PERSONA_RAZON_SOCIAL, PERSONA_EMAIL ," _
                                       & " PERSONA_APEPAT,PERSONA_APEMAT, PERSONA_NOMBRES, PERSONA_TELF1, PERSONA_DIRECCION, PERSONA_DPTO, PERSONA_PROV,PERSONA_DIST, PERSONA_REFERENCIA ) " _
                                       & " VALUES('" & Session("CodEmpresa") & "'," & Nz(lblCodCliente.Text) & ",'" & TxtRuc.Text & "','1','0','" & psRazonsocial & "','" & TxtEmail.Text & "', " _
                                       & " '" & TxtApePat.Text & "', '" & TxtApeMat.Text & "', '" & TxtNombres.Text & "', '" & TxtTelefono.Text & "', '" & TxtDireccion.Text & "'," & St3 & "," & St1 & "," & St2 & ", '" & TxtReferencia.Text & "')"
                CmdGlobal.ExecuteNonQuery()

                If TxtFecNac.Text <> "" Then
                    psFecNacimiento = Right(TxtFecNac.Text, 4) & Mid(TxtFecNac.Text, 4, 2) & Left(TxtFecNac.Text, 2)
                    objContInsUpd.Cont_Upd_Persona_FecNac(Session("CodEmpresa"), psFecNacimiento, Nz(lblCodCliente.Text), Session("Ruta_Emp"))
                End If

                CmdGlobal2.CommandText = "Select MAX(DIRECCION_N_CODIGO) FROM TBDATA_PERSONAS_DIRECCION "
                RsG = CmdGlobal2.ExecuteReader
                If RsG.HasRows = True Then
                    While RsG.Read
                        Cod = Nz(RsG(0)) + 1
                    End While
                Else
                    Cod = 1
                End If
                RsG.Close()

                CmdGlobal3.CommandText = " INSERT INTO TBDATA_PERSONAS_DIRECCION(EMPRESA_CODIGO, DATAPERSONA_N_CODIGO, DIRECCION_N_CODIGO, DIRECCION_C_DESCRIPCION, " _
                                      & " DIRECCION_C_PAIS, DIRECCION_C_DPTO, DIRECCION_C_PROV, DIRECCION_C_DIST, DIRECCION_C_PRINCIPAL, DIRECCION_C_TELEFONO, DIRECCION_C_REFERENCIA ) " _
                                      & " VALUES('" & Session("CodEmpresa") & "'," & lblCodCliente.Text & "," & Cod & ",'" & TxtDireccion.Text & "', '0051', " _
                                      & " " & St3 & ", " & St1 & ", " & St2 & ", '0', '" & Trim(TxtTelefono.Text) & "', '" & Trim(TxtReferencia.Text) & "')"
                CmdGlobal3.ExecuteNonQuery()

            Else

                dt = ObjContList.Existe_DierccionPersona(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodCliente.Text))
                If dt.Rows.Count = 0 Then
                    CmdGlobal.CommandText = " UPDATE TBDATA_PERSONAS SET PERSONA_DIRECCION = '" & TxtDireccion.Text & "', " _
                                          & " PERSONA_DPTO = " & St3 & ", PERSONA_REFERENCIA = '" & TxtReferencia.Text & "', " _
                                          & " PERSONA_PROV = " & St1 & ", PERSONA_DIST = " & St2 & ", PERSONA_EMAIL = '" & TxtEmail.Text & "' " _
                                          & " WHERE PERSONA_CODIGO =" & Nz(lblCodCliente.Text)
                    CmdGlobal.ExecuteNonQuery()
                    If TxtFecNac.Text <> "" Then
                        psFecNacimiento = Right(TxtFecNac.Text, 4) & Mid(TxtFecNac.Text, 4, 2) & Left(TxtFecNac.Text, 2)
                        objContInsUpd.Cont_Upd_Persona_FecNac(Session("CodEmpresa"), psFecNacimiento, Nz(lblCodCliente.Text), Session("Ruta_Emp"))
                    End If
                End If
                dt = Nothing

                dt = ObjContList.Existe_TelefonoDierccionPersona(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodCliente.Text), TxtTelefono.Text)
                If dt.Rows.Count = 0 Then
                    CmdGlobal2.CommandText = "Select MAX(DIRECCION_N_CODIGO) FROM TBDATA_PERSONAS_DIRECCION "
                    RsG = CmdGlobal2.ExecuteReader
                    If RsG.HasRows = True Then
                        While RsG.Read
                            Cod = Nz(RsG(0)) + 1
                        End While
                    Else
                        Cod = 1
                    End If
                    RsG.Close()
                    CmdGlobal3.CommandText = " INSERT INTO TBDATA_PERSONAS_DIRECCION(EMPRESA_CODIGO, DATAPERSONA_N_CODIGO, DIRECCION_N_CODIGO, DIRECCION_C_DESCRIPCION, " _
                                          & " DIRECCION_C_PAIS, DIRECCION_C_DPTO, DIRECCION_C_PROV, DIRECCION_C_DIST, DIRECCION_C_PRINCIPAL, DIRECCION_C_TELEFONO, DIRECCION_C_REFERENCIA ) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodCliente.Text & "," & Cod & ",'" & TxtDireccion.Text & "', '0051', " _
                                          & " " & St3 & ", " & St1 & ", " & St2 & ", '0', '" & Trim(TxtTelefono.Text) & "', '" & Trim(TxtReferencia.Text) & "')"
                    CmdGlobal3.ExecuteNonQuery()
                End If

            End If

            Cn.Close() : Cn2.Close() : Cn3.Close()
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3

            Dim psCodVenta As String = ""

            CmdGlobal.CommandText = "SELECT MAX(RE_CODIGO) FROM TBVENTAS_RE" & Session("CodEmpresa") & Left(FechaActual, 4) & " WHERE RE_SYS_EST='0'"
            RsG = CmdGlobal.ExecuteReader
            If RsG.HasRows = True Then
                While RsG.Read
                    psCodVenta = Nz(RsG(0)) + 1
                End While
            Else
                psCodVenta = "1"
            End If
            RsG.Close()

            CmdGlobal2.CommandText = " INSERT INTO TBVENTAS_RE" & Session("CodEmpresa") & Left(FechaActual, 4) & " (RE_CODIGO,RE_TIPODOC, RE_PERSONA_CODIGO, " _
                                  & " RE_SYS_EST,RE_SYS_CRE,RE_ANULADO,RE_ESTADO_PAGO,RE_FORMA_PAGO,RE_ESTADO, RE_MESA_NRO, RE_MESA_ACTIVA, RE_MONEDA ) " _
                                  & " VALUES(" & Nz(psCodVenta) & ",'" & psTipoDoc & "', '" & lblCodCliente.Text & "', " _
                                  & " '0','" & ValorSys & "','N','1','1','1'," & Nz(TxtNroDelivery.Text) & ",'D', '2')"
            CmdGlobal2.ExecuteNonQuery()

            Dim psValorIgv As Double : psValorIgv = 0
            Dim psFechaEmision As String = ""
            psFechaEmision = Right(TxtDFecha.Text, 4) & Mid(TxtDFecha.Text, 4, 2) & Left(TxtDFecha.Text, 2)
            psValorIgv = ObjVentas.Obtener_ValorIgv(Ruta_GrEmp) * 100
            CmdGlobal3.CommandText = " UPDATE TBVENTAS_RE" & Session("CodEmpresa") & Left(FechaActual, 4) & " SET " _
                                  & " RE_DESTINO         = '1', " _
                                  & " RE_FECHA_EMI       = '" & psFechaEmision & "', " _
                                  & " RE_FECHA_PAGO      = '" & psFechaEmision & "', " _
                                  & " RE_FECHA_CANC      = '" & psFechaEmision & "', " _
                                  & " RE_TOTAL_APAGAR    = " & Nz(TxtDTotal.Text) & ", " _
                                  & " RE_TOTAL_CANC      = " & Nz(TxtDTotal.Text) & ", " _
                                  & " RE_SUBTOTAL        = " & Nz(TxtDSubTotal.Text) & ", " _
                                  & " RE_TOTAL_IGV       = " & Nz(TxtDIgv.Text) & " , " _
                                  & " RE_VALOR_IGV       = " & Nz(psValorIgv) & ", " _
                                  & " RE_NRODET          = " & Flex.Rows.Count & ", " _
                                  & " RE_CANT_FALT       = " & Nz(TxtDCant.Text) & " " _
                                  & " WHERE RE_SYS_EST   = '0' AND RE_CODIGO = " & Nz(psCodVenta)
            CmdGlobal3.ExecuteNonQuery()

            Cn.Close() : Cn2.Close() : Cn3.Close()
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3

            CmdGlobal.CommandText = " INSERT INTO TBVENTAS_DELIVERY (EMPRESA_CODIGO, DELIVERY_AÑO, DELIVERY_FACTURAR_PERSONA, " _
                                  & " DELIVERY_CODIGO, DELIVERY_TELEFONO, DELIVERY_FECHA, DELIVERY_HORA, " _
                                  & " PERSONA_CODIGO, RE_CODIGO, DELIVERY_ESTADO , DELIVERY_SYS_EST, DELIVERY_FORMA_PAGO, DELIVERY_TARJETA_TIPO, " _
                                  & " DELIVERY_EFECTIVO, DELIVERY_DOC_TIPO, DELIVERY_TIEMPO_LLEGADA, DELIVERY_TOTAL)" _
                                  & " VALUES ('" & Session("CodEmpresa") & "', '" & Left(FechaActual, 4) & "', " & Nz(lblFCodCliente.Text) & ", " _
                                  & " " & Nz(TxtNroDelivery.Text) & ", '" & TxtTelefono.Text & "', '" & FechaActual() & "', '" & HoraActual() & "', " _
                                  & " " & Nz(lblCodCliente.Text) & ", " & Nz(psCodVenta) & ", '0', '0', '" & DdlFormaPago.SelectedValue & "', " & psTipoTarjeta & ", " _
                                  & " " & Format(Nz(TxtEfectivo.Text), "0.00") & ",'" & psTipoDoc & "', '" & TxtTimeAprox.Text & "', " & Format(Nz(TxtDTotal.Text), "0.00") & ")"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal2.CommandText = " UPDATE TBVENTAS_DELIVERY SET DELIVERY_DIRECCION = '" & TxtDireccion.Text & "', " _
                                  & " DELIVERY_DPTO = " & St3 & ", DELIVERY_REFERENCIA = '" & TxtReferencia.Text & "', " _
                                  & " DELIVERY_PROV = " & St1 & ", DELIVERY_DIST = " & St2 & " " _
                                  & " WHERE DELIVERY_CODIGO = " & Nz(TxtNroDelivery.Text)
            CmdGlobal2.ExecuteNonQuery()

            Cn.Close() : Cn2.Close() : Cn3.Close()
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3


            Dim psCantidad As Double : psCantidad = 0
            Dim psColCant As String = ""
            For i = 0 To Flex.Rows.Count - 1
                psColCant = "txtCant"
                Dim txtCant As TextBox = Flex.Rows(i).Cells(4).FindControl(psColCant)
                If Flex.Rows(i).Cells(2).Text <> "" And Nz(txtCant.Text) > 0 Then   '8 - 12
                    psCantidad = Nz(txtCant.Text)

                    CmdGlobal.CommandText = " INSERT INTO TBVENTAS_DELIVERY_DETALLE (EMPRESA_CODIGO, DELIVERY_CODIGO, DELIVERYD_ITEM, DELIVERYD_PRODUCTO, DELIVERYD_CANT, DELIVERYD_PRECIO_UNIT, DELIVERYD_PRECIO_IGV, DELIVERYD_PRECIO_SUBTOTAL ) " _
                                            & " VALUES('" & Session("CodEmpresa") & "', " & Nz(TxtNroDelivery.Text) & "," & Nz(i) + 1 & ", " & Nz(Flex.Rows(i).Cells(2).Text) & "," & psCantidad & "," & Nz(Flex.Rows(i).Cells(5).Text) & "," & Nz(Flex.Rows(i).Cells(8).Text) & "," & Nz(Flex.Rows(i).Cells(7).Text) & ") "
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "INSERT INTO TBVENTAS_REDET" & Session("CodEmpresa") & Left(FechaActual, 4) & "(RE_CODIGO, RED_ITEM, RED_CANTIDAD, RED_CONCEPTO, " _
                                            & " RED_SYS_EST,RED_ESTADO_ATENCION, RED_ATENCION_INICIA  ) " _
                                            & " VALUES(" & Nz(psCodVenta) & "," & Nz(i) + 1 & "," & psCantidad & ",'" & Nu(Flex.Rows(i).Cells(1).Text) & "', " _
                                            & " '0', '1', '" & HoraActual() & "' ) "
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBVENTAS_REDET" & Session("CodEmpresa") & Left(FechaActual, 4) & " SET RED_ENTREGADO='0',RED_CANT_ENTREG=0,RED_CANT_FALT=" & Nz(psCantidad) & " WHERE RE_CODIGO=" & psCodVenta & " AND RED_SYS_EST='0' AND RED_ITEM=" & i + 1
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBVENTAS_REDET" & Session("CodEmpresa") & Left(FechaActual, 4) & " SET RED_REF_TIPOCONCEP='6', RED_REF_V1=" & Nz(Flex.Rows(i).Cells(2).Text) & ", RED_REF_V2 = '1'  WHERE RE_CODIGO=" & Nz(psCodVenta) & " AND RED_SYS_EST='0' AND RED_ITEM=" & i + 1
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBVENTAS_REDET" & Session("CodEmpresa") & Left(FechaActual, 4) & " SET RED_ESTADO='0' WHERE RE_CODIGO=" & psCodVenta & " AND RED_SYS_EST='0' AND RED_ITEM=" & i + 1
                    CmdGlobal.ExecuteNonQuery()

                    If Nu(Flex.Rows(i).Cells(9).Text) = "SI" Then
                        Dim psCodCompuesto As String = ""
                        CmdGlobal.CommandText = " SELECT A.TRANSF_CODIGO, TRANSF_ART_CODIGO, TRANSF_ART_DESCRIPCION, TRANSFDET_ARTICULO, ART_DESCRIPCION, CANTIDAD " _
                                                & " FROM TBINV_ARTICULOS_TRANSFORMACION AS A " _
                                                & " INNER JOIN TBINV_ARTICULOS_TRANSFORMACION_DET AS B ON A.TRANSF_CODIGO = B.TRANSFDET_CODIGO " _
                                                & " INNER JOIN TBINV_ARTICULOS AS C ON B.TRANSFDET_ARTICULO = C.ART_CODIGO " _
                                                & " WHERE TRANSF_ART_SYS_EST = '0' AND TRANSF_ART_CODIGO = " & Nz(Flex.Rows(i).Cells(2).Text)
                        RsG = CmdGlobal.ExecuteReader
                        If RsG.HasRows = True Then
                            While RsG.Read
                                CmdGlobal2.CommandText = "SELECT MAX(COMPUESTO_REG_NUM) FROM TBVENTAS_REDET_COMPUESTO "
                                RsG2 = CmdGlobal.ExecuteReader
                                If RsG2.HasRows = True Then
                                    While RsG2.Read
                                        psCodCompuesto = Nz(RsG2(0)) + 1
                                    End While
                                Else
                                    psCodCompuesto = "1"
                                End If
                                RsG2.Close()
                                CmdGlobal3.CommandText = " INSERT INTO TBVENTAS_REDET_COMPUESTO (EMPRESA_CODIGO, COMPUESTO_RE_AÑO, COMPUESTO_RE_CODIGO, COMPUESTO_RED_ITEM, COMPUESTO_RED_ARTICULO, COMPUESTO_REG_NUM, COMPUESTO_ART_CODIGO, " _
                                                        & " COMPUESTO_CANTIDAD, COMPUESTO_ATENCION_INI, COMPUESTO_ESTADO_ATENCION)" _
                                                        & " VALUES ( '" & Session("CodEmpresa") & "', '" & Left(FechaActual, 4) & "', " & Nz(psCodVenta) & "," & Nz(i) + 1 & ",  " & Nz(Flex.Rows(i).Cells(2).Text) & " , " & psCodCompuesto & ", " & Nz(RsG!TRANSFDET_ARTICULO) & ", " _
                                                        & " " & Nz(RsG!Cantidad) * psCantidad & ", '" & HoraActual() & "', '1')"
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        End If
                        RsG.Close()
                    End If
                End If
            Next

            Cn.Close() : Cn2.Close() : Cn3.Close()

            LblMensajeError.Text = "Datos guardados."

            Call Limpiar(sender, e)
            TxtTelefono.Text = ""

        Catch ex As SqlException
            LblMensajeError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblMensajeError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        End Try

    End Sub

    Private Sub BtnTelefono_Click(sender As Object, e As EventArgs) Handles BtnTelefono.Click
        Dim dt As New DataTable
        Dim psCodCliente As Double = 0
        LblError.Text = ""
        Try

            If lblFCodCliente.Text = "" And TxtTelefono.Text = "" Then Exit Sub
            If lblFCodCliente.Text <> "" Then psCodCliente = lblFCodCliente.Text

            dt = ObjVentas.ListaTelefonos_xCliente(Session("Ruta_Emp"), Session("CodEmpresa"), psCodCliente, TxtTelefono.Text)
            GvListaTelefono.DataSource = dt
            GvListaTelefono.DataBind()
            dt = Nothing

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTelefonos').modal('show');", True)

        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        End Try

    End Sub

    Private Sub BtnCerrarTelefono_Click(sender As Object, e As EventArgs) Handles BtnCerrarTelefono.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTelefonos').modal('hide');", True)
    End Sub

    Private Sub GvListaTelefono_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaTelefono.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            If e.CommandName = "Seleccionar" Then
                TxtRazonSocial.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtTelefono.Text = GvBusArt.Rows(index).Cells(2).Text
                TxtDireccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtReferencia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtRuc.Text = GvBusArt.Rows(index).Cells(7).Text
                TxtApePat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtEmail.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                lblFCodCliente.Text = GvBusArt.Rows(index).Cells(14).Text
                TxtBusRuc.Text = GvBusArt.Rows(index).Cells(7).Text
                txtBusRazon.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtFecNac.Text = FormatoFecha(GvBusArt.Rows(index).Cells(3).Text)
                DdlDpto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                DdlProvincia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                DdlDistrito.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaTelefono.Rows(index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTelefonos').modal('hide');", True)
            End If
        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicacion. " & ex.Message
        End Try

    End Sub
End Class
