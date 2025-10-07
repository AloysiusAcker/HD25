<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Relacion_Salidas_CCosto.aspx.vb" Inherits="Inventario_Inventario_Relacion_Salidas_CCosto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Relación de Salidas de CCostos" CssClass="Titulos"></asp:Label>
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
        <div class="row">                   
        </div> 
        <div class="row">
        </div>

        <div class="row">
            <div class="col-md-9">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                        <asp:Label ID="LblRegistro" runat="server" Text="" ></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>      

        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate> 
                    <asp:GridView ID="gridSalida" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
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
                            <asp:BoundField DataField="OSAL_ESTADO" SortExpression="DESP_ESTADO">
                                <ItemStyle ForeColor="White" />
                            </asp:BoundField>
                         </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div> 
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

</asp:Content>

