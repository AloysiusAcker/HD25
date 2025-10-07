<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Analisis.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Analisis" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Lista de Evaluaciones por Tienda</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 650px; height: 20px;" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Size="8" Font-Names="arial" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="bottom"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 23px;" valign="top"></td>
                <td align="justify" style="width: 750px; height: 23px;" valign="middle">
                    <asp:Label ID="Label1" runat="server" Text="Año" CssClass="EstiloLabel"></asp:Label>
                    <asp:DropDownList ID="DdlAño" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"></asp:DropDownList>
               </td>
                <td align="left" style="width: 25px; height: 23px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 23px;" valign="top"></td>
                <td align="justify" style="width: 750px; height: 23px;" valign="middle">
                    <asp:Label ID="lblEtiqueta" runat="server" Text="Proceso" CssClass="EstiloLabel"></asp:Label>
                    <asp:DropDownList ID="DdlProceso" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True"></asp:DropDownList>
               </td>
                <td align="left" style="width: 25px; height: 23px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width:750px; height: 20px;" valign="bottom"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                        <div id="divLista">
                            <asp:GridView ID="gwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" GridLines="None" BorderWidth="0px">
                                <Columns>
                                    <asp:BoundField DataField="Oficina_Nombre" HeaderText="Oficina" />
                                    <asp:BoundField DataField="oficina_codigo">
                                    <ItemStyle ForeColor="White" Width="0px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="c1" HeaderText="Promedio" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCodEval1" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtFecha1" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtEstado1" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtPromedio1" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCodEval2" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtFecha2" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtEstado2" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtPromedio2" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCodEval3" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtFecha3" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtEstado3" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtPromedio3" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>                                    
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCodEval4" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtFecha4" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtEstado4" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtPromedio4" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>                                                                       
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCodEval5" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtFecha5" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtEstado5" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtPromedio5" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>                                                                       
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCodEval6" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtFecha6" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtEstado6" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            <br />
                                            <asp:TextBox ID="txtPromedio6" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

