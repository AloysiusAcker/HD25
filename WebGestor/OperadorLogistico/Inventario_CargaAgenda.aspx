<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_Oplogistico.master" AutoEventWireup="false" CodeFile="Inventario_CargaAgenda.aspx.vb" Inherits="Inventario_CargaAgenda" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td align="left" style="width: 25px; height: 50px" valign="top">
            </td>
            <td align="left" colspan="2" style="height: 50px; text-align: center" valign="top">
                <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                    font-size: 14pt; left: 225px; vertical-align: middle; width: 234px; color: gray;
                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                    height: 1px; text-align: center">
                    Importar Agenda</div>
            </td>
            <td align="left" style="width: 25px; height: 50px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4" style="background-image: url(../Fotos/linea.JPG)" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 15px" valign="top">
            </td>
            <td align="left" style="width: 90px; height: 15px" valign="top">
            </td>
            <td align="left" style="width: 460px; height: 15px" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 15px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                <asp:Label ID="lbl1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombre Archivo"
                    Width="90px"></asp:Label></td>
            <td align="left" style="vertical-align: middle; width: 460px; height: 22px" valign="top">
                <asp:FileUpload ID="NomArchivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Style="left: 92px; top: 107px" Width="460px" /></td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
            </td>
            <td align="left" style="vertical-align: middle; width: 460px; height: 22px; text-align: right"
                valign="top">
                <asp:Button ID="btnArchExcel" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnArchExcel_Click"
                    onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                    Text="Importar Arch. Excel" Width="136px" /></td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                <asp:Label ID="lblMensajeExportada" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Maroon"></asp:Label></td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
            <td align="left" colspan="2" style="font-size: 8pt; vertical-align: middle; font-family: Arial;
                height: 22px; text-align: center" valign="top">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="top">
            </td>
        </tr>
    </table>
</asp:Content>

