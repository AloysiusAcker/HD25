<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Regularizar_Equipos.aspx.vb" Inherits="Inventario_Inventario_Regularizar_Equipos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Regularizar Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                        <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
      <%--                  <asp:RadioButton GroupName="ubicacion" ID="RBUbicaciones" runat="server" Text="Ubicaciones" AutoPostBack="True" />--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                  <%--      <asp:Label ID="LblCodigo" runat="server" Text="Código :" CssClass="control-label-2"  ForeColor="white"></asp:Label>--%>
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                   <%--     <asp:Label ID="Label1"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>--%>
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-9 ">
        <%--                <asp:Label ID="Label2"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>--%>
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                         <asp:Label ID="LblUbicaCodigo" runat="server" Text="" visible="false" ></asp:Label>
                         <asp:Label ID="lblCodEstado" runat="server" Text="" visible="false" ></asp:Label>
                         <asp:Label ID="LblUbicaCodigoInv" runat="server" Text="" visible="false" ></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
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
                        <asp:GridView ID="gvResumen" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" />
                                <asp:BoundField DataField="Estado_Inventario" HeaderText="Estado" SortExpression="Estado_Inventario" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                                <asp:BoundField DataField="INVDET_ESTADO_INVENTARIO" SortExpression="INVDET_ESTADO_INVENTARIO">
                                    <ItemStyle ForeColor="White" Width="" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="LblRegistro2" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>  
                <div class="row">                    
                    <div class="col-md-3">
                        <asp:Button ID="BtnRegularizar" runat="server" Text="Regularizar Todo" ControlStyle-CssClass="form-control btn btn-default" visible="false"/>
                    </div> 
                </div> 
                <br />
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvListaVerificarInventario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Regularizar" Text="Regularizar" />
                                <asp:BoundField DataField="SERIE_NUMERAR" HeaderText="Numerar" SortExpression="SERIE_NUMERAR" />
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="SERIE_ORIGINAL" HeaderText="Nro. Serie" SortExpression="SERIE_ORIGINAL" />
                                <asp:BoundField DataField="SERIE_NUEVA" HeaderText="Nro. Serie Nueva" SortExpression="SERIE_NUEVA" />
                                <asp:BoundField DataField="PLACA_ORIGINAL" HeaderText="Nro. Placa" SortExpression="PLACA_ORIGINAL" />
                                <asp:BoundField DataField="PLACA_NUEVA" HeaderText="Nro. Placa Nueva" SortExpression="PLACA_NUEVA" />
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                                <asp:BoundField DataField="EST_CONCILIACION" HeaderText="Estado Conciliado" SortExpression="EST_CONCILIACION" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="UBICACT_TIPO" SortExpression="UBICACT_TIPO">
                                    <ItemStyle ForeColor="White" Width="" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UBICACT_CODIGO" SortExpression="UBICACT_CODIGO">
                                    <ItemStyle ForeColor="White" Width="" />
                                </asp:BoundField>
                                <asp:BoundField DataField="regularizado" HeaderText="Regularizado" SortExpression="regularizado" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnRegularizar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="gvResumen" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>

    </div>


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
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
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
                                                        <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodUbi" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
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


</asp:Content>

