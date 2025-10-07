
<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_ReclamoLista.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_ReclamoLista" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
       <div style="text-align: center">
       <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px; background-color: white;">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline; font-weight: bold; font-size: 14pt; vertical-align: middle; width: 650px; color: gray; font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute; height: 1px; text-align: center">
                        Relación de Reclamo
                    </div>
                </td>.
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 950px; height: 20px;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblError" runat="server" Font-Size="8" Font-Names="arial" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 950px; height: 20px;" valign="middle">
                    <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton" Text="Listar" Height="19px" />
                    <asp:Button ID="BtnRegistrar" runat="server" CssClass="EstiloBoton" Text="Registrar Reclamo" />
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 950px; height: 20px;" valign="middle">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblRegistro" runat="server" Font-Size="8" Font-Names="arial" ForeColor="Maroon"></asp:Label>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 950px; height: 20px;" valign="middle">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="divLista">
                                <asp:GridView ID="gwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" Width="950px">
                                    <Columns>
                                        <asp:ButtonField CommandName="Editar" Text="Cambiar Estado" >
                                        <ControlStyle CssClass="EstiloBoton" Width="90px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="90px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="Detalle" Text="Detalle">
                                        <ControlStyle CssClass="EstiloBoton" Width="70px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COD_RECLAMO" HeaderText="Cod. Reclamo">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha Reclamo" />
                                        <asp:BoundField DataField="OFICINA_NOMBRE" HeaderText="Tienda">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONSUMIDOR" HeaderText="Consumidor ">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECLAMO_DOMICILIO" HeaderText="Dirección ">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECLAMO_EMAIL" HeaderText="E-Mail " />
                                        <asp:BoundField DataField="BIEN" HeaderText="Bien Contratado" />
                                        <asp:BoundField DataField="RECLAMO_DESCRIPCION" HeaderText="Motivo Reclamo" />
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width:950px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">&nbsp;</td>
                <td align="left" style="width:950px; height: 20px;" valign="middle">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DetailsView id="DetalleLista" runat="server" Width="950px" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" BackColor="White" BorderWidth="1px" BorderStyle="None" BorderColor="LightGray" AutoGenerateRows="False" CellPadding="4">
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
                                <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
                                <Fields>
                                    <asp:BoundField DataField="COD_RECLAMO" HeaderText="Nro. Reclamo">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OFICINA_NOMBRE" HeaderText="Tienda">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CONSUMIDOR" HeaderText="Consumidor">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECLAMO_DOMICILIO" HeaderText="Dirección">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BIEN" HeaderText="Bien Contratado">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECLAMO_MONTO" HeaderText="Monto Reclamo">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="850px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECLAMO_DESCRIPCION" HeaderText="Descripción ">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="850px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO" HeaderText="Tipo">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECLAMO_DETALLE" HeaderText="Detalle">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="RECLAMO_OBS" HeaderText="Observación">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle Width="850px"></ItemStyle>
                                    </asp:BoundField>
                                </Fields>
                                <HeaderStyle BackColor="#333333" BorderColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></EditRowStyle>
                            </asp:DetailsView>
                        </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="gwLista" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width:950px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
       </table>

        <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
            <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div style="position: relative; top: 30%; text-align: center;">
                        <img src="/Fotos/5.gif" /></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />

  </div>


</asp:Content>

