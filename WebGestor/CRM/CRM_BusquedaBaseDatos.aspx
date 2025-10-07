<%@ Page Title="" Language="VB"  MasterPageFile="~/PagPrincipal_A.master"  AutoEventWireup="false" CodeFile="CRM_BusquedaBaseDatos.aspx.vb" Inherits="CRM_CRM_BusquedaBaseDatos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script type="text/javascript" language="javascript">
		    var ModalProgress = '<%= ModalProgress.ClientID %>';         
        </script>

            <div style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
                    <tr>
                        <td align="left" style="width: 25px; height: 50px" valign="top">
                        </td>
                        <td align="left" colspan="7" style="height: 50px; text-align: center" valign="top">
                            <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                                font-size: 14pt; vertical-align: middle;color: seagreen;
                                font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; 
                                height: 1px; text-align: center">
                                Base de Conocimientos</div>
                        </td>
                        <td align="left" style="width: 25px; height: 50px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="9" style="background-image: url(/Fotos/linea.JPG); height: 11px"
                            valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 100px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 100px; height: 20px" valign="top"></td>
                        <td align="left" valign="top" style="width: 25px; height: 20px;"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px;" valign="top">
                        </td>
                        <td align="left" valign="top" style="height: 20px; vertical-align: middle;" colspan="2">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativos"></asp:Label></td>
                        <td align="left" valign="top" style="height: 20px; vertical-align: middle;" colspan="2">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Productos"></asp:Label></td>
                        <td align="left" valign="top" style="height: 20px; vertical-align: middle;" colspan="2">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sub-Productos"></asp:Label></td>
                        <td align="left" style="width: 100px; height: 20px" valign="top"></td>
                        <td align="left" valign="top" style="width: 25px; height: 20px;"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                        <td align="left" style="height: 20px" valign="top" colspan="2">
                            <asp:DropDownList ID="cboAplicativo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="246px" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td align="left" style="height: 20px" valign="top" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList id="cboProducto" runat="server" Width="246px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                                    </asp:DropDownList> 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>

                         </td>
                        <td align="left" style="height: 20px" valign="top" colspan="2">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList id="cboSubProducto" runat="server" Width="246px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                                    </asp:DropDownList> 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 100px; height: 20px; text-align: right;" valign="top">
                            <asp:Button id="btnListar" runat="server" Width="90px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Buscar" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></asp:Button>
                            <asp:Button id="btnTop10" onclick="btnTop10_Click" runat="server" Width="90px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Top 10" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray"></asp:Button>
                        </td>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px" valign="top">
                        </td>
                        <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Palabras a Buscar:"></asp:Label></td>
                        <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
                        </td>
                        <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Modo de Busqueda:"
                                Width="100px"></asp:Label></td>
                        <td align="left" style="width: 100px; height: 20px; text-align: right;" valign="top">
                            <asp:Button id= "BtnImprimir" runat="server" Width="90px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Imprimir" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" OnClick="BtnImprimir_Click"></asp:Button></td>
                        <td align="left" style="width: 25px; height: 20px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px" valign="top">
                        </td>
                        <td align="left" colspan="4" style="height: 20px" valign="top">
                            <asp:TextBox id="txtBuscador" runat="server" Width="580px" Height="30px" TextMode="MultiLine"></asp:TextBox> 
                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                Text="** Palabras a buscar separadas por una coma. Si no lo busca como una frase completa."
                                Width="590px"></asp:Label></td>
                        <td align="left" colspan="3" style="height: 20px; text-align: left" valign="top">
                            <asp:RadioButtonList id="optModoBus" runat="server" Width="300px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatLayout="Flow" RepeatDirection="Horizontal"><asp:ListItem Selected="True" Value="0">A ó B</asp:ListItem>
                            <asp:ListItem Value="1">A y B</asp:ListItem>
                            </asp:RadioButtonList> 
                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                Text="**A ó B: busca las palabras en cualquier consulta o solución" Width="330px"></asp:Label><asp:Label
                                    ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                Text="**A y B: busca las palabras en la misma consulta o solución" Width="330px"></asp:Label></td>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                        <td align="left" colspan="7" style="vertical-align: middle; text-align: left;" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblError1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red" Width="540px"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTop10" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboSubProducto" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                        <td align="left" colspan="7" style="vertical-align: middle; text-align: left;" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>                            
                                    <asp:Label id="lblMensaje" runat="server" Width="900px" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></asp:Label> 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTop10" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboSubProducto" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                        <td align="left" colspan="7" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">

                                        <Columns>
                                            <asp:ButtonField CommandName="Detalle" Text="Detalle">
                                            <ControlStyle CssClass="EstiloBoton" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
                                            <ItemStyle Width="800px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_CODIGO">
                                            <ItemStyle ForeColor="White" Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTop10" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cboSubProducto" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            
                        </td>
                        <td align="left" valign="top" style="width: 25px;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 10px" valign="top">
                        </td>
                        <td align="left" colspan="7" style="height: 10px" valign="top">
                            </td>
                        <td align="left" style="width: 25px; height: 10px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                        <td align="left" colspan="7" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DetailsView id="DetalleLista" runat="server" Width="950px" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="LightGray" AutoGenerateRows="False" CellPadding="4">
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
                                    <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
                                    <Fields>
                                    <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle Width="820px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle Width="820px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SUBPRODUCTO" HeaderText="Subproducto">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle Width="820px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle Width="820px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle Width="820px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle Width="820px"></ItemStyle>
                                    </asp:BoundField>
                                    </Fields>
                                    <HeaderStyle BackColor="#333333" BorderColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></EditRowStyle>
                                    </asp:DetailsView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px;" valign="top"></td>
                        <td align="left"  colspan="7" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="DetalleArchivo" runat="server" AutoGenerateColumns="False" Width="540px" Font-Size="8pt" Font-Names="Arial">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nombre del Archivo">
                                            <ItemTemplate>
                                                <div id="Doc" runat="server" style="width: 350px; height: 22px"></div>                                    
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CARCON_CODIGO" HeaderText="BC" >
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CODIGO" HeaderText="Codigo" >
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                </ContentTemplate> 
                            </asp:UpdatePanel>       
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                        <td align="left" colspan="7" style="height: 20px" valign="top">
                            <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                                Width="900px"></asp:Label></td>
                        <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 150px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 100px; height: 20px" valign="top"></td>
                        <td align="left" style="width: 100px; height: 20px" valign="top"></td>
                        <td align="left" valign="top" style="width: 25px; height: 20px;"></td>
                    </tr>
                </table>
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

