<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Relacionar.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Relacionar" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Relación del Personal RM y DM</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Size="8" Font-Names="arial" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="height: 20px;" valign="top" colspan="3">  
                    
                    <cc1:TabContainer id="Ficha" runat="server" Width="100%"  Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" ActiveTabIndex="1">
                        <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                        <HeaderTemplate>Asignar DM</HeaderTemplate>
                        <ContentTemplate>

                    <div>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                    <asp:Button ID="BtnAsignar" runat="server" CssClass="EstiloBoton" Text="Asignar DM" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 20px;" valign="top" colspan="2">
                                    <asp:GridView ID="GwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                        <Columns>
                                            <asp:ButtonField CommandName="Oficina" Text="Oficina">
                                            <ItemStyle CssClass="EstiloBoton" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="c1" HeaderText="RM" />
                                            <asp:BoundField DataField="c2" HeaderText="Código" />
                                            <asp:BoundField DataField="c3" HeaderText="DM" />
                                            <asp:BoundField DataField="c4" HeaderText="Código" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width:10%; height: 20px;" valign="top"></td>
                                <td align="left" style="width:90%; height: 20px;" valign="bottom"></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 20px;" valign="top" colspan="2">

                                    <div id="divAsignar" runat="server" visible="False"  >
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td align="left" style="height: 20px;" valign="middle" colspan="3">
                                                <asp:Label ID="Label2" runat="server" Text="Asignar DM" Font-Bold="True" ForeColor="Maroon" CssClass="EstiloLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width:10%; height: 20px;" valign="middle">
                                                <asp:Label ID="Label3" runat="server" Text="RM" CssClass="EstiloLabel"></asp:Label></td>
                                            <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                                <asp:DropDownList ID="ddlRM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnGuardar" runat="server" CssClass="EstiloBoton" Text="Guardar" />
                                                <asp:Button ID="BtnCancelar" runat="server" CssClass="EstiloBoton" Text="Cancelar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width:10%; height: 20px;" valign="middle">
                                                <asp:Label ID="Label1" runat="server" Text="Personal DM" CssClass="EstiloLabel"></asp:Label>
                                            </td>
                                            <td align="left" style="width:50%; height: 20px;" valign="middle"></td>
                                            <td align="left" style="width:40%; height: 20px;" valign="middle"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px;" valign="middle" colspan="3">
                                                <asp:GridView ID="GwListaDM" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkUser" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PERSON_CODIGO" HeaderText="Usuario" />
                                                        <asp:BoundField DataField="NOMBRE_PERSONAL" HeaderText="Nombres y Apellidos" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        </table>

                                    </div>
                                </td>
                            </tr>
                        </table>
                </div>
  
                        </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                                <HeaderTemplate>Asignar Oficina a DM</HeaderTemplate>
                        <ContentTemplate>

                    <div>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                    <asp:Button ID="BtnAsignarOf" runat="server" CssClass="EstiloBoton" Text="Asignar Oficina " />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 20px;" valign="top" colspan="2">
                                    <asp:GridView ID="GwListaOficinaxDM" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                        <Columns>
                                            <asp:BoundField DataField="c1" HeaderText="DM" />
                                            <asp:BoundField DataField="c2" HeaderText="Código" />
                                            <asp:BoundField DataField="c3" HeaderText="Oficina" />
                                            <asp:BoundField DataField="c4" HeaderText="Código" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width:10%; height: 20px;" valign="top"></td>
                                <td align="left" style="width:90%; height: 20px;" valign="bottom"></td>
                            </tr>
                            <tr>
                                <td align="left" style="height: 20px;" valign="top" colspan="2">

                                    <div id="divOficina" runat="server" visible="False"  >
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td align="left" style="height: 20px;" valign="middle" colspan="3">
                                                <asp:Label ID="Label4" runat="server" Text="Asignar Oficina" Font-Bold="True" ForeColor="Maroon" CssClass="EstiloLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width:10%; height: 20px;" valign="middle">
                                                <asp:Label ID="Label5" runat="server" Text="DM" CssClass="EstiloLabel"></asp:Label></td>
                                            <td align="left" style="height: 20px;" valign="middle" colspan="2">
                                                <asp:DropDownList ID="DdlDM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnGuardarOf" runat="server" CssClass="EstiloBoton" Text="Guardar" />
                                                <asp:Button ID="BtnCancelarOf" runat="server" CssClass="EstiloBoton" Text="Cancelar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width:10%; height: 20px;" valign="middle">
                                                <asp:Label ID="Label6" runat="server" Text="Oficinas" CssClass="EstiloLabel"></asp:Label>
                                            </td>
                                            <td align="left" style="width:50%; height: 20px;" valign="middle"></td>
                                            <td align="left" style="width:40%; height: 20px;" valign="middle"></td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="height: 20px;" valign="middle" colspan="3">
                                                <asp:GridView ID="GwOficinas" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkOf" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="OFICINA_CODIGO" HeaderText="Cod. Of." />
                                                        <asp:BoundField DataField="OFICINA_NOMBRE" HeaderText="Oficina Nombre" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        </table>

                                    </div>
                                </td>
                            </tr>
                        </table>
                </div>
  
                        </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 650px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 650px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

