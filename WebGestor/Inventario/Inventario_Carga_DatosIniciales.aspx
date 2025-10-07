<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Carga_DatosIniciales.aspx.vb" Inherits="Inventario_Inventario_Carga_DatosIniciales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Carga Datos Iniciales " CssClass="Titulos" />
        </div> 
    </div>
    <br />  
    
    
    <div id="MovFilas" >
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Lbl4" CssClass="control-label-2" runat="server" Text="Fila empieza"></asp:Label>
                <asp:TextBox ID="TxtIni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Lbl5" CssClass="control-label-2" runat="server" Text="Fila termina"></asp:Label>
                <asp:TextBox ID="Txtfin" runat="server" CssClass="form-control"></asp:TextBox>
            </div>             
            <div class="col-lg-5">
                <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="Fila termina" ForeColor="white"></asp:Label>
                <asp:FileUpload ID="fileUpload" runat="server"  CssClass="form-control" />
            </div> 
        </div>
        <div class="row">          
            <div class="col-lg-3">
                <asp:Button ID="BtnUpload" runat="server" Text="Cargar Datos Iniciales"  CssClass="form-control btn btn-default" visible="true"  />
            </div> 
        </div>
    </div> 


</asp:Content>

