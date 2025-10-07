<%@ Page Language="VB" MasterPageFile="~/Menu/PagMenu.master" AutoEventWireup="false" CodeFile="Detalle.aspx.vb" Inherits="Detalle" title="Página Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function Table1_onclick() {

}

// ]]>
</script>

    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td> 
                <td align="left" style="width: 550px; height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="width: 562px; height: 18px; font-weight: bold; font-size: 15pt; color: seagreen; font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; vertical-align: middle; text-align: center;">
                        Menu
                    </div>
                    </td>
                <td align="left" style="width: 26px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px" valign="top">
                    <img src="../Menu/Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 23px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 23px" valign="top">
                    <asp:Label ID="lblMensaje" runat="server" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Red"
                        Width="400px"></asp:Label></td>
                <td align="left" style="width: 26px; height: 23px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px" valign="top">
                    <asp:DataGrid ID="Lista" runat="server" AutoGenerateColumns="False"
                        BorderWidth="1px" CellPadding="4" ShowFooter="True" ShowHeader="False"
                        Width="540px" BackColor="White" BorderStyle="None">
                        <Columns>
                            <asp:BoundColumn DataField="C1" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="C2" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="C3" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <FooterTemplate>
                                    &nbsp;<asp:HyperLink ID="Hyperlink1" runat="server" Font-Names="Tahoma" Font-Size="9pt" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton"
                                        NavigateUrl="_Default.aspx" Style="text-align: right" Width="550px" Font-Bold="True" ForeColor="Black" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Italic="False">«« Regresar</asp:HyperLink>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="font-size: 9pt;
                                        width: 550px; font-family: Verdana; position: relative; left: 0px; border-left-color: gray; border-bottom-color: gray; border-top-color: gray; border-right-color: gray;" onclick="return Table1_onclick()">
                                        <tr>
                                            <td style="width: 550px; position: relative; height: 34px;">
                                                <div id="L1" runat="server" style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 550px; padding-top: 3px;
                                                    height: 21px; background-color: #ffcc66; font-weight: bold; font-size: 16pt; text-transform: capitalize; color: white; font-style: normal; font-family: 'Bell MT', Broadway, Arial, Serif;">
                                                    L1</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 550px; position: relative; height: 140px;" valign="top">
                                                <div id="FraImage" runat="server" style="overflow: auto; width: 550px">
                                                    <asp:Image ID="Img" runat="server" BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px"
                                                        Height="120px" /></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative; height: 14px;">
                                                <div id="L2" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 535px; padding-top: 3px; height: 20px">
                                                    L2</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative; height: 14px;">
                                                <div id="L3" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L3</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative; height: 14px;">
                                                <div id="L4" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L4</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L5" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L5</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L6" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L6</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L7" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L7</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L8" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L9</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L9" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L9</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L10" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L10</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L11" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L11</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative;">
                                                <div id="L12" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L12</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative; height: 14px;">
                                                <div id="L13" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 536px; padding-top: 3px; height: 20px">
                                                    L13</div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 550px; position: relative; background-color: gainsboro; height: 18px;">
                                                <div id="lblComent" runat="server" style="padding-right: 3px; display: inline; padding-left: 3px;
                                                    padding-bottom: 3px; width: 480px; padding-top: 3px; height: 1px; color: black;">
                                                    Comentarios: 0</div>
                                                &nbsp;
                                                <asp:ImageButton ID="BVerC" runat="server" CommandName="eVerC" ImageUrl="ArchModMenu/Iconos/ico_ver.gif"
                                                    ToolTip="Ver Comentarios" />&nbsp;
                                                <asp:ImageButton ID="BAddC" runat="server" CommandName="eAddC" ImageUrl="ArchModMenu/Iconos/ico_com.gif"
                                                    ToolTip="Comentar" style="background-color: gainsboro" /></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" />
                        <PagerStyle ForeColor="#003399" HorizontalAlign="Left" Mode="NumericPages" />
                        <ItemStyle BackColor="White" />
                        <HeaderStyle Font-Bold="True" />
                        <FooterStyle BackColor="SeaGreen" HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:DataGrid></td>
                <td align="left" style="width: 26px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

