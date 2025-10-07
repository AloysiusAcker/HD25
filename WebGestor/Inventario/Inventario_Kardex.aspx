<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Kardex.aspx.vb" Inherits="Inventario_Kardex" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang ="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script> 
    
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">
                    <img alt="" src="../Fotos/5.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			BackgroundCssClass="modalBackground" 
			PopupControlID="panelUpdateProgress" />
    
    <div class="container-fluid">             
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblTitulo" runat="server" Text="KARDEX" CssClass="Titulos"></asp:Label><br/><br/>
                    </div>
                </div>
                <div class="row">       
                    <div class="col-lg-2">
                        <asp:Button ID="BtnExportar" runat="server" Text="Exportar Listado" ControlStyle-CssClass="form-control btn btn-default"  />
                    </div>    
                </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>   
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                        <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                        <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="Listar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnListar" runat="server" Text="Listar"  CssClass="form-control btn btn-default"/>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label4" runat="server" Text="Limpiar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar"  CssClass="form-control btn btn-default"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblError" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Red" CssClass="control-label-2"></asp:Label>
                    </div> 
                </div>
	            <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblEtiq1" runat="server" Text="Tipo Ubicación" CssClass="control-label-2" ></asp:Label>
                        <asp:DropDownList id="cboUbica" runat="server" CssClass="form-control" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Value="&lt; Seleccionar &gt;">&lt; Seleccionar &gt;</asp:ListItem>
                                    <asp:ListItem Value="1">Almac&#233;n</asp:ListItem>
                                    <asp:ListItem Value="2">Secci&#243;n</asp:ListItem>
                        </asp:DropDownList>
                    </div>   
                    <div class="col-md-2">
                        <asp:Label ID="lblEtiqUbicacion" runat="server" Text="Búsqueda" CssClass="control-label-2"  ></asp:Label>
                        <asp:TextBox ID="txtUbicaCodInterno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblEtiq3" runat="server" Text="Buscar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="btnBusUbicacion" runat="server" Text="..." CssClass="form-control btn btn-default" Enabled="false" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblEtiq4" runat="server" Text="NombreArt" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:TextBox ID="txtUbicaDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                </div>
                 <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblEtiq5" runat="server" Text="Artículos" CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="txtArtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblEtiq6" runat="server" Text="Buscar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="btnBusArticulo" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-9">
                        <asp:Label ID="lblEtiq7" runat="server" Text="NombreArt" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:TextBox ID="txtArtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="txtUbicaCodigo" runat="server" Class="control-label" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="MOV_ARTCODIGO" HeaderText="Cod. Art.">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_ARTDESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_FECHA" HeaderText="Fecha">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_INGRESO" HeaderText="Ingreso">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_SALIDA" HeaderText="Salida">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_SALDO" HeaderText="Saldo">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_ORIGEN_DESTINO" HeaderText="Proveedor Origen/Destino">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_MOTIVO" HeaderText="Motivo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MOV_STOCK" HeaderText="STOCK">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView> 
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboUbica" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="ModalUbicacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblEtiqUbicacion2" runat="server" Text="Buscar" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBusUbicacion" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step1">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="txtBusUbicDescripcion" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnUbicListar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="txtBusUbicCodInterno" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnUbicCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUbicCerrar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                               <%--             <div class="col-lg-12">--%>
                                                <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView id="FlexUbicacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="CODIGO">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="DarkGray" Width="0px"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnUbicListar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnUbicCerrar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                   <%--         </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
     <div id="ModalArticulo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="Label1" runat="server" Text="Busqueda de Artículos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="txtPArtDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnListarArt" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="txtPArtCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnCerrarArt" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                            <%--             <div class="col-lg-12">--%>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView id="FlexArt" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Codigo">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>                                                                </asp:BoundField>
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Nombres" ReadOnly="True">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="350px"></ItemStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle BorderWidth="1px" BorderStyle="Outset"></HeaderStyle>
                                                        <PagerStyle VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></PagerStyle>
                                                    </asp:GridView> 
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnListarArt" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnCerrarArt" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                <%--         </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

<%--    <div style="text-align: left">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="WIDTH: 600px" cellSpacing=0 cellPadding=0 border=0>
                    <tbody>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 50px" vAlign=top align=left></td>
                        <td style="HEIGHT: 50px" vAlign=top align=left colSpan=7>
                            <div style="FONT-WEIGHT: bold; FONT-SIZE: 14pt; LEFT: 253px; VERTICAL-ALIGN: middle; COLOR: gray; FONT-STYLE: italic; FONT-FAMILY: 'Bell MT', Broadway, Arial, Serif; TOP: 275px; 
                                    TEXT-ALIGN: center" id="Div1" class="EstiloTitleMenu" runat="server">Movimiento de Equipos</div></td>
                        <td style="WIDTH: 24px; HEIGHT: 50px" vAlign=top align=left></td>
                    </tr>
                    <tr>
                        <td style="BACKGROUND-IMAGE: url(../Fotos/linea.JPG); HEIGHT: 11px" vAlign=top align=left colSpan=9></td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 10px" vAlign=top align=left>
                            <asp:TextBox id="txtUbicaCodigo" runat="server" Width="1px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 10px" vAlign=top align=left></td>
                        <td style="WIDTH: 24px; HEIGHT: 10px" vAlign=top align=left></td>
                    </tr>
                    <tr>
                        <td style="WIDTH: 25px" vAlign=top align=left></td>
                            <td style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=7>
                                <asp:Label id="lblError" runat="server" Width="544px" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label></td>
                            <td style="WIDTH: 24px" vAlign=top align=left></td></tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:Label id="lblEtiq1" runat="server" Width="56px" Font-Size="8pt" Font-Names="Arial" Text="Movimiento"></asp:Label></td>
                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2>
                            <asp:DropDownList id="cboUbica" runat="server" Width="108px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="&lt; Seleccionar &gt;">&lt; Seleccionar &gt;</asp:ListItem>
                                        <asp:ListItem Value="1">Almac&#233;n</asp:ListItem>
                                        <asp:ListItem Value="2">Secci&#243;n</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:TextBox id="txtUbicaCodInterno" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:Button id="btnBusUbicacion" runat="server" CssClass="EstiloBoton_Ac" Width="25px" Text="..." Enabled="False"></asp:Button></td>
                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2>
                            <asp:TextBox id="txtUbicaDescripcion" runat="server" Width="260px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></td>
                        <td style="WIDTH: 24px; HEIGHT: 22px" vAlign=top align=left></td></tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:Label id="lblEtiq2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Artículo"></asp:Label></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:TextBox id="txtArtCodigo" runat="server" Width="75px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:Button id="btnBusArticulo" runat="server" CssClass="EstiloBoton_Ac" Width="25px" Text="..."></asp:Button></td>
                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=4>
                            <asp:TextBox id="txtArtDescripcion" runat="server" Width="371px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></td>
                        <td style="WIDTH: 24px; HEIGHT: 22px" vAlign=top align=left></td></tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:Label id="lblEtiq3" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False" Text="Fecha"></asp:Label></td>
                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2>
                            <asp:TextBox id="txtFechaIni" runat="server" Width="101px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></td>
                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=2>
                            <asp:TextBox id="txtFechaFin" runat="server" Width="102px" Visible="False"></asp:TextBox></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left>            
                            <asp:Button id="btnLimpiar" onclick="btnLimpiar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="95px" Text="Limpiar"></asp:Button></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left>
                            <asp:Button id="btnListar" runat="server" CssClass="EstiloBoton_Ac" Width="95px" Text="Listar"></asp:Button></td>
                        <td style="WIDTH: 24px; HEIGHT: 22px" vAlign=top align=left></td></tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=7>
                            <asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon"></asp:Label></td>
                        <td style="WIDTH: 24px; HEIGHT: 22px" vAlign=top align=left></td></tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 300px" vAlign=top align=left></td>
                        <td style="HEIGHT: 300px" vAlign=top align=left colSpan=7>
                            <div style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 545px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 300px">
                                <asp:GridView id="Flex" runat="server" Width="990px" Font-Size="8pt" Font-Names="Arial" Font-Overline="False" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="MOV_ARTCODIGO" HeaderText="Cod. Art.">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_ARTDESCRIPCION" HeaderText="Descripci&#243;n">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_FECHA" HeaderText="Fecha">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_INGRESO" HeaderText="Ingreso">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_SALIDA" HeaderText="Salida">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_SALDO" HeaderText="Saldo">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_ORIGEN_DESTINO" HeaderText="Proveedor Origen/Destino">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MOV_MOTIVO" HeaderText="Motivo">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView> 
                            </div>
                        </td>
                        <td style="WIDTH: 24px; HEIGHT: 300px" vAlign=top align=left></td></tr>
                    <tr>
                        <td style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 170px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px" vAlign=top align=left></td>
                        <td style="WIDTH: 24px; HEIGHT: 22px" vAlign=top align=left></td></tr>

                  </tbody>

                </table>
    <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" Y="200" X="200" PopupControlID="Panel1" CancelControlID="btnUbicCerrar" CacheDynamicResults="True" 
        BackgroundCssClass="modalBackground" TargetControlID="btnBusUbicacion">
    </cc1:ModalPopupExtender> 
    <asp:Panel id="Panel1" runat="server">
    <table style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 400px; BORDER-BOTTOM: gray 1px outset; 
    BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD>
    <TD style="HEIGHT: 20px; TEXT-ALIGN: center" vAlign=top align=left colSpan=3>
    <asp:Label id="lblEtiqUbicacion" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" Text="Relación de Oficina"></asp:Label> </TD>
    <TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 21px" vAlign=top align=left></TD>
    <TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 21px" vAlign=top align=left>
    <asp:Label id="lblEtiq4" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD>
    <TD style="WIDTH: 210px; HEIGHT: 21px" vAlign=top align=left>
    <asp:TextBox id="txtBusUbicCodInterno" runat="server" Width="180px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD>
    <TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 21px; TEXT-ALIGN: right" vAlign=top align=left>
    <asp:Button id="btnUbicCerrar" onclick="btnUbicCerrar_Click" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Cerrar"></asp:Button> </TD>
    <TD style="WIDTH: 20px; HEIGHT: 21px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD>
    <TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 20px" vAlign=top align=left>
    <asp:Label id="lblEtiq5" runat="server" Width="64px" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD>
    <TD style="WIDTH: 210px; HEIGHT: 20px" vAlign=top align=left>
    <asp:TextBox id="txtBusUbicDescripcion" runat="server" Width="180px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD>
    <TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 20px; TEXT-ALIGN: right" vAlign=top align=left>
    <asp:Button id="btnUbicListar" runat="server" CssClass="EstiloBoton_Ac" Width="70px" Text="Listar"></asp:Button> </TD>
    <TD style="WIDTH: 20px; HEIGHT: 20px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 266px" vAlign=top align=left></TD>
    <TD style="HEIGHT: 266px" vAlign=top align=left colSpan=3>
    <DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 360px; BORDER-BOTTOM: 
    gray 1px outset; HEIGHT: 240px" id="DIV2" runat="server">
    <asp:GridView id="FlexUbicacion" runat="server" Width="360px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" OnRowCommand="FlexUbicacion_RowCommand"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" Width="30px"></ControlStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CODIGO">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="DarkGray" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD><TD style="WIDTH: 20px; HEIGHT: 266px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 210px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 19px" vAlign=top align=left></TD><TD style="WIDTH: 20px; HEIGHT: 19px" vAlign=top align=left></TD></TR></TBODY></table></asp:Panel><cc1:ModalPopupExtender id="ModalPopupExtender2" runat="server" Y="200" X="200" PopupControlID="Panel2" CancelControlID="btnCerrarArt" CacheDynamicResults="True" BackgroundCssClass="modalBackground" TargetControlID="btnBusArticulo"></cc1:ModalPopupExtender> <asp:Panel id="Panel2" runat="server"><TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; BORDER-LEFT: gray 1px outset; WIDTH: 400px; BORDER-BOTTOM: gray 1px outset" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 25px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: center" vAlign=top align=left colSpan=5><asp:Label id="lblP3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" ForeColor="Maroon" Text="Búsqueda de Artículos"></asp:Label>&nbsp;</TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Label id="lblP2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 180px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:TextBox id="txtPArtCodigo" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: center" vAlign=top align=left><DIV style="VERTICAL-ALIGN: middle; TEXT-ALIGN: right"><asp:Button id="btnCerrarArt" onclick="btnCerrarArt_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Cerrar"></asp:Button>&nbsp;&nbsp;</DIV></TD><TD style="WIDTH: 25px; HEIGHT: 18px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:Label id="lblP1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 180px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left><asp:TextBox id="txtPArtDescripcion" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> </TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray; TEXT-ALIGN: right" vAlign=top align=left><asp:Button id="btnListarArt" onclick="btnListarArt_Click" runat="server" CssClass="EstiloBoton_Ac" Width="80px" Font-Size="8pt" Font-Names="Arial" Text="Listar"></asp:Button>&nbsp;&nbsp;</TD><TD style="WIDTH: 25px; HEIGHT: 19px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 250px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: baseline; HEIGHT: 250px; BACKGROUND-COLOR: darkgray" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 350px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV3" runat="server"><asp:GridView id="FlexArt" runat="server" Width="430px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" BorderStyle="Outset" BorderWidth="1px" UseAccessibleHeader="False" AllowPaging="True"><Columns>
<asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
<ControlStyle CssClass="EstiloBoton_Ac" ForeColor="Gray" Width="30px"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Codigo">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Nombres" ReadOnly="True">
<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="350px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle BorderWidth="1px" BorderStyle="Outset"></HeaderStyle>

<PagerStyle VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></PagerStyle>
</asp:GridView> </DIV></TD><TD style="WIDTH: 25px; HEIGHT: 250px; BACKGROUND-COLOR: darkgray" vAlign=top align=left></TD></TR><TR><TD style="HEIGHT: 20px; BACKGROUND-COLOR: darkgray" vAlign=top align=left colSpan=5></TD></TR></TBODY></TABLE></asp:Panel> 
</ContentTemplate>
            <Triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="cboUbica" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnUbicListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnBusUbicacion" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtUbicaCodInterno" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="txtArtCodigo" EventName="TextChanged"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>
        </asp:UpdatePanel>
        </div>--%>
</asp:Content>

