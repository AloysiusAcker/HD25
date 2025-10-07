<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="inventario_Regularizar_UbicacionCarga.aspx.vb" Inherits="Inventario_inventario_Regularizar_UbicacionCarga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container-fluid">        
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Lista de Equipos a Regularizar" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label3" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RbTodos" runat="server" Text="Todos" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="BtnRegularizar" runat="server" Text="Regularizar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                         <asp:Label ID="LblUbicaCodigo" runat="server" Text="" visible="false" ></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
            </Triggers>
         </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server"  UpdateMode ="Conditional" >
            <ContentTemplate>  
                <div class="row">
                    <div class="col-md-12">
                        <asp:Timer ID="Timer1" runat="server" Interval="20000"></asp:Timer>
                    </div> 
                </div>              
                <div class="row">                    
                    <div class="col-md-12">
                         <asp:Label ID="LblRegRegularizar" runat="server" Text="" ></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="LblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvListaEquipos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="SERIE_NUMERAR" HeaderText="Numerar" />
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo"/>
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie"  />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa"/>
                                <asp:BoundField DataField="UBICACION_TIPO" HeaderText="Tipo" />
                                <asp:BoundField DataField="UBICACION_CODIGO" HeaderText="Cod. Interno" />
                                <asp:BoundField DataField="UBICACION_NOMBRE" HeaderText="Ubicacion" />
                                <asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Cod. Interno Carga" />
                                <asp:BoundField DataField="CECOSE_DESCRIPCION" HeaderText="Ubicación Carga"  />
                                <asp:BoundField DataField="CECOSE_CODIGO" >
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UBICACT_CODIGO" >
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>


        
     <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                <div class="col-sm-3 col-xs-5">
                                                    <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row col-md-12">
                                        <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="CodInterno" HeaderText="Codigo" SortExpression="CodInterno" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="Codigo" SortExpression="Codigo"/>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
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
    </div>


    </div>


</asp:Content>

