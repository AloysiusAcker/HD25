<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Informacion_Bienes.aspx.vb" Inherits="Inventario_Inventario_Informacion_Bienes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Información del Bien" CssClass="Titulos" />
            </div>
        </div>
        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="lblError" runat="server" CssClass="control-label-2"></asp:Label>
            </div>
        </div>
        <div class="row espacio">
            <div class="col-md-3">
                <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Serie Nro"></asp:Label>
                <asp:TextBox ID="txtNroSerie" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Placa Nro"></asp:Label>
                <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="..." ForeColor ="White" ></asp:Label>
                <asp:Button ID="BtnListar" runat="server" Text="Listar" CssClass="form-control btn btn-default" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="lblRegistro" runat="server" CssClass="control-label-2" Text="Datos del Bien" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:BoundField DataField="COD_ARTICULO" HeaderText="Cod. Artículo" SortExpression="COD_ARTICULO" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />
                                <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cód. Ubicación" SortExpression="COD_ALMACEN" />
                                <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Nombre Ubicación" SortExpression="ALMACEN_NOMBRE" />
                                <asp:BoundField DataField="ESTADO_EQUIPO" HeaderText="Estado del equipo" SortExpression="ESTADO_EQUIPO" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="lblRecepcion" runat="server" CssClass="control-label-2" Text="Lista de Recepciones" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:GridView id="gridRecepcion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="true" >
                            <Columns>
                                <asp:BoundField DataField="cod_Recepcion" HeaderText="C&#243;digo"  SortExpression="cod_Recepcion" ></asp:BoundField>
                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo"  SortExpression="MOTIVO" ></asp:BoundField>
                                <asp:BoundField DataField="FECHA_REG" HeaderText="Fec. Reg."  SortExpression="FECHA_REG" ></asp:BoundField>
                                <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc."  SortExpression="TIPO_DOC" ></asp:BoundField>
                                <asp:BoundField DataField="NRO_DOC" HeaderText="N° Doumento"  SortExpression="NRO_DOC" ></asp:BoundField>
                                <asp:BoundField DataField="RECEP_NRO_OC" HeaderText="N° Orden Compra"  SortExpression="RECEP_NRO_OC" ></asp:BoundField>
                                <asp:BoundField DataField="FECHA_RECEPCION" HeaderText="Fec. Recep."  SortExpression="FECHA_RECEPCION" ></asp:BoundField>
                                <asp:BoundField DataField="Tipo_Origen" HeaderText="Origen"  SortExpression="Tipo_Origen" />
                                <asp:BoundField DataField="RUC" HeaderText="Código"  SortExpression="RUC" ></asp:BoundField>
                                <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Descripción"  SortExpression="RAZON_SOCIAL" ></asp:BoundField>
                                <asp:BoundField DataField="Tipo_Destino" HeaderText="Destino"  SortExpression="Tipo_Destino" />
                                <asp:BoundField DataField="Destino_Cod" HeaderText="Código"  SortExpression="Destino_Cod" />
                                <asp:BoundField DataField="Destino" HeaderText="Descripción"  SortExpression="Destino" />
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" ></asp:BoundField>
                                <asp:BoundField DataField="ITEM" HeaderText="N&#176; Items" SortExpression="ITEM" ></asp:BoundField>
                                <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. x Rec." SortExpression="CANT_XREC" ></asp:BoundField>
                                <asp:BoundField DataField="CANT_REC" HeaderText="Cant. Rec." SortExpression="CANT_REC" ></asp:BoundField>
                                <asp:BoundField DataField="RECEP_OBSERVACION" HeaderText="Observación" SortExpression="RECEP_OBSERVACION" ></asp:BoundField>
                                <asp:BoundField DataField="TICKET" HeaderText="Nro. Ticket" SortExpression="TICKET" />
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="OC">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"RecepcionHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("RECEP_CODIGO") IsNot DBNull.Value, Eval("RECEP_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="Guía">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#"RecepGuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("RECEP_CODIGO") IsNot DBNull.Value, Eval("RECEP_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        </asp:GridView>
                    </div> 
                </div> 
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="lblSalida" runat="server" CssClass="control-label-2" Text="Lista de Salidas" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView ID="gridSalida" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="Codsalida" HeaderText="Codigo" SortExpression="Codsalida" />
                                <asp:BoundField DataField="Fecha_Sal" HeaderText="Fecha" SortExpression="Fecha_Sal" />
                                <asp:BoundField DataField="Hora_Salida" HeaderText="Hora" SortExpression="Hora_Salida" />
                                <asp:BoundField DataField="Origen_codigo" HeaderText="Cod. Almacén" SortExpression="Origen_codigo" />
                                <asp:BoundField DataField="origen_nombre" HeaderText="Nombre" SortExpression="origen_nombre" />
                                <asp:BoundField DataField="Destino" HeaderText="Destino tipo" SortExpression="Destino" />
                                <asp:BoundField DataField="DESTINO_CODIGO" HeaderText="Cod. Destino" SortExpression="DESTINO_CODIGO" />
                                <asp:BoundField DataField="DESTINO_DESCRIPCION" HeaderText="Nombre Destino" SortExpression="DESTINO_DESCRIPCION" />
                                <asp:BoundField DataField="MOTIVO_GRAL" HeaderText="Motivo" SortExpression="MOTIVO_GRAL" />
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                <asp:TemplateField HeaderText="Nro. Ticket">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("TICKET") %>' CommandName="Select" CommandArgument='<%# Eval("TICKET") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"GuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("GUIREM_CODIGO") IsNot DBNull.Value, Eval("GUIREM_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Nro_Guia" HeaderText="Guía" SortExpression="Nro_Guia" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="LblSalidaCC" runat="server" CssClass="control-label-2" Text="Lista de Centro de Costos" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate> 
                            <asp:GridView ID="gridCC" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:BoundField DataField="Codsalida" HeaderText="Codigo" SortExpression="Codsalida" />
                                    <asp:BoundField DataField="Fecha_Sal" HeaderText="Fecha" SortExpression="Fecha_Sal" />
                                    <asp:BoundField DataField="Hora_Salida" HeaderText="Hora" SortExpression="Hora_Salida" />
                                    <asp:BoundField DataField="Origen_codigo" HeaderText="Cod. Almacén" SortExpression="Origen_codigo" />
                                    <asp:BoundField DataField="Origen" HeaderText="Nombre" SortExpression="Origen" />
                                    <asp:BoundField DataField="Destino" HeaderText="Destino tipo" SortExpression="Destino" />
                                    <asp:BoundField DataField="DESTINO_CODINTERNO" HeaderText="Cod. Destino" SortExpression="DESTINO_CODINTERNO" />
                                    <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Nombre Destino" SortExpression="DESTINO_NOMBRE" />
                                    <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                <asp:BoundField DataField="TICKET" HeaderText="Nro. Ticket" SortExpression="TICKET" />
                                 </Columns>
                            </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div> 
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="lblTicket" runat="server" CssClass="control-label-2" Text="Datos del Ticket" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-12">
                         <asp:DetailsView id="DetalleTicket" runat="server" AutoGenerateRows="False"  CssClass="modern-detailsview">
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>
                            <PagerStyle HorizontalAlign="Right" BackColor="White" ForeColor="Black"></PagerStyle>
                            <Fields>
                                <asp:BoundField DataField="TICKET" HeaderText="Nro Ticket"></asp:BoundField>
                                <asp:BoundField DataField="REPORTA_FECHA" HeaderText="Fecha"></asp:BoundField>
                                <asp:BoundField DataField="REPORTA_HORA" HeaderText="Hora"></asp:BoundField>
                                <asp:BoundField DataField="CANAL" HeaderText="Canal"></asp:BoundField>
                                <asp:BoundField DataField="Proceso" HeaderText="Proceso" />
                                <asp:BoundField DataField="Tipo_Peticion" HeaderText="Tipo de Petición"></asp:BoundField>
                                <asp:BoundField DataField="Elemento" HeaderText="Elemento"></asp:BoundField>
                                <asp:BoundField DataField="PESTADO" HeaderText="Estado"></asp:BoundField>
                                <asp:BoundField DataField="TICKET_MOTIVO" HeaderText="Motivo"></asp:BoundField>
                                <asp:BoundField DataField="TICKET_DESCRIPCION" HeaderText="Descripción"></asp:BoundField>
                                <asp:BoundField DataField="TICKET_SOLUCION" HeaderText="Solución"></asp:BoundField>
                            </Fields>
                            <HeaderStyle BackColor="#333333" BorderColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White"></EditRowStyle>
                        </asp:DetailsView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="gridSalida" EventName="RowCommand"/>
            </Triggers>
        </asp:UpdatePanel>
    </div> 

</asp:Content>

