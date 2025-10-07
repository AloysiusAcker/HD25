<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Imprimir_Placas.aspx.vb" Inherits="Inventario_Inventario_Imprimir_Placas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       
<%--    <script>
        document.getElementById('<%= FileUpload.ClientID %>').addEventListener('change', function () {
            var fileName = this.files[0] ? this.files[0].name : "Ningún archivo seleccionado";
            document.getElementById('fileName').textContent = fileName;
        });
    </script>--%>
    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Imprimir Placas" CssClass="Titulos" />
            </div> 
        </div>
 
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Label ID="Label19" runat="server" Text="Placa" CssClass="control-label-2" />
                <asp:TextBox ID="TxtPlacaIni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-xs-6">
                <asp:Label ID="Label1" runat="server" Text="Placa" CssClass="control-label-2" />
                <asp:TextBox ID="TxtPlacaFin" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2 col-xs-6">
            </div>
            <div class="col-md-2 ">
                <asp:Label ID="Label2" runat="server" Text="Placa" CssClass="control-label-2" forecolor="White" />
                <asp:Button ID="btnGenerate" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Generar PDF" OnClick="btnGenerate_Click" />
            </div> 
        </div>
                
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="Label11" CssClass="control-label-2" runat="server" Text="Archivo"></asp:Label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control btn btn-default" />
            </div>           

            <div class="col-md-2">
                <asp:Label ID="Label10"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="form-control btn btn-default" Text="Imprimir Placas" OnClick="BtnCargaArchivo_Click" />
            </div>
        </div> 
        
<%--        <div class="row">
        <div class="form-group">
            <label class="btn btn-primary">
                Seleccionar Archivo
                <asp:FileUpload ID="FileUpload" runat="server" CssClass="file-upload" style="display:none;" />
            </label>
            <span id="fileName" class="file-name">Ningún archivo seleccionado</span>
        </div>
        </div> --%>
    </div> 
</asp:Content>

