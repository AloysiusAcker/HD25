<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CProyLeft.ascx.vb" Inherits="CProyLeft" %>
<%@ Register Src="~/Menu/menu.ascx" TagPrefix="uc1" TagName="menu" %>

<table id="TbMenu" style="width: 100%; POSITION: relative; HEIGHT: 0.03%;" runat="server" cellspacing="0" cellpadding="0"  border="0">
    <tr>
        <td>
            <uc1:menu runat="server" ID="menu" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Menu ID="Menu2" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Gray" MaximumDynamicDisplayLevels="2" StaticSubMenuIndent="10px"
                Width="150px" >
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" />
                <DynamicHoverStyle CssClass="DynamicHover" />
            </asp:Menu>	</td>
    </tr>
</table>
