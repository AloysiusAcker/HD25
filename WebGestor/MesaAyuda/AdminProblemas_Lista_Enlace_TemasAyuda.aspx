<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_Lista_Enlace_TemasAyuda.aspx.vb" Inherits="AdminProblemas_Lista_Enlace_TemasAyuda" title="Mesa de Ayuda - Lista de Archivos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script lang="javascript" type="text/javascript">
</script>

    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 331px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Archivos</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px" valign="top">
                    <img src="../Fotos/linea.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 12px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 12px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 12px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<cc1:TabContainer id="Ficha" runat="server" Width="550px" Height="500px" AutoPostBack="True" ActiveTabIndex="0"><cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1"><HeaderTemplate>
Temas de Ayuda
</HeaderTemplate>
<ContentTemplate>
<DIV style="TEXT-ALIGN: left"><TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left><asp:Button id="cmdListarTA" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="cmdListarTA_Click" runat="server" CssClass="EstiloBoton" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" EnableTheming="True" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Listar"></asp:Button>
 </TD></TR><TR><TD vAlign=top align=left><asp:UpdatePanel id="UpdatePanel1" runat="server"><ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 530px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 420px"><asp:GridView id="FlexTA" runat="server" Width="930px" Font-Size="8pt" Font-Names="Arial" AllowPaging="True" AutoGenerateColumns="False" PageSize="15"><Columns>
<asp:BoundField DataField="CLASSE" HeaderText="Clasificaci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Nombre del Documento">
    <ItemTemplate>
        <div id="Doc" runat="server" style="width: 150px; height: 22px">
        </div>                                    
    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="TEMA_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Persona" HeaderText="Nombre de Creaci&#243;n ">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_INGRESO" HeaderText="F. Ingreso">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="APROB_CODIGO" HeaderText="Cod. Prob.">
<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_CODIGO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_TIPO_DOC">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_USUARIO">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_CLASIFICACION">
<ItemStyle ForeColor="White" Width="0px"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="cmdListarTA" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="FlexTA" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel>
 </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblErrorTA" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label>
 </TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2"><HeaderTemplate>
Enlaces
</HeaderTemplate>
<ContentTemplate>
<TABLE style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 150px; HEIGHT: 15px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left><asp:Button id="cmdListar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="cmdListar_Click" runat="server" CssClass="EstiloBoton" Width="72px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" EnableTheming="True" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button> </TD><TD style="WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 100px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 25px; TEXT-ALIGN: right" vAlign=top align=left>&nbsp;</TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left>&nbsp;</TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 160px" vAlign=top align=left colSpan=5><DIV style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 520px; BORDER-BOTTOM: gray 1px inset; HEIGHT: 420px" id="DIV1" runat="server"><asp:GridView id="Flex" runat="server" Width="550px" Font-Size="8pt" Font-Names="Arial" PageSize="15" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:BoundField DataField="ENLACE_CODIGO" HeaderText="C&#243;digo">
<ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ENLACE_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="URL">
<ItemStyle Width="250px"></ItemStyle>
<ItemTemplate>
                                                                                    <div id="Abrir" runat="server" style="display: inline; font-size: 8pt; width: 240px;
                                                                                        color: gray; font-style: italic; font-family: Arial; height: 20px">
                                                                                    </div>
                                                                                
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField>
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5><asp:Label id="lblError" runat="server" ForeColor="Red" Font-Size="8pt" Font-Names="Arial"></asp:Label> </TD></TR><TR><TD style="VERTICAL-ALIGN: middle; HEIGHT: 23px" vAlign=top align=left colSpan=5><DIV style="TEXT-ALIGN: left">&nbsp;</DIV></TD></TR></TBODY></TABLE>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 12px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

