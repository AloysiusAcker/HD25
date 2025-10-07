<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Carga_Individual.aspx.vb" Inherits="Inventario_Inventario_Carga_Individual" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 1px; text-align: center; left: 253px; top: 275px;">
                        Carga Individual</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 101px" valign="top"></td>
                <td align="left" style="width: 90px" valign="top"></td>
                <td align="left" style="width: 30px" valign="top"></td>
                <td align="left" style="width: 430px" valign="top"></td>
                <td align="left" style="width: 100px" valign="top"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" colspan="5" style="height: 22px" valign="top">
                    <asp:Label id="lblError" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Red" __designer:wfdid="w21"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
           <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 101px; height: 22px" valign="top">
                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Placa"></asp:Label></td>
                <td align="left" colspan="4" style="height: 22px" valign="top">
                    <asp:TextBox ID="txtPlaca" runat="server" Font-Names="Arial" Font-Size="8pt" Width="96px" AutoPostBack="True" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 101px" valign="middle">
                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nº Serie"></asp:Label>
                </td>
                <td align="left" style="height: 22px;" valign="top" colspan="4">
                    <asp:TextBox ID="txtNroSerie" runat="server" AutoPostBack="True" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" Width="96px"></asp:TextBox>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" style="width: 101px" valign="middle">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ubicación Destino"></asp:Label>
                </td>
                <td align="left" valign="middle" colspan="4">
                    <asp:RadioButtonList ID="optUbicacionD" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Height="1px" RepeatDirection="Horizontal" Width="240px">
                        <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                        <asp:ListItem Value="1">Almacén</asp:ListItem>
                        <asp:ListItem Value="2">Centro Costo</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top"></td>
                <td align="left" valign="middle" colspan="5"></td>
                <td align="left" style="width: 25px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="height: 22px; vertical-align: middle;" valign="top" colspan="5">
                    <asp:TextBox ID="txtDCodigo" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="68px"></asp:TextBox>
                    <asp:Button ID="btnUbica" runat="server" CssClass="EstiloBoton_Ac" Text="..." Width="22px" />
                    <asp:TextBox ID="txtDDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True" Width="200px"></asp:TextBox>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle" valign="top" colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <contenttemplate>
                            <asp:TextBox ID="txtDUbicacion" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="70px"></asp:TextBox>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="FlexRecep" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                        </triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="4">
                    <asp:Button ID="btnListar" runat="server" CssClass="EstiloBoton_Ac" Text="Listar" Width="96px" />
                   <input id="BtnIngresar" type="button" value="Equipo Nuevo" runat="server" class="EstiloBoton_Ac" visible ="true"   style="width: 96px" />
                    <asp:Button ID="BtnIngresarEq" runat="server" CssClass="EstiloBoton_Ac" Text="Generar Ingreso"  Width="96px"  />
                 </td>
                <td align="left" style="width: 100px; height: 22px" valign="top"></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>       
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblRegistroRe" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>                       
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="FlexRecep" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>     
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div style="width:748px; overflow: scroll; border-right: white 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset; visibility: visible; border-style: none; border-color: #FFFFFF;" id="DIV4" runat="server">
                                <asp:GridView id="FlexRecep" runat="server" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" AllowPaging="True" PageSize="100">
                                    <Columns>
                                        <asp:ButtonField CommandName="Quitar" Text="Quitar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Artículo">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                        <asp:BoundField DataField="TIPOBIEN" HeaderText="Tipo Bien">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adquisición">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Antiguedad" HeaderText="Antiguedad">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Zona">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlRecZona" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlRecEstado" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Responsable / Observación">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecObs" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Obs") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Fin">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecFecha" runat="server"  BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Fecha") %>' Height="17px" Width="110px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRecFecha" PopupButtonID="txtRecFecha" ></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="serie_numerar">
                                        <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Volumen">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRecVolumen" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Obs") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UBICACT_CODIGO">
                                        <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px; vertical-align: middle; text-align: left;" valign="top" colspan="5">
                    <asp:UpdatePanel id="UpdatePanel4" runat="server">
                        <contenttemplate>
                            <asp:Label id="lblRegistro" runat="server" Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon"></asp:Label>
                            <input id="btnOpen" type="button" value="Si" runat="server" class="EstiloBoton" visible ="false"  />
                            <asp:Button ID="BtnNo" runat="server" CssClass="EstiloBoton" Text="No" Visible="False" />
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 19px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 1px;" valign="top"></td>
                <td align="left" colspan="5" valign="top" style="height: 1px">
                    <div style="width:748px; overflow: scroll; border-right: white 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset; visibility: visible; border-style: none; border-color: #FFFFFF;" id="DIV1" runat="server">
                                <asp:GridView id="Flex" runat="server" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderColor="Gray" AutoGenerateColumns="False" AllowPaging="True">
                                    <Columns>
                                        <asp:ButtonField CommandName="Enviar" Text="Enviar a Almacén">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="Agregar" Text="Agregar">
                                        <ControlStyle CssClass="EstiloBoton" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Artículo">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                        <asp:BoundField DataField="TIPOBIEN" HeaderText="Tipo Bien">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" ></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adquisición">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Antiguedad" HeaderText="Antiguedad">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Zona">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlZona" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlEstado" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Responsable / Observación">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObs" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Obs") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Fin">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFecha" runat="server"  BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Fecha") %>' Height="17px" Width="110px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha" PopupButtonID="txtFecha" ></cc1:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="serie_numerar">
                                        <ItemStyle ForeColor="White" Width="0px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Volumen">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtVolumen" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Obs") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                </asp:GridView> 
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 1px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 101px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 90px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 30px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 430px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 100px; height: 19px;" valign="top"></td>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
                <td align="left" style="height: 19px;" valign="top" colspan="5">
                    <asp:DetailsView ID="FlexDetalle" runat="server" AutoGenerateRows="False" Font-Names="Arial" Font-Size="8pt" AllowPaging="True">

                        <Fields>
                            <asp:BoundField DataField="Cod_Articulo" HeaderText="Artículo" />
                            <asp:BoundField DataField="art_descripcion" HeaderText="Descripción" />
                            <asp:BoundField DataField="Serie_nro" HeaderText="Serie Nro." />
                            <asp:BoundField DataField="placa_nro" HeaderText="Placa Nro." />
                            <asp:BoundField DataField="Almacen_Nombre" HeaderText="Ubicación" />
                            <asp:BoundField DataField="tipobien" HeaderText="Tipo Bien" />
                            <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adquisión" />
                            <asp:BoundField DataField="Antiguedad" HeaderText="Antiguedad" />
                            <asp:BoundField DataField="SERIE_VALORRESIDUAL" HeaderText="Valor Libro" />
                            <asp:TemplateField HeaderText="Zona">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDZona" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <InsertItemTemplate >
                                    <asp:DropDownList ID="ddlDEstado" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                    </asp:DropDownList>
                                </InsertItemTemplate>                    
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDEstado" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" >
                                    </asp:DropDownList>
                                  <%--  <label id="lblEstado" runat="server" text='<%# Bind("Estado") %>'></label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observación / Responsable">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDObs" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Obs") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Fin">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDFecha" runat="server"  BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("Fecha") %>' Height="17px" Width="110px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDFecha" PopupButtonID="txtDFecha" ></cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Enviar" Text="Enviar al Destino">
                            <ControlStyle CssClass="EstiloBoton" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox id="lblSerieNumerar" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt"  text='<%# Bind("SERIE_NUMERAR") %>' style="color: #FFFFFF"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>

                    </asp:DetailsView>
                </td>
                <td align="left" style="width: 25px; height: 19px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px" valign="top" colspan="5">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
                <td align="left" style="height: 19px" valign="top" colspan="5"></td>
                <td align="left" style="width: 25px; height: 19px" valign="top"></td>
            </tr>
        </table>
    </div> 
    <div>    
    <asp:Panel ID="Panel2" runat="server">
            <div style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 500px; background-color: darkgray; border-right: gray 1px outset; border-top: gray 1px outset; border-left: gray 1px outset; border-bottom: gray 1px outset;">
                    <tr>
                        <td align="left" style="width: 25px; height: 25px" valign="middle">
                        </td>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 25px; text-align: center"
                            valign="middle">
                            <asp:UpdatePanel id="UpdatePanel16" runat="server">
                                <contenttemplate>
                                <asp:Label ID="lblBusUbica" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Maroon" Text="Busqueda de Almacén y/o Centro de Costos" Width="280px"></asp:Label>
                                </contenttemplate>
                            </asp:UpdatePanel>&nbsp;&nbsp;
                        </td>
                        <td align="left" style="width: 25px; height: 25px" valign="middle">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código"></asp:Label></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel18" runat="server">
                                <contenttemplate>
                            <asp:TextBox ID="txtBusCod" runat="server" Font-Names="Arial" Font-Size="8pt" Width="270px"></asp:TextBox>
                            </contenttemplate>
                                <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                </triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                            <asp:Button ID="btnUbiCerrar" runat="server" BackColor="LightGray" BorderColor="Silver"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Cerrar" Width="80px" /></td>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 70px; height: 22px; text-align: left">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"
                                Width="60px"></asp:Label></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 280px; height: 22px; text-align: left">
                            <asp:UpdatePanel id="UpdatePanel17" runat="server">
                                <contenttemplate>
                                <asp:TextBox ID="txtBusDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="270px"></asp:TextBox>
                                </contenttemplate>
                                <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                </triggers>
                            </asp:UpdatePanel></td>
                        <td align="left" valign="middle" style="vertical-align: middle; width: 100px; height: 22px; text-align: right">
                            <asp:Button ID="btnUbiListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                Text="Listar" Width="80px" /></td>
                        <td align="left" style="width: 25px; height: 22px;" valign="middle"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px" valign="middle">
                        </td>
                        <td align="left" colspan="3" valign="middle">
                            <asp:UpdatePanel id="UpdatePanel7" runat="server">
                                <contenttemplate>
                                    <div style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; OVERFLOW: auto; BORDER-LEFT: gray 2px outset; WIDTH: 450px; BORDER-BOTTOM: gray 2px outset; HEIGHT: 250px" id="DIV2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Height="139px" Font-Size="8pt" Font-Names="Arial" __designer:wfdid="w64" AutoGenerateColumns="False" Font-Overline="False"><Columns>
                                    <asp:ButtonField CommandName="Aceptar" Text="&lt;&lt;" ButtonType="Button">
                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial" Font-Size="8pt" Width="30px"></ControlStyle>

                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CODIGO">
                                    <ItemStyle ForeColor="DarkGray" Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                    </Columns>
                                    </asp:GridView></div>
                                </contenttemplate>
                                <triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click"></asp:AsyncPostBackTrigger>
                                    <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click"></asp:AsyncPostBackTrigger>
                                </triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 25px" valign="middle"></td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 25px; height: 19px;" valign="middle">
                        </td>
                        <td align="left" valign="middle" style="width: 70px; height: 19px">
                        </td>
                        <td align="left" valign="middle" style="width: 280px; height: 19px">
                        </td>
                        <td align="left" valign="middle" style="width: 100px; height: 19px">
                        </td>
                        <td align="left" style="width: 25px; height: 19px;" valign="middle">
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>        
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
            CacheDynamicResults="True" CancelControlID="btnUbiCerrar" PopupControlID="Panel2"
            TargetControlID="btnUbica" X="300" Y="200">
        </cc1:ModalPopupExtender>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="Flex" EventName="PageIndexChanging" />
        <asp:AsyncPostBackTrigger ControlID="Flex" EventName="DataBound" />
        <asp:AsyncPostBackTrigger ControlID="FlexRecep" EventName="RowCommand" />
        <asp:AsyncPostBackTrigger ControlID="FlexRecep" EventName="DataBound" />
        <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>

