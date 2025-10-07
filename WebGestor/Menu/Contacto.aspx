<%@ Page Language="VB" MasterPageFile="~/Menu/PagMenu.master" AutoEventWireup="false" CodeFile="Contacto.aspx.vb" Inherits="Menu_Contacto" title="Contacto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="height: 50px" valign="top" colspan="2">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        MENU</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 11px" valign="top" colspan="4">
                    <img src="../Menu/Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="height: 20px" valign="top" colspan="2">
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label1" runat="server" Text="Empresa" Font-Names="Arial" Font-Size="8pt"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:TextBox ID="txtEmpresa" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Persona de Contacto"
                        Width="103px"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:TextBox ID="txtPerContacto" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Dirección"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:TextBox ID="txtDireccion" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 17px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código Postal"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 17px" valign="top">
                    <asp:TextBox ID="txtCodPostal" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="50"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="País"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:DropDownList ID="cboPais" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="337px" AutoPostBack="True">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 20px; text-align: right"
                    valign="top">
                    &nbsp; &nbsp;<asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Text="Departamento"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboDpto" runat="server" Width="337px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboDpto_SelectedIndexChanged"></asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="cboPais" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Provincia"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cboProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="337px" AutoPostBack="True" OnSelectedIndexChanged="cboProv_SelectedIndexChanged">
                    </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboDpto" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 18px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 18px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Distrito"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 18px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cboDist" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="337px" AutoPostBack="True">
                    </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboProv" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 18px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Teléfono"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:TextBox ID="txtTelefono" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="50"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="E-Mail"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    <asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="No es Válido" Font-Names="Arial" Font-Size="8pt" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 160px" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 130px; height: 160px; text-align: right"
                    valign="top">
                    <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Comentario"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 419px; height: 160px" valign="top">
                    <asp:TextBox ID="txtComentario" runat="server" Font-Names="Arial" Font-Size="8pt" Height="150px"
                        MaxLength="1490" TextMode="MultiLine" Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 160px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 130px; height: 20px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="width: 419px; height: 20px" valign="top">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnEnviar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Enviar" CssClass="EstiloBoton" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" Width="67px" ForeColor="Gray"/></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 130px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="width: 419px;" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="350px"></asp:Label></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
        </table>
    </div>
    &nbsp; &nbsp;
    &nbsp; &nbsp;
</asp:Content>

