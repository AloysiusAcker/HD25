<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Inventario_Ubicacion.aspx.vb" Inherits="Inventario_Ubicacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
<%--    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>--%>
   <%-- <script type="text/javascript">
        $("[src*=plus]").on("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../Icono/minus.gif");
        });
        $("[src*=minus]").on("click", function () {
            $(this).attr("src", "../Icono/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>--%>
    <script type="text/javascript">
        $(document).on("click", "[src*='plus']", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan='999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../Icono/minus.gif");
        });

        $(document).on("click", "[src*='minus']", function () {
            $(this).attr("src", "../Icono/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>
    <style type="text/css">
        /* Estilo personalizado para el CalendarExtender */
        .ajax__calendar_container {
            position: absolute;
            z-index: 1000; /* Puedes ajustar este valor según tus necesidades */
        }
        .custom-calendar .ajax__calendar_container {
            background-color: #f2f2f2; /* Color de fondo del calendario */
            border: 1px solid #ccc; /* Borde del calendario */
        }

        .custom-calendar .ajax__calendar_header {
            background-color: #333; /* Color de fondo del encabezado del calendario */
            color: #fff; /* Color del texto del encabezado del calendario */
        }

        .custom-calendar .ajax__calendar_dayname {
            background-color: #eee; /* Color de fondo de los días de la semana */
            color: #666; /* Color del texto de los días de la semana */
        }

        .custom-calendar .ajax__calendar_day {
            background-color: #fff; /* Color de fondo de los días */
            color: #333; /* Color del texto de los días */
        }

        .custom-calendar .ajax__calendar_hover {
            background-color: #ddd; /* Color de fondo al pasar el mouse por encima de un día */
            color: #333; /* Color del texto al pasar el mouse por encima de un día */
        }

        .custom-calendar .ajax__calendar_active {
            background-color: #007bff; /* Color de fondo de un día seleccionado */
            color: #fff; /* Color del texto de un día seleccionado */
        }

        .custom-calendar .ajax__calendar_other {
            color: #999; /* Color del texto de los días de otros meses */
        }
    </style>

    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12">
                <asp:Label ID="LblDefinicionInventario" runat="server" Text="Definición de Ubicación del Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="Label4" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="ddlBusInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="Label5" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="rbBusAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="rbBusCCosto" runat="server" Text="Centro de Costo" Checked="False" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="rbBusTodos" runat="server" Text="Todos" Checked="false"  AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:TextBox ID="txtBusUbicaCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnBusUbicaion" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtBusUbicaNombre" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:CheckBox ID="CbArticuloCargar" runat="server" Text="Artículo A Cargar" CssClass="checkbox checkbox-inline" AutoPostBack="true" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtCodArticulo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Button ID="BtnBuscarArticulo" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" Enabled="false" />
                    </div>
                    <div class="col-lg-9">
                        <asp:TextBox ID="TxtDescArticulo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-md-9">
                        <asp:Label ID="lblBusUbicaCod" runat="server" Text="" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblBusUbicaCodInv" runat="server" Text="" Visible ="false" ></asp:Label>
                    </div> 
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-3">
                <asp:Button ID="BtnResumen" runat="server" Text="Resumen" ControlStyle-CssClass="form-control btn btn-default" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="BtnIngresaUbic" runat="server" Text="Nueva Ubic." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="Ingresardatos" runat="server" visible ="false"  >
                    <div class="row">
                        <div class="col-lg-9">
                            <asp:Label ID="LblInventario" runat="server" Text="Inventario :" Visible="False" CssClass="control-label-2" />
                            <asp:DropDownList ID="DdlInventario" runat="server" Visible="False" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label1"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                            <asp:Button ID="BtnAgregar" runat="server" Text="Guardar" Visible="False" ControlStyle-CssClass="form-control btn btn-default" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-9">
                            <asp:Label ID="LblResponsable" runat="server" Text="Responsable :" Visible="False" CssClass="control-label-2" />
                            <asp:TextBox ID="TxtResponsable" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label16"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" Visible="False" ControlStyle-CssClass="form-control btn btn-default" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="LblUbicacion" runat="server" Text="" Visible="False" CssClass="control-label-2" />
                            <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Visible="false" Checked="true" />
                            <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" Visible="false" />
                            <asp:RadioButton GroupName="ubicacion" ID="RBUbicaciones" runat="server" Text="Ubicaciones" Visible="false" />
                        </div>
                    </div> 
                    <div class="row">
                        <div class="col-lg-2">
                            <asp:Label ID="LblCodigo" runat="server" Text="Código :" Visible="False" CssClass="control-label-2" />
                            <asp:TextBox ID="TxtCodigo" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-1">
                            <asp:Label ID="Label2"  CssClass="control-label-2" runat="server" Text=".." ForeColor="white"></asp:Label>
                            <asp:Button ID="BtnBusca" runat="server" Visible="False" Text="..." class="form-control btn btn-default" />
                        </div>
                        <div class="col-lg-6">
                            <asp:Label ID="Label3"  CssClass="control-label-2" runat="server" Text="Descripción" ForeColor="white"></asp:Label>
                            <asp:TextBox ID="TxtDescripcion" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label6"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                            <asp:Button ID="BtnPersonal" runat="server" Text="Personal" Visible="False" ControlStyle-CssClass="form-control btn btn-default" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="LlbEtiqFecha" runat="server" Text="Fecha Programada" CssClass="control-label-2"  Visible="False" />
                            <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="LlbEtiqFechaC" runat="server" Text="Fecha Cierre" CssClass="control-label-2"  Visible="False" />
                            <asp:TextBox ID="TxtFechaCierre" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaCierre" Format="dd/MM/yyyy" PopupButtonID="TxtFechaCierre" ></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label8"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                            <asp:Button ID="BtnCostos" runat="server" Text="Costos" Visible="False" ControlStyle-CssClass="form-control btn btn-default" />
                        </div>
                    </div>
                    <div id="CostoTitulo" runat="server" visible="false">
                        <h3>Ingreso de Costos</h3>
                    </div>
                    <div id="Costos" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label9" runat="server" Text="Costo Verificación" CssClass="control-label-2"  Visible="False" />
                                <asp:TextBox ID="TxtCostoVerif" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label10" runat="server" Text="Costo Recojo de llaves" CssClass="control-label-2"  Visible="False" />
                                <asp:TextBox ID="TxtCostoRecojo" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label11" runat="server" Text="Costo Movilidad" CssClass="control-label-2"  Visible="False" />
                                <asp:TextBox ID="TxtCostoMovilidad" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label14"  CssClass="control-label-2" runat="server" Text="Guardar" ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnCostoGuardar" runat="server" Text="Guardar" Visible="False" ControlStyle-CssClass="form-control btn btn-default" />
                            </div>
                        </div>
                    </div>
                    <div id="CostosBoton" runat="server" visible="false" >
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label12" runat="server" Text="Costo Placado" CssClass="control-label-2"  Visible="False" />
                                <asp:TextBox ID="TxtCostoPlacado" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label13" runat="server" Text="Costo x Bien" CssClass="control-label-2"  Visible="False" />
                                <asp:TextBox ID="TxtCostoxBien" runat="server" CssClass="form-control" Text=""  Visible="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label15"  CssClass="control-label-2" runat="server" Text="Cerrar" ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnCostoCerrar" runat="server" Text="Cerrar" Visible="False" ControlStyle-CssClass="form-control btn btn-default" />
                            </div>
                        </div>
                        <br />
                    </div>
                    <div id="Personal" runat="server" visible="false" >
                        <div class="row">
                            <div class="col-lg-5">
                                <asp:Label ID="LblPersonal1" runat="server" Text="Personal :" CssClass="control-label-2" />
                                <asp:DropDownList ID="DdlPersonal" runat="server" CssClass="form-control" AutoPostBack="true" >
                                </asp:DropDownList>
                            </div> 
                            <div class="col-lg-2">
                                <asp:Label ID="LblPersonal2"  CssClass="control-label-2" runat="server" Text=".." ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnPAgregar" runat="server" Text="Agregar"  ControlStyle-CssClass="form-control btn btn-default" />
                            </div> 
                            <div class="col-lg-2">
                                <asp:Label ID="Label7"  CssClass="control-label-2" runat="server" Text=".." ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnPCerrar" runat="server" Text="Cerrar"  ControlStyle-CssClass="form-control btn btn-default" />
                            </div> 
                        </div> 
                        <br />
                        <div class="row">
                            <div class="col-md-9">
                                <asp:GridView ID="GvPersonal" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                    <Columns>
                                        <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                            <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="CODIGO" HeaderText="Codigo" SortExpression="Codigo" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>        
                    </div>     
                    <div class="row">                    
                        <div class="col-lg-12">
                            <asp:TextBox ID="TxtCodUbica" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="TxtCodInventarioUbicacion" runat="server" Visible="False" CssClass="control-label-2" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvListaUbicacion" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnPAgregar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnPersonal" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCostos" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCostoCerrar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCostoGuardar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>        
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div>                    
                    <div class="col-md-3">
                        <asp:Button ID="BtnCerrarResumen" runat="server" Text="Cerrar Resumen" ControlStyle-CssClass="form-control btn btn-default" visible="false"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="Estado_Cod" CssClass="table table-bordered GridView"
                            OnRowDataBound="OnRowDataBound" visible="False">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <img alt="" style="cursor: pointer" src="../Icono/plus.gif" />
                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                            <asp:GridView ID="gv_Child" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered GridView">
                                                <Columns>                                                       
                                                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" >  
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                                    </asp:BoundField>                      
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="Estado_Cod" HeaderText="" FooterStyle-ForeColor ="White" >   
                                    <FooterStyle ForeColor="White" />
                                    <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" >  
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnResumen" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCerrarResumen" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>        
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistro"  CssClass="control-label-2" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                    </div>
                </div> 
                <div class="row">
                    <div class="col-lg-12">
                        <asp:GridView ID="GvListaUbicacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                            <Columns>
                                <asp:ButtonField CommandName="EliminaInventario" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="DetalleInventario" ButtonType="Image" ImageUrl="~/icono/details_opt.png">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="CargarInventario" ButtonType="Image" ImageUrl="~/icono/upload.png">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="INVENTUBIC_NRO" HeaderText="Inventario" SortExpression="INVENTUBIC_NRO" />
                                <asp:BoundField DataField="INVENT_DESCRIPCION" HeaderText="Descrpción" SortExpression="INVENT_DESCRIPCION" />
                                <asp:BoundField DataField="UBIC_TIPO" HeaderText="Tipo" SortExpression="UBIC_TIPO" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="ELEMEN_VALOR" HeaderText="Estado" SortExpression="ELEMEN_VALOR" />
                                <asp:BoundField DataField="INVENTUBIC_RESPONSABLE" HeaderText="Responsable" SortExpression="INVENTUBIC_RESPONSABLE" />
                                <asp:BoundField DataField="INVENTUBIC_CODIGO" SortExpression="INVENTUBIC_CODIGO">
                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="INVENTUBIC_UBIC_CODIGO" SortExpression="INVENTUBIC_UBIC_CODIGO">
                                    <ItemStyle ForeColor="White" Width="0.1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cant_bienes" HeaderText="Cant. Bienes" SortExpression="cant_bienes" />
                                <asp:BoundField DataField="Fecha_Programacion" HeaderText="F. Programación" SortExpression="Fecha_Programacion" />
                                <asp:BoundField DataField="Fecha_Cierre" HeaderText="F. Cierre" SortExpression="Fecha_Cierre" />
          
                                <asp:ButtonField CommandName="Cerrar" Text="Cerrar" ButtonType="Button" HeaderText="Cierre Inventario" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </div>         
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistroDetalle"  CssClass="control-label-2" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                    </div>
                </div> 
                <div class="row">
                    <div class="col-lg-12">
                        <asp:GridView ID="GvListaDetalleInventario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="ESTADO_SERIE" HeaderText="Estado" SortExpression="ESTADO_SERIE" />
                                <asp:BoundField DataField="ESTADO_INGRESO" HeaderText="Est. Ingreso" SortExpression="ESTADO_INGRESO" />
                                <asp:BoundField DataField="ESTADO_EQUIPO" HeaderText="Est. Equipo" SortExpression="ESTADO_EQUIPO" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("ART_CODIGO") IsNot DBNull.Value, Eval("ART_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>                        
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvListaUbicacion" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnResumen" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

  <%--   <div id="ModalPersonal" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpPersonalModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                            <asp:Label runat="server" ID="LblPersonal3" Text="Personal para el Inventario" />
                        </div>
                        <div class="form-horizontal">
                            <div class="modal-body" style="padding: 20px 10px 0;">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <asp:Label ID="LblPersonal1" runat="server" Text="Personal :" CssClass="control-label-2" />
                                                <asp:DropDownList ID="DdlPersonal" runat="server" CssClass="form-control" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div> 
                                            <div class="col-md-3">
                                                <asp:Label ID="LblPersonal2" runat="server" Text="Per" CssClass="control-label" ForeColor="White"  />
                                                <asp:Button ID="BtnPAgregar" runat="server" Text="Agregar" CssClass="btn btn-default" />
                                            </div> 
                                        </div>   
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-default" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="BtnPCerrar" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                            </div>
                                        </div>                                          
                                        <asp:UpdatePanel ID="UpPersonal" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="GvPersonal" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:BoundField DataField="CODIGO" HeaderText="Codigo" SortExpression="Codigo" />
                                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnPAgregar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>               
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvListaUbicacion" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnPCerrar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnPAgregar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>--%>


    <div id="ModalPregunta" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                            <asp:Label runat="server" ID="TituloPregunta" Text="¿Desea actualizar los equipos del inventario?" />
                        </div>
                        <div class="form-horizontal">
                            <div class="modal-body" style="padding: 20px 10px 0;">
                                <div class="panel-group">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div class="form-group">
                                                <div class="col-sm-8 col-xs-2 col-lg-offset-3" style="padding-left: 12%">
                                                    <asp:Button ID="BtnSi" CssClass="btn btn-info" runat="server" Text="Sí" />
                                                    <asp:Button ID="BtnNo" ControlStyle-CssClass="btn btn-info" runat="server" Text="No" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvListaUbicacion" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnSi" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnNo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="ModalBuscaArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="TituloBuscarArticulos" Text="Búsqueda de Artículos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_codArt">Codigo Art :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtCodArticuloBA" type="text" runat="server" />
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_clasificacionBA">Clasificacíon :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtClasificacionBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaClasificacionBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodClasificacionBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_descripcionBA">Descripción :</label>
                                                <div class="col-sm-8 col-xs-5">
                                                    <input class="form-control" id="TxtDescripcionBA" name="Descripcion" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_tipoArticuloBA">Tipo de Art :</label>
                                                <div class="col-sm-3 col-xs-7 selectContainer">
                                                    <asp:DropDownList ID="DdlTipoBA" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">Número Parte :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtNumParteBA" type="text" runat="server" />
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_codEspecificoBA">Cod. Especif :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtCodEspecificoBA" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_marcaBA">Marca :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtMarcaBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaMarcaBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                                <label class="control-label col-sm-2 col-xs-12" for="id_modeloBA">Modelo :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row form-group col-md-12">
                                                        <div class="col-lg-12">
                                                            <asp:GridView ID="GvBuscarArticulos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                                <Columns>
                                                                    <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/images/ok.png">
                                                                        <ItemStyle Height="10px" Width="10px" />
                                                                    </asp:ButtonField>
                                                                    <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                                    <asp:BoundField DataField="ART_CLASIFICACION" HeaderText="Clasificación" SortExpression="ART_CLASIFICACION" />
                                                                    <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                                        <ItemStyle ForeColor="White" Width="1px" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="SelectedNodeChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
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

    <div id="ModalClasificacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="TituloClasificacion" Text="Clasificación" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-6 col-lg-offset-4">
                                            <asp:Button ID="BtnBuscaClasificacion" class="btn btn-primary" runat="server" Text="Buscar" />
                                            <asp:Button ID="BtnCerrarClasificacion" class="btn btn-primary" runat="server" Text="Cerrar" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="TrvClasificacion" runat="server" ShowExpandCollapse="true" ShowLines="True"
                                                PopulateNodesFromClient="true" ExpandDepth="0">
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <Nodes>
                                                </Nodes>
                                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                                            </asp:TreeView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaClasificacion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="TreeNodePopulate" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
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

    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBusca" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarcaBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaModeloBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnBusUbicaion" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="BuscarDescripcion">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="BuscarCodigo">Código :</label>
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
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnBusUbicaion" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row col-md-12">
                                        <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="CodInterno" HeaderText="Codigo" SortExpression="CodInterno" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="0.1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CodUbi" SortExpression="CodUbi">
                                                            <ItemStyle ForeColor="White" Width="0.1px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="GvBusquedaM" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/images/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="codigoInt" HeaderText="Codigo" SortExpression="codigoInt" />
                                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                                                        <asp:BoundField DataField="codigo" SortExpression="codigo">
                                                            <ItemStyle ForeColor="White" Width="0.1px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnBusUbicaion" EventName="Click" />
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
