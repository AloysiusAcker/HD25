<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_DefineTablas2.aspx.vb" Inherits="AdminProblemas_DefineTablas2" title="GestorPlus" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>

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
                <td align="left" style="width: 550px; height: 50px; text-align: center" valign="top">
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
                <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/lineaCas.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 87px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 87px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="400px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" ActiveTabIndex="2"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                        Empresa
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" Width="519px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w87"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnENuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnENuevo_Click" runat="server" CssClass="EstiloBoton" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo" __designer:wfdid="w88"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarE" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar" __designer:wfdid="w89"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV1" runat="server"><asp:GridView id="FlexE" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="8" __designer:wfdid="w80"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="AEMP_CODIGO" HeaderText="Codigo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AEMP_NOMBRE" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="430px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoE" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtiquetaE" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w91"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblE1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nombre" __designer:wfdid="w92"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 470px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtENombre" runat="server" Width="466px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w93"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodEmpresa" runat="server" Width="20px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w94"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 470px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnEGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnEGuardar_Click" runat="server" CssClass="EstiloBoton" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar" __designer:wfdid="w95"></asp:Button> <asp:Button id="btnECancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnECancelar_Click" runat="server" CssClass="EstiloBoton" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar" __designer:wfdid="w96"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                        Oficina
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorO" runat="server" Width="522px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w35"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnONuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnONuevo_Click" runat="server" CssClass="EstiloBoton" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w36" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarO" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListarO_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w37" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexO" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w38" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="AOFICINA_CODIGO_INTERNO" HeaderText="Cod. Int.">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AOFICINA_NOMBRE" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="EMPRESA" HeaderText="Empresa">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AOFICINA_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AOFICINA_EMPRESA">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoO" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 20px" vAlign=top align=left colSpan=4 runat="server"><asp:Label id="lblEtiquetaO" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w39"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblO1" runat="server" Width="61px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w40" Text="Cód. Interno"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtOCodInt" runat="server" Width="91px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w41"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblO3" runat="server" Width="44px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w42" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboEmpresa" runat="server" Width="298px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w43"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblO2" runat="server" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w44" Text="Nombre"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtONombre" runat="server" Width="442px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w45" MaxLength="30"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodOficina" runat="server" Width="18px" Height="12px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w46" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnOGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnOGuardar_Click" runat="server" CssClass="EstiloBoton" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w47" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> <asp:Button id="btnOCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnOCancelar_Click" runat="server" CssClass="EstiloBoton" Width="70px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w48" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
                                        Puesto
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorP" runat="server" Width="519px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w59"></asp:Label> </TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnPNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPNuevo_Click" runat="server" CssClass="EstiloBoton" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w60" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarP" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListarP_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w61" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV4" runat="server"><asp:GridView id="FlexP" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w52" AutoGenerateColumns="False" PageSize="40"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="APUESTO_CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APUESTO_CODINTERNO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APUESTO_NOMBRE" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" Width="420px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 522px" id="lblIngresoP" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtiquetaP" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w63"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblP2" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w69" Text="Código"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 470px; HEIGHT: 22px" vAlign=top align=left runat="server"><TABLE style="WIDTH: 470px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 60px" vAlign=top align=left><asp:TextBox id="txtPCodInterno" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w70" MaxLength="4"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px" vAlign=top align=left><asp:Label id="lblP1" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w54" Text="Nombre"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 360px" vAlign=top align=left><asp:TextBox id="txtPNombre" runat="server" Width="348px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w65" MaxLength="100"></asp:TextBox></TD></TR></TBODY></TABLE></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodPuesto" runat="server" Width="11px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w66" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 470px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnPGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPGuardar_Click" runat="server" CssClass="EstiloBoton" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w67" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> <asp:Button id="btnPCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPCancelar_Click" runat="server" CssClass="EstiloBoton" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w68" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button>&nbsp;&nbsp;</TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
                                        Tipo de Incidentes e Importancia
                                    
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorC" runat="server" Width="522px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w35"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnCNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCNuevo_Click" runat="server" CssClass="EstiloBoton" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo" __designer:wfdid="w36"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarC" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListarC_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar" __designer:wfdid="w37"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV3" runat="server"><asp:GridView id="FlexC" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="40" __designer:wfdid="w24"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="TIPO" HeaderText="Tipo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="admcri_codigo" HeaderText="Codigo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ADMCRI_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ADMCRI_INICIA" HeaderText="Inicia">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ADMCRI_TIPO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoC" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4 runat="server"><asp:Label id="lblEtiquetaC" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w39"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblC1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo" __designer:wfdid="w40"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboCTipo" runat="server" Width="199px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w41"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server">&nbsp;<asp:Label id="lblC2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Inicia" __designer:wfdid="w42"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboCInicia" runat="server" Width="205px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w43"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblC3" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción" __designer:wfdid="w44"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtCDescripcion" runat="server" Width="449px" Font-Size="8pt" Font-Names="Arial" MaxLength="100" __designer:wfdid="w45"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodCriterio" runat="server" Width="22px" Font-Size="8pt" Font-Names="Arial" Visible="False" __designer:wfdid="w46"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnCGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCGuardar_Click" runat="server" CssClass="EstiloBoton" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar" __designer:wfdid="w47"></asp:Button> <asp:Button id="btnCCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCCancelar_Click" runat="server" CssClass="EstiloBoton" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar" __designer:wfdid="w48"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                        <Triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 87px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress" Width="200px">
        <asp:UpdateProgress id="UpdateProg1" runat="server" DisplayAfter="0">
            <progresstemplate>
<DIV style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center"> &nbsp;<IMG src="../Fotos/5.gif" /></DIV>
</progresstemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="panelUpdateProgress" TargetControlID="panelUpdateProgress">
    </cc1:ModalPopupExtender>
</asp:Content>

