<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Contabilidad_Voucher.aspx.vb" Inherits="Contabilidad_Voucher" title="GestorPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px; text-align: right;">
            <tr>
                <td align="left" valign="top" style="height: 50px; text-align: center" colspan="8">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Comprobantes</div>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="height: 11px" colspan="8">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 11px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 15px; text-align: right"
                    valign="top">
                    <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray" CssClass="EstiloBoton" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="80px" />
                    <asp:Button ID="btnNuevo" runat="server" BackColor="LightGray" BorderColor="Gray" CssClass="EstiloBoton" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Nuevo Comprobante" Width="156px" />
                    </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 11px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    &nbsp;<asp:Label ID="lblEtiq1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Año"
                        Width="26px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    <asp:CheckBox ID="chkPeriodo" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Text="Periodos" Width="70px" /></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboPeriodos" runat="server" Width="260px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Enabled="False"></asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="chkPeriodo" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboA&#241;o" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 20px; text-align: right"
                    valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    <asp:DropDownList ID="cboAño" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="66px">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    <asp:CheckBox ID="chkAsiento" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Text="Asientos" Width="70px" /></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboAsientos" runat="server" Width="260px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Enabled="False" EnableTheming="True"></asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="chkAsiento" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboA&#241;o" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 90px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    <asp:CheckBox ID="chkMes" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Mes Calendario" Width="94px" /></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboMes" runat="server" Width="96px" Font-Size="8pt" Font-Names="Arial" Enabled="False" EnableTheming="True">
                            </asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="chkMes" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 20px" valign="top">
                    <asp:CheckBox ID="chkMoneda" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Moneda" Width="60px" /></td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboMoneda" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" Enabled="False" EnableTheming="True"></asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="chkMoneda" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 20px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
<DIV style="BORDER-RIGHT: gainsboro 1px outset; BORDER-TOP: gainsboro 1px outset; OVERFLOW: auto; BORDER-LEFT: gainsboro 1px outset; WIDTH: 550px; BORDER-BOTTOM: gainsboro 1px outset; POSITION: static; HEIGHT: 200px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Continuar" Text="Continuar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="DimGray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="DimGray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="COMPROB_PERIODO" HeaderText="Per."></asp:BoundField>
<asp:BoundField DataField="FEC_REG" HeaderText="Fecha Registro"></asp:BoundField>
<asp:BoundField DataField="COMPROB_ASIENTO_CODIGO" HeaderText="Tipo Asiento"></asp:BoundField>
<asp:BoundField DataField="COMPROB_NRO_VOUCHER" HeaderText="N&#186; Comprob."></asp:BoundField>
<asp:BoundField DataField="PLAN_CUENTA" HeaderText="Cuenta"></asp:BoundField>
<asp:BoundField DataField="MONEDAV" HeaderText="Moneda"></asp:BoundField>
<asp:BoundField DataField="COMPROB_IMPORTE_DEBE_S" HeaderText="Debe (S/.)"></asp:BoundField>
<asp:BoundField DataField="COMPROB_IMPORTE_HABER_S" HeaderText="Haber (S/.)"></asp:BoundField>
<asp:BoundField DataField="COMPROB_IMPORTE_DEBE_D" HeaderText="Debe ($.)"></asp:BoundField>
<asp:BoundField DataField="COMPROB_IMPORTE_HABER_D" HeaderText="Haber ($.)"></asp:BoundField>
<asp:BoundField DataField="COMPROB_TIPOCAM" HeaderText="Tipo Cambio"></asp:BoundField>
<asp:BoundField DataField="FEC_DOC" HeaderText="Fecha Dcto."></asp:BoundField>
<asp:BoundField DataField="FEC_VCTO" HeaderText="Fecha Vcto."></asp:BoundField>
<asp:BoundField DataField="COMPROB_DOC_CODIGO" HeaderText="Tipo Dcto."></asp:BoundField>
<asp:BoundField DataField="COMPROB_NRO_DOC" HeaderText="N&#186; Dcto."></asp:BoundField>
<asp:BoundField DataField="COMPROB_DOC_REF" HeaderText="Tipo Dcto. Ref."></asp:BoundField>
<asp:BoundField DataField="COMPROB_NRO_DOC_REF" HeaderText="N&#186; Dcto. Ref."></asp:BoundField>
<asp:BoundField DataField="RUC" HeaderText="R.U.C."></asp:BoundField>
<asp:BoundField DataField="COMPROB_GLOSA" HeaderText="Glosa"></asp:BoundField>
<asp:BoundField DataField="CENTRO_COSTOV" HeaderText="Centro Costo"></asp:BoundField>
<asp:BoundField DataField="COMPROB_NUMERAR" HeaderText="N"></asp:BoundField>
<asp:BoundField DataField="COMPROB_RELAC_COMPROB" HeaderText="R"></asp:BoundField>
<asp:BoundField DataField="COMPROB_DIFERENCIA_D"></asp:BoundField>
<asp:BoundField DataField="PART_PRESU" HeaderText="Partida Presupuestaria"></asp:BoundField>
<asp:BoundField DataField="FLUJOCAJA" HeaderText="Flujo de Caja"></asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView></DIV>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 25px; height: 20px">
                </td>
                <td align="left" valign="top" style="height: 20px" colspan="5">
                    &nbsp;</td>
                <td align="left" style="width: 90px; height: 20px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 25px; height: 20px">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 25px; height: 20px">
                </td>
                <td align="left" valign="top" style="height: 20px" colspan="5">
                </td>
                <td align="left" style="width: 90px; height: 20px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 25px; height: 20px">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 25px; height: 21px">
                </td>
                <td align="left" valign="top" style="height: 21px; vertical-align: middle;" colspan="6">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#C00000"></asp:Label></td>
                <td align="left" valign="top" style="width: 25px; height: 21px">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

