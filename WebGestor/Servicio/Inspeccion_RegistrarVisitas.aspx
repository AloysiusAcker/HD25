<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_RegistrarVisitas.aspx.vb" Inherits="Inspeccion_RegistrarVisitas" title="Servicio - Registrar" %>

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
     <asp:UpdatePanel id="UpdatePanel2" runat="server">
        <contenttemplate>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: top; height: 50px; text-align: center"
                    valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 253px; vertical-align: middle; width: 548px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; text-align: center">
                        Registrar Servicio</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 10px" valign="top">
                    <asp:Label ID="LblNumero" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"
                        Width="60px"></asp:Label></td>
                <td align="left" style="width: 470px; height: 10px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 10px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        ForeColor="Maroon"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                            <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Numero"
                                Width="60px"></asp:Label></td>
                <td align="left" colspan="1" style="vertical-align: middle; width: 470px; height: 22px;
                    text-align: left" valign="top">
                            <asp:TextBox ID="txtnumero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="73px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo"></asp:Label></td>
                <td align="left" colspan="1" style="vertical-align: middle; width: 470px; height: 22px;
                    text-align: left" valign="top">
                    <asp:DropDownList ID="CboTipo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="460px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                            <asp:Label ID="lbl3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Persona" Width="63px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <asp:DropDownList ID="cboTecnico" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="150px" OnSelectedIndexChanged="cboTecnico_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Persona"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 470px">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: left"
                                    valign="top">
                                    <asp:TextBox ID="txtTipoPersona" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="90px"></asp:TextBox></td>
                                <td align="left" style="width: 30px; height: 22px" valign="top">
                                    <asp:Button ID="btnBuscarTipoPersona" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="..." Width="20px" CssClass="EstiloBoton" Height="20px" /></td>
                                <td align="left" style="vertical-align: middle; width: 340px; height: 22px; text-align: left"
                                    valign="top">
                                    <asp:TextBox ID="txtTipoNombre" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="323px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 470px">
                            <tr>
                                <td align="left" style="width: 100px; height: 22px" valign="top">
                                    <asp:TextBox ID="txtOficina" runat="server" Font-Names="Arial" Font-Size="8pt" Width="90px"></asp:TextBox></td>
                                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: left"
                                    valign="top">
                <asp:Button ID="btnBuscar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="..." Width="20px" CssClass="EstiloBoton" Height="20px" /></td>
                                <td align="left" style="width: 340px; height: 22px" valign="top">
                                    <asp:TextBox ID="txtOficinaDesc" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="323px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                          <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Prog." Width="62px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 470px">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: left"
                                    valign="top">
                                    <asp:TextBox ID="txtFechaProgramada" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="90px"></asp:TextBox></td>
                                <td align="left" style="vertical-align: middle; width: 33px; height: 22px; text-align: left"
                                    valign="top">
                <asp:ImageButton ID="I1" runat="server" ImageUrl="~/Fotos/Calendario.bmp" Width="20px" /></td>
                                <td align="left" style="vertical-align: middle; width: 60px; height: 22px; text-align: left"
                                    valign="top">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Hora Prog." Width="54px"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 110px; height: 22px; text-align: left"
                                    valign="top">
                                    <asp:TextBox ID="txtHoraProg" runat="server" Font-Names="Arial" Font-Size="8pt" Width="90px"></asp:TextBox></td>
                                <td align="left" style="vertical-align: middle; width: 60px; height: 22px; text-align: left"
                                    valign="top">
                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tiempo P." Width="51px"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 110px; height: 22px; text-align: left"
                                    valign="top">
                                    <asp:TextBox ID="txtTiempoProgramado" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="93px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </div>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="I1"
                        TargetControlID="txtFechaProgramada">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ClearMaskOnLostFocus="False"
                        Mask="99:99" MaskType="Number" TargetControlID="txtTiempoProgramado">
                    </cc1:MaskedEditExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="False"
                        Mask="99:99" MaskType="Number" TargetControlID="txtHoraProg">
                    </cc1:MaskedEditExtender>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Prioridad" Width="62px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 470px">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 130px; height: 22px; text-align: left"
                                    valign="top">
                                    <asp:DropDownList ID="cboPrioridad" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="120px">
                                        <asp:ListItem>&lt; Seleccionar &gt;</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList></td><td align="left" style="vertical-align: middle; width: 60px; height: 22px; text-align: left"
                                    valign="top">
                    <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Motivo" Width="34px"></asp:Label></td>
                                <td align="left" colspan="1" style="vertical-align: middle; width: 280px; height: 22px;
                                    text-align: left" valign="top">
                                    <asp:DropDownList ID="cboMotivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="270px">
                                    </asp:DropDownList></td>
                            </tr>
                    </table>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 80px; height: 22px; text-align: left"
                    valign="top">
                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Observacion"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <asp:TextBox ID="txtObservacion" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="44px" MaxLength="500" TextMode="MultiLine" Width="453px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 80px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Objetivo"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <asp:TextBox ID="txtObjetivo" runat="server" Font-Names="Arial" Font-Size="8pt" Height="44px"
                        MaxLength="500" TextMode="MultiLine" Width="453px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: top; width: 80px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripcion"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <asp:TextBox ID="txtDescrip" runat="server" Font-Names="Arial" Font-Size="8pt" Height="44px"
                        MaxLength="500" TextMode="MultiLine" Width="453px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 470px; height: 22px; text-align: left"
                    valign="top">
                    <asp:Button ID="btnGrabar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" onmouseout="this.style.fontWeight='normal'"
                                onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="78px" OnClick="btnGrabar_Click" />
                    <asp:Button ID="btnNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" onmouseout="this.style.fontWeight='normal'"
                                onmouseover="this.style.fontWeight='bolder'" Text="Limpiar" Width="78px" OnClick="btnNuevo_Click" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                    valign="top">
                    <asp:Label ID="lblEtiqPendiente" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Maroon" Text="Pendiente" Visible="False"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                    valign="top">
                    <div id="lblPendiente" runat="server" style="border-right: gray 1px outset; border-top: gray 1px outset;
                        overflow: auto; border-left: gray 1px outset; width: 520px; border-bottom: gray 1px outset;
                        height: 100px" visible="false">
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" BorderColor="DarkGray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Width="660px">
                            <Columns>
                                <asp:BoundField DataField="Cod_Interno" HeaderText="Oficina">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTADO_FINAL" HeaderText="Estado">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CECOSE_ESTADO_OBS" HeaderText="Observaci&#243;n">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; text-align: left"
                    valign="top">
                    <asp:TextBox ID="txtTecnico" runat="server" Font-Names="Arial" Font-Size="8pt" Height="11px"
                        Visible="False" Width="1px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 470px; text-align: left"
                    valign="top">
                    &nbsp;<asp:TextBox ID="txtcodOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="10px" Visible="False" Width="94px"></asp:TextBox>
                    &nbsp;
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; text-align: left" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 470px; text-align: left" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnCerrar" PopupControlID="Panel1" TargetControlID="btnBuscar"
        X="300" Y="200" CacheDynamicResults="True">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnCerrar2" PopupControlID="Panel2" TargetControlID="btnBuscarTipoPersona"
        X="300" Y="200" CacheDynamicResults="True">
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
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Text="Relación de Oficina" Width="110px"></asp:Label></td>
                    <td align="left" style="width: 80px; height: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 25px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                    <td align="left" style="width: 200px; height: 22px" valign="top">
                        <asp:TextBox ID="txtBusCodigo" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnCerrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Cerrar" Width="70px" /></td>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px" valign="top">
                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label></td>
                    <td align="left" style="width: 200px; height: 20px" valign="top">
                        <asp:TextBox ID="txtBusDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListarOf" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="70px" /></td>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px" valign="top">
                    </td>
                    <td align="left" colspan="3" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="DIV2" runat="server"><asp:GridView id="FlexOf" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnRowCommand="FlexOf_RowCommand">
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
                                    </asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListarOf" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 20px" valign="top">
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
    &nbsp;
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
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Text="Relación de Tipo Persona" Width="110px"></asp:Label></td>
                    <td align="left" style="width: 80px; height: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 25px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="RUC"></asp:Label></td>
                    <td align="left" style="width: 200px; height: 22px" valign="top">
                        <asp:TextBox ID="txtRuc" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnCerrar2" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Cerrar" Width="70px" /></td>
                    <td align="left" style="width: 20px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px" valign="top">
                        <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label></td>
                    <td align="left" style="width: 200px; height: 20px" valign="top">
                        <asp:TextBox ID="txtRazonSocial" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 20px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListarTipoPers" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="70px" /></td>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px" valign="top">
                    </td>
                    <td align="left" colspan="3" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="DIV3" runat="server"><asp:GridView id="FlexTipoPers" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnRowCommand="FlexOf_RowCommand">
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
                    <td align="left" style="width: 20px" valign="top">
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
    </asp:Panel></contenttemplate>
        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexOf" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexTipoPers" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
    </asp:UpdatePanel>
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <div style="text-align: left">
        &nbsp;</div>
</asp:Content>

