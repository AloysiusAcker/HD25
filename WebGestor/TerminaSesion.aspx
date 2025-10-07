<%@ Page Language="VB" MasterPageFile="~/PagPrincipal.master" AutoEventWireup="false" CodeFile="TerminaSesion.aspx.vb" Inherits="TerminaSesion" title="CAS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <div id="lblMensaje" runat="server" style="font-weight: normal; font-size: 14pt;
                        width: 543px; color: dimgray; font-family: Arial; height: 28px; text-align: center">
                        El tiempo de su sesión ha terminado, vuelva a ingresar !!!</div>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <div style="font-size: 8pt; vertical-align: middle; width: 543px; color: darkgray;
                        font-family: Arial; height: 16px; text-align: center">
                        Por favor, hacer clic en el botón para continuar navegando en nuestra web.
                    </div>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 550px; height: 19px; text-align: center"
                    valign="top">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" Font-Underline="False" ForeColor="SeaGreen" NavigateUrl="Default.aspx">Inicio</asp:HyperLink></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

