<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Articulo_Imagen.aspx.vb" Inherits="Inventario_Inventario_Articulo_Imagen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <h1 class="Titulos">Relación de Artículos con imágenes</h1>
        <div class="row">
            <div class="col-lg-3">
                <asp:Button ID="btnExportImages" runat="server" CssClass="form-control btn btn-default" Text="Export Images to Excel" OnClick="btnExportImages_Click" />
            </div>
            <div class="col-lg-3">
                <asp:Button ID="BtnListar" runat="server" CssClass="form-control btn btn-default" Text="Listar" />
            </div> 
            <div class="col-lg-3">
                <asp:Button ID="BtnListarSI" runat="server" CssClass="form-control btn btn-default" Text="Listar sin Imagen" />
            </div> 
        </div>
        <div class="row">
            <div class="col-md-3">  
                <asp:Label ID="SKU" runat="server" CssClass="control-label-2" Text="SKU"></asp:Label>    
                <asp:TextBox ID="TxtSku" runat="server" CssClass="form-control"></asp:TextBox>                                     
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblNroSerie" runat="server" CssClass="control-label-2" Text="Familia"></asp:Label>
                <asp:TextBox ID="TxtFamilia" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <asp:Label ID="Label1" runat="server" CssClass="control-label-2" Text="Descripción"></asp:Label>
                <asp:TextBox ID="TxtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>  
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView ID="GvLista" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="ART." SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="SKU" SortExpression="ART_SKU" />
                                <asp:BoundField DataField="ART_FAMILIA" HeaderText="FAMILIA" SortExpression="ART_FAMILIA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="DESCRIPCION" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="ART_MARCA" HeaderText="MARCA" SortExpression="ART_MARCA" />
                                <asp:BoundField DataField="ART_MODELO" HeaderText="MODELO" SortExpression="ART_MODELO" />
                                <asp:BoundField DataField="ART_LARGO_CM" HeaderText="LARGO" SortExpression="ART_LARGO_CM" />
                                <asp:BoundField DataField="ART_ANCHO_CM" HeaderText="ANCHO" SortExpression="ART_ANCHO_CM" />
                                <asp:BoundField DataField="ART_ALTO_CM" HeaderText="ALTA" SortExpression="ART_ALTO_CM" />
                                <asp:BoundField DataField="ART_M3" HeaderText="M3" SortExpression="ART_M3" />
                                <asp:BoundField DataField="ART_PESO_GR" HeaderText="PESO" SortExpression="ART_PESO_GR" />
                                <%--<asp:TemplateField ItemStyle-Width="100px" HeaderText="Imagen">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "ProductoSku.ashx?id=" & HttpUtility.UrlEncode(Eval("ART_CODIGO").ToString()) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="Imagen">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ProductoSku.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("ART_CODIGO") IsNot DBNull.Value, Eval("ART_CODIGO"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnListarSI" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>        
    </div>    

</asp:Content>

