
<%@ Control Language="vb" AutoEventWireup="false" Inherits="MProyLeft" CodeFile="MProyLeft.ascx.vb" %>
<%@ Register Src="~/Menu/menu.ascx" TagPrefix="uc1" TagName="menu" %>



<table id="Table1" style="WIDTH: 150px; POSITION: relative; HEIGHT: 0.03%" runat="server"  cellspacing="0"
	cellpadding="0" width="152" border="0">
	<tr>
		<td style="POSITION: relative; width: 150px; height: 168px;">
            <uc1:menu runat="server" ID="menu" />
        </td>
	</tr>
	<tr>	
		<td style="POSITION: relative; width: 150px;">
            <asp:Menu ID="Menu2" runat="server" DynamicHorizontalOffset="2" Font-Names="Arial"
                Font-Size="8pt" ForeColor="Gray" MaximumDynamicDisplayLevels="2" StaticSubMenuIndent="10px"
                Width="150px" >
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" />
                <DynamicHoverStyle CssClass="DynamicHover" />
            </asp:Menu>	
        </td>
	</tr>
	<tr>
		<td style="POSITION: relative; width: 150px;"></td>
	</tr>
</table>
