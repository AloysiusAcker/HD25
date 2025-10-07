<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ventas_Punto_Cliente.aspx.vb" Inherits="Ventas_Ventas_Punto_Cliente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <section id="web">
            <div class="colum">
                <asp:button ID="Cerrar" runat="server" Font-Names="Arial" Text="Cerrar" CssClass="botoncito_cerrar"></asp:button>
                <asp:Button ID="BtnPlacaSerie" runat="server" CssClass="botoncito_cerrar" Text="Guardar Venta" />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red" ></asp:Label> 
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Cliente" Font-Names="Arial" Font-Size="8pt" ></asp:Label><br />
                        <asp:TextBox id="txtRuc" runat="server"  BackColor="WhiteSmoke" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" Font-Size="8" Font-Names="Arial" ReadOnly="True" Height="18px" ></asp:TextBox>
                        <asp:Button ID="btnUbica" runat="server" CssClass="botoncito_buscar" Height="22px" Width="27px" />
                        <asp:TextBox id="txtRazonSocial" runat="server"  BackColor="WhiteSmoke" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" Font-Size="8" Font-Names="Arial" ReadOnly="True" Height="17px" Width="107px"  ></asp:TextBox><br />
                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Documento Tipo" ></asp:Label><br/>
                        <asp:DropDownList ID="DdlDocTipo" runat="server"  BorderColor="#CCCCCC" Font-Names="Arial" Font-Size="8pt"  Height="20" ></asp:DropDownList><br/>
                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Documento Nro." ></asp:Label><br/>
                        <asp:TextBox ID="txtDocSerie" runat="server" BackColor="WhiteSmoke" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" Font-Size="8" Font-Names="Arial" Height="18px" ></asp:TextBox>
                        <asp:TextBox ID="txtDocNro" runat="server" BackColor="WhiteSmoke" BorderWidth="1px" BorderStyle="Solid" BorderColor="#CCCCCC" Font-Size="8" Font-Names="Arial" Height="18px" ></asp:TextBox><br/>
                        <asp:TextBox ID="txtCodCliente" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="70px"></asp:TextBox>

                        <!--========================================= modal ======================================-->
    
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
                        
                            <cc1:ModalPopupExtender 
                                ID="ModalPopupExtender1" 
                                TargetControlID="btnUbica"
                                runat="server" 
                                PopupControlID="Panel1" 
                                BackgroundCssClass="modalBackground"
                                CancelControlID="btnCerrarCC" 
                                DropShadow="True">
                            </cc1:ModalPopupExtender>
                        
                        <div id="Modal" class="dere_modal_left" > 
                            <asp:Panel ID="Panel1" runat="server" CssClass= "ventana_modal">
                                <div  class="EstiloTitleMenu" id="Div1" runat="server">
                                        &nbsp;<asp:Label id="lblBusUbica" Text ="Cliente" runat="server" style="font-weight: bold; font-size: 14pt;  vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; text-align: center;" ></asp:Label>
                                </div>
                                <asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="RUC"></asp:Label>
                                <asp:TextBox ID="txtNCodigo" runat="server"  class="text" Height="15px"></asp:TextBox>
                                <br/>
                                <asp:Label id="Label17" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Razon Social"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:TextBox ID="txtNDescripcion" runat="server"  class="text" Height="15px"></asp:TextBox>
                                <br/>
                                <asp:Button ID="btnListaCC" runat="server" class="botoncito" Text="Listar"></asp:Button>
                                <asp:Button ID="btnCerrarCC" runat="server" class="botoncito" Text="Cerrar"></asp:Button>
                                <br />
                                <div style="border: 1px outset #C0C0C0; overflow: auto; height: 200px; width: 100%;" > 
                                    <asp:GridView ID="gridCentroCosto" runat="server" AutoGenerateColumns="False" 
                                        Font-Names="Arial" Font-Size="8pt" Width="100%">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" CommandName="sel_detalle" ImageUrl="~/Fotos/BtnSeleccionar.png">
                                            <ControlStyle Height="20px"  />
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="persona_ruc" HeaderText="RUC" 
                                                SortExpression="persona_ruc">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="persona_razon_social" HeaderText="Razón Social" 
                                                SortExpression="persona_razon_social">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="persona_codigo" HeaderText="">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" ForeColor="#CCCCCC" 
                                                Width="0px"/>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                        <SelectedRowStyle CssClass="SelectRowStyle" />
                                        <SortedDescendingHeaderStyle Wrap="True" />
                                    </asp:GridView>
                                </div>          
                                <br/>                
                            </asp:Panel>
                        </div>      
                        

                    </ContentTemplate>
<%--                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListaArt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnListaCC" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="optUbicacion" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="gridCentroCosto" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrarArt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrarCC" EventName="Click" />
                    </Triggers>--%>
                </asp:UpdatePanel>
                </div>  
        </section>
    </form>
</body>
</html>
