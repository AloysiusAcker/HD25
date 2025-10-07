<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Contabilidad_Voucher_Detalle.aspx.vb" Inherits="Contabilidad_Voucher_Detalle" title="GestorPlus" %>

<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDataInput.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics2.WebUI.WebSchedule.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="ig_sched" %>
<%@ Register Assembly="Infragistics2.WebUI.WebCombo.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics2.WebUI.Shared.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Shared.Style" TagPrefix="igtbl1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="9" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Nuevo Comprobantes</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px;" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 10px;" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 10px;" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 10px;" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 10px" valign="top">
                    </td>
                <td align="left" style="width: 90px; height: 10px;" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 10px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 10px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 200px">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Cambio (S/.)"
                        Width="100px"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel17" runat="server">
                                        <contenttemplate>
                                    <asp:TextBox ID="txtValorVenta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="vertical-align: middle; text-align: right" Width="70px">0.00</asp:TextBox>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboPeriodos" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtFechaDoc" EventName="ValueChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 60px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:Button ID="btnGuardar" runat="server" BackColor="LightGray" BorderColor="Gray" CssClass="EstiloBoton" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Guardar"
                        Width="65px" ForeColor="Gray" /></td>
                <td align="left" style="width: 90px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 10px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 10px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 10px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 10px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 10px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Periodo"
                        Width="36px"></asp:Label></td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:DropDownList ID="cboPeriodos" runat="server" AutoPostBack="True"
                        Font-Names="Arial" Font-Size="8pt" Width="346px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Registro"
                        Width="80px"></asp:Label></td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<igsch:WebDateChooser id="txtFechaReg" runat="server" Width="141px" Height="14px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DimGray" Editable="False" Value="2009-03-02" BrowserTarget="UpLevel">
<DropButton>
<HoverStyle BackColor="Gray"></HoverStyle>
</DropButton>

<ExpandEffects Duration="10" ShadowColor="Silver"></ExpandEffects>

<CalendarLayout NextMonthText="" PrevMonthText="" ShowMonthDropDown="False" ShowYearDropDown="False" ShowFooter="False" ChangeMonthToDateClicked="True" HideOtherMonthDays="True">
<CalendarStyle BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"></CalendarStyle>

<DayHeaderStyle BackColor="Silver"></DayHeaderStyle>

<DayStyle BackColor="Gainsboro"></DayStyle>

<SelectedDayStyle BackColor="LightSkyBlue"></SelectedDayStyle>

<TitleStyle BackColor="Silver"></TitleStyle>

<TodayDayStyle BackColor="LightSkyBlue"></TodayDayStyle>

<DropDownStyle BackColor="#E0E0E0"></DropDownStyle>
</CalendarLayout>
</igsch:WebDateChooser> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboPeriodos" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px;" valign="top">
                    <asp:Label ID="lblEtiq4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Asiento"
                        Width="80px"></asp:Label></td>
                <td align="left" colspan="6" style="height: 22px; vertical-align: middle;" valign="top">
                    <asp:DropDownList ID="cboAsientos" runat="server" AutoPostBack="True" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" Width="346px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    <asp:Label ID="lblEtiq5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Comprobante"
                        Width="80px" Height="14px"></asp:Label></td>
                <td align="left" style="width: 100px; height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<asp:TextBox id="lblPrefVoucher" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" MaxLength="4" ReadOnly="True"></asp:TextBox><asp:TextBox id="txtNroVoucher" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" MaxLength="4" ReadOnly="True"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboAsientos" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboPeriodos" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 60px; height: 20px; vertical-align: middle;" valign="top">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblEtiq6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Moneda"
                        Width="38px"></asp:Label></td>
                <td align="left" style="height: 20px; vertical-align: middle;" valign="top" colspan="4">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
<asp:DropDownList id="cboMoneda" runat="server" Width="186px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" EnableTheming="True" OnSelectedIndexChanged="cboMoneda_SelectedIndexChanged"></asp:DropDownList> 
</contenttemplate>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cuenta"
                        Width="80px"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <div style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 160px">
                                <tr>
                                    <td align="left" valign="top" style="vertical-align: middle; width: 110px; height: 22px;">
                    <asp:UpdatePanel id="UpdatePanel9" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtCuenta" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial" ValidationGroup="^\d{3}-\d{2}-\d{4}$"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexCuenta" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                                    </td>
                                    <td align="left" valign="top" style="vertical-align: middle; width: 50px; height: 22px;">
                                        &nbsp;<asp:Button
                        ID="btnUbicaCuenta" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="..."
                        Width="25px" /></td>
                                </tr>
                            </table>
                        </div>
                </td>
                <td align="left" colspan="4" valign="top" rowspan="2">
                    <asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="186px" Height="30px" Font-Size="6pt" Font-Names="Arial" AutoPostBack="True" EnableTheming="True" ActiveTabIndex="1"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Centro Costos
                            
</HeaderTemplate>
<ContentTemplate>
<DIV><TABLE style="WIDTH: 173px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 210px; HEIGHT: 25px" vAlign=top align=left><asp:DropDownList id="cboCentroCosto" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                Flujo Caja
                            
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 173px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 198px; HEIGHT: 25px" vAlign=top align=left><asp:DropDownList id="cboFlujoCaja" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
Part. Presup.
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 174px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 210px; HEIGHT: 25px" vAlign=top align=left><asp:DropDownList id="cboPartPresupuestaria" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexCuenta" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                                   </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                    <asp:Label ID="lblEtiq8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Opcion"
                        Width="80px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
<asp:RadioButtonList id="opt" runat="server" Width="93px" Height="21px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"><asp:ListItem Selected="True" Value="0">Debe</asp:ListItem>
<asp:ListItem Value="1">Haber</asp:ListItem>
</asp:RadioButtonList> 
</contenttemplate>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 25px; text-align: left"
                    valign="top">
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Importe"
                        Width="80px"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel13" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
<TABLE style="WIDTH: 200px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 110px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox style="VERTICAL-ALIGN: middle; TEXT-ALIGN: right" id="txtImporte" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial">0.00</asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Button id="btnImporte" runat="server" Width="25px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="..." BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 80px; height: 22px" valign="top">
                    <div style="text-align: left">
                        &nbsp;</div>
                </td>
                <td align="left" style="width: 90px; height: 22px; vertical-align: middle;" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                    <asp:Label ID="lblEtiq10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Doc."
                        Width="80px"></asp:Label></td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 25px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel16" runat="server">
                        <contenttemplate>
<TABLE style="WIDTH: 402px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left colSpan=2><igsch:WebDateChooser id="txtFechadoc" runat="server" Width="143px" Height="14px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DimGray" BrowserTarget="UpLevel" Value="2009-03-02" __designer:wfdid="w1">
<DropButton>
<HoverStyle BackColor="Gray"></HoverStyle>
</DropButton>

<ExpandEffects Duration="10" ShadowColor="Silver"></ExpandEffects>

<CalendarLayout ShowMonthDropDown="False" ShowYearDropDown="False" ShowFooter="False" HideOtherMonthDays="True">
<CalendarStyle BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt"></CalendarStyle>

<DayHeaderStyle BackColor="Silver"></DayHeaderStyle>

<DayStyle BackColor="Gainsboro"></DayStyle>

<SelectedDayStyle BackColor="OliveDrab"></SelectedDayStyle>

<TitleStyle BackColor="Silver"></TitleStyle>

<DropDownStyle BackColor="#E0E0E0"></DropDownStyle>
</CalendarLayout>
</igsch:WebDateChooser></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtiq11" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" Text="Fec. Vcto."></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left colSpan=2><igsch:WebDateChooser id="txtFechaVcto" runat="server" Width="143px" Height="14px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DimGray" BrowserTarget="UpLevel" Value="2009-03-02">
<DropButton>
<HoverStyle BackColor="Gray"></HoverStyle>
</DropButton>

<ExpandEffects Duration="10" ShadowColor="Silver"></ExpandEffects>

<CalendarLayout ShowMonthDropDown="False" ShowYearDropDown="False" ShowFooter="False" HideOtherMonthDays="True">
<CalendarStyle BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"></CalendarStyle>

<DayHeaderStyle BackColor="Silver"></DayHeaderStyle>

<DayStyle BackColor="Gainsboro"></DayStyle>

<SelectedDayStyle BackColor="OliveDrab"></SelectedDayStyle>

<TitleStyle BackColor="Silver"></TitleStyle>

<TodayDayStyle BackColor="LightSkyBlue"></TodayDayStyle>

<DropDownStyle BackColor="#E0E0E0"></DropDownStyle>
</CalendarLayout>
</igsch:WebDateChooser></TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="R.U.C."
                        Width="78px"></asp:Label></td>
                <td align="left" colspan="5" style="height: 22px; vertical-align: middle;" valign="top">
                    <div>
                        <div style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 400px">
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 310px; height: 22px" valign="top">
                                        <asp:UpdatePanel id="UpdatePanel11" runat="server">
                                            <contenttemplate>
                    <asp:TextBox ID="txtRuc" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="11"
                        Width="310px" ></asp:TextBox>
</contenttemplate>
                                            <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexPersonas" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                                        </asp:UpdatePanel></td>
                                    <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                        &nbsp;<asp:Button
                        ID="btnBuscaPer" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="..."
                        Width="26px" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Doc."
                        Width="78px"></asp:Label></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:DropDownList ID="cboTipoDoc" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="346px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Doc."
                        Width="78px"></asp:Label></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 400px">
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 310px; height: 22px" valign="top">
                    <asp:TextBox ID="txtNroDoc" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="20"
                        Width="310px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                &nbsp;<asp:Button
                        ID="btnBuscarDoc" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="..."
                        Width="26px" /></td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Doc. Ref."
                        Width="78px"></asp:Label></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:DropDownList ID="cboTipoDocRef" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="346px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Doc. Ref."
                        Width="78px"></asp:Label></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtNroDocRef" runat="server" Font-Names="Arial" Font-Size="8pt"
                        MaxLength="20" Width="340px"></asp:TextBox></td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Glosa"
                        Width="78px"></asp:Label></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtGlosa" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="200"
                        Width="340px"></asp:TextBox></td>
                <td align="left" style="width: 50px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 19px" valign="top" colspan="7">
                    <asp:UpdatePanel id="UpdatePanel12" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 434px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 174px" id="DIV3" runat="server"><asp:GridView id="Flex" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="C1"></asp:BoundField>
<asp:BoundField DataField="C2" HeaderText="CUENTA">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="C3" HeaderText="DEBE (S/.)">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="C4" HeaderText="HABER (S/.)">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="C5" HeaderText="DEBE ($.)">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="C6" HeaderText="HABER ($.)">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="C7">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="C8">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView></DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboMoneda" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtImporte" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnImporte" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="opt" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 19px; vertical-align: middle;" valign="top">
                    <div style="text-align: left">
                        <div style="text-align: left">
                            <asp:UpdatePanel id="UpdatePanel15" runat="server">
                                <contenttemplate>
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 450px">
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 160px; height: 25px; text-align: right;" valign="top">
                                        <asp:Label ID="lblEtiq20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="TOTAL COMPROBANTE"
                                            Width="115px"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 30px; height: 25px; text-align: center"
                                        valign="top">
                                        <asp:Label ID="lblSigno" runat="server" Font-Names="Arial" Font-Size="8pt" Text="S/."
                                            Width="20px"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 25px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="lblTotDebe" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="4"
                                            ReadOnly="True" Style="vertical-align: middle; text-align: right" Width="80px">0.00</asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 25px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="lblTotHaber" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="4"
                                            ReadOnly="True" Style="vertical-align: middle; text-align: right" Width="80px">0.00</asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 100px; height: 25px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="lblDiferencia" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="4" ReadOnly="True" Style="vertical-align: middle; text-align: right"
                                            Width="80px">0.00</asp:TextBox></td>
                                </tr>
                            </table></contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="cboMoneda" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnImporte" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="opt" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel>&nbsp;</div>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 19px" valign="top" colspan="7">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 21px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; height: 21px" valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 21px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:ModalPopupExtender 
                    ID="ModalPopupExtender1" 
                    runat="server" 
                    TargetControlID="btnUbicaCuenta"
                    CancelControlID ="btnCerrar"
                    PopupControlID ="Panel1"
                    X="500"
                    Y="300" 
                    CacheDynamicResults="True">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender 
                    ID="ModalPopupExtender2" 
                    runat="server"
                    TargetControlID="btnBuscaPer"
                    CancelControlID ="btnCerrarPer" 
                    PopupControlID="Panel2"
                    X="500"
                    Y="300" 
                    CacheDynamicResults="True">
    </cc1:ModalPopupExtender>
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <br />
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
        <div style="text-align: left">
            <table id="TABLE1" runat="server" border="0" cellpadding="0" cellspacing="0" style="border-right: gray 2px outset;
                border-top: gray 2px outset; border-left: gray 2px outset; width: 500px; border-bottom: gray 2px outset;
                background-color: darkgray">
                <tr>
                    <td align="left" colspan="3" valign="top" style="height: 20px">
                    </td>
                </tr>
                <tr>
                    <td align="left" rowspan="4" style="width: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 450px" valign="top">
                        <div style="text-align: left">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 450px">
                                <tr>
                                    <td align="left" style="width: 80px; vertical-align: middle; height: 23px;" valign="top">
                                        <asp:Label ID="lblEtiq18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cuenta"
                                            Width="40px"></asp:Label></td>
                                    <td align="left" style="width: 230px; height: 23px; vertical-align: middle;" valign="top">
                                        <asp:UpdatePanel id="UpdatePanel6" runat="server">
                                            <contenttemplate>
<asp:TextBox id="txtBusCuenta" runat="server" Width="180px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> 
</contenttemplate>
                                            <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="vertical-align: middle; width: 200px; height: 23px; text-align: right"
                                        valign="top">
                    <asp:Button ID="btnListar" runat="server" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Listar"
                        Width="50px" /><asp:Button ID="btnCerrar" runat="server" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="50px" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td align="left" rowspan="4" style="width: 25px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 450px; height: 10px" valign="top">
                        <asp:UpdatePanel id="UpdatePanel18" runat="server">
                            <contenttemplate>
<asp:Label id="lblErrorC" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexCuenta" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" valign="top" style="width: 450px; height: 265px;">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 450px; BORDER-BOTTOM: dimgray 1px outset; POSITION: static; HEIGHT: 260px; BACKGROUND-COLOR: white" id="DIV1" runat="server"><asp:GridView id="FlexCuenta" runat="server" Width="600px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="7" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="SeaGreen" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="20px"></ControlStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="PLAN_CUENTA" HeaderText="Cuenta">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="120px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_NOMBRE_CUENTA" HeaderText="Nombre de la Cuenta" ReadOnly="True">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="240px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_NIVEL_CUENTA" HeaderText="Niv C.">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_CODIGO">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_CUENTA_DEUDORA">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_CUENTA_ACREEDORA">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_CENTRO_COSTOS">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_PRESUPUESTO">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_FLUJOCAJA">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexCuenta" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                </tr>
                <tr>
                    <td align="left" style="width: 450px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3" style="height: 20px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="50px" Width="125px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 450px; background-color: darkgray; border-right: gray 2px outset; border-top: gray 2px outset; border-left: gray 2px outset; border-bottom: gray 2px outset;" id="TABLE2" runat="server">
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 400px; height: 20px;">
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" rowspan="3" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 400px">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 400px">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 50px; height: 20px" valign="top">
                                    <asp:Label ID="lblEtiq19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="R.U.C."
                                        Width="32px"></asp:Label></td>
                                <td align="left" style="width: 180px; height: 20px" valign="top">
                                    <asp:UpdatePanel id="UpdatePanel10" runat="server">
                                        <contenttemplate>
<asp:TextBox id="txtBusRUC" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> 
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListaPer" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="left" style="vertical-align: middle; height: 20px; text-align: right; width: 170px;"
                                    valign="top">
                                    <asp:Button ID="btnListaPer" runat="server" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Listar"
                        Width="50px" /><asp:Button ID="btnCerrarPer" runat="server" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="50px" /></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" rowspan="3" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 400px; height: 10px;">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="1" valign="top" style="width: 400px; height: 265px;">
                    <asp:UpdatePanel id="UpdatePanel7" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 400px; BORDER-BOTTOM: dimgray 1px outset; POSITION: static; HEIGHT: 260px; BACKGROUND-COLOR: white" id="DIV2" runat="server"><asp:GridView id="FlexPersonas" runat="server" Width="400px" Font-Size="8pt" Font-Names="Arial" AllowPaging="True" AutoGenerateColumns="False" PageSize="7"><Columns>
<asp:ButtonField CommandName="AceptarPer" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="SeaGreen" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="20px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="TIPOP" HeaderText="TIPO PERSONA">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_RUC" HeaderText="R.U.C." ReadOnly="True">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_RAZON_SOCIAL" HeaderText="NOMBRE / RAZ&#211;N SOCIAL">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="230px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_CODIGO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView></DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListaPer" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 400px; height: 20px;">
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
        </table>
    </asp:Panel>
    &nbsp;
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
<asp:TextBox id="lblFIni" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox><asp:TextBox id="lblFFin" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCodCuenta" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCtaDeudora" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCtaAcreedora" runat="server" Width="127px" Height="21px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCentroCosto" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblPresupuesto" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblFlujoCaja" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCodPersona" runat="server" Width="127px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexCuenta" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
</asp:Content>

