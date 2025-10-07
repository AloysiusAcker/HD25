
<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Lista_BienesInventariado.aspx.vb" Inherits="Inventario_Inventario_Lista_BienesInventariado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Lista Bienes Inventariados" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        
                <div class="row">    
                    <div class="col-lg-2">
                        <asp:Button ID="BtnListar" runat="server" Text="Bienes a Desactivar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-2">
                        <asp:Button ID="BtnListarTodos" runat="server" Text="Bienes Inventariados" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-3">
                        <asp:Button ID="BtnExportar" runat="server" Text="Exportar Bienes Inventariados" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-2">
                        <asp:Button ID="BtnListaNoEncontrados" runat="server" Text="Lista No Encontrados" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-3">
                        <asp:Button ID="BtnExportaNE" runat="server" Text="Exporta No Encontrados" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBTodos" runat="server" Text="Todos"  Checked="true" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-xs-2">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-xs-1">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6 col-xs-6">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:TextBox ID="txtCodCecose" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="txtCodInvUbicacion" runat="server" Width="102px" Visible="false"></asp:TextBox>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-3 col-xs-6">
                        <asp:Label ID="LblNroPlaca" runat="server" class="control-label-2" Text="Nro. Placa :" ></asp:Label>
                        <asp:TextBox ID="TxtNroPlaca" runat="server" CssClass="form-control"  AutoPostBack="True"></asp:TextBox>
                    </div>
                  <%--  <div class="col-md-3 col-xs-6">
                        <asp:Label ID="LblNroSerie" runat="server" class="control-label-2" Text="Nro. Serie :" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtNroSerie" runat="server" CssClass="form-control" Visible="false" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-6">
                        <asp:Label ID="Label1" runat="server" class="control-label-2" Text="Nro. ATM :" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtNroAtm" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-6">                    
                       <asp:Button ID="BtnMostrarModal" runat="server" Text="Datos Oficina" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                        <asp:Label ID="lblBusArticulo" runat="server" class="control-label-2" Text="Cód. Artículo :" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtBusArticulo" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                    </div> --%>
                </div>
                <div class="row">
                    <div class="col-lg-2">
                        <asp:Label ID="Label10" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                        <asp:TextBox ID="TxtFechaIni" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaIni" Format="dd/MM/yyyy" PopupButtonID="TxtFechaIni" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-lg-2">
                        <asp:Label ID="Label11" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                        <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                    </div>
                </div> 

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblEtiquetaUbi" runat="server" Text="Ubicación :" CssClass="control-label-2"  Visible="false" />
                        <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="form-control" AutoPostBack="true"  Visible="false">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="LblContador" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="gvListaTop5" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Desactivar" Text="Desactivar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="inventario_nombre" HeaderText="Inventario" SortExpression="inventario_nombre" />
                                <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo" SortExpression="CentroCosto" />
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
                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                                <asp:BoundField DataField="ACTIVO" HeaderText="Activo" SortExpression="ACTIVO" />
                                <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SERIE_NUMERAR" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SERIE_STATUSU" HeaderText="STATUSU" SortExpression="SERIE_STATUSU" />   
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="GvListarTodo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="Nombre_Inventario" HeaderText="Inventario" SortExpression="Nombre_Inventario" />
                                <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo" SortExpression="CentroCosto" />
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
                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                                <asp:BoundField DataField="FECHA_INV" HeaderText="Fecha" SortExpression="FECHA_INV" />
                                <asp:BoundField DataField="USUARIO_INVENTARIO" HeaderText="Usuario" SortExpression="USUARIO_INVENTARIO" />
                                <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SERIE_NUMERAR" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="GvNoEncontrados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="inventario_nombre" HeaderText="Inventario" SortExpression="inventario_nombre" />
                                <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo" SortExpression="CentroCosto" />
                                <asp:BoundField DataField="CentroCosto_Nombre" HeaderText="Descripción Centro Costo" SortExpression="CentroCosto_Nombre" />
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
                                <asp:TemplateField HeaderText="Est. Inventario">
                                    <ItemTemplate>
                                        <div class="two-lines-cell">
                                            <%# Eval("ESTADO_INVENTARIO") %>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SERIE_NUMERAR" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DdlInventario" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBTodos" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnListarTodos" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvListaTop5" EventName="RowCommand" />
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

