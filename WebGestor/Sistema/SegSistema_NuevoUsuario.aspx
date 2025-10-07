<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_NuevoUsuario.aspx.vb" Inherits="Sistema_SegSistema_NuevoUsuario" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 253px; vertical-align: middle; width: 544px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px;
                        text-align: center">
                        Registro Nuevo Usuario</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                    valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 150px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 60px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 160px; height: 15px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 15px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Datos de Usuario" Width="96px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq30" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Usuario *"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtUsuario" runat="server" Font-Names="Arial" Font-Size="8pt" Width="208px" TabIndex="1" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    <asp1:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblEtq31" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                                Text="Usuario ya existe." Visible="False"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp1:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                            <asp1:AsyncPostBackTrigger ControlID="txtUsuario" EventName="TextChanged" />
                        </Triggers>
                    </asp1:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Contraseña *"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox
                ID="txtClaveN1" runat="server" Font-Names="Arial" Font-Size="8pt"
                MaxLength="12" Style="z-index: 108; left: 424px; top: 416px" TabIndex="2" TextMode="Password"
                Width="208px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    <asp1:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblEtq32" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                                Text="Contiene caracter no válido." Visible="False" Width="136px"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp1:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                            <asp1:AsyncPostBackTrigger ControlID="txtClaveN1" EventName="TextChanged" />
                        </Triggers>
                    </asp1:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblEtq5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Debe contener seis caracteres como mínimo y 12 como máximo, distingue entre mayúsculas y minúsculas."
                        Width="272px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Confirmar Contraseña *" Width="64px"></asp:Label></td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtClaveN2" runat="server" Font-Names="Arial"
                    Font-Size="8pt" MaxLength="12" Style="z-index: 109; left: 424px;
                    top: 464px" TabIndex="3" TextMode="Password" Width="208px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    <asp1:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblEtq33" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                                Text="Las Contraseñas no coinciden." Visible="False" Width="152px"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp1:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click" />
                            <asp1:AsyncPostBackTrigger ControlID="txtClaveN2" EventName="TextChanged" />
                        </Triggers>
                    </asp1:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Datos Personales"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ap. Paterno *"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtApePat" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="15"
                        TabIndex="4" Width="360px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ap. Materno"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtApeMat" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="15"
                        TabIndex="5" Width="360px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombres *"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtNombres" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="40"
                        TabIndex="6" Width="360px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Email *"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
        <asp:TextBox ID="txtEmail" runat="server" Font-Names="Arial" Font-Size="8pt"
            MaxLength="50" Style="z-index: 107; left: 424px; top: 384px" TabIndex="7" Width="360px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha de Nac. *"
                        Width="80px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:TextBox ID="txtFechaNac" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="10"
                        TabIndex="8" Width="136px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="dd/mm/aaaa" Width="56px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Doc. Identidad *"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboDoc" runat="server" Font-Names="Arial" Font-Size="8pt"
                        TabIndex="9" Width="144px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="N° *"
                        Width="24px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:TextBox id="txtNroDoc" tabIndex=10 runat="server" CssClass="bordeTexbox" Width="140px" Font-Size="8pt" Font-Names="Arial" MaxLength="15" __designer:wfdid="w50" Enabled="False"></asp:TextBox> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboDoc" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sexo *"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboSexo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        TabIndex="11" Width="144px">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado Civil" Width="56px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboEstadoCivil" runat="server" Font-Names="Arial" Font-Size="8pt"
                        TabIndex="12" Width="150px">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 47px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 47px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 47px" valign="top">
                    <asp:Label ID="lblEtq16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="País"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 47px" valign="top">
                    <asp:DropDownList ID="cboPais" runat="server" Font-Names="Arial" Font-Size="8pt"
                        TabIndex="13" Width="144px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 47px" valign="top">
                    <asp:Label ID="lblEtq18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Depart."></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 47px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboDpto" tabIndex=14 runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Visible="False" Enabled="False" __designer:wfdid="w59" OnSelectedIndexChanged="cboDpto_SelectedIndexChanged"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboPais" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 47px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Provincia"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
<asp:DropDownList id="cboProv" tabIndex=15 runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" Enabled="False" __designer:wfdid="w62">
                    </asp:DropDownList> 
</ContentTemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboDpto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Distrito"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboDist" tabIndex=16 runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial" Enabled="False" __designer:wfdid="w65"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboProv" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Dirección"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:TextBox ID="txtDireccion" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="80"
                        TabIndex="17" Width="360px" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq23" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Datos Empresa" Width="88px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblEtq24" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Razón Social *"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:DropDownList ID="cboEmpresa" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="368px" AutoPostBack="True" TabIndex="18">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                    <asp:Label ID="lblEtq25" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina *"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 25px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboOficina" tabIndex=19 runat="server" Width="368px" Font-Size="8pt" Font-Names="Arial"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboEmpresa" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                    <asp:Label ID="lbletq26" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Puesto *"></asp:Label></td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 25px" valign="top">
                    <asp:DropDownList ID="cboPuesto" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="368px" TabIndex="20">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                    <asp:Label ID="lblEtq27" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Teléfonos" Visible="False"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                    </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 25px" valign="top">
                    </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 25px" valign="top">
                    <asp:Button ID="btnRegistrar" runat="server"
                        Text="Registrar" Width="144px" TabIndex="21" CssClass="EstiloBoton_Ac" /></td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                    <asp:Label ID="lblEtq28" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Telefono" Visible="False"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 25px" valign="top">
                    <asp:DropDownList ID="cboTipoTelef" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="144px" Visible="False">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 25px" valign="top">
                    <asp:Label ID="lblEtq29" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Número" Visible="False"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 25px" valign="top">
                    <asp:TextBox ID="txtNumero" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="15"
                        Width="136px" Visible="False" CssClass="bordeTexbox"></asp:TextBox></td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 25px" valign="top">
                    <asp:Button ID="Button1" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                        Text="Agregar" Width="56px" Visible="False" /></td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 25px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 5px" valign="top">
                    <asp:Label ID="lblEtq21" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        Text="Nota:"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 5px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 5px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 5px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblEtq22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="* Datos de carácter obligatorio." Width="544px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <contenttemplate>
<asp:Label id="lblErrorData" runat="server" Width="544px" ForeColor="Red" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w86"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnRegistrar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <cc1:modalpopupextender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
			            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" CacheDynamicResults="True"></cc1:modalpopupextender>
    &nbsp;
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress" Style="left: 248px;
        top: 1480px" Width="200px">
        <asp:UpdateProgress id="UpdateProg1" runat="server" DisplayAfter="0">
            <progresstemplate>
<DIV style="POSITION: relative; TOP: 30%; TEXT-ALIGN: center">&nbsp;<IMG src="../Fotos/5.gif" /></DIV>
</progresstemplate>
        </asp:UpdateProgress>
    </asp:Panel>
</asp:Content>

