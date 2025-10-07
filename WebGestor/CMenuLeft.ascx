<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CMenuLeft.ascx.vb" Inherits="CMenuLeft" %>
<%@ Register Src="~/Menu/menu.ascx" TagPrefix="uc1" TagName="menu" %>
<div class="sidebar">    
    <%--<ul id="customMenu" runat="server" class="nav nav-pills nav-stacked custom-menu"></ul>--%>
            <asp:Menu ID="Menu2" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Gray" MaximumDynamicDisplayLevels="2" StaticSubMenuIndent="10px">
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" />
            </asp:Menu>	

</div>