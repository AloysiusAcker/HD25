<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_BusquedaBaseDatos.aspx.vb" Inherits="Cas_BusquedaBaseDatos" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="9" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Busqueda de Base de Datos</div>
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 11px;" valign="top" colspan="9">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" style="height: 20px; vertical-align: middle;" colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativos"></asp:Label></td>
                <td align="left" valign="top" style="height: 20px; vertical-align: middle;" colspan="2">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Productos"></asp:Label></td>
                <td align="left" valign="top" style="height: 20px; vertical-align: middle;" colspan="2">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sub-Productos"></asp:Label></td>
                <td align="left" style="width: 80px; height: 20px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 25px; height: 20px;">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="height: 20px" valign="top" colspan="2">
                    <asp:DropDownList ID="cboAplicativo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="156px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="height: 20px" valign="top" colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboProducto" runat="server" Width="156px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                            </asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>&nbsp;</td>
                <td align="left" style="height: 20px" valign="top" colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboSubProducto" runat="server" Width="146px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                            </asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 80px; height: 20px; text-align: left;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:Button id="btnListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="60px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></asp:Button> <BR /><asp:Button id="btnTop10" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnTop10_Click" runat="server" Width="60px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Top 10" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></asp:Button>
</ContentTemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="optModoBus" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Palabras a Buscar:"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Modo de Busqueda:"
                        Width="100px"></asp:Label></td>
                <td align="left" style="width: 80px; height: 20px; text-align: left" valign="top">
                <asp:Button ID="btnImprimir" runat="server" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Imprimir"
                        Width="60px" Visible="False" ForeColor="Gray" BackColor="LightGray" /></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="4" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtBuscador" runat="server" Width="300px" Height="34px" TextMode="MultiLine"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>&nbsp;&nbsp;
                </td>
                <td align="left" colspan="3" style="height: 20px; text-align: left" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:RadioButtonList id="optModoBus" runat="server" Width="106px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatLayout="Flow" RepeatDirection="Horizontal"><asp:ListItem Selected="True" Value="0">A ó B</asp:ListItem>
<asp:ListItem Value="1">A y B</asp:ListItem>
</asp:RadioButtonList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="7" valign="top">
                    <div id="DIV1" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                        overflow: auto; border-left: darkgray 1px outset; width: 550px; border-bottom: darkgray 1px outset;
                        height: 337px">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<asp:GridView id="Flex" runat="server" Width="1760px" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" UseAccessibleHeader="False" DataKeyNames="CARCON_APLICATIVO,CARCON_PRODUCTO,CARCON_SUBPRODUCTO" AllowPaging="True" PageSize="5" AutoGenerateColumns="False" ><Columns>
<asp:ButtonField CommandName="Detalle" Text="Detalle">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="1000px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_CODIGO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle Wrap="True" BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset"></HeaderStyle>
</asp:GridView> 
</contenttemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnTop10" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel></div>
                </td>
                <td align="left" valign="top" style="width: 25px;">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 10px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="7" valign="top">
                    <asp:UpdatePanel id="UpdatePanel7" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 190px">
    <asp:DetailsView id="DetalleLista" runat="server" Width="550px" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="LightGray" CellPadding="4" AutoGenerateRows="False">
<FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>

<PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
<Fields>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SUBPRODUCTO" HeaderText="Subproducto">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
</Fields>

<HeaderStyle BackColor="#333333" BorderColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></EditRowStyle>
</asp:DetailsView></DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 80px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 80px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 80px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 80px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 79px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" style="width: 80px; height: 19px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 25px; height: 19px;">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 19px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="498px"></asp:Label></td>
                <td align="left" valign="top" style="width: 25px; height: 19px;">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" style="height: 19px; width: 80px;">
                </td>
                <td align="left" valign="top" style="height: 19px; width: 80px;">
                </td>
                <td align="left" valign="top" style="height: 19px; width: 80px;">
                </td>
                <td align="left" valign="top" style="height: 19px; width: 80px;">
                </td>
                <td align="left" valign="top" style="height: 19px; width: 79px;">
                </td>
                <td align="left" valign="top" style="height: 19px; width: 70px;">
                </td>
                <td align="left" style="width: 80px; height: 19px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 25px; height: 19px">
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

