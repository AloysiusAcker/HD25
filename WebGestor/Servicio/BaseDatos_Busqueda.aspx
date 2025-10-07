<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="BaseDatos_Busqueda.aspx.vb" Inherits="BaseDatos_Busqueda" title="Servicio - Base de Datos Listado" %>

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
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
<TABLE style="WIDTH: 600px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 50px; TEXT-ALIGN: center" vAlign=top align=left colSpan=9><DIV style="FONT-WEIGHT: bold; FONT-SIZE: 14pt; LEFT: 225px; VERTICAL-ALIGN: middle; WIDTH: 550px; COLOR: gray; FONT-STYLE: italic; FONT-FAMILY: 'Bell MT', Broadway, Arial, Serif; TOP: 284px; HEIGHT: 1px; TEXT-ALIGN: center" id="lblTitulo" class="EstiloTitleMenu" runat="server">Busqueda de Base de Datos</DIV></TD></TR><TR><TD style="BACKGROUND-IMAGE: url(../Fotos/linea.JPG); HEIGHT: 11px" vAlign=top align=left colSpan=9></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Aplicativos"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Productos"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Sub-Productos"></asp:Label></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboAplicativo" runat="server" Width="156px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                    </asp:DropDownList></TD><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboProducto" runat="server" Width="156px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                            </asp:DropDownList></TD><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=2>&nbsp;<asp:DropDownList id="cboSubProducto" runat="server" Width="146px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                            </asp:DropDownList></TD><TD style="WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Button id="btnListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Listar"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Palabras a Buscar:"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label5" runat="server" Width="100px" Font-Size="8pt" Font-Names="Arial" Text="Modo de Busqueda:"></asp:Label></TD><TD style="WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Button id="btnTop10" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnTop10_Click" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Top 10"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 40px" vAlign=top align=left></TD><TD style="HEIGHT: 40px" vAlign=top align=left colSpan=4><asp:TextBox id="txtBuscador" runat="server" Width="300px" Height="38px" TextMode="MultiLine"></asp:TextBox></TD><TD style="HEIGHT: 40px; TEXT-ALIGN: left" vAlign=top align=left colSpan=3><asp:RadioButtonList id="optModoBus" runat="server" Width="106px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow"><asp:ListItem Selected="True" Value="0">A ó B</asp:ListItem>
<asp:ListItem Value="1">A y B</asp:ListItem>
</asp:RadioButtonList></TD><TD style="WIDTH: 25px; HEIGHT: 40px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; TEXT-ALIGN: center" vAlign=top align=left colSpan=7><asp:UpdateProgress id="UpdateProgress1" runat="server"><ProgressTemplate>
<IMG src="../Fotos/5.gif" /><BR /><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Esperando ..."></asp:Label> 
</ProgressTemplate>
</asp:UpdateProgress></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=7><asp:Label id="lblError" runat="server" Width="498px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7><asp:Label id="lblCount" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Total de Registros : 0"></asp:Label></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD vAlign=top align=left colSpan=7><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 540px; BORDER-BOTTOM: darkgray 1px outset; HEIGHT: 337px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1760px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="5" DataKeyNames="CARCON_APLICATIVO,CARCON_PRODUCTO,CARCON_SUBPRODUCTO" UseAccessibleHeader="False" EnableTheming="True"><Columns>
<asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Font-Underline="False" Width="60px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="1000px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_CODIGO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle Wrap="True"></HeaderStyle>
</asp:GridView></DIV></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="HEIGHT: 5px" vAlign=top align=left colSpan=7></TD><TD style="WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD vAlign=top align=left colSpan=7><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 540px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 190px" id="lblDetalle" runat="server" Visible="false"><asp:DetailsView id="DetalleLista" runat="server" Width="550px" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="LightGray" AutoGenerateRows="False" CellPadding="4">
<FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>

<PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
<Fields>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SUBPRODUCTO" HeaderText="Subproducto">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="470px"></ItemStyle>
</asp:BoundField>
</Fields>

<HeaderStyle BackColor="#333333" BorderColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></EditRowStyle>
</asp:DetailsView></DIV></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 79px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=7></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 79px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</contenttemplate>
            <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnTop10" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optModoBus" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
        </asp:UpdatePanel>&nbsp;</div>

</asp:Content>

