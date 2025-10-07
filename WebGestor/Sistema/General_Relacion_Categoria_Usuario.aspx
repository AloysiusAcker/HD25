<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="General_Relacion_Categoria_Usuario.aspx.vb" Inherits="General_Relacion_Categoria_Usuario" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; width: 544px; color: gray; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; height: 1px; text-align: center">
                        Relación Usuario - Categoria</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 80px">
                </td>
                <td align="left" valign="top" style="width: 80px">
                </td>
                <td align="left" valign="top" style="width: 150px">
                </td>
                <td align="left" valign="top" style="width: 140px">
                </td>
                <td align="left" valign="top" style="width: 100px">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
                <td align="left" valign="top" style="vertical-align: middle; height: 22px" colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnListar" runat="server" CssClass="EstiloBoton_Ac" Text="Listar"
                        Width="80px" /></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnAsignar" runat="server" CssClass="EstiloBoton_Ac" Text="Asignar"
                        Width="80px" /></td>
                <td align="left" style="width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 140px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblRegistro" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto;
                                border-left: gray 1px outset; width: 550px; border-bottom: gray 1px outset; height: 250px">
                                <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="8pt">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Button" CommandName="Quitar" Text="Quitar">
                                            <ControlStyle CssClass="EstiloBoton_Ac" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="CATEGORIA_CODIGO" HeaderText="C&#243;digo">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CATEGORIA" HeaderText="Categoria">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USUARI_CODIGO" HeaderText="Usuario">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USUARIO" HeaderText="Nombre de Usuario">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 80px" valign="top">
                </td>
                <td align="left" style="width: 80px" valign="top">
                </td>
                <td align="left" style="width: 150px" valign="top">
                </td>
                <td align="left" style="width: 140px" valign="top">
                </td>
                <td align="left" style="width: 100px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="5" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div style="text-align: left">
                                <table id="tbAsignar" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 548px"
                                    visible="false">
                                    <tr>
                                        <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                            <asp:Label ID="lblEtq" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Maroon" Text="Relacionar Usuario"></asp:Label></td>
                                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                        </td>
                                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                                            <asp:Label ID="lblEtq2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Categoria"
                                                Width="56px"></asp:Label></td>
                                        <td align="left" style="vertical-align: middle; width: 328px; height: 22px" valign="top">
                                            <asp:DropDownList ID="cboCategoria" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                Font-Size="8pt" Width="320px">
                                            </asp:DropDownList></td>
                                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                            <asp:Button ID="btnGuardar" runat="server" CssClass="EstiloBoton_Ac" OnClick="btnGuardar_Click"
                                                Text="Guardar" Width="72px" /></td>
                                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="EstiloBoton_Ac" Text="Cancelar"
                                                Width="72px" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <div id="DIV5" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                                                        overflow: auto; border-left: darkgray 1px outset; width: 540px; border-bottom: darkgray 1px outset;
                                                        position: static; height: 176px">
                                                        <asp:GridView ID="FlexUsuario" runat="server" AutoGenerateColumns="False" BorderColor="DarkGray"
                                                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" PageSize="7"
                                                            Width="520px">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkUsuario" runat="server" Font-Names="Arial" Font-Size="8pt" Width="20px" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="USUARI_CODIGO" HeaderText="C&#243;digo">
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NOMBRES" HeaderText="Nombres y Apellidos">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="460px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cboCategoria" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                                        </td>
                                        <td align="left" style="vertical-align: middle; width: 328px; height: 22px" valign="top">
                                        </td>
                                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                        </td>
                                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAsignar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 80px" valign="top">
                </td>
                <td align="left" style="width: 80px" valign="top">
                </td>
                <td align="left" style="width: 150px" valign="top">
                </td>
                <td align="left" style="width: 140px" valign="top">
                </td>
                <td align="left" style="width: 100px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

