<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Pedidos.aspx.vb" Inherits="Inventario_Inventario_Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Registro Pedidos" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                    </div> 
                </div>
                <div class="row espacio">
                    <div class="col-md-3 col-xs-6">
                       <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-3">
                        <asp:Label ID="lblEtiq2" runat="server" class="control-label-2" Text="Nro. Ticket :"></asp:Label>
                        <asp:TextBox ID="TxtNroTicket" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblEtiq3" runat="server" class="control-label-2" Text="Tarea :"></asp:Label>
                        <asp:TextBox ID="TxtNroTarea" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-3">
                        <asp:Label ID="lblEtiq4" runat="server" Text="Tipo de Pedido :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlTipoPedido" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblEtiq11" runat="server" class="control-label-2" Text="Motivo"></asp:Label>
                        <asp:TextBox ID="TxtMotivo" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-3">
                        <asp:Label ID="lblEtiq12" runat="server" Text="Tipo de Atención :" CssClass="control-label-2" visible="false"  />
                        <asp:DropDownList ID="DdlTipoAtencion" runat="server" CssClass="form-control" Visible ="false" >
                        </asp:DropDownList>
                    </div>
                </div> 
                <div class="row espacio">
                    <div class="col-md-2 col-xs-12">
                        <asp:Label ID="lblEtiq5" runat="server" Text="Usuario:" CssClass="control-label-2" />
                        <asp:TextBox ID="TxtUsuaio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-xs-11">
                        <asp:Label ID="lblEtiq6" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                        <asp:Button ID="BtnBuscarUser" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6 col-xs-12">
                        <asp:Label ID="lblEtiq7" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                        <asp:TextBox ID="TxtNombre" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 
                <div class="row espacio">
                    <div class="col-md-2 col-xs-12">
                        <asp:Label ID="lblEtiq8" runat="server" Text="Centro Costos" CssClass="control-label-2" />
                        <asp:TextBox ID="TxtCC_CodInterno" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-xs-11">
                        <asp:Label ID="lblEtiq9" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                        <asp:Button ID="BtnCC" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6 col-xs-12">
                        <asp:Label ID="lblEtiq10" runat="server" Text="..." CssClass="control-label-2" forecolor="White" />
                        <asp:TextBox ID="TxtCC_Descripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 
                <div class="row espacio">
                        <asp:TextBox ID="TxtCC_Codigo" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                </div> 
                <div class="row espacio">
                    <div class="col-md-6">
                        <asp:Label ID="Label2" runat="server" class="control-label-2" Text="Observación"></asp:Label>
                        <asp:TextBox ID="TxtObservacion" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-3">
                        <asp:Button ID="BtnAgregar" runat="server" Text="Agregar Artículo" CssClass="form-control btn btn-default" />
                    </div> 
                </div>
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:GridView id="FlexItem" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="true" >
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Quitar" Text="Quitar">
                                    <ControlStyle CssClass="EstiloBoton_Ac" Width="50px" />
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="c0" HeaderText="Item">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c1" HeaderText="Cod. Art.">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c2" HeaderText="Nro. Parte">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c3" HeaderText="Descripción">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="c4" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCant" runat="server" CssClass="form-control"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        </asp:GridView>
                    </div> 
                </div>
            </ContentTemplate>
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="BtnGuardar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="DdlTipoPedido" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div> 

    <div id="ModalBuscaArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="TituloBuscarArticulos" Text="Búsqueda de Artículos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_codArt">Codigo Art :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtCodArticuloBA" type="text" runat="server" />
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_clasificacionBA">Clasificacíon :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtClasificacionBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaClasificacionBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodClasificacionBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_descripcionBA">Descripción :</label>
                                                <div class="col-sm-8 col-xs-5">
                                                    <input class="form-control" id="TxtDescripcionBA" name="Descripcion" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_tipoArticuloBA">Tipo de Art :</label>
                                                <div class="col-sm-3 col-xs-7 selectContainer">
                                                    <asp:DropDownList ID="DdlTipoBA" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">Número Parte :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtNumParteBA" type="text" runat="server" />
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_codEspecificoBA">Cod. Especif :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtCodEspecificoBA" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_marcaBA">Marca :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtMarcaBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaMarcaBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                                <label class="control-label col-sm-2 col-xs-12" for="id_modeloBA">Modelo :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row form-group col-md-12">
                                                        <div class="col-lg-12">
                                                            <asp:GridView ID="GvBuscarArticulos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                                <Columns>
                                                                    <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                                    <asp:BoundField DataField="ART_CLASIFICACION" HeaderText="Clasificación" SortExpression="ART_CLASIFICACION" />
                                                                    <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                                        <ItemStyle ForeColor="White" Width="1px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="SelectedNodeChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
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
    <div id="ModalClasificacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="TituloPopupp" Text="Buscar Clasificación" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-6 col-lg-offset-4">
                                            <asp:Button ID="btnModalBuscarClas" class="btn btn-default" runat="server" Text="Buscar" />
                                            <asp:Button ID="BtnCerrarClasificacion" class="btn btn-default" runat="server" Text="Cancelar" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="trvClasificacion" runat="server" ShowExpandCollapse="true"
                                                ShowLines="True" PopulateNodesFromClient="true" ExpandDepth="0">
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <Nodes>
                                                </Nodes>
                                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                                            </asp:TreeView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="TreeNodePopulate" />
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

   
    <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnCC" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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

