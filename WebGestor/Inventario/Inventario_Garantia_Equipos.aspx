<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Garantia_Equipos.aspx.vb" Inherits="Inventario_Garantia_Equipos" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; text-align: center">
                        Garantía de Equipos</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px;" valign="top">
                    <asp:Button ID="btnExportar" runat="server" CssClass="EstiloBoton_Ac" OnClick="btnExportar_Click"
                        Style="left: 608px; top: 380px" Text="Exportar" Width="88px" /></td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px;" valign="top">
                    &nbsp;</td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="5">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="0" AutoPostBack="True" __designer:wfdid="w16"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
Garantía
</HeaderTemplate>
<ContentTemplate>
<div style="TEXT-ALIGN: left"><table style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><tbody><tr><td style="vertical-align : middle; width : 60px; HEIGHT: 10px" vAlign="top" align="left"></td><td style="vertical-align : middle; width : 100px; height : 10px" valign="top" align="left"></td><td style="vertical-align : middle; width: 30px; height: 10px; text-align:left" valign="top" ></td><td style="VERTICAL-ALIGN: middle; WIDTH: 240px; HEIGHT: 10px" vAlign=top align=left></td><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 10px" vAlign=top align=left></TD></tr><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left>
    <asp:Label id="Proveedor" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Proveedor" __designer:wfdid="w17"></asp:Label>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtProvRuc" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w18"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnBusProveedor" runat="server" CssClass="EstiloBoton_Ac" Width="25px" Text="..." __designer:wfdid="w19"></asp:Button>
 </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtProvRazonSocial" runat="server" Width="328px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w20"></asp:TextBox>
 </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 21px" vAlign=top align=left><asp:Label id="Articulo" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Articulo" __designer:wfdid="w5"></asp:Label>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 21px" vAlign=top align=left><asp:TextBox id="txtArtCodigo" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w22"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 21px" vAlign=top align=left><asp:Button id="btnBusArticulo" runat="server" CssClass="EstiloBoton_Ac" Width="25px" Text="..." __designer:wfdid="w23"></asp:Button>
 </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 21px" vAlign=top align=left colSpan=2><asp:TextBox id="txtArtDescripcion" runat="server" Width="328px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w24" ReadOnly="True"></asp:TextBox>
 </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Serie" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Nro Serie" __designer:wfdid="w9"></asp:Label>
 </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtSerie" runat="server" Width="122px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w26"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 240px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:TextBox id="txtProvCodigo" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w27" Visible="False"></asp:TextBox>
 &nbsp; </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Button id="btnListar" onclick="btnListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="88px" Text="Listar" __designer:wfdid="w28"></asp:Button>
 </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w29" ForeColor="Maroon"></asp:Label>
 </TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w30" ForeColor="Red"></asp:Label>
 </TD></TR><TR><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 520px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 400px"><asp:GridView id="Flex" runat="server" Width="820px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w31" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="GARANTIA_CODIGO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROVEEDOR" HeaderText="Proveedor">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ARTDESCRIPCION" HeaderText="Art&#237;culo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GARANTIA_SERIE" HeaderText="Nro Serie">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NRO_PLACA" HeaderText="Nro Placa">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_COMPRA" HeaderText="Fec. Compra">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_COMPRA_FIN" HeaderText="Fin Garant&#237;a">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIEMPO_GARANTIA" HeaderText="Tiempo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GARANTIA_FACTURA" HeaderText="Nro Factura">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CONDICION" HeaderText="Condici&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView>
 </DIV></TD></TR></tbody></table></div><cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" __designer:wfdid="w32" TargetControlID="btnBusArticulo" Enabled="True" DynamicServicePath="" Y="200" X="250" CancelControlID="btnCerrarArt" BackgroundCssClass="modalBackground" PopupControlID="Panel1"></cc1:ModalPopupExtender> <asp:Panel id="Panel1" runat="server" __designer:wfdid="w34"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 400px; BORDER-BOTTOM: gray 1px outset" cellSpacing=0 cellPadding=0 border=0 __designer:dtid="1407374883553383"><TBODY><TR __designer:dtid="1407374883553384"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: center" vAlign=top align=left colSpan=5 __designer:dtid="1407374883553385"><asp:Label id="lblP3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Búsqueda de Artículos" __designer:wfdid="w46" ForeColor="Maroon"></asp:Label>
 </TD></TR><TR __designer:dtid="1407374883553386"><TD style="WIDTH: 25px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="1407374883553387"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Label id="lblP2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código" __designer:wfdid="w41"></asp:Label>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 180px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:TextBox id="txtPArtCodigo" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w43"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: center" vAlign=top align=left __designer:dtid="1407374883553388"><DIV style="VERTICAL-ALIGN: middle; TEXT-ALIGN: right" __designer:dtid="1407374883553389"><asp:Button id="btnCerrarArt" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Font-Size="8pt" Font-Names="Arial" __designer:dtid="1407374883553391" Text="Cerrar" __designer:wfdid="w36" OnClick="btnCerrarArt_Click"></asp:Button>
&nbsp;&nbsp;</DIV></TD><TD style="WIDTH: 25px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="1407374883553392"></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Label id="lblP1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción" __designer:wfdid="w42"></asp:Label>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 180px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:TextBox id="txtPArtDescripcion" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w44"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnListarArt" onclick="btnListarArt_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Font-Size="8pt" Font-Names="Arial" __designer:dtid="1407374883553390" Text="Listar" __designer:wfdid="w35"></asp:Button>
&nbsp;&nbsp; </TD><TD style="WIDTH: 25px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD></TR><TR __designer:dtid="1407374883553393"><TD style="WIDTH: 25px; HEIGHT: 250px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="1407374883553394"></TD><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 250px; BACKGROUND-COLOR: darkgray" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 350px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV3" runat="server"><asp:GridView id="FlexArt" runat="server" Width="430px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w45" AutoGenerateColumns="False" BorderStyle="Outset" BorderWidth="1px" UseAccessibleHeader="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" ForeColor="Gray" Width="30px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Nombres" ReadOnly="True">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="350px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle BorderWidth="1px" BorderStyle="Outset"></HeaderStyle>

<PagerStyle VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></PagerStyle>
</asp:GridView>
 </DIV>
</TD><TD style="WIDTH: 25px; HEIGHT: 250px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="1407374883553399"></TD></TR><TR __designer:dtid="1407374883553400"><TD style="HEIGHT: 20px; BACKGROUND-COLOR: darkgray" vAlign=top align=left colSpan=5 __designer:dtid="1407374883553401"></TD></TR></TBODY></TABLE></asp:Panel> <cc1:ModalPopupExtender id="ModalPopupExtender2" runat="server" __designer:wfdid="w47" TargetControlID="btnBusProveedor" Enabled="True" DynamicServicePath="" Y="200" X="250" CancelControlID="btnCerrarProv" BackgroundCssClass="modalBackground" PopupControlID="Panel2" CacheDynamicResults="True"></cc1:ModalPopupExtender> <asp:Panel id="Panel2" runat="server" __designer:wfdid="w48"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 400px; BORDER-BOTTOM: gray 1px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 20px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 25px; TEXT-ALIGN: center" vAlign=top align=left><asp:Label id="lbltipoper" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Busqueda de Proveedor" __designer:wfdid="w67" ForeColor="Maroon"></asp:Label>
 </TD><TD style="WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblruc" runat="server" Font-Size="8pt" Font-Names="Arial" Text="RUC" __designer:wfdid="w68"></asp:Label>
 </TD><TD style="WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtPRuc" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w69"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button accessKey=" " id="btnCerrarProv" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Cerrar" __designer:wfdid="w70" OnClick="btnCerrarProv_Click"></asp:Button>
 </TD><TD style="WIDTH: 20px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px" vAlign=top align=left><asp:Label id="lbldesc" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción" __designer:wfdid="w71"></asp:Label>
 </TD><TD style="WIDTH: 200px; HEIGHT: 20px" vAlign=top align=left><asp:TextBox id="txtPRazonSocial" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w72"></asp:TextBox>
 </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnListarProv" onclick="btnListarProv_Click" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Listar" __designer:wfdid="w73"></asp:Button>
 </TD><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 266px" vAlign=top align=left></TD><TD style="HEIGHT: 266px" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 240px" id="DIV4" runat="server"><asp:GridView id="FlexProv" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w76" AutoGenerateColumns="False" OnSelectedIndexChanged="FlexProv_SelectedIndexChanged"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="30px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="PERSONA_RUC" HeaderText="CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_RAZON_SOCIAL" HeaderText="DESCRIPCION">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView>
 </DIV>
</TD><TD style="WIDTH: 20px; HEIGHT: 266px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 200px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE></asp:Panel> <BR />
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

