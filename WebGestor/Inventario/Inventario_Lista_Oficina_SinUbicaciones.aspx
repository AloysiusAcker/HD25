<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Lista_Oficina_SinUbicaciones.aspx.vb" Inherits="Inventario_Inventario_Lista_Oficina_SinUbicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Lista de Oficinas Sin Ubicaciones" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnListar" runat="server" Text="Listar"  ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar a Excel"  ControlStyle-CssClass="form-control btn btn-default" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Sin Ubicación :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlUbicacion" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>    
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="Ubicacion_Cod_Interno" HeaderText="Oficina Cod." SortExpression="Ubicacion_Cod_Interno" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Oficina" SortExpression="Ubicacion" />
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

