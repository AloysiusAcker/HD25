<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Mantenimiento_TablasEspeciales.aspx.vb" Inherits="Mantenimiento_TablasEspeciales" title="Sistema - Tablas Externas e Internas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td align="left" colspan="10" style="height: 50px; text-align: center" valign="middle">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 14pt; left: 253px; vertical-align: middle; width: 600px; color: gray;
                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                    text-align: center">
                    Tablas Especiales</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="10" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="10" style="height: 11px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="8" style="height: 22px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="0"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
Tablas Especiales&nbsp; 
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 448px; HEIGHT: 22px" vAlign=middle align=left colSpan=9><asp:RadioButtonList id="opcTablas" runat="server" Width="432px" Font-Size="8pt" Font-Names="Arial" OnSelectedIndexChanged="opcTablas_SelectedIndexChanged" AutoPostBack="True" RepeatDirection="Horizontal"><asp:ListItem Selected="True">Tabla de Opciones Externas</asp:ListItem>
<asp:ListItem>Tabla de Opciones Internas</asp:ListItem>
</asp:RadioButtonList> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left><asp:Button id="btnNuevaTabla" onclick="btnNuevaTabla_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Nuevo" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR><TR><TD style="WIDTH: 448px; HEIGHT: 22px" vAlign=middle align=left colSpan=9><asp:Label id="lblRegistroTabla" runat="server" Width="200px" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="HEIGHT: 350px" vAlign=middle align=left colSpan=10><asp:UpdatePanel id="UpdatePanel2" runat="server"><ContentTemplate>
<DIV style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; OVERFLOW: auto; BORDER-LEFT: 1px outset; WIDTH: 528px; BORDER-BOTTOM: 1px outset; POSITION: static; HEIGHT: 350px" id="div1" runat="server"><asp:GridView id="FlexTablasExternas" runat="server" Width="680px" Font-Size="8pt" Font-Names="Arial" OnRowCommand="FlexTablasExternas_RowCommand" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Mant" Text="Mantenimiento" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="90px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Borrar" Text="Borrado Logico" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="90px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="TABLAS_CODIGO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TABLAS_DESCRIPCION" HeaderText="Nombre / Descripcion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TABLAS_VER" HeaderText="Activo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView>&nbsp;</DIV>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="opcTablas" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD></TR><TR><TD style="HEIGHT: 5px" vAlign=middle align=left colSpan=10></TD></TR><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=10><DIV style="WIDTH: 530px; POSITION: static; HEIGHT: 100px" id="lblNuevaTabla" runat="server" Visible="False"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=3><asp:Label id="Label19" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Datos por Completar" ForeColor="Maroon"></asp:Label> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtEditar" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtCodTablaBorrarLog" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label20" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:TextBox id="txtCodTabla" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left>&nbsp;</TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label21" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=9><asp:TextBox id="txtDescTabla" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left><asp:Button id="Button4" onclick="Button4_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left><asp:Button id="btnGrabaNuevaTabla" onclick="btnGrabaNuevaTabla_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Grabar" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
                                    Mantenimiento Tablas 
                                
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:TextBox id="txtCodMant" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 40px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left><asp:Button id="btnNuevoElemento" onclick="btnNuevoElemento_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Nuevo" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left><asp:Button id="btnRegresarTablas" onclick="btnRegresarTablas_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Regresar" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR><TR><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=9><asp:TextBox id="txtDescMant" runat="server" Width="475px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=5><asp:Label id="Label9" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Lista de sus Elementos" ForeColor="Maroon"></asp:Label> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=10><asp:Label id="LblRegistroElemento" runat="server" Width="496px" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label> </TD></TR><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=10><DIV style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; OVERFLOW: auto; BORDER-LEFT: 1px outset; WIDTH: 528px; BORDER-BOTTOM: 1px outset; POSITION: static; HEIGHT: 300px" id="DIV3" runat="server"><asp:GridView id="FlexEmentosTablas" runat="server" Width="1040px" Font-Size="8pt" Font-Names="Arial" OnRowCommand="FlexEmentosTablas_RowCommand" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Borrar" Text="Borrado Logico" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="90px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="BorradoFisico" Text="Borrado Fisico" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="90px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="ELEMEN_CODIGO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Descripcion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACTIVO" HeaderText="Activo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ELEMEN_CODIGO_MINIS" HeaderText="Valor 1">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ELEMEN_VALOR_MINIS" HeaderText="Valor 2">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR><TD style="HEIGHT: 5px" vAlign=middle align=left colSpan=10></TD></TR><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=10><DIV style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-BOTTOM-WIDTH: 1px; WIDTH: 528px; POSITION: static; HEIGHT: 200px; BORDER-RIGHT-WIDTH: 1px" id="lblEditarElementos" runat="server" Visible="False"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblEtElemento" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Ingresar Elemneto" ForeColor="Maroon"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Datos Por Completar" ForeColor="Maroon"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCodElem" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 230px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtEditarElem" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="txtCodElemBorrar" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCodigoElemen" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:TextBox id="txtCodigoElem" runat="server" Width="1px" Visible="False"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label4" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:TextBox id="txtDesElem" runat="server" Width="454px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Campos Adicionales" ForeColor="Maroon"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Valor 1"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtVal1Elem" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 230px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Valor 2"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:TextBox id="txtVal2Elem" runat="server" Width="454px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 230px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnCancelarElem" onclick="btnCancelarElem_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnGuardarElem" onclick="btnGuardarElem_Click" runat="server" Width="78px" Font-Size="8pt" Font-Names="Arial" Text="Grabar" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR></TBODY></TABLE></DIV><DIV style="TEXT-ALIGN: left">&nbsp;</DIV></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="8" style="height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 100px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 90px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="8" style="height: 22px" valign="middle">
                &nbsp;</td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
    </table>
</asp:Content>

