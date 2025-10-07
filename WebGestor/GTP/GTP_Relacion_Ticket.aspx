<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="GTP_Relacion_Ticket.aspx.vb" Inherits="GTP_GTP_Relacion_Ticket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>--%>
<asp:UpdatePanel ID="UpLista" runat="server">
<ContentTemplate >
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 1000px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="9" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; vertical-align: middle;color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; 
                        height: 1px; text-align: center">
                        Relación de Tickets</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="11" style="background-image: url(/Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 150px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 8px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="height: 19px;" valign="middle" colspan="9">
                    <asp:Label ID="LblError" runat="server" CssClass="EstiloLabel" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>            
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:CheckBox ID="chkCliente" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Text="Cliente" />
                </td>
                <td align="left" style="vertical-align: middle; width: 120px; height: 22px" valign="top">
                    <asp:TextBox id="txtRuc" runat="server" Width="110px" Height="16px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" AutoPostBack="True" MaxLength="11"></asp:TextBox> 
                </td>
                <td align="left" style="height: 22px;" valign="top" colspan="7">
                    <asp:Button ID="btnDatos" runat="server"
                    BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                    Height="20px" Text="..." Width="20px" />&nbsp;<asp:TextBox ID="txtRazon" runat="server" BorderColor="Black" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" ReadOnly="True" Width="382px"></asp:TextBox>
                </td>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
            </tr>
             <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 22px;" valign="middle">
                    <asp:Label ID="Label6" runat="server" CssClass="EstiloLabel" Text="Proceso"></asp:Label>
                </td>
                <td align="left" style="height: 22px;" valign="middle" colspan="2">
                    <asp:DropDownList ID="DdlProceso" runat="server" CssClass="EstiloDropDownList" Width="196px" Height="16px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 120px; height: 22px;" valign="middle">
                    &nbsp;</td>
                <td align="left" style="height: 22px;" valign="middle" colspan="2">
                    <asp:Label ID="lblCodCliente" runat="server" CssClass="EstiloLabel" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodEstado" runat="server" CssClass="EstiloLabel" Visible="False"></asp:Label>
                </td>
                <td align="left" style="width: 150px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="middle">
                    <asp:Label ID="Label4" runat="server" CssClass="EstiloLabel" Text="Tipo de Petición"></asp:Label>
                </td>
                <td align="left" style="height: 8px;" valign="top" colspan="2">
                    <asp:DropDownList ID="DdlComponente" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" Width="196px" Height="16px"></asp:DropDownList>
                </td>
                <td align="left" style="width: 120px; height: 8px;" valign="middle">
                    <asp:CheckBox ID="chkAnulados" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Anulados" />
                </td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 150px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 8px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 22px;" valign="middle">
                    <asp:Label ID="Label5" runat="server" CssClass="EstiloLabel" Text="Elemento"></asp:Label>
                </td>
                <td align="left" style="height: 26px;" valign="middle" colspan="2">
                    <asp:DropDownList ID="DdlElemento" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" Width="196px" Height="16px"></asp:DropDownList></td>
                <td align="left" style="height: 22px;" valign="middle" colspan="2">
                    <asp:DropDownList ID="DdlElemento2" runat="server" CssClass="EstiloDropDownList" Height="16px" Width="196px">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 120px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 150px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 22px;" valign="middle">
                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Estado"></asp:Label>
                </td>
                <td align="left" style="height: 22px;" valign="middle" colspan="3">
                    <asp:DropDownList ID="DdlEstado" runat="server" CssClass="EstiloDropDownList" Height="16px" Width="196px">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 80px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 150px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 22px;" valign="middle">
                    <asp:Label ID="Label2" runat="server" CssClass="EstiloLabel" Text="Fecha"></asp:Label>
                </td>
                <td align="left" style="height: 22px;" valign="middle" colspan="3">
                    <asp:TextBox ID="txtFechaIni" runat="server" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" Width="110px"></asp:TextBox>
                    <asp:TextBox ID="txtFechaFin" runat="server" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" Width="110px"></asp:TextBox>
                 <%--   <cc1:CalendarExtender ID="Cal1" runat="server" PopupButtonID="txtFechaIni" Format="dd/MM/yyyy" TargetControlID="txtFechaIni"></cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="Cal2" runat="server" PopupButtonID="txtFechaFin" Format="dd/MM/yyyy" TargetControlID="txtFechaFin"></cc1:CalendarExtender>--%>
                </td>
                <td align="left" style="width: 80px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 120px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 150px; height: 22px;" valign="middle"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="middle"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="middle"></td>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 22px;" valign="top">
                    <asp:Button ID="BtnListar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Height="20px" Text="Listar" Width="77px" />
                </td>
                <td align="left" style="width: 120px; height: 22px;" valign="middle">
                    <asp:Button ID="BtnLimpiar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Limpiar" Width="77px" />
                </td>
                <td align="left" style="width: 80px; height: 22px;" valign="middle">  
                    <asp:Button ID="BtnExportar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Exportar" Width="77px" />
                </td>
                <td align="left" style="width: 120px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 150px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 22px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px;" valign="top"></td>
                <td align="left" style="height: 19px;" valign="middle" colspan="9">
                    <asp:Label ID="lblRegistro" runat="server" CssClass="EstiloLabel" ForeColor="Maroon"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 22px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" style="height: 19px;" valign="top" colspan="9">
                    <div id="divGrilla" runat="server" style="vertical-align: top; width: 950px; overflow: scroll;">
                            <asp:GridView ID="GwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" PageSize="2000" >
                                <Columns>
                                    <asp:ButtonField CommandName="Accion" Text="Acciones">
                                    <ControlStyle CssClass="EstiloBoton" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="c1" HeaderText="Nro Ticket" ></asp:BoundField>
                                    <asp:BoundField DataField="c2" HeaderText="" ></asp:BoundField>
                                    <asp:BoundField DataField="c3" HeaderText="Fecha Reporta" ></asp:BoundField>
                                    <asp:BoundField DataField="c4" HeaderText="Hora Reporta" ></asp:BoundField>
                                    <asp:BoundField DataField="c5" HeaderText="Nombre del Cliente" />
                                    <asp:BoundField DataField="c6" HeaderText="Asig. cliente" ></asp:BoundField>
                                    <asp:BoundField DataField="c7" HeaderText="Grupo" ></asp:BoundField>
                                    <asp:BoundField DataField="c8" HeaderText="Estado" ></asp:BoundField>
                                    <asp:BoundField DataField="c9" HeaderText="Contacto" ></asp:BoundField>
                                    <asp:BoundField DataField="c10" HeaderText="Proceso" ></asp:BoundField>
                                    <asp:BoundField DataField="c11" HeaderText="Canal" ></asp:BoundField>
                                    <asp:BoundField DataField="c12" HeaderText="Tipo Peticion" ></asp:BoundField>
                                    <asp:BoundField DataField="c13" HeaderText="Elemento" ></asp:BoundField>
                                    <asp:BoundField DataField="c14" HeaderText="Estado" ></asp:BoundField>
                                    <asp:BoundField DataField="c15" HeaderText="Motivo" ></asp:BoundField>
                                    <asp:BoundField DataField="c16" HeaderText="Descripcion" ></asp:BoundField>
                                    <asp:BoundField DataField="c17" HeaderText="Solucion" ></asp:BoundField>
                                    <asp:BoundField DataField="c18" HeaderText="Fecha visto" ></asp:BoundField>
                                    <asp:BoundField DataField="c19" HeaderText="Hora Visto" ></asp:BoundField>
                                    <asp:BoundField DataField="c20" HeaderText="Fecha Asignado" ></asp:BoundField>
                                    <asp:BoundField DataField="c21" HeaderText="Hora Asignado" ></asp:BoundField>
                                    <asp:BoundField DataField="c22" HeaderText="Fecha Asig. Visto" ></asp:BoundField>
                                    <asp:BoundField DataField="c23" HeaderText="Hora Asig. Visto" ></asp:BoundField>
                                    <asp:BoundField DataField="c24" HeaderText="Fecha Solucion" ></asp:BoundField>
                                    <asp:BoundField DataField="c25" HeaderText="Hora Solucion" ></asp:BoundField>
                                    <asp:BoundField DataField="c26" HeaderText="TICKET_ESTADO" ></asp:BoundField>
                                    <asp:BoundField DataField="c27" HeaderText="TICKET_SYS_EST" ></asp:BoundField>
                                    <asp:BoundField DataField="c28" HeaderText="TICKET_TIPO" ></asp:BoundField>
                                    <asp:BoundField DataField="c29" HeaderText="Fecha Estado" ></asp:BoundField>
                                    <asp:BoundField DataField="c30" HeaderText="Hora Estado" ></asp:BoundField>
                                    <asp:BoundField DataField="c31" HeaderText="" ></asp:BoundField>
                                    <asp:BoundField DataField="c32" HeaderText="TICKET_ESTADO_FECHA" ></asp:BoundField>
                                    <asp:BoundField DataField="c33" HeaderText="" ></asp:BoundField>
                                    <asp:BoundField DataField="c34" HeaderText="Duracion Ticket" ></asp:BoundField>
                                    <asp:BoundField DataField="c35" HeaderText="" ></asp:BoundField>
                                    <asp:BoundField DataField="c36" HeaderText="" ></asp:BoundField>
                                    <asp:BoundField DataField="c37" HeaderText="" ></asp:BoundField>
                                    <asp:BoundField DataField="c38" HeaderText="PERSON_ASIG2" ></asp:BoundField>
                                    <asp:BoundField DataField="c39" HeaderText="TICKET_ASIGNADO_PERSONA" ></asp:BoundField>
                                    <asp:BoundField DataField="c40" HeaderText="Observacion" ></asp:BoundField>
                                    <asp:BoundField DataField="c41" HeaderText="Ruc Cliente" ></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                    </div>

                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 80px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 120px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 80px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 120px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 80px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 120px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 150px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 100px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 100px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" style="width: 25px; height: 8px;" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 8px;" valign="top">&nbsp;</td>
                <td align="left" colspan="9" style="height: 8px;" valign="top">
                    <div>
                        <asp:GridView ID="GvAcciones" runat="server" Font-Names="Arial" Font-Size="8pt" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="c1" HeaderText="Nro. Ticket" />
                                <asp:BoundField DataField="c2" HeaderText="Secuencia" />
                                <asp:BoundField DataField="c3" HeaderText="Acción" />
                                <asp:BoundField DataField="c4" HeaderText="Fecha" />
                                <asp:BoundField DataField="c5" HeaderText="Hora" />
                                <asp:BoundField DataField="c6" HeaderText="Usuario" />
                                <asp:BoundField DataField="c7" HeaderText="Referencia" />
                                <asp:BoundField DataField="c8" HeaderText="Cod. Referencia" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 8px;" valign="top">&nbsp;</td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 80px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 120px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 150px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 8px;" valign="top"></td>
            </tr>
        </table>         
    </div>
      <div style="text-align: left">
        <asp:Panel ID="Panel2" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 350px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;" cancelcontrolid="btnCerrarTI">
                <tr>
                    <td align="left" style="width: 25px; background-color: darkgray; height: 25px;" valign="top">
                    </td>
                    <td align="left" colspan="3" style="background-color: darkgray; vertical-align: middle; height: 25px; text-align: center; " valign="top">
                        <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Relación de Clientes"></asp:Label></td>
                    <td align="left" style="width: 25px; background-color: darkgray; height: 25px;" valign="top">
                    </td>
                </tr>
                <tr>                    
                    <td align="left" style="width: 25px; background-color: darkgray; height: 22px;" valign="top"></td>
                    <td align="left" style="width: 100px; background-color: darkgray; height: 22px;" valign="middle">
                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Text="RUC"></asp:Label></td>
                    <td align="left" style="width: 400px; background-color: darkgray; height: 22px;" valign="middle" colspan="2">
                        <asp:TextBox id="txtBusRuc" runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="17px"></asp:TextBox> 
                    </td>
                    <td align="left" style="width: 25px; background-color: darkgray; height: 22px;" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                    <td align="left" style="height: 22px; background-color: darkgray; width: 100px;" valign="middle">
                        <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            Text="Razón Social"></asp:Label></td>
                    <td align="left" style="height: 22px; background-color: darkgray; width: 400px;" valign="top" colspan="2">
                        <asp:TextBox id="txtBusRazon" runat="server" Width="350px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="17px"></asp:TextBox> 
                    </td>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; background-color: darkgray;
                        text-align: left; " valign="top">
                        <asp:Button ID="btnCerrarTI" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Cerrar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/>
                        <asp:Button ID="btnListarTI" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Listar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"/></td>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                    <td align="left" colspan="3" style="background-color: darkgray; " valign="top">     

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate >                                
                                <div style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 500px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 160px" id="DIV2" runat="server">
                                    <asp:GridView id="FlexTI" runat="server" Width="490px" Height="1px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="DarkGray" AutoGenerateColumns="False" PageSize="5"><Columns>
                                <asp:ButtonField CommandName="AceptarTI" Text="Aceptar" ButtonType="Button">
                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="TBTICKET_CLIENTE_CIF" HeaderText="RUC">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE" HeaderText="Razón Social">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="300px"></ItemStyle>
                                </asp:BoundField>
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TBTICKET_CLIENTE_CODIGO">
                                        <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TBTICKET_CLIENTE_ESTADO">
                                        <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                    </asp:BoundField>
                                </Columns>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:GridView> </div>
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>

                    </td>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                    <td align="left" colspan="3" style="height: 25px; background-color: darkgray; width: 500px;" valign="top"></td>
                    <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                </tr>
            </table>
            
            <cc1:ModalPopupExtender 
            id="ModalPopupExtender2" 
                            runat="server" 
                            TargetControlID="btnDatos"
                            CancelControlID ="btnCerrarTI"
                            PopupControlID ="Panel2" 
                            CacheDynamicResults="True" 
                            BackgroundCssClass="modalBackground" X="200" Y="200" >
            </cc1:ModalPopupExtender> 
        </asp:Panel>
       </div>            
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="DdlProceso" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="DdlComponente" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="DdlElemento" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click" />
    </Triggers>

</asp:UpdatePanel> 
    
        <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
            <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div style="position: relative; top: 30%; text-align: center;">
                            <img src="/Fotos/5.gif" /></div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
		    BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>

