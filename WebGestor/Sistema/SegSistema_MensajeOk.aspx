<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_MensajeOk.aspx.vb" Inherits="Sistema_SegSistema_MensajeOk" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="height: 50px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="background-image: url(../Fotos/linea.JPG); height: 12px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="height: 25px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 60px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 60px; text-align: center"
                    valign="top">
                    &nbsp;<asp:Label ID="lblMensaje" runat="server" Font-Names="Arial" Font-Size="14pt"
                        ForeColor="SeaGreen" Text="Ficha de Datos actualizada !!!" Width="536px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 60px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px; text-align: center"
                    valign="top">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" NavigateUrl="~/Default.aspx" Visible="False" Width="352px">Aceptar</asp:HyperLink></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

