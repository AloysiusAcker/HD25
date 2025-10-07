<%@ Page Language="VB" MasterPageFile="~/MenuWeb/PagPrincipal_MenuWeb.master" AutoEventWireup="false" CodeFile="MenuWeb_Definicion.aspx.vb" Inherits="MenuWeb_MenuWeb_Definicion" title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 48px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 48px; text-align: center;" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Definiciones</div>
                </td>
                <td align="left" style="width: 25px; height: 48px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4" style="background-image: url(../Fotos/Linea_Gris.bmp);
                    height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" style="height: 5px" valign="top">
                </td>
                <td align="left" style="height: 5px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 5px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="2" AutoPostBack="True"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                Párrafos de la Página de Inicio
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblError" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w131" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label6" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w132" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboGrupoP" runat="server" Width="396px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w133"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" onclick="btnListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w134" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label7" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w135" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboEmpresaP" runat="server" Width="396px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w136"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnNuevo" onclick="btnNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w137" Text="Nuevo"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 536px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 168px"><asp:GridView id="Flex" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w138" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="PARRAFO_CODIGO" HeaderText="Nro.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PARRAFO_TITULO" HeaderText="T&#237;tulo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PARRAFO_DESCRIP" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="codgrupo">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="codempresa">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 21px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblIngresoParrafo" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 306px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblIngreso" runat="server" Width="88px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w139" ForeColor="Maroon"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: top; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label8" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w140" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboGrupoPIng" runat="server" Width="462px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w141"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: top; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label9" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w142" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboEmpresaPIng" runat="server" Width="462px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w143"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: top; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label2" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w144" Text="Título"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtTitulo" runat="server" Width="456px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w145" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: top; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label3" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w146" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtDescripcion" runat="server" Width="456px" Height="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w147" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: top; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: top; WIDTH: 306px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodigo" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w148" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnGuardar" onclick="btnGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w149" Text="Guardar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w150" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                Items del Menú
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 80px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD><TD style="WIDTH: 380px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblErrorItem" runat="server" Width="528px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w65" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarItem" onclick="btnListarItem_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w66" Text="Listar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnNuevoItem" onclick="btnNuevoItem_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w67" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 380px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 536px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px"><asp:GridView id="FlexItem" runat="server" Width="770px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w68" AutoGenerateColumns="False" Font-Overline="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="70px"></ControlStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="ITEM_NOMBRE" HeaderText="Nombre">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Codigo" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_PAGINA" HeaderText="P&#225;gna">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ETIQUETA" HeaderText="Campo Etiqueta">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CAMPO_NOMBRE" HeaderText="Campo Nombre">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField>
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 11px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 11px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 380px; HEIGHT: 11px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblItemIngreso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="WIDTH: 96px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 280px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblIngresoItem" runat="server" Font-Italic="False" Width="144px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w69" ForeColor="Maroon" Text="Nuevo Items del Menú"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left colSpan=1 runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left colSpan=1 runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 96px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label1" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w70" Text="Nombre del Item"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtNombreItem" runat="server" Width="432px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w71"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 96px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label4" runat="server" Width="90px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w72" Text="Nombre del Página"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtPaginaItem" runat="server" Width="432px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w73"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 30px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="Label5" runat="server" Width="256px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w74" ForeColor="Maroon" Text="Etiquetas y campos que conforman el Items"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 30px" vAlign=top align=left colSpan=1 runat="server"><asp:TextBox id="txtCodigoItem" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w75" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 30px" vAlign=top align=left colSpan=1 runat="server"></TD></TR><TR runat="server"><TD style="HEIGHT: 22px" vAlign=top align=left colSpan=4 runat="server"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 528px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 176px"><asp:GridView id="FlexItemCampo" runat="server" Width="1020px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w76" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="c0" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Marcar"><ItemTemplate>
                                                                                    <asp:CheckBox ID="chkM" runat="server" Enabled="False" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="25px" />
                                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
</asp:TemplateField>
<asp:ButtonField CommandName="MSi" Text="Si">
<HeaderStyle Width="20px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="MNo" Text="No">
<ControlStyle Width="20px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c1" HeaderText="Referencia">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="170px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Tipo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Etiqueta">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField><ItemTemplate>
                                                                                    <asp:TextBox ID="txtI" runat="server" Enabled="False" Font-Names="Arial" Font-Size="8pt"
                                                                                        Width="190px"></asp:TextBox>
                                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c4" HeaderText="Obligatorio">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:ButtonField CommandName="Si" Text="Si">
<ControlStyle Width="20px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="No" Text="No" CausesValidation="True">
<ControlStyle Width="20px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c5" HeaderText="Nombre del Campo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="120px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="WIDTH: 96px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 280px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnCancelarItem" onclick="btnCancelarItem_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w77" Text="Cancelar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnGuardarItem" onclick="btnGuardarItem_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w78" Text="Guardar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 380px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
                                Categoría
                            
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" id="TABLE1" cellSpacing=0 cellPadding=0 border=0 runat="server"><TBODY><TR runat="server"><TD style="WIDTH: 60px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 130px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 120px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 150px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5 runat="server"><asp:Label id="lblErrorCat" runat="server" Width="504px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w33" ForeColor="Red"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblCat1" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w34" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboGrupo" runat="server" Width="396px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w35"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnListarCat" onclick="btnListarCat_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w36" Text="Listar"></asp:Button> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblCat2" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w37" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboEmpresa" runat="server" Width="396px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w38"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnNuevoCat" onclick="btnNuevoCat_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w39" Text="Nuevo"></asp:Button> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5 runat="server"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 536px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 200px"><asp:GridView id="FlexCat" runat="server" Width="540px" Height="144px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w40" AutoGenerateColumns="False" Font-Overline="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="60px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="COD_ITEM" HeaderText="Cod. Item">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_NOMBRE" HeaderText="Nombre Item">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="COD_CATEGORIA" HeaderText="Cod. Categoria">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CATEG_NOMBRE" HeaderText="Nombre Categoria">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5 runat="server"><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblCategoria" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="WIDTH: 60px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 316px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblCatEtiqueta" runat="server" Width="112px" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w41" ForeColor="Maroon" Text="Ingresar Categoría"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblCat3" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w42" Text="Item"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboCatItem" runat="server" Width="474px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w43"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblCat4" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w44" Text="Categoría"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtCatNombre" runat="server" Width="467px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w45"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCatCodigo" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w46" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnCancelarCat" onclick="btnCancelarCat_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w47" Text="Cancelar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnGuardarCat" onclick="btnGuardarCat_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w48" Text="Guardar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR></TBODY></TABLE></DIV></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
Items del Menu&nbsp;a Utilizar 
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblErrorUtil" runat="server" Width="536px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w9" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label10" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w10" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboGrupoUtil" runat="server" Width="398px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w11"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarUtil" onclick="btnListarUtil_Click" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w12" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label11" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w13" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboEmpresaUtil" runat="server" Width="398px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w14"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnGuardarUtil" runat="server" CssClass="EstiloBoton_Ac" Width="76px" __designer:wfdid="w15" Text="Guardar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 534px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 320px"><asp:GridView id="FlexUtil" runat="server" Width="630px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w16" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="chkUsar" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w50"></asp:CheckBox>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_CODIGO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ITEM_NOMBRE" HeaderText="Item">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="500px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="TEXT-ALIGN: left">&nbsp;</DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

