<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_AnalisisL.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_AnalisisL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div id="DivContenedor" runat="server" visible ="true" >
                <table border="0" cellpadding="0" cellspacing="0" style="width:800px;">
                    <tr>
                        <td align="left" colspan="6" style="height: 50px" valign="top">
                                <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline;
                                    font-weight: bold; font-size: 14pt; vertical-align: middle; width: 700px; color: gray;
                                    font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                                    height: 1px; text-align: center">
                                    Cuadros de Mando RMs, DMs y Tiendas </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="6" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
                    </tr>                    
                    <tr>
                        <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                        <td align="left"  colspan="4" style="height: 20px;" valign="middle">
                            <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="1" Width="750px" Font-Names="Arial" Font-Size="8pt" AutoPostBack="True">
                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Lista RMs DMs">                                            
                                    <ContentTemplate >                                                
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 740px">
                                           <tr>
                                              <td align="left" colspan="4" valign="middle">
                                                 <asp:Label ID="lblError" runat="server" Font-Size="8pt" Font-Names="arial" ForeColor="Red"></asp:Label>
                                              </td>
                                           </tr>
                                                            <tr>
                                                                <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="EstiloLabel" Text="Año"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                                    <asp:DropDownList ID="DdlAño" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton" Text="Listar" />
                                                                </td>
                                                                <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                                <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="RM"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                                    <asp:DropDownList ID="ddlRM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddlRM_SelectedIndexChanged" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                                <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="EstiloLabel" Text="DM"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                                    <asp:DropDownList ID="ddlDM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                                <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="width: 80px; height:10px;" valign="middle"></td>
                                                                <td align="left" style="width: 240px; height: 10px;" valign="middle">&nbsp;</td>
                                                                <td align="left" style="width: 80px; height: 10px;" valign="middle"></td>
                                                                <td align="left" style="width: 340px; height: 10px;" valign="middle"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" style="height: 20px;" valign="middle" colspan="4">
                                                                    <div id="DivLista1" runat="server" style="overflow: scroll; width: 640px;">
                                                                        <asp:GridView ID="gwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Mes_Nro" HeaderText="Mes Cód." />
                                                                                <asp:BoundField DataField="mes_nombre" HeaderText="Mes" />
                                                                                <asp:BoundField DataField="soa_aprobadas" HeaderText="SOA #Aprobadas">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle BackColor="#00CC00" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="soa_auditadas" HeaderText="SOA #Auditadas">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle BackColor="#00CC00" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="Soa_aprobadas_porcentaje" HeaderText="SOA % Aprobadas">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="soa_promedio_porcentaje" HeaderText="SOA % Promedio">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="QASA_aprobadas" HeaderText="QASA SOA #Aprobadas">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle BackColor="#00CC00" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="QASA_auditadas" HeaderText="QASA SOA #Auditadas">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle BackColor="#00CC00" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="QASA_aprobadas_porcentaje" HeaderText="QASA SOA % Aprobadas">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField DataField="QASA_promedio_porcentaje" HeaderText="QASA SOA % Promedio">
                                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                                </asp:BoundField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>                            
                                                                </td>
                                                            </tr>
                                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Lista por Tienda">
                                    <ContentTemplate >
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 740px">
                                                    <tr>
                                                        <td align="left" colspan="4" valign="middle">
                                                            <asp:Label ID="lblErrorT" runat="server" Font-Size="8pt" Font-Names="arial" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                            <asp:Label ID="Label13" runat="server" CssClass="EstiloLabel" Text="Año"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                            <asp:DropDownList ID="DdlAño2" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <asp:Button ID="BtnTLista" runat="server" CssClass="EstiloBoton" Text="Listar" />
                                                        </td>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                        <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                            <asp:Label ID="Label4" runat="server" CssClass="EstiloLabel" Text="RM"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                            <asp:DropDownList ID="ddlTRM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                        <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                            <asp:Label ID="Label5" runat="server" CssClass="EstiloLabel" Text="DM"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                            <asp:DropDownList ID="ddlTDM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                        <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                            <asp:Label ID="Label6" runat="server" CssClass="EstiloLabel" Text="Tienda"></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                            <asp:DropDownList ID="ddlTienda" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                        <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 80px; height:10px;" valign="middle"></td>
                                                        <td align="left" style="width: 240px; height: 10px;" valign="middle"></td>
                                                        <td align="left" style="width: 80px; height: 10px;" valign="middle"></td>
                                                        <td align="left" style="width: 340px; height: 10px;" valign="middle"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="height: 20px;" valign="middle" colspan="4">
                                                            <div>
                                                                <asp:GridView ID="dgwListaTienda" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Mes_Nro" HeaderText="Mes Cód." />
                                                                        <asp:BoundField DataField="mes_nombre" HeaderText="Mes" />
                                                                        <asp:BoundField DataField="soa_promedio_porcentaje" HeaderText="SOA % Promedio">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="QASA_promedio_porcentaje" HeaderText="QASA SOA % Promedio">
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>                            
                                                        </td>
                                                    </tr>
                                                </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="SOA">
                                    <ContentTemplate >
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 740px">
                                                <tr>
                                                    <td align="left" colspan="4" valign="middle">
                                                        <asp:Label ID="lblErrorSoa" runat="server" Font-Size="8pt" Font-Names="arial" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label14" runat="server" CssClass="EstiloLabel" Text="Año"></asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                                        <asp:DropDownList ID="DdlAño3" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                        <asp:Button ID="Button1" runat="server" CssClass="EstiloBoton" Text="Listar" />
                                                        <asp:CheckBox ID="chkDesaprob" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Text="Solo Desaprobados" />
                                                    </td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label7" runat="server" CssClass="EstiloLabel" Text="RM"></asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                                        <asp:DropDownList ID="ddlSoaRM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label8" runat="server" CssClass="EstiloLabel" Text="DM"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                        <asp:DropDownList ID="ddlSoaDM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label9" runat="server" CssClass="EstiloLabel" Text="Tienda"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                        <asp:DropDownList ID="ddlSoaTienda" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height:10px;" valign="middle"></td>
                                                    <td align="left" style="width: 240px; height: 10px;" valign="middle"></td>
                                                    <td align="left" style="width: 80px; height: 10px;" valign="middle"></td>
                                                    <td align="left" style="width: 340px; height: 10px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="height: 20px;" valign="middle" colspan="4">
                                                        <div id="DivLista3" runat="server" style="overflow: scroll; width:640px;">
                                                            <asp:GridView ID="gwListaSoa" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DM" HeaderText="DM">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="RM" HeaderText="RM">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TIENDA" HeaderText="TIENDA">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C01" HeaderText="ENE">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C02" HeaderText="FEB">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C03" HeaderText="MAR">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C04" HeaderText="ABR">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C05" HeaderText="MAY">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C06" HeaderText="JUN">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C07" HeaderText="JUL">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C08" HeaderText="AGO">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C09" HeaderText="SET">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C10" HeaderText="OCT">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C11" HeaderText="NOV">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C12" HeaderText="DIC">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>                            
                                                    </td>
                                                </tr>
                                            </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="QASA SOA">
                                    <ContentTemplate >
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 740px">
                                                <tr>
                                                    <td align="left" colspan="4" valign="middle">
                                                        <asp:Label ID="lblErrorQ" runat="server" Font-Size="8pt" Font-Names="arial" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label15" runat="server" CssClass="EstiloLabel" Text="Año"></asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                                        <asp:DropDownList ID="DdlAño4" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                        <asp:Button ID="BtnListarQasa" runat="server" CssClass="EstiloBoton" Text="Listar" />
                                                        <asp:CheckBox ID="chkDesaprobQasa" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Text="Solo Desaprobados" />
                                                    </td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label10" runat="server" CssClass="EstiloLabel" Text="RM"></asp:Label>
                                                    </td>
                                                    <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                                        <asp:DropDownList ID="ddlRMQasa" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label11" runat="server" CssClass="EstiloLabel" Text="DM"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                        <asp:DropDownList ID="ddlDMQasa" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle">
                                                        <asp:Label ID="Label12" runat="server" CssClass="EstiloLabel" Text="Tienda"></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 240px; height: 20px;" valign="middle">
                                                        <asp:DropDownList ID="ddlTiendaQasa" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"  >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="left" style="width: 80px; height: 20px;" valign="middle"></td>
                                                    <td align="left" style="width: 340px; height: 20px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 80px; height:10px;" valign="middle"></td>
                                                    <td align="left" style="width: 240px; height: 10px;" valign="middle"></td>
                                                    <td align="left" style="width: 80px; height: 10px;" valign="middle"></td>
                                                    <td align="left" style="width: 340px; height: 10px;" valign="middle"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="height: 20px;" valign="middle" colspan="4">
                                                        <div id="Div1" runat="server" style="overflow: scroll; width:640px;">
                                                            <asp:GridView ID="gwListaQasa" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DM" HeaderText="DM">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="RM" HeaderText="RM">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="TIENDA" HeaderText="TIENDA">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C01" HeaderText="ENE">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C02" HeaderText="FEB">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C03" HeaderText="MAR">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C04" HeaderText="ABR">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C05" HeaderText="MAY">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C06" HeaderText="JUN">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C07" HeaderText="JUL">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C08" HeaderText="AGO">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C09" HeaderText="SET">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C10" HeaderText="OCT">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C11" HeaderText="NOV">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="C12" HeaderText="DIC">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" VerticalAlign="Middle" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>                            
                                                    </td>
                                                </tr>
                                            </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                        <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                    </tr>
                </table>
            </div>
</asp:Content>

