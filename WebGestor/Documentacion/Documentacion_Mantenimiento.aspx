<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.master" CodeFile="Documentacion_Mantenimiento.aspx.vb" Inherits="Documentacion_Mantenimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <%-- <style>
        input[type="file"] {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
        }
    </style>--%>
    <script type="text/javascript">
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
        
        //$("#file").change(function () {
        //    document.getElementById("txtNombreDocumento").value=$(this).val()
        //});
    </script>
    
    <div class="form-horizontal">
        <br />
        <asp:Label runat="server" Text="Mantenimiento" CssClass="Titulos"></asp:Label><br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">

            <ContentTemplate>
                <div class="form-group">
                    <div class="col-lg-12">
                        <asp:Button ID="BtnListarDocumentos" runat="server" CssClass="btn btn-group" Text="Listar" />
                        <asp:Button ID="BtnNuevoTema" runat="server" CssClass="btn btn-group" Text="Nuevo Tema" />
                        <asp:Button ID="BtnFiltro" runat="server" CssClass="btn btn-group" Text="Filtro" />


                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label ID="lblCodigoDocumento" runat="server" Text="Código :" CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtCodDoc" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label ID="lblAplicacion" runat="server" Text="Aplicacion :" CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlAplicacion" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblTipoIngreso" runat="server" Text="Tipo Ingreso :" CssClass=" col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlTipoIngreso" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha: " CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-3">
                                    <input id="TxtFecha" type="text" runat="server" class="form-control" visible="True" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblClasificacion" runat="server" Text="Clasificacion: " CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlClasificacion1" runat="server" AutoPostBack="true" Enabled="True" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlClasificacion2" runat="server" AutoPostBack="true" Enabled="True" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlClasificacion3" runat="server" AutoPostBack="true" Enabled="True" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lvlNivelAcceso" runat="server" Text="Nivel Acceso: " CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlNivelAcceso" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <asp:Label ID="lvlCargarDodumento" runat="server" Text="Documento: " CssClass="col-lg-2 control-label"></asp:Label>
                                <%--<div class="col-lg-3">
                                    <%--<asp:TextBox ID="txtNombreDocumento" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>--%>
                                   
                                <%--</div>--%>
                                <div class="col-lg-5">
                                    <asp:FileUpload ID="FileUpload1"  runat="server" CssClass="form-control col-lg-8" />
                                 <%--<label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" visible="true">Buscar Archivo</label>--%>
                                   <%-- <asp:Button ID="btnAgregar" runat="server" Text="Guardar" />--%>
                                   
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-1 col-lg-offset-1">
                                    <asp:RadioButton ID="RbTicket" runat="server" Text="Ticket " CssClass="radio radio-inline" />
                                </div>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtTicket" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <%--<asp:Label ID="lblRuta" runat="server" Text="Ruta: " CssClass="col-lg-3 control-label"></asp:Label>--%>
                             <%--   <div class="col-lg-3">
                                    <asp:TextBox ID="txtRuta" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </div>--%>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: " CssClass="col-lg-2 control-label"></asp:Label>
                                <div class="col-lg-10">
                                    <textarea id="txtDescricion" runat="server" class="form-control"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-offset-4">
                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-group" Text="Guardar" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-group" Text="Cancelar" />
                                <asp:Button ID="btnAbrirBandeja" runat="server" CssClass="btn btn-group" Text="Abrir Bandeja" />
                                <asp:Button ID="btnSMS" runat="server" CssClass="btn btn-group" Text="SMS" />
                            </div>
                        </div>
<%--                        <div>
                             <input type="text"  id="txtNombreDocumento" name="name" value="" />
                                    <input type="text" id="txtRutaDoc"name="name" value="" />
                             <input type="file" id="file"/>
                        </div>--%>
                    </ContentTemplate>
                   <Triggers>
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>           
                <asp:GridView ID="GvListaArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                    <Columns>

                        <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="~/Icono/Editar_opt.png"></asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" CommandName="Eliminar" Text="Eliminar" ImageUrl="~/Icono/delete2_opt.png"></asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="Vista del Tema" Text="Ver Tema" />
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

        </asp:UpdatePanel>

    </div>
    <script type="text/javascript">
        var fileUpload = document.getElementById("file");
        function obtenerValor() {
            document.getElementById("txtRutaDoc").value = document.getElementById("file").value;
            document.getElementById("txtNombreDocumento").value = document.getElementById("file").files[0].name;
        }
        fileUpload.onchange = () => { obtenerValor() }
        
    </script>
    <script type="text/javascript">
        var fileUpload = document.getElementById("FileUpload1");
        function obtenerValor() {
            document.getElementById("txtRutaDoc").value = document.getElementById("FileUpload1").value;
           
        }
        fileUpload.onchange = () => { obtenerValor() }
        
    </script>
</asp:Content>
