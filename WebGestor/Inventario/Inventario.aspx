<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inventario.aspx.vb" Inherits="Inventario_Inventario" %>

<%@ Register Src="../Contador.ascx" TagName="Contador" TagPrefix="uc5" %>
<%@ Register Src="../MProyLeft.ascx" TagName="MProyLeft" TagPrefix="uc6" %>
<%@ Register Src="../MProyRight.ascx" TagName="MProyRight" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../EstiloWebTec.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style2 {
            height: 19px;
        }
        .auto-style3 {
            /*BORDER-RIGHT: white 1px Outset;
    BORDER-TOP: GRAY 1px Outset;
    BORDER-LEFT: GRAY 1px Outset;
    BORDER-BOTTOM: GRAY 1px Outset;*/
        CURSOR: hand;
            COLOR: GRAY;
            BACKGROUND-COLOR: white;
            font-family: Arial;
            font-size: 8pt;
            font-weight: bolder;
            text-align: CENTER;
        }
    </style>
</head>
<body style="vertical-align: middle; text-align: center">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 911px; background-color: white;">
            <tr>
                <td align="left" style="width: 200px; height: 160px; vertical-align: middle; text-align: center; background-image: url(../Fotos/proceso.JPG);" valign="top">
                    </td>
                <td align="left" style="width: 611px; height: 160px;" valign="top" colspan="2">
                   <img src="../Fotos/LOGO WEBCASH-06.jpg" style="width: 611px"/>
                </td>
                <td align="left" style="width: 100px; height: 160px;" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 16px; width: 511px;" valign="top">
                    <asp:LinkButton ID="Inicio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="15px" PostBackUrl="~/Default.aspx" Width="60px" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton" Font-Italic="True">Inicio</asp:LinkButton><asp:LinkButton ID="PaginaP" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="15px" PostBackUrl="~/PaginaPrincipal.aspx" Width="100px" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton" Font-Italic="True">PáginaPrincipal</asp:LinkButton>
                    <asp:LinkButton ID="btnCambioPass" runat="server" CssClass="EstiloBoton" Font-Bold="False"
                        Font-Italic="True" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" Height="15px" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'" PostBackUrl="~/Sistema/SegSistema_CambioContraseña.aspx"
                        Width="120px">Cambiar Contraseña</asp:LinkButton>
                    <asp:LinkButton
                                ID="Cerrar" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="15px" PostBackUrl="~/Salida.aspx" Width="100px" Font-Underline="False" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" CssClass = "EstiloBoton" Font-Italic="True">Cerrar Sesión</asp:LinkButton></td>
                <td align="left" colspan="2" style="width: 611px; height: 16px; text-align: right"
                    valign="top">
                    <div
                                    id="lblFecha" runat="server" style="font-weight: normal; font-size: 8pt; text-transform: capitalize;
                                    width: 350px; color: seagreen; font-family: Arial; height: 16px; text-align: right; font-style: italic; display: inline;">
                                </div>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 200px; height: 17px; background-position: center center; background-repeat: no-repeat; background-color: transparent; text-align: left;" valign="top">
                  <%--  <uc5:Contador ID="Contador1" runat="server" />--%>
                </td>
                <td align="left" colspan="3" style="height: 17px; text-align: right; width: 711px;" valign="top">
                    <div id="lblAgrup" runat="server" style="width: 700px; color: seagreen; font-family: Arial;
                        height: 17px; text-align: right; font-size: 8pt; font-style: italic; display: inline;">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 200px;" valign="top">
                    <uc6:MProyLeft ID="MProyLeft1" runat="server" />
                </td>
                <td align="left" valign="top" colspan="3">
                    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Enviar Lista a Tratar</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 90px" valign="top"></td>
                <td align="left" style="width: 70px" valign="top"></td>
                <td align="left" style="width: 30px" valign="top"></td>
                <td align="left" style="width: 460px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" colspan="5" style="height: 22px" valign="top">
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                            <asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w21"></asp:Label>
                    <%--           </ContentTemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>--%>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="width: 90px; height: 22px; vertical-align: middle;" valign="top">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" Width="76px" CssClass="EstiloBoton_Ac" /></td>
                <td align="left" style="vertical-align: middle; width: 70px; height: 22px; text-align: left" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 30px; height: 22px; text-align: center" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 460px; height: 22px; text-align: left" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <%--                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>--%>
                            <asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
                    <%--                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </triggers>
                    </asp:UpdatePanel>--%>

                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div id="DIV2" runat="server" style="width:748px; overflow: auto; border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-right-color: white; border-bottom-color: white; border-left-color: white; border-style: none;" >
                        <%--        <asp:UpdatePanel id="UpdatePanel3" runat="server">
                            <contenttemplate>--%>
                                <asp:GridView id="Flex" runat="server" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" PageSize="1000">
                                    <Columns>
                                        <asp:ButtonField Text="Detalle" CommandName="Detalle">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="Exportar" Text="Exportar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="Enviar" Text="Enviar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COD_REG" HeaderText="# Reg.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FECHA" HeaderText="Fecha">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HORA" HeaderText="Hora">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USUARIO" HeaderText="Usuario">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANT" HeaderText="Cant. Eq." >
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ESTADO">
                                        <ItemStyle ForeColor="White" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
                        <%--                            </contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <%-- <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>--%>
                            <asp:Label id="lblRegDetalle" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
                    <%--            </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </triggers>
                    </asp:UpdatePanel>--%>

                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
               <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div id="DIV1" runat="server" style="width:748px; overflow: auto; border-right: white 1px outset; border-top: white 1px outset; border-left: white 1px outset; border-bottom: white 1px outset; border-style: none;" >
                        <%--                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>--%>
                                <asp:GridView id="FlexDet" runat="server" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" >
                                    <Columns>
                                        <asp:ButtonField Text="Quitar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Artículo">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPOBIEN" HeaderText="Tipo Bien">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adquisición">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Antiguedad" HeaderText="Antiguedad">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                        <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro" />
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
                        <%--                      </contenttemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 90px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 70px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 30px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 460px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px" valign="top" colspan="5">
                    <div id="DivCorreo" runat="server" visible ="false" >
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 750px; background-color: white;">
                            <tr>
                                <td align="left" valign="top" class="auto-style2" colspan="3">
                                    <asp:Label ID="Label2" runat="server" CssClass="EstiloLabel" Font-Bold="True" ForeColor="Maroon" Text="Llenar Datos para el envio de correo"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 90px; height: 19px;" valign="middle">
                                    <asp:Label ID="Label3" runat="server" CssClass="EstiloLabel" Text="Para :"></asp:Label>
                                </td>
                                <td align="left" style="width: 570px; height: 19px;" valign="middle">
                                    <asp:TextBox ID="txtPara" runat="server" CssClass="EstiloTextbox" Width="560px"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 90px; height: 19px;" valign="top">
                                    <asp:Button ID="BtnEnviarCorreo" runat="server" CssClass="auto-style3" Text="Enviar Correo" Width="86px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 90px; height: 19px;" valign="middle">
                                    <asp:Label ID="Label4" runat="server" CssClass="EstiloLabel" Text="Asunto :"></asp:Label>
                                </td>
                                <td align="left" valign="middle" class="auto-style2" colspan="2">
                                    <asp:TextBox ID="txtAsunto" runat="server" CssClass="EstiloTextbox" Width="650px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 90px; height: 19px;" valign="middle">
                                    <asp:Label ID="Label5" runat="server" CssClass="EstiloLabel" Text="Mensaje :"></asp:Label>
                                </td>
                                <td align="left" valign="middle" class="auto-style2" colspan="2">
                                    <asp:TextBox ID="txtMensaje" runat="server" CssClass="EstiloTextbox" Width="650px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 90px; height: 19px;" valign="middle">
                                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Adjuntar Archivo"></asp:Label>
                                </td>
                                <td align="left" valign="middle" class="auto-style2" colspan="2">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="8pt" CssClass="EstiloBoton" Width="634px" />
                                    <asp:Button ID="cmdAddFile" runat="server" Text="+" ToolTip="Añade el fichero a la lista" OnClick="cmdAddFile_Click" CssClass="EstiloBoton" Width="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 90px; height: 19px;" valign="top"></td>
                                <td align="left" valign="top" class="auto-style2" colspan="2">
                                    <asp:ListBox ID="lstFiles" runat="server" Font-Names="Arial" Font-Size="8pt" Width="634px"></asp:ListBox>
                                    <asp:Button ID="cmdDelFile" runat="server" Text="-" ToolTip="Elimina el fichero seleccionado de la lista" OnClick="cmdDelFile_Click" CssClass="EstiloBoton" Width="20px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 90px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 570px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 90px; height: 19px;" valign="top">
                                    <asp:TextBox ID="lblCodLista" runat="server" CssClass="EstiloTextbox" EnableTheming="False" Visible="False" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
            </tr>
        </table>
    </div>
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 200px; height: 15px" valign="middle">
                    
                </td>
                <td align="left" style="width: 611px; height: 15px; font-weight: bold; font-size: 13pt; vertical-align: middle; color: darkgray; font-style: italic; text-align: center; font-variant: normal;" valign="top" colspan="2">
                    Derechos Reservados: HAC-DATA</td>
                <td align="left" style="width: 100px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 200px; height: 15px" valign="middle">
                </td>
                <td align="left" colspan="2" style="font-weight: bold; font-size: 13pt; vertical-align: middle;
                    width: 611px; color: darkgray; font-style: italic; height: 15px; text-align: center;
                    font-variant: normal" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 15px" valign="top">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
