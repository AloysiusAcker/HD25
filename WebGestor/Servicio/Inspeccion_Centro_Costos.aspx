<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Centro_Costos.aspx.vb" Inherits="Inspeccion_Centro_Costos" title="Servicio - Centro de Costos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>    
    <div style="text-align: left">
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
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE2" language="javascript" onclick="return TABLE2_onclick()">
        <tr>
            <td align="left" colspan="9" style="height: 50px; text-align: center" valign="top">
                <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                    left: 253px; vertical-align: middle; width: 582px; color: gray; font-style: italic;
                    font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; height: 1px; text-align: center">
                    Centro de Costos</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="9" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 19px; height: 15px" valign="top">
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
            <td align="left" style="width: 75px; height: 15px" valign="top">
            </td>
            <td align="left" style="width: 15px; height: 15px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 19px; height: 1px" valign="top">
            </td>
            <td align="left" colspan="7" style="height: 1px" valign="top">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" ActiveTabIndex="1" AutoPostBack="True"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
                                    Centro de Costos
                                
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" id="TABLE1" cellSpacing=0 cellPadding=0 border=0 runat="server"><TBODY><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left runat="server"></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 380px; HEIGHT: 22px" vAlign=top align=left runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Button id="btnNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnNuevo_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Nuevo"></asp:Button> <asp:Button id="btnListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnListar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Listar"></asp:Button> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; WIDTH: 50px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Label id="Label1" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodInterno" runat="server" Width="70px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:Button id="btnUbica" runat="server" Width="25px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="..." BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset"></asp:Button> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 380px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtDescripcion" runat="server" Width="365px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3 runat="server"><asp:Label id="lblNumCentroCostos" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 380px; HEIGHT: 22px" vAlign=top align=left runat="server"><asp:TextBox id="txtCodigoCostos" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=4 runat="server"><asp:Label id="lblErrorCosto" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 250px" vAlign=top align=left colSpan=4 runat="server"><DIV style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; OVERFLOW: auto; BORDER-LEFT: 1px outset; WIDTH: 520px; BORDER-BOTTOM: 1px outset; POSITION: static; HEIGHT: 300px" id="DIV2" runat="server"><asp:GridView id="FlexCentroCostos" runat="server" Width="1070px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnRowCommand="FlexCentroCostos_RowCommand"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="40px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px"></ItemStyle>
</asp:ButtonField>
<asp:ButtonField CommandName="Seccion" Text="Seccion" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="60px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CCOSTO_COD_INTERNO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_DESCRIPCION" HeaderText="Descripcion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_PISO" HeaderText="Piso">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_DIRECCION" HeaderText="Direcccion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_EDIFICIO" HeaderText="Edificio">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_UBICACION" HeaderText="Ubicacion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_ACTIVO" HeaderText="Activo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO_ESTAB" HeaderText="Tipo Establecimiento">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_RUC" HeaderText="Ruc">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CCOSTO_CODIGO"></asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR runat="server"><TD style="VERTICAL-ALIGN: middle; HEIGHT: 110px" vAlign=top align=left colSpan=4 runat="server"><DIV style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-BOTTOM-WIDTH: 1px; WIDTH: 530px; POSITION: static; HEIGHT: 120px; BORDER-RIGHT-WIDTH: 1px" id="lblIngresoCentroCostos" runat="server" Visible="False"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=8><asp:Label id="lblEtiqCentroCosto" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Nuevo Centro de Costo"></asp:Label> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label4" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtCodCentroCostos" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label5" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=5><asp:TextBox id="txtDescripCentroCostos" runat="server" Width="295px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Piso"></asp:Label> </TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtPisoCentroCosto" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Direccion"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=5><asp:TextBox id="txtDireccCentroCosto" runat="server" Width="295px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label8" runat="server" Width="32px" Font-Size="8pt" Font-Names="Arial" Text="Edificio"></asp:Label> </TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtEdificioCentroCosto" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label9" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Ubicacion"></asp:Label> </TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtUbicaCentroCosto" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 30px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label10" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" Text="Ruc"></asp:Label> </TD><TD style="WIDTH: 170px; HEIGHT: 22px" vAlign=middle align=left colSpan=3><asp:TextBox id="txtRucCentroCosto" runat="server" Width="163px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtCodCostos" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 70px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 30px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 170px; HEIGHT: 22px" vAlign=middle align=left colSpan=3><asp:Button id="btnCancelarCentroCostos" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCancelarCentroCostos_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Cancelar"></asp:Button> <asp:Button id="btnGrabarCentroCostos" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGrabarCentroCostos_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Grabar"></asp:Button> </TD></TR></TBODY></TABLE></DIV></TD></TR></TBODY></TABLE></DIV><cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" TargetControlID="btnUbica" CancelControlID="btnBusCerrar" PopupControlID="Panel1" CacheDynamicResults="True" BackgroundCssClass="modalBackground" X="300" Y="200" Enabled="True" DynamicServicePath=""></cc1:ModalPopupExtender> <asp:Panel id="Panel1" runat="server"><DIV style="TEXT-ALIGN: center"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 500px; BORDER-BOTTOM: gray 1px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; TEXT-ALIGN: center" vAlign=middle align=left colSpan=3><asp:Label id="lblBusCCosto" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Busqueda de Centro de Costos"></asp:Label> </TD><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="lblBusCCodigo" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusCCod" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnBusCerrar" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cerrar" BackColor="LightGray" BorderColor="Silver" BorderWidth="1px" BorderStyle="Outset"></asp:Button> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="lblBusCDescrip" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusCDescripcion" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnBusCListar" onclick="btnBusCListar_Click" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset"></asp:Button> </TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=middle align=left></TD><TD vAlign=middle align=left colSpan=3><DIV style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 450px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV3" runat="server"><asp:GridView id="FlexBusCCosto" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" Font-Overline="False"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="30px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CODIGO">
<ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD><TD style="WIDTH: 25px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 280px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD></TR></TBODY></TABLE></DIV></asp:Panel> <BR />
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
                                    Seccion
                                
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=5><asp:Label id="LblRegistroSeccion" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtCodigoCostosSecc" runat="server" Width="8px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=middle align=left colSpan=3>&nbsp;&nbsp;&nbsp;<asp:Button id="btnRegresar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnRegresar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Regresar"></asp:Button> <asp:Button id="btnNuevoSeccion" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnNuevoSeccion_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Nuevo"></asp:Button> </TD></TR><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:Label id="Label3" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Centro Costo"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:TextBox id="txtCodInternoSeccion" runat="server" Width="95px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=6><asp:TextBox id="txtDescSeccion" runat="server" Width="325px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=10><asp:Label id="LblNumSeccion" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:Label> </TD></TR><TR><TD style="HEIGHT: 250px" vAlign=middle align=left colSpan=10><DIV style="BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; OVERFLOW: auto; BORDER-LEFT: 1px outset; WIDTH: 520px; BORDER-BOTTOM: 1px outset; POSITION: static; HEIGHT: 250px" id="DIV4" runat="server"><asp:GridView id="FlexCostoSeccion" runat="server" Width="1270px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False"><Columns>
<asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="60px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_DESCRIPCION" HeaderText="Descripcion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_PISO" HeaderText="Piso">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_DIRECCION" HeaderText="Direccion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_EDIFICIO" HeaderText="Edificio">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_UBICACION" HeaderText="Ubicacion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_HALL" HeaderText="Hall">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="TIPO_ESTAB" HeaderText="Tipo Establecimiento">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_TERRITORIO" HeaderText="Territorio">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_TIPOUBICACION" HeaderText="Tipo Ubicacion">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_TELEF" HeaderText="Telefono">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_FAX" HeaderText="Fax">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_TTA" HeaderText="Tablero TTA">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_TSI" HeaderText="Tablero TSI">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_TIPO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CECOSE_RUC" HeaderText="RUC">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
</asp:BoundField>
</Columns>
</asp:GridView> </DIV></TD></TR><TR><TD style="HEIGHT: 135px" vAlign=middle align=left colSpan=10><DIV style="BORDER-TOP-WIDTH: 1px; BORDER-LEFT-WIDTH: 1px; BORDER-BOTTOM-WIDTH: 1px; WIDTH: 530px; POSITION: static; HEIGHT: 160px; BORDER-RIGHT-WIDTH: 1px" id="lblIngresoSeccion" runat="server" Visible="False"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=8><asp:Label id="lblEtiqSeccion" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Nueva Sección"></asp:Label> </TD></TR><TR><TD style="WIDTH: 60px" vAlign=middle align=left><asp:Label id="Label11" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Text="Codigo"></asp:Label> </TD><TD style="WIDTH: 100px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtCodSeccion" runat="server" Width="93px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 60px" vAlign=middle align=left><asp:Label id="Label12" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Descripcion"></asp:Label> </TD><TD vAlign=middle align=left colSpan=5><asp:TextBox id="txtDescCostosSeccion" runat="server" Width="302px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 60px" vAlign=middle align=left><asp:Label id="Label13" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" Text="Ruc"></asp:Label> </TD><TD style="WIDTH: 100px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtRucCostosSeccion" runat="server" Width="93px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 60px" vAlign=middle align=left><asp:Label id="Label14" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Establec."></asp:Label> </TD><TD vAlign=middle align=left colSpan=5><asp:DropDownList id="cboEstabSeccion" runat="server" Width="308px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label15" runat="server" Width="48px" Font-Size="8pt" Font-Names="Arial" Text="Direccion"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=7><asp:TextBox id="txtDireccSeccion" runat="server" Width="460px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label16" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Tablero Tta"></asp:Label> </TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:DropDownList id="cboTta" runat="server" Width="98px" Font-Size="8pt" Font-Names="Arial"><asp:ListItem>Si</asp:ListItem>
<asp:ListItem>No</asp:ListItem>
<asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
</asp:DropDownList> </TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label20" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tablero Tsi"></asp:Label> </TD><TD style="HEIGHT: 22px" vAlign=middle align=left colSpan=2><asp:DropDownList id="cboTsi" runat="server" Width="98px" Font-Size="8pt" Font-Names="Arial"><asp:ListItem>Si</asp:ListItem>
<asp:ListItem>No</asp:ListItem>
<asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
</asp:DropDownList> </TD><TD style="WIDTH: 90px; HEIGHT: 22px" vAlign=middle align=left colSpan=1></TD><TD style="WIDTH: 30px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Piso" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" Text="Piso"></asp:Label> </TD><TD style="WIDTH: 100px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtPisoSeccion" runat="server" Width="93px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label17" runat="server" Width="55px" Font-Size="8pt" Font-Names="Arial" Text="Edificio"></asp:Label> </TD><TD style="WIDTH: 80px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtEdificioSeccion" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 50px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label18" runat="server" Width="45px" Font-Size="8pt" Font-Names="Arial" Text="Ubicacion"></asp:Label> </TD><TD style="WIDTH: 90px; HEIGHT: 22px" vAlign=middle align=left colSpan=1><asp:TextBox id="txtUbicaSeccion" runat="server" Width="85px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="WIDTH: 30px; HEIGHT: 22px" vAlign=middle align=left><asp:Label id="Label19" runat="server" Width="24px" Font-Size="8pt" Font-Names="Arial" Text="Hall"></asp:Label> </TD><TD style="WIDTH: 60px; HEIGHT: 22px" vAlign=middle align=left><asp:TextBox id="txtHallSeccion" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 60px; HEIGHT: 19px" vAlign=middle align=left><asp:TextBox id="txtCodigoSeccion" runat="server" Width="50px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> </TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="HEIGHT: 19px" vAlign=middle align=left colSpan=3>&nbsp;&nbsp;<asp:Button id="btnCancelarSeccion" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnCancelarSeccion_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Cancelar" Font-Overline="False"></asp:Button> <asp:Button id="btnGrabarSeccion" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnGrabarSeccion_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Text="Guardar"></asp:Button> </TD></TR><TR><TD style="WIDTH: 60px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 60px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 50px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 90px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 30px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 60px; HEIGHT: 19px" vAlign=middle align=left></TD></TR></TBODY></TABLE></DIV></TD></TR><TR><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD><TD style="WIDTH: 80px" vAlign=middle align=left></TD><TD style="WIDTH: 60px" vAlign=middle align=left></TD><TD style="WIDTH: 50px" vAlign=middle align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 15px; height: 1px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 19px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
            </td>
            <td align="left" style="width: 15px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 19px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="7" style="vertical-align: middle; height: 22px" valign="top">
                &nbsp;</td>
            <td align="left" style="width: 15px; height: 22px" valign="top">
            </td>
        </tr>
    </table>
</asp:Content>

