<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Resumen_Estadistica.aspx.vb" Inherits="Inventario_Inventario_Resumen_Estadistica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../Icono/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../Icono/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>


    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Resumen Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label3" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label1" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnListaBienes" runat="server" Text="Bienes Inventariados" ControlStyle-CssClass="form-control btn btn-default" />
            </div>                               
            <div class="col-md-3">
                <asp:Label ID="Label2" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar bienes" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">                   
                        <div id="accordion" role="tablist" aria-multiselectable="true" runat="server" >
                            <div class="card">
                                <div class="card-header" role="tab" id="section1HeaderId">
                                    <h5 class="mb-0">                            
                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="false" aria-controls="section1ContentId">
                                           Todos los Bienes Inventariados
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                                    <div class="card-body"> 
                                        <div class="row">
                                            <div class="col-lg-12">                                            
                                                <asp:Label ID="lblRegistroInv" runat="server"  CssClass="control-label-2" />
                                            </div>
                                        </div>  
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GvListaBienes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                    <Columns>
                                                        <asp:BoundField DataField="Oficina_Codigo" HeaderText="Cod. Interno" SortExpression="Oficina_Codigo" />
                                                        <asp:BoundField DataField="Oficina" HeaderText="Oficina" SortExpression="Oficina" />
                                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                                                        <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Artículo" SortExpression="Cod_Articulo" />
                                                        <asp:BoundField DataField="Nro_Parte" HeaderText="Nro. Parte" SortExpression="Nro_Parte" />
                                                        <asp:TemplateField HeaderText="Desc. Artículo">
                                                            <ItemTemplate>
                                                                <div class="two-lines-cell">
                                                                    <%# Eval("Descripcion_Articulo") %>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                        <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                                        <asp:TemplateField HeaderText="Est. Inventario">
                                                            <ItemTemplate>
                                                                <div class="two-lines-cell">
                                                                    <%# Eval("Estado_Inventario") %>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SERIE_STATUSU" HeaderText="Stat. Sist." SortExpression="SERIE_STATUSU" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div> 
                                </div> 
                            </div> 
                        </div> 
                    </div> 
                </div> 



                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="ID" CssClass="table table-bordered GridView"
                            OnRowDataBound="OnRowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" src="../Icono/plus.gif" />
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView ID="gv_Child" runat="server" AutoGenerateColumns="false" DataKeyNames="DetalleID" CssClass="table table-bordered GridView" OnRowDataBound="OnRowDataBound2">
                                                <Columns>
                                                    <asp:BoundField DataField="Descripcion" HeaderText="Nombre" />  
                                                    <asp:BoundField DataField="DetalleID" HeaderText="Codigo" />            
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                                    <asp:BoundField DataField="CantBien" HeaderText="Cant. Bien" />
                                                    <asp:BoundField DataField="Inventariado" HeaderText="Inventariado" />
                                                    <asp:BoundField DataField="no_Inventariado" HeaderText="No Inventariado" /> 
                                                     <asp:BoundField DataField="avance" HeaderText="avance" />                                  
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="CantBien" HeaderText="Cant. Bien" />
                                <asp:BoundField DataField="Inventariado" HeaderText="Inventariado" />
                                <asp:BoundField DataField="no_Inventariado" HeaderText="No Inventariado" />
                                <asp:BoundField DataField="avance" HeaderText="avance" />
                                <asp:BoundField DataField="id" HeaderText="id" />
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

