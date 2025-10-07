<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_CambioContraseña.aspx.vb" Inherits="Sistema_SegSistema_CambioContraseña" title="Untitled Page" %>
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
                        Cambio de Contraseña</div>
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
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px; text-align: center"
                    valign="top">
                    <div id="DIV1" runat="server" style="border-right: gray 1px outset; border-top: gray 1px outset;
                        font-size: 8pt; border-left: gray 1px outset; width: 416px; border-bottom: gray 1px outset;
                        font-family: Tahoma; position: relative; height: 192px; left: 56px; top: 0px;">
                        <div style="display: inline; font-size: 9pt; z-index: 101; left: 64px; width: 104px;
                            font-family: Tahoma; position: absolute; top: 48px; height: 16px">
                            Nueva Contraseña</div>
                        <asp:TextBox ID="txtClaveN1" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                            MaxLength="12" Style="z-index: 102; left: 192px; position: absolute; top: 48px"
                            TabIndex="2" TextMode="Password" Width="160px" CssClass="bordeTexbox"></asp:TextBox>
                        <div style="display: inline; font-size: 9pt; z-index: 103; left: 64px; width: 128px;
                            font-family: Tahoma; position: absolute; top: 80px; height: 16px; text-align: left">
                            Confirmar Contraseña</div>
                        <asp:TextBox ID="txtClaveN2" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                            MaxLength="12" Style="z-index: 104; left: 192px; position: absolute; top: 80px"
                            TabIndex="3" TextMode="Password" Width="160px" CssClass="bordeTexbox"></asp:TextBox>
                        <div style="display: inline; font-size: 9pt; z-index: 105; left: 64px; width: 120px;
                            font-family: Tahoma; position: absolute; top: 16px; height: 14px; text-align: left">
                            Contraseña Actual</div>
                        <asp:TextBox ID="txtClaveAnt" runat="server" Font-Names="Tahoma" Font-Size="9pt" MaxLength="12" Style="z-index: 106; left: 192px; position: absolute;
                            top: 16px" TabIndex="1" TextMode="Password" Width="160px" CssClass="bordeTexbox"></asp:TextBox>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="32px" ShowMessageBox="True"
                            ShowSummary="False" Style="z-index: 107; left: 32px; position: absolute; top: 112px"
                            Width="160px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClaveN1"
                            Display="Dynamic" ErrorMessage="Ingresar la Nueva contraseña" Font-Names="Tahoma"
                            Font-Size="9pt" Style="z-index: 108; left: 352px; position: absolute; top: 48px">*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblErrorData" runat="server" ForeColor="Red" Style="z-index: 111;
                            left: 64px; position: absolute; top: 160px"></asp:Label>
                        <asp:Button ID="Guardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Bold="True" Style="z-index: 110; left: 200px; position: absolute;
                            top: 128px" TabIndex="17" Text="Cambiar" Width="128px" CssClass="EstiloBoton_Ac" />
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="txtClaveAnt"
                            Display="Dynamic" ErrorMessage="Ingresar la Contraseña Anterior" Font-Names="Tahoma"
                            Font-Size="9pt" Style="z-index: 112; left: 352px; position: absolute; top: 16px">*</asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validaClave"
                            ControlToValidate="txtClaveN1" Display="Dynamic" ErrorMessage="La Nueva Contraseña contiene un caracter no válido."
                            Font-Names="Tahoma" Font-Size="9pt" Style="z-index: 113; left: 360px; position: absolute;
                            top: 48px" Width="8px">*</asp:CustomValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtClaveN1"
                            ControlToValidate="txtClaveN2" Display="Dynamic" ErrorMessage="Las Contraseñas no coinciden"
                            Style="z-index: 114; left: 360px; position: absolute; top: 80px" Width="8px">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="txtClaveN2"
                            Display="Dynamic" ErrorMessage="Confirmar la Nueva contraseña" Font-Names="Tahoma"
                            Font-Size="9pt" Style="z-index: 115; left: 352px; position: absolute; top: 80px" Width="1px">*</asp:RequiredFieldValidator>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top">
                </td>
                <td align="left" valign="top">
                </td>
                <td align="left" valign="top">
                </td>
                <td align="left" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

