<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Movimiento_Equipos.aspx.vb" Inherits="Inventario_Movimiento_Equipos" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script> 
    <div style="text-align: left">
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">
                    <img src="../Fotos/5.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
                <td align="left" colspan="7" style="height: 50px" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Movimiento de Equipos</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="background-image: url(../Fotos/linea.JPG); height: 11px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 10px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 10px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel7" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtDesCodigo" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="txtUbicaCodigo" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 10px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 10px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 10px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 170px; height: 10px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 10px" valign="top"></td>
                <td align="left" style="width: 24px; height: 10px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" colspan="7" style="vertical-align: middle" valign="top">
                    <asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblError" runat="server" Width="544px" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="cboDestino" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="cboUbica" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Origen"></asp:Label>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel15" runat="server">
                        <contenttemplate>
                            <asp:DropDownList id="cboUbica" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="&lt; Seleccionar &gt;">&lt; Seleccionar &gt;</asp:ListItem>
                                <asp:ListItem Value="1">Almac&#233;n</asp:ListItem>
                                <asp:ListItem Value="2">Secci&#243;n</asp:ListItem>
                            </asp:DropDownList>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtUbicaCodInterno" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel17" runat="server">
                        <contenttemplate>
                            <asp:Button id="btnBusUbicacion" runat="server" CssClass="EstiloBoton_Ac" Width="25px" Text="..." Enabled="False"></asp:Button> 
                               <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" Y="200" X="200" 
                                   PopupControlID="Panel1" CancelControlID="btnUbicCerrar" CacheDynamicResults="True" BackgroundCssClass="modalBackground" 
                                   TargetControlID="btnBusUbicacion">
                               </cc1:ModalPopupExtender>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboUbica" EventName="TextChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>                    
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel9" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtUbicaDescripcion" runat="server" Width="260px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Destino"></asp:Label>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel16" runat="server">
                        <contenttemplate>
                            <asp:DropDownList id="cboDestino" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                                <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                <asp:ListItem Value="1">Almac&#233;n</asp:ListItem>
                                <asp:ListItem Value="2">Secci&#243;n</asp:ListItem>
                            </asp:DropDownList>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel10" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtDesCodInterno" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel18" runat="server">
                        <contenttemplate>
                            <asp:Button id="btnDestino" runat="server" CssClass="EstiloBoton_Ac" Width="25px" Text="..." Enabled="False"></asp:Button> 
                                <cc1:ModalPopupExtender id="ModalPopupExtender3" runat="server" Y="200" X="200" PopupControlID="Panel1" 
                                  CancelControlID="btnUbicCerrar" CacheDynamicResults="True" BackgroundCssClass="modalBackground" 
                                  TargetControlID="btnDestino">
                                </cc1:ModalPopupExtender>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboDestino" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>             
                    </asp:UpdatePanel></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel11" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtDesDescripcion" runat="server" Width="260px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Artículo"></asp:Label>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtArtCodigo" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                    <asp:Button ID="btnBusArticulo" runat="server" CssClass="EstiloBoton_Ac" Text="..."  />
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtArtDescripcion" runat="server" Width="371px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>                      
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 24px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtiq3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel12" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtFechaIni" runat="server" Width="101px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> <cc1:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtFechaIni" PopupButtonID="txtFechaIni" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel14" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtFechaFin" runat="server" Width="102px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> <cc1:CalendarExtender id="CalendarExtender2" runat="server" TargetControlID="txtFechaFin" PopupButtonID="txtFechaFin" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 170px; height: 22px; text-align: right"
                    valign="top">
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="EstiloBoton_Ac" OnClick="btnLimpiar_Click"
                        Text="Limpiar" Width="95px" /></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Button ID="btnListar" runat="server" CssClass="EstiloBoton_Ac" Text="Listar"
                        Width="95px" /></td>
                <td align="left" style="width: 24px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro. Serie"></asp:Label></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel13" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtSerie" runat="server" Width="215px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 170px; height: 22px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Button ID="btnExportar" runat="server" CssClass="EstiloBoton_Ac" Text="Exportar"
                        Width="95px" /></td>
                <td align="left" style="width: 24px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle" valign="top">
                    <asp:UpdatePanel id="UpdatePanel19" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </contenttemplate>
                        <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 24px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 300px" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 300px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                            <div style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 545px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 300px; border-color: #FFFFFF;">
                                <asp:GridView id="Flex" runat="server" Width="870px" Font-Size="8pt" Font-Names="Arial" Font-Overline="False" AutoGenerateColumns="False"><Columns>
                                    <asp:BoundField DataField="ORIGEN_NOMBRE" HeaderText="Origen">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DESTINO" HeaderText="Destino">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FECHA_MOV" HeaderText="Fecha">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HORA" HeaderText="Hora">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ARTCODIGO" HeaderText="Cod. Art">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO_MOV" HeaderText="Tipo Mov.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"></ItemStyle>
                                    </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:GridView> 
                            </div>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 24px; height: 300px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 170px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top"></td>
                <td align="left" style="width: 24px; height: 22px" valign="top"></td>
            </tr>
        </table>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
            CacheDynamicResults="True" CancelControlID="btnCerrarArt" PopupControlID="Panel2"
            TargetControlID="btnBusArticulo" X="200" Y="200">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
                border-top: gray 1px outset; border-left: gray 1px outset; width: 400px; border-bottom: gray 1px outset;
                background-color: darkgray">
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top"></td>
                    <td align="left" colspan="3" style="height: 20px; text-align: center" valign="top">
                        <asp:Label ID="lblEtiqUbicacion" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Maroon" Text="Relación de Oficina"></asp:Label>
                    </td>
                    <td align="left" style="width: 20px; height: 20px" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 21px" valign="top">                    </td>
                    <td align="left" style="vertical-align: middle; width: 70px; height: 21px" valign="top">
                        <asp:Label ID="lblEtiq4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label>
                    </td>
                    <td align="left" style="width: 210px; height: 21px" valign="top">
                        <asp:TextBox ID="txtBusUbicCodInterno" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="180px"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnUbicCerrar" runat="server" CssClass="EstiloBoton_Ac" OnClick="btnUbicCerrar_Click"
                            Text="Cerrar" Width="70px" />
                    </td>
                    <td align="left" style="width: 20px; height: 21px" valign="top">                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top"></td>
                    <td align="left" style="vertical-align: middle; width: 70px; height: 20px" valign="top">
                        <asp:Label ID="lblEtiq5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"
                            Width="64px"></asp:Label>
                    </td>
                    <td align="left" style="width: 210px; height: 20px" valign="top">
                        <asp:TextBox ID="txtBusUbicDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="180px"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnUbicListar" runat="server" CssClass="EstiloBoton_Ac" Text="Listar"
                            Width="70px" />
                    </td>
                    <td align="left" style="width: 20px; height: 20px" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 266px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="height: 266px" valign="top">
                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                            <contenttemplate>
                            <div style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 240px" id="DIV2" runat="server">
                                <asp:GridView id="FlexUbicacion" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnRowCommand="FlexUbicacion_RowCommand"><Columns>
                                    <asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
                                    <ControlStyle CssClass="EstiloBoton_Ac" Width="30px"></ControlStyle>

                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CODIGO">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="DarkGray" Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                    </Columns>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:GridView> 
                            </div>  
                            </contenttemplate>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnUbicListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            </triggers>
                        </asp:UpdatePanel></td>
                    <td align="left" style="width: 20px; height: 266px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 19px" valign="top"></td>
                    <td align="left" style="width: 70px; height: 19px" valign="top"></td>
                    <td align="left" style="width: 210px; height: 19px" valign="top"></td>
                    <td align="left" style="width: 80px; height: 19px" valign="top"></td>
                    <td align="left" style="width: 20px; height: 19px" valign="top"></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
                border-top: gray 1px outset; border-left: gray 1px outset; width: 400px; border-bottom: gray 1px outset">
                <tr>
                    <td align="left" colspan="5" style="vertical-align: middle; height: 25px; background-color: darkgray;
                        text-align: center" valign="top">
                        <asp:Label ID="lblP3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Maroon" Text="Búsqueda de Artículos"></asp:Label>&nbsp;</td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 18px; background-color: darkgray" valign="top"></td>
                    <td align="left" style="vertical-align: middle; width: 70px; height: 18px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="lblP2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 180px; height: 18px; background-color: darkgray"
                        valign="top">
                        <asp:TextBox ID="txtPArtCodigo" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="170px"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 18px; background-color: darkgray;
                        text-align: center" valign="top">
                        <div style="vertical-align: middle; text-align: right">
                            <asp:Button ID="btnCerrarArt" runat="server" CssClass="EstiloBoton_Ac" Font-Names="Arial"
                                Font-Size="8pt" OnClick="btnCerrarArt_Click" Text="Cerrar" Width="80px" />
                            &nbsp;</div>
                    </td>
                    <td align="left" style="width: 25px; height: 18px; background-color: darkgray" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top"></td>
                    <td align="left" style="vertical-align: middle; width: 70px; height: 19px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="lblP1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 180px; height: 19px; background-color: darkgray"
                        valign="top">
                        <asp:TextBox ID="txtPArtDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="170px"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 19px; background-color: darkgray;
                        text-align: right" valign="top">
                        <asp:Button ID="btnListarArt" runat="server" CssClass="EstiloBoton_Ac" Font-Names="Arial"
                            Font-Size="8pt" OnClick="btnListarArt_Click" Text="Listar" Width="80px" />
                        </td>
                    <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 250px; background-color: darkgray" valign="top"></td>
                    <td align="left" colspan="3" style="vertical-align: baseline; height: 250px; background-color: darkgray"
                        valign="top">
                        <asp:UpdatePanel id="UpdatePanel3" runat="server">
                            <contenttemplate>
                            <div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 350px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV3" runat="server">
                                <asp:GridView id="FlexArt" runat="server" Width="430px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" BorderStyle="Outset" BorderWidth="1px" UseAccessibleHeader="False" AllowPaging="True"><Columns>
                                    <asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
                                    <ControlStyle CssClass="EstiloBoton_Ac" ForeColor="Gray" Width="30px"></ControlStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Codigo">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Nombres" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="350px"></ItemStyle>
                                    </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BorderWidth="1px" BorderStyle="Outset"></HeaderStyle>
                                    <PagerStyle VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></PagerStyle>
                                </asp:GridView> 
                            </div>
                            </contenttemplate>      
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListarArt" EventName="Click"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand" />
                            </triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 250px; background-color: darkgray" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" colspan="5" style="height: 20px; background-color: darkgray" valign="top"></td>
                </tr>
            </table>
        </asp:Panel>
        </div>
</asp:Content>

