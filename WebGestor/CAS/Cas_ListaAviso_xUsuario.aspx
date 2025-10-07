<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_ListaAviso_xUsuario.aspx.vb" Inherits="CAS_Cas_ListaAviso_xUsuario" %>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="form-horizontal">                
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <asp:Label ID="Label5" runat="server" Text="Lista de Avisos" CssClass="TitulosCas"></asp:Label>
                </div>
            </div> 
           
            <div class="form-group col-lg-12">
                <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
            </div> 

            <div class="form-horizontal">
                <div class="row"> 
                    <div class="col-lg-4">     
                        <div class="form-group">       
                            <asp:Label ID="LblEtq_10" runat="server" CssClass="col-lg-3 control-label-2" Text="Avisos"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlAviso"  CssClass="form-control-ddl" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>                
                    </div>
                    <div class="col-lg-4"> 
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" CssClass="col-lg-3 control-label-2" Text="Estado"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlEstado"  CssClass="form-control-ddl" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div> 
                    <div class="col-lg-4"> 
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" CssClass="col-lg-3 control-label-2" Text="Tipo"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlTipo" CssClass="form-control-ddl" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>                    
                </div>                
            </div>

            <div class="form-horizontal">
                <div class="row"> 
                    <div class="col-lg-4">     
                        <div class="form-group">       
                            <asp:Label ID="Label6" runat="server" CssClass="col-lg-3 control-label-2" Text="Aplicativo"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlAplicativo"  CssClass="form-control-ddl" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>                
                    </div>
                    <div class="col-lg-4"> 
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" CssClass="col-lg-3 control-label-2" Text="Producto"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlProducto"  CssClass="form-control-ddl" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div> 
                    <div class="col-lg-4"> 
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" CssClass="col-lg-3 control-label-2" Text="Sub-Producto"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlSubProd" CssClass="form-control-ddl" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>                    
                </div>                
            </div>

 <%--           <div class="form-group">
                <div class="col-lg-9">
                    <asp:Button ID="BtnListar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Listar" />
                </div> 
            </div>    --%>
            
            <div class="form-horizontal">
                <div class="row">
                    <div class="col-lg-4" id="DivAviso" runat="server" >
                        <asp:Label ID="Label4" runat="server" CssClass="col-lg-6 control-label-2" Text="Avisos"></asp:Label>
                        <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridViewLista">
                            <Columns>
<%--                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:Image ID="Image" runat="server" ImageUrl="~/Icono/ok20.png"></asp:Image>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:ButtonField ButtonType="Image" CommandName="Ok" Text="Ok" ImageUrl="~/Icono/ok20.png">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="Aviso" HeaderText="Aviso">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AVISO_TIPO1" HeaderText="Tipo">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHAHORA_REG" HeaderText="Fecha">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AVISO_DESCRIPCION" HeaderText="Descripci&#243;n">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                        </asp:GridView>
                    </div>  
                    <div class="col-lg-8" id="DivAvisoDet" runat="server" > 
                        <asp:Label ID="Label3" runat="server" CssClass="col-lg-6 control-label-2" Text="Detalle"></asp:Label>
                        <asp:DetailsView ID="FlexDetalle" runat="server"  CssClass="table table-bordered GridView" AutoGenerateRows="False">
                            <FooterStyle BackColor="WHITE" ForeColor="Black"></FooterStyle>
                            <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
                            <Fields>
                            <asp:BoundField DataField="AVISO" HeaderText="Nro. Aviso">
<%--                                <HeaderStyle BorderColor="White" />
                                <ItemStyle BorderColor="White" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="AVISO_TIPO1" HeaderText="Tipo Aviso">
<%--                                <HeaderStyle BorderColor="White" />
                                <ItemStyle BorderColor="White" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="FECHAHORA_REG" HeaderText="Fecha y Hora">
<%--                                <HeaderStyle BorderColor="White" />
                                <ItemStyle BorderColor="White" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="APLICACION" HeaderText="Aplicativo">
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                            </asp:BoundField>
                            <asp:BoundField DataField="SUBPRODUCTO" HeaderText="Sub-Producto">
                            </asp:BoundField>
                            <asp:BoundField DataField="AVISO_DESCRIPCION" HeaderText="Descripci&#243;n">
                                <%--<HeaderStyle BorderColor="White" />--%>
<%--                                <ItemStyle BorderColor="White" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="AVISO_DETALLE" HeaderText="Detalle">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="AVISO_ESTADO1" HeaderText="Estado">
<%--                                <HeaderStyle BorderColor="White" />
                                <ItemStyle BorderColor="White" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="AVISO_ESTADO2" HeaderText="Aviso">
<%--                                <HeaderStyle BorderColor="White" />
                                <ItemStyle BorderColor="White" />--%>
                            </asp:BoundField>
                            </Fields>
                            <HeaderStyle BackColor="#333333" BorderColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></EditRowStyle>
                        </asp:DetailsView>
                        <asp:GridView ID="gvArchivo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridViewLista" BorderColor="White">
                            <Columns>
                                <asp:BoundField DataField="ARCHIVO" HeaderText="Archivo">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="White" ></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <div id="Doc" runat="server" style="width: 50px; height: 22px"></div>                                    
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AVISOARCH_CODIGO" >
                                    <FooterStyle BorderColor="White" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="0px" BorderColor="White" ForeColor="White"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div > 
            </div>                   


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DdlTipo" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlEstado" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlAviso" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowDataBound" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

