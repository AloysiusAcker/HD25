<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Lista_OfPendiente.aspx.vb" Inherits="Inspeccion_Lista_OfPendiente" title="Servicio - Pendiente de Oficinas" %>

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
         <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 550px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Relación de Pendientes de Oficina</div>
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
                    </td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: baseline; text-align: center"
                    valign="top">
                </td>
                <td align="left" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 10px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="420px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="0" AutoPostBack="True"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Pendientes
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=6><asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblArticulo" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Text="Oficina"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtUbiCodigo" runat="server" Width="65px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px; TEXT-ALIGN: center" vAlign=top align=left><asp:Button id="btnUbica" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" Text="..." ForeColor="Gray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtUbiDescripcion" runat="server" Width="245px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Listar" ForeColor="Gray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label20" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipificación"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:DropDownList id="cboTipificacion" runat="server" Width="440px" Font-Size="8pt" Font-Names="Arial" Font-Overline="False"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label18" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Verificado"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboVerificado" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial"><asp:ListItem Selected="True">(Seleccionar)</asp:ListItem>
<asp:ListItem>SI</asp:ListItem>
<asp:ListItem>NO</asp:ListItem>
</asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label19" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" Text="Estado"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 250px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboEstado" runat="server" Width="96px" Font-Size="8pt" Font-Names="Arial"><asp:ListItem>(Seleccionar)</asp:ListItem>
<asp:ListItem Value="1">Pendiente</asp:ListItem>
<asp:ListItem Value="2">Completa</asp:ListItem>
</asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtUbicacion" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=6><asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 270px" vAlign=top align=left colSpan=6><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 520px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 300px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1410px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Registrar" Text="Registrar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="80px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="Cod_Interno" HeaderText="Oficina">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO_FINAL" HeaderText="Estado">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
    <asp:BoundField DataField="TSI" HeaderText="TSI">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
    </asp:BoundField>
    <asp:BoundField DataField="TTA" HeaderText="TTA">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
    </asp:BoundField>
<asp:BoundField DataField="tipificacion" HeaderText="Tipificaci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_ESTADO_OBS" HeaderText="Observaci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="responsable" HeaderText="Responsable">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="fecha_solucion" HeaderText="Soluci&#243;n Posible">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="VERIFICADO" HeaderText="Verificado">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_VERIFICADO" HeaderText="Fecha Verificaci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=6></TD></TR></TBODY></TABLE></DIV>&nbsp;&nbsp; <cc1:ModalPopupExtender id="ModalPopupExtender2" runat="server" TargetControlID="btnUbica" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="200" X="300" CancelControlID="btnUbiCerrar" BackgroundCssClass="modalBackground" PopupControlID="Panel2"></cc1:ModalPopupExtender> <asp:Panel id="Panel2" runat="server"><DIV style="TEXT-ALIGN: center"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 500px; BORDER-BOTTOM: gray 1px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: center" vAlign=middle align=left colSpan=3><asp:Label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Busqueda de Almacén y/o Centro de Costos" ForeColor="Black"></asp:Label> </TD><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusCod" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnUbiCerrar" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Cerrar" ForeColor="Gray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Silver" BackColor="LightGray"></asp:Button> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="Label4" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusDescripcion" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnUbiListar" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Listar" ForeColor="Gray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=middle align=left></TD><TD vAlign=middle align=left colSpan=3><asp:UpdatePanel id="UpdatePanel7" runat="server"><ContentTemplate>
<DIV style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 450px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" Font-Overline="False"><Columns>
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
</asp:GridView></DIV>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD><TD style="WIDTH: 25px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 280px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD></TR></TBODY></TABLE></DIV></asp:Panel> 
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
Registrar Visita
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 80px; HEIGHT: 9px" vAlign=top align=left><asp:Label id="LblNumero" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> </TD><TD style="WIDTH: 450px; HEIGHT: 9px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=2><asp:Label id="lblMensaje" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lbl2" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Numero"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=1><asp:TextBox id="txtnumero" runat="server" Width="73px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=1><asp:DropDownList id="CboTipo" runat="server" Width="448px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lbl3" runat="server" Width="63px" Font-Size="8pt" Font-Names="Arial" Text="Tipo Persona"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:DropDownList id="cboTecnico" runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label14" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Persona"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 450px; HEIGHT: 8px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTipoPersona" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnBuscarTipoPersona" runat="server" CssClass="EstiloBoton_Ac" Width="20px" Text="..."></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 340px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTipoNombre" runat="server" Width="320px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Oficina"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 450px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtOficina" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=2><asp:TextBox id="txtOficinaDesc" runat="server" Width="336px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label13" runat="server" Width="62px" Font-Size="8pt" Font-Names="Arial" Text="Fecha Prog."></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 450px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtFechaProgramada" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:ImageButton id="I1" runat="server" Width="20px" ImageUrl="~/Fotos/Calendario.bmp"></asp:ImageButton> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label1" runat="server" Width="54px" Font-Size="8pt" Font-Names="Arial" Text="Hora Prog."></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtHoraProg" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label9" runat="server" Width="51px" Font-Size="8pt" Font-Names="Arial" Text="Tiempo P."></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTiempoProgramado" runat="server" Width="88px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label17" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Prioridad"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><TABLE style="WIDTH: 450px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:DropDownList id="cboPrioridad" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial"><asp:ListItem>( Seleccionar )</asp:ListItem>
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
</asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label16" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial" Text="Motivo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 260px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=1><asp:DropDownList id="cboMotivo" runat="server" Width="256px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR></TBODY></TABLE></TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Observacion"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtObservacion" runat="server" Width="440px" Height="44px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine" MaxLength="500"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label12" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Objetivo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtObjetivo" runat="server" Width="440px" Height="44px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine" MaxLength="500"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 16px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label15" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 16px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtDescrip" runat="server" Width="440px" Height="44px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine" MaxLength="500"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTecnico" runat="server" Width="8px" Height="11px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="txtcodOficina" runat="server" Width="16px" Height="10px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Button id="btnGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGrabar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Guardar"></asp:Button> <asp:Button id="btnNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Limpiar"></asp:Button> <asp:Button id="btnRegresar" onclick="btnRegresar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Regresar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=2><asp:Label id="lblEtiqPendiente" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Pendiente" ForeColor="Maroon"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=2><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 520px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 100px" id="lblPendiente" runat="server"><asp:GridView id="FlexROficina" runat="server" Width="1280px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="Cod_Interno" HeaderText="Oficina">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO_FINAL" HeaderText="Estado">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
    <asp:BoundField DataField="TSI" HeaderText="TSI">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
    </asp:BoundField>
    <asp:BoundField DataField="TTA" HeaderText="TTA">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
    </asp:BoundField>
<asp:BoundField DataField="tipificacion" HeaderText="Tipificaci&#243;n">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_ESTADO_OBS" HeaderText="Observaci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="responsable" HeaderText="Responsable">
<HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="fecha_solucion" HeaderText="Soluci&#243;n Posible">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="VERIFICADO" HeaderText="Verificado">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_VERIFICADO" HeaderText="Fecha Verificaci&#243;n">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
</asp:GridView> </DIV></TD></TR></TBODY></TABLE><cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" TargetControlID="btnBuscarTipoPersona" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="200" X="200" CancelControlID="btnCerrar2" BackgroundCssClass="modalBackground" PopupControlID="Panel1"></cc1:ModalPopupExtender> <asp:Panel id="Panel1" runat="server"><DIV style="TEXT-ALIGN: left"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 400px; BORDER-BOTTOM: gray 1px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 20px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 25px; TEXT-ALIGN: center" vAlign=top align=left><asp:Label id="Label2" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Relación de Tipo Persona"></asp:Label> </TD><TD style="WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label10" runat="server" Font-Size="8pt" Font-Names="Arial" Text="RUC"></asp:Label> </TD><TD style="WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtRuc" runat="server" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnCerrar2" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Cerrar"></asp:Button> </TD><TD style="WIDTH: 20px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px" vAlign=top align=left><asp:Label id="Label11" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="WIDTH: 200px; HEIGHT: 20px" vAlign=top align=left><asp:TextBox id="txtRazonSocial" runat="server" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnListarTipoPers" onclick="btnListarTipoPers_Click" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Listar"></asp:Button> </TD><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px" vAlign=top align=left></TD><TD vAlign=top align=left colSpan=3><asp:UpdatePanel id="UpdatePanel3" runat="server"><ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="DIV3" runat="server"><asp:GridView id="FlexTipoPers" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnSelectedIndexChanged="FlexTipoPers_SelectedIndexChanged"><Columns>
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
</asp:UpdatePanel> </TD><TD style="WIDTH: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 200px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV></asp:Panel> <cc1:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtFechaProgramada" Enabled="True" PopupButtonID="I1" Format="dd/MM/yyyy"></cc1:CalendarExtender> <cc1:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtHoraProg" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender> <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="txtTiempoProgramado" Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender> 
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                    </asp:UpdatePanel></td>
                <td align="left" style="height: 10px" valign="top">
                </td>
            </tr>
        </table>
        <div>
            &nbsp;</div>
    </div>
</asp:Content>

