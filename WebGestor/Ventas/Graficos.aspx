<%@ Page Language="VB" MasterPageFile="~/Ventas/PagPrincipal_Nuevo.master" AutoEventWireup="false" CodeFile="Graficos.aspx.vb" Inherits="Graficos" title="Graficos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px;">
        
         <tr>
            <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Gráficos Generales</div>
                </td>
         </tr>
         <tr>
            <td style="height: 19px; " colspan="2">
                <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
             </td>
            <td style="height: 19px; width: 250px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 100px;">
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Datos"></asp:Label>
             </td>
            <td style="height: 19px; width: 350px;">
                <asp:DropDownList ID="ddlTipoDato" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt">
                    <asp:ListItem Value="1">Ventas por Mes</asp:ListItem>
                    <asp:ListItem Value="2">Ventas por Productos</asp:ListItem>
                    <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                </asp:DropDownList>
             </td>
            <td style="height: 19px; width: 250px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 100px;">
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Gráfica"></asp:Label>
             </td>
            <td style="height: 19px; width: 350px;">
                <asp:DropDownList ID="ddlTipoGrafica" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt">
                    <asp:ListItem Value="1">Pie</asp:ListItem>
                    <asp:ListItem Value="2">Barras</asp:ListItem>
                    <asp:ListItem Value="3">Lineas</asp:ListItem>
                    <asp:ListItem Value="4">Area</asp:ListItem>
                    <asp:ListItem Value="5">Barras Multiples</asp:ListItem>
                    <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                </asp:DropDownList>
             </td>
            <td style="height: 19px; width: 250px;"></td>
        </tr>
        <tr>
            <td style="width: 100px;"></td>
            <td colspan="2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" class="tdchart">
                    <ContentTemplate>

                        
                        <asp:Chart ID="chartBarras2" runat="server" Visible="False" >
                            <Series>                       
                                <asp:Series Name="VentasMes" ChartType="Column" >
                                </asp:Series>           
                                <asp:Series Name="VentasMes2" ChartType="Column">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="SellThruChartArea">
                                    <AxisY>
                                        <MajorGrid LineColor="White" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid LineColor="White" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>

                        </asp:Chart>
                        <asp:Chart ID="chartBarras" runat="server" Visible="False" >
                            <Series>                       
                                <asp:Series Name="VentasMes" ChartType="Column" >
                                </asp:Series>  
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="SellThruChartArea">
                                    <AxisY>
                                        <MajorGrid LineColor="White" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid LineColor="White" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>

                        </asp:Chart>

                        <asp:Chart ID="chartLinea" runat="server" Visible="False" style="margin-bottom: 0px" >
                            <Series>           
                                <asp:Series Name="VentasMes" ChartType="Line" >
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="SellThruChartArea" >
                                    <AxisY>
                                        <MajorGrid LineColor="White" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid LineColor="White" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>

                        </asp:Chart>
                        
                        <asp:Chart ID="ChartPie" runat="server" Visible="False" >
                            <series>
                                <asp:Series Name="VentasMes" ChartType="pie"></asp:Series>
                                <%--Label="#VAL{N}" LabelToolTip="#VALX" LegendText="#VALX" LegendToolTip="#VAL{N}"--%>
                            </series>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </chartareas>
                        </asp:Chart>
                        <asp:Chart ID="ChartArea" runat="server">
                            <Series>
                                <asp:Series ChartType="Area" Name="VentasMes" >
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                    <AxisY>
                                        <MajorGrid LineColor="White" />
                                    </AxisY>
                                    <AxisX>
                                        <MajorGrid LineColor="White" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="height: 19px; width: 100px;"></td>
            <td style="height: 19px; width: 350px;">
                <asp:Repeater ID="repLeyenda" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="lblColor" runat="server" BackColor="Red" />
                            <asp:Label ID="lblDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt" />
                            <asp:Label ID="lblCantidad" runat="server" Font-Names="Arial" Font-Size="8pt"  />
                            <br />
                        </ItemTemplate>
                 </asp:Repeater>
            </td>
            <td style="height: 19px; width: 250px;"></td>
        </tr>
    </table>

</asp:Content>