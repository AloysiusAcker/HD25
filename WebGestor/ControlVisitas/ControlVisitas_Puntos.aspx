<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="ControlVisitas_Puntos.aspx.vb" Inherits="ControlVisitas_ControlVisitas_Puntos" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
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
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 50px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 5px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" ActiveTabIndex="1" AutoPostBack="True"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
Puntos de Control
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblPCError" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w653" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq1" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w654" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w655" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPCListar" onclick="btnPCListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w656" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq2" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w657" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w658" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPCNuevo" onclick="btnPCNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w659" Text="Nuevo"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexPC" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w660" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c0" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="Pto. Control">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Agencia">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Piso">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Ubicaci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c7">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblPCIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtq9" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w661" ForeColor="Maroon" Text="Ingresar Pto. de Control"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq10" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w662" Text="Agencia"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboPCAgencia" runat="server" Width="470px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w663"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq11" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w664" Text="Piso"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtPCPiso" runat="server" Width="464px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w665"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq12" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w666" Text="Ubicación"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtPCUbicacion" runat="server" Width="464px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w667"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq13" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w668" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtPCDescripcion" runat="server" Width="464px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w669"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtPCCodigo" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w670" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnPCGuardar" onclick="btnPCGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w671" Text="Guardar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnPCCancelar" onclick="btnPCCancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w672" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
Personal que Labora
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 60px" vAlign=top align=left></TD><TD style="WIDTH: 200px" vAlign=top align=left></TD><TD style="WIDTH: 200px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblPLError" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w842" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq5" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w843" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboPLGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w844"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPLListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w845" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq6" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w846" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboPLEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w847"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPLAsignar" onclick="btnPLAsignar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w848" Text="Asignar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexPL" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w849" OnSelectedIndexChanged="FlexPL_SelectedIndexChanged" Font-Overline="False" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c1" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Apellidos y Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Pto. Control">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Descripci&#243;n del Pto. de Control">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblPLIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtq17" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w855" ForeColor="Maroon" Text="Personal que labora en un punto de control"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq18" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w850" Text="Pto. Control"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboPLPtoControl" runat="server" Width="472px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w854"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq19" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w851" Text="Personal"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4 runat="server"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px" id="DIV1"><asp:GridView id="FlexPLPersonal" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w856" AutoGenerateColumns="False"><Columns>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="chkPer" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w858"></asp:CheckBox> 
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c2" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Apellidos y Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="500px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnPLGuardar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w852" Text="Guardar" OnClick="btnPLGuardar_Click"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnPLCancelar" onclick="btnPLCancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w853" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
Personal que Controla
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 60px" vAlign=top align=left></TD><TD style="WIDTH: 400px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblPeCError" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w822" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq7" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w823" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboPeCGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w824" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPeCListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w825" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq8" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w826" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboPeCEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w827" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPeCAsignar" onclick="btnPeCAsignar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w828" Text="Asignar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexPeC" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w829" AutoGenerateColumns="False" Font-Overline="False"><Columns>
<asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c1" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Apellidos y Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="450px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblPeCIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 376px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq16" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w830" ForeColor="Maroon" Text="Personal que Controla Puntos de Control"></asp:Label> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 38px" vAlign=top align=left colSpan=3 runat="server"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexPCPersonal" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w831" AutoGenerateColumns="False" Font-Overline="False"><Columns>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="chkPer" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w821"></asp:CheckBox> 
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c2" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Apellidos y Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="450px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="WIDTH: 376px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnPeCGuardar" onclick="btnPeCGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w832" Text="Guardar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnPeCCancelar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w833" Text="Cancelar" OnClick="btnPeCCancelar_Click"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
Agencias
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblAError" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w785" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq3" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w786" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboAGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w787" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnAListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w788" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq4" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w789" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboAEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w790" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnANuevo" onclick="btnANuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w791" Text="Nuevo"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexA" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w792" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c0" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Nombre">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblAIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtq14" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" __designer:wfdid="w793" ForeColor="Maroon" Text="Nueva Agencia"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq15" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w794" Text="Nombre"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtANombre" runat="server" Width="464px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w795"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtACodigo" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w796" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnAGuardar" onclick="btnAGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w797" Text="Guardar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnACancelar" onclick="btnACancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" __designer:wfdid="w798" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

