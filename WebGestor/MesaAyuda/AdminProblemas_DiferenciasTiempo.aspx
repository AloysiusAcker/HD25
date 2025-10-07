<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_DiferenciasTiempo.aspx.vb" Inherits="AdminProblemas_DiferenciasTiempo" title="Untitled Page" %>
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
                        Administracion de Problemas: Diferencia de Tiempos</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                    <asp:Button ID="BTNVER" runat="server" BackColor="DarkSlateBlue" BorderColor="SteelBlue"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="AliceBlue" Text="VER TODOS LOS PROBLEMAS REPORTADOS  Y SU ESTADO"
                        Width="311px" /></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <div id="DIV1" runat="server" style="border-right: seagreen 1px outset; border-top: seagreen 1px outset;
                        overflow: auto; border-left: seagreen 1px outset; width: 550px; border-bottom: seagreen 1px outset;
                        height: 178px">
                        <asp:DataGrid ID="Grdestadoprob" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Arial"
                            Font-Size="8pt" HorizontalAlign="Justify" Width="540px">
                            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <ItemStyle ForeColor="#000066" />
                            <HeaderStyle BackColor="#666699" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="link a detalle">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            NavigateUrl='<%# Request.ServerVariables("script_name")&amp; "?id=" &amp; DataBinder.Eval(Container.DataItem,"codigo") %>'
                                            Text='<%# DataBinder.Eval(Container.DataItem, "CODIGO") %>'>
							</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CODIGO" HeaderText="CODIGO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="USUARIO" HeaderText="USUARIO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRIORIDAD" HeaderText="PRIORIDAD"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CLASE 1" HeaderText="CLASE 1"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CLASE 2" HeaderText="CLASE 2"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIPCION" HeaderText="DESCRIPCI&#211;N"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ESTADO" HeaderText="ESTADO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA REP" HeaderText="FECHA REP."></asp:BoundColumn>
                                <asp:BoundColumn DataField="HORA REP" HeaderText="HORA REP."></asp:BoundColumn>
                                <asp:BoundColumn DataField="FECHA SOL" HeaderText="FECHA SOL."></asp:BoundColumn>
                                <asp:BoundColumn DataField="HORA SOL" HeaderText="HORA SOL."></asp:BoundColumn>
                            </Columns>
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" Mode="NumericPages"
                                VerticalAlign="Middle" />
                        </asp:DataGrid></div>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:DataGrid ID="grddetalle" runat="server" BackColor="White" BorderColor="#336666"
                        BorderStyle="Double" BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="8pt"
                        GridLines="Horizontal">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <SelectedItemStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <ItemStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

