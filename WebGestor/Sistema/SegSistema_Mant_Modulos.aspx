<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_Mant_Modulos.aspx.vb" Inherits="SegSistema_Mant_Modulos" title="Sistema - Módulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="4" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Define Módulos</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 175px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 175px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Gray" Height="20px" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="80px" /></td>
                <td align="left" style="vertical-align: middle; width: 175px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 120px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 175px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 274px"><asp:GridView id="Flex" runat="server" Width="750px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" PageSize="40" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="MODINTEG_NOMBRE" HeaderText="M&#243;dulo Integraci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="MOD_CODIGO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="MOD_NOMBRE" HeaderText="Nombre del M&#243;dulo Interno">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="MOD_ESTADO" HeaderText="En el Sistema?">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="MOD_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Left" VerticalAlign="Middle"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <div style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                        <table id="lblModuloIntegracion" runat="server" border="0" cellpadding="0" cellspacing="0"
                            style="width: 550px" visible="false">
                            <tr>
                                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:Label ID="lblMEtiqueta" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Maroon"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Label ID="lblM2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombre"
                                        Width="40px"></asp:Label></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:TextBox ID="txtnombre" runat="server" Font-Names="Arial" Font-Size="8pt" Width="460px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Label ID="lblM3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:TextBox ID="txtdescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="460px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Label ID="lblM4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Puesto en el Sistema"
                                        Width="64px"></asp:Label></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:RadioButtonList ID="OptSN" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        RepeatDirection="Horizontal" Width="101px">
                                        <asp:ListItem Value="0">SI</asp:ListItem>
                                        <asp:ListItem Value="1">NO</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="2">
                                    <asp:Label ID="lblM5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Marcar los Mod. de Integración que pertenece"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 220px; height: 22px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:TextBox ID="lblcodigo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="27px"></asp:TextBox></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <div id="DIV1" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                                        overflow: auto; border-left: darkgray 1px outset; width: 460px; border-bottom: darkgray 1px outset;
                                        height: 100px">
                                        <asp:CheckBoxList ID="lstModulosInteg" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            RepeatColumns="2" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList></div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 20px;" valign="top">
                                </td>
                                <td align="left" style="vertical-align: middle; width: 250px; height: 20px;" valign="top">
                                </td>
                                <td align="left" style="vertical-align: middle; width: 220px; height: 20px; text-align: right;" valign="top">
                                    <asp:Button ID="btnGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Gray" Height="20px" onmouseout="this.style.fontWeight='normal'"
                                        onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="80px" />
                                    <asp:Button ID="btnCancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Gray" Height="20px" onmouseout="this.style.fontWeight='normal'"
                                        onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="80px" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        &nbsp;</div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="4">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

