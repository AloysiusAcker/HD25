<%@ Page Title="" Language="VB" MasterPageFile="~/CRM/PagPrincipal_CRM.master" AutoEventWireup="false" CodeFile="Inventario_Estadistica.aspx.vb" Inherits="Inventario_Inventario_Estadistica" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <%--  <div class="container-fluid">--%>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Resumen Estadística Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="Label2" runat="server" Text="Exportar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label3" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label1" runat="server" Text="Exportar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnExportarInvOk" runat="server" Text="Exportar Bienes Inventariado Ok" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label4" runat="server" Text="Listar" CssClass="control-label-2" ForeColor="White"  />
                <asp:Button ID="BtnExportarNuevos" runat="server" Text="Exportar Bienes Nuevos" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblRegistro" runat="server" Text="" CssClass="control-label-2"  />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvUbicaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:BoundField DataField="CodInterno_Ubicacion" HeaderText="Cód. Interno" SortExpression="CodInterno_Ubicacion" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Estado" SortExpression="ELEMEN_VALOR" />
                                <asp:BoundField DataField="Cant_Inicial" HeaderText="Cant. Bienes Inicial" SortExpression="Cant_Inicial" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cant_Inventariados" HeaderText="Inventariados" SortExpression="Cant_Inventariados">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cant_Inventariados_Ok" HeaderText="Inventariados Ok" SortExpression="Cant_Inventariados_Ok" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nuevos" HeaderText="Nuevos" SortExpression="Nuevos" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="No_Inventariado" HeaderText="No Inventariado" SortExpression="No_Inventariado" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="P_Habido" HeaderText="% Habidos" SortExpression="P_Habido" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="P_Nuevos" HeaderText="% Nuevos" SortExpression="P_Nuevos" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="P_NoEncontrados" HeaderText="% No Encontrados" SortExpression="P_NoEncontrados" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>    
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    <%--</div>--%> 
</asp:Content>

