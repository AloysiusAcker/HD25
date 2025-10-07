<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_Mant_Perfiles.aspx.vb" Inherits="SegSistema_Mant_Perfiles" title="Sistema - Perfiles" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" lang="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
  <div style="text-align: center">
      <div style="text-align: left">
          <table border="0" cellpadding="0" cellspacing="0" style="width: 900px">
              <tr>
                  <td align="left" style="width: 25px; height: 50px" valign="top"></td>
                  <td align="left" style="width: 850px; height: 50px; text-align: center" valign="top">
                      <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                          font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: gray;
                          font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                          height: 1px; text-align: center">
                          Perfiles de Usuario</div>
                  </td>
                  <td align="left" style="width: 25px; height: 50px" valign="top"></td>
              </tr>
              <tr>
                  <td align="left" colspan="3" style="height: 11px; 
                        background-image: url(../Fotos/linea.JPG);" valign="top">
                  </td>
              </tr>
              <tr>
                  <td align="left" style="width: 25px; height: 14px" valign="top"></td>
                  <td align="left" style="width: 850px; height: 14px" valign="top"></td>
                  <td align="left" style="width: 25px; height: 14px" valign="top"></td>
              </tr>
              <tr>
                  <td align="left" style="width: 25px; height: 811px;" valign="top">
                  </td>
                  <td align="left" style="width: 850px; height: 611px;" valign="top">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>
                            <cc1:TabContainer id="Ficha" runat="server" Width="850px" Height="450px" Font-Size="8pt" Font-Names="Arial" ActiveTabIndex="0" AutoPostBack="True">
                                <cc1:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
                                    <HeaderTemplate>Define Perfiles</HeaderTemplate>
                                    <ContentTemplate>
                                        <div style="TEXT-ALIGN: left">
                                            <div style="TEXT-ALIGN: left">
                                            <table style="WIDTH: 830px; POSITION: static" cellSpacing=0 cellPadding=0 border=0>
                                                <tbody>
                                                    <tr>
                                                        <td style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 155px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 155px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 180px; HEIGHT: 15px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 180px; HEIGHT: 15px" vAlign=top align=left></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left>
                                                            <asp:Button id="btnNuevo" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" runat="server" CssClass="EstiloBoton" Width="80px" Height="20px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w21" 
                                                                ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Nuevo"></asp:Button> 

                                                        </td>
                                                        <td style="WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 155px; HEIGHT: 22px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 155px; HEIGHT: 22px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 180px; HEIGHT: 22px" vAlign=top align=left></td>
                                                        <td style="WIDTH: 180px; HEIGHT: 22px" vAlign=top align=left></td>

                                                    </tr>
                                                    <tr>
                                                        <td style="HEIGHT: 250px" vAlign=top align=left colSpan=6>
                                                            <div style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 828px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 235px">
                                                                <asp:GridView id="FlexPU" runat="server" Width="920px" Font-Size="8pt" 
                                                                    Font-Names="Arial" __designer:wfdid="w22" BorderStyle="Outset" 
                                                                    BorderWidth="1px" BorderColor="DarkGray" 
                                                                    AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>

                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                                        </asp:ButtonField>
                                                                        <asp:ButtonField CommandName="AsignarPag" Text="Asignar P&#225;gina" ButtonType="Button">
                                                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="90px"></ControlStyle>

                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px"></ItemStyle>
                                                                        </asp:ButtonField>
                                                                        <asp:BoundField DataField="GE_NOMBRE" HeaderText="Grupo de Empresa">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"></HeaderStyle>

                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GEE_NOMBRE" HeaderText="Empresa">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="200px"></HeaderStyle>

                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Names="Arial" Font-Size="8pt" Width="100px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="MODINTEG_NOMBRE" HeaderText="M&#243;dulo de Integraci&#243;n">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PERFIL_CODIGO" HeaderText="Perfil">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PERFIL_DES" HeaderText="Descripci&#243;n del Perfil">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="GRPOEMPRESA_CODIGO">
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt" Width="50px"></HeaderStyle>

                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="EMPRESA_CODIGO">
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="MODINTEG_CODIGO">
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="PERFIL_CODUNICO">
                                                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                    </Columns>

                                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                                                                    <PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>
                                                                </asp:GridView> 
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="VERTICAL-ALIGN: middle" vAlign=top align=left colSpan=6>
                                                            <asp:Label id="lblPUError" runat="server" Font-Size="8pt" Font-Names="Arial" 
                                                                __designer:wfdid="w23" ForeColor="Red"></asp:Label> 
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td vAlign=top align=left colSpan=6><DIV style="TEXT-ALIGN: left">
                                                        <table style="WIDTH: 528px" id="lblDefinePerfil" cellSpacing=0 cellPadding=0 border=0 runat="server" Visible="False">
                                                            <tr runat="server">
                                                                <td runat="server" align="left" colspan="6" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                    <asp:Label ID="lblPEtiqueta" runat="server" __designer:wfdid="w24" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" Text="Nuevo Perfil"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" valign="top">
                                                                    <asp:Label ID="lblP1" runat="server" __designer:wfdid="w25" Font-Names="Arial" Font-Size="8pt" Text="Grupo Empresa" Width="75px"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="5" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                    <asp:DropDownList ID="cboGrpoEmp" runat="server" __designer:wfdid="w26" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Width="448px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" valign="top">
                                                                    <asp:Label ID="lblP4" runat="server" __designer:wfdid="w27" Font-Names="Arial" Font-Size="8pt" Text="Empresa"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="3" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                    <asp:DropDownList ID="cboEmp" runat="server" __designer:wfdid="w28" Font-Names="Arial" Font-Size="8pt" Width="181px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" valign="top">
                                                                    <asp:Label ID="lblP2" runat="server" __designer:wfdid="w29" Font-Names="Arial" Font-Size="8pt" Text="Mod. Integración" Width="79px"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 184px; HEIGHT: 22px" valign="top">
                                                                    <asp:DropDownList ID="cboModInteg" runat="server" __designer:wfdid="w30" Font-Names="Arial" Font-Size="8pt" Width="184px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" valign="top">
                                                                    <asp:Label ID="lblP3" runat="server" __designer:wfdid="w31" Font-Names="Arial" Font-Size="8pt" Text="Código Perfil"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" valign="top">
                                                                    <asp:TextBox ID="txtPCodUnico" runat="server" __designer:wfdid="w32" Font-Names="Arial" Font-Size="8pt" MaxLength="3" Width="51px"></asp:TextBox>
                                                                </td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" valign="top">
                                                                    <asp:Label ID="lblP5" runat="server" __designer:wfdid="w33" Font-Names="Arial" Font-Size="8pt" Text="Descripción" Width="62px"></asp:Label>
                                                                </td>
                                                                <td runat="server" align="left" colspan="3" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                                                    <asp:TextBox ID="txtPDescripcion" runat="server" __designer:wfdid="w34" Font-Names="Arial" Font-Size="8pt" MaxLength="30" Width="312px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server">
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" valign="top">
                                                                    <asp:TextBox ID="txtCodPerfil" runat="server" __designer:wfdid="w35" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="26px"></asp:TextBox>
                                                                </td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 60px; HEIGHT: 22px" valign="top"></td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" valign="top"></td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 54px; HEIGHT: 22px" valign="top"></td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" valign="top"></td>
                                                                <td runat="server" align="left" style="VERTICAL-ALIGN: middle; WIDTH: 184px; HEIGHT: 22px; TEXT-ALIGN: right" valign="top">
                                                                    <asp:Button ID="btnPGuardar" runat="server" __designer:wfdid="w36" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Height="20px" OnClick="btnPGuardar_Click" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="80px"></asp:Button>
                                                                    <asp:Button ID="btnCancelar" runat="server" __designer:wfdid="w37" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Height="20px" OnClick="btnCancelar_Click" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="80px"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </DIV></td></tr><TR>
                                                <TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD>
                                                <TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD>
                                                <TD style="WIDTH: 105px; HEIGHT: 15px" vAlign=top align=left></TD>
                                                <TD style="WIDTH: 105px; HEIGHT: 15px" vAlign=top align=left></TD>
                                                <TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD>
                                                <TD style="WIDTH: 80px; HEIGHT: 15px" vAlign=top align=left></TD>
                                            </TR></tbody></table></div>

                                        </div>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            <cc1:TabPanel runat="server" HeaderText="TabPanel2" ID="TabPanel2">
                                <HeaderTemplate>Asignar Página</HeaderTemplate>
                                <ContentTemplate>
                                    <table style="WIDTH: 530px" cellSpacing=0 cellPadding=0 border=0>
                                        <tbody>
                                            <tr>
                                                <td style="WIDTH: 30px" vAlign=top align=left></td>
                                                <td style="WIDTH: 51px" vAlign=top align=left></td>
                                                <td style="WIDTH: 290px" vAlign=top align=left></td>
                                                <td style="WIDTH: 81px" vAlign=top align=left></td>
                                                <td style="WIDTH: 80px" vAlign=top align=left></td>
                                            </tr>
                                            <tr>
                                                <td style="VERTICAL-ALIGN: middle; WIDTH: 30px; HEIGHT: 22px" vAlign=top align=left>
                                                    <asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" 
                                                        __designer:wfdid="w38" Text="Pérfil"></asp:Label> 
                                                </td>
                                                <td style="VERTICAL-ALIGN: middle; WIDTH: 51px; HEIGHT: 22px" vAlign=top align=left>
                                                    <asp:TextBox id="txtAPCodPerfil" runat="server" Width="41px" Font-Size="8pt" 
                                                        Font-Names="Arial" __designer:wfdid="w39" ReadOnly="True"></asp:TextBox> 
                                                </td>
                                                <td style="VERTICAL-ALIGN: middle; WIDTH: 290px; HEIGHT: 22px" vAlign=top align=left>
                                                    <asp:TextBox id="txtAPPerfil" runat="server" Width="288px" Font-Size="8pt" 
                                                        Font-Names="Arial" __designer:wfdid="w40" ReadOnly="True"></asp:TextBox> 
                                                </td>
                                                <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=top align=left colSpan=2>
                                                    <asp:Button id="btnRegresar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" 
                                                        onclick="btnRegresar_Click" runat="server" CssClass="EstiloBoton" Width="68px" Font-Size="8pt" Font-Names="Arial" 
                                                        __designer:wfdid="w41" ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" 
                                                        BackColor="LightGray" Text="Regresar"></asp:Button> 
                                                    <asp:Button id="btnAPGuardar" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'" onclick="btnAPGuardar_Click" 
                                                        runat="server" CssClass="EstiloBoton" Width="68px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w42" 
                                                        ForeColor="Gray" BorderStyle="Outset" BorderWidth="1px" BorderColor="Gray" BackColor="LightGray" Text="Guardar">
                                                    </asp:Button> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5>
                                                    <asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" 
                                                        __designer:wfdid="w43" ForeColor="Black" Text="**Antes de pasar a la sgte. página debe de guardar las páginas marcadas"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="VERTICAL-ALIGN: middle; HEIGHT: 21px" vAlign=top align=left colSpan=5>
                                                    <div style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; 
                                                        OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; 
                                                        WIDTH: 826px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 247px">
                                                        <asp:GridView id="FlexPag" runat="server" Width="870px" Font-Size="8pt" 
                                                            Font-Names="Arial" __designer:wfdid="w44" PageSize="7" 
                                                            AutoGenerateColumns="False" AllowPaging="false">
                                                            <Columns>
                                                                <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="50px"></ControlStyle>
                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                </asp:ButtonField>
                                                                <asp:TemplateField><ItemTemplate>
                                                                   <asp:CheckBox ID="chkPag" runat="server" Height="20px" Width="1px" />                                                                      
                                                                </ItemTemplate>
                                                                <ControlStyle Width="20px"></ControlStyle>
                                                                <ItemStyle Width="20px"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PAG_CODIGO" HeaderText="C&#243;d. P&#225;gina" SortExpression="PAG_CODIGO">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PAG_NOMBRE" HeaderText="Nombre P&#225;gina" SortExpression="PAG_NOMBRE">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PAG_DESCRIPCION" HeaderText="Descripci&#243;n de la P&#225;gina" SortExpression="PAG_DESCRIPCION">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="250px"></ItemStyle>
                                                                </asp:BoundField>
                                                                </Columns>
                                                        </asp:GridView> 
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr><td style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=5>
                                                <asp:Label id="lblAPError" runat="server" Font-Size="8pt" Font-Names="Arial" 
                                                    __designer:wfdid="w45" ForeColor="Red"></asp:Label> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="WIDTH: 30px; HEIGHT: 24px" vAlign=top align=left></td>
                                                <td style="WIDTH: 51px; HEIGHT: 24px" vAlign=top align=left></td>
                                                <td style="WIDTH: 290px; HEIGHT: 24px" vAlign=top align=left>
                                                    <asp:TextBox id="txtCodUnicoAP" runat="server" Width="20px" Font-Size="8pt" 
                                                        Font-Names="Arial" __designer:wfdid="w46" Visible="False"></asp:TextBox> 
                                                    <asp:TextBox id="txtModIntegAP" runat="server" Font-Size="8pt" Font-Names="Arial"
                                                        __designer:wfdid="w47" Visible="False"></asp:TextBox> 
                                                </td>
                                                <td style="WIDTH: 81px; HEIGHT: 24px" vAlign=top align=left></td>
                                                <td style="WIDTH: 80px; HEIGHT: 24px" vAlign=top align=left></td>
                                            </tr>
                                        </tbody>

                                    </table>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            </cc1:TabContainer> 
                          </ContentTemplate>
                          <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                          </Triggers>
                      </asp:UpdatePanel>
                  </td>
                  <td align="left" style="width: 25px; height: 611px;" valign="top"></td>
              </tr>
          </table>
      </div>
     </div>
    <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div style="position: relative; top: 30%; text-align: center;">
                    &nbsp;<img src="../Fotos/5.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
	    BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
</asp:Content>

