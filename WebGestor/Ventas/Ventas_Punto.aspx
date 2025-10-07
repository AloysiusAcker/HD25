<%@ Page Title="" Language="VB" MasterPageFile="~/Ventas/PagPrincipal_Nuevo.master" AutoEventWireup="false" CodeFile="Ventas_Punto.aspx.vb" Inherits="Ventas_Ventas_Punto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <div>    
        <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">
                    <IMG src="../Fotos/5.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" 
			            PopupControlID="panelUpdateProgress" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Punto de Venta</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 50px" valign="top"></td>
                <td align="left" style="width: 50px" valign="top"></td>
                <td align="left" style="width: 121px" valign="top"></td>
                <td align="left" style="width: 430px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" style="height: 19px;" valign="middle" colspan="5">
                    <asp:Label ID="lblError" runat="server" CssClass="EstiloLabel" Text=""></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" valign="middle" colspan="4" style="height: 19px">
                    <asp:Button ID="Btn" runat="server" CssClass="EstiloBoton_Ac" Text="Nuevo Venta" />
                    <input id="BtnCliente" type="button" value="Seleccionar Cliente" runat="server" class="EstiloBoton" visible ="true"  />
                </td>
                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 50px" valign="middle">
                    <asp:Label ID="lblEtq1" runat="server" Text="Caja" CssClass="EstiloLabel"></asp:Label>
                </td>
                <td align="left" valign="middle" colspan="3">
                    <asp:DropDownList ID="DdlCaja" runat="server" CssClass="EstiloDropDownList"></asp:DropDownList>
                    <asp:TextBox ID="txtCaja" runat="server" CssClass="EstiloTextbox"></asp:TextBox>
                </td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="middle"></td>
                <td align="left" style="width: 50px" valign="middle">
                    <asp:Label ID="lblEqt2" runat="server" CssClass="EstiloLabel" Text="Almacén"></asp:Label>
                </td>
                <td align="left" valign="middle" colspan="3" >
                    <asp:DropDownList ID="DdlAlmacen" runat="server" CssClass="EstiloDropDownList">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 50px" valign="middle">
                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Producto"></asp:Label>
                </td>
                <td align="left" valign="top" colspan="3">
                    <asp:TextBox ID="txtProducto" runat="server" CssClass="EstiloTextbox" Width="185px" AutoPostBack="True"></asp:TextBox>
                    <asp:Button ID="BtnBuscar" runat="server" CssClass="EstiloBoton_Ac" Text="..." />
                    <input id="btnOpen" type="button" value="..." runat="server" class="EstiloBoton" visible ="true"  />
                </td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" valign="top" colspan="3" style="height: 20px">
                    &nbsp;</td>
                <td align="left" style="width: 430px; height: 20px;" valign="middle">
                </td>
                <td align="left" style="width: 100px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="top" colspan="5">
                    <div>
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                            <Columns>
                                <asp:ButtonField CommandName="Quitar" Text="Quitar">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Calcular" Text="Calcular" />
                                <asp:BoundField DataField="c1" HeaderText="Artículo" />
                                <asp:BoundField DataField="c2" HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Cant">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtCant" text='<%# Bind("c3") %>' runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt" Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:BoundField >
                                <ItemStyle HorizontalAlign="Right" Width="0px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Precio Venta">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtPV" OnTextChanged="TxtPV_OnTextChanged" text='<%# Bind("c5") %>' runat="server" CssClass="bordeTexboxPag"  Width="70px" AutoPostBack="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="c6" HeaderText="SubTotal" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c7" HeaderText="IGV" >
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c8" HeaderText="Total">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c9" HeaderText="Stock">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 50px" valign="top"></td>
                <td align="left" style="width: 50px" valign="top"></td>
                <td align="left" style="width: 121px" valign="top"></td>
                <td align="left" style="width: 430px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 50px" valign="top"></td>
                <td align="left" style="width: 50px" valign="top"></td>
                <td align="left" style="width: 121px" valign="top"></td>
                <td align="left" style="width: 430px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
        </table>
    </div>     
    <div>    
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="345px" style="border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
            border-top: gray 1px outset; border-left: gray 1px outset; width: 400px; border-bottom: gray 1px outset"><tr>
                <td align="left" colspan="5" style="vertical-align: middle; height: 25px; background-color: darkgray;
                    text-align: center" valign="top">
                    <asp:Label ID="lblP3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Maroon" Text="Búsqueda de Artículos"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 18px; background-color: darkgray" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 18px; background-color: darkgray"
                    valign="top">
                    <asp:Label ID="lblP2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 180px; height: 18px; background-color: darkgray"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel13" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtPArtCodigo" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" ></asp:TextBox> 
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 18px; background-color: darkgray;
                    text-align: center" valign="top">
                    <div style="vertical-align: middle; text-align: right">
                        <asp:Button ID="BtnCerrarArt" runat="server" CssClass="EstiloBoton_Ac" Font-Names="Arial"
                            Font-Size="8pt" OnClick="btnCerrarArt_Click" Text="Cerrar" Width="80px" />&nbsp;</div>
                </td>
                <td align="left" style="width: 25px; height: 18px; background-color: darkgray" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 19px; background-color: darkgray"
                    valign="top">
                    <asp:Label ID="lblP1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 180px; height: 19px; background-color: darkgray"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel12" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtPArtDescripcion" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w55"></asp:TextBox> 
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 19px; background-color: darkgray;
                    text-align: right" valign="top">
                    <asp:Button ID="btnListarArt" runat="server" CssClass="EstiloBoton_Ac" Font-Names="Arial"
                        Font-Size="8pt" OnClick="btnListarArt_Click" Text="Listar" Width="80px" /></td>
                <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; background-color: darkgray" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; background-color: darkgray"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel15" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblErrorArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnListarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; background-color: darkgray" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 19px; background-color: darkgray"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel14" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblRegArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" __designer:wfdid="w1"></asp:Label>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnListarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                            
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 250px; background-color: darkgray" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: baseline; height: 250px; background-color: darkgray"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
                            <div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV3" runat="server">
                               <asp:DataList ID="FlexArt" runat="server" RepeatColumns="3" Font-Names="Arial" Font-Size="8pt" Width="349px" >
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" 
                    ImageUrl='<%# Eval("ART_CODIGO", "Ventas/imagenes/{0}.jpg") %>' />
                            <br />
                            Código:
                            <asp:Label ID="lblCodigo" runat="server" 
                    Text='<%# Eval("producto_cod") %>'></asp:Label>
                            <br />
                            Nombre:
                            <asp:Label ID="lblNombre" runat="server" 
                    Text='<%# Eval("ART_DESCRIPCION") %>'></asp:Label>
                            <br />
                            Precio:
                            <asp:Label ID="lblPrecio" runat="server" 
                    Text='<%# Eval("PRECIO_VENTA") %>'></asp:Label>
                            <br />
                            Precio IGV:
                            <asp:Label ID="Label2" runat="server" 
                    Text='<%# Eval("PRECIO_VENTA_IGV") %>'></asp:Label>
                            <br />
                            Stock:
                            <asp:Label ID="lblStock" runat="server" 
                    Text='<%# Eval("STOCK_ACTUAL") %>'></asp:Label>
                            <br />
                            <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" />
                        </ItemTemplate>
                    </asp:DataList>
                            </div>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnListarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 250px; background-color: darkgray" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="height: 20px; background-color: darkgray" valign="top">
                </td>
            </tr>
        </table>
    </asp:Panel>

        </div>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
            CacheDynamicResults="True" CancelControlID="BtnCerrarArt" PopupControlID="Panel1"
            TargetControlID="BtnBuscar" X="300" Y="200">
        </cc1:ModalPopupExtender>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

