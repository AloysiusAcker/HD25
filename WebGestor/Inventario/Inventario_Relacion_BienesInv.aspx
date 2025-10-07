<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Relacion_BienesInv.aspx.vb" Inherits="Inventario_Inventario_Relacion_BienesInv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div class="row">
            <div class="col-lg-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Informe Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-lg-3">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" OnClick="BtnListar_Click" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-3">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar Detalle" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-3">
                <asp:Button ID="BtnListarPlacdos" runat="server" Text="Listar Bienes x Placar" CssClass="form-control btn btn-default"/>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-3">
                <asp:Button ID="BtnInforme" runat="server" Text="Informe" CssClass="form-control btn btn-default" Visible ="false" />
            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red"/>
                    </div> 
                </div> 
                <div class="row">
                    <div class="col-lg-9">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RbTodos" runat="server" Text="Todos"  Checked="true" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-6">
                        <asp:Label ID="lblEtiquetaUbi" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div> 
                <div class="row espacio">
                    <div class="col-md-6">
                        <asp:Label ID="LblPlaca" runat="server" Text="Placa" CssClass="control-label-2" />
                        <asp:Textbox ID="TxtPlacaNro" runat="server" AutoPostBack="True" OnTextChanged="TxtPlacaNro_TextChanged" CssClass="form-control">
                        </asp:Textbox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-lg-12">
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
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="gvResumen" runat="server" Width="50%" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <%--<asp:BoundField DataField="Estado_Inventario" HeaderText="Estado" SortExpression="Estado_Inventario" />--%>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <div class="two-lines-cell">
                                            <%# Eval("Estado_Inventario") %>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INVDET_ESTADO_INVENTARIO" SortExpression="INVDET_ESTADO_INVENTARIO">
                                    <ItemStyle ForeColor="White" Width="0%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>             

                 <div class="row">                    
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistro2" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="gvListaBienesxPlacar" runat="server" AutoGenerateColumns="False" width="100%" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="Cod_Ubicacion" HeaderText="Cod. Interno" SortExpression="Cod_Ubicacion" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Oficina" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="UBICACION_DESCRIPCION" HeaderText="Ubicacion" SortExpression="UBICACION_DESCRIPCION" />
                                <asp:BoundField DataField="art_codigo" HeaderText="Cod. Artículo" SortExpression="art_codigo" />
                                <asp:BoundField DataField="art_codequiva" HeaderText="Nro. Parte" SortExpression="art_codequiva" />
                                <asp:BoundField DataField="art_descripcion" HeaderText="Desc. Artículo" SortExpression="art_descripcion" />
                                <asp:BoundField DataField="Serie_Nro" HeaderText="Serie Nro" SortExpression="Serie_Nro" />
                                <asp:BoundField DataField="Placa_Nro" HeaderText="Placa Nro" SortExpression="Placa_Nro" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CON_CAJA" HeaderText="Con Caja" SortExpression="CON_CAJA" />
                                <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="gvListaxPlaca" runat="server" AutoGenerateColumns="False" width="100%" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Placado" Text="Placado" ButtonType="Button">
                                    <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="Cod_Ubicacion" HeaderText="Cod. Interno" SortExpression="Cod_Ubicacion" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Oficina" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="UBICACION_DESCRIPCION" HeaderText="Ubicacion" SortExpression="UBICACION_DESCRIPCION" />
                                <asp:BoundField DataField="art_codigo" HeaderText="Cod. Artículo" SortExpression="art_codigo" />
                                <asp:BoundField DataField="art_codequiva" HeaderText="Nro. Parte" SortExpression="art_codequiva" />
                                <asp:BoundField DataField="art_descripcion" HeaderText="Desc. Artículo" SortExpression="art_descripcion" />
                                <asp:BoundField DataField="Serie_Nro" HeaderText="Serie Nro" SortExpression="Serie_Nro" />
                                <asp:BoundField DataField="Placa_Nro" HeaderText="Placa Nro" SortExpression="Placa_Nro" />
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CON_CAJA" HeaderText="Con Caja" SortExpression="CON_CAJA" />
                                <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="GvListaVerificarInventario" runat="server" AutoGenerateColumns="False" width="100%" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="PLACA_ORIGINAL" HeaderText="Nro. Placa" SortExpression="PLACA_ORIGINAL" />
                                <asp:BoundField DataField="CAF" HeaderText="CAF" SortExpression="CAF" />
                                <asp:BoundField DataField="SUB" HeaderText="SUB" SortExpression="SUB" />
                                <asp:BoundField DataField="serie_denominacion" HeaderText="serie_denominacion" SortExpression="serie_denominacion" />
                                <asp:BoundField DataField="SERIE_ORIGINAL" HeaderText="Nro. Serie" SortExpression="SERIE_ORIGINAL" />
                                <asp:BoundField DataField="Cod_Ubicacion" HeaderText="Centro Costos" SortExpression="Cod_Ubicacion" />
                                <asp:BoundField DataField="serie_modelo" HeaderText="Modelo" SortExpression="serie_modelo" />
                                <asp:BoundField DataField="serie_marca" HeaderText="Marca" SortExpression="serie_marca" />
                                <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="UBICACION_DESCRIPCION" HeaderText="Ubicacion" SortExpression="UBICACION_DESCRIPCION" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Material" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado Inv." SortExpression="ESTADO_INVENTARIO" />
                                <asp:BoundField DataField="CON_CAJA" HeaderText="Con Caja" SortExpression="CON_CAJA" />
                                <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="gvResumen" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="TxtPlacaNro" EventName="TextChanged" />
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

