<%@ Page Language="VB" MasterPageFile="~/Encuesta/PagPrincipal_Encuenta.master" AutoEventWireup="false" CodeFile="Encuestas.aspx.vb" Inherits="Encuestas" title="Untitled Page" %>
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
                        Lista de Encuestas
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
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px;" valign="top">
              <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:DataGrid ID="Tabla" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BorderColor="DimGray" BorderWidth="1px" CellPadding="3" Font-Names="Arial" Font-Size="8pt"
                        Height="100px" HorizontalAlign="Left" OnPageIndexChanged="Tabla_Page" Width="550px">
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
                                <HeaderStyle Width="300px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="C5" HeaderText="_tipo_rpta" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Desarrollar" runat="server" CommandName="Desarrollar" CssClass="EstiloBoton"
                                        Font-Underline="False" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                        onmouseover="this.style.fontWeight='bolder'" Width="80px">Desarrollar</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Ver" runat="server" CommandName="VerResultados" CssClass="EstiloBoton"
                                        Font-Underline="False" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                        onmouseover="this.style.fontWeight='bolder'" Width="100px">Ver Resultados</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="150px" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:Label ID="lblMensaje" runat="server" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Red"
                        Visible="False" Width="528px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:Label ID="lblMensaje2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt"
                        ForeColor="DarkSlateGray" Width="528px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
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

