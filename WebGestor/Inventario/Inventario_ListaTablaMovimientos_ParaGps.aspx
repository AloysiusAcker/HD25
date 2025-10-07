<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_ListaTablaMovimientos_ParaGps.aspx.vb" Inherits="Inventario_Inventario_ListaTablaMovimientos_ParaGps" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Lista de Movimientos" CssClass="Titulos" />
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
            <div class="col-md-12">
                <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
            </div> 
        </div>    
        <div class="row espacio">                    
            <div class="col-md-12">
                <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                    <Columns>
                        <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Art." SortExpression="ART_CODIGO" />
                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripcion" SortExpression="ART_DESCRIPCION" />
                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie Nro" SortExpression="SERIE_NRO" />
                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro" SortExpression="PLACA_NRO" />
                        <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                        <asp:BoundField DataField="ORIGEN_CODINTERNO" HeaderText="Origen Cod." SortExpression="ORIGEN_CODINTERNO" />
                        <asp:BoundField DataField="ORIGEN_NOMBRE" HeaderText="Origen Nombre" SortExpression="ORIGEN_NOMBRE" />
                        <asp:BoundField DataField="DESTINO_CODINTERNO" HeaderText="Destino codigo" SortExpression="DESTINO_CODINTERNO" />
                        <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Destino Nombre" SortExpression="DESTINO_NOMBRE" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>  
    </div> 
    



</asp:Content>

