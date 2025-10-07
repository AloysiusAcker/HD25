<%@ Page Language="VB" MasterPageFile="~/Encuesta/PagPrincipal_Encuenta.master" AutoEventWireup="false" CodeFile="Encuesta_Des.aspx.vb" Inherits="Encuesta_Des" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 51px" valign="top">
                </td>
                <td align="left" style="height: 51px" valign="top" colspan="2">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: navy; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Desarrollo de la Encuesta ....</div>
                </td>
                <td align="left" style="width: 25px; height: 51px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <div id="lblTitulo2" runat="server" style="font-weight: bold; font-size: 12pt; vertical-align: baseline;
                        width: 550px; color: navy; font-family: Arial; height: 32px; text-align: center">
                        Desarrollo de la Encuesta ....</div>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="height: 20px">
                    <div id="lbl1" runat="server" style="font-weight: bold; font-size: 8pt; width: 85px;
                        font-family: Tahoma; height: 22px; text-decoration: underline">
                        Instrucciones :</div>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="width: 30px;" valign="top">
                    <div id="lbl2" runat="server" style="font-weight: bold; font-size: 8pt; width: 21px;
                        font-family: Tahoma; height: 15px">
                        1.-</div>
                </td>
                <td align="left" style="width: 520px;" valign="top">
                    <div id="lblIns1" runat="server" style="font-weight: normal; font-size: 8pt; width: 520px;
                        font-family: Arial; height: 15px; text-align: justify">
                    </div>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 30px" valign="top">
                    <div id="lbl3" runat="server" style="font-weight: bold; font-size: 8pt; width: 24px;
                        font-family: Tahoma; height: 15px">
                        2.-</div>
                </td>
                <td align="left" style="width: 520px" valign="top">
                    <div id="lblIns2" runat="server" style="font-weight: normal; font-size: 8pt; width: 517px;
                        font-family: Tahoma; height: 15px; text-align: justify">
                    </div>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 34px;" valign="top">
                </td>
                <td align="left" style="height: 34px;" valign="top" colspan="2">
                    <div id="lblNota" runat="server" style="font-weight: normal; font-size: 8pt; width: 545px;
                        font-family: Tahoma; height: 15px; text-align: justify">
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 34px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 30px" valign="top">
                </td>
                <td align="left" style="width: 520px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
                <td align="left" style="height: 22px;" valign="top" colspan="2">
                    <asp:LinkButton ID="GuardarRptas" runat="server" Font-Names="Arial" Font-Size="9pt" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        Font-Underline="False" ForeColor="Gray" Height="22px" Width="122px" Font-Italic="True">Guardar Respuestas</asp:LinkButton><asp:HyperLink
                            ID="Cancelar" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                            ForeColor="Gray" Height="22px" NavigateUrl="encuestas.aspx" Font-Italic="True">Cancelar</asp:HyperLink></td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:DataGrid ID="Flex" runat="server" AutoGenerateColumns="False" BorderColor="DimGray"
                        BorderWidth="1px" CellPadding="3" Font-Names="Tahoma" Font-Size="8pt" Height="100px"
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
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREG_CODIGO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Contestar">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="OptRespuestas" runat="server" CellPadding="0" CellSpacing="0"
                                        Font-Names="Arial" Font-Size="8pt" Height="3px" RepeatColumns="1" RepeatDirection="Horizontal"
                                        Width="225px">
                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Contestar">
                                <ItemTemplate>
                                    <asp:CheckBoxList ID="chkRpta" runat="server" CellPadding="0" CellSpacing="0" Font-Names="Arial"
                                        Font-Size="8pt" RepeatColumns="1" Width="225px">
                                    </asp:CheckBoxList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="2" valign="top">
                    <asp:DataGrid ID="Flex222" runat="server" AutoGenerateColumns="False" BorderColor="DimGray"
                        BorderWidth="1px" CellPadding="2" Font-Size="8pt" Height="100px" HorizontalAlign="Left"
                        Width="550px">
                        <EditItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <SelectedItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <PagerStyle Visible="False" />
                        <AlternatingItemStyle BackColor="WhiteSmoke" Font-Names="Arial" HorizontalAlign="Left"
                            VerticalAlign="Middle" />
                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundColumn DataField="PREG_ORDEN" HeaderText="N&#186;">
                                <HeaderStyle Width="20px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREG_DESCRIPCION" HeaderText="Descripci&#243;n de la Pregunta">
                                <HeaderStyle Width="200px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREG_CODIGO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="R1">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo1" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R2">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo2" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R3">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo3" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R4">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo4" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R5">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo5" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R6">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo6" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R7">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo7" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R8">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo8" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R9">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo9" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="R10">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cbo10" runat="server" Font-Names="Arial" Font-Size="8pt" Height="5px"
                                        Width="100%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Width="33px" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="2" valign="top">
                    <asp:DataGrid ID="FlexLey" runat="server" AutoGenerateColumns="False" BorderColor="DimGray"
                        CellPadding="3" Font-Names="Arial" Font-Size="8pt" Height="100PX" Width="328px">
                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundColumn DataField="C1" HeaderText="Men&#250; de Valores"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px" valign="top">
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
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <div id="lblResultado" runat="server" align="center" style="border-right: dimgray 1pt solid;
                        border-top: dimgray 1pt solid; font-weight: bold; font-size: 10pt; background-image: none;
                        vertical-align: baseline; border-left: dimgray 1pt solid; width: 548px; color: mediumblue;
                        direction: ltr; text-indent: 5pt; border-bottom: dimgray 1pt solid; font-style: italic;
                        font-family: Arial; height: 20px; background-color: gainsboro">
                        <p>
                            100</p>
                    </div>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 146px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="2" style="height: 146px">
                    <asp:DataGrid ID="FlexResultado" runat="server" BorderColor="DimGray" CellPadding="3"
                        Font-Names="Arial" Font-Size="8pt" Height="100px" Width="550px">
                        <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:DataGrid></td>
                <td align="left" style="width: 25px; height: 146px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="2" valign="top">
                    <asp:LinkButton ID="GuardarRptas2" runat="server" Font-Names="Arial" Font-Size="9pt" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                       Font-Underline="False" ForeColor="Gray"  Height="22px" Width="122px" Font-Italic="True">Guardar Respuestas</asp:LinkButton><asp:HyperLink ID="Cancelar2" 
                       Font-Underline="False" ForeColor="Gray"      runat="server" Font-Names="Arial" Font-Size="9pt" Height="22px" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" NavigateUrl="encuestas.aspx" Font-Italic="True">Cancelar</asp:HyperLink></td>
                <td align="left" style="width: 25px" valign="top">
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
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial" Font-Size="9pt" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        Font-Underline="False" ForeColor="Gray" NavigateUrl="encuestas.aspx" Font-Italic="True">Continuar</asp:HyperLink></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblNroPregCont" runat="server" Height="8px" Style="z-index: 113; left: 982px;
        position: absolute; top: 370px" Visible="False" Width="64px"></asp:Label>
    <asp:Label ID="lblFormaResponder" runat="server" Height="8px" Style="z-index: 105;
        left: 936px; position: absolute; top: 202px" Visible="False" Width="64px"></asp:Label>
    <asp:Label ID="lblFormaMarcar" runat="server" Height="8px" Style="z-index: 103; left: 952px;
        position: absolute; top: 302px" Visible="False" Width="64px"></asp:Label>
    <asp:Label ID="lblTipoRptaCorrecta" runat="server" Height="24px" Style="z-index: 112;
        left: 944px; position: absolute; top: 261px" Visible="False" Width="80px"></asp:Label>
    <asp:Label ID="lblTipoRpta" runat="server" Height="8px" Style="z-index: 110; left: 949px;
        position: absolute; top: 232px" Visible="False" Width="64px"></asp:Label>
</asp:Content>

