<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Lista_EquiposATratar.aspx.vb" Inherits="Inventario_Inventario_Lista_EquiposATratar" %>

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
                        Enviar Lista a Tratar</div>
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
                   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                            <asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w21"></asp:Label>
             <%--           </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>--%>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="width: 90px; height: 22px; vertical-align: middle;" valign="top">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" Width="76px" CssClass="EstiloBoton_Ac" /></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: center" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 460px; height: 22px; text-align: left" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
<%--                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>--%>
                            <asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
<%--                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </triggers>
                    </asp:UpdatePanel>--%>

                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div style="width:748px; overflow: auto; border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-right-color: white; border-bottom-color: white; border-left-color: white; border-style: none;" id="DIV2" runat="server">
                <%--        <asp:UpdatePanel id="UpdatePanel3" runat="server">
                            <contenttemplate>--%>
                                <asp:GridView id="Flex" runat="server" Font-Size="8pt" CssClass="table table-bordered GridView" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" PageSize="1000">
                                    <Columns>
                                        <asp:ButtonField Text="Detalle" CommandName="Detalle">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="Enviar" Text="Enviar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COD_REG" HeaderText="# Reg.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HORA" HeaderText="Hora">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANT" HeaderText="Cant. Eq." />
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ESTADO">
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
<%--                            </contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                   <%-- <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>--%>
                            <asp:Label id="lblRegDetalle" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
            <%--            </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </triggers>
                    </asp:UpdatePanel>--%>

                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
               <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div style="width:748px; overflow: auto; border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; border-style: none;" id="DIV1" runat="server">
        <%--                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>--%>
                                <asp:GridView id="FlexDet" runat="server" Font-Size="8pt" CssClass="table table-bordered GridView" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" >
                                    <Columns>
                                        <asp:ButtonField Text="Quitar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        </asp:ButtonField>
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
                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                        <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro" />
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
      <%--                      </contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
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
    </div>
</asp:Content>

