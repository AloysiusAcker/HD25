<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_DefineTablas1.aspx.vb" Inherits="AdminProblemas_DefineTablas1" title="GestorPlus" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script> 
    <div style="text-align: left">
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
<DIV style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center"> &nbsp;&nbsp;&nbsp;
    <IMG src="../Fotos/5.gif" /></DIV>
</ProgressTemplate>
                        </asp:UpdateProgress>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" 
			            PopupControlID="panelUpdateProgress" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Define Tablas</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 9px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px;" valign="top">
                </td>
                <td align="left" style="width: 150px; vertical-align: middle; height: 15px;" valign="top">
                </td>
                <td align="left" style="width: 150px; vertical-align: middle; height: 15px;" valign="top">
                </td>
                <td align="left" style="width: 90px; vertical-align: middle; height: 15px;" valign="top">
                </td>
                <td align="left" style="width: 80px; vertical-align: middle; height: 15px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 25px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="250px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w1" ActiveTabIndex="1" AutoPostBack="True"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                        Enlace
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w28"></asp:Label> </TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnENuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnENuevo_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w29" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="cmdListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w30" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 200px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; HEIGHT: 200px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w31" PageSize="8" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="ENLACE_CODIGO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ENLACE_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="URL"><ItemTemplate>
                                                                                    <div id="Abrir" runat="server" style="display: inline; font-size: 8pt; width: 240px;
                                                                                        color: gray; font-style: italic; font-family: Arial; height: 20px">
                                                                                    </div>
                                                                                
</ItemTemplate>

<ItemStyle Width="250px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField>
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoE" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtiqueta" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w32"></asp:Label> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lbl2" runat="server" Width="23px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w33">URL</asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtUrl" runat="server" Width="446px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w34"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: text-top; WIDTH: 70px; HEIGHT: 44px" vAlign=top align=left runat="server"><asp:Label id="lbl3" runat="server" Width="58px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w35">Descripción</asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 44px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtDescripcion" runat="server" Width="446px" Height="47px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w36" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR runat="server"><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodigo" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w37" Visible="False" ReadOnly="True"></asp:TextBox> </TD><TD style="WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnEGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnEGuardar_Click" runat="server" CssClass="EstiloBoton" Width="76px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w38" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnECancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnECancelar_Click" runat="server" CssClass="EstiloBoton" Width="76px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w39" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 23px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left">&nbsp;</DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
                                        Avisos
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorA" runat="server" Width="520px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w63"></asp:Label> </TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnNuevoAviso" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnNuevoAviso_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w64" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="cmdListarA" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w65" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 200px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; HEIGHT: 200px" id="DIV3"><asp:GridView id="FlexA" runat="server" Width="1000px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w52" PageSize="40" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Publicar" Text="Publicar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="AVISO_NRO" HeaderText="N&#186; Aviso">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_TIPO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_TIPO1" HeaderText="Tipo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="HORA_REGISTRO" HeaderText="Hora">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="550px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_ESTADO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AVISO_ESTADO1" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoAviso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=6 runat="server"><asp:Label id="lblEtiquetaA" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w67"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblA1" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w68" Text="Tipo Aviso"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 210px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboTipoAviso" runat="server" Width="209px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w69"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left runat="server">&nbsp;<asp:Label id="lblA2" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w70" Text="Estado"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:DropDownList id="cboEstadoAviso" runat="server" Width="205px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w71"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: text-top; WIDTH: 60px; HEIGHT: 44px" vAlign=top align=left runat="server"><asp:Label id="lblA3" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w72" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 44px" vAlign=top align=left colSpan=5 runat="server"><asp:TextBox id="txtDescripcionAviso" runat="server" Width="448px" Height="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w73" TextMode="MultiLine" MaxLength="1999"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodAviso" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w74" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 210px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; TEXT-ALIGN: right" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; TEXT-ALIGN: left" vAlign=top align=left runat="server"><asp:Button id="btnCancelarAviso" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCancelarAviso_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w75" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px" vAlign=top align=left runat="server"><asp:Button id="btnGuardarAviso" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGuardarAviso_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w76" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
                                        Publicar Aviso
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px; POSITION: static" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="lblErrorUser" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 40px; HEIGHT: 44px" vAlign=top align=left><asp:Label id="lblA4" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" Text="Aviso"></asp:Label> </TD><TD style="HEIGHT: 44px" vAlign=top align=left colSpan=2><asp:TextBox id="txtAvisoDescripcion" runat="server" Width="402px" Height="31px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine" MaxLength="1999"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 44px" vAlign=top align=left><asp:Button id="btnARegresar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnARegresar_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Regresar" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> <asp:Button id="btnPublicar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPublicar_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Publicar" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblA5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nivel"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2><asp:DropDownList id="cboANivel" runat="server" Width="116px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" OnSelectedIndexChanged="cboANivel_SelectedIndexChanged"><asp:ListItem Value="1">Nivel 1</asp:ListItem>
<asp:ListItem Value="2">Nivel 2</asp:ListItem>
<asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
</asp:DropDownList> &nbsp; <asp:CheckBox id="chkMarcartodo" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Text="Marcar Todo" OnCheckedChanged="chkMarcartodo_CheckedChanged"></asp:CheckBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtAvisoCodigo" runat="server" Width="21px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4><asp:Label id="Label1" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" Text="**Antes de pasar a la sgte. página debe de publicar los usuarios marcados"></asp:Label> </TD></TR><TR><TD style="HEIGHT: 240px" vAlign=top align=left colSpan=4><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 240px" id="DIV4" runat="server"><asp:GridView id="FlexUser" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" PageSize="40" AutoGenerateColumns="False"><Columns>
<asp:TemplateField HeaderText="Enviar"><ItemTemplate>
                                                                            <asp:CheckBox ID="chk" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                                Width="19px" />
                                                                        
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="Usuario" HeaderText="Usuario">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NomPersonal" HeaderText="Nombres">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="390px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Nivel1" HeaderText="Nivel">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Nivel">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 2px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 2px" valign="top" colspan="5">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 2px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 2px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 2px" valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 2px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

