<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos_Generar.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos_Generar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>--%>
        <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline; font-weight: bold; font-size: 14pt; vertical-align: middle; width: 650px; color: gray; font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute; height: 1px; text-align: center">
                        Evaluación de Procesos
                    </div>
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
                    <asp:Button ID="BtnProgramar" runat="server" CssClass="EstiloBoton" Text="Programar Evaluación" />
                    <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton" Text="Listar" Height="19px" />
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width:750px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width:750px" valign="middle">
                    <asp:Label ID="lblEtiqueta" runat="server" Text="" Font-Names="arial" Font-Size="8"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 750px" valign="middle">
                    <div id="divRegistro" runat="server" visible="false">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="middle">
                                    <asp:Label ID="lblEtqProceso" runat="server" CssClass="EstiloLabel" Text="Proceso"></asp:Label>
                                </td>
                                <td align="left" style="width: 80%; height: 20px;" valign="middle">
                                    <asp:CheckBoxList ID="chkProceso" runat="server" Font-Names="Arial" Font-Size="8pt" RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="middle">
                                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Responsable"></asp:Label>
                                </td>
                                <td align="left" style="width: 80%; height: 20px;" valign="middle">
                                    <asp:DropDownList ID="DdlResponsable" runat="server" CssClass="EstiloDropDownList" Width="350px" AutoPostBack="True" CausesValidation="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="middle">
                                    <asp:Label ID="lblOficina" runat="server" Text="Oficina" CssClass="EstiloLabel"></asp:Label>
                                </td>
                                <td align="left" style="width: 80%; height: 20px;" valign="middle">
                                    <%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>--%>
                                            <asp:DropDownList ID="ddlOficina" runat="server" AutoPostBack="true" CssClass="EstiloTextbox" Width="350px" CausesValidation="True">
                                            </asp:DropDownList>
                                    <%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="middle">
                                    <asp:CheckBox ID="chkTipo" runat="server" CssClass="EstiloLabel" Text="Tipo Evaluación" AutoPostBack="True" />
                                </td>
                                <td align="left" style="width: 80%; height: 20px;" valign="middle">
                                    <asp:DropDownList ID="DdlTipoEval" runat="server" CssClass="EstiloDropDownList" Enabled="False" Width="350px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="middle">
                                    <asp:Label ID="lblFecha" runat="server" CssClass="EstiloLabel" Text="Fecha"></asp:Label>
                                </td>
                                <td align="left" style="width: 80%; height: 20px;" valign="middle">
                                    <asp:TextBox ID="txtFecha" runat="server" CssClass="EstiloTextbox" Height="16px" Width="92px"></asp:TextBox>
                                    <asp:ImageButton runat="server" ImageUrl="~/Fotos/Calendario.bmp" Height="15px" Width="15px" ID="btnI1" FirstDayOfWeek="Wednesday"></asp:ImageButton>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFecha" Format="dd/MM/yyyy" PopupButtonID="btnI1"></cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="top"></td>
                                <td align="left" style="width: 80%; height: 20px;" valign="middle">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="EstiloBoton" Text="Guardar" Width="87px" />
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="EstiloBoton" Text="Cancelar" Width="87px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 20%; height: 20px;" valign="top">&nbsp;</td>
                                <td align="left" style="width: 80%; " valign="middle">
                                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="EstiloDropDownList" Width="350px" AutoPostBack="True" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label2" runat="server" CssClass="EstiloLabel" Text="Responsable"></asp:Label>
                    <asp:DropDownList ID="DdlBusResponsable" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True" >
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <div id="divLista">
                        <asp:GridView ID="gwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                            <Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" />
                                <asp:BoundField DataField="CodProceso" HeaderText="Cod. Proceso">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NombreProceso" HeaderText="Nombre Proceso">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FrecuenciaProceso" HeaderText="Frecuencia Proceso">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EVALUACION_CODIGO" HeaderText="Nro. Eval.">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Oficina" HeaderText="Oficina" />
                                <asp:BoundField DataField="fecha_eval" HeaderText="Fecha Eval." />
                                <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
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
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="lblCodProceso" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodEval" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                                                    
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            </table>
        
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>--%>
</div>
</asp:Content>