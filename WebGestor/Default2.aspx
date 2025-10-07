<%@ Page Language="VB" AutoEventWireup="false" CodeFile ="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>Untitled Page</title>
    <link href="EstiloWebTec.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style2 {
            width: 811px;
            height: 7px;
        }
        .auto-style3 {
            height: 15px;
        }
    </style>
    </head>
<body style="vertical-align: middle; text-align: center">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 911px; background-color: white;">
            <tr>
<%--                <td align="left" style="width: 200px; height: 160px; vertical-align: middle; text-align: center; background-image: url(Fotos/proceso.JPG);" valign="top"></td>
                <td align="left" style="width: 611px; height: 160px;" valign="top">
                   <img src="Fotos/LOGO WEBCASH-06.jpg" style="width: 611px"/>
                </td>
                <td align="left" style="width: 100px; height: 160px;" valign="top"></td>--%>

                <td align="left" style="width: 200px;" valign="center">
                    <img src="Fotos/LOGO WEBCASH-06.jpg" style="width: 200px; height: 60px;" /></td>
                <td align="left" style="width: 611px; height: 60px;" valign="top" colspan="2">
                    <img src="Fotos/LOGO WEBCASH-06.jpg" style="width: 611px; height: 60px;" />
                </td>
                <td align="left" style="width: 100px; height: 60px;" valign="top"></td>


            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 16px; width: 511px;" valign="top">
                    <asp:LinkButton ID="Inicio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="15px" PostBackUrl="~/Default2.aspx" Width="60px" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton" Font-Italic="True">Inicio</asp:LinkButton><asp:LinkButton ID="PaginaP" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="15px" PostBackUrl="~/PaginaPrincipal.aspx" Width="100px" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton" Font-Italic="True">PáginaPrincipal</asp:LinkButton>
                    <asp:LinkButton ID="btnCambioPass" runat="server" CssClass="EstiloBoton" Font-Bold="False"
                        Font-Italic="True" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" Height="15px" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" PostBackUrl="~/Sistema/SegSistema_CambioContraseña.aspx"
                        Width="120px">Cambiar Contraseña</asp:LinkButton>
                    <asp:LinkButton ID="Cerrar" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="15px" PostBackUrl="~/Salida.aspx" Width="100px" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton" Font-Italic="True">Cerrar Sesión</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 200px; height: 17px; background-position: center center; background-repeat: no-repeat; background-color: transparent; text-align: left;" valign="top"> </td>
                <td align="left" colspan="2" style="height: 17px; text-align: right; width: 711px;" valign="top">
                    <div id="lblAgrup" runat="server" style="width: 700px; color: seagreen; font-family: Arial;
                        height: 17px; text-align: right; font-size: 8pt; font-style: italic; display: inline;">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle" class="auto-style3" colspan="2">
                    <div id="divtitle" runat="server" style="text-align: center; vertical-align: middle">
                    <asp:Label ID="Label1" runat="server" Font-Size ="9pt" Text="Elegir Empresa" CssClass="EstiloTitle"></asp:Label>                    
                    </div>
                </td>
                <td align="left" style="width: 100px; height: 15px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="height: 6px;" valign="top" colspan="3">
                              <img src="Fotos/lineaCas.JPG" style="background-image: url('Fotos/lineaCas.JPG')" class="auto-style2" />
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 200px; height: 15px" valign="middle"></td>
                <td align="left" valign="middle">
                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" font-color="black" Font-Names="Arial" Font-Size="10pt" GridLines="None">
                        <Columns>
                            <asp:ButtonField CommandName="Entrar" Text="Entrar" />
                            <asp:BoundField DataField="Empresa" />
                            <asp:BoundField DataField="nombre">
                            <ItemStyle ForeColor="White" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td align="left" style="width: 100px; height: 15px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 200px;" valign="top"></td>
                <td align="left" valign="top" colspan="2"></td>
            </tr>
            <tr>
                <td align="left" style="width: 200px; height: 15px;" valign="top"></td>
                <td align="left" valign="top" height="15px" colspan="2"></td>
            </tr>
            <tr>
                <td align="left" style="width: 200px; height: 15px;" valign="top"></td>
                <td align="left" height="15px" valign="top" colspan="2"></td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="width: 811px; height: 15px; font-weight: bold; font-size: 13pt; vertical-align: middle; color: darkgray; font-style: italic; text-align: center; font-variant: normal;" valign="top">
                    Derechos Reservados: HAC-DATA</td>
                <td align="left" style="width: 100px; height: 15px" valign="top"></td>
            </tr>
        </table>
    </form>
</body>
</html>



