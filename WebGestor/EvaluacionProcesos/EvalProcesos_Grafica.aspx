<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Grafica.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Grafica" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 800px;">
        
         <tr>
            <td align="left" colspan="4" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 800px ;color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Graficos Generales</div>
                </td>
         </tr>
            <tr>
                <td align="left" colspan="4" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
         <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; " colspan="2">
                <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
             </td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; width:375px;" valign="middle">
                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Año"></asp:Label>
                <asp:DropDownList ID="DdlAño" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList"></asp:DropDownList>
             </td>
            <td style="height: 19px; width: 375px;" valign="middle"></td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; width:375px;" valign="middle">
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Datos"></asp:Label>
                <asp:DropDownList ID="ddlTipoDato" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList">
                    <asp:ListItem Value="1" Selected="True">Preguntas erroneas</asp:ListItem>
                    <asp:ListItem>&lt; Seleccionar &gt;</asp:ListItem>
                </asp:DropDownList>
             </td>
            <td style="height: 19px; width: 375px;" valign="middle">                
                <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="RM"></asp:Label>
                <asp:DropDownList ID="DdlRM" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList">
                </asp:DropDownList>
            </td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; width: 375px;" valign="middle">
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Gráfica"></asp:Label>
                <asp:DropDownList ID="ddlTipoGrafica" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt">
                    <asp:ListItem Value="1" Selected="True">Barras</asp:ListItem>
                    <asp:ListItem>&lt; Seleccionar &gt;</asp:ListItem>
                </asp:DropDownList>
             </td>
            <td style="height: 19px; width: 375px;" valign="middle">             
                <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="DM"></asp:Label>
                <asp:DropDownList ID="ddlDM" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList">
                </asp:DropDownList>
            </td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; width: 375px;" valign="middle">
                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Proceso"></asp:Label>
                <asp:DropDownList ID="DdlProceso" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt">
                </asp:DropDownList>
             </td>
            <td style="height: 19px; width: 375px;" valign="middle">        
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tienda"></asp:Label>
                <asp:DropDownList ID="DdlTienda" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList">
                </asp:DropDownList>
            </td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
         <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; width:375px;" valign="middle">
                <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Top"></asp:Label>
                <asp:DropDownList ID="ddlTop" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt">
                </asp:DropDownList>
             </td>
            <td style="height: 19px; width: 375px;"></td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
        <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="width: 375px;" valign="top" >
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" class="tdchart">
                    <ContentTemplate>                        
                        <asp:Chart ID="chartBarras" runat="server" Visible="False">
                            <Series>                       
                                <asp:Series Name="VentasMes" LabelToolTip="#VALX" Legend="Lista De Errores" XValueType="Int32">
                                </asp:Series>  
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="SellThruChartArea">
                                    <AxisY>
                                        <MajorGrid LineColor="White" />
                                    </AxisY>
                                    <AxisX TitleFont="Arial, 8.25pt">
                                        <MajorGrid LineColor="White" />
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <div id="Leyenda" runat="server" style="text-align: left; vertical-align: top;">
                            <asp:Repeater ID="repLeyenda" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblColor" runat="server" BackColor="Red" Font-Size="8pt" Font-Names="Arial" />
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                        <asp:Label ID="lblCantidad" runat="server" Font-Names="Arial" Font-Size="8pt"  />
                                        <br />
                                    </ItemTemplate>
                             </asp:Repeater>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="height: 19px; width: 375px;">
                        <asp:Chart ID="ChartPie" runat="server" Visible="False">
                            <series>
                                <asp:Series ChartType="pie" Name="VentasMes" IsVisibleInLegend="False">
                                </asp:Series>
                            </series>
                           <Legends>
                                <asp:Legend TitleFont="Aria, 6pt, style=Bold" BackColor="Transparent" IsEquallySpacedItems="True" Font="Trebuchet MS, 6pt, style=Bold" IsTextAutoFit="False" Name="Default" DockedToChartArea="ChartArea1" Docking="Bottom" IsDockedInsideChartArea="False" ItemColumnSpacing="10" TableStyle="Wide" TextWrapThreshold="10" MaximumAutoSize="20"></asp:Legend>
                            </Legends>
                            <chartareas>
                                <asp:ChartArea Name="ChartArea1">
                                    <area3dstyle Rotation="0" ></area3dstyle>
									<axisy LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisy>
									<axisx LineColor="64, 64, 64, 64">
										<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
										<MajorGrid LineColor="64, 64, 64, 64" />
									</axisx>
                                </asp:ChartArea>
                            </chartareas>
                        </asp:Chart>

                        <asp:Chart ID="chartLinea" runat="server" Visible="False" style="margin-bottom: 0px" >
                            <Series>           
                                <asp:Series Name="VentasMes" ChartType="Line" YValuesPerPoint="4" >
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
                
                        <div id="Div1" runat="server" style="text-align: left; vertical-align: top;">
                            <asp:Repeater ID="RepLeyenda2" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblColor" runat="server" BackColor="Red" Font-Size="8pt" Font-Names="Arial" />
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                        <asp:Label ID="lblCantidad" runat="server" Font-Names="Arial" Font-Size="8pt"  />
                                        <br />
                                    </ItemTemplate>
                             </asp:Repeater>
                        </div>
                    </td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
        <tr>
            <td style="height: 19px; width: 25px;"></td>
            <td style="height: 19px; " colspan="2">
            </td>
            <td style="height: 19px; width: 25px;"></td>
        </tr>
    </table>
</asp:Content>

