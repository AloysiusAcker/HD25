<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Almacen_Salida.aspx.vb" Inherits="Inventario_Almacen_Salida" title="GestorPlus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        $(document).ready(function () {
            $("#ModalBusEquipo").draggable({
                handle: ".modal-header"
            });
        });
    </script>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Nueva Salida de Almacén" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">                        
                        <asp:Label id="lblMensaje" runat="server" CssClass="control-label-2" ForeColor="Maroon" ></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lbl1" runat="server" Text="Nro. Salida" CssClass="control-label2"></asp:Label>
                        <asp:TextBox ID="lblCodigo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                    </div>                 
                    <div class="col-md-3">
                        <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha:"></asp:Label>
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                    </div>    
                    <div class="col-md-3">
                        <asp:Label ID="Label5" CssClass="control-label-2" runat="server" Text="Hora:"></asp:Label>
                        <asp:TextBox ID="txtHora" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>                 
                    <div class="col-md-3">
                        <asp:Label ID="lblFechaDevol" CssClass="control-label-2" runat="server" Text="Fecha Dev.:"></asp:Label>
                        <asp:TextBox ID="txtFechaDevol" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaDevol" Format="dd/MM/yyyy" PopupButtonID="txtFechaDevol" ></cc1:CalendarExtender>
                    </div>   
                </div>
                <div class="row">           
                    <asp:Label ID="lblCodOrigen" runat="server" Text="" Visible="False"></asp:Label> 
                    <asp:Label ID="txtAlmacen" runat="server" Text="" Visible="False"></asp:Label>
                    <asp:Label ID="txtNomAlmacen" runat="server" Text="" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodDestino" runat="server" Text="" Visible="False"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-12">                
                        <asp:Label ID="Label2" runat="server" Text="Origen" CssClass="control-label-2"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:TextBox ID="txtOrigCodExt" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="_Ubica1" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtOrigDescrip" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9 col-xs-6">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Destino :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:TextBox ID="txtDesCodExterno" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="_Ubica2" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtDesDescrip" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMotivo" runat="server" Text="Motivo :" CssClass="control-label-2" />
                        <asp:DropDownList ID="cboMotivo" runat="server" AutoPostBack="True" class="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label6" runat="server" Text="Envia Sal."  CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="txtPerEnvia" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label3" runat="server" Text="Observación"  CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="txtObs" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">

                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label8" runat="server" Text="Nro. Salida" CssClass="control-label2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnAgregarEq" runat="server" Text="Agregar Equipos" ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label7" runat="server" Text="Nro. Salida" CssClass="control-label2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnAgregarAcc" runat="server" Text="Agregar Acc." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lbl2" runat="server" Text="Nro. Salida" CssClass="control-label2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="_Grabar" runat="server" Text="Grabar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                </div>
                <div class="row col-md-12">
                    <hr />
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="lbl12" runat="server" Text="Equipos" CssClass="control-label-2"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView id="_DetalleEq"  runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="QuitarFila" Text="&gt;" ButtonType="Button">
                                <ControlStyle CssClass="EstiloBoton_Ac" Font-Names="Arial" Font-Size="8pt" Width="18px"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Equipo"></asp:BoundField>
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
                                <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="N&#186; Serie">
                                <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="N&#186; Placa">
                                <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SERIE_NUMERAR" ShowHeader="False">
                                <ItemStyle ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Funci&#243;n"><ItemTemplate>
                                    <asp:DropDownList ID="cboFuncion" runat="server"  CssClass ="form-control" >
                                    </asp:DropDownList>                                    
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="COD_FUNCION" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="REEN_NUMERO" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="AVERIA" Visible="False"></asp:BoundField>
                                <asp:TemplateField HeaderText="Falla Aver&#237;a">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboAveria" runat="server"  CssClass ="form-control" >
                                        </asp:DropDownList>                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="COD_FALLA" Visible="False"></asp:BoundField>
                                <asp:TemplateField HeaderText="Detalle de la Aver&#237;a"><ItemTemplate>
                                    <asp:TextBox ID="txtDetAveria" runat="server" CssClass ="form-control" ></asp:TextBox>                                    
                                </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="Cyan"></SelectedRowStyle>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        </asp:GridView> 
                    </div>
                </div>                 
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="lbl13" runat="server" Text="Accesorios" CssClass="control-label-2"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView id="_DetalleAc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="QuitarFila" Text="&gt;" ButtonType="Button">
                                    <ControlStyle CssClass="EstiloBoton_Ac" Font-Names="Arial" Font-Size="8pt" Width="18px"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Accesorio"></asp:BoundField>
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock Actual">
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Q Salida">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantSal" runat="server"  CssClass ="form-control" ></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="False"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        </asp:GridView> 
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lbl9" runat="server"  Text="Fecha Registro" CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="lblFecha" runat="server" class="form-control" ReadOnly="true" ></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lbl10" runat="server" Text="Hora Registro" CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="lblHora" runat="server" class="form-control" ReadOnly="true" ></asp:TextBox>
                    </div>
                    <div class="col-md-8">
                        <asp:Label ID="lbl11" runat="server" Text="Usuario Registra" CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="lblUsuario" runat="server" class="form-control" ReadOnly="true" ></asp:TextBox>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="_BusEq" EventName="RowCommand"/>
                <asp:AsyncPostBackTrigger ControlID="_BusAc" EventName="RowCommand"/>
                <asp:AsyncPostBackTrigger ControlID="_DetalleEq" EventName="RowCommand"/>
                <asp:AsyncPostBackTrigger ControlID="_DetalleAc" EventName="RowCommand"/>
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    
    <div id="ModalArticuloEq" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
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
                                            <div class="row">
                                                <div class="col-md-4 col-xs-7">
                                                    <asp:Label id="id_codArt" runat="server" Text="Artículo" Cssclass="control-label-2" />
                                                    <input class="form-control" id="TxtCodArticuloBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-8 col-xs-7">
                                                    <asp:Label id="id_descripcionBA" runat="server" Text="Descripción" Cssclass="control-label-2" />
                                                    <input class="form-control" id="TxtDescripcionBA" name="Descripcion" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4 col-xs-7">
                                                    <asp:Label id="id_numParteBA" runat="server" Text="Nro. Partes" Cssclass="control-label-2" />
                                                    <input class="form-control" id="TxtNumParteBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-7 col-xs-7">
                                                    <asp:Label id="id_clasificacionBA" runat="server" Text="Clasificación" Cssclass="control-label-2" />    
                                                    <input class="form-control" id="TxtClasificacionBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-1 col-xs-2">
                                                    <asp:Label ID="Label15" runat="server" text="Bus" CssClass="control-label-2" ForeColor="White" />
                                                    <asp:Button ID="BtnBuscaClasificacionBA" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="LblCodClasificacionBA" runat="server" CssClass="control-label" Visible="false" />
                                                <asp:Label ID="lblCodClas" runat="server" CssClass="control-label" Visible="false" />
                                                <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                                <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-xs-7">
                                                    <asp:Label id="id_codEspecificoBA" runat="server" Text="Cod. Especifico" Cssclass="control-label-2" />  
                                                    <input class="form-control" id="TxtCodEspecificoBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-3 col-xs-5">
                                                    <asp:Label id="id_marcaBA" runat="server" Text="Marca" Cssclass="control-label-2" /> 
                                                    <input class="form-control" id="TxtMarcaBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-1">
                                                    <asp:Label ID="Label17" runat="server" text="Bus" CssClass="control-label-2" ForeColor="White" />
                                                    <asp:Button ID="BtnBuscaMarcaBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="form-control btn btn-default" />
                                                </div>
                                                <div class="col-md-3 col-xs-5">
                                                    <asp:Label id="id_modeloBA" runat="server" Text="Modelo" Cssclass="control-label-2" /> 
                                                    <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-1 col-xs-1">
                                                    <asp:Label ID="Label16" runat="server" text="Bus" CssClass="control-label-2" ForeColor="White" />
                                                    <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="form-control btn btn-default" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-xs-12">
                                                    <asp:Label id="lblSku" runat="server" Text="SKU" Cssclass="control-label-2" /> 
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-4 col-xs-7">
                                                    <asp:Label id="id_tipoArticuloBA" runat="server" Text="Tipo Artículo" Cssclass="control-label-2" />  
                                                    <asp:DropDownList ID="DdlTipoBA" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4 col-sx-6">
                                                    <asp:Label ID="Label10" runat="server" Text="Nro. Placa:" CssClass="control-label-2"></asp:Label>
                                                    <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-sx-6">
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="true"  Text="Nro. Serie:" CssClass="control-label-2"></asp:Label>
                                                    <asp:TextBox ID="txtSerieArt" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label18" runat="server" text="Bus" CssClass="control-label-2" ForeColor="White" />
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="form-control btn btn-default" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label19" runat="server" text="Bus" CssClass="control-label-2" ForeColor="White" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="form-control btn btn-default" />
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarAcc" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarEq" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvBusEquipo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="AgregarFila" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Código" SortExpression="ARTICULO_CODIGO" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo" SortExpression="TIPO_ART"/>
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="N&#186; Serie"></asp:BoundField>
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="N&#186; Placa"></asp:BoundField>
                                                        <asp:BoundField DataField="SERIE_NUMERAR">
                                                        <ItemStyle ForeColor="White"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REEN_NUMERO"></asp:BoundField>
                                                        <asp:BoundField DataField="AVERIA"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="GvBusAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible="false" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="AgregarFila" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Código" SortExpression="ARTICULO_CODIGO" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo" SortExpression="TIPO_ART"/>
                                                        <asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock"  SortExpression="STOCK_ACTUAL"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnAgregarAcc" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnAgregarEq" EventName="Click" />
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
       
    <div id="ModalBusEquipo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:Label ID="lbl14" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Equipos" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>                          
                                            <div class="row form-group col-md-12">
                                                <div class="col-md-3 col-sx-6">
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="true"  Text="Código Equipo:" CssClass="control-label"></asp:Label>
                                                    <asp:TextBox ID="txtCodigoArt" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-9 col-sx-6">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true"  Text="Descripción Equipo:" CssClass="control-label"></asp:Label>
                                                    <asp:TextBox ID="txtNomArt" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                          
                                            <div class="row form-group col-md-12">
                                                <div class="col-md-3 col-sx-2">
                                                    <asp:Button ID="_CerrarEq" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                                <div class="col-md-3 col-sx-2">
                                                    <asp:Button ID="_BuscarEq" runat="server" Text="Buscar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>               
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblCountBusEq" runat="server" Text="" CssClass="control-label-2"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <asp:GridView id="_BusEq" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="AgregarFila" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Equipo"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n"></asp:BoundField>
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="N&#186; Serie"></asp:BoundField>
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="N&#186; Placa"></asp:BoundField>
                                                        <asp:BoundField DataField="SERIE_NUMERAR">
                                                        <ItemStyle ForeColor="White"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REEN_NUMERO"></asp:BoundField>
                                                        <asp:BoundField DataField="AVERIA"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView> 
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="_Ubica1" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="_Ubica2" EventName="Click" />
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

    <div id="ModalBusAcc" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:Label ID="Label12" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Equipos" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>                          
                                            <div class="row form-group col-md-12">
                                                <div class="col-md-3 col-sx-6">
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="true"  Text="Código Equipo:" CssClass="control-label"></asp:Label>
                                                    <asp:TextBox ID="txtCodigoAc" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-9 col-sx-6">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="true"  Text="Descripción Equipo:" CssClass="control-label"></asp:Label>
                                                    <asp:TextBox ID="txtNomAc" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-md-3 col-sx-2">
                                                    <asp:Button ID="_CerrarAc" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                                <div class="col-md-3 col-sx-2">
                                                    <asp:Button ID="_BuscarAc" runat="server" Text="Buscar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>               
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <asp:Label ID="lblCountBusAc" runat="server" Text="" CssClass="control-label-2"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <asp:GridView id="_BusAc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="AgregarFila" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Accesorio"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n"></asp:BoundField>
                                                        <asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock Actual"></asp:BoundField>
                                                    </Columns>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                </asp:GridView> 
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="_Ubica1" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="_Ubica2" EventName="Click" />
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
    
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
             <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 50px;" valign="top">
                    <asp:Label ID="lbl19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Doc. Sal."
                        Width="48px" Visible="False"></asp:Label></td>
                <td align="left" colspan="9" style="vertical-align: middle;" valign="top">
                    <asp:RadioButtonList ID="OptDocSalida" runat="server" Font-Names="Arial" Font-Size="8pt"
                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="176px" Visible="False">
                        <asp:ListItem Selected="True" Value="1">Gu&#237;a Remisi&#243;n</asp:ListItem>
                        <asp:ListItem Value="2">Gu&#237;a Interna</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>

        </table>
    </div>
           
    <div id="ModalBusqueda" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12" >
                        <asp:Label ID="lblEtq_BusDestino" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Centro de Costos" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>                          
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="lblEtiq_Modal1" runat="server" Font-Bold="true"  Text="Código :" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-3 col-sx-5">
                                                    <asp:TextBox ID="txtBusCod" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-3">
                                                    <asp:Button ID="btnUbiCerrar" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="lblEtiq_Modal2" runat="server" Font-Bold="true"  Text="Descripción :" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-5 col-sx-5">
                                                    <asp:TextBox ID="txtBusDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-1">
                                                    <asp:Button ID="btnUbiListar" runat="server" Text="Listar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <asp:GridView ID="FlexUbicacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
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
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="_Ubica1" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="_Ubica2" EventName="Click" />
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
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12 col-sm-6" >
                                    <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                                </div> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="_Grabar" EventName="Click" />
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
                                                <asp:Label ID="Label1" runat="server" Font-Size="16px" class="control-label2" Text="Elegir Tipo de Documento a Generar" />
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

</asp:Content>

