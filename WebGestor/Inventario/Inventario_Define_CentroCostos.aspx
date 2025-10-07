<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Define_CentroCostos.aspx.vb" Inherits="Inventario_Inventario_Define_CentroCostos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="container">
            <cc1:TabContainer id="Ficha" runat="server" ActiveTabIndex="1" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header"  >
                <cc1:TabPanel ID="PnCentroCostos" runat="server" HeaderText="Centro de Costos">      
                    <ContentTemplate>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="LblTitulo" runat="server" Text="Centro de Costos" CssClass="subTitulos"></asp:Label>
                            </div>
                        </div> 
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>  
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="LblCCError" runat="server" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"  ForeColor="Red"></asp:Label>
                                    </div> 
                                </div>                       
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label23" CssClass="control-label-2" runat="server" Text="Codigo Centro Costo"></asp:Label>
                                        <asp:TextBox ID="TxtBusCodInterno" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label5" CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White" ></asp:Label>
                                        <asp:Button ID="BtnListarCC" runat="server" Text="Listar" CssClass="form-control btn btn-default" />
                                    </div> 
                                    <div class="col-md-3">
                                        <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                                        <asp:Button ID="BtnNuevaCC" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />
                                    </div>
                                </div>                  
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="LblEtiq1" CssClass="control-label-2" runat="server" Text="Descripción Centro Costo"></asp:Label>
                                        <asp:TextBox ID="TxtBuscarCC" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div> 
                                </div> 
                                <div class="row" id="divNuevoCC"  runat="server" visible="false"  >
                                    <div class="col-md-12">
                                        <h4>
                                            Nuevo Centro de Costos
                                        </h4>
                                    </div>  
                                </div>  
                                <div class="row" id="divEditarCC"  runat="server" visible="false"  >
                                    <div class="col-md-12">
                                        <h4>
                                            Editar Centro de Costos
                                        </h4>
                                    </div>  
                                </div>  
                                <div id="FichaNuevo" runat="server" visible="False" >           
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label ID="LblCCE1" runat="server" Text="Codigo Interno" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtCodInterno" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Label ID="LblCCE2" runat="server" Text="Descripcion" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="LblCCE3" runat="server" Text="RUC" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtRuc" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                    </div>      
                                 
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label ID="LblCCE5" runat="server" Text="Piso" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtPiso" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="LblCCE6" runat="server" Text="Edificio" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtEdificio" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="LblCCE7" runat="server" Text="Ubicacion" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtUbicacion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="LblCCE8" runat="server" Text="Establecimiento" CssClass="control-label-2"></asp:Label>
                                            <asp:DropDownList ID="DdlEstablecimiento" runat="server" CssClass="form-control" ></asp:DropDownList>                    
                                        </div> 
                                    </div> 
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Label ID="LblCCE4" runat="server" Text="Direccion" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                    </div> 
                                    <div class="row">                            
                                        <div class="col-md-3">
                                            <asp:Label ID="Label20" runat="server" CssClass="control-label-2" Text="Departamento:" ></asp:Label>
                                            <asp:DropDownList ID="DdlDptoCC" runat="server" AutoPostBack="True" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>                        
                                        <div class="col-md-3">
                                            <asp:Label ID="Label21" runat="server" CssClass="control-label-2" Text="Provincia:" ></asp:Label>
                                            <asp:DropDownList ID="DdlProvCC" runat="server" AutoPostBack="True" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>                        
                                        <div class="col-md-3">
                                            <asp:Label ID="Label22" runat="server" CssClass="control-label-2" Text="Distrito:" ></asp:Label>
                                            <asp:DropDownList ID="DdlDistCC" runat="server" AutoPostBack="True" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>    
                                    </div> 
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>                                     
                                        <div class="col-md-3">
                                        </div> 
                                        <div class="col-md-3">
                                            <asp:Label ID="Label15" CssClass="control-label-2" runat="server" Text="Guardar" ForeColor="White" ></asp:Label>
                                            <asp:Button ID="BtnCCGuardar" runat="server" Text="Guardar" CssClass="form-control btn btn-default" Visible="False" />
                                        </div> 
                                        <div class="col-md-3">
                                            <asp:Label ID="Label16" CssClass="control-label-2" runat="server" Text="Cancelar" ForeColor="White" ></asp:Label>
                                            <asp:Button ID="BtnCCCancelar" runat="server" Text="Cancelar" CssClass="form-control btn btn-default" Visible="False" />
                                        </div> 
                                    </div> 
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Label ID="LblCodCC" runat="server" Text="" visible="false"  CssClass ="col-lg-2 control-label-2"></asp:Label>
                                        </div> 
                                    </div> 
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="LblCCRegistro"  CssClass="control-label-2" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                                    </div>
                                </div> 
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GvCC" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                                            <Columns>
                                                <asp:ButtonField CommandName="Editar" ButtonType="Image" ImageUrl="~/icono/edit.gif">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField CommandName="Eliminar" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField CommandName="Seccion" ButtonType="Image" ImageUrl="~/icono/details_opt.png">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="c0" HeaderText="# Reg." />
                                                <asp:BoundField DataField="c1" HeaderText="Cod. Interno" />
                                                <asp:BoundField DataField="c2" HeaderText="Descripcion" />
                                                <asp:BoundField DataField="c3" HeaderText="Codigo" />
                                                <asp:BoundField DataField="c4" HeaderText="Direccion" />
                                                <asp:BoundField DataField="c5" HeaderText="Piso" />
                                                <asp:BoundField DataField="c6" HeaderText="Edificio" />
                                                <asp:BoundField DataField="c7" HeaderText="Ubicacion" />
                                                <asp:BoundField DataField="c8" HeaderText="Activo" />
                                                <asp:BoundField DataField="c10" HeaderText="Tipo de Establecimiento" />
                                            </Columns>
                                        </asp:GridView>   
                                    </div> 
                                </div> 
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GvCC" EventName="RowCommand" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnListarCC" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnNuevaCC" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnCCGuardar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnCCCancelar" EventName="Click" />
                            
                                </Triggers>
                            </asp:UpdatePanel>
                            <div id="ModalMensaje" class="modal fade" role="dialog" data-backdrop="static" style="position:fixed; top:25%;"> 
                                <div class="modal-dialog modal-sm">
    		                        <div class="modal-content">
                                        <div class="modal-header" style="padding: 8px 10px; text-align:center; background-color:white;">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="Mensaje" class="col-lg-12"/>
                                            </div>
    						                <div class="col-lg-12">
                                                <asp:Button ID="BtnSi" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel  ID="PnSeccion" runat="server" HeaderText="Seccion">      
                    <ContentTemplate>
                        <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="Label17" runat="server" Text="Sección" CssClass="subTitulos"></asp:Label>
                                </div>
                            </div> 
                            <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>                                
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="LblSecError" runat="server" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"  ForeColor="Red"></asp:Label>
                                    </div> 
                                </div>   
                                <div class="form-group">  
                                    <div class="col-lg-3">
                                        <asp:Label ID="LblSecCodInterno" runat="server" Text="Codigo Interno" CssClass="control-label-2"></asp:Label>
                                        <asp:TextBox ID="TxtSecCodInterno" runat="server" CssClass="form-control"></asp:TextBox> 
                                    </div>                        
                                    <div class="col-lg-3">
                                        <asp:Label ID="LblSecDescripcion" runat="server" Text="Descripción" CssClass="control-label-2"></asp:Label>
                                        <asp:TextBox ID="TxtSecDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>       
                                    <div class="col-lg-3">
                                        <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                                        <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />
                                    </div>       
                                </div>                               
                                <div class="row" id="dvNuevaSec"  runat="server" visible="false"  >
                                    <div class="col-md-12">
                                        <h4>
                                            Nueva Sección
                                        </h4>
                                    </div>  
                                </div>  
                                <div class="row" id="dvEditarSec"  runat="server" visible="false"  >
                                    <div class="col-md-12">
                                        <h4>
                                            Editar Sección
                                        </h4>
                                    </div>  
                                </div>  
                                <div id="DivSeccion" runat="server" visible="False" >
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label2" runat="server" Text="Codigo Interno" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecCodInt" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-6">
                                            <asp:Label ID="Label3" runat="server" Text="Descripcion" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecDescrip" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label4" runat="server" Text="RUC" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecRuc" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                    </div>
                                    <div class="row">    
                                        <div class="col-lg-12">
                                            <asp:Label ID="Label6" runat="server" Text="Direccion" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                    </div>
                                    <div class="row">                            
                                        <div class="col-md-3">
                                            <asp:Label ID="LblEtiq8" runat="server" CssClass="control-label-2" Text="Departamento:" ></asp:Label>
                                            <asp:DropDownList ID="DdlDpto" runat="server" AutoPostBack="True" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>                        
                                        <div class="col-md-3">
                                            <asp:Label ID="LblEtiq9" runat="server" CssClass="control-label-2" Text="Provincia:" ></asp:Label>
                                            <asp:DropDownList ID="DdlProv" runat="server" AutoPostBack="True" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>                        
                                        <div class="col-md-3">
                                            <asp:Label ID="LblEtiq10" runat="server" CssClass="control-label-2" Text="Distrito:" ></asp:Label>
                                            <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="True" CssClass="form-control" >
                                            </asp:DropDownList>
                                        </div>    
                                    </div> 
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label7" runat="server" Text="Piso" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecPiso" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label8" runat="server" Text="Edificio" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecEdificio" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label9" runat="server" Text="Ubicacion" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecUbicacion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label11" runat="server" Text="Hall" CssClass="control-label-2"></asp:Label>
                                            <asp:TextBox ID="TxtSecHall" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">  
                                            <asp:Label ID="Label10" runat="server" Text="Responsable" CssClass="control-label-2"></asp:Label>
                                            <asp:DropDownList ID="DdlResponsable" runat="server" CssClass="form-control" ></asp:DropDownList>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label12" runat="server" Text="Tipo de Establecimiento" CssClass="control-label-2"></asp:Label>
                                            <asp:DropDownList ID="DdlSecEstablecimiento" runat="server" CssClass="form-control" ></asp:DropDownList>
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label13" runat="server" Text="Modo" CssClass="control-label-2"></asp:Label>
                                            <asp:DropDownList ID="DdlModo" runat="server" CssClass="form-control" >
                                                <asp:ListItem Selected="True" Value="1">A</asp:ListItem>
                                                <asp:ListItem Value="2">M</asp:ListItem>
                                                <asp:ListItem >&lt; Seleccionar &gt;</asp:ListItem>
                                            </asp:DropDownList>
                                        </div> 
                                    </div>
                                    <div class="row">   
                                        <div class="col-lg-3">
                                        </div>
                                        <div class="col-lg-3">
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label18" CssClass="control-label-2" runat="server" Text="Guardar" ForeColor="White" ></asp:Label>
                                            <asp:Button ID="BtnSecGuardar" runat="server" visible="False" Text="Guardar" CssClass="form-control btn btn-default" />
                                        </div> 
                                        <div class="col-lg-3">
                                            <asp:Label ID="Label19" CssClass="control-label-2" runat="server" Text="Cancelar" ForeColor="White" ></asp:Label>
                                            <asp:Button ID="BtnSecCancelar" runat="server" visible="False" Text="Cancelar" CssClass="form-control btn btn-default" />
                                        </div>
                                    </div>   
                                    <div class="row">
                                        <div class="col-lg-12">  
                                            <asp:Label ID="LblCodCCSec" runat="server" Text="" visible="false"  CssClass ="control-label-2"></asp:Label>
                                            <asp:Label ID="LblCodSec" runat="server" Text="" visible="false"  CssClass ="control-label-2"></asp:Label>
                                            <asp:Label ID="LblCodCentroCosto" runat="server" visible="False"></asp:Label>   
                                        </div> 
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="LblRegistro"  CssClass="control-label-2" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                                    </div>
                                </div> 
                                <div class="row">
                                    <div class="col-lg-12">  
                                        <asp:GridView ID="GvSeccion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                                            <Columns>
                                                <asp:ButtonField CommandName="Editar" ButtonType="Image" ImageUrl="~/icono/edit.gif">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:ButtonField CommandName="Eliminar" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                                    <ItemStyle Height="10px" Width="10px" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="c0" HeaderText="# Reg." />
                                                <asp:BoundField DataField="c1" HeaderText="Cod. Interno" />
                                                <asp:BoundField DataField="c2" HeaderText="Descripcion" />
                                                <asp:BoundField DataField="c3" HeaderText="Codigo" />
                                                <asp:BoundField DataField="c4" HeaderText="Direccion" />
                                                <asp:BoundField DataField="c5" HeaderText="Piso" />
                                                <asp:BoundField DataField="c6" HeaderText="Edificio" />
                                                <asp:BoundField DataField="c7" HeaderText="Ubicacion" />
                                                <asp:BoundField DataField="c8" HeaderText="Activo" />
                                                <asp:BoundField DataField="c10" HeaderText="Tipo de Establecimiento" />
                                            </Columns>
                                        </asp:GridView>
                                    </div> 
                                </div>     
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GvSeccion" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="BtnNuevo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnSecGuardar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnSecCancelar" EventName="Click" />
                            
                            </Triggers>
                        </asp:UpdatePanel>

                        <div id="ModalMensajeSec" class="modal fade" role="dialog" data-backdrop="static" style="position:fixed; top:25%;"> 
                            <div class="modal-dialog modal-sm">
    		                    <div class="modal-content">
                                    <div class="modal-header" style="padding: 8px 10px; text-align:center; background-color:white;">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="MensajeSec" class="col-lg-12"/>
                                        </div>
    						            <div class="col-lg-12">
                                            <asp:Button ID="BtnCerrarSec" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
                            
            </div> 
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

