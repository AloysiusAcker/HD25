<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ControlMenu.ascx.vb" Inherits="ControlMenu" %>
<%@ Register Src="~/Menu/MenuPrincipal.ascx" TagName="MenuPrincipal" TagPrefix="uc1" %>
<div style="text-align: left">
    <table border="0"  style="width: 200px">
        <tr>
            <td >
                <uc1:MenuPrincipal ID="MenuPrincipal1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
<asp:Menu ID="Menu1" runat="server" BackColor="AliceBlue" DynamicHorizontalOffset="2"
    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="20px" MaximumDynamicDisplayLevels="2"
    StaticSubMenuIndent="10px" Width="200px" style="background-color: #ffffff">
    <StaticMenuStyle BackColor="LightGray" />
    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <DynamicHoverStyle BackColor="#336699" ForeColor="White" />
    <DynamicMenuStyle BackColor="#B5C7DE" />
    <StaticSelectedStyle BackColor="#507CD1" />
    <DynamicSelectedStyle BackColor="#507CD1" />
    <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
    <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
</asp:Menu>
            </td>
        </tr>
    </table>
</div>
