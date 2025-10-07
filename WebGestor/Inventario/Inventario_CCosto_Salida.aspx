<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_CCosto_Salida.aspx.vb" Inherits="Inventario_CCosto_Salida" title="GestorPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="10" style="height: 50px; text-align: center" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Nueva Salida de Centro de Costos</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="12" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="N° Salida" CssClass="EstiloLabel" Font-Overline="False"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel16" runat="server">
                        <contenttemplate>
<asp:TextBox id="lblCodigo" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" BackColor="WhiteSmoke" ReadOnly="True" __designer:wfdid="w10"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="3">
                    <asp:UpdatePanel id="UpdatePanel17" runat="server">
                        <contenttemplate>
<asp:TextBox id="lblCodOrigen" runat="server" Width="1px" Height="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="txtAlmacen" runat="server" Width="8px" Height="1px" Visible="False"></asp:TextBox> <asp:TextBox id="txtNomAlmacen" runat="server" Width="8px" Height="8px" Visible="False"></asp:TextBox> <asp:TextBox id="lblCodDestino" runat="server" Width="8px" Height="8px" Visible="False"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px;" valign="top">
                    &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="_Grabar" runat="server" CssClass="EstiloBoton_Ac" EnableTheming="True"
                        onmouseout="this.style.fontWeight='bolder'" onmouseover="this.style.fontWeight='normal'"
                        Text="Grabar" Width="90px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Origen"></asp:Label></td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 330px">
                        <tr>
                            <td align="left" style="width: 60px" valign="top">
                                <asp:UpdatePanel id="UpdatePanel9" runat="server">
                                    <contenttemplate>
<asp:TextBox id="txtOrigCodExt" tabIndex=-1 runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" BackColor="WhiteSmoke" ReadOnly="True" __designer:wfdid="w235"></asp:TextBox> 
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel></td>
                            <td align="left" style="width: 30px; text-align: center" valign="top">
                    <asp:Button ID="_Ubica1" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='bolder'"
                        onmouseover="this.style.fontWeight='normal'" Text="..." ToolTip="Busqueda" Width="25px" CssClass="EstiloBoton_Ac" /></td>
                            <td align="left" style="width: 240px" valign="top">
                                <asp:UpdatePanel id="UpdatePanel10" runat="server">
                                    <contenttemplate>
<asp:TextBox id="txtOrigDescrip" tabIndex=-1 runat="server" Width="230px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" BackColor="WhiteSmoke" ReadOnly="True" __designer:wfdid="w236"></asp:TextBox> 
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Destino"></asp:Label></td>
                <td align="left" colspan="9" style="vertical-align: text-top; height: 22px" valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px">
                            <tr>
                                <td align="left" style="width: 60px; height: 19px" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel11" runat="server">
                                        <contenttemplate>
<asp:TextBox id="txtDesCodExterno" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" BackColor="WhiteSmoke" ReadOnly="True" __designer:wfdid="w267"></asp:TextBox> 
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel></td>
                                <td align="left" style="width: 30px; height: 19px; text-align: center" valign="top">
                                    <asp:Button ID="_Ubica2" runat="server" CssClass="EstiloBoton_Ac" EnableTheming="True"
                                        onmouseout="this.style.fontWeight='bolder'" onmouseover="this.style.fontWeight='normal'"
                                        Text="..." ToolTip="Busqueda" Width="25px" /></td>
                                <td align="left" style="width: 240px; height: 19px" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel12" runat="server">
                                        <contenttemplate>
<asp:TextBox id="txtDesDescrip" runat="server" Width="229px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" BackColor="WhiteSmoke" ReadOnly="True" __designer:wfdid="w270"></asp:TextBox> 
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel></td>
                                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel18" runat="server">
                        <contenttemplate>
<asp:RadioButtonList id="OptDestino" runat="server" Width="160px" Font-Size="8pt" Font-Names="Arial" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="True" __designer:wfdid="w273">
                        <asp:ListItem Selected="True" Value="1">Almac&#233;n</asp:ListItem>
                        <asp:ListItem Value="2">Centro Costo</asp:ListItem>
                    </asp:RadioButtonList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: text-top; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fec./Hora"></asp:Label></td>
                <td align="left" colspan="9" style="vertical-align: text-top; height: 22px" valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 500px">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 65px; height: 22px" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel22" runat="server">
                                        <contenttemplate>
                    <asp:TextBox ID="txtFecha" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                        Font-Size="8pt" MaxLength="10" ToolTip="Formato Fecha : dd/mm/aaaa" Width="60px"></asp:TextBox>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="dtpFecha" EventName="SelectionChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="left" style="vertical-align: middle; width: 55px; height: 22px" valign="top">
                    <asp:TextBox ID="txtHora" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                        Font-Size="8pt" MaxLength="5" ToolTip="Formato Hora : 24 hrs" Width="48px"></asp:TextBox></td>
                                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: center"
                                    valign="top">
                    <asp:Button ID="_Ubica3" runat="server" CssClass="EstiloBoton_Ac" EnableTheming="True"
                        onmouseout="this.style.fontWeight='bolder'" onmouseover="this.style.fontWeight='normal'"
                        Text="..." ToolTip="Busqueda" Width="25px" /></td>
                                <td align="left" style="vertical-align: middle; width: 55px; height: 22px" valign="top">
                    <asp:Label ID="lblFechaDevol" runat="server" Font-Bold="False" Font-Names="Arial"
                        Font-Size="8pt" Text="Fec. Dev." Width="48px"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 65px; height: 22px" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel21" runat="server">
                                        <contenttemplate>
                    <asp:TextBox ID="txtFechaDevol" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Arial" Font-Size="8pt" MaxLength="10" ToolTip="Formato dd/mm/aaaa"
                        Width="60px"></asp:TextBox>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="dtpFechaFin" EventName="SelectionChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel></td>
                                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: center"
                                    valign="top">
                    <asp:Button ID="_Ubica4" runat="server" CssClass="EstiloBoton_Ac" EnableTheming="True"
                        onmouseout="this.style.fontWeight='bolder'" onmouseover="this.style.fontWeight='normal'"
                        Text="..." ToolTip="Busqueda" Width="25px" /></td>
                                <td align="left" style="vertical-align: middle; width: 200px; height: 22px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel19" runat="server">
                                        <contenttemplate>
<DIV style="LEFT: 304px; VERTICAL-ALIGN: text-top; WIDTH: 150px; POSITION: static; TOP: 456px; TEXT-ALIGN: left" id="lblFecIni" align=center runat="server" visible="false"><asp:Calendar id="dtpFecha" runat="server" Width="150px" Height="100px" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderWidth="1px" BorderStyle="Solid" BorderColor="OliveDrab" BackColor="White" NextPrevFormat="ShortMonth" EnableViewState="False" OnSelectionChanged="dtpFecha_SelectionChanged">
<SelectedDayStyle BackColor="#333399" ForeColor="White"></SelectedDayStyle>

<TodayDayStyle BackColor="White" BorderColor="OliveDrab" BorderWidth="1px" BorderStyle="Solid" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Font-Underline="False"></TodayDayStyle>

<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>

<NextPrevStyle VerticalAlign="Bottom" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="White"></NextPrevStyle>

<DayHeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></DayHeaderStyle>

<TitleStyle BackColor="OliveDrab" BorderColor="White" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="White"></TitleStyle>
</asp:Calendar> &nbsp;</DIV>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Ubica3" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="dtpFecha" EventName="SelectionChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel></td>
                                <td align="left" colspan="3" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel20" runat="server">
                                        <contenttemplate>
<DIV style="LEFT: 8px; WIDTH: 150px; POSITION: static; TOP: 8px" id="lblFechafin" align=center runat="server" visible="false"><asp:Calendar id="dtpFechaFin" runat="server" Width="150px" Height="100px" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderWidth="1px" BorderStyle="Solid" BorderColor="OliveDrab" BackColor="White" NextPrevFormat="ShortMonth" EnableViewState="False" OnSelectionChanged="dtpFechaFin_SelectionChanged">
<SelectedDayStyle BackColor="#333399" ForeColor="White"></SelectedDayStyle>

<TodayDayStyle BackColor="White" BorderColor="OliveDrab" BorderWidth="1px" BorderStyle="Solid" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Font-Underline="False"></TodayDayStyle>

<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>

<NextPrevStyle VerticalAlign="Bottom" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="White"></NextPrevStyle>

<DayHeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></DayHeaderStyle>

<TitleStyle BackColor="OliveDrab" BorderColor="White" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="White"></TitleStyle>
</asp:Calendar> &nbsp;</DIV>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Ubica4" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="dtpFechaFin" EventName="SelectionChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Motivo"></asp:Label></td>
                <td align="left" colspan="8" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel15" runat="server">
                        <contenttemplate>
                    <asp:DropDownList ID="cboMotivo" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="288px">
                    </asp:DropDownList>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="10" style="vertical-align: middle;" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:Label id="lblError" runat="server" Height="8px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w239"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_BuscarEq" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_BuscarAc" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Ubica2" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="10" style="vertical-align: middle;" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblMensaje" runat="server" Height="8px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" colspan="10" style="vertical-align: middle; height: 5px" valign="top">
                    <asp:Label ID="lbl10" runat="server" CssClass="EstiloLabel" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Maroon" Text="Equipos" Width="48px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 280px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 280px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; LEFT: 0px; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 270px; BORDER-BOTTOM: gray 1px outset; POSITION: static; TOP: 0px; HEIGHT: 280px" id="MarcoEq" align=left runat="server">
    <asp:GridView id="_DetalleEq" runat="server" Width="100%" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" CellPadding="2" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="QuitarFila" Text="&gt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Font-Names="Arial" Font-Size="8pt" Width="18px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Equipo"></asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NRO" HeaderText="N&#186; Serie">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLACA_NRO" HeaderText="N&#186; Placa">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NUMERAR" ShowHeader="False">
<ItemStyle ForeColor="White"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Funci&#243;n"><ItemTemplate>
                                        <asp:DropDownList ID="cboFuncion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="140px">
                                        </asp:DropDownList>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="COD_FUNCION" Visible="False"></asp:BoundField>
<asp:BoundField DataField="REEN_NUMERO" Visible="False"></asp:BoundField>
<asp:BoundField DataField="AVERIA" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Falla Aver&#237;a"><ItemTemplate>
                                        <asp:DropDownList ID="cboAveria" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="140px">
                                        </asp:DropDownList>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="COD_FALLA" Visible="False"></asp:BoundField>
<asp:TemplateField HeaderText="Detalle de la Aver&#237;a"><ItemTemplate>
                                        <asp:TextBox ID="txtDetAveria" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="Arial" Font-Size="8pt" MaxLength="150" Width="140px"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
</Columns>

<SelectedRowStyle BackColor="Cyan"></SelectedRowStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_BusEq" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_DetalleEq" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" colspan="5" style="height: 280px" valign="top">
                    <table id="tbBusEq" runat="server" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
                        border-top: gray 1px outset; border-left: gray 1px outset; width: 280px; border-bottom: gray 1px outset; height: 280px;">
                        <tr>
                            <td style="position: relative; height: 22px; vertical-align: middle;" valign="middle" colspan="2">
                                &nbsp;<asp:Label ID="lbl12" runat="server" CssClass="EstiloLabel" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" Text="Busqueda"></asp:Label></td>
                            <td style="width: 50px; position: relative; height: 22px" valign="middle">
                            </td>
                            <td style="width: 90px; position: relative; height: 22px" valign="middle">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px; position: relative; height: 22px" valign="middle">
                                &nbsp;<asp:Label ID="lbl6" runat="server" CssClass="EstiloLabel" Font-Names="Arial"
                                    Font-Size="8pt" Text="Código"></asp:Label></td>
                            <td align="left" style="width: 90px; position: relative; height: 22px;" valign="middle">
                                <asp:TextBox ID="txtCodigoArt" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Arial" Font-Size="8pt" MaxLength="10" Width="70px"></asp:TextBox></td>
                            <td style="width: 50px; position: relative; height: 22px;" valign="middle">
                                <asp:Label ID="lbl8" runat="server" CssClass="EstiloLabel" Font-Names="Arial" Font-Size="8pt"
                                    Text="Serie"></asp:Label></td>
                            <td style="width: 90px; position: relative; height: 22px;" valign="middle">
                                <asp:TextBox ID="txtSerieArt" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Arial" Font-Size="8pt" MaxLength="30" Width="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 50px; position: relative; height: 22px" valign="middle">
                                &nbsp;<asp:Label ID="lbl7" runat="server" CssClass="EstiloLabel" Font-Names="Arial"
                                    Font-Size="8pt" Text="Descrip."></asp:Label></td>
                            <td align="left" style="width: 90px; position: relative; height: 22px;" valign="middle">
                                <asp:TextBox ID="txtNomArt" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Arial" Font-Size="8pt" MaxLength="30" Width="70px"></asp:TextBox></td>
                            <td style="width: 50px; position: relative; height: 22px;" valign="middle">
                                <asp:Label ID="lbl9" runat="server" CssClass="EstiloLabel" Font-Names="Arial" Font-Size="8pt"
                                    Text="Placa"></asp:Label></td>
                            <td style="width: 90px; position: relative; height: 22px;" valign="middle">
                                <asp:TextBox ID="txtPlaca" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="30" Width="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="position: relative; vertical-align: middle; height: 22px;" valign="middle">
                                &nbsp;<asp:Label ID="lbl11" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                    Height="8px" Width="14px" CssClass="EstiloLabel" Font-Bold="False">-</asp:Label>
                                <asp:Label ID="lblCountBusEq" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                    Height="8px" Width="140px" CssClass="EstiloLabel" Font-Bold="False"></asp:Label></td>
                            <td style="width: 90px; position: relative; vertical-align: middle; height: 22px;" valign="middle">
                                <asp:Button ID="_BuscarEq" runat="server" CssClass="EstiloBoton_Ac" EnableTheming="True"
                                    onmouseout="this.style.fontWeight='bolder'" onmouseover="this.style.fontWeight='normal'"
                                    Text="Buscar" Width="73px" /></td>
                        </tr>
                        <tr>
                            <td style="position: relative; height: 192px" valign="middle" colspan="4">
                                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                    <contenttemplate>
<div style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: silver; LEFT: -374px; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: silver; OVERFLOW: auto; WIDTH: 280px; BORDER-TOP-COLOR: silver; TOP: 24px; HEIGHT: 192px; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: silver" id="MarcoBusEq" align=left runat="server"><asp:GridView id="_BusEq" runat="server" Width="100%" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" CellPadding="2" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="AgregarFila" Text="&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Font-Names="Arial" Font-Size="8pt" Width="18px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Equipo">
<HeaderStyle Font-Bold="True"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NRO" HeaderText="N&#186; Serie">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLACA_NRO" HeaderText="N&#186; Placa">
<HeaderStyle Wrap="True"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NUMERAR">
<ItemStyle ForeColor="White"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="REEN_NUMERO"></asp:BoundField>
<asp:BoundField DataField="AVERIA"></asp:BoundField>
</Columns>

<SelectedRowStyle BackColor="Cyan"></SelectedRowStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="False" Font-Underline="False"></HeaderStyle>
</asp:GridView> </div>
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="_BuscarEq" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 25px; height: 280px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle;" valign="top">
                    <asp:Label ID="lbl14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Maroon" Text="Accesorios" Visible="False"></asp:Label></td>
                <td align="left" colspan="5" valign="top">
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="5" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
<DIV style="border: 1px outset gray; LEFT: 1x; OVERFLOW: auto; 
 WIDTH: 270px; POSITION: static; 
 TOP: 48px; height: 280px;" id="MarcoAc" align=left runat="server" >
    <asp:GridView id="_DetalleAc" runat="server" Width="100%" Font-Size="8pt" Font-Names="Arial" BorderStyle="None" 
        CellPadding="2" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="QuitarFila" Text="&gt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Font-Names="Arial" Font-Size="8pt" Width="18px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Accesorio"></asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock Actual">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Q Salida"><ItemTemplate>
                                                    <asp:TextBox ID="txtCantSal" runat="server" Width="40px" Font-Names="Arial" Font-Size="8pt" MaxLength="4" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                                
</ItemTemplate>

<ItemStyle Wrap="False"></ItemStyle>
</asp:TemplateField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_BusAc" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_DetalleAc" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" colspan="5" valign="top">
                    <table id="tbBusAc" runat="server" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
                        border-top: gray 1px outset; border-left: gray 1px outset; width: 280px; border-bottom: gray 1px outset;">
                        <tr>
                            <td colspan="4" style="position: relative;" valign="middle">
                                <asp:Label ID="lbl13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Maroon" Text="Busqueda"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 50px; position: relative; height: 22px" valign="middle">
                                &nbsp;<asp:Label ID="lbl15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                            <td align="left" style="width: 90px; position: relative; height: 22px" valign="middle">
                                <asp:TextBox ID="txtCodigoAc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Arial" Font-Size="8pt" MaxLength="10" Width="70px"></asp:TextBox>
                            </td>
                            <td style="width: 50px; position: relative; height: 22px" valign="middle">
                                <asp:Label ID="lbl16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descrip."></asp:Label></td>
                            <td style="width: 90px; position: relative; height: 22px" valign="middle">
                                <asp:TextBox ID="txtNomAc" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial"
                                    Font-Size="8pt" MaxLength="30" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="position: relative;" valign="middle">
                                &nbsp;<asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                    Height="8px" Width="14px">-</asp:Label>
                                <asp:Label ID="lblCountBusAc" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                    Height="8px" Width="120px"></asp:Label></td>
                            <td style="width: 90px; position: relative;" valign="middle">
                                <asp:Button ID="_BuscarAc" runat="server" CssClass="EstiloBoton_Ac" EnableTheming="True"
                                    onmouseout="this.style.fontWeight='bolder'" onmouseover="this.style.fontWeight='normal'"
                                    Text="Buscar" Width="73px" /></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="position: relative; height: 214px" valign="middle">
                                <asp:UpdatePanel id="UpdatePanel3" runat="server">
                                    <contenttemplate>
<DIV style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: silver; LEFT: -6px; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: silver; OVERFLOW: auto; WIDTH: 280px; BORDER-TOP-COLOR: silver; POSITION:relative; TOP: 0px; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: silver" id="MarcoBusAc" align=left runat="server"><asp:GridView id="_BusAc" runat="server" Width="100%" Font-Size="8pt" Font-Names="Arial" BorderStyle="None" CellPadding="2" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="AgregarFila" Text="&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Font-Names="Arial" Font-Size="8pt" Width="18px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Accesorio">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock Actual">
<HeaderStyle Wrap="False"></HeaderStyle>

<ItemStyle Wrap="False"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</contenttemplate>
                                    <triggers>
<asp:AsyncPostBackTrigger ControlID="_BuscarAc" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" colspan="10" style="vertical-align: middle; height: 5px" valign="top">
                    &nbsp; &nbsp;
                </td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Envia Sal."
                        Width="48px"></asp:Label></td>
                <td align="left" colspan="9" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel13" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtPerEnvia" runat="server" Width="480px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" MaxLength="100"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: text-top; width: 50px; height: 22px" valign="top">
                    <asp:Label ID="lbl18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Obs."
                        Width="40px"></asp:Label></td>
                <td align="left" colspan="9" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel14" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtObs" runat="server" Width="480px" Height="29px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Solid" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px;" valign="top">
                    <asp:Label ID="lbl19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Doc. Sal."
                        Width="48px" Visible="False"></asp:Label></td>
                <td align="left" colspan="9" style="vertical-align: middle;" valign="top">
                    <asp:RadioButtonList ID="OptDocSalida" runat="server" Font-Names="Arial" Font-Size="8pt"
                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="176px" Visible="False">
                        <asp:ListItem Selected="True" Value="1">Gu&#237;a Remisi&#243;n</asp:ListItem>
                        <asp:ListItem Value="2">Gu&#237;a Interna</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 23px" valign="top">
                </td>
                <td align="left" colspan="10" style="vertical-align: middle; height: 23px" valign="top">
                    <asp:Label ID="lbl21" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Registro"
                        Width="80px"></asp:Label>
                    <asp:TextBox ID="lblFecha" runat="server" AutoPostBack="True" BackColor="WhiteSmoke"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                        TabIndex="-1" Width="60px"></asp:TextBox>
                    <asp:TextBox ID="lblHora" runat="server" AutoPostBack="True" BackColor="WhiteSmoke"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                        TabIndex="-1" Width="60px"></asp:TextBox>
                    <asp:Label ID="lbl20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="U. Registra"></asp:Label>
                    <asp:TextBox ID="lblUsuario" runat="server" AutoPostBack="True" BackColor="WhiteSmoke"
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                        TabIndex="-1" Width="256px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 23px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 6px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 6px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 6px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel id="UpdatePanel7" runat="server">
        <contenttemplate>
<DIV style="LEFT: 320px; WIDTH: 100px; POSITION: absolute; TOP: 320px; HEIGHT: 100px" id="lblBusCentroCosto" runat="server" visible="false"><TABLE style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; BORDER-LEFT: gray 2px outset; WIDTH: 500px; BORDER-BOTTOM: gray 2px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 30px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 30px; TEXT-ALIGN: center" vAlign=middle align=left colSpan=3><asp:Label id="lblEtq_BusDestino" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Busqueda de Centro de Costos" __designer:wfdid="w257"></asp:Label></TD><TD style="WIDTH: 25px; HEIGHT: 30px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código" __designer:wfdid="w258"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusCod" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w259"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnUbiCerrar" onclick="btnUbiCerrar_Click" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cerrar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Silver" BackColor="LightGray" __designer:wfdid="w260"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="Label12" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción" __designer:wfdid="w261"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusDescripcion" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w262"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnUbiListar" onclick="btnUbiListar_Click" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w263"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 306px" vAlign=middle align=left></TD><TD style="HEIGHT: 306px" vAlign=middle align=left colSpan=3><asp:UpdatePanel id="UpdatePanel8" runat="server" __designer:wfdid="w264"><ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 444px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 250px" id="lblBusCentroCosto2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" Font-Overline="False" __designer:wfdid="w265" AutoGenerateColumns="False">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="&lt;&lt;">
                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                        Font-Names="Arial" Font-Size="8pt" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CODIGO">
                                    <ItemStyle ForeColor="DarkGray" Width="0px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView> </DIV>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD><TD style="WIDTH: 25px; HEIGHT: 306px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 280px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD></TR></TBODY></TABLE></DIV>
</contenttemplate>
        <triggers>
<asp:AsyncPostBackTrigger ControlID="_Ubica1" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="_Ubica2" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
    </asp:UpdatePanel>

    <!-- Cuadro de diálogo modal -->
        <div id="myModalGuia" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12 col-sm-6" >
                                    <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                                </div> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div> 
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="Label1" runat="server" Font-Size="16px" class="control-label2" Text="Elegir Tipo de Documento a Generar" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-3">
                                                <asp:Button ID="btnRedirectYes" runat="server" class="form-control btn btn-default" Text="Generar Guía de Remisión" OnClick="btnRedirectYes_Click" />
                                            </div>
                                            <div class="col-md-6 col-sm-3 ">
                                               <asp:Button ID="btnRedirectNo" runat="server" class="form-control btn btn-default" Text="Generar Guía Interna" OnClick="btnRedirectNo_Click" />
                                            </div>
                                        </div>
                                    </div> 
                                </div>
                            </div> 
                        </div> 
                    </div> 
                </div> 
            </div>
        </div>

</asp:Content>

