<%@ Page Language="VB" MasterPageFile="~/SPagPrincipal_A.master" AutoEventWireup="false" CodeFile="SIntegral_Servicio.aspx.vb" Inherits="Servicios_SIntegral_Servicio" title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 15pt; vertical-align: middle; width: 536px; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 18px; text-align: center">
                        Publicar Servicios</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="background-image: url(../Fotos/Linea_Gris.bmp);
                    height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 135px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 135px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="1"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Servicios
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 110px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" Width="536px" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w191"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Sector Económico" __designer:wfdid="w192"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboSector" runat="server" Width="368px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w193"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Listar" __designer:wfdid="w194"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo" __designer:wfdid="w195"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboTipo" runat="server" Width="368px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w196"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="Button2" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Button" Visible="False" __designer:wfdid="w197"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" __designer:wfdid="w198"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 528px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 208px"><asp:GridView id="Flex" runat="server" Width="920px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w199" OnSelectedIndexChanged="Flex_SelectedIndexChanged"><Columns>
<asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODIGO" HeaderText="Nro. Servicio">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SECTOR" HeaderText="Sector Econ&#243;mico">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO" HeaderText="Tipo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERVDET_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROVEEDOR" HeaderText="Proveedor">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:ImageField DataImageUrlField="SERVDET_FOTO" DataImageUrlFormatString="../ServicioIntegral/Imagenes/{0}" HeaderText="Imagen">
<ControlStyle Height="50px" Width="150px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:ImageField>
<asp:BoundField DataField="SERVDET_ITEM">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 110px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
Detalle
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 90px" vAlign=top align=left></TD><TD style="WIDTH: 145px" vAlign=top align=left></TD><TD style="WIDTH: 65px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=6><asp:Label id="lblErrorDet" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w256"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label10" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Datos del Servicio" ForeColor="Maroon" __designer:wfdid="w228"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label1" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Nro. Servicio" __designer:wfdid="w229"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 145px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtNroServicio" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w230" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="txtSector" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Sector Económico" __designer:wfdid="w231"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:TextBox id="txtSectorEconomico" runat="server" Width="440px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w232" ReadOnly="True"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo" __designer:wfdid="w233"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:TextBox id="txtTipo" runat="server" Width="440px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w234" ReadOnly="True"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción" __designer:wfdid="w235"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:TextBox id="txtDescripcion" runat="server" Width="440px" Height="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w236" ReadOnly="True" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Proveedor" __designer:wfdid="w237"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:TextBox id="txtProveedor" runat="server" Width="440px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w238" ReadOnly="True"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Dirección" __designer:wfdid="w239"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:TextBox id="txtDireccion" runat="server" Width="440px" Height="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w240" ReadOnly="True" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label7" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Observación" __designer:wfdid="w241"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:TextBox id="txtObservacion" runat="server" Width="440px" Height="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w242" ReadOnly="True" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Precio" __designer:wfdid="w243"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 145px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtPrecioProveedor" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w244" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Estado" __designer:wfdid="w245"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="txtEstado" runat="server" Width="230px" __designer:wfdid="w246"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label9" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Datos de la Publicación" ForeColor="Maroon" __designer:wfdid="w247"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label13" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Precio" __designer:wfdid="w248"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 145px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtPrecio" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w249"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label11" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Fec. Inicia" __designer:wfdid="w250"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtFecInicia" runat="server" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w251"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label12" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Fec. Termina" __designer:wfdid="w252"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtFecTermina" runat="server" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w253"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 145px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnRegresar" onclick="btnRegresar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" Text="Regresar" __designer:wfdid="w254"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPublicar" onclick="btnPublicar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" Text="Publicar" __designer:wfdid="w255"></asp:Button> </TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

