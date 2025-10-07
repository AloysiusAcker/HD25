<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SIntegral_Servicio_Proveedor.aspx.vb" Inherits="Servicios_SIntegral_Servicio_Proveedor" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: left">
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress" style="left: 0px; top: 0px">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div style="position: relative; top: 30%; text-align: center;">
                                    &nbsp;<img src="../Fotos/5.gif" /></div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 15pt; vertical-align: middle; width: 550px; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 18px; text-align: center">
                        Relacionar Servicios - Proveedor</div>
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
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 5px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" ActiveTabIndex="0"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Relación
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 90px" vAlign=top align=left></TD><TD style="WIDTH: 120px" vAlign=top align=left></TD><TD style="WIDTH: 120px" vAlign=top align=left></TD><TD style="WIDTH: 130px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w3" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq1" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w4" Text="Sector Económico"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboBusSecEconomico" runat="server" Width="368px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w5"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" onclick="btnListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w6" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnAgregar" onclick="btnAgregar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w7" Text="Agregar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 534px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 192px"><asp:GridView id="Flex" runat="server" Width="800px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w8" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c1" HeaderText="Cod. Provee.">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Proveedor">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Sector Economico">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Nombre del Servicio">
<ItemStyle Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Descripcion del Servicio">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" id="lblEtqRelacionar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 290px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5 runat="server"><asp:Label id="lblEtq2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w9" ForeColor="Maroon" Text="Relacionar Servicio"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq3" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w10" Text="Proveedor"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtProveedor" runat="server" Width="412px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w11" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnBusProveedor" runat="server" CssClass="EstiloBoton_Ac" Width="25px" __designer:wfdid="w12" Text="..."></asp:Button> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq4" runat="server" Width="88px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w13" Text="Sector Económico"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:DropDownList id="cboSecEconomico" runat="server" Width="368px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w14"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Button id="btnIngListar" onclick="btnIngListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="74px" __designer:wfdid="w15" Text="Listar"></asp:Button> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5 runat="server"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 532px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 160px"><asp:GridView id="FlexServicio" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w16" AutoGenerateColumns="False"><Columns>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="chkServ" runat="server" Width="25px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w2"></asp:CheckBox> 
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c2" HeaderText="Nombre del Servicio">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="170px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Descripci&#243;n del Servicio">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 290px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodProveedor" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w17"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w18" Text="Cancelar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Button id="btnGuardar" runat="server" CssClass="EstiloBoton_Ac" Width="74px" __designer:wfdid="w19" Text="Guardar" OnClick="btnGuardar_Click"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV><cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" __designer:wfdid="w20" TargetControlID="btnBusProveedor" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="200" X="300" CancelControlID="btnBusCerrar" BackgroundCssClass="modalBackground" PopupControlID="pnPersona"></cc1:ModalPopupExtender> <asp:Panel id="pnPersona" runat="server" __designer:wfdid="w21"><TABLE style="BORDER-RIGHT: black 1px outset; BORDER-TOP: black 1px outset; LEFT: 503px; BORDER-LEFT: black 1px outset; WIDTH: 450px; BORDER-BOTTOM: black 1px outset; TOP: 541px" cellSpacing=0 cellPadding=0 border=0 __designer:dtid="281474976710685"><TBODY><TR __designer:dtid="281474976710686"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 26px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: center" vAlign=top align=left colSpan=5 __designer:dtid="281474976710687"><asp:Label id="lblEtq5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:dtid="281474976710688" __designer:wfdid="w22" Text="Relación de Proveedores"></asp:Label> </TD></TR><TR __designer:dtid="281474976710689"><TD style="WIDTH: 25px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="281474976710690"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Label id="lblEtq6" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w23" Text="Ap. Paterno"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 250px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="281474976710691"><asp:TextBox id="txtBusApePat" runat="server" Width="240px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w24"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Button id="btnBusCerrar" onclick="btnBusCerrar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:dtid="281474976710700" __designer:wfdid="w25" ForeColor="Gray" Text="Cerrar"></asp:Button> </TD><TD style="WIDTH: 25px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left __designer:dtid="281474976710697"></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left>&nbsp;</TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 250px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left>&nbsp;</TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Button id="btnListarProvee" onclick="btnListarProvee_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w26" Text="Listar"></asp:Button> </TD><TD style="WIDTH: 25px; HEIGHT: 22px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 200px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD><TD style="HEIGHT: 200px; BACKGROUND-COLOR: darkgray" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; FONT-SIZE: 8pt; VERTICAL-ALIGN: middle; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 392px; BORDER-BOTTOM: darkgray 1px outset; FONT-FAMILY: Arial; HEIGHT: 198px; TEXT-ALIGN: center" id="DIV2" runat="server" __designer:dtid="281474976710692"><asp:GridView id="FlexP" runat="server" Width="500px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w103" AutoGenerateColumns="False" PageSize="5" OnSelectedIndexChanged="FlexP_SelectedIndexChanged"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="PROVEE_CODIGO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROVEE_APEPAT" HeaderText="Ap. Paterno">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROVEE_APEMAT" HeaderText="Ap. Materno">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROVEE_NOMBRE" HeaderText="Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>
</asp:GridView> &nbsp;<BR __designer:dtid="281474976710696" /></DIV></TD><TD style="WIDTH: 25px; HEIGHT: 200px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD></TR><TR __designer:dtid="281474976710698"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: center" vAlign=top align=left colSpan=5 __designer:dtid="281474976710699"></TD></TR></TBODY></TABLE></asp:Panel> 
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
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 110px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

