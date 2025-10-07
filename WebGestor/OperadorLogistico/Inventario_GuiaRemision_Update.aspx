<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_Oplogistico.master" AutoEventWireup="false" CodeFile="Inventario_GuiaRemision_Update.aspx.vb" Inherits="Inventario_GuiaRemision_Update" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 51px; text-align: center" valign="top">
                        <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                            font-size: 14pt; vertical-align: middle; width: 550px; color: gray; font-style: italic;
                            font-family: 'Bell MT', Broadway, Arial, Serif; position: static; height: 1px;
                            text-align: center">
                            Guía de Remisión x Courier</div>
                    </td>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                        </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 115px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Entrega" Width="70px"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                                                <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" Width="78px"></asp:TextBox></td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="78px"></asp:TextBox></td>
                    <td align="left" style="width: 150px; height: 22px" valign="top">
                        &nbsp;</td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="80px" /></td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="lblEt11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Courier"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCurrierRuc" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ReadOnly="True" Width="77px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="FlexCurrier" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Button ID="btnBusC" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="..." Width="23px" /></td>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
<asp:TextBox id="txtCurrierRS" runat="server" Width="350px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="FlexCurrier" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 22px; vertical-align: middle;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRegistro" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Maroon"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 22px; text-align: right"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
<asp:TextBox id="lblCodCurrier" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> 
</ContentTemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="FlexCurrier" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 19px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: dimgray 1px inset; BORDER-TOP: dimgray 1px inset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px inset; WIDTH: 548px; BORDER-BOTTOM: dimgray 1px inset; POSITION: static; HEIGHT: 300px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1340px" Font-Size="8pt" Font-Names="Arial" UseAccessibleHeader="False" OnSelectedIndexChanged="Flex_SelectedIndexChanged" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="IngEstado" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="70px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="ESTADO_ENTREGA" HeaderText="Est. Entrega">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_SERIE" HeaderText="Serie">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_NUMERO" HeaderText="Nro. Gu&#237;a">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_ENTREGA" HeaderText="Fecha Entrega">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_NRO" HeaderText="Nro. Pedido">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_NROLIQUIDACION" HeaderText="Nro. Liquidaci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESTINO_CODIGO" HeaderText="Cod. Destinatario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Destinatario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_DIRECCION_LLEGADA" HeaderText="Direcci&#243;n Destinatario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CURRIERRUC" HeaderText="RUC Courier">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CURRIERRAZON_SOCIAL" HeaderText="Raz&#243;n Social Courier">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_PERASIGNADO" HeaderText="Persona Asignada">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_ESTADO_ENTREGA">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PEDIDO_ESTADO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_CURRIER">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Italic="False" Font-Names="Arial"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 19px; vertical-align: middle;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
<asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
</ContentTemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                    <td align="left" colspan="6" valign="top">
                        <div style="text-align: left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
<TABLE style="WIDTH: 550px" id="lblIngresarFecha" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></asp:Label></TD><TD style="WIDTH: 75px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left colSpan=2><asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> <asp:Button id="btnGuardar" onclick="btnGuardar_Click" runat="server" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Guardar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Cod. Guía"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 155px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtCodGuia" runat="server" Width="145px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Serie Guía"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 75px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtSerieGuia" runat="server" Width="67px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt5" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Nro. Guía"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtNroGuia" runat="server" Width="92px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Destinatario"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=3><asp:TextBox id="txtDestinatario" runat="server" Width="302px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt7" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtCodDestino" runat="server" Width="92px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt8" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Estado Entrega"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 155px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtEstado" runat="server" Width="145px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt10" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Cambiar Estado"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=3><asp:DropDownList id="cboEstado" runat="server" Width="234px" Font-Size="8pt" Font-Names="Arial" OnSelectedIndexChanged="cboEstado_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; TEXT-ALIGN: left" vAlign=top align=left colSpan=6><DIV style="TEXT-ALIGN: left"><DIV style="TEXT-ALIGN: left"><asp:UpdatePanel id="UpdatePanel12" runat="server"><ContentTemplate>
<TABLE style="WIDTH: 550px" id="lblEstEnProceso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 17px" vAlign=top align=left><asp:Label id="lblEt12" runat="server" Width="69px" Font-Size="8pt" Font-Names="Arial" Text="Persona Asig." __designer:wfdid="w1"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 330px; HEIGHT: 17px" vAlign=top align=left><asp:TextBox id="txtPerAsignada" runat="server" Width="319px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w2"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 17px" vAlign=top align=left><asp:Label id="lblEt13" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w3">Hora</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 17px" vAlign=top align=left><asp:TextBox id="txtHora" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w4" MaxLength="5"></asp:TextBox></TD></TR></TBODY></TABLE><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 550px" id="lblEstEntrega" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px" vAlign=top align=left><asp:Label id="lblEt14" runat="server" Width="71px" Font-Size="8pt" Font-Names="Arial" Text="Fecha Entrega" __designer:wfdid="w5"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 155px; HEIGHT: 20px" vAlign=top align=left><asp:TextBox id="txtFecReprog1" runat="server" Width="145px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w6"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px" vAlign=top align=left><asp:Label id="lblEt15" runat="server" Width="77px" Font-Size="8pt" Font-Names="Arial" Text="N° PAI ó Liquidación" __designer:wfdid="w7"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 235px; HEIGHT: 20px; TEXT-ALIGN: left" vAlign=top align=left><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 235px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCodLiquida" runat="server" Width="68px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w8" MaxLength="10"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtqPai" runat="server" Width="44px" Font-Size="8pt" Font-Names="Arial" Text="Llamó al" __designer:wfdid="w9"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 105px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboPai101" runat="server" Width="105px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w10"></asp:DropDownList></TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left><asp:Label id="lblEt16" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Hora Entrega" __designer:wfdid="w11"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=3><asp:TextBox id="txtHoraReprog" runat="server" Width="464px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w12" MaxLength="250" TextMode="MultiLine"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left><asp:Label id="lblEt17" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo Obs." __designer:wfdid="w13"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboTipoObs" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w14"></asp:DropDownList></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 69px" vAlign=top align=left><asp:Label id="lblEt18" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Observación" __designer:wfdid="w15"></asp:Label></TD><TD style="HEIGHT: 69px" vAlign=top align=left colSpan=3><asp:TextBox id="txtObs" runat="server" Width="464px" Height="61px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w16" MaxLength="250" TextMode="MultiLine" ToolTip="Máximo 250 caracteres"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 155px; HEIGHT: 19px" vAlign=top align=left><asp:TextBox id="txtCodPedido" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w17"></asp:TextBox></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left><asp:TextBox id="txtCodEstado" runat="server" Width="37px" Visible="False" __designer:wfdid="w18"></asp:TextBox></TD><TD style="WIDTH: 235px; HEIGHT: 19px" vAlign=top align=left><asp:TextBox id="txtCodCurrier" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w1"></asp:TextBox></TD></TR></TBODY></TABLE></DIV><cc1:CalendarExtender id="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecReprog1" PopupButtonID="txtFecReprog1" __designer:wfdid="w19"></cc1:CalendarExtender> 
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="cboEstado" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel>&nbsp;</DIV></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=5></TD></TR></TBODY></TABLE>
</ContentTemplate>
                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                            </asp:UpdatePanel>
                            &nbsp;</div>
                    </td>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </div>
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFecha"
                            TargetControlID="txtFecha" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFechaFin"
        TargetControlID="txtFechaFin">
    </cc1:CalendarExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnCCerrar" PopupControlID="Panel1" TargetControlID="btnBusC" CacheDynamicResults="True">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server">
        <div style="text-align: left">
            <table id="TABLE1" runat="server" border="0" cellpadding="0" cellspacing="0" style="border-right: black 1px outset;
                border-top: black 1px outset; border-left: black 1px outset; width: 450px; border-bottom: black 1px outset;
                background-color: darkgray">
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: center"
                        valign="top">
                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Busqueda de Courier"></asp:Label></td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="RUC"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 160px; height: 22px; text-align: left"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCRuc" runat="server" Font-Names="Arial" Font-Size="8pt" Width="101px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCCerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="FlexCurrier" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 160px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnCCerrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Cerrar" Width="70px" />
                        <asp:Button ID="btnCListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="70px" /></td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Razón Social"
                            Width="70px"></asp:Label></td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtCRazonSocial" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="306px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCCerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="FlexCurrier" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRegistro1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Maroon"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCCerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="FlexCurrier" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="height: 22px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div id="DIV2" runat="server" style="border-right: gray 1px outset; border-top: gray 1px outset;
                                    overflow: auto; border-left: gray 1px outset; width: 390px; border-bottom: gray 1px outset;
                                    position: static; height: 200px">
                                    <asp:GridView ID="FlexCurrier" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                        Font-Size="8pt">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="Aceptar">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Gray" Width="60px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="VCODIGO">
                                                <ItemStyle Width="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VCODIGOEXTERNO" HeaderText="RUC">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VDESCRIPCION" HeaderText="Raz&#243;n Social">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 19px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>

