<%@ Control Language="vb" AutoEventWireup="false" Inherits="Menu_Menu" CodeFile="Menu.ascx.vb" %>
<script runat= "server">
    'Sub MenuA_MenuItemClick(ByVal sender As Object, ByVal e As MenuEventArgs)
    '    ' Display the text of the menu item selected by the user. 'Muestra el texto del elemento de menú seleccionado por el usuario.
    '    Session("MenuCod") = e.Item.Value
    '    Session("MenuNom") = e.Item.Text

    'End Sub

</script> 
<TABLE id="Table1" style="WIDTH: 150px; POSITION: relative; HEIGHT: 0%" cellSpacing="0"
	cellPadding="0" width="152" border="0">
	<TR>
		<TD style="POSITION: relative; width: 151px; vertical-align: baseline; text-align: left;">
            <asp:Menu ID="MenuA" runat="server" CssClass="A.LINK" DynamicHorizontalOffset="2"
                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" MaximumDynamicDisplayLevels="2"
                StaticSubMenuIndent="10px" Width="150px" onmenuitemclick = "MenuA_MenuItemClick">
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="LightGray" Font-Bold="True" ForeColor="Black" />
            </asp:Menu>
        </TD>
	</TR>
	<TR>
		<TD style="POSITION: relative; height: 17px; width: 151px;"></TD>
	</TR>
	<TR>
		<TD style="POSITION: relative; width: 151px;"></TD>
	</TR>
</TABLE>
