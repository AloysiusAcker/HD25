<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Monitoreo.aspx.vb" Inherits="Inventario_Inventario_Monitoreo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

           <style>
                .rotatedHeaderCell {
                    writing-mode: vertical-rl;
                    transform: rotate(180deg);
                    /*width: 30px; /* Ancho de la cabecera girada */*/
                    text-align: match-parent; /* Alineación del texto */
                    white-space: nowrap; /* Evitar que el texto se ajuste */
                }

                .styled-checkboxlist {
                    display: block;
                    border: 1px solid #ccc;
                    width: 100%;
                    height: 150px;
                    overflow-y: scroll;
                    padding: 5px;      
                    font-family: Roboto, arial, sans-serif;
                    font-size:11px;            
                }
                .styled-checkboxlist label {
                    display: flex;
                    align-items: center;
                    margin: 0;
                    cursor: pointer;
                }
                .styled-checkboxlist input[type="checkbox"] {
                    margin-right: 10px;
                }

            </style>

    <script type="text/javascript">
    function updateClock() {
        var now = new Date();
        var hours = now.getHours();
        var minutes = now.getMinutes();
        var seconds = now.getSeconds();
        minutes = minutes < 10 ? '0' + minutes : minutes;
        seconds = seconds < 10 ? '0' + seconds : seconds;
        var timeString = hours + ':' + minutes + ':' + seconds;
        document.getElementById('<%= lblCurrentTime.ClientID %>').innerText = timeString;
    }

    setInterval(updateClock, 1000); // Update every second
    window.onload = updateClock; // Initialize clock on page load
</script>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Monitoreo" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row espacio">
            <div class="col-md-3 col-xs-6">
                <%--<asp:Button ID="BtnExportar2" runat="server" Text="Exportar " visible="false"  ControlStyle-CssClass="form-control btn btn-default" />--%>
            </div> 
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar"  ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3 col-xs-6">
                <asp:Button ID="BtnExportar2" runat="server" Text="Exportar Grilla" visible="false"  controlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-2 col-xs-6">
            </div> 
            <div class="col-md-1 col-xs-6">
                <asp:label ID="lblCurrentTime" runat="server" BorderStyle="Groove" Width="65"></asp:label>
            </div> 
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
               
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                        <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                        <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label2" runat="server" Text="Estado :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlEstado" runat="server" CssClass="form-control" AutoPostBack="true"  >
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-xs-6">
                        <asp:Label ID="Label11" runat="server" class="control-label-2" Text="Listar" forecolor="White" ></asp:Label>
                        <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2"  />
                        <asp:CheckBoxList ID="LstInventario" runat="server" CssClass="styled-checkboxlist"></asp:CheckBoxList>
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true" visible="false"   >
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" AutoPostBack="True"  />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBTodos" runat="server" Text="Todos" AutoPostBack="True" Checked="true"  />
                    </div>
                </div>
                <div class="row" >
                    <div class="col-md-2 col-xs-12">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-xs-11">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6 col-xs-12">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="row">                    
                    <div class="col-md-12">
                        <asp:TextBox ID="TxtUbicaCodigo" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TxtUbicaCodigoInv" runat="server" Width="102px" Visible="false"></asp:TextBox>
                    </div> 
                </div>              
                <div class="row espacio">                    
                    <div class="col-md-12">
                        <asp:Label ID="Label3" runat="server" class="control-label-2" Text="Cantidad de Oficinas x Estado" ></asp:Label>
                    </div> 
                </div>   
                <div class="row espacio">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="gvResumen" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="Ubi_Total" HeaderText="Cant.Total" SortExpression="Ubi_Total" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="av_total" HeaderText="% Total" SortExpression="av_total" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Ubi_Generado" HeaderText="Cant. Generados" SortExpression="Ubi_Generado" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="av_generado" HeaderText="% Generados" SortExpression="av_generado" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Ubi_Proceso" HeaderText="Cant. En Proceso" SortExpression="Ubi_Proceso" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="av_proceso" HeaderText="% En Proceso" SortExpression="av_proceso" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ubi_Cerrado" HeaderText="Cant.Cerrado" SortExpression="Ubi_Cerrado" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="av_cerrado" HeaderText="% Cerrado" SortExpression="av_cerrado" >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"  />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>                 
                <div class="row espacio">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro2" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>   
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvResumenCostos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                          <%--  <HeaderStyle CssClass="rotatedHeader" />--%>
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="No Acceso"/>
                                <asp:BoundField DataField="Cod_Ubicacion" HeaderText="Oficina" SortExpression="Cod_Ubicacion" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Descripción" SortExpression="Ubicacion" >
                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                <ItemStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Estado" SortExpression="ELEMEN_VALOR" >
                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                <ItemStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha_Programa" HeaderText="Fecha" SortExpression="Fecha_Programa" />
                                <asp:BoundField DataField="hora_inicio" HeaderText="Hora" SortExpression="hora_inicio" />
                                <asp:BoundField DataField="Avance" HeaderText="Avance" >
                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total General">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Cant. Bienes</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("CantBien") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariados">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariados</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Total") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariado OK">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariado Ok</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_1") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Encontrado en otro lugar" >
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell" >Encontrado en otro lugar</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_3") %>
                                    </ItemTemplate>
                                    <ControlStyle Font-Bold="False" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nuevo Bien">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Nuevo Bien</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_7") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Encontrado en otro lugar">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Encontrado en otro lugar placado</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_8") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariado OK">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariado Ok placado</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_9") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Por placar (Encontrado por serie)">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Por placar (Encontrado por serie)</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_5") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PLACADOS">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Placados</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("placado") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="personal programado">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">personal programado</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("personal_programado") %>
                                    </ItemTemplate> 
                                </asp:TemplateField>
                                <asp:BoundField DataField="Informe" HeaderText="Informe"/>
                                <asp:BoundField DataField="GPS" HeaderText="GPS"/>
                                <asp:BoundField DataField="INVENTUBIC_CODIGO" HeaderText="" SortExpression="INVENTUBIC_CODIGO" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:Timer ID="Timer1" runat="server" Interval="100000" OnTick="Timer1_Tick"></asp:Timer>
                    </div>
                </div>  
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="gvListaxUsuario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                          <%--  <HeaderStyle CssClass="rotatedHeader" />--%>
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="No Acceso"/>
                                <asp:BoundField DataField="Cod_Ubicacion" HeaderText="Oficina" SortExpression="Cod_Ubicacion" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Descripción" SortExpression="Ubicacion" >
                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                <ItemStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Estado" SortExpression="ELEMEN_VALOR" >
                                <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                <ItemStyle Font-Bold="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha_Programa" HeaderText="Fecha" SortExpression="Fecha_Programa" />
                                <asp:BoundField DataField="hora_inicio" HeaderText="Hora" SortExpression="hora_inicio" />
                                <asp:BoundField DataField="Avance" HeaderText="Avance" >
                                    <HeaderStyle Font-Bold="True" ForeColor="Black" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Font-Bold="True" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total General">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Cant. Bienes</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("CantBien") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariados">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariados</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Total") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inventariado OK">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Inventariado Ok</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Inventario_Ok_Todos") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nuevo Bien">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Nuevo Bien</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("Estado_7") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PLACADOS">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">Placados</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("placado") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="personal programado">
                                    <HeaderTemplate>
                                        <div class="rotatedHeaderCell">personal programado</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Eval("personal_programado") %>
                                    </ItemTemplate> 
                                </asp:TemplateField>
                                <asp:BoundField DataField="Informe" HeaderText="Informe"/>
                                <asp:BoundField DataField="GPS" HeaderText="GPS"/>
                                <asp:BoundField DataField="INVENTUBIC_CODIGO" HeaderText="" SortExpression="INVENTUBIC_CODIGO" >
                                    <HeaderStyle BorderColor="White" />
                                    <ItemStyle ForeColor="White" Width="0.1px" BorderColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>  
            </ContentTemplate>
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </div> 

    <div id="ModalSinAcceso" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label12" runat="server" Text="No acceso a ubicaciones" CssClass="Titulos"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label14" runat="server" Text="..." CssClass="control-label" ForeColor="white"></asp:Label>
                                    <asp:Button ID="BtnSinAcceso_Cerrar" runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Cerrar" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="GvSinAcceso" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                        <Columns>
                                            <asp:BoundField DataField="INVUBICA_CORRELATIVO" HeaderText="Nro. " SortExpression="INVUBICA_CORRELATIVO" />
                                            <asp:BoundField DataField="INVUBICA_DESCRIPCION" HeaderText="Detalle de la Ubicación" SortExpression="INVUBICA_DESCRIPCION" />
                                            <asp:BoundField DataField="INVUBICA_CODIGO" HeaderText="" SortExpression="INVUBICA_CODIGO" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>                                
                            <asp:AsyncPostBackTrigger ControlID="GvResumenCostos" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSinAcceso_Cerrar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

     <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBusca" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                <div class="col-sm-3 col-xs-5">
                                                    <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row col-md-12">
                                        <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="CodInterno" HeaderText="Codigo" SortExpression="CodInterno" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodUbi" SortExpression="CodUbi">
                                                            <ItemStyle ForeColor="White" Width="0.1px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

