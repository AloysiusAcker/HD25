<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contabilidad_Registrar_Clientes.aspx.vb" Inherits="Contabilidad_Contabilidad_Registrar_Clientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Gestor</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <link href="../Css_WebGestor.css" rel="stylesheet" />
    <link href="../EstiloWebTec.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section id="web">
            <img src="../Fotos/LOGO WEBCASH-06.jpg" />
            <div id="lblFecha" runat="server" style="display: inline; font-size: 8pt;  font-family: Arial;  text-align: right"></div><br>
            <div id="lblAgrup" runat="server" style="display: inline; font-size: 8pt;  font-family: Arial;  text-align: right"></div><br>
            <img src="../Fotos/lineaCas.jpg" /> 

            <div id="lblTitulo" class="title">
                <asp:Label ID="lblTitle" runat="server" Text="Registrar Cliente" Font-Names ="Arial" Font-Size ="14px"></asp:Label>        
            </div>
                <asp:button ID="Cerrar0" runat="server" Font-Names="Arial" Text="Regresar" CssClass="botoncito_cerrar"></asp:button>
                <asp:button ID="BtnGuardar" runat="server" Font-Names="Arial" Text="Guardar" CssClass="botoncito_cerrar"></asp:button>
                <asp:button ID="BtnLimpiar" runat="server" Font-Names="Arial" Text="Limpiar" CssClass="botoncito_cerrar"></asp:button>
            <br />
             <div class="Colum">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate >
                        <asp:Label ID="lblError" runat="server" Text="" Font-Names ="arial" Font-Size ="8pt" ForeColor="red" ></asp:Label>
                        <br />
                        <asp:Label id="Label25" runat="server" Font-Size="8pt" Font-Names="Arial" Text="RUC ó DNI"></asp:Label>  
                        <asp:TextBox id="txtRuc" runat="server" class="text" ></asp:TextBox>
                        <br />

                        <asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Proyecto"></asp:Label>
                        <asp:DropDownList id="DdlTipoPersona" runat="server"  Font-Size="8pt" Font-Names="Arial"></asp:DropDownList>
                        <br /> 
                    </ContentTemplate> 
                </asp:UpdatePanel> 
            </div> 
        </section>
        <div>
        </div>
    </form>
</body>
</html>
