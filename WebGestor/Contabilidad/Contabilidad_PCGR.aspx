<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Contabilidad_PCGR.aspx.vb" Inherits="Contabilidad_PCGR" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td align="left" colspan="6" style="height: 50px; text-align: center" valign="top">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                    font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                    Plan Contable General</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="6" style="height: 11px" valign="top">
                <img src="../Fotos/linea.JPG" /></td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 20px" valign="top">
            </td>
            <td align="left" style="width: 50px; height: 20px" valign="top">
            </td>
            <td align="left" style="width: 150px; height: 20px" valign="top">
            </td>
            <td align="left" style="width: 150px; height: 20px" valign="top">
            </td>
            <td align="left" style="width: 200px; height: 20px" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 20px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 19px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 50px; height: 19px; text-align: left"
                valign="top">
                <asp:Label ID="lblEtiq1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Año"
                    Width="26px"></asp:Label></td>
            <td align="left" style="vertical-align: middle; width: 150px; height: 19px; text-align: left"
                valign="top">
                <asp:DropDownList ID="cboAño" runat="server" AutoPostBack="True" Font-Names="Arial"
                    Font-Size="8pt" Width="66px">
                </asp:DropDownList>
                <asp:Button ID="btnNuevo" runat="server" CssClass="EstiloBoton_Ac" onmouseout="this.style.fontWeight='normal'"
                    onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="60px" Visible="False" /></td>
            <td align="left" style="width: 150px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 200px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 19px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 20px" valign="top">
            </td>
            <td align="left" style="height: 20px; vertical-align: middle;" valign="top" colspan="4">
                <asp:Label ID="lblRegistro" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" Font-Bold="True"></asp:Label></td>
            <td align="left" style="width: 25px; height: 20px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 19px" valign="top">
            </td>
            <td align="left" colspan="4" style="height: 19px" valign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 275px" id="DIV1" runat="server"><asp:GridView style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: darkgray; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: darkgray; OVERFLOW: auto; BORDER-TOP-COLOR: darkgray; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: darkgray" id="Flex" runat="server" Width="1030px" Font-Size="8pt" Font-Names="Arial" PageSize="7" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="PLAN_CUENTA" HeaderText="Cuenta">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_NOMBRE_CUENTA" HeaderText="Nombre Cuenta">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_NIVEL_CUENTA" HeaderText="Nivel Cuenta">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_TIPO_CUENTA" HeaderText="Tipo Cuenta">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_TIPO_SALDO" HeaderText="Tipo de Saldo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_TIPO_ANALISIS" HeaderText="Tipo de Analisis">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_CENTRO_COSTOS" HeaderText="Centro de Costo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_PRESUPUESTO" HeaderText="Presu - Puesto">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_FLUJOCAJA" HeaderText="Flujo de Caja">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_ASIENTO_DESTINO" HeaderText="Asiento Destino">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUENTA_DEUDORA" HeaderText="Cuenta Deudora">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUENTA_ACREEDORA" HeaderText="Cuenta Acreedora">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PLAN_COD_NIVEL">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE_BANCO" HeaderText="Rel #Cuenta de Banco">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="IMPUESTON" HeaderText="Relacionado con un Impuesto">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                    <Triggers>
<asp:AsyncPostBackTrigger ControlID="cboA&#241;o" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
</Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 19px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 50px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 150px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 150px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 200px; height: 19px" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 19px" valign="top">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
            <td align="left" colspan="4" style="height: 17px" valign="top">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                <table id="lblIngresar2" runat="server" border="0" cellpadding="0" cellspacing="0"
                    style="border-top-width: 1px; border-left-width: 1px; border-left-color: gray;
                    border-bottom-width: 1px; border-bottom-color: gray; width: 550px; border-top-color: gray;
                    border-right-width: 1px; border-right-color: gray" visible="false">
                    <tr style="font-size: 12pt; font-family: Times New Roman">
                        <td align="left" colspan="12" style="vertical-align: middle; height: 22px" valign="top">
                            <asp:Label ID="lblEtiqueta" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Maroon"></asp:Label></td>
                    </tr>
                    <tr style="font-size: 12pt; font-family: Times New Roman">
                        <td align="left" style="vertical-align: middle; width: 85px; height: 43px" valign="top">
                            <asp:Label ID="lbl3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nivel Cuenta"
                                Width="75px"></asp:Label></td>
                        <td align="left" colspan="11" style="vertical-align: middle; height: 43px" valign="top">
                            <asp:RadioButtonList ID="optNivelCta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                RepeatDirection="Horizontal" Width="226px">
                                <asp:ListItem Value="0">Principal</asp:ListItem>
                                <asp:ListItem Value="1">Sub-Cuenta</asp:ListItem>
                                <asp:ListItem Value="2">Registro</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
            <td align="left" colspan="4" style="height: 17px" valign="top">
                <table id="lblIngresar3" runat="server" border="0" cellpadding="0" cellspacing="0"
                    style="border-top-width: 1px; border-left-width: 1px; border-left-color: gray;
                    border-bottom-width: 1px; border-bottom-color: gray; width: 550px; border-top-color: gray;
                    border-right-width: 1px; border-right-color: gray" visible="false">
                    <tr style="font-size: 12pt; font-family: Times New Roman">
                        <td align="left" style="vertical-align: middle; width: 85px; height: 22px" valign="top">
                            &nbsp;<asp:Label ID="lbl41" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cta. Contable"
                                Width="76px" Visible="False"></asp:Label></td>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                            <asp:TextBox ID="txtCta" runat="server" Font-Names="Arial" Font-Size="8pt" Width="176px" Visible="False"></asp:TextBox>
                            <asp:Button ID="btnBusCuenta" runat="server" CssClass="EstiloBoton_Ac" onmouseout="this.style.fontWeight='normal'"
                                onmouseover="this.style.fontWeight='bolder'" Text="..." Width="20px" /></td>
                        <td align="left" style="vertical-align: middle; width: 40px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 25px; height: 22px" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                        </td>
                        <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                            <asp:Label ID="lbl40" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Nivel"
                                Width="45px" Visible="False"></asp:Label></td>
                        <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                            <asp:TextBox ID="txtNivel" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                Width="40px" Visible="False"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
            <td align="left" colspan="4" style="height: 17px" valign="top">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
<TABLE style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-LEFT-COLOR: gray; BORDER-BOTTOM-WIDTH: 1px; BORDER-BOTTOM-COLOR: gray; WIDTH: 550px; BORDER-TOP-COLOR: gray; BORDER-RIGHT-WIDTH: 1px; BORDER-RIGHT-COLOR: gray" id="lblIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Label id="lbl2" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial" Text="Cta. Nombre"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=11><asp:TextBox id="txtNombre" runat="server" Width="455px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:CheckBox id="chkCtaBanco" runat="server" Width="250px" Font-Size="8pt" Font-Names="Arial" Text="Cuenta relacionada con Nº de Cuenta de Banco" AutoPostBack="True"></asp:CheckBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left colSpan=1></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=11><asp:DropDownList id="cboCtaBanco" runat="server" Width="459px" Font-Size="8pt" Font-Names="Arial" Enabled="False">
                            </asp:DropDownList></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:CheckBox id="chkImpuesto" runat="server" Width="234px" Font-Size="8pt" Font-Names="Arial" Text="Cuenta relacionada con el Impuesto" AutoPostBack="True"></asp:CheckBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left colSpan=1></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 21px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 21px" vAlign=top align=left colSpan=11><asp:DropDownList id="cboImpuesto" runat="server" Width="459px" Font-Size="8pt" Font-Names="Arial" Enabled="False">
                            </asp:DropDownList></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Label id="lbl5" runat="server" Width="74px" Font-Size="8pt" Font-Names="Arial" Text="Tipo de Cuenta"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboTipoCta" runat="server" Width="206px" Font-Size="8pt" Font-Names="Arial">
                            </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="lbl13" runat="server" Width="63px" Font-Size="8pt" Font-Names="Arial" Text="Tipo Análisis"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=6><asp:DropDownList id="cboTipoAnalisis" runat="server" Width="179px" Font-Size="8pt" Font-Names="Arial">
                            </asp:DropDownList></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Label id="lbl14" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Balance General"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboBalanceGral" runat="server" Width="206px" Font-Size="8pt" Font-Names="Arial" Enabled="False">
                            </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="lbl6" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" Text="Tipo de Saldo"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=6><asp:RadioButtonList id="optTipoCta" runat="server" Width="131px" Font-Size="8pt" Font-Names="Arial" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Deudor</asp:ListItem>
                                <asp:ListItem Value="1">Acreedor</asp:ListItem>
                            </asp:RadioButtonList></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Label id="lbl8" runat="server" Width="76px" Font-Size="8pt" Font-Names="Arial" Text="Asiento Destino"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:RadioButtonList id="RadioButtonList2" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">S&#237;</asp:ListItem>
                            </asp:RadioButtonList></TD><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=2 rowSpan=1><asp:Label id="lbl7" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Centro Costo"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:RadioButtonList id="optCC" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">S&#237;</asp:ListItem>
                            </asp:RadioButtonList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Label id="lbl11" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" Text="Cta. Deudora"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="txtCtaDeudora" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> <asp:Button id="btnCtaDeudor" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="20px" Text="..." Enabled="False"></asp:Button></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="lbl9" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Presupuesto"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:RadioButtonList id="optPresup" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">S&#237;</asp:ListItem>
                            </asp:RadioButtonList></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Button id="btnGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="69px" Text="Guardar"></asp:Button></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 22px" vAlign=top align=left>&nbsp;<asp:Label id="lbl10" runat="server" Width="74px" Font-Size="8pt" Font-Names="Arial" Text="Cta. Acreedora"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="txtCtaAcreedora" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> <asp:Button id="btnCtaAcreedor" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="20px" Text="..." Enabled="False"></asp:Button></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="lbl12" runat="server" Width="62px" Font-Size="8pt" Font-Names="Arial" Text="Flujo de Caja"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:RadioButtonList id="optFCaja" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                <asp:ListItem Value="1">S&#237;</asp:ListItem>
                            </asp:RadioButtonList></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Button id="btnCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton_Ac" Width="69px" Text="Cancelar"></asp:Button></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; WIDTH: 85px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 25px; HEIGHT: 5px" vAlign=top align=left></TD></TR><TR style="FONT-SIZE: 12pt; FONT-FAMILY: Times New Roman"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 5px" vAlign=top align=left colSpan=12>&nbsp;<asp:Label id="lblAño" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCodigo" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCtaAnt" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta1" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta2" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta3" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label>&nbsp; <asp:Label id="lblCuenta4" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta5" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta6" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta7" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta8" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta9" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCuenta10" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblMascara" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCodCuentaD" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblCodCuentaA" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblNroNiveles" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> <asp:Label id="lblTieneHijos" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label></TD></TR></TBODY></TABLE>
</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
            <td align="left" colspan="4" style="height: 17px" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 17px" valign="top">
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td align="left" style="width: 25px; height: 6px" valign="top">
            </td>
            <td align="left" colspan="4" style="height: 6px" valign="top">
                <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#C00000"></asp:Label>&nbsp;
            </td>
            <td align="left" style="width: 25px; height: 6px" valign="top">
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 450px; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset; background-color: darkgray;">
            <tr>
                <td align="left" style="width: 30px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 25px; text-align: center"
                    valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"
                        Text="Plan Contable"></asp:Label></td>
                <td align="left" style="width: 30px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 30px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 25px; text-align: left"
                    valign="top">
                    <asp:Button ID="btnCerrarCta" runat="server" CssClass="EstiloBoton_Ac" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Cerrar" Width="70px" /></td>
                <td align="left" style="width: 30px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 30px" valign="top">
                </td>
                <td align="left" colspan="4" valign="top">
                    <div style="border-right: gray 1px inset; border-top: gray 1px inset; overflow: auto;
                        border-left: gray 1px inset; width: 400px; border-bottom: gray 1px inset; position: static;
                        height: 200px">
                        <asp:GridView ID="FlexBusCta" runat="server" AutoGenerateColumns="False"
                            Font-Names="Arial" Font-Size="8pt" PageSize="5" Width="828px">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Insertar" Text="Ins. Sgte. Nivel">
                                    <ControlStyle Width="114px" CssClass="EstiloBoton_Ac" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="114px" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Button" CommandName="Agregar" Text="Ag. mismo Nivel">
                                    <ControlStyle
                                        CssClass="EstiloBoton_Ac" Width="114px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="114px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="PLAN_CUENTA" HeaderText="Cuenta">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAN_NOMBRE_CUENTA" HeaderText="Nombre Cuenta">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAN_NIVEL_CUENTA" HeaderText="Nivel">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAN_CODIGO">
                                    <ItemStyle Width="0px" ForeColor="DarkGray" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAN_COD_NIVEL">
                                    <ItemStyle Width="0px" ForeColor="DarkGray" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CUENTA_DEUDORA" HeaderText="Cta. Deudora">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CUENTA_ACREEDORA" HeaderText="Cta. Acreedora">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField>
                                    <ItemStyle Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField>
                                    <ItemStyle Width="0px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 30px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 30px; height: 21px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 21px; text-align: left"
                    valign="top">
                    <asp:TextBox ID="txtNewCta" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px"></asp:TextBox>
                    <asp:Button ID="btnAceptar" runat="server" CssClass="EstiloBoton_Ac" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Aceptar Cuenta" Width="120px" /></td>
                <td align="left" style="width: 120px; height: 21px" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 21px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 30px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 30px; height: 25px" valign="top">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:ModalPopupExtender 
                        ID="ModalPopupExtender1" 
                        runat="server" 
                        TargetControlID="btnBusCuenta"
                        CancelControlID ="btnCerrarCta"
                        PopupControlID ="Panel1"
                        X="500"
                        Y="300" 
                        CacheDynamicResults="True" Enabled="True">
    </cc1:ModalPopupExtender>
</asp:Content>

