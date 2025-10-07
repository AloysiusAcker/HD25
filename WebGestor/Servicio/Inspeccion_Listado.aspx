<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Listado.aspx.vb" Inherits="Inspeccion_Listado" title="GestorPlus" %>

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
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Relación del Servicio</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 95px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 20px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 20px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 175px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 95px; height: 25px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Programada"
                        Width="91px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 25px" valign="top">
                    <asp:TextBox ID="txtFechaIni" runat="server" Font-Names="Arial" Font-Size="8pt" Width="85px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 20px; height: 25px" valign="top">
                    <asp:ImageButton ID="I1" runat="server" ImageUrl="~/Fotos/Calendario.bmp" /></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 25px" valign="top">
                    <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="85px"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 20px; height: 25px" valign="top">
                    <asp:ImageButton ID="I2" runat="server" ImageUrl="~/Fotos/Calendario.bmp" /></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 175px; height: 25px; text-align: right;" valign="top">
                    <asp:Button ID="btnExportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Exportar" Width="60px" /><asp:Button ID="btnListar" runat="server" BackColor="LightGray"
                            BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Gray" Text="Listar" Width="60px" /></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 95px; height: 25px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 25px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtCodIntOficina" runat="server" Width="85px" Font-Size="8pt" Font-Names="Arial" OnTextChanged="txtCodIntOficina_TextChanged"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexOf" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 20px; height: 25px" valign="top">
                    <asp:Button ID="btnBuscar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="..." Width="18px" /></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 25px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtOficina" runat="server" Width="340px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexOf" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtCodIntOficina" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 95px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 20px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; text-align: left" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtCodOficina" runat="server" Width="62px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="txtCodIntOficina" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexOf" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 24px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; height: 24px; text-align: left"
                    valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblRegistro" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></asp:Label>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 24px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 274px"><asp:GridView id="Flex" runat="server" Width="1450px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" PageSize="8" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="NRO_VISITA" HeaderText="Nro Visita">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO" HeaderText="Tipo Visita">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_PROG" HeaderText="Fecha Prog.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_PROG" HeaderText="Hora Prog.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPOPER" HeaderText="Tipo Persona">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PERSONA_ASIG" HeaderText="Persona Asignada">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="OFICINA" HeaderText="Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DIRECCION" HeaderText="Direcci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="OBS" HeaderText="Observaci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Left" VerticalAlign="Middle"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
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
                <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
                    <div style="text-align: left">
                        &nbsp;&nbsp;</div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="7">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="I1"
        TargetControlID="txtFechaIni">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="I2"
        TargetControlID="txtFechaFin">
    </cc1:CalendarExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCerrar"
        PopupControlID="Panel1" TargetControlID="btnBuscar" X="300" Y="200" BackgroundCssClass="modalBackground">
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
                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                            <contenttemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px" id="DIV1" runat="server"><asp:GridView id="FlexOf" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="30px"></ControlStyle>

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
</asp:GridView></DIV>
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListarOf" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel></td>
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
</asp:Content>

