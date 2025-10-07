<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="CRM_Relacion_Ticket.aspx.vb" Inherits="CRM_CRM_Relacion_Ticket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container">
        <asp:Label ID="Label5" runat="server" Text="Relación de Tickets" CssClass="Titulos"></asp:Label><br />
        <br />
        <asp:Button ID="BtnLlamar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Llamar" visible="false" />
        
        <div class="row">          
            <div class="col-lg-3">           
                <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>             
            </div>
        </div> 

        <div class="row">          
            <div class="col-lg-3">  
                <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Listar" />                    
            </div>
            <div class="col-lg-3">                       
                <asp:Button ID="BtnLimpiar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Limpiar" />
            </div>
            <div class="col-lg-3">    
                <asp:Button ID="BtnExportar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Exportar" />                    
            </div>
            <div class="col-lg-3">    
                <asp:Button ID="BtnReprogramados" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Listado de Tickets Reprogramados" />                    
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-3 col-lg-2">
                        <asp:CheckBox ID="chkCliente" CssClass="checkbox checkbox-inline" Text="Cliente" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-lg-2">
                        <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Button ID="btnDatos" runat="server" ControlStyle-CssClass="btn btn-block" Text="..." />
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox ID="txtRazon" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <asp:Label ID="LblProceso" runat="server" CssClass="control-label-2" Text="Proceso"></asp:Label>
                        <asp:DropDownList ID="DdlProceso" runat="server" AutoPostBack="True"  CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="LblEstado" runat="server" CssClass="control-label-2" Text="Estado"></asp:Label>
                        <asp:DropDownList ID="DdlEstado" runat="server" AutoPostBack="True"  CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:Label ID="lblCodCliente" runat="server" CssClass="EstiloLabel" Visible="False"></asp:Label>
                <asp:Label ID="lblCodEstado" runat="server" CssClass="EstiloLabel" Visible="False"></asp:Label>

                <div class="row">
                </div>

                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="LblTipoPeticion" runat="server" CssClass="control-label-2" Text="Tipo de Petición"></asp:Label>
                        <asp:DropDownList ID="DdlComponente" runat="server" AutoPostBack="True"  CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="LblElement" runat="server" CssClass="control-label-2" Text="Elemento"></asp:Label>
                        <asp:DropDownList ID="DdlElemento" runat="server" AutoPostBack="True"  CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label13" runat="server" CssClass="control-label-2" Text="Elemento" ForeColor ="white"></asp:Label>
                        <asp:DropDownList ID="DdlElemento2" runat="server" AutoPostBack="True"  CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                </div>

                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="LblFechaIni" runat="server" CssClass="control-label-2" Text="Fecha"></asp:Label>
                        <asp:TextBox ID="txtFechaIni" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaIni" Format="dd/MM/yyyy" PopupButtonID="txtFechaIni" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="LblFechaFin" runat="server" CssClass="control-label-2" Text="Fecha Final"></asp:Label>
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaFin" Format="dd/MM/yyyy" PopupButtonID="txtFechaFin" ></cc1:CalendarExtender>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DdlComponente" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DdlElemento" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DdlProceso" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblTotalGW" runat="server" CssClass="control-label-2" Text="Total de Registros :"  visible="false" ></asp:Label>
                        <asp:Label ID="LblTotalGWL" runat="server" CssClass="control-label-2" Text=""  visible="false" ></asp:Label>
                    </div>
                </div>
                <div class="row col-lg-12" id="TablaClientes" runat="server">
                    <asp:GridView ID="GwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:ButtonField CommandName="Lista Documentos" Text="Documentos" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Mostrar Ticket" Text="Mostrar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Accion" Text="Accion" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Tracking" Text="Tracking" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="c1" HeaderText="Nro Ticket"></asp:BoundField>
                            <asp:BoundField DataField="c2" HeaderText="Fecha"></asp:BoundField>
                            <asp:BoundField DataField="c3" HeaderText="Hora"></asp:BoundField>
                            <asp:BoundField DataField="c4" HeaderText="Canal"></asp:BoundField>
                            <asp:BoundField DataField="c15" HeaderText="Proceso" />
                            <asp:BoundField DataField="c5" HeaderText="Tipo de Petición"></asp:BoundField>
                            <asp:BoundField DataField="c6" HeaderText="Elemento"></asp:BoundField>
                            <asp:BoundField DataField="c7" HeaderText="Estado"></asp:BoundField>
                            <asp:BoundField DataField="c8" HeaderText="Motivo"></asp:BoundField>
                            <asp:BoundField DataField="c9" HeaderText="Descripción"></asp:BoundField>
                            <asp:BoundField DataField="c10" HeaderText="Solución"></asp:BoundField>
                            <asp:BoundField DataField="c16" HeaderText="">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="c17" HeaderText="">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="c18" HeaderText="Asignado a"></asp:BoundField>
                            <asp:BoundField DataField="c19" HeaderText="">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="c20" HeaderText="">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>    
    </div>

    <div id="ModalPopupExtender2" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label1" Text="Relación de Clientes" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label2" runat="server" Text="" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step3" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-1">
                                                </div> 
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label15" runat="server" class="control-label-2" Text="RUC"></asp:Label>
                                                    <asp:TextBox ID="txtBusRuc" runat="server" class="form-control" />
                                                </div>
                                                <div class="col-md-3">
                                                </div> 
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label19" runat="server" class="control-label-2" Text="Listar" ForeColor="white"></asp:Label>
                                                    <asp:Button ID="btnListarTI" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                                                </div> 
                                            </div>
                                            <div class="row">
                                                <div class="col-md-1">
                                                </div> 
                                                <div class="col-md-6">
                                                    <asp:Label ID="Label18" runat="server" class="control-label-2" Text="Razón Social"></asp:Label>
                                                    <asp:TextBox ID="txtBusRazon" runat="server" class="form-control" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label20" runat="server" class="control-label-2" Text="Cerrar" ForeColor="white"></asp:Label>
                                                    <asp:Button ID="btnCerrarTI" runat="server" CssClass="form-control btn btn-default" Text="Cerrar" />
                                                </div> 
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                </div> 
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblTotalFlexL" runat="server" class="control-label-2" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <asp:GridView ID="FlexTI" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="AceptarTI" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CIF" HeaderText="RUC">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE" HeaderText="Razón Social">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TBTICKET_CLIENTE_CODIGO">
                                                                <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TBTICKET_CLIENTE_ESTADO">
                                                                <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                                            </asp:BoundField>
                                                        </Columns>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCerrarTI" EventName="Click" />
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
    
    <div id="ModalAsignarAcciones" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label3" Text="LISTA DE ACCIONES" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button ID="BtnCancelarAccion" runat="server" Text="Cancelar" CssClass="form-control btn btn-default" />
                                                    </div>
                                            </div>
                                            <asp:Label ID="LblNroTicket" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblCodProceso" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblCodEstadoMM" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblPersonal" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblFechaEstado" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblHoraEstado" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblCodUsuario" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblElemento" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblFechaReporta" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="LblHoraReporta" runat="server" Visible="false"></asp:Label>

                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="LblTotalAccionL" runat="server" class="control-label-2" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:GridView ID="GvListaAcciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                <Columns>
                                                                    <asp:ButtonField CommandName="Seleccionar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="ELEMEN_CODIGO" HeaderText="Codigo" SortExpression="ELEMEN_CODIGO" />
                                                                    <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Acción" SortExpression="ELEMEN_VALOR" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnCancelarAccion" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCancelarAccion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarEstado" EventName="Click" />
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

    <div id="ModalCambiarEstado" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="LblTituloMCE" Text="Acciones del Ticket - Cambio de Estado" />
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="CodAcción" runat="server" Text="" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
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
                                            <div class="row form-group col-lg-12">
                                                <asp:Label ID="LblNroTicketCA" runat="server" Text="N° Ticket :" CssClass="control-label col-sm-3 col-xs-12" />
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtNroTicketCA" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row form-group col-md-12 ">
                                                <asp:Label ID="lblEstadoModal" runat="server" CssClass="control-label col-sm-3 col-xs-12" Text="Est.a Cambiar :"></asp:Label>
                                                <div class="col-sm-6 col-xs-7">
                                                    <asp:DropDownList ID="DdlEstadoModal" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="LblHoraCE" runat="server" CssClass="control-label col-sm-3 col-xs-12" Text="Hora :"></asp:Label>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input id="TxtHoraCE" type="time" runat="server" cssclass="form-control-grid" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="LblFechaCE" runat="server" CssClass="control-label col-sm-3 col-xs-12" Text="Fecha :"></asp:Label>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input id="TxtFechaCE" type="date" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="LblObservacion" runat="server" Text="Observación :" CssClass="control-label col-sm-3 col-xs-12" />
                                                <div class="col-lg-9">
                                                    <textarea id="TxtObservacion" runat="server" rows="5" class="form-control" style="resize: none"></textarea>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnGuardarEstado" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <div class="col-sm-5 col-xs-2 col-lg-offset-4">
                                            <asp:Button ID="BtnGuardarEstado" runat="server" CssClass=" btn btn-default" Text="Guardar" />
                                            <asp:Button ID="BtnCerrarEstado" runat="server" CssClass=" btn btn-default" Text="Cerrar" />
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

    <div id="ModalAplicarAccionesVarias" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label7" Text="Acciones del Ticket - Aplicar Acciones Varias" />
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label8" runat="server" Text="" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step5" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="row form-group col-lg-13">
                                                <asp:Label ID="LblNroAccionAV" runat="server" Text="N° Acción :" Class="col-lg-2 control-label" />
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtNroAccionAV" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row form-group col-lg-13">
                                                <asp:Label ID="LblFechaAV" runat="server" CssClass="control-label col-sm-2 col-xs-12" Text="Fecha :"></asp:Label>
                                                <div class="col-sm-4 col-xs-3">
                                                    <input id="TxtFechaAV" type="date" runat="server" />
                                                </div>
                                                <asp:Label ID="LblHoraAV" runat="server" CssClass="control-label col-sm-1 col-xs-12" Text="Hora :"></asp:Label>
                                                <div class="col-sm-2 col-xs-3">
                                                    <input id="TxtHoraAV" type="time" runat="server" cssclass="form-control-grid" />
                                                </div>
                                            </div>

                                            <div class="row form-group col-lg-13">

                                                <asp:Label ID="LblCanalAV" runat="server" CssClass="control-label col-sm-2 col-xs-12" Text="Canal :"></asp:Label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <asp:DropDownList ID="DdlCanalAV" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Label ID="LblAccionAV" runat="server" CssClass="control-label col-sm-2 col-xs-12" Text="Acción :"></asp:Label>
                                                <div class="col-sm-4 col-xs-7">
                                                    <asp:DropDownList ID="DdlAccionAV" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row form-group col-lg-13">
                                                <asp:Label ID="LblContactoAV" runat="server" CssClass="control-label col-sm-2 col-xs-12" Text="Contacto :"></asp:Label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <asp:DropDownList ID="DdlContactoAV" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Label ID="LblDescripcionAccion" runat="server" Text="Descripción de la Acción :" Class="col-lg-2 control-label" />
                                                <div class="col-lg-4">
                                                    <textarea id="TxtDescripcionAccion" runat="server" rows="1" class="form-control" style="resize: none"></textarea>
                                                </div>
                                            </div>

                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-4">
                                                    <asp:Button ID="BtnNuevaAccionAV" runat="server" CssClass="btn btn-group" Text="Nueva Acción" />
                                                    <asp:Button ID="BtnGuardarAV" runat="server" CssClass="btn btn-group" Text="Guardar" />
                                                    <asp:Button ID="BtnCerrarAV" runat="server" CssClass="btn btn-group" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <p id="LblTotalAccionVarias" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros : </p>
                                                <p id="LblTotalAccionVariasL" class="control-label" style="color: darkred; font-weight: bold" runat="server" visible="false"></p>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <asp:GridView ID="GvListaAccionesVarias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="TICKETD_SECUENCIA" HeaderText="N° Acción" SortExpression="TICKETD_SECUENCIA" />
                                                            <asp:BoundField DataField="TICKETD_FECHA_ACCION" HeaderText="Fecha Acción" SortExpression="TICKETD_FECHA_ACCION" />
                                                            <asp:BoundField DataField="TICKETD_HORA_ACCION" HeaderText="Hora Inicio" SortExpression="TICKETD_HORA_ACCION" />
                                                            <asp:BoundField DataField="TICKETD_ACCION_HORA_FIN" HeaderText="Hora Fin " SortExpression="TICKETD_ACCION_HORA_FIN" />
                                                            <asp:BoundField DataField="CANAL" HeaderText="Canal" SortExpression="CANAL" />
                                                            <asp:BoundField DataField="CONTACTO" HeaderText="Contacto" SortExpression="CONTACTO" />
                                                            <asp:BoundField DataField="TICKETD_ACCION" HeaderText="Acción" SortExpression="TICKETD_ACCION" />
                                                            <asp:BoundField DataField="TICKETD_ACCION_DESCRIPCION" HeaderText="Descripción" SortExpression="TICKETD_ACCION_DESCRIPCION" />
                                                            <asp:BoundField DataField="TICKETD_USUARIO_ACCION" HeaderText="" SortExpression="TICKETD_USUARIO_ACCION" />
                                                            <asp:BoundField DataField="PERSONAL1" HeaderText="Personal" SortExpression="PERSONAL1" />
                                                            <asp:BoundField DataField="TICKETD_ACCION_ESTADO" HeaderText="Estado" SortExpression="TICKETD_ACCION_ESTADO" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarAV" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
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

    <div id="ModalReabrirTicket" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label9" Text="Acciones del Ticket - Reabrir el Ticket" />
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label10" runat="server" Text="" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step6" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <div class="row form-group col-lg-12">
                                                <asp:Label ID="LblNroTicketRT" runat="server" Text="N° Ticket :" Class="col-lg-2 control-label" />
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtNroTicketRT" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row form-group col-lg-6">
                                                <asp:Label ID="LblFechaRT" runat="server" CssClass="control-label col-sm-2 col-xs-12" Text="Fecha :"></asp:Label>
                                                <div class="col-sm-2 col-xs-3">
                                                    <input id="TxtFechaRT" type="date" runat="server" />
                                                </div>
                                                <asp:Label ID="LblHoraRT" runat="server" CssClass="control-label col-sm-1 col-xs-12" Text="Hora :"></asp:Label>
                                                <div class="col-sm-3 col-xs-3">
                                                    <input id="TxtHoraRT" type="time" runat="server" cssclass="form-control-grid" />
                                                </div>

                                                <asp:Button ID="BtnGuardarRT" runat="server" CssClass="btn btn-group" Text="Guardar" />
                                                <asp:Button ID="BtnCerrarRT" runat="server" CssClass="btn btn-group" Text="Cerrar" />
                                            </div>


                                            <div class="row form-group col-lg-12">
                                                <asp:Label ID="LblMotivoRT" runat="server" Text="Motivo :" Class="col-lg-2 control-label" />
                                                <div class="col-lg-9">
                                                    <textarea id="TxtMotivoRT" runat="server" rows="8" class="form-control" style="resize: none"></textarea>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
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

    <div id="ModalReasignarAsignarTicket" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label11" Text="Acciones del Ticket - Reasignar o Asignar el Ticket" />
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label14" runat="server" Text="" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step7" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:Label ID="LblNroTicketAT" runat="server" Text="N° Ticket :" Class="control-label" />
                                                    <asp:TextBox ID="TxtNroTicketAT" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label4" runat="server" Text="N° Ticket :" ForeColor="white" />
                                                    <asp:Button ID="BtnCerrarAT" runat="server" CssClass="form-control btn btn-default" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblTotalUsuariosL" runat="server" class="control-label-2" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <asp:GridView ID="GvListaUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="USUARI_CODIGO" HeaderText="Usuario" SortExpression="USUARI_CODIGO" />
                                                            <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" SortExpression="NOMBRES" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarAT" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvListaAcciones" EventName="RowCommand" />
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


    <div id="ModalTicketsReprogramados" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label16" Text="Tickets Reprogramados" />
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label17" runat="server" Text="" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnReprogramados" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step8" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnListarReprogramados" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnCerrarReprogramados" runat="server" CssClass="form-control btn btn-default" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblTotalReprogramadosL" runat="server" class="control-label-2" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <asp:GridView ID="GvListaReprogramados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="TICKET_CODIGO" HeaderText="N° Ticket" SortExpression="TICKET_CODIGO" />
                                                            <asp:BoundField DataField="PERSON_ASIGNADA" HeaderText="Persona Asignada" SortExpression="PERSON_ASIGNADA" />
                                                            <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                                                            <asp:BoundField DataField="MOTIVO_DESCRIPCION" HeaderText="Motivo Descripción" SortExpression="MOTIVO_DESCRIPCION" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarReprogramados" EventName="Click" />
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


    <div id="ModalListaDocumentos" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="LblTituloDcumentos" Text="" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step99" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnAgregarDocumentos" runat="server" CssClass="form-control btn btn-default" Text="Agregar Documentos" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnCerrarDocumentos" runat="server" CssClass="form-control btn btn-default" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblTotalDocumentosL" runat="server" class="control-label-2" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <div id="TablaListaDocumentos" runat="server">
                                                        <asp:GridView ID="GvListaDocumentos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:BoundField DataField="TEMA_AYUDA_REFERENCIA" HeaderText="N° Ticket" SortExpression="TEMA_AYUDA_REFERENCIA" />
                                                                <asp:BoundField DataField="TEMA_AYUDA_CODIGO" HeaderText="Documento" SortExpression="TEMA_AYUDA_CODIGO" />
                                                                <asp:BoundField DataField="TEMA_AYUDA_FECHA_INGRESO" HeaderText="Fecha Ingreso" SortExpression="TEMA_AYUDA_FECHA_INGRESO" />
                                                                <asp:BoundField DataField="TIPOINGRESO" HeaderText="Tipo Ingreso" SortExpression="TIPOINGRESO" />
                                                                <asp:BoundField DataField="N4" HeaderText="Clasificación" SortExpression="N4" />
                                                                <asp:BoundField DataField="TEMA_AYUDA_TIPODOC" HeaderText="Tipo Doc." SortExpression="TEMA_AYUDA_TIPODOC" />
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("TEMA_AYUDA_NOMBRE_DOC") %>' CommandName="OpenFile" CommandArgument='<%# Eval("TEMA_AYUDA_NOMBRE_DOC") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="TEMA_AYUDA_DESCRIPCION" HeaderText="Descripción" SortExpression="TEMA_AYUDA_DESCRIPCION" />
                                                                <asp:BoundField DataField="USUARIO" HeaderText="Usuario" SortExpression="USUARIO" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
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

    <div id="ModalTraking" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label12" Text="TRAKING DE TICKET" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step9" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnCerrarTraking" runat="server" CssClass="form-control btn btn-default" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%"  AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header" Visible="true" >
                                                                <cc1:TabPanel runat="server" HeaderText="Acciones" ID="TabPanel1">     
                                                                    <ContentTemplate >           
                                                                        <asp:GridView ID="GvTraking" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"  CssClass="table table-bordered GridView">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Fila" HeaderText="# ">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TICKET_CODIGO" HeaderText="Cod. Ticket">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Accion" HeaderText="Acción">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FECHAAC" HeaderText="Fecha">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="HORAAC" HeaderText="Hora">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="etiqueta_referencia" HeaderText="Referencia">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="cod_referencia" HeaderText="Codigo">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="ACCION_CODIGO" HeaderText="">
                                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                                                </asp:ButtonField>
                                                                            </Columns>
                                                                        </asp:GridView> 
                                                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate> 
                                                                                <div class="row espacio" runat="server" visible ="false" id="Salida1">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="lblCodSalida"  CssClass="control-label-2" runat="server" Text=""></asp:Label> 
                                                                                        <asp:GridView ID="gridSalida" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Codsalida" HeaderText="Codigo" SortExpression="Codsalida" />
                                                                                                <asp:BoundField DataField="Fecha_Sal" HeaderText="Fecha" SortExpression="Fecha_Sal" />
                                                                                                <asp:BoundField DataField="Hora_Salida" HeaderText="Hora" SortExpression="Hora_Salida" />
                                                                                                <asp:BoundField DataField="Origen_codigo" HeaderText="Cod. Almacén" SortExpression="Origen_codigo" />
                                                                                                <asp:BoundField DataField="Origen" HeaderText="Nombre" SortExpression="Origen" />
                                                                                                <asp:BoundField DataField="Destino" HeaderText="Destino tipo" SortExpression="Destino" />
                                                                                                <asp:BoundField DataField="DESTINO_CODINTERNO" HeaderText="Cod. Destino" SortExpression="DESTINO_CODINTERNO" />
                                                                                                <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Nombre Destino" SortExpression="DESTINO_NOMBRE" />
                                                                                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                                                                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                                                                <asp:TemplateField ItemStyle-Width="20px">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"CrmGuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("GUIREM_CODIGO") IsNot DBNull.Value, Eval("GUIREM_CODIGO"), Nothing))) %>' Width="100" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="Nro_Guia" HeaderText="Guía" SortExpression="Nro_Guia" />
                                                                                                <asp:BoundField DataField="DESP_ESTADO" SortExpression="DESP_ESTADO">
                                                                                                    <ItemStyle ForeColor="White" />
                                                                                                </asp:BoundField>
                                                                                                </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>   
                                                                                </div> 
                                                                                <div class="row espacio" runat="server" visible ="false" id="Salida2">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="LblEtiq35"  CssClass="control-label-2" runat="server" Text="Lista de producto"></asp:Label>                
                                                                                        <asp:GridView ID="gridSalidaEq" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                                                                <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Articulo" SortExpression="Cod_Articulo" /> 
                                                                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" /> 
                                                                                                <asp:BoundField DataField="Descripcion_Articulo" HeaderText="Artículo" SortExpression="Descripcion_Articulo" />
                                                                                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                                                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div> 
                                                                                </div>        
                                                                                <div class="row espacio" runat="server" visible ="false" id="Salida3">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="Label6"  CssClass="control-label-2" runat="server" Text="Lista de Accesorios"></asp:Label>                
                                                                                        <asp:GridView ID="gridSalidaAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                                                                <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Articulo" SortExpression="Cod_Articulo" /> 
                                                                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" /> 
                                                                                                <asp:BoundField DataField="Descripcion_Articulo" HeaderText="Artículo" SortExpression="Descripcion_Articulo" />
                                                                                                <asp:BoundField DataField="CANT" HeaderText="Cantidad" SortExpression="CANT" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div> 
                                                                                </div> 
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="GvTraking" EventName="RowCommand" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <asp:UpdatePanel ID="UpRecepcion" runat="server"  UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <div class="row espacio" runat="server" visible ="false" id="Recepcion1">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="LblCodRecepcion"  CssClass="control-label-2" runat="server" Text=""></asp:Label> 
                                                                                        <asp:GridView id="FlexRecep" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="RECEP_CODIGO" HeaderText="Código"  SortExpression="RECEP_CODIGO" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo"  SortExpression="MOTIVO" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="FECHA_REG" HeaderText="Fec. Reg."  SortExpression="FECHA_REG" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc."  SortExpression="TIPO_DOC" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="NRO_DOC" HeaderText="N° Documento"  SortExpression="NRO_DOC" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="RECEP_NRO_OC" HeaderText="N° O.C."  SortExpression="RECEP_NRO_OC" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="FECHA_RECEPCION" HeaderText="Fec. Recep."  SortExpression="FECHA_RECEPCION" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="ORIGEN_DESCRIPCION" HeaderText="Descripción"  SortExpression="ORIGEN_DESCRIPCION" ></asp:BoundField>
                                                                                                <asp:BoundField DataField="DESTINO_DESCRIPCION" HeaderText="Descripción"  SortExpression="DESTINO_DESCRIPCION" />
                                                                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" ></asp:BoundField>
                                                                                                <asp:TemplateField ItemStyle-Width="10px" HeaderText="OC">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"~/Inventario/RecepcionHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("RECEP_CODIGO") IsNot DBNull.Value, Eval("RECEP_CODIGO"), Nothing))) %>' Width="70" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField ItemStyle-Width="10px" HeaderText="Guía">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#"~/Inventario/RecepGuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("RECEP_CODIGO") IsNot DBNull.Value, Eval("RECEP_CODIGO"), Nothing))) %>' Width="70" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                                        </asp:GridView>
                                                                                    </div> 
                                                                                </div> 
                                                                                <div class="row espacio" runat="server" visible ="false" id="Recepcion2">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="lblRegDet" runat="server" CssClass="EstiloLabel" Font-Bold="True" Font-Italic="False" ForeColor="Maroon"></asp:Label>
                                                                                    </div>
                                                                                </div> 
                                                                                <div class="row espacio" runat="server" visible ="false" id="Recepcion3">
                                                                                    <div class="col-md-12">
                                                                                        <asp:GridView ID="FlexRecepDet" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="ITEM" HeaderText="Item" />
                                                                                                <asp:BoundField DataField="ART_COD" HeaderText="Art. Cod." />
                                                                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" />
                                                                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                                                                                <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. xRec." />
                                                                                                <asp:BoundField DataField="CANT_REC" HeaderText="Cant. Rec." />
                                                                                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" />
                                                                                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>
                                                                                </div> 
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="GvTraking" EventName="RowCommand" />
                                                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarTraking" EventName="Click" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate> 
                                                                                <div class="row espacio" runat="server" visible ="false" id="CentroCosto">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="lblCodSalidaCC"  CssClass="control-label-2" runat="server" Text=""></asp:Label>                                                                                         
                                                                                        <asp:GridView ID="gridCC" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Codsalida" HeaderText="Codigo" SortExpression="Codsalida" />
                                                                                                <asp:BoundField DataField="Fecha_Sal" HeaderText="Fecha" SortExpression="Fecha_Sal" />
                                                                                                <asp:BoundField DataField="Hora_Salida" HeaderText="Hora" SortExpression="Hora_Salida" />
                                                                                                <asp:BoundField DataField="Origen_codigo" HeaderText="Cod. Almacén" SortExpression="Origen_codigo" />
                                                                                                <asp:BoundField DataField="Origen" HeaderText="Nombre" SortExpression="Origen" />
                                                                                                <asp:BoundField DataField="Destino" HeaderText="Destino tipo" SortExpression="Destino" />
                                                                                                <asp:BoundField DataField="DESTINO_CODINTERNO" HeaderText="Cod. Destino" SortExpression="DESTINO_CODINTERNO" />
                                                                                                <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Nombre Destino" SortExpression="DESTINO_NOMBRE" />
                                                                                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                                                                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                                                                <asp:BoundField DataField="Nro_Guia" HeaderText="Nro Guia" SortExpression="Nro_Guia" />
                                                                                             </Columns>
                                                                                        </asp:GridView>
                                                                                    </div>   
                                                                                </div> 
                                                                                <div class="row espacio" runat="server" visible ="false" id="CentroCostoEq">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="lblgrdCCDet"  CssClass="control-label-2" runat="server" Text="Lista de producto"></asp:Label>                
                                                                                        <asp:GridView ID="grdCCDet" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                                                                <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Articulo" SortExpression="Cod_Articulo" /> 
                                                                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" /> 
                                                                                                <asp:BoundField DataField="Descripcion_Articulo" HeaderText="Artículo" SortExpression="Descripcion_Articulo" />
                                                                                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                                                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div> 
                                                                                </div>        
                                                                                <div class="row espacio" runat="server" visible ="false" id="CentroCostoAcc">
                                                                                    <div class="col-md-12">
                                                                                        <asp:Label ID="lblgrdCCDetAcc"  CssClass="control-label-2" runat="server" Text="Lista de Accesorios"></asp:Label>                
                                                                                        <asp:GridView ID="grdCCDetAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                                                                <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Articulo" SortExpression="Cod_Articulo" /> 
                                                                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" /> 
                                                                                                <asp:BoundField DataField="Descripcion_Articulo" HeaderText="Artículo" SortExpression="Descripcion_Articulo" />
                                                                                                <asp:BoundField DataField="CANT" HeaderText="Cantidad" SortExpression="CANT" />
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </div> 
                                                                                </div> 
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="GvTraking" EventName="RowCommand" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel> 
                                                                <cc1:TabPanel runat="server" HeaderText="Correo" ID="TabPanel2">      
                                                                    <ContentTemplate >           
                                                                        <asp:GridView ID="GvCorreo" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"  CssClass="table table-bordered GridView">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Fila" HeaderText="# ">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TICKET_NRO" HeaderText="Cod. Ticket">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razon Social">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="CONTACTO" HeaderText="Contacto">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="HORA" HeaderText="Hora">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="APROB_ESTADO" HeaderText="Estado">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="NRO_VEZ" HeaderText="Veces">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PERSONA_ENVIADA" HeaderText="Persona Enviada">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="PUESTO" HeaderText="Cargo">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView> 
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel> 
                                                                <cc1:TabPanel runat="server" HeaderText="Llamadas" ID="TabPanel3">     
                                                                    <ContentTemplate >       
                                                                        <asp:GridView ID="GvLlamada" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"  CssClass="table table-bordered GridView">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Fila" HeaderText="# ">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TICKET_CODIGO" HeaderText="Cod. Ticket">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="HORA" HeaderText="Hora">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TRAKING_EMAIL" HeaderText="E-mail">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView> 
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel> 
                                                                <cc1:TabPanel runat="server" HeaderText="Asignados" ID="TabPanel4">  
                                                                    <ContentTemplate >   
                                                                        <asp:GridView ID="GvAsignados" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"  CssClass="table table-bordered GridView">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Fila" HeaderText="# ">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="TICKET_CODIGO" HeaderText="Cod. Ticket">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="HORA" HeaderText="Hora">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Usuario" HeaderText="Usuario">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView> 
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel> 
                                                                <cc1:TabPanel runat="server" HeaderText="Estados" ID="TabPanel5">
                                                                    <ContentTemplate >           
                                                                        <asp:GridView ID="GvEstados" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt"  CssClass="table table-bordered GridView">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Fila" HeaderText="# ">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="aprob_CODIGO" HeaderText="Cod. Ticket">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="pEstado" HeaderText="Estado">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="HORA" HeaderText="Hora">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="FECHA_FIN" HeaderText="Fecha Fin">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="HORA_fIN" HeaderText="Hora Fin">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="DIFERENCIA" HeaderText="Duracion">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="REGISTRO_OBSERVACION" HeaderText="Observacion">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView> 
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel> 
                                                            </cc1:TabContainer>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GwLista" EventName="RowCommand" />
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

