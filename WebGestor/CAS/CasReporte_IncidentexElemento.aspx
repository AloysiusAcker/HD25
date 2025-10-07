<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="CasReporte_IncidentexElemento.aspx.vb" Inherits="CasReporte_IncidentexElemento" title="CAS" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<script runat="server">
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

        End Sub
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Incidentes por Elementos</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="background-image: url(Fotos/lineaCas.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 22px; text-align: left; vertical-align: middle;" valign="top">
                    <asp:Button ID="btnRegresar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        CssClass="EstiloBoton"  onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Regresar" Width="80px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 22px" valign="top">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                        HasCrystalLogo="False" HasDrillUpButton="False" HasExportButton="False" HasGotoPageButton="False" HasRefreshButton="True" HasSearchButton="False"
                        HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False"
                        Height="500px" ReportSourceID="CR2" Width="550px" DisplayGroupTree="False" EnableTheming="True" BestFitPage="False" BorderColor="DarkGray" BorderStyle="Outset" BorderWidth="1px" />
                    <CR:CrystalReportSource ID="CR2" runat="server">
                        <Report FileName="C:\Inetpub\wwwroot\WebCas\Reportes\IxE.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="1" style="height: 19px; width: 550px;" valign="top">
                    &nbsp;
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

