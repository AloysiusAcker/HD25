<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MenuPrincipal.ascx.vb" Inherits="Menu_MenuPrincipal" %>
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
<table border="0" cellpadding="0" cellspacing="0" style="width: 200px" id="TABLE1" onclick="return TABLE1_onclick()">
    <tr>
        <td align="left" style="height: 114px; width: 214px;" valign="top">
            <asp:Menu ID="MenuPrincipal" runat="server" BackColor="AliceBlue" DynamicHorizontalOffset="2"
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="20px" MaximumDynamicDisplayLevels="2"
                StaticSubMenuIndent="10px" Width="200px" style="background-color: #ffffff">
                <StaticMenuStyle BackColor="LightGray" />
                <StaticSelectedStyle BackColor="#507CD1" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <DynamicHoverStyle BackColor="#336699" ForeColor="White" />
                <DynamicMenuStyle BackColor="#B5C7DE" />
                <DynamicSelectedStyle BackColor="#507CD1" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
            </asp:Menu>
        </td>
    </tr>
</table>
