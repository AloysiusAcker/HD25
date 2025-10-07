<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_GenerarLista.aspx.vb" Inherits="Inventario_Inventario_GenerarLista" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Generar Listado</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 90px" valign="top"></td>
                <td align="left" style="width: 70px" valign="top"></td>
                <td align="left" style="width: 30px" valign="top"></td>
                <td align="left" style="width: 460px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" colspan="5" style="height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w21"></asp:Label>
                        </ContentTemplate>
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
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ubicación"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
                            <asp:RadioButtonList id="optUbicacion" runat="server" Width="216px" Height="1px" Font-Size="8pt" Font-Names="Arial" OnSelectedIndexChanged="optUbicacion_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True" __designer:wfdid="w22"><asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                            <asp:ListItem Value="1">Almac&#233;n</asp:ListItem>
                            <asp:ListItem Value="2">Centro Costo</asp:ListItem>
                            </asp:RadioButtonList> 
                        </contenttemplate>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="width: 90px; height: 22px; vertical-align: middle;" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left" valign="top">
                    <asp:UpdatePanel id="UpdatePanel9" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtUbiCodigo" runat="server" Width="68px" Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke" ReadOnly="True"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="optUbicacion" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: center" valign="top">
                    <asp:Button ID="btnUbica" runat="server" CssClass="EstiloBoton_Ac" Text="..." Width="22px" /></td>
                <td align="left" style="vertical-align: middle; width: 460px; height: 22px; text-align: left" valign="top">
                    <asp:UpdatePanel id="UpdatePanel10" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtUbiDescripcion" runat="server" Width="448px" Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke" ReadOnly="True"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="optUbicacion" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" Width="96px" CssClass="EstiloBoton_Ac" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"> </td>
                <td align="left" style="width: 90px; height: 22px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Artículo:" Width="48px"></asp:Label></td>
                <td align="left" style="width: 70px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtCodArt" runat="server" Width="68px" Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 30px; height: 22px; vertical-align: middle; text-align: center;" valign="top">
                    <asp:Button ID="btnBuscar" runat="server"  CssClass="EstiloBoton_Ac" Text="..." Width="22px" /></td>
                <td align="left" style="width: 460px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtNomArt" runat="server" Width="448px" Font-Size="8pt" Font-Names="Arial" BackColor="WhiteSmoke"></asp:TextBox>
                        </contenttemplate>              
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                    <asp:Button ID="btnExportar" runat="server"  CssClass="EstiloBoton_Ac" Text="Exportar" Width="96px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Serie"></asp:Label></td>
                <td align="left" colspan="2" style="height: 22px" valign="top">
                    <asp:TextBox ID="txtNroSerie" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox></td>
                <td align="left" style="width: 460px; height: 22px" valign="top">
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Placa"></asp:Label>
                    <asp:TextBox ID="txtPlaca" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                    <asp:Button ID="BtnGenerarLista" runat="server" Text="Generar Lista" Width="96px" CssClass="EstiloBoton_Ac" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="middle">
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Bien"></asp:Label></td>
                <td align="left" colspan="2" style="height: 22px" valign="top">
                    <asp:DropDownList ID="DdlTipo" runat="server" CssClass="EstiloDropDownList">
                        <asp:ListItem Value="2">Mobiliario</asp:ListItem>
                        <asp:ListItem Value="3">Informática</asp:ListItem>
                        <asp:ListItem Selected="True">&lt; Todos &gt;</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 460px; height: 22px" valign="middle">
                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Antiguedad Mayor a "></asp:Label>
                    <asp:DropDownList ID="DdlAntiguedad" runat="server" CssClass="EstiloDropDownList">
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkMarcar" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList" Text="Marcar Todo" />
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>  
             <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top"></td>
                <td align="left" colspan="2" style="height: 22px" valign="top"></td>
                <td align="left" style="width: 460px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel11" runat="server">
                        <contenttemplate>
                            <asp:TextBox id="txtUbicacion" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="optUbicacion" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 100px; height: 22px" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <asp:UpdatePanel id="UpdatePanel20" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblRegEnviar" Text="Lista a tratar : " runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="chkMarcar" EventName="CheckedChanged" />
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div style="width:748px; overflow: auto; border-right: white 1px outset; border-top: white 1px outset; border-left: gray 1px outset; border-bottom: white 1px outset; border-style: none;" id="DIV4" runat="server">
                        <asp:UpdatePanel id="UpdatePanel19" runat="server">
                            <contenttemplate>
                                <asp:GridView id="FlexLista" runat="server" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" PageSize="1000">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chkMar" runat="server" Height="20px" Width="1px" />                                                                      
                                            </ItemTemplate>
                                            <ControlStyle Width="20px"></ControlStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Artículo">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                        <asp:BoundField DataField="TIPOBIEN" HeaderText="Tipo Bien">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adquisición">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Antiguedad" HeaderText="Antiguedad">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro" />
                                        <asp:BoundField DataField="SERIE_NUMERAR">
                                        <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
                            </contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="chkMarcar" EventName="CheckedChanged" />
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div style="width:748px; overflow: auto; border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; border-style: none;" id="DIV1" runat="server">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
                                <asp:GridView id="Flex" runat="server" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" PageSize="1000">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                               <asp:CheckBox ID="chkMar" runat="server" Height="20px" Width="1px" />                                                                      
                                            </ItemTemplate>
                                            <ControlStyle Width="20px"></ControlStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Artículo">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                        <asp:BoundField DataField="TIPOBIEN" HeaderText="Tipo Bien">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adquisición">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Antiguedad" HeaderText="Antiguedad">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro" />
                                        <asp:BoundField DataField="SERIE_NUMERAR">
                                        <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
                            </contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 90px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 70px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 30px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 460px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px" valign="top" colspan="5"></td>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
            </tr>
        </table>
        <div>
        <asp:Panel ID="Panel1" runat="server" Height="50px" Width="345px" style="border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: gray 1px outset;
            border-top: gray 1px outset; border-left: gray 1px outset; width: 400px; border-bottom: gray 1px outset">
            <tr>
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
<asp:TextBox id="txtPArtCodigo" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w56"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 18px; background-color: darkgray;
                    text-align: center" valign="top">
                    <div style="vertical-align: middle; text-align: right">
                        <asp:Button ID="btnCerrarArt" runat="server" CssClass="EstiloBoton_Ac" Font-Names="Arial"
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
                        Font-Size="8pt" OnClick="btnListarArt_Click" Text="Listar" Width="80px" />&nbsp;</td>
                <td align="left" style="width: 25px; height: 19px; background-color: darkgray" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; background-color: darkgray" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; background-color: darkgray"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel15" runat="server">
                        <contenttemplate>
<asp:Label id="lblErrorArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w67"></asp:Label>
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
<asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand"></asp:AsyncPostBackTrigger>
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
<div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 350px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV3" runat="server"><asp:GridView id="FlexArt" runat="server" Width="430px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" __designer:wfdid="w65" AutoGenerateColumns="False" UseAccessibleHeader="False" AllowPaging="True"><Columns>
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
</asp:GridView> </div>
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
            &nbsp;</div>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                TargetControlID="btnBuscar" 
                CancelControlID ="btnCerrarArt"
                PopupControlID ="Panel1"
                X="300"
                Y="200" CacheDynamicResults="True" BackgroundCssClass="modalBackground" Drag="True">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panel2" runat="server">
            <div style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; background-color: darkgray; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;">
                    <tr>
                        <td align="left" style="width: 25px; height: 25px" valign="middle">
                        </td>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 25px; text-align: center"
                            valign="middle">
                            <asp:UpdatePanel id="UpdatePanel16" runat="server">
                                <contenttemplate>
                            <asp:Label ID="lblBusUbica" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Maroon" Text="Busqueda de Almacén y/o Centro de Costos" Width="280px"></asp:Label>
</contenttemplate>
                            </asp:UpdatePanel>&nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 25px; height: 25px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel18" runat="server">
                                <contenttemplate>
                            <asp:TextBox ID="txtBusCod" runat="server" Font-Names="Arial" Font-Size="8pt" Width="270px"></asp:TextBox>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                            <asp:Button ID="btnUbiCerrar" runat="server" BackColor="LightGray" BorderColor="Silver"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Cerrar" Width="80px" /></td>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"
                                Width="60px"></asp:Label></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel17" runat="server">
                                <contenttemplate>
                            <asp:TextBox ID="txtBusDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="270px"></asp:TextBox>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                            <asp:Button ID="btnUbiListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Listar" Width="80px" /></td>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px" valign="middle">
                        </td>
                        <td align="left" colspan="3" valign="middle">
                            <asp:UpdatePanel id="UpdatePanel7" runat="server">
                                <contenttemplate>
<div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 450px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Height="139px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w64" AutoGenerateColumns="False" Font-Overline="False"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="30px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CODIGO">
<ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView></div>
</contenttemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 19px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="width: 70px; height: 19px">
                        </td>
                        <td align="left" valign="middle" style="width: 280px; height: 19px">
                        </td>
                        <td align="left" valign="middle" style="width: 100px; height: 19px">
                        </td>
                        <td align="left" style="width: 25px; height: 19px;" valign="middle">
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
            CacheDynamicResults="True" CancelControlID="btnUbiCerrar" PopupControlID="Panel2"
            TargetControlID="btnUbica" X="300" Y="200">
        </cc1:ModalPopupExtender>
    </div>
</asp:Content>

