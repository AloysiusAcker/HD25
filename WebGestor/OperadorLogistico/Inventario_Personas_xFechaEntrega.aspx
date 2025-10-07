<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_Oplogistico.master" AutoEventWireup="false" CodeFile="Inventario_Personas_xFechaEntrega.aspx.vb" Inherits="Inventario_Personas_xFechaEntrega" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 51px; text-align: center" valign="top">
                        <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                            font-size: 14pt; vertical-align: middle; width: 514px; color: gray; font-style: italic;
                            font-family: 'Bell MT', Broadway, Arial, Serif; position: static; height: 1px;
                            text-align: center">
                            Lista Persona x Fecha de Entrega</div>
                    </td>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                        </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 90px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 90px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 190px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="N° Pedido"></asp:Label></td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtNroPedido" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="170px"></asp:TextBox></td>
                    <td align="left" style="width: 190px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: right"
                        valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="N° Serie"></asp:Label></td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtNroSerie" runat="server" Font-Names="Arial" Font-Size="8pt" Width="170px"></asp:TextBox></td>
                    <td align="left" style="width: 190px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnExportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Exportar" Width="80px" /></td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px;" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 22px; vertical-align: middle; text-align: left;" valign="top">
                                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Entrega" Width="70px"></asp:Label></td>
                    <td align="left" style="width: 90px; height: 22px; vertical-align: middle; text-align: left;" valign="top">
                                                <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 90px; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"></asp:TextBox></td>
                    <td align="left" style="width: 190px; height: 22px;" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 100px; text-align: right; height: 22px;"
                        valign="top">
                        <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="80px" /></td>
                    <td align="left" style="width: 25px; height: 22px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="4" style="height: 19px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRegistro" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Maroon"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 19px; text-align: right"
                        valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 19px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: dimgray 1px inset; BORDER-TOP: dimgray 1px inset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px inset; WIDTH: 550px; BORDER-BOTTOM: dimgray 1px inset; POSITION: static; HEIGHT: 300px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1960px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnSelectedIndexChanged="Flex_SelectedIndexChanged" UseAccessibleHeader="False"><Columns>
<asp:ButtonField CommandName="Archivo" Text="Detalle" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="PER_PEDIDO" HeaderText="Nro Pedido">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA" HeaderText="Fecha Entrega">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_PROCESO" HeaderText="Fecha Proceso">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_NROLIQUIDACION" HeaderText="Nro. Liquidaci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_NROPAI_RECHAZO" HeaderText="Nro. PAI Rechazo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SAP" HeaderText="SAP">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NRO_EQUIPO" HeaderText="Nro. Serie">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PER_NOMBRECOMPLETO" HeaderText="Raz&#243;n Social &#243; Apellidos y Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_VISITA_CANT" HeaderText="Nro. Visitas">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_REAGENDA_1" HeaderText="Reagenda 1">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_REAGENDA_2" HeaderText="Reagenda 2">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_CODPAI_2" HeaderText="N&#176; PAI">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_REAGENDA_3" HeaderText="Reagenda 3">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_CODPAI_3" HeaderText="N&#176; PAI">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PER_CODIGO">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_CODIGO">
<ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_FRANJAHORARIA" HeaderText="Franja Horaria">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_AGENDA" HeaderText="Fecha Agenda">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE_EQUIPO" HeaderText="Nombre Equipo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_REABIERTO" HeaderText="Correlativo PAI">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="OBSERVACION_RECHAZO" HeaderText="Observaci&#243;n del Pedido">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_REG" HeaderText="Fecha Registro">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_REG" HeaderText="Hora Registro">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Italic="False" Font-Names="Arial"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                    <td align="left" colspan="5" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: dimgray 1px outset" id="DIV2" runat="server" visible="false"><asp:GridView id="FlexDet" runat="server" Width="550px" AutoGenerateColumns="False" __designer:wfdid="w1">
                                        <Columns>
                                            <asp:BoundField DataField="ARCHIVO_CODIGO" HeaderText="Codigo">
                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TIPO" HeaderText="Tipo Archivo">
                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Nombre del Archivo">
                                                <ItemTemplate>
                                                    <div id="Doc" runat="server" style="width: 200px; height: 22px">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Width="200px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ARCHIVO_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Width="200px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Names="Arial" Font-Size="8pt" />
                                    </asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                    <td align="left" colspan="5" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: dimgray 1px outset" id="DIV3" runat="server" visible="false"><asp:GridView id="FlexObs" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="PEDOBS_CODIGO" HeaderText="C&#243;digo">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TIPO" HeaderText="Tipo de Observaci&#243;n">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PEDOBS_DETALLE" HeaderText="Descripci&#243;n de la Observaci&#243;n">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 19px; vertical-align: middle;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 19px" valign="top">
                        <div style="text-align: left">
                            &nbsp;</div>
                    </td>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 80px" valign="top">
                    </td>
                    <td align="left" style="width: 90px" valign="top">
                    </td>
                    <td align="left" style="width: 90px" valign="top">
                    </td>
                    <td align="left" style="width: 190px" valign="top">
                    </td>
                    <td align="left" style="width: 100px" valign="top">
                    </td>
                    <td align="left" style="width: 25px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </div>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFecha"
                            TargetControlID="txtFecha" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFechaFin"
        TargetControlID="txtFechaFin">
    </cc1:CalendarExtender>
</asp:Content>

