<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Agenda_Citas.aspx.vb" Inherits="PersonalAgenda_Agenda_Citas" Title="Untitled Page" %>

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
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; height: 2px; text-align: center">
                        Agenda de Citas
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="background-image: url(../Fotos/linea.JPG)" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top"></td>
                <td align="left" style="width: 110px; height: 5px" valign="top"></td>
                <td align="left" style="width: 110px; height: 5px" valign="top"></td>
                <td align="left" style="width: 110px; height: 5px" valign="top"></td>
                <td align="left" style="width: 110px; height: 5px" valign="top"></td>
                <td align="left" style="width: 110px; height: 5px" valign="top"></td>
                <td align="left" style="width: 25px; height: 5px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" colspan="5" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <cc1:TabContainer ID="Ficha" runat="server" Width="550px" AutoPostBack="True" ActiveTabIndex="0" BorderColor="White">
                                <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                                    <HeaderTemplate>
                                        Citas del Personal
                            
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <table style="width: 544px" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 60px" valign="top" align="left"></td>
                                                    <td style="width: 134px" valign="top" align="left"></td>
                                                    <td style="width: 140px" valign="top" align="left"></td>
                                                    <td style="width: 130px" valign="top" align="left"></td>
                                                    <td style="width: 80px" valign="top" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                                                        <asp:Label ID="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w132" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left">
                                                        <asp:Label ID="lblAEtiq1" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w133" Text="Area"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                        <asp:DropDownList ID="cboAArea" runat="server" Width="400px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w134" AutoPostBack="True" CssClass="borderCbo">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                        <asp:Button ID="btnAListar" OnClick="btnAListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w135" Text="Listar"></asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left">
                                                        <asp:Label ID="lblAEtiq2" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w136" Text="Personal"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                        <asp:DropDownList ID="cboAPersonal" runat="server" Width="400px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w137" AutoPostBack="True" CssClass="borderCbo">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left">
                                                        <asp:Label ID="lblAEtiq3" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w138" Text="T. Atención"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                        <asp:DropDownList ID="cboATAtencion" runat="server" Width="400px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w139" AutoPostBack="True" OnSelectedIndexChanged="cboATAtencion_SelectedIndexChanged" CssClass="borderCbo">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 60px; height: 22px" valign="top" align="left">
                                                        <asp:Label ID="lblAEtiq4" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w140" Text="Fecha"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                        <asp:TextBox ID="txtAFecha" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w141" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        &nbsp;</td>
                                                    <td style="vertical-align: middle; width: 130px; height: 22px" valign="top" align="left">
                                                        <asp:TextBox ID="txtAFechaFin" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w142" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle" valign="top" align="left" colspan="3">&nbsp;</td>
                                                    <td style="vertical-align: middle" valign="top" align="left" colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                        <asp:Label ID="lblAEtiq8" runat="server" Font-Italic="False" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w143" ForeColor="Maroon" Text="Horario de Citas" Font-Overline="False"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                        <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w144" ForeColor="Maroon" Text="Días con Cita"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle" valign="top" align="left" colspan="3" rowspan="2">
                                                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 328px; border-bottom: gray 1px outset; height: 120px">
                                                            <asp:GridView ID="FlexCitas" runat="server" Width="380px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w145" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="c0" HeaderText="#">
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c1" HeaderText="Horario">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c2" HeaderText="Persona">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c3" HeaderText="Asunto">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c4" HeaderText="Modo Cita">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c5" HeaderText="Obs. Cita">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                        <asp:Label ID="Label2" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" __designer:wfdid="w146" Text="Mes"></asp:Label>
                                                        <asp:DropDownList ID="cboAMes" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w147" AutoPostBack="True" OnSelectedIndexChanged="cboAMes_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="cboAAno" runat="server" Width="83px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w148" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: text-top; height: 22px" valign="top" align="left" colspan="2">
                                                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 200px; border-bottom: gray 1px outset; height: 100px">
                                                            <asp:GridView ID="FlexMes" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w149" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="c0" HeaderText="Lun">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c1" HeaderText="Mar">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c2" HeaderText="Mie">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c3" HeaderText="Jue">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c4" HeaderText="Vie">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c5" HeaderText="Sab">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c6" HeaderText="Dom">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                        <asp:Label ID="lblAEtiq5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w150" ForeColor="Maroon" Text="Horaro de Atención"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 140px; height: 22px" valign="top" align="left"></td>
                                                    <td style="vertical-align: middle; width: 130px; height: 22px" valign="top" align="left">
                                                        <asp:Label ID="lblAEtiq6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w151" ForeColor="Maroon" Text="Disponibilidad"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: text-top; height: 22px" valign="top" align="left" colspan="3">
                                                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 328px; border-bottom: gray 1px outset; height: 100px">
                                                            <asp:GridView ID="FlexHorario" runat="server" Width="380px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w152" Font-Overline="False" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="c1" HeaderText="#">
                                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c2" HeaderText="Tipo Atenci&#243;n">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c3" HeaderText="D&#237;a y Hora">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 200px; border-bottom: gray 1px outset; height: 100px">
                                                            <asp:GridView ID="FlexDispo" runat="server" Width="180px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w124" Font-Overline="False" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:ButtonField CommandName="Cita" Text="Sarcar Cita" ButtonType="Button">
                                                                        <ControlStyle CssClass="EstiloBoton_Ac" Width="80px"></ControlStyle>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="c1" HeaderText="H. Inicio">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c2" HeaderText="H. Fin">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                        <asp:Label ID="lblAEtiq7" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w154" ForeColor="Maroon" Text="Minutos aproximados por cita:"></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 140px; height: 22px" valign="top" align="left">
                                                        <asp:TextBox ID="txtAMinutoCita" runat="server" Width="128px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w155" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 130px; height: 22px" valign="top" align="left">
                                                        <asp:Button ID="btnACita" OnClick="btnACita_Click" runat="server" CssClass="EstiloBoton_Ac" Width="88px" __designer:wfdid="w156" Text="Hacer Cita" Visible="False"></asp:Button>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 208px; border-bottom: gray 1px outset; height: 112px" id="DIV3" runat="server" visible="False">
                                                            <asp:GridView ID="FlexHSemana" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w157" Font-Overline="False" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="cDe" HeaderText="De">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="cA" HeaderText="A">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="c1" HeaderText="Dia">
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAFecha" __designer:wfdid="w158" Enabled="True" __designer:errorcontrol="No se puede establecer 'True' en la propiedad 'Enabled'." PopupButtonID="txtAFecha" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAFechaFin" __designer:wfdid="w159" Enabled="True" __designer:errorcontrol="No se puede establecer 'True' en la propiedad 'Enabled'." Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                        <asp:Calendar ID="dtpFecha" runat="server" Width="208px" Height="100px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w160" ForeColor="Red" Visible="true" OnSelectionChanged="dtpFecha_SelectionChanged" EnableTheming="True" BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" BackColor="White" SelectedDate="2011-12-14" NextPrevFormat="ShortMonth">
                                            <DayHeaderStyle Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></DayHeaderStyle>

                                            <NextPrevStyle VerticalAlign="Bottom" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="White"></NextPrevStyle>

                                            <OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>

                                            <SelectedDayStyle BackColor="LightGray" BorderColor="Gray" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"></SelectedDayStyle>

                                            <TitleStyle BackColor="Gray" BorderColor="White" BorderWidth="1px" BorderStyle="Outset" Font-Bold="True" Font-Size="10pt" ForeColor="White"></TitleStyle>

                                            <TodayDayStyle BackColor="White" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Font-Underline="False"></TodayDayStyle>
                                        </asp:Calendar>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2">
                                    <HeaderTemplate>
                                        Registrar
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="text-align: left">
                                            <table style="width: 540px" cellspacing="0" cellpadding="0" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 229px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 70px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px" valign="top" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                                                            <asp:Label ID="lblRError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w53" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq11" runat="server" __designer:wfdid="w54" Font-Names="Arial" Font-Size="8pt" Text="Area"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                                                            <asp:TextBox ID="txtRArea" runat="server" Width="290px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w55" ReadOnly="True" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq12" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w56" Text="Fecha de Cita"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRFecha" runat="server" __designer:wfdid="w57" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="70px" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 21px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq10" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w58" Text="Personal"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 21px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRPersonal" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w59" ReadOnly="True" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq1" runat="server" __designer:wfdid="w60" Font-Names="Arial" Font-Size="8pt" Text="Cita para:" Width="48px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:DropDownList ID="cboRCita" runat="server" Width="376px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w61" OnSelectedIndexChanged="cboRCita_SelectedIndexChanged" AutoPostBack="True" CssClass="borderCbo">
                                                                <asp:ListItem Selected="True">(Seleccionar)</asp:ListItem>
                                                                <asp:ListItem Value="3">Atención al Público</asp:ListItem>
                                                                <asp:ListItem Value="5">Atenci&#243;n al Personal de la Empresa</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnRBuscar" runat="server" __designer:wfdid="w62" CssClass="EstiloBoton_Ac" Text="Buscar" Width="76px"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq2" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w63" Text="Tipo Persona"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                            <asp:DropDownList ID="cboRTipoPer" runat="server" Width="457px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w64" CssClass="borderCbo">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq3" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w65" Text="Apellidos"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 229px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRApePat" runat="server" Width="220px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w66" MaxLength="30" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                                                            <asp:TextBox ID="txtRApeMat" runat="server" Width="220px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w67" MaxLength="30" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq4" runat="server" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w68" Text="Nombre"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRNombres" runat="server" Width="448px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w69" MaxLength="139" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq14" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w70" Text="Empresa"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtREmpresa" runat="server" Width="448px" __designer:wfdid="w71" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: text-top; width: 80px; height: 43px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq5" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w72" Text="Asunto"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 43px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRAsunto" runat="server" Width="450px" Height="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w73" MaxLength="299" TextMode="MultiLine" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: text-top; width: 80px; height: 43px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq6" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w74" Text="Obs. Cita"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 43px" valign="top" align="left" colspan="4">
                                                            <asp:TextBox ID="txtRObs" runat="server" Width="450px" Height="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w75" MaxLength="299" TextMode="MultiLine" CssClass="bordeTexboxPag"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq13" runat="server" __designer:wfdid="w76" Font-Names="Arial" Font-Size="8pt" Text="Modo de Cita"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 229px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboRModoCita" runat="server" __designer:wfdid="w77" Font-Names="Arial" Font-Size="8pt" Width="224px" CssClass="borderCbo">
                                                                <asp:ListItem Selected="True">(Seleccionar)</asp:ListItem>
                                                                <asp:ListItem Value="1">Voluntaria</asp:ListItem>
                                                                <asp:ListItem Value="2">Por Citación</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px;" valign="top" align="left">
                                                            <asp:Label ID="lblREtq7" runat="server" __designer:wfdid="w78" Font-Names="Arial" Font-Size="8pt" Text="Minutos x Cita"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRMinCita" runat="server" __designer:wfdid="w79" Font-Names="Arial" Font-Size="8pt" Width="27px" CssClass="bordeTexboxPag"></asp:TextBox>
                                                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="min"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq8" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w80" Text="Comienza"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 229px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboRComienza" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w82" CssClass="borderCbo">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; height: 22px; text-align: right" valign="top" align="left" colspan="2">
                                                            <asp:CheckBox ID="chkCitaRepro" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w83" Text="Cita Reprogramada" TextAlign="Left"></asp:CheckBox>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Label ID="lblREtq9" runat="server" __designer:wfdid="w84" Font-Names="Arial" Font-Size="8pt" Text="Termina"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 229px; height: 22px" valign="top" align="left">
                                                            <asp:DropDownList ID="cboRTermina" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w86" CssClass="borderCbo">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px; text-align: right" valign="top" align="left">
                                                            <asp:Button ID="btnRGuardar" OnClick="btnRGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w87" Text="Guardar"></asp:Button>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                                                            <asp:Button ID="btnRCancelar" runat="server" __designer:wfdid="w88" CssClass="EstiloBoton_Ac" OnClick="btnRCancelar_Click" Text="Cancelar" Width="76px"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 229px; height: 22px" valign="top" align="left">
                                                            <asp:TextBox ID="txtRCodArea" runat="server" __designer:wfdid="w89" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="24px"></asp:TextBox>
                                                            <asp:TextBox ID="txtRCodPersonal" runat="server" __designer:wfdid="w90" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="24px"></asp:TextBox>
                                                            <asp:TextBox ID="txtRCodRazon" runat="server" __designer:wfdid="w91" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="16px"></asp:TextBox>
                                                            <asp:TextBox ID="txtRGrabar" runat="server" __designer:wfdid="w92" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="1px"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px" valign="top" align="left"></td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px; text-align: right;" valign="top" align="left">&nbsp;</td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">&nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="text-align: left">
                                            <asp:Panel ID="pnPersona" runat="server" __designer:wfdid="w95">
                                                <table style="border-right: black 1px outset; border-top: black 1px outset; left: 503px; border-left: black 1px outset; width: 450px; border-bottom: black 1px outset; top: 541px" cellspacing="0" cellpadding="0" border="0" __designer:dtid="281474976710685">
                                                    <tbody>
                                                        <tr __designer:dtid="281474976710686">
                                                            <td style="vertical-align: middle; height: 26px; background-color: darkgray; text-align: center" valign="top" align="left" colspan="5" __designer:dtid="281474976710687">
                                                                <asp:Label ID="lblBP1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:dtid="281474976710688" __designer:wfdid="w96" Text="Relación de Personal de la Empresa"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr __designer:dtid="281474976710689">
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left" __designer:dtid="281474976710690"></td>
                                                            <td style="vertical-align: middle; width: 70px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Label ID="lblRV20" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w97" Text="Tipo Persona"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 250px; height: 22px; background-color: darkgray" valign="top" align="left" __designer:dtid="281474976710691">
                                                                <asp:DropDownList ID="cboBusTipoPer" runat="server" Width="248px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w98">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 80px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Button ID="btnBPCerrar" OnClick="btnBPCerrar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:dtid="281474976710700" __designer:wfdid="w99" ForeColor="Gray" Text="Cerrar"></asp:Button>
                                                            </td>
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left" __designer:dtid="281474976710697"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                                                            <td style="vertical-align: middle; width: 70px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Label ID="lblRV21" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w100" Text="Ap. Paterno"></asp:Label>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 250px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:TextBox ID="txtBusApePat" runat="server" Width="240px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w101"></asp:TextBox>
                                                            </td>
                                                            <td style="vertical-align: middle; width: 80px; height: 22px; background-color: darkgray" valign="top" align="left">
                                                                <asp:Button ID="btnBPListar" OnClick="btnBPListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w102" Text="Listar"></asp:Button>
                                                            </td>
                                                            <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 25px; height: 200px; background-color: darkgray" valign="top" align="left"></td>
                                                            <td style="height: 200px; background-color: darkgray" valign="top" align="left" colspan="3">
                                                                <div style="border-right: darkgray 1px outset; border-top: darkgray 1px outset; font-size: 8pt; vertical-align: middle; overflow: auto; border-left: darkgray 1px outset; width: 392px; border-bottom: darkgray 1px outset; font-family: Arial; height: 198px; text-align: center" id="DIV2" runat="server" __designer:dtid="281474976710692">
                                                                    <asp:GridView ID="FlexP" runat="server" Width="770px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w103" OnSelectedIndexChanged="FlexP_SelectedIndexChanged" PageSize="5" AutoGenerateColumns="False">
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
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnPersona" BackgroundCssClass="modalBackground" TargetControlID="btnRBuscar" __designer:wfdid="w104" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="300" X="300" CancelControlID="btnBPCerrar">
                                            </cc1:ModalPopupExtender>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 110px" valign="top"></td>
                <td align="left" style="width: 110px" valign="top"></td>
                <td align="left" style="width: 110px" valign="top"></td>
                <td align="left" style="width: 110px" valign="top"></td>
                <td align="left" style="width: 110px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

