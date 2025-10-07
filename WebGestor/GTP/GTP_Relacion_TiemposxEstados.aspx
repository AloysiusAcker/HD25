<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="GTP_Relacion_TiemposxEstados.aspx.vb" Inherits="GTP_GTP_Relacion_TiemposxEstados" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

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
                        <td align="left" style="width: 80px; height: 22px;" valign="middle">
                            <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Estado"></asp:Label>
                        </td>
                        <td align="left" style="height: 22px;" valign="middle" colspan="3">
                                     
                                    <asp:DropDownList ID="DdlEstado" runat="server" CssClass="EstiloDropDownList" Height="16px" Width="196px" AutoPostBack="True">
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
                        &nbsp;<asp:TextBox ID="txtFechaFin" runat="server" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Height="16px" Width="110px"></asp:TextBox>
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
                        <td align="left" style="height: 22px;" valign="middle" colspan="2">  
                            <asp:Button ID="BtnExportar" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Text="Exportar" Width="77px" />
                        </td>
                        <td align="left" style="width: 80px; height: 22px;" valign="top"></td>
                        <td align="left" style="width: 120px; height: 22px;" valign="top"></td>
                        <td align="left" style="width: 150px; height: 22px;" valign="top"></td>
                        <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
                        <td align="left" style="width: 100px; height: 8px;" valign="top"></td>
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


                                    <asp:GridView ID="GwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" >
                                        <Columns>
                                            <asp:BoundField DataField="c0" HeaderText="Nro. Registro" ></asp:BoundField>
                                            <asp:BoundField DataField="c1" HeaderText="Nro Ticket" ></asp:BoundField>
                                            <asp:BoundField DataField="c2" HeaderText="CIF" ></asp:BoundField>
                                            <asp:BoundField DataField="c3" HeaderText="Cliente" ></asp:BoundField>
                                            <asp:BoundField DataField="c4" HeaderText="GPS" ></asp:BoundField>
                                            <asp:BoundField DataField="c5" HeaderText="Grupo" />
                                            <asp:BoundField DataField="c6" HeaderText="Proceso" ></asp:BoundField>
                                            <asp:BoundField DataField="c7" HeaderText="Tipo de Peticion" ></asp:BoundField>
                                            <asp:BoundField DataField="c8" HeaderText="Asesor" ></asp:BoundField>
                                            <asp:BoundField DataField="c9" HeaderText="Estado" ></asp:BoundField>
                                            <asp:BoundField DataField="c10" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c11" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c12" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c13" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c14" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c15" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c16" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c17" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c18" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c19" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c20" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c21" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c22" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c23" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c24" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c25" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c26" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c27" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c28" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c29" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c30" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c31" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c32" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c33" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c34" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c35" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c36" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c37" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c38" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c39" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c40" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c41" HeaderText="" Visible="False" ></asp:BoundField>

                                            <asp:BoundField DataField="c42" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c43" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c44" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c45" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c46" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c47" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c48" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c49" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c50" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c51" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c52" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c53" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c54" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c55" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c56" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c57" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c58" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c59" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c60" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c61" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c62" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c63" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c64" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c65" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c66" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c67" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c68" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c69" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c70" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c71" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c72" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c73" HeaderText="" Visible="False" ></asp:BoundField>
                                    
                                            <asp:BoundField DataField="c74" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c75" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c76" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c77" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c78" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c79" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c80" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c81" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c82" HeaderText="" Visible="False"  ></asp:BoundField>
                                            <asp:BoundField DataField="c83" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c84" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c85" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c86" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c87" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c88" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c89" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c90" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c91" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c92" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c93" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c94" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c95" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c96" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c97" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c98" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c99" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c100" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c101" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c102" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c103" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c104" HeaderText="" Visible="False" ></asp:BoundField>
                                            <asp:BoundField DataField="c105" HeaderText="" Visible="False" ></asp:BoundField>

                                            <asp:BoundField DataField="c106" HeaderText="" Visible="False" ></asp:BoundField>
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
            <asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="DdlProceso" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

