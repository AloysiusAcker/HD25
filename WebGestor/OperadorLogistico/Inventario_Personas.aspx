<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_Oplogistico.master" AutoEventWireup="false" CodeFile="Inventario_Personas.aspx.vb" Inherits="Inventario_Personas" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                    <td align="left" colspan="4" style="height: 51px; text-align: center" valign="top">
                        <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                            font-size: 14pt; vertical-align: middle; width: 550px; color: gray; font-style: italic;
                            font-family: 'Bell MT', Broadway, Arial, Serif; position: static; height: 1px;
                            text-align: center">
                            Personas</div>
                    </td>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                        </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px;" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 22px;" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 22px;" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 22px;" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; text-align: right; height: 22px;"
                        valign="top">
                        <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="80px" /></td>
                    <td align="left" style="width: 25px; height: 22px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="3" style="height: 19px" valign="top">
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
                    <td align="left" style="vertical-align: middle; width: 150px; height: 19px; text-align: right"
                        valign="top">
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="4" style="height: 19px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div id="DIV1" runat="server" style="border-right: dimgray 1px inset; border-top: dimgray 1px inset;
                                    overflow: auto; border-left: dimgray 1px inset; width: 550px; border-bottom: dimgray 1px inset;
                                    position: static; height: 300px">
                                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                        Font-Size="8pt" Width="880px" OnSelectedIndexChanged="Flex_SelectedIndexChanged">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" Text="Ing. Fecha" CommandName="Editar">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="60px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="PER_CODIGO" HeaderText="Cod. Persona">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PER_DNI" HeaderText="Nro. Doc.">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PER_NOMBRECOMPLETO" HeaderText="Raz&#243;n Social &#243; Apellidos y Nombres">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="180px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PER_TELEFONO1" HeaderText="Tel&#233;fono">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PER_DIRECCION" HeaderText="Direcci&#243;n">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PER_PEDIDO" HeaderText="Nro Pedido">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha Entrega">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="4" style="height: 19px; vertical-align: middle;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                    <td align="left" colspan="4" style="height: 19px" valign="top">
                        <div style="text-align: left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
<TABLE style="WIDTH: 550px" id="lblIngresarFecha" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"></asp:Label></TD><TD style="WIDTH: 75px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left colSpan=2><asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> <asp:Button id="btnGuardar" onclick="btnGuardar_Click" runat="server" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Guardar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Cod. Persona"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 155px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtCodPer" runat="server" Width="145px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo Doc."></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 75px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTipoDoc" runat="server" Width="67px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt5" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Nro. Doc."></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtNroDoc" runat="server" Width="92px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Razón Social"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=3><asp:TextBox id="txtRazonSocial" runat="server" Width="302px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt6" runat="server" Width="54px" Font-Size="8pt" Font-Names="Arial" Text="Nro Pedido"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtNroPedido" runat="server" Width="92px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Dirección"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left colSpan=3><asp:TextBox id="txtDireccion" runat="server" Width="302px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEt7" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Teléfono"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtTelef" runat="server" Width="92px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Fecha Entrega"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 155px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:TextBox id="txtFecha" runat="server" Width="145px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left><asp:Button id="btnBus" runat="server" Width="19px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="..." BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 75px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=top align=left></TD></TR></TBODY></TABLE><cc1:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtFecha" PopupButtonID="btnBus" Format="dd/MM/yyyy"></cc1:CalendarExtender> 
</ContentTemplate>
                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                            </asp:UpdatePanel>
                            &nbsp;</div>
                    </td>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 100px" valign="top">
                    </td>
                    <td align="left" style="width: 150px" valign="top">
                    </td>
                    <td align="left" style="width: 150px" valign="top">
                    </td>
                    <td align="left" style="width: 150px" valign="top">
                    </td>
                    <td align="left" style="width: 25px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

