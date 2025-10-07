<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Rep_RegistroPorductoXOficina.aspx.vb" Inherits="Contabilidad_Rep_RegistroPorductoXOficina" title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 25px; height: 50px">
            </td>
            <td style="width: 550px; height: 50px">
            </td>
            <td style="width: 25px; height: 50px">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="height: 11px">
                <img src="Fotos/linea.JPG" /></td>
        </tr>
        <tr>
            <td style="width: 25px; height: 19px">
            </td>
            <td style="width: 550px; height: 19px">
            </td>
            <td style="width: 25px; height: 19px">
            </td>
        </tr>
        <tr>
            <td style="width: 25px; height: 19px;">
            </td>
            <td style="width: 550px; height: 19px;">
            </td>
            <td style="width: 25px; height: 19px;">
            </td>
        </tr>
        <tr>
            <td style="width: 25px">
            </td>
            <td style="width: 550px">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                    BestFitPage="False" DisplayGroupTree="False" HasCrystalLogo="False" HasDrillUpButton="False"
                    HasExportButton="False" HasGotoPageButton="False" HasRefreshButton="True" HasSearchButton="False"
                    HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False"
                    Height="500px" ReportSourceID="Rpt29" Width="550px" />
                <CR:CrystalReportSource ID="Rpt29" runat="server">
                    <Report FileName="Reportes\Registro_ProductoXOficina.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </td>
            <td style="width: 25px">
            </td>
        </tr>
    </table>
</asp:Content>

