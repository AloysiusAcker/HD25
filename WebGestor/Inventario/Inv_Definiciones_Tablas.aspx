<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Inv_Definiciones_Tablas.aspx.vb" Inherits="Inv_Definiciones_Tablas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
        <cc1:TabPanel ID="Panel1" runat="server" HeaderText="Almacén">
            <ContentTemplate>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblTitulo" runat="server" Text="Almacén" CssClass="subTitulos"></asp:Label>
                    </div>
                </div> 
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="LblEtiq1" CssClass="control-label-2" runat="server" Text="Almacenes :"></asp:Label>
                                <asp:TextBox ID="TxtDescAlmacen" runat="server" CssClass="form-control"></asp:TextBox>
                            </div> 
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq2" CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White" ></asp:Label>
                            <asp:Button ID="BtnListarAlmacen" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq3" CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                            <asp:Button ID="BtnNuevoAlmacen" runat="server" CssClass="form-control btn btn-default" Text="Nuevo" />
                            </div>
                        </div>

                        <div class="row" id="divNuevoAlm"  runat="server" visible="false"  >
                            <div class="col-md-12">
                                <h4>
                                    Nuevo Almacén
                                </h4>
                            </div>  
                        </div>  
                        <div class="row" id="divEditarAlm"  runat="server" visible="false"  >
                            <div class="col-md-12">
                                <h4>
                                    Editar Almacén
                                </h4>
                            </div>  
                        </div>  

                        <div class="row">
                            <div class="col-md-9">
                                <asp:Label ID="LblEtiq4" runat="server" CssClass="control-label-2" Text="Descripción :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtDescripcionAlmacen" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            </div> 
                            <div class="col-md-3">
                            <asp:Label ID="LblEtiq5" runat="server" CssClass="control-label-2" Text="Tipo :" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlTipoAlmacen" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-9">
                                <asp:Label ID="LblEtiq6" runat="server" CssClass="control-label-2" Text="Dirección :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtDireccionAlmacen" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq7" runat="server" CssClass="control-label-2" Text="Modo :" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlModoAlmacen" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    <asp:ListItem Value="1">A</asp:ListItem>
                                    <asp:ListItem Value="2">M</asp:ListItem>
                                    <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                </asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row">                            
                            <div class="col-md-3">
                            <asp:Label ID="LblEtiq8" runat="server" CssClass="control-label-2" Text="Departamento:" Visible="false"></asp:Label>
                                <asp:DropDownList ID="DdlDpto" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>                        
                            <div class="col-md-3">
                            <asp:Label ID="LblEtiq9" runat="server" CssClass="control-label-2" Text="Provincia:" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlProv" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>                        
                            <div class="col-md-3">
                            <asp:Label ID="LblEtiq10" runat="server" CssClass="control-label-2" Text="Distrito:" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlDist" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>                        
                            <div class="col-md-3">
                            <asp:Label ID="LblEtiq11" runat="server" CssClass="control-label-2" Text="De Baja:" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlBajaAlmacen" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="2">NO</asp:ListItem>
                                    <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                </asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="LblEtiq12" runat="server" CssClass="control-label-2" Text="Centro Costo :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtCCCodigoAlmacen" runat="server" CssClass="form-control" Visible="False" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="LblEtiq15" runat="server" CssClass="control-label-2" Text="buscar" ForeColor="White"  Visible="False" ></asp:Label>
                                <asp:Button ID="BtnBuscarCC" runat="server" CssClass="form-control btn btn-default" Text="..." Visible="false" />
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="LblEtiq16" runat="server" CssClass="control-label-2" Text="Descripción"  Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtCCDescripcionAlmacen" runat="server" CssClass="form-control" Visible="False" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="LblEtiq13" runat="server" CssClass="control-label-2" Text="Sección :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtCCSCodigoAlmacen" runat="server" CssClass="form-control" Visible="False" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="LblEtiq17" runat="server" CssClass="control-label-2" Text="Buscar" ForeColor="White"  Visible="False" ></asp:Label>
                                <asp:Button ID="BtnBuscarCCS" runat="server" CssClass="form-control btn btn-default" Text="..." Visible="false" />
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="LblEtiq18" runat="server" CssClass="control-label-2" Text="Descripción"  Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtCCSDescripcionAlmacen" runat="server" CssClass="form-control" Visible="False" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq14" runat="server" CssClass="control-label-2" Text="Ubicación :" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlUbicacionAlmacen" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                </div> 
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq19" runat="server" CssClass="control-label-2" Text="Guardar" ForeColor="White"  Visible="False"></asp:Label>
                                <asp:Button ID="BtnAgregarAlmacen" runat="server" CssClass="form-control btn btn-default" Text="Guardar" Visible="False" />
                                </div>
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq20" runat="server" CssClass="control-label-2" Text="Cancelar" ForeColor="White"  Visible="False"></asp:Label>
                                <asp:Button ID="BtnCancelarAlmacen" runat="server" CssClass="form-control btn btn-default" Text="Cancelar" Visible="False" />
                            </div>
                        </div>
                        <asp:TextBox ID="TxtCodigoCCAyuda" runat="server" Visible="false" />
                        <asp:TextBox ID="TxtCodAlmacen" runat="server" Visible="false" />
                        <asp:TextBox ID="TxtCodigoCCSAyuda" runat="server" Visible="false" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarAlmacen" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarAlmacen" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GvListaAlmacen" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="EditaAlmacen" ImageUrl="~/icono/Editar_opt.png" Text="Editar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminaAlmacen" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="Relacion" Text="Usuarios" ButtonType="Button">
                                            <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ALMACEN_CODIGO" HeaderText="Código" SortExpression="INVENT_CODIGO" />
                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción" SortExpression="ALMACEN_NOMBRE" />
                                        <asp:BoundField DataField="ALMACEN_DIRECCION" HeaderText="Dirección" SortExpression="ALMACEN_DIRECCION" />
                                        <asp:BoundField DataField="CODEXTCC" HeaderText="Cod. CC" SortExpression="CODEXTCC" />
                                        <asp:BoundField DataField="NOMBRECC" HeaderText="Centro Costo" SortExpression="NOMBRECC" />
                                        <asp:BoundField DataField="CODEXTSC" HeaderText="Cod. CCS" SortExpression="CODEXTSC" />
                                        <asp:BoundField DataField="NOMBRESC" HeaderText="Sección CC" SortExpression="NOMBRESC" />
                                        <asp:BoundField DataField="PLANTA" HeaderText="Planta" SortExpression="PLANTA" />
                                        <asp:BoundField DataField="ALMACEN_ACTIVO" HeaderText="Activo" SortExpression="ALMACEN_ACTIVO" />
                                        <asp:BoundField DataField="ALMACEN_BAJA" HeaderText="Baja" SortExpression="ALMACEN_BAJA" />
                                        <asp:BoundField DataField="ALMACEN_MODO" HeaderText="Modo" SortExpression="ALMACEN_MODO" />
                                        <asp:BoundField DataField="MOVIL" HeaderText="Tipo" SortExpression="MOVIL" />
                                    </Columns>
                                </asp:GridView>
                            </div> 
                        </div> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListarAlmacen" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:TextBox ID="codigoAlmacenAyuda" runat="server" Visible="False" Width="293px"></asp:TextBox>

                <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="TituloPopup" runat="server" Text="Buscar" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="BtnBuscarCC" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="BtnBuscarCCS" EventName="Click" />
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
                                                    <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                <Columns>
                                                                    <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="CodInterno" HeaderText="Código" SortExpression="CodInterno" />
                                                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
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
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalMensaje" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="Mensaje" class="col-lg-12" />
                                        </div>
                                        <div class="col-lg-12">
                                            <asp:Button ID="BtnSi" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GvListaAlmacen" EventName="RowCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalUsuario" class="modal fade" role="dialog" data-backdrop="static"  style="position: fixed; top: 0%;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-12 col-sm-6" >
                                            <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="Relación de Almacén con Usuarios" />
                                        </div> 
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GvListaAlmacen" EventName="RowCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div> 
                            <div class="form-horizontal">
                                <div class="modal-body" style="padding: 20px 10px 0;">
                                    <div class="panel-group">
                                        <div class="panel panel-default">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="row espacio">
                                                            <div class="col-md-6">
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Button ID="BtnRelacionCerrar" runat="server" Text="Cerrar" CssClass="form-control btn btn-default" />
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Button ID="BtnRelacionGuardar" runat="server" Text="Guardar" CssClass="form-control btn btn-default" />
                                                            </div>
                                                        </div>
                                                        <div class="row espacio">
                                                            <div class="col-md-3">
                                                                <asp:Label ID="LblEM1" runat="server" Font-Size="14px" CssClass="control-label2" Text="Almacén" />
                                                                <asp:TextBox ID="TxtMCodAlmacen" runat="server" CssClass="form-control"  />
                                                            </div>
                                                            <div class="col-md-9">
                                                                <asp:Label ID="Label7" runat="server" Font-Size="14px" CssClass="control-label2" Text="Almacén" ForeColor ="white" />
                                                                <asp:TextBox ID="TextMAlmacen" runat="server" CssClass="form-control"  />
                                                            </div>
                                                        </div> 
                                                        <div class="row espacio">
                                                            <div class="col-md-12">
                                                                <asp:GridView ID="gvUsuario"  runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                                                            <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                                                            <ItemStyle Height="10px" Width="10px" />
                                                                        </asp:ButtonField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkUsuario" runat="server" CssClass="checkbox checkbox-inline" Font-Bold ="true" AutoPostBack="True" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="USUARI_CODIGO" HeaderText="Código" SortExpression="USUARI_CODIGO" />
                                                                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres y Apellidos" SortExpression="NOMBRES" />
                                                                    </Columns>
                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="GvListaAlmacen" EventName="RowCommand" />
                                                        <asp:AsyncPostBackTrigger ControlID="gvUsuario" EventName="RowCommand" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnRelacionGuardar" EventName="Click" />
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

            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel2" runat="server" HeaderText="Marca">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="LblEtqM1" runat="server" Text="Marca" CssClass="subTitulos"></asp:Label>
                            </div>
                        </div> 
                        <br />
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="LblDescMarca" runat="server" CssClass="control-label-2" Text="Descripción :"></asp:Label>
                                <asp:TextBox ID="TxtDescMarca" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="LblEtqM2" runat="server" CssClass="control-label-2" Text="Listar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnListarMarca" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
                            </div> 
                            <div class="col-md-3">
                                <asp:Label ID="LblEtqM3" runat="server" CssClass="control-label-2" Text="Nuevo" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnNuevaMarca" runat="server" CssClass="form-control btn btn-default" Text="Nuevo" />
                            </div> 
                        </div>
                        <div class="row" id="divMarcaNuevo"  runat="server" visible="false"  >
                            <div class="col-md-12">
                                <h4>
                                    Nueva Marca
                                </h4>
                            </div>  
                        </div>  
                        <div class="row" id="divMarcaEdit"  runat="server" visible="false"  >
                            <div class="col-md-12">
                                <h4>
                                    Editar Marca
                                </h4>
                            </div>  
                        </div>  
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblCodigoMarca" runat="server" CssClass="control-label-2" Text="Código :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtCodigoMarca" runat="server" CssClass="form-control" Enabled="False" Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="LblDescripcionMarca" runat="server" CssClass="control-label-2" Text="Descripción :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtDescripcionMarca" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                </div> 
                            <div class="col-md-3">
                                </div> 
                            <div class="col-md-3">
                                <asp:Label ID="LblEtqM4" runat="server" CssClass="control-label-2" Text="Agregar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnAgregarMarca" runat="server" CssClass="form-control btn btn-default" Text="Agregar" Visible="False" />
                            </div> 
                            <div class="col-md-3">
                                <asp:Label ID="LblEtqM5" runat="server" CssClass="control-label-2" Text="Cancelar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnCancelarMarca" runat="server" CssClass="form-control btn btn-default" Text="Cancelar" Visible="False" />
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GvListaMarcas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="EditaMarca" ImageUrl="~/icono/Editar_opt.png" Text="Editar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminaMarca" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="DetalleMarca" ImageUrl="~/icono/details_opt.png" Text="Detalle">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ARTMAR_CODIGO" HeaderText="Código" SortExpression="ARTMAR_CODIGO" />
                                        <asp:BoundField DataField="ARTMAR_DESCRIPCION" HeaderText="Descripción" SortExpression="ARTMAR_DESCRIPCION" />
                                    </Columns>
                                </asp:GridView>
                            </div> 
                        </div> 
                        <br />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvListaMarcas" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnListarMarca" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarMarca" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarMarca" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
               
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel3" runat="server" HeaderText="Modelo">
            <ContentTemplate>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblEtiqMo1" runat="server" Text="Modelo" CssClass="subTitulos"></asp:Label>
                    </div>
                </div>  
                <br />  
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Marca:"></asp:Label>
                                <asp:DropDownList ID="DdlMarca" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div> 
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnListarModelo" runat="server" Text="Listar" CssClass="form-control btn btn-default" />
                            </div> 
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnNuevoModelo" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label CssClass="control-label-2" runat="server" Visible="False" Text="Descripción :" ID="LblDescripcionModelo" />
                                <input class="form-control" id="TxtDescripcionModelo" type="text" runat="server" visible="False" />
                            </div>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Agregar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnAgregarModelo" runat="server" Text="Agregar" CssClass="btn btn-default" Visible="False" />
                            </div> 
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Cancelar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnCancelarModelo" runat="server" Text="Cancelar" CssClass="btn btn-default" Visible="False" />
                            </div> 
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarModelo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnNuevoModelo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarModelo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" AutoGenerateColumns="False" ID="GvListaModelo" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="EditaModelo" Text="Editar" ImageUrl="~/icono/Editar_opt.png">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="EliminaModelo" Text="Eliminar" ImageUrl="~/icono/delete2_opt.png">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="DetalleModelo" Text="DetalleModelo" ImageUrl="~/icono/details_opt.png">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ARTMAR_CODIGO" HeaderText="Cod. Marca" SortExpression="ARTMAR_CODIGO" />
                                        <asp:BoundField DataField="ARTMOD_CODIGO" HeaderText="Cod. Modelo" SortExpression="ARTMOD_CODIGO" />
                                        <asp:BoundField DataField="ARTMOD_DESCRIPCION" HeaderText="Descripción" SortExpression="ARTMOD_DESCRIPCION" />
                                    </Columns>
                                </asp:GridView>
                            </div> 
                        </div> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarModelo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarModelo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnListarModelo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="row">                    
                    <div class="col-md-12">
                        <input class="form-control" id="codigoModelo" type="text" runat="server" visible="False" />
                    </div>
                </div>
                <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblTituloDetalle" runat="server" Text="Modelo Detalle" CssClass="subTitulos"></asp:Label>
                        </div>
                    </div>  
                    <br />  
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Label ID="lblNomModelo" runat="server" Text="Modelo :" CssClass="control-label-2"></asp:Label>
                                    <asp:TextBox ID="txtNomModelo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                                    <asp:Button ID="btnNuevoDetalle" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCodModDet" runat="server" Text="Cod. Mod Detalle :" CssClass="control-label-2" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtCodigoDetaMo" runat="server" CssClass="form-control" ReadOnly="True" Visible="false"></asp:TextBox>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lbldescripDetalle" runat="server" Text="Descripción :" CssClass="control-label-2" Visible="false" />
                                    <asp:TextBox ID="txtDescripcionDetalle" runat="server" CssClass="form-control" Visible="false" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label CssClass="control-label-2" runat="server" Text="Agregar" ForeColor="White" ></asp:Label>
                                    <asp:Button ID="btnAgregarDetalle" runat="server" Text="Agregar" CssClass="btn btn-default" Visible="False" />
                                </div> 
                            </div>
                            <div class="row">                                
                                <div class="col-md-3">
                                </div>                               
                                <div class="col-md-3">
                                </div>                               
                                <div class="col-md-3">
                                </div> 
                                <div class="col-md-3">
                                    <asp:Label CssClass="control-label-2" runat="server" Text="Cancelar" ForeColor="White" ></asp:Label>
                                    <asp:Button ID="btnCancelarDetalle" runat="server" Text="Cancelar" CssClass="btn btn-default" Visible="False" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtCodigoMo" runat="server" ClientIDMode="Static" ReadOnly="True" Visible="False" />
                                </div> 
                            </div> 
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GvListaDetalle" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Image" CommandName="EditaDetalle" ImageUrl="~/icono/Editar_opt.png" Text="Editar">
                                                <ItemStyle Height="10px" Width="10px" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="EliminaDetalle" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                                <ItemStyle Height="10px" Width="10px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ARMODE_CODIGO" HeaderText="Código Detalle" SortExpression="ARMODE_CODIGO" />
                                            <asp:BoundField DataField="ARMODE_DESCRIPCION" HeaderText="Descripción" SortExpression="ARMODE_DESCRIPCION" />
                                            <asp:BoundField DataField="ARTMOD_CODIGO" HeaderText="Código Modelo" SortExpression="ARTMOD_CODIGO" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaDetalle" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnAgregarDetalle" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnCancelarDetalle" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnNuevoDetalle" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel4" runat="server" HeaderText="Propietario">
            <ContentTemplate>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label3" runat="server" Text="Propietario" CssClass="subTitulos"></asp:Label>
                    </div>
                </div>  
                <br /> 
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="LblDescPropietario" runat="server" Text="Descripción :" CssClass="control-label-2"></asp:Label>
                                <asp:TextBox ID="TxtDescPropietario" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnListarPropietario" runat="server" Text="Listar" CssClass="form-control btn btn-default" />
                            </div> 
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnNuevoPropietario" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblCodigoPropietario" runat="server" Text="Código :" Visible="False" CssClass="control-label-2" />
                                <asp:TextBox ID="TxtCodigoPropietario" runat="server" Visible="False" Enabled="False" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="LblDescripcionPropietario" runat="server" Text="Descripción :" Visible="False" CssClass="control-label-2" />
                                <asp:TextBox ID="TxtDescripcionPropietario" runat="server" Visible="False" CssClass="form-control" />
                            </div>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Agregar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnAgregarPropietario" runat="server" Text="Agregar" Visible="False" CssClass="form-control btn btn-default" />
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblPlacabilidadPropietario" runat="server" Text="Placabilidad :" Visible="False" CssClass="control-label-2" />
                                <asp:DropDownList ID="DdlPlacabilidadPropietario" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                                    <asp:ListItem Value="S">SI</asp:ListItem>
                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="< Seleccionar >">&lt; Seleccionar &gt;</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                            <asp:Label ID="LblPlacaInicial" runat="server" Text="Placa Inicial :" CssClass="control-label-2" Visible="false" />
                                <asp:TextBox ID="TxtPlacaInicial" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div> 
                            <div class="col-md-3">
                            <asp:Label ID="LblPlacaFinal" runat="server" Text="Placa Final :" CssClass="control-label-2" Visible="false" />
                                <asp:TextBox ID="TxtPlacaFinal" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                            </div> 
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Cancelar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnCancelarPropietario" runat="server" Text="Cancelar" Visible="False" CssClass="form-control btn btn-default" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GvListaPropietario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField CommandName="EditaPropietario" Text="Editar" ButtonType="Image" ImageUrl="~/icono/Editar_opt.png">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="EliminaPropietario" Text="Eliminar" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="ALTIBI_CODIGO" HeaderText="Código" SortExpression="ALTIBI_CODIGO" />
                                        <asp:BoundField DataField="ALTIBI_DESCRIPCION" HeaderText="Descripción" SortExpression="INVENT_DESCRIPCION" />
                                        <asp:BoundField DataField="ALTIBI_PLACABILIDAD" HeaderText="Placabilidad" SortExpression="ALTIBI_PLACABILIDAD" />
                                        <asp:BoundField DataField="PLACA_COMIENZA" HeaderText="Placa Inicio" SortExpression="PLACA_COMIENZA" />
                                        <asp:BoundField DataField="PLACA_FIN" HeaderText="Placa Fin" SortExpression="PLACA_FIN" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListarPropietario" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnNuevoPropietario" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="GvListaPropietario" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarPropietario" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarPropietario" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel5" runat="server" HeaderText="Proyectos">
            <ContentTemplate>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label2" runat="server" Text="Proyectos" CssClass="subTitulos"></asp:Label>
                    </div>
                </div>  
                <br /> 
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Año" ></asp:Label>
                                <asp:DropDownList ID="DdlAño" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="btnListar_Proyectos" runat="server" CssClass="form-control btn btn-default" Text="Listar"></asp:Button>
                            </div>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Nuevo" ForeColor="White" ></asp:Label>
                                <asp:Button ID="btnNuevo_Proyectos" runat="server" CssClass="form-control btn btn-default" Text="Nuevo"></asp:Button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblAño_Proy" runat="server" CssClass="control-label-2" Text="Año :" Visible="False" />
                                <asp:TextBox ID="txtAño" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Grabar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnGrabar_Proyectos" runat="server" CssClass="form-control btn btn-default" Text="Grabar" Visible="False" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblCodigo_Proy" runat="server" CssClass="control-label-2" Text="Código:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtCodigo_Proy" runat="server" CssClass="form-control" Enabled="False" Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="LblDescripción_Proy" runat="server" CssClass="control-label-2" Text="Descripción:" Visible="False"></asp:Label>
                                <asp:TextBox ID="txtDescripcion_Proy" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            </div>
                            <%--<div class="col-lg-3">
                            </div>--%>
                            <div class="col-md-3">
                                <asp:Label CssClass="control-label-2" runat="server" Text="Cancelar" ForeColor="White" ></asp:Label>
                                <asp:Button ID="BtnCancelar_Proyectos" runat="server" CssClass="form-control btn btn-default" Text="Cancelar" Visible="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnGrabar_Proyectos" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNuevo_Proyectos" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelar_Proyectos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="GridView_Proyectos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/icono/Editar_opt.png" Text="Editar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="PROYECTO_CODIGO" HeaderText="Código" SortExpression="PROYECTO_CODIGO" />
                                        <asp:BoundField DataField="PROYECTO_AÑO" HeaderText="Año" SortExpression="PROYECTO_AÑO" />
                                        <asp:BoundField DataField="PROYECTO_DESCRIPCION" HeaderText="Descripción" SortExpression="PROYECTO_DESCRIPCION" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnGrabar_Proyectos" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNuevo_Proyectos" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnListar_Proyectos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

                <div id="ModalMensajeProyecto" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="MensajeProyecto" class="col-lg-12" />
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Button ID="BtnPOk" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView_Proyectos" EventName="RowCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel6" runat="server" HeaderText="Ubicaciones">
            <ContentTemplate>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server" Text="Define Ubicaciones" CssClass="subTitulos"></asp:Label>
                    </div>
                </div>  
                <br /> 
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="BtnNuevo_Ubicaciones" runat="server" CssClass="form-control btn btn-default" Text="Nuevo" />
                            </div> 
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblCodigo" runat="server" CssClass="control-label-2" Text="Código :" Visible="False" />
                                <asp:TextBox ID="TxtCodigo_Ubicaciones" runat="server" CssClass="form-control" Enabled="False" Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label6" CssClass="control-label-2" runat="server" Text="Tipo:" Visible="False"></asp:Label>
                                <asp:DropDownList ID="DdlTipo" runat="server" CssClass="form-control"  Visible="False">
                                    <asp:ListItem Text="Almacén" Value="1" />
                                    <asp:ListItem Text="Sessión CC" Value="2" />
                                    <asp:ListItem Text="< Seleccionar >" Value="< Seleccionar >" Selected="True" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label4" runat="server" CssClass="control-label-2" Text="Grabar" Visible="False" forecolor="white"></asp:Label>
                            <asp:Button ID="BtnGrabar_Ubicaciones" runat="server" CssClass="form-control btn btn-default" Text="Grabar" Visible="False" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="LblDescripción" runat="server" CssClass="control-label-2" Text="Descripción :" Visible="False"></asp:Label>
                                <asp:TextBox ID="TxtDescripcion_Ubicaciones" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label5" runat="server" CssClass="control-label-2" Text="Cancelar" Visible="False" forecolor="white"></asp:Label>
                            <asp:Button ID="BtnCancelar_Ubicaciones" runat="server" CssClass="form-control btn btn-default" Text="Cancelar" Visible="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnGrabar_Ubicaciones" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnNuevo_Ubicaciones" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCancelar_Ubicaciones" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView_Ubicaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/icono/Editar_opt.png" Text="Editar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="Eliminar" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                            <ItemStyle Height="10px" Width="10px" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="UBICACION_CODIGO" HeaderText="Código" SortExpression="UBICACION_CODIGO" />
                                        <asp:BoundField DataField="UBICACION_DESCRIPCION" HeaderText="Descripción" SortExpression="UBICACION_DESCRIPCION" />
                                        <asp:BoundField DataField="UBICACION_TIPO" HeaderText="" SortExpression="UBICACION_TIPO" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnGrabar_Ubicaciones" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCancelar_Ubicaciones" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div id="ModalMensajeUbic" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="MensajeUbic" class="col-md-12" />
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Button ID="BtnUOk" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView_Ubicaciones" EventName="RowCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>

</asp:Content>
