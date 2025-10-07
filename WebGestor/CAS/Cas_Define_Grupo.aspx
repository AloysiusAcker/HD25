<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Define_Grupo.aspx.vb" Inherits="Cas_Define_Grupo" title="GestorPlus" %>

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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Define Grupo</div>
                </td>
                <td align="left" style="width: 28px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="background-image: url(../Fotos/lineaCas.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 28px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 687px;" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 687px;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
<%--<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" ActiveTabIndex="2">--%>
    <cc1:TabPanel runat="server" ID="TabPanel1" HeaderText="TabPanel1"><ContentTemplate>
                                        <div style="text-align: left">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                                <tr>
                                                    <td align="left" style="width: 100px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 100px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 100px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 151px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 15px" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 100px; height: 22px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 100px; height: 22px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 100px; height: 22px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 151px; height: 22px; vertical-align: middle; text-align: right;" valign="top"><asp:Button ID="btnGNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="73px" OnClick="btnGNuevo_Click" />
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 22px; vertical-align: middle;" valign="top">
                                                        <asp:Button ID="btnGListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Listar" Width="73px" OnClick="btnGListar_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" valign="top">
                                                        <div id="DIV1" runat="server" style="border-right: gray 1px inset; border-top: gray 1px inset;
                                                            overflow: auto; border-left: gray 1px inset; width: 520px; border-bottom: gray 1px inset;
                                                            position: static; height: 200px">
                                                            <asp:GridView ID="FlexG" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                Font-Names="Arial" Font-Size="8pt" PageSize="7" Width="520px">
                                                                <Columns>
                                                                    <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                    </asp:ButtonField>
                                                                    <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="GRUPO_COD" HeaderText="Grupo">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="GRUPO_NOMBRE" HeaderText="Nombre">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="360px" Wrap="True" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top" colspan="5">
                                                        <div style="text-align: left">
                                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 520px" id="lblIngresoG" runat="server" visible="False">
                                                                <tr runat="server">
                                                                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top" runat="server">
                                                                        <asp:Label ID="lblEtiquetaG" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server">
                                                                    <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top" runat="server">
                                                                        <asp:Label ID="lblG1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Grupo"></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="vertical-align: middle; width: 480px; height: 22px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtGNombre" runat="server" Font-Names="Arial" Font-Size="8pt" Width="473px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr runat="server">
                                                                    <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top" runat="server">
                                                                        <asp:TextBox ID="txtCodGrupo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="10px" Visible="False"></asp:TextBox></td>
                                                                    <td align="left" style="vertical-align: middle; width: 480px; height: 22px; text-align: right"
                                                                        valign="top" runat="server">
                                                                        &nbsp;<asp:Button ID="btnGGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="73px" OnClick="btnGGuardar_Click" />
                                                                        <asp:Button ID="btnGCancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="73px" OnClick="btnGCancelar_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                                                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    
</ContentTemplate>
<HeaderTemplate>
                                        Grupo
                                    
</HeaderTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" ID="TabPanel2" HeaderText="TabPanel2"><ContentTemplate>
                                        <div style="text-align: left">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                                <tr>
                                                    <td align="left" style="width: 100px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 100px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 100px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 150px; height: 15px" valign="top">
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 15px" valign="top">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                                    </td>
                                                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                                    </td>
                                                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                                    </td>
                                                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; text-align: right;" valign="top"><asp:Button ID="btnRCNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="73px" OnClick="btnRCNuevo_Click" /></td>
                                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                                        <asp:Button ID="btnRCListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Listar" Width="73px" OnClick="btnRCListar_Click" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" valign="top">
                                                        <div id="DIV2" runat="server" style="border-right: gray 1px inset; border-top: gray 1px inset;
                                                            overflow: auto; border-left: gray 1px inset; width: 520px; border-bottom: gray 1px inset;
                                                            position: static; height: 200px">
                                                            <asp:GridView ID="FlexRC" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                Font-Names="Arial" Font-Size="8pt" PageSize="7" Width="520px">
                                                                <Columns>
                                                                    <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                    </asp:ButtonField>
                                                                    <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px" />
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="GRUPO_NOMBRE" HeaderText="Grupo">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="220px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Descripci&#243;n">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="COMPONENTE_COD">
                                                                        <ItemStyle ForeColor="White" Width="0px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="GRUPO_COD">
                                                                        <ItemStyle ForeColor="White" Width="0px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" style="vertical-align: middle" valign="top">
                                                            <table id="lblIngresoRC" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                                style="width: 520px" visible="False">
                                                                <tr id="Tr1" runat="server">
                                                                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top" id="Td1" runat="server">
                                                                        <asp:Label ID="lblEtiquetaRC" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label></td>
                                                                </tr>
                                                                <tr id="Tr2" runat="server">
                                                                    <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top" id="Td2" runat="server">
                                                                        <asp:Label ID="lblRC1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Grupo"></asp:Label></td>
                                                                    <td align="left" style="vertical-align: middle; width: 450px; height: 22px" valign="top" id="Td3" runat="server">
                                                                        <asp:DropDownList ID="cboRCGrupo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            Width="449px">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr id="Tr3" runat="server">
                                                                    <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top" id="Td4" runat="server">
                                                                        <asp:Label ID="lblRC2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Componente"></asp:Label></td>
                                                                    <td align="left" style="vertical-align: middle; width: 450px; height: 22px" valign="top" id="Td5" runat="server">
                                                                        <asp:DropDownList ID="cboRCComponente" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                            Width="449px">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr id="Tr4" runat="server">
                                                                    <td align="left" style="width: 70px; height: 22px" valign="top" id="Td6" runat="server">
                                                                    </td>
                                                                    <td align="left" style="vertical-align: middle; width: 450px; height: 22px; text-align: right"
                                                                        valign="top" id="Td7" runat="server">
                                                                        <asp:Button ID="btnRCGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="73px" OnClick="btnRCGuardar_Click" /><asp:Button ID="btnRCCancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                            onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="73px" OnClick="btnRCCancelar_Click" /></td>
                                                                </tr>
                                                            </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                                                        <asp:Label ID="lblErrorRC" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    
</ContentTemplate>
<HeaderTemplate>
                                        Relacionar&nbsp;con Componente&nbsp;
                                    
</HeaderTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" ID="TabPanel3" HeaderText="TabPanel3"><ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 150px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnRUAsignar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnRUAsignar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Asignar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray" __designer:wfdid="w13"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnRUListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnRUListar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Listar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray" __designer:wfdid="w14"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 221px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 200px" id="DIV3" runat="server"><asp:GridView id="FlexRU" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" AllowPaging="True" AutoGenerateColumns="False" __designer:wfdid="w15"><Columns>
<asp:ButtonField Text="Quitar" ButtonType="Button" CommandName="Eliminar">
<ControlStyle BackColor="LightGray" BorderStyle="Outset" Width="48px" ForeColor="Gray" BorderWidth="1px" BorderColor="Gray" Font-Size="8pt" Font-Names="Arial"></ControlStyle>

<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="GRUPO_NOMBRE" HeaderText="Grupo">
<ItemStyle Width="185px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Usuario" HeaderText="Usuario">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBREPER" HeaderText="Nombre de Usuario">
<ItemStyle Width="235px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GRUPO_COD">
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" id="lblRelacionUsuario" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:Label id="lblEtiquetaGRU" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w16"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Grupo" __designer:wfdid="w17"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:DropDownList id="cboGrupoPer" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboGrupoPer_SelectedIndexChanged" __designer:wfdid="w18"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:RadioButtonList id="optPersonas" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="optPersonas_SelectedIndexChanged" RepeatDirection="Horizontal" __designer:wfdid="w19"><asp:ListItem Selected="True" Value="0">Personal</asp:ListItem>
<asp:ListItem Value="1">Usuarios Externos del Sistema</asp:ListItem>
</asp:RadioButtonList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 240px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnRUGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnRUGuardar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Guardar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray" __designer:wfdid="w20"></asp:Button> <asp:Button id="btnRUCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnRUCancelar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Cancelar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray" __designer:wfdid="w21"></asp:Button> &nbsp;&nbsp; </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:Label id="lblRU10" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" Text="**Antes de pasar a la sgte. página debe de guardar las páginas marcadas" __designer:wfdid="w22"></asp:Label> </TD></TR><TR runat="server"><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 520px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 230px" id="DIV5" runat="server"><asp:GridView id="FlexPersonal" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="DarkGray" AllowPaging="True" AutoGenerateColumns="False" PageSize="7" __designer:wfdid="w23">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
<Columns>
<asp:TemplateField>
<ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
<ItemTemplate>
                                                                                            <asp:CheckBox ID="chkPer" runat="server" Font-Names="Arial" Font-Size="8pt" Width="20px" />
                                                                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="USUARIO" HeaderText="C&#243;digo">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRESP" HeaderText="Nombres y Apellidos">
<ItemStyle Width="450px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 240px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 240px; TEXT-ALIGN: right" vAlign=top align=left runat="server">&nbsp; &nbsp;&nbsp; </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorRU" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w24"></asp:Label> </TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
<HeaderTemplate>
                                        Relacionar Usuarios
                                    
</HeaderTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" ID="TabPanel4" HeaderText="TabPanel4"><ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 150px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnUNAsignar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnUNAsignar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Asignar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnUNListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnUNListar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Listar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 19px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 200px" id="DIV4" runat="server"><asp:GridView id="FlexUN" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" AllowPaging="True" AutoGenerateColumns="False" PageSize="7">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
<Columns>
<asp:ButtonField Text="Quitar" ButtonType="Button" CommandName="Eliminar">
<ControlStyle BackColor="LightGray" BorderStyle="Outset" Width="48px" ForeColor="Gray" BorderWidth="1px" BorderColor="Gray" Font-Size="8pt" Font-Names="Arial"></ControlStyle>

<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="NIVELES" HeaderText="Nivel">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Usuario" HeaderText="Usuario">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRESP" HeaderText="Nombre de Usuario">
<ItemStyle Width="370px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Nivel">
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" id="lblUsuariosNivel" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:Label id="lblEtiquetaUN" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="WIDTH: 50px; HEIGHT: 20px" vAlign=top align=left runat="server"><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nivel"></asp:Label> </TD><TD style="HEIGHT: 20px" vAlign=top align=left colSpan=2 runat="server"><asp:DropDownList id="cboUNivel" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboUNivel_SelectedIndexChanged"></asp:DropDownList> </TD></TR><TR runat="server"><TD vAlign=top align=left colSpan=2 runat="server"><asp:RadioButtonList id="optUN" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="optUN_SelectedIndexChanged" RepeatDirection="Horizontal"><asp:ListItem Value="0">Personal</asp:ListItem>
<asp:ListItem Value="1">Usuarios Externos del Sistema</asp:ListItem>
</asp:RadioButtonList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 241px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnUNGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnUNGuardar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Guardar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray"></asp:Button> <asp:Button id="btnUNCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnUNCancelar_Click" runat="server" CssClass="EstiloBoton" Width="73px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" Text="Cancelar" BackColor="LightGray" BorderWidth="1px" BorderColor="Gray"></asp:Button> &nbsp; &nbsp;</TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:Label id="lblNU11" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" Text="**Antes de pasar a la sgte. página debe de guardar las páginas marcadas"></asp:Label> </TD></TR><TR runat="server"><TD style="HEIGHT: 19px" vAlign=top align=left colSpan=3 runat="server"><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 520px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 230px" id="DIV6" runat="server"><asp:GridView id="FlexUNPersonal" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" BorderStyle="Outset" BorderWidth="1px" BorderColor="DarkGray" AllowPaging="True" AutoGenerateColumns="False" PageSize="7">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
<Columns>
<asp:TemplateField>
<ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
<ItemTemplate>
                                                                                            <asp:CheckBox ID="chkPersonal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                                Width="20px" />
                                                                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="USUARIO" HeaderText="C&#243;digo">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRES" HeaderText="Nombres y Apellidos">
<ItemStyle Width="450px" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="WIDTH: 50px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 240px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 241px" vAlign=top align=left runat="server"></TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorUN" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
<HeaderTemplate>
                                        Usuarios con Nivel
                                    
</HeaderTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 28px; height: 687px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="width: 550px;" valign="top">
                </td>
                <td align="left" style="width: 28px;" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

