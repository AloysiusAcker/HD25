<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Relacion_Estadisticas.aspx.vb" Inherits="Cas_Relacion_Estadisticas" title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
     <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div style="position: relative; top: 30%; text-align: center;">
                                    &nbsp;<img src="Fotos/5.gif" /></div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 321px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Formato de Consultas Cas Colombia</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="background-image: url(Fotos/lineaCas.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 390px; height: 15px" valign="top">
                </td>
                <td align="left" style="height: 15px; width: 90px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Año"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 390px; height: 22px; text-align: left"
                    valign="top">
                    <asp:DropDownList ID="cboAño" runat="server" Font-Names="Arial" Font-Size="8pt" Width="77px">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px; text-align: right"
                    valign="top">
                    <asp:Button ID="btnVistaPrevia" runat="server" BackColor="LightGray" BorderColor="Gray"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Vista Previa" Width="80px" BorderStyle="Outset" BorderWidth="1px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Reporte de :"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 390px; height: 22px; text-align: left"
                    valign="top">
                    <asp:DropDownList ID="cboReportes" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="380px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="height: 22px; text-align: right; vertical-align: middle; width: 90px;" valign="top">
                    &nbsp;
                    </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Mes"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 390px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboMes" runat="server" Font-Names="Arial" Font-Size="8pt" Width="380px">
                    </asp:DropDownList></td>
                <td align="left" style="height: 22px; width: 90px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 390px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:DropDownList ID="cboOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="380px" Enabled="False">
                    </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboReportes" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 390px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="lblError" runat="server" BorderStyle="None" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Text="Label"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 390px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

