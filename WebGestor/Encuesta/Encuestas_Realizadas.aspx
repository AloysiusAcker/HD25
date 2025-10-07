<%@ Page Language="VB" MasterPageFile="~/Encuesta/PagPrincipal_Encuenta.master" AutoEventWireup="false" CodeFile="Encuestas_Realizadas.aspx.vb" Inherits="Encuestas_Realizadas" title="Untitled Page" %>
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
                        Encuestas y Pruebas Realizadas
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
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:DataGrid ID="Tabla" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BorderColor="DimGray" BorderWidth="1px" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt"
                        Height="100px" HorizontalAlign="Left" OnPageIndexChanged="Tabla_Page" PageSize="7" Width="550px" BorderStyle="Outset">
                        <PagerStyle BackColor="Gainsboro" HorizontalAlign="Center" Mode="NumericPages" NextPageText="Siguiente&amp;gt;&amp;gt;"
                            PrevPageText="&amp;lt;&amp;lt;Anterior" VerticalAlign="Middle" />
                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" ForeColor="Black" Height="25px"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundColumn DataField="C1" HeaderText="#">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="20px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="C2" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="50px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="C3" HeaderText="N&#186;">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle Width="30px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="C4" HeaderText="Nombre">
                                <HeaderStyle Width="150px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="C5" HeaderText="_grpo_codigo" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="C6" HeaderText="_ver_result_final" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="C7" HeaderText="_ver_result_sgrpo" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="cmdDes" runat="server" CommandName="Desarrollar" CssClass="EstiloBoton"
                                        Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Gray"
                                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                        Width="110px">Volver a desarrollar</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="120px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Fechas Desarrolladas">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:DataGrid ID="Flex2" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                        Font-Names="Arial" Font-Size="8pt" GridLines="None"
                                        Height="50px" OnItemCommand="Flex2_VerResultados" ShowHeader="False" Width="180px">
                                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:BoundColumn DataField="C1">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="C2" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="C3" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="60px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="cmdVer" runat="server" Width="80px" Font-Underline="False" ForeColor="Gray" CssClass="EstiloBoton" onmouseover ="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" Font-Names="Arial" Font-Size="8pt">Ver Resultados</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                    <asp:Label ID="lblMensaje" runat="server" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Red"
                        Width="496px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" runat="server" style="width: 550px; height: 19px" valign="top" id="Info">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 550px">
                            <tr>
                                <td align="left" style="width: 551px; height: 43px" valign="top">
                                    <div id="Div1" runat="server" class="EstiloTitle" style="vertical-align: middle;
                                        width: 540px; height: 24px; text-align: center">
                                        Resultados</div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px; height: 19px" valign="top">
                                    <div id="lbl1" runat="server" style="width: 537px; height: 7px">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px; height: 19px" valign="top">
                                    <div id="lbl2" runat="server" style="width: 537px; height: 7px">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px" valign="top">
                                    <div id="lbl3" runat="server" style="width: 537px; height: 7px">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px; height: 38px;" valign="top" id="lbl2">
                                    <div id="lbl4" runat="server" style="width: 537px; height: 7px">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px" valign="top">
                                    <div id="lblResultado" runat="server" align="center" style="border-right: dimgray 1pt solid;
                                        border-top: dimgray 1pt solid; font-weight: bold; font-size: 10pt; background-image: none;
                                        border-left: dimgray 1pt solid; width: 100%; color: mediumblue; border-bottom: dimgray 1pt solid;
                                        font-style: italic; font-family: Verdana, 'Bookman Old Style'; height: 20px;
                                        background-color: gainsboro">
                                        100
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px" valign="top">
                                    <asp:DataGrid ID="FlexResultado" runat="server" BorderColor="DimGray" CellPadding="3"
                                        Font-Names="Arial" Font-Size="8pt" Height="100%" Width="540px">
                                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                                            HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:DataGrid></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 551px; vertical-align: middle; height: 19px; text-align: center;" valign="top">
                                    <asp:Button ID="CerrarResult" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Gray" Text="Cerrar Resultados" /></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

