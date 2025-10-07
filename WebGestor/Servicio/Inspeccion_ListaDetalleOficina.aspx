<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_ListaDetalleOficina.aspx.vb" Inherits="Inspeccion_ListaDetalleOficina" title="Servicio - Detalle de Oficina" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>    
    <div style="text-align: left">
     <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="10">
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
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 400px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; text-align: center; left: 253px; top: 275px;">
                        Detalle de la Oficina</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:Label id="lblError" runat="server" Width="484px" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w219"></asp:Label> 
</ContentTemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 310px; height: 11px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel10" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtUbicacion" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w7"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 80px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblArticulo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Text="Oficina" Width="36px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel9" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtUbiCodigo" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke" __designer:wfdid="w17" OnTextChanged="txtUbiCodigo_TextChanged"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: center"
                    valign="top">
                    <asp:Button ID="btnUbica" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="..." Width="22px" /></td>
                <td align="left" style="vertical-align: middle; width: 310px; height: 22px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtUbiDescripcion" runat="server" Width="304px" Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke" __designer:wfdid="w18"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 80px; height: 22px; vertical-align: middle; text-align: right;" valign="top">
                    <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Listar"
                        Width="76px" ForeColor="Gray" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel15" runat="server">
                        <contenttemplate>
<asp:Label id="lblDatos" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Datos de la Oficina" ForeColor="Maroon" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 200px;" valign="top">
                </td>
                <td align="left" colspan="5" valign="top" style="height: 200px">
                    <div style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
<DIV style="WIDTH: 100px; HEIGHT: 100px"><DIV style="TEXT-ALIGN: left"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 550px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="lblDatoOf" runat="server" Visible="false"><TABLE style="WIDTH: 550px" id="LLLL" cellSpacing=0 cellPadding=0 border=0 Visible="true"><TBODY><TR><TD accessKey="lblDatoOf" vAlign=top align=left><asp:DetailsView id="FlexDet" runat="server" Width="544px" Height="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w51" Visible="False" Font-Overline="False" AutoGenerateRows="False"><Fields>
<asp:BoundField DataField="c0" HeaderText="Oficina">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="460px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="Direcci&#243;n">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Gerente">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Sub-Gerente">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Tel&#233;fono">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Tablero El&#233;ctrico">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6" HeaderText="Tipificaci&#243;n">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c7" HeaderText="Pendiente Obs">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="Red" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c8" HeaderText="Responsable">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c9" HeaderText="Soluci&#243;n Posible">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
</Fields>
</asp:DetailsView> <asp:DetailsView id="FlexDet2" runat="server" Width="544px" Height="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w51" Visible="False" Font-Overline="False" AutoGenerateRows="False"><Fields>
<asp:BoundField DataField="c0" HeaderText="Oficina">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="460px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="Direcci&#243;n">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Gerente">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Sub-Gerente">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Tel&#233;fono">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Tablero El&#233;ctrico">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6" HeaderText="Tipificaci&#243;n">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c7" HeaderText="Pendiente Obs">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c8" HeaderText="Responsable">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c9" HeaderText="Soluci&#243;n Posible">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
</Fields>
</asp:DetailsView></TD></TR></TBODY></TABLE></DIV></DIV></DIV>
</ContentTemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel>
                        &nbsp;</div>
                </td>
                <td align="left" style="width: 25px; height: 200px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <asp:UpdatePanel id="UpdatePanel13" runat="server">
                        <contenttemplate>
<asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" __designer:wfdid="w220" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 548px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="lblequipo" runat="server" __designer:dtid="281474976710732" Visible="false"><asp:GridView id="Flex" runat="server" Width="1120px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" __designer:wfdid="w3" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="COD_ARTICULO" HeaderText="Art&#237;culo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="310px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NRO_IP" HeaderText="Numero IP">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROV_NOMBRE" HeaderText="Proveedor">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROV_DIRECCION" HeaderText="Direcci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROV_TELEFONO" HeaderText="Tel&#233;fono">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_SALIDA_FIN" HeaderText="Fecha Garant&#237;a">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CONDICION" HeaderText="Condici&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView></DIV>
</contenttemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 25px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
<asp:Label id="lblRegVisitas" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" __designer:wfdid="w222" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 1px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 548px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="lblVisita" runat="server" Visible="false"><asp:GridView id="FlexVisita" runat="server" Width="1280px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" __designer:wfdid="w224" AutoGenerateColumns="False" PageSize="8"><Columns>
<asp:BoundField DataField="NRO_VISITA" HeaderText="Nro Visita">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO" HeaderText="Tipo Visita">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_PROG" HeaderText="Fecha Prog.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_PROG" HeaderText="Hora Prog.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="COD_OFICINA" HeaderText="Oficina">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE_OFICINA" HeaderText="Nombre Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_TRABREALIZADO" HeaderText="Trabajo a Realizar / Realizado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DIRECCION" HeaderText="Direcci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Left" VerticalAlign="Middle"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView></DIV>
</contenttemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 1px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 25px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblRegistroDoc" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" __designer:wfdid="w225" Visible="False"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 1px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 548px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="lblDocumento" runat="server" Visible="false"><asp:GridView id="FlexDoc" runat="server" Width="800px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w226" AutoGenerateColumns="False" UseAccessibleHeader="False"><Columns>
<asp:BoundField DataField="TEMA_AYUDA_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Nombre del Documento"><ItemTemplate>
                                <div id="Doc" runat="server" style="width: 250px; height: 22px">
                                </div>
                            
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="TEMA_AYUDA_DESCRIPCION" HeaderText="Descripcion">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Fecha" HeaderText="F. Ingreso">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="COD_OFICINA" HeaderText="Cod. Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="230px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView></DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbiCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 1px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 310px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="height: 19px" valign="top" colspan="5">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
        <div style="text-align: center">
            &nbsp;&nbsp;</div>
        <asp:Panel ID="Panel2" runat="server">
            <div style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; background-color: darkgray; border-right: gray 2px outset; border-top: gray 2px outset; border-left: gray 2px outset; border-bottom: gray 2px outset;">
                    <tr>
                        <td align="left" style="width: 25px; height: 30px" valign="middle">
                        </td>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 30px; text-align: center"
                            valign="middle">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Maroon" Text="Busqueda de Centro de Costos"></asp:Label></td>
                        <td align="left" style="width: 25px; height: 30px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel11" runat="server">
                                <contenttemplate>
                            <asp:TextBox ID="txtBusCod" runat="server" Font-Names="Arial" Font-Size="8pt" Width="270px"></asp:TextBox>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                            <asp:Button ID="btnUbiCerrar" runat="server" BackColor="LightGray" BorderColor="Silver"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Cerrar" Width="80px" /></td>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"
                                Width="60px"></asp:Label></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel12" runat="server">
                                <contenttemplate>
                            <asp:TextBox ID="txtBusDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="270px"></asp:TextBox>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                            <asp:Button ID="btnUbiListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Listar" Width="80px" /></td>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px" valign="middle">
                        </td>
                        <td align="left" colspan="3" valign="middle">
                            <asp:UpdatePanel id="UpdatePanel7" runat="server">
                                <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 444px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" Font-Overline="False" OnSelectedIndexChanged="FlexUbicacion_SelectedIndexChanged"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="30px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CODIGO">
<ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 19px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="width: 70px; height: 19px">
                        </td>
                        <td align="left" valign="middle" style="width: 280px; height: 19px">
                        </td>
                        <td align="left" valign="middle" style="width: 100px; height: 19px">
                        </td>
                        <td align="left" style="width: 25px; height: 19px;" valign="middle">
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnUbica" CacheDynamicResults="True" CancelControlID="btnUbiCerrar" PopupControlID="Panel2" X="300" Y="200" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        &nbsp;
        &nbsp;&nbsp;
    </div>
</asp:Content>

