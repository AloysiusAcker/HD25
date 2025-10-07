<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_Define_Personas.aspx.vb" Inherits="AdminProblemas_Define_Personas" title="Mesa de Ayuda - Personas Registro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="11" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 295px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Mantenimiento de Personas</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="13" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 21px;" valign="top">
                </td>
                <td align="left" colspan="11" style="vertical-align: middle; height: 21px;"
                    valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<asp:Label id="lblError" runat="server" Width="545px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="cmdListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 21px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 21px" valign="top">
                </td>
                <td align="left" colspan="11" style="vertical-align: middle; height: 21px" valign="top">
                    <asp:Button ID="btnNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Nuevo" Width="60px" />
                    <asp:Button ID="cmdListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Listar" Width="60px" /></td>
                <td align="left" style="width: 25px; height: 21px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 292px;" valign="top">
                </td>
                <td align="left" colspan="11" style="vertical-align: middle; height: 292px;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 256px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1100px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="40" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="APERSONA_CODIGO" HeaderText="Cod.">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_USUARIO" HeaderText="Usuario">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_APELLIDOS" HeaderText="Apellidos">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_NOMBRE" HeaderText="Nombre">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Oficinas" HeaderText="Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Territorio" HeaderText="Territorio">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Puesto" HeaderText="Puesto">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_TELEFONO" HeaderText="Telefono">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_ANEXO" HeaderText="Anexo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_CORREO_ELECTRONICO" HeaderText="Correo Electr&#243;nico">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AOFICINA_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ATERRI_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APUESTO_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_ANTIGUEDAD">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APERSONA_BANCA">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmdListar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 292px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" colspan="11" style="vertical-align: middle; height: 11px" valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="11" style="vertical-align: middle; height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
<TABLE style="WIDTH: 550px" id="lblIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=10><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl3" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Usuario</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtUsuario" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl7" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Nombres</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:TextBox id="txtNombres" runat="server" Width="210px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl8" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Apellidos</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:TextBox id="txtApellidos" runat="server" Width="210px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 24px" vAlign=top align=left><asp:Label id="lbl4" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Oficina</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px" vAlign=top align=left colSpan=4><asp:DropDownList id="cboOficina" runat="server" Width="216px" Font-Size="8pt" Font-Names="Arial">
                                        </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 24px" vAlign=top align=left><asp:Label id="lbl10" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Teléfono</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px" vAlign=top align=left colSpan=2><asp:TextBox id="txtTelefono" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 24px" vAlign=top align=left colSpan=2><asp:Label id="lbl13" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Anexo</asp:Label> <asp:TextBox id="txtAnexo" runat="server" Width="52px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl5" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Territorio</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:DropDownList id="cboTerrotorio" runat="server" Width="216px" Font-Size="8pt" Font-Names="Arial">
                                        </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl12" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial">Cod. Banca</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtBanca" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl6" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">Puesto</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:DropDownList id="cboPuesto" runat="server" Width="216px" Font-Size="8pt" Font-Names="Arial">
                                        </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl11" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial">Antiguedad</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtAntiguedad" runat="server" Width="110px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Button id="btnGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGuardar_Click" runat="server" Width="96px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Guardar" EnableTheming="True" CssClass="EstiloBoton" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl9" runat="server" Width="34px" Font-Size="8pt" Font-Names="Arial">E-Mail</asp:Label></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7><asp:TextBox id="txtCorreo" runat="server" Width="390px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Button id="btnCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCancelar_Click" runat="server" Width="96px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" EnableTheming="True" CssClass="EstiloBoton" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCodigo" runat="server" Width="39px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 40px; height: 11px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 61px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="11" style="vertical-align: middle; height: 19px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress" Width="200px">
            <asp:UpdateProgress id="UpdateProg1" runat="server" DisplayAfter="0">
                <progresstemplate>
<DIV style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">&nbsp;<IMG src="../Fotos/5.gif" /></DIV>
</progresstemplate>
            </asp:UpdateProgress>
        </asp:Panel>
        <cc1:modalpopupextender id="ModalProgress" runat="server" backgroundcssclass="modalBackground"
            popupcontrolid="panelUpdateProgress" targetcontrolid="panelUpdateProgress">
    </cc1:modalpopupextender>
        &nbsp;</div>
</asp:Content>

