<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inventario_CatalogoArticulo.aspx.vb" Inherits="Inventario_Inventario_CatalogoArticulo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <title></title>
    <link href="../Css_WebGestor.css" rel="stylesheet" type="text/css" />
    <%--<link href="../EstiloWebTec.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        function MantenSesion() {
            var CONTROLADOR = 'refresh_session.ashx';
            var head = document.getElementsByTagName('head').item(0);
            script = document.createElement('script');
            script.src = CONTROLADOR;
            script.setAtribute('type', 'text/javascript');
            script.defer = true;
            head.appendChild(script);
        }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>
        <section id="web">
            <div class="colum">
                <asp:button ID="Cerrar" runat="server" Font-Names="Arial" Text="Cerrar" CssClass="botoncito_cerrar"></asp:button>
                                <asp:Button ID="BtnListaArt" runat="server" class="botoncito" Text="Listar" CssClass="botoncito_cerrar"></asp:Button>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                        <asp:Panel ID="panelUpdateProgress" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div class="Progress">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../Fotos/5.gif"/>
                                        <strong style="font-size:medium"><br>Por Favor Espere...</strong><strong style="font-size:small"><br>¡Cargando!</strong></div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>    

                        <cc1:ModalPopupExtender ID="ModalProgress" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="panelUpdateProgress" TargetControlID="panelUpdateProgress">
                        </cc1:ModalPopupExtender>                        

                        <div id="Modal2" class="dere_modal_left" > 
                                <div style="font-weight: bold; font-size: 14pt;  vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; text-align: center;" class="EstiloTitleMenu" id="Div2" runat="server">
                                        Lista de Artículos  </div>
                                <asp:Label id="lblErrorArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label>
                                <br/>
                                <asp:Label id="Label8" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label>
                                <asp:TextBox ID="txtBusArtC" runat="server"  class="text" Height="15px"></asp:TextBox><br/>
                                <asp:Label id="Label9" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label>
                                <asp:TextBox ID="txtBusArtD" runat="server"  class="text" Height="15px"></asp:TextBox>
                                <br/>
                                <br />
                                <asp:Label id="lblRegArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
                                <br />
                                <div style="border: 1px outset #C0C0C0; overflow: auto; height: 200px; width: 100%;" > 
                                    <asp:DataList ID="FlexArt" runat="server" RepeatColumns="3" Font-Names="Arial" Font-Size="8pt" >
                                        <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ART_CODIGO", "Ventas/imagenes/{0}.jpg") %>' />
                                        <br />
                                        Código:
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("producto_cod") %>'></asp:Label>
                                        <br />
                                        Nombre:
                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("ART_DESCRIPCION") %>'></asp:Label>
                                        <br />
                                        Precio:
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%# Eval("PRECIO_VENTA") %>'></asp:Label>
                                        <br />
                                        Precio IGV:
                                        <asp:Label ID="lblPrecioIgv" runat="server" Text='<%# Eval("PRECIO_VENTA_IGV") %>'></asp:Label>
                                        <br />
                                        Stock:
                                        <asp:Label ID="lblStock" runat="server" Text='<%# Eval("STOCK_ACTUAL") %>'></asp:Label>
                                        <br />
                                        <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" />
                                    </ItemTemplate>
                                </asp:DataList>
                                </div>          
                                <br/>      
                        </div>      

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListaArt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrarArt" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>  
        </section>
    </form>
</body>
</html>
