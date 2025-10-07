<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Person_Control_EntSal_Ingreso_Directo.aspx.vb" Inherits="Person_Control_EntSal_Ingreso_Directo" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 253px; vertical-align: middle; width: 440px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                        text-align: center">
                        Ingreso Directo de Entradas y Salidas del Personal</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="6">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnMasivo" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha"
                        Width="32px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                    <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="104px" BackColor="WhiteSmoke"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
                    <asp:TextBox ID="txtDia" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                        Width="104px" BackColor="WhiteSmoke"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="txtFecha" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
                    <asp:Button ID="btnGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Guardar" Width="104px" Enabled="False" />
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnMasivo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                    <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="66px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="2">
                    <asp:Button ID="btnMasivo" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Marcado de Salida Masiva" Width="176px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 20px" valign="top" colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 544px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px"><asp:GridView id="Flex" runat="server" Width="1330px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w2"><Columns>
<asp:BoundField DataField="c0" HeaderText="N&#176;">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="Apellidos y Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Hr. Trabajo Ent">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Hr. Trabajo Sal.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Min. Tolerancia">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6" HeaderText="Hr. Refrigerio">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c7" HeaderText="Asist&#237;o?">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:ButtonField CommandName="Si" Text="S&#237;">
<ControlStyle Font-Names="Arial" Font-Size="8pt"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="No" Text="No">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c8" HeaderText="Hr. Entrada">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField><ItemTemplate>
<asp:TextBox id="txtHEnt" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w35"></asp:TextBox><cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtHEnt" __designer:wfdid="w36" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c9" HeaderText="Hr. Salida">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField><ItemTemplate>
<asp:TextBox id="txtHSal" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w37"></asp:TextBox> <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="txtHSal" __designer:wfdid="w38" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c10" HeaderText="Min. Dif. Entrada">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c11" HeaderText="Min. Dif. Salida">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c12" HeaderText="Tarde">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c13" HeaderText="Min. Tarde">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c14" HeaderText="Hrs. Trabajadas">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c15" HeaderText="Hrs. Extras">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c16">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c17">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c18">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFecha"
        TargetControlID="txtFecha">
    </cc1:CalendarExtender>
</asp:Content>

