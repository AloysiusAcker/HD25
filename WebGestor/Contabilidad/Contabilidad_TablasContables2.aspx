<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Contabilidad_TablasContables2.aspx.vb" Inherits="Contabilidad_TablasContables2" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="3" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Tablas Contables</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px"
                    valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 26px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="width: 550px;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="4" Font-Names="Arial"
                        Font-Size="8pt" Width="550px" AutoPostBack="True" Height="400px">
                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                            <HeaderTemplate>
                                Aduana
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div style="text-align: left">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                        <tr>
                                            <td align="left" style="width: 530px; height: 15px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; width: 530px; height: 22px" valign="top">
                                                <asp:DropDownList ID="cboAAño" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                    Font-Size="8pt" OnSelectedIndexChanged="cboAAño_SelectedIndexChanged" Width="66px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnANuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnANuevo_Click" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="51px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; width: 530px;" valign="top">
                                                <div id="DIV1" runat="server" style="border-right: dimgray 1px outset; border-top: dimgray 1px outset;
                                                    overflow: auto; border-left: dimgray 1px outset; width: 500px; border-bottom: dimgray 1px outset;
                                                    position: static">
                                                    <asp:GridView ID="FlexA" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        Font-Names="Arial" Font-Size="8pt" PageSize="7" >
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ADU_CODIGO" HeaderText="C&#243;digo">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ADU_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="350px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; width: 530px; height: 15px" valign="top">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; width: 530px; height: 19px" valign="top">
                                                <div style="text-align: left">
                                                    <table id="lblAIngreso" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 500px"
                                                        visible="False">
                                                        <tr runat="server">
                                                            <td runat="server" align="left" colspan="4" style="vertical-align: middle; height: 22px"
                                                                valign="top">
                                                                <asp:Label ID="lblAEtiqueta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 40px; height: 22px"
                                                                valign="top">
                                                                <asp:Label ID="lblA1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"
                                                                    Width="35px"></asp:Label>
                                                            </td>
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 22px"
                                                                valign="top">
                                                                <asp:TextBox ID="txtCodAduana" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    MaxLength="3" Width="60px"></asp:TextBox>
                                                            </td>
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 22px"
                                                                valign="top">
                                                                <asp:Label ID="lblA2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"
                                                                    Width="44px"></asp:Label>
                                                            </td>
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 320px; height: 22px"
                                                                valign="top">
                                                                <asp:TextBox ID="txtADescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    MaxLength="100" Width="314px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 40px; height: 22px"
                                                                valign="top">
                                                            </td>
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 22px"
                                                                valign="top">
                                                            </td>
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 22px"
                                                                valign="top">
                                                            </td>
                                                            <td runat="server" align="left" style="vertical-align: middle; width: 320px; height: 22px;
                                                                text-align: right" valign="top">
                                                                <asp:Button ID="btnAGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnAGuardar_Click" onmouseout="this.style.fontWeight='normal'"
                                                                    onmouseover="this.style.fontWeight='bolder'" Text="Grabar" Width="51px" />
                                                                &nbsp;<asp:Button ID="btnACancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnACancelar_Click" onmouseout="this.style.fontWeight='normal'"
                                                                    onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="51px" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; width: 530px; height: 22px" valign="top">
                                                <asp:Label ID="lblAError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#C00000"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                            <ContentTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 500px">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25px" valign="top">
                                            </td>
                                            <td align="left" style="width: 450px" valign="top">
                                            </td>
                                            <td align="left" style="width: 25px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px; height: 22px" valign="top">
                                            </td>
                                            <td align="left" style="vertical-align: middle; width: 450px; height: 22px" valign="top">
                                                <asp:DropDownList ID="cboAñoP" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                    Font-Size="8pt" OnSelectedIndexChanged="cboAñoP_SelectedIndexChanged" Width="66px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnPNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="51px" OnClick="btnPNuevo_Click" />
                                            </td>
                                            <td align="left" style="width: 25px; height: 22px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px; height: 19px" valign="top">
                                            </td>
                                            <td align="left" style="width: 450px; height: 19px" valign="top">
                                                <div id="DIV5" runat="server" style="border-right: dimgray 1px outset; border-top: dimgray 1px outset;
                                                    overflow: auto; border-left: dimgray 1px outset; width: 450px; border-bottom: dimgray 1px outset">
                                                    <asp:GridView ID="FlexP" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        Font-Names="Arial" Font-Size="8pt" PageSize="7" Width="450px">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" />
                                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                    Width="50px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="PRES_CUENTA" HeaderText="Partida Presupuestaria">
                                                                <HeaderStyle Font-Names="Arial" Font-Overline="False" Font-Size="8pt" HorizontalAlign="Center"
                                                                    VerticalAlign="Middle" Width="100px" />
                                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                    Width="100px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRES_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                    Width="250px" />
                                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Top"
                                                                    Width="250px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRES_NIVEL_CUENTA" HeaderText="Nivel">
                                                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle"
                                                                    Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRES_CODIGO">
                                                                <ItemStyle ForeColor="White" Width="0px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                            <td align="left" style="width: 25px; height: 19px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px; height: 21px" valign="top">
                                            </td>
                                            <td align="left" style="width: 450px; height: 21px" valign="top">
                                            </td>
                                            <td align="left" style="width: 25px; height: 21px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px; height: 18px" valign="top">
                                            </td>
                                            <td align="left" style="vertical-align: middle; width: 450px; height: 18px" valign="top">
                                                <div style="text-align: left">
                                                    <table id="FraPIngreso" border="0" cellpadding="0" cellspacing="0" style="width: 450px" runat="server" visible="False">
                                                            <tr runat="server">
                                                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top" runat="server">
                                                                    <asp:Label ID="lblPEtiqueta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top" runat="server">
                                                                    <asp:Label ID="lblP1" runat="server" Font-Names="Arial" Font-Size="8pt" Width="125px">Cuenta P. Presupuestaria</asp:Label>
                                                                </td>
                                                                <td align="left" style="vertical-align: middle; width: 320px; height: 22px" valign="top" runat="server">
                                                                    <asp:TextBox ID="txtPCuenta" runat="server" Font-Names="Arial" Font-Size="8pt" Width="203px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top" runat="server">
                                                                    <asp:Label ID="lblP2" runat="server" Font-Names="Arial" Font-Size="8pt" Width="125px">Nivel de la cuenta</asp:Label>
                                                                </td>
                                                                <td align="left" style="vertical-align: middle; width: 320px; height: 22px" valign="top" runat="server">
                                                                    <asp:RadioButtonList ID="optPNivel" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="0">Principal</asp:ListItem>
                                                                        <asp:ListItem Value="1">Sub-Partida</asp:ListItem>
                                                                        <asp:ListItem Value="2">Registro</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top" runat="server">
                                                                    <asp:Label ID="lblP3" runat="server" Font-Names="Arial" Font-Size="8pt" Width="125px">Nombre P. Presupuestaria</asp:Label>
                                                                </td>
                                                                <td align="left" style="vertical-align: middle; width: 320px; height: 22px" valign="top" runat="server">
                                                                    <asp:TextBox ID="txtPDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="304px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td align="left" style="vertical-align: middle; width: 130px; height: 19px" valign="top" runat="server">
                                                                </td>
                                                                <td align="left" style="vertical-align: middle; width: 320px; height: 19px; text-align: right"
                                                                    valign="top" runat="server">
                                                                </td>
                                                            </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td align="left" style="width: 25px; height: 18px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px; height: 19px" valign="top">
                                            </td>
                                            <td align="left" style="width: 450px; height: 19px" valign="top">
                                            </td>
                                            <td align="left" style="width: 25px; height: 19px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px" valign="top">
                                            </td>
                                            <td align="left" style="width: 450px" valign="top">
                                            </td>
                                            <td align="left" style="width: 25px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 25px" valign="top">
                                            </td>
                                            <td align="left" style="width: 450px" valign="top">
                                            </td>
                                            <td align="left" style="width: 25px" valign="top">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                            <HeaderTemplate>
                                Part. Presupuestaria
                            </HeaderTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                            <ContentTemplate>
                                <div style="text-align: left">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                        <tr>
                                            <td align="left" style="height: 15px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; height: 22px" valign="top">
                                                <asp:DropDownList ID="cboMAño" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                    Font-Size="8pt" OnSelectedIndexChanged="cboMAño_SelectedIndexChanged" Width="66px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnMNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnMNuevo_Click" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="51px" /></td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <div style="border-right: dimgray 1px outset; border-top: dimgray 1px outset; overflow: auto;
                                                    border-left: dimgray 1px outset; width: 500px; border-bottom: dimgray 1px outset;
                                                    position: static">
                                                    <asp:GridView ID="FlexM" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        Font-Names="Arial" Font-Size="8pt" PageSize="7">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Editar" Text="Editar">
                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="MEDIO_CODIGO" HeaderText="C&#243;digo">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MEDIO_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="350px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 19px" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <div style="text-align: left">
                                                    <table id="lblMIngreso" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                        style="width: 500px" visible="False">
                                                        <tr runat="server">
                                                            <td align="left" style="vertical-align: middle; height: 22px;" valign="top" colspan="4" runat="server">
                                                                <asp:Label ID="lblMEtiqueta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td align="left" style="width: 40px; height: 22px; vertical-align: middle;" valign="top" runat="server">
                                                                <asp:Label ID="lblM1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 22px; vertical-align: middle; width: 70px;" valign="top" runat="server">
                                                                <asp:TextBox ID="txtCodMedioPago" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Width="64px"></asp:TextBox>
                                                            </td>
                                                            <td align="left" style="height: 22px; vertical-align: middle; width: 70px;" valign="top" runat="server">
                                                                <asp:Label ID="lblM2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label>
                                                            </td>
                                                            <td align="left" style="height: 22px; vertical-align: middle; width: 320px;" valign="top" runat="server">
                                                                <asp:TextBox ID="txtMDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Width="314px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server">
                                                            <td align="left" style="width: 40px; height: 22px; vertical-align: middle;" valign="top" runat="server">
                                                            </td>
                                                            <td align="left" style="height: 22px; vertical-align: middle; width: 70px;" valign="top" runat="server">
                                                            </td>
                                                            <td align="left" style="height: 22px; vertical-align: middle; width: 70px;" valign="top" runat="server">
                                                            </td>
                                                            <td align="left" style="height: 22px; vertical-align: middle; width: 320px; text-align: right;" valign="top" runat="server"><asp:Button ID="btnMGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnMGuardar_Click" onmouseout="this.style.fontWeight='normal'"
                                                                    onmouseover="this.style.fontWeight='bolder'" Text="Grabar" Width="51px" />
                                                                <asp:Button ID="btnMCancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnMCancelar_Click" onmouseout="this.style.fontWeight='normal'"
                                                                    onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="51px" /></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="vertical-align: middle; height: 22px" valign="top">
                                                <asp:Label ID="lblMError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                            <HeaderTemplate>
                                Medio Pago
                            </HeaderTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                            <HeaderTemplate>
                                Cuenta Bancos
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div style="text-align: left">
                                    <div style="text-align: left">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                            <tr>
                                                <td align="left" style="height: 15px; width: 530px;" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="vertical-align: middle; height: 22px; width: 530px;" valign="top">
                                                    <asp:Button ID="btnBNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnBNuevo_Click" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="51px" /></td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 22px; width: 530px;" valign="top">
                                                    <div id="DIV2" runat="server" style="border-right: dimgray 1px outset; border-top: dimgray 1px outset;
                                                        overflow: auto; border-left: dimgray 1px outset; width: 500px; border-bottom: dimgray 1px outset;
                                                        position: static">
                                                        <asp:GridView ID="FlexB" runat="server" AllowPaging="True" Font-Names="Arial" Font-Size="8pt"
                                                            PageSize="7" AutoGenerateColumns="False">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="BANCO_NOMBRE" HeaderText="Banco">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TIPOC" HeaderText="Tipo de Cuenta">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CBAN_CUENTA" HeaderText="N&#186; de Cuenta">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CBAN_CODIGO">
                                                                    <ItemStyle ForeColor="White" Width="0px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 15px; width: 530px;" valign="top">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" style="height: 19px; width: 530px;">
                                                    <div style="text-align: left">
                                                        <table id="lblBIngreso" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                            style="width: 500px" visible="False">
                                                            <tr runat="server">
                                                                <td runat="server" align="left" colspan="5" style="vertical-align: middle; height: 19px"
                                                                    valign="top">
                                                                    <asp:Label ID="lblBEtiqueta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="1" style="vertical-align: middle; width: 90px;
                                                                    height: 19px" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" rowspan="2" style="vertical-align: text-top; width: 61px"
                                                                    valign="top">
                                                                    <asp:Label ID="lblB1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Banco"
                                                                        Width="34px"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" rowspan="2" style="vertical-align: middle; width: 75px"
                                                                    valign="top">
                                                                    <asp:RadioButtonList ID="optBBanco" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Height="26px" Width="66px" AutoPostBack="True" OnSelectedIndexChanged="optBBanco_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0" Selected="True">Existente</asp:ListItem>
                                                                        <asp:ListItem Value="1">Nuevo</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px"
                                                                    valign="top">
                                                                    <asp:DropDownList ID="cboBBancoNom" runat="server" Enabled="False" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="272px">
                                                                    </asp:DropDownList></td>
                                                                <td runat="server" align="left" style="vertical-align: middle; width: 90px; height: 22px;
                                                                    text-align: right" valign="top">
                                                                    <asp:Button ID="btnBBorrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnBBorrar_Click" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Borrar Banco" Width="86px" Enabled="False" /></td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" colspan="4" style="vertical-align: middle; height: 23px"
                                                                    valign="top">
                                                                    <asp:TextBox ID="txtBBancoNom" runat="server" Enabled="False" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="358px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" style="vertical-align: middle; width: 61px; height: 22px"
                                                                    valign="top">
                                                                    <asp:Label ID="lblB2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Moneda"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="2" style="vertical-align: middle; height: 22px"
                                                                    valign="top">
                                                                    <asp:DropDownList ID="cboBMoneda" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="174px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td runat="server" align="left" style="vertical-align: middle; width: 80px; height: 22px"
                                                                    valign="top">
                                                                    <asp:Label ID="lblB4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Cuenta"
                                                                        Width="74px"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="2" style="vertical-align: middle; height: 22px"
                                                                    valign="top">
                                                                    <asp:DropDownList ID="cboBTipo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Width="184px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" id="Tr1">
                                                                <td runat="server" align="left" style="vertical-align: middle; width: 61px; height: 22px"
                                                                    valign="top">
                                                                    <asp:Label ID="lblB3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Cuenta"
                                                                        Width="50px"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="2" style="vertical-align: middle; height: 22px"
                                                                    valign="top">
                                                                    <asp:TextBox ID="txtBCuenta" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="50"
                                                                        Width="168px"></asp:TextBox>
                                                                </td>
                                                                <td runat="server" align="left" style="vertical-align: middle; width: 80px; height: 22px"
                                                                    valign="top">
                                                                </td>
                                                                <td runat="server" align="left" style="height: 22px; vertical-align: middle; text-align: right;" valign="top" colspan="2"><asp:Button ID="btnBGuardar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnBGuardar_Click" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Grabar" Width="60px" />
                                                                    <asp:Button ID="btnBCancelar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" OnClick="btnBCancelar_Click" onmouseout="this.style.fontWeight='normal'"
                                                    onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="60px" /></td>
                                                            </tr>
                                                            <tr runat="server" id="Tr3">
                                                                <td runat="server" align="left" style="width: 61px" valign="top" id="Td7">
                                                                </td>
                                                                <td runat="server" align="left" style="width: 75px" valign="top" id="Td8">
                                                                </td>
                                                                <td runat="server" align="left" style="width: 100px" valign="top" id="Td9">
                                                                </td>
                                                                <td runat="server" align="left" style="width: 80px" valign="top" id="Td10">
                                                                </td>
                                                                <td runat="server" align="left" style="width: 95px" valign="top" id="Td11">
                                                                </td>
                                                                <td runat="server" align="left" style="width: 90px" valign="top" id="Td12">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 22px; vertical-align: middle; width: 530px;" valign="top">
                                                    <asp:Label ID="lblBError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5">
                            <HeaderTemplate>
                                &nbsp;Conciliación Bancaria&nbsp;
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                    <tr>
                                        <td colspan="7" style="height: 15px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 22px;">
                                            <asp:Label ID="Label1" runat="server" Font-Size="8pt" Text="Nº Cta de Bancos:"
                                                Width="99px" Font-Names="Arial"></asp:Label>
                                        </td>
                                        <td colspan="6" style="height: 22px">
                                            <asp:DropDownList ID="cboCBCtaBnc" runat="server" Width="429px" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="cboCBCtaBnc_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 22px">
                                            <asp:Label ID="Label2" runat="server" Font-Size="8pt" Text="Año:" Width="32px" Font-Names="Arial"></asp:Label>
                                        </td>
                                        <td style="width: 80px; height: 22px">
                                            <asp:DropDownList ID="cboAñoCB" runat="server" Width="77px" Font-Names="Arial" Font-Size="8pt" OnSelectedIndexChanged="cboAñoCB_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td colspan="2" style="height: 22px">
                                            <asp:Label ID="Label4" runat="server" Font-Size="8pt" Text="Periodo:" Width="46px" Font-Names="Arial"></asp:Label>
                                        </td>
                                        <td colspan="3" style="height: 22px">
                                            <asp:DropDownList ID="cboCBPeriodo" runat="server" Width="298px" AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="cboCBPeriodo_SelectedIndexChanged" Font-Names="Arial" Font-Size="8pt">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 22px">
                                            <asp:Label ID="Label3" runat="server" Font-Size="8pt" Text="Saldo Banco:" Width="72px" Font-Names="Arial"></asp:Label>
                                        </td>
                                        <td style="width: 80px; height: 22px">
                                            <asp:TextBox ID="TxtCBSaldoBnc" runat="server" Width="69px"></asp:TextBox>
                                        </td>
                                        <td colspan="1" style="width: 50px; height: 22px">
                                        </td>
                                        <td colspan="2" style="height: 22px">
                                        </td>
                                        <td colspan="2" style="vertical-align: middle; height: 22px; text-align: right">
                                            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="80px" /><asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="80px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 22px">
                                        </td>
                                        <td style="width: 80px; height: 22px">
                                        </td>
                                        <td style="width: 50px; height: 22px">
                                        </td>
                                        <td style="height: 22px">
                                        </td>
                                        <td style="width: 100px; height: 22px">
                                        </td>
                                        <td style="width: 100px; height: 22px">
                                        </td>
                                        <td style="height: 22px; vertical-align: middle; width: 100px; text-align: right;">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 26px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 13px;" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 13px;" valign="top">
                    </td>
                <td align="left" style="width: 26px; height: 13px;" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

