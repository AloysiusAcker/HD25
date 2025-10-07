<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Verificar_Masivo.aspx.vb" Inherits="Inventario_Inventario_Verificar_Masivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Verificación Masiva de Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-9 col-xs-6">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6">
                       <asp:Label ID="Label11" runat="server" class="control-label-2" Text="Listar" forecolor="White" ></asp:Label>
                       <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                </div>        
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBUbicaciones" runat="server" Text="Ubicaciones" AutoPostBack="True" />
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
                    <div class="col-md-3 col-xs-12">                    
                        <input id="btnOpen" type="button" value="Datos Oficina" runat="server" class="form-control btn btn-default" />
                    </div> 
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:TextBox ID="TxtCodigoAyudaUbicacion" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TxtCodigoAyuda" runat="server" Width="102px" Visible="false"></asp:TextBox>
                    </div> 
                </div>   
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DdlInventario" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>     
        <div class="row" runat="server" visible ="true" id="Carga"> 
            <div class="col-lg-4">
                <asp:Label ID="Label12" CssClass="control-label-2" runat="server" Text="Archivo" ></asp:Label>
                <asp:FileUpload ID="fileUpload" runat="server"  CssClass="form-control" />
            </div> 
            <div class="col-lg-2">
                <asp:Label ID="Label10"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnCargarArchivo" runat="server" CssClass="form-control btn btn-default" Text="Carga Placas" OnClick="BtnCargarArchivo_Click" />
            </div>                  
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblEtiquetaUbi" runat="server" Text="Ubicación :" CssClass="control-label-2"  />
                        <asp:DropDownList ID="ddlUbicacion" runat="server"  CssClass="form-control autocomplete" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div> 

                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>    
                               
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblPlacaNoExite" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>   
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="gvPlacaNoExite" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro." SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
                                <asp:BoundField DataField="Hora" HeaderText="Hora" SortExpression="Hora" />
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                                <asp:BoundField DataField="UBICACION_CODIGO" HeaderText="Ubicación" SortExpression="UBICACION_CODIGO" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="LblContador" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="gvListaTop5" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <%--<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />--%>
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
                                <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                                <asp:TemplateField HeaderText="Est. Inventario">
                                    <ItemTemplate>
                                        <div class="two-lines-cell">
                                            <%# Eval("ESTADO_INVENTARIO") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
            
            
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro2" runat="server" class="control-label-2" Text="" visible="false" ></asp:Label>
                    </div> 
                </div>                          
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvListaVerificarInventarioOtros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >
                            <Columns>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                                    <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>                                         
                
                <div class="row">
                    <div class="col-lg-12">                   
                        <div id="accordion" role="tablist" aria-multiselectable="true" runat="server" visible="false" >
                            <div class="card">
                                <div class="card-header" role="tab" id="section1HeaderId">
                                    <h5 class="mb-0">                            
                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="false" aria-controls="section1ContentId">
                                           Inventariado Ok
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                                    <div class="card-body">                                 
                                       <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="lblRegistraTodo" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:GridView ID="GvListaVerificarInventario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <%--<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />--%>
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
                                                        <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                                        <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                                                        <asp:TemplateField HeaderText="Est. Inventario">
                                                            <ItemTemplate>
                                                                <div class="two-lines-cell">
                                                                    <%# Eval("ESTADO_INVENTARIO") %>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                                                        <asp:BoundField DataField="SERIE_STATUSU" HeaderText="Stat. Sist." SortExpression="SERIE_STATUSU" />
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
                                            No Inventariado
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId2" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId2">
                                    <div class="card-body">  
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="lblContador2" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvListaNoInventariado" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <%--<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />--%>
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
                                                        <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                                        <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                                                        <asp:TemplateField HeaderText="Est. Inventario">
                                                            <ItemTemplate>
                                                                <div class="two-lines-cell">
                                                                    <%# Eval("ESTADO_INVENTARIO") %>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SERIE_STATUSU" HeaderText="Stat. Sist." SortExpression="SERIE_STATUSU" />
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

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DdlInventario" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
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

