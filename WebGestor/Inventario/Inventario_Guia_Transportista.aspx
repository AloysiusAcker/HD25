<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inventario_Guia_Transportista.aspx.vb" Inherits="Inventario_Inventario_Guia_Transportista" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>

<%@ Register Src="../Contador.ascx" TagName="Contador" TagPrefix="uc5" %>
<%@ Register Src="../MProyLeft.ascx" TagName="MProyLeft" TagPrefix="uc6" %>
<%@ Register Src="../MProyRight.ascx" TagName="MProyRight" TagPrefix="uc7" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Gestor Plus</title>
    <link href="../EstiloWebTec.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Script/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../Script/upclick-min.js"></script>
    <script lang="es-pe" type="text/javascript">
        function btnCamara_onclick() {
            var vDNI = $.trim(document.getElementById('<%=hndQR.ClientID %>').value);
            var cad = "";
            cad = prompt("Nombre Foto Guía", vDNI);
            if (cad != null) {
                if (cad.length >= 1) {
                    var frm = document.forms[0];

                    var hndDNI = frm.getElementsByTagName("input")[0];
                    hndDNI.value = cad;
                    //hndDNI.value = 100000;

                    document.getElementById('btnCamara').style.display = "none";

                    var afuFoto = document.getElementById("afuFoto");
                    afuFoto.style.display = "block";
                } else {
                    alert('Es un nombre incorrecto')
                }
            }
        }

        function FileUpload() {
            upclick(
                {
                    element: "afuFoto",
                    action: "fotoUpload.ashx",
                    action_params: {
                        'hndDNI': ''
                    },
                    onstar:
                        function (filename) {
                            var imgLoader = document.getElementById('imgLoader');
                            imgLoader.style.display = "block";
                        },
                    oncomplete:
                        function (response_data) {
                            var imgLoader = document.getElementById('imgLoader');
                            imgLoader.style.display = "none";

                            document.getElementById('<%=btnAgregar.ClientID %>').click();
                        }
                });
        }

        window.onload = function () {
            FileUpload();
        }
    </script>
    <script type="text/javascript">
        function MantenSesion() {
            var CONTROLADOR = 'refresh_session.ashx';
            var head = document.getElementsByTagName('head').item(0);
            script = document.createElement('script');
            script.src = CONTROLADOR;
            script.setAtribute('type', 'text/javascript');
            script.defer = true;
            head.appendChild(script);
        }
    </script> 
    <style type="text/css">
        .auto-style2 {
            width: 117px;
            }
    </style>
</head>
<body style="vertical-align: middle; text-align: center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <script lang="es-pe" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(FileUpload);
    </script>
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
   <%--                 <uc6:MProyLeft ID="MProyLeft1" runat="server" />--%>
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
                                        height: 1px; text-align: center;">
                                        Guía del Transportista</div>
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
                                <td align="left" style="width: 100px" valign="top"></td>
                                <td align="left"  colspan="4"  valign="top"></td>
                                <td align="left" style="width: 25px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                                <td align="left" colspan="5" style="height: 22px" valign="top">
                                    <asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w21"></asp:Label>
                                </td>
                                <td align="left" style="width: 25px; height: 22px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px" valign="top"></td>
                                <td align="left" valign="top" colspan="2">
                                    <asp:Button ID="btnListar" runat="server" CssClass="EstiloBoton" Text="Listar" Width="96px" />
                                    <asp:Button ID="BtnIngresarEq" runat="server" CssClass="EstiloBoton" Text="Generar Guía" Width="96px" />
                                </td>
                                <td align="left" style="width: 30px" valign="top"></td>
                                <td align="left" style="width: 420px" valign="top">
                                    <asp:Button ID="BtnLeerGmail" runat="server" Text="Button" />
                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                                <td align="left" style="width: 25px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                                <td align="left" colspan="5" valign="top" style="height: 1px">
                                </td>
                                <td align="left" style="width: 25px; height: 1px;" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                                <td align="left" colspan="5" valign="middle" style="height: 1px">
                                    <asp:Label ID="lblRegistroGuia" runat="server" CssClass="EstiloLabel" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                                </td>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                                <td align="left" style="height: 19px;" valign="top" colspan="5">
                                    <asp:GridView ID="Flexd" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                        <Columns>
                                            <asp:ButtonField CommandName="Cambiar" Text="Cambiar Estado">
                                            <ControlStyle CssClass="EstiloBoton" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:ButtonField>
                                            <asp:ButtonField CommandName="Archivos" Text="Archivos" >
                                            <ControlStyle CssClass="EstiloBoton" />
                                            <ItemStyle VerticalAlign="Top" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="GUIAT_CODIGO" HeaderText="Cód." />
                                            <asp:BoundField DataField="GUIAT_NUMERO" HeaderText="Guía Tradel" />
                                            <asp:BoundField DataField="FECHA_TRASLADO" HeaderText="Fecha Traslado" />
                                            <asp:BoundField DataField="FECHA_GUIA" HeaderText="Fecha Guía" />
                                            <asp:BoundField DataField="GUIA_CODIGO" HeaderText="Cód." />
                                            <asp:BoundField DataField="GUIA_NUMERO" HeaderText="GuíaBBVA" />
                                            <asp:BoundField DataField="BULTO" HeaderText="Bulto" />
                                            <asp:BoundField DataField="PESO" HeaderText="Peso" />
                                            <asp:BoundField DataField="COD_INTERNO" HeaderText="Oficina Código" />
                                            <asp:BoundField DataField="OFICINA" HeaderText="Oficina" />
                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                            <asp:BoundField DataField="GUIREMTD_ESTADO">
                                            <ItemStyle ForeColor="White" Width="0px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 30px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 420px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                                <td align="left" style="height: 19px;" valign="top" colspan="5">
                                    <%--                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>     --%>     
                                            <div id="divEstado" runat="server" visible="false" >
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 750px">                            
                                                    <tr>
                                                        <td align="left" style="height: 19px;" valign="middle">
                                                            <asp:Label ID="lblEtiqueta1" runat="server" Text="Cambio de Estado" CssClass="EstiloLabel" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 80px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 80px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>                        
                                                    <tr>
                                                        <td align="left" style="height: 19px;" valign="middle">
                                                            <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Guía Remisión del Transportista" Width="168px"></asp:Label>
                                                        </td>
                                                        <td align="left" style="height: 19px;" valign="middle" colspan="3">
                                                            <asp:TextBox ID="TxtNroGuiaT" runat="server" Font-Names="Arial" Font-Size="8pt" Width="175px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top">
                                                            <asp:Button ID="BtnGuardar" runat="server" CssClass="EstiloBoton" Text="Guardar" Width="67px" />
                                                        </td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>              
                                                    <tr>
                                                        <td align="left" style="height: 19px; vertical-align: middle;" valign="middle">
                                                            <asp:Label ID="lblEtiqueta4" runat="server" CssClass="EstiloLabel" Text="Número de Guía BBVA "></asp:Label>
                                                        </td>
                                                        <td align="left" style="height: 19px;" valign="top" colspan="3">
                                                            <asp:TextBox ID="TxtNroGuia" runat="server" Font-Names="Arial" Font-Size="8pt" Width="175px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top">
                                                            <asp:Button ID="BtnCerrar" runat="server" CssClass="EstiloBoton" Text="Cerrar" Width="67px" />
                                                        </td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>                   
                                                    <tr>
                                                        <td align="left" style="height: 19px;" valign="middle">
                                                            <asp:Label ID="lblEtiqueta2" runat="server" CssClass="EstiloLabel" Text="Estado Actual"></asp:Label>
                                                        </td>
                                                        <td align="left" style="height: 19px;" valign="middle" colspan="3">
                                                            <asp:TextBox ID="TxtEstadoActual" runat="server" Font-Names="Arial" Font-Size="8pt" Width="175px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>                   
                                                    <tr>
                                                        <td align="left" style="height: 19px;" valign="middle">
                                                            <asp:Label ID="lblEtiqueta3" runat="server" CssClass="EstiloLabel" Text="Cambiar Estado a"></asp:Label>
                                                        </td>
                                                        <td align="left" style="height: 19px;" valign="middle" colspan="3">
                                                            <asp:DropDownList ID="DdlEstado" runat="server" CssClass="EstiloDropDownList" Width="180px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top">
                                                            <asp:TextBox ID="txtCodGuia" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="29px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>               
                                                    <tr>
                                                        <td align="left" style="height: 19px;" valign="middle">
                                                            <asp:Label ID="lblEtiqueta6" runat="server" CssClass="EstiloLabel" Text="Peso"></asp:Label>
                                                        </td>
                                                        <td align="left" style="height: 19px;" valign="middle" colspan="2">
                                                            <asp:TextBox ID="txtPeso" runat="server" Width="175px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 80px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td align="left" style="width: 100px; height: 19px;" valign="top">
                                                            <asp:TextBox ID="txtCodGuiaT" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 19px;" valign="middle">
                                                            <asp:Label ID="lblEtiqueta5" runat="server" CssClass="EstiloLabel" Text="Fecha Estado"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="height: 19px;" valign="middle">
                                                            <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" style="margin-bottom: 0px" Width="175px"></asp:TextBox>
                                                            <%--                                                            <cc1:calendarextender ID="txtFecha_CalendarExtender" runat="server" CssClass="Calendar" Enabled="True" Format="yyyy/MM/dd" PopupButtonID="txtFecha" TargetControlID="txtFecha">
                                                            </cc1:calendarextender>--%>
                                                        </td>
                                                        <td style="width: 80px; height: 19px;" valign="top"></td>
                                                        <td style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>
                            
                                                    <tr>
                                                        <td style="height: 19px;"></td>
                                                        <td colspan="3" style="height: 19px;">
                                                            <input type="button" id="afuFoto" value="Subir Foto" style="width: 117px;height: 43px; display:none; " />
                                                            <input type="button" id="btnCamara" value="Foto Guía" onclick="btnCamara_onclick();" class="auto-style2" style="font-family: Arial;" />
                                                            <asp:Button ID="btnAgregar" runat="server" style="display:none" />
                                                            <img id="imgLoader" src="Fotos/loading.gif" alt="" style="display:none;" />
                                                        </td>
                                                        <td style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td style="width: 100px; height: 19px;" valign="top"></td>
                                                        <td style="width: 110px; height: 19px;" valign="top"></td>
                                                    </tr>
                            
                                                    <tr>
                                                        <td align="left" colspan="7" style="height: 19px;" valign="middle">
                                                            <input type="hidden" id="hndQR" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="divArchivo" runat="server" visible="false" >       
                                                <div>

                                                </div>
                                                <asp:GridView ID="FlexAr" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                    <Columns>
                                                        <asp:BoundField DataField="GUIA_T" HeaderText="Guía Tradel" />
                                                        <asp:BoundField DataField="GUIA_NUMERO" HeaderText="Guia BBVA" />
                                                        <asp:TemplateField HeaderText="Nombre Archivo">
                                                                <ItemTemplate>
                                                                    <div id="Doc" runat="server" style="width: 150px; height: 22px">
                                                                    </div>                                    
                                                                </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="GUIREM_CODIGO">
                                                        <ItemStyle ForeColor="White" Width="0px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ARCHIVO_CODIGO">
                                                        <ItemStyle ForeColor="White" Width="0px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView> 
                                            </div> 
                                    <%--                                       </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Flexd" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                </td>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 30px; height: 19px;" valign="top">&nbsp;</td>
                                <td align="left" style="width: 420px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                            </tr>
                        </table>
                    </div> 
                </td>
            </tr>
            <tr>
                <td style="width: 200px; height: 15px" ></td>
                <td style="width: 611px; height: 15px; font-weight: bold; font-size: 13pt; vertical-align: middle; color: darkgray; font-style: italic; text-align: center; font-variant: normal;" valign="top" colspan="2">
                    Derechos Reservados: HAC-DATA</td>
                <td style="width: 100px; height: 15px" ></td>
            </tr>
            <tr>
                <td style="width: 200px; height: 15px" ></td>
                <td colspan="2" style="font-weight: bold; font-size: 13pt; vertical-align: middle;
                    width: 611px; color: darkgray; font-style: italic; height: 15px; text-align: center;
                    font-variant: normal" >
                </td>
                <td style="width: 100px; height: 15px" ></td>
            </tr>
        </table>
        <%--        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="Flexd" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>

