<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Definir_Zonas.aspx.vb" Inherits="Inventario_Inventario_Definir_Zonas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">

        <h1 class="Titulos">Definición de Zonas por Almacén</h1>
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <div class="row espacio">
                    <div class="col-md-6 col-xs-6">
                        <asp:Label ID="LblEtiq_1" runat="server" Text="Almacén" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlBusAlmacen" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq2" CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White" ></asp:Label>
                    <asp:Button ID="BtnListar" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq3" CssClass="control-label-2" runat="server" Text="Ingresar Zona" ForeColor="White" ></asp:Label>
                    <asp:Button ID="BtnIngZona" runat="server" CssClass="form-control btn btn-default" Text="Ingresar Zona" />
                    </div>
                </div>
                <div class="row espacio">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvZonas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Rack" Text="Ing. Rack" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                    <ItemStyle Width="50px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="Almacen" HeaderText="Cod. Almacen" SortExpression="Almacen" />
                                <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Almacen Nombre" SortExpression="ALMACEN_NOMBRE" />
                                <asp:BoundField DataField="Zona" HeaderText="Zona" SortExpression="Zona" />
                                <asp:BoundField DataField="AZONA_NOMBRE" HeaderText="Zona" SortExpression="AZONA_NOMBRE" />
                                <asp:BoundField DataField="AZONA_CORRELATIVO" SortExpression="AZONA_CORRELATIVO">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                    <ItemStyle Width="50px" />
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>  
            </ContentTemplate> 
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 
    </div>

    <div id="ModalZona" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="LblModalZona" Text="Ingresar Zona" />
                        </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvZonas" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnIngZona" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <asp:Label ID="lblError" runat="server" ForeColor="Red" />
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Label ID="Label2" runat="server" Text="Almacen" CssClass="control-label-2" />
                                                    <asp:DropDownList ID="DdlAlmacen" runat="server" CssClass="form-control" AutoPostBack="true"  >
                                                    </asp:DropDownList>
                                                </div>
                                            </div> 
                                            <div class="row espacio">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label19" runat="server" Text="Zona" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtZona" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-10 col-xs-6">
                                                    <asp:Label ID="Label1" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                                                    <asp:TextBox ID="TxtZonaNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>      
                                            <div class="row espacio">
                                                <asp:Label ID="LblCodRegistro" runat="server" CssClass="form-control" Visible ="false" ></asp:Label>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label22" runat="server" Text="Compra" CssClass="control-label-2" forecolor="white"  />
                                                     <asp:Button ID="BtnGuardar" runat="server" Text="Guardar"  ControlStyle-CssClass="form-control btn btn-default"/>
                                               </div> 
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label23" runat="server" Text="Compra" CssClass="control-label-2"  forecolor="white"   />
                                                    <asp:Button ID="BtnLimpiar" runat="server" Text="Cerrar" ControlStyle-CssClass="form-control btn btn-default"/>
                                                </div>
                                            </div>                                                         
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvZonas" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnIngZona" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="DdlAlmacen" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div> 
    <div id="ModalRack" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="Label3" Text="Ingresar Rack con niveles y columnas" />
                        </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvZonas" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnIngZona" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step2">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row espacio">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label10" runat="server" Text="Almacen" CssClass="control-label-2" forecolor="White" />
                                                    <asp:TextBox ID="TxtRackAlmacen" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-10 col-xs-6">
                                                    <asp:Label ID="Label5" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                                                    <asp:TextBox ID="TxtRackAlmacenNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div> 
                                            <div class="row espacio">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label6" runat="server" Text="Zona" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtRackZona" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-10 col-xs-6">
                                                    <asp:Label ID="Label7" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                                                    <asp:TextBox ID="TxtRackZonaNombre" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>      
                                            <div class="row espacio">
                                                <asp:Label ID="LblCodRegistroZona" runat="server" CssClass="form-control" Visible ="false" ></asp:Label>
                                            </div>   
                                            <div class="row espacio">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label4" runat="server" Text="Rack" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtRack" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-10 col-xs-6">
                                                    <asp:Label ID="Label11" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                                                    <asp:TextBox ID="TxtRackNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>      
                                            <div class="row espacio">                                                
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label12" runat="server" Text="Niveles" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtRackNivel" runat="server" CssClass="form-control" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label13" runat="server" Text="Columnas" CssClass="control-label-2"/>
                                                    <asp:TextBox ID="TxtRackCol" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label8" runat="server" Text="Compra" CssClass="control-label-2" forecolor="white"  />
                                                     <asp:Button ID="BtnRackGuardar" runat="server" Text="Guardar"  ControlStyle-CssClass="form-control btn btn-default"/>
                                               </div> 
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label9" runat="server" Text="Compra" CssClass="control-label-2"  forecolor="white"   />
                                                    <asp:Button ID="BtnRackCerrar" runat="server" Text="Cerrar" ControlStyle-CssClass="form-control btn btn-default"/>
                                                </div>
                                            </div>        
                                            
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvZonas" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnIngZona" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="DdlAlmacen" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div> 

    
    <div id="ModalRackDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="Label14" Text="Detalle Rack x Zona" /> 
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step3">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <asp:Label ID="Label15" runat="server" ForeColor="Red" />
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Label ID="Label16" runat="server" Text="Almacen" CssClass="control-label-2" />
                                                    <asp:Textbox ID="TxtDetAlmacen" runat="server" CssClass="form-control" AutoPostBack="true"  >
                                                    </asp:Textbox>
                                                </div>
                                            </div> 
                                            <div class="row espacio">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label17" runat="server" Text="Zona" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtDetZona" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-10 col-xs-6">
                                                    <asp:Label ID="Label18" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                                                    <asp:TextBox ID="TxtDetZonaDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>      
                                            <div class="row espacio">
                                                <asp:Label ID="Label20" runat="server" CssClass="form-control" Visible ="false" ></asp:Label>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label21" runat="server" Text="Compra" CssClass="control-label-2" forecolor="white"  />
                                                     <asp:Button ID="BtnRegresar" runat="server" Text="Regresar"  ControlStyle-CssClass="form-control btn btn-default"/>
                                               </div> 
                                            </div>        
                                            <div class="row espacio">                    
                                                <div class="col-md-12">
                                                    <asp:GridView ID="GvRack" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="Rack" HeaderText="Cod. Rack" SortExpression="Rack" />
                                                            <asp:BoundField DataField="Rack_Nombre" HeaderText="Rack Nombre" SortExpression="Rack_Nombre" />
                                        <%--                    <asp:BoundField DataField="Niveles" HeaderText="Niveles" SortExpression="Niveles" />
                                                            <asp:BoundField DataField="Columnas" HeaderText="Columnas" SortExpression="Columnas" />--%>
                                                            <asp:BoundField DataField="Nivel" HeaderText="Nivel" SortExpression="Nivel" />
                                                            <asp:BoundField DataField="Col" HeaderText="Ubicacion" SortExpression="Col" />
                                                            <asp:BoundField DataField="ALMAREA_CORRELATIVO" SortExpression="ALMAREA_CORRELATIVO">
                                                                <ItemStyle ForeColor="White" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>  
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvZonas" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnIngZona" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="DdlAlmacen" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div> 

</asp:Content>

