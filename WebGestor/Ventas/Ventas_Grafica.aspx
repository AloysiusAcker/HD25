<%@ Page Language="VB" MasterPageFile="~/Ventas/PagPrincipal_Nuevo.master" AutoEventWireup="false" CodeFile="Ventas_Grafica.aspx.vb" Inherits="Ventas_Grafica" title="Graficos" %>

<%--<%@ Register Assembly="DevExpress.Dashboard.v18.1.Web.WebForms, Version=18.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 900px;">
        
         <tr>
            <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Gráficos Generales</div>
                </td>
         </tr>
         <tr>
            <td style="height: 19px; " colspan="3">
                <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
             </td>
        </tr>
         <tr>
            <td style="height: 19px; width: 100px;"></td>
            <td style="height: 19px; width: 200px;"></td>
            <td style="height: 19px; width: 400px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 100px;"></td>
            <td style="height: 19px; " colspan="2">

             </td>
        </tr>
         <tr>
            <td style="height: 19px; width: 100px;"></td>
            <td style="height: 19px; width: 200px;"></td>
            <td style="height: 19px; width: 400px;">
                <div style="position:absolute; left:0; right:0; top:0; bottom:0;">
<%--                    <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" 
                        WorkingMode="Viewer"
                        Height="100%" Width="100%">
                        <ClientSideEvents
                            ItemWidgetCreated="function(s, e) { customizeWidgets(s, e); }" 
                            ItemWidgetUpdated="function(s, e) { customizeWidgets(s, e); }" 
                            ItemWidgetUpdating="function(s, e) { unsubscribeFromEvents(s, e); }" />
                    </dx:ASPxDashboard>--%>
                </div>

            </td>
        </tr>
         </table>

</asp:Content>

