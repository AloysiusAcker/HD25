<%@ Page Language="VB" MasterPageFile="~/Ventas/PagPrincipal_Venta.master" AutoEventWireup="false" CodeFile="Ventas_Cotizar_Proveedor.aspx.vb" Inherits="Ventas_Cotizar_Proveedor" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="7" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 550px; color: navy;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Cotizar Pedido</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 79px; height: 20px">
                </td>
                <td align="left" valign="top" style="width: 110px; height: 20px">
                </td>
                <td align="left" valign="top" style="width: 120px; height: 20px">
                </td>
                <td align="left" style="width: 130px; height: 20px;" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 20px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="5" valign="top">
                    <div id="DIV1" runat="server" style="width: 100px; height: 100px">
                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                            <contenttemplate>
<asp:GridView id="FlexPedido" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" BorderColor="SeaGreen" BorderWidth="1px" BorderStyle="Outset" AllowPaging="True" PageSize="5"><Columns>
<asp:ButtonField Text="Ingresar" ButtonType="Button" CommandName="Ingresar">
<ControlStyle BackColor="LightGray" BorderStyle="Outset" Width="80px" ForeColor="Gray" BorderWidth="1px" BorderColor="Gray" Font-Size="8pt" Font-Names="Arial"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="NroPedido" HeaderText="N&#186; Pedido">
<ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_PEDIDO" HeaderText="Fecha">
<ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Proveedor">
<ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="MONEDA" HeaderText="Cotizar en:">
<ItemStyle Width="110px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="110px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> 
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel></div>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 79px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
<asp:Label id="lblPedido" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial" Text="Nº de Pedido:" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexDetalle" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 110px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
                    <asp:Label ID="lblCodPedido" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="75px"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexDetalle" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 120px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 25px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:Button id="btnGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGuardar_Click" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Visible="False" Text="Guardar" BackColor="LightGray"></asp:Button> 
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="5" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblError2" runat="server" Width="520px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 247px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="5" style="height: 247px">
                    <div style="width: 550px; height: 229px; overflow: auto;">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<asp:GridView id="FlexDetalle" runat="server" Width="830px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" BorderColor="SeaGreen" BorderWidth="1px" BorderStyle="Outset" AllowPaging="True" PageSize="5" Visible="False" Font-Overline="False"><Columns>
<asp:BoundField DataField="NRO_PARTE" HeaderText="N&#186; de Parte">
<ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ARTICULO" HeaderText="Cod. Art.">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Width="180px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="180px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CANTIDAD" HeaderText="Cant.">
<ItemStyle Width="40px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Proveedor">
<ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="COD_PROVEEDOR">
<ItemStyle Width="0px"></ItemStyle>

<HeaderStyle Width="0px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="FENTREGA" HeaderText="F.Entrega">
<ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="F.Entrega">
<ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
<ItemTemplate>
<asp:TextBox id="txtFechaEntrega" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="PRECIO" HeaderText="Precio">
<ItemStyle Width="60px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Precio Uni.">
<ItemStyle Width="60px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
<ItemTemplate>
<asp:TextBox id="txtPrecioUnit" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" MaxLength="10"></asp:TextBox> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="FPAGO" HeaderText="F.Pago">
<ItemStyle Width="60px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="F.Pago">
<ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
<ItemTemplate>
<asp:TextBox id="txtFormaPago" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> 
</ItemTemplate>
</asp:TemplateField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> 
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexDetalle" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel></div>
                </td>
                <td align="left" style="width: 25px; height: 247px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" colspan="5" valign="top" style="height: 19px">
                    </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="5" style="height: 19px">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="520px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

