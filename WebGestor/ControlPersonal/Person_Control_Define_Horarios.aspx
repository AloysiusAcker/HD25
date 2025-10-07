<%--<%@ Page Language="VB" MasterPageFile="~/ControlPersonal/PagPrincipal_CPersonal.master" AutoEventWireup="false" CodeFile="Person_Control_Define_Horarios.aspx.vb" Inherits="Person_Control_Define_Horarios" title="GestorPlus" %>--%>
<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Person_Control_Define_Horarios.aspx.vb" Inherits="Person_Control_Define_Horarios" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td align="left" style="width: 25px; height: 50px" valign="top">
            </td>
            <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 14pt; left: 253px; vertical-align: middle; width: 544px; color: gray;
                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                    text-align: center">
                    Define Horario de Trabajo por Cargo</div>
            </td>
            <td align="left" style="width: 25px; height: 50px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="7" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                <asp:UpdatePanel id="UpdatePanel4" runat="server">
                    <contenttemplate>
                <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red" Width="536px"></asp:Label>
</contenttemplate>
                    <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                </asp:UpdatePanel></td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="3">
                <asp:UpdatePanel id="UpdatePanel3" runat="server">
                    <contenttemplate>
<asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label> 
</contenttemplate>
                    <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                <asp:Button ID="btnNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Gray" Text="Nuevo" Width="78px" /></td>
            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                    Text="Listar" Width="78px" /></td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                    <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 548px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px"><asp:GridView id="Flex" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" Font-Overline="False" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="HOR_CARGO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOM_CARGO" HeaderText="Cargo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_ENTRADA" HeaderText="Hora Entrada">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_SALIDA" HeaderText="Hora Salida">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
</asp:GridView></DIV>
</contenttemplate>
                    <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 5px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 100px; height: 5px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 200px; height: 5px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 90px; height: 5px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 80px; height: 5px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 80px; height: 5px" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 5px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                <div style="text-align: left">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 544px" id="lblIngreso" runat="server" visible="false">
                        <tr>
                            <td align="left" colspan="1" style="vertical-align: middle; width: 383px; height: 22px"
                                valign="top">
                                <asp:Label ID="lblIngDatos" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Maroon"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                            </td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 383px; height: 22px" valign="top">
                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cargo"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="H. Entrada"
                                    Width="56px"></asp:Label></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="H. Salida"
                                    Width="48px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 383px; height: 22px" valign="top">
                                <asp:DropDownList ID="cboCargo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="380px">
                                </asp:DropDownList></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:TextBox ID="txtHEntrada" runat="server" Font-Names="Arial" Font-Size="8pt" Width="72px"></asp:TextBox></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:TextBox ID="txtHSalida" runat="server" Font-Names="Arial" Font-Size="8pt" Width="72px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" style="vertical-align: middle; width: 383px; height: 22px" valign="top">
                            </td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnGuardar_Click" Width="76px" /></td>
                            <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnCancelar_Click" Width="76px" /></td>
                        </tr>
                    </table><cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="txtHSalida" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender> <cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtHEntrada" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>&nbsp;</div>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px" valign="top">
            </td>
            <td align="left" style="width: 100px" valign="top">
            </td>
            <td align="left" style="width: 200px" valign="top">
            </td>
            <td align="left" style="width: 90px" valign="top">
            </td>
            <td align="left" style="width: 80px" valign="top">
            </td>
            <td align="left" style="width: 80px" valign="top">
            </td>
            <td align="left" style="width: 25px" valign="top">
            </td>
        </tr>
    </table>
    &nbsp;
</asp:Content>

