<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="ControlVisitas_Registro.aspx.vb" Inherits="ControlVisitas_Registro" title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
<!--

function TABLE1_onclick() {

}

// -->
</script>
 <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <asp:Panel ID="PanelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    <img alt="Cargando" src="../Fotos/5.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="PanelUpdateProgress"
		BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1"  onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Control de Visitas</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top" colspan="7">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 11px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" ActiveTabIndex="1">
    <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
        <HeaderTemplate>Visitas</HeaderTemplate>
<ContentTemplate>
    <div id="DivTabla" style="text-align: left">
        <table style="width: 544px" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td style="vertical-align: middle; width: 90px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 160px; text-align: left" valign="top" align="left">&nbsp;</td>
                    <td style="vertical-align: middle; width: 60px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 160px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 74px; text-align: left" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 540px;  height: 22px; text-align: left" valign="top" align="left" colspan="5">
                        <asp:Label ID="LblError" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 90px; height: 24px; text-align: left" valign="top" align="left">
                        <asp:Label ID="Label1" runat="server" Font-Size="8pt"  width="86px" Font-Names="Arial" Text="Pto. Control"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; height: 24px; text-align: left; width: 380px;" valign="top" align="left" colspan="3">
                        <asp:DropDownList ID="CboPtoControl" runat="server" Width="370px" Font-Size="8pt" Font-Names="Arial"
                            AutoPostBack="True" Height="16px">
                        </asp:DropDownList></td>
                    <td style="vertical-align: middle; width: 74px; height: 24px; text-align: left" valign="top" align="left">
                        <asp:Button ID="BtnRegistrar" runat="server" BorderWidth="1px" CssClass="EstiloBoton_Ac" OnClick="btnRegistrar_Click" Text="Registrar" Width="70px" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 90px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Persona Controla" Width="84px"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; height: 22px; text-align: left; width: 380px;" valign="top" align="left" colspan="3">
                        <asp:DropDownList ID="CboPControla" runat="server" Width="370px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Height="16px"></asp:DropDownList>
                    </td>
                    <td style="vertical-align: middle; width: 74px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton_Ac" Font-Size="8pt" OnClick="btnListar_Click" Text="Listar" Width="70px" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 90px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:Label ID="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Fecha" Width="84px"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:TextBox ID="TxtFecha" runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial" Height="16px"></asp:TextBox>
                    </td>
                    <td style="vertical-align: middle; width: 60px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:Label ID="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Registro" Width="50px"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:DropDownList ID="CboRegistro" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px">
                            <asp:ListItem Selected="True" Value="0">... TODOS</asp:ListItem>
                            <asp:ListItem Value="1">... ENTRADAS</asp:ListItem>
                            <asp:ListItem Value="2">... SALIDAS</asp:ListItem>
                            <asp:ListItem Value="3">... ENT./SAL.</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="vertical-align: middle; width: 74px; height: 22px; text-align: left" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 90px; height: 22px; text-align: left" valign="top" align="left">&nbsp;</td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left">&nbsp;</td>
                    <td style="vertical-align: middle; width: 60px; height: 22px; text-align: left" valign="top" align="left">&nbsp;</td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left">&nbsp;</td>
                    <td style="vertical-align: middle; width: 74px; height: 22px; text-align: left" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; Width=544px; height: 22px; text-align: left" valign="top" align="left" colspan="5">
                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 536px; border-bottom: gray 1px outset; position: static; height: 232px">
                            <asp:GridView ID="Flex" runat="server" Width="536px" Height="168px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" AllowPaging="True">
                                <Columns>
                                    <asp:ButtonField CommandName="Equipo" Text="Equipo" ButtonType="Button">
                                        <ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>

                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="c0" HeaderText="#"></asp:BoundField>
                                    <asp:BoundField DataField="c1"></asp:BoundField>
                                    <asp:BoundField DataField="c2" HeaderText="H. Entrada"></asp:BoundField>
                                    <asp:BoundField DataField="c3" HeaderText="H. Salida"></asp:BoundField>
                                    <asp:BoundField DataField="c4" HeaderText="Nro. Tarjeta"></asp:BoundField>
                                    <asp:BoundField DataField="c5" HeaderText="Ap. y Nombres del Visitante"></asp:BoundField>
                                    <asp:BoundField DataField="c6" HeaderText="Tipo y Nro. Documento"></asp:BoundField>
                                    <asp:BoundField DataField="c7" HeaderText="Empresa "></asp:BoundField>
                                    <asp:BoundField DataField="c8" HeaderText="A Quien Visita"></asp:BoundField>
                                    <asp:BoundField DataField="c9" HeaderText="Tipo de Visita"></asp:BoundField>
                                    <asp:BoundField DataField="c10" HeaderText="Registra Entrada"></asp:BoundField>
                                    <asp:BoundField DataField="c11" HeaderText="Registra Salida"></asp:BoundField>
                                    <asp:BoundField DataField="c12" HeaderText="Estado Visita"></asp:BoundField>
                                    <asp:BoundField DataField="c13" HeaderText="Cod. Visita"></asp:BoundField>
                                    <asp:BoundField DataField="c14" HeaderText="Fecha"></asp:BoundField>
                                    <asp:BoundField DataField="c15">
                                        <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            &nbsp;
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 90px; height: 22px; text-align: left" valign="top" align="left">
                        <asp:Label ID="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" Text="Equipos" Width="80px"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 60px; height: 22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 74px; height: 22px; text-align: left" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px; text-align: left" valign="top" align="left" colspan="5">
                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 536px; border-bottom: gray 1px outset; height: 100px">
                            <asp:GridView ID="FlexEquipo" runat="server" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="C1" HeaderText="#">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nro_Serie" HeaderText="Nro Serie">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Marca" HeaderText="Marca">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripci&#243;n">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 90px; height: 22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 160px; height:22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 60px; height: 22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 160px; height: 22px; text-align: left" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 74px; height: 22px; text-align: left" valign="top" align="left"></td>
                </tr>
            </tbody>
        </table>
    </div>
    <cc1:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtFecha" Enabled="True" PopupButtonID="txtFecha" Format="dd/MM/yyyy"></cc1:CalendarExtender> &nbsp; 
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2">
    <HeaderTemplate>Registrar</HeaderTemplate>
<ContentTemplate>
    <div style="text-align: left">
        <table style="width: 544px" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 19px;" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 120px; height: 19px;" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 102px; height: 19px;" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 19px;" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 19px;" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                        <asp:Label ID="LblRVError" runat="server" Width="536px" Font-Size="8pt" Font-Names="Arial"
                            ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV14" runat="server" Font-Size="8pt" Font-Names="Arial"
                            Text="Pto. Control"></asp:Label></td>
                    <td style="vertical-align: middle; width: 343px; height: 22px" valign="top" align="left" colspan="3">
                        <asp:DropDownList ID="CboRVPtoControl" runat="server" Width="336px" Font-Size="8pt"
                            Font-Names="Arial" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtRVFecha" runat="server" Width="108px" Font-Size="8pt"
                            Font-Names="Arial" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV15" runat="server" Width="80px" Font-Size="8pt" Font-Names="Arial"
                            Text="Persona Control"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; width: 343px; height: 22px" valign="top" align="left" colspan="3">
                        <asp:DropDownList ID="CboRVPControla" runat="server" Width="336px" Font-Size="8pt"
                            Font-Names="Arial">
                        </asp:DropDownList>
                    </td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtRVCodPersonaControl" runat="server" Width="108px" Font-Size="8pt"
                            Font-Names="Arial" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 21px" valign="top" align="left">
                        <asp:Label ID="LblRV1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Registrar"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; height: 21px" valign="top" align="left" colspan="4">
                        <asp:DropDownList ID="CboRV" runat="server" Width="458px" Font-Size="8pt" Font-Names="Arial"
                            OnSelectedIndexChanged="CboRV_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="Seleccionar">Seleccionar</asp:ListItem>
                            <asp:ListItem Value="1">Una entrada</asp:ListItem>
                            <asp:ListItem Value="2">Una salida</asp:ListItem>
                            <asp:ListItem Value="3">Una salida de una entrada</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Visitante"></asp:Label>
                    </td>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                        <asp:DropDownList ID="CboRVisitante" runat="server" Width="458px" Font-Size="8pt"
                            Font-Names="Arial" Enabled="False" OnSelectedIndexChanged="CboRVisitante_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; 23px: ;" valign="top" align="left" colspan="4">
                        <asp:RadioButtonList ID="OptRV" runat="server" Font-Size="8pt" Font-Names="Arial"
                            OnSelectedIndexChanged="OptRV_SelectedIndexChanged" AutoPostBack="True"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="Personal en General">Personal en General</asp:ListItem>
                            <asp:ListItem Value="Personal que Labora">Personal que Labora</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="vertical-align: middle; width: 121px; height: 23px" valign="top" align="left">
                        <asp:TextBox ID="LblRVPtoControl" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="TxtRVIDato" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="TxtRVCodDato" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="TxtRVCodVisita" runat="server" Width="16px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                        <div style="text-align: left">
                            <table style="width: 540px" id="LblPersonaGeneral" cellspacing="0" cellpadding="0" border="0" runat="server">
                                <tr runat="server">
                                    <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 19px" valign="top"></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 150px; height: 19px" valign="top"></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 75px; height: 19px" valign="top"></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 75px; height: 19px" valign="top"></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 170px; height: 19px" valign="top"></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                                        <asp:Label ID="LblRV3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Tipo Persona" Width="64px"></asp:Label></td>
                                    <td runat="server" align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                        <asp:DropDownList ID="CboRVTipoPer" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Width="224px"></asp:DropDownList></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 75px; height: 22px" valign="top">
                                        <asp:Button ID="BtnRVBuscar" runat="server" CssClass="EstiloBoton_Ac" 
                                            Text="Buscar" Width="70px"></asp:Button></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 170px; height: 22px" valign="top">
                                        <asp:Label ID="LblRV6" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Tipo Doc."></asp:Label></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" align="left" style="vertical-align: middle; width: 70px" valign="top">
                                        <asp:Label ID="LblRV4" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Apellidos"></asp:Label></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 150px" valign="top">
                                        <asp:TextBox ID="TxtRVApePat" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Width="140px"></asp:TextBox></td>
                                    <td runat="server" align="left" colspan="2" style="vertical-align: middle" valign="top">
                                        <asp:TextBox ID="TxtRVApeMat" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="140px"></asp:TextBox></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 170px" valign="top">
                                        <asp:DropDownList ID="CboRVTipoDoc" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Width="168px"></asp:DropDownList></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" align="left" style="vertical-align: middle; width: 70px" valign="top">
                                        <asp:Label ID="LblRV5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres"></asp:Label></td>
                                    <td runat="server" align="left" colspan="3" style="vertical-align: middle" valign="top">
                                        <asp:TextBox ID="TxtRVNombres" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Width="290px"></asp:TextBox></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 170px" valign="top">
                                        <asp:Label ID="LblRV7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro. Doc."></asp:Label></td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                                        <asp:Label ID="LblRV8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"></asp:Label></td>
                                    <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                        <asp:TextBox ID="TxtRVEmpresa" runat="server" Font-Names="Arial" Font-Size="8pt" Width="290px"></asp:TextBox></td>
                                    <td runat="server" align="left" style="vertical-align: middle; width: 170px; height: 22px" valign="top">
                                        <asp:TextBox ID="TxtRVNroDoc" runat="server" Font-Names="Arial" Font-Size="8pt" Width="160px" AutoPostBack="True"></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: left"></div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV10" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Tipo de Visita"></asp:Label></td>
                    <td style="vertical-align: middle; width: 120px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 102px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:Label ID="lblRV12" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Hora Entrada"></asp:Label></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV13" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Hora Salida"></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                        <asp:DropDownList ID="CboRVTipoVisita" runat="server" Width="296px" Font-Size="8pt" 
                            Font-Names="Arial"></asp:DropDownList></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtRVHora" runat="server" Width="104px" Font-Size="8pt" 
                            Font-Names="Arial"></asp:TextBox></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtRVHSalida" runat="server" Width="104px" Font-Size="8pt" 
                            Font-Names="Arial" Visible="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                        <asp:Label ID="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Agencia"></asp:Label></td>
                    <td style="vertical-align: middle; width: 102px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                    
                    <td style="vertical-align: middle; width: 121px;height: 22px" valign="top" align="left" colspan="2">
                        <asp:Label ID="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Tarjeta N°"></asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="4">
                        <asp:DropDownList ID="CboRVAgencia" runat="server" Width="414px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtTarjeta" runat="server" Width="104"  Font-Size="8pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                        <asp:Label ID="LblRV9" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Personal que Visita"></asp:Label></td>
                    <td style="vertical-align: middle; width: 102px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                        <asp:DropDownList ID="CboRVPersonal" runat="server" Width="536px" Font-Size="8pt" 
                            Font-Names="Arial"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV11" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Asunto"></asp:Label></td>
                    <td style="vertical-align: middle; width: 120px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 102px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                        <asp:TextBox ID="TxtRVAsunto" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV16" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Font-Bold="True" ForeColor="Maroon" Text="Equipos"></asp:Label></td>
                    <td style="vertical-align: middle; width: 120px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtRVCodEquipo" runat="server" Width="16px" Font-Size="8pt" 
                            Font-Names="Arial" Visible="False"></asp:TextBox></td>
                    <td style="vertical-align: middle; width: 102px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV17" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="N° Serie"></asp:Label></td>
                    <td style="vertical-align: middle; width: 120px; height: 22px" valign="top" align="left">
                        <asp:TextBox ID="TxtRVNroSerie" runat="server" Width="112px" Font-Size="8pt" 
                            Font-Names="Arial"></asp:TextBox></td>
                    <td style="vertical-align: middle; width: 102px; height: 22px" valign="top" align="left">
                        <table style="width: 100px" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td style="vertical-align: middle; width: 40px; height: 22px" valign="top" align="left">
                                        <asp:Button ID="BtnRVBusEq" OnClick="btnRVBusEq_Click" runat="server" 
                                            CssClass="EstiloBoton_Ac" Width="25px" Text="..."></asp:Button></td>
                                    <td style="vertical-align: middle; width: 60px; height: 22px; text-align: right" valign="top" align="left">
                                        <asp:Label ID="LblRV18" runat="server" Width="32px" Font-Size="8pt" 
                                            Font-Names="Arial" Text="Marca"></asp:Label></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="2">
                        <asp:TextBox ID="TxtRVMarca" runat="server" Width="224px" Font-Size="8pt" 
                            Font-Names="Arial"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left">
                        <asp:Label ID="LblRV19" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Descripción"></asp:Label></td>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="3">
                        <asp:TextBox ID="TxtRVDescripcion" runat="server" Width="328px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:Button ID="BtnRVIngEquipo" OnClick="BtnRVIngEquipo_Click" runat="server" 
                            CssClass="EstiloBoton_Ac" Width="110px" Text="Agregar Equipo"></asp:Button></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 22px" valign="top" align="left" colspan="5">
                        <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto; border-left: gray 1px outset; width: 530px; border-bottom: gray 1px outset; height: 100px">
                            <asp:GridView ID="FlexEquipos" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="c1" HeaderText="#">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c2" HeaderText="Nro. Serie">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c3" HeaderText="Marca">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c4" HeaderText="Descripcion">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c5">
                                        <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; width: 80px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 120px; height: 22px" valign="top" align="left"></td>
                    <td style="vertical-align: middle; height: 22px; text-align: right" valign="top" align="left" colspan="2">
                        <asp:Button ID="BtnRVRegresar" OnClick="BtnRVRegresar_Click" runat="server" 
                            CssClass="EstiloBoton_Ac" Width="110px" Text="Regresar"></asp:Button></td>
                    <td style="vertical-align: middle; width: 121px; height: 22px" valign="top" align="left">
                        <asp:Button ID="BtnRVGuardar" OnClick="BtnRVGuardar_Click" runat="server" 
                            CssClass="EstiloBoton_Ac" Width="110px" Text="Guardar"></asp:Button></td>
                </tr>
            </tbody>
        </table>
    </div>
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtRVHora"
        Enabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
        CultureThousandsPlaceholder="" CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder=""
        ClearMaskOnLostFocus="False" MaskType="Number" Mask="99:99">
    </cc1:MaskedEditExtender> 
    <cc1:MaskedEditExtender id="MaskedEditExtender2" runat="server" TargetControlID="txtRVHSalida" Enabled="True" 
        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureThousandsPlaceholder="" 
        CultureDecimalPlaceholder="" CultureTimePlaceholder="" CultureDatePlaceholder="" ClearMaskOnLostFocus="False" 
        MaskType="Number" Mask="99:99"></cc1:MaskedEditExtender> 
    <asp:Panel id="pnPersona" runat="server">
        <table style="border-right: black 1px outset; border-top: black 1px outset; left: 503px; 
                border-left: black 1px outset; width: 450px; border-bottom: black 1px outset; top: 541px" 
                cellspacing ="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td style="vertical-align: middle; height: 26px; background-color: darkgray; text-align: center" valign="top" align="left" colspan="5">
                        <asp:Label ID="lblBP1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                            Text="Relación de Personal de la Empresa"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 70px; height: 22px; background-color: darkgray" valign="top" align="left">
                        <asp:Label ID="lblRV20" runat="server" Font-Size="8pt" Font-Names="Arial"
                            Text="Tipo Persona"></asp:Label></td>
                    <td style="vertical-align: middle; width: 250px; height: 22px; background-color: darkgray" valign="top" align="left">
                        <asp:DropDownList ID="cboBusTipoPer" runat="server" Width="248px" Font-Size="8pt"
                            Font-Names="Arial">
                        </asp:DropDownList></td>
                    <td style="vertical-align: middle; width: 80px; height: 22px; background-color: darkgray" valign="top" align="left">
                        <asp:Button ID="btnBPCerrar" OnClick="btnBPCerrar_Click" runat="server"
                            CssClass="EstiloBoton_Ac" Width="72px" ForeColor="Gray" Text="Cerrar"></asp:Button></td>
                    <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                    <td style="vertical-align: middle; width: 70px; height: 22px; background-color: darkgray" valign="top" align="left">
                        <asp:Label ID="lblRV21" runat="server" Font-Size="8pt" Font-Names="Arial" 
                            Text="Ap. Paterno"></asp:Label></td>
                    <td style="vertical-align: middle; width: 250px; height: 22px; background-color: darkgray" valign="top" align="left">
                        <asp:TextBox ID="txtBusApePat" runat="server" Width="240px" Font-Size="8pt" 
                            Font-Names="Arial"></asp:TextBox></td>
                    <td style="vertical-align: middle; width: 80px; height: 22px; background-color: darkgray" valign="top" align="left">
                        <asp:Button ID="btnBPListar" OnClick="btnBPListar_Click" runat="server" 
                            CssClass="EstiloBoton_Ac" Width="72px" Text="Listar"></asp:Button></td>
                    <td style="width: 25px; height: 22px; background-color: darkgray" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="width: 25px; height: 200px; background-color: darkgray" valign="top" align="left"></td>
                    <td style="height: 200px; background-color: darkgray" valign="top" align="left" colspan="3">
                        <div style="border-right: darkgray 1px outset; border-top: darkgray 1px outset; font-size: 8pt; vertical-align: middle; overflow: auto; border-left: darkgray 1px outset; width: 392px; border-bottom: darkgray 1px outset; font-family: Arial; height: 198px; text-align: center" id="DIV2" runat="server">
                            <asp:GridView ID="FlexP" runat="server" Width="770px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="5">
                                <Columns>
                                    <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Button">
                                        <ControlStyle CssClass="EstiloBoton_Ac" Width="50px"></ControlStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="PERSON_CODIGO" HeaderText="C&#243;digo">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO_PER" HeaderText="Tipo Persona">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERSON_APEPAT" HeaderText="Ap. Paterno">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERSON_APEMAT" HeaderText="Ap. Materno">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="125px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERSON_NOMBRES" HeaderText="Nombres">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EMPRESA" HeaderText="Empresa">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc.">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERSON_NUMDOCIDE" HeaderText="Numero Doc.">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO_CODDOC">
                                        <ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO_CODPER">
                                        <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>
                            </asp:GridView>
                            <br />
                        </div>
                    </td>
                    <td style="width: 25px; height: 200px; background-color: darkgray" valign="top" align="left"></td>
                </tr>
                <tr>
                    <td style="vertical-align: middle; height: 25px; background-color: darkgray; text-align: center" valign="top" align="left" colspan="5"></td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
    <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" PopupControlID="pnPersona" BackgroundCssClass="modalBackground" TargetControlID="btnRVBuscar" Enabled="True" CacheDynamicResults="True" Y="200" X="300" CancelControlID="btnBPCerrar" DynamicServicePath=""></cc1:ModalPopupExtender> 
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
<triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
<asp1:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp1:AsyncPostBackTrigger>
</triggers>
</asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 160px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 160px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 11px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 11px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

