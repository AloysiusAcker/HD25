<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Rep_RegistroTematicasXOficina.aspx.vb" Inherits="Cas_Rep_RegistroTematicasXOficina" title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 550px">
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
            <td style="width: 25px; height: 15px">
            </td>
            <td style="width: 550px; height: 15px">
            </td>
            <td style="width: 25px; height: 15px">
            </td>
        </tr>
        <tr>
            <td style="width: 25px; height: 15px">
            </td>
            <td style="width: 550px; height: 15px">
                <asp:Button ID="btnRegresar" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                    onmouseover="this.style.fontWeight='bolder'" Text="Regresar" Width="80px" /></td>
            <td style="width: 25px; height: 15px">
            </td>
        </tr>
        <tr>
            <td style="width: 25px; height: 18px;">
            </td>
            <td style="width: 550px; height: 18px;">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                    BestFitPage="False" DisplayGroupTree="False" HasCrystalLogo="False" HasDrillUpButton="False"
                    HasExportButton="False" HasGotoPageButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"
                    HasViewList="False" HasZoomFactorList="False" Height="600px" ReportSourceID="RptOP"
                    Width="550px" HasRefreshButton="True" />
                <CR:CrystalReportSource ID="RptOP" runat="server">
                    <Report FileName="Reportes\RepCas_OfxProducto.rpt">
                    </Report>
                </CR:CrystalReportSource>
                &nbsp; &nbsp;
            </td>
            <td style="width: 25px; height: 18px;">
            </td>
        </tr>
    </table>
</asp:Content>

