<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_SalidasEnv_xRecibir.aspx.vb" Inherits="Inventario_Inventario_SalidasEnv_xRecibir" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Inventario - Salidas Enviadas" CssClass="Titulos"></asp:Label>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <cc1:TabContainer ID="ficha" runat="server" ActiveTabIndex="1" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                    <cc1:TabPanel runat="server" HeaderText="Salidas x Recibir" ID="TabPanel1">
                        <ContentTemplate>                            
                            <div class="row">
                                <div class="col-md-9">
                                    <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
                                </div> 
                            </div>                      
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="LblEtiq1" CssClass="control-label-2" runat="server" Text="Guia Serie"></asp:Label>
                                    <asp:TextBox ID="TxtGuiaSerie" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-2">
                                            <asp:Label ID="LblEtiq2" CssClass="control-label-2" runat="server" Text="Guia Numero"></asp:Label>
                                            <asp:TextBox ID="TxtGuiaNumero" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>   
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="col-md-6">
                                </div>  
                                <div class="col-md-2">
                                    <asp:Label ID="LblEtiq4"  CssClass="control-label-2" runat="server" Text="Listar" ForeColor="White"></asp:Label>
                                    <asp:Button ID="BtnListarSalidas" runat="server" Text="Listar" CssClass="form-control btn btn-default"/>
                                </div> 
                            </div> 
                            <div class="row">
                            </div> 
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="LblEtiq5" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                                    <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" Enabled="True" ></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="LblEtiq6" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                                    <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" Enabled="True" ></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="LblEtiq8" CssClass="control-label-2" runat="server" Text="Remitente:"></asp:Label>
                                    <asp:DropDownList ID="DdlRemitente" runat="server" CssClass="form-control" >
                                        <asp:ListItem Text="Almacén" Value="1" Selected="True" />
                                        <asp:ListItem Text="Sessión CC" Value="2" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="LblEtiq9"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                                    <asp:TextBox ID="TxtRemCodigo" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="LblEtiq10"  CssClass="control-label-2" runat="server" Text="..." ForeColor="White"></asp:Label>
                                    <asp:Button ID="BtnRemitente" runat="server"  CssClass="form-control btn btn-default"  Text="..." />
                                </div> 
                                <div class="col-md-7">
                                    <asp:Label ID="LblEtiq11"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                                    <asp:TextBox ID="txtRemDescripcion" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>                                       
                            <div class="row espacio"> 
                                <div class="col-md-4">
                                    <asp:Label ID="LblEtiq12" runat="server" Text="Lista de Salidas" CssClass="control-label-2"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate> 
                                        <asp:GridView ID="gridSalida" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                            <Columns>
                                                <asp:ButtonField CommandName="Ingreso" Text="Ingresar Series" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                </asp:ButtonField>
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
                                                <asp:TemplateField ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"GuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("GUIREM_CODIGO") IsNot DBNull.Value, Eval("GUIREM_CODIGO"), Nothing))) %>' Width="100" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nro_Guia" HeaderText="Guía" SortExpression="Nro_Guia" />
                                                <asp:BoundField DataField="Codigo_Destino" HeaderText="" />
                                                <asp:BoundField DataField="CODORIGEN" HeaderText="" />
                                                <asp:BoundField DataField="motivo_codigo" HeaderText="" />
                                                <asp:BoundField DataField="Salida" HeaderText="" />
                                             </Columns>
                                        </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnListarSalidas" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>                       
                            <div class="row espacio">
                                <div class="col-md-12">
                                    <asp:Label ID="LblCodOrigen" runat="server" Text="" CssClass="control-label-2" ForeColor="white" />
                                </div> 
                                <div class="col-md-12">
                                    <asp:Label ID="LblCodDestino" runat="server" Text="" CssClass="control-label-2" ForeColor="white" />
                                </div> 
                                <div class="col-md-12">
                                    <asp:Label ID="LblCodMotivo" runat="server" Text="" CssClass="control-label-2" ForeColor="white" />
                                </div> 
                            </div> 
                        </ContentTemplate>                            
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server"  HeaderText="Recibir Salida e ingresar series" ID="TabPanel2">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>                         
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                            <asp:Label ID="LblErrort" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                                        </div> 
                                    </div>   
                                    <div class="row espacio">
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_5" CssClass="control-label-2" runat="server" Text="Nro. Salida"></asp:Label>
                                            <asp:TextBox ID="txtIngSalida" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-5">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="LblEtiq3"  CssClass="control-label-2" runat="server" Text="Ejecutar" ForeColor="White"></asp:Label>
                                            <asp:Button ID="BtnEjecutar" runat="server" Text="Ejecutar" CssClass="form-control btn btn-default"/>
                                        </div> 
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_6" CssClass="control-label-2" runat="server" Text="..." ForeColor ="White" ></asp:Label>
                                            <asp:Button ID="btnRegresar" Text="Regresar" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                                        </div>
                                    </div>   
                                    <div class="row espacio">
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_13" CssClass="control-label-2" runat="server" Text="Origen"></asp:Label>
                                            <asp:TextBox ID="TxtOrigenTipo" runat="server" CssClass="form-control" Text="Almacén"></asp:TextBox>
                                        </div>   
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_7" CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                                            <asp:TextBox ID="TxtOrigenCodigo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>   
                                        <div class="col-md-7 col-xs-6">
                                            <asp:Label ID="LblEtiq_8" CssClass="control-label-2" runat="server" Text="Descrpción"></asp:Label>
                                            <asp:TextBox ID="txtOrigenDescrip" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                    </div>   
                                    <div class="row espacio">
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_14" CssClass="control-label-2" runat="server" Text="Destino"></asp:Label>
                                            <asp:TextBox ID="TxtDestinoTipo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>   
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_9" CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                                            <asp:TextBox ID="TxtDestinoCodigo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>   
                                        <div class="col-md-7 col-xs-6">
                                            <asp:Label ID="LblEtiq_10" CssClass="control-label-2" runat="server" Text="Descrpción"></asp:Label>
                                            <asp:TextBox ID="TxtDestinoDescripcion" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                    </div>     
                                    <div class="row espacio" runat="server" id="Exportar" >
                                        <div class="col-md-7 col-xs-6">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass ="form-control" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnCargarPlacas" />
                                                    <asp:PostBackTrigger ControlID="BtnCargarSeries" />
                                                </Triggers>
                                            </asp:UpdatePanel> 
                                        </div> 
                                        <div class="col-lg-2">
                                            <asp:Button ID="BtnCargarPlacas" Text="Cargar Placas" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button ID="BtnCargarSeries" Text="Cargar Series" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                        </div>
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-3 col-xs-6">
                                            <asp:Label ID="LblNroPlaca" runat="server" class="control-label-2" Text="Nro. Placa :" ></asp:Label>
                                            <asp:TextBox ID="TxtNroPlaca" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3 col-xs-6">
                                            <asp:Label ID="LblNroSerie" runat="server" class="control-label-2" Text="Nro. Serie :" ></asp:Label>
                                            <asp:TextBox ID="TxtNroSerie" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>                                  
                                    <div class="row espacio"> 
                                        <div class="col-md-4">
                                            <asp:Label ID="LblEtiq_11" runat="server" Text="Lista Cantidad de Bienes" CssClass="control-label-2"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row espacio"> 
                                        <div class="col-md-9">
                                            <asp:GridView ID="gvCantidadesBienes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >                            
                                                <Columns>
                                                    <asp:BoundField DataField="DESPD_ITEM" HeaderText="Nro. Item"></asp:BoundField>
                                                    <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Art. Codigo"></asp:BoundField>
                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte"></asp:BoundField>
                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción"></asp:BoundField>
                                                    <asp:BoundField DataField="CANT" HeaderText="Cant. Despachada"></asp:BoundField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                                            </asp:GridView>                                
                                        </div>
                                    </div>                              
                                    <div class="row espacio"> 
                                        <div class="col-md-4">
                                            <asp:Label ID="LblEtiq_24" runat="server" Text="Lista de Bienes" CssClass="control-label-2"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row espacio"> 
                                        <div class="col-md-9">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate> 
                                                <asp:GridView ID="GvSalidaBienes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >                            
                                                    <Columns>
                                                        <asp:BoundField DataField="DESPD_ITEM" HeaderText="Nro. Item"></asp:BoundField>
                                                        <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Art. Codigo"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción"></asp:BoundField>
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie Nro."></asp:BoundField>
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro."></asp:BoundField>
                                                        <asp:BoundField DataField="Recibido" HeaderText="Recibido"></asp:BoundField>
                                                        <asp:BoundField DataField="SERIE_NUMERAR" HeaderText=""></asp:BoundField>
                                                        <asp:BoundField DataField="Ruc" HeaderText=""></asp:BoundField>
                                                        <asp:BoundField DataField="Oficina" HeaderText=""></asp:BoundField>
                                                        <asp:BoundField DataField="Ubicact_tipo" HeaderText=""></asp:BoundField>
                                                        <asp:BoundField DataField="Ubicact_codigo" HeaderText=""></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>     
                                                </ContentTemplate>
                                                <Triggers>                                    
                                                    <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                                                </Triggers>         
                                            </asp:UpdatePanel>                   
                                        </div>
                                    </div>                                    
                                    <div class="row espacio"> 
                                        <div class="col-md-4">
                                            <asp:Label ID="LblEtiq_12" runat="server" Text="Lista de Accesorios" CssClass="control-label-2"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row espacio"> 
                                        <div class="col-md-3">
                                            <asp:CheckBox ID="chkRecibirAcc" CssClass="checkbox checkbox-inline" Text="Recibir Todo" Font-Bold ="true" runat="server" AutoPostBack="True" Visible ="false"  />
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                       <%-- <div class="col-md-2">
                                            <asp:Button ID="btnGuardarAccCant" Text="Guardar Cantidades" runat="server" ControlStyle-CssClass="form-control btn btn-default" Visible ="false" ></asp:Button>                                
                                        </div>--%>
                                    </div>
                                    <div class="row espacio"> 
                                        <div class="col-md-9">
                                            <asp:GridView ID="GvSalidaAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >                            
                                                <Columns>
                                                    <asp:BoundField DataField="DESPD_ITEM" HeaderText="Nro. Item"></asp:BoundField>
                                                    <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Art. Codigo"></asp:BoundField>
                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte"></asp:BoundField>
                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción"></asp:BoundField>
                                                    <asp:BoundField DataField="DESPD_CANT_DESP" HeaderText="Cant. Despachada"></asp:BoundField>
                                                    <asp:BoundField DataField="DESPD_CANT_REC" HeaderText="Cant. Recibida"></asp:BoundField>
                                                    <asp:BoundField DataField="DESPD_CANT_FALT_REC" HeaderText="Falta Recibir"></asp:BoundField>
                                                    <asp:BoundField DataField="" HeaderText="Recibido"></asp:BoundField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                                            </asp:GridView>                                
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>                                    
                                    <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel> 
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ficha" EventName="ActiveTabChanged" />
            </Triggers>
        </asp:UpdatePanel>     
        


    </div> 



</asp:Content>


