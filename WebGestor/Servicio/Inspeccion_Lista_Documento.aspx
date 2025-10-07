<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Lista_Documento.aspx.vb" Inherits="Inspeccion_Lista_Documento" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="9" style="vertical-align: baseline; height: 50px; text-align: center"
                    valign="middle">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 253px; vertical-align: middle; width: 500px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                        height: 1px; text-align: center">
                        Lista de Documentos</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 70px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 60px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 90px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 170px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="middle">
                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Inspec."
                                    Width="65px"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="middle">
                                <asp:TextBox ID="txtNroInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="120px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="middle">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Ingreso"
                        Width="60px"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="middle">
                    <asp:DropDownList ID="cboPorTipoIng" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="120px">
                    </asp:DropDownList></td>
                <td align="left" colspan="1" style="vertical-align: middle; height: 22px; width: 170px;" valign="middle">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Button ID="btnListar" runat="server" Text="Listar" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="100px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="middle">
                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"
                                    Width="65px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="middle">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
<asp:TextBox id="txtPorCodOficina" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial" OnTextChanged="txtPorCodOficina_TextChanged" __designer:wfdid="w18"></asp:TextBox> 
</ContentTemplate>
                                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                                </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="middle">
                    <asp:Button ID="btnBuscarXOficina" runat="server" BackColor="LightGray" BorderColor="Gray"
                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Gray" Height="20px"                                     Text="..." Width="25px" /></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="middle">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
<asp:TextBox id="txtPorOficDescrip" runat="server" Width="343px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w19"></asp:TextBox> 
</ContentTemplate>
                                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="FlexOficina" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                                </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="middle">
                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Ing."
                                    Width="65px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="middle">
                    <asp:TextBox ID="txtPorFechaInicio" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="95px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="middle">
                    <asp:Button ID="btnFecIni" runat="server" BackColor="LightGray" BorderColor="Gray"
                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Gray" Height="20px" 
                                    Text="..." Width="25px" Visible="False" /></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="middle">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Fin"
                        Width="56px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="middle">
                    <asp:TextBox ID="txtPorFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="90px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="middle"><asp:Button ID="btnFecfin" runat="server" BackColor="LightGray" BorderColor="Gray"
                                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Gray" Height="20px" 
                                    Text="..." Width="25px" Visible="False" /></td>
                <td align="left" style="vertical-align: middle; width: 170px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="middle">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px;" valign="middle">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle" valign="middle">
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                        TargetControlID="txtPorFechaInicio" Format="dd/MM/yyyy" PopupButtonID="txtPorFechaInicio">
                    </cc1:CalendarExtender>
                </td>
                <td align="left" colspan="3" style="vertical-align: middle" valign="middle">
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                        TargetControlID="txtPorFechaFin" Format="dd/MM/yyyy" PopupButtonID="txtPorFechaFin">
                    </cc1:CalendarExtender>
                </td>
                <td align="left" style="width: 25px;" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="middle">
                </td>
                <td align="left" colspan="7" style="height: 20px" valign="middle">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:Label id="lblRegistro" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w137"></asp:Label> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 310px" valign="middle">
                </td>
                <td align="left" colspan="7" style="height: 310px" valign="middle">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 300px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1070px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w138" AutoGenerateColumns="False"><Columns>
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
<asp:BoundField DataField="COD_OFICINA" HeaderText="Codigo Interno">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="230px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPECCION" HeaderText="Nro Inspeccion"></asp:BoundField>
<asp:BoundField DataField="Categoria" HeaderText="Categoria">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 310px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 70px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 60px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 90px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 170px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 70px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 60px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 90px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 30px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 170px; height: 22px" valign="middle">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            </tr>
        </table>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
         
        TargetControlID="btnBuscarXOficina" BackgroundCssClass="modalBackground" CacheDynamicResults="True" CancelControlID="btnCerrarOficina" PopupControlID="Panel1" X="300" Y="300">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
                border-top: gray 1px outset; border-left: gray 1px outset; width: 400px; border-bottom: gray 1px outset;
                background-color: darkgray">
                <tr>
                    <td align="left" style="width: 20px; height: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 25px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 200px; height: 25px; text-align: center"
                        valign="top">
                        <asp:Label ID="LblTituloOficina" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Text="Relación de Oficina" Width="110px"></asp:Label>
                    </td>
                    <td align="left" style="width: 77px; height: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 25px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        <asp:Label ID="lblCodigoOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Text="Código"></asp:Label>
                    </td>
                    <td align="left" style="width: 200px; height: 22px" valign="top">
                        <asp:TextBox ID="txtBusCodigo" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 77px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnCerrarOficina" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Cerrar" Width="70px" />
                    </td>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px" valign="top">
                        <asp:Label ID="lblOficinaDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Text="Descripción"></asp:Label>
                    </td>
                    <td align="left" style="width: 200px; height: 20px" valign="top">
                        <asp:TextBox ID="txtBusDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 77px; height: 20px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListarOficina" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            OnClick="btnListarOficina_Click" Text="Listar" Width="70px" />
                    </td>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 266px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="height: 266px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div id="DIV3" runat="server" style="border-right: gray 1px outset; border-top: gray 1px outset;
                                    overflow: auto; border-left: gray 1px outset; width: 360px; border-bottom: gray 1px outset;
                                    height: 240px">
                                    <asp:GridView ID="FlexOficina" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                        Font-Size="8pt" OnRowCommand="FlexOficina_RowCommand" Width="360px">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="&lt;&lt;">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CODIGO">
                                                <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Top" Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListarOficina" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 20px; height: 266px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 200px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 77px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 19px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <br />
    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
        <ContentTemplate>
<asp:TextBox id="txtcodOficina" runat="server" Width="94px" Height="10px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w139" Visible="False"></asp:TextBox> 
</ContentTemplate>
        <Triggers>
<asp:AsyncPostBackTrigger ControlID="FlexOficina" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</Triggers>
    </asp:UpdatePanel>
    <br />
</div>
</asp:Content>

