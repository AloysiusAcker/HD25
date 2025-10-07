<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="InvReport_Recepcion.aspx.vb" Inherits="Inventario_InvReport_Recepcion" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--    <rsweb:ReportViewer ID="ReportViewer1" runat="server"  BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="800px">
       <LocalReport ReportPath="Inventario\Report_Recepcion.rdlc">
           <DataSources>
               <rsweb:ReportDataSource DataSourceId="Report_datos" Name="Informe_Recepcion" />
           </DataSources>
       </LocalReport>
    </rsweb:ReportViewer>--%>
    <asp:SqlDataSource ID="Report_datos" runat="server" ConnectionString="<%$ ConnectionStrings:Cn_bdEmpresa %>" SelectCommand="Prc_Reporte_Recepcion" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0001" Name="CodEmpresa" SessionField="CodEmpresa" Type="String" />
            <asp:SessionParameter DefaultValue="2" Name="CodRecep" SessionField="CodRecep" Type="Double" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

