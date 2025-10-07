<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Rep_PorCargos.aspx.vb" Inherits="Cas_Rep_PorCargos" title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="height: 50px; width: 550px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
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
                <td align="left" style="height: 15px; width: 550px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px; width: 550px;" valign="top">
                    <asp:Button ID="btnRegresar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Regresar" Width="80px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px; width: 550px;" valign="top">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                        BestFitPage="False" DisplayGroupTree="False" HasCrystalLogo="False" HasDrillUpButton="False"
                        HasExportButton="False" HasGotoPageButton="False" HasRefreshButton="True" HasSearchButton="False"
                        HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False"
                        Height="600px" ReportSourceID="RptCas_Cargos" Width="550px" />
                    <CR:CrystalReportSource ID="RptCas_Cargos" runat="server">
                        <Report FileName="Reportes\RepCas_CargosContactan.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px; width: 550px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

