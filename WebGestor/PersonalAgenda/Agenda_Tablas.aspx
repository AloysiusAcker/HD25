<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Agenda_Tablas.aspx.vb" Inherits="Agenda_Tablas" title="Agenda - Definiciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" lang="javascript">
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
<cc1:TabContainer id="Ficha" runat="server" Width="550px" AutoPostBack="True" ActiveTabIndex="0"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
Personal - Horario de Atención&nbsp; 
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblPCError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w39"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Grupo" __designer:wfdid="w40"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w41"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPCListar" onclick="btnPCListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Listar" __designer:wfdid="w42"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Empresa" __designer:wfdid="w43"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w44"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPCNuevo" onclick="btnPCNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Agregar" __designer:wfdid="w45"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexPC" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w46"><Columns>
<asp:BoundField DataField="c0" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="Area">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Apellidos y Nombres del Personal">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Codigo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4" HeaderText="Horario">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Atenci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c6">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c7">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblEtqNuevo" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="false"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblHAEtq1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" Text="Define / Modifica Horarios de Atención del Personal" __designer:wfdid="w47"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="lblHAEtq6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" Text="Nuevo Horario" Visible="False" __designer:wfdid="w65"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblHAEtq2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Area" __designer:wfdid="w48"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboHAArea" runat="server" Width="244px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboHAArea_SelectedIndexChanged" __designer:wfdid="w51"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblHAEtq5" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Text="Atención" Visible="False" __designer:wfdid="w62"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboHATipo" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w63"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblHAEtq3" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Text="Personal" __designer:wfdid="w49"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:DropDownList id="cboHAPersonal" runat="server" Width="244px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboHAPersonal_SelectedIndexChanged" __designer:wfdid="w56"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left colSpan=3><asp:Button id="btnHACancelar" onclick="btnHACancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Cancelar" Visible="False" __designer:wfdid="w68"></asp:Button> <asp:Button id="btnHAGuardar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Guardar" Visible="False" __designer:wfdid="w69" OnClick="btnHAGuardar_Click"></asp:Button> &nbsp;</TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 290px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 150px"><asp:GridView id="FlexHorAtencion" runat="server" Width="330px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w57"><Columns>
<asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="c1" HeaderText="#">
<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c2" HeaderText="Dias y Horas">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Tipo de Atenci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c4">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 230px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 150px" id="lblGrillaHorario" runat="server" Visible="False"><asp:GridView id="FlexHora" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w70" AllowPaging="True" PageSize="7"><Columns>
<asp:BoundField DataField="c0" HeaderText="#">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c1" HeaderText="D&#237;a">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="De"><ItemTemplate>
<asp:DropDownList id="cboD1" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w82"></asp:DropDownList>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="A"><ItemTemplate>
<asp:DropDownList id="cboA1" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w72"></asp:DropDownList>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="De"><ItemTemplate>
<asp:DropDownList id="cboD2" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w73"></asp:DropDownList>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="A"><ItemTemplate>
<asp:DropDownList id="cboA2" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w74"></asp:DropDownList>
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c6">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:Label id="lblHAEtq4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Indicar Minutos aprox. que puede tener por cita" __designer:wfdid="w59"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboHAMin" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w58"><asp:ListItem>--</asp:ListItem>
<asp:ListItem Value="5">5 min</asp:ListItem>
<asp:ListItem Value="10">10 min</asp:ListItem>
<asp:ListItem Value="15">15 min</asp:ListItem>
<asp:ListItem Value="20">20 min</asp:ListItem>
<asp:ListItem Value="25">25 min</asp:ListItem>
<asp:ListItem Value="30">30 min</asp:ListItem>
<asp:ListItem Value="35">35 min</asp:ListItem>
<asp:ListItem Value="40">40 min</asp:ListItem>
<asp:ListItem Value="45">45 min</asp:ListItem>
<asp:ListItem Value="50">50 min</asp:ListItem>
<asp:ListItem Value="55">55 min</asp:ListItem>
<asp:ListItem Value="60">60 min</asp:ListItem>
<asp:ListItem Value="65">65 min</asp:ListItem>
<asp:ListItem Value="70">70 min</asp:ListItem>
<asp:ListItem Value="75">75 min</asp:ListItem>
<asp:ListItem Value="80">80 min</asp:ListItem>
<asp:ListItem Value="85">85 min</asp:ListItem>
<asp:ListItem Value="90">90 min</asp:ListItem>
</asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 21px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 21px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 21px" vAlign=top align=left><asp:Button id="btnHAHorario" runat="server" CssClass="EstiloBoton_Ac" Width="118px" Text="Hor. Semanal" __designer:wfdid="w61" OnClick="btnHAHorario_Click"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 68px; HEIGHT: 21px" vAlign=top align=left><asp:Button id="btnHANuevo" onclick="btnHANuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="63px" Text="Agregar" __designer:wfdid="w60"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 21px" vAlign=top align=left colSpan=4><asp:Button id="btnHACerrar" runat="server" CssClass="EstiloBoton_Ac" Width="144px" Text="Cerrar Mantenimiento" __designer:wfdid="w75" OnClick="btnHACerrar_Click"></asp:Button></TD></TR></TBODY></TABLE></DIV></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
Personal por Area&nbsp; 
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 60px" vAlign=top align=left></TD><TD style="WIDTH: 200px" vAlign=top align=left></TD><TD style="WIDTH: 200px" vAlign=top align=left></TD><TD style="WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblPLError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w16"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Grupo" __designer:wfdid="w17"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboPLGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w18"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPLListar"  runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" Text="Listar" __designer:wfdid="w19"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Empresa" __designer:wfdid="w20"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboPLEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w21"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnPLAsignar" onclick="btnPLAsignar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Asignar" __designer:wfdid="w22"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexPL" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnSelectedIndexChanged="FlexPL_SelectedIndexChanged" Font-Overline="False" __designer:wfdid="w23"><Columns>
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
<asp:BoundField DataField="c4" HeaderText="Cod. Area">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c5" HeaderText="Nombre del Area">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblPLIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtq17" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" Text="Personal x Area" __designer:wfdid="w24"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq18" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Personal" __designer:wfdid="w25"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboPLPersonal" runat="server" Width="472px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboPLPersonal_SelectedIndexChanged" __designer:wfdid="w26"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq19" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Personal" __designer:wfdid="w27"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4 runat="server"><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px" id="DIV1"><asp:GridView id="FlexPLPersonal" runat="server" Width="580px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" __designer:wfdid="w28"><Columns>
<asp:TemplateField><ItemTemplate>
<asp:CheckBox id="chkPer" runat="server" Font-Size="8pt" Font-Names="Arial"></asp:CheckBox> 
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="c2" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="c3" HeaderText="Nombre del Area">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="500px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnPLGuardar" onclick="btnPLGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" Text="Guardar" __designer:wfdid="w29"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnPLCancelar" onclick="btnPLCancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" Text="Cancelar" __designer:wfdid="w30"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
    Areas
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 540px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblAError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboAGrupo" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnAListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" Text="Listar" OnClick="btnAListar_Click"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblEtq4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboAEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnANuevo" onclick="btnANuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Font-Size="8pt" Font-Names="Arial" Text="Nuevo"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 200px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 530px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 190px"><asp:GridView id="FlexA" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False"><Columns>
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
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 536px" id="lblAIngresar" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtq14" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" Text="Nueva Area"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblEtq15" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nombre"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtANombre" runat="server" Width="464px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 316px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtACodigo" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnAGuardar" onclick="btnAGuardar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Guardar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnACancelar" onclick="btnACancelar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Cancelar"></asp:Button> </TD></TR></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp2:AsyncPostBackTrigger>
<asp2:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp2:AsyncPostBackTrigger>
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

