<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Cas_RedireccionarIncidencia.aspx.vb" Inherits="Cas_RedireccionarIncidencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Busqueda</title>
    <Link href ="EstiloWebEmp.css" type="text/css" rel="stylesheet">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
</head>
<body style="background-color: darkgray">
    <form id="Form1" runat="server">
    <div style="background-color: darkgray">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 530px">
            <tr>
                <td style="width: 50px; height: 15px">
                </td>
                <td style="width: 30px; height: 15px">
                </td>
                <td style="width: 70px; height: 15px">
                </td>
                <td style="width: 80px; height: 15px">
                </td>
                <td style="width: 150px; height: 15px">
                </td>
                <td style="width: 50px; height: 15px">
                </td>
                <td style="width: 20px; height: 15px">
                </td>
                <td style="width: 80px; height: 15px">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="lbl1M" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Text="Nº de Incidente" Width="75px"></asp:Label>
                </td>
                <td style="vertical-align: middle; width: 70px; height: 22px">
                    <asp:TextBox ID="txtNIncidente" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Width="68px"></asp:TextBox>
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                </td>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="Label15M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Inicia Llamada"
                        Width="69px"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtIniLlamada" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" MaxLength="8" ReadOnly="True"
                        Width="72px"></asp:TextBox>
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                    <asp:Button ID="btnRegresar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnRegresar_Click"
                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Consultar" Width="85px" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 50px; height: 22px">
                    <asp:Label ID="Label1M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Usuario"
                        Width="45px"></asp:Label>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:TextBox ID="txtNUsuario" runat="server" AutoPostBack="True" BorderColor="Black"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        MaxLength="7" Width="98px"></asp:TextBox>
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                    <asp:Label ID="Label3M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Banco/Oficina"
                        Width="71px"></asp:Label>
                </td>
                <td colspan="4" style="vertical-align: middle; height: 22px">
                    <asp:TextBox ID="txtNOficina" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True"
                        Width="298px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle; width: 50px; height: 22px">
                    <asp:Label ID="Label2M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres"
                        Width="45px"></asp:Label>
                </td>
                <td colspan="4" style="vertical-align: middle; height: 22px">
                    <asp:TextBox ID="txtNNombre" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True"
                        Width="328px"></asp:TextBox>
                </td>
                <td style="vertical-align: middle; width: 50px; height: 22px">
                    <asp:Label ID="Label4M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Teléfono"
                        Width="45px"></asp:Label>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:TextBox ID="txtNTelefono" runat="server" BorderColor="Black" BorderStyle="Outset"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True"
                        Width="98px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="Label7M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Componente"
                        Width="50px"></asp:Label>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="Label5M" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Elemento"
                        Width="50px"></asp:Label>
                </td>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                    <asp:DropDownList ID="cboNComponente" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="150px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:DropDownList ID="cboNElemento" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="228px">
                    </asp:DropDownList>
                </td>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                    <asp:DropDownList ID="cboNElemento2" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="148px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción del Problema"></asp:Label>
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 150px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 50px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 20px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                </td>
            </tr>
            <tr>
                <td colspan="8" style="vertical-align: middle; height: 22px">
                    <asp:TextBox ID="txtNDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="140px" MaxLength="2000" TextMode="MultiLine" Width="528px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Solución"></asp:Label>
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 150px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 50px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 20px; height: 22px">
                </td>
                <td style="vertical-align: middle; width: 80px; height: 22px">
                </td>
            </tr>
            <tr>
                <td colspan="8" style="vertical-align: middle; height: 22px">
                    <asp:TextBox ID="txtNSolucion" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="140px" MaxLength="2000" TextMode="MultiLine" Width="528px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="Label9m" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Importancia"
                        Width="69px"></asp:Label>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    <asp:DropDownList ID="cboNImportancia" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="148px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    &nbsp;<asp:Label ID="Label11m" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Text="Tipo" Width="23px"></asp:Label>
                    <asp:DropDownList ID="cboNTipo" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="147px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="vertical-align: middle; height: 22px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="8" style="vertical-align: middle; height: 22px">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 50px; height: 19px">
                </td>
                <td style="width: 30px; height: 19px">
                </td>
                <td style="width: 70px; height: 19px">
                </td>
                <td style="width: 80px; height: 19px">
                </td>
                <td style="width: 150px; height: 19px">
                </td>
                <td style="width: 50px; height: 19px">
                </td>
                <td style="width: 20px; height: 19px">
                </td>
                <td style="width: 80px; height: 19px">
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
