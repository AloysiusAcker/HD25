<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_.aspx.vb" Inherits="Inventario_Inventario_" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Realizar Inventario</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" colspan="5" style="height: 22px" valign="middle">
                    <asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" ></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="top" colspan="4">
                    <asp:Button ID="BtnIngresarEq" runat="server" CssClass="EstiloBoton_Ac" Text="Iniciar Inventario" Width="150px" style="margin-bottom: 0px" />
                    <asp:Button ID="BtnInvSeguir" runat="server" CssClass="EstiloBoton_Ac" Text="Continuar Inventario" Width="150px" />
                </td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top" height="20"></td>
                <td align="left" style="width: 100px" valign="top" height="20"></td>
                <td align="left" style="width: 90px" valign="top" height="20"></td>
                <td align="left" style="width: 30px" valign="top" height="20"></td>
                <td align="left" style="width: 430px" valign="top" height="20"></td>
                <td align="left" style="width: 100px" valign="top" height="20"></td>
                <td align="left" style="width: 25px" valign="top" height="20"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="top" colspan="5">
                    <div id="DivInv" runat="server" >
                        <table border="0" cellpadding="0" cellspacing="0" style="width:750px;">
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Placa"></asp:Label>
                                </td>
                                <td align="left" valign="middle" colspan="3">
                                            <asp:TextBox ID="txtPlaca" runat="server"  BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <asp:Button ID="BtnAgregar" runat="server" CssClass="EstiloBoton_Ac" Text="Agregar" Width="80px" />
                                 
                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Serie"></asp:Label>
                                </td>
                                <td align="left" valign="middle" colspan="3">

                                            <asp:TextBox ID="txtNroSerie" runat="server"  BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>

                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px; " valign="middle">
                                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Inventario"></asp:Label>
                                </td>
                                <td align="left" valign="middle" colspan="3">
                                    <asp:DropDownList ID="DdlInventario" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px" Width="108px" ></asp:DropDownList>
                                </td>
                                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px; " valign="middle">
                                    <asp:Label ID="lblEtq1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código Inventario"></asp:Label>
                                </td>
                                <td align="left" valign="middle" colspan="3">
                                    <asp:TextBox ID="txtInvCod" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" ></asp:TextBox>
                                </td>
                                <td align="left" style="width: 100px; " valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <asp:Label ID="lblEtq2" runat="server" Font-Names="Arial" Font-Size="8pt"  Text="Nombre Inventario"></asp:Label>
                                </td>
                                <td align="left" valign="middle" colspan="3">
                                    <asp:TextBox ID="txtInvNombre" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Width="200px"></asp:TextBox>
                                    <asp:Button ID="BtnInvBuscar" runat="server" CssClass="EstiloBoton_Ac" Text="Buscar Inventario" Width="150px" Visible="False" />
                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="middle">
                                    <asp:Label ID="lblEtq3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ubicación Destino"></asp:Label>
                                </td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:RadioButtonList ID="optUbicacionD" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Height="1px" RepeatDirection="Horizontal" Width="240px">
                                        <asp:ListItem Value="1">Almacén</asp:ListItem>
                                        <asp:ListItem Value="2">Centro Costo</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="4">

                                        <asp:TextBox ID="txtDCodigo" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="68px"></asp:TextBox>
                                        <asp:Button ID="btnUbica" runat="server" CssClass="EstiloBoton_Ac" Text="..." Width="22px" />
                                        <asp:TextBox ID="txtDDescripcion" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="200px"></asp:TextBox>                                                                      


                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="4">

                                        <asp:Label ID="lblRegistroRe" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon"></asp:Label>
                                        <input id="btnOpen" type="button" value="Si" runat="server" class="EstiloBoton" visible ="false"  />
                                        <asp:Button ID="BtnNo" runat="server" CssClass="EstiloBoton" Text="No" Visible="False" style="width: 28px" />
                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" valign="middle" colspan="4">
                                    <asp:Button ID="BtnGrabarInv" runat="server" CssClass="EstiloBoton_Ac" Text="Grabar Inventario" Width="150px" />
                                 
                                    <asp:Button ID="BtnImprimir" runat="server" Text="Imprimir Placa" CssClass="EstiloBoton_Ac"/>
                                 
                                </td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="top">
                                    <asp:TextBox ID="txtDUbicacion" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="70px"></asp:TextBox></td>                                    
                                <td align="left" style="width: 90px" valign="top">
                                    <asp:TextBox ID="txtInvCodUbic" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px" Visible="False" Width="27px"></asp:TextBox>
                                </td>
                                <td align="left" style="width: 30px" valign="top"></td>
                                <td align="left" style="width: 430px" valign="top"></td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" colspan="5">

                                    <div id="divLista" runat="server">
                                        <asp:GridView ID="gvLista" runat="server" AutoGenerateColumns="False" 
                                                    BorderColor="Gray" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt">
                                            <Columns>
                                                <asp:ButtonField CommandName="Quitar" Text="Quitar">
                                                <ControlStyle CssClass="EstiloBoton" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="c1" HeaderText="Artículo">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c2" HeaderText="Descripción">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c3" HeaderText="Nro. Serie">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c4" HeaderText="Nro. Placa" />
                                                <asp:BoundField DataField="c5" HeaderText="Tipo Bien">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c6" HeaderText="Tipo Ubicación">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c7" HeaderText="Cod. Ubicación">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c8" HeaderText="Descripción Ubicación">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c9">
                                                <ItemStyle ForeColor="White" Width="0px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c10" />
                                                <asp:BoundField DataField="c11" />
                                                <asp:BoundField DataField="c12">
                                                <ItemStyle ForeColor="White" Width="0px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c13">
                                                <ItemStyle ForeColor="White" Width="0px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="c14">
                                                <ItemStyle ForeColor="White" Width="0px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                    </div>                                        
                                </td>                                    
                            </tr>
                            <tr>
                                <td align="left" style="width: 100px" valign="top"></td>                                    
                                <td align="left" style="width: 90px" valign="top"></td>
                                <td align="left" style="width: 30px" valign="top"></td>
                                <td align="left" style="width: 430px" valign="top"></td>
                                <td align="left" style="width: 100px" valign="top"></td>
                            </tr>
                        </table>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                            CacheDynamicResults="True" CancelControlID="btnUbiCerrar" PopupControlID="Panel2"
                                            TargetControlID="btnUbica" X="300" Y="200">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server">
                            <div style="text-align: center">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; background-color: darkgray; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;">
                                <tr>
                                    <td align="left" style="width: 25px; height: 25px" valign="middle">
                                    </td>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 25px; text-align: center"
                                        valign="middle">
                                        <asp:UpdatePanel id="UpdatePanel16" runat="server">
                                            <contenttemplate>
                                            <asp:Label ID="lblBusUbica" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Maroon" Text="Busqueda de Almacén y/o Centro de Costos" Width="280px"></asp:Label>
                                            </contenttemplate>
                                        </asp:UpdatePanel>&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 25px; height: 25px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle">
                                    </td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                                        <asp:UpdatePanel id="UpdatePanel18" runat="server">
                                            <contenttemplate>
                                        <asp:TextBox ID="txtBusCod" runat="server" Font-Names="Arial" Font-Size="8pt" Width="270px"></asp:TextBox>
                                        </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                                        <asp:Button ID="btnUbiCerrar" runat="server" BackColor="LightGray" BorderColor="Silver"
                                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                            Text="Cerrar" Width="80px" /></td>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle">
                                    </td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"
                                            Width="60px"></asp:Label></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                                        <asp:UpdatePanel id="UpdatePanel17" runat="server">
                                            <contenttemplate>
                                            <asp:TextBox ID="txtBusDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="270px"></asp:TextBox>
                                            </contenttemplate>
                                            <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                                        <asp:Button ID="btnUbiListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                            Text="Listar" Width="80px" /></td>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px" valign="middle">
                                    </td>
                                    <td align="left" colspan="3" valign="middle">
                                        <asp:UpdatePanel id="UpdatePanel7" runat="server">
                                            <contenttemplate>
                                                <div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 450px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Height="139px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w64" AutoGenerateColumns="False" Font-Overline="False"><Columns>
                                                <asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="30px"></ControlStyle>

                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CODIGO">
                                                <ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
                                                </asp:BoundField>
                                                </Columns>
                                                </asp:GridView></div>
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="width: 25px" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px; height: 19px;" valign="middle"></td>
                                    <td align="left" valign="middle" style="width: 70px; height: 19px"></td>
                                    <td align="left" valign="middle" style="width: 280px; height: 19px"></td>
                                    <td align="left" valign="middle" style="width: 100px; height: 19px"></td>
                                    <td align="left" style="width: 25px; height: 19px;" valign="middle"></td>
                                </tr>
                            </table>
                            </div>
                        </asp:Panel>     

                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                            CacheDynamicResults="True" CancelControlID="btnInvCerrar" PopupControlID="Panel1"
                                            TargetControlID="BtnInvBuscar" X="300" Y="200">
                        </cc1:ModalPopupExtender>
                        <asp:Panel ID="Panel1" runat="server">
                            <div style="text-align: center">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; background-color: darkgray; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;">
                                <tr>
                                    <td align="left" style="width: 25px; height: 25px" valign="middle">
                                    </td>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 25px; text-align: center"
                                        valign="middle">
                                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                                            <contenttemplate>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Maroon" Text="Busqueda de Inventario" Width="280px"></asp:Label>
                                            </contenttemplate>
                                        </asp:UpdatePanel>&nbsp;&nbsp;
                                    </td>
                                    <td align="left" style="width: 25px; height: 25px" valign="middle">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                                        <asp:Button ID="btnInvCerrar" runat="server" BackColor="LightGray" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Cerrar" Width="80px" />
                                        <asp:Button ID="btnInvListar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Listar" Width="80px" />
                                    </td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                                        &nbsp;</td>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left"></asp:Label></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left"></td>
                                    <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                                        &nbsp;</td>
                                    <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px" valign="middle">
                                    </td>
                                    <td align="left" colspan="3" valign="middle">
                                        <asp:UpdatePanel id="UpdatePanel5" runat="server">
                                            <contenttemplate>
                                                <div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 450px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV1" runat="server">
                                                    <asp:GridView id="gvListaInv" runat="server" Height="139px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w64" AutoGenerateColumns="False" Font-Overline="False"><Columns>
                                                <asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="30px"></ControlStyle>

                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="INVENT_CODIGO" HeaderText="Inventario">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVENT_DESCRIPCION" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ubiccodigo" HeaderText="Ubic.">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ubicdes" HeaderText="Descripción">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVENTUBIC_UBIC_CODIGO">
                                                <ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVENTUBIC_UBIC_TIPO">
                                                <ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVENTUBIC_CODIGO">
                                                <ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
                                                </asp:BoundField>
                                                </Columns>
                                                </asp:GridView>
                                                </div>
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnInvCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                                <asp:AsyncPostBackTrigger ControlID="btnInvListar" EventName="Click"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="width: 25px" valign="middle"></td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 25px; height: 19px;" valign="middle"></td>
                                    <td align="left" valign="middle" style="width: 70px; height: 19px"></td>
                                    <td align="left" valign="middle" style="width: 280px; height: 19px"></td>
                                    <td align="left" valign="middle" style="width: 100px; height: 19px"></td>
                                    <td align="left" style="width: 25px; height: 19px;" valign="middle">&nbsp;</td>
                                </tr>
                            </table>
                            </div>
                        </asp:Panel>     

                    </div>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>  
        </table>
       </div>
            
<%--            <asp:SqlDataSource ID="Conex_Emp" runat="server" ConnectionString="<%$ ConnectionStrings:Cn_bdEmpresa %>" SelectCommand="Prc_Reporte_Recepcion" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0001" Name="CodEmpresa" SessionField="Cod_Empresa" Type="String" />
                    <asp:SessionParameter DefaultValue="1" Name="CodRecep" SessionField="Cod_Recep" Type="Double" />
                </SelectParameters>
            </asp:SqlDataSource>--%>
    </contenttemplate>
    <triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnAgregar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
        <asp:AsyncPostBackTrigger ControlID="BtnGrabarInv" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="gvListaInv" EventName="RowCommand" />
    </triggers>
</asp:UpdatePanel>



</asp:Content>

