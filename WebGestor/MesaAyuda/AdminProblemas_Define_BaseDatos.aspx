<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_Define_BaseDatos.aspx.vb" Inherits="AdminProblemas_Define_BaseDatos" title="Mesa de Ayuda - Base de Datos Registro" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script> 
    <div style="text-align: left">

    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
<DIV style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">
    <IMG src="../Fotos/5.gif" /></DIV>
    </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" 
			            PopupControlID="panelUpdateProgress" />    
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" colspan="8" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Mantenimiento de Base de Datos</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" style="height: 19px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="540px"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 18px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 18px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativo"></asp:Label></td>
                <td align="left" colspan="2" style="height: 18px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Producto"
                        Width="50px"></asp:Label></td>
                <td align="left" colspan="2" style="height: 18px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                        Text="Sub-Producto" Width="66px"></asp:Label></td>
                <td align="left" style="height: 18px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:DropDownList ID="cboBusAplicativo" runat="server" CausesValidation="True" Font-Names="Arial"
                        Font-Size="8pt" Width="184px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboBusProducto" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboBusAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboBusSubProd" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboBusProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="height: 11px" valign="top" colspan="2">
                </td>
                <td align="left" style="width: 70px; height: 11px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 11px; text-align: right" valign="top">
                    </td>
                <td align="left" style="height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 20px; text-align: right" valign="top">
                    <asp:Button ID="btnListar" runat="server" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Listar" Width="80px" BackColor="LightGray" ForeColor="Gray" />
                    <asp:Button ID="btnNuevo" runat="server" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Nuevo" Width="80px" BackColor="LightGray" ForeColor="Gray" /></td>
                <td align="left" style="height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblCount" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Total de Registros : 0"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 18px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 18px" valign="top">
                    <div style="border-right: seagreen 1px outset; border-top: seagreen 1px outset; overflow: auto;
                        border-left: seagreen 1px outset; width: 550px; border-bottom: seagreen 1px outset;
                        height: 338px">
                        <asp:UpdatePanel id="UpdatePanel3" runat="server">
                            <contenttemplate>
<asp:GridView id="Flex" runat="server" Width="1770px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="40" DataKeyNames="ACARCON_APLICATIVO,ACARCON_PRODUCTO,ACARCON_SUBPRODUCTO"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

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
<asp:BoundField DataField="ACARCON_TRANSACCION" HeaderText="Transacci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_CONSULTA" HeaderText="Consulta">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_SOLUCION" HeaderText="Soluci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="1000px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Categoria" HeaderText="Categoria">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_APLICATIVO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_PRODUCTO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_SUBPRODUCTO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_CATEGORIA">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> 
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel></div>
                    &nbsp; &nbsp; &nbsp;<br />
                </td>
                <td align="left" style="height: 18px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 19px" valign="top">
                    </td>
                <td align="left" style="width: 100px; height: 19px" valign="top">
                    &nbsp;</td>
                <td align="left" style="height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
<TABLE style="WIDTH: 550px" id="lblIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Aplicativo" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta2" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" Text="Producto" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta3" runat="server" Width="84px" Font-Size="8pt" Font-Names="Arial" Text="Sub-Producto" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboAplicativo" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" CausesValidation="True" Visible="False">
                    </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 22px" vAlign=top align=left><asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboProducto" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" Visible="False" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left><asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboSubProd" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial" Visible="False" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Categoria" __designer:wfdid="w3"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboCategoria" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w11"></asp:DropDownList></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta4" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Transacción" Visible="False"></asp:Label></TD><TD style="WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=3><asp:TextBox id="txtTransaccion" runat="server" Width="544px" Height="50px" Font-Size="8pt" Font-Names="Arial" Visible="False" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Consulta" Visible="False"></asp:Label></TD><TD style="WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=3><asp:TextBox id="txtConsulta" runat="server" Width="544px" Height="50px" Font-Size="8pt" Font-Names="Arial" Visible="False" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left><asp:Label id="lblEtiqueta6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Solución" Visible="False"></asp:Label></TD><TD style="WIDTH: 190px; HEIGHT: 18px" vAlign=top align=left></TD><TD style="WIDTH: 170px; HEIGHT: 18px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 58px" vAlign=top align=left colSpan=3><asp:TextBox id="txtSolucion" runat="server" Width="544px" Height="50px" Font-Size="8pt" Font-Names="Arial" Visible="False" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 190px; HEIGHT: 25px" vAlign=top align=left><asp:TextBox id="txtCodConsulta" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Font-Overline="False" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=left colSpan=2><asp:Button id="btnGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Guardar" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" Visible="False"></asp:Button> <asp:Button id="btnCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" BackColor="LightGray" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" Visible="False"></asp:Button></TD></TR></TBODY></TABLE>
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="height: 20px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

