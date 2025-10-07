<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Stock_Articulos.aspx.vb" Inherits="Inventario_Stock_Articulos" title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
   <%-- <div class="container-fluid">--%>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="Label1" runat="server" Text="Stock de Articulos" CssClass="Titulos"></asp:Label><br/><br/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Button ID="btnListar" runat="server" Text="Listar"  CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-md-3">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-md-3">
                <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar"  CssClass="form-control btn btn-default"/>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblError" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Red" CssClass="control-label-2"></asp:Label>
                    </div> 
                </div>
	            <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblArticulo" runat="server" Text="Almacen" CssClass="control-label-2" ></asp:Label>
                        <asp:DropDownList ID="cboAlmacen" runat="server"  CssClass="form-control">
                        </asp:DropDownList>
                    </div>   
                </div>
                <div class="row">
                    <div class="col-md-5">
                        <asp:Label ID="lblClasificacion" runat="server" Text="Clasificación :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtClasificacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label4" runat="server" Text="Buscar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnBuscarClas" runat="server" CssClass="form-control btn btn-default" Text="..." />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="Artículos" CssClass="control-label-2" ></asp:Label>
                        <asp:TextBox ID="TxtCodArt" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label5" runat="server" Text="Buscar" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnBuscar" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="NombreArt" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:TextBox ID="txtNomArt" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblCodClas" runat="server" Class="col-lg-2 control-label" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
                    </div>
                </div>
	            <div class="row">
                    <div class="col-md-12">
                    <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Almac&#233;n">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripci&#243;n">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art&#237;culo">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ART_SKU" HeaderText="SKU">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripci&#243;n">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cant.">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ART_CODEXTERNO" HeaderText="SAP">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                    </asp:GridView>
                </div>
            </div> 
    
            </ContentTemplate>
            <Triggers>
                <asp1:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                <asp1:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />
                <asp1:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="SelectedNodeChanged" />
            </Triggers>
        </asp:UpdatePanel>


      <div id="ModalClasificacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopupp" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarClas" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
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
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
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
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarClas" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnModalBuscarClas" EventName="Click" />
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
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="SelectedNodeChanged" />  
                                            <asp:AsyncPostBackTrigger ControlID="btnCerrarClasificacion" EventName="Click" />  
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
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
                                                        <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo" SortExpression="TIPO_ART"/>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="SelectedNodeChanged" />   
                                                <asp:AsyncPostBackTrigger ControlID="btnCerrarClasificacion" EventName="Click" />
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

        <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarcaBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaModeloBA" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcionM">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnBuscaMarca" class="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigoM">Código :</label>
                                                <div class="col-sm-3 col-xs-5">
                                                    <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="btnCancela" class="btn btn-default" runat="server" Text="Cancelar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancela" EventName="Click" />
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
                                                            <asp:BoundField DataField="codigoInt" HeaderText="Codigo" SortExpression="codigoInt" />
                                                            <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                                            <asp:BoundField DataField="codigo" SortExpression="codigo">
                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarca" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnCancela" EventName="Click" />
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
     
</asp:Content>

