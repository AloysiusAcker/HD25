<%@ Page Title="" Language="VB" MasterPageFile="~/CRM/PagPrincipal_CRM.master" AutoEventWireup="false" CodeFile="Inventario_SalidaIngreso_Alavez.aspx.vb" Inherits="Inventario_Inventario_SalidaIngreso_Alavez" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container-fluid">

        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Salida e Ingreso a la vez" CssClass="Titulos"></asp:Label>
            </div>
        </div><br />
        <div class="row espacio">
            <div class="col-md-9">
                <asp:Label ID="LblError" runat="server" ForeColor="red"></asp:Label>
            </div> 
        </div>
        
        <div class="row espacio">
            <div class="col-md-2">
                <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha:"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Label18" CssClass="control-label-2" runat="server" Text="Hora:"></asp:Label>
                <asp:TextBox ID="TxtHora" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>                
            </div>
            <div class="col-md-2">
            </div>
            <div class="col-md-2">
            </div>
            <div class="col-md-2">
            </div>
            <div class="col-md-2">
            </div>
        </div>                
        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        
                <div class="row espacio">
                    <div class="col-md-2">
                        <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Nro. Placa:"></asp:Label>
                        <asp:TextBox ID="TxtNroPlaca" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Nro. Serie:"></asp:Label>
                        <asp:TextBox ID="TxtNroSerie" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:TextBox>                
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Lbl2" CssClass="control-label-2" runat="server" Text="Cantidad"></asp:Label>
                        <asp:TextBox ID="TxtCantidad" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label16"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnIngcant" runat="server"  CssClass="form-control btn btn-default" Text="Ing. Cant." />
                    </div> 
                    <div class="col-md-2">
                        <asp:Label ID="Label4"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnEjecutar" runat="server"  CssClass="form-control btn btn-default" Text="Ejecutar Salida" />
                    </div>
                </div>
                
                        
                <div class="row espacio">          
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-6">
                                <asp:Label ID="Label6" CssClass="control-label-2" runat="server" Text="Archivo"></asp:Label>
                                <!-- Contenido dentro del UpdatePanel -->
                                <div class="mb-3">
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                                </div>
                            </div> 
                            <div class="col-md-2">
                                <asp:Label ID="Label10"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="form-control btn btn-default" Text="Carga Placas" OnClick="BtnCargaArchivo_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label11"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnCargaSeries" runat="server" CssClass="form-control btn btn-default" Text="Carga Series" OnClick="BtnCargaSeries_Click" />
                            </div>
                            <div class="col-md-2">                                
                                <asp:Label ID="Label3"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnLimpiar" runat="server"  CssClass="form-control btn btn-default" Text="Limpiar" OnClick="BtnLimpiar_Click"/>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnCargaArchivo" />
                            <asp:PostBackTrigger ControlID="BtnCargaSeries" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div> 
                           
                <div class="row espacio">
                    <div class="col-md-6"> 
                        <asp:RadioButton ID="optIngreso" text="Ingreso" Checked="false"  runat="server"  GroupName="Movimiento"  AutoPostBack="True" />
                        <asp:RadioButton ID="optIngreso2" text="Salida" Checked="true"  runat="server" GroupName="Movimiento"  AutoPostBack="True" />
                    </div> 
                </div>      
                 <div class="row" runat="server" id="Articulo" visible ="false" >
                    <div class="col-md-2">
                        <asp:Label ID="lblEtiq5" runat="server" Text="Artículos" CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="txtArtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblEtiq6" runat="server" Text="Buscar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="btnBusArticulo" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-9">
                        <asp:Label ID="lblEtiq7" runat="server" Text="NombreArt" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:TextBox ID="txtArtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>


              <%--  <div class="row">
                    <div class="col-md-6"> 
                        <asp:Label ID="Lbl6" CssClass="control-label-2" runat="server" Text="Origen"></asp:Label>
                    </div>
                </div>--%>
         
                <div class="row espacio">
                    <div class="col-md-6"> 
                        <asp:Label ID="Lbl6" runat="server" Text="Origen : " CssClass="control-label-2" />
                        <asp:RadioButton ID="optOrigen" text="Almacén" Checked="false"  runat="server"  GroupName="Origen"  AutoPostBack="True" />
                        <asp:RadioButton ID="optOrigen2" text="Centro Costo" Checked="false"  runat="server" GroupName="Origen"  AutoPostBack="True" />
                    </div> 
                </div> 
                <div class="row espacio">
                    <div class="col-md-2">
                        <asp:Label ID="Label15"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="txtCodOrigen" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label5"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnBuscarOrigen" runat="server"  CssClass="form-control btn btn-default" Enabled="false" Text="..." />
                    </div> 
                    <div class="col-md-9">
                        <asp:Label ID="Label17"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>                  
                <br />
            <%--    <div class="row espacio">
                    <div class="col-md-12"> 
                        <asp:Label ID="lblDestino" CssClass="control-label-2" runat="server" Text="Destino"></asp:Label>
                    </div>
                </div>--%>

                <div class="row espacio">
                    <div class="col-md-12"> 
                        <asp:Label ID="lblDestino" runat="server" Text="Destino : " CssClass="control-label-2" />
                        <asp:RadioButton ID="RbDestino" text="Almacén" Checked="false"  runat="server" GroupName="Destino"  AutoPostBack="True" />
                        <asp:RadioButton ID="RbDestino2" text="Centro Costo" Checked="false"  runat="server" GroupName="Destino"  AutoPostBack="True" />
                        <asp:RadioButton ID="RbDestino3" text="Proveedores" Checked="false"  runat="server" GroupName="Destino"  AutoPostBack="True" />
                        <asp:RadioButton ID="RbDestino4" text="Clientes" Checked="false"  runat="server" GroupName="Destino"  AutoPostBack="True" />
                        <asp:RadioButton ID="RbDestino5" text="Personas" Checked="false"  runat="server" GroupName="Destino"  AutoPostBack="True" />
                    </div>
                </div>        

                <div class="row espacio">
                    <div class="col-md-2">
                        <asp:Label ID="lblEtiqDestino"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="TxtDestinoCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label8"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnBuscaDetino" runat="server"  CssClass="form-control btn btn-default" Enabled="false" Text="..." />
                    </div> 
                    <div class="col-md-9">
                        <asp:Label ID="Label9"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="TxtDestinoDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>          
                <div class="row espacio">
                    <div class="col-md-9">
                        <asp:Label ID="lblCodOrigen" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodDestino" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblTipoArt" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div> 
                
                <div class="row espacio">
                    <div class="col-md-6">
                        <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Motivo"></asp:Label>
                        <asp:DropDownList ID="DdlMotivo" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                    </div>                    
                </div> 
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:Label ID="Label7"  CssClass="control-label-2" runat="server" Text="Lista de producto"></asp:Label>                
                        <asp:GridView ID="GvListaArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="QuitarArt" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="COD_ARTICULO" HeaderText="Código" SortExpression="COD_ARTICULO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="STOCK" HeaderText="Stock" SortExpression="STOCK" />
                                <asp:BoundField DataField="CANTIDAD" HeaderText="Cant." SortExpression="CANTIDAD" />   
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie Nro." SortExpression="SERIE_NRO" />  
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro." SortExpression="PLACA_NRO" />  
                                <asp:BoundField DataField="TipoBien" HeaderText="Tipo Bien" SortExpression="TipoBien" />  
                                <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />  
                                <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cód. Ubicación" SortExpression="COD_ALMACEN" />
                                <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación" SortExpression="ALMACEN_NOMBRE"/>
                                <asp:BoundField DataField="ubicact_codigo" SortExpression="ubicact_codigo">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ubicact_tipo" SortExpression="ubicact_tipo">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Serie_Numerar" SortExpression="Serie_Numerar">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Art_Tipo" SortExpression="Art_Tipo">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RbDestino" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RbDestino2" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RbDestino3" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RbDestino4" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RbDestino5" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="optOrigen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="optOrigen2" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnLimpiar"  EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="optIngreso" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="optIngreso2" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <div id="ModalUbicacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="TituloPopup" runat="server" Text="Buscar" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarOrigen" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscaDetino" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step1">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnBuscar" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnCancelar" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                               <%--             <div class="col-lg-12">--%>
                                                <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CodInterno" HeaderText="Código" SortExpression="CodInterno" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                                <asp:BoundField DataField="Direccion"  HeaderText="Dirección" SortExpression="Codigo"/>
                                                                <asp:BoundField DataField="Ubigeo"  HeaderText="Ubigeo" SortExpression="Codigo"/>
                                                                <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                   <%--         </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ModalArticulo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
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
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
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
                                                        ControlStyle-CssClass="form-control btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodClasificacionBA" runat="server" CssClass="control-label" Visible="false" />
                                                <asp:Label ID="lblCodClas" runat="server" CssClass="control-label" Visible="false" />
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
                                                        ControlStyle-CssClass="form-control btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                                <label class="control-label col-sm-2 col-xs-12" for="id_modeloBA">Modelo :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="form-control btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">SKU :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="form-control btn btn-default" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="form-control btn btn-default" />
                                                    <asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GvBusArticulo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Código" SortExpression="ART_CODIGO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nº Parte" SortExpression="ART_CODEQUIVA"/>
                                                            <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo" SortExpression="TIPO_ART"/>
                                                            <asp:BoundField DataField="Art_Tipo" HeaderText="Tipo" SortExpression="Art_Tipo"/>
                                                            <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnLimpiar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        <%--</div>--%>
                                    </div>
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
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
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
                                            <asp:AsyncPostBackTrigger ControlID="btnModalBuscarClas" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="TreeNodePopulate" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
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



        <!-- Cuadro de diálogo modal -->
        <div id="myModalGuia" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12 col-sm-6" >
                                    <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                                </div> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnEjecutar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div> 
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="Label12" runat="server" Font-Size="16px" class="control-label2" Text="Elegir Tipo de Documento a Generar" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-3">
                                                <asp:Button ID="btnRedirectYes" runat="server" class="form-control btn btn-default" Text="Generar Guía de Remisión" OnClick="btnRedirectYes_Click" />
                                            </div>
                                            <div class="col-md-6 col-sm-3 ">
                                               <asp:Button ID="btnRedirectNo" runat="server" class="form-control btn btn-default" Text="Generar Guía Interna" OnClick="btnRedirectNo_Click" />
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

    </div>
 </asp:Content>

