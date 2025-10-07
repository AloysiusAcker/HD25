<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.master" CodeFile="CRM_Enviar_Email.aspx.vb" Inherits="CRM_Enviar_Email" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        input[class="file"] {
            width: 0.1px;
            height: 10px;
            opacity: 0;
        }
    </style>


    <script type="text/javascript">
        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>

    &nbsp;<asp:Label ID="Label5" runat="server" Text="GTP - Envial Email" CssClass="Titulos"></asp:Label><br />
    <br />

    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
        <ContentTemplate>
            <div style="display: initial; position: relative; width: 60%; float: left;">
                <div class="col-lg-offset-1">
                    <asp:Button ID="BtnDireccionesCopiar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Direcciones a Copiar" />
                    <asp:Button ID="BtnLimpiarEmail" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Limpiar" />
                    <asp:Button ID="BtnCerrarEmail" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Cerrar" />
                    <asp:Button ID="BtnEnviarCorreo" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Enviar Correo" />
                </div>
                <div class="form-group">
                    <asp:Label ID="LblPara" runat="server" Text="Para :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <asp:TextBox ID="TxtPara" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblCopia" runat="server" Text="Copia :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <asp:TextBox ID="TxtCopia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblAsunto" runat="server" Text="Asunto :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <asp:TextBox ID="TxtAsunto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblSaludo" runat="server" Text="Saludo :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <asp:TextBox ID="TxtSaludo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblMensaje" runat="server" Text="Mensaje :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <textarea id="TxtMensaje" runat="server" rows="10" class="form-control" style="resize: none; font-size: 10px"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblImagen" runat="server" Text="Imagen :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <asp:FileUpload ID="UploadImagen" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblDespedida" runat="server" Text="Despedida :" Class="col-lg-2 control-label" />
                    <div class="col-lg-4">
                        <textarea id="TxtDespedida" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                    </div>
                    <asp:Label ID="LblFirma" runat="server" Text="Firma :" Class="col-lg-2 control-label" />
                    <div class="col-lg-4">
                        <textarea id="TxtFirma" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblFirmaImagen" runat="server" Text="Imagen Firma :" Class="col-lg-2 control-label" />
                    <div class="col-lg-10">
                        <asp:FileUpload ID="UploadFirmaImagen" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-sm-7 col-xs-4 col-lg-offset-5">
                    <div class="col-lg-4" style="margin-top:10px">
                        <asp:Button ID="BtnAdjuntarArchivos" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Seleccionar" />
                    </div>
                    <asp:FileUpload ID="FileUploadAdjuntar" CssClass="file" runat="server" ClientIDMode="Static" />
                    <label id="FileNombre" runat="server" class="btn btn-default" for="FileUploadAdjuntar">Adjuntar</label>
                    <asp:Button ID="BtnQuitar" runat="server" CssClass="btn btn-default" Text="Quitar" />
                </div>

                &nbsp;<asp:Label ID="LblTituloGV" runat="server" Text="Archivos Adjuntos : " CssClass="subTitulos"></asp:Label><br />

                <asp:GridView ID="GvArchivosAdjuntos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                    <Columns>
                        <asp:BoundField DataField="c1" HeaderText="Adjunto" />
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnEnviarCorreo" />
            <asp:PostBackTrigger ControlID="BtnAdjuntarArchivos" />
        </Triggers>
    </asp:UpdatePanel>

    <div style="display: initial; position: relative; width: 40%; float: right">

        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
            <cc1:TabPanel ID="Panel1" runat="server" HeaderText="Información">
                <ContentTemplate>
                    &nbsp;<asp:Label ID="LblTituloInformacion" runat="server" Text="Información" CssClass="subTitulos"></asp:Label><br />
                    <br />

                    <asp:Label ID="LblCodCliente" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LblUsuario" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LblRepeatCorreo" runat="server" Text="0" Visible="false"></asp:Label>
                    <asp:Label ID="LblRepeatAcciones" runat="server" Text="0" Visible="false"></asp:Label>
                    <asp:Label ID="LblRepeatEstado" runat="server" Text="0" Visible="false"></asp:Label>

                    <asp:Label ID="LblCodRelacion" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="LblCodAplicacion" runat="server" Visible="false"></asp:Label>

                    <div class="form-group" style="font-size: 11px">
                        <asp:Label ID="LblCliente" runat="server" Text="Cliente :" Class="col-lg-3 control-label" />
                        <div class="col-lg-9">
                            <textarea id="TxtCliente" runat="server" rows="2" class="form-control" style="resize: none" readonly="readonly"></textarea>
                        </div>
                    </div>
                    <div class="form-group" style="font-size: 11px">
                        <asp:Label ID="LblNroTicket" runat="server" Text="N° Ticket :" Class="col-lg-3 control-label" />
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtNroTicket" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" style="font-size: 11px">
                        <asp:Label ID="LblInformacion" runat="server" Text="Información :" Class="col-lg-3 control-label" />
                        <div class="col-lg-9">
                            <asp:DropDownList ID="DdlInformacion" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div id="TablaListaInformacion" runat="server" style="height: 350px; width: 500px; margin-left: 18px; margin-top: 10px; margin-bottom: 10px; border-top: solid 1px lightgray; border-bottom: solid 1px lightgray">
                            <asp:GridView ID="GvListaInformacion" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:ButtonField CommandName="Carga" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                        <ItemStyle Height="10px" Width="10px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="c1" />
                                    <asp:BoundField DataField="c2" />
                                    <asp:BoundField DataField="c3" />
                                    <asp:BoundField DataField="c4" />
                                    <asp:BoundField DataField="c5" />
                                    <asp:BoundField DataField="c6" />
                                    <asp:BoundField DataField="c7" />
                                    <asp:BoundField DataField="c8" />
                                    <asp:BoundField DataField="c9" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <asp:Label ID="LblIndex" runat="server" Visible="false"></asp:Label>

                    <div class="form-group" style="font-size: 10px; padding-bottom: 15px">
                        <div style="display: initial; position: relative; width: 49%; float: left; border-right: solid 1px lightgray">
                            <div class="form-group col-lg-offset-2">
                                <asp:Button ID="BtnAdjuntarTraking" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Adjuntar Traking" />
                            </div>
                            <div class="col-lg-12">
                                <asp:RadioButton GroupName="traking" ID="RBAcciones" runat="server" Text="Acciones" Checked="true" AutoPostBack="false" />
                                <asp:RadioButton GroupName="traking" ID="RBCorreo" runat="server" Text="Correo" AutoPostBack="false" />
                                <asp:RadioButton GroupName="traking" ID="RBEstados" runat="server" Text="Estados" AutoPostBack="false" />
                            </div>
                        </div>
                        <div style="display: initial; position: relative; width: 51%; float: left;">
                            <div class="form-group">
                                <asp:Label ID="LblNroEnvios" runat="server" Text="N° Envíos : " Class="col-lg-6 control-label" />
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtNroEnvios" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblUltimoEnvio" runat="server" Text="Últ. Envío : " Class="col-lg-6 control-label" />
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtUltimoEnvio" runat="server" CssClass="form-control" Font-Size="12px"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblHoraEnvio" runat="server" Visible="false" />
                            </div>
                        </div>
                    </div>






                    <div id="ModalCargaDatosEmpleados" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="Label4" runat="server" Text="Carga Datos : " />
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-horizontal">
                                    <div class="modal-body" style="padding: 20px 10px 0;">
                                        <input type="hidden" name="metodo" value="registrarP" />
                                        <div id="step4" class="panel-group">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row form-group col-md-12">
                                                                <div class="col-lg-12">
                                                                    <asp:RadioButton GroupName="empleados" ID="RBEmpleadosPara" runat="server" Text="Cargar Email en Para" Checked="true" AutoPostBack="false" />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group col-md-12">
                                                                <div class="col-lg-12">
                                                                    <asp:RadioButton GroupName="empleados" ID="RBEmpleadosCopia" runat="server" Text="Cargar Email en Copia" AutoPostBack="false" />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group col-md-12">
                                                                <asp:Button ID="BtnAceptarCargaEmpleados" runat="server" CssClass=" btn btn-default" Text="Aceptar" />
                                                                <asp:Button ID="BtnCerrarCargaEmpleados" runat="server" CssClass=" btn btn-default" Text="Cerrar" />
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarCargaEmpleados" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="GvListaInformacion" EventName="RowCommand" />
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




                    <div id="ModalCargaDatosContactos" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
                        <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Carga Datos : " />
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-horizontal">
                                    <div class="modal-body" style="padding: 20px 10px 0;">
                                        <input type="hidden" name="metodo" value="registrarP" />
                                        <div id="step4" class="panel-group">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="row form-group col-md-12">
                                                                <div class="col-lg-12">
                                                                    <asp:RadioButton GroupName="empleados" ID="RBContactosPara" runat="server" Text="Cargar Email en Para" Checked="true" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group col-md-12">
                                                                <div class="col-lg-12">
                                                                    <asp:RadioButton GroupName="empleados" ID="RBContactosCopia" runat="server" Text="Cargar Email en Copia" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group col-md-12">
                                                                <div class="col-lg-12">
                                                                    <asp:RadioButton GroupName="empleados" ID="RBContactosNombre" runat="server" Text="Cargar Nombre Contacto" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                            <div class="row form-group col-md-12">
                                                                <asp:Button ID="BtnAceptarCargaContactos" runat="server" CssClass=" btn btn-default" Text="Aceptar" />
                                                                <asp:Button ID="BtnCerrarCargaContactos" runat="server" CssClass=" btn btn-default" Text="Cerrar" />
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarCargaContactos" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="GvListaInformacion" EventName="RowCommand" />
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


                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Otros Datos">
                <ContentTemplate>
                    &nbsp;<asp:Label ID="LblTituloOtrosDatos" runat="server" Text="Otros Datos" CssClass="subTitulos"></asp:Label><br />
                    <br />

                    <div class="form-group">
                        <asp:Label ID="LblTipoCorreo" runat="server" Text="Tipo de Correo :" Class="col-lg-9 control-label" />
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <asp:DropDownList ID="DdlTipoCorreo" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LblNivelServicio" runat="server" Text="Tipo de Nivel de Servicio :" Class="col-lg-9 control-label" />
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <textarea id="TxtNivelServicio" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LblCompañia" runat="server" Text="Compañía que lo realiza :" Class="col-lg-9 control-label" />
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <textarea id="TxtCompañia" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LblTipoFalla" runat="server" Text="Tipo de Falla :" Class="col-lg-9 control-label" />
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <textarea id="TxtTipoFalla" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LblTipoLocal" runat="server" Text="Tipo de Local :" Class="col-lg-9 control-label" />
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <textarea id="TxtTipoLocal" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="LblEncargadoCompañia" runat="server" Text="Encargado según Compañía :" Class="col-lg-9 control-label" />
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12">
                            <textarea id="TxtEncargadoCompañia" runat="server" rows="2" class="form-control" style="resize: none"></textarea>
                        </div>
                    </div>

                    <div class="form-group col-lg-offset-3">
                        <asp:Button ID="BtnVerUsuarios" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Ver Usuarios" Enabled="false" />
                        <asp:Button ID="BtnVerListado" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Ver Listado" Enabled="false" />
                    </div>

                    <div class="form-group" style="padding-bottom: 20px">
                        <div class="col-lg-5">
                            <asp:TextBox ID="Txt" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>


                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>






</asp:Content>
