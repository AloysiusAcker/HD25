<%@ Page Language="VB" MasterPageFile="~/CallCenter/PagPrincipal_Call.master" AutoEventWireup="false" CodeFile="CallCenter_Pantalla_Operador.aspx.vb" Inherits="CallCenter_CallCenter_Pantalla_Operador" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="form-horizontal">
                <br />
                <asp:Label ID="TituloLlamada" runat="server" Text="Llamada Entrante" CssClass="Titulos"></asp:Label><br />
                <br />
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row form-group col-md-12 ">
                                <label class="control-label col-sm-2 col-xs-12">Cod. Persona :</label>
                                <div class="col-lg-2">
                                    <input class="form-control" id="TxtCodPersonaLLE" type="text" runat="server" />
                                </div>
                                <label class="control-label col-sm-3 col-xs-12">Código Interno :</label>
                                <div class="col-lg-2">
                                    <input class="form-control" id="TxtCodInternoLLE" type="text" runat="server" />
                                </div>
                                <label class="control-label col-sm-2 col-xs-12">Usuario :</label>
                                <label id="TxtUsuarioLLE" class="control-label col-sm-1 col-xs-12" runat="server" />
                                <label id="TxtCodClienteLLE" runat="server" visible="false" />
                                <label id="TxtNroTicket" runat="server" visible="false" />
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">DNI / RUC :</label>
                                <div class="col-lg-2">
                                    <input class="form-control" id="TxtDniRucLLE" type="text" runat="server" />
                                </div>
                                <div class="col-lg-1">
                                    <asp:Button ID="BtnDniRucLLE" runat="server" Text="..." ControlStyle-CssClass="btn btn-group" />
                                </div>
                                <div class="col-lg-4">
                                    <input class="form-control" id="TxtDescDniRucLLE" type="text" runat="server" />
                                </div>
                                <label class="control-label col-sm-2 col-xs-12">Hora de Inicio :</label>
                                <label id="TxtHoraInicioLLE" class="control-label col-sm-1 col-xs-12" runat="server" />
                            </div>
                            <div class="row form-group col-md-12 ">
                                <label class="control-label col-sm-2 col-xs-12">Cargo :</label>
                                <div class="col-lg-4">
                                    <input class="form-control" id="TxtCargoLLE" type="text" runat="server" />
                                </div>
                                <label class="control-label col-sm-2 col-xs-12">Contacto :</label>
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="DdlContactoLLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group col-md-12 ">
                                <label class="control-label col-sm-2 col-xs-12">Email :</label>
                                <div class="col-lg-10">
                                    <input class="form-control" id="TxtEmailLLE" type="text" runat="server" />
                                </div>
                            </div>
                            <div class="row form-group col-md-12 ">
                                <label class="control-label col-sm-2 col-xs-12">Dirección :</label>
                                <div class="col-lg-10">
                                    <input class="form-control" id="TxtDireccionLLE" type="text" runat="server" />
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Argumentario :</label>
                                <div class="col-lg-10">
                                    <textarea id="TxtArgumentarioLLE" runat="server" rows="5" class="form-control" style="resize: none"></textarea>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <div class="col-sm-5 col-xs-12 col-lg-offset-5">
                                    <asp:Button ID="BtnGuardarLLE" runat="server" CssClass=" btn btn-default" Text="Guardar" />
                                    <asp:Button ID="BtnCerrarLLE" runat="server" CssClass=" btn btn-default" Text="Cerrar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                                <label style="font-size: medium; text-align: center">Campaña - Lista de Productos</label>
                            </div>
                            <div class="row form-group col-md-12">
                                <asp:GridView ID="GvListaProductosLLE" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:BoundField DataField="ELEMEN_CODIGO" HeaderText="Codigo" SortExpression="ELEMEN_CODIGO" />
                                        <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Acción" SortExpression="ELEMEN_VALOR" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                                <label style="font-size: medium; text-align: center">Campos a Ingresar</label>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Tipo Persona :</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlTipoContactoLLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="< Seleccionar >" Selected="True">&#60; Seleccionar &#62;</asp:ListItem>
                                        <asp:ListItem Value="1">INDIRECTO</asp:ListItem>
                                        <asp:ListItem Value="2">DIRECTO</asp:ListItem>
                                        <asp:ListItem Value="3">NO CONTACTO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="control-label col-sm-2 col-xs-12">Respuesta :</label>
                                <div class="col-lg-5">
                                    <asp:DropDownList ID="DdlRespuestaLLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <asp:CheckBox runat="server" ID="ChkFechaPagoLLE" CssClass="control-label col-sm-2 col-xs-12 checkbox-inline" Text="F. Pago :" AutoPostBack="true" />
                                <div class="col-lg-3">
                                    <input id="TxtFechaPagoLLE" type="date" runat="server" class="form-control" readonly="readonly" />
                                </div>
                                <asp:CheckBox runat="server" ID="ChkHoraLLE" CssClass="control-label col-sm-2 col-xs-12 checkbox-inline" Text="Hora :" AutoPostBack="true" />
                                <div class="col-lg-2">
                                    <input id="TxtHoraLLE" type="time" runat="server" class="form-control" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <asp:CheckBox runat="server" ID="ChkFechaReLlamadaLLE" CssClass="control-label col-sm-2 col-xs-12 checkbox-inline" Text="F. Rellamada :" AutoPostBack="true" />
                                <div class="col-lg-3">
                                    <input id="TxtFechaReLlamadaLLE" type="date" runat="server" class="form-control" readonly="readonly" />
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Observación :</label>
                                <div class="col-lg-10">
                                    <textarea id="TxtObservacionLLE" runat="server" rows="5" class="form-control" style="resize: none"></textarea>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Acciones :</label>
                                <div class="col-lg-7">
                                    <asp:DropDownList ID="DdlAccionesLLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                                <label style="font-size: medium; text-align: center">Teléfono a Llamar</label>
                            </div>
                            <div style="display: initial; position: relative; width: 55%; float: left;">
                                <div class="col-lg-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="llamada" ID="Celular1" runat="server" AutoPostBack="false" Visible="false" />
                                </div>
                                <div class="col-lg-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="llamada" ID="Celular2" runat="server" AutoPostBack="false" Visible="false" />
                                </div>
                                <div class="col-lg-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="llamada" ID="Telefono1" runat="server" AutoPostBack="false" Visible="false" />
                                </div>
                                <div class="col-lg-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="llamada" ID="Telefono2" runat="server" AutoPostBack="false" Visible="false" />
                                </div>
                                <div class="col-lg-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="llamada" ID="Telefono3" runat="server" AutoPostBack="false" Visible="false" />
                                </div>
                                <div class="col-sm-1 col-xs-12 col-lg-offset-5">
                                    <asp:Button ID="BtnLlamarLLE" runat="server" CssClass=" btn btn-default" Text="Llamar" />
                                </div>
                            </div>
                            <div style="display: initial; position: relative; width: 40%; float: right;">
                                <div class="row form-group col-md-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="telefono" runat="server" ID="ChkClienteInubicableLLE" Width="250px" Text="Cliente Inubicable :" />
                                </div>
                                <div class="row form-group col-md-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="telefono" runat="server" ID="ChkTelefonoNoExisteLLE" Width="250px" Text="Teléfono no Existe :" />
                                </div>
                                <div class="row form-group col-md-12">
                                    <div class="col-lg-10">
                                        <input class="form-control" id="TxtTelefonoNoExisteLLE" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row form-group col-md-12">
                                    <asp:RadioButton CssClass="radio radio-inline" GroupName="telefono" runat="server" ID="ChkTelefonoNuevoLLE" Width="250px" Text="Teléfono Nuevo :" />
                                </div>
                                <div class="row form-group col-md-12">
                                    <div class="col-lg-10">
                                        <input class="form-control" id="TxtTelefonoNuevoLLE" type="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                                <label style="font-size: medium; text-align: center">Registrar Petición</label>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Proceso :</label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlProcesoLLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <label class="control-label col-sm-2 col-xs-12">Tipo Petición :</label>
                                <div class="col-lg-5">
                                    <asp:DropDownList ID="DdlTipoPeticionLLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Elemento :</label>
                                <div class="col-lg-5">
                                    <asp:DropDownList ID="DdlElemento1LLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-5">
                                    <asp:DropDownList ID="DdlElemento2LLE" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Descripción :</label>
                                <div class="col-lg-10">
                                    <textarea id="TxtDescripcionLLE" runat="server" rows="5" class="form-control" style="resize: none"></textarea>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <label class="control-label col-sm-2 col-xs-12">Solucion :</label>
                                <div class="col-lg-10">
                                    <textarea id="TxtSolucionLLE" runat="server" rows="5" class="form-control" style="resize: none"></textarea>
                                </div>
                            </div>
                            <div class="row form-group col-md-12">
                                <div class="col-lg-1 col-lg-offset-2">
                                    <asp:Button ID="BtnBuscarBaseConocimientoLLE" runat="server" Text="Buscar Base de Conocimiento" ControlStyle-CssClass="btn btn-default" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-group">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                                <label style="font-size: medium; text-align: center">Llamadas Anteriores</label>
                            </div>
                            <div class="row form-group col-md-12">
                                <asp:GridView ID="GvLlamadasAnterioresLLE" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:BoundField DataField="FECHA_LLAMADA" HeaderText="Fec. Llamada" SortExpression="FECHA_LLAMADA" />
                                        <asp:BoundField DataField="FECHA_COMPROMISO" HeaderText="Fec. Compromiso" SortExpression="FECHA_COMPROMISO" />
                                        <asp:BoundField DataField="FECHA_ALLAMAR" HeaderText="Fec. a Llamar" SortExpression="FECHA_ALLAMAR" />
                                        <asp:BoundField DataField="HORA_ALLAMAR" HeaderText="Hora a Llamar" SortExpression="HORA_ALLAMAR" />
                                        <asp:BoundField DataField="CALLDET_QCONTESTA_TPERSONA" HeaderText="Tipo Persona" SortExpression="CALLDET_QCONTESTA_TPERSONA" />
                                        <asp:BoundField DataField="CALLDET_QCONTESTA_NOMBRE" HeaderText="Quien Contestó" SortExpression="CALLDET_QCONTESTA_NOMBRE" />
                                        <asp:BoundField DataField="CALLDET_OBSERVACION" HeaderText="Observación" SortExpression="CALLDET_OBSERVACION" />
                                        <asp:BoundField DataField="CALLDET_TELEF_QLLAMAR" HeaderText="Telf. que Llama" SortExpression="CALLDET_TELEF_QLLAMAR" />
                                        <asp:BoundField DataField="RESPUESTA" HeaderText="Tipo Respuesta" SortExpression="RESPUESTA" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DdlProcesoLLE" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlTipoPeticionLLE" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlElemento1LLE" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlContactoLLE" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

