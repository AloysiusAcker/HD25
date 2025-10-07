<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Tablas_Especiales_Mantenimiento.aspx.vb" Inherits="Tablas_Especiales_Mantenimiento" title="CAS" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>

    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                    <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                        <ProgressTemplate>
                            <div style="position: relative; top: 30%; text-align: center;">
                                &nbsp;<img src="../Fotos/5.gif" /></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />--%>

    <div class="container">
        <h1 class="Titulos">Tablas Especiales</h1>        
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="1" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                    <cc1:TabPanel runat="server" HeaderText="Definición" ID="TabPanel3">
                        <ContentTemplate>                                        
                            <div class="row espacio">          
                                <div class="col-lg-12">           
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>             
                                </div>
                            </div>                             
                            <div class="row espacio">          
                                <div class="col-lg-2">  
                                    <asp:Button ID="BtnNuevo" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Nuevo" />                    
                                </div>
                            </div> 
                            <div id="lblTablaEspecial" runat="server" visible ="false" >
                                <div class="row espacio">           
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblEtiqueta" runat="server" CssClass="control-label-2" Text="" ForeColor="Maroon" Font-Bold="true"  ></asp:Label>
                                    </div>  
                                </div>                         
                                <div class="row espacio">       
                                    <div class="col-lg-9">  
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label-2" Text="Descripción ó Referencia de en donde va hacer utilizado"></asp:Label> 
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>   
                                    </div>       
                                    <div class="col-lg-3">  
                                        <asp:Label ID="Label7" runat="server" CssClass="control-label-2" Text="Guardar" ForeColor="White"></asp:Label> 
                                        <asp:Button ID="btnGuardar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" />                    
                                    </div>
                                </div>                
                                <div class="row espacio">       
                                    <div class="col-lg-9">  
                                        <asp:Label ID="Label5" runat="server" CssClass="control-label-2" Text="Momenclatura por el Tema que va hacer utilizado:"></asp:Label> 
                                        <asp:TextBox ID="txtPrefijo" runat="server" CssClass="form-control"></asp:TextBox>   
                                    </div>       
                                    <div class="col-lg-3">  
                                        <asp:Label ID="Label6" runat="server" CssClass="control-label-2" Text="Cancelar" ForeColor="White"></asp:Label> 
                                        <asp:Button ID="btnCancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar" />                    
                                    </div>
                                </div>
                            </div>                                    
                            <div class="row espacio">       
                                <div class="col-lg-9"> 
                                    <asp:Label ID="lblCodigo" runat="server" CssClass="control-label-2" Text="" visible="false" ></asp:Label> 
                                </div>
                            </div>
                            <div class="row espacio">
                                <div class="col-lg-12">  
                                    <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/icono/Editar_opt.png" Text="Editar">
                                                <ItemStyle Height="10px" Width="10px" />
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="Tablas" Text="Tablas" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ESP_PREFIJO" HeaderText="Prefijo">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESP_DESCRIP" HeaderText="Descripción ó Referencia de en donde va hacer utilizado"></asp:BoundField>
                                            <asp:BoundField DataField="ESP_TABLA1" HeaderText="Tabla 1">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESP_TABLA2" HeaderText="Tabla 2">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESP_TABLA3" HeaderText="Tabla 3">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ESP_CODIGO">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" Width="0px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                    </asp:GridView>                                                       
                                </div>
                            </div>
                        </ContentTemplate> 
                    </cc1:TabPanel> 
                    <cc1:TabPanel runat="server" HeaderText="Tablas" ID="TabPanel4">
                        <ContentTemplate>   
                            <div class="row espacio">          
                                <div class="col-lg-12">  
                                    <asp:Label ID="lblErrorTE" runat="server" CssClass="control-label-2" ForeColor="Red"></asp:Label>    
                                </div>
                            </div>       
                            <div class="row espacio">          
                                <div class="col-lg-12">    
                                    <asp:Label ID="Label8" runat="server" CssClass="control-label-2" Text="Elementos de la Tabla Especial" ForeColor="Maroon"></asp:Label> 
                                </div>
                            </div>               
                            <div class="row espacio">          
                                <div class="col-lg-2">                
                                    <asp:DropDownList ID="cboTabla" runat="server"  CssClass="form-control" AutoPostBack="True" ></asp:DropDownList>
                                </div>        
                                <div class="col-lg-2">  
                                    <asp:Button ID="btnNuevoTE" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />                    
                                </div>        
                                <div class="col-lg-2">  
                                    <asp:Button ID="btnRegresar" runat="server" Text="Regresa" CssClass="form-control btn btn-default" />                    
                                </div>    
                                <div class="col-lg-2">  
                                    <asp:Button ID="BtnExportar" runat="server" Text="Exportar" CssClass="form-control btn btn-default" />                    
                                </div>
                            </div>   
                            <asp:Label ID="lblTabla3" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                            <asp:Label ID="lblTabla2" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                            <asp:Label ID="lblTabla1" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                            <div id="lblIngresoTE" runat="server" visible ="False" >                                
                                <div class="row espacio">           
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblEtiquetaTE" runat="server" CssClass="control-label-2" ForeColor="Maroon" Font-Bold="True"  ></asp:Label>
                                    </div>  
                                </div>                                   
                                <div class="row espacio">           
                                    <div class="col-lg-12">                                        
                                    <asp:Label ID="Label11" runat="server" CssClass="control-label-2" Text="Nivel 1"></asp:Label>       
                                    <asp:DropDownList ID="cboNivel1" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    </div>  
                                </div>                               
                                <div class="row espacio">           
                                    <div class="col-lg-12">                                        
                                        <asp:Label ID="Label12" runat="server" CssClass="control-label-2" Text="Nivel 2"></asp:Label>       
                                        <asp:DropDownList ID="cboNivel2" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    </div>  
                                </div>             
                                <div class="row espacio">           
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblTE7" runat="server" Text="Código" CssClass="control-label-2"></asp:Label>
                                        <asp:TextBox ID="txtTECodigo" runat="server"  CssClass="form-control"></asp:TextBox>
                                    </div>                               
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblTE3" runat="server" Text="Nombre" CssClass="control-label-2"></asp:Label>
                                        <asp:TextBox ID="txtTEDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>                               
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblTE4" runat="server" Text="Días" CssClass="control-label-2"></asp:Label>
                                        <asp:DropDownList ID="cboDias" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>                               
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblTE5" runat="server" Text="Horas" CssClass="control-label-2"></asp:Label>
                                        <asp:DropDownList ID="cboHoras" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>                               
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblTE6" runat="server" Text="Minutos" CssClass="control-label-2"></asp:Label>
                                        <asp:DropDownList ID="cboMin" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>  
                                </div>   
                                <div class="row espacio">           
                                    <div class="col-lg-12">
                                        <asp:TextBox ID="txtTEDescripcionE" runat="server" CssClass="form-control" Visible="False" ></asp:TextBox>
                                    </div>  
                                </div>   
                                <div class="row espacio">           
                                    <div class="col-lg-2">
                                        <asp:Button ID="btnTEGuardar" runat="server" Text="Guardar" CssClass="form-control btn btn-default" />
                                    </div>   
                                    <div class="col-lg-2">
                                        <asp:Button ID="btnTECancelar" runat="server" Text="Cancelar" CssClass="form-control btn btn-default" />
                                    </div>  
                                </div>   
                            </div>            
                            <div class="row espacio">
                                <div class="col-lg-12">
                                    <asp:GridView ID="FlexTE" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" >
                                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                <ItemStyle Width="70px" />
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" >
                                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                <ItemStyle Width="70px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="C1" />
                                            <asp:BoundField DataField="C2" >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="C3"  >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="C4"  >
                                            </asp:BoundField>
                                            <asp:BoundField DataField="C5"  >
                                            <HeaderStyle BorderColor="White" ForeColor="White" />
                                            <ItemStyle BorderColor="White" ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="C6"  >
                                            <HeaderStyle BorderColor="White" ForeColor="White" />
                                            <ItemStyle BorderColor="White" ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="C7"  >
                                            <HeaderStyle BorderColor="White" ForeColor="White" />
                                            <ItemStyle BorderColor="White" ForeColor="White" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel> 
                </cc1:TabContainer> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div> 

</asp:Content>

