<%@ Page Language="VB" MasterPageFile="~/CallCenter/PagPrincipal_Call.master" AutoEventWireup="false" CodeFile="CallCenter_DatosCliente.aspx.vb" Inherits="CallCenter_DatosCliente" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 182px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Clientes</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 150px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 150px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label1" runat="server" Text="Tipo Dato" Font-Names="Arial" Font-Size="8pt" Width="56px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboTipoDato" runat="server" Font-Names="Arial" Font-Size="8pt" Width="148px">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado" Width="40px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                <asp:DropDownList ID="cboEstado" runat="server" Font-Names="Arial" Font-Size="8pt" Width="148px">
                </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: right;" valign="top">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="70px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" Width="144px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado Operación"
                        Width="88px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboEstOperacion" runat="server" Font-Names="Arial" Font-Size="8pt" Width="148px">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="5">
                    <div id="DIV1" runat="server" style="border-right: gray 1px outset; border-top: gray 1px outset;
                        overflow: auto; border-left: gray 1px outset; width: 550px; border-bottom: gray 1px outset;
                        position: static; height: 200px">
                        <asp:GridView ID="Flex" runat="server" Font-Names="Arial" Font-Overline="False" Font-Size="8pt">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Llamar" Text="Llamar">
                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                </asp:ButtonField>
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFecha"
        TargetControlID="txtfecha">
    </cc1:CalendarExtender>
 </asp:Content>

