<%@ Page Language="VB" MasterPageFile="~/MenuWeb/PagPrincipal_MenuWeb.master" AutoEventWireup="false" CodeFile="MenuWeb_Elementos.aspx.vb" Inherits="MenuWeb_MenuWeb_Elementos" title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 50px; text-align: center;" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center; width: 536px;">
                        Elementos de los Items del Menu</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="background-image: url(../Fotos/Linea_Gris.bmp);
                    height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 5px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <contenttemplate>
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="544px"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboGrupo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboEmpresa" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Grupo"
                        Width="48px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboGrupo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="406px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnListar" runat="server" CssClass="EstiloBoton_Ac" Text="Listar"
                        Width="76px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"
                        Width="56px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <contenttemplate>
                    <asp:DropDownList ID="cboEmpresa" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="406px" AutoPostBack="True">
                    </asp:DropDownList>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboGrupo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="EstiloBoton_Ac" Text="Nuevo" Width="76px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Item" Width="32px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboItem" runat="server" Width="406px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w5">
                    </asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="cboEmpresa" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                    </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Categoria" Width="56px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <contenttemplate>
                    <asp:DropDownList ID="cboCategoria" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="406px" Enabled="False">
                    </asp:DropDownList>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    &nbsp;
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 544px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 216px"><asp:GridView id="Flex" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Overline="False" AutoGenerateColumns="False" __designer:wfdid="w12"><Columns>
<asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:ButtonField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView></DIV>&nbsp; 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <div style="overflow: auto; width: 500px; height: 100px">
        <asp:GridView ID="Flex1" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Width="100%">
            <Columns>
                <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                    <ControlStyle CssClass="EstiloBoton_Ac" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                </asp:ButtonField>
                <asp:BoundField DataField="c1" HeaderText="Categor&#237;a" />
                <asp:BoundField DataField="c2" HeaderText="Nombre" />
                <asp:BoundField DataField="c3" HeaderText="Descripci&#243;n Breve" />
                <asp:BoundField DataField="c4" HeaderText="Descripci&#243;n Larga" />
                <asp:BoundField DataField="c5" HeaderText="Link 1" />
                <asp:BoundField DataField="c6" HeaderText="Link 2" />
                <asp:BoundField DataField="c8" HeaderText="Fecha 1" />
                <asp:BoundField DataField="c9" HeaderText="Fecha 2" />
                <asp:BoundField DataField="c10" HeaderText="Fecha 3" />
                <asp:BoundField DataField="c11" HeaderText="Completar 1" />
                <asp:BoundField DataField="c12" HeaderText="Completar 2" />
                <asp:BoundField DataField="c13" HeaderText="Completar 3" />
                <asp:BoundField DataField="c14" HeaderText="Completar 4" />
                <asp:BoundField DataField="c15" HeaderText="Completar 5" />
                <asp:BoundField DataField="c7" HeaderText="Imagen" />
                <asp:BoundField DataField="c16" HeaderText="Archivo" />
            </Columns>
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:GridView>
    </div>
</asp:Content>

