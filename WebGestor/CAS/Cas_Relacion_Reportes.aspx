<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Relacion_Reportes.aspx.vb" Inherits="Cas_Relacion_Reportes" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="8" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 207px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Relación de Reportes</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8" style="background-image: url(Fotos/lineaCas.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 105px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 125px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Reportes"></asp:Label></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:DropDownList ID="cboReporte" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="486px">
                        <asp:ListItem Value="0">Incidentes Vs Componentes</asp:ListItem>
                        <asp:ListItem Value="1">Incidentes Vs Elementos</asp:ListItem>
                        <asp:ListItem Value="2">Incidentes Totales</asp:ListItem>
                        <asp:ListItem Value="3">Incidentes: Oficinas por Componente</asp:ListItem>
                        <asp:ListItem Value="4">Totales de la Base de Datos y los Top 10 del Mes</asp:ListItem>
                        <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:CheckBox ID="chkEstado" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Text="Estado" Width="55px" /></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboEstado" runat="server" Enabled="False" Font-Names="Arial"
                                Font-Size="8pt" Width="201px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkEstado" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:CheckBox ID="chkImport" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Text="Importancia" Width="77px" /></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboImportancia" runat="server" Enabled="False" Font-Names="Arial"
                                Font-Size="8pt" Width="201px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkImport" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 25px" valign="top">
                    &nbsp;<asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                    <asp:TextBox ID="txtFechaIni" runat="server" Font-Names="Arial" Font-Size="8pt" Width="77px"></asp:TextBox><asp:ImageButton
                        ID="img1" runat="server" ImageUrl="~/Fotos/Calendario.bmp" /></td>
                <td align="left" style="vertical-align: middle; width: 105px; height: 25px" valign="top">
                    <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"></asp:TextBox><asp:ImageButton
                        ID="img2" runat="server" ImageUrl="~/Fotos/Calendario.bmp" /></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 125px; height: 25px; text-align: right"
                    valign="top">
                    <asp:Button ID="btnImprimir" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Imprimir" Width="76px" /></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                    <asp:Button ID="btnBListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt"
                        CssClass="EstiloBoton" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Listar" Width="76px" /></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="6" style="vertical-align: middle;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div style="border-right: darkgray 1px outset; border-top: darkgray 1px outset; overflow: auto;
                                border-left: darkgray 1px outset; width: 545px; border-bottom: darkgray 1px outset;
                                position: static;">
                                <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" BorderColor="DarkGray"
                                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" PageSize="15"
                                    Width="545px">
                                    <Columns>
                                        <asp:BoundField DataField="C1" />
                                        <asp:BoundField DataField="C2" />
                                        <asp:BoundField DataField="C3" />
                                        <asp:BoundField DataField="C4" />
                                        <asp:BoundField DataField="C5" />
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBListar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cboReporte" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="img1"
        TargetControlID="txtFechaIni">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="img2"
        TargetControlID="txtFechaFin">
    </cc1:CalendarExtender>
</asp:Content>

