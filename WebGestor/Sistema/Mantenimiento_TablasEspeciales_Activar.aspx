<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Mantenimiento_TablasEspeciales_Activar.aspx.vb" Inherits="Mantenimiento_TablasEspeciales_Activar" title="Sistema - Activación de Tablas Externas e Internas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td align="left" colspan="13" style="height: 50px; text-align: center" valign="middle">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 14pt; left: 253px; vertical-align: middle; width: 550px; color: gray;
                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                    text-align: center">
                    Activar
                    Tablas Especiales</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="13" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="11" style="height: 22px" valign="middle">
                <asp:UpdatePanel id="UpdatePanel5" runat="server">
                    <contenttemplate>
<asp:Label id="lblError" runat="server" Width="544px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
</contenttemplate>
                    <triggers>
<asp:AsyncPostBackTrigger ControlID="opcTablas" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGrabaNuevaTabla" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexTablasEspeciales" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                </asp:UpdatePanel></td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="11" style="height: 22px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
<asp:RadioButtonList id="opcTablas" runat="server" Width="328px" Height="22px" Font-Size="8pt" Font-Names="Arial" RepeatDirection="Horizontal" AutoPostBack="True"><asp:ListItem Selected="True" Value="0">Tabla de Opciones Externas</asp:ListItem>
<asp:ListItem Value="1">Tabla de Opciones Internas</asp:ListItem>
</asp:RadioButtonList> 
</ContentTemplate>
                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="opcTablas" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="11" style="height: 22px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
<asp:Label id="lblRegistroTabla" runat="server" Width="544px" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w2"></asp:Label> 
</ContentTemplate>
                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnGrabaNuevaTabla" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="opcTablas" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="11" style="height: 22px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<DIV style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; OVERFLOW: auto; BORDER-LEFT: 1px outset; WIDTH: 548px; BORDER-BOTTOM: 1px outset; POSITION: static; HEIGHT: 350px" id="div1" runat="server"><asp:GridView id="FlexTablasEspeciales" runat="server" Width="560px" Font-Size="8pt" Font-Names="Arial" OnRowCommand="FlexTablasEspeciales_RowCommand" AutoGenerateColumns="False" __designer:wfdid="w5"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle ForeColor="White" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="TABLAS_CODIGO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TABLAS_DESCRIPCION" HeaderText="Nombre / Descripcion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TABLAS_SYS_EST" HeaderText="Activo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TABLAS_VER" HeaderText="Se Visualiza">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV>
</ContentTemplate>
                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="opcTablas" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 130px" valign="middle">
            </td>
            <td align="left" colspan="11" style="height: 130px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
<DIV style="WIDTH: 548px; HEIGHT: 120px" id="lblNuevaTabla" runat="server" visible="False"><TABLE style="WIDTH: 548px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=3><asp:Label id="Label19" runat="server" Width="128px" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Datos por Completar"></asp:Label></TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtVerTabla" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtUso" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD><TD style="WIDTH: 68px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label20" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:TextBox id="txtCodTabla" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label1" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Ver Tabla"></asp:Label></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:DropDownList id="cboVerTabla" runat="server" Width="100px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                                    <asp:ListItem>SI</asp:ListItem>
                                    <asp:ListItem>NO</asp:ListItem>
                                </asp:DropDownList></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label2" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" Text="Uso"></asp:Label></TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:DropDownList id="cboUsoTabla" runat="server" Width="166px" Height="24px" Font-Size="8pt" Font-Names="Arial"><asp:ListItem>Externo</asp:ListItem>
<asp:ListItem>Interno</asp:ListItem>
</asp:DropDownList></TD></TR><TR><TD style="WIDTH: 70px; HEIGHT: 21px" vAlign=middle align=left><asp:Label id="Label21" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD style="HEIGHT: 21px" vAlign=middle align=left colSpan=7><asp:TextBox id="txtDescTabla" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnCancelar" runat="server" Width="64px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="WIDTH: 68px; HEIGHT: 22px" vAlign=middle align=left><asp:Button id="btnGrabaNuevaTabla" onclick="btnGrabaNuevaTabla_Click" runat="server" Width="64px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Grabar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="FlexTablasEspeciales" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnGrabaNuevaTabla" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 130px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 50px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
    </table>
</asp:Content>

