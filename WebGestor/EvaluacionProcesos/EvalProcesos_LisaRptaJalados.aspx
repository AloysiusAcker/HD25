<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_LisaRptaJalados.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_LisaRptaJalados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Lista de Respuestas por Oficina</div>
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
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton" Text="Listar" Height="19px" />
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label4" runat="server" CssClass="EstiloLabel" Text="Año"></asp:Label>
                    <asp:DropDownList ID="DdlAño" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" CssClass="EstiloLabel" Text="Proceso"></asp:Label>
                    <asp:DropDownList ID="DdlProceso" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True" Height="19px">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Mes Inicia"></asp:Label>
                    <asp:DropDownList ID="DdlMes" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" CssClass="EstiloLabel" Text="Mes Fin"></asp:Label>
                    <asp:DropDownList ID="DdlMesFin" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label3" runat="server" CssClass="EstiloLabel" Text="RM"></asp:Label>
                    <asp:DropDownList ID="DdlRM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label6" runat="server" CssClass="EstiloLabel" Text="DM"></asp:Label>
                    <asp:DropDownList ID="DdlDM" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label8" runat="server" CssClass="EstiloLabel" Text="Tienda"></asp:Label>
                    <asp:DropDownList ID="DdlTienda" runat="server" AutoPostBack="True" CssClass="EstiloDropDownList">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label7" runat="server" CssClass="EstiloLabel" Text="Top"></asp:Label>
                    <asp:DropDownList ID="DdlTop" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True" style="margin-bottom: 0px">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="lblRegistro" runat="server" CssClass="EstiloLabel" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <div id="divLista">
                        <asp:GridView ID="gwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                            <Columns>
                                <asp:BoundField DataField="c1" HeaderText="Pregunta" />
                                <asp:BoundField DataField="c2" >
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
<%--                                <asp:BoundField DataField="c3" Visible="False" >
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c4" Visible="False">
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c5" Visible="False">
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c6" Visible="False" >
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c7" Visible="False"  >
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c8"  Visible="False">
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c9" Visible="False" >
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="c10"  Visible="False" >
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; " valign="middle"></td>                 
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

