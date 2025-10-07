<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inventario_Emergente.aspx.vb" Inherits="Inventario_Inventario_Emergente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <title></title>
    <link href="../Css_WebGestor.css" rel="stylesheet" type="text/css" />
    <link href="../EstiloWebTec.css" rel="stylesheet" type="text/css" />
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
        <fieldset>
        <section id="web">
            <div class="columnEmergente">
                <asp:button ID="Cerrar" runat="server" Font-Names="Arial" Text="Cerrar" CssClass="botoncito_cerrar"></asp:button>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                        <asp:CheckBox ID="chkRegularizar" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Regularizar Descripcion del Activo" /><br>
                        <br/>
                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ></asp:Label> <br/>
                        <asp:Label ID="Label15" runat="server" CssClass="label"  Text="Tipo Destino" ></asp:Label>
                        <asp:RadioButtonList id="optUbicacion" runat="server" CssClass="text"  RepeatDirection="Horizontal" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt" >
                            <asp:ListItem Value="1">Almacén</asp:ListItem>
                            <asp:ListItem Value="2">Centro Costo</asp:ListItem>
                        </asp:RadioButtonList> <br/>
                        <asp:Label ID="Label2" runat="server"  CssClass="label" Text="Destino"></asp:Label> 
                        <asp:TextBox id="txtUbiCodigo" runat="server"  CssClass="text" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt" ></asp:TextBox>
                        <asp:Button ID="btnUbica" runat="server" CssClass="botoncito_buscar" Height="22px" Width="27px" /><br/>
                        <asp:Label ID="Label14" runat="server" CssClass="label"  Text="" ></asp:Label>
                        <asp:TextBox id="txtUbiDescripcion" runat="server"  CssClass="text" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"   ></asp:TextBox><br />
                        <asp:Label ID="Label1" runat="server" CssClass="label"  Text="Artículo:" ></asp:Label>
                        <asp:TextBox id="txtCodArt" runat="server"  CssClass="text" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt" ></asp:TextBox>
                        <asp:Button ID="btnBuscar" runat="server" CssClass="botoncito_buscar" Height="22px" Width="27px" /><br />
                        <asp:Label ID="Label13" runat="server" CssClass="label"  Text="" ></asp:Label>
                        <asp:TextBox id="txtNomArt" runat="server"  CssClass="text" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"  ></asp:TextBox><br />
                        <asp:Label ID="Label7" runat="server" CssClass="label"  Text="Serie Nro." ></asp:Label>
                        <asp:TextBox ID="txtSerie" runat="server" CssClass="text"  ></asp:TextBox><br/>
                        <asp:Label ID="Label10" runat="server" CssClass="label"  Text="Placa Nro." ></asp:Label>
                        <asp:TextBox ID="txtPlaca" runat="server"  CssClass="text" ></asp:TextBox><br/>
                        <asp:Label ID="Label16" runat="server" CssClass="label"  Text="Cod. Relacionado" ></asp:Label>
                        <asp:TextBox ID="txtCodRelacionado" runat="server"  CssClass="text" ></asp:TextBox><br/>
                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Responsable" ></asp:Label>
                        <asp:DropDownList ID="DdlResponsable" runat="server"  CssClass="EstiloDropDownList" ></asp:DropDownList><br/>
                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Estado Equipo" ></asp:Label>
                        <asp:DropDownList ID="DdlEstado" runat="server" CssClass="EstiloDropDownList" ></asp:DropDownList><br/>
                        <asp:Label ID="Label12" runat="server" CssClass="labelEmergente" Text="Ubicacion" ></asp:Label>
                        <asp:DropDownList ID="DdlUbicacion" runat="server" CssClass="EstiloDropDownList" ></asp:DropDownList><br/>
                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Observación / Responsable" ></asp:Label>
                        <asp:TextBox ID="txtObs" runat="server" CssClass="text" ></asp:TextBox><br/>
                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Fecha en Custodía" ></asp:Label>
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="text" ></asp:TextBox><br/>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha" PopupButtonID="txtFecha" ></cc1:CalendarExtender>
                        <asp:TextBox ID="txtUbicacion" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="70px"></asp:TextBox>
                        <asp:TextBox ID="txtCodArtAnt" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="70px"></asp:TextBox>
                        <br />
                        <asp:button ID="Guardar" runat="server" Font-Names="Arial" Text="Ingresar Equipo" CssClass="botoncito_cerrar"></asp:button>
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
                        
                            <cc1:ModalPopupExtender 
                                ID="ModalPopupExtender3" 
                                TargetControlID="btnBuscar"
                                runat="server" 
                                PopupControlID="Panel2" 
                                BackgroundCssClass="modalBackground"
                                CancelControlID="BtnCerrarArt" 
                                DropShadow="True">
                            </cc1:ModalPopupExtender>
                        
                            <cc1:ModalPopupExtender 
                                ID="ModalPopupExtender2" 
                                TargetControlID="BtnNuevoart"
                                runat="server" 
                                PopupControlID="Panel3" 
                                BackgroundCssClass="modalBackground"
                                CancelControlID="BtnCerrarnArt" 
                                DropShadow="True">
                            </cc1:ModalPopupExtender>

                        <div id="Modal" class="dere_modal_2" > 
                            <asp:Panel ID="Panel1" runat="server" CssClass= "ventana_modal">
                                <div  class="EstiloTitleMenu" id="Div1" runat="server">
                                        <asp:Label id="lblBusUbica" Text ="Lista Ubicación" runat="server" style="font-weight: bold; font-size: 14pt;  vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; text-align: center;" ></asp:Label>
                                </div>
                                <asp:Label id="Label6" runat="server" CssClass="label" Text="Código Interno"></asp:Label>
                                <asp:TextBox ID="txtNCodigo" runat="server"  class="text" Height="15px"></asp:TextBox>
                                <br/>
                                <asp:Label id="Label17" runat="server" CssClass="label" Text="Descripción"></asp:Label>
                                <asp:TextBox ID="txtNDescripcion" runat="server"  class="text" ></asp:TextBox>
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
                                            <asp:BoundField DataField="CODINTERNO" HeaderText="Cod. Interno" 
                                                SortExpression="CODINTERNO">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" 
                                                SortExpression="DESCRIPCION">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CODIGO" HeaderText="">
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
                        

                        <div id="Modal2" class="dere_modal_2" > 
                            <asp:Panel ID="Panel2" runat="server" CssClass= "ventana_modal">
                                <div style="font-weight: bold; font-size: 14pt;  vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; text-align: center;" class="EstiloTitleMenu" id="Div2" runat="server">
                                        Lista de Artículos  </div>
                                <asp:Label id="lblErrorArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red"></asp:Label>
                                <br/>
                                <asp:Label id="Label8" runat="server"  class="label"  Text="Código"></asp:Label>
                                <asp:TextBox ID="txtBusArtC" runat="server"  class="text" Height="15px"></asp:TextBox><br/>
                                <asp:Label id="Label9" runat="server"  class="label"  Text="Descripción"></asp:Label>
                                <asp:TextBox ID="txtBusArtD" runat="server"  class="text" Height="15px"></asp:TextBox>
                                <br/>
                                <asp:Button ID="BtnListaArt" runat="server" class="botoncito" Text="Listar"></asp:Button>
                                <asp:Button ID="BtnCerrarArt" runat="server" class="botoncito" Text="Cerrar"></asp:Button>
                                <asp:Button ID="BtnNuevoart" runat="server" class="botoncito" Text="Nuevo"></asp:Button>
                                <br />
                                <asp:Label id="lblRegArt" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
                                <br />
                                <div style="border: 1px outset #C0C0C0; overflow: auto; height: 200px; width: 100%;" > 
                                    <asp:GridView ID="FlexArt" runat="server" AutoGenerateColumns="False" 
                                        Font-Names="Arial" Font-Size="8pt" Width="100%">
                                        <Columns>
                                           <asp:ButtonField ButtonType="Image" CommandName="sel_detalle" ImageUrl="~/Fotos/BtnSeleccionar.png">
                                            <ControlStyle Height="20px"  />
                                            <ItemStyle HorizontalAlign="Center" />
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
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                        <SelectedRowStyle CssClass="SelectRowStyle" />
                                        <SortedDescendingHeaderStyle Wrap="True" />
                                    </asp:GridView>
                                </div>          
                                <br/>                
                            </asp:Panel>
                        </div>      

                        
                <div id="ModalArt" class="dere_modal_2" > 
                    <asp:Panel ID="Panel3" runat="server"  CssClass= "ventana_modal">
                        <div style="font-weight: bold; font-size: 14pt;  vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif; text-align: center;" class="EstiloTitleMenu" id="Div3" runat="server">
                                        Nuevo Artículo  </div>
                        <asp:Label ID="lblErrorNArt" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red" ></asp:Label><br/>
                        <asp:Label ID="lblCodArt" runat="server" CssClass="label"  Text="Artículo:" ></asp:Label>
                        <asp:TextBox id="txtNCodArt" runat="server"  CssClass="text" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt" ></asp:TextBox><br/>
                        <asp:Label ID="lblTipoArt" runat="server" CssClass="label" Text="Tipo Art." ></asp:Label>
                        <asp:DropDownList ID="DdlTipoArt" runat="server"  CssClass="EstiloDropDownList" ></asp:DropDownList><br/>
                        <asp:Label ID="lblTipoBien" runat="server" CssClass="label" Text="Tipo Bien" ></asp:Label>
                        <asp:DropDownList ID="DdlTipoBien" runat="server"  CssClass="EstiloDropDownList" ></asp:DropDownList><br/>
                        <asp:Label ID="lblClasif" runat="server" CssClass="label"  Text="Clasificacion" ></asp:Label>
                        <asp:TextBox ID="txtCodClasif" runat="server" CssClass="text"  ></asp:TextBox><br/>
                        <asp:Label ID="lblMarca" runat="server" CssClass="label"  Text="Marca" ></asp:Label>
                        <asp:DropDownList ID="DdlMarca" runat="server"  CssClass="EstiloDropDownList" AutoPostBack="True" ></asp:DropDownList><br/>
                        <asp:Label ID="lblModelo" runat="server" CssClass="label"  Text="Modelo" ></asp:Label>
                        <asp:DropDownList ID="DdlModelo" runat="server"  CssClass="EstiloDropDownList" ></asp:DropDownList><br/>
                        <asp:Label ID="lblDescrip" runat="server" CssClass="label"  Text="Descripcion" ></asp:Label>
                        <asp:TextBox id="txtNDescripcionArt" runat="server"  CssClass="text" ></asp:TextBox><br />
                        <asp:Label ID="lblAbrev" runat="server" CssClass="label"  Text="Abreviatura" ></asp:Label>
                        <asp:TextBox id="txtAbreviatura" runat="server"  CssClass="text" ></asp:TextBox><br />
                        <asp:Label ID="lblNroParte" runat="server" CssClass="label"  Text="Nro. Parte" ></asp:Label>
                        <asp:TextBox id="txtNroParte" runat="server"  CssClass="text" ></asp:TextBox><br />
                        <asp:Button ID="BtnGuardarArt" runat="server" class="botoncito" Text="Guardar"></asp:Button>
                        <asp:Button ID="BtnCerrarNArt" runat="server" class="botoncito" Text="Cerrar"></asp:Button>
                    </asp:Panel>
                </div> 

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListaArt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnListaCC" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="optUbicacion" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="FlexArt" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="gridCentroCosto" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrarArt" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrarCC" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="DdlMarca" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="BtnNuevoart" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>  
        </section>
        </fieldset>
    </form>
</body>
</html>
