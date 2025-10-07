<%@ Page Language="VB" MasterPageFile="~/Encuesta/PagPrincipal_Encuenta.master" AutoEventWireup="false" CodeFile="VerResultEnc.aspx.vb" Inherits="VerResultEnc" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: navy; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Resultados de la Encuesta
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 51px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 51px" valign="top">
                    <div id="lblTitulo2" runat="server" style="font-weight: bold; font-size: 12pt; vertical-align: baseline;
                        width: 549px; color: navy; font-family: Arial; height: 32px; text-align: center">
                        Titulo</div>
                </td>
                <td align="left" style="width: 25px; height: 51px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                    <asp:DataGrid ID="Tabla" runat="server" BorderColor="DimGray" BorderWidth="1px" CellPadding="3"
                        Font-Names="Arial" Font-Size="8pt" Height="100px" Width="550px">
                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:Label ID="lblTotalEnc" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt"
                        Height="2px" Visible="False" Width="542px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                    <asp:Label ID="lblMensaje" runat="server" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Red"
                        Visible="False" Width="544px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                    <asp:HyperLink ID="Cancelar" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        ForeColor="Gray" Height="22px" NavigateUrl="Encuestas.aspx" Font-Italic="True">Continuar</asp:HyperLink></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

