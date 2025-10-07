<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Contador.ascx.vb" Inherits="Contador" %>


<div style="text-align: left">
    <%--<div id="lblFecha" runat="server" style="display: inline; font-size: 8pt;  font-family: Arial;  text-align: right">Total de Visitas:</div>
    <div id="lbl" runat="server" style="display: inline; font-size: 8pt;  font-family: Arial;  text-align: right"></div>--%>
    <table border="0" style="width: 200px; border-top-width: 1px; border-left-width: 1px; border-left-color: #3366ff; border-bottom-width: 1px; border-bottom-color: #3366ff; border-top-color: #3366ff; border-right-width: 1px; border-right-color: #3366ff;">
        <tr>
            <td style="vertical-align: middle; font-size: 8pt; font-family: Arial; color: black; text-align: center; font-weight: bold;" class="auto-style1" >
                Total de Visitas:</td>
            <td  style="vertical-align: middle; width: 40px; height: 16px; text-align: left;" >
                <asp:Label ID="lbl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="36px" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
        </tr>
    </table>
</div>
