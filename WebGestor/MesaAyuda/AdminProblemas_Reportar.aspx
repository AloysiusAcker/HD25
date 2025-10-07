<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="AdminProblemas_Reportar.aspx.vb" Inherits="AdminProblemas_Reportar" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="7" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; vertical-align: middle; width: 550px; color: navy; font-style: italic;
                        font-family: 'Bell MT', Broadway, Arial, Serif; height: 1px; text-align: center">
                        Administración de Problemas - Reportando un Problema&nbsp;
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="9" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 90px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 50px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px;">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        position: static; top: 353px; height: 19px; text-align: right">
                        Fecha que se reporta</div>
                </td>
                <td align="left" valign="top" style="width: 90px;">
                    <asp:TextBox ID="txtRep_Fecha" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="16px" ReadOnly="True" Style="z-index: 119; left: 373px; top: 355px" Width="77px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px;">
                </td>
                <td align="left" valign="top" style="width: 70px;">
                </td>
                <td align="left" valign="top" style="width: 70px;">
                </td>
                <td align="left" valign="top" style="width: 50px;">
                </td>
                <td align="left" valign="top" style="width: 70px;">
                    <asp:HyperLink ID="Hyperlink2" runat="server" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Gray" Height="20px" Style="z-index: 139; left: 755px; top: 362px" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        Width="61px" Font-Underline="False">Regresar</asp:HyperLink></td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Hora que se reporta</div>
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <asp:TextBox ID="txtRep_Hora" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        ReadOnly="True" Style="z-index: 119; left: 373px; top: 355px" Width="77px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 50px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Nº de Problema</div>
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <asp:TextBox ID="txtRep_Codigo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="16px" ReadOnly="True" Style="z-index: 119; left: 373px; top: 355px" Width="77px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 50px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Prioridad</div>
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <asp:DropDownList ID="cboRep_Prioridad" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="83px">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 50px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Tipo de Problema</div>
                </td>
                <td align="left" valign="top" colspan="6">
                    <asp:DropDownList ID="cboRep_Problema" runat="server" AutoPostBack="True" Font-Bold="True"
                        Font-Names="Arial" Font-Size="8pt" Width="418px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px" valign="top">
                    <div id="lblMsj1" runat="server" style="font-size: 8pt; z-index: 117; left: 745px;
                        width: 18px; color: red; font-family: Arial; top: 507px; height: 17px; text-align: left">
                        *</div>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Concepto de Problema</div>
                </td>
                <td align="left" valign="top" colspan="6">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                    <asp:DropDownList ID="cboRep_P2" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="418px">
                    </asp:DropDownList>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboRep_Problema" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                    <div id="lblMsj2" runat="server" style="font-size: 8pt; z-index: 117; left: 745px;
                        width: 17px; color: red; font-family: Arial; top: 507px; height: 17px; text-align: left">
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Clase de Problema</div>
                </td>
                <td align="left" valign="top" colspan="6">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
                    <asp:DropDownList ID="cboRep_P3" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Width="418px">
                    </asp:DropDownList>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboRep_P2" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px" valign="top">
                    <div id="lblMsj3" runat="server" style="font-size: 8pt; z-index: 117; left: 745px;
                        width: 16px; color: red; font-family: Arial; top: 507px; height: 17px; text-align: left">
                        *</div>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 120px; font-family: Arial;
                        top: 353px; height: 19px; text-align: right">
                        Descripción del Problema</div>
                </td>
                <td align="left" colspan="6" valign="top">
                    <asp:TextBox ID="txtRep_Descrip" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="69px" Style="z-index: 119; left: 373px; top: 355px" TextMode="MultiLine"
                        Width="411px"></asp:TextBox></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px" rowspan="5">
                    <div style="font-size: 8pt; z-index: 124; left: 254px; width: 119px; font-family: Arial;
                        top: 353px; height: 1px; text-align: right">
                        Datos de la Persona que Reporta el Problema</div>
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <div style="font-size: 8pt; z-index: 131; left: 378px; width: 80px; font-family: Arial;
                        top: 670px; height: 17px; text-align: right">
                        App. y Nombres</div>
                </td>
                <td align="left" valign="top" colspan="4">
                    <asp:TextBox ID="lblRep_Nombres" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="16px" Style="z-index: 119; left: 373px; top: 355px" Width="253px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px">
                    <asp:TextBox ID="lblRep_Personal" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="16px" ReadOnly="True" Style="z-index: 103; left: 671px; top: 701px" Width="60px"></asp:TextBox></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <div style="font-size: 8pt; z-index: 131; left: 378px; width: 84px; font-family: Arial;
                        top: 670px; height: 17px; text-align: right">
                        Cod. Interno</div>
                </td>
                <td align="left" valign="top" colspan="2">
                    <asp:TextBox ID="txtPeCodInterno" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="16px" Style="z-index: 119; left: 373px; top: 355px" Width="129px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" valign="top" style="width: 50px">
                </td>
                <td align="left" valign="top" style="width: 70px">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <div style="font-size: 8pt; z-index: 131; left: 378px; width: 84px; font-family: Arial;
                        top: 670px; height: 17px; text-align: right">
                        Cargo Principa</div>
                </td>
                <td align="left" valign="top" colspan="5">
                    <asp:TextBox ID="txtPeCargo" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 119; left: 373px; top: 355px" Width="324px"></asp:TextBox></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" style="width: 90px">
                    <div style="font-size: 8pt; z-index: 131; left: 378px; width: 84px; font-family: Arial;
                        top: 670px; height: 17px; text-align: right">
                        Área/s</div>
                </td>
                <td align="left" valign="top" colspan="5">
                    <asp:TextBox ID="txtPeArea" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 119; left: 373px; top: 355px" Width="323px"></asp:TextBox></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 36px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 90px; height: 36px">
                    <div style="font-size: 8pt; z-index: 131; left: 378px; width: 84px; font-family: Arial;
                        top: 670px; height: 17px; text-align: right">
                        Telfs/Anexos</div>
                </td>
                <td align="left" valign="top" style="width: 70px; height: 36px">
                    <asp:TextBox ID="txtPeTelf1" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 119; left: 373px; top: 355px" Width="61px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px; height: 36px">
                    <asp:TextBox ID="txtPeAnex1" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 103; left: 671px; top: 701px" Width="60px"></asp:TextBox></td>
                <td align="left" colspan="2" style="height: 36px" valign="top">
                    <asp:TextBox ID="txtPeTelf2" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 103; left: 671px; top: 701px" Width="109px"></asp:TextBox></td>
                <td align="left" valign="top" style="width: 70px; height: 36px">
                    <asp:TextBox ID="txtPeAnex2" runat="server" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 103; left: 671px; top: 701px" Width="64px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 36px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 90px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 50px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" style="vertical-align: middle; text-align: center; height: 19px;" colspan="7">
                    <asp:Button ID="Enviar" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Gray" 
                        onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" TabIndex="1" Text="Enviar Problema"
                        Width="170px" />&nbsp;<asp:Button ID="Nuevo" runat="server" BackColor="LightGray"
                            BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" 
                            onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" TabIndex="1" Text="Reportar Nuevo Problema"
                            Width="170px" /></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
                <td align="left" valign="top" style="width: 130px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 90px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 50px; height: 19px">
                </td>
                <td align="left" valign="top" style="width: 70px; height: 19px">
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" valign="top" colspan="7">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="16px" Style="z-index: 115; left: 5px; top: 401px" Width="497px"></asp:Label></td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
                <td align="left" valign="top" style="height: 16px" colspan="7">
                    <asp:Label ID="lblMensaje2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="DarkGray" Height="15px" Style="z-index: 112; left: 5px; top: 420px"
                        Width="497px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 16px;" valign="top">
                </td>
            </tr>
        </table>
        <asp:Label ID="lblUsuarioCodigo" runat="server" Height="16px" Style="z-index: 103;
            left: 812px; position: absolute; top: 17px" Visible="False" Width="64px"></asp:Label>
        <asp:Label ID="lblTipoProb" runat="server" Height="3px" Style="z-index: 104; left: 813px;
            position: absolute; top: 36px" Visible="False" Width="28px"></asp:Label>
        <asp:Label ID="lblUltProb" runat="server" Height="10px" Style="z-index: 108; left: 816px;
            position: absolute; top: 59px" Visible="False" Width="27px"></asp:Label>
        <asp:ListBox ID="lstCorreos" runat="server" Height="31px" Style="z-index: 109; left: 813px;
            position: absolute; top: 83px" Visible="False" Width="88px"></asp:ListBox>
    </div>
</asp:Content>

