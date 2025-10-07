<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Documento_TemasAyuda.aspx.vb" Inherits="Documentacion_Documento_TemasAyuda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <%--   <script type="text/javascript">
        function showimagepreview(input) {
            var fileInput = document.getElementById('FileUpload1');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.docx)$/i;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                if (!allowedExtensions.exec(filePath)) {
                    alert('Seleccione una imagen');
                    fileInput.value = '';
                    document.getElementById("imagenCarga").setAttribute("src", "");
                    return false;
                } else {
                    reader.onload = function (e) {
                        document.getElementById("imagenCarga").setAttribute("src", e.target.result);

                    }
                    reader.readAsDataURL(input.files[0]);
                    return false;
                }
            } else {
                document.getElementById("imagenCarga").setAttribute("src", "");
            }        
        }

        function VerImg(input, name) {
            var img = document.getElementById("imagenCarga").getAttribute("src");
            if (name.toString() != "") {
                if (img.toString() == "") {
                    $('#ModalImagen').modal('show');
                    document.getElementById("imagenVisualizar").setAttribute("src", input);
                }
            }
        }
        
    </script>--%>

    <script runat="server">
        Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
            Dim strSaveFileAs As String
            Dim strStatusMessage As String = ""
            Dim posicion As Integer = 0
            Dim i As Integer = 0
            Dim NCant As String = 0
            Dim Variable As String = ""
            Dim NombreArchivo As String = ""
            Dim Mensaje As String = ""
            Dim objCas As New Cls_Documentos
            Dim dt As New Data.DataTable
            Dim CodTemaAyuda As Double = 0
            Dim psCodModulo As String = "18"
            Dim psNombreCarpeta As String = ""
            Dim objSeg As New ModuloSeguridad
            Dim psNombrePag As String = ""
            'If Session("NombrePag") <> "" Then
            '    psNombrePag = Session("NombrePag")
            'Else
            psNombrePag = "CRM/Documentacion_Mantenimiento.aspx"
            'End If
            lblError.Text = ""
            dt = objSeg.Obtener_ModuloxPagina(psNombrePag)
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    psCodModulo = dr("PAG_COD_MODULO")
                Next
            End If
            dt = Nothing
            Dim psNombreTabla As String = ""
            Dim psCodTablaRelacion As String = ""

            dt = objSeg.Obtener_TablaRelacion(Session("Ruta_Emp"), psCodModulo, "")
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    psNombreTabla = dr(1).ToString : psNombreCarpeta = dr(4).ToString : psCodTablaRelacion = dr(6).ToString
                Next
            End If

            dt = Nothing
            Dim Ruta_Final As String = Server.MapPath("Temas")

            If psNombreCarpeta <> "" Then
                Ruta_Final = Server.MapPath("Temas\" & psNombreCarpeta)
            End If
            If Not System.IO.Directory.Exists(Ruta_Final) Then
                ' Crear la carpeta
                System.IO.Directory.CreateDirectory(Ruta_Final)
            End If


            Dim clasif1 As String = DdlClasificacion1.SelectedValue
            Dim clasif2 As String = DdlClasificacion2.SelectedValue
            Dim clasif3 As String = DdlClasificacion3.SelectedValue
            Dim nivelAcceso As String = DdlNivelAcceso.SelectedValue
            Dim nomDoc As String = FileUpload1.FileName.ToString
            Dim ticket As Double = 0
            If txtTicket.Text <> "" Then
                ticket = txtTicket.Text
            End If
            Dim descripcion As String = txtDescricion.InnerText.ToString
            Dim Fecha As String = TxtFecha.Value.ToString
            Dim aplicacion As Double = 0
            If DdlAplicacion.SelectedValue <> "< Seleccionar >" Then
                aplicacion = DdlAplicacion.SelectedValue
            End If
            Dim tipoIngreso As String = DdlTipoIngreso.SelectedValue
            If DdlClasificacion1.SelectedValue = "< Seleccionar >" Then lblError.Text = "Es necesario seleccionar la Clasificación del Tema" : Exit Sub
            If DdlTipoIngreso.SelectedValue = "< Seleccionar >" Then lblError.Text = "Es necesario seleccionar el Tipo de Ingreso" : Exit Sub

            If tipoIngreso = "< Seleccionar >" Then tipoIngreso = ""
            If clasif3 = "< Seleccionar >" Then clasif3 = ""
            If clasif2 = "< Seleccionar >" Then clasif2 = ""

            Dim anio As String = Fecha.Substring(0, 4)
            Dim mes As String = Fecha.Substring(5, 2)
            Dim dia As String = Fecha.Substring(8, 2)
            Fecha = anio + mes + dia

            'archivo
            'strSaveFileAs = Server.MapPath("uploads/" & Upload.FileName)
            If (FileUpload1.HasFile) Then
                Dim FileName As String = Server.HtmlEncode(FileUpload1.FileName)
                Dim Extensión As String = ""
                FileName = System.IO.Path.GetExtension(FileName)
                Extensión = FileName
                For i = 1 To Len(FileUpload1.PostedFile.FileName)
                    If Mid(FileUpload1.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
                Next
                Variable = UCase(FileUpload1.PostedFile.FileName)
                For i = 1 To NCant
                    posicion = InStr(Variable, "\")
                    Variable = Mid(Variable, posicion + 1)
                    If i = NCant Then NombreArchivo = Variable
                Next
                If NombreArchivo = "" Then NombreArchivo = FileUpload1.PostedFile.FileName
            Else
                lblError.Text = "No hay Archivo que guardar" : Exit Sub
            End If
            Dim psCoddocumento As Double = 0
            Try
                If btnGuardar.Text = "Guardar" Then
                    dt = objCas.DocConsulta_ExisteTemaAyuda(UCase(NombreArchivo), Session("Ruta_Emp"))
                    If dt.Rows.Count > 0 Then
                        If Mensaje <> "" Then Mensaje = Mensaje & Chr(13)
                        Mensaje = Mensaje & "        " & NombreArchivo
                        If MsgBox("Se ha encontrado archivos con nombres similares :" & Chr(13) & Chr(13) & Mensaje & Chr(13) & Chr(13) & "¿De todas maneras desea guardar?", vbQuestion + vbYesNo, "Temas de Ayuda") = vbYes Then

                            dt = objCas.Insertar_Documento(Session("CodEmpresa"), Session("Ruta_Emp"), DdlClasificacion1.SelectedValue.Trim, clasif2, clasif3, NombreArchivo, descripcion, Session("User"), Fecha, Session("User") & Fecha,
                                                      tipoIngreso, nivelAcceso, psCodTablaRelacion, psCodModulo, aplicacion, 0, ticket, "1", 0)
                            If dt.Rows.Count > 0 Then
                                For Each dr As Data.DataRow In dt.Rows
                                    psCoddocumento = dr(0)
                                Next
                            End If
                            'strSaveFileAs = Server.MapPath("Temas/" & FileUpload1.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                            strSaveFileAs = Ruta_Final & "/" & FileUpload1.FileName
                            FileUpload1.SaveAs(strSaveFileAs)
                        Else
                            Exit Sub
                        End If
                    Else
                        dt = objCas.Insertar_Documento(Session("CodEmpresa"), Session("Ruta_Emp"), DdlClasificacion1.SelectedValue.Trim, clasif2, clasif3, NombreArchivo, descripcion, Session("User"), Fecha, Session("User") & Fecha,
                                                       tipoIngreso, nivelAcceso, psCodTablaRelacion, psCodModulo, aplicacion, 0, ticket, "1", 0)
                        'strSaveFileAs = Server.MapPath("Temas/" & FileUpload1.FileName)If dt.Rows.Count > 0 Then
                        If dt.Rows.Count > 0 Then
                            For Each dr As Data.DataRow In dt.Rows
                                psCoddocumento = dr(0)
                            Next
                        End If
                        strSaveFileAs = Ruta_Final & "/" & FileUpload1.FileName
                        FileUpload1.SaveAs(strSaveFileAs)
                    End If
                    dt = Nothing
                Else
                    psCoddocumento = lblCodigoDocumento.Text
                    objCas.Insertar_Documento(Session("CodEmpresa"), Session("Ruta_Emp"), DdlClasificacion1.SelectedValue.Trim, clasif2, clasif3, NombreArchivo, descripcion, Session("User"), Fecha, Session("User") & Fecha,
                                                    tipoIngreso, nivelAcceso, psCodTablaRelacion, psCodModulo, aplicacion, 0, ticket, "2", psCoddocumento)
                End If

                Dim obj As New clsInv_Procesos
                If Session("TicketNro") <> "" And Session("NombrePag") = "CRM/CRM_Relacion_Ticket.aspx" Then
                    Dim psNroTicket As String = ""
                    psNroTicket = Session("TicketNro")
                    Dim psConexion2 As String = ""
                    psConexion2 = Session("Ruta_Emp")
                    obj.Guardar_RelacionTicket(psConexion2, psNroTicket, "3", psCoddocumento, Session("User"))
                    Response.Redirect("~/CRM/CRM_Relacion_Ticket.aspx")
                Else
                    btnCancelar_Click(sender, e)
                    Call Listar_Documentos()
                End If

            Catch Ex As Data.SqlClient.SqlException
                lblError.Text = "Unable to save the uploaded file.The error was: " & Ex.Message
                lblError.Visible = True
                'lblErrorTA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            'End If
        End Sub
    </script>
     <div class="container-fluid"> 
         
<%--    
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate> --%>
        <div class="row">
            <div class="col-lg-12 col-md-6">
                <asp:Label ID="LblTitulo" runat="server" Text="Mantenimiento de Documentos" CssClass="Titulos"></asp:Label><br/><br/>
            </div>
        </div>
          
        <div class="row">
            <div class="col-lg-12 col-md-6">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label"></asp:Label><br/><br/>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-sm-6">
                <asp:Button ID="BtnListarDocumentos" runat="server" CssClass="btn btn-default" Text="Listar" />
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Button ID="BtnNuevoTema" runat="server" CssClass="btn btn-default" Text="Nuevo Tema" />
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Button ID="BtnFiltro" runat="server" CssClass="btn btn-default" Text="Filtro" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-sm-6">
                <asp:Label ID="lblCodigoDocumento" runat="server" Text="Código :" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="txtCodDoc" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-lg-3 col-sm-6">                        
                <asp:Label ID="lblAplicacion" runat="server" Text="Aplicacion :" CssClass="control-label"></asp:Label>
                <asp:DropDownList ID="DdlAplicacion" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-lg-3 col-sm-6">                            
                <asp:Label ID="lblTipoIngreso" runat="server" Text="Tipo Ingreso :" CssClass="control-label"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>  
                        <asp:DropDownList ID="DdlTipoIngreso" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DdlAplicacion" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-sm-6">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha: " CssClass="control-label"></asp:Label>
                <input id="TxtFecha" type="text" runat="server" class="form-control" visible="True" />
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Label ID="lvlNivelAcceso" runat="server" Text="Nivel Acceso: " CssClass="control-label"></asp:Label>
                <asp:DropDownList ID="DdlNivelAcceso" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-1 col-sm-1">
                <asp:Label ID="Label3" runat="server" Text="Ticket" CssClass="control-label" ForeColor="White" ></asp:Label>
                <asp:RadioButton ID="RbTicket" runat="server" Text="Ticket " CssClass="radio radio-inline" />
            </div>
            <div class="col-md-2 col-sm-2">
                <asp:Label ID="Label4" runat="server" Text="Ticket" CssClass="control-label" ForeColor="White" ></asp:Label>
                <asp:TextBox ID="txtTicket" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
        </div> 
        <div class="row">
            <div class="col-lg-3 col-sm-6">
                <asp:Label ID="lblClasificacion" runat="server" Text="Clasificacion: " CssClass="control-label"></asp:Label>
                <asp:DropDownList ID="DdlClasificacion1" runat="server" AutoPostBack="true" Enabled="True" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Label ID="Label1" runat="server" Text="Clasificacion: " CssClass="control-label"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>  
                        <asp:DropDownList ID="DdlClasificacion2" runat="server" AutoPostBack="true" Enabled="True" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DdlClasificacion1" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Label ID="Label2" runat="server" Text="Clasificacion: " CssClass="control-label"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>  
                        <asp:DropDownList ID="DdlClasificacion3" runat="server" AutoPostBack="true" Enabled="True" CssClass="form-control"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DdlClasificacion2" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-9 col-sm-6">
                <asp:Label ID="lvlCargarDodumento" runat="server" Text="Documento: " CssClass="control-label"></asp:Label>
                <asp:FileUpload ID="FileUpload1"  runat="server" CssClass="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-9 col-sm-6">                        
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: " CssClass="control-label"></asp:Label>
                <textarea id="txtDescricion"  runat="server" class="form-control" cols="20" rows="2"></textarea>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-3 col-sm-6">                        
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-default" Text="Guardar" />
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default" Text="Cancelar" />
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Button ID="btnAbrirBandeja" runat="server" CssClass="btn btn-default" Text="Abrir Bandeja" />
            </div>
            <div class="col-lg-3 col-sm-6">
                <asp:Button ID="btnSMS" runat="server" CssClass="btn btn-default" Text="SMS" visible="false" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12 col-sm-6">    
                <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
            </div>
        </div>
         
        <div class="row">
            <div class="col-lg-12 col-sm-6">   
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>      
                        <asp:GridView ID="GvListaArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" OnRowCommand="GvListaArticulos_RowCommand">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="~/Icono/Editar_opt.png"></asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" Text="Eliminar" ImageUrl="~/Icono/delete2_opt.png"></asp:ButtonField>
                                <asp:TemplateField HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkPDF2" runat="server" Text="Ver"  NavigateUrl='<%# Eval("TEMA_AYUDA_NOMBRE_DOC", "~/Documentacion/Temas/{0}") %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TA_APLICACION_DESCRIPCION" HeaderText="Aplicacion" SortExpression="APLICACION" />
                                <asp:BoundField DataField="TIPOINGRESO" HeaderText="Tipo Ingreso" SortExpression="ELEMENTO_TIPO_INGRESO" />
                                <asp:BoundField DataField="N4" HeaderText="Clasificación" SortExpression="CLASIFICACION" />
                                <asp:BoundField DataField="TEMA_AYUDA_NOMBRE_DOC" HeaderText="Nombre del Documento" SortExpression="ELEMENTO_NOMBRE_DOCUMENTO" />
                                <asp:BoundField DataField="TEMA_AYUDA_DESCRIPCION" HeaderText="DESCRIPCION" SortExpression="ELEMENTO_DESCRIPCION" />
                                <asp:BoundField DataField="USUARIO" HeaderText="Usuario" SortExpression="ELEMENTO_USUARIO" />
                                <asp:BoundField DataField="TEMA_AYUDA_CODIGO" SortExpression="TEMA_AYUDA_CODIGO">
                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListarDocumentos" EventName="Click"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div> 
 
    
</asp:Content>
