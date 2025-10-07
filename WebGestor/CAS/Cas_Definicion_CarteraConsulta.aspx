<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Definicion_CarteraConsulta.aspx.vb" Inherits="Cas_Definicion_CarteraConsulta" title="GestorPlus" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
		var ModalProgress = '<%= ModalProgress.ClientID %>';         
    </script>
    <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px" id="TABLE1">
            <tr>
                <td align="left" colspan="8" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 550px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Mantenimiento de Base de Datos</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8" style="height: 11px" valign="top">
                    <img src="/Fotos/lineaCas.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 13px" valign="top"></td>
                <td align="left" style="width: 70px; height: 13px" valign="top"></td>
                <td align="left" style="width: 120px; height: 13px" valign="top"></td>
                <td align="left" style="width: 70px; height: 13px" valign="top"></td>
                <td align="left" style="width: 120px; height: 13px" valign="top"></td>
                <td align="left" style="width: 70px; height: 13px" valign="top"></td>
                <td align="left" style="width: 100px; height: 13px" valign="top"></td>
                <td align="left" style="width: 25px; height: 13px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 18px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 18px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativo"></asp:Label></td>
                <td align="left" colspan="2" style="height: 18px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Producto"
                        Width="50px"></asp:Label></td>
                <td align="left" colspan="2" style="height: 18px; vertical-align: middle;" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                        Text="Sub-Producto" Width="66px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 18px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:DropDownList ID="cboBusAplicativo" runat="server" CausesValidation="True" Font-Names="Arial"
                        Font-Size="8pt" Width="184px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
                            <asp:DropDownList id="cboBusProducto" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboBusAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" colspan="2" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>
                            <asp:DropDownList id="cboBusSubProd" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboBusProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 11px" valign="top"></td>
                <td align="left" style="height: 11px" valign="top" colspan="2"></td>
                <td align="left" style="width: 70px; height: 11px" valign="top"></td>
                <td align="left" colspan="3" style="height: 11px; text-align: right" valign="top"></td>
                <td align="left" style="width: 25px; height: 11px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                <td align="left" style="width: 70px; height: 20px" valign="top"></td>
                <td align="left" style="width: 120px; height: 20px" valign="top"></td>
                <td align="left" style="width: 70px; height: 20px" valign="top"></td>
                <td align="left" colspan="3" style="height: 20px; text-align: right" valign="top">
                    <asp:Button ID="btnListar" runat="server" BorderColor="Gray" BorderStyle="Outset" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Listar" Width="80px" BackColor="LightGray" ForeColor="Gray" />
                    <asp:Button ID="btnNuevo" runat="server" BorderColor="Gray" BorderStyle="Outset" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Nuevo" Width="80px" BackColor="LightGray" ForeColor="Gray" /></td>
                <td align="left" style="width: 25px; height: 20px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                <td align="left" colspan="6" style="height: 20px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel6" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblCount" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Total de Registros : 0"></asp:Label>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 18px" valign="top"></td>
                <td align="left" colspan="6" style="height: 18px" valign="top">                    
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
                            <div style="border-right: seagreen 1px outset; border-top: seagreen 1px outset; overflow: auto;
                                border-left: seagreen 1px outset; width: 550px; border-bottom: seagreen 1px outset;
                                height: 338px">
                                <asp:GridView id="Flex" runat="server" Width="1700px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" PageSize="40" DataKeyNames="CARCON_APLICATIVO,CARCON_PRODUCTO,CARCON_SUBPRODUCTO">
                                    <Columns>
                                        <asp:ButtonField Text="Editar" ButtonType="Button" CommandName="Editar">
                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="70px"></ControlStyle>                                            
                                        <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        </asp:ButtonField>

                                        <asp:ButtonField Text="Archivos" ButtonType="Button" CommandName="Archivo">
                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="70px"></ControlStyle>
                                        <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        </asp:ButtonField>

                                        <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
                                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
                                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
                                        <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
                                        <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
                                        <ItemStyle Width="960px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_CODIGO">
                                        <ItemStyle Width="0px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_APLICATIVO">
                                        <ItemStyle Width="0px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_PRODUCTO">
                                        <ItemStyle Width="0px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CARCON_SUBPRODUCTO">
                                        <ItemStyle Width="0px"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView> 
                            </div>          
                            <div id="DivArchivo" style="border-right: seagreen 1px outset; border-top: seagreen 1px outset; overflow: auto;
                                border-left: seagreen 1px outset; width: 550px; border-bottom: seagreen 1px outset">
                                <asp:GridView ID="FlexDetalle" runat="server" AutoGenerateColumns="False" Width="540px" Font-Size="8pt" Font-Names="Arial">
                                    <Columns>
                                        <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                        <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px"></ControlStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                        </asp:ButtonField>
                                        <asp:TemplateField HeaderText="Nombre del Archivo">
                                            <ItemTemplate>
                                                <div id="Doc" runat="server" style="width: 350px; height: 22px"></div>                                    
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NRO_AVISO" HeaderText="BC" >
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo" HeaderText="Codigo" >
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel>                    
                    <br />
                </td>
                <td align="left" style="width: 25px; height: 18px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" colspan="5" style="height: 19px" valign="top"></td>
                <td align="left" style="width: 100px; height: 19px" valign="top"></td>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top"></td>
                <td align="left" colspan="6" style="height: 20px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <table id="lblIngreso" runat="server" border="0" cellpadding="0" cellspacing="0"
                                style="width: 550px" visible="false">
                                <tr>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                        <asp:Label ID="lblEtiqueta" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Maroon" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 18px" valign="top">
                                        <asp:Label ID="lblEtiqueta1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Aplicativo"
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 18px" valign="top">
                                        <asp:Label ID="lblEtiqueta2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Producto"
                                            Visible="False" Width="50px"></asp:Label>
                                    </td>
                                    <td align="left" style="vertical-align: middle; width: 170px; height: 18px" valign="top">
                                        <asp:Label ID="lblEtiqueta3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sub-Producto"
                                            Visible="False" Width="84px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                                        <asp:DropDownList ID="cboAplicativo" runat="server" CausesValidation="True" Font-Names="Arial"
                                            Font-Size="8pt" Visible="False" Width="184px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                                        <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                            <contenttemplate>
                                                <asp:DropDownList id="cboProducto" runat="server" Width="184px" Font-Size="8pt" Font-Names="Arial" Visible="False" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left" style="vertical-align: middle; width: 170px; height: 22px" valign="top">
                                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                                            <contenttemplate>
                                                <asp:DropDownList id="cboSubProd" runat="server" Width="170px" Font-Size="8pt" Font-Names="Arial" Visible="False" AutoPostBack="True" CausesValidation="True"></asp:DropDownList> 
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 18px" valign="top">
                                        <asp:Label ID="lblEtiqueta4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Transacción"
                                            Visible="False" Width="64px"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 190px; height: 18px" valign="top">
                                    </td>
                                    <td align="left" style="width: 170px; height: 18px" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" style="vertical-align: middle" valign="top">
                                        <asp:TextBox ID="txtTransaccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Height="50px" TextMode="MultiLine" Visible="False" Width="544px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 18px" valign="top">
                                        <asp:Label ID="lblEtiqueta5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Consulta"
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 190px; height: 18px" valign="top">
                                    </td>
                                    <td align="left" style="width: 170px; height: 18px" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" style="vertical-align: middle" valign="top">
                                        <asp:TextBox ID="txtConsulta" runat="server" Font-Names="Arial" Font-Size="8pt" Height="50px"
                                            TextMode="MultiLine" Visible="False" Width="544px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 190px; height: 18px" valign="top">
                                        <asp:Label ID="lblEtiqueta6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Solución"
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 190px; height: 18px" valign="top"></td>
                                    <td align="left" style="width: 170px; height: 18px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 58px" valign="top">
                                        <asp:TextBox ID="txtSolucion" runat="server" Font-Names="Arial" Font-Size="8pt" Height="50px"
                                            TextMode="MultiLine" Visible="False" Width="544px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td runat="server" colspan="3" align="left" style="VERTICAL-ALIGN: text-top; HEIGHT: 22px" valign="top">
                                        <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Adjuntar"></asp:Label>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td runat="server" align="left" colspan="3" style="VERTICAL-ALIGN: middle; HEIGHT: 22px" valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Arial" Font-Size="10px" Width="460px" />
                                                <asp:Button ID="BtnArchivo" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" 
                                                    BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" 
                                                    ForeColor="Gray" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" 
                                                    Text="Adjuntar" Width="72px" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="BtnArchivo" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" align="left" style="VERTICAL-ALIGN: text-top; HEIGHT: 22px" valign="top" colspan="3">
                                        <div id="div4" style="BORDER-RIGHT: gray 1px inset; BORDER-TOP: gray 1px inset; OVERFLOW: auto; BORDER-LEFT: gray 1px inset; WIDTH: 544px; 
                                            BORDER-BOTTOM: gray 1px inset;">
                                            <asp:GridView ID="GvArchivo" runat="server" AutoGenerateColumns="False" Width="540px" Font-Size="8pt" Font-Names="Arial"><Columns>
                                                <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" 
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="48px"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                </asp:ButtonField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <div id="Doc" runat="server" style="width: 50px; height: 22px"></div>                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ARCHIVO" HeaderText="Archivo">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NRO_AVISO" HeaderText="Numero" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="90"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CODIGO" HeaderText="Codigo" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="50"></ItemStyle>
                                                </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 190px; height: 25px" valign="top">
                                        <asp:TextBox ID="txtCodConsulta" runat="server" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" ReadOnly="True" Width="39px"></asp:TextBox></td>
                                    <td align="left" colspan="2" style="height: 25px; text-align: right; vertical-align: middle;" valign="top">
                                        <asp:Button ID="btnGuardar" runat="server" BorderColor="Gray" BorderStyle="Outset"
                                            BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Guardar" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                            Width="80px" Visible="False" BackColor="LightGray" ForeColor="Gray" />
                                        <asp:Button ID="btnCancelar" runat="server" BorderColor="Gray" BorderStyle="Outset"
                                            BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Text="Cancelar" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                            Width="80px" Visible="False" BackColor="LightGray" ForeColor="Gray" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 20px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="498px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 120px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 70px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 100px; height: 20px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 20px" valign="top">
                </td>
            </tr>
        </table>
    </div>
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

