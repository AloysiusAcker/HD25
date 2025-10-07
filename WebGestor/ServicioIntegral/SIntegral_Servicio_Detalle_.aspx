<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SIntegral_Servicio_Detalle_.aspx.vb" Inherits="Servicios_SIntegral_Servicio_Detalle_" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 15pt; vertical-align: middle; width: 536px; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 18px; text-align: center">
                        Servicios</div>
                </td>
                <td align="left" style="width: 30px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="background-image: url(../Fotos/Linea_Gris.bmp);
                    height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="width: 100px;" valign="top">
                </td>
                <td align="left" style="width: 120px;" valign="top">
                </td>
                <td align="left" style="width: 120px;" valign="top">
                </td>
                <td align="left" style="width: 120px;" valign="top">
                </td>
                <td align="left" style="width: 90px;" valign="top">
                </td>
                <td align="left" style="width: 30px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" ActiveTabIndex="0" Font-Overline="False"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Servicios
                            
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 350px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w79"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label1" runat="server" Width="96px" Font-Size="8pt" Font-Names="Arial" Text="Sector Económico" __designer:wfdid="w80"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 350px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboSector" runat="server" Width="352px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w81"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" runat="server" CssClass="EstiloBoton_Ac" Width="76px" Text="Listar" __designer:wfdid="w82"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label2" runat="server" Font-Underline="False" Font-Size="8pt" Font-Names="Arial" Text="Tipo" __designer:wfdid="w83" Font-Overline="False" Font-Strikeout="False"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 350px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboTipo" runat="server" Width="352px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w84"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 408px"><asp:GridView id="Flex" runat="server" Width="600px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w85" Font-Overline="False"><Columns>
<asp:ButtonField Text="Ver" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODIGO" HeaderText="Nro. Servicio">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:ImageField DataImageUrlField="SERVDET_FOTO" DataImageUrlFormatString="~/ServicioIntegral/Imagenes/{0}" HeaderText="Imagen">
<ControlStyle Height="50px" Width="100px"></ControlStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:ImageField>
<asp:BoundField DataField="SECTOR" HeaderText="Sector Econ&#243;mico">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO_TIPO2" HeaderText="Tipo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVDET_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVDET_ITEM">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 350px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                        <td align="left" style="width: 30px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

