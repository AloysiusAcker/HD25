<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Relacion_Salidas_Almacent.aspx.vb" Inherits="Inventario_Inventario_Relacion_Salidas_Almacent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Relación de Salidas de Almacén" CssClass="Titulos"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblError" runat="server" Text="" ForeColor="red"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="lblEtiqueta1" CssClass="control-label-2" runat="server" Text="Nro. Salida"></asp:Label>
                 <asp:TextBox ID="TxtNroSalida" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label4"  CssClass="control-label-2" runat="server" Text="Listar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnRegularizar" runat="server" Text="Regularizar traking" CssClass="form-control btn btn-default" Visible ="false" />
            </div> 
            <div class="col-md-3">
                <asp:Label ID="LblEtiq10"  CssClass="control-label-2" runat="server" Text="Listar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnListar" runat="server" Text="Listar" CssClass="form-control btn btn-default"/>
            </div> 
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Motivo"></asp:Label>
                <asp:DropDownList ID="DdlMotivo" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
            </div>   
            <div class="col-md-6">
                <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="Estado"></asp:Label>
                <asp:DropDownList ID="DdlEstado" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
            </div>                  
        </div>   
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate> 
                <div class="row espacio">                
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkBusArticulo" CssClass="checkbox checkbox-inline" Text="Artículo" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="TxtBusArtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnBusArt" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox ID="TxtBusArtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                   
                </div> 
                <div class="row">
                </div>

                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="LblRegistro" runat="server" Text="" ></asp:Label>
                    </div>
                </div>    
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gridSalida" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="Codsalida" HeaderText="Codigo" SortExpression="Codsalida" />
                                <asp:BoundField DataField="Fecha_Sal" HeaderText="Fecha" SortExpression="Fecha_Sal" />
                                <asp:BoundField DataField="Hora_Salida" HeaderText="Hora" SortExpression="Hora_Salida" />
                                <asp:BoundField DataField="Origen_codigo" HeaderText="Cod. Almacén" SortExpression="Origen_codigo" />
                                <asp:BoundField DataField="Origen" HeaderText="Nombre" SortExpression="Origen" />
                                <asp:BoundField DataField="Destino" HeaderText="Destino tipo" SortExpression="Destino" />
                                <asp:BoundField DataField="DESTINO_CODINTERNO" HeaderText="Cod. Destino" SortExpression="DESTINO_CODINTERNO" />
                                <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Nombre Destino" SortExpression="DESTINO_NOMBRE" />
                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"GuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("GUIREM_CODIGO") IsNot DBNull.Value, Eval("GUIREM_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Nro_Guia" HeaderText="Guía" SortExpression="Nro_Guia" />
                                <asp:BoundField DataField="DESP_ESTADO" SortExpression="DESP_ESTADO">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                             </Columns>
                        </asp:GridView>
                </div>
            </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>


    </div>

    <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gridSalida" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step4">
                            <div class="panel panel-default">
                                <div class="panel-body">   
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnCerrar" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="btnCerrar_Click" />
                                        </div>
                                        <div class="col-md-4">
                                            </div>
                                    </div>
                                       
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>                
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblEtiq35"  CssClass="control-label-2" runat="server" Text="Lista de producto"></asp:Label>                
                                                    <asp:GridView ID="gridSalidaEq" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                            <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Articulo" SortExpression="Cod_Articulo" /> 
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" /> 
                                                            <asp:BoundField DataField="Descripcion_Articulo" HeaderText="Artículo" SortExpression="Descripcion_Articulo" />
                                                            <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div> 
                                            </div>        
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label2"  CssClass="control-label-2" runat="server" Text="Lista de Accesorios"></asp:Label>                
                                                    <asp:GridView ID="gridSalidaAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                            <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Articulo" SortExpression="Cod_Articulo" /> 
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" /> 
                                                            
                                                            <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                            <asp:BoundField DataField="Descripcion_Articulo" HeaderText="Artículo" SortExpression="Descripcion_Articulo" />
                                                            <asp:BoundField DataField="CANT" HeaderText="Cantidad" SortExpression="CANT" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div> 
                                            </div>   
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gridSalida" EventName="RowCommand" />
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

    <div id="ModalAnulacion" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12" >
                                    <asp:Label ID="LblTituloModalAnul" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridSalida" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnAnular" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblAnulacion" runat="server" Text="Motivo de Anulación" CssClass="control-label-2"></asp:Label>
                                                        <asp:TextBox ID="txtAnulacion" runat="server" CssClass="form-control" TextMode="MultiLine" ></asp:TextBox>
                                                    </div>
                                                    <div class="row">
                                                        <asp:Label ID="lblCodsalida" runat="server" Text="" Visible ="false" CssClass="control-label-2"></asp:Label>
                                                        <asp:Label ID="lblCodEstado" runat="server" Text="" Visible ="false" CssClass="control-label-2"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Button ID="BtnAnularCerrar" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="BtnAnularCerrar_Click" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnAnular" runat="server" class="form-control btn btn-default" Text="Anular" OnClick="btnAnular_Click" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gridSalida" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="btnAnular" EventName="Click" />
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
                    <asp:Label runat="server" ID="TituloBuscarArticulos" Text="Búsqueda de Artículos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
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
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
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
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnBusArt" EventName="Click" />
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

