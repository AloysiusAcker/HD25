<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="BaseDatos_Definicion.aspx.vb" Inherits="BaseDatos_Definicion" title="Servicio - Base de Datos Registro" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
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
<TABLE style="WIDTH: 600px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 50px; TEXT-ALIGN: center" vAlign=top align=left colSpan=8><DIV style="FONT-WEIGHT: bold; FONT-SIZE: 14pt; LEFT: 225px; VERTICAL-ALIGN: middle; WIDTH: 550px; COLOR: gray; FONT-STYLE: italic; FONT-FAMILY: 'Bell MT', Broadway, Arial, Serif; TOP: 284px; HEIGHT: 1px; TEXT-ALIGN: center" id="lblTitulo" class="EstiloTitleMenu" runat="server">Mantenimiento de Base de Datos</DIV></TD></TR><TR><TD style="BACKGROUND-IMAGE: url(../Fotos/linea.JPG); HEIGHT: 11px" vAlign=top align=left colSpan=8></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 120px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 120px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 13px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 13px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 18px" vAlign=top align=left colSpan=2><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Aplicativo"></asp:Label></TD><TD style="FONT-SIZE: 12pt; VERTICAL-ALIGN: middle; FONT-FAMILY: Times New Roman; HEIGHT: 18px" vAlign=top align=left colSpan=2><asp:Label id="Label2" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" Text="Producto"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 18px" vAlign=top align=left colSpan=2><asp:Label style="TEXT-ALIGN: right" id="Label3" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" Text="Sub-Producto"></asp:Label></TD><TD style="WIDTH: 25px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboBusAplicativo" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" CausesValidation="True" AutoPostBack="True">
                    </asp:DropDownList></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboBusProducto" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" CausesValidation="True" AutoPostBack="True"></asp:DropDownList></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboBusSubProd" runat="server" Width="166px" Font-Size="8pt" Font-Names="Arial" CausesValidation="True" AutoPostBack="True"></asp:DropDownList></TD><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 11px" vAlign=top align=left></TD><TD style="HEIGHT: 11px" vAlign=top align=left colSpan=2></TD><TD style="WIDTH: 70px; HEIGHT: 11px" vAlign=top align=left></TD><TD style="HEIGHT: 11px; TEXT-ALIGN: right" vAlign=top align=left colSpan=3></TD><TD style="WIDTH: 25px; HEIGHT: 11px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 120px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=3>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <asp:Button id="btnListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Listar"></asp:Button> <asp:Button id="btnNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Nuevo"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; TEXT-ALIGN: center" vAlign=top align=left colSpan=6><asp:UpdateProgress id="UpdateProgress1" runat="server"><ProgressTemplate>
<IMG src="../Fotos/5.gif" /><BR /><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Esperando ..."></asp:Label> 
</ProgressTemplate>
</asp:UpdateProgress></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=6><asp:Label id="lblError" runat="server" Width="498px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=6><asp:Label id="lblCount" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Total de Registros : 0"></asp:Label></TD><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="HEIGHT: 18px" vAlign=top align=left colSpan=6><DIV style="BORDER-RIGHT: seagreen 1px outset; BORDER-TOP: seagreen 1px outset; OVERFLOW: auto; BORDER-LEFT: seagreen 1px outset; WIDTH: 543px; BORDER-BOTTOM: seagreen 1px outset; HEIGHT: 338px"><asp:GridView id="Flex" runat="server" Width="1700px" Font-Size="8pt" Font-Names="Arial" DataKeyNames="CARCON_APLICATIVO,CARCON_PRODUCTO,CARCON_SUBPRODUCTO" PageSize="40" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
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
<asp:BoundField DataField="CARCON_APLICATIVO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_PRODUCTO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CARCON_SUBPRODUCTO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView></DIV></TD><TD style="WIDTH: 25px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="HEIGHT: 19px" vAlign=top align=left colSpan=5></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=top align=left>&nbsp;</TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=6><TABLE style="WIDTH: 550px" id="lblIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Aplicativo" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta2" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" Text="Producto" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta3" runat="server" Width="84px" Font-Size="8pt" Font-Names="Arial" Text="Sub-Producto" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboAplicativo" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" CausesValidation="True" AutoPostBack="True" Visible="False">
                    </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboProducto" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" CausesValidation="True" AutoPostBack="True" Visible="False"></asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboSubProd" runat="server" Width="166px" Font-Size="8pt" Font-Names="Arial" CausesValidation="True" AutoPostBack="True" Visible="False"></asp:DropDownList></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta4" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Transacción" Visible="False"></asp:Label></TD><TD style="WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=3><asp:TextBox id="txtTransaccion" runat="server" Width="540px" Height="50px" Font-Size="8pt" Font-Names="Arial" Visible="False" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Consulta" Visible="False"></asp:Label></TD><TD style="WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=3><asp:TextBox id="txtConsulta" runat="server" Width="540px" Height="50px" Font-Size="8pt" Font-Names="Arial" Visible="False" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Solución" Visible="False"></asp:Label></TD><TD style="WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 58px" vAlign=top align=left colSpan=3><asp:TextBox id="txtSolucion" runat="server" Width="540px" Height="50px" Font-Size="8pt" Font-Names="Arial" Visible="False" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 190px; HEIGHT: 25px" vAlign=top align=left><asp:TextBox id="txtCodConsulta" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False" ReadOnly="True" Font-Overline="False"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px" vAlign=top align=left colSpan=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button id="btnGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Guardar" Visible="False"></asp:Button> <asp:Button id="btnCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Cancelar" Visible="False"></asp:Button></TD></TR></TBODY></TABLE></TD><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=6></TD><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 120px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 120px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 20px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 20px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</contenttemplate>
            <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboBusProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboBusAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="DataBinding"></asp:AsyncPostBackTrigger>
</triggers>
        </asp:UpdatePanel>&nbsp;</div>
</asp:Content>

