<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Lista_DatosAdicionales.aspx.vb" Inherits="Inspeccion_Lista_DatosAdicionales" title="Servicio - Movilidad" %>

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
            <td align="left" colspan="7" style="height: 50px; text-align: center;" valign="top">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 14pt; left: 253px; vertical-align: middle; width: 600px; color: gray;
                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                    text-align: center">
                    Datos Adicionales</div>
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="middle" colspan="7">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 100px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 100px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 100px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 100px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 150px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 42px; height: 15px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="5" style="height: 22px" valign="middle">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 550px">
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Inspecc."
                                Width="63px"></asp:Label></td>
                        <td align="left" style="width: 100px; height: 22px" valign="middle">
                            <asp:TextBox ID="txtNroInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="95px"></asp:TextBox></td>
                        <td align="left" style="width: 30px; height: 22px" valign="middle">
                        </td>
                        <td align="right" colspan="5" style="height: 22px" valign="middle">
                        <asp:Button ID="btnListarDatosAdicionales" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                    Text="Listar" Width="60px" />
                <asp:Button ID="btnExportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                    Text="Exportar" Width="60px" /></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Prog."
                                Width="61px"></asp:Label></td>
                        <td align="right" colspan="7" style="vertical-align: middle; height: 22px; text-align: left"
                            valign="middle">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 300px">
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 110px; height: 22px; text-align: left"
                                        valign="top">
                            <asp:TextBox ID="txtPorFechaInicio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="95px"></asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 110px; height: 22px; text-align: left"
                                        valign="top">
                            <asp:TextBox ID="txtPorFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="95px"></asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"
                                Width="40px"></asp:Label>
                        </td>
                        <td align="left" style="width: 100px; height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPorCodOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        OnTextChanged="txtPorCodOficina_TextChanged" Width="95px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 30px; height: 22px; vertical-align: middle; text-align: center;" valign="middle">
                            <asp:Button ID="btnBuscarXOficina" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" OnClick="btnBuscarXOficina_Click"
                                Text="...." Width="25px" />
                        </td>
                        <td align="left" colspan="5" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPorOficDescrip" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="336px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="FlexOficina" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tecnico"
                                Width="70px"></asp:Label>
                        </td>
                        <td align="left" colspan="2" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                            <asp:DropDownList ID="cboTipoPersona" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="128px" AutoPostBack="True">
                            </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboTipoPersona" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                            <asp:TextBox ID="txtRucTipoPersona" runat="server" Font-Names="Arial" Font-Size="8pt"
                                OnTextChanged="txtRucTipoPersona_TextChanged" Width="75px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="FlexTipoPers" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 30px; height: 22px; vertical-align: middle; text-align: center;" valign="middle">
                            <asp:Button ID="btnBuscarTipoPersona" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" Text="..." Width="25px" />
                        </td>
                        <td align="left" colspan="3" style="height: 22px; width: 230px;" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                            <asp:TextBox ID="txtRazonSocialTipoPersona" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="225px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="FlexTipoPers" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 19px" valign="middle">
                            <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Inspeccion"
                                Width="75px"></asp:Label></td>
                        <td align="left" colspan="7" style="height: 19px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cboTipoInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        OnSelectedIndexChanged="cboTipoInspeccion_SelectedIndexChanged" Width="470px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarXOficina" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado"
                                Width="48px"></asp:Label></td>
                        <td align="left" colspan="7" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
<asp:DropDownList id="cboEstadoInspeccion" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" OnSelectedIndexChanged="cboEstadoInspeccion_SelectedIndexChanged">
                                    </asp:DropDownList> 
</ContentTemplate>
                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnBuscarXOficina" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                            </asp:UpdatePanel></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="8" style="height: 25px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblRegistro" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Maroon"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnListarDatosAdicionales" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtPorFechaFin"
                                TargetControlID="txtPorFechaFin" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtPorFechaInicio"
                                TargetControlID="txtPorFechaInicio" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
            </td>
            <td align="left" style="width: 42px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 210px;" valign="middle">
            </td>
            <td align="left" colspan="5" valign="middle" style="height: 210px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 245px" id="DIV2" runat="server"><asp:GridView id="Flex" runat="server" Width="2060px" Font-Size="8pt" Font-Names="Arial" OnRowCommand="Flex_RowCommand" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="NRO_VISITA" HeaderText="Nro Visita">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO" HeaderText="Tipo Visita">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_ASIG" HeaderText="Tecnico">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="OFICINA" HeaderText="Oficina">
<ControlStyle Width="200px"></ControlStyle>

<FooterStyle Width="200px"></FooterStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_RELIZA" HeaderText="F. Realizada">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_LLEGADA" HeaderText="Hora LLegada">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_INICIO" HeaderText="Hora Inicio">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_EXTRAS" HeaderText="Horas Extras">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_FIN" HeaderText="Hora Termino">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_FIN" HeaderText="Fecha Fin">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_MOVILIDAD" HeaderText="Mov. Ida">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_MOVILIDAD_VUELTA" HeaderText="Mov. Vuelta">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_MOVILIDAD_DESCRIPCION" HeaderText="Mov. Descripcion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="OBJETIVO" HeaderText="Estado Obj.">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSEPC_OBJETIVO" HeaderText="Objetivo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_OBS" HeaderText="Observacion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_MOVILIDAD_IDA_2" HeaderText="Mov. Ida 2">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_MOVILIDAD_VUELTA_2" HeaderText="Mov. Vuelta 2">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_MOVILIDAD_DESCRIPCION_2" HeaderText="Mov. Descripcion 2">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INSPEC_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="EXTRA_HORA_1" HeaderText=" 2 Primeras Horas">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="EXTRA_HORA_2" HeaderText="pasadas 2 p. hr.">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_PROG" HeaderText="Fecha Prog.">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_PROG" HeaderText="Hora Prog.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
    <asp:BoundField DataField="INSPEC_MOV_PROCEDE" HeaderText="Procede" />
</Columns>
</asp:GridView> </DIV>
</ContentTemplate>
                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListarDatosAdicionales" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                </asp:UpdatePanel>
                &nbsp;
            </td>
            <td align="left" style="width: 42px; height: 210px;" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px;" valign="middle">
            </td>
            <td align="left" colspan="5" style="height: 22px" valign="middle">           
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
               
                            <table border="0" runat="server" cellpadding="0" cellspacing="0" style="width: 550px" id="lblMovilidad" visible="false">
                                <tr>
                                    <td align="left" colspan="2" style="height: 22px" valign="middle">
                                        <asp:Label ID="lblEditarMovilidad" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Text="Editar" Visible="False"></asp:Label></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtNroInspecc" runat="server" Font-Names="Arial" Font-Size="8pt" Width="95px" Visible="False"></asp:TextBox></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 130px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Visita"></asp:Label></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtNroVisita" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="95px" Enabled="False"></asp:TextBox></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Procede"></asp:Label></td>
                                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="middle">
                                        <asp:RadioButtonList ID="OptProcede" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>SI</asp:ListItem>
                                            <asp:ListItem Selected="True">NO</asp:ListItem>
                                        </asp:RadioButtonList></td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Movilidad Ida 1"
                                            Width="95px"></asp:Label></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtMovilidadIda1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="95px" Enabled="False"></asp:TextBox></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Movilidad Vuelta 1"
                                            Width="95px"></asp:Label></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtMovilidadVuelta1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="95px" Enabled="False"></asp:TextBox></td>
                                    <td align="left" style="width: 130px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Movilidad Ida 2"
                                            Width="95px"></asp:Label></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtMovilidadIdaEditar" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="95px"></asp:TextBox></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Movilidad Vuelta 2"
                                            Width="95px"></asp:Label></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtMovilidadVueltaEditar" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="95px"></asp:TextBox></td>
                                    <td align="left" style="width: 130px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Movilidad Descripcion 1"
                                            Width="95px"></asp:Label></td>
                                    <td align="left" colspan="4" style="height: 22px" valign="middle">
                                        <asp:TextBox ID="txtMovilidadDescripcion1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            TextMode="MultiLine" Width="425px" Enabled="False"></asp:TextBox></td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Movilidad Descripcion 2"
                                            Width="95px"></asp:Label></td>
                                    <td align="left" colspan="4" style="height: 22px" valign="middle">
                                        <asp:TextBox ID="txtMovilidadDescripcionEditar" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            TextMode="MultiLine" Width="425px"></asp:TextBox></td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 130px; height: 22px" valign="middle">
                                        <asp:Button ID="btnCancelarMovilidad" runat="server" BackColor="LightGray" BorderColor="Gray"
                                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" OnClick="btnCancelarMovilidad_Click1"
                                            Text="Cancelar" Width="60px" />
                                        <asp:Button ID="btnGuardarMovilidad" runat="server" BackColor="LightGray" BorderColor="Gray"
                                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" OnClick="btnGuardarMovilidad_Click"
                                            Text="Guardar" Width="60px" /></td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                        <asp:TextBox ID="txtProcede" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"
                                            Width="44px"></asp:TextBox></td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 100px; height: 22px" valign="middle">
                                    </td>
                                    <td align="left" style="width: 100px; height: 22px; vertical-align: middle; text-align: right;" valign="middle">
                                    </td>
                                    <td align="left" style="width: 130px; height: 22px; text-align: right" valign="middle">
                                        </td>
                                    <td align="left" style="width: 19px; height: 22px" valign="middle">
                                        </td>
                                </tr>
                            </table>       
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarMovilidad" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardarMovilidad" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 42px; height: 22px; font-size: 12pt; font-family: Times New Roman;" valign="middle">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 150px" valign="middle">
            </td>
            <td align="left" style="width: 42px" valign="middle">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 100px" valign="middle">
            </td>
            <td align="left" style="width: 150px" valign="middle">
            </td>
            <td align="left" style="width: 42px" valign="middle">
            </td>
        </tr>
    </table>
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
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        CacheDynamicResults="True" CancelControlID="btnCerrarOficina" PopupControlID="Panel1"
        TargetControlID="btnBuscarXOficina" X="300" Y="300">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        CacheDynamicResults="True" CancelControlID="btnCerrar2" PopupControlID="Panel2"
        TargetControlID="btnBuscarTipoPersona" X="300" Y="300">
    </cc1:ModalPopupExtender>
    <br />
    <asp:Panel ID="Panel2" runat="server">
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
                        <asp:Label ID="lbltipoper" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Text="Relación de Tipo Persona" Width="110px"></asp:Label>
                    </td>
                    <td align="left" style="width: 80px; height: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 25px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        <asp:Label ID="lblruc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="RUC"></asp:Label>
                    </td>
                    <td align="left" style="width: 200px; height: 22px" valign="top">
                        <asp:TextBox ID="txtRucTipoPers" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnCerrar2" runat="server" BackColor="LightGray" BorderColor="Gray"
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
                        <asp:Label ID="lbldesc" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label>
                    </td>
                    <td align="left" style="width: 200px; height: 20px" valign="top">
                        <asp:TextBox ID="txtRazonSocialTipoPers" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListarTipoPers" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            OnClick="btnListarTipoPers_Click1" Text="Listar" Width="70px" />
                    </td>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 266px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="height: 266px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 240px" id="DIV5" runat="server"><asp:GridView id="FlexTipoPers" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnRowCommand="FlexTipoPers_RowCommand" __designer:wfdid="w3">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="&lt;&lt;">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="CODINTERNO" HeaderText="CODIGO">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CODIGO">
                                                <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Top" Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListarTipoPers" EventName="Click" />
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
                    <td align="left" style="width: 80px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 19px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="txtcodOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                Height="10px" Visible="False" Width="94px"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="FlexOficina" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="txtCodigoInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                Visible="False" Width="91px"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="FlexOficina" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="txtTecnico" runat="server" Font-Names="Arial" Font-Size="8pt" Height="11px"
                Visible="False" Width="122px"></asp:TextBox>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="FlexTipoPers" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

