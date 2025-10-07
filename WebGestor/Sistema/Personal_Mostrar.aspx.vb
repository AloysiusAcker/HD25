Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Imports System.IO
Imports ImageResizer

Partial Class Personal_Mostrar
    Inherits System.Web.UI.Page
    Private objSeg As New ModuloSeguridad
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                lblError.Text = ""
                Dim dt As New DataTable
                Dim dt2 As New DataTable
                Dim i As Integer = 0
                Dim xi As Integer = 0
                cboGE.Items.Clear() : cboGEE.Items.Clear()
                dt = objSeg.Lista_GrupoEmpresa(HttpContext.Current.User.Identity.Name, "1")
                dt2 = objSeg.Lista_GrupoEmpresa("", "4")
                If dt2.Rows.Count = 0 Then lblError.Text = "No se ha podido encontrar en que empresa o empresas pertenece el personal."
                If dt.Rows.Count > 0 And dt2.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        For Each dr2 As Data.DataRow In dt2.Rows
                            If dr("GEECOD") = dr2("GEECOD") Then
                                Dim Item As New ListItem
                                Item.Text = dr("GE_NOMBRE")
                                Item.Value = dr("GEECOD")
                                cboGE.Items.Add(Item) : Exit For
                            End If
                        Next
                    Next
                End If
                dt = Nothing
                dt2 = Nothing
                cboGE.Items.Add("< Seleccionar >") : cboGE.SelectedValue = "< Seleccionar >"
                cboGEE.Items.Add("< Seleccionar >") : cboGEE.SelectedValue = "< Seleccionar >"
                If cboGE.Items.Count = 1 Then
                    FlexP.DataSource = Nothing
                    FlexP.DataBind()
                    lblError.Text = "Su usuario no tiene accesos a las empresas que lleva mantenimiento del personal."
                    Exit Sub
                Else
                    btnActivo.Enabled = True
                    btnEliminado.Enabled = True
                    btnRetirado.Enabled = True
                    If cboGE.Items.Count > 1 Then cboGE.Items.Add("<< Todos >>")
                    cboGE.SelectedValue = Session("CodGrupoEmpresa")
                    'lblError.Text = "Le recordamos que el Grupo Actual " & Session("NombreGrupoEmpresa") & " no lleva mantenimiento del personal."
                    If cboGE.SelectedValue <> "< Seleccionar >" Then cboGE_SelectedIndexChanged(sender, e)
                End If
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Private Sub Listar_Personal(ByVal sysest As String, ByVal codest As String, _
                                ByVal apepat As String, ByVal apemat As String, _
                                ByVal nombres As String, ByVal CodGrupo As Double, ByVal CodEmpresa As String)
        Try
            Dim objSeg As New ModuloSeguridad
            FlexP.DataSource = objSeg.Listar_Personal(sysest, codest, apepat, apemat, nombres, CodGrupo, CodEmpresa, "1")
            FlexP.DataBind()
        Catch Ex As SqlException
            'lblError.Visible = True
            'lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            'lblError.Visible = True
            'lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If cboGE.SelectedValue = "< Seleccionar >" Or cboGE.SelectedValue = "<< Todos >>" Then lblError.Text = "Seleccionar Grupo de Empresa" : Exit Sub
        If cboGEE.SelectedValue = "< Seleccionar >" Or cboGEE.SelectedValue = "<< Todos >>" Then lblError.Text = "Seleccionar Empresa" : Exit Sub
        fraDatosPersonal.Visible = True
        lblError.Text = "" : txtUsuario.Text = "" : txtApepat.Text = ""
        txtApemat.Text = "" : txtNombres.Text = "" : txtEmail.Text = "" : txtDireccion.Text = ""
        cboEstado.Items.Clear() : cboSexo.Items.Clear() : cboPais.Items.Clear()
        cboDpto.Items.Clear() : cboProv.Items.Clear() : cboDist.Items.Clear()
        Call LlenaComboItem("TBOPC019", cboSexo) : Call LlenaComboItem("TBOPC032", cboEstado)
        Call LlenaComboItem("TBOPC006", cboPais) : Call LlenaComboItem("TBOPC002", cboDpto)
        lblEtiqueta.Text = "Nuevo Personal para la Empresa " & cboGEE.SelectedItem.Text & " del Grupo " & cboGE.SelectedItem.Text
        Dim Cdg_Principal As String = ""
        Cdg_Principal = Genera_CodUni_Personal("N")
        Session("Usuario") = Cdg_Principal
        txtUsuario.Text = Mid(Cdg_Principal, 1, 4) + "-" + Mid(Cdg_Principal, 5, 4)
        If cboEstado.Items.Count > 1 Then cboEstado.SelectedValue = "00"
        cboPais.SelectedValue = "51"
        cboPais_SelectedIndexChanged(sender, e)
        cboEstado.Enabled = False
        FlexP.Enabled = False
        btnNuevo.Enabled = False
        imgUsuario.ImageUrl = "~/Fotos/persona.jpg" ' Imagen por defecto si no hay ID
    End Sub
    Private Sub Listar(ByVal pSysEst As String, ByVal pCodest As String)
        lblError.Text = ""
        Dim dCodGrupo As Double = 0
        Dim pCodempresa As String = ""
        If cboGE.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Grupo de Empresa" : Exit Sub
        If cboGEE.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Empresa" : Exit Sub
        If cboGE.SelectedValue = "<< Todos >>" Then dCodGrupo = 0 Else dCodGrupo = cboGE.SelectedValue.Trim
        If cboGEE.SelectedValue = "<< Todos >>" Then pCodempresa = "%" Else pCodempresa = cboGEE.SelectedValue.Trim
        Call Listar_Personal(pSysEst, pCodest, "%", "%", "%", dCodGrupo, pCodempresa)
    End Sub
    Protected Sub btnActivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivo.Click
        Session("SysEst") = "0"
        Session("Codest") = "00"
        Call Listar(Session("SysEst"), Session("Codest"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnRetirado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetirado.Click
        Session("SysEst") = "0"
        Session("Codest") = "01"
        Call Listar(Session("SysEst"), Session("Codest"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnEliminado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminado.Click
        Session("SysEst") = "1"
        Session("Codest") = "00"
        Call Listar(Session("SysEst"), Session("Codest"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboGE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGE.SelectedIndexChanged
        Try
            lblError.Text = ""
            Dim objSeg As New ModuloSeguridad
            Dim dt As New Data.DataTable
            Dim dt2 As New Data.DataTable
            Dim CodGrupoEmp As Double = 0
            If cboGE.SelectedValue = "< Seleccionar >" Then
                cboGEE.Enabled = False
            Else
                CodGrupoEmp = cboGE.SelectedValue.Trim
                cboGEE.Items.Clear()
                cboGEE.DataSource = objSeg.Lista_Empresa(HttpContext.Current.User.Identity.Name, CodGrupoEmp, "5")
                cboGEE.DataTextField = "GEE_NOMBRE"
                cboGEE.DataValueField = "GEE_CODIGO"
                cboGEE.DataBind()
                cboGEE.Items.Add("< Seleccionar >") : cboGEE.SelectedValue = "< Seleccionar >"
                cboGEE.Enabled = True
            End If
            If cboGEE.Items.Count > 1 Then
                cboGEE.Items.Add("<< Todos >>")
            End If
            cboGEE.SelectedValue = Session("CodEmpresa")
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDireccion.TextChanged

    End Sub
    Protected Sub TextBox5_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        fraDatosPersonal.Visible = False
        Call Limpiar()
        FlexP.Enabled = True
        btnNuevo.Enabled = True
    End Sub
    Private Sub Limpiar()
        lblError.Text = ""
        txtUsuario.Text = "" : txtApepat.Text = ""
        txtApemat.Text = "" : txtNombres.Text = ""
        txtEmail.Text = "" : txtDireccion.Text = ""
        cboEstado.Items.Clear() : cboSexo.Items.Clear() : cboPais.Items.Clear()
        cboDpto.Items.Clear() : cboProv.Items.Clear() : cboDist.Items.Clear()
        cboEstado.Items.Add("< Seleccionar >") : cboEstado.SelectedValue = "< Seleccionar >"
        cboSexo.Items.Add("< Seleccionar >") : cboSexo.SelectedValue = "< Seleccionar >"
        cboPais.Items.Add("< Seleccionar >") : cboPais.SelectedValue = "< Seleccionar >"
        cboDpto.Items.Add("< Seleccionar >") : cboDpto.SelectedValue = "< Seleccionar >"
        cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Try
            LblError.Text = ""
            If cboEstado.SelectedValue = "< Seleccionar >" Then MsgBox("Falta seleccionar su estado.", vbExclamation, "Mantenimiento del Personal") : Exit Sub
            If txtApepat.Text.Trim = "" Then
                LblError.Text = "Falta ingresar el apellido paterno (Obligatorio)."
                Exit Sub
            End If
            If txtNombres.Text.Trim = "" Then
                LblError.Text = "Falta ingresar el Nombre (Obligatorio)."
                Exit Sub
            End If
            Dim obj As New ClsPersonal
            Dim psconexion As String = Session("Ruta_Emp")
            Session("Usuario") = ""
            btnCancelar_Click(sender, e)
            Call Listar("0", "00")
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDpto.SelectedIndexChanged
        If cboDpto.SelectedValue = "< Seleccionar >" Then
            cboProv.Items.Clear() : cboDist.Items.Clear()
            cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
            cboDist.Items.Clear() : cboDist.Items.Clear()
            cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        Else
            cboProv.Items.Clear() : cboDist.Items.Clear()
            If cboDpto.SelectedValue <> "< Seleccionar >" Then
                Try
                    Dim obj As New Listados
                    cboProv.DataSource = obj.Listar_Provincia(cboDpto.SelectedValue.Trim)
                    cboProv.DataTextField = "ELEMEN_VALOR"
                    cboProv.DataValueField = "ELEMEN_CODIGO"
                    cboProv.DataBind()
                    cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
                Catch ex As SqlException
                    lblError.Text = ex.Message
                Catch Ex As Exception
                    lblError.Text = Ex.Message
                Finally
                End Try
                cboProv_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub
    Protected Sub cboPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPais.SelectedIndexChanged
        If cboPais.SelectedValue = "51" Then
            Call LlenaComboItem("TBOPC002", cboDpto)
            cboDpto.Items.Add("< Seleccionar >") : cboDpto.SelectedValue = "< Seleccionar >"
        Else
            cboDpto.Items.Clear() : cboProv.Items.Clear() : cboDist.Items.Clear()
            cboDpto.Items.Add("< Seleccionar >") : cboDpto.SelectedValue = "< Seleccionar >"
            cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
            cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        End If
        cboDpto_SelectedIndexChanged(sender, e)
    End Sub
    Protected Sub cboProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProv.SelectedIndexChanged
        Try
            Dim obj As New Listados
            cboDist.Items.Clear()
            cboDist.DataSource = obj.Listar_Distrito(cboDpto.SelectedValue.Trim, cboProv.SelectedValue.Trim)
            cboDist.DataTextField = "ELEMEN_VALOR"
            cboDist.DataValueField = "ELEMEN_CODIGO"
            cboDist.DataBind()
            cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch Ex As Exception
            LblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Function Guarda_Registro(ByVal CodPerson As String) As Boolean
        Dim pUsuario As String = ""
        Dim dt As New DataTable
        Dim dCodGrupo As Double = 0
        Dim N1 As String, n2 As String, n3 As String, Estad As String, Mensaje As String
        dCodGrupo = cboGE.SelectedValue.Trim
        Guarda_Registro = True
        If lblEtiqueta.Text = "Editar Personal para la Empresa " & cboGEE.SelectedItem.Text & " del Grupo " & cboGE.SelectedItem.Text Then
            dt = objSeg.Existe_Personal(CodPerson, "1")
            If dt.Rows.Count > 0 Then
                objSeg.InsUpd_Personal(HttpContext.Current.User.Identity.Name, CodPerson, dCodGrupo, "", "2")
            Else
                dt = Nothing
                Guarda_Registro = False
                lblError.Text = "Error inesperado, no se encontró los registros del personal." & Chr(13) & "Cierre esta ventana e intente de nuevo."
                Exit Function
            End If
            dt = Nothing
            Call Cuerpo_Grabar()
        Else
            N1 = "" : n2 = "" : n3 = "" : Estad = "" : Mensaje = ""
            If BuscaDuplicados_Personal(txtApepat.Text, txtApemat.Text, txtNombres.Text, Estad, N1, n2, n3) = 1 Then
                If N1 <> "" Then Mensaje = Mensaje & "     " & N1 & Chr(13)
                If n2 <> "" Then Mensaje = Mensaje & "     " & n2 & Chr(13)
                If n3 <> "" Then Mensaje = Mensaje & "     " & n3 & Chr(13)
                If MsgBox("Se ha encontrado lo sgte : " & Chr(13) & Mensaje & "¿Está seguro de guardar los datos ingresados de todas maneras?.", vbQuestion + vbYesNo, "Mantenimiento del Personal") = vbYes Then
                    GoTo Grabar_Datos
                Else
                    Guarda_Registro = False
                    Exit Function
                End If
            End If
Grabar_Datos:
            pUsuario = Session("Usuario")
            dt = objSeg.Existe_Personal(pUsuario, "2")
            If dt.Rows.Count > 0 Then
                dt = Nothing
                Guarda_Registro = False
                MsgBox("Se ha detectado que el código de personal ya existe, pulse Aceptar ó Ok para generarle un nuevo código y nuevamente le da guardar.", vbExclamation, "Mantenimiento del Personal")
                pUsuario = Genera_CodUni_Personal("N")
                txtUsuario.Text = Mid(pUsuario, 1, 4) + "-" + Mid(pUsuario, 5, 4)
                Exit Function
            End If
            dt = Nothing
            pUsuario = Genera_CodUni_Personal("S")
            Session("Usuario") = pUsuario
            objSeg.InsUpd_Personal(HttpContext.Current.User.Identity.Name, pUsuario, dCodGrupo, "", "1")
            objSeg.InsUpd_PersonalEmpresa(HttpContext.Current.User.Identity.Name, pUsuario, dCodGrupo, cboGEE.SelectedValue.Trim, "1")
            objSeg.InsUpd_Personal(HttpContext.Current.User.Identity.Name, pUsuario, dCodGrupo, cboGEE.SelectedValue.Trim, "3")
            Call Cuerpo_Grabar()
            Me.Page.Session.Timeout = 1080
        End If
    End Function
    Private Sub Cuerpo_Grabar()
        Dim St1 As String, St2 As String, pDpto As String
        Dim dcodGrupo As Double = 0
        dcodGrupo = cboGE.SelectedValue.Trim
        If cboProv.SelectedValue <> "< Seleccionar >" Then pDpto = cboDpto.SelectedValue.Trim Else pDpto = "0"
        If cboProv.SelectedValue <> "< Seleccionar >" Then St1 = cboProv.SelectedValue.Trim Else St1 = "0"
        If cboDist.SelectedValue <> "< Seleccionar >" Then St2 = cboDist.SelectedValue.Trim Else St2 = "0"
        objSeg.InsUpd_Personal(HttpContext.Current.User.Identity.Name, Session("Usuario"), dcodGrupo, cboGEE.SelectedValue.Trim, "3", cboEstado.SelectedValue.Trim, txtApepat.Text.Trim, txtApemat.Text.Trim, txtNombres.Text.Trim, cboSexo.SelectedValue.Trim, txtEmail.Text.Trim, cboPais.SelectedValue.Trim, txtDireccion.Text.Trim, txtCodInterno.Text.Trim)
        If pDpto <> "0" Then objSeg.InsUpd_PersonalUbigeo(Session("Usuario"), pDpto, St1, St2, "1") 'ingresa solo departamento
        If St1 <> "0" Then objSeg.InsUpd_PersonalUbigeo(Session("Usuario"), pDpto, St1, St2, "2") 'ingresa solo provincia
        If St2 <> "0" Then objSeg.InsUpd_PersonalUbigeo(Session("Usuario"), pDpto, St1, St2, "3") 'ingresa solo distrito
        objSeg.InsUpd_Usuarios("S", Session("Usuario"), txtCodInterno.Text.Trim, txtApepat.Text.Trim, txtApepat.Text.Trim, txtNombres.Text.Trim, "", "", "3")
    End Sub
    Private Function BuscaDuplicados_Personal(ByVal Pat As String, ByVal Mat As String, ByVal Nom As String, ByRef Est As String, ByRef Nom1 As String, ByRef Nom2 As String, ByRef Nom3 As String) As Integer
        Dim ap As String, am As String, nm As String
        Dim i As Integer = 0
        Dim dt As New DataTable
        ap = Tildes(Trim(Pat))
        am = Tildes(Trim(Mat))
        nm = Tildes(Trim(Nom))
        BuscaDuplicados_Personal = 0
        dt = objSeg.Listar_Personal("0", Est, ap, am, nm, 0, "", "2")
        If dt.Rows.Count > 0 Then
            BuscaDuplicados_Personal = 1
            For Each dr As DataRow In dt.Rows
                i = i + 1
                If dt.Rows.Count = 1 Then Nom1 = "- Estado " & dr("CodEst") + " : " + dr("PERSON_APEPAT") + " " + dr("PERSON_APEMAT") + ", " + dr("PERSON_NOMBRES")
                If dt.Rows.Count = 2 Then
                    If i = 1 Then Nom1 = "- Estado " & dr("CodEst") + " : " + dr("PERSON_APEPAT") + " " + dr("PERSON_APEMAT") + ", " + dr("PERSON_NOMBRES")
                    If i = 2 Then Nom2 = "- Estado " & dr("CodEst") + " : " + dr("PERSON_APEPAT") + " " + dr("PERSON_APEMAT") + ", " + dr("PERSON_NOMBRES")
                End If
                If dt.Rows.Count >= 3 Then
                    If i = 1 Then Nom1 = "- Estado " & dr("CodEst") + " : " + dr("PERSON_APEPAT") + " " + dr("PERSON_APEMAT") + ", " + dr("PERSON_NOMBRES")
                    If i = 2 Then Nom2 = "- Estado " & dr("CodEst") + " : " + dr("PERSON_APEPAT") + " " + dr("PERSON_APEMAT") + ", " + dr("PERSON_NOMBRES")
                    If i = 3 Then Nom3 = "- Estado " & dr("CodEst") + " : " + dr("PERSON_APEPAT") + " " + dr("PERSON_APEMAT") + ", " + dr("PERSON_NOMBRES")
                End If
            Next
        End If
    End Function
    Protected Sub FlexP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexP.PageIndexChanging
        lblError.Text = ""
        FlexP.PageIndex = e.NewPageIndex
        Call Listar(Session("SysEst"), Session("Codest"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Editar" Or e.CommandName = "Foto" Then
            fraDatosPersonal.Visible = True
            Call LlenaComboItem("TBOPC019", cboSexo)
            Call LlenaComboItem("TBOPC032", cboEstado)
            Call LlenaComboItem("TBOPC006", cboPais)
            Call LlenaComboItem("TBOPC002", cboDpto)
            cboPais.SelectedValue = "51"
            Session("Usuario") = FlexP.Rows(Index).Cells(2).Text.Trim
            lblEtiqueta.Text = "Editar Personal para la Empresa " & cboGEE.SelectedItem.Text & " del Grupo " & cboGE.SelectedItem.Text
            Call DatosPersonales(FlexP.Rows(Index).Cells(2).Text.Trim, sender, e)
            FlexP.Enabled = False
            dvFoto.Visible = False
            BtnGuardar.Enabled = True
            BtnCancelar.Enabled = True
            btnNuevo.Enabled = False
            cboEstado.Enabled = True
            txtCodInterno.ReadOnly = False
            txtApemat.ReadOnly = False
            txtApepat.ReadOnly = False
            txtNombres.ReadOnly = False
            txtDireccion.ReadOnly = False
            txtEmail.ReadOnly = False
            cboSexo.Enabled = True
            cboDist.Enabled = True
            cboDpto.Enabled = True
            cboProv.Enabled = True
            cboProv.Enabled = True
            If e.CommandName = "Foto" Then
                dvFoto.Visible = True
                BtnGuardar.Enabled = False
                txtCodInterno.ReadOnly = True
                txtApemat.ReadOnly = True
                txtApepat.ReadOnly = True
                txtNombres.ReadOnly = True
                txtDireccion.ReadOnly = True
                txtEmail.ReadOnly = True
                cboProv.Enabled = False
                cboSexo.Enabled = False
                cboDist.Enabled = False
                cboDpto.Enabled = False
                cboProv.Enabled = False
                cboEstado.Enabled = False
            End If
            Dim connectionString As String = Session("Ruta_Emp")
            Dim objNombre As New Cls_Catalogo
            Session("ComprimirImagen") = "No"
            imgUsuario.ImageUrl = "~/Fotos/persona.jpg" ' Imagen por defecto si no hay ID
            Using connection As New SqlConnection(Ruta_GrEmp)
                Using cmd As New SqlCommand("SELECT PERSON_IMAGEN as imagen FROM TBPERSONAL WHERE PERSON_CODIGO  = @PERSON_CODIGO", connection)
                    cmd.Parameters.Add("@PERSON_CODIGO", SqlDbType.VarChar).Value = Session("Usuario") ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                Session("ComprimirImagen") = "Si"
                            Else
                                Session("ComprimirImagen") = "No"
                            End If
                        End If
                    End Using
                End Using
            End Using

            If Session("ComprimirImagen") = "Si" Then
                ComprimirImagenEnBaseDeDatos(Session("Usuario"))
            End If

            Dim query As String = "SELECT PERSON_IMAGEN as imagen FROM TBPERSONAL WHERE PERSON_CODIGO  = @PERSON_CODIGO"
            Using connection As New SqlConnection(Ruta_GrEmp)
                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.Add("@PERSON_CODIGO", SqlDbType.VarChar).Value = Session("Usuario") ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imgUsuario.ImageUrl = "data:image/jpeg;base64," + base64String
                                imgUsuario.Visible = True
                                Session("NuevaImagen") = "No"
                            End If
                        End If
                    End Using
                End Using
            End Using
        End If
    End Sub
    Protected Sub ComprimirImagen(rutaOriginal As String, rutaComprimida As String)
        Dim settings As New ResizeSettings("maxwidth=800&maxheight=600&format=jpg")
        ImageBuilder.Current.Build(rutaOriginal, rutaComprimida, settings)
    End Sub

    Private Sub BtnGuardarImg_Click(sender As Object, e As EventArgs) Handles BtnGuardarImg.Click
        Try
            Dim obj As New ClsPersonal

            If FileUpload2.HasFile Then

                Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
                Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")
                FileUpload2.SaveAs(rutaOriginal)
                ComprimirImagen(rutaOriginal, rutaComprimida)
                Dim bytesImagen As Byte() = File.ReadAllBytes(rutaComprimida)

                Dim filename As String = Path.GetFileName(FileUpload2.PostedFile.FileName)

                Dim Cn As New SqlConnection(Ruta_GrEmp)
                Dim cmdSql As New SqlCommand
                'Dim Rs As SqlDataReader
                Dim pdCodImg As Double = 0

                Dim inputStream As System.IO.Stream = FileUpload2.PostedFile.InputStream
                Dim tamaño As Integer = FileUpload2.PostedFile.ContentLength
                Dim imagenData(tamaño - 1) As Byte
                inputStream.Read(imagenData, 0, tamaño)
                obj.GuardarImagen(Session("Ruta_Emp"), Session("Usuario"), bytesImagen)

            End If

            Using connection As New SqlConnection(Ruta_GrEmp)
                Using cmd As New SqlCommand("SELECT PERSON_IMAGEN as imagen FROM TBPERSONAL WHERE PERSON_CODIGO  = @PERSON_CODIGO", connection)
                    cmd.Parameters.Add("@PERSON_CODIGO", SqlDbType.VarChar).Value = Session("Usuario") ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imgUsuario.ImageUrl = "data:image/jpeg;base64," + base64String
                                imgUsuario.Visible = True
                            End If
                        End If
                    End Using
                End Using
            End Using



        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub ComprimirImagenEnBaseDeDatos(ByVal psUsuario As String)
        ' Cadena de conexión a la base de datos
        Dim connectionString As String = Ruta_GrEmp

        ' Establece la consulta para recuperar la imagen
        Dim query As String = "SELECT person_imagen FROM tbpersonal WHERE person_codigo =  '" & psUsuario & "'"

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Using command As New SqlCommand(query, connection)

                ' Lee la imagen de la base de datos
                Dim bytesImagenOriginal As Byte() = DirectCast(command.ExecuteScalar(), Byte())

                ' Guarda los bytes en un archivo temporal
                Dim rutaTemporal As String = Path.GetTempFileName()
                File.WriteAllBytes(rutaTemporal, bytesImagenOriginal)

                ' Comprime la imagen utilizando ImageResizer
                Dim settings As New ResizeSettings("maxwidth=600&maxheight=600&format=jpg")
                ImageBuilder.Current.Build(rutaTemporal, rutaTemporal, settings)

                ' Lee los bytes de la imagen comprimida
                Dim bytesImagenComprimida As Byte() = File.ReadAllBytes(rutaTemporal)

                ' Actualiza los bytes de la imagen comprimida en la base de datos
                Dim updateQuery As String = "UPDATE tbpersonal SET person_imagen = @Imagen WHERE person_codigo = '" & psUsuario & "'"

                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@Imagen", bytesImagenComprimida)
                    updateCommand.ExecuteNonQuery()
                End Using


                ' Elimina el archivo temporal
                File.Delete(rutaTemporal)
            End Using
        End Using
    End Sub


    Private Sub DatosPersonales(ByVal codigo As String, ByVal sender As Object, ByVal e As System.EventArgs)
        txtUsuario.Text = Mid(codigo, 1, 4) & "-" & Mid(codigo, 5, 4)
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        dt = objSeg.Existe_Personal(codigo, "3")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                txtCodInterno.Text = dr("PERSON_COD_INTERNO")
                cboEstado.SelectedValue = Nu(dr("PERSON_CODEST").ToString)
                ''cboEstado.SelectedValue = IIf(Left(dr("PERSON_CODEST"), 1) = "0", Right(dr("PERSON_CODEST"), 1), dr("PERSON_CODEST"))
                txtApepat.Text = dr("PERSON_APEPAT")
                txtApemat.Text = dr("PERSON_APEMAT")
                txtNombres.Text = dr("PERSON_NOMBRES")
                txtEmail.Text = dr("PERSON_EMAIL")
                If Nu(dr("person_sexo")) <> "" Then
                    If IsDBNull(dr("person_sexo")) = True Then cboSexo.SelectedValue = Nu(dr("person_sexo"))
                End If
                If IsDBNull(dr("person_dom_pais")) = True Then cboPais.SelectedValue = "51" Else cboPais.SelectedValue = dr("person_dom_pais") : cboPais_SelectedIndexChanged(sender, e)
                If IsDBNull(dr("person_dom_dpto")) = True Then cboDpto.SelectedValue = "< Seleccionar >" Else cboDpto.SelectedValue = dr("person_dom_dpto") : cboDpto_SelectedIndexChanged(sender, e)
                If IsDBNull(dr("person_dom_provincia")) = True Then cboProv.SelectedValue = "< Seleccionar >" Else cboProv.SelectedValue = dr("person_dom_provincia") : cboProv_SelectedIndexChanged(sender, e)
                If IsDBNull(dr("person_dom_distrito")) = True Then cboDist.SelectedValue = "< Seleccionar >" Else cboDist.SelectedValue = dr("person_dom_distrito")
                If IsDBNull(dr("PERSON_DOM_DIRECCION")) = True Then txtDireccion.Text = "" Else txtDireccion.Text = dr("PERSON_DOM_DIRECCION")
            Next
        Else
            lblError.Text = "No existe registro en Personal de Empresa para su Usuario."
        End If
    End Sub

    Private Sub FlexP_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles FlexP.RowCancelingEdit

    End Sub
End Class
