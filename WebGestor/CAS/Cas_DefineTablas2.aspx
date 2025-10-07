<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_DefineTablas2.aspx.vb" Inherits="Cas_DefineTablas2" title="GestorPlus" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="width: 550px; height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 183px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Define Tablas Cas</div>
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
                            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                            <%--<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="400px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" ActiveTabIndex="1">--%>
                                <cc1:TabPanel runat="server" HeaderText="Empresa" ID="TabPanel1">
<%--                                    <HeaderTemplate>
                                        Empresa                                    
                                    </HeaderTemplate>--%>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" Width="519px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnENuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnENuevo_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarE" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV1" runat="server"><asp:GridView id="FlexE" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" PageSize="8" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="TBCAS_EMPRESA_CODIGO" HeaderText="Codigo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TBCAS_EMPRESA_NOMBRE" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="430px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoE" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtiquetaE" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblE1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nombre"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 470px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtENombre" runat="server" Width="466px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodEmpresa" runat="server" Width="20px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 470px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnEGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnEGuardar_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> <asp:Button id="btnECancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnECancelar_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                                                        Oficina
                                    
                                </HeaderTemplate>
                                <ContentTemplate>
                                <DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorO" runat="server" Width="522px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnONuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnONuevo_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarO" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListarO_Click" runat="server" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexO" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False"><Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="TBCAS_OFICINA_CODIGO_INTERNO" HeaderText="Cod. Int.">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TBCAS_OFICINA_NOMBRE" HeaderText="Descripci&#243;n">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPRESA" HeaderText="Empresa">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TBCAS_OFICINA_CODIGO">
                                <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TBCAS_EMPRESA">
                                <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                </asp:BoundField>
                                </Columns>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoO" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 20px" vAlign=top align=left colSpan=4 runat="server"><asp:Label id="lblEtiquetaO" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblO1" runat="server" Width="61px" Font-Size="8pt" Font-Names="Arial" Text="Cód. Interno"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtOCodInt" runat="server" Width="91px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblO3" runat="server" Width="44px" Font-Size="8pt" Font-Names="Arial" Text="Empresa"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboEmpresa" runat="server" Width="298px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblO2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nombre"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtONombre" runat="server" Width="442px" Font-Size="8pt" Font-Names="Arial" MaxLength="30"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodOficina" runat="server" Width="18px" Height="12px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnOGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnOGuardar_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> <asp:Button id="btnOCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnOCancelar_Click" runat="server" Width="70px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
                                </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="TabPanel3" ID="TabPanel3"><HeaderTemplate>
                                                                        Puesto
                                    
                                </HeaderTemplate>
                                <ContentTemplate>
                                <DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorP" runat="server" Width="519px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnPNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPNuevo_Click" runat="server" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarP" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListarP_Click" runat="server" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV4" runat="server"><asp:GridView id="FlexP" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" PageSize="40" AutoGenerateColumns="False"><Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="PUESTO_CODIGO" HeaderText="C&#243;digo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PUESTO_NOMBRE" HeaderText="Descripci&#243;n">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" Width="420px"></ItemStyle>
                                </asp:BoundField>
                                </Columns>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 522px" id="lblIngresoP" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server"><asp:Label id="lblEtiquetaP" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblP1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nombre"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtPNombre" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" MaxLength="100"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodPuesto" runat="server" Width="11px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 450px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnPGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPGuardar_Click" runat="server" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> <asp:Button id="btnPCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnPCancelar_Click" runat="server" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
                                </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="TabPanel4" ID="TabPanel4"><HeaderTemplate>
                                                                        Tipo de Incidentes e Importancia
                                    
                                </HeaderTemplate>
                                <ContentTemplate>
                                <DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblErrorC" runat="server" Width="522px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnCNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCNuevo_Click" runat="server" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListarC" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListarC_Click" runat="server" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 260px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 250px" id="DIV3" runat="server"><asp:GridView id="FlexC" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="40"><Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="TIPO" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="cascri_codigo" HeaderText="Codigo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CASCRI_DESCRIPCION" HeaderText="Descripci&#243;n">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CASCRI_INICIA" HeaderText="Inicia">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="30px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CASCRI_TIPO">
                                <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                </asp:BoundField>
                                </Columns>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 520px" id="lblIngresoC" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4 runat="server"><asp:Label id="lblEtiquetaC" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblC1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboCTipo" runat="server" Width="199px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server">&nbsp;<asp:Label id="lblC2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Inicia"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:DropDownList id="cboCInicia" runat="server" Width="205px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="lblC3" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:TextBox id="txtCDescripcion" runat="server" Width="449px" Font-Size="8pt" Font-Names="Arial" MaxLength="100"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 65px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 200px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodCriterio" runat="server" Width="22px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 205px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left runat="server"><asp:Button id="btnCGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCGuardar_Click" runat="server" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar"></asp:Button> <asp:Button id="btnCCancelar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCCancelar_Click" runat="server" Width="74px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Cancelar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV>
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

