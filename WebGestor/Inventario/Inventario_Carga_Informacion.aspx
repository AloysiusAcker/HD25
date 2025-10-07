<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Carga_Informacion.aspx.vb" Inherits="Inventario_Inventario_Carga_Informacion" title="Gestor" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width:700px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" style="height: 50px" valign="top" colspan="5">
                    <div id="div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Lista de Información Cargada</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height:20px" valign="top"></td>
                <td align="left" style="width: 80px; height:20px" valign="top"></td>
                <td align="left" style="width: 120px; height:20px" valign="top"></td>
                <td align="left" style="width: 150px; height:20px" valign="top"></td>
                <td align="left" style="width: 150px; height:20px" valign="top"></td>
                <td align="left" style="width: 150px; height:20px" valign="top"></td>
                <td align="left" style="width: 25px; height:20px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="middle" colspan="5">
                    <asp:Label ID="lblError" runat="server" CssClass="EstiloLabel" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="middle" colspan="3">
                    <asp:CheckBox ID="chkDenominacion" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Denominación" AutoPostBack="True" />
                    <asp:DropDownList ID="cboDenominacion" runat="server" CssClass="EstiloDropDownList"  Enabled="false" ></asp:DropDownList>
                </td>
                <td align="left" style="width: 150px" valign="top"></td>
                <td align="left" style="width: 150px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="middle" colspan="3">
                    <asp:CheckBox ID="chkMarca" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Marca" AutoPostBack="True" />
                    <asp:DropDownList ID="cboMarca" runat="server" CssClass="EstiloDropDownList" Enabled="false" ></asp:DropDownList>
                </td>
                </td>
                <td align="left" style="width: 150px" valign="top"></td>
                <td align="left" style="width: 150px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height:30px" valign="top"></td>
                <td align="left" style="width: 90px; height:20px" valign="middle">
                    <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton" Text="Listar" Width="80px" />
                </td>
                <td align="left" style="width: 110px; height:30px" valign="top"></td>
                <td align="left" style="width: 150px; height:30px" valign="top"></td>
                <td align="left" style="width: 150px; height:30px" valign="top"></td>
                <td align="left" style="width: 150px; height:30px" valign="top"></td>
                <td align="left" style="width: 25px; height:30px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height:20px" valign="top"></td>
                <td align="left" valign="middle" colspan="5">
                    <asp:Label ID="lblRegistro" runat="server" CssClass="EstiloLabel" Font-Bold="True" ForeColor="Maroon" Text="0 Registros"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height:20px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="top" colspan="5">
                    <div id="DivLista" runat="server" style="overflow: scroll;height:500px"  >
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" PageSize="500" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="SERIE_NUMERO" HeaderText="Nro. Serie" />
                                <asp:BoundField DataField="SERIE_MATERIAL" HeaderText="Material" />
                                <asp:BoundField DataField="SERIE_DENOMINACION" HeaderText="Denominación"  />
                                <asp:BoundField DataField="SERIE_EQUIPO" HeaderText="Equipo"  />
                                <asp:BoundField DataField="SERIE_T" HeaderText="T"  />
                                <asp:BoundField DataField="SERIE_ACTIVOFIJO" HeaderText="Activo fijo"  />
                                <asp:BoundField DataField="SERIE_SN" HeaderText="SN"  />
                                <asp:BoundField DataField="SERIE_CE_COSTO" HeaderText="Ce. Costo"  />
                                <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adq."  />
                                <asp:BoundField DataField="SERIE_STATUSU"  HeaderText="Stat. Usu." />
                                <asp:BoundField DataField="SERIE_STATSIST" HeaderText="Stat. Sist."  />
                                <asp:BoundField DataField="SERIE_MARCA" HeaderText="Marca"  />
                                <asp:BoundField DataField="SERIE_MODELO" HeaderText="Modelo"  />
                                <asp:BoundField DataField="SERIE_OBS" HeaderText="Observación"  />
                                <asp:BoundField DataField="SERIE_ARTICULO" HeaderText="Cod. Art."  />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" colspan ="5" valign="top">

                    &nbsp;</td>
            </tr>
        </table>
    </div>
</contenttemplate>
<triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
    <asp:AsyncPostBackTrigger ControlID="chkMarca" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkDenominacion" EventName="CheckedChanged" />
</triggers>
</asp:UpdatePanel>
</asp:Content>

