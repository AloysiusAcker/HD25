<%@ Page Language="VB" MasterPageFile="~/PaginaMaestra_Web.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" title="GestorPlus" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%--<%@ Register assembly="DevExpress.Web.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section id="web">
            <div class="form-greeting">
	            <div id="Titulo" runat="server" class="titleLogin" >
                   Iniciar sesión
	            </div>
            </div>
<%--            <div class="field-wrapper">--%>
	            <asp:TextBox ID="txtUsuario" runat="server" Font-Names="Roboto, arial, sans-serif" cssclass="textLogin"></asp:TextBox>
<%--            <div class="field-placeholder"><span>Ingresar Usuario</span></div>--%>
	        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario" Display="Dynamic" EnableClientScript="False" ErrorMessage="Ingresar Usuario" Font-Names="Arial" Font-Size="8pt">*</asp:RequiredFieldValidator><br />
<%--            </div>--%>
	        <asp:TextBox ID="txtClave" cssclass="textLogin" runat="server" TextMode="Password" EnableViewState="True"></asp:TextBox>
	        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtClave" Display="Dynamic" EnableClientScript="False" ErrorMessage="Ingresar Contraseña" Font-Names="Arial" Font-Size="8pt">*</asp:RequiredFieldValidator><br />
	    <br/>
        <asp:LinkButton ID="btnContraseña" runat="server" Font-Bold="False" Font-Names="Roboto, arial, sans-serif"
                                            Font-Size="8pt" ForeColor="Gray" PostBackUrl="~/Sistema/SegSistema_OlvidoContraseña.aspx" CssClass="LINK" style="vertical-align: middle; text-align: center" meta:resourcekey="btnContraseñaResource1">¿Olvidaste tu contraseña?</asp:LinkButton>
        <asp:Button ID="cmdEntrar" runat="server" class="EstiloBoton_AC" Font-Bold="False"  Text="Iniciar Sesión"  />
        <br />
        <asp:Label ID="lblMensajeLogin" runat="server"  Font-Names="Roboto, arial, sans-serif" Font-Size="8pt" ForeColor="Red"> </asp:Label>
        <asp:Label ID="lblMensajeUsuario" runat="server"  Font-Names="Roboto, arial, sans-serif" Font-Size="8pt" ForeColor="Blue" Height="1px" Visible="False" >Mensaje</asp:Label>
    </section>
</asp:Content>

