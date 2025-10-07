<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EvalProcesos_Dashboard.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Dashboard" %>

<%@ Register Assembly="DevExpress.Dashboard.v19.1.Web.WebForms, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <div style = "position: absolute; left: 80px; right: 0; top: 0; bottom: 30px;" >
            <dx:ASPxButton ID = "ASPxButton1" runat = "server" Text = "Cambiar al Visor"  
                ClientInstanceName = "botón"  
                AutoPostBack = "False" >
                <ClientSideEvents Click = "function (s, e) {
                    var workingMode = webDashboard.GetWorkingMode ();
                    if (workingMode == 'designer') {
                        webDashboard.SwitchToViewer ();
                        button.SetText ('Cambiar a diseñador');
                    }
                    else {
                        webDashboard.SwitchToDesigner ();
                        button.SetText ('Cambiar a Visor');
                    }
                } "/>
            </dx:ASPxButton>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Regresar" PostBackUrl="~/PaginaPrincipal.aspx" />
        <div style = "position: absolute; left: 0; right: 0; top: 30px; bottom: 0;" >
           <%-- <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" Width = "100%" Height = "100%"  
            ClientInstanceName = "webDashboard"  
                onconfiguredataconnection = "ASPxDashboard1_ConfigureDataConnection" ></dx:ASPxDashboard>--%>
            <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" ClientInstanceName = "webDashboard"  
                onconfiguredataconnection = "ASPxDashboard1_ConfigureDataConnection">
                <PdfExportOptions MapAutomaticPageLayout="True" />
<ClientSideEvents DashboardChanged=""></ClientSideEvents>

<BackendOptions Uri=""></BackendOptions>

<DataRequestOptions ItemDataRequestMode="Auto"></DataRequestOptions>
            </dx:ASPxDashboard>
        </div>
    </form>
</body>
</html>
