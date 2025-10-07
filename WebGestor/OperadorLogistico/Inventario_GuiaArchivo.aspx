<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_Oplogistico.master" AutoEventWireup="false" CodeFile="Inventario_GuiaArchivo.aspx.vb" Inherits="Inventario_GuiaArchivo" title="Untitled Page" %>

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
                            Archivo de
                            Guía de Remisión</div>
                    </td>
                    <td align="left" style="width: 58px; height: 51px" valign="top">
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
                    <td align="left" style="width: 58px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                                                </td>
                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                        valign="top">
                                                </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; text-align: left"
                        valign="top">
                                                </td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: left"
                        valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 22px" valign="top">
                        &nbsp;</td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="80px" /></td>
                    <td align="left" style="width: 58px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 19px; vertical-align: middle; text-align: left;" valign="top">
                        <asp:Label ID="lblRegistro" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Maroon"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 19px; text-align: right"
                        valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 19px" valign="top">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<DIV style="BORDER-RIGHT: dimgray 1px inset; BORDER-TOP: dimgray 1px inset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px inset; WIDTH: 550px; BORDER-BOTTOM: dimgray 1px inset; POSITION: static; HEIGHT: 300px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1700px" Font-Size="8pt" Font-Names="Arial" UseAccessibleHeader="False" OnSelectedIndexChanged="Flex_SelectedIndexChanged" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="GUIREM_CODIGO" HeaderText="Cod. Gu&#237;a">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_SERIE" HeaderText="Serie">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_NUMERO" HeaderText="Nro. Gu&#237;a">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Nombre Archivo"><ItemTemplate>
                                                    <div id="Doc" runat="server" style="width: 150px; height: 22px">
                                                    </div>
                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="ESTADO_ENTREGA" HeaderText="Est. Entrega">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="True" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ESTADO_SITUACION" HeaderText="Est. Situacion">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_ENTREGA" HeaderText="Fecha Entrega">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="REMITENTE_CODIGO" HeaderText="Cod. Remitente">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="REMITENTE_NOMBRE" HeaderText="Remitente">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESTINO_CODIGO" HeaderText="Cod. Destinatario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Destinatario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CURRIERRUC" HeaderText="RUC Courier">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CURRIERRAZON_SOCIAL" HeaderText="Raz&#243;n Social Courier">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_DIRECCION_LLEGADA" HeaderText="Direcci&#243;n Destinatario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREM_ESTADO_ENTREGA">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GUIREMARCH_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Font-Italic="False" Font-Names="Arial"></HeaderStyle>
</asp:GridView> </DIV>
</contenttemplate>
                            <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 58px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 19px; vertical-align: middle;" valign="top">
                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                    <td align="left" style="width: 58px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                    <td align="left" valign="top" colspan="2" style="vertical-align: middle; text-align: left; height: 19px;">
                        </td>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 115px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 19px;" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    &nbsp;
</asp:Content>

