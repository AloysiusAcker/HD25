<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Gestion_Inspeccion_Desplazamiento.aspx.vb" Inherits="Gestion_Inspeccion_Desplazamiento" title="Servicio - Gestion de Desplazamiento" %>

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
            <td align="left" colspan="7" style="height: 50px; text-align: center" valign="top">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 14pt; left: 253px; vertical-align: middle; width: 550px; color: gray;
                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                    text-align: center">
                    Gestion de Desplazamiento</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="7" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                valign="middle">
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
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Inspecc."
                                Width="70px"></asp:Label></td>
                        <td align="left" style="width: 140px; height: 22px" valign="middle">
                            <asp:TextBox ID="txtNroInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px"></asp:TextBox></td>
                        <td align="left" style="width: 30px; height: 22px" valign="middle">
                        </td>
                        <td align="right" colspan="5" style="height: 22px" valign="middle">
                            &nbsp;<asp:Button ID="btnListarDatosAdicionales" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Listar" Width="60px" />
                            <asp:Button ID="btnExportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Exportar" Width="60px" /></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"
                                Width="70px"></asp:Label>
                        </td>
                        <td align="left" style="width: 140px; height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPorCodOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        OnTextChanged="txtPorCodOficina_TextChanged" Width="135px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtPorCodOficina" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 30px; height: 22px" valign="middle">
                            <asp:Button ID="btnBuscarXOficina" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" OnClick="btnBuscarXOficina_Click"
                                Text="...." Width="25px" />
                        </td>
                        <td align="left" colspan="5" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPorOficDescrip" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="275px"></asp:TextBox>
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
                                    <asp:DropDownList ID="cboTipoPersona" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="140px" OnSelectedIndexChanged="cboTipoPersona_SelectedIndexChanged">
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
                        <td align="left" style="width: 30px; height: 22px" valign="middle">
                            <asp:Button ID="btnBuscarTipoPersona" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" Text="..." Width="25px" />
                        </td>
                        <td align="left" colspan="3" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtRazonSocialTipoPersona" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="165px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="FlexTipoPers" EventName="RowCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado"
                                Width="65px"></asp:Label>
                        </td>
                        <td align="left" colspan="2" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cboEstadoInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        OnSelectedIndexChanged="cboEstadoInspeccion_SelectedIndexChanged" Width="140px">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarXOficina" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Inspeccion"
                                Width="75px"></asp:Label>
                        </td>
                        <td align="left" colspan="4" style="height: 22px" valign="middle">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cboTipoInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        OnSelectedIndexChanged="cboTipoInspeccion_SelectedIndexChanged" Width="200px">
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
                            <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Inicio"
                                Width="65px"></asp:Label>
                        </td>
                        <td align="left" colspan="2" style="height: 22px" valign="middle">
                            <asp:TextBox ID="txtPorFechaInicio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="135px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtPorFechaInicio"
                                TargetControlID="txtPorFechaInicio" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                        <td align="left" style="width: 80px; height: 22px" valign="middle">
                            <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Fin"
                                Width="75px"></asp:Label>
                        </td>
                        <td align="left" colspan="4" style="height: 22px" valign="middle">
                            <asp:TextBox ID="txtPorFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="135px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2"  runat="server" PopupButtonID="txtPorFechaFin"
                                TargetControlID="txtPorFechaFin" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3" style="height: 25px" valign="middle">
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
                        <td align="left" style="width: 80px; height: 25px" valign="middle">
                            <asp:TextBox ID="txtProcede" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"
                                Width="70px"></asp:TextBox></td>
                        <td align="left" colspan="4" style="height: 25px" valign="middle">
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" style="width: 42px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 360px" valign="middle">
            </td>
            <td align="left" colspan="5" style="height: 360px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div id="DIV2" runat="server" style="border-right: darkgray 1px outset; border-top: gray 1px outset;
                            overflow: auto; border-left: darkgray 1px outset; width: 530px; border-bottom: darkgray 1px outset;
                            position: static; height: 350px">
                            <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                Font-Size="8pt" Width="1200px">
                                <Columns>
                                    <asp:BoundField DataField="FECHA_PROG" HeaderText="Fecha Prog.">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO" HeaderText="Tipo Visita">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HORA_PROG" HeaderText="Hora Prog.">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Oficina">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CECOSE_DESCRIPCION" HeaderText="Nombre Oficina">
                                        <ControlStyle Width="200px" />
                                        <FooterStyle Width="200px" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HORA_LLEGADA" HeaderText="Hora LLegada">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DIRECCION" HeaderText="Direccion">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="400px" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HORA_INICIO" HeaderText="Hora Inicio">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HORA_FIN" HeaderText="Hora Fin">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INSPEC_MOVILIDAD_IDA_2" HeaderText="Movilidad Ida">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INSPEC_MOVILIDAD_VUELTA_2" HeaderText="Movilidad Vuelta">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INSPEC_CODIGO">
                                        <ItemStyle ForeColor="White" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnListarDatosAdicionales" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 42px; height: 360px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="5" style="height: 22px" valign="middle">
                &nbsp;</td>
            <td align="left" style="font-size: 12pt; width: 42px; font-family: Times New Roman;
                height: 22px" valign="middle">
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
    &nbsp;
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        CacheDynamicResults="True" CancelControlID="btnCerrarOficina" PopupControlID="Panel1"
        TargetControlID="btnBuscarXOficina" X="300" Y="300">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        CacheDynamicResults="True" CancelControlID="btnCerrar2" PopupControlID="Panel2"
        TargetControlID="btnBuscarTipoPersona" X="300" Y="300">
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
                                <div id="DIV5" runat="server" style="border-right: gray 1px outset; border-top: gray 1px outset;
                                    overflow: auto; border-left: gray 1px outset; width: 360px; border-bottom: gray 1px outset;
                                    height: 240px">
                                    <asp:GridView ID="FlexTipoPers" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                        Font-Size="8pt" OnRowCommand="FlexTipoPers_RowCommand" Width="360px">
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
                                    </asp:GridView>
                                </div>
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
    <br />
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
    <br />
</asp:Content>

