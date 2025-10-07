<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_Mant_Paginas.aspx.vb" Inherits="SegSistema_Mant_Paginas" title="Sistema - Páginas" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
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
     </div>
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<TABLE style="WIDTH: 600px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 52px" vAlign=top align=left></TD><TD style="HEIGHT: 52px; TEXT-ALIGN: center" vAlign=top align=left colSpan=6><DIV style="FONT-WEIGHT: bold; FONT-SIZE: 14pt; LEFT: 225px; VERTICAL-ALIGN: middle; WIDTH: 550px; COLOR: gray; FONT-STYLE: italic; FONT-FAMILY: 'Bell MT', Broadway, Arial, Serif; TOP: 284px; HEIGHT: 1px; TEXT-ALIGN: center" id="lblTitulo" class="EstiloTitleMenu" runat="server">Define Páginas</DIV></TD><TD style="WIDTH: 25px; HEIGHT: 52px" vAlign=top align=left></TD></TR><TR><TD style="BACKGROUND-IMAGE: url(../Fotos/linea.JPG); HEIGHT: 11px" vAlign=top align=left colSpan=8></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 15px" vAlign=top align=left colSpan=6><asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label></TD><TD style="WIDTH: 25px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="72px" Height="20px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="76px" Height="20px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Nuevo" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 115px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 115px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD vAlign=top align=left colSpan=6>
    <DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 600px; BORDER-BOTTOM: darkgray 1px outset; HEIGHT: 250px"><asp:GridView id="Flex" runat="server" Width="850px" Font-Size="8pt" Font-Names="Arial" PageSize="40" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="MOD_CODIGO">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="MOD_NOMBRE" HeaderText="M&#243;dulo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PAG_CODIGO" HeaderText="C&#243;d. P&#225;gina" SortExpression="PAG_CODIGO">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PAG_NOMBRE" HeaderText="Nombre P&#225;gina" SortExpression="PAG_NOMBRE">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PAG_DESCRIPCION" HeaderText="Descripci&#243;n de la P&#225;gina" SortExpression="PAG_DESCRIPCION">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO" HeaderText="Tipo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PAG_ESTADO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PAG_DISPOSICION">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PAG_TIPO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=top align=left></TD><TD vAlign=top align=left colSpan=6><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 544px" id="lblDefinePagina" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Página"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="txtPagina" runat="server" Width="467px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl2" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="txtDescripcion" runat="server" Width="467px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Estado"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboEstado" runat="server" Width="203px" Font-Size="8pt" Font-Names="Arial">
                                          <asp:ListItem Selected="True">&lt; Seleccionar &gt; </asp:ListItem>
                                          <asp:ListItem Value="0">Activo</asp:ListItem>
                                          <asp:ListItem Value="1">De Baja</asp:ListItem>
                                      </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 199px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboTipo" runat="server" Width="198px" Font-Size="8pt" Font-Names="Arial">
                                          <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                          <asp:ListItem Value="1">P&#225;gina</asp:ListItem>
                                          <asp:ListItem Value="2">Modulo Clase</asp:ListItem>
                                          <asp:ListItem Value="3">MDI</asp:ListItem>
                                          <asp:ListItem Value="4">Sub P&#225;gina</asp:ListItem>
                                      </asp:DropDownList></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Módulo"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboModulo" runat="server" Width="203px" Font-Size="8pt" Font-Names="Arial">
                                      </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl6" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Disposición" BorderStyle="None"></asp:Label></TD><TD style="WIDTH: 199px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboDisposicion" runat="server" Width="198px" Font-Size="8pt" Font-Names="Arial">
                                          <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                          <asp:ListItem Value="1">Teminado en Prueba</asp:ListItem>
                                          <asp:ListItem Value="2">Terminado Contratado</asp:ListItem>
                                          <asp:ListItem Value="3">Terminado No Contratado</asp:ListItem>
                                          <asp:ListItem>No Terminado</asp:ListItem>
                                      </asp:DropDownList></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCodPagina" runat="server" Width="44px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> <asp:TextBox id="txtNomPag" runat="server" Width="96px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 199px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGrabar_Click" runat="server" CssClass="EstiloBoton" Width="65px" Height="20px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Guardar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> <asp:Button id="btnCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCancelar_Click" runat="server" CssClass="EstiloBoton" Width="65px" Height="20px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD></TR></TBODY></TABLE></DIV></TD><TD style="WIDTH: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 115px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 115px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</contenttemplate>
        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click"></asp2:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp2:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp2:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp2:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp2:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp2:AsyncPostBackTrigger>
</triggers>
    </asp:UpdatePanel>
</asp:Content>

