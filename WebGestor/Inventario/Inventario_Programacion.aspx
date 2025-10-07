<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Programacion.aspx.vb" Inherits="Inventario_Inventario_Programacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Ventas - Registro Oportunidades" CssClass="Titulos" />
            </div> 
        </div>
        <br />

        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default"/>
            </div> 
        </div>

        <asp:GridView ID="GridViewCalendario" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:TemplateField HeaderText="Evento">
                    <ItemTemplate>
                        <asp:Label ID="lblEvento" runat="server" Text='<%# Eval("Evento") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <!-- Aquí puedes agregar más columnas según tus necesidades -->
            </Columns>
        </asp:GridView>


    </div> 

</asp:Content>

