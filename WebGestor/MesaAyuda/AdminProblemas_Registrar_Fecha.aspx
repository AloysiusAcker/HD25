<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_Registrar_Fecha.aspx.vb" Inherits="AdminProblemas_Registrar_Fecha" title="Mesa de Ayuda - Registrar Fecha" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';   
		function divFileHide() {   
          var divFile = document.getElementById('fileUploadDiv');   
          divFile.style.display = 'none';
          var divLoading = document.getElementById('loadingFileDiv');   
          divLoading.style.display = 'block';}
        function divFileShow() {   
          var divFile = document.getElementById('fileUploadDiv');   
          iframe.style.display = 'block';   
          var divLoading = document.getElementById('loadingFileDiv');   
          divLoading.style.display = 'none';}
        function upload(){   
          divFileShow();}
        function onComplete( result ) {   
          divFileShow();}          
    </script>
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 233px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Registro de Problemas</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 19px;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtaviso" runat="server" BackColor="Red" BorderColor="Red" BorderStyle="Outset"
                                BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Width="68px">No hay Aviso</asp:TextBox>&nbsp;
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdBorrar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdResolver" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cboComponente" EventName="DataBinding" />
                            <asp:AsyncPostBackTrigger ControlID="cboElemento" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnDatos" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="FlexTG" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnNotificar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnACerrar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnCerrarTG" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 60px; height: 19px;" valign="top">
                </td>
                <td align="left" style="height: 19px" valign="top" colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Size="16pt" ForeColor="Maroon"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="FlexTG" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="cmdBorrar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cmdResolver" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="7" valign="top">
                    <asp:UpdatePanel id="UpdatePanel34" runat="server">
                        <contenttemplate>
<asp:Label id="lblErrorInc" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w1"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexTG" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdResolver" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnNotificar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnDatos" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnListarTG" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexTG" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexB" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnAListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<asp:RadioButtonList id="optIncidente" runat="server" Width="284px" Height="20px" Font-Size="8pt" Font-Names="Arial" RepeatDirection="Horizontal" OnSelectedIndexChanged="optIncidente_SelectedIndexChanged" AutoPostBack="True"><asp:ListItem Selected="True" Value="0">Reportar Problemae</asp:ListItem>
<asp:ListItem Value="1">Buscar Problema Reportado</asp:ListItem>
</asp:RadioButtonList> 
</ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnBusIncidente" runat="server" BackColor="LightGray"
                        BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton"
                        EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Prob. Reportado" Width="106px" /></td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 19px; text-align: right;" valign="top">
                    <asp:Button ID="btnBandeja" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px"  EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" 
                        CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Bandeja" Width="80px" /></td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 19px; text-align: left;" valign="top">
                    <asp:Button ID="btnAviso" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px"  EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" 
                        CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Aviso" Width="80px" /></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Llamada"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:TextBox ID="txtFechaLlamada" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ClearMaskOnLostFocus="False"
                        Mask="99/99/9999" MaskType="Number" TargetControlID="txtFechaLlamada">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFechaLlamada"
                        TargetControlID="txtFechaLlamada">
                    </cc1:CalendarExtender>
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Inicia Llamada"
                        Width="68px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:TextBox ID="txtIniLlamada" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" MaxLength="8" Width="60px"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="False"
                        Mask="99:99:99" MaskType="Number" TargetControlID="txtIniLlamada" UserTimeFormat="TwentyFourHour">
                    </cc1:MaskedEditExtender>
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px; text-align: right"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Text="Nº de Problema"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtIncidente" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" BackColor="WhiteSmoke" __designer:wfdid="w1" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                        <ContentTemplate>
<asp:Button id="btnBuscarInc" runat="server" Width="20px" Height="20px" Text="..." BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w29"></asp:Button>
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px; text-align: right"
                    valign="top">
                    &nbsp;</td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 23px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 23px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Usuario"
                        Width="45px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 23px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtUsuario" runat="server" Width="60px" Height="16px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" BackColor="WhiteSmoke" __designer:wfdid="w2" AutoPostBack="True" MaxLength="8"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 23px; text-align: right"
                    valign="top">
                    <asp:Button ID="btnDatos" runat="server"
                                    BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                    Height="20px" Text="..." Width="20px" />&nbsp;&nbsp;</td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 23px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
<asp:Label id="Label3" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" Text="Oficina"></asp:Label><asp:TextBox id="txtOficina" runat="server" Width="301px" Height="16px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" BackColor="WhiteSmoke" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnDatos" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUsuario" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 23px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres"
                        Width="45px"></asp:Label></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtNombre" runat="server" Width="266px" Height="16px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" BackColor="WhiteSmoke" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnDatos" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUsuario" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
<asp:Label id="Label4" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" Text="Teléfono"></asp:Label> <asp:TextBox id="txtTelefono" runat="server" Width="117px" Height="16px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" BackColor="WhiteSmoke" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnDatos" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUsuario" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox ID="chkOficina" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" OnCheckedChanged="chkOficina_CheckedChanged" Text="Oficina Actual"
                                Width="90px" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboOficina" runat="server" Enabled="False" Font-Names="Arial"
                                Font-Size="8pt" Width="270px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkOficina" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Teléfono"
                                Width="45px"></asp:Label>
                            <asp:TextBox ID="txtTelefActual" runat="server" BorderColor="Black" BorderStyle="Outset"
                                BorderWidth="1px" Enabled="False" Font-Names="Arial" Font-Size="8pt" Height="16px"
                                Width="117px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkOficina" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 9px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 9px" valign="top">
                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Componente"
                        Width="50px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 9px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 9px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 9px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 9px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 9px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 9px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 9px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboComponente" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" Width="180px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboElemento" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" OnSelectedIndexChanged="cboElemento_SelectedIndexChanged" Width="176px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboComponente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 19px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboElemento2" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" OnSelectedIndexChanged="cboElemento2_SelectedIndexChanged" Width="170px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboElemento" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 28px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 28px" valign="top">
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción del Problema"></asp:Label></td>
                <td align="left" colspan="1" style="vertical-align: middle; width: 30px; height: 28px"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 28px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 28px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 28px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 28px; text-align: left"
                    valign="top">
                    <asp:Button ID="cmdBuscar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onkeypress="javascript:if(event.keyCode==13){retur n false;}"
                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Buscar" Width="80px" /></td>
                <td align="left" style="width: 25px; height: 28px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtDescripcion" runat="server" Width="530px" Height="117px" Font-Size="8pt" Font-Names="Arial" MaxLength="2000" TextMode="MultiLine"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdBorrar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                    <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Solución"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtSolucion" runat="server" Width="530px" Height="116px" Font-Size="8pt" Font-Names="Arial" MaxLength="2000" TextMode="MultiLine"></asp:TextBox> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdBorrar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Prioridad"
                        Width="48px"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo"
                        Width="69px"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblImpacto" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Impacto"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboImportancia" runat="server" Width="176px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" Width="176px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel10" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboImpacto" runat="server" Width="166px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle" valign="top">
                    <div id="fileUploadDiv" style="width: 550px; height: 30px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 550px; height: 24px">
                            <tr>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:Label ID="lblEtArchivo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        Font-Strikeout="False" ForeColor="Maroon" Text="Solo para registrar"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 50px; height: 25px" valign="top">
                                    <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Archivo"
                                        Width="40px"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 500px; height: 25px" valign="top">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Visible="true" Width="480px" /></td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="height: 15px" valign="top">
                                    <div id="loadingFileDiv" style="display: none; font-size: 8pt; font-family: Arial">
                                        El archivo está subiendo...</div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle;" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 550px">
                        <tr>
                            <td align="left" style="width: 50px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
<asp:Label id="lblEtiqEstado" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Text="Estado" __designer:wfdid="w23" Visible="False"></asp:Label> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtIncidente" EventName="TextChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                            <td align="left" style="width: 500px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtEstado" runat="server" Width="308px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" __designer:wfdid="w24" ReadOnly="True" Visible="False"></asp:TextBox><BR />
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel></td>
                        </tr>
                    </table>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; height: 25px; text-align: center"
                    valign="top">
                    <asp:Button ID="btnRegistrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Gray" OnClick="btnRegistrar_Click" Text="Registrar"
                        Width="85px" />
                    <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<asp:Button id="cmdResolver" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="85px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Resolver" EnableTheming="True" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w15"></asp:Button><asp:Button id="btnNotificar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="85px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Notificar" EnableTheming="True" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w16"></asp:Button><asp:Button id="btnTGrupo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="113px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Transferir Grupo" EnableTheming="True" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w17"></asp:Button><asp:Button id="btnTIndividual" onmouseover="this.style.fontWeight='bolder'" onkeypress="javascript:if(event.keyCode==13){retur n false;}" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="129px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Transferir Individual" EnableTheming="True" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray" __designer:wfdid="w18"></asp:Button> <cc1:ModalPopupExtender id="ModalPopupExtender2" runat="server" PopupControlID="Panel3" TargetControlID="btnTGrupo" Y="100" X="300" CacheDynamicResults="True" CancelControlID="btnCerrarTG" __designer:wfdid="w19"></cc1:ModalPopupExtender> <cc1:ModalPopupExtender id="ModalPopupExtender3" runat="server" PopupControlID="Panel2" TargetControlID="btnTIndividual" Y="100" X="300" CacheDynamicResults="True" CancelControlID="btnCerrarTI" __designer:wfdid="w20"></cc1:ModalPopupExtender> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="7" style="vertical-align: middle; text-align: center"
                    valign="top">
                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                        <ContentTemplate>
<asp:Button id="cmdLimpiar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="85px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Limpiar" EnableTheming="True" CssClass="EstiloBoton" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button><asp:Button id="cmdBorrar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="173px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Borrar Descripción y Solución" EnableTheming="True" CssClass="EstiloBoton" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> <cc1:ModalPopupExtender id="ModalPopupExtender6" runat="server" PopupControlID="Panel6" BackgroundCssClass="modalBackground" TargetControlID="cmdLimpiar" CacheDynamicResults="True" CancelControlID="btnNo" Y="200" X="300" __designer:wfdid="w1"></cc1:ModalPopupExtender> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" colspan="7" valign="top" style="text-align: center">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            &nbsp;<asp:TextBox ID="lblElemento" runat="server" BorderColor="White" BorderStyle="Outset"
                                BorderWidth="1px" ReadOnly="True" Visible="False" Width="5px"></asp:TextBox>
                            <asp:TextBox ID="lblElemento2" runat="server" BorderColor="White" BorderStyle="Outset"
                                BorderWidth="1px" ReadOnly="True" Visible="False" Width="13px"></asp:TextBox>
                            <asp:TextBox ID="lblComponente" runat="server" BorderColor="White" BorderStyle="Outset"
                                BorderWidth="1px" ReadOnly="True" Visible="False" Width="8px"></asp:TextBox>
                            <asp:TextBox ID="lblCodOficina" runat="server" BorderColor="White" BorderStyle="Outset"
                                BorderWidth="1px" Visible="False" Width="5px"></asp:TextBox>
                            <asp:TextBox ID="lblCodEstado" runat="server" BorderColor="White" BorderStyle="Outset"
                                BorderWidth="1px" Width="17px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnDatos" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="txtUsuario" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cboComponente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cboElemento" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cboElemento2" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="optIncidente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmdLimpiar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarInc" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
        </table>
                    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress" style="left: 176px; top: 0px">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div style="position: relative; top: 30%; text-align: center;">
                                    &nbsp;<img src="../Fotos/5.gif" /></div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
        &nbsp;&nbsp;<asp:Button ID="btnUpload" runat="server" BackColor="LightGray" BorderColor="Gray"
            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Gray" OnClick="btnUpload_Click" Text="Subir Archivo"
            Visible="False" Width="88px" /></div>
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 950px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;" id="TABLE1" runat="server">
            <tr>
                <td align="left" rowspan="6" style="width: 25px; background-color: darkgray" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 20px; background-color: darkgray" valign="top">
                </td>
                <td align="left" rowspan="6" style="width: 25px; background-color: darkgray" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: middle; width: 150px; background-color: darkgray; height: 22px;"
                    valign="top">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Modo de Busqueda:"
                        Width="100px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 250px; background-color: darkgray; height: 22px;"
                    valign="top">
                            <asp:RadioButtonList ID="optModoBus" runat="server" Font-Names="Arial"
                                Font-Size="8pt" RepeatDirection="Horizontal" Width="116px" >
                                <asp:ListItem Selected="True" Value="0">A &#243; B</asp:ListItem>
                                <asp:ListItem Value="1">A y B</asp:ListItem>
                            </asp:RadioButtonList></td>
                <td align="left" style="width: 500px; background-color: darkgray;
                    text-align: right; height: 22px;" valign="top">
                    <asp:Button ID="btnListar" runat="server" BorderColor="Gray" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Listar"
                        Width="60px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" BackColor="LightGray"/>
                    <asp:Button ID="btnCerrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/></td>
            </tr>
            <tr>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px; background-color: darkgray"
                    valign="top">
                    <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Palabras a Buscar:"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px; background-color: darkgray"
                    valign="top">
                    <asp:CheckBox ID="chkFiltros" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sin filtros" Width="67px" />&nbsp;
                    <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0"
                        Text="** Palabras a buscar separadas por una coma. Si no lo busca como una frase completa."
                        Width="427px"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="background-color: darkgray; height: 40px; text-align: right;" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
<asp:TextBox id="txtBuscador" runat="server" Width="887px" Height="35px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine"></asp:TextBox>
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="background-color: darkgray; text-align: right;"
                    valign="top">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 890px; BORDER-BOTTOM: darkgray 1px outset; HEIGHT: 452px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="1100px" Font-Size="7pt" Font-Names="Arial" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ACARCON_APLICATIVO,ACARCON_PRODUCTO,ACARCON_SUBPRODUCTO" PageSize="30"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_TRANSACCION" HeaderText="Transacci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_CONSULTA" HeaderText="Consulta">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_SOLUCION" HeaderText="Soluci&#243;n">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="500px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_APLICATIVO">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_PRODUCTO">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ACARCON_SUBPRODUCTO">
<HeaderStyle Width="0px"></HeaderStyle>

<ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>
</asp:GridView>&nbsp;</DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging" />
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 20px; background-color: darkgray" valign="top">
                    &nbsp; &nbsp;&nbsp; &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:ModalPopupExtender 
    id="ModalPopupExtender1" 
                    runat="server" 
                    TargetControlID="cmdBuscar"
                    CancelControlID ="btnCerrar"
                    PopupControlID ="Panel1" 
                    CacheDynamicResults="True" BackgroundCssClass="modalBackground" X="2" Y="2" >
    </cc1:ModalPopupExtender>
    &nbsp;
    <div style="text-align: left">
        <asp:Panel ID="Panel2" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 350px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;">
                <tr>
                    <td align="left" rowspan="4" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" colspan="3" style="background-color: darkgray; vertical-align: middle; height: 25px; text-align: center; width: 300px;" valign="top">
                        <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Relación de Usuario"></asp:Label></td>
                    <td align="left" rowspan="4" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; background-color: darkgray;
                        text-align: left; width: 300px;" valign="top"><asp:Button ID="btnCerrarTI" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/>
                        <asp:Button ID="btnListarTI" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/></td>
                </tr>
                <tr>
                    <td align="left" colspan="3" style="background-color: darkgray; width: 300px;" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                            <ContentTemplate>
                        <div id="DIV2" runat="server" style="overflow: auto; width: 300px;
                            position: static; border-right: darkgray 1px outset; border-top: darkgray 1px outset; border-left: darkgray 1px outset; border-bottom: darkgray 1px outset; height: 160px;">
                            <asp:GridView ID="FlexTI" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                Font-Names="Arial" Font-Size="8pt" PageSize="5" Width="300px" Height="1px" BorderColor="DarkGray" BorderStyle="Outset" BorderWidth="1px">
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="AceptarTI" Text="Aceptar">
                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="Usuario" HeaderText="Usuario">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NOMBRESP" HeaderText="Nombre del Usuario">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3" style="height: 25px; background-color: darkgray; width: 300px;" valign="top">
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server">
            <div style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 350px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;" id="TABLE4" runat="server">
                    <tr>
                        <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; height: 25px; background-color: darkgray;
                            text-align: center" valign="top">
                        <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Relación de Grupos"></asp:Label></td>
                        <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; height: 22px; background-color: darkgray"
                            valign="top">
                        <asp:Button ID="btnCerrarTG" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/>
                        <asp:Button ID="btnListarTG" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/></td>
                        <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; height: 22px; background-color: darkgray"
                            valign="top">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                <div id="DIV3" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                                    overflow: auto; border-left: darkgray 1px outset; width: 300px; border-bottom: darkgray 1px outset;
                                    position: static; height: 160px">
                                    <asp:GridView ID="FlexTG" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        Font-Names="Arial" Font-Size="8pt" PageSize="5" Width="300px" BorderColor="DarkGray" BorderStyle="Outset" BorderWidth="1px">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="AceptarTG" Text="Aceptar">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="GRUPO_NOMBRE" HeaderText="Grupo">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GRUPO_COD">
                                                <ItemStyle Width="0px" ForeColor="DarkGray" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnListarTG" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px; height: 22px; background-color: darkgray" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                        </td>
                        <td align="left" style="vertical-align: middle; height: 25px; background-color: darkgray"
                            valign="top">
                        </td>
                        <td align="left" style="width: 25px; height: 25px; background-color: darkgray" valign="top">
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        &nbsp;</div>
    <asp:Panel ID="Panel4" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: black 1px outset;
                border-top: black 1px outset; border-left: black 1px outset; width: 530px; border-bottom: black 1px outset" id="TABLE3" runat="server">
                <tr>
                    <td align="left" rowspan="4" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 480px; height: 25px; background-color: darkgray;
                        text-align: center" valign="top">
                        <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Bandeja"></asp:Label></td>
                    <td align="left" rowspan="4" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 480px; height: 22px; background-color: darkgray;
                        text-align: left" valign="top">
                        <asp:Button ID="btnBCerrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/>
                        <asp:Button ID="btnBListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/></td>
                </tr>
                <tr>
                    <td align="left" style="width: 480px; background-color: darkgray" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 480px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 195px" id="DIV4" runat="server"><asp:GridView id="FlexB" runat="server" Width="480px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" __designer:wfdid="w2" AllowPaging="True" AutoGenerateColumns="False" PageSize="20"><Columns>
<asp:BoundField DataField="APROB_ASIGNADO_PERSONA" HeaderText="Usuario">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NomPersonal" HeaderText="Nombre del Usuario">
<ItemStyle Width="400px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Cant" HeaderText="Cant.">
<ItemStyle Width="30px" HorizontalAlign="Right" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnBListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBandeja" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 480px; height: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" CacheDynamicResults="True"
        CancelControlID="btnBCerrar" PopupControlID="Panel4" TargetControlID="btnBandeja"
        X="300" Y="100" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel5" runat="server">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;" id="TABLE2" runat="server">
                <tr>
                    <td align="left" rowspan="5" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" colspan="5" style="vertical-align: middle; height: 25px; background-color: darkgray;
                        text-align: center" valign="top">
                        <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Lista de Avisos"></asp:Label></td>
                    <td align="left" rowspan="5" style="width: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 35px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aviso"
                            Width="30px"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 160px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:DropDownList ID="cboEstUsuario" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="160px">
                        </asp:DropDownList></td>
                    <td align="left" style="vertical-align: middle; width: 40px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado"
                            Width="35px"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 160px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:DropDownList ID="cboEstAviso" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="160px">
                        </asp:DropDownList></td>
                    <td align="left" style="vertical-align: middle; width: 55px; height: 22px; background-color: darkgray;
                        text-align: right" valign="top">
                        <asp:Button ID="btnAListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="50px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/></td>
                </tr>
                <tr>
                    <td align="left" style="vertical-align: middle; width: 35px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 160px; height: 22px; background-color: darkgray"
                        valign="top">
                        <asp:DropDownList ID="cboTipoAviso" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="160px">
                        </asp:DropDownList></td>
                    <td align="left" style="vertical-align: middle; width: 40px; height: 22px; background-color: darkgray"
                        valign="top">
                    </td>
                    <td align="left" style="width: 160px; height: 22px; background-color: darkgray" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 55px; height: 22px; background-color: darkgray;
                        text-align: right" valign="top">
                        <asp:Button ID="btnACerrar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="50px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" /></td>
                </tr>
                <tr>
                    <td align="left" colspan="5" style="height: 19px; background-color: darkgray" valign="top">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                            <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 450px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 160px"><asp:GridView id="FlexA" runat="server" Width="700px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" AllowPaging="True" AutoGenerateColumns="False" PageSize="30" UseAccessibleHeader="False" Font-Overline="False"><Columns>
<asp:BoundField DataField="Aviso" HeaderText="Aviso">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_TIPO1" HeaderText="Tipo">
<ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="REGFECHA" HeaderText="Fecha">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="REGHORA" HeaderText="Hora">
<ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Width="250px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_ESTADO1" HeaderText="Estado">
<ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_TIPO">
<ItemStyle Width="0px" ForeColor="DarkGray"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_ESTADO">
<ItemStyle Width="0px" ForeColor="DarkGray"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="5" style="height: 25px; background-color: darkgray" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" CacheDynamicResults="True" PopupControlID="Panel5" TargetControlID="btnAviso"
        X="300" Y="100" CancelControlID="btnACerrar" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel6" runat="server" Height="50px" Width="125px">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: black 1px outset;
                border-top: black 1px outset; border-left: black 1px outset; width: 200px; border-bottom: black 1px outset;
                background-color: darkgray">
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 40px" valign="top">
                    </td>
                    <td align="left" colspan="2" style="height: 40px; text-align: center" valign="top">
                        <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px">Desea limpiar los datos ingresados?</asp:Label></td>
                    <td align="left" style="width: 20px; height: 40px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; text-align: right" valign="top">
                        <asp:Button ID="btnSi" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                            BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                            Text="Sí" Width="44px" /></td>
                    <td align="left" style="width: 80px" valign="top">
                        <asp:Button ID="btnNo" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                            BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                            Text="No" Width="43px" /></td>
                    <td align="left" style="width: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 20px; height: 20px" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>

