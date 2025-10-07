<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Carga_Atms.aspx.vb" Inherits="Inventario_Inventario_Carga_Atms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Carga Atms" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="Label6" CssClass="control-label-2" runat="server" Text="Archivo"></asp:Label>
                <!-- Contenido dentro del UpdatePanel -->
                <div class="mb-3">
                    <%--<input type="file" id="fileUpload" runat="server" class ="form-control" />--%>
                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                </div>
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label10"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="form-control btn btn-default" Text="Carga Excel"  OnClick="BtnCargaArchivo_Click" />                        
            </div> 
        </div>          
        <div class="row">            
            <div class="col-md-3">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro2" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>     
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvListaAtms" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView"  >
                            <Columns>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="SERIE_ATM_NROTERMINAL" HeaderText="Nro. Terminal" SortExpression="SERIE_ATM_NROTERMINAL" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            <%--</ContentTemplate>
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </div> 

</asp:Content>

