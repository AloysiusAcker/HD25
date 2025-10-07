<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Inventario_Definicion.aspx.vb" Inherits="Inventario_Definicion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="LblTitulo" runat="server" Text="Definición de Inventario" CssClass="Titulos"></asp:Label>
        </div>
    </div>
    <br />
	<div class="row">
        <div class="col-lg-3">
            <asp:Button ID="BtnListar" runat="server" Text="Listar"  CssClass="form-control btn btn-default"/>
        </div> 
        <div class="col-lg-3">
            <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="form-control btn btn-default"/>
        </div> 
    </div> 
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
	        <div class="row">
                <div class="col-lg-3">
                    <asp:Label ID="LblCodigo" runat="server" Text="Código:" CssClass="ontrol-label-2" Visible="False"></asp:Label>
                    <asp:TextBox ID="TxtCodigo" runat="server" Visible="False" CssClass="form-control" Enabled="False"></asp:TextBox>
                </div>      
                <div class="col-lg-6">
                    <asp:Label ID="LblDescripción" runat="server" Text="Descripción:" CssClass="control-label-2" Visible="False"></asp:Label>
                    <asp:TextBox ID="TxtDescripcion" runat="server" Visible="False" CssClass="form-control"  ></asp:TextBox>
                </div> 
                <div class="col-lg-3" >
                    <asp:Label ID="Label2" runat="server" Text="Grabar:" CssClass="control-label-2" ForeColor="White" ></asp:Label>
                    <asp:Button ID="BtnGrabar" runat="server" Text="Grabar" Visible="False" ControlStyle-CssClass="form-control btn btn-default"/>
                </div> 
            </div>
     
            <div class="row">       
            </div>

            <div class="row">
                <div class="col-lg-3" >
                    <asp:Label ID="LblFecha" runat="server" Text="Fecha:" CssClass="control-label-2"  Visible="False"></asp:Label>
                    <asp:TextBox ID="TxtFecha" runat="server" class="form-control"  Visible="False"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" TargetControlID="TxtFecha"></cc1:CalendarExtender>
                </div>
                <div class="col-lg-6">
                    <asp:Label ID="LblResponsable" runat="server" Text="Responsable:" CssClass="control-label-2" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtResponsable" runat="server" Visible="False" CssClass="form-control"  ></asp:TextBox>
                </div>
                <div class="col-lg-3" >
                    <asp:Label ID="Label3" runat="server" Text="Cancelar:" CssClass="control-label-2" ForeColor="White" ></asp:Label>
                    <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" Visible="False" ControlStyle-CssClass="form-control btn btn-default"/>
                </div> 
            </div>
            <div class="row">
            </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="BtnGrabar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
            <div class="row">
                <div class="col-lg-12" >
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Image" ControlStyle-CssClass=" btn btn-default" ImageUrl="~/Icono/Editar_opt.png">
                                <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                <ItemStyle Height="10px" Width="10px" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Image" CommandName="Eliminar" Text="Eliminar" ControlStyle-CssClass="btn btn-default" ImageUrl="~/Icono/delete2_opt.png">
                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                <ItemStyle Height="10px" Width="10px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="INVENT_CODIGO" HeaderText="Codigo" SortExpression="INVENT_CODIGO" />
                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                            <asp:BoundField DataField="INVENT_DESCRIPCION" HeaderText="Descripcion" SortExpression="INVENT_DESCRIPCION" />
                            <asp:BoundField DataField="INVENT_RESPONSABLE" HeaderText="Responsable" SortExpression="INVENT_RESPONSABLE" />
                        </Columns>
                    </asp:GridView>
                </div> 
            </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnGrabar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    <div id="ModalMensajeUbic" class="modal fade" role="dialog" data-backdrop="static" style="position:fixed; top:25%;"> 
        <div class="modal-dialog modal-sm">
    	    <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align:center; background-color:white;">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="form-group">
                                <asp:Label runat="server" ID="MensajeUbic" class="col-lg-12"/>
                            </div>
    					    <div class="col-lg-12">
                                <asp:Button ID="BtnUOk" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div> 
</asp:Content>
