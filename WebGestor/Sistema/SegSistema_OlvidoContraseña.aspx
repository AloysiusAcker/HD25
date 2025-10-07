<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_OlvidoContraseña.aspx.vb" Inherits="Sistema_SegSistema_OlvidoContraseña" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="4" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 253px; vertical-align: middle; width: 550px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                        text-align: center">
                        Recordar Contraseña</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 140px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 135px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 140px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 135px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 50px; text-align: center"
                    valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Para recordarle su contraseña le agradeceremos ingresar el email con el cual se suscribió al servicio"
                        Width="312px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px;" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; text-align: center; height: 25px;" valign="top">
                        <asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="8pt"
                            MaxLength="50" Style="z-index: 114; left: 112px; position: static; top: 224px"
                            TabIndex="1" Width="280px" CssClass="bordeTexbox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="Ingresar Email" Font-Names="Arial"
                        Font-Size="8pt">*</asp:RequiredFieldValidator>
                </td>
                <td align="left" style="width: 25px; height: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 25px; text-align: center"
                    valign="top">
                        <asp:Button ID="Enviar" runat="server" CssClass="EstiloBoton_Ac" Style="z-index: 103;
                            left: 224px; top: 81px" Text="Aceptar" Width="128px" /></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; text-align: center" valign="top">
                        <asp:Label ID="lblMsg" runat="server" Font-Names="Tahoma" Font-Size="8pt" ForeColor="Red"
                            Height="16px" Style="z-index: 101; left: 8px; top: 120px"
                            Width="512px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 140px" valign="top">
                </td>
                <td align="left" style="width: 135px" valign="top">
                </td>
                <td align="left" style="width: 140px" valign="top">
                </td>
                <td align="left" style="width: 135px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

