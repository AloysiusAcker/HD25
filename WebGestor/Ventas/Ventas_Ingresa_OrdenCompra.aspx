<%@ Page Language="VB" MasterPageFile="~/Ventas/PagPrincipal_Venta.master" AutoEventWireup="false" CodeFile="Ventas_Ingresa_OrdenCompra.aspx.vb" Inherits="Ventas_Ingresa_OrdenCompra" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>

    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" colspan="7" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 550px; color: navy;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Ingresar Orden de Compra</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 85px; height: 20px">
                </td>
                <td align="left" valign="top" style="width: 110px; height: 20px">
                </td>
                <td align="left" valign="top" style="width: 105px; height: 20px">
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
                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                            <contenttemplate>
<asp:GridView id="FlexPedido" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" BorderColor="SeaGreen" BorderStyle="Outset" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"><Columns>
<asp:ButtonField Text="Ingresar O.C." ButtonType="Button" CommandName="Ingresar">
<ControlStyle BackColor="LightGray" BorderStyle="Outset" Width="80px" ForeColor="Gray" BorderWidth="1px" BorderColor="Gray" Font-Size="8pt" Font-Names="Arial"></ControlStyle>

<ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:ButtonField>
<asp:BoundField DataField="COD_COTIZACION" HeaderText="N&#186; Cotiz.">
<ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="NRO_ORDEN" HeaderText="N&#186; O.C.">
<ItemStyle Width="90px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_COTIZ" HeaderText="F.Cotiz.">
<ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="FENTREGA" HeaderText="F.Entrega">
<ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CLIENTE" HeaderText="Cliente">
<ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> 
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 85px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 110px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 105px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 15px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 120px; height: 15px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 85px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
<asp:Label id="lblCotizacion" runat="server" Width="85px" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" Text="Nº de Cotización:" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 110px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
<asp:TextBox id="lblCodCotiz" runat="server" Font-Italic="False" Width="90px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Visible="False" BorderWidth="1px" BorderStyle="Solid" ReadOnly="True"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 105px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel7" runat="server">
                        <contenttemplate>
<asp:Label id="lblOrdenCompra" runat="server" Width="105px" Font-Size="8pt" Font-Names="Arial" Text="Nº Orden de Compra:" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 130px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtOrdenCompra" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" Visible="False" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 120px; height: 25px; vertical-align: top; text-align: right;" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
                    <asp:Button ID="btnGuardar" runat="server" BorderColor="SeaGreen" BorderStyle="Outset" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                         BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Guardar" OnClick="btnGuardar_Click" Visible="False" Width="80px" />
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" colspan="5" valign="top" style="height: 20px">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblError2" runat="server" Width="520px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 204px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="5" style="height: 204px">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<asp:GridView id="FlexDetalle" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" PageSize="5" AllowPaging="True" AutoGenerateColumns="False" BorderWidth="1px" BorderStyle="Outset" BorderColor="SeaGreen" Font-Overline="False"><Columns>
<asp:BoundField DataField="NRO_PARTE" HeaderText="N&#186; de Parte">
<ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="COD_ART" HeaderText="Cod. Art.">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ARTICULO" HeaderText="Descripci&#243;n">
<ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="200px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="NUEVO_CANT" HeaderText="Cant.">
<ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="NPRECIO_UNIT" HeaderText="P.Unit.">
<ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="NIGV" HeaderText="IGV">
<ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="NPRECIO" HeaderText="Total">
<ItemStyle Width="50px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> 
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPedido" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexDetalle" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 204px;" valign="top">
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
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="5" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

