<%@ Page Language="VB" MasterPageFile="~/Menu/PagMenu.master" AutoEventWireup="false" CodeFile="IngresarElemento.aspx.vb" Inherits="Menu_IngresarElemento" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
    <script language="JavaScript" type="text/javascript">
            var x=5000;
            var Guardar;
            function asignarRutaFoto()
            {
                var tiempo,tiempo2;      
                if(x>=0)
                {
                    x-=1;
                    if (UBICACIONTMP.value=="")
                        {
                         tiempo2=setTimeout("asignarRutaFoto()",50); 
                            
                        }
                    else
                        {
                        UBICACION.value=UBICACIONTMP.value;
                        x=-1;
                        __doPostBack('linkPostBack','');
                        }
                }
            
            }    
            function validarkey()
            {
                 // Para que use el browse y no teclee la ruta
                 alert ("Presione el boton Browse para buscar una foto")
                 return false;
             }
         
 </script>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: seagreen; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        MENU</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4" valign="top">
                    <img src="../Menu/Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 14px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 14px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 14px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px;" valign="top">
                    <asp:Label ID="ELEMENTO_NOMBRE" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Nombre</asp:Label>
                </td>
                <td align="left" style="vertical-align: middle; width: 381px; height: 19px; text-align: left"
                    valign="top">
                    <asp:TextBox ID="txtNombre" runat="server" Font-Names="Arial" Font-Size="8pt" Height="50px"
                        Width="370px" Visible="False" MaxLength="500" TextMode="MultiLine"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:Label ID="ELEMENTO_NOMBRE2" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Nombre (Html)</asp:Label>
                </td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:TextBox ID="txtNombreHtml" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="50px" Width="370px" Visible="False" MaxLength="500" TextMode="MultiLine"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:Label ID="ELEMENTO_CATEGORIA" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Categoria</asp:Label>
                </td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:DropDownList ID="cboCategoria" runat="server" Width="376px" Visible="False" Font-Names="Arial" Font-Size="8pt">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:Label ID="ELEMENTO_DESCRIP_CORTA" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Descripción Corta</asp:Label></td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:TextBox ID="txtDescripCorta" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="50px" Width="370px" Visible="False" MaxLength="500" TextMode="MultiLine"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:Label ID="ELEMENTO_DESCRIP_LARGA" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Descripción Completa</asp:Label></td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:TextBox ID="txtDescripcCompleta" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="50px" Width="370px" Visible="False" MaxLength="1500" TextMode="MultiLine"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 78px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 78px" valign="top">
                    <asp:Label ID="ELEMENTO_IMAGEN" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Visible="False" Width="160px">Imagen</asp:Label></td>
                <td align="left" style="width: 381px; height: 78px" valign="top">
                    <input id="Archivo" runat="server" style="font-size: 8pt; width: 315px; font-family: Arial"
                        type="file" />
                    <asp:Button ID="btnImg" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Imagen" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Height="21px" Width="56px" ForeColor="Gray" /><br />
                    <asp:Image ID="Img" runat="server" Visible="False" /></td>
                <td align="left" style="width: 25px; height: 78px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:Label ID="ELEMENTO_FECHA1" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Fecha</asp:Label></td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:TextBox ID="txtFecha" runat="server" Width="179px" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:TextBox>
                    </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 21px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 21px" valign="top">
                    <asp:Label ID="ELEMENTO_COMPLETAR1" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Comentario 1</asp:Label></td>
                <td align="left" style="width: 381px; height: 21px" valign="top">
                    <asp:TextBox ID="txtComentario1" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="50px" Width="370px" Visible="False" MaxLength="500" TextMode="MultiLine"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 21px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    <asp:Label ID="ELEMENTO_COMPLETAR2" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="160px" Height="16px" Visible="False">Comentario 2</asp:Label></td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:TextBox ID="txtComentario2" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="50px" Width="370px" Visible="False" MaxLength="1500" TextMode="MultiLine"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                    </td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 19px; text-align: right" valign="top">
                    <asp:Button ID="btnGuardar" runat="server"
                        BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                        CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="67px" ForeColor="Gray" /><asp:Button
                            ID="btnRegresar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                            BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" onmouseout="this.style.fontWeight='normal'"
                            onmouseover="this.style.fontWeight='bolder'" PostBackUrl="_Default.aspx" Text="Regresar"
                            Width="67px" ForeColor="Gray" /></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 170px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 381px; height: 19px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="366px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha">
    </cc1:CalendarExtender>
                    <asp:LinkButton ID="LinkPostBack" runat="server"></asp:LinkButton>
                    <asp:Image ID="IMAGEN" runat="server" Height="30px" Width="28px" Visible="False" />&nbsp;
    <asp:HiddenField ID="UBICACION" runat="server" />
</asp:Content>

