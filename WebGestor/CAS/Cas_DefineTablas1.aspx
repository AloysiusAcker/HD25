<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_DefineTablas1.aspx.vb" Inherits="Cas_DefineTablas1" title="GestorPlus" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" lang="javascript">
    var ModalProgress = '<%= ModalProgress.ClientID %>'; 
</script> 
    <div style="text-align: left">
        <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
            <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">
                        <img src="../Fotos/5.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
		    BackgroundCssClass="modalBackground" 
		    PopupControlID="panelUpdateProgress" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 177px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Define Tablas Cas</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 9px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px;" valign="top"></td>
                <td align="left" style="width: 250px; vertical-align: middle; height: 15px;" valign="top"></td>
                <td align="left" style="width: 250px; vertical-align: middle; height: 15px;" valign="top"></td>
                <td align="left" style="width: 150px; vertical-align: middle; height: 15px;" valign="top"></td>
                <td align="left" style="width: 150px; vertical-align: middle; height: 15px;" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 15px" valign="top"></td>
                <td align="left" style="width: 25px; height: 15px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top"></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 25px" valign="top">
<%--                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>   --%> 
                            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="2" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">                         
                                <cc1:TabPanel runat="server" HeaderText="Enlace" ID="TabPanel1">
                                    <ContentTemplate>
                                        <div style="TEXT-ALIGN: left">
                                            <table style="WIDTH: 950px" cellspacing=0 cellpadding=0 border=0>
                                                <tbody>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px;" align="left" valign="top" colSpan=4>
                                                            <asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="HEIGHT: 22px" vAlign=top align=left colspan="2">
                                                            <asp:Button id="btnENuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnENuevo_Click" runat="server" 
                                                                CssClass="EstiloBoton" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" 
                                                                EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" 
                                                                Text="Nuevo"></asp:Button> 
                                                            <asp:Button ID="cmdListar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" 
                                                                BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" 
                                                                ForeColor="Gray" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" 
                                                                Text="Listar" Width="80px" />
                                                        </td>
                                                        <td style="WIDTH: 200px; HEIGHT: 22px; vertical-align: middle; text-align: right;" vAlign=top align=left></td>
                                                        <td style="VERTICAL-ALIGN: middle; WIDTH: 550px; HEIGHT: 22px; " valign=top align=left></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 200px" vAlign=top align=left colSpan=4>
                                                            <div style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; 
                                                                    WIDTH:930px; BORDER-BOTTOM: gray 1px inset; HEIGHT: 200px" id="DIV1" runat="server">
                                                                <asp:GridView id="Flex" runat="server" Width="930px" Font-Size="8pt" Font-Names="Arial" PageSize="8" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" 
                                                                                BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px">
                                                                            </ControlStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                        </asp:ButtonField>
                                                                        <asp:BoundField DataField="ENLACE_CODIGO" HeaderText="C&#243;digo">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ENLACE_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="URL">
                                                                            <ItemTemplate>
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
                                                                </asp:GridView> 
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4>
                                                            <div style="TEXT-ALIGN: left">
                                                                <table style="WIDTH: 950px" id="lblIngresoE" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False">
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" colspan="2" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                            <asp:Label ID="lblEtiqueta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" style="WIDTH: 150px; HEIGHT: 22px" valign="top"></td>
                                                                        <td runat="server" align="left" style="WIDTH: 150px; HEIGHT: 22px" valign="top"></td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="vertical-align: middle; width : 70px; height: 22px" valign="top">
                                                                            <asp:Label ID="lbl2" runat="server" Font-Names="Arial" Font-Size="8pt" Width="23px">URL</asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                                                            <asp:TextBox ID="txtUrl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="860px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: text-top; WIDTH: 70px; HEIGHT: 44px" valign="top">
                                                                            <asp:Label ID="lbl3" runat="server" Font-Names="Arial" Font-Size="8pt" Width="58px">Descripción</asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="3" style="VERTICAL-ALIGN: middle; HEIGHT: 44px" valign="top">
                                                                            <asp:TextBox ID="txtDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt" Height="47px" TextMode="MultiLine" Width="860px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="WIDTH: 70px; HEIGHT: 22px" valign="top">
                                                                            <asp:TextBox ID="txtCodigo" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Visible="False" Width="1px"></asp:TextBox>
                                                                        </td>
                                                                        <td runat="server" align="left" style="HEIGHT: 22px; width: 560px;" valign="top"></td>
                                                                        <td runat="server" align="right" colspan="2" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                            <asp:Button ID="btnECancelar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnECancelar_Click" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="76px" />
                                                                            <asp:Button ID="btnEGuardar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnEGuardar_Click" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="76px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 23px" vAlign=top align=left colSpan=4>
                                                            <div style="TEXT-ALIGN: left">&nbsp;</div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="Tema de Ayuda" ID="TabPanel2">
                                    <ContentTemplate>
                                        <div style="TEXT-ALIGN: left">
                                            <table style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0>
                                                <tbody>
                                                    <tr>
                                                        <td style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 150px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left></td>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=left colSpan=2>
                                                            <asp:Button id="btnTANuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                onclick="btnTANuevo_Click" runat="server" Width="72px" Height="19px" ForeColor="Gray" Font-Size="8pt" 
                                                                Font-Names="Arial" CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" 
                                                                BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> 
                                                            <asp:Button id="cmdListarTA" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                onclick="cmdListarTA_Click" runat="server" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" 
                                                                CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" 
                                                                BackColor="LightGray" Text="Listar"></asp:Button> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td vAlign=top align=left colSpan=5>
                                                            <div style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 530px; 
                                                                    BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 295px" id="DIV2" runat="server">
                                                                <asp:GridView id="FlexTA" runat="server" Width="930px" Font-Size="8pt" Font-Names="Arial" OnSelectedIndexChanged="FlexTA_SelectedIndexChanged" PageSize="8" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="38px"></ControlStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px"></ItemStyle>
                                                                        </asp:ButtonField>
                                                                        <asp:BoundField DataField="CLASSE" HeaderText="Clasificaci&#243;n">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Nombre del Documento">                                                                                
                                                                            <ItemTemplate>
                                                                                <div id="Doc" runat="server" style="width: 150px; height: 22px"></div>                                                                        
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="TEMA_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Persona" HeaderText="Nombre de Creaci&#243;n ">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FECHA_INGRESO" HeaderText="F. Ingreso">
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TEMA_CODIGO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TEMA_TIPO_DOC">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TEMA_USUARIO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="TEMA_CLASIFICACION">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField>
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                </asp:GridView> 
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=5>
                                                            <asp:Label id="lblErrorTA" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 19px" vAlign=top align=left colSpan=5>
                                                            <div style="TEXT-ALIGN: left">
                                                                <div style="TEXT-ALIGN: left">
                                                                    <table style="WIDTH: 530px; POSITION: static" id="lblIngresoTA" cellSpacing=0 cellPadding=0 
                                                                        border=0 runat="server" Visible="False">
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2 runat="server">
                                                                                <asp:Label id="lblEtiquetaTA" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server"></td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:DropDownList id="cboTipo" runat="server" Width="429px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:Label id="lblTA" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Clasificación"></asp:Label>
                                                                            </td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:DropDownList id="cboClasif" runat="server" Width="429px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:Label id="lblTA3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nombre Documento"></asp:Label>
                                                                            </td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:TextBox id="txtTANombre" runat="server" Width="423px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:Label id="lblTA4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Ruta de Documento"></asp:Label>
                                                                            </td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 22px" vAlign=top align=left runat="server"></td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server"></td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 22px" vAlign=top align=left runat="server"></td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:Label id="lblTA5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label>
                                                                            </td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 22px" vAlign=top align=left runat="server">
                                                                                <asp:TextBox id="txtTADescripcion" runat="server" Width="423px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr runat="server">
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left runat="server"></td>
                                                                            <td style="VERTICAL-ALIGN: middle; WIDTH: 430px; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=left runat="server">
                                                                                <asp:Button id="btnGuardarTA" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                                    onclick="btnGuardarTA_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" 
                                                                                    CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" 
                                                                                    Text="Guardar"></asp:Button>
                                                                                <asp:Button id="btnCancelarTA" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                                    onclick="btnCancelarTA_Click" runat="server" Width="75px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" 
                                                                                    CssClass="EstiloBoton" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" 
                                                                                    BackColor="LightGray" Text="Cancelar"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="TEXT-ALIGN: left">
                                            <div style="TEXT-ALIGN: left"></div>
                                        </div>                                    
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel runat="server" HeaderText="Avisos" ID="TabPanel3">
                                    <ContentTemplate>
                                        <div style="TEXT-ALIGN: left">
                                            <table style="WIDTH: 950px" cellSpacing=0 cellPadding=0 border=0>
                                                <tbody>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4>
                                                            <asp:Label id="lblErrorA" runat="server" Width="950px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" align="left" style="vertical-align: middle; width : 100px; height: 22px" valign="top">
                                                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativo"></asp:Label>
                                                        </td>
                                                        <td runat="server" align="left" style="vertical-align: middle; width: 375px; height: 22px" valign="top">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DdlBusAplicativo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" Height="16px" AutoPostBack="True"></asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>                                                            
                                                        </td>
                                                        <td runat="server" align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Producto"></asp:Label>
                                                        </td>
                                                        <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DdlBusProducto" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" AutoPostBack="True"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="DdlBusAplicativo" EventName="SelectedIndexChanged" />
                                                                </triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" align="left" style="vertical-align: middle; width : 100px; height: 22px" valign="top">
                                                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sub-Producto"></asp:Label>
                                                        </td>
                                                        <td runat="server" align="left" style="vertical-align: middle; width: 375px; height: 22px" valign="top">
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DdlBusSubProd" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" Height="16px"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="DdlBusProducto" EventName="SelectedIndexChanged" />
                                                                </triggers>
                                                            </asp:UpdatePanel>                                                            
                                                        </td>
                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 100px" valign="top"></td>
                                                        <td runat="server" align="left" colspan="3" style="VERTICAL-ALIGN: middle; WIDTH: 375px" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: justify" vAlign=top align=left colspan="2">
                                                            <asp:Button id="btnNuevoAviso" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                onclick="btnNuevoAviso_Click" runat="server" CssClass="EstiloBoton" Width="80px" ForeColor="Gray" Font-Size="8pt" 
                                                                Font-Names="Arial" Text="Nuevo" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" 
                                                                BackColor="LightGray"></asp:Button>
                                                            <asp:Button ID="cmdListarA" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" 
                                                                CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" 
                                                                OnClick="cmdListarTA_Click" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" 
                                                                Text="Listar" Width="80px" />
                                                        </td>
                                                        <td style="WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 375px; HEIGHT: 22px" vAlign=top align=left></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="HEIGHT: 200px" vAlign=top align=left colspan="4">
                                                                    <div id="div3" style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 950px; 
                                                                        BORDER-BOTTOM: gray 1px inset; HEIGHT: 200px">
                                                                        <asp:GridView id="FlexA" runat="server" Width="1460px" Font-Size="8pt" Font-Names="Arial" OnSelectedIndexChanged="FlexA_SelectedIndexChanged" 
                                                                            PageSize="40" AutoGenerateColumns="False"><Columns>
                                                                            <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                                                            <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                                                                                                                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="80px"></ControlStyle>

                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                                                            </asp:ButtonField>
                                                                            <asp:ButtonField CommandName="Publicar" Text="Publicar" ButtonType="Button">
                                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="80px"></ControlStyle>
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                                                            </asp:ButtonField>
                                                                            <asp:BoundField DataField="AVISO_NRO" HeaderText="N&#186; Aviso">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_TIPO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_TIPO1" HeaderText="Tipo">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="FECHA_REGISTRO" HeaderText="Fecha">
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="HORA_REGISTRO" HeaderText="Hora">
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_APLICATIVO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="APLICATIVO" HeaderText="Aplicactivo">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="550px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_PRODUCTO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="550px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_SUBPRODUCTO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="SUBPRODUCTO" HeaderText="Sub-Producto">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="550px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_ESTADO">
                                                                            <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_ESTADO1" HeaderText="Estado">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="AVISO_DETALLE" HeaderText="Detalle">
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="550px"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            </Columns>

                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4>
                                                            <div style="TEXT-ALIGN: left">
                                                                <table style="WIDTH: 950px; margin-right: 0px;" id="lblIngresoAviso" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False">
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" colspan="6" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                            <asp:Label ID="lblEtiquetaA" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; TEXT-ALIGN: left" valign="top" colspan="6">
                                                                            <asp:Button ID="btnCancelarAviso" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" 
                                                                                CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnCancelarAviso_Click" 
                                                                                onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="72px" />
                                                                            <asp:Button ID="btnGuardarAviso" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" 
                                                                                CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnGuardarAviso_Click" 
                                                                                onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="72px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="vertical-align: middle; width : 100px; height: 22px" valign="top">
                                                                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativo"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" style="vertical-align: middle; width: 375px; height: 22px" valign="top">
                                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="DdlAplicativo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" Height="18px"  AutoPostBack="True"></asp:DropDownList>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>                                                                            
                                                                        </td>
                                                                        <td runat="server" align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                                                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Producto"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="DdlProducto" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" AutoPostBack="True"></asp:DropDownList>
                                                                                </ContentTemplate>
                                                                                <triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="DdlAplicativo" EventName="SelectedIndexChanged" />
                                                                                </triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="vertical-align: middle; width : 100px; height: 22px" valign="top">
                                                                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sub-Producto"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" style="vertical-align: middle; width: 375px; height: 22px" valign="top">
                                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:DropDownList ID="DdlSubProd" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" AutoPostBack="True"></asp:DropDownList>
                                                                                </ContentTemplate>
                                                                                <triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="DdlProducto" EventName="SelectedIndexChanged" />
                                                                                </triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 100px" valign="top"></td>
                                                                        <td runat="server" align="left" colspan="3" style="VERTICAL-ALIGN: middle; WIDTH: 375px" valign="top"></td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="vertical-align: middle; width : 100px; height: 22px" valign="top">
                                                                            <asp:Label ID="lblA1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Aviso"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" style="vertical-align: middle; width: 375px; height: 22px" valign="top">
                                                                            <asp:DropDownList ID="cboTipoAviso" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px" Height="16px"></asp:DropDownList>
                                                                        </td>
                                                                        <td runat="server" align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                                                                            <asp:Label ID="lblA2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                                                            <asp:DropDownList ID="cboEstadoAviso" runat="server" Font-Names="Arial" Font-Size="8pt" Width="375px"></asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: text-top; WIDTH: 100px; HEIGHT: 22px" valign="top">
                                                                            <asp:Label ID="lblA3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="5" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                            <asp:TextBox ID="txtDescripcionAviso" runat="server" Font-Names="Arial" Font-Size="8pt" Height="20px" 
                                                                                MaxLength="100" Width="859px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: text-top; WIDTH: 100px; HEIGHT: 44px" valign="top">
                                                                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Detalle"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="5" style="VERTICAL-ALIGN: middle; HEIGHT: 44px" valign="top">
                                                                            <asp:TextBox ID="TxtDetalleAviso" runat="server" Font-Names="Arial" Font-Size="8pt" Height="40px" MaxLength="4999" TextMode="MultiLine" Width="861px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: text-top; WIDTH: 100px; HEIGHT: 22px" valign="top">
                                                                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Adjuntar"></asp:Label>
                                                                        </td>
                                                                        <td runat="server" align="left" colspan="4" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="10px" Width="441px" />
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="BtnArchivo" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 100px; text-align: right;" valign="top">
                                                                            <asp:Button ID="BtnArchivo" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" 
                                                                                        CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'" 
                                                                                        onmouseover="this.style.fontWeight='bolder'" Text="Adjuntar" Width="72px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: text-top; HEIGHT: 22px" valign="top" colspan="6">
                                                                            <div id="div4" style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 950px; 
                                                                                BORDER-BOTTOM: gray 1px inset;">
                                                                                <asp:GridView ID="GvArchivo" runat="server" AutoGenerateColumns="False" Width="942px" Font-Size="8pt" Font-Names="Arial"><Columns>
                                                                                    <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                                                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="78px"></ControlStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                                                                    </asp:ButtonField>                                                                                    
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <div id="Doc" runat="server" style="width: 50px; height: 22px"></div>                                    
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="ARCHIVO" HeaderText="Archivo">
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="770px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="NRO_AVISO" HeaderText="Nro. Aviso" >
                                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="100px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="AVISOARCH_CODIGO" HeaderText="Codigo" >
                                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50px"></ItemStyle>
                                                                                    </asp:BoundField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server">
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 100px" valign="top">
                                                                            <asp:TextBox ID="txtCodAviso" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="24px"></asp:TextBox>
                                                                        </td>
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 375px" valign="top"></td>
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 100px" valign="top"></td>
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 200px; TEXT-ALIGN: right" valign="top"></td>
                                                                        <td runat="server" align="left" style="VERTICAL-ALIGN: middle; TEXT-ALIGN: left" valign="top" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>                                
                                <cc1:TabPanel runat="server" HeaderText="Publicar Aviso" ID="TabPanel4">
                                    <ContentTemplate>
                                        <div style="TEXT-ALIGN: left">
                                            <table style="WIDTH: 530px; POSITION: static" cellSpacing=0 cellPadding=0 border=0>
                                                <tbody>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4>
                                                            <asp:Label id="lblErrorUser" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: top; WIDTH: 40px; HEIGHT: 44px" vAlign=top align=left>
                                                            <asp:Label id="lblA4" runat="server" Width="30px" Font-Size="8pt" Font-Names="Arial" Text="Aviso"></asp:Label>
                                                        </td>
                                                        <td style="HEIGHT: 44px" vAlign=top align=left colSpan=2>
                                                            <asp:TextBox id="txtAvisoDescripcion" runat="server" Width="402px" Height="31px" Font-Size="8pt" Font-Names="Arial" 
                                                                TextMode="MultiLine" MaxLength="1999"></asp:TextBox>
                                                        </td>
                                                        <td style="VERTICAL-ALIGN: top; WIDTH: 80px; HEIGHT: 44px" vAlign=top align=left>
                                                            <asp:Button id="btnARegresar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                onclick="btnARegresar_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" 
                                                                Font-Names="Arial" Text="Regresar" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" 
                                                                BackColor="LightGray"></asp:Button>
                                                            <asp:Button id="btnPublicar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                                onclick="btnPublicar_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" 
                                                                Font-Names="Arial" Text="Publicar" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" 
                                                                BackColor="LightGray"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; WIDTH: 40px; HEIGHT: 22px" vAlign=top align=left>
                                                            <asp:Label id="lblA5" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Nivel"></asp:Label> 
                                                        </td>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2>
                                                            <asp:DropDownList id="cboANivel" runat="server" Width="116px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" 
                                                                OnSelectedIndexChanged="cboANivel_SelectedIndexChanged">
                                                                <asp:ListItem Value="1">Nivel 1</asp:ListItem>
                                                                <asp:ListItem Value="2">Nivel 2</asp:ListItem>
                                                                <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                                                </asp:DropDownList>
                                                            <asp:CheckBox id="chkMarcartodo" runat="server" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" 
                                                                Text="Marcar Todo" OnCheckedChanged="chkMarcartodo_CheckedChanged"></asp:CheckBox>
                                                        </td>
                                                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left>
                                                            <asp:TextBox id="txtAvisoCodigo" runat="server" Width="21px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4>
                                                            <asp:Label id="Label1" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" 
                                                                Text="**Antes de pasar a la sgte. página debe de publicar los usuarios marcados"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="HEIGHT: 240px" vAlign=top align=left colSpan=4>
                                                            <div style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; 
                                                                WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; POSITION: static; HEIGHT: 240px" id="DIV4" runat="server">
                                                                <asp:GridView id="FlexUser" runat="server" Width="520px" Font-Size="8pt" Font-Names="Arial" PageSize="40" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Enviar">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chk" runat="server" Font-Names="Arial" Font-Size="8pt" Width ="19px" />                                                                        
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
                                                                </asp:GridView>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>                                    
                                    </ContentTemplate> 
                                </cc1:TabPanel>   
                            </cc1:TabContainer> 
<%--                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 2px" valign="top"></td>
                <td align="left" style="vertical-align: middle; height: 2px" valign="top" colspan="5"></td>
                <td align="left" style="width: 25px; height: 2px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 2px" valign="top"></td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 2px" valign="top"></td>
                <td align="left" style="width: 25px; height: 2px" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

