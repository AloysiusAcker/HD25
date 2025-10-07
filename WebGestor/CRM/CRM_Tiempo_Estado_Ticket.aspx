<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.master" CodeFile="CRM_Tiempo_Estado_Ticket.aspx.vb" Inherits="CRM_Tiempo_Estado_Ticket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="5" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
        <cc1:TabPanel runat="server" HeaderText="Tiempo Estado Ticket" ID="TabPanel1">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <br />
                        <asp:Label ID="Label5" runat="server" Text="Estado Tiempo" CssClass="Titulos"></asp:Label><br />
                        <br />
                        <div class="form-group">
                            <div class="col-lg-12">
                                <asp:Button ID="BtnListarRelaciónT" runat="server" CssClass="btn btn-group" Text="Listar" />
                                <asp:Button ID="BtnEstadoSiguiente" runat="server" CssClass="btn btn-group" Text="Estado Siguiente" />
                            </div>
                        </div>

                        <asp:Label ID="LblIngreso" runat="server" Text="" CssClass="subTitulos" Visible="false"></asp:Label><br />
                        <div class="form-group">
                            <asp:Label ID="LblTipoProceso" runat="server" CssClass="col-lg-3 control-label-2" Text="Tipo de Proceso:" Visible="False"></asp:Label>
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlTipoProceso" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnGuardarEstadoRelacion" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="false" />
                            <asp:Button ID="BtnActualizarTiempo" runat="server" CssClass="btn btn-group" Text="Actualizar" Visible="false" />
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEstadoTicket" runat="server" CssClass="col-lg-3 control-label-2" Text="Estado Ticket :" Visible="False"></asp:Label>
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlEstadoTicket" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnCancelarEstadoRelacion" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                            <asp:Button ID="BtnCancelarTiempo" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEstadoRelacion" runat="server" CssClass="col-lg-3 control-label-2" Text="Estado Relación :" Visible="False"></asp:Label>
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlEstadoRelacion" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblDuracion" runat="server" CssClass="col-lg-3 control-label-2" Text="Duración :" Visible="False"></asp:Label>

                            <div class="col-lg-2">
                                <asp:DropDownList ID="DdlDias" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Label ID="LblDias" runat="server" CssClass="col-lg-1 control-label" Text="Días" Visible="False"></asp:Label>

                            <div class="col-lg-2">
                                <asp:DropDownList ID="DdlHoras" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Label ID="LblHoras" runat="server" CssClass="col-lg-1 control-label" Text="Horas" Visible="False"></asp:Label>

                            <div class="col-lg-2">
                                <asp:DropDownList ID="DdlMinutos" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Label ID="LblMinutos" runat="server" CssClass="col-lg-1 control-label" Text="Minutos" Visible="False"></asp:Label>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvListaEstadoTiempo" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="DdlTipoProceso" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel20">
                    <ProgressTemplate>
                        Cargando, por favor espere......  <img src="../Icono/loadin-barra.gif" style="width:100px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <p id="LblTotalEstadosTiempo" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros : </p>
                            <p id="LblTotalEstadosTiempoL" class="control-label" style="color: darkred; font-weight: bold" runat="server" visible="false"></p>
                        </div>

                        <div class="row form-group-lg">
                            <div class="col-lg-12">
                                <asp:GridView ID="GvListaEstadoTiempo" AutoGenerateColumns="False" runat="server" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="BtnEliminarRelacion" ImageUrl="~/Icono/delete2_opt.png">
                                            <ControlStyle CssClass=" btn btn-default" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="BtnEditarTiempo" ImageUrl="~/Icono/EDITAR_TIEMPO_opt.png">
                                            <ControlStyle CssClass=" btn btn-default" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="TICKET_ESTADO" SortExpression="TICKET_ESTADO" ItemStyle-ForeColor="White">
                                            <ItemStyle ForeColor="White"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                        <asp:BoundField DataField="DURACION" HeaderText="Duración" SortExpression="DURACION" />
                                        <asp:BoundField DataField="ESTADO_REL" HeaderText="Estado Relación" SortExpression="ESTADO_REL" />
                                        <asp:BoundField DataField="TICKET_ESTADO_RELACION" HeaderText="" SortExpression="TICKET_ESTADO_RELACION" ItemStyle-ForeColor="White">
                                            <ItemStyle ForeColor="White"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListarRelaciónT" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnGuardarEstadoRelacion" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnActualizarTiempo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarEstadoRelacion" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarTiempo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Clientes" ID="TabPanel2">
            <ContentTemplate>
                <div class="form-horizontal">
                    <br />
                    <asp:Label runat="server" Text="Clientes" CssClass="Titulos"></asp:Label><br />
                    <br />
                    <div style="display: initial; position: relative; width: 55%; float: left;">
                        <div class="form-group">
                            <asp:CheckBox ID="ChkArchivo" runat="server" Text="Leer Archivo" AutoPostBack="True" CssClass="col-lg-3 control-label" />
                            <div class="col-lg-6">
                                <asp:TextBox ID="TxtArchivoCliente" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:Button ID="BtnArchivoCliente" runat="server" CssClass="btn btn-group" Text="..." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="N° Filas de" Class="col-lg-3 control-label" />
                            <div class="col-lg-2">
                                <asp:TextBox ID="TxtFilaA" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:Label runat="server" Text="A" Class="control-label" />
                            <div class="col-lg-2">
                                <asp:TextBox ID="TxtFilaB" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-4">
                                <asp:Button ID="BtnImportar" runat="server" CssClass="btn btn-group" Text="Importar Clientes y Contactos" />
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="col-lg-12">
                                        <asp:Button ID="BtnAgregarCliente" runat="server" CssClass="btn btn-group" Text="Agregar" />
                                        <asp:Button ID="BtnListarCliente" runat="server" CssClass="btn btn-group" Text="Listar" />
                                        <asp:Button ID="BtnFiltrosCliente" runat="server" CssClass="btn btn-group" Text="Filtros" />
                                        <asp:Button ID="BtnAsignarCarteraMasivaCliente" runat="server" CssClass="btn btn-group" Text="Asignar Cartera Masiva" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnAgregarCliente" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnListarCliente" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnFiltrosCliente" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnAsignarCarteraMasivaCliente" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <asp:Button ID="BtnExportarExcelCliente" runat="server" CssClass="btn btn-group" Text="Exportar Excel Clientes" />
                                <asp:Button ID="BtnDeseleccionarCliente" runat="server" CssClass="btn btn-group" Text="Deseleccionar Clientes" />
                            </div>
                        </div>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <asp:Label ID="LblRazonSocialClienteBuscar" runat="server" Text="Razón Social :" Class="col-lg-3 control-label" />
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="TxtRazonSocialClienteBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button ID="BtnBuscarCliente" runat="server" CssClass="btn btn-group" Text="Buscar" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblRucClienteBuscar" runat="server" Text="R.U.C. :" Class="col-lg-3 control-label" />
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="TxtRucClienteBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarCliente" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                    <div style="display: initial; position: relative; width: 40%; float: right;">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Literal ID="LiteralAyuda" runat="server"></asp:Literal>
                                <div class="form-group" style="height: 30px">
                                    <p class="col-lg-5 control-label">TOTAL </p>
                                    <p id="TotalClientesP" class="control-label-2" style="width: 15px" runat="server"></p>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnListarCliente" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <br />
                            <asp:Label ID="TituloAgregarCliente" runat="server" CssClass="subTitulos" Visible="False"></asp:Label><br />
                            <br />
                            <div class="form-group">
                                <asp:Label ID="LblCIFCliente" runat="server" Text="C.I.F. :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtCIFCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblCodGPSCliente" runat="server" Text="Cod. G.P.S. :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtCodGPSCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblAdquiraCliente" runat="server" Text="Adquira :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtAdquiraCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblFechaCliente" runat="server" Text="F. Adquira:" CssClass="col-lg-2 control-label" Visible="False"></asp:Label>
                                <div class="col-lg-3">
                                    <input id="TxtFechaCliente" type="text" runat="server" class="form-control fecha" visible="false" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblNombreCliente" runat="server" Text="Nombre :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TxtNombreCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblGMICliente" runat="server" Text="G.M.I. :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtGMICliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblDireccionCliente" runat="server" Text="Dirección :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TxtDireccionCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblTelefono2Cliente" runat="server" Text="Teléfono 2 :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtTelefono2Cliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblCiudadCliente" runat="server" Text="Ciudad :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtCiudadCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblProvinciaCliente" runat="server" Text="Provincia :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtProvinciaCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblTelefono3Cliente" runat="server" Text="Telefono 3 :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtTelefono3Cliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblPaisCliente" runat="server" Text="País :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtPaisCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblCodPostalCliente" runat="server" Text="Cod. Postal :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtCodPostalCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblTelefonoEfectivoCliente" runat="server" Text="Teléfono Efectivo :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtTelefonoEfectivoCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblModoFacturacionCliente" runat="server" Text="Modo Facturación :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtModoFacturacionCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblGrupoCliente" runat="server" Text="Grupo :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtGrupoCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblOCCliente" runat="server" Text="OC :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtOCCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblModoHojaEntradaCliente" runat="server" Text="Modo Hoja Entrada :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtModoHojaEntradaCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblSociedadCliente" runat="server" Text="Sociedad :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtSociedadCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblCargoContactoCliente" runat="server" Text="Cargo Contrato :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtCargoContactoCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblNombreNegociadorCliente" runat="server" Text="Nombre Negociador :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtNombreNegociadorCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblEmailCliente" runat="server" Text="Email N. :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtEmailCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblTelefonoNCliente" runat="server" Text="Teléfono N. :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtTelefonoNCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblExtranjeroCliente" runat="server" Text="Extranjero :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtExtranjeroCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblGrupoABCCliente" runat="server" Text="Grupo A / B / C :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtGrupoABCCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblOkComprasCliente" runat="server" Text="Ok Compras :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtOkComprasCliente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblEstadoCliente" runat="server" Text="Estado :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlEstadoCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="CodPersonaAyuda" runat="server" Visible="False" />
                                <asp:Label ID="CodClienteAyuda" runat="server" Visible="False" />
                                <asp:Label ID="RucClienteAyuda" runat="server" Visible="False" />
                                <asp:Label ID="CifClienteAyuda" runat="server" Visible="False" />
                                <asp:Label ID="RazSoClienteAyuda" runat="server" Visible="False" />
                                <asp:Label ID="CodAsignadoAyuda" runat="server" Visible="False" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblAccionesCliente" runat="server" Text="Acciones :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="DdlAccionesCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-5">
                                    <asp:Button ID="BtnGuardarCliente" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="False" />
                                    <asp:Button ID="BtnCancelarCliente" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="False" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaClientes" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarCliente" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel11">
                        <ProgressTemplate>
                            Cargando, por favor espere......
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <p id="LblTotalClientesL" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros : </p>
                                <p id="TotalClientesL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                            </div>
                            <div class="form-group">
                                <div id="TablaClientes" runat="server">
                                    <asp:GridView ID="GvListaClientes" AutoGenerateColumns="False" runat="server" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Check" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Image" CommandName="EliminarCliente" ImageUrl="~/Icono/delete2_opt.png"></asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="EditarCliente" ImageUrl="~/Icono/Editar_opt.png"></asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="TrackingCliente" ImageUrl="~/Icono/Tracking_opt.png"></asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="CambiarEstadoCliente" ImageUrl="~/Icono/CambioDeEstado_opt.png"></asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="AplicarAccionesCliente" ImageUrl="~/Icono/CambioDeAccion_opt.png"></asp:ButtonField>
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CIF" HeaderText="C.I.F." SortExpression="TBTICKET_CLIENTE_CIF" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE" HeaderText="NOMBRE" SortExpression="TBTICKET_CLIENTE_NOMBRE" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_COD_GPS" HeaderText="COD. GPS" SortExpression="TBTICKET_CLIENTE_COD_GPS" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_GRUPO" HeaderText="GRUPO" SortExpression="TBTICKET_CLIENTE_GRUPO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_SOCIEDAD" HeaderText="SOCIEDAD" SortExpression="TBTICKET_CLIENTE_SOCIEDAD" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_GMI" HeaderText="GMI" SortExpression="TBTICKET_CLIENTE_GMI" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_MRHOJA_ENTRADA" HeaderText="ENTRADA" SortExpression="TBTICKET_CLIENTE_MRHOJA_ENTRADA" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_MODO_FACTURACION" HeaderText="FACTURACIÓN" SortExpression="TBTICKET_CLIENTE_MODO_FACTURACION" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_USUA_ADQUIRA" HeaderText="ADQUIRA USUARIO" SortExpression="TBTICKET_CLIENTE_USUA_ADQUIRA" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_FECINICIO_ADQUIRA" HeaderText="ADQUIRA INICIO" SortExpression="TBTICKET_CLIENTE_FECINICIO_ADQUIRA" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CODIGO" HeaderText="COD. CLIENTE" SortExpression="TBTICKET_CLIENTE_CODIGO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_ESTADO" HeaderText="EST. CLIENTE" SortExpression="TBTICKET_CLIENTE_ESTADO" />
                                            <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_ASIGNADO_A" HeaderText="ASIGNADO A." SortExpression="TBTICKET_CLIENTE_ASIGNADO_A" />
                                            <asp:BoundField DataField="ASIGNADO" HeaderText="ASIGNADO" SortExpression="ASIGNADO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_TELEF_2" HeaderText="TELF. 2" SortExpression="TBTICKET_CLIENTE_TELEF_2" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_TELEF_3" HeaderText="TELF. 3" SortExpression="TBTICKET_CLIENTE_TELEF_3" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_TELEF_EFECTIVO" HeaderText="TELF. EFECTIVO" SortExpression="TBTICKET_CLIENTE_TELEF_EFECTIVO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_DIRECCION" HeaderText="DIRECCION" SortExpression="TBTICKET_CLIENTE_DIRECCION" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CIUDAD" HeaderText="CIUDAD" SortExpression="TBTICKET_CLIENTE_CIUDAD" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_PROVINCIA" HeaderText="PROVINCIA" SortExpression="TBTICKET_CLIENTE_PROVINCIA" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CODPOSTAL" HeaderText="COD. POSTAL" SortExpression="TBTICKET_CLIENTE_CODPOSTAL" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_PAIS" HeaderText="PAIS" SortExpression="TBTICKET_CLIENTE_PAIS" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_OC" HeaderText="OC" SortExpression="TBTICKET_CLIENTE_OC" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CARGO_CONTACTO" HeaderText="CARGO" SortExpression="TBTICKET_CLIENTE_CARGO_CONTACTO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_EXTRANJERO" HeaderText="EXTRANJERO" SortExpression="TBTICKET_CLIENTE_EXTRANJERO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_GPO_ABC" HeaderText="GPO" SortExpression="TBTICKET_CLIENTE_GPO_ABC" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NEGOCIADOR_NOMBRE" HeaderText="NOM. NEGOCIADOR" SortExpression="TBTICKET_CLIENTE_NEGOCIADOR_NOMBRE" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NEGOCIADOR_EMAIL" HeaderText="EMAIL NEGOCIADOR" SortExpression="TBTICKET_CLIENTE_NEGOCIADOR_EMAIL" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NEGOCIADOR_TELEFONO" HeaderText="TELF. NEGOCIADOR" SortExpression="TBTICKET_CLIENTE_NEGOCIADOR_TELEFONO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_OK_COMPRAS" HeaderText="OK COMPRAS" SortExpression="TBTICKET_CLIENTE_OK_COMPRAS" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_DPTO" HeaderText="DEPARTAMENTO" SortExpression="TBTICKET_CLIENTE_DPTO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NROTRABAJADORES" HeaderText="NRO. TRABAJADORES" SortExpression="TBTICKET_CLIENTE_NROTRABAJADORES" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE_COMERCIAL" HeaderText="NOM. COMERCIAL" SortExpression="TBTICKET_CLIENTE_NOMBRE_COMERCIAL" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_PAGINAWEB" HeaderText="PÁGINA WEB" SortExpression="TBTICKET_CLIENTE_PAGINAWEB" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CARGO_CODIGO" HeaderText="COD. CARGO" SortExpression="TBTICKET_CLIENTE_CARGO_CODIGO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_EMAIL_SECRETARIA" HeaderText="EMAIL SECRETARIA" SortExpression="TBTICKET_CLIENTE_EMAIL_SECRETARIA" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CIIU_ESP_DETALLE" HeaderText="DETALLE" SortExpression="TBTICKET_CLIENTE_CIIU_ESP_DETALLE" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_EMPRESA_TAMAÑO" HeaderText="TAMAÑO EMPRESA" SortExpression="TBTICKET_CLIENTE_EMPRESA_TAMAÑO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_ACTUALIZADO" HeaderText="ACT. CLIENTE" SortExpression="TBTICKET_CLIENTE_ACTUALIZADO" />
                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CODPERSONA" HeaderText="COD. PERSONA" SortExpression="TBTICKET_CLIENTE_CODPERSONA" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaClientes" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="GvCarteraMasiva" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnListarCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnDeseleccionarCliente" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div id="ModalTracking" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="form-horizontal">
                                <div class="modal-body" style="padding: 20px 10px 0;">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                                                            <cc1:TabPanel runat="server" HeaderText="Tracking de Acciones" ID="TabPanel8">
                                                                <ContentTemplate>
                                                                    <div class="row form-group-lg">
                                                                        <div class="col-lg-12">
                                                                            <asp:GridView ID="GvTrackingAcciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="ACCION" HeaderText="Acción" SortExpression="ACCION" />
                                                                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                                                                    <asp:BoundField DataField="HORA" HeaderText="Hora" SortExpression="HORA" />
                                                                                    <asp:BoundField DataField="USUARIO" HeaderText="Usuario" SortExpression="USUARIO" />
                                                                                    <asp:BoundField DataField="ETIQUETA_REFERENCIA" HeaderText="Referencia" SortExpression="ETIQUETA_REFERENCIA" />
                                                                                    <asp:BoundField DataField="COD_REFERENCIA" HeaderText="Código" SortExpression="COD_REFERENCIA" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </cc1:TabPanel>
                                                            <cc1:TabPanel runat="server" HeaderText="Tracking de Estados" ID="TabPanel9">
                                                                <ContentTemplate>
                                                                    <div class="row form-group col-md-12">
                                                                        <div class="col-lg-12">
                                                                            <asp:GridView ID="GvTrackingEstados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="ESTADO" HeaderText="Fecha" SortExpression="ESTADO" />
                                                                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                                                                    <asp:BoundField DataField="HORA" HeaderText="Hora" SortExpression="HORA" />
                                                                                    <asp:BoundField DataField="USUARIO" HeaderText="Usuario Registra" SortExpression="USUARIO" />
                                                                                    <asp:BoundField DataField="ASIGNADO" HeaderText="Usuario Asignado" SortExpression="ASIGNADO" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </cc1:TabPanel>
                                                        </cc1:TabContainer>
                                                        <asp:Button ID="BtnCerrarTracking" runat="server" CssClass="btn btn-group" Text="Cerrar" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="GvListaClientes" EventName="RowCommand" />
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

                <div id="ModalCarteraMasiva" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="form-horizontal">
                                <div class="modal-body" style="padding: 20px 10px 0;">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row form-group">
                                                            <div class="col-lg-12">
                                                                <asp:GridView ID="GvCarteraMasiva" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png"></asp:ButtonField>
                                                                        <asp:BoundField DataField="USUARI_CODIGO" HeaderText="CÓDIGO" SortExpression="USUARI_CODIGO" />
                                                                        <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRE" SortExpression="NOMBRES" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="BtnCerrarCarteraMasiva" runat="server" CssClass="btn btn-group" Text="Cerrar" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnAsignarCarteraMasivaCliente" EventName="Click" />
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
        <cc1:TabPanel runat="server" HeaderText="Contactos del Cliente" ID="TabPanel3">
            <ContentTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text="Contactos del Cliente" CssClass="Titulos"></asp:Label><br />
                <br />
                <div class="form-group">
                    <asp:Label ID="LblClienteCC" runat="server" Text="Cliente :" Class="col-lg-2 control-label" />
                    <div class="col-lg-4">
                        <asp:TextBox ID="TxtCliente" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="BtnAgregarContacto" runat="server" CssClass="btn btn-group" Text="Agregar Contacto" />
                    <asp:Button ID="BtnListarContacto" runat="server" CssClass="btn btn-group" Text="Listar" />
                </div>
                <div class="form-group">
                    <asp:Label ID="LblApPaternoCC" runat="server" Text="Ap. Paterno :" Class="col-lg-2 control-label" />
                    <div class="col-lg-4">
                        <asp:TextBox ID="TxtApPaterno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <asp:Button ID="BtnExportarExcel" runat="server" CssClass="btn btn-group" Text="Exportar a Excel" />
                </div>

                <div class="form-horizontal">
                    <br />
                    <asp:Label ID="LblTituloAgregarContacto" runat="server" Text="" CssClass="subTitulos" Visible="false"></asp:Label><br />
                    <br />

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <asp:Label ID="LblCliente" runat="server" Text="Cliente :" Class="col-lg-2 control-label" Visible="false" />
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtCIFClienteMOD" runat="server" CssClass="form-control" Visible="false" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:Button ID="BtnBuscaCliente" runat="server" ControlStyle-CssClass="btn btn-block" Visible="false" Text="..." />
                                </div>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TxtRZClienteMOD" runat="server" CssClass="form-control" Visible="false" Enabled="false"></asp:TextBox>
                                </div>
                                <asp:Label ID="LblCodCLIENTE" runat="server" Visible="false" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvBuscarClienteModal" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <asp:Label ID="LblApePaterno" runat="server" Text="Ape. Paterno :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-2">
                            <asp:TextBox ID="TxtApePaterno" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                        <asp:Label ID="LblApeMaterno" runat="server" Text="Ape. Materno :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-2">
                            <asp:TextBox ID="TxtApeMaterno" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                        <asp:Label ID="LblNombres" runat="server" Text="Nombres :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-2">
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LblTelefono" runat="server" Text="Teléfono :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-2">
                            <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                        <asp:Label ID="LblCelular" runat="server" Text="Celular :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-2">
                            <asp:TextBox ID="TxtCelular" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LblEmail" runat="server" Text="Email :" Class="col-lg-2 control-label" Visible="false" />
                        <div class="col-lg-2">
                            <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                        </div>
                        <div class="col-lg-offset-5">
                            <asp:Button ID="BtnGuardarContacto" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="false" />
                            <asp:Button ID="BtnCancelarContacto" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                            <asp:Label ID="LblCodContacto" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <p id="LblTotalContactosClientes" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                    <p id="LblTotalContactosClientesL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                </div>
                <div class="form-group">
                    <div id="TablaContactosClientes" runat="server">
                        <asp:GridView ID="GvListaContactosClientes" AutoGenerateColumns="False" runat="server" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Actualizar" Text="Actualizar" ButtonType="Image" ImageUrl="~/icono/Editar_opt.png">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="TBTICKET_CONTACTO_CODIGO" HeaderText="" SortExpression="TBTICKET_CONTACTO_CODIGO" />
                                <asp:BoundField DataField="TBTICKET_CLIENTE_CIF" HeaderText="CIF" SortExpression="TBTICKET_CLIENTE_CIF" />
                                <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE" HeaderText="Nombre" SortExpression="TBTICKET_CLIENTE_NOMBRE" />
                                <asp:BoundField DataField="TBTICKET_CONTACTO_APEPAT" HeaderText="Contacto Apellido Paterno" SortExpression="TBTICKET_CONTACTO_APEPAT" />
                                <asp:BoundField DataField="TBTICKET_CONTACTO_APEMAT" HeaderText="Contacto Apellido Materno" SortExpression="TBTICKET_CONTACTO_APEMAT" />
                                <asp:BoundField DataField="TBTICKET_CONTACTO_NOMBRES" HeaderText="Contacto Nombres" SortExpression="TBTICKET_CONTACTO_NOMBRES" />
                                <asp:BoundField DataField="TBTICKET_CONTACTO_TELEF1" HeaderText="Teléfono" SortExpression="TBTICKET_CONTACTO_TELEF1" />
                                <asp:BoundField DataField="TBTICKET_CONTACTO_CEL1" HeaderText="Celular" SortExpression="TBTICKET_CONTACTO_CEL1" />
                                <asp:BoundField DataField="TBTICKET_CONTACTO_EMAIL" HeaderText="Correo Eletrónico" SortExpression="TBTICKET_CONTACTO_EMAIL" />
                                <asp:BoundField DataField="TBTICKET_CLIENTE_CODIGO" HeaderText="" SortExpression="TBTICKET_CLIENTE_CODIGO" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


                <div id="ModalBuscaCliente" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:Label runat="server" ID="TituloBuscarCliente" Text="Busqueda Cliente" />
                            </div>
                            <div class="form-horizontal">
                                <div class="modal-body" style="padding: 20px 10px 0;">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row form-group col-md-12">
                                                            <label class="control-label col-sm-3 col-xs-12" for="id_codArt">Razón Social :</label>
                                                            <div class="col-sm-8 col-xs-7">
                                                                <input class="form-control" id="TxtRazonSocial" type="text" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row form-group col-md-12">
                                                            <label class="control-label col-sm-3 col-xs-12" for="id_clasificacionBA">CIF :</label>
                                                            <div class="col-sm-8 col-xs-7">
                                                                <input class="form-control" id="TxtCIF" type="text" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row form-group col-md-12">
                                                            <div class="col-sm-5 col-xs-2 col-lg-offset-4">
                                                                <asp:Button ID="BtnListarClienteModal" runat="server" Text="Listar" CssClass="btn btn-default" />
                                                                <asp:Button ID="BtnCerrarClienteModal" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                            </div>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="form-group">
                                                                    <p id="LblTotalClientesMM" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                                                                    <p id="LblTotalClientesMML" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                                                                </div>
                                                                <div class="row form-group col-md-12">
                                                                    <div class="col-lg-12">
                                                                        <asp:GridView ID="GvBuscarClienteModal" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                                            <Columns>
                                                                                <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                                </asp:ButtonField>
                                                                                <asp:BoundField DataField="TBTICKET_CLIENTE_CIF" HeaderText="CIF" SortExpression="TBTICKET_CLIENTE_CIF" />
                                                                                <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE" HeaderText="Razón Social" SortExpression="TBTICKET_CLIENTE_NOMBRE" />
                                                                                <asp:BoundField DataField="TBTICKET_CLIENTE_CODIGO" HeaderText="" SortExpression="TBTICKET_CLIENTE_CODIGO">
                                                                                    <ItemStyle ForeColor="White"></ItemStyle>
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="BtnListarClienteModal" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnBuscaCliente" EventName="Click" />
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
        <cc1:TabPanel runat="server" HeaderText="Proceso - Petición" ID="TabPanel4">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        &nbsp;<asp:Label ID="LblTituloProcesoPeticion" runat="server" Text="Proceso - Petición" CssClass="Titulos"></asp:Label><br />
                        <br />
                        <div class="form-group">
                            <asp:Button ID="BtnListarProcesos" runat="server" CssClass="btn btn-group" Text="Listar" />
                            <asp:Button ID="BtnAgregarRelacionPP" runat="server" CssClass="btn btn-group" Text="AgregarRelacion" />
                            <asp:Button ID="BtnExportar" runat="server" CssClass="btn btn-group" Text="Exportar" />
                        </div>

                        <br />
                        <asp:Label ID="LblTituloAgregarRelacion" runat="server" Text="Agregar Relación" CssClass="subTitulos" Visible="false"></asp:Label><br />
                        <br />
                        <div class="form-group">
                            <asp:Label ID="LblTipoProcesoRELACION" runat="server" Text="Tipo de Proceso :" Class="col-lg-2 control-label" Visible="false" />
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlTipoProcesoRELACION" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnGuardarAgregarRelacionPP" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="false" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblTipoPeticionRELACION" runat="server" Text="Tipo de Petición :" Class="col-lg-2 control-label" Visible="false" />
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlTipoPeticionRELACION" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnCancelarAgregarRelacionPP" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <p id="LblTotalPeticion" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                                    <p id="LblTotalPeticionL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                                </div>
                                <div class="row form-group">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="GvListaProcesoPeticion" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                            <Columns>
                                                <asp:ButtonField CommandName="QuitarRelacion" Text="Quitar Relación" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="GTP1_CODIGO" HeaderText="" SortExpression="GTP1_CODIGO">
                                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PROCESO" HeaderText="Tipo de Proceso" SortExpression="PROCESO" />
                                                <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Tipo de Petición" SortExpression="NIVEL1_DESCRIP" />
                                                <asp:BoundField DataField="PROCESO_CODIGO" HeaderText="" SortExpression="PROCESO_CODIGO">
                                                    <ItemStyle ForeColor="White"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <br />
            </ContentTemplate>
        </cc1:TabPanel>


        <cc1:TabPanel runat="server" HeaderText="Proceso Estado" ID="TabPanel5">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="espacio">
                            <asp:Label ID="Label3" runat="server" Text="Proceso Estado" CssClass="Titulos"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="BtnListarEstados" runat="server" CssClass="btn btn-group" Text="Listar" />
                            <asp:Button ID="BtnAgregarRelacionPE" runat="server" CssClass="btn btn-group" Text="AgregarRelacion" />
                        </div>

                        <br />
                        <asp:Label ID="LblTituloAgregarRelacionPE" runat="server" Text="Agregar Relación" CssClass="subTitulos" Visible="false"></asp:Label><br />
                        <br />
                        <div class="form-group">
                            <asp:Label ID="LblTipoProcesoESTADO" runat="server" Text="Tipo de Proceso :" Class="col-lg-2 control-label" Visible="false" />
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlTipoProcesoESTADO" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnGuardarAgregarRelacionPE" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="false" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblEstadoPROCESOESTADO" runat="server" Text="Estado :" Class="col-lg-2 control-label" Visible="false" />
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlEstadoPROCESOESTADO" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnCancelarAgregarRelacionPE" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <p id="LblTotalEstado" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                                    <p id="LblTotalEstadoL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                                </div>
                                <div class="row form-group">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="GvListaProcesoEstado" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                            <Columns>
                                                <asp:ButtonField CommandName="AsignarAcciones" Text="Asignar Acciones" ButtonType="Image" ImageUrl="~/icono/plus.gif">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField CommandName="QuitarRelacion" Text="Quitar Relación" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="ESTADO_CODIGO" HeaderText="" SortExpression="ESTADO_CODIGO">
                                                    <ItemStyle ForeColor="White"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PROCESO" HeaderText="Tipo de Proceso" SortExpression="PROCESO" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                <asp:BoundField DataField="ACCION" HeaderText="Acción" SortExpression="ACCION" />
                                                <asp:BoundField DataField="PROCESO_CODIGO" HeaderText="" SortExpression="PROCESO_CODIGO" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnGuardarAccion" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <br />
                <div id="ModalAsignarAcciones" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:Label runat="server" ID="Label4" Text="Asignar Acciones" />
                            </div>
                            <div class="form-horizontal">
                                <div class="modal-body" style="padding: 20px 10px 0;">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row form-group col-md-12">
                                                            <label class="control-label col-sm-3 col-xs-12" for="id_codArt">Proceso :</label>
                                                            <div class="col-sm-8 col-xs-7">
                                                                <asp:TextBox ID="TxtProcesoMAA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <asp:Label ID="LblCodPROCESO" runat="server" Visible="false" />
                                                        </div>
                                                        <div class="row form-group col-md-12">
                                                            <label class="control-label col-sm-3 col-xs-12" for="id_clasificacionBA">Estado :</label>
                                                            <div class="col-sm-8 col-xs-7">
                                                                <asp:TextBox ID="TxtEstadoMAA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <asp:Label ID="LblCodESTADO" runat="server" Visible="false" />
                                                        </div>
                                                        <div class="row form-group col-md-12">
                                                            <label class="control-label col-sm-3 col-xs-12" for="id_clasificacionBA">Acción :</label>
                                                            <div class="col-sm-8 col-xs-7 selectContainer">
                                                                <asp:DropDownList ID="DdlAccionMAA" runat="server" CssClass="form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="row form-group col-md-12">
                                                            <div class="col-sm-5 col-xs-2 col-lg-offset-4">
                                                                <asp:Button ID="BtnGuardarAccion" runat="server" Text="Guardar" CssClass="btn btn-default" />
                                                                <asp:Button ID="BtnCancelarAccion" runat="server" Text="Cancelar" CssClass="btn btn-default" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="GvListaProcesoEstado" EventName="RowCommand" />
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


        <cc1:TabPanel runat="server" HeaderText="Tiempo Estado Cliente" ID="TabPanel6">
            <ContentTemplate>

                <div class="form-horizontal">
                    <br />
                    <asp:Label runat="server" Text="Tiempo Estado Clientes" CssClass="Titulos"></asp:Label><br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <asp:Button ID="BtnAsignarAccionesTiempoEstadoCliente" runat="server" CssClass="btn btn-group" Text="Asignar Acciones" />
                                    <asp:Button ID="BtnIngresarTiempoEstadoCliente" runat="server" CssClass="btn btn-group" Text="Ingresar Tiempo" />
                                    <asp:Button ID="BtnListarTiempoEstadoCliente" runat="server" CssClass="btn btn-group" Text="Listar" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnAsignarAccionesTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnIngresarTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnListarTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnCancelarTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnGuardarTiempoEstadoCliente" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <br />
                            <asp:Label ID="TituloAgregarTiempoEstadoCliente" runat="server" CssClass="subTitulos" Visible="False"></asp:Label><br />
                            <br />
                            <div class="form-group">
                                <asp:Label ID="LblEstadoTiempoEstadoCliente" runat="server" Text="Estado :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="DdlEstadoTiempoEstadoCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblDuracionTiempoEstadoCliente" runat="server" CssClass="col-lg-2 control-label" Text="Duración :" Visible="False"></asp:Label>

                                <asp:Label ID="LblDiasTiempoEstadoCliente" runat="server" CssClass="col-lg-1 control-label" Text="Días :" Visible="False"></asp:Label>
                                <div class="col-lg-1">
                                    <asp:DropDownList ID="DdlDiasTiempoEstadoCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False" Width="70px">
                                    </asp:DropDownList>
                                </div>

                                <asp:Label ID="LblHorasTiempoEstadoCliente" runat="server" CssClass="col-lg-1 control-label" Text="Horas :" Visible="False"></asp:Label>
                                <div class="col-lg-1">
                                    <asp:DropDownList ID="DdlHorasTiempoEstadoCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False" Width="70px">
                                    </asp:DropDownList>
                                </div>

                                <asp:Label ID="LblMinutosTiempoEstadoCliente" runat="server" CssClass="col-lg-1 control-label" Text="Minutos :" Visible="False"></asp:Label>
                                <div class="col-lg-1">
                                    <asp:DropDownList ID="DdlMinutosTiempoEstadoCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False" Width="70px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblAccionesTiempoEstadoCliente" runat="server" Text="Acciones :" Class="col-lg-2 control-label" Visible="False" />
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="DdlAccionesTiempoEstadoCliente" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-offset-2 col-lg-5">
                                    <asp:Button ID="BtnGuardarTiempoEstadoCliente" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="False" />
                                    <asp:Button ID="BtnCancelarTiempoEstadoCliente" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="False" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnAsignarAccionesTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnIngresarTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="GvListaTiempoEstadoCliente" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel17">
                        <ProgressTemplate>
                            Cargando, por favor espere......
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <p id="LblTotalTiempoEstadosL" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                                <p id="TotalTiempoEstadosL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                            </div>
                            <div class="row form-group">
                                <div class="col-lg-12">
                                    <asp:GridView ID="GvListaTiempoEstadoCliente" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" CommandName="EditarTiempo" ImageUrl="~/Icono/EDITAR_TIEMPO_opt.png">
                                                <ItemStyle Height="10px" Width="10px" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="EliminarAccionTiempo" ImageUrl="~/Icono/delete2_opt.png">
                                                <ItemStyle Height="10px" Width="10px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                            <asp:BoundField DataField="DURACION" HeaderText="Duración" SortExpression="DURACION" />
                                            <asp:BoundField DataField="ACCION" HeaderText="Acción" SortExpression="ACCION" />
                                            <asp:BoundField DataField="CODIGO" SortExpression="CODIGO">
                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnListarTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnGuardarTiempoEstadoCliente" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarAccionesEstado" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div id="ModalAccionesTiempoEstado" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:Label runat="server" Text="Eliminar Acción" />
                            </div>
                            <div class="form-horizontal">
                                <div class="modal-body" style="padding: 20px 10px 0;">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Label ID="LblCodTiempoEstadoCliente" runat="server" Visible="False" />
                                                        <div class="row form-group col-md-12">
                                                            <div class="col-lg-6 col-lg-offset-3">
                                                                <asp:GridView ID="GvAccionesXEstado" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                                                            <ItemStyle Height="10px" Width="10px" />
                                                                        </asp:ButtonField>
                                                                        <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="ACCIÓN" SortExpression="ELEMEN_VALOR" />
                                                                        <asp:BoundField DataField="ELEMEN_CODIGO" SortExpression="ELEMEN_CODIGO">
                                                                            <ItemStyle ForeColor="White" Height="1px" Width="1px" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="row form-group col-md-12">
                                                            <div class="col-lg-offset-5">
                                                                <asp:Button ID="BtnCerrarAccionesEstado" runat="server" CssClass="btn btn-group" Text="Cerrar" />
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="GvListaTiempoEstadoCliente" EventName="RowCommand" />
                                                        <asp:AsyncPostBackTrigger ControlID="GvAccionesXEstado" EventName="RowCommand" />
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



        <cc1:TabPanel runat="server" HeaderText="Proceso - Estado Cliente" ID="TabPanel7">
            <ContentTemplate>
                &nbsp;<asp:Label ID="LblTituloProcesoEstadoCliente" runat="server" Text="Proceso - Estado Cliente" CssClass="Titulos"></asp:Label><br />
                <br />
                <div class="form-group">
                    <asp:Button ID="BtnListarEstadoCliente" runat="server" CssClass="btn btn-group" Text="Listar" />
                    <asp:Button ID="BtnAgregarRelacionCliente" runat="server" CssClass="btn btn-group" Text="Agregar Relación" />
                </div>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="LblTituloAgregarRelacionESTADOCLIENTE" runat="server" Text="Agregar Relación" CssClass="subTitulos" Visible="false"></asp:Label><br />
                        <br />
                        <div class="form-group">
                            <asp:Label ID="LblTipoProcesoESTADOCLIENTE" runat="server" Text="Tipo de Proceso :" Class="col-lg-2 control-label" Visible="false" />
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlTipoProcesoESTADOCLIENTE" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnGuardarAgregarRelacionEC" runat="server" CssClass="btn btn-group" Text="Guardar" Visible="false" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="LblEstadoESTADOCLIENTE" runat="server" Text="Estado :" Class="col-lg-2 control-label" Visible="false" />
                            <div class="col-lg-3">
                                <asp:DropDownList ID="DdlEstadoESTADOCLIENTE" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="BtnCancelarAgregarRelacionEC" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvListaEstadoCliente" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group">
                            <p id="LblTotalEstadoCliente" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                            <p id="LblTotalEstadoClienteL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <asp:GridView ID="GvListaEstadoCliente" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField CommandName="QuitarRelacion" Text="Quitar Relación" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ESTADO_CODIGO" HeaderText="" SortExpression="ESTADO_CODIGO">
                                            <ItemStyle ForeColor="White"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PROCESO" HeaderText="Tipo de Proceso" SortExpression="PROCESO" />
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                        <asp:BoundField DataField="PROCESO_CODIGO" HeaderText="" SortExpression="PROCESO_CODIGO">
                                            <ItemStyle ForeColor="White"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnGuardarAgregarRelacionEC" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnListarEstadoCliente" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <br />

            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
