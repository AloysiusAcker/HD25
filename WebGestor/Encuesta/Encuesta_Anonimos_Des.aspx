<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Encuesta_Anonimos_Des.aspx.vb" Inherits="Encuesta_Anonimos_Des" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="height: 50px" valign="top" colspan="2">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; vertical-align: middle; width: 550px; color: navy; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Desarrollo de la Encuesta ....</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 45px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="vertical-align: middle; height: 45px; text-align: center">
                    <div id="lblTitulo2" runat="server" style="font-weight: bold; font-size: 12pt; vertical-align: baseline;
                        width: 550px; color: navy; font-family: Arial; height: 19px; text-align: center; left: 345px; position: static; top: 345px;">
                        Desarrollo de la Encuesta ....</div>
                </td>
                <td align="left" style="width: 25px; height: 45px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="height: 16px">
                    <div id="lbl1" runat="server" style="font-weight: bold; font-size: 8pt; width: 85px;
                        font-family: Tahoma; height: 18px; text-decoration: underline">
                        Instrucciones :</div>
                </td>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 16px" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 16px" valign="top">
                    <div id="lbl2" runat="server" style="font-weight: bold; font-size: 8pt; width: 21px;
                        font-family: Tahoma; height: 13px">
                        1.-</div>
                </td>
                <td align="left" style="width: 520px; height: 16px" valign="top">
                    <div id="lblIns1" runat="server" style="font-weight: normal; font-size: 8pt; width: 520px;
                        font-family: Arial; height: 15px; text-align: justify">
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 16px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 16px;" valign="top">
                    <div id="lbl3" runat="server" style="font-weight: bold; font-size: 8pt; width: 24px;
                        font-family: Tahoma; height: 15px">
                        2.-</div>
                </td>
                <td align="left" style="width: 520px; height: 16px;" valign="top">
                    <div id="lblIns2" runat="server" style="font-weight: normal; font-size: 8pt; width: 517px;
                        font-family: Tahoma; height: 15px; text-align: justify">
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="height: 16px">
                    <div id="lblNota" runat="server" style="font-weight: normal; font-size: 8pt; width: 545px;
                        font-family: Tahoma; height: 15px; text-align: justify; display: inline;">
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="height: 20px">
                    <asp:LinkButton ID="GuardarRptas" runat="server" Font-Names="Arial" Font-Size="9pt" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        Font-Underline="False" ForeColor="Gray" Height="22px" Width="122px" Font-Italic="True">Guardar Respuestas</asp:LinkButton><asp:HyperLink
                            ID="Cancelar" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"  onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                            ForeColor="Gray" Height="22px" NavigateUrl="encuestas_anonimos.aspx" Font-Italic="True">Cancelar</asp:HyperLink></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="height: 19px;" valign="top" colspan="2">
                    <asp:DataGrid ID="Flex" runat="server" AutoGenerateColumns="False" BorderColor="DimGray"
                        BorderWidth="1px" CellPadding="3" Font-Names="Arial" Font-Size="8pt" Height="100px"
                        HorizontalAlign="Left" Width="550px">
                        <EditItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <SelectedItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <PagerStyle Mode="NumericPages" Visible="False" />
                        <AlternatingItemStyle BackColor="WhiteSmoke" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundColumn DataField="PREG_ORDEN" HeaderText="N&#186;">
                                <HeaderStyle Width="20px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREG_DESCRIPCION" HeaderText="Descripci&#243;n de la Pregunta">
                                <HeaderStyle Width="450px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREG_CODIGO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Contestar">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="OptRespuestas" runat="server" CellPadding="0" CellSpacing="0"
                                        Font-Names="Tahoma" Font-Size="8pt" Height="21px" RepeatColumns="1" RepeatDirection="Horizontal"
                                        Width="225px">
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:DataGrid ID="Flex1" runat="server" AutoGenerateColumns="False" BorderColor="DimGray"
                        CellPadding="3" Font-Names="Arial" Font-Size="8pt" Height="100px" HorizontalAlign="Left"
                        Width="550px">
                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                            ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundColumn DataField="PREG_CODIGO" HeaderText="AAA" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Contestar">
                                <ItemTemplate>
                                    <asp:Label ID="lblPregunta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                        Height="2px" Width="495px"></asp:Label>
                                    <asp:Label ID="Preg" runat="server" Font-Names="Arial" Font-Size="8pt" Height="3px"
                                        Width="8px"></asp:Label>
                                    <asp:RadioButtonList ID="OptRespuestas1" runat="server" Font-Names="Tahoma" Font-Size="8pt"
                                        ForeColor="Black" Height="26px" RepeatColumns="1" Width="527px">
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                                <HeaderStyle Width="550px" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="height: 19px;" valign="top" colspan="2">
                    <div id="lblResultado" runat="server" align="center" style="border-right: dimgray 1pt solid;
                        border-top: dimgray 1pt solid; font-weight: bold; font-size: 10pt; background-image: none;
                        vertical-align: baseline; border-left: dimgray 1pt solid; width: 548px; color: mediumblue;
                        direction: ltr; text-indent: 5pt; border-bottom: dimgray 1pt solid; font-style: italic;
                        font-family: Arial; height: 20px; background-color: gainsboro">
                        <p>
                            100</p>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="height: 19px;" valign="top" colspan="2">
                    <asp:DataGrid ID="FlexResultado" runat="server" BorderColor="DimGray" CellPadding="3"
                        Font-Names="Arial" Font-Size="8pt" Height="100px" Width="550px">
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:Label ID="lblMensaje2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="DarkSlateGray" Height="8px" Width="541px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"
                        Width="540px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="height: 19px">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial" Font-Size="9pt"  onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        Font-Underline="False" ForeColor="Gray" NavigateUrl="encuestas_anonimos.aspx" Font-Italic="True">Continuar</asp:HyperLink></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblNroPregCont" runat="server" Height="8px" Style="z-index: 113; left: 544px;
        position: absolute; top: 328px" Visible="False" Width="64px"></asp:Label>
    <asp:Label ID="lblFormaResponder" runat="server" Height="8px" Style="z-index: 105;
        left: 288px; position: absolute; top: 320px" Visible="False" Width="64px"></asp:Label>
    <asp:Label ID="lblFormaMarcar" runat="server" Height="8px" Style="z-index: 103; left: 224px;
        position: absolute; top: 328px" Visible="False" Width="64px"></asp:Label>
    <asp:Label ID="lblTipoRptaCorrecta" runat="server" Height="24px" Style="z-index: 112;
        left: 416px; position: absolute; top: 328px" Visible="False" Width="80px"></asp:Label>
    <asp:Label ID="lblTipoRpta" runat="server" Height="8px" Style="z-index: 110; left: 656px;
        position: absolute; top: 328px" Visible="False" Width="64px"></asp:Label>
</asp:Content>

