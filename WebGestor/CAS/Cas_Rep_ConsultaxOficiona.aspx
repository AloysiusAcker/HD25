<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Rep_ConsultaxOficiona.aspx.vb" Inherits="Cas_Rep_ConsultaxOficiona" title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 550px">
            <tr>
                <td style="width: 25px; height: 50px">
                </td>
                <td style="width: 498px; height: 50px">
                </td>
                <td style="width: 25px; height: 50px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="background-image: url(Fotos/lineaCas.JPG); height: 10px">
                </td>
            </tr>
            <tr>
                <td style="width: 25px; height: 15px">
                </td>
                <td style="width: 498px; height: 15px">
                </td>
                <td style="width: 25px; height: 15px">
                </td>
            </tr>
            <tr>
                <td style="width: 25px; height: 22px">
                </td>
                <td style="width: 498px; height: 22px">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                        BestFitPage="False" DisplayGroupTree="False" HasCrystalLogo="False" HasDrillUpButton="False"
                        HasExportButton="False" HasGotoPageButton="False" HasRefreshButton="True" HasSearchButton="False"
                        HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False"
                        Height="550px" ReportSourceID="RpConsultaxOficina" Width="500px" />
                    <CR:CrystalReportSource ID="RpConsultaxOficina" runat="server">
                        <Report FileName="Reportes\Registro_ConsultaXOficina.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                </td>
                <td style="width: 25px; height: 22px">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

