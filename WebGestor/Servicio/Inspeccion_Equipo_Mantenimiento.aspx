<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Equipo_Mantenimiento.aspx.vb" Inherits="Inspeccion_Equipo_Mantenimiento" title="Servicio - Equipos en Mantenimiento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>    
    <div style="text-align: left">
     <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div style="position: relative; top: 30%; text-align: center;">
                                    &nbsp;<img src="../Fotos/5.gif" /></div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 544px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Relación de Equipos en Mantenimiento</div>
                </td>
                <td align="left" style="height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 22px; vertical-align: middle;" valign="top">
                    <asp:Button ID="btnExportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Exportar" Width="80px" /></td>
                <td align="left" style="width: 70px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 260px; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 10px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="0" AutoPostBack="True"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
Equipos&nbsp; 
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 250px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtiqSerie" runat="server" Font-Size="8pt" Font-Names="Arial" Text="N° Serie" __designer:wfdid="w11"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="txtNroSerie" runat="server" Width="136px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w12"></asp:TextBox> &nbsp;&nbsp;</TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Listar" ForeColor="Gray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w13"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" __designer:wfdid="w14"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 270px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 520px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 300px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1140px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" __designer:wfdid="w9" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Registrar" Text="Registrar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="80px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Art&#237;culo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NRO" HeaderText="N&#176; Serie ">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLACA_NRO" HeaderText="N&#176; Placa">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_COMPRA" HeaderText="Fecha Compra">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA" HeaderText="Fecha Mantenimiento">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UBIC_CODIGO" HeaderText="Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UBIC_NOMBRE" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UBICACT_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SERIE_NUMERAR">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5>&nbsp;<asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w16"></asp:Label> </TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
Registrar Visita
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 80px; HEIGHT: 9px" vAlign=top align=left><asp:Label id="LblNumero" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w529" Visible="False"></asp:Label> </TD><TD style="WIDTH: 450px; HEIGHT: 9px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=2><asp:Label id="lblMensaje" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" __designer:wfdid="w530"></asp:Label> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lbl2" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Numero" __designer:wfdid="w531"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=1><asp:TextBox id="txtnumero" runat="server" Width="73px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w532"></asp:TextBox> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo" __designer:wfdid="w533"></asp:Label></TD><TD style="FONT-SIZE: 12pt; VERTICAL-ALIGN: middle; WIDTH: 450px; FONT-FAMILY: Times New Roman; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=1><asp:DropDownList id="CboTipo" runat="server" Width="448px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w534"></asp:DropDownList> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lbl3" runat="server" Width="63px" Font-Size="8pt" Font-Names="Arial" Text="Tipo Persona" __designer:wfdid="w535"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:DropDownList id="cboTecnico" runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w536"></asp:DropDownList> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label14" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Persona" __designer:wfdid="w537"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 450px; HEIGHT: 8px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTipoPersona" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w538"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnBuscarTipoPersona" runat="server" CssClass="EstiloBoton_Ac" Width="20px" Text="..." __designer:wfdid="w539"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 340px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTipoNombre" runat="server" Width="320px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w540"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Oficina" __designer:wfdid="w541"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 450px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtOficina" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w542" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 350px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=2><asp:TextBox id="txtOficinaDesc" runat="server" Width="340px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w543" ReadOnly="True"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="N° Serie" __designer:wfdid="w2"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtRVSerie" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w1" ReadOnly="True"></asp:TextBox> <asp:TextBox id="txtRVArticulo" runat="server" Width="340px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w3" ReadOnly="True"></asp:TextBox></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label13" runat="server" Width="62px" Font-Size="8pt" Font-Names="Arial" Text="Fecha Prog." __designer:wfdid="w544"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 450px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtFechaProgramada" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w545"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:ImageButton id="I1" runat="server" Width="20px" __designer:wfdid="w546" ImageUrl="~/Fotos/Calendario.bmp"></asp:ImageButton> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label1" runat="server" Width="54px" Font-Size="8pt" Font-Names="Arial" Text="Hora Prog." __designer:wfdid="w547"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtHoraProg" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w548"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label9" runat="server" Width="51px" Font-Size="8pt" Font-Names="Arial" Text="Tiempo P." __designer:wfdid="w549"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTiempoProgramado" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w550"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label17" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Prioridad" __designer:wfdid="w551"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><TABLE style="WIDTH: 450px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:DropDownList id="cboPrioridad" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w552"><asp:ListItem>&lt; Seleccionar &gt;</asp:ListItem>
<asp:ListItem>1</asp:ListItem>
<asp:ListItem>2</asp:ListItem>
<asp:ListItem>3</asp:ListItem>
<asp:ListItem>4</asp:ListItem>
<asp:ListItem>5</asp:ListItem>
<asp:ListItem>6</asp:ListItem>
<asp:ListItem>7</asp:ListItem>
<asp:ListItem>8</asp:ListItem>
<asp:ListItem>9</asp:ListItem>
<asp:ListItem>10</asp:ListItem>
</asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label16" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial" Text="Motivo" __designer:wfdid="w553"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 260px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=1><asp:DropDownList id="cboMotivo" runat="server" Width="256px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w554"></asp:DropDownList> </TD></TR></TBODY></TABLE></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Observacion" __designer:wfdid="w555"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtObservacion" runat="server" Width="440px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w556" TextMode="MultiLine" MaxLength="500"></asp:TextBox> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label12" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Objetivo" __designer:wfdid="w557"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtObjetivo" runat="server" Width="440px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w558" TextMode="MultiLine" MaxLength="500"></asp:TextBox> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 16px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label15" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripcion" __designer:wfdid="w559"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 16px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtDescrip" runat="server" Width="440px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w560" TextMode="MultiLine" MaxLength="500"></asp:TextBox> </TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTecnico" runat="server" Width="8px" Height="11px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w561" Visible="False"></asp:TextBox> <asp:TextBox id="txtcodOficina" runat="server" Width="16px" Height="10px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w562" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Button id="btnGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGrabar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Guardar" __designer:wfdid="w563"></asp:Button> <asp:Button id="btnNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Limpiar" __designer:wfdid="w564"></asp:Button> <asp:Button id="btnRegresar" onclick="btnRegresar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Regresar" __designer:wfdid="w565"></asp:Button> <asp:TextBox id="txtSerieNumerar" runat="server" Width="8px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w4" Visible="False" ReadOnly="True"></asp:TextBox></TD></TR></TBODY></TABLE></DIV><cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" __designer:wfdid="w566" TargetControlID="txtHoraProg" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender> <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" __designer:wfdid="w567" TargetControlID="txtTiempoProgramado" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender> <cc1:CalendarExtender id="CalendarExtender1" runat="server" __designer:wfdid="w568" TargetControlID="txtFechaProgramada" Enabled="True" PopupButtonID="I1" Format="dd/MM/yyyy"></cc1:CalendarExtender> <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" __designer:wfdid="w569" TargetControlID="btnBuscarTipoPersona" Enabled="True" DynamicServicePath="" Y="200" X="200" CancelControlID="btnCerrar2" BackgroundCssClass="modalBackground" PopupControlID="Panel1"></cc1:ModalPopupExtender> <asp:Panel id="Panel1" runat="server" __designer:wfdid="w570"><DIV style="TEXT-ALIGN: left"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 400px; BORDER-BOTTOM: gray 1px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 20px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: center" vAlign=top align=left colSpan=3><asp:Label id="Label2" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Relación de Tipo Persona" ForeColor="#400000" __designer:wfdid="w571"></asp:Label> </TD><TD style="WIDTH: 20px; HEIGHT: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label10" runat="server" Font-Size="8pt" Font-Names="Arial" Text="RUC" __designer:wfdid="w572"></asp:Label> </TD><TD style="WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtRuc" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w573"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnCerrar2" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Cerrar" __designer:wfdid="w574"></asp:Button> </TD><TD style="WIDTH: 20px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px" vAlign=top align=left><asp:Label id="Label11" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción" __designer:wfdid="w575"></asp:Label> </TD><TD style="WIDTH: 200px; HEIGHT: 20px" vAlign=top align=left><asp:TextBox id="txtRazonSocial" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w576"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnListarTipoPers" onclick="btnListarTipoPers_Click" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Listar" __designer:wfdid="w577"></asp:Button> </TD><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px" vAlign=top align=left></TD><TD vAlign=top align=left colSpan=3><asp:UpdatePanel id="UpdatePanel3" runat="server" __designer:wfdid="w578"><ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="DIV3" runat="server"><asp:GridView id="FlexTipoPers" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w579" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="30px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODINTERNO" HeaderText="CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListarTipoPers" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD><TD style="WIDTH: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 200px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV></asp:Panel> 
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                    </asp:UpdatePanel></td>
                <td align="left" style="height: 10px" valign="top">
                </td>
            </tr>
        </table>
        <div style="text-align: center">
            &nbsp; &nbsp;
        &nbsp;
        &nbsp;&nbsp;
    </div>
    </div>
</asp:Content>

