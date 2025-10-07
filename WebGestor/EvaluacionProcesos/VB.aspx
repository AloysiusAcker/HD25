<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VB.aspx.vb" Inherits="EvaluacionProcesos_VB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="txtLocation" runat="server" Text=""></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="FindCoordinates" />
    <br />
    <br />
    <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
        runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Address" HeaderText="Address" />
            <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
            <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>
