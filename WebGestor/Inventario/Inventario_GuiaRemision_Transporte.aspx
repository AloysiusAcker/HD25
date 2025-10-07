<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_GuiaRemision_Transporte.aspx.vb" Inherits="Inventario_Inventario_GuiaRemision_Transporte" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="../Script/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="../Script/upclick-min.js"></script>
<%-- <script type="text/javascript" src="Script/jquery-2.1.4.min.js"></script>--%>
<%--    <script type="text/javascript" src="Script/upclick-min.js"></script>--%>
    <script  type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(FileUpload);
    </script>
    <script  type="text/javascript">
        function btnCamara_onclick() {
            var vDNI = $.trim(document.getElementById('<%=hndQR.ClientID %>').value);
            var cad = "";
            cad = prompt("Ingresar Nro. Guía", vDNI);
            if (cad != null) {
                if ($.isNumeric(cad) && cad.length >= 8) {
                    var frm = document.forms[0];

                    var hndDNI = frm.getElementsByTagName("input")[0];
                    hndDNI.value = cad;
                    //hndDNI.value = 100000;

                    document.getElementById('btnCamara').style.display = "none";

                    var afuFoto = document.getElementById("afuFoto");
                    afuFoto.style.display = "block";
                } else {
                    alert('Es un DNI incorrecto')
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

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>   
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
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 30px" valign="top"></td>
                <td align="left" style="width: 420px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
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
                <td align="left" style="width: 420px" valign="top"></td>
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
                                            <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Guía Remisión del Transportista"></asp:Label>
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
                                        <td align="left" style="height: 19px;" valign="middle">
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
                                        <td align="left" style="width: 100px; height: 19px;" valign="top">
                                            &nbsp;</td>
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
                                        <td align="left" style="height: 19px;" valign="middle">
                                            <asp:Label ID="lblEtiqueta5" runat="server" CssClass="EstiloLabel" Text="Fecha Estado"></asp:Label>
                                        </td>
                                        <td align="left" colspan="2" style="height: 19px;" valign="middle">
                                            <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" style="margin-bottom: 0px" Width="175px"></asp:TextBox>
                                            <cc1:calendarextender ID="txtFecha_CalendarExtender" runat="server" CssClass="Calendar" Enabled="True" Format="yyyy/MM/dd" PopupButtonID="txtFecha" TargetControlID="txtFecha">
                                            </cc1:calendarextender>
                                        </td>
                                        <td align="left" style="width: 80px; height: 19px;" valign="top"></td>
                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                        <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 19px;" ></td>
                                        <td align="left" colspan="5" style="height: 19px;" >  
                                            <input type="button" id="afuFoto" value="Subir Imagen" style="width: 117px;height: 43px; display:none; " />
                                            <input type="button" id="btnCamara" value="Nro. Guía" onclick="btnCamara_onclick();" style="width: 117px;height: 43px;" />
                                            <asp:Button ID="btnAgregar" runat="server" style="display:none" />
                                            <img id="imgLoader" src="Fotos/loading.gif" alt="" style="display:none;" />
        
                                            <%--<figure style="margin:1em 40px;">--%>
                                                <asp:Repeater ID="repFotos" runat="server">
                                                <ItemTemplate>
                                                    <div style="display:inline-block; position:relative; width:220px; margin-bottom:5px">
                                                        <img id="imgFotos" src="Fotos/persona.jpg" alt="" runat="server" style="height:245px;" />
                                                        <div id="objDescrip" runat="server" style="color:rgb(0, 0, 0); background-color:rgb(76, 255, 0); text-align:center;">43059906</div>
                                                    </div>
                                                </ItemTemplate>
                                                </asp:Repeater>
                                                <input type="hidden" id="hndQR" runat="server" />
                                            <%--</figure>--%>
                                        </td>
                                        <td align="left" style="width: 110px; height: 19px;" valign="top"></td>
                                    </tr>
                            
                                    <tr>
                                        <td align="left" colspan="7" style="height: 19px;" valign="middle">
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
    
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Flexd" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>

