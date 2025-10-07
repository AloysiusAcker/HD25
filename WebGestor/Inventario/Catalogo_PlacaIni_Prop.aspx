<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Catalogo_PlacaIni_Prop.aspx.vb" Inherits="Catalogo_PlacaIni_Prop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" Width="101px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnListar" runat="server" Text="Listar" Width="104px" />
    <br />
    <br />
    <asp:GridView ID="grvDatosPlaca" runat="server" AutoGenerateColumns="False" Height="238px" Width="591px">
        <Columns>
            <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass=" btn btn-default" />
            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass=" btn btn-default" />
            
            <asp:BoundField HeaderText="Código" DataField="PLACA_ALTIBI_CODIGO" SortExpression="PLACA_ALTIBI_CODIGO"/>
            <asp:BoundField HeaderText="Propietario" DataField="ALTIBI_DESCRIPCION" SortExpression="ALTIBI_DESCRIPCION"/>
            <asp:BoundField HeaderText="Placa Inicial" DataField="PLACA_COMIENZA" SortExpression="PLACA_COMIENZA"/>
            <asp:BoundField HeaderText="Ultima Placa"  DataField="PLACA_FIN" SortExpression="PLACA_FIN" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <asp:Label ID="lblPropietario" runat="server" Text="Propietario"></asp:Label> 
    &nbsp;<asp:DropDownList ID="drpProp" runat="server" Width="376px">
    </asp:DropDownList> 
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="110px" />
    <br />
    <br />

    <asp:Label ID="lblPlacaInicial" runat="server" Text="Placa Inicial"></asp:Label> 
    <asp:TextBox ID="txtPlacaIn" runat="server" Width="148px"></asp:TextBox>
    <asp:Label ID="lblPlacaFin" runat="server" Text="Placa Final"></asp:Label>
    <asp:TextBox ID="txtPlacaFin" runat="server" Width="134px"></asp:TextBox>
    &nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="110px" />
    <br />
    <br />

    <br />
    <br />
    <br />
 
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
