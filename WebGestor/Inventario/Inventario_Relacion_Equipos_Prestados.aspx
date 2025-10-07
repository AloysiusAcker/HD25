<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Relacion_Equipos_Prestados.aspx.vb" Inherits="Inventario_Inventario_Relacion_Equipos_Prestados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Inventario - Equipos Prestados" CssClass="Titulos"></asp:Label>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblError" runat="server" Text="" ForeColor="red"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="BtnListar" runat="server" Text="Listar" CssClass="form-control btn btn-default"/>
                    </div> 
                    <div class="col-md-2">
                        <asp:Button ID="BtnListarAcc" runat="server" Text="Listar Accesorios" CssClass="form-control btn btn-default" Visible ="false"/>
                    </div> 
                    <div class="col-md-2">
                        <asp:Button ID="BtnListarTodo" runat="server" Text="Listar Todos" CssClass="form-control btn btn-default" Visible ="false" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta1" CssClass="control-label-2" runat="server" Text="Fecha Préstamo"></asp:Label>
                        <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta2" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                        <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta11" CssClass="control-label-2" runat="server" Text="Fecha Devolución"></asp:Label>
                        <asp:TextBox ID="TxtFechaD" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaD" Format="dd/MM/yyyy" PopupButtonID="TxtFechaD" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta12" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                        <asp:TextBox ID="TxtFechaFinD" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFinD" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFinD" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Estado"></asp:Label>
                        <asp:DropDownList ID="DdlEstado" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta3" CssClass="control-label-2" runat="server" Text="Origen:"></asp:Label>
                        <asp:DropDownList ID="DdlOrigen" runat="server" CssClass="form-control" AutoPostBack="True" >
                            <asp:ListItem Text="< Seleccionar >" Selected="True" />
                            <asp:ListItem Text="Almacén" Value="1" />
                            <asp:ListItem Text="Sessión CC" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta4" CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="TxtOrigCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiqueta5"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnBuscarO" runat="server"  CssClass="form-control btn btn-default"  Text="..." />
                    </div> 
                    <div class="col-md-7">
                        <asp:Label ID="LblEtiqueta6"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="txtOrigDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>     
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta7" CssClass="control-label-2" runat="server" Text="Destino:"></asp:Label>
                        <asp:DropDownList ID="DdlDestino" runat="server" CssClass="form-control" AutoPostBack="True" >
                            <asp:ListItem Text="< Seleccionar >" Selected="True" />
                            <asp:ListItem Text="Almacén" Value="1" />
                            <asp:ListItem Text="Sessión CC" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiqueta8"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="TxtDestCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiqueta9"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnBuscarD" runat="server"  CssClass="form-control btn btn-default"  Text="..." />
                    </div> 
                    <div class="col-md-7">
                        <asp:Label ID="LblEtiqueta10"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="TxtDestDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>    
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiqueta14"  CssClass="control-label-2" runat="server" Text="Serie Nro."></asp:Label>
                        <asp:TextBox ID="TxtSerieNro" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="LblEtiqueta15"  CssClass="control-label-2" runat="server" Text="Placa Nro."></asp:Label>
                        <asp:TextBox ID="TxtPlacaNro" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>   
                <div class="row">
                    <div class="col-md-12">                
                        <asp:Label ID="LblCodOrigen" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="LblCodDestino" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div>      
                
                <div class="row">
                    <div class="col-lg-12">                   
                        <div id="accordion" role="tablist" aria-multiselectable="true" runat="server" visible="false" >
                            <div class="card">
                                <div class="card-header" role="tab" id="section1HeaderId">
                                    <h5 class="mb-0">                            
                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="false" aria-controls="section1ContentId">
                                           Equipos
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                                    <div class="card-body">    
                                        <div class="row">
                                            <div class="col-md-9">
                                                <asp:Label ID="LblRegistro" runat="server" Text="" ></asp:Label>
                                            </div>
                                        </div>      
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvListadoEq" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="true"  >
                                                    <Columns>
                                                        <asp:BoundField DataField="TIPO_PRESTAMO" HeaderText="Tipo" SortExpression="TIPO_PRESTAMO" />
                                                        <asp:BoundField DataField="PRESTAMO_CODIGO" HeaderText="Prestamo cód." SortExpression="PRESTAMO_CODIGO" />
                                                        <asp:BoundField DataField="PRESTAMO_ITEM" HeaderText="ITEM" SortExpression="PRESTAMO_ITEM" />
                                                        <asp:BoundField DataField="FPRESTAMO" HeaderText="Fecha Prestamo" SortExpression="FPRESTAMO" />
                                                        <asp:BoundField DataField="ESTADO_PRESTAMO_EQUIPO" HeaderText="Estado" SortExpression="ESTADO_PRESTAMO_EQUIPO" />
                                                        <asp:BoundField DataField="FECHA_PORDEVOLVER" HeaderText="Fecha x Devolver" SortExpression="FECHA_PORDEVOLVER" />
                                                        <asp:BoundField DataField="FDEVOL" HeaderText="F. Devolución" SortExpression="FDEVOL" />
                                                        <asp:BoundField DataField="ARTICULO" HeaderText="Artículo" SortExpression="ARTICULO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro: Serie" SortExpression="SERIE_NRO" />
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro: Placa" SortExpression="PLACA_NRO" />
                                                        <asp:BoundField DataField="ORIGEN" HeaderText="Origen" SortExpression="ORIGEN" />
                                                        <asp:BoundField DataField="ORIGEN_CODEXTERNO" HeaderText="Origen Cód." SortExpression="ORIGEN_CODEXTERNO" />
                                                        <asp:BoundField DataField="ORIGEN_NOMBRE" HeaderText="Origen Nombre" SortExpression="ORIGEN_NOMBRE" />
                                                        <asp:BoundField DataField="DESTINO" HeaderText="Destino" SortExpression="DESTINO" />
                                                        <asp:BoundField DataField="DESTINO_CODEXTERNO" HeaderText="Destino Cód." SortExpression="DESTINO_CODEXTERNO" />
                                                        <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Destino Nombre" SortExpression="DESTINO_NOMBRE" />
                                                        <asp:BoundField DataField="ESTADO_ENVIO" HeaderText="Sit. Envio" SortExpression="ESTADO ENVIO" />
                                                        <asp:BoundField DataField="TIPO_MOVIMIENTO" HeaderText="Tipo Mov." SortExpression="TIPO_MOVIMIENTO" />
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
                                            Accesorios
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId2" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId2">
                                    <div class="card-body">  <div class="row">
                                        <div class="col-md-9">
                                            <asp:Label ID="LblRegistrosAcc" runat="server" Text="" ></asp:Label>
                                        </div>
                                    </div>  
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvAccesorios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="true"  >
                                                <Columns>
                                                    <asp:BoundField DataField="TIPO_PRESTAMO" HeaderText="Tipo" SortExpression="TIPO_PRESTAMO" />
                                                    <asp:BoundField DataField="ESTADO_PRESTAMO_EQUIPO" HeaderText="Estado" SortExpression="ESTADO_PRESTAMO_EQUIPO" />
                                                    <asp:BoundField DataField="PRESTAMO_CODIGO" HeaderText="Prestamo cód." SortExpression="PRESTAMO_CODIGO" />
                                                    <asp:BoundField DataField="FECHA_PORDEVOLVER" HeaderText="Fecha x Devolver" SortExpression="FECHA_PORDEVOLVER" />
                                                    <asp:BoundField DataField="FPRESTAMO" HeaderText="Fecha Prestamo" SortExpression="FPRESTAMO" />
                                                    <asp:BoundField DataField="FDEVOL" HeaderText="F. Devolución" SortExpression="FDEVOL" />
                                                    <asp:BoundField DataField="ARTICULO" HeaderText="Artículo" SortExpression="ARTICULO" />
                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                    <asp:BoundField DataField="PREDET_CANT_PRESTADA" HeaderText="Cant. Prestada" SortExpression="PREDET_CANT_PRESTADA" />
                                                    <asp:BoundField DataField="PREDET_CANT_XDEVOLVER" HeaderText="Cant. x Devolver" SortExpression="PREDET_CANT_XDEVOLVER" />
                                                    <asp:BoundField DataField="PREDET_CANT_FALT_DEVOLVER" HeaderText="Cant. Falt. Devol." SortExpression="PREDET_CANT_FALT_DEVOLVER" />
                                                    <asp:BoundField DataField="PREDET_CANT_DEVUELTA" HeaderText="Cant. Devuelta" SortExpression="PREDET_CANT_DEVUELTA" />
                                                    <asp:BoundField DataField="ORIGEN" HeaderText="Origen" SortExpression="ORIGEN" />
                                                    <asp:BoundField DataField="ORIGEN_CODEXTERNO" HeaderText="Origen Cód." SortExpression="ORIGEN_CODEXTERNO" />
                                                    <asp:BoundField DataField="ORIGEN_NOMBRE" HeaderText="Origen Nombre" SortExpression="ORIGEN_NOMBRE" />
                                                    <asp:BoundField DataField="DESTINO" HeaderText="Destino" SortExpression="DESTINO" />
                                                    <asp:BoundField DataField="DESTINO_CODEXTERNO" HeaderText="Destino Cód." SortExpression="DESTINO_CODEXTERNO" />
                                                    <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Destino Nombre" SortExpression="DESTINO_NOMBRE" />
                                                    <asp:BoundField DataField="TIPO_MOVIMIENTO" HeaderText="Tipo Mov." SortExpression="TIPO_MOVIMIENTO" />
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
                <asp:AsyncPostBackTrigger ControlID="DdlOrigen" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 
    </div>

    <div id="ModalBusqueda" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblEtq_BusDestino" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Centro de Costos" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DdlOrigen" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="DdlDestino" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                                      
                        
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarO" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarD" EventName="Click" />
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

