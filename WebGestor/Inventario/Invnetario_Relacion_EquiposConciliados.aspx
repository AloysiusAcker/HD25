<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Invnetario_Relacion_EquiposConciliados.aspx.vb" Inherits="Inventario_Invnetario_Relacion_EquiposConciliados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Lista de Bienes Conciliados" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnListar" runat="server" Text="Listar"  ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnExportarConciliados" runat="server" Text="Lista Equipos Conciliados"  ControlStyle-CssClass="form-control btn btn-default" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:Label ID="loadingMessage" runat="server" Text="Processing, please wait..." Style="display:none;color:red;" />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-xs-12">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-xs-11">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6 col-xs-12">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:TextBox ID="TxtCodUbica" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TxtCodUbicaInv" runat="server" Width="102px" Visible="false"></asp:TextBox>
                    </div> 
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>    
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="CONCILIA_CODIGO" HeaderText="Nro. Concilia" SortExpression="CONCILIA_CODIGO" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha Concilia" SortExpression="Fecha" />
                                <asp:BoundField DataField="INVENT_DESCRIPCION" HeaderText="Inventario" SortExpression="INVENT_DESCRIPCION" />
                                <asp:BoundField DataField="Ubicacion_Cod_Interno" HeaderText="Oficina Cod." SortExpression="Ubicacion_Cod_Interno" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Oficina" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="INVENT_UBIC_ART_CODIGO" HeaderText="Art." SortExpression="INVENT_UBIC_ART_CODIGO" />
                                <asp:BoundField DataField="invdet_Articulo_descripcion" HeaderText="Descripción" SortExpression="invdet_Articulo_descripcion" />
                                <asp:BoundField DataField="INVENT_UBIC_SERIE_NRO" HeaderText="Serie Nro." SortExpression="INVENT_UBIC_SERIE_NRO"/>
                                <asp:BoundField DataField="INVENT_UBIC_PLACA_NRO" HeaderText="Placa Nro." SortExpression="INVENT_UBIC_PLACA_NRO" />
                                <asp:BoundField DataField="CONCILIA_ART_CODIGO" HeaderText="Art. Conciliado" SortExpression="CONCILIA_ART_CODIGO" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción Conciliado" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="CONCILIA_SERIE_NRO" HeaderText="Serie Nro. Conciliado" SortExpression="CONCILIA_SERIE_NRO" />
                                <asp:BoundField DataField="CONCILIA_PLACA_NRO" HeaderText="Placa Nro. Conciliado" SortExpression="CONCILIA_PLACA_NRO" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>  
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
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
                            <asp:AsyncPostBackTrigger ControlID="BtnBusca" EventName="Click" />
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
                                                        <asp:BoundField DataField="CodUbi" SortExpression="CodUbi">
                                                            <ItemStyle ForeColor="White" Width="0.1px" />
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

