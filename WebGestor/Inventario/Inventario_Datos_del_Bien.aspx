<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Inventario_Datos_del_Bien.aspx.vb" Inherits="Inventario_Datos_del_Bien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<asp:Label ID="Label5" runat="server" Text="Datos del Bien" CssClass="Titulos"></asp:Label><br />
    <br />
    <div class="form-horizontal">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group">
                    <asp:Label ID="lblNroPlaca" runat="server" CssClass="col-lg-2 control-label-2" Text="Nro.  Placa"></asp:Label>
                    <div class="col-lg-1">
                        <asp:TextBox ID="txtNroPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblNroSerie" runat="server" CssClass="col-lg-2 control-label-2" Text="Nro. Serie"></asp:Label>
                    <div class="col-lg-1">
                        <asp:TextBox ID="txtNroSerie" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="chckCodArticulo" runat="server" AutoPostBack="True" CssClass="control-label-2" Text="Cód. Articulo" />
                    <div class="col-lg-1">
                        <asp:TextBox ID="txtCodArticulo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Button ID="BtnBuscaArticulo" runat="server" ControlStyle-CssClass="btn btn-block" Enabled="false" Text="..." />
                    </div>
                    <div class="col-lg-3">
                        <asp:TextBox ID="txtDescArticulo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDescripcion" runat="server" CssClass="col-lg-2 control-label-2" Text="Descripcíon"></asp:Label>
                    <div class="col-lg-5">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="chckUbicacion" runat="server" AutoPostBack="True" CssClass="control-label-2" Text="Ubicación" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlUbicacion" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCodRelacionado" runat="server" CssClass="col-lg-2 control-label-2" Text="Cod. Relacionado" />
                    <div class="col-lg-5">
                        <asp:TextBox ID="txtCodRelacionado" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Listar" />
                </div>

                <asp:Label ID="LblSerieNum" runat="server" Visible="false" />
                <div class="row">
                    <div class="col-lg-11">
                        <br />
                        <asp:GridView ID="GvListaDatosBien" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-condensed small-top-margin GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Modificar" Text="Modificar" ButtonType="Image" ImageUrl="~/icono/Editar_opt.png">
                                <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                <asp:BoundField DataField="SERIE_COD_RELACIONADO" HeaderText="Cod. Relacionado" SortExpression="SERIE_COD_RELACIONADO" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripcíon Articulo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="cant" HeaderText="CANT" SortExpression="cant" />
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro.Serie" SortExpression="SERIE_NRO" />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro.Placa" SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicación" SortExpression="AREA_NOMBRE" />
                                <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Destino" SortExpression="TIPO_UBICACION" />
                                <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Destino" SortExpression="COD_ALMACEN" />
                                <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Destino" SortExpression="ALMACEN_NOMBRE" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="TIPOBIEN" HeaderText="Tipo Bien" SortExpression="TIPOBIEN" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
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
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaAreaM" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaUbicacionM" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarcaBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaModeloBA" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_lugarHecho">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_lugarHecho">Código :</label>
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
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaU" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-5 col-lg-offset-1">
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
                                                    <asp:GridView ID="GvBusquedaU" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="CodInterno" HeaderText="Codigo" SortExpression="CodInterno" />
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                            <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GvBusquedaM" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="codigoInt" HeaderText="Codigo" SortExpression="codigoInt" />
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                                            <asp:BoundField DataField="codigo" SortExpression="codigo">
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
    </div>



    <div id="ModalArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="TituloArticulo" Text="Inventario - Datos Iniciales" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_placaNro">Placa Nro :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtPlacaNroM" type="text" runat="server" />
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_serieNro">Serie Nro :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtSerieNroM" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_estado">Estado :</label>
                                                <div class="col-sm-3 col-xs-7 selectContainer">
                                                    <asp:DropDownList ID="DdlEstadoM" runat="server" AutoPostBack="True" CssClass="form-control" />
                                                </div>
                                                <div class="col-sm-2 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnIngresarEquipo" ControlStyle-CssClass="btn btn-success" runat="server" Text="Ingresar Equipo" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_codRelacionado">Cod. Relacionado :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtCodRelacionadoM" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnCerrarArticulo" ControlStyle-CssClass="btn btn-success" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_responsable">Responsable :</label>
                                                <div class="col-sm-9 col-xs-7 selectContainer">
                                                    <asp:DropDownList ID="DdlResponsableM" runat="server" AutoPostBack="True" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_articulo">Artículo :</label>
                                                <div class="col-sm-2 col-xs-7">
                                                    <input class="form-control" id="TxtCodArticuloM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaArticuloM" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input class="form-control" id="TxtDescArticuloM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <asp:Label ID="LblArticuloM" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-lg-5 col-lg-offset-3">
                                                    <asp:RadioButton GroupName="area" ID="RBAlmacenArea" runat="server" Text="Almacén" Checked="true" />
                                                    <asp:RadioButton GroupName="area" ID="RBCentroCArea" runat="server" Text="Centro de Costo" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_area">Area :</label>
                                                <div class="col-sm-2 col-xs-7">
                                                    <input class="form-control" id="TxtCodAreaM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaAreaM" runat="server" Text="..." ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input class="form-control" id="TxtDescAreaM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <asp:Label ID="LblCodAreaM" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_ubicacion">Ubicación :</label>
                                                <div class="col-sm-2 col-xs-7">
                                                    <input class="form-control" id="TxtCodUbicacionM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaUbicacionM" runat="server" Text="..." ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input class="form-control" id="TxtDescUbicacionM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <asp:Label ID="LblCodUbicacionM" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_observacion">Observación :</label>
                                                <div class="col-sm-9 col-xs-7">
                                                    <input class="form-control" id="TxtObservacionM" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_busArticulo">Bus. Artículo :</label>
                                                <div class="col-lg-5">
                                                    <input class="form-control" id="TxtBuscarArticulo" type="text" runat="server" />
                                                </div>
                                                <label class="col-lg-2 control-label" for="id_busSerieNro">Serie Nro :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtBuscarSerie" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="GvArticulo1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                            <asp:BoundField DataField="ART_TIPO" HeaderText="Tipo" SortExpression="ART_TIPO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="GvArticulo2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField HeaderText=" " />
                                                            <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                            <asp:BoundField DataField="SERIE_COD_RELACIONADO" HeaderText="Cod. Relacionado" SortExpression="SERIE_COD_RELACIONADO" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="GvListaDatosBien" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaU" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
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

    <div id="ModalBuscaArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloBuscarArticulos" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaArticuloM" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaArticulo" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
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
                                            <div class="row espacio">
                                                <div class="col-md-6">
                                                    <label class="control-label" for="id_numParteBA">SKU</label>
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <%--<div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                    <asp:Button ID="" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                </div>
                                            </div>--%>
                                            <div class="row espacio">
                                                <div class="col-md-3 col-xs-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                </div>
                                                <div class="col-md-3 col-xs-3">
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                </div>
                                                <div class="col-md-3 col-xs-3">
                                                    <asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="btn btn-default" Visible="false" />
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row form-group col-md-12">
                                                        <div class="col-lg-12">
                                                            <asp:GridView ID="GvBuscarArticulos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                                <%--<Columns>
                                                                    <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                                    <asp:BoundField DataField="ART_CLASIFICACION" HeaderText="Clasificación" SortExpression="ART_CLASIFICACION" />
                                                                    <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                                        <ItemStyle ForeColor="White" Width="1px" />
                                                                    </asp:BoundField>
                                                                </Columns>--%>
                                                                <Columns>
                                                                    <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte"  SortExpression="ART_CODEQUIVA"/>
                                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                                    <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo"  SortExpression="TIPO_ART"></asp:BoundField>
                                                                    <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                                        <ItemStyle ForeColor="White" Width="1px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                                    <asp:TemplateField ItemStyle-Width="20px">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                                                         </ItemTemplate>
                                                                    </asp:TemplateField>
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
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
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
                    <asp:Label runat="server" ID="TituloClasificacion" Text="Clasificación" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-6 col-lg-offset-4">
                                            <asp:Button ID="BtnBuscaClasificacion" class="btn btn-primary" runat="server" Text="Buscar" />
                                            <asp:Button ID="BtnCerrarClasificacion" class="btn btn-primary" runat="server" Text="Cerrar" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="TrvClasificacion" runat="server" ShowExpandCollapse="true" ShowLines="True"
                                                PopulateNodesFromClient="true" ExpandDepth="0">
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <Nodes>
                                                </Nodes>
                                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                                            </asp:TreeView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaClasificacion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="TreeNodePopulate" />
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
