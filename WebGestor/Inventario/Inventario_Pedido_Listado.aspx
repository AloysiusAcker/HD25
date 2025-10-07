<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Pedido_Listado.aspx.vb" Inherits="Inventario_Inventario_Pedido_Listado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Lista de Pedidos" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
            </div> 
        </div>
        <div class="row espacio">
            <div class="col-md-2">
                <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha:"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-3 col-xs-6">
                <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text=".." ForeColor ="White" ></asp:Label>
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div>
        </div>
        <div class="row espacio">
            <div class="col-md-3">
                <asp:Label ID="lblEtiq4" runat="server" Text="Tipo de Pedido :" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlTipoPedido" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-lg-6 col-sm-6">
                <asp:Label ID="lvlCargarDodumento" runat="server" Text="Archivo:" CssClass="control-label"></asp:Label>
                <asp:FileUpload ID="FileUpload1"  runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3 col-xs-6">
                <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text=".." ForeColor ="White" ></asp:Label>
                <asp:Button ID="BtnLeer" runat="server" Text="Cargar Archivo" ControlStyle-CssClass="form-control btn btn-default" />
            </div>
        </div>
        <div class="row espacio">                    
            <div class="col-md-12">
                <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
            </div> 
        </div>    
        <div class="row espacio">                    
            <div class="col-md-12">
                <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                    <Columns>
                        <asp:BoundField DataField="CodPedido" HeaderText="Cod. Pedido" SortExpression="CodPedido" />
                        <asp:BoundField DataField="FECHA_reg" HeaderText="Fecha Reg" SortExpression="FECHA_reg" />
                        <asp:BoundField DataField="hora_reg" HeaderText="Hora Reg." SortExpression="hora_reg" />
                        <asp:BoundField DataField="Tipo_Pedido" HeaderText="Tipo de Pedido" SortExpression="Tipo_Pedido" />
                        <asp:BoundField DataField="INVPEDIDO_NRO_TICKET" HeaderText="Nro Ticket" SortExpression="INVPEDIDO_NRO_TICKET" />
                        <asp:BoundField DataField="INVPEDIDO_TAREA" HeaderText="Tarea" SortExpression="INVPEDIDO_TAREA" />
                        <asp:BoundField DataField="INVPEDIDO_USUARIO" HeaderText="Usuario" SortExpression="INVPEDIDO_USUARIO" />
                        <asp:BoundField DataField="INVPEDIDO_USUARIO_NOMBRE" HeaderText="Ususario Nombre" SortExpression="INVPEDIDO_USUARIO_NOMBRE" />
                        <asp:BoundField DataField="INVPEDIDO_CCOSTO" HeaderText="CC" SortExpression="INVPEDIDO_CCOSTO" />
                        <asp:BoundField DataField="INVPEDIDO_CCOSTO_NOMBRE" HeaderText="CC Nombre" SortExpression="INVPEDIDO_CCOSTO_NOMBRE" />
                        <asp:BoundField DataField="INVPEDIDO_NRO_OC" HeaderText="OC" SortExpression="INVPEDIDO_NRO_OC" />
                        <asp:BoundField DataField="INVPEDIDO_ARTICULO_NOMBRE" HeaderText="Artículo" SortExpression="INVPEDIDO_ARTICULO_NOMBRE" />
                        <asp:BoundField DataField="INVPEDIDO_ARTICULO_CANT" HeaderText="cant." SortExpression="INVPEDIDO_ARTICULO_CANT" />
                        <asp:BoundField DataField="INVPEDIDO_TIPO_ATENCION_NOMBRE" HeaderText="Tipo Atencion" SortExpression="INVPEDIDO_TIPO_ATENCION_NOMBRE" />
                        <asp:BoundField DataField="INVPEDIDO_SERIE" HeaderText="Serie" SortExpression="INVPEDIDO_SERIE" />
                        <asp:BoundField DataField="INVPEDIDO_PLACA" HeaderText="Placa" SortExpression="INVPEDIDO_PLACA" />
                        <asp:BoundField DataField="INVPEDIDO_MOTIVO_RECOJO" HeaderText="Motivo de Recojo" SortExpression="INVPEDIDO_MOTIVO_RECOJO" />
                        <asp:BoundField DataField="INVPEDIDO_ENVIO" HeaderText="Envio" SortExpression="INVPEDIDO_ENVIO" />
                        <asp:BoundField DataField="INVPEDIDO_OBSERVACION" HeaderText="Observación" SortExpression="INVPEDIDO_OBSERVACION" />
                        <asp:BoundField DataField="INVPEDIDO_ESTADO" HeaderText="Estado" SortExpression="INVPEDIDO_ESTADO" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>  
    </div> 
</asp:Content>

