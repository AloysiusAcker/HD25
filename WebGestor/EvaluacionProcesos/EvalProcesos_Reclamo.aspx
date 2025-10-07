

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EvalProcesos_Reclamo.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Reclamo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="DevExpress.Web.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Gestor</title>
    <meta name="viewport" content="width=device-width, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0" />
    <link href="../Css_WebGestor.css" rel="stylesheet" type="text/css" />
    <link href="../Css_IniciarSesion.css" rel="stylesheet" type="text/css" />
    <link href="../Css_Gestor/Content.css" rel="stylesheet" />
    <link href="../Css_Gestor/Layout.css" rel="stylesheet" />
    <link href="../EstiloWebTec.css" rel="stylesheet" />
    <script src="../Css_Gestor/Script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%--        <dx:ASPxPanel runat="server" ID="HeaderPanel" ClientInstanceName="headerPanel" FixedPosition="WindowTop"
            FixedPositionOverlap="true" CssClass="app-header">
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <div class="left-block">
                        <dx:ASPxMenu runat="server" ID="LeftAreaMenu" ClientInstanceName="leftAreaMenu"
                            ItemAutoWidth="false" ItemWrap="false" SeparatorWidth="0" EnableHotTrack="false"
                            Width="100%" CssClass="header-menu" SyncSelectionMode="None">
                            <ItemStyle VerticalAlign="Middle" CssClass="item" />
                            <Items>
                                <dx:MenuItem Text="" Name="ToggleLeftPanel" GroupName="LeftPanel">
                                    <ItemStyle CssClass="toggle-item vertically-aligned" CheckedStyle-CssClass="checked selected" >
                                        <CheckedStyle CssClass="checked selected"></CheckedStyle>
                                    </ItemStyle>
                                    <Image Url="../Css_Gestor/Imagenes/Menu.svg" Height="18px" Width="18px" />
                                </dx:MenuItem>
                                <dx:MenuItem Text="" Name="Back">
                                    <ItemStyle CssClass="toggle-item vertically-aligned" />
                                    <Image Url="../Css_Gestor/Imagenes/back.svg" Height="18px" Width="18px" />
                                </dx:MenuItem>
                                <dx:MenuItem Text="" ItemStyle-CssClass="image-item vertically-aligned" NavigateUrl="~/">
                                    <Image SpriteProperties-CssClass="header-logo" >
                                        <SpriteProperties CssClass="header-logo"></SpriteProperties>
                                    </Image>
                                    <ItemStyle CssClass="image-item vertically-aligned"></ItemStyle>
                                </dx:MenuItem>
                            </Items>
                            <ClientSideEvents ItemClick="onLeftMenuItemClick" />
                        </dx:ASPxMenu>
                    </div>
                    
                     <div class="menu-container">
                            <div>
                                <dx:ASPxMenu runat="server" ID="ApplicationMenu" ClientInstanceName="applicationMenu" 
                                    DataSourceID="AppMenuDataSource" ItemAutoWidth="false" EnableSubMenuScrolling="true"
                                    ShowPopOutImages="True" SeparatorWidth="0" ItemWrap="false"
                                    CssClass="header-menu application-menu" Width="100%" HorizontalAlign="Right" OnItemDataBound="ApplicationMenu_ItemDataBound">
                                    <SettingsAdaptivity Enabled="true" EnableAutoHideRootItems="true" />
                                    <ItemStyle VerticalAlign="Middle" CssClass="item" SelectedStyle-CssClass="selected" HoverStyle-CssClass="hovered" >
                                        <SelectedStyle CssClass="selected">

                                        </SelectedStyle>

                                        <HoverStyle CssClass="hovered">

                                        </HoverStyle>
                                    </ItemStyle>
                                    <ItemImage Width="22" Height="22" />
                                    <SubMenuStyle CssClass="header-sub-menu" />
                                    <AdaptiveMenuImage SpriteProperties-CssClass="adaptive-image" >
                                        <SpriteProperties CssClass="adaptive-image">

                                        </SpriteProperties>
                                    </AdaptiveMenuImage>
                                </dx:ASPxMenu>
                                <dx:ASPxSiteMapDataSource ID="AppMenuDataSource" runat="server" SiteMapFileName="~/web.sitemap" />
                            </div>
                    </div>
 
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>--%>

        <section id="web">
            <img src="../Fotos/LOGO WEBCASH-06.jpg" />
            <img src="../Fotos/lineaCas.jpg" style="text-align: left" /> 
            <div id="lblTitulo" class="title">
                <asp:Label ID="lblTitle" runat="server" Text="Reclamo" Font-Names ="Arial" Font-Size ="14px"></asp:Label>        
            </div>
                <asp:button ID="Cerrar0" runat="server" Font-Names="Arial" Text="Regresar" CssClass="botoncito_cerrar"></asp:button>
                <asp:button ID="BtnGuardar" runat="server" Font-Names="Arial" Text="Guardar" CssClass="botoncito_cerrar"></asp:button>
                <asp:button ID="BtnLimpiar" runat="server" Font-Names="Arial" Text="Limpiar" CssClass="botoncito_cerrar"></asp:button>
            <br />
            <br />

            <fieldset>
            <div class="content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate >
                        <asp:Label ID="lblError" runat="server" Text="" Font-Names ="arial" Font-Size ="8pt" ForeColor="red" ></asp:Label>
                        <br />
                              <asp:Label ID="lblEt" runat="server" Text="Nro. Reclamo" CssClass="label"></asp:Label>
                              <asp:TextBox ID="txtCodReclamo" runat="server" CssClass ="text" readonly="true" ></asp:TextBox>
                        <br />
                            <asp:Label ID="lblEt2" runat="server" Text="Fecha" CssClass="label"></asp:Label>
                            <asp:TextBox ID="txtFecha" runat="server" CssClass ="text" ></asp:TextBox>
                        <br />
                            <asp:Label ID="lblEt22" runat="server" Text="Hora" CssClass="label"></asp:Label>
                            <asp:TextBox ID="txtHora" runat="server" CssClass ="text" ></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt3" runat="server" Text="Razón Social" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtRuc"  runat="server"  CssClass ="text" ReadOnly="True" ></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt23" runat="server" Text="" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtRazonSocial" runat="server"  CssClass ="text" ReadOnly="True"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt4" runat="server" Text="Tienda" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="DdlTienda" runat="server" CssClass="EstiloDropDownList"></asp:DropDownList>
                        <br /><br />
                        <asp:Label ID="Label2" runat="server" Text="1. Identificación del consumidor reclamante (En caso el cliente sea menor de edad, se llenará los " CssClass="label_caja" forecolor="Gray"  ></asp:Label>
                        <br /><asp:Label ID="Label3" runat="server" Text="  datos de uno de los padres o representante.)" CssClass="label_caja" forecolor="Gray"  ></asp:Label>
                        <br />
                        <asp:Label ID="lblEt5" runat="server" Text="Nombres" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt6" runat="server" Text="Apellidos" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt7" runat="server" Text="Domicilio" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtDomicilio" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt8" runat="server" Text="DNI" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtDni" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt9" runat="server" Text="E-Mail  (Obligatorio)" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt10" runat="server" Text="Teléfono(Obligatorio)" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTelef" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt11" runat="server" Text="Padre o Madre (para el caso de menores de edad)" CssClass="EstiloLabel"></asp:Label>
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtPadre" runat="server" CssClass ="text"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblEt12" runat="server" Text="2. Identificación del bien contratado" Font-Names="Arial" Font-Size ="10pt" forecolor="Gray" ></asp:Label>
                        <br />
                        <asp:Label ID="lblEt13" runat="server" Text="Tipo Bien"  CssClass="label"></asp:Label>
                        <asp:DropDownList ID="DdlTipo" runat="server"  CssClass="borderCbo">
                            <asp:ListItem Value="1">Producto</asp:ListItem>
                            <asp:ListItem Value="2">Servicio</asp:ListItem>
                            <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblEt14" runat="server" Text="Monto Reclamado" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtMonto" runat="server"  CssClass ="text"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblEt15" runat="server" Text="Descripción" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server"  CssClass ="textDescripcion" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                        <br /><br />
                        <asp:Label ID="lblEt16" runat="server" Text="3. Detalle del la reclamación y pedido del consumidor" CssClass="label_caja" forecolor="Gray" ></asp:Label>
                        <asp:RadioButtonList ID="optTipo" runat="server" Font-Names="Arial" Font-Size="8pt" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Reclamo *</asp:ListItem>
                            <asp:ListItem Value="2">Queja **</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:TextBox ID="txtDetalle" runat="server"  CssClass ="textDescripcion2" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                        <br /><br />
                        <asp:Label ID="lblEt17" runat="server" Text="4. Observaciones y accones adoptadas por el proveedor" Font-Names="Arial" Font-Size ="10pt" forecolor="Gray" ></asp:Label>
                        <br />
                        <asp:TextBox ID="txtObs" runat="server"  CssClass ="textDescripcion2" MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
                        <br /><br />
                        <asp:Label ID="lblE18" runat="server" Text="5. Fecha de comunicación de la respuesta : Consignar 30 días calendario posteriores a la fecha de " CssClass="label_caja" forecolor="Gray" ></asp:Label>
                        <br />
                        <asp:Label ID="Label4" runat="server" Text="   reclamo (que corresponde a la fecha maxima de respuesta , salvo prórroga)" CssClass="label_caja" forecolor="Gray" ></asp:Label>
                        <br />
                        <asp:Label ID="lblEt19" runat="server" Text="Fecha" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtFechaRpta" runat="server" CssClass ="text" ></asp:TextBox>
                        <asp:ImageButton ID="btnI1" runat="server" FirstDayOfWeek="Wednesday" Height="15px" ImageUrl="~/Fotos/Calendario.bmp" Width="15px" />
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaRpta" Format="dd/MM/yyyy" PopupButtonID="btnI1"></cc1:CalendarExtender>
                        <br /><br />
                        <asp:Label ID="lblEt20" runat="server" Text="* Reclamo : Disconformidad relacionada a los productos o servicios"  CssClass="label_caja" forecolor="Gray" ></asp:Label>
                        <br />
                        <asp:Label ID="lblEt21" runat="server" Text="** Queja : Disconformidad no relacionada a los productos o servicios; o malestar o descontento"  CssClass="label_caja" forecolor="Gray" ></asp:Label>
                        <br />
                        <asp:Label ID="Label5" runat="server" Text="    respecto a la atención al público. "  CssClass="label_caja" forecolor="Gray" ></asp:Label>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
                
            </fieldset>
        </section>        
    </form>
</body>
</html>


