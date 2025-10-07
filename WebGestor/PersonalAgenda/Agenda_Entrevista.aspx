<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Agenda_Entrevista.aspx.vb" Inherits="PersonalAgenda_Agenda_Entrevista" Title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" lang="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    &nbsp;<img src="../Fotos/5.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
                <td align="left" colspan="3" style="height: 50px; text-align: center" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; left: 253px; vertical-align: middle; width: 536px; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; height: 2px; text-align: center">
                        Entrevista
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top"></td>
                <td align="left" style="width: 150px; height: 5px" valign="top"></td>
                <td align="left" style="width: 200px; height: 5px" valign="top"></td>
                <td align="left" style="width: 200px; height: 5px" valign="top"></td>
                <td align="left" style="width: 25px; height: 5px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" colspan="3" style="height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <cc1:TabContainer ID="Ficha" runat="server" Width="550px" ActiveTabIndex="2" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                                <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                                    <HeaderTemplate>
                                        Entrevista
                            
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: left; margin-left:10px;">
                                            <div style="text-align: left">
                                                <table style="width: 536px" cellspacing="0" cellpadding="0" border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 30px" valign="top" align="left"></td>
                                                            <td style="width: 70px" valign="top" align="left"></td>
                                                            <td style="width: 50px" valign="top" align="left"></td>
                                                            <td style="width: 60px" valign="top" align="left"></td>
                                                            <td style="width: 140px" valign="top" align="left"></td>
                                                            <td style="width: 61px" valign="top" align="left"></td>
                                                            <td style="width: 45px" valign="top" align="left"></td>
                                                            <td style="width: 80px" valign="top" align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="8">
                                                                <asp:Label ID="lblError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w310" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30px; height: 22px" valign="top" align="left">
                                                                <asp:Label ID="lblEntEtq1" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w311" Text="Año"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                                <asp:DropDownList ID="cboEntAño" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w312"></asp:DropDownList>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 50px; height: 22px" valign="top" align="left">
                                                                <asp:Label ID="lblEntEtq2" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w313" Text="Personal"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                                <asp:DropDownList ID="cboEntPersonal" runat="server" Width="304px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w314" OnSelectedIndexChanged="cboEntPersonal_SelectedIndexChanged"></asp:DropDownList>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                                <asp:TextBox ID="txtEntPersonal" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w315" ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle" valign="top" align="left" colspan="3" rowspan="2">
                                                                <asp:RadioButtonList ID="optEntTipo" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w316">
                                                                    <asp:ListItem Selected="True" Value="0">Sin Cita / Modo Libre</asp:ListItem>
                                                                    <asp:ListItem Value="1">De una Cita Programada</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                                <asp:Button ID="btnEntNuevo" OnClick="btnEntNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="192px" __designer:wfdid="w317" Text="Efectuar Entrevista"></asp:Button>
                                                            </td>
                                                            <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                                <asp:Button ID="btnHistorial" runat="server" CssClass="EstiloBoton_Ac" Width="180px" __designer:wfdid="w318" Text="Historial de Entrevistas"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 60px; height: 23px" valign="top" align="left">&nbsp;</td>
                                                            <td style="vertical-align: middle; width: 140px; height: 23px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 61px; height: 23px" valign="top" align="left">
                                                                <asp:Label ID="lblEntEtq3" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w319" Text="Día de Cita" Visible="False"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; height: 23px" valign="top" align="left" colspan="2">
                                                                <asp:TextBox ID="txtEntFecha" runat="server" Width="88px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w320" ReadOnly="True" Visible="False"></asp:TextBox>
                                                                <asp:Button ID="btnCal" OnClick="btnCal_Click" runat="server" CssClass="EstiloBoton_Ac" Width="20px" __designer:wfdid="w321" Text="..." Visible="False"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="8">
                                                                <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 530px; border-bottom: gray 1px outset; height: 200px">
                                                                    <asp:GridView ID="FlexCita" runat="server" Width="100%" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w308" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:ButtonField CommandName="Entrevista" Text="Entrevista" ButtonType="Button">
                                                                                <ControlStyle CssClass="EstiloBoton_Ac" Width="70px"></ControlStyle>

                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                            </asp:ButtonField>
                                                                            <asp:BoundField DataField="c1" HeaderText="#"></asp:BoundField>
                                                                            <asp:BoundField DataField="c2" HeaderText="N&#176; Cita"></asp:BoundField>
                                                                            <asp:BoundField DataField="c3" HeaderText="Horario"></asp:BoundField>
                                                                            <asp:BoundField DataField="c4" HeaderText="Area"></asp:BoundField>
                                                                            <asp:BoundField DataField="c5" HeaderText="A quien Entrevistar"></asp:BoundField>
                                                                            <asp:BoundField DataField="c6" HeaderText="Persona"></asp:BoundField>
                                                                            <asp:BoundField DataField="c7" HeaderText="Asunto"></asp:BoundField>
                                                                            <asp:BoundField DataField="c8" HeaderText="Modo Cita"></asp:BoundField>
                                                                            <asp:BoundField DataField="c9" HeaderText="Obs. Cita"></asp:BoundField>
                                                                        </Columns>

                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="vertical-align: middle; width: 30px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 50px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 140px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 61px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 45px; height: 22px" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div style="left: 568px; width: 184px; position: absolute; top: 392px; height: 128px" id="lblCalendario" runat="server" visible="False">
                                            <asp:Calendar ID="Cal1" runat="server" Width="200px" Height="150px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w323" ForeColor="Black" BorderColor="Gray" BackColor="White" NextPrevFormat="ShortMonth">
                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt"></DayHeaderStyle>

                                                <DayStyle Width="14%"></DayStyle>

                                                <NextPrevStyle Font-Size="8pt" ForeColor="White"></NextPrevStyle>

                                                <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>

                                                <SelectedDayStyle BackColor="DarkKhaki" ForeColor="White"></SelectedDayStyle>

                                                <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" Width="1%"></SelectorStyle>

                                                <TitleStyle BackColor="DarkGray" Font-Bold="True" Font-Size="10pt" ForeColor="White" Height="14px"></TitleStyle>

                                                <TodayDayStyle BackColor="#CCCC99"></TodayDayStyle>
                                            </asp:Calendar>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2">
                                    <HeaderTemplate>
                                        Historial de Entrevista
                            
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: left; margin-left:10px;">
                                            <table style="width: 536px" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 150px" valign="top" align="left"></td>
                                                        <td style="width: 306px" valign="top" align="left"></td>
                                                        <td style="width: 80px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:Label ID="lblHError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w16" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px" valign="top" align="left">
                                                            <asp:CheckBox ID="chkBus1" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w17" Text="Por a Quien se Entrevisto"></asp:CheckBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 306px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboBus1" runat="server" Width="300px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w18" Enabled="False"></asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnBusListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w19" Text="Listar"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px" valign="top" align="left">
                                                            <asp:CheckBox ID="chkBus2" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w20" Text="Por Modo de Entrevista"></asp:CheckBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 306px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboBus2" runat="server" Width="300px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w21" Enabled="False"></asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnBusRegresar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w22" Text="Regresar"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px" valign="top" align="left">
                                                            <asp:CheckBox ID="chkBus3" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w23" Text="Por Tipo de Entrevista"></asp:CheckBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 306px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboBus3" runat="server" Width="300px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w24" Enabled="False"></asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px" valign="top" align="left">
                                                            <asp:CheckBox ID="chkBus5" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w25" Text="Por Personal Entrevistado"></asp:CheckBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 306px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboBus4" runat="server" Width="300px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w26" Enabled="False"></asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px" valign="top" align="left">
                                                            <asp:CheckBox ID="chkBus4" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w27" Text="Por Fecha de Entrevista"></asp:CheckBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 306px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtBusFecha1" runat="server" Width="136px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w28" Enabled="False"></asp:TextBox>
                                                            <asp:TextBox ID="txtBusFecha2" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w29" Enabled="False"></asp:TextBox></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 530px; border-bottom: gray 1px outset; height: 168px">
                                                                <asp:GridView ID="FlexBus" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w15" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="c1" HeaderText="#"></asp:BoundField>
                                                                        <asp:BoundField DataField="c2" HeaderText="Fecha"></asp:BoundField>
                                                                        <asp:BoundField DataField="c12" HeaderText="Area"></asp:BoundField>
                                                                        <asp:BoundField DataField="c3" HeaderText="A quien"></asp:BoundField>
                                                                        <asp:BoundField DataField="c4" HeaderText="Horario"></asp:BoundField>
                                                                        <asp:BoundField DataField="c5" HeaderText="Persona"></asp:BoundField>
                                                                        <asp:BoundField DataField="c6" HeaderText="Modo "></asp:BoundField>
                                                                        <asp:BoundField DataField="c7" HeaderText="Tipo"></asp:BoundField>
                                                                        <asp:BoundField DataField="c8" HeaderText="Asunto"></asp:BoundField>
                                                                        <asp:BoundField DataField="c9" HeaderText="Acuerdo"></asp:BoundField>
                                                                        <asp:BoundField DataField="c14" HeaderText="Obs"></asp:BoundField>
                                                                        <asp:BoundField DataField="c15" HeaderText="Prox. Cita"></asp:BoundField>
                                                                        <asp:BoundField DataField="c10" HeaderText="Proviene"></asp:BoundField>
                                                                        <asp:BoundField DataField="c11" HeaderText="Participantes"></asp:BoundField>
                                                                        <asp:BoundField DataField="c13">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                    </Columns>

                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 306px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBusFecha1" __designer:wfdid="w31" PopupButtonID="txtBusFecha1" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBusFecha2" __designer:wfdid="w32" PopupButtonID="txtBusFecha2" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3">
                                    <HeaderTemplate>
                                        Registrar
                            
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: left; margin-left:10px;">
                                            <table style="width: 540px" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 110px" valign="top" align="left">&nbsp;</td>
                                                        <td style="vertical-align: middle; width: 30px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 40px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRCodRazon" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w230" Visible="False"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 60px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRCodPersonal" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w231" Visible="False"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRCodArea" runat="server" Width="8px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w232" Visible="False"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRGrabar" runat="server" Width="8px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w233" Visible="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="8">
                                                            <asp:Label ID="lblRError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w234" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:Label ID="lblREtq1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w235" ForeColor="Maroon" Text="Información de la Entrevista"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left">&nbsp;</td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRCodCita" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w236" Visible="False"></asp:TextBox>
                                                            <asp:TextBox ID="txtREstadoCita" runat="server" Width="8px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w237" Visible="False"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left">&nbsp;</td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnRGuardar" OnClick="btnRGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="64px" __designer:wfdid="w238" Text="Guardar"></asp:Button>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnRCancelar" OnClick="btnRCancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="64px" __designer:wfdid="w239" Text="Regresar"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq2" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w240" Text="Area"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="7">
                                                            <asp:DropDownList ID="cboRArea" runat="server" Width="456px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w241" OnSelectedIndexChanged="cboRArea_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq3" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w242" Text="Personal"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="7">
                                                            <asp:TextBox ID="txtRPersonal" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w243" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq4" runat="server" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w244" Text="Entrevista con"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="6">
                                                            <asp:DropDownList ID="cboREnt" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w245" OnSelectedIndexChanged="cboREnt_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnRBuscar" runat="server" CssClass="EstiloBoton_Ac" Width="64px" __designer:wfdid="w246" Text="Buscar" Enabled="False"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq5" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w247" Text="Tipo Persona"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="7">
                                                            <asp:DropDownList ID="cboRTipoPer" runat="server" Width="456px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w248" OnSelectedIndexChanged="cboRTipoPer_SelectedIndexChanged" Enabled="False"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 20px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq6" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w249" Text="Apellidos"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 20px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRApePat" runat="server" Width="250px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w250" ReadOnly="True" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 20px" valign="top" align="left" colspan="3">
                                                            <asp:TextBox ID="txtRApeMat" runat="server" Width="190px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w251" ReadOnly="True" MaxLength="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq7" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w252" Text="Nombres"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="7">
                                                            <asp:TextBox ID="txtRNombres" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w253" ReadOnly="True" MaxLength="139"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq8" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w254" Text="Empresa"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="7">
                                                            <asp:TextBox ID="txtREmpresa" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w255" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq10" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w256" Text="Modo de Ent."></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                            <asp:DropDownList ID="cboRModoEnt" runat="server" Width="136px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w257">
                                                                <asp:ListItem Selected="True">(Seleccionar)</asp:ListItem>
                                                                <asp:ListItem Value="1">Voluntaria</asp:ListItem>
                                                                <asp:ListItem Value="2">Por Citaci&#243;n</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq9" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w258" Text="Fecha"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRFecha" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w259" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq11" runat="server" Width="52px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w260" Text="Horario de"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRComienza" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w261">07:00</asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRTermina" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w262">07:00</asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:Label ID="lblREtq12" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w263" ForeColor="Maroon" Text="Asunto de la Entrevista"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                            <asp:Label ID="lblREtq20" runat="server" Width="112px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w264" ForeColor="Blue" Text="(Formato de 24 horas)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:Label ID="lblREtq13" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" __designer:wfdid="w265" Text="Descripción del Asunto"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq14" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" __designer:wfdid="w266" Text="Tipo y Asunto"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: text-top" valign="top" align="left" colspan="4" rowspan="3">
                                                            <asp:TextBox ID="txtRAsunto" runat="server" Width="250px" Height="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w267" MaxLength="299" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                            <asp:DropDownList ID="cboREnt1" runat="server" Width="276px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w268" OnSelectedIndexChanged="cboREnt1_SelectedIndexChanged" Font-Overline="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                            <asp:DropDownList ID="cboREnt2" runat="server" Width="276px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w269" OnSelectedIndexChanged="cboREnt2_SelectedIndexChanged" Font-Overline="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                            <asp:DropDownList ID="cboREnt3" runat="server" Width="276px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w270" Font-Overline="True"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq15" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w271" Text="Acuerdos"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 110px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 30px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq16" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w272" Text="Observación"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 50px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRAcuerdo" runat="server" Width="250px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w273" MaxLength="399" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 50px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRObs" runat="server" Width="270px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w274" MaxLength="399" TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                            <asp:Label ID="lblREtq17" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w275" ForeColor="Maroon" Text="Participantes en la Entrevista"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 30px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq18" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w276" Text="Tipo Participan."></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:DropDownList ID="cboRParticipante" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w277"></asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                            <asp:CheckBox ID="chkEntrevistador" runat="server" Width="135px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w278" Text="Nomb. del Entrevistador" OnCheckedChanged="chkEntrevistador_CheckedChanged"></asp:CheckBox>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                            <asp:CheckBox ID="chkEntrevistado" runat="server" Width="135px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w279" Text="Nomb. del Entrevistado" OnCheckedChanged="chkEntrevistado_CheckedChanged"></asp:CheckBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq19" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w280" Text="Ape. y Nombres"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="6">
                                                            <asp:TextBox ID="txtRParticipante" runat="server" Width="380px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w281" MaxLength="149"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnRAgregar" OnClick="btnRAgregar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="64px" __designer:wfdid="w282" Text="Agregar"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="8">
                                                            <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 530px; border-bottom: gray 1px outset; height: 120px">
                                                                <asp:GridView ID="FlexParticipante" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w283" AutoGenerateColumns="False" Font-Overline="False">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                                                            <ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                        </asp:ButtonField>
                                                                        <asp:BoundField DataField="c1" HeaderText="#">
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="c2" HeaderText="Participante">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="c3" HeaderText="Apellidos y Nombres">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="c4">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                    </Columns>

                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 110px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 30px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnPersona" BackgroundCssClass="modalBackground" TargetControlID="btnRBuscar" __designer:wfdid="w284" Enabled="True" DynamicServicePath="" Y="300" X="300" CancelControlID="btnBPCerrar"></cc1:ModalPopupExtender>
                                        </div>
                                        <div style="text-align: left">
                                            <asp:Panel ID="pnPersona" runat="server" __designer:wfdid="w285">
                                                <table style="border-right: black 1px outset; border-top: black 1px outset; left: 503px; border-left: black 1px outset; width: 450px; border-bottom: black 1px outset; top: 541px" cellspacing="0" cellpadding="0" border="0" __designer:dtid="281474976710685">
                                                    <tbody>
                                                        <tr __designer:dtid="281474976710686">
                                                            <td style="vertical-align: middle; height: 26px; background-color: darkgray; text-align: center" valign="top" align="left" colspan="5" __designer:dtid="281474976710687">
                                                                <asp:Label ID="lblBP1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:dtid="281474976710688" __designer:wfdid="w286" Text="Relación de Personal de la Empresa"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr __designer:dtid="281474976710689">
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left" __designer:dtid="281474976710690"></td>
                                                            <td style="vertical-align: middle; width: 70px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Label ID="lblRV20" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w287" Text="Tipo Persona"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 250px; height: 22px; background-color: darkgray" valign="top" align="left" __designer:dtid="281474976710691">
                                                                <asp:DropDownList ID="cboBusTipoPer" runat="server" Width="248px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w288"></asp:DropDownList>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 80px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Button ID="btnBPCerrar" OnClick="btnBPCerrar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:dtid="281474976710700" __designer:wfdid="w289" ForeColor="Gray" Text="Cerrar"></asp:Button>
                                                            </td>
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left" __designer:dtid="281474976710697"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 70px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Label ID="lblRV21" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w290" Text="Ap. Paterno"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 250px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:TextBox ID="txtBusApePat" runat="server" Width="240px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w291"></asp:TextBox>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 80px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Button ID="btnBPListar" OnClick="btnBPListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w292" Text="Listar"></asp:Button>
                                                            </td>
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25px; height: 200px; background-color: darkgray" valign="top" align="left"></td>
                                                            <td style="height: 200px; background-color: darkgray" valign="top" align="left" colspan="3">
                                                                <div style="border-right: darkgray 1px outset; border-top: darkgray 1px outset; font-size: 8pt; vertical-align: middle; overflow: auto; border-left: darkgray 1px outset; width: 392px; border-bottom: darkgray 1px outset; font-family: Arial; height: 198px; text-align: center" id="DIV2" runat="server" __designer:dtid="281474976710692">
                                                                    <asp:GridView ID="FlexP" runat="server" Width="770px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w293" AutoGenerateColumns="False" PageSize="5">
                                                                        <Columns>
                                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Button">
                                                                                <ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                            </asp:ButtonField>
                                                                            <asp:BoundField DataField="PERSON_CODIGO" HeaderText="C&#243;digo">
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="TIPO_PER" HeaderText="Tipo Persona">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="PERSON_APEPAT" HeaderText="Ap. Paterno">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="PERSON_APEMAT" HeaderText="Ap. Materno">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="PERSON_NOMBRES" HeaderText="Nombres">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="EMPRESA" HeaderText="Empresa">
                                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="TIPO_CODPER">
                                                                                <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                        </Columns>

                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                                                        <PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>
                                                                    </asp:GridView>
                                                                    &nbsp;<br __designer:dtid="281474976710696" />
                                                                </div>
                                                            </td>
                                                            <td style="width: 25px; height: 200px; background-color: darkgray" valign="top" align="left"></td>
                                                        </tr>
                                                        <tr __designer:dtid="281474976710698">
                                                            <td style="vertical-align: middle; height: 25px; background-color: darkgray; text-align: center" valign="top" align="left" colspan="5" __designer:dtid="281474976710699"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtRComienza" __designer:wfdid="w294" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtRTermina" __designer:wfdid="w295" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
                            <asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
                            <asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="width: 150px; height: 22px" valign="top"></td>
                <td align="left" style="width: 200px; height: 22px" valign="top"></td>
                <td align="left" style="width: 200px; height: 22px" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

