<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Garantia_ConsultaEquipo.aspx.vb" Inherits="Garantia_ConsultaEquipo" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="4" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; vertical-align: middle; width: 424px; color: gray; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; position: static; height: 1px;
                        text-align: center">
                        Garantía Consulta de Equipos</div>
                </td>
                <td align="left" style="width: 26px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 55px; height: 10px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 10px" valign="top">
                </td>
                <td align="left" style="width: 26px; height: 10px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 55px; height: 10px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro. Serie"
                        Width="48px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 168px; height: 10px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                    <asp:TextBox ID="txtNroSerie" runat="server" Font-Names="Arial" Font-Size="8pt" Width="168px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 10px; text-align: left"
                    valign="top">
                    <asp:Button ID="btnBuscar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Buscar"
                        Width="60px" /><asp:Button ID="btnLimpiar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Limpiar"
                        Width="60px" /></td>
                <td align="left" style="width: 26px; height: 10px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 55px; height: 10px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 168px; height: 10px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 10px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 147px; height: 10px" valign="top">
                </td>
                <td align="left" style="width: 26px; height: 10px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Maroon" Text="Datos de la Garantía del Equipos" Width="184px"></asp:Label></td>
                <td align="left" style="width: 26px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="DIV1" runat="server" style="width: 550px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 540px">
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cod. Garantía"
                                    Width="72px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtCodGarantia" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="90px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="Nro. Serie   " Width="56px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                                <asp:TextBox ID="txtSerie" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                    Width="100px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="O. Compra" Width="56px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                                <asp:TextBox ID="txtOCompra" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                    Width="94px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Artículo"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtArtCodigo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="90px"></asp:TextBox></td>
                            <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                <asp:TextBox ID="txtArtDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="351px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Parte"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtArtParte" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                    Width="90px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="Modelo" Width="40px"></asp:Label></td>
                            <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                <asp:TextBox ID="txtArtModelo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="273px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Proveedor"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtRucProveedor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="90px"></asp:TextBox></td>
                            <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                <asp:TextBox ID="txtProveedor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="351px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cliente"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtRucCliente" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="90px"></asp:TextBox></td>
                            <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                <asp:TextBox ID="txtCliente" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                    Width="351px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Compra"
                                    Width="72px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtFecCompra" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="90px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="G. Proveedor" Width="68px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                                <asp:TextBox ID="txtFinCompra" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="100px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="Nro. Factura" Width="64px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                                <asp:TextBox ID="txtFactura" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                    Width="94px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Salida"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                <asp:TextBox ID="txtFecSalida" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="90px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="G. Cliente" Width="52px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                                <asp:TextBox ID="txtFinSalida" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="100px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: right"
                                valign="top">
                                <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                    Text="Condición" Width="52px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                                <asp:TextBox ID="txtCondicion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="94px"></asp:TextBox></td>
                        </tr>
                    </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 26px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 26px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: left">
        <div style="text-align: left">
            &nbsp;</div>
    </div>
</asp:Content>

