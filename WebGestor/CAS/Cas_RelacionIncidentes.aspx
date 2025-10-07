<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_RelacionIncidentes.aspx.vb" Inherits="Cas_RelacionIncidentes" title="GestorPlus" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: left">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td align="left" style="width: 25px; height: 50px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: text-top; width: 551px; height: 50px; text-align: center"
                        valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Relación de Incidentes</div>
                    </td>
                    <td align="left" style="width: 25px; height: 50px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                        </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 15px" valign="top">
                    </td>
                    <td align="left" style="width: 551px; height: 15px" valign="top">
                        <asp:Button ID="btnExpportar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Exportar" Width="60px" style="left: 688px; top: 432px;" /></td>
                    <td align="left" style="width: 25px; height: 15px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px" valign="top">
                    </td>
                    <td align="left" style="width: 551px" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
<%--<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="550px" ActiveTabIndex="0">--%>
    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                            Incidentes
                                        
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 60px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 40px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 130px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl1" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Nº Incidente"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtIncidente" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lbl2" runat="server" Width="33px" Font-Size="8pt" Font-Names="Arial" Text="Fecha"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtFechaD" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" MaxLength="10"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtFechaA" runat="server" Width="57px" Font-Size="8pt" Font-Names="Arial" MaxLength="10"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkTipo" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" Text="Tipo" AutoPostBack="True"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboTipo" runat="server" Width="128px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkImportancia" runat="server" Width="77px" Font-Size="8pt" Font-Names="Arial" Text="Importancia" AutoPostBack="True"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:DropDownList id="cboImportancia" runat="server" Width="236px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkEstado" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" Text="Estado" AutoPostBack="True"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboEstado" runat="server" Width="128px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkComponente" runat="server" Width="76px" Font-Size="8pt" Font-Names="Arial" Text="Componente" AutoPostBack="True"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:DropDownList id="cboComponente" runat="server" Width="236px" Font-Size="8pt" Font-Names="Arial" Enabled="False" OnSelectedIndexChanged="cboComponente_SelectedIndexChanged"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:CheckBox id="chkElemento" runat="server" Width="66px" Font-Size="8pt" Font-Names="Arial" Text="Elemento" AutoPostBack="True" OnCheckedChanged="chkElemento_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboElemento" runat="server" Width="128px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Enabled="False"></asp:DropDownList> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left>&nbsp;</TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left>&nbsp;<asp:Button id="cmdListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="60px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" EnableTheming="True" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 530px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 284px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1840px" Font-Size="8pt" Font-Names="Arial" PageSize="30" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Solucion" Text="Soluci&#243;n" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="75px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Mostrar" Text="Mostrar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="75px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="75px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="APROB_FECHA_REPORTA" HeaderText="Fecha Rep.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_HORA_REPORTA" HeaderText="Hora Rep.">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="pEstado" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRIORIDAD" HeaderText="Importancia">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_CODIGO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Componente">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOM_PROB1_NOM_PROB2" HeaderText="Concepto de Problema">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_PROBLEMA_DESCRIPCION" HeaderText="Descripci&#243;n del Problema">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_USUARIO_REPORTA" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TBCAS_PERSONA_APELLIDOS" HeaderText="Apellidos y Nombres del persona que report&#243; el problema">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="BANCO_OFICINA" HeaderText="Banco - Oficina">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_ASIGNADO_PERSONA" HeaderText="Para Usuario">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_ESTADO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_TIPO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="COD_PROBLEMA" HeaderText="N&#186; Prob.">
<ItemStyle Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_PROBLEMA1">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_PROBLEMA2">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_PRIORIDAD">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INC_TELEFONO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_ASIGNADO_TIPO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_USUARIO_REGISTRA" HeaderText="Usuario que Registra">
<ItemStyle Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="INC_SEGUIMIENTO">
<ItemStyle Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SEGUIMIENTO" HeaderText="Seguimiento">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 130px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7><DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 530px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 100px"><asp:GridView id="FlexDet" runat="server" Width="650px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False"><Columns>
<asp:BoundField DataField="FECHA" HeaderText="Fecha">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA" HeaderText="Hora">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NOMBRE" HeaderText="Quien Soluciona">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_ACCION" HeaderText="Inicia Seguimiento">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHAFIN_SEG" HeaderText="Termina Seguimiento">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7><asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="WIDTH: 80px" vAlign=top align=left></TD><TD style="WIDTH: 60px" vAlign=top align=left></TD><TD style="WIDTH: 40px" vAlign=top align=left></TD><TD style="WIDTH: 70px" vAlign=top align=left></TD><TD style="WIDTH: 70px" vAlign=top align=left></TD><TD style="WIDTH: 70px" vAlign=top align=left></TD><TD style="WIDTH: 130px" vAlign=top align=left></TD></TR></TBODY></TABLE></DIV><cc1:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtFechaD" Enabled="True" PopupButtonID="txtFechaD" Format="dd/MM/yyyy"></cc1:CalendarExtender> <cc1:CalendarExtender id="CalendarExtender2" runat="server" TargetControlID="txtFechaA" Enabled="True" PopupButtonID="txtFechaA" Format="dd/MM/yyyy" ScriptPath=""></cc1:CalendarExtender> 
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                            Información del Incidente
                                        
</HeaderTemplate>
<ContentTemplate>
                                            <div style="text-align: left">
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
                                                    <tr>
                                                        <td style="width: 50px; height: 15px">
                                                        </td>
                                                        <td style="width: 30px; height: 15px">
                                                        </td>
                                                        <td style="width: 70px; height: 15px">
                                                        </td>
                                                        <td style="width: 80px; height: 15px">
                                                        </td>
                                                        <td style="width: 150px; height: 15px">
                                                        </td>
                                                        <td style="width: 50px; height: 15px">
                                                        </td>
                                                        <td style="width: 20px; height: 15px">
                                                        </td>
                                                        <td style="width: 80px; height: 15px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; height: 22px" colspan="2">
                                                            <asp:Label ID="lbl1M" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                                                                Text="Nº de Incidente" Width="75px"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 70px; height: 22px">
                                                            <asp:TextBox ID="txtNIncidente" runat="server" BorderColor="Black" BorderStyle="Outset"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="68px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 80px; height: 22px; vertical-align: middle;">
                                                        </td>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="Label15M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Inicia Llamada"
                                                                Width="69px"></asp:Label>
                                                            &nbsp;<asp:TextBox ID="txtIniLlamada" runat="server" BorderColor="Black" BorderStyle="Outset"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" MaxLength="8" ReadOnly="True"
                                                                Width="72px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 80px; height: 22px; vertical-align: middle;">
                                                            <asp:Button ID="btnRegresar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnRegresar_Click"
                                                                onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                                                Text="Regresar" Width="85px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 50px; height: 22px">
                                                            <asp:Label ID="Label1M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Usuario"
                                                                Width="45px"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="vertical-align: middle; height: 22px">
                                                            <asp:TextBox ID="txtNUsuario" runat="server" AutoPostBack="True" BorderColor="Black"
                                                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px"
                                                                MaxLength="7" Width="98px"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px">
                                                            <asp:Label ID="Label3M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Banco/Oficina"
                                                                Width="71px"></asp:Label>
                                                        </td>
                                                        <td colspan="4" style="vertical-align: middle; height: 22px">
                                                            <asp:TextBox ID="txtNOficina" runat="server" BorderColor="Black" BorderStyle="Outset"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True"
                                                                Width="298px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: middle; width: 50px; height: 22px">
                                                            <asp:Label ID="Label2M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres"
                                                                Width="45px"></asp:Label>
                                                        </td>
                                                        <td colspan="4" style="vertical-align: middle; height: 22px">
                                                            <asp:TextBox ID="txtNNombre" runat="server" BorderColor="Black" BorderStyle="Outset"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True"
                                                                Width="328px"></asp:TextBox>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 50px; height: 22px">
                                                            <asp:Label ID="Label4M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Teléfono"
                                                                Width="45px"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="vertical-align: middle; height: 22px">
                                                            <asp:TextBox ID="txtNTelefono" runat="server" BorderColor="Black" BorderStyle="Outset"
                                                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True"
                                                                Width="98px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="Label7M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Componente"
                                                                Width="50px"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="Label5M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Elemento"
                                                                Width="50px"></asp:Label>
                                                        </td>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                            <asp:DropDownList ID="cboNComponente" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                Font-Size="8pt" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td colspan="2" style="vertical-align: middle; height: 22px">
                                                            <asp:DropDownList ID="cboNElemento" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                Font-Size="8pt" Width="228px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                            <asp:DropDownList ID="cboNElemento2" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                Font-Size="8pt" Width="148px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción del Problema"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 50px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 20px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="vertical-align: middle; height: 22px">
                                                            <asp:TextBox ID="txtNDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Height="140px" MaxLength="2000" TextMode="MultiLine" Width="528px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Solución"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 150px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 50px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 20px; height: 22px">
                                                        </td>
                                                        <td style="vertical-align: middle; width: 80px; height: 22px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="vertical-align: middle; height: 22px">
                                                            <asp:TextBox ID="txtNSolucion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Height="140px" MaxLength="2000" TextMode="MultiLine" Width="528px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="Label9m" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Importancia"
                                                                Width="69px"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="vertical-align: middle; height: 22px">
                                                            <asp:DropDownList ID="cboNImportancia" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                Font-Size="8pt" Width="148px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" colspan="2">
                                                            &nbsp;<asp:Label ID="Label11m" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo"
                                                                Width="23px"></asp:Label>
                                                            <asp:DropDownList ID="cboNTipo" runat="server" AutoPostBack="True" Font-Names="Arial"
                                                                Font-Size="8pt" Width="147px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="vertical-align: middle; height: 22px" colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8" style="vertical-align: middle; height: 22px">
                                                            <asp:Label ID="lblIError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50px; height: 19px;">
                                                        </td>
                                                        <td style="width: 30px; height: 19px;">
                                                        </td>
                                                        <td style="width: 70px; height: 19px;">
                                                        </td>
                                                        <td style="width: 80px; height: 19px;">
                                                        </td>
                                                        <td style="width: 150px; height: 19px;">
                                                        </td>
                                                        <td style="width: 50px; height: 19px;">
                                                        </td>
                                                        <td style="width: 20px; height: 19px;">
                                                        </td>
                                                        <td style="width: 80px; height: 19px;">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="left" style="width: 25px" valign="top">
                    </td>
                </tr>
            </table>
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
        </div>
    </div>
    </asp:Content>

