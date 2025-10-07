<%@ Page Language="VB" MasterPageFile="~/Menu/PagMenu.master" AutoEventWireup="false" CodeFile="Subscriptor.aspx.vb" Inherits="Menu_Subscriptor" title="Subscriptor" %>
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
                    <asp:Label ID="Label1" runat="server" Text="Nombre" Font-Names="Arial" Font-Size="8pt"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 420px; height: 20px" valign="top">
                    <asp:TextBox ID="txtNombre" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Apellido Paterno"
                        Width="103px"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 420px; height: 20px" valign="top">
                    <asp:TextBox ID="txtApepat" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 20px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Apellido Materno"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 420px; height: 20px" valign="top">
                    <asp:TextBox ID="txtApemat" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="100"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 17px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Teléfono"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 420px; height: 17px" valign="top">
                    <asp:TextBox ID="txtTelefono" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="50"
                        Width="330px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 17px; text-align: right"
                    valign="top">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="E-Mail"></asp:Label>&nbsp;</td>
                <td align="left" style="width: 420px; height: 17px" valign="top">
                    <asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="50"
                        Width="330px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="No válido" Height="16px" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Width="4px">*</asp:RegularExpressionValidator></td>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 17px; text-align: right; font-size: 8pt; font-family: Arial;"
                    valign="top">
                </td>
                <td align="left" style="width: 420px; height: 17px; font-size: 8pt; vertical-align: middle; font-family: Arial; text-align: left;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
                <td align="left" colspan="2" style="font-weight: bold; font-size: 8pt; vertical-align: middle;
                    color: gray; font-family: Arial; height: 17px; text-align: left" valign="top">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Seleccionar alguna de estas opciones:</td>
                <td align="left" style="width: 25px; height: 17px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 120px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 120px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="width: 420px; height: 120px" valign="top">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:CheckBoxList ID="chkListado" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="330px" Height="70px" RepeatColumns="2">
                        <asp:ListItem Value="0">Recepci&#243;n de Art&#237;culos</asp:ListItem>
                        <asp:ListItem Value="1">Recepci&#243;n de Capacitaci&#243;n</asp:ListItem>
                        <asp:ListItem Value="2">Recepci&#243;n de Boletines</asp:ListItem>
                        <asp:ListItem Value="3">Publicar Art&#237;culos</asp:ListItem>
                        <asp:ListItem Value="4">Publicar Capacitaciones</asp:ListItem>
                        <asp:ListItem Value="5">Publicar Boletines</asp:ListItem>
                    </asp:CheckBoxList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkListado" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                <td align="left" style="width: 25px; height: 120px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 20px; text-align: center"
                    valign="top" colspan="2">
                    <asp:Button ID="btnGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Guardar" CssClass="EstiloBoton" onmouseover ="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" Width="67px" ForeColor="Gray"/></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 130px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="width: 420px;" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="334px"></asp:Label></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
        </table>
    </div>
    &nbsp; &nbsp;
    &nbsp; &nbsp;
</asp:Content>

