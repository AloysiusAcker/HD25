Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Drawing
Imports Image = iTextSharp.text.Image
Imports Rectangle = iTextSharp.text.Rectangle
Imports Font = iTextSharp.text.Font
Imports iTextSharp.text.pdf.draw
Partial Class Inventario_Inventario_GenerarGuia
    Inherits System.Web.UI.Page

    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Dim objEmp As New ModuloGeneral
    Dim CodSalida As String = ""
    Dim i As Long = 0
    Dim a As Long = i
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Cls_Catalogo
            Dim dt As New DataTable
            Dim valor As String = Request.QueryString("parametro")
            Dim valor2 As String = Request.QueryString("parametro2")

            Dim psconexion As String = Session("Ruta_Emp")

            TxtFecha.Text = FormatoFecha(FechaActual)
            TxtHora.Text = FormatoHoraSeg(HoraActual(True))
            TxtFechaTraslado.Text = FormatoFecha(FechaActual)
            TxtHoraTraslado.Text = FormatoHoraSeg(HoraActual(True))

            Call LlenaComboItem("TBOPC549", DdlModTransporte)
            Call LlenaComboItem("TBOPC222", DdlMotivoTraslado)
            Session("TipoDestinatario") = DdlDestinatario.SelectedValue
            Session("TipoRemitente") = DdlRemitente.SelectedValue
            BtnGuardar.Enabled = True

            If valor <> "" Then
                Session("CodSalida") = valor
                Session("TipoSalida") = valor2
                Call LlenarDatosGuia(valor)
                If Session("TipoGuia") = "1" Then
                    LblTitulo.Text = "Inventario - Genera Guía de Remisión"
                    id_GuiaNumero.Visible = True
                    id_DatosTransportista.Visible = True
                    id_Transportista.Visible = True
                    id_Vehiculo.Visible = True
                    id_Chofer.Visible = True
                    id_ModalidadTransporte.Visible = True
                    id_MotivoTrasldo.Visible = True
                    id_GuiaInterna.Visible = False
                Else
                    LblTitulo.Text = "Inventario - Genera Guía Interna"
                    id_GuiaInterna.Visible = True
                    id_GuiaNumero.Visible = False
                    id_DatosTransportista.Visible = False
                    id_Transportista.Visible = False
                    id_Vehiculo.Visible = False
                    id_Chofer.Visible = False
                    id_ModalidadTransporte.Visible = False
                    id_MotivoTrasldo.Visible = False
                End If
            End If

            UpdatePanel1.Update()
            upSetSession.Update()
            UpdatePanel2.Update()
            UpdatePanel3.Update()
            UpdatePanel4.Update()
            UpdatePanel5.Update()

        End If
    End Sub

    Private Sub LlenarDatosGuia(ByVal psCodSalida As Double)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim RsGuia As SqlDataReader

        TxtFecha.Text = FormatoFecha(FechaActual)
        TxtHora.Text = FormatoHoraSeg(HoraActual(True))
        TxtFechaTraslado.Text = FormatoFecha(FechaActual)
        TxtHoraTraslado.Text = FormatoHoraSeg(HoraActual(True))

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3

        If Session("TipoSalida") = "2" Then
            CmdGlobal.CommandText = "SELECT * FROM TBINV_CCOSTO_SALIDA WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & psCodSalida
        ElseIf Session("TipoSalida") = "1" Then
            CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & psCodSalida
        End If
        RsGuia = CmdGlobal.ExecuteReader
        If RsGuia.HasRows Then
            While RsGuia.Read
                Session("TipoRemitente") = Session("TipoSalida")
                DdlRemitente.SelectedValue = Session("TipoSalida")
                If Session("TipoSalida") = "1" Then
                    lblCodRemitente.Text = Nu(RsGuia!ALMACEN_ORIGEN)
                    DdlDestinatario.SelectedValue = Nu(RsGuia!DESP_TIPODESTINO)
                    Session("TipoDestinatario") = Nu(RsGuia!DESP_TIPODESTINO)
                    Session("TipoGuia") = Nu(RsGuia("DESP_TIPO_DOC_SALIDA"))
                ElseIf Session("TipoSalida") = "2" Then
                    lblCodRemitente.Text = Nu(RsGuia!CECOSE_CODIGO_ORIGEN)
                    DdlDestinatario.SelectedValue = Nu(RsGuia!OSAL_TIPODESTINO)
                    Session("TipoDestinatario") = Nu(RsGuia!OSAL_TIPODESTINO)
                    Session("TipoGuia") = Nu(RsGuia("OSAL_TIPO_DOC_SALIDA"))
                End If
                If DdlRemitente.SelectedValue = "1" Then
                    CmdGlobal2.CommandText = "SELECT ALMACEN_DIRECCION,ALMACEN_NOMBRE,ALMACEN_DISTRITO,ALMACEN_DPTO, ALMACEN_PROV FROM TBINV_ALMACENES WHERE ALMACEN_CODIGO = " & lblCodRemitente.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtRemUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!ALMACEN_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!ALMACEN_PROV), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!ALMACEN_DISTRITO), 2), 2)
                            TxtPuntoPartida.Text = Nu(Rs!ALMACEN_DIRECCION)
                            TxtRemCodigo.Text = Llenar_Ceros(lblCodRemitente.Text, 3)
                            txtRemDescripcion.Text = Nu(Rs!ALMACEN_NOMBRE)
                        End While
                    End If
                    Rs.Close()
                ElseIf DdlRemitente.SelectedValue = "2" Then
                    CmdGlobal2.CommandText = "SELECT CECOSE_DIRECCION,CECOSE_COD_INTERNO,CECOSE_DESCRIPCION,CECOSE_DISTRITO,CECOSE_PROVINCIA,CECOSE_DPTO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CECOSE_CODIGO = " & lblCodRemitente.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtRemUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!CECOSE_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!CECOSE_PROVINCIA), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!CECOSE_DISTRITO), 2), 2)
                            TxtPuntoPartida.Text = Nu(Rs!CECOSE_DIRECCION)
                            TxtRemCodigo.Text = Nu(Rs!CECOSE_COD_INTERNO)
                            txtRemDescripcion.Text = Nu(Rs!CECOSE_DESCRIPCION)
                        End While
                    End If
                    Rs.Close()
                End If
                If Session("TipoDestinatario") = "1" Then
                    lblCodDestinatario.Text = Nu(RsGuia!ALMACEN_CODIGO_DESTINO)
                    CmdGlobal2.CommandText = "SELECT ALMACEN_DIRECCION,ALMACEN_NOMBRE,ALMACEN_DISTRITO,ALMACEN_DPTO, ALMACEN_PROV FROM TBINV_ALMACENES WHERE ALMACEN_CODIGO = " & lblCodDestinatario.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtDestUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!ALMACEN_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!ALMACEN_PROV), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!ALMACEN_DISTRITO), 2), 2)
                            TxtPuntoLlegada.Text = Nu(Rs!ALMACEN_DIRECCION)
                            TxtDestCodigo.Text = Llenar_Ceros(lblCodDestinatario.Text, 3)
                            TxtDestDescripcion.Text = Nu(Rs!ALMACEN_NOMBRE)
                        End While
                    End If
                    Rs.Close()
                ElseIf Session("TipoDestinatario") = "2" Then
                    lblCodDestinatario.Text = Nu(RsGuia!CECOSE_CODIGO_DESTINO)
                    CmdGlobal2.CommandText = "SELECT CECOSE_DIRECCION,CECOSE_COD_INTERNO,CECOSE_DESCRIPCION,CECOSE_DISTRITO,CECOSE_PROVINCIA,CECOSE_DPTO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CECOSE_CODIGO = " & lblCodDestinatario.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtDestUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!CECOSE_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!CECOSE_PROVINCIA), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!CECOSE_DISTRITO), 2), 2)
                            TxtPuntoLlegada.Text = Nu(Rs!CECOSE_DIRECCION)
                            TxtDestCodigo.Text = Nu(Rs!CECOSE_COD_INTERNO)
                            TxtDestDescripcion.Text = Nu(Rs!CECOSE_DESCRIPCION)
                        End While
                    End If
                    Rs.Close()
                ElseIf Session("TipoDestinatario") = "3" Then
                    If Session("TipoRemitente") = "1" Then lblCodDestinatario.Text = Nu(RsGuia!PROVEEDOR_CODIGO_DESTINO)
                    If Session("TipoRemitente") = "2" Then lblCodDestinatario.Text = Nu(RsGuia!OSAL_PROVEEDOR_CODIGO)
                    CmdGlobal2.CommandText = " SELECT PERSONA_RUC,PERSONA_RAZON_SOCIAL,PERSONA_DIRECCION,PERSONA_CODIGO, " _
                        & " PERSONA_PAIS,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC006' AND ELEMEN_CODIGO = PERSONA_PAIS) AS PPAIS," _
                        & " PERSONA_DPTO,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC002' AND ELEMEN_CODIGO = PERSONA_DPTO) AS PDPTO," _
                        & " PERSONA_PROV,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC003' AND ELEMEN_CODIGO = PERSONA_PROV) AS PPROV," _
                        & " PERSONA_DIST,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) AS PDIST" _
                        & " FROM TBDATA_PERSONAS WHERE PERSONA_CODIGO = " & lblCodDestinatario.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtDestUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!PERSONA_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!PERSONA_PROV), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!PERSONA_DIST), 2), 2)
                            TxtPuntoLlegada.Text = Nu(Rs!PERSONA_DIRECCION) & IIf(Nu(Rs!PDIST) = "", "", " " & Nu(Rs!PDIST)) & IIf(Nu(Rs!PPROV) = "", "", ", " & Nu(Rs!PPROV)) & IIf(Nu(Rs!PDPTO) = "", "", ", " & Nu(Rs!PDPTO)) & IIf(Nu(Rs!PPAIS) = "", "", " - " & Nu(Rs!PPAIS)) ': telf1 = Nu(Rs!PERSONA_TELF1): telf2 = Nu(Rs!PERSONA_TELF2)
                            TxtDestCodigo.Text = Nu(Rs!PERSONA_RUC)
                            TxtDestDescripcion.Text = Nu(Rs!PERSONA_RAZON_SOCIAL)
                        End While
                    End If
                    Rs.Close()
                ElseIf Session("TipoDestinatario") = "4" Then
                    If Session("TipoRemitente") = "1" Then lblCodDestinatario.Text = Nu(RsGuia!EQUIPO_CODIGO_DESTINO)
                    If Session("TipoRemitente") = "2" Then lblCodDestinatario.Text = Nu(RsGuia!OSAL_EQUIPO_CODIGO_DESTINO)
                    CmdGlobal2.CommandText = "SELECT  S.SERIE_NUMERAR, S.SERIE_NRO, S.ARTICULO_CODIGO, A.ART_DESCRIPCION FROM   dbo.TBINV_ARTICULOS A INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON A.ART_CODIGO = S.ARTICULO_CODIGO WHERE S.SERIE_NUMERAR = " & lblCodDestinatario.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtPuntoLlegada.Text = ""
                            TxtDestCodigo.Text = Nu(Rs!Serie_Nro)
                            TxtDestDescripcion.Text = Nu(Rs!art_descripcion)
                        End While
                    End If
                    Rs.Close()
                ElseIf Session("TipoDestinatario") = "5" Then
                    If Session("TipoRemitente") = "1" Then lblCodDestinatario.Text = Nu(RsGuia!PERSONA_CODIGO_DESTINO)
                    If Session("TipoRemitente") = "2" Then lblCodDestinatario.Text = Nu(RsGuia!OSAL_PERSONA_CODIGO)
                    CmdGlobal2.CommandText = "SELECT PER_DNI,PER_APEPAT+' '+PER_APEMAT+', '+PER_NOMBRES AS PERSONA,PER_DIRECCION,PER_CODIGO,PER_DIST,PER_DPTO,PER_PROV FROM TBINV_PERSONAS WHERE PER_CODIGO = " & lblCodDestinatario.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtDestUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!PER_DIST), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!PER_PROV), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!PER_DIST), 2), 2)
                            TxtPuntoLlegada.Text = Nu(Rs!PER_DIRECCION)
                            TxtDestCodigo.Text = Nu(Rs!PER_DNI)
                            TxtDestDescripcion.Text = Nu(Rs!PERSONA)
                        End While
                    End If
                    Rs.Close()
                    Dim Contact, Ref, Ped As String
                    Contact = ""
                    Ref = ""
                    Ped = ""

                    Dim psCodPedido As String = ""
                    CmdGlobal2.CommandText = " SELECT PEDIDO_CODIGO FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND DESP_CODIGO = " & Session("CodSalida")
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodPedido = Nu(Rs!PEDIDO_CODIGO)
                        End While
                    End If
                    Rs.Close()
                ElseIf Session("TipoDestinatario") = "6" Then
                    If Session("TipoRemitente") = "1" Then lblCodDestinatario.Text = Nu(RsGuia!CLIENTE_CODIGO_DESTINO)
                    If Session("TipoRemitente") = "2" Then lblCodDestinatario.Text = Nu(RsGuia!OSAL_CLIENTE_CODIGO)
                    CmdGlobal2.CommandText = " SELECT PERSONA_RUC,PERSONA_RAZON_SOCIAL,PERSONA_DIRECCION,PERSONA_CODIGO, " _
                        & " PERSONA_PAIS,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC006' AND ELEMEN_CODIGO = PERSONA_PAIS) AS PPAIS," _
                        & " PERSONA_DPTO,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC002' AND ELEMEN_CODIGO = PERSONA_DPTO) AS PDPTO," _
                        & " PERSONA_PROV,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC003' AND ELEMEN_CODIGO = PERSONA_PROV) AS PPROV," _
                        & " PERSONA_DIST,(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC004' AND ELEMEN_CODIGO = PERSONA_DIST) AS PDIST" _
                        & " FROM TBDATA_PERSONAS WHERE PERSONA_TIPO = '1' AND PERSONA_CODIGO = " & lblCodDestinatario.Text
                    Rs = CmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtPuntoLlegada.Text = Nu(Rs!PERSONA_DIRECCION) & IIf(Nu(Rs!PDIST) = "", "", " " & Nu(Rs!PDIST)) & IIf(Nu(Rs!PPROV) = "", "", ", " & Nu(Rs!PPROV)) & IIf(Nu(Rs!PDPTO) = "", "", ", " & Nu(Rs!PDPTO)) & IIf(Nu(Rs!PPAIS) = "", "", " - " & Nu(Rs!PPAIS)) ': telf1 = Nu(Rs!PERSONA_TELF1): telf2 = Nu(Rs!PERSONA_TELF2)
                            TxtDestCodigo.Text = Nu(Rs!PERSONA_RUC)
                            TxtDestDescripcion.Text = Nu(Rs!PERSONA_RAZON_SOCIAL)
                            TxtDestUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!PERSONA_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!PERSONA_PROV), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!PERSONA_DIST), 2), 2)
                            If Trim(TxtPuntoLlegada.Text) = "" Then
                                CmdGlobal3.CommandText = " SELECT PD.DIRECCION_C_DESCRIPCION,DIRECCION_C_DIST,DIRECCION_C_PROV,DIRECCION_C_DPTO " _
                                    & " FROM dbo.TBDATA_PERSONAS_DIRECCION AS PD INNER JOIN dbo.TBVENTAS_FORMA_ENTREGA_DIRECCION AS FED " _
                                    & " ON PD.EMPRESA_CODIGO = FED.EMPRESA_CODIGO AND PD.DIRECCION_N_CODIGO = FED.FORMDIRECCION_N_CODIGO " _
                                    & " WHERE (FED.FORMDIRECCION_N_CODSALIDA = " & Session("CodSalida") & ") AND (PD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                    & " AND (FED.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (PD.DATAPERSONA_N_CODIGO = " & Nu(Rs!PERSONA_CODIGO) & ") "
                                Rs2 = CmdGlobal3.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        TxtDestUbigeo.Text = Llenar_Ceros(Left(Nu(Rs!DIRECCION_C_DPTO), 2), 2) + Llenar_Ceros(Mid(Nu(Rs!DIRECCION_C_PROV), 3, 2), 2) + Llenar_Ceros(Right(Nu(Rs!DIRECCION_C_DIST), 2), 2)
                                        TxtPuntoLlegada.Text = Nu(Rs2!DIRECCION_C_DESCRIPCION)
                                    End While
                                End If
                                Rs2.Close()
                            End If
                        End While
                    End If
                End If
            End While
        End If
        RsGuia.Close()

        Dim dt As New DataTable
        Dim drt As DataRow
        dt.Columns.Add("Cod_Salida")
        drT = dt.NewRow()
        drT("Cod_Salida") = Llenar_Ceros(Session("CodSalida"), 6)
        dt.Rows.Add(drT)
        GvListaSalida.DataSource = dt
        GvListaSalida.DataBind()

        Dim dtDatos As New DataTable
        dtDatos = obj.Lista_DetalleSinSeries_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, Session("TipoSalida"))
        If dtDatos.Rows.Count > 0 Then
            GvListaAcc.DataSource = dtDatos
            GvListaAcc.DataBind()
        End If

        dtDatos = Nothing

        dtDatos = obj.Lista_Detalle_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, Session("TipoSalida"))
        GvListaArticulos.DataSource = dtDatos
        GvListaArticulos.DataBind()

        dtDatos = Nothing



    End Sub

    Protected Sub TxtGuiaSerie_TextChanged(sender As Object, e As EventArgs) Handles TxtGuiaSerie.TextChanged
        Dim psCodGuia As String = ""
        If TxtGuiaSerie.Text.Trim = "" Then Exit Sub
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        CmdGlobal.CommandText = " SELECT DOC_CODIGO FROM TBDOCUMENTOS WHERE DOC_EMPRESA = '" & Session("CodEmpresa") & "' " _
                               & " AND DOC_SYS_EST ='0' AND DOC_AÑO='" & AñoActual(Session("CodEmpresa"), Session("Ruta_Emp")) & "' AND (DOC_CODIGO)='09' "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psCodGuia = Nu(Rs!DOC_CODIGO)
            End While
        End If
        Rs.Close()

        'verificar nro serie valida
        CmdGlobal.CommandText = "  SELECT GURESE_VALOR_INICIAL,GURESE_NUMERO FROM TBINV_GUIA_REMISION_SERIE WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                              & " AND GURESE_NUMERO LIKE '" & TxtGuiaSerie.Text & "%' AND GURESE_SYS_EST ='0' AND GURESE_TIPO_DOC='09'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                TxtGuiaSerie.Text = Nu(Rs!GURESE_NUMERO)
                TxtGuiaNumero.Text = Llenar_Ceros(Nz(Rs!GURESE_VALOR_INICIAL), 8)
            End While
        Else
            LblError.Text = "El Nro de Serie ingresada no es válido."
            TxtGuiaSerie.Text = ""
            TxtGuiaNumero.Text = ""
        End If
        Rs.Close()
        Cn.Close()

    End Sub
    Private Sub DdlDestinatario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDestinatario.SelectedIndexChanged
        TxtDestCodigo.Text = ""
        TxtDestDescripcion.Text = ""
        lblCodDestinatario.Text = ""
        Session("TipoDestinatario") = DdlDestinatario.SelectedValue
        BtnDestinatario.Enabled = True

    End Sub

    Private Sub BtnDestinatario_Click(sender As Object, e As EventArgs) Handles BtnDestinatario.Click
        TituloPopup.Text = "Destinatario - Busqueda de " & DdlDestinatario.SelectedItem.Text
        Session("TipoBusqueda") = "Destinatario"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub BtnRemitente_Click(sender As Object, e As EventArgs) Handles BtnRemitente.Click
        TituloPopup.Text = "Destinatario - Busqueda de " & DdlRemitente.SelectedItem.Text
        Session("TipoBusqueda") = "Remitente"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub DdlRemitente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlRemitente.SelectedIndexChanged
        TxtRemCodigo.Text = ""
        txtRemDescripcion.Text = ""
        lblCodRemitente.Text = ""
        Session("TipoRemitente") = DdlRemitente.SelectedValue
        BtnDestinatario.Enabled = True
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Call Limpiar_Popup()
    End Sub
    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Remitente" Then
            TxtRemCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtRemDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtPuntoPartida.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtRemUbigeo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodRemitente.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        ElseIf e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Destinatario" Then
            TxtDestCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtDestDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtPuntoLlegada.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtDestUbigeo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodDestinatario.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodigo As Double = 0
        Dim objCont As New clsCont_Listados
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""
        If (Session("TipoRemitente") = "1" And Session("TipoBusqueda") = "Remitente") Or (Session("TipoDestinatario") = "1" And Session("TipoBusqueda") = "Destinatario") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodigo = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaAlmacen(psconexion, Session("CodEmpresa"), psBusCodigo, descripcion)
        ElseIf (Session("TipoRemitente") = "2" And Session("TipoBusqueda") = "Remitente") Or (Session("TipoDestinatario") = "2" And Session("TipoBusqueda") = "Destinatario") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaCentroCosto(psconexion, Session("CodEmpresa"), psBusCodInterno, descripcion)
        ElseIf Session("TipoDestinatario") = "3" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "2")
        ElseIf Session("TipoDestinatario") = "6" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "1")
        ElseIf Session("TipoDestinatario") = "5" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "5")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub BtnTransporte_Click(sender As Object, e As EventArgs) Handles BtnTransporte.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('show');", True)
    End Sub

    Private Sub BtnVehiculo_Click(sender As Object, e As EventArgs) Handles BtnVehiculo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('show');", True)
    End Sub

    Private Sub BtnChofer_Click(sender As Object, e As EventArgs) Handles BtnChofer.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('show');", True)
    End Sub

    Private Sub BtnChoferBuscar_Click(sender As Object, e As EventArgs) Handles BtnChoferBuscar.Click
        Dim obj As New clsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        psBusCodInterno = TxtBusChoferDni.Value.Trim.ToString
        descripcion = TxtBusChoferNombres.Value.Trim.ToString
        dt = obj.Cont_BusquedaChofer(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('show');", True)
        GvChofer.DataSource = dt
        GvChofer.DataBind()
    End Sub
    Protected Sub Limpiar_Popup_Chofer()
        TxtBusChoferDni.Value = ""
        TxtBusChoferNombres.Value = ""
        GvChofer.DataSource = Nothing
        GvChofer.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('hide');", True)
    End Sub

    Private Sub BtnChoferCerrar_Click(sender As Object, e As EventArgs) Handles BtnChoferCerrar.Click
        Call Limpiar_Popup_Chofer()
    End Sub

    Private Sub GvChofer_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvChofer.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtChoferDNI.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtChoferNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtLicencia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodChofer.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Chofer()

    End Sub
    Private Sub BtnVehiculoBuscar_Click(sender As Object, e As EventArgs) Handles BtnVehiculoBuscar.Click
        Dim obj As New clsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        psBusCodInterno = TxtBusPlaca.Value.Trim.ToString
        descripcion = TxtBusMarca.Value.Trim.ToString
        dt = obj.Cont_BusquedaVehiculo(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('show');", True)
        GvVehiculo.DataSource = dt
        GvVehiculo.DataBind()
    End Sub

    Protected Sub Limpiar_Popup_Vehiculo()
        TxtBusPlaca.Value = ""
        TxtBusMarca.Value = ""
        GvVehiculo.DataSource = Nothing
        GvVehiculo.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('hide');", True)
    End Sub

    Private Sub BtnVehiculoCerrars_Click(sender As Object, e As EventArgs) Handles BtnVehiculoCerrar.Click
        Call Limpiar_Popup_Vehiculo()
    End Sub

    Private Sub GvVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvVehiculo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtNroPlaca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtMarca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtconfVehicular.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtRucTrasnportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtRazonTransportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtCertInscripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodVehiculo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Vehiculo()

    End Sub

    Private Sub BtnBusTransporte_Click(sender As Object, e As EventArgs) Handles BtnBusTransporte.Click
        Dim obj As New clsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        If TxtBusTransRUC.Value.ToString <> "" Then psBusCodInterno = TxtBusTransRUC.Value
        descripcion = TxtBusTransRazon.Value.Trim.ToString
        dt = obj.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "4")

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('show');", True)
        GvTransporte.DataSource = dt
        GvTransporte.DataBind()
    End Sub

    Protected Sub Limpiar_Popup_Transporte()
        TxtBusTransRazon.Value = ""
        TxtBusTransRUC.Value = ""
        GvTransporte.DataSource = Nothing
        GvTransporte.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('hide');", True)
    End Sub

    Private Sub BtnCancelarTrans_Click(sender As Object, e As EventArgs) Handles BtnCancelarTrans.Click
        Call Limpiar_Popup_Transporte()
    End Sub

    Private Sub GvTransporte_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvTransporte.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtRucTrasnportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtRazonTransportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtCertInscripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodTrasporte.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Transporte()

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2

        Dim ValorSys As String = ""

        Dim CodAlmacenRem As String = ""
        Dim CodSeccionRem As String = ""
        Dim CodAlmacenDes As String = ""
        Dim CodSeccionDes As String = ""
        Dim CodProvee As String = ""
        Dim CodCliente As String = ""
        Dim SalidaAlmacen As String = ""
        Dim SalidaCC As String = ""

        Dim cantAcc As Double = 0
        Dim cantItemAcc As Double = 0
        Dim cantSeries As Double = 0
        For i = 0 To GvListaArticulos.Rows.Count - 1
            cantSeries = cantSeries + 1
        Next
        For i = 0 To GvListaAcc.Rows.Count - 1
            cantAcc = cantAcc + Nz(GvListaAcc.Rows(i).Cells(5).Text.Trim)
            cantItemAcc = cantItemAcc + 1
        Next

        If Session("TipoGuia") = "1" And Trim(TxtGuiaSerie.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar la serie de la guía.');", True)
        ElseIf Session("TipoGuia") = "1" And Trim(TxtGuiaNumero.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el numero de la guía.');", True)
        ElseIf lblCodRemitente.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el remitente.');", True)
        ElseIf lblCodDestinatario.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el destinatario.');", True)
        ElseIf Session("TipoGuia") = "2" And TxtQuienRetira.Text.trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar persona quien recibe.');", True)
        ElseIf Session("TipoGuia") = "2" And TxtQuienRecibe.Text.trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar persona quien entrega.');", True)
        ElseIf cantSeries = 0 And cantAcc = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el detalle de la guía.');", True)
        Else

            BtnGuardar.Enabled = False
            Dim CodPersona As String = ""
            Dim CodEquipo As String = ""
            ValorSys = FechaActual() & HoraActual() & Session("User")

            CodAlmacenRem = "NULL"
            CodSeccionRem = "NULL"
            CodPersona = "NULL"
            CodAlmacenDes = "NULL"
            CodSeccionDes = "NULL"
            CodCliente = "NULL"
            CodProvee = "NULL"
            CodEquipo = "NULL"

            Dim CantLineaGuia As Integer : CantLineaGuia = 34
            Dim psCantLinea As Double = 0
            CantLineaGuia = 24
            Dim iGuia As Long = 0
            Dim psCantGuia As Integer = 0
            Dim psCantSobra As Integer = 0
            Dim pdCantDetalle As Integer = 0
            Dim CodCurrier As String = ""
            Dim aa As Integer = 0
            Dim psGuiaSerie As Integer : psGuiaSerie = 0
            Dim psGuiaAcc As Integer : psGuiaAcc = 0
            psCantLinea = cantSeries + cantItemAcc
            psCantGuia = 1
            Dim psCodGuiaVarias As String = ""
            If psCantLinea > CantLineaGuia Then
                psCantGuia = psCantLinea / CantLineaGuia
                psCantSobra = psCantLinea - (CantLineaGuia * psCantGuia)
                If psCantSobra > 0 Then psCantGuia = psCantGuia + 1
            End If

            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se generará " & psCantGuia & " guías.');", True)

            pdCantDetalle = 0
            Dim a As Long : a = 0
            Dim pdCantDetalleGuia As Integer
            pdCantDetalleGuia = 0
            If DdlRemitente.SelectedValue = 1 Then 'S/ALMACEN
                CodAlmacenRem = lblCodRemitente.Text
            Else 'S/CC
                CodSeccionRem = lblCodRemitente.Text
            End If
            If DdlDestinatario.SelectedValue = 1 Then 'S/ALMACEN
                CodAlmacenDes = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 2 Then 'S/CC
                CodSeccionDes = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 6 Then 'cLIENTE
                CodCliente = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 5 Then 'cLIENTE
                CodPersona = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 3 Then
                CodProvee = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 3 Then
                CodEquipo = lblCodDestinatario.Text
            End If

            Dim psFechaEmision As String = ""
            Dim psHoraEmision As String = ""
            psFechaEmision = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
            psHoraEmision = Mid(TxtHora.Text, 1, 2) + Mid(TxtHora.Text, 4, 2) + Mid(TxtHora.Text, 7, 2)

            Dim psFechaTraslado As String = ""
            Dim psHoraTraslado As String = ""
            psFechaTraslado = Mid(TxtFechaTraslado.Text, 7, 4) + Mid(TxtFechaTraslado.Text, 4, 2) + Mid(TxtFechaTraslado.Text, 1, 2)
            psHoraTraslado = Mid(TxtHoraTraslado.Text, 1, 2) + Mid(TxtHoraTraslado.Text, 4, 2) + Mid(TxtHoraTraslado.Text, 7, 2)


            For a = 1 To psCantGuia
                'tipo de guia 1 guia remision 2 guia interna
                If Session("TipoGuia") = "1" Then

                    CmdGlobal.CommandText = "SELECT GUIREM_CODIGO FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " WHERE GUIREM_SERIE='" & Trim(TxtGuiaSerie.Text) & "' AND GUIREM_NUMERO='" & Trim(TxtGuiaNumero.Text) & "'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La serie y numero ingresado ya existe.');", True)
                            Exit Sub
                        End While
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "SELECT MAX(GUIREM_CODIGO) FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            Session("CodGuia") = Llenar_Ceros(Nz(Rs(0)) + 1, 8)
                        End While
                    Else
                        Session("CodGuia") = "00000001"
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUIREM_TIPO, GUIREM_SERIE,GUIREM_NUMERO, GUIREM_SYS_EST,GUIREM_SYS_CRE,GUIREM_RECEPCIONADA,GUIREM_ESTADO) " _
                                               & " VALUES(" & Session("CodGuia") & ",'" & Session("TipoGuia") & "','" & Trim(TxtGuiaSerie.Text) & "','" & Trim(TxtGuiaNumero.Text) & "','0','" & ValorSys & "','0','0')"
                    CmdGlobal.ExecuteNonQuery()

                    CodCurrier = "NULL"
                    Session("NroGuiaRem") = Trim(TxtGuiaSerie.Text) & "-" & Trim(TxtGuiaNumero.Text)

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_SERIE SET GURESE_VALOR_INICIAL = " & Val(TxtGuiaNumero.Text) + 1 & "  WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND GURESE_NUMERO = '" & TxtGuiaSerie.Text & "'AND GURESE_TIPO_DOC='09'"
                    CmdGlobal.ExecuteNonQuery()

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                                & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & ",GUIREM_CURRIER = " & CodCurrier & ",GUIREM_ESTADO_ENTREGA='1',GUIREM_ESTADO_SITUACION='2'," _
                                & " GUIREM_DIRECCION_PARTIDA_UBIGEO='" & TxtRemUbigeo.Text & "',GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "',GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "',ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & ",EQUIPO_CODIGO_DESTINATARIO=" & CodEquipo & "," _
                                & " PERSONA_CODIGO_DESTINATARIO=" & CodPersona & ",CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ",CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",    PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ", GUIREM_NOMBRE_DESTINATARIO = '" & Trim(TxtDestDescripcion.Text) & "',GUIREM_DIRECCION_LLEGADA_UBIGEO='" & TxtDestUbigeo.Text & "', GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text) & "', TRANSPORTISTA_RUC='" & TxtRucTrasnportista.Text.Trim & "',TRANSPORTISTA_RAZONSOCIAL='" & TxtRazonTransportista.Text.Trim & "'," _
                                & " VEHICU_PLACA='" & TxtNroPlaca.Text & "',VEHICU_MARCA='" & TxtMarca.Text & "',VEHICU_CERT_INSCP='" & TxtCertInscripcion.Text & "',CHOFER_DNI='" & TxtChoferDNI.Text & "',CHOFER_NOMBRES='" & TxtChoferNombre.Text & "',CHOFER_LICENCIA='" & TxtLicencia.Text & "', " _
                                & " GUIREM_MOTIVO_DESCRIPCION='" & Trim(Solo_Texto(TxtMotivoDescripcion.Text)) & "' ,GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                                & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_PERSONA_RECIBE = NULL ,GUIREM_PERSONA_RETIRA=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                    If DdlModTransporte.SelectedValue <> "< Seleccionar >" Then
                        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  TRANSPORTISTA_MODALIDAD='" & DdlModTransporte.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    If DdlMotivoTraslado.SelectedValue <> "< Seleccionar >" Then
                        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_MOTIVO_TRASLADO='" & DdlMotivoTraslado.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_BULTO='" & TxtNroBulto.Text & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()

                Else
                    CmdGlobal.CommandText = "SELECT MAX(GUIREM_CODIGO) FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            Session("CodGuia") = Llenar_Ceros(Nz(Rs(0)) + 1, 8)
                        End While
                    Else
                        Session("CodGuia") = "00000001"
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUIREM_TIPO, GUIREM_SERIE,GUIREM_NUMERO, GUIREM_SYS_EST,GUIREM_SYS_CRE,GUIREM_RECEPCIONADA,GUIREM_ESTADO) " _
                                               & " VALUES(" & Session("CodGuia") & ",'" & Session("TipoGuia") & "',NULL,NULL,'0','" & ValorSys & "','0','0')"
                    CmdGlobal.ExecuteNonQuery()

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                                & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",  CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & "," _
                                & " GUIREM_DIRECCION_PARTIDA_UBIGEO='" & TxtRemUbigeo.Text & "',GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "',GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "',ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & ",EQUIPO_CODIGO_DESTINATARIO=" & CodEquipo & "," _
                                & " PERSONA_CODIGO_DESTINATARIO=" & CodPersona & ",CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ",CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",   PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ", GUIREM_NOMBRE_DESTINATARIO = '" & Trim(TxtDestDescripcion.Text) & "' , GUIREM_DIRECCION_LLEGADA_UBIGEO='" & TxtDestUbigeo.Text & "',GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text) & "', TRANSPORTISTA_RUC=NULL,TRANSPORTISTA_RAZONSOCIAL=NULL," _
                                & " VEHICU_PLACA=NULL,VEHICU_MARCA=NULL,VEHICU_CERT_INSCP=NULL,CHOFER_DNI=NULL,CHOFER_NOMBRES=NULL,CHOFER_LICENCIA=NULL,GUIREM_MOTIVO_DESCRIPCION=NULL , GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                                & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_PERSONA_RECIBE = '" & Trim(TxtQuienRecibe.Text) & "' , GUIREM_PERSONA_RETIRA='" & Trim(TxtQuienRetira.Text) & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                    CantLineaGuia = 30

                End If

                iGuia = 0

                If a = 1 Then
                    iGuia = 0
                Else
                    aa = a - 1
                    iGuia = (CantLineaGuia * aa) + 1
                End If

                pdCantDetalle = 0

                With GvListaArticulos
                    If psGuiaSerie <= .Rows.Count - 1 Then
                        If .Rows.Count - 1 >= iGuia Then
                            iGuia = psGuiaSerie
                            For i = iGuia To .Rows.Count - 1
                                pdCantDetalle = pdCantDetalle + 1
                                pdCantDetalleGuia = pdCantDetalleGuia + 1
                                If DdlRemitente.SelectedValue = "1" Then  'ALMACEN
                                    SalidaAlmacen = .Rows(i).Cells(1).Text
                                    SalidaCC = "NULL"
                                Else 'CC
                                    SalidaAlmacen = "NULL"
                                    SalidaCC = .Rows(i).Cells(1).Text
                                End If
                                psGuiaSerie = psGuiaSerie + 1
                                CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,OSAL_CODIGO, SERIE_NUMERAR,ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                                                  & " VALUES(" & Session("CodGuia") & "," & pdCantDetalle & "," & SalidaAlmacen & "," & SalidaCC & "," & .Rows(i).Cells(7).Text & ",'" & .Rows(i).Cells(4).Text & "','1')"
                                CmdGlobal.ExecuteNonQuery()

                                If DdlRemitente.SelectedValue = "1" Then  'ALMACEN
                                    CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET GUIREM_CODIGO=" & Session("CodGuia") & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & SalidaAlmacen & ""
                                    CmdGlobal.ExecuteNonQuery()
                                Else
                                    CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA SET GUIREM_CODIGO=" & Session("CodGuia") & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & SalidaCC & ""
                                    CmdGlobal.ExecuteNonQuery()
                                End If
                                If pdCantDetalle = CantLineaGuia And Session("TipoGuia") = "2" Then Exit For
                            Next
                        End If
                    End If
                End With

                pdCantDetalle = 0
                If psGuiaAcc <= GvListaAcc.Rows.Count - 1 Then
                    If pdCantDetalleGuia < CantLineaGuia Then
                        With GvListaAcc
                            iGuia = psGuiaAcc
                            For i = iGuia To .Rows.Count - 1
                                pdCantDetalle = pdCantDetalle + 1
                                pdCantDetalleGuia = pdCantDetalleGuia + 1
                                If DdlRemitente.SelectedValue = "1" Then  'ALMACEN
                                    SalidaAlmacen = .Rows(i).Cells(1).Text
                                    SalidaCC = "NULL"
                                Else 'CC
                                    SalidaAlmacen = "NULL"
                                    SalidaCC = .Rows(i).Cells(1).Text
                                End If
                                CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,OSAL_CODIGO,ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                                              & " VALUES(" & Session("CodGuia") & "," & pdCantDetalle & "," & SalidaAlmacen & "," & SalidaCC & ",'" & .Rows(i).Cells(2).Text & "'," & .Rows(i).Cells(5).Text & ")"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_SINSERIE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,OSAL_CODIGO, ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                                              & " VALUES(" & Session("CodGuia") & "," & pdCantDetalle & "," & SalidaAlmacen & "," & SalidaCC & "," & .Rows(i).Cells(2).Text & "," & .Rows(i).Cells(5).Text & ")"
                                CmdGlobal.ExecuteNonQuery()
                                psGuiaAcc = psGuiaAcc + 1
                                If DdlRemitente.SelectedValue = "1" Then  'ALMACEN
                                    CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET GUIREM_CODIGO=" & Session("CodGuia") & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & SalidaAlmacen & ""
                                    CmdGlobal.ExecuteNonQuery()
                                Else
                                    CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA SET GUIREM_CODIGO=" & Session("CodGuia") & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & SalidaCC & ""
                                    CmdGlobal.ExecuteNonQuery()
                                End If
                                If pdCantDetalle = CantLineaGuia And Session("TipoGuia") = "2" Then Exit For
                            Next
                        End With
                    End If
                End If

                TxtGuiaNumero.Text = Llenar_Ceros(Val(TxtGuiaNumero.Text) + 1, 8)
                If psCodGuiaVarias <> "" Then psCodGuiaVarias = psCodGuiaVarias & " ,"
                psCodGuiaVarias = psCodGuiaVarias & Session("CodGuia")

            Next

            If Session("TipoGuia") = "1" Then 'con transportista,chofer y vehiculo

                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                            & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",  CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & ", GUIREM_CURRIER = " & CodCurrier & ",GUIREM_ESTADO_ENTREGA='1',GUIREM_ESTADO_SITUACION='2'," _
                            & " GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "' ,     GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "', ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & "," _
                            & " CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ", CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",   PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ",   GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text.Trim) & "', TRANSPORTISTA_RUC='" & TxtRucTrasnportista.Text.Trim & "',TRANSPORTISTA_RAZONSOCIAL='" & TxtRazonTransportista.Text.Trim & "'," _
                            & " VEHICU_PLACA='" & TxtNroPlaca.Text.Trim & "',VEHICU_MARCA='" & TxtMarca.Text.Trim & "',VEHICU_CERT_INSCP='" & TxtCertInscripcion.Text.Trim & "',CHOFER_DNI='" & TxtChoferDNI.Text & "',CHOFER_NOMBRES='" & TxtChoferNombre.Text & "',CHOFER_LICENCIA='" & TxtLicencia.Text & "', VEHI_CONFIGURACION = '" & TxtconfVehicular.Text & "', " _
                            & " GUIREM_MOTIVO_DESCRIPCION='" & Trim(Solo_Texto(TxtMotivoDescripcion.Text)) & "' ,GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                            & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_PERSONA_RECIBE = NULL,GUIREM_PERSONA_RETIRA=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()

                If DdlModTransporte.SelectedValue <> "< Seleccionar >" Then
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  TRANSPORTISTA_MODALIDAD='" & DdlModTransporte.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                End If

                If DdlMotivoTraslado.SelectedValue <> "< Seleccionar >" Then
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_MOTIVO_TRASLADO='" & DdlMotivoTraslado.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                End If

                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_BULTO='" & TxtNroBulto.Text & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()

            Else 'sin transportista,chofer y vehiculo

                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                        & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",  CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & "," _
                        & " GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "',     GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "',     ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & "," _
                        & " CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ",  CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",  PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ",   GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text) & "', TRANSPORTISTA_RUC=NULL,TRANSPORTISTA_RAZONSOCIAL=NULL," _
                        & " VEHICU_PLACA=NULL,VEHICU_MARCA=NULL,VEHICU_CERT_INSCP=NULL,CHOFER_DNI=NULL,CHOFER_NOMBRES=NULL,CHOFER_LICENCIA=NULL,GUIREM_MOTIVO_DESCRIPCION=NULL , GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                        & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_PERSONA_RECIBE = '" & Trim(TxtQuienRecibe.Text) & "' , GUIREM_PERSONA_RETIRA='" & Trim(TxtQuienRetira.Text) & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()

            End If

            Dim psMensaje As String = ""

            If Session("TipoGuia") = "1" Then
                psMensaje = "Guía de Remisión N° " & Session("NroGuiaRem") & " Generada."
                btnImprimir.Enabled = False
                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Guía de Remisión Generada.');", True)
            Else
                btnImprimir.Enabled = True
                psMensaje = "Guía Interna N° " & Session("CodGuia") & " Generada."
                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", psMensaje, True)
            End If

            Session("ProcesoEjecutado") = True
            LblTituloModal.Text = psMensaje
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)

        End If
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        If Session("PaginaViene") = "Inventario_SalidaIngreso_alavez.aspx" Then
            Response.Redirect("~/Inventario/Inventario_SalidaIngreso_alavez.aspx")
        ElseIf Session("PaginaViene") = "Inventario_Almacen_Salida.aspx" Then
            Response.Redirect("~/Inventario/Inventario_Almacen_Salida.aspx")
        ElseIf Session("PaginaViene") = "Inventario_CCosto_Salida.aspx" Then
            Response.Redirect("~/Inventario/Inventario_CCosto_Salida.aspx")
        End If
    End Sub
    Protected Sub btnRedirectYes_Click(sender As Object, e As EventArgs) Handles btnRedirectYes.Click
        If Session("PaginaViene") = "Inventario_SalidaIngreso_alavez.aspx" Then
            Response.Redirect("~/Inventario/Inventario_SalidaIngreso_alavez.aspx")
        ElseIf Session("PaginaViene") = "Inventario_Almacen_Salida.aspx" Then
            Response.Redirect("~/Inventario/Inventario_Almacen_Salida.aspx")
        ElseIf Session("PaginaViene") = "Inventario_CCosto_Salida.aspx" Then
            Response.Redirect("~/Inventario/Inventario_CCosto_Salida.aspx")
        End If
    End Sub
    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If Session("TipoGuia") = "2" Then
            Dim numeroGuia As String = ""
            Dim fechaEmision As String = ""
            Dim fechaTraslado As String = ""
            Dim HoraEmision As String = ""
            Dim psPtoPartida As String = ""
            Dim psPtoLlegada As String = ""
            Dim motivo As String = ""
            Dim pstextoQR As String = ""
            Dim psDestinatario As String = ""
            Dim psEmpresa As String = ""
            Dim psModalidadTransporte As String = ""
            Dim psUnidadMedida As String = ""
            Dim psPesoBruto As String = ""
            Dim psNroBultos As String = ""
            Dim psTransportista As String = ""
            Dim psTransportistaRUC As String = ""
            Dim psNroRegistroMTC As String = ""
            Dim psRucDestinatario As String = ""
            Dim psUbigeoPartida As String = ""
            Dim psUbigeoLlegada As String = ""
            Dim EmpresaRuc As String = ""
            Dim EmpresaNombre As String = ""
            Dim psRemitente As String = "REMITENTE"
            Dim dt As New DataTable
            Dim dtEmp As New DataTable
            Dim psPeriodo As String = ""
            Dim psCodexRemGuia As String = ""
            Dim psCodexDesc As String = ""
            Dim psGuiaSerie As String = ""
            Dim psRetira As String = ""
            Dim psRecibe As String = ""
            Dim psObs As String = ""

            dt = obj.Lista_GuiaRemision_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), Session("CodGuia"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    psGuiaSerie = Nu(dr("GUIREM_SERIE")) + "-" + Nu(dr("Guia_Numeracion"))
                    numeroGuia = "Nro. T" & Nu(dr("GUIREM_SERIE")) + "-" + Nu(dr("Guia_Numeracion"))
                    fechaEmision = Nu(dr("Fecha_Guia"))
                    psPtoPartida = Nu(dr("GUIREM_DIRECCION_PARTIDA"))
                    psPtoLlegada = Nu(dr("GUIREM_DIRECCION_LLEGADA"))
                    pstextoQR = Nu(dr("GUIREM_QR"))
                    motivo = Nu(dr("MOTIVO_TRASLADO"))
                    psModalidadTransporte = Nu(dr("ModalidadTransporte"))
                    psDestinatario = Nu(dr("DESTINATARIO_NOMBRE"))
                    psUnidadMedida = Nu(dr("GUIREM_UNIDAD_MEDIDA_BRUTO"))
                    psPesoBruto = Nu(dr("GUIREM_PESO_BRUTO"))
                    psNroBultos = Nu(dr("GUIREM_BULTO"))
                    psTransportista = Nu(dr("TRANSPORTISTA_RAZONSOCIAL"))
                    HoraEmision = Nu(dr("Hora_Guia"))
                    fechaTraslado = Nu(dr("Fecha_Traslado"))
                    psRucDestinatario = Nu(dr("DESTINATARIO_CODINTERNO"))
                    psTransportistaRUC = Nu(dr("TRANSPORTISTA_RUC"))
                    psUbigeoPartida = Nu(dr("GUIREM_DIRECCION_PARTIDA_UBIGEO"))
                    psUbigeoLlegada = Nu(dr("GUIREM_DIRECCION_LLEGADA_UBIGEO"))
                    psPeriodo = Nu(dr("Fecha_Periodo"))
                    psNroBultos = Nu(dr("GUIREM_BULTO"))
                    psPesoBruto = Nu(dr("GUIREM_PESO_BRUTO"))
                    psUnidadMedida = Nu(dr("GUIREM_UNIDAD_MEDIDA_BRUTO"))
                    psRemitente = Nu(dr("REMITENTE_NOMBRE"))
                    psCodexRemGuia = Nu(dr("CODEXT_REM"))
                    psCodexDesc = Nu(dr("CODEX_DES"))
                    psRetira = Nu(dr("GUIREM_PERSONA_RETIRA"))
                    psRecibe = Nu(dr("GUIREM_PERSONA_RECIBE"))
                    psObs = Nu(dr("GUIREM_OBSERVACION"))
                    Exit For
                Next
            End If
            dt = Nothing

            dtEmp = objEmp.Datos_Empresa(Session("Ruta_Emp"), Session("CodEmpresa"))
            If dtEmp.Rows.Count > 0 Then
                For Each drEmp As Data.DataRow In dtEmp.Rows
                    EmpresaRuc = Nu(drEmp("emp_ruc"))
                    EmpresaNombre = Nu(drEmp("emp_nombre"))
                Next
            End If
            dtEmp = Nothing
            '
            Dim savePath As String = Server.MapPath("~/Inventario/GuiaInterna/")
            Dim fileName As String = "GuiaInterna_Nro_" & Session("CodGuia") & ".pdf" ' "Informe_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim fullPath As String = Path.Combine(savePath, fileName)

            Dim NombrePdfGuia As String = ""
            NombrePdfGuia = "GuiaInterna_Nro_" & Session("CodGuia")

            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand

            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " UPDATE TBINV_GUIA_REMISION_0001 SET GUIREM_ARCHIVO = '" & fileName & "' WHERE GUIREM_CODIGO =  " & Session("CodGuia")
            CmdGlobal.ExecuteNonQuery()
            Cn.Close()
            ' Crear el documento PDF
            Dim document As New Document(PageSize.A4.Rotate, 5, 5, 2, 2)
            Dim output As New MemoryStream() ' Crear el escritor PDF


            Dim archivo As String = Server.MapPath("~\Inventario\GuiaInterna\" & NombrePdfGuia & ".pdf") ' Ruta y nombre del archivo PDF
            Dim psRuta As String = ""
            'psRuta = "\\" & NomServer & "\GRE_PDF\" & NombrePdfGuia & ".pdf"

            Dim carpeta As String = Server.MapPath("~/Inventario/GuiaInterna/")

            ' Verificar si la carpeta existe
            If Not Directory.Exists(carpeta) Then
                ' Crear la carpeta
                Directory.CreateDirectory(carpeta)
            End If

            Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(archivo, FileMode.Create))
            ' Abrir el documento
            document.Open()

            'linea vertical punteada al centro
            Dim cb As PdfContentByte = writer.DirectContent
            cb.SetLineDash(3, 3) ' Ancho de la línea
            cb.MoveTo(document.PageSize.Width / 2, document.PageSize.Height) ' Inicio de la línea
            cb.LineTo(document.PageSize.Width / 2, 0) ' Fin de la línea
            cb.Stroke()

            ' Crear dos columnas
            Dim leftColumn As New ColumnText(writer.DirectContent)
            Dim rightColumn As New ColumnText(writer.DirectContent)

            ' Establecer coordenadas y dimensiones de las columnas
            leftColumn.SetSimpleColumn(-40, 0, document.PageSize.Width / 2 + 90, document.PageSize.Height - 10)
            rightColumn.SetSimpleColumn(document.PageSize.Width / 2 - 40, 5, document.PageSize.Width + 90, document.PageSize.Height - 10)

            ' Crear un elemento de contenido (párrafo) para las columnas
            'Dim contentParagraph As New Paragraph("Contenido que se repite en ambas columnas")


            '-----------------------------------------
            Dim bf As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 7, BaseColor.BLACK)
            Dim fFont = New iTextSharp.text.Font(bf)
            Dim bf1 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim fFont1 = New iTextSharp.text.Font(bf1)
            Dim bf2 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 6, BaseColor.BLACK)
            Dim fFont2 = New iTextSharp.text.Font(bf2)

            'crea linea
            Dim separator As New LineSeparator() ' Crear una instancia de LineSeparator
            separator.LineColor = New BaseColor(128, 128, 128) ' Negro' Configurar el color y grosor de la línea
            separator.LineWidth = 0 ' Grosor de 1 punto

            ' DateTime.Now.ToString("dd/MM/yyyyy - HH:mm:ss")


            Dim tableCabecera As New PdfPTable(3) ' Crear una tabla con 2 columnas
            Dim widths As Single() = {3.0F, 3.0F, 4.0F} '' Establecer el estilo de borde de la tabla
            tableCabecera.SetWidths(widths)
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("NRO GUIA INTERNA: " & Session("CodGuia"), fFont))
            tableCabecera.AddCell(New Phrase(EmpresaNombre, fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase(DateTime.Now.ToString("dd/MM/yyyyy"), fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase(DateTime.Now.ToString("HH:mm:ss"), fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("TRASLADO", fFont1))
            tableCabecera.AddCell(New Phrase("", fFont))

            For Each cell As PdfPCell In tableCabecera.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next
            'tabla 2
            Dim tableRemitente As New PdfPTable(3) ' Crear una tabla con 2 columnas
            Dim widthsRe As Single() = {5.0F, 2.0F, 2.0F} '' Establecer el estilo de borde de la tabla
            tableRemitente.SetWidths(widthsRe)
            tableRemitente.AddCell(New Phrase("REMITENTE    : " & psRemitente, fFont))
            tableRemitente.AddCell(New Phrase("", fFont))
            tableRemitente.AddCell(New Phrase(psCodexRemGuia, fFont))
            tableRemitente.AddCell(New Phrase("DESTINATARIO : " & psDestinatario, fFont))
            tableRemitente.AddCell(New Phrase("", fFont))
            tableRemitente.AddCell(New Phrase(psCodexDesc, fFont))

            For Each cell As PdfPCell In tableRemitente.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next

            'tabla 3
            Dim tableDetalleCab As New PdfPTable(4) ' Crear una tabla con 2 columnas
            Dim widths3 As Single() = {1.0F, 3.0F, 3.0F, 7.0F} '' Establecer el estilo de borde de la tabla
            tableDetalleCab.SetWidths(widths3)
            tableDetalleCab.AddCell(New Phrase("Cant.", fFont))
            tableDetalleCab.AddCell(New Phrase("Nro. Serie", fFont))
            tableDetalleCab.AddCell(New Phrase("Nro. Placa", fFont))
            tableDetalleCab.AddCell(New Phrase("Descripción del equipo", fFont))

            For Each cell As PdfPCell In tableDetalleCab.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next

            'tabla 4 detalle
            Dim table As New PdfPTable(4)
            Dim widths4 As Single() = {1.0F, 3.0F, 3.0F, 7.0F} '' Establecer el estilo de borde de la tabla
            table.SetWidths(widths4)
            dt = Nothing
            dt = obj.Lista_GuiaRemision_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), Session("CodGuia"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    table.AddCell(New Phrase(Nu(dr("Cant")), fFont2))
                    table.AddCell(New Phrase(Nu(dr("SERIE_NRO")), fFont2))
                    table.AddCell(New Phrase(Nu(dr("PLACA_NRO")), fFont2))
                    table.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                Next
            End If
            dt = Nothing

            dt = obj.Lista_GuiaRemision_Detalle_Acc(Session("Ruta_Emp"), Session("CodEmpresa"), Session("CodGuia"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    table.AddCell(New Phrase(Nu(dr("Cant")), fFont2))
                    table.AddCell(New Phrase("", fFont2))
                    table.AddCell(New Phrase("", fFont2))
                    table.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                Next
            End If
            dt = Nothing
            For Each cell As PdfPCell In table.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next
            ' Agregar el contenido a ambas columnas columna derecha

            ' Agregar el contenido a ambas columnas
            leftColumn.AddElement(tableCabecera)
            leftColumn.AddElement(New Chunk(separator))
            leftColumn.AddElement(tableRemitente)
            leftColumn.AddElement(New Chunk(separator))
            leftColumn.AddElement(tableDetalleCab)
            leftColumn.AddElement(New Chunk(separator))
            leftColumn.AddElement(table)

            rightColumn.AddElement(tableCabecera)
            rightColumn.AddElement(New Chunk(separator))
            rightColumn.AddElement(tableRemitente)
            rightColumn.AddElement(New Chunk(separator))
            rightColumn.AddElement(tableDetalleCab)
            rightColumn.AddElement(New Chunk(separator))
            rightColumn.AddElement(table)

            ' Agregar las columnas al documento
            leftColumn.Go()
            rightColumn.Go()
            ' Agregar una tabla al final de ambas columnas

            Dim tablaPie As New PdfPTable(2) ' Crear una tabla con 2 columnas
            Dim widths5 As Single() = {4.0F, 10.0F} '' Establecer el estilo de borde de la tabla
            tablaPie.SetWidths(widths5)
            tablaPie.AddCell(New Phrase("PERSONA QUIEN RECIBE :", fFont))
            tablaPie.AddCell(New Phrase(psRecibe, fFont))
            tablaPie.AddCell(New Phrase("PERSONA QUIEN ENTREGA  :", fFont))
            tablaPie.AddCell(New Phrase(psRetira, fFont))
            tablaPie.AddCell(New Phrase("OBSERVACION:", fFont))
            tablaPie.AddCell(New Phrase(psObs, fFont))

            Dim tableAtBottomColumnTextLeft As New ColumnText(writer.DirectContent)
            tableAtBottomColumnTextLeft.AddElement(New Chunk(separator))
            tableAtBottomColumnTextLeft.SetSimpleColumn(-20, 0, document.PageSize.Width / 2 + 20, 120)
            tableAtBottomColumnTextLeft.AddElement(tablaPie)
            tableAtBottomColumnTextLeft.AddElement(New Paragraph("  "))
            tableAtBottomColumnTextLeft.AddElement(New Paragraph("                                             ------------------------------                                                                 ----------------------------- ", fFont))
            tableAtBottomColumnTextLeft.AddElement(New Paragraph("                                         Unidad quien entrega el equipo                                                           Unidad quien recibe el equipo ", fFont))
            tableAtBottomColumnTextLeft.Go()
            Dim tableAtBottomColumnTextRight As New ColumnText(writer.DirectContent)
            tableAtBottomColumnTextRight.AddElement(New Chunk(separator))
            tableAtBottomColumnTextRight.SetSimpleColumn(document.PageSize.Width / 2 - 20, 0, document.PageSize.Width + 20, 120)
            tableAtBottomColumnTextRight.AddElement(tablaPie)
            tableAtBottomColumnTextRight.AddElement(New Paragraph("  "))
            tableAtBottomColumnTextRight.AddElement(New Paragraph("                                            ------------------------------                                                                  ----------------------------- ", fFont))
            tableAtBottomColumnTextRight.AddElement(New Paragraph("                                        Unidad quien entrega el equipo                                                           Unidad quien recibe el equipo ", fFont))


            tableAtBottomColumnTextRight.Go()

            ' Cerrar el documento
            document.Close()

            Response.Redirect("~/Inventario/Inventario_Descargas.aspx")


            '' Descargar el PDF generado
            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            'Response.TransmitFile(fullPath)
            'Response.End()

        End If



    End Sub
End Class
