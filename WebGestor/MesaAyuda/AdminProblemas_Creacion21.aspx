<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_Creacion21.aspx.vb" Inherits="AdminProblemas_Creacion21" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 18pt; vertical-align: middle; width: 550px; color: navy; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Administración de Problemas</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 150px; height: 19px;" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Problemas" Width="57px"></asp:Label></td>
                <td align="left" style="width: 200px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 200px; height: 19px;" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" colspan="3" valign="top" style="height: 20px">
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chk1" runat="server" Height="20px" Style="z-index: 120; left: 484px; top: 359px" Text="Abiertos No Vistos" Width="119px" Font-Names="Arial" Font-Size="8pt" Checked="True" /><asp:CheckBox ID="chk3" runat="server" Height="20px" Style="z-index: 120; left: 484px; top: 359px" Text="Asignados" Width="131px" Font-Names="Arial" Font-Size="8pt" /><asp:CheckBox ID="chk5" runat="server" Height="20px" Style="z-index: 120; left: 484px; top: 359px" Text="Asignados con Acción" Width="133px" Font-Names="Arial" Font-Size="8pt" /></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
                <td align="left" colspan="3" valign="top" style="height: 20px">
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chk2" runat="server" Height="20px" Style="z-index: 120; left: 484px; top: 359px" Text="Abiertos Vistos" Width="119px" Font-Names="Arial" Font-Size="8pt" /><asp:CheckBox ID="chk4" runat="server" Height="20px" Style="z-index: 120; left: 484px; top: 359px" Text="Asignados Vistos" Width="131px" Font-Names="Arial" Font-Size="8pt" /><asp:CheckBox ID="chk0" runat="server" Height="20px" Style="z-index: 120; left: 484px; top: 359px" Text="Cerrados" Width="133px" Font-Names="Arial" Font-Size="8pt" /></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Fecha Desde" Width="78px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 200px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Fecha Hasta" Width="78px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 200px; height: 22px; text-align: right"
                    valign="top">
                    <asp:Button ID="Listar" runat="server" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'"  TabIndex="1" Text="Listar Problemas" Width="178px" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:TextBox ID="txtFechaIni" runat="server" Width="120px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:ImageButton
                        ID="ImageButton1" runat="server" ImageUrl="~/Fotos/Calendario.bmp" Width="15px" /></td>
                <td align="left" style="vertical-align: middle; width: 200px; height: 22px" valign="top">
                    <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="120px"></asp:TextBox><asp:ImageButton
                        ID="ImageButton2" runat="server" ImageUrl="~/Fotos/Calendario.bmp" Width="15px" /></td>
                <td align="left" style="vertical-align: middle; width: 200px; height: 22px; text-align: right"
                    valign="top">
                    <asp:Button ID="Nuevo" runat="server" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'"
                        onmouseover="this.style.fontWeight='bolder'"  TabIndex="1" Text="Reportar Nuevo Problema" Width="178px" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="3" style="height: 19px; vertical-align: middle;">
                    <asp:Label ID="lblMensaje" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                         Width="540px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 150px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 200px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 200px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 19px" valign="top">
                    <div id="DIV1" runat="server" style="border-right: navy 1px outset; border-top: navy 1px outset;
                        overflow: auto; border-left: navy 1px outset; width: 550px; border-bottom: navy 1px outset;
                        height: 186px">
                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                            <contenttemplate>
<asp:GridView id="FlexProb" runat="server" Font-Size="8pt" Font-Names="Arial" PageSize="5" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField Text="Mostrar" ButtonType="Button" CommandName="Mostrar">
<ControlStyle BorderStyle="Outset" Width="60px" BorderWidth="1px" BorderColor="Gray" Font-Size="8pt" Font-Names="Arial" BackColor="LightGray" ForeColor="Gray"></ControlStyle>
</asp:ButtonField>
<asp:BoundField DataField="APROB_CODIGO" HeaderText="N&#186; Probl."></asp:BoundField>
<asp:BoundField DataField="APROB_FECHA_REPORTA" HeaderText="Fecha Reporta"></asp:BoundField>
<asp:BoundField DataField="APROB_HORA_REPORTA" HeaderText="Hora Reporta"></asp:BoundField>
<asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Tipo Problema"></asp:BoundField>
<asp:BoundField DataField="APROB_FECHA_VISTO" HeaderText="Fecha Visto"></asp:BoundField>
<asp:BoundField DataField="APROB_HORA_VISTO" HeaderText="Hora Visto"></asp:BoundField>
<asp:BoundField DataField="APROB_FECHA_ASIGNADO" HeaderText="Fecha Asig."></asp:BoundField>
<asp:BoundField DataField="APROB_HORA_ASIGNADO" HeaderText="Hora Asig."></asp:BoundField>
<asp:BoundField DataField="PERSON_ASIG1" HeaderText="Persona Asig."></asp:BoundField>
<asp:BoundField DataField="APROB_FECHA_ASIGVISTO" HeaderText="Fecha Asig. Visto"></asp:BoundField>
<asp:BoundField DataField="APROB_HORA_ASIGVISTO" HeaderText="Hora Asig. Visto"></asp:BoundField>
<asp:BoundField DataField="APROB_FECHA_SOLUCION" HeaderText="Fecha Sol."></asp:BoundField>
<asp:BoundField DataField="APROB_HORA_SOLUCION" HeaderText="Hora Sol."></asp:BoundField>
<asp:BoundField DataField="PESTADO" HeaderText="Estado"></asp:BoundField>
<asp:BoundField DataField="APROB_PRIORIDAD" HeaderText="Prior."></asp:BoundField>
<asp:BoundField DataField="NOM_PROB1" HeaderText="Concepto Problema"></asp:BoundField>
<asp:BoundField DataField="APROB_PROBLEMA_DESCRIPCION" HeaderText="Descripci&#243;n Problema"></asp:BoundField>
<asp:BoundField DataField="ECONFORME" Visible="False"></asp:BoundField>
<asp:BoundField DataField="NOM_PROB_ORIG" Visible="False"></asp:BoundField>
<asp:BoundField DataField="NOM_PROB_ORIG1" Visible="False"></asp:BoundField>
<asp:BoundField DataField="COLOR_ROJO" Visible="False"></asp:BoundField>
<asp:BoundField DataField="COLOR_VERDE" Visible="False"></asp:BoundField>
<asp:BoundField DataField="COLOR_AZUL" Visible="False"></asp:BoundField>
<asp:BoundField DataField="PERSONAL1" HeaderText="Persona Report&#243;"></asp:BoundField>
<asp:BoundField DataField="APROB_ASIGNADO_PERSONA" Visible="False"></asp:BoundField>
<asp:BoundField DataField="APROB_ESTADO" Visible="False"></asp:BoundField>
<asp:BoundField HeaderText="Comformidad x Persona que Reporta"></asp:BoundField>
<asp:BoundField HeaderText="Tipo de Problema Real"></asp:BoundField>
<asp:BoundField HeaderText="Concepto Problema Real"></asp:BoundField>
<asp:TemplateField HeaderText="Visto"><ItemTemplate>
<asp:LinkButton id="cmdVer" runat="server" Width="4px" Height="8px" CommandName="SiVisto">Sí</asp:LinkButton> <asp:Label id="lblVisto" runat="server" Width="2px" Height="8px" Font-Bold="True">Sí</asp:Label> 
</ItemTemplate>
</asp:TemplateField>
</Columns>

<PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView>
</contenttemplate>
                        </asp:UpdatePanel></div>
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="height: 19px;" valign="top" colspan="3">
                    <div id="fraFlex1" runat="server" style="font-size: 8pt; overflow: auto; width: 550px; font-family: Arial; position: static; height: 262px; border-top-width: 1px; border-left-width: 1px; border-left-color: seagreen; border-bottom-width: 1px; border-bottom-color: seagreen; border-top-color: seagreen; border-right-width: 1px; border-right-color: seagreen;">
                        <asp:DataGrid ID="Flex" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BorderColor="DimGray" BorderWidth="1px" CellPadding="2" Font-Names="Arial" Font-Size="8pt"
                            OnItemCommand="OpcionesFlex" OnPageIndexChanged="MyFlex_Page" PageSize="5">
                            <AlternatingItemStyle BackColor="WhiteSmoke" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                            <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle BackColor="Gainsboro" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:ButtonColumn CommandName="MostrarAcciones" Text="Mostrar Acciones"></asp:ButtonColumn>
                                <asp:BoundColumn DataField="cr" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="cv" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ca" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="C1" HeaderText="#">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C2" HeaderText="N&#186; Prob.">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C3" HeaderText="Fecha Reporte">
                                    <HeaderStyle Width="30px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C4" HeaderText="Hora Rep.">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C5" HeaderText="Tipo de Problema">
                                    <HeaderStyle Width="50px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C6" HeaderText="Fecha Visto">
                                    <HeaderStyle Width="30px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C7" HeaderText="Hora Visto">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C8" HeaderText="Fecha Asignado">
                                    <HeaderStyle Width="30px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C9" HeaderText="Hora Asig.">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C10" HeaderText="Persona Asignada"></asp:BoundColumn>
                                <asp:BoundColumn DataField="C11" HeaderText="F. Asig. Visto">
                                    <HeaderStyle Width="30px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C12" HeaderText="H. Asig. Visto">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C13" HeaderText="Fecha Sol.">
                                    <HeaderStyle Width="30px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C14" HeaderText="Hora Sol.">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C15" HeaderText="Estado"></asp:BoundColumn>
                                <asp:BoundColumn DataField="C16" HeaderText="Prior.">
                                    <HeaderStyle Width="20px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C17" HeaderText="Concepto de Problema">
                                    <HeaderStyle Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C18" HeaderText="Descripci&#243;n del Problema">
                                    <HeaderStyle Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C22" HeaderText="Persona Report&#243;">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C23" HeaderText="_person_asig" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="C19" HeaderText="Conformidad x persona que report&#243;">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C20" HeaderText="Tipo de Problema Real"></asp:BoundColumn>
                                <asp:BoundColumn DataField="C21" HeaderText="Concepto Problema Real">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="C24" HeaderText="_estado" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Visto">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="cmdVer" runat="server" CommandName="SiVisto" Height="8px" Width="4px">Sí</asp:LinkButton>
                                        <asp:Label ID="lblVisto" runat="server" Font-Bold="True" Height="8px" Width="2px">Sí</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" NextPageText="&amp;gt;&amp;gt;" PrevPageText="&amp;lt;&amp;lt;" />
                        </asp:DataGrid></div>
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 24px;" valign="top">
                </td>
                <td align="left" valign="top" colspan="3" style="height: 24px">
                    <asp:Label ID="lblMensaje2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Height="24px" Width="550px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 24px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 183px;" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 183px" valign="top">
                    <div id="fraFlex2" runat="server" style="overflow: auto; width: 550px;
                        position: static; height: 162px; border-top-width: 1px; border-left-width: 1px; border-left-color: seagreen; border-bottom-width: 1px; border-bottom-color: seagreen; border-top-color: seagreen; border-right-width: 1px; border-right-color: seagreen;">
                        <asp:DataGrid ID="Flex2" runat="server" AutoGenerateColumns="False" BorderColor="DimGray"
                            BorderWidth="1px" CellPadding="2" Font-Names="Arial" Font-Size="8pt" Height="101px"
                            Width="535px">
                            <AlternatingItemStyle BackColor="WhiteSmoke" />
                            <HeaderStyle BackColor="Gainsboro" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>
                                <asp:BoundColumn DataField="c1" HeaderText="N&#186; Acc.">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="c2" HeaderText="Fecha Acci&#243;n">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="c3" HeaderText="Hora Acci&#243;n">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="c4" HeaderText="Acci&#243;n Tomada"></asp:BoundColumn>
                                <asp:BoundColumn DataField="c5" HeaderText="Descripci&#243;n de la Acci&#243;n"></asp:BoundColumn>
                                <asp:BoundColumn DataField="c6" HeaderText="Observaci&#243;n"></asp:BoundColumn>
                                <asp:BoundColumn DataField="c7" HeaderText="Descripci&#243;n de Obs."></asp:BoundColumn>
                                <asp:BoundColumn DataField="c8" HeaderText="Personal de la Acci&#243;n"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid></div>
                </td>
                <td align="left" style="width: 25px; height: 183px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" style="height: 19px;" valign="top" colspan="3">
                    <div style="border-right: seagreen 1px outset; border-top: seagreen 1px outset; border-left: seagreen 1px outset;
                        width: 550px; border-bottom: seagreen 1px outset; height: 100px">
                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                            <contenttemplate>
<asp:GridView id="GridView1" runat="server"></asp:GridView>
</contenttemplate>
                        </asp:UpdatePanel></div>
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 150px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 200px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 200px; height: 19px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="width: 150px" valign="top">
                    </td>
                <td align="left" style="width: 200px" valign="top">
                </td>
                <td align="left" style="width: 200px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
        </table>
        <cc1:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImageButton1"
            targetcontrolid="txtFechaIni"></cc1:calendarextender>
        <cc1:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImageButton2"
            targetcontrolid="txtFechaFin"></cc1:calendarextender>
        <br />
        <asp:Label ID="lblUsuarioCodigo" runat="server" Height="16px" Style="z-index: 104;
            left: 925px; position: absolute; top: 242px" Visible="False" Width="88px"></asp:Label>
        <asp:Label ID="lblSeguridad" runat="server" Font-Names="Arial" Font-Size="8pt" Height="8px"
            Style="z-index: 101; left: 927px; position: absolute; top: 272px" Visible="False"
            Width="16px"></asp:Label>
    </div>
</asp:Content>

