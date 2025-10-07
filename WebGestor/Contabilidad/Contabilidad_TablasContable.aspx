<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Contabilidad_TablasContable.aspx.vb" Inherits="Contabilidad_TablasContable" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="3" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Tablas Contables</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 105px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 105px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="400px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" ActiveTabIndex="2"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Asientos
                            
</HeaderTemplate>
<ContentTemplate>
<DIV><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="WIDTH: 480px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 25px; vertical-align: middle;" valign="top">
        <asp:DropDownList id="cboAño" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> <asp:Button id="btnANuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnANuevo_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> 
    </td>
</TR><TR>
    <td align="left" colspan="3" valign="top">
        <DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 500px; BORDER-BOTTOM: dimgray 1px outset" id="DIV1" runat="server"><asp:GridView id="FlexAsiento" runat="server" Width="500px" Font-Size="8pt" Font-Names="Arial" PageSize="7" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="ASIENTO_CODIGO" HeaderText="C&#243;digo">
<ItemStyle Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ASIENTO_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ASIENTO_PREFIJO" HeaderText="Prefijo">
<ItemStyle Width="40px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV>
    </td>
</TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 480px; HEIGHT: 22px" vAlign=top align=left>&nbsp;</TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 22px" valign="top">
        <TABLE style="WIDTH: 500px" id="FraIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=4 runat="server"><asp:Label id="lblAEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblA1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:TextBox id="txtACodigo" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" MaxLength="4"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblA3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Prefijo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:TextBox id="txtAPrefijo" runat="server" Width="298px" Font-Size="8pt" Font-Names="Arial" MaxLength="4"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblA2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=3 runat="server"><asp:TextBox id="txtADescripcion" runat="server" Width="438px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=center colSpan=3 runat="server"><asp:Button id="btnAGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnAGrabar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Grabar"></asp:Button> <asp:Button id="btnACancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnACancelar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TABLE>
    </td>
</TR><TR>
    <td align="left" colspan="3" style="height: 22px" valign="top">
        <asp:Label id="lblAError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
    </td>
</TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 480px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="lblNombre" runat="server" Width="134px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblpref" runat="server" Width="134px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                Documentos
                            
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="WIDTH: 480px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="WIDTH: 26px; HEIGHT: 10px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 25px" valign="top">
        <asp:DropDownList id="cboAñoDoc" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> <asp:Button id="btnDNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnDNuevo_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> 
    </td>
</TR><TR>
    <td align="left" colspan="3" valign="top">
        <DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 500px; BORDER-BOTTOM: dimgray 1px outset" id="DIV2" runat="server"><asp:GridView id="FlexDoc" runat="server" Width="500px" Font-Size="8pt" Font-Names="Arial" PageSize="7" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="DOC_CODIGO" HeaderText="C&#243;digo">
<ItemStyle Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DOC_DOCUMENTO" HeaderText="Descripci&#243;n">
<ItemStyle Width="350px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV>
    </td>
</TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 480px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 26px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 22px" valign="top">
        <TABLE style="WIDTH: 500px" id="FraDIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=4 runat="server"><asp:Label id="lblDEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblD1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:TextBox id="txtDCodigo" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" MaxLength="4"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblD2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=3 runat="server"><asp:TextBox id="txtDDescripcion" runat="server" Width="438px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=center colSpan=3 runat="server"><asp:Button id="btnDGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnDGrabar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Grabar"></asp:Button> <asp:Button id="btnDCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnDCancelar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TABLE>
    </td>
</TR><TR>
    <td align="left" colspan="3" style="height: 18px" valign="top">
        <asp:Label id="lblDError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
    </td>
</TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 480px; HEIGHT: 19px" vAlign=top align=left><asp:TextBox id="lblDDescripcion" runat="server" Font-Italic="True" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 26px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
                                Centro Costos
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="HEIGHT: 10px; width: 480px;" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 24px" valign="top">
        <asp:DropDownList id="cboAñoCC" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> <asp:Button id="btnCCNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCCNuevo_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> 
    </td>
</TR><TR>
    <td align="left" colspan="3" style="height: 200px" valign="top">
        <DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 500px; BORDER-BOTTOM: dimgray 1px outset" id="DIV3" runat="server"><asp:GridView id="FlexCC" runat="server" Width="500px" Font-Size="8pt" Font-Names="Arial" PageSize="7" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CCOSTO_ORGANIGRAMA" HeaderText="Organigrama">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_DESCRIPCION" HeaderText="Nombre de Centro de Costos">
<ItemStyle Width="290px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_NIVEL_ORDEN" HeaderText="Nivel">
<ItemStyle Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_CODIGO" HeaderText="C&#243;digo">
<ItemStyle Width="30px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
    </td>
</TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; width: 480px;" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 159px" valign="top">
        <TABLE style="WIDTH: 500px" id="FraCCIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=center colSpan=4 runat="server"><asp:Label id="lblCCEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblCC1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:TextBox id="txtCCCodigo" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblCC3" runat="server" Width="96px" Font-Size="8pt" Font-Names="Arial" Text="Orden Organigrama"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 290px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:TextBox id="txtCCOrden" runat="server" Width="288px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR id="Tr2" runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblCC4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nivel"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=3 runat="server"><asp:RadioButtonList id="optCCNivel" runat="server" Font-Size="8pt" Font-Names="Arial" RepeatDirection="Horizontal"><asp:ListItem Value="0">Principal</asp:ListItem>
<asp:ListItem Value="1">Sub-Centro</asp:ListItem>
<asp:ListItem Value="2">Registro</asp:ListItem>
</asp:RadioButtonList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblCC2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=3 runat="server"><asp:TextBox id="txtCCDescripcion" runat="server" Width="438px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=center colSpan=3 runat="server"><asp:Button id="btnCCGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCCGrabar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Grabar"></asp:Button> <asp:Button id="btnCCCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCCCancelar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TABLE>
    </td>
</TR><TR>
    <td align="left" colspan="3" style="height: 21px" valign="top">
        <asp:Label id="lblCCError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
    </td>
</TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; width: 480px;" vAlign=top align=left><asp:TextBox id="lblCCDescripcion" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCCuenta1" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCCuenta2" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCCuenta3" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCCuenta4" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCOrden" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCTieneHijos" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblCCNroNiveles" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False">4</asp:TextBox> <asp:TextBox id="lblCCNivel" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; width: 480px;" vAlign=top align=left><asp:TextBox id="txtCCMascara" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 65px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 65px; width: 480px;" vAlign=top align=left><cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtCCOrden" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99.99.99.99"></cc1:MaskedEditExtender> <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="txtCCMascara" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" MaskType="Number" Mask="99.99.99.99"></cc1:MaskedEditExtender> </TD><TD style="WIDTH: 25px; HEIGHT: 65px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
                                Flujo Caja
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 26px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="WIDTH: 480px; HEIGHT: 10px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 25px" valign="top">
        <asp:DropDownList id="cboAñoFC" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> <asp:Button id="btnFNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnFNuevo_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> 
    </td>
</TR><TR>
    <td align="left" colspan="3" style="height: 200px" valign="top">
        <DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; BORDER-LEFT: dimgray 1px outset; WIDTH: 500px; BORDER-BOTTOM: dimgray 1px outset" id="DIV4" runat="server"><asp:GridView id="FlexFC" runat="server" Width="500px" Font-Size="8pt" Font-Names="Arial" PageSize="7" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="FLUCAJA_CODIGO">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FLUCAJA_CODINTERNO" HeaderText="C&#243;digo">
<ItemStyle Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FLUCAJA_DESCRIPCION" HeaderText="Descipci&#243;n">
<ItemStyle Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FLUCAJA_TIPO" HeaderText="Tipo">
<ItemStyle Width="40px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
    </td>
</TR><TR><TD style="WIDTH: 26px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 480px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="3" style="height: 19px" valign="top">
        <TABLE style="WIDTH: 500px" id="FraFIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TR id="Tr1" runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" id="Td1" vAlign=top align=center colSpan=4 runat="server"><asp:Label id="lblFEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblF1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:TextBox id="txtFCodigo" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" MaxLength="6"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblF3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:RadioButtonList id="optFTipo" runat="server" Font-Size="8pt" Font-Names="Arial" RepeatDirection="Horizontal"><asp:ListItem Selected="True" Value="0">Ingreso</asp:ListItem>
<asp:ListItem Value="1">Egreso</asp:ListItem>
</asp:RadioButtonList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center runat="server"><asp:Label id="lblF2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px; TEXT-ALIGN: left" vAlign=top align=center colSpan=3 runat="server"><asp:TextBox id="txtFDescripcion" runat="server" Width="438px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 25px; TEXT-ALIGN: left" vAlign=top align=center runat="server"></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=center colSpan=3 runat="server"><asp:Button id="btnFGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Grabar" OnClick="btnFGrabar_Click"></asp:Button> <asp:Button id="btnFCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnFCancelar_Click" runat="server" CssClass="EstiloBoton" Width="51px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TABLE>
    </td>
</TR><TR>
    <td align="left" colspan="2" style="vertical-align: middle; height: 19px" valign="top">
        <asp:Label id="lblFError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
    </td>
    <TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD></TR><TR>
    <td align="left" colspan="2" style="vertical-align: middle; height: 20px" valign="top">
        <asp:TextBox id="lblFDescripcion" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="lblFCodigo" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> 
    </td>
    <TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel5" ID="TabPanel5"><HeaderTemplate>
    Tipo Cambio&nbsp;
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
            <tr>
                <td align="left" style="width: 80px; height: 10px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 30px; height: 10px">
                </td>
                <td align="left" style="width: 70px; height: 10px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 10px">
                </td>
                <td align="left" valign="top" style="width: 100px; height: 10px">
                </td>
                <td align="left" style="width: 80px; height: 10px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 100px; height: 10px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 80px; vertical-align: middle; height: 25px;" valign="top">
                    <asp:Label ID="lblTC2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Sistema"
                        Width="74px"></asp:Label>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 25px" valign="top">
                    <asp:TextBox ID="txtFechaSistema" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="90px"></asp:TextBox>
                </td>
                <td align="left" valign="top" style="vertical-align: middle; width: 70px; height: 25px">
                    <asp:Label ID="lblTC3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Hora Sistema"
                        Width="65px"></asp:Label>
                </td>
                <td align="left" valign="top" style="vertical-align: middle; width: 100px; height: 25px">
                    <asp:TextBox ID="txtHoraSistema" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="90px"></asp:TextBox>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="vertical-align: middle; width: 100px; height: 25px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 80px; vertical-align: middle; height: 22px;" valign="top"><asp:DropDownList id="cboAñoTC" runat="server" Width="72px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                </asp:DropDownList>
                </td>
                <td align="left" valign="top" style="vertical-align: middle; width: 30px; height: 22px">
                    <asp:Label ID="lblTC1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Mes"
                        Width="22px"></asp:Label>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:DropDownList id="cboMesTC" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                        <asp:ListItem Value="01">ENERO</asp:ListItem>
                        <asp:ListItem Value="02">FEBRERO</asp:ListItem>
                        <asp:ListItem Value="03">MARZO</asp:ListItem>
                        <asp:ListItem Value="04">ABRIL</asp:ListItem>
                        <asp:ListItem Value="05">MAYO</asp:ListItem>
                        <asp:ListItem Value="06">JUNIO</asp:ListItem>
                        <asp:ListItem Value="07">JULIO</asp:ListItem>
                        <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                        <asp:ListItem Value="09">SEPTIEMBRE</asp:ListItem>
                        <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                        <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                        <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" valign="top" style="vertical-align: middle; width: 100px; height: 22px">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" valign="top" style="vertical-align: middle; width: 100px; height: 22px">
                    <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" OnTick="Timer1_Tick">
                    </asp:Timer>
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: middle; height: 199px;" valign="top" colspan="7">
                    <div style="border-right: dimgray 1px outset; border-top: dimgray 1px outset; overflow: auto;
                        border-left: dimgray 1px outset; width: 350px; border-bottom: dimgray 1px outset">
                        <asp:GridView ID="FlexTC" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            Font-Names="Arial" Font-Size="8pt" PageSize="7" Width="350px">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPCAM_COMPRA" HeaderText="Valor Compra">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPCAM_VENTA" HeaderText="Valor Venta">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
                    <div style="text-align: left">
                        <table id="lblIngresoTC" runat="server" border="0" cellpadding="0" cellspacing="0"
                            style="width: 350px" visible="False">
                            <tr runat="server">
                                <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px"
                                    valign="top">
                                    <asp:Label ID="lblEtiquetaTC" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" align="left" style="vertical-align: middle; width: 100px; height: 22px"
                                    valign="top">
                                    <asp:Label ID="lblTC4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha"></asp:Label>
                                </td>
                                <td runat="server" align="left" style="vertical-align: middle; width: 125px; height: 22px;
                                    text-align: right" valign="top">
                                    <asp:Label ID="lblTC5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Compra"></asp:Label>
                                    <asp:Label ID="lblTC7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="S/."></asp:Label>
                                    &nbsp;</td>
                                <td runat="server" align="left" style="vertical-align: middle; width: 125px; height: 22px"
                                    valign="top">
                                    <asp:TextBox ID="txtTCCompra" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" align="left" style="vertical-align: middle; width: 100px; height: 22px"
                                    valign="top">
                                    <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                        Width="90px"></asp:TextBox>
                                </td>
                                <td runat="server" align="left" style="vertical-align: middle; width: 125px; height: 22px;
                                    text-align: right" valign="top">
                                    <asp:Label ID="lblTC6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Venta"></asp:Label>
                                    <asp:Label ID="lblTC8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="S/."></asp:Label>
                                    &nbsp;</td>
                                <td runat="server" align="left" style="vertical-align: middle; width: 125px; height: 22px"
                                    valign="top">
                                    <asp:TextBox ID="txtTCVenta" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" align="left" style="vertical-align: middle; width: 100px; height: 25px"
                                    valign="top">
                                </td>
                                <td runat="server" align="left" style="vertical-align: middle; width: 125px; height: 25px;
                                    text-align: right" valign="top">
                                </td>
                                <td runat="server" align="left" style="vertical-align: middle; width: 125px; height: 25px; text-align: right;"
                                    valign="top">
                                    <asp:Button ID="btnTCGrabar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Gray" OnClick="btnTCGrabar_Click" onmouseout="this.style.fontWeight='normal'"
                                        onmouseover="this.style.fontWeight='bolder'" Text="Grabar" Width="51px" />
                                    <asp:Button ID="btntcCancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Gray" OnClick="btntcCancelar_Click" onmouseout="this.style.fontWeight='normal'"
                                        onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="51px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblTCError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</DIV>
</ContentTemplate>
</cc1:TabPanel>
    <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="TabPanel6">
        <HeaderTemplate>
            Periodos
        </HeaderTemplate>
        <ContentTemplate>
            <div style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                    <tr>
                        <td align="left" style="width: 80px; height: 10px" valign="top">
                        </td>
                        <td align="left" style="width: 80px; height: 10px" valign="top">
                        </td>
                        <td align="left" style="width: 130px; height: 10px" valign="top">
                        </td>
                        <td align="left" style="width: 60px; height: 10px" valign="top">
                        </td>
                        <td align="left" style="width: 40px; height: 10px" valign="top">
                        </td>
                        <td align="left" style="width: 140px; height: 10px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                            <asp:DropDownList id="cboAñoPP" runat="server" Width="72px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboAñoPP_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                            <asp:Label ID="lblP1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Periodo"
                                Width="75px"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                            <asp:DropDownList ID="cboTipoPer" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="127px">
                            </asp:DropDownList></td>
                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                            <asp:Label ID="lblP2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="# Periodos"
                                Width="53px"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                            <asp:TextBox ID="txtNPeriodo" runat="server" BorderColor="Black" BorderStyle="Outset"
                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="31px"></asp:TextBox></td>
                        <td align="left" style="vertical-align: middle; width: 140px; height: 22px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 200px" valign="top" colspan="6">
                            <div id="DIV5" runat="server" style="border-right: dimgray 1px outset; border-top: dimgray 1px outset;
                                overflow: auto; border-left: dimgray 1px outset; width: 500px; border-bottom: dimgray 1px outset;
                                position: static; height: 290px;">
                                <asp:GridView ID="FlexPP" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                    Font-Size="8pt" PageSize="12" Width="500px"  >
                                    <Columns>
                                        <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="38px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" CommandName="PActual" Text="P. Actual">
                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" CommandName="AbrirP" Text="Abrir P.">
                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" CommandName="CerrarP" Text="Cerrar P.">
                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="PER_PERIODO" HeaderText="Per.">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PER_NOMBRE" HeaderText="Nombre">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHAINI" HeaderText="F. Inicial">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHAFIN" HeaderText="F. Final">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Estado">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PER_ESTADO">
                                            <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANT_COMP">
                                            <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PER_ACTUAL">
                                            <ItemStyle Width="0px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="2">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha de inicio con el Sistema"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                            <asp:TextBox ID="txtFechaInicio" runat="server" BorderColor="Black" BorderStyle="Outset"
                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Width="82px"></asp:TextBox></td>
                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 140px; height: 22px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 140px; height: 22px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="6">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                                Text="NOTA : Se recomienda definir los periodos una única vez por cada año contable. No podrá eliminar los  periodos definidos si hubiera por lo mínimo un comprobante ingresado. Por cada comprobante ingresado en un determinado periodo a este NO se le podrá editar sus fechas."
                                Width="503px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 140px; height: 22px" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                            <asp:Label ID="lblPPError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 105px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 19px;" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#C00000"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 20px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

