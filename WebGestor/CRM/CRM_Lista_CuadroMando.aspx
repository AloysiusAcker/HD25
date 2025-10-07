
<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="FALSE" CodeFile="CRM_Lista_CuadroMando.aspx.vb" Inherits="CRM_CRM_Lista_CuadroMando" title="GestorPlus"  %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../Css_Gestor/FormLayout.css" rel="stylesheet" />
    <link href="../Css_Gestor/Layout.css" rel="stylesheet" />
    <link href="../Css_Gestor/Content.css" rel="stylesheet" />
    <script src="../Css_Gestor/Script.js"></script>
    
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px" id="TABLE1">
                    <tr>
                        <td align="left" style="width: 25px; height: 50px" valign="top">
                        </td>
                        <td align="left" colspan="6" style="height: 50px; text-align: center" valign="top">
                            <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                                font-size: 14pt; vertical-align: middle;color: seagreen;
                                font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; 
                                height: 1px; text-align: center">
                                Cuadro de Mando</div>
                        </td>
                        <td align="left" style="width: 25px; height: 50px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="8" style="background-image: url(/Fotos/linea.JPG); height: 11px"
                            valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 255px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 255px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 200px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td  align="left" colspan="6" style="vertical-align: middle;" valign="middle">
                            <asp:Label ID="lblError" runat="server" Text="" CssClass="EstiloLabel" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="middle">
                            <asp:Label ID="Label1" runat="server" Text="Tipo Lista" CssClass="EstiloLabel"></asp:Label>
                        </td>
                        <td align="left" style="height: 13px" valign="middle" colspan="3">
                            <asp:DropDownList ID="DdlTipoLista" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList" Height="16px">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 200px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="middle">
                            <asp:Label ID="Label2" runat="server" Text="Proceso" CssClass="EstiloLabel"></asp:Label>
                        </td>
                        <td align="left" style="width: 255px; height: 13px" valign="middle">
                            <asp:DropDownList ID="DdlProceso" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList"></asp:DropDownList>
                        </td>
                        <td align="left" style="width: 80px; height: 13px" valign="middle">
                            <asp:Label ID="lblEtiq3" runat="server" Text="Tipo Petición" CssClass="EstiloLabel" Visible="False"></asp:Label>
                        </td>
                        <td align="left" style="width: 255px; height: 13px" valign="middle">
                            <asp:DropDownList ID="DdlTipoPeticion" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList" Visible="False"></asp:DropDownList>
                        </td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 200px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="middle">
                            <asp:CheckBox ID="chkAsesor" runat="server" text="Asesor" CssClass="EstiloDropDownList" />
                        </td>
                        <td align="left" style="height: 13px" valign="middle" colspan="3">
                            <asp:DropDownList ID="DdlAsesor" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList" Enabled="False"></asp:DropDownList>
                        </td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 200px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="middle">
                            &nbsp;</td>
                        <td align="left" style="width: 255px; height: 13px" valign="middle">
                            &nbsp;</td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 255px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 200px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                        <td align="left" style="height: 20px" valign="middle" colspan="6">
                            <asp:Button ID="btnListar" runat="server" BorderColor="Gray" BorderStyle="Outset" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Listar" Width="80px" BackColor="LightGray" ForeColor="Gray" />
                        </td>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px" valign="top">
                        </td>
                        <td align="left" colspan="6" style="height: 20px" valign="middle">
                            <asp:Label id="lblCount" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Total de Registros : 0"></asp:Label>
                        </td>
                        <td align="left" style="width: 25px; height: 20px" valign="top">
                        </td>
                    </tr>     
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" colspan="3" style="height: 18px; vertical-align: top;" valign="top">
                            <div id="DivLista" runat="server" style="vertical-align: top; text-align: justify">
                                <asp:GridView ID="FlexGrafico" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">

                                    <Columns>
                                        <asp:BoundField DataField="pestado" HeaderText="Estado" />
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                    </Columns>

                                </asp:GridView>

                                <asp:Chart ID="ChartPie" runat="server" Visible="False" Width="400px">
                                    <series>
                                        <asp:Series ChartType="pie" LabelToolTip="#VALX" IsValueShownAsLabel="True" Name="VentasMes" Font="Arial, 8.25pt">
                                        </asp:Series>
                                    </series>
                                    <legends>
                                        <asp:Legend BackColor="Transparent" DockedToChartArea="ChartArea1" Docking="Bottom" Font="Trebuchet MS, 6pt, style=Bold" IsDockedInsideChartArea="False" IsEquallySpacedItems="True" IsTextAutoFit="False" ItemColumnSpacing="10" MaximumAutoSize="20" Name="Default" TableStyle="Wide" TextWrapThreshold="10" TitleFont="Aria, 6pt, style=Bold">
                                        </asp:Legend>
                                    </legends>
                                    <chartareas>
                                        <asp:ChartArea Name="ChartArea1">
                                            <area3dstyle rotation="0" />
                                            <axisy linecolor="64, 64, 64, 64">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </axisy>
                                            <axisx linecolor="64, 64, 64, 64">
                                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                                <MajorGrid LineColor="64, 64, 64, 64" />
                                            </axisx>
                                        </asp:ChartArea>
                                    </chartareas>
                                </asp:Chart>
                            </div>
                        </td>
                        <td align="left" colspan="3" style="height: 18px; vertical-align: middle;" valign="top">
                            <div>                       
                                <asp:Chart ID="chartBarras" runat="server">
                                    <Series>                       
                                        <asp:Series Name="VentasMes" LabelToolTip="#VALX" Legend="Lista De Errores" XValueType="Int32" ChartType="StackedColumn" YValuesPerPoint="6">
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
                            </div>
                        </td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>  
                    <tr>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 255px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 255px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 80px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 200px; height: 13px" valign="top"></td>
                        <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                    </tr>
                </table>
            </ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="DdlAsesor" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DdlProceso" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DdlTipoLista" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DdlTipoPeticion" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkAsesor" EventName="CheckedChanged" />
            </triggers>
        </asp:UpdatePanel>
    </div>
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img src="/Fotos/5.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
		BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>

