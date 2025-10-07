<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_Oplogistico.master" AutoEventWireup="false" CodeFile="Inventario_Archivo_Telefonica.aspx.vb" Inherits="Inventario_Archivo_Telefonica" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="4" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 284px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Archivo Telefonica</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 270px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                    </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 11px; text-align: left"
                    valign="top">
                    </td>
                <td align="left" style="width: 100px; height: 11px" valign="top">
                    <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Listar"
                        Width="80px" ForeColor="Gray" /></td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Entrega"
                        Width="70px"></asp:Label></td>
                <td align="left" style="width: 90px; height: 11px; vertical-align: middle;" valign="top">
                    <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" Width="88px"></asp:TextBox></td>
                <td align="left" style="width: 270px; height: 11px; vertical-align: middle;" valign="top">
                    <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="88px"></asp:TextBox></td>
                <td align="left" style="width: 100px; height: 11px" valign="top">
                    <asp:Button ID="btnExportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Exportar" Width="80px" /></td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="height: 19px" valign="top" colspan="3">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<asp:Label id="lblRegistro" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 100px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; text-align: center" valign="top">
                    <asp:UpdateProgress id="UpdateProgress1" runat="server">
                        <progresstemplate>
<IMG src="../Fotos/5.gif" /><BR /><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Esperando ..."></asp:Label> 
</progresstemplate>
                    </asp:UpdateProgress></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 624px;" valign="top">
                </td>
                <td align="left" colspan="4" valign="top" style="height: 624px">
                    <div style="width: 550px; overflow: auto; height: 600px; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;" id="DIV1" runat="server">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<asp:GridView id="Flex" runat="server" Width="1230px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="Fecha" HeaderText="Fecha">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PER_NOMBRECOMPLETO" HeaderText="Nombre">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_NRO" HeaderText="Nro. Pedido">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_TELEF" HeaderText="Telefono">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NRO_EQUIPO" HeaderText="Nro. Serie">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE_EQUIPO" HeaderText="Modelo Equipo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="OBSERVACION" HeaderText="Observaci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPIFICACION" HeaderText="Tipificaci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> 
</contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel></div>
                </td>
                <td align="left" style="width: 25px; height: 624px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 270px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="height: 19px" valign="top" colspan="4">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
        <div style="text-align: center">
            &nbsp;</div>
        &nbsp; &nbsp;
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha" PopupButtonID="txtFecha">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin" PopupButtonID="txtFechaFin">
        </cc1:CalendarExtender>
    </div>
</asp:Content>

