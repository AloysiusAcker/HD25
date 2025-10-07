<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_GenerarGuia_SinSalida.aspx.vb" Inherits="Inventario_Inventario_GenerarGuia_SinSalida" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Inventario - Genera Guía" CssClass="Titulos"></asp:Label>
            </div>
        </div><br />
        <div class="row">
            <div class="col-md-9">
                <asp:Label ID="LblError" runat="server" ForeColor="red"></asp:Label>
            </div> 
        </div>
        
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="LblEtiq" CssClass="control-label-2" runat="server" Text="Fecha Emisión:"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblEtiq2" CssClass="control-label-2" runat="server" Text="Hora Emisión:"></asp:Label>
                <asp:TextBox ID="TxtHora" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>                
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblEtiq3" CssClass="control-label-2" runat="server" Text="Fecha Traslado:"></asp:Label>
                <asp:TextBox ID="TxtFechaTraslado" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaTraslado" Format="dd/MM/yyyy" PopupButtonID="TxtFechaTraslado" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblEtiq4" CssClass="control-label-2" runat="server" Text="Hora Traslado:"></asp:Label>
                <asp:TextBox ID="TxtHoraTraslado" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>    
            </div>
            <div class="col-md-2">
                <asp:Label ID="Label3"  CssClass="control-label-2" runat="server" Text="Regresar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnLimpiar" runat="server"  CssClass="form-control btn btn-default" Text="Limpiar" />
            </div>
            <div class="col-md-2">
                <asp:Label ID="Label1"  CssClass="control-label-2" runat="server" Text="Guardar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnGuardar" runat="server"  CssClass="form-control btn btn-default" Text="Guardar" />
            </div>
        </div>        
        
        <div class="row">
            <div class="col-md-9 col-xs-6">
                <asp:Label ID="LblUbicacion" runat="server" Text="Destino :" CssClass="control-label-2" />
                <asp:RadioButton GroupName="ubicacion" ID="RbGuiaRem" runat="server" Text="Guía Remisión" Checked="true" AutoPostBack="True" />
                <asp:RadioButton GroupName="ubicacion" ID="RbGuiaInt" runat="server" Text="Guía Interna" AutoPostBack="True" />
            </div>
        </div>
        <div class="row"  id="id_GuiaNumero"  runat="server" visible="false" >
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq6" CssClass="control-label-2" runat="server" Text="Guia Serie"></asp:Label>
                        <asp:TextBox ID="TxtGuiaSerie" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq7" CssClass="control-label-2" runat="server" Text="Guia Numero"></asp:Label>
                        <asp:TextBox ID="TxtGuiaNumero" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TxtGuiaSerie" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel> 
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>               

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq8" CssClass="control-label-2" runat="server" Text="Remitente:"></asp:Label>
                        <asp:DropDownList ID="DdlRemitente" runat="server" CssClass="form-control" >
                            <asp:ListItem Text="Almacén" Value="1" Selected="True" />
                            <asp:ListItem Text="Sessión CC" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq9"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="TxtRemCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiq10"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnRemitente" runat="server"  CssClass="form-control btn btn-default"  Text="..." />
                    </div> 
                    <div class="col-md-7">
                        <asp:Label ID="LblEtiq11"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="txtRemDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>       
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-7">
                        <asp:Label ID="LblEtiq12"  CssClass="control-label-2" runat="server" Text="Punto de partida"></asp:Label>
                        <asp:TextBox ID="TxtPuntoPartida" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq13" CssClass="control-label-2" runat="server" Text="Ubigeo"></asp:Label>
                        <asp:TextBox ID="TxtRemUbigeo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 

                
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq14" CssClass="control-label-2" runat="server" Text="Destinatario:"></asp:Label>
                        <asp:DropDownList ID="DdlDestinatario" runat="server" CssClass="form-control" >
                            <asp:ListItem Text="Almacén" Value="1" />
                            <asp:ListItem Text="Sessión CC" Value="2" Selected="True" />
                            <asp:ListItem Text="Proveedor" Value="3" />
                            <asp:ListItem Text="Personas" Value="5" />
                            <asp:ListItem Text="Clientes" Value="6" />
                            <asp:ListItem Text="Centro de Costos" Value="7" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq15"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="TxtDestCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiq16"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnDestinatario" runat="server"  CssClass="form-control btn btn-default" Text="..." />
                    </div> 
                    <div class="col-md-7">
                        <asp:Label ID="LblEtiq17"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="TxtDestDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>       
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-7">
                        <asp:Label ID="LblEtiq18"  CssClass="control-label-2" runat="server" Text="Punto de llegada"></asp:Label>
                        <asp:TextBox ID="TxtPuntoLlegada" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq19" CssClass="control-label-2" runat="server" Text="Ubigeo"></asp:Label>
                        <asp:TextBox ID="TxtDestUbigeo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="Label7" CssClass="control-label-2" runat="server" Text="Motivo de la Salida:"></asp:Label>
                        <asp:DropDownList ID="DdlMotivo" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div> 
                <div class="row"  id="id_MotivoTrasldo"  runat="server" visible="false" >
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq20" CssClass="control-label-2" runat="server" Text="Motivo Translado:"></asp:Label>
                        <asp:DropDownList ID="DdlMotivoTraslado" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq36" CssClass="control-label-2" runat="server" Text="Descripción:"></asp:Label>
                        <asp:TextBox ID="TxtMotivoDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row"  id="id_ModalidadTransporte"  runat="server" visible="false" >
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq21" CssClass="control-label-2" runat="server" Text="Modalidad Transporte:"></asp:Label>
                        <asp:DropDownList ID="DdlModTransporte" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                
                <div id="id_GuiaInterna" class="row" runat="server" visible="false"  >
                    <div class="col-md-6">
                        <hr />
                        <asp:Label ID="Label4"  CssClass="control-label-2" runat="server" Text="Persona quien entrega" ></asp:Label>
                        <asp:TextBox ID="TxtQuienRetira" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <hr />
                        <asp:Label ID="Label5" CssClass="control-label-2" runat="server" Text="Persona quien recibe" ></asp:Label>
                        <asp:TextBox ID="TxtQuienRecibe" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div> 

                <div class="row" id="id_DatosTransportista"  runat="server" visible="false"  >
                    <div class="col-md-12">
                        <hr />
                        <h5>
                            Datos del Transportistas
                        </h5>
                    </div>  
                </div>  

                <div class="row" id="id_Vehiculo"  runat="server" visible="false"  >
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq23"  CssClass="control-label-2" runat="server" Text="Placa Vehículo" ></asp:Label>
                        <asp:TextBox ID="TxtNroPlaca" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiq24"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnVehiculo" runat="server"  CssClass="form-control btn btn-default" Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq25"  CssClass="control-label-2" runat="server" Text="Marca Vehículo" ></asp:Label>
                        <asp:TextBox ID="TxtMarca" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq26"  CssClass="control-label-2" runat="server" Text="Conf. Vehicular" ></asp:Label>
                        <asp:TextBox ID="TxtconfVehicular" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
              
                <div class="row" id="id_Transportista"  runat="server" visible="false" >
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq27"  CssClass="control-label-2" runat="server" Text="R.U.C." ></asp:Label>
                        <asp:TextBox ID="TxtRucTrasnportista" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiq28"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnTransporte" runat="server"  CssClass="form-control btn btn-default"  Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq29"  CssClass="control-label-2" runat="server" Text="Razon social" ></asp:Label>
                        <asp:TextBox ID="TxtRazonTransportista" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq30"  CssClass="control-label-2" runat="server" Text="Cert. Inscripción" ></asp:Label>
                        <asp:TextBox ID="TxtCertInscripcion" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                
                <div class="row"  id="id_Chofer"  runat="server" visible="false" >
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq31"  CssClass="control-label-2" runat="server" Text="Chofer DNI" ></asp:Label>
                        <asp:TextBox ID="TxtChoferDNI" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiq32"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnChofer" runat="server"  CssClass="form-control btn btn-default" Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq33"  CssClass="control-label-2" runat="server" Text="Apellidos y Nombres" ></asp:Label>
                        <asp:TextBox ID="TxtChoferNombre" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq34"  CssClass="control-label-2" runat="server" Text="Nro. Licencia" ></asp:Label>
                        <asp:TextBox ID="TxtLicencia" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                 
                <div class="row">
                    <div class="col-md-12">
                    <hr />
                    </div> 
                </div>

                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="LblEtiq38"  CssClass="control-label-2" runat="server" Text="Observación"></asp:Label>
                        <asp:TextBox ID="TxtObsGuia" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiq39" CssClass="control-label-2" runat="server" Text="Nro. de Bultos"></asp:Label>
                        <asp:TextBox ID="TxtNroBulto" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 


            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvTransporte" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvVehiculo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvChofer" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>         
        
        <div class="row">
            <div class="col-md-12">
            <hr />
            </div> 
        </div>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>     
                <div class="row">                    
                    <div class="col-md-3">
                        <asp:Label ID="Label6"  CssClass="control-label-2" runat="server" Text="P" ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnBuscarArt" runat="server"  CssClass="form-control btn btn-default" Text="Agregar Producto" />                    
                    </div>   
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="Label2"  CssClass="control-label-2" runat="server" Text="Lista de Artículos"></asp:Label>               
                                    
                        <asp:GridView ID="GvListaArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="QuitarArt" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Código" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_TIPO" HeaderText="tipo" SortExpression="ART_TIPO" />
                                <asp:BoundField DataField="CANT" HeaderText="Nro. Col. Cant." SortExpression="CANT_COL_EXCEL" />   
                                <asp:TemplateField>                                                                       
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCant" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>   
        
                <div class="form-group">
                    <div class="col-md-9">
                        <asp:Label ID="lblCodRemitente" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodDestinatario" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodTrasporte" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodVehiculo" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodChofer" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvTransporte" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvVehiculo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvChofer" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvListaArticulos" EventName="RowCommand" />
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
                                <asp:AsyncPostBackTrigger ControlID="BtnRemitente" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnDestinatario" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step1">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnCancelar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
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
         
        <div id="ModalTransporte" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:Label ID="LblPopudTransporte" runat="server" Text="Busqueda Empresa Transporte" />
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step2">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label"  for="id_descripcion">Razón social :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusTransRazon" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnBusTransporte" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label" for="id_codigo">RUC :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusTransRUC" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnCancelarTrans" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvTransporte" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCancelarTrans" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                                            <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvTransporte" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CodInterno" HeaderText="Código" SortExpression="CodInterno" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                                <asp:BoundField DataField="CertInscripcion" HeaderText="Cert. Inscripción" SortExpression="CertInscripcion"/>
                                                                <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnBusTransporte" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarTrans" EventName="Click" />
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

        <div id="ModalVehiculo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:Label ID="Label12" runat="server" Text="Busqueda Vehículo" />
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step3">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label"  for="id_descripcion">Nro. Placa :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusPlaca" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnVehiculoBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label" for="id_codigo">Marca :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusMarca" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnVehiculoCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvVehiculo" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnVehiculoCerrar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                                            <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvVehiculo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="VEHICU_PLACA" HeaderText="Placa" SortExpression="VEHICU_PLACA" />
                                                                <asp:BoundField DataField="VEHICU_MARCA" HeaderText="Marca" SortExpression="VEHICU_MARCA" />
                                                                <asp:BoundField DataField="VEHICU_CERTIF_INSCRIP" HeaderText="Cert. Inscripción" SortExpression="VEHICU_CERTIF_INSCRIP"/>
                                                                <asp:BoundField DataField="RUCTRANSPORTISTA" HeaderText="RUC" SortExpression="RUCTRANSPORTISTA" />
                                                                <asp:BoundField DataField="NOMTRANSPORTISTA" HeaderText="Razón Social" SortExpression="NOMTRANSPORTISTA" />
                                                                <asp:BoundField DataField="CERTRANSPORTISTA" HeaderText="Cert. Inscripcion" SortExpression="CERTRANSPORTISTA" />
                                                                <asp:BoundField DataField="VEHICU_CODIGO" SortExpression="VEHICU_CODIGO">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnVehiculoBuscar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnVehiculoCerrar" EventName="Click" />
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

        <div id="ModalChofer" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:Label ID="Label13" runat="server" Text="Busqueda Chofer" />
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step4">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label"  for="id_descripcion">DNI :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusChoferDni" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnChoferBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label" for="id_codigo">Nombres :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusChoferNombres" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnChoferCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvChofer" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnChoferCerrar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                                            <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvChofer" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CHOFER_DNI" HeaderText="DNI" SortExpression="CHOFER_DNI" />
                                                                <asp:BoundField DataField="NOMBRES" HeaderText="Apellidos y Nombres" SortExpression="NOMBRES" />
                                                                <asp:BoundField DataField="CHOFER_BREVETE" HeaderText="Nº Licencia" SortExpression="CHOFER_BREVETE"/>
                                                                <asp:BoundField DataField="CHOFER_CODIGO" SortExpression="CHOFER_CODIGO">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnChoferBuscar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnChoferCerrar" EventName="Click" />
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

        <div id="ModalPregunta" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12" >
                                    <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                                </div> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnGuardar" EventName="Click" />
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
                                                <asp:Button ID="btnRedirectYes" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="btnRedirectYes_Click" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnImprimir" runat="server" class="form-control btn btn-default" Text="Imprimir" OnClick="btnImprimir_Click" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnGuardar" EventName="Click" />
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
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
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
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">SKU :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnNuevoBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />   
                                            <asp:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="SelectedNodeChanged" />                                      </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GvBusArticulo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Código" SortExpression="ART_CODIGO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nº Parte" SortExpression="ART_CODEQUIVA"/>
                                                            <asp:BoundField DataField="ART_TIPO" HeaderText="Tipo" SortExpression="TIPO_ART"/>
                                                            <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnNuevoBA" EventName="Click" />
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
         


</asp:Content>

