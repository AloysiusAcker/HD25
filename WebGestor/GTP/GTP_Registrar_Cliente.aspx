<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="GTP_Registrar_Cliente.aspx.vb" Inherits="GTP_GTP_Registrar_Cliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label5" runat="server" Text="Registrar Cliente" CssClass="Titulos"></asp:Label><br />
    <br />
    <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" CssClass="col-lg-2 control-label-2" Text="DNI"></asp:Label>
                <div class="col-lg-3">
                    <asp:TextBox ID="TxtRuc" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:TextBox>
                </div>
            </div>
            
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" CssClass="col-lg-2 control-label-2" Text="Apellido Paterno"></asp:Label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TxtApePat" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" CssClass="col-lg-2 control-label-2" Text="Apellido Materno"></asp:Label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TxtApeMat" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" runat="server" CssClass="col-lg-2 control-label-2" Text="Nombres"></asp:Label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TxtNombres" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="LblProceso" runat="server" CssClass="col-lg-2 control-label-2" Text="Telefono"></asp:Label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <asp:Label ID="lblCodCliente" runat="server" CssClass="EstiloLabel" Visible="False"></asp:Label>
            
            <div class="form-group">
                <asp:Label ID="LblFechaIni" runat="server" CssClass="col-lg-2 control-label-2" Text="Correo Electronico"></asp:Label>
                <div class="col-lg-4">
                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblCoCliente" runat="server" CssClass="col-lg-2 control-label-2" Text=""  Visible="False"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Button ID="BtnRegistrar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Registrar" />
                <asp:Button ID="BtnLimpiar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Limpiar" />
                <asp:Button ID="BtnBuscar" runat="server" ControlStyle-CssClass=" btn btn-default" Text="Mostrar o Crear Nuevo Incidente" visible="false" />
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

