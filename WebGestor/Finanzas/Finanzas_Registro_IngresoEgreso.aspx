<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Finanzas_Registro_IngresoEgreso.aspx.vb" Inherits="Finanzas_Finanzas_Registro_IngresoEgreso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        /* Estilos para el GridView */
            .gridview-container {
                font-family:  Roboto, arial, sans-serif;
                border: 1px solid #ddd;
                border-radius: 5px;
                overflow: hidden;
            }
        .gridview {    
            font-family: Roboto, arial, sans-serif;
            font-size: 8pt;
            border-collapse: collapse;
            border:1px solid #ddd;
            width: 100%;
        }

        .gridview th, .gridview td {
            padding: 3px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .gridview th {
            background-color: #f2f2f2;
        }

        .gridview tr:hover {
            background-color: #f5f5f5;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Finanzas" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnNuevo" runat="server" Text="Registrar" ControlStyle-CssClass="form-control btn btn-default" />
                <%--<input id="BtnNuevo" type="button" value="Registrar" runat="server" class="form-control btn btn-default" />--%>
            </div>
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default"/>
            </div> 
        </div>    
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="LblAño" runat="server" Text="Año" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlAño" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblMoneda" runat="server" Text="Moneda" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlMoneda" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Módulo" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlModulo" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
        
  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" Text="Fecha Emision" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""  ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Fecha" CssClass="control-label-2" ForeColor="White"  />
                    <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""  ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                </div>  
            </div>

            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="lblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                </div> 
            </div>    
            <div class="row">                    
                <div class="col-md-12">
                    <asp:GridView ID="GvFinanza" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                        <Columns>
                            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="Fecha_Emi" HeaderText="Fecha Emi" SortExpression="Fecha_Emi" />
                            <asp:BoundField DataField="Fecha_Pago" HeaderText="Fecha Pago" SortExpression="Fecha_Pago" />
                            <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                            <asp:BoundField DataField="Ingreso" HeaderText="Ingreso" SortExpression="Ingreso" />
                            <asp:BoundField DataField="Egreso" HeaderText="Egreso" SortExpression="Egreso" />
                            <asp:BoundField DataField="Motivo" HeaderText="Motivo" SortExpression="Motivo" />
                            <asp:BoundField DataField="FINANZA_GLOSA" HeaderText="Glosa" SortExpression="FINANZA_GLOSA" />
                            <asp:BoundField DataField="PERSONA" HeaderText="PERSONA" SortExpression="PERSONA" />
                            <asp:BoundField DataField="Documento" HeaderText="Documento" SortExpression="Documento" />
                            <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc" SortExpression="Nrodoc" />
                            <asp:BoundField DataField="Banco" HeaderText="Banco" SortExpression="Banco" />
                            <asp:BoundField DataField="BancoDestino" HeaderText="Banco Destino" SortExpression="BancoDestino" />
                            <asp:BoundField DataField="CCosto" HeaderText="CCosto" SortExpression="CCosto" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>  
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                
            </Triggers>
        </asp:UpdatePanel>   


    </div> 


</asp:Content>

