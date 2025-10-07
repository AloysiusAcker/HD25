<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.master" CodeFile="CRM_Contenido_del_Correo.aspx.vb" Inherits="CRM_Contenido_del_Correo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function VerImg(input) {
            $('#ModalImagen').modal('show');
            document.getElementById("imagenVisualizar").setAttribute("src", input);
        }

    </script>

    &nbsp;<asp:Label ID="Label5" runat="server" Text="GTP - Contenido del Correo" CssClass="Titulos"></asp:Label><br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:CheckBox ID="chckTipoCorreo" runat="server" AutoPostBack="True" CssClass="control-label-2" Text="Tipo de Correo :" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlTipoCorreo" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="BtnAgregar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Agregar" />
                    <asp:Button ID="BtnAgregarTipo" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Agregar Tipo" />
                    <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Listar" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GvListaCorreo" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Label ID="LblTituloAgregarCorreo" runat="server" Text="Agregar contenido del Correo" CssClass="subTitulos" Visible="false"></asp:Label><br />
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="LblTipoCorreo" runat="server" Text="Tipo de Correo :" Class="col-lg-2 control-label" Visible="false" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlTipoCorreoAGREGAR" runat="server" CssClass="form-control" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <asp:Label ID="LblDescripcion" runat="server" Text="Descripción :" Class="col-lg-2 control-label" Visible="false" />
                    <div class="col-lg-6">
                        <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblAsunto" runat="server" Text="Asunto :" Class="col-lg-2 control-label" Visible="false" />
                    <div class="col-lg-5">
                        <textarea id="TxtAsunto" runat="server" rows="2" visible="false" class="form-control" style="resize: none"></textarea>
                    </div>
                    <asp:Label ID="LblSaludo" runat="server" Text="Saludo :" Class="col-lg-2 control-label" Visible="false" />
                    <div class="col-lg-6">
                        <textarea id="TxtSaludo" runat="server" rows="2" visible="false" class="form-control" style="resize: none"></textarea>
                    </div>
                </div>
                <div style="display: initial; position: relative; width: 47%; float: left">
                    <div class="form-group">
                        <asp:Label ID="LblCuerpo" runat="server" Text="Cuerpo :" Class="col-lg-4 control-label" Visible="false" />
                        <div class="col-lg-10">
                            <textarea id="TxtCuerpo" runat="server" rows="5" visible="false" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>
                </div>
                <div style="display: initial; position: relative; width: 47.7%; float: right">
                    <div class="form-group">
                        <asp:Label ID="LblDespedida" runat="server" Text="Despedida :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-10">
                            <textarea id="TxtDespedida" runat="server" cols="20" rows="2" visible="false" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LblFirma" runat="server" Text="Firma :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-10">
                            <textarea id="TxtFirma" runat="server" cols="20" rows="2" visible="false" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-lg-5 col-lg-offset-1">
                        <asp:FileUpload ID="UploadImagen" runat="server" ClientIDMode="Static" Visible="false" CssClass="form-control" />
                    </div>
                    <div class="col-lg-5 col-lg-offset-0">
                        <asp:FileUpload ID="UploadFirmaImagen" runat="server" Visible="false" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-lg-offset-2">
                    <asp:Button ID="BtnGuardarCorreo" runat="server" CssClass="btn btn-group" Text="" Visible="false" />
                    <asp:Button ID="BtnCancelarCorreo" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                </div>
                <asp:Label ID="LblCodTIPO" runat="server" Text="" Visible="false" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnAgregar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnCancelarCorreo" EventName="Click" />
            <asp:PostBackTrigger ControlID="BtnGuardarCorreo" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            Cargando, por favor espere......
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <div class="form-group">
                <p id="LblTotalCorreos" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros : </p>
                <p id="LblTotalCorreosL" class="control-label" style="color: darkred; font-weight: bold" runat="server" visible="false"></p>
            </div>
            <div class="row">
                <div class="col-lg-11">
                    <br />
                    <asp:GridView ID="GvListaCorreo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Image" ImageUrl="~/icono/Editar_opt.png">
                                <ItemStyle Height="10px" Width="10px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="CORREO_NUMREG" HeaderText="" SortExpression="CORREO_NUMREG" />
                            <asp:BoundField DataField="CORREO_TIPO" HeaderText="Tipo Negocio" SortExpression="CORREO_TIPO" />
                            <asp:BoundField DataField="Negocio" HeaderText="Descripción" SortExpression="Negocio" />
                            <asp:BoundField DataField="CORREO_ASUNTO" HeaderText="Asunto" SortExpression="CORREO_ASUNTO" />
                            <asp:BoundField DataField="CORREO_SALUDO" HeaderText="Saludo" SortExpression="CORREO_SALUDO" />
                            <asp:BoundField DataField="CORREO_CUERPO" HeaderText="Cuerpo" SortExpression="CORREO_CUERPO" />
                            <asp:BoundField DataField="CORREO_DESPEDIDA" HeaderText="Despedida" SortExpression="CORREO_DESPEDIDA" />
                            <asp:BoundField DataField="CORREO_FIRMA" HeaderText="Firma" SortExpression="CORREO_FIRMA" />

                            <asp:BoundField DataField="CORREO_FIRMA_IMAGEN" HeaderText="Imagen Firma" SortExpression="CORREO_FIRMA_IMAGEN" />
                            <asp:TemplateField ItemStyle-Width="20px" HeaderText="Imagen Firma">
                                <ItemTemplate>
                                    <img onclick="VerImg('data:image/jpg;base64,<%# Convert.ToBase64String(DataBinder.Eval(Container.DataItem, "FIRMAIMAGEN"))%>')" style="width: 50px" src="data:image/jpg;base64,<%# Convert.ToBase64String(DataBinder.Eval(Container.DataItem, "FIRMAIMAGEN"))%>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="CORREO_IMAGEN" HeaderText="Imagen Correo" SortExpression="CORREO_IMAGEN" />
                            <asp:TemplateField ItemStyle-Width="20px" HeaderText="Imagen Correo">
                                <ItemTemplate>
                                    <img onclick="VerImg('data:image/jpg;base64,<%# Convert.ToBase64String(DataBinder.Eval(Container.DataItem, "IMAGEN"))%>')" style="width: 50px" src="data:image/jpg;base64,<%# Convert.ToBase64String(DataBinder.Eval(Container.DataItem, "IMAGEN"))%>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GvListaCorreo" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>


    <div id="ModalAgregarTipoCorreo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label4" Text="Agregar Tipo - Correo" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_codArt">Tipo :</label>
                                                <div class="col-sm-8 col-xs-7">
                                                    <asp:TextBox ID="TxtTipo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-4">
                                                    <asp:Button ID="BtnGuardarTipo" runat="server" Text="Guardar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnCancelarTipo" runat="server" Text="Cancelar" CssClass="btn btn-default" />
                                                </div>
                                            </div>

                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    Cargando, por favor espere......
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>

                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <p id="LblTotalTipo" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros : </p>
                                                        <p id="LblTotalTipoL" class="control-label" style="color: darkred; font-weight: bold" runat="server" visible="false"></p>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-11">
                                                            <br />
                                                            <asp:GridView ID="GvListaTipo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                <Columns>
                                                                    <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="ADMIN_TCORREO_CODIGO" HeaderText="Codigo Tipo" SortExpression="ADMIN_TCORREO_CODIGO" />
                                                                    <asp:BoundField DataField="TIPO" HeaderText="Descripción" SortExpression="TIPO" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarTipo" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="ModalPregunta" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPregunta" />
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="col-sm-8 col-xs-2 col-lg-offset-3" style="padding-left: 12%">
                                            <asp:Button ID="BtnSi" CssClass="btn btn-info" runat="server" Text="Sí" />
                                            <asp:Button ID="BtnNo" ControlStyle-CssClass="btn btn-info" runat="server" Text="No" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalImagen" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <img id="imagenVisualizar" style="width: 100%" src="" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-5">
                                                    <asp:Button ID="BtnCerrarImagen" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaCorreo" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarImagen" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
