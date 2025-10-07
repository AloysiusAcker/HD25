<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_BienesNuevos_NoConsiderar.aspx.vb" Inherits="Inventario_Inventario_BienesNuevos_NoConsiderar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Bienes Nuevos " CssClass="Titulos" />
        </div> 
    </div>
    <br />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">    
                <div class="col-lg-2">
                    <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                </div> 
                <div class="col-lg-2">
                    <asp:Button ID="BtnNoconsiderar" runat="server" Text="No Considerar" ControlStyle-CssClass="form-control btn btn-default" />
                </div> 
                <div class="col-lg-3">
                    <asp:Button ID="BtnListarNo" runat="server" Text="Lista bienes No Considerados" ControlStyle-CssClass="form-control btn btn-default" />
                </div> 
                <div class="col-lg-3">
                    <asp:Button ID="BtnRegresar" runat="server" Text="Regresar a nuevo" ControlStyle-CssClass="form-control btn btn-default" />
                </div> 
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                    <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                    <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                    <asp:RadioButton GroupName="ubicacion" ID="RBTodos" runat="server" Text="Todos" AutoPostBack="True" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-xs-2">
                    <asp:TextBox ID="txtOficina_CodInterno" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1 col-xs-1">
                    <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                </div>
                <div class="col-md-6 col-xs-6">
                    <asp:TextBox ID="txtOficina_Descripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div>            
                <div class="row">
                    <div class="col-lg-12">
                        <asp:CheckBox ID="CbArticuloCargar" runat="server" Text="Artículo A Cargar" CssClass="checkbox checkbox-inline" AutoPostBack="true" />
                    </div> 
                </div>
            <div class="row">
                <div class="col-lg-2">
                    <asp:TextBox ID="TxtCodArticulo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-1">
                    <asp:Button ID="BtnBuscarArticulo" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" Enabled="false" />
                </div>
                <div class="col-lg-9">
                    <asp:TextBox ID="TxtDescArticulo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">                    
                <div class="col-md-12">
                    <asp:label ID="lblCodInv_Ubica" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblOficina_Codigo" runat="server" Width="102px" Visible="false"></asp:label>
                </div> 
            </div>
                       
            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
                </div> 
            </div>    
            <div class="row">                    
                <div class="col-md-12">
                    <asp:GridView ID="GvListaVerificarInventarioNuevos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Height="20px" Width="1px" />                                                                      
                                </ItemTemplate>
                                <ControlStyle Width="20px"></ControlStyle>
                                <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                            <asp:TemplateField HeaderText="Desc. Artículo">
                                <ItemTemplate>
                                    <div class="two-lines-cell">
                                        <%# Eval("ART_DESCRIPCION") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                            <asp:BoundField DataField="Ubicacion_Cod_Interno" HeaderText="c. Costo" SortExpression="Ubicacion_Cod_Interno" />
                            <asp:BoundField DataField="Ubicacion" HeaderText="Oficina" SortExpression="Ubicacion" />
                            <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                            <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="serie_numerar" SortExpression="serie_numerar">
                                <ItemStyle ForeColor="White" Width="0.1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" SortExpression="INVDET_INVENTUBIC_CODIGO">
                                <ItemStyle ForeColor="White" Width="0.1px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>  
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="DdlInventario" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="RBTodos" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNoconsiderar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnListarNo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnRegresar" EventName="Click" />
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

 <%-- 
         --%>
      <div id = "ModalBuscaArticulos" Class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Búsqueda de Artículos</h4>
                </div>
                <div class="modal-body" style="padding: 20px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                             <div class="row">
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_codArt">Código Artículo</label>
                                        <input class="form-control" id="TxtCodArticuloBA" type="text" runat="server" />
                                    </div>                                              
                                    <div class="col-md-6 col-xs-6 selectContainer">
                                        <label class="control-label" for="id_tipoArticuloBA">Tipo de Art :</label>
                                        <asp:DropDownList ID="DdlTipoBA" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-xs-10">
                                        <label class="control-label" for="id_descripcionBA">Descripción :</label>
                                        <input class="form-control" id="TxtDescripcionBA" name="Descripcion" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label ID="LblCodClasificacionBA" runat="server" CssClass="control-label" Visible="false" />
                                    <asp:Label ID="lblCodClas" runat="server" CssClass="control-label" Visible="false" />
                                </div>
                                <div class="row">
                                    <div class="col-md-10 col-xs-10">
                                        <label class="control-label" for="id_clasificacionBA">Clasificacíon</label>
                                        <input class="form-control" id="TxtClasificacionBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-xs-2">
                                        <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="C" forecolor="White" />
                                        <asp:Button ID="BtnBuscaClasificacionBA" runat="server" Text="..." ControlStyle-CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <div class="row">      
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_numParteBA">Número Parte</label>
                                        <input class="form-control" id="TxtNumParteBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_codEspecificoBA">Cod. Especif</label>
                                        <input class="form-control" id="TxtCodEspecificoBA" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-10 col-xs-10">
                                    <label class="control-label" for="id_marcaBA">Marca</label>
                                        <input class="form-control" id="TxtMarcaBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-xs-2">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Mar" forecolor="White" />
                                        <asp:Button ID="BtnBuscaMarcaBA" runat="server" Text="..." ControlStyle-CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <div class="row">            
                                    <div class="col-md-10 col-xs-10">
                                        <label class="control-label" for="id_modeloBA">Modelo</label>
                                        <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-xs-2">
                                        <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="Mar" forecolor="White" />
                                        <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..." ControlStyle-CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                    <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="form-group btn btn-default" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                         <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblCantArtReg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="GvBuscarArticulos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo"  SortExpression="TIPO_ART"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
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
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div> 
    
</asp:Content>

