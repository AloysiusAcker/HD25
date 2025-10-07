<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Inventario_EquiposConciliados.aspx.vb" Inherits="Inventario_EquiposConciliados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                <cc1:TabPanel runat="server" HeaderText="Lista Equipos" ID="TabPanel1" BorderStyle="NotSet">
                    <ContentTemplate>--%>
                    <div class="container">
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="Label5" runat="server" Text="Equipos Conciliados" CssClass="subTitulos"></asp:Label>
                            </div>
                        </div>  
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="btnExportar" runat="server" CssClass="form-control btn btn-default"  Text="Exportar" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="BtnListar" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                            </div>
                        </div>                       
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-9">
                                        <asp:Label ID="lblInventario" runat="server" CssClass="control-label-2" Text="Inventario" />                                               
                                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control">
                                        </asp:DropDownList>                                            
                                    </div>
                                </div>    
                                <div class="row">
                                    <div class="col-md-3">  
                                        <asp:Label ID="lblNroPlaca" runat="server" CssClass="control-label-2" Text="Nro. Placa"></asp:Label>    
                                        <asp:TextBox ID="txtNroPlaca" runat="server" CssClass="form-control"></asp:TextBox>                                     
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="lblNroSerie" runat="server" CssClass="control-label-2" Text="Nro. Serie"></asp:Label>
                                        <asp:TextBox ID="txtNroSerie" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>  
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:CheckBox ID="chckArea" runat="server" AutoPostBack="True" CssClass="checkbox checkbox-inline" Text="Area" />
                                    </div>
                                    <div class="col-md-9">
                                        <asp:RadioButton ID="RBAlmacen" runat="server" Checked="true" Enabled="false" GroupName="ubicacion" Text="Almacén" />
                                        <asp:RadioButton ID="RBSeccion" runat="server" Enabled="false" GroupName="ubicacion" Text="Sección" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2">
                                        <asp:TextBox ID="txtArea" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button ID="BtnBuscaAlmacen" runat="server" ControlStyle-CssClass="btn btn-block" Enabled="false" Text="..." />
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtDescripcionArea" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row" runat="server" visible ="false">
                                    <div class="col-md-3">  
                                        <asp:Label ID="Label6" runat="server" CssClass="control-label-2" Text="Est. Inventario"></asp:Label>    
                                            <asp:DropDownList ID="DdlEstInventario" runat="server" CssClass="form-control">       
                                            </asp:DropDownList>                          
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label9" runat="server" CssClass="control-label-2" Text="Est.Conciliación"></asp:Label>
                                        <asp:DropDownList ID="DdlEstConciliacion" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="row">
                                    <div class="col-md-1">  
                                        <asp:CheckBox ID="chckCodArticulo" runat="server" AutoPostBack="True" CssClass="checkbox checkbox-inline" Text="Articulo" />                       
                                    </div>
                                </div> 
                                <div class="row">
                                    <div class="col-md-2">    
                                        <asp:TextBox ID="txtCodArticulo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>                     
                                    </div>
                                    <div class="col-md-1">   
                                        <asp:Button ID="BtnBuscaArticulo" runat="server" ControlStyle-CssClass="btn btn-block" Enabled="false" Text="..." />                   
                                    </div>
                                    <div class="col-md-6">   
                                        <asp:TextBox ID="txtDescArticulo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>                   
                                    </div>
                                </div> 
                                <br />
                                <div class="row">
                                    <div class="col-lg-2">
                                        <asp:CheckBox ID="chckDescripcion" runat="server" AutoPostBack="True"  CssClass="checkbox checkbox-inline" Text="Descripcion" />
                                    </div>
                                    <div class="col-lg-7">
                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2">
                                        <asp:CheckBox ID="chckUbicacion" runat="server" AutoPostBack="True"  CssClass="checkbox checkbox-inline" Text="Ubicación" />
                                    </div>
                                    <div class="col-lg-7">
                                        <asp:DropDownList ID="DdlUbicacion" runat="server" CssClass="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row" runat="server" visible ="false">
                                    <div class="col-lg-3">
                                    <asp:CheckBox ID="chckClasificacion" runat="server" AutoPostBack="True" CssClass="checkbox checkbox-inline" Text="Clasificación" />
                                    </div>
                                </div>
                                <div class="row" runat="server" visible ="false">
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtClasificacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button ID="BtnBuscaClasificacionM" runat="server" ControlStyle-CssClass="btn btn-block" Enabled="false" Text="..." />
                                    </div>
                                </div>
                                <div class="row">                                                
                                    <asp:Label ID="lblCodUbica" runat="server" Text="" Visible ="false" ></asp:Label>
                                    <asp:Label ID="lblcodUbicaInv" runat="server" Text="" Visible ="false" ></asp:Label>
                                </div>
                                <div class="row" runat="server" visible="false" >
                                    <div class="col-md-12">
                                        <asp:GridView ID="GvListaResumen" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover GridView">
                                            <Columns>
                                                <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" />
                                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" SortExpression="cantidad" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>                                         
                                <div class="row">
                                    <div class="col-lg-12">                                            
                                        <asp:Label ID="LblSerieNum" runat="server" Visible="false" />
                                        <asp:Label ID="LblCodClasificacionBA" runat="server" Visible="false" />
                                    </div>
                                </div>                                  
                                <hr />


                                <div class="row">
                                    <div class="col-lg-12">                   
                                        <div id="accordion" role="tablist" aria-multiselectable="true" runat="server" >
                                            <div class="card">
                                                <div class="card-header" role="tab" id="section1HeaderId">
                                                    <h5 class="mb-0">                            
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="false" aria-controls="section1ContentId">
                                                           Inventariados
                                                        </a>
                                                    </h5>
                                                </div>
                                                <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                                                    <div class="card-body"> 
                                                        <div class="row">
                                                            <div class="col-lg-12">                                            
                                                                <asp:Label ID="lblRegistroInv" runat="server"  CssClass="control-label-2" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <asp:GridView ID="GvListaConciliados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                                    <Columns>
                                                                        <%--<asp:ButtonField CommandName="CambiarEstado" Text="Cambiar Estado" />--%>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkPag" runat="server" Height="20px" Width="1px" />                                                                      
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
                                                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                                        <asp:TemplateField HeaderText="Est. Inventario">
                                                                            <ItemTemplate>
                                                                                <div class="two-lines-cell">
                                                                                    <%# Eval("ESTADO_INVENTARIO") %>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="UBICACION_DESCRIPCION" HeaderText="Ubicacion" SortExpression="UBICACION_DESCRIPCION" />
                                                                        <asp:BoundField DataField="INVENT_UBIC_PLACA_NRO" HeaderText="Placa Conciliada" SortExpression="INVENT_UBIC_PLACA_NRO" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="card">
                                                <div class="card-header" role="tab" id="section1HeaderId2">
                                                    <h5 class="mb-0">
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId2" aria-expanded="false" aria-controls="section1ContentId2">
                                                            Equipos Conciliados
                                                        </a>
                                                    </h5>
                                                </div>
                                                <div id="section1ContentId2" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId2">
                                                    <div class="card-body">  
                                                        <div class="row">                    
                                                            <div class="col-md-12">
                                                                <asp:Label ID="lblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>   
                                


                                <div class="row">
                                    <div class="col-lg-12">                                            
                                        <asp:Label ID="lblNuevos" runat="server"  CssClass="control-label-2" />
                                    </div>
                                </div>
                                <div class="row">                    
                                    <div class="col-md-12">
                                        <asp:GridView ID="GvListaVerificarInventarioNuevos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="True">
                                            <Columns>
                                                <asp:ButtonField CommandName="Ingresar" Text="Ingresar Dueño" />
                                                <asp:ButtonField CommandName="Conciliar" Text="Conciliar" />
                                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                                                <asp:BoundField DataField="NOMUSUARIO" HeaderText="Usuario" SortExpression="NOMUSUARIO" />
                                                <asp:BoundField DataField="SERIE_NUMERAR" SortExpression="SERIE_NUMERAR">
                                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicación" SortExpression="AREA_NOMBRE" />
                                                <asp:TemplateField ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("ART_CODIGO") IsNot DBNull.Value, Eval("ART_CODIGO"), Nothing))) %>' Width="100" />
                                                        </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="invdet_serie_area" SortExpression="invdet_serie_area">
                                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" SortExpression="INVDET_INVENTUBIC_CODIGO">
                                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVENTUBIC_UBIC_TIPO" SortExpression="INVENTUBIC_UBIC_TIPO">
                                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVENTUBIC_UBIC_CODIGO" SortExpression="INVENTUBIC_UBIC_CODIGO">
                                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div> 
                                </div> 
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="psCodUbicacionArea" runat="server" CssClass="control-label-2" Visible ="false"  />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblRegistroCant" runat="server" CssClass="control-label-2" />
                                    </div>
                                </div>
                                <div class="row form-group-lg">                                    
                                    <div class="col-lg-12">
                                        <asp:GridView ID="GvListaCantidadesXActivos" runat="server" AutoGenerateColumns="False" AllowSorting="true"  CssClass="table table-bordered GridView" Visible="false" >
                                            <Columns>
                                                <asp:ButtonField CommandName="Detalle" Text="Detalle" />
                                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripcíon" SortExpression="ART_DESCRIPCION" />
                                                <asp:BoundField DataField="CANT" HeaderText="Cantidad" SortExpression="CANT" />
                                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Código" SortExpression="COD_ARTICULO" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>                    
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GvBuscAlmacen" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="GvListaVerificarInventarioNuevos" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="BtnGuardarEST" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="SelectedNodeChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chckArea" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

    
                    </div>
                           
                         <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                            <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="Detalle" />
                                        </div>
                                        <div class="form-horizontal">
                                            <div class="modal-body" style="padding: 20px 10px 0;">
                                                <div class="panel-group" id="step44">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">   
                                                            <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <asp:Button ID="BtnCerrarModal" runat="server" class="form-control btn btn-default" Text="Cerrar"/>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row form-group col-md-12">
                                                                        <asp:Label ID="lblRegDet" runat="server" CssClass="EstiloLabel" Font-Bold="True" Font-Italic="False" ForeColor="Maroon"></asp:Label>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-9"> 
                                                                            <asp:GridView ID="GvListaVerificarInventario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                                                                    <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                                                    <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                                                    <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                                                                    <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>  
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="GvListaCantidadesXActivos" EventName="RowCommand" />
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

                        <div id="ModalConciliar" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                        <asp:Label runat="server" ID="Label2" Text="Conciliación de Bienes" />    
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="modal-body" style="padding: 20px 10px 0;">
                                            <input type="hidden" name="metodo" value="registrarP" />
                                            <div class="panel-group" id="step11">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>   
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="lblModalEt1" runat="server" Text="Código Bien" CssClass="control-label-2"></asp:Label>
                                                                        <asp:TextBox ID="txtModalArtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:Label ID="lblModalEt2" runat="server" Text="Descripción Bien" CssClass="control-label-2"></asp:Label>
                                                                        <asp:TextBox ID="txtModalArtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="lblModalEt4" runat="server" Text="Nro. de Serie" CssClass="control-label-2"></asp:Label>
                                                                        <asp:TextBox ID="txtModalNroSerie" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="lblModalEt5" runat="server" Text="Nro. de Placa" CssClass="control-label-2"></asp:Label>
                                                                        <asp:TextBox ID="txtModalNroPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="lblModalEt3" runat="server" Text="Conciliar" CssClass="control-label-2" ForeColor="White" ></asp:Label>
                                                                        <asp:Button ID="BtnModalConciliar" runat="server" Text="Conciliar" CssClass="form-control btn btn-default" />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <asp:Label ID="lblModalSerieNumerar" runat="server" Text="" Visible ="false" ></asp:Label>
                                                                   <asp:Label ID="lblCodInventarioUbica" runat="server" Text="" Visible ="false" ></asp:Label>
                                                                   <asp:Label ID="lblModalUbicaTipo" runat="server" Text="" Visible ="false" ></asp:Label>
                                                                   <asp:Label ID="lblModalUbicaCodigo" runat="server" Text="" Visible ="false" ></asp:Label>
                                                                </div>
                                                                <hr />
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="Label3" runat="server" Text="Código Bien" CssClass="control-label-2"></asp:Label>
                                                                        <asp:TextBox ID="TxtBusModalArtCod" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-sm-9">
                                                                        <asp:Label ID="Label8" runat="server" Text="Descripción Bien" CssClass="control-label-2"></asp:Label>
                                                                        <asp:TextBox ID="TxtBusModalArtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="Label7" runat="server" Text="Buscar" CssClass="control-label-2" ForeColor="White" ></asp:Label>
                                                                        <asp:Button ID="BtnModalBuscar" runat="server" Text="Buscar" CssClass="form-control btn btn-default" />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="lblModalEt6" runat="server" Text="Cerrar" CssClass="control-label-2" ForeColor="White" ></asp:Label>
                                                                        <asp:Button ID="BtnModalCerrar" runat="server" Text="Cerrar" CssClass="form-control btn btn-default" />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:Label ID="Label1" runat="server" Text="Cerrar" CssClass="control-label-2" ForeColor="White" ></asp:Label>
                                                                        <asp:Button ID="BtnListarNE" runat="server" Text="De Otras Oficinas" CssClass="form-control btn btn-default"/>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="GvListaVerificarInventarioNuevos" EventName="RowCommand" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnModalBuscar" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>     
                                                            <div class="row espacio">                    
                                                                <div class="col-md-12">
                                                                    <asp:Label ID="LblCantNE" runat="server" class="control-label-2" Text="" ></asp:Label>
                                                                </div> 
                                                            </div>    
                                                            <div class="row">                    
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="gvNEUsuario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkPag" runat="server" Height="20px" Width="1px" />                                                                      
                                                                                </ItemTemplate>
                                                                                <ControlStyle Width="20px"></ControlStyle>
                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie" SortExpression="SERIE_NRO" />
                                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa" SortExpression="PLACA_NRO" />
                                                                            <asp:BoundField DataField="CodUbicacion" HeaderText="Cod. Oficina" SortExpression="CodUbicacion" />
                                                                            <asp:BoundField DataField="UBICACION" HeaderText="Oficina" SortExpression="UBICACION" />
                                                                            <asp:BoundField DataField="SERIE_NUMERAR" SortExpression="SERIE_NUMERAR">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="INVNE_INVUBICA_CODIGO" SortExpression="INVNE_INVUBICA_CODIGO">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="INVNE_UBICA_TIPO" SortExpression="INVNE_UBICA_TIPO">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="INVNE_UBICA_CODIGO" SortExpression="INVNE_UBICA_CODIGO">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>      
                                                                            <asp:BoundField DataField="SERIE_STATUSU" HeaderText="SERIE_STATUSU" SortExpression="SERIE_STATUSU" />           
                                                                            <asp:TemplateField ItemStyle-Width="20px">
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("ART_CODIGO") IsNot DBNull.Value, Eval("ART_CODIGO"), Nothing))) %>' Width="100" />
                                                                                    </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div> 
                                                            </div> 
                                                            <div class="row espacio">    
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="gvNoInventariado" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkPag" runat="server" Height="20px" Width="1px" />                                                                      
                                                                                </ItemTemplate>
                                                                                <ControlStyle Width="20px"></ControlStyle>
                                                                                <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                                                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                                            <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                                                                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" SortExpression="Ubicacion" />
                                                                            <asp:BoundField DataField="SERIE_NUMERAR" SortExpression="SERIE_NUMERAR">
                                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" SortExpression="INVDET_INVENTUBIC_CODIGO">
                                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                                            </asp:BoundField>     
                                                                            <asp:BoundField DataField="ubica_tipo" SortExpression="ubica_tipo">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField DataField="ubica_codigo" SortExpression="ubica_codigo">
                                                                                <ItemStyle ForeColor="White" Width="1px" />
                                                                            </asp:BoundField>                           
                                                                            <asp:BoundField DataField="SERIE_STATUSU" HeaderText="Stat-Usu" SortExpression="SERIE_STATUSU" />                   
                                                                            <asp:TemplateField ItemStyle-Width="20px">
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("ART_CODIGO") IsNot DBNull.Value, Eval("ART_CODIGO"), Nothing))) %>' Width="100" />
                                                                                    </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>   
                                                                </div>     
                                                            </div>    
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="GvListaVerificarInventarioNuevos" EventName="RowCommand" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnModalBuscar" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnListarNE" EventName="Click" />
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
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                                <asp:Label runat="server" ID="TituloPopupMM" Text="Búsqueda" />                                            
                                        </ContentTemplate>
                                        <Triggers>
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
                                                        <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                                                <div class="row form-group col-md-12">
                                                                    <label class="col-lg-3 control-label" for="id_lugarHecho">Descripción :</label>
                                                                    <div class="col-sm-5 col-xs-5">
                                                                        <input class="form-control" id="BuscarDescripcionMM" type="text" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                                        <asp:Button ID="BtnBuscarMM" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                                    </div>
                                                                </div>
                                                                <div class="row form-group col-md-12">
                                                                    <label class="col-lg-3 control-label" for="id_lugarHecho">Código :</label>
                                                                    <div class="col-sm-3 col-xs-5">
                                                                        <input class="form-control" id="BuscarCodigoMM" type="text" runat="server" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-2 col-lg-offset-3">
                                                                        <asp:Button ID="BtnCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                                    </div>
                                                                </div>                                                            
                                                            </ContentTemplate>
                                                            <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                            </Triggers>
                                                            </asp:UpdatePanel>
                                                        <div class="row form-group col-md-12">
                                                            <div class="col-lg-5 col-lg-offset-1">
                                                                <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional"><ContentTemplate>
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
                                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarMM" EventName="Click" />
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

                        <div id="ModalAlmacen" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
                            <div class="modal-dialog modal-md">
                                <div class="modal-content">
                                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                                <asp:Label ID="TituloPopup" runat="server" Text="Búsqueda" />
                                            
                                        </ContentTemplate>
                                        <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="BtnBuscaAlmacen" EventName="Click" />
                                        </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="modal-body" style="padding: 20px 10px 0;">
                                            <input type="hidden" name="metodo" value="registrarP" />
                                            <div id="step12" class="panel-group">
                                                <div class="panel panel-default">
                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                                            <div class="panel-body">
                                                                <div class="row form-group col-md-12">
                                                                    <label class="col-lg-3 control-label" for="id_lugarHecho">
                                                                        Descripción :</label>
                                                                    <div class="col-sm-5 col-xs-5">
                                                                        <input id="BuscarDescripcion" runat="server" class="form-control" type="text" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                                        <asp:Button ID="BtnBuscar" runat="server" CssClass="btn btn-default" Text="Buscar" />
                                                                    </div>
                                                                </div>
                                                                <div class="row form-group col-md-12">
                                                                    <label class="col-lg-3 control-label" for="id_lugarHecho">
                                                                        Código :</label>
                                                                    <div class="col-sm-3 col-xs-5">
                                                                        <input id="BuscarCodigo" runat="server" class="form-control" type="text" />
                                                                        &nbsp;&nbsp;
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-2 col-lg-offset-3">
                                                                        <asp:Button ID="BtnCerrarAlmacen" runat="server" CssClass="btn btn-default" Text="Cerrar" />
                                                                    </div>
                                                                </div>
                                                                <div class="row col-md-12">
                                                                    <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="GvBuscAlmacen" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
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
                                                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarAlmacen" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>                                                        
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaArticulo" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="modal-body" style="padding: 20px 10px 0;">
                                            <div class="panel-group">
                                                <div class="panel panel-default">

                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional"><ContentTemplate>
                                                            <div class="panel-body">
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
                                                                    <asp:Label ID="LblCodClasModal" runat="server" CssClass="control-label" Visible="false" />
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
                                                            </div>                                                        
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

                        <div id="ModalClasificacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                            <div class="modal-dialog modal-md">
                                <div class="modal-content">
                                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label runat="server" ID="TituloClasificacion" />                                            
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscaClasificacionBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscaClasificacionM" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="modal-body" style="padding: 20px 10px 0;">
                                            <div class="panel-group">
                                                <div class="panel panel-default">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="panel-body">
                                                                <div class="row form-group col-md-12">
                                                                    <div class="col-lg-6 col-lg-offset-4">
                                                                        <asp:Button ID="BtnBuscaClasificacion" class="btn btn-primary" runat="server" Text="Buscar" />
                                                                        <asp:Button ID="BtnCerrarClasificacion" class="btn btn-primary" runat="server" Text="Cerrar" />
                                                                    </div>
                                                                </div>
                                                                <asp:TreeView ID="TrvClasificacion" runat="server" ShowExpandCollapse="true" ShowLines="True"
                                                                    PopulateNodesFromClient="true" ExpandDepth="0">
                                                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                                    <Nodes>
                                                                    </Nodes>
                                                                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                                    <ParentNodeStyle Font-Bold="False" />
                                                                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                                                                </asp:TreeView>
                                                            </div>                                                        
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaClasificacionM" EventName="Click" />
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

                        <div id="ModalCambiarEstado" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label ID="Label4" runat="server" Text="Búsqueda" />                                            
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvListaConciliados" EventName="RowCommand" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="modal-body" style="padding: 20px 10px 0;">
                                            <input type="hidden" name="metodo" value="registrarP" />
                                            <div id="step4" class="panel-group">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="row form-group col-md-12">
                                                            <asp:Label ID="lblEstInventarioEST" runat="server" CssClass="col-lg-3 control-label" Text="Est. Inventario"></asp:Label>
                                                            <div class="col-lg-5">
                                                                <asp:DropDownList ID="DdlEstInventarioEST" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="TxtMensajeError" runat="server" Visible="true"></asp:Label>                                                                
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="BtnGuardarEST" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <asp:Button ID="BtnGuardarEST" runat="server" CssClass=" btn btn-default" Text="Guardar" />
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="row form-group col-md-12">
                                                                    <asp:CheckBox ID="ChckEstConciliacionEST" runat="server" AutoPostBack="True" CssClass="col-lg-3 control-label" Text="Est. Conciliación" />
                                                                    <div class="col-lg-5">
                                                                        <asp:DropDownList ID="DdlEstConciliacionEST" runat="server" CssClass="form-control" Enabled="False">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <asp:Button ID="BtnCerrarEST" runat="server" CssClass=" btn btn-default" Text="Cerrar" />
                                                                </div>                                                            
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>           
                        
                   <%-- </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Conciliar" ID="TabPanel2">
                    <ContentTemplate>
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="Conciliacion de Equipos Inventariados" CssClass="subTitulos"></asp:Label><br>
                        <br />
                        <div class="container-fluid">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">                                    
                                            <asp:GridView ID="GvListaConciliadosCON" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-condensed small-top-margin GridView">
                                                <Columns>
                                                    <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripcíon Articulo" SortExpression="ART_DESCRIPCION" />
                                                    <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro.Serie" SortExpression="SERIE_NRO" />
                                                    <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro.Placa" SortExpression="PLACA_NRO" />
                                                    <asp:BoundField DataField="SERIE_COD_RELACIONADO" HeaderText="Cod. Relacionado" SortExpression="SERIE_COD_RELACIONADO" />
                                                    <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />
                                                    <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Ubicación" SortExpression="COD_ALMACEN" />
                                                    <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación" SortExpression="ALMACEN_NOMBRE" />
                                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                    <asp:BoundField DataField="EST_CONCILIACION" HeaderText="Conciliado" SortExpression="EST_CONCILIACION" />
                                                    <asp:BoundField DataField="EST_INVENTARIO" HeaderText="Inventariado" SortExpression="EST_INVENTARIO" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">  
                                            <asp:Label ID="LblConciliar" runat="server" CssClass="control-label-2" Text="Que desea hacer:" Visible="False"></asp:Label>
                                            <asp:DropDownList ID="DdlConciliar" runat="server" CssClass="form-control" Visible="False">
                                                <asp:ListItem Value="1">Ingresar Codigo Relacionado</asp:ListItem>
                                                <asp:ListItem Value="2">Reemplazar el Articulo y Cod. Relacionado</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">  
                                            <asp:Button ID="BtnConciliar" runat="server" CssClass="form-control btn btn-default" Text="Conciliar" Visible="False" />
                                        </div>
                                        <div class="col-md-3">  
                                            <asp:Button ID="BtnCancelarConciliar" runat="server" CssClass="form-control btn btn-default" Text="Cancelar" Visible="False" />
                                        </div>
                                    </div>
                                    <div class="row">                                        
                                        <asp:Label ID="LblSerieNumNoInv" runat="server" Visible="False"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">  
                                            <asp:Label ID="lblDescripcionCON" runat="server" CssClass="control-label-2" Text="Descripción"></asp:Label>
                                            <asp:TextBox ID="txtDescripcionCON" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">  
                                            <asp:Label ID="lblNroSerieCON" runat="server" CssClass="control-label-2" Text="Nro. Serie"></asp:Label>
                                            <asp:TextBox ID="txtNroSerieCON" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">  
                                            <asp:Button ID="BtnListarCON" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                                        </div>
                                        <div class="col-md-2">  
                                            <asp:Button ID="BtnConciliarCON" runat="server" CssClass="form-control btn btn-default" Text="Conciliar" />
                                        </div>
                                    </div>
                                    <div class="row">                                        
                                        <div class="col-md-12">
                                            <asp:Label ID="TituloListaEquiposNoInventariado" runat="server" CssClass="subTitulos" Text="Equipos no Inventariados" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">                                        
                                        <div class="col-md-12">
                                            <asp:GridView ID="GvListaEquiposNoInventariado" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-condensed small-top-margin GridView">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="Check" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                    <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                    <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                    <asp:BoundField DataField="SERIE_COD_RELACIONADO" HeaderText="Cod. Relacionado" SortExpression="SERIE_COD_RELACIONADO" />
                                                    <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />
                                                    <asp:BoundField DataField="UBICACT_CODIGO" HeaderText="Cod. Ubicación" SortExpression="UBICACT_CODIGO" />
                                                    <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación" SortExpression="ALMACEN_NOMBRE" />
                                                    <asp:BoundField DataField="SERIE_NUMERAR" HeaderText="Serie Numerar" SortExpression="SERIE_NUMERAR" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                                <asp:AsyncPostBackTrigger ControlID="BtnConciliarCON" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCancelarConciliar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnListarCON" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                         <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="col-lg-6">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-4">
                                    </div>
                                </div>
                                <br />
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>                    
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
