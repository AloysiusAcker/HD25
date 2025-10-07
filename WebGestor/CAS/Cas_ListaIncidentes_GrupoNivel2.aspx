<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_ListaIncidentes_GrupoNivel2.aspx.vb" Inherits="Cas_ListaIncidentes_GrupoNivel2" title="GestorPlus" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" colspan="9" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 352px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Relación de Incidentes para Grupo</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="background-image: url(../Fotos/lineaCas.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 45px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 45px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 140px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
                <td align="left" colspan="7" valign="top" style="height: 1px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" Font-Names="Arial"  CssClass="MyTabStyle ajax__tab_header">
<%--<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="500px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="1" AutoPostBack="True">--%>
    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                        Relación de Incidentes
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 45px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 95px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 310px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 45px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblI1" runat="server" Width="38px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w187" Text="Grupo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboGrupo" runat="server" Width="404px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w188"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="77px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" __designer:wfdid="w189" Text="Listar" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" EnableTheming="True" BackColor="LightGray"></asp:Button> </TD></TR></TBODY></TABLE></DIV><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkImportancia" runat="server" Width="84px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w190" Text="Importancia" OnCheckedChanged="chkImportancia_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left><asp:UpdatePanel id="UpdatePanel5" runat="server" __designer:wfdid="w191"><ContentTemplate>
<asp:DropDownList id="cboImportancia" runat="server" Width="168px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w192" Enabled="False">
                    </asp:DropDownList> 
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="chkImportancia" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkTipo" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w193" Text="Tipo" OnCheckedChanged="chkTipo_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left><asp:UpdatePanel id="UpdatePanel4" runat="server" __designer:wfdid="w194"><ContentTemplate>
<asp:DropDownList id="cboTipo" runat="server" Width="197px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w195" Enabled="False">
                    </asp:DropDownList> 
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="chkTipo" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkComponente" runat="server" Width="81px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w196" Text="Componente" OnCheckedChanged="chkComponente_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left><asp:UpdatePanel id="UpdatePanel3" runat="server" __designer:wfdid="w197"><ContentTemplate>
<asp:DropDownList id="cboComponente" runat="server" Width="168px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w198" OnSelectedIndexChanged="cboComponente_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList> 
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="chkComponente" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkElemento" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w199" Text="Elemento" OnCheckedChanged="chkElemento_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left><asp:UpdatePanel id="UpdatePanel2" runat="server" __designer:wfdid="w200"><ContentTemplate>
<asp:DropDownList id="cboElemento" runat="server" Width="197px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w201" OnSelectedIndexChanged="cboElemento_SelectedIndexChanged" EnableTheming="True" Enabled="False">
                    </asp:DropDownList> 
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="chkElemento" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboComponente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 90px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Label id="lblCI" runat="server" Width="20px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w202" BorderStyle="Outset" BorderWidth="1px" BorderColor="DarkGray" BackColor="YellowGreen"></asp:Label> <asp:Label id="lblCIL" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w203" Text="Con Seguimiento"></asp:Label> &nbsp;</TD></TR></TBODY></TABLE></DIV><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 15px; TEXT-ALIGN: center" vAlign=top align=left colSpan=7>&nbsp;&nbsp; </TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=7><DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 525px; BORDER-BOTTOM: dimgray 1px outset; HEIGHT: 200px"><asp:GridView id="Flex" runat="server" Width="1610px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w204" PageSize="8" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Solucion" Text="Detalle" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Mostrar" Text="Soluci&#243;n" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="COD_PROBLEMA" HeaderText="N&#186; Prob.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_FECHA_REPORTA" HeaderText="Fecha">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_HORA_REPORTA" HeaderText="Hora">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="pEstado" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRIORIDAD" HeaderText="Importancia">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Componente">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOM_PROB1_NOM_PROB2" HeaderText="Elemento">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_PROBLEMA_DESCRIPCION" HeaderText="Descripci&#243;n del Problema">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_USUARIO_REPORTA" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TBCAS_PERSONA_APELLIDOS" HeaderText="Persona que report&#243; el problema">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="BANCO_OFICINA" HeaderText="Banco - Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_ASIGNADO_PERSONA" HeaderText="Para Usuario">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRESU" HeaderText="Usuario que Registra">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_ESTADO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INC_SEGUIMIENTO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_REDIREC_PERSONA">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_USUARIO_REGISTRA">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 46px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 45px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 120px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=7><DIV style="BORDER-RIGHT: dimgray 1px outset; BORDER-TOP: dimgray 1px outset; OVERFLOW: auto; BORDER-LEFT: dimgray 1px outset; WIDTH: 525px; BORDER-BOTTOM: dimgray 1px outset; POSITION: static; HEIGHT: 100px"><asp:GridView id="FlexDet" runat="server" Width="650px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w205" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="FECHA" HeaderText="Fecha">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA" HeaderText="Hora">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="400px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE" HeaderText="Quien Soluciona">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7><asp:Label id="lblErrorF" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w206"></asp:Label> </TD></TR></TBODY></TABLE></DIV><DIV style="TEXT-ALIGN: left">&nbsp;&nbsp;</DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                        Nivel 2
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 50px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 30px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 150px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 50px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="lbl1" runat="server" Width="75px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w143" Text="Nº de Incidente"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtIncidente" runat="server" Width="63px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w144" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label15" runat="server" Width="68px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w145" Text="Inicia Llamada"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtIniLlamada" runat="server" Width="74px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w146" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black" ReadOnly="True" MaxLength="8"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label1" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w147" Text="Usuario"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtNUsuario" runat="server" Width="93px" Height="16px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w148" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black" MaxLength="7"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label3" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w149" Text="Banco/Oficina"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:TextBox id="txtNOficina" runat="server" Width="294px" Height="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w150" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black" ReadOnly="True"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label2" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w151" Text="Nombres"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:TextBox id="txtNNombre" runat="server" Width="324px" Height="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w152" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black" ReadOnly="True"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label4" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w153" Text="Teléfono"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:TextBox id="txtNTelefono" runat="server" Width="94px" Height="16px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w154" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black" ReadOnly="True"></asp:TextBox> </TD></TR></TBODY></TABLE></DIV><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label7" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w155" Text="Componente"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 151px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label5" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w156" Text="Elemento"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboNComponente" runat="server" Width="146px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w157" OnSelectedIndexChanged="cboNComponente_SelectedIndexChanged"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 151px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboNElemento" runat="server" Width="147px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w158" OnSelectedIndexChanged="cboNElemento_SelectedIndexChanged"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboNElemento2" runat="server" Width="148px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w159"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="cmdNBuscar" onmouseover="this.style.fontWeight='bolder'" onkeypress="javascript:if(event.keyCode==13){retur n false;}" onmouseout="this.style.fontWeight='normal'" runat="server" Width="77px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" __designer:wfdid="w160" Text="Buscar" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" EnableTheming="True" BackColor="LightGray"></asp:Button> </TD></TR></TBODY></TABLE></DIV><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:CheckBox id="chkNModificar" runat="server" ForeColor="Blue" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w161" Text="Modificar componente y Elemento" OnCheckedChanged="chkNModificar_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w162" Text="Descripción del Problema"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 150px" vAlign=top align=left colSpan=5><asp:TextBox id="txtNDescripcion" runat="server" Width="521px" Height="140px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w163" MaxLength="2000" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w164" Text="Solución"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 150px" vAlign=top align=left colSpan=5><asp:TextBox id="txtNSolucion" runat="server" Width="521px" Height="140px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w165" MaxLength="2000" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label9" runat="server" Width="69px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w166" Text="Importancia"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboNImportancia" runat="server" Width="148px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w167"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label11" runat="server" Width="23px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w168" Text="Tipo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboNTipo" runat="server" Width="147px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" __designer:wfdid="w169"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkNSeguimiento" runat="server" ForeColor="Blue" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w170" Text="Seguimiento"></asp:CheckBox> </TD></TR></TBODY></TABLE></DIV><DIV style="TEXT-ALIGN: left"></DIV><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: center" vAlign=top align=left colSpan=4><asp:Button id="btnGrabar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGrabar_Click" runat="server" Width="109px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" __designer:wfdid="w171" Text="Guardar Solución" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" EnableTheming="True" BackColor="LightGray"></asp:Button> <asp:Button id="btnRedireccionar" onmouseover="this.style.fontWeight='bolder'" onkeypress="javascript:if(event.keyCode==13){retur n false;}" onmouseout="this.style.fontWeight='normal'" onclick="btnRedireccionar_Click" runat="server" Width="113px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" __designer:wfdid="w172" Text="Redireccionar" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" EnableTheming="True" BackColor="LightGray"></asp:Button> <asp:Button id="btnTerminar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnTerminar_Click" runat="server" Width="133px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" __designer:wfdid="w173" Text="Terminar Seguimiento" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" EnableTheming="True" BackColor="LightGray"></asp:Button> <asp:Button id="btnRegresar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnRegresar_Click" runat="server" Width="85px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" __designer:wfdid="w174" Text="Regresar" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" EnableTheming="True" BackColor="LightGray"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label10" runat="server" Width="96px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w175" Text="Usuario que trabaja"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="lblNombreUsuarioSistema" runat="server" Width="394px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w176" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label12" runat="server" Width="99px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w177" Text="Registró el Inicidente"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="lblUserRegistra" runat="server" Width="394px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w178" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblRedireccion1" runat="server" Width="122px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w179" Text="Redireccionó el incidente"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left colSpan=3><asp:TextBox id="lblRedireccion2" runat="server" Width="394px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w180" BorderStyle="Outset" BorderWidth="1px" BorderColor="Black"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=4></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w181"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCodigoUG" runat="server" Width="46px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w188" BorderStyle="None"></asp:TextBox> <asp:TextBox id="txtTipoUG" runat="server" Width="43px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w187" BorderStyle="None"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left colSpan=3>&nbsp;&nbsp; <asp:TextBox id="txtNEstado" runat="server" Width="25px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w182" BorderStyle="None" Visible="False"></asp:TextBox> <asp:TextBox id="txtNSeguimiento" runat="server" Width="22px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w183" BorderStyle="None" Visible="False"></asp:TextBox> <asp:TextBox id="txtUserRedirec" runat="server" Width="32px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w184" BorderStyle="None" Visible="False"></asp:TextBox> <asp:TextBox id="txtUserRegistra" runat="server" Width="20px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w185" BorderStyle="None" Visible="False"></asp:TextBox> <asp:TextBox id="txtCodComponente" runat="server" Width="34px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w186" BorderStyle="None"></asp:TextBox> <asp:TextBox id="txtMotivoR" runat="server" Height="14px" ForeColor="White" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w189" BorderStyle="None"></asp:TextBox></TD></TR></TBODY></TABLE></DIV><cc1:ModalPopupExtender id="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="cmdNBuscar" __designer:wfdid="w190" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="100" X="400" CancelControlID="btnBCerrar"></cc1:ModalPopupExtender> <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="btnRedireccionar" __designer:wfdid="w191" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="100" X="400" CancelControlID="btnCerrarR" Drag="True"></cc1:ModalPopupExtender> <cc1:ModalPopupExtender id="ModalPopupExtender3" runat="server" PopupControlID="Panel3" TargetControlID="btnTerminar" __designer:wfdid="w192" Enabled="True" CacheDynamicResults="True" DynamicServicePath="" Y="300" X="500" CancelControlID="btnSN"></cc1:ModalPopupExtender> 
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                            <asp:AsyncPostBackTrigger ControlID="FlexG" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnSS" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnSN" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; height: 22px;" valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 350px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;" id="TABLE1" onclick="return TABLE1_onclick()">
                <tr>
                    <td align="left" rowspan="8" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 25px; background-color: darkgray;
                        text-align: center" valign="top">
                        <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Redireccionar Incidencia"></asp:Label></td>
                    <td align="left" rowspan="8" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; height: 22px; background-color: darkgray;" valign="top" colspan="2">
                        <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Motivo para la Redirección"
                            Width="130px"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; height: 22px; background-color: darkgray;" valign="top" colspan="2">
                        <asp:TextBox ID="txtMotivo" runat="server" Font-Names="Arial" Font-Size="8pt" Height="62px"
                            TextMode="MultiLine" Width="293px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 110px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Redireccionar hacia:"
                            Width="102px"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 190px; height: 22px; background-color: darkgray"
                        valign="top">
                                <asp:RadioButtonList ID="optRedireccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    RepeatDirection="Horizontal" Width="120px" AutoPostBack="True">
                                    <asp:ListItem Value="0">Grupo</asp:ListItem>
                                    <asp:ListItem Value="1">Usuario</asp:ListItem>
                                </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; height: 22px; background-color: darkgray; text-align: right;" valign="top" colspan="2"><asp:Button ID="btnCerrarR" runat="server"
                                                                        BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                        CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt"
                                                                        ForeColor="Gray" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                                                        Text="Cerrrar" Width="70px"  />
                        </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                                    <div id="Div2" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                                                        overflow: auto; border-left: darkgray 1px outset; width: 300px; border-bottom: darkgray 1px outset;
                                                        position: static; height: 180px">
                                                        <asp:GridView ID="FlexG" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            Font-Names="Arial" Font-Size="8pt" PageSize="7" Width="300px" BorderColor="DimGray" BorderStyle="Outset" BorderWidth="1px">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="Aceptar">
                                                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="GRUPO_COD">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Grupo">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="75px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Usuario">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NOMBRESP">
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="75px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                                    </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="optRedireccion" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                        <asp:Label ID="lblErrorR" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="FlexG" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="optRedireccion" EventName="PreRender" />
                                <asp:AsyncPostBackTrigger ControlID="FlexG" EventName="PageIndexChanging" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" style="background-color: darkgray; height: 25px;" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;">
                <tr>
                    <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Modo de Busqueda:"
                            Width="100px"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                        <asp:RadioButtonList ID="optModoBus" runat="server" AutoPostBack="True" Font-Names="Arial"
                            Font-Size="8pt" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="120px">
                            <asp:ListItem Selected="True" Value="0">A &#243; B</asp:ListItem>
                            <asp:ListItem Value="1">A y B</asp:ListItem>
                        </asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray; text-align: right;"
                        valign="top">
                        <asp:Button ID="btnBListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                            onmouseover="this.style.fontWeight='bolder'" Text="Listar" Width="60px" /></td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Palabras a Buscar:"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray; text-align: right;"
                        valign="top">
                        <asp:CheckBox ID="chkFiltros" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sin filtros" /></td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                        <asp:TextBox ID="txtBuscador" runat="server" Height="34px" TextMode="MultiLine" Width="440px"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 11px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 11px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 11px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 11px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 11px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; background-color: darkgray"
                        valign="top" colspan="3">
                        <div id="DIV1" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                            overflow: auto; border-left: darkgray 1px outset; width: 450px; border-bottom: darkgray 1px outset;
                            height: 221px">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="FlexB" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="CARCON_APLICATIVO,CARCON_PRODUCTO,CARCON_SUBPRODUCTO" Font-Names="Arial"
                                        Font-Size="8pt" PageSize="5" Width="1760px" BorderColor="DarkGray" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Top">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="Aceptar">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="60px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="1000px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_APLICATIVO">
                                                <HeaderStyle Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_PRODUCTO">
                                                <HeaderStyle Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CARCON_SUBPRODUCTO">
                                                <HeaderStyle Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="FlexB" EventName="PageIndexChanging" />
                                    <asp:AsyncPostBackTrigger ControlID="btnBListar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray; text-align: center;"
                        valign="top">
                        <asp:Button ID="btnBCerrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                            onmouseover="this.style.fontWeight='bolder'" Text="Cerrar" Width="52px" /></td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel3" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 200px; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;">
                <tr>
                    <td align="left" style="width: 24px; height: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 25px; background-color: darkgray;
                        text-align: center" valign="top">
                        <asp:Label ID="lblE1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Maroon" Text="Seguimiento"></asp:Label></td>
                    <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 24px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; background-color: darkgray;
                        text-align: center" valign="top">
                        <asp:Label ID="Label17" runat="server" Text="¿Seguro que desea Terminar el Seguimiento del Incidente.?" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                    <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 24px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 75px; height: 22px; background-color: darkgray; text-align: right;"
                        valign="top"><asp:Button ID="btnSS" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                            onmouseover="this.style.fontWeight='bolder'" Text="Si" Width="35px" />
                    </td>
                    <td align="left" style="vertical-align: middle; width: 75px; height: 22px; background-color: darkgray"
                        valign="top"><asp:Button ID="btnSN" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                            onmouseover="this.style.fontWeight='bolder'" Text="No" Width="35px" /></td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 24px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 75px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 75px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 25px; height: 25px; background-color: darkgray"
                        valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    &nbsp;
</asp:Content>

