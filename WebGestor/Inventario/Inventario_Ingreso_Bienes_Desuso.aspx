<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Ingreso_Bienes_Desuso.aspx.vb" Inherits="Inventario_Inventario_Ingreso_Bienes_Desuso" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- <script type="text/javascript">
        function desactivarBoton() {
            // Desactivar el botón usando ClientID para obtener el ID correcto
            document.getElementById('<%= btnProcesar.ClientID %>').disabled = true;

            // Mostrar el spinner
            document.getElementById('spinner').style.display = 'block';
        }
    </script>


    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <!-- Botón que procesa la solicitud -->
                <asp:Button ID="btnProcesar" runat="server" Text="Procesar" 
                            OnClick="btnProcesar_Click" 
                            OnClientClick="desactivarBoton();" />

                <!-- Indicador de carga, inicialmente oculto -->
                <div id="spinner" style="display:none;">
                    <img src="spinner.gif" alt="Cargando..." />
                    Procesando, por favor espere...
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>

    <div class="container-fluid">
        <h1 class="Titulos">Ingreso de Bienes</h1>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="row espacio">
                <div class="col-md-2">
                    <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Serie Nro"></asp:Label>
                    <asp:TextBox ID="txtNroSerie" runat="server" CssClass="form-control"  ></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="Placa Nro"></asp:Label>
                    <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label4" CssClass="control-label-2" runat="server" Text="..." ForeColor="White"></asp:Label>
                    <asp:Button ID="BtnListar" runat="server" Text="Listar" CssClass="form-control btn btn-default" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label5" CssClass="control-label-2" runat="server" Text="..." ForeColor="White"></asp:Label>
                    <asp:Button ID="BtnGenerar" runat="server" Text="Generar Recepción" CssClass="form-control btn btn-default" />
                </div>
            </div>      
            <div class="row">
                <div class="col-md-6 col-xs-6">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass ="form-control" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnCargarPlacas" />
                            <asp:PostBackTrigger ControlID="BtnCargarSeries" />
                            <asp:PostBackTrigger ControlID="BtnCargaExcel" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div> 
                <div class="col-lg-2">
                    <asp:Button ID="BtnCargarPlacas" Text="Cargar Txt Placas" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                </div>
                <div class="col-lg-2">
                    <asp:Button ID="BtnCargarSeries" Text="Cargar Txt Series" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                </div>
                <div class="col-md-2">
                    <%--<asp:Label ID="Label11" runat="server" Text="Limpiar" CssClass="control-label-2" ForeColor="White"  />--%>
                    <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" CssClass="form-control btn btn-default" />
                </div>
            </div>
            <div class="row espacio" runat="server" id="Exportar" >
                <div class="col-md-2">
                    <asp:Label ID="Label8" CssClass="control-label-2" runat="server" Text="Nro. Col. Serie"></asp:Label>
                    <asp:TextBox ID="TxtColSerie" runat="server" CssClass="form-control"  ></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label9" CssClass="control-label-2" runat="server" Text="Nro. Col. Placa"></asp:Label>
                    <asp:TextBox ID="TxtcolPlaca" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label11" CssClass="control-label-2" runat="server" Text="Nro. Col. Sku"></asp:Label>
                    <asp:TextBox ID="TxtColSku" runat="server" CssClass="form-control"  ></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Lbl4" CssClass="control-label-2" runat="server" Text="Fila empieza"></asp:Label>
                    <asp:TextBox ID="TxtIni" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Lbl5" CssClass="control-label-2" runat="server" Text="Fila termina"></asp:Label>
                    <asp:TextBox ID="Txtfin" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label10" runat="server" Text="Carga" CssClass="control-label-2" ForeColor="White"  />
                    <asp:Button ID="BtnCargaExcel" runat="server" Text="Cargar Excel" CssClass="form-control btn btn-default" />
                </div>
            </div> 
            <div class="row espacio" runat="server" id="Div1" >
                <div class="col-md-2">
                    <asp:Label ID="Label12" CssClass="control-label-2" runat="server" Text="Nro. Col. Orden Compra"></asp:Label>
                    <asp:TextBox ID="TxtColOC" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label15" CssClass="control-label-2" runat="server" Text="Nro. Col. Guia"></asp:Label>
                    <asp:TextBox ID="TxtColGuia" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label13" CssClass="control-label-2" runat="server" Text="Nro. Col. Art. Refrencia"></asp:Label>
                    <asp:TextBox ID="TxtColRef" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Nro. Col. Cant."></asp:Label>
                    <asp:TextBox ID="TxtColCant" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label30" CssClass="control-label-2" runat="server" Text="Nro. Col. Fecha"></asp:Label>
                    <asp:TextBox ID="TxtColFecha" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label31" CssClass="control-label-2" runat="server" Text="Nro. Col. CC"></asp:Label>
                    <asp:TextBox ID="TxtColCC" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div> 
            <div class="row espacio">    
                <div class="col-md-2">
                    <asp:Label ID="Label22" CssClass="control-label-2" runat="server" Text="Cant. Ingresar"></asp:Label>
                    <asp:TextBox ID="TxtCantIng" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label23" runat="server" Text="Carga" CssClass="control-label-2" ForeColor="White"  />
                    <asp:Button ID="BtnIngCant" runat="server" Text="Ingresar Cantidad" CssClass="form-control btn btn-default" />
                </div>
            </div> 
            <h4>Datos de la Recepción y/o Despacho</h4>
            <div class="row espacio">    
                <div class="col-md-3">
                    <asp:Label ID="Label28" runat="server" Text="Tipo de Servicio :" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlServicio" runat="server" AutoPostBack="True" class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha Recepción"></asp:Label>
                    <asp:TextBox ID="txtFechaRecep" runat="server" CssClass="form-control"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaRecep" Format="dd/MM/yyyy" PopupButtonID="txtFechaRecep" ></cc1:CalendarExtender>
                </div>   
                <div class="col-md-2">
                    <asp:Label ID="LblEtiq_3" CssClass="control-label-2" runat="server" Text="Fecha Registro"></asp:Label>
                    <asp:TextBox ID="txtFecRegistra" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                </div> 
                <div class="col-md-2">
                    <asp:Label ID="Label24" CssClass="control-label-2" runat="server" Text="Hora Registro"></asp:Label>
                    <asp:TextBox ID="txtHoraRegistra" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                </div>       
            </div>
            <div id="divOrigen" runat="server" visible ="false" >
                <div class ="row espacio">
                    <div class="col-md-9 col-xs-6">
                        <asp:Label ID="Label29" runat="server" Text="Origen :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="UbiOrigen" ID="RbOrigAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" Enabled ="false"  />
                        <asp:RadioButton GroupName="UbiOrigen" ID="RbOrigCC" runat="server" Text="Centro de Costo" AutoPostBack="True" Enabled ="false" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-2">
                        <asp:TextBox ID="TxtOrigCodInt" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnBusOrigen" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default"  Enabled ="false"/>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="TxtOrigDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row espacio">
                <div class="col-md-9 col-xs-6">
                    <asp:Label ID="LblUbicacion" runat="server" Text="Destino :" CssClass="control-label-2" />
                    <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                    <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                </div>
            </div>
            <div class="row espacio">
                <div class="col-md-2">
                    <asp:TextBox ID="TxtDesCodExterno" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="BtnBuscar" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                </div>
                <div class="col-md-7">
                    <asp:TextBox ID="TxtDesDescrip" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row espacio">
                <asp:TextBox ID="TxtDesCodigo" runat="server" Enabled="False" CssClass="form-control" Visible ="false" ></asp:TextBox>
                <asp:TextBox ID="TxtOriCodigo" runat="server" Enabled="False" CssClass="form-control" Visible ="false" ></asp:TextBox>
            </div>
            <div class="row espacio">
                <div class="col-md-4">
                    <asp:Label ID="lblMotivo" runat="server" Text="Motivo :" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlMotivo" runat="server" AutoPostBack="True" class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Condición :" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlEstado" runat="server" AutoPostBack="True" class="form-control">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label32" runat="server" Text="Operativa :" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlOperativa" runat="server" AutoPostBack="True" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row espacio"  id="id_GuiaNumero"  runat="server" visible="false" >
                <div class="col-md-2">
                    <asp:Label ID="LblEtiq6" CssClass="control-label-2" runat="server" Text="Guia Serie"></asp:Label>
                    <asp:TextBox ID="TxtGuiaSerie" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="LblEtiq7" CssClass="control-label-2" runat="server" Text="Guia Numero"></asp:Label>
                    <asp:TextBox ID="TxtGuiaNumero" runat="server" CssClass="form-control"></asp:TextBox>
                </div>    
            </div>
            <div class="row espacio"   id="id_GuiaRecep"  runat="server" visible="false"  >
                <div class="col-md-2">
                    <asp:Label ID="Label6" runat="server" Text="Tipo Documento:" CssClass="control-label-2" />
                    <asp:DropDownList ID="cboTipoDoc" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div> 
                <div class="col-md-2">
                    <asp:Label ID="Label7" CssClass="control-label-2" runat="server" Text="Serie Documento"></asp:Label>
                    <asp:TextBox ID="txtSerieDoc" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                </div> 
                <div class="col-md-4">
                    <asp:Label ID="Label25" CssClass="control-label-2" runat="server" Text="Nro. Documento"></asp:Label>
                    <asp:TextBox ID="txtNroDoc" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                </div> 
                <div class="col-md-2">
                    <asp:Label ID="Label16" CssClass="control-label-2" runat="server" Text="Nro. Orden Compra"></asp:Label>
                    <asp:TextBox ID="txtNroOC" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                </div> 
            </div>
            <div class="row espacio"   id="id_Proveedor"  runat="server" visible="false"  >
                <div class="col-md-2">
                    <asp:Label ID="Label17" CssClass="control-label-2" runat="server" Text="Proveedor"></asp:Label>
                    <asp:TextBox ID="txtProvRuc" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label18" runat="server" class="control-label-2" Text="..." forecolor="White" ></asp:Label>
                    <asp:Button ID="btnBus" runat="server" Text="..." CssClass="form-control btn btn-default" />
                </div>
                <div class="col-md-5">
                    <asp:Label ID="Label26" runat="server" class="control-label-2" Text="..." forecolor="White" ></asp:Label>
                    <asp:TextBox ID="txtProvNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row espacio">   
                <div class="col-md-8">
                    <asp:Label ID="Label33" runat="server" class="control-label-2" Text="Referencia" ></asp:Label>
                    <asp:TextBox ID="TxtReferencia" runat="server" CssClass="form-control"></asp:TextBox>
                </div>      
            </div>
            <div class="row espacio">
                <asp:TextBox ID="txtProvCodigo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>        
            </div>
            <div class="row espacio">
            </div>
   <%--             <div class="row">
                    <div class="col-lg-12">                   
                        <div id="accordion" role="tablist" aria-multiselectable="true" runat="server" >
                            <div class="card">
                                <div class="card-header" role="tab" id="section1HeaderId">
                                    <h5 class="mb-0">                            
                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="true" aria-controls="section1ContentId">
                                           Lista de Bienes Encontrados
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                                    <div class="card-body">  --%>       
                                        <h5 class="mb-0">Lista de Bienes encontrados</h5>                        
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="LblRegistroE" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:GridView ID="GvListaBienes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="QuitarFila" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Código" SortExpression="COD_ARTICULO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="CANT" HeaderText="Cant." /> 
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie Nro." SortExpression="SERIE_NRO" />  
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro." SortExpression="PLACA_NRO" />  
                                                        <asp:BoundField DataField="TipoBien" HeaderText="Tipo Bien" SortExpression="TipoBien" />  
                                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />  
                                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cód. Ubicación" SortExpression="COD_ALMACEN" />
                                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación" SortExpression="ALMACEN_NOMBRE"/>
                                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adq." SortExpression="SERIE_FECHA_ADQ"/>
                                                        <asp:BoundField DataField="SERIE_SKU" HeaderText="SKU" SortExpression="SERIE_SKU"/>
                                                        <asp:BoundField DataField="SERIE_ORDEN_COMPRA" HeaderText="OC" SortExpression="SERIE_ORDEN_COMPRA"/>
                                                        <asp:BoundField DataField="SERIE_GUIA" HeaderText="GUIA" SortExpression="SERIE_GUIA"/> 
                                                        <asp:BoundField DataField="ARTICULO_REFERENCIA" HeaderText="Referencia" SortExpression="ARTICULO_REFERENCIA" />   
                                                        <asp:BoundField DataField="ubicact_tipo" SortExpression="ubicact_tipo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ubicact_codigo" SortExpression="ubicact_codigo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Serie_Numerar" SortExpression="Serie_Numerar">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Art_Tipo" SortExpression="Art_Tipo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CS">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha_Servicio" HeaderText="Fecha R/S" SortExpression="Fecha_Servicio" /> 
                                                        <asp:BoundField DataField="Centro_Costo" HeaderText="Centro Costo" SortExpression="Centro_Costo" /> 
                                                    </Columns>
                                                </asp:GridView>
                                            </div> 
                                        </div>    
                                    <%--</div>
                                </div>
                            </div>--%>
                            <%--<div class="card">
                                <div class="card-header" role="tab" id="section1HeaderId2">
                                    <h5 class="mb-0">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId2" aria-expanded="false" aria-controls="section1ContentId2">
                                            Lista de Bienes no encontrados
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId2" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId2">
                                    <div class="card-body">  --%>
                                        <h5 class="mb-0">Lista de Bienes no encontrados</h5>
                                        <div class="row espacio">
                                            <div class="col-md-3">
                                                <asp:Button ID="BtnBuscarArt" runat="server"  CssClass="form-control btn btn-default" Text="Producto a Todos" />                    
                                            </div>      
                                        </div>
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="LblRegistroNE" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row">              
                                            <div class="col-md-12">
                                                <asp:GridView ID="GvListaNoEncontrados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="QuitarFila" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField CommandName="IngArt" Text="Ing. Art." ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                            <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Código" SortExpression="COD_ARTICULO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="CANT" HeaderText="Cant." /> 
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie Nro." SortExpression="SERIE_NRO" />  
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro." SortExpression="PLACA_NRO" />  
                                                        <asp:BoundField DataField="TipoBien" HeaderText="Tipo Bien" SortExpression="TipoBien" />  
                                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />  
                                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cód. Ubicación" SortExpression="COD_ALMACEN" />
                                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación" SortExpression="ALMACEN_NOMBRE"/>
                                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adq." SortExpression="SERIE_FECHA_ADQ"/>
                                                        <asp:BoundField DataField="SERIE_SKU" HeaderText="SKU" SortExpression="SERIE_SKU"/>
                                                        <asp:BoundField DataField="SERIE_ORDEN_COMPRA" HeaderText="OC" SortExpression="SERIE_ORDEN_COMPRA"/>
                                                        <asp:BoundField DataField="SERIE_GUIA" HeaderText="GUIA" SortExpression="SERIE_GUIA"/>
                                                        <asp:BoundField DataField="ARTICULO_REFERENCIA" HeaderText="Descripcion Referencia" SortExpression="ARTICULO_REFERENCIA" />  
                                                        <asp:BoundField DataField="ubicact_tipo" SortExpression="ubicact_tipo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ubicact_codigo" SortExpression="ubicact_codigo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Serie_Numerar" SortExpression="Serie_Numerar">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Art_Tipo" SortExpression="Art_Tipo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CS">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha_Servicio" HeaderText="Fecha R/S" SortExpression="Fecha_Servicio" /> 
                                                        <asp:BoundField DataField="Centro_Costo" HeaderText="Centro Costo" SortExpression="Centro_Costo" /> 
                                                    </Columns>
                                                </asp:GridView>
                                            </div> 
                                        </div>                               
<%--                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header" role="tab" id="section1HeaderId3">
                                    <h5 class="mb-0">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId3" aria-expanded="false" aria-controls="section1ContentId3">
                                            Lista de Cantidades
                                        </a>
                                    </h5>
                                </div>
                                <div id="section1ContentId3" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId3">
                                    <div class="card-body"> --%> 
                                        <h5 class="mb-0">Lista de Cantidades</h5>
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="LblRegistroCant" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row">              
                                            <div class="col-md-12">
                                                <asp:GridView ID="GvListaCantidades" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="QuitarFila" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField CommandName="IngArt" Text="Ing. Art." ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                            <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Código" SortExpression="COD_ARTICULO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="CANT" HeaderText="Cant." />  
                                                        <asp:BoundField DataField="TipoBien" HeaderText="Tipo Bien" SortExpression="TipoBien" />  
                                                        <asp:BoundField DataField="TIPO_UBICACION" HeaderText="Tipo Ubicación" SortExpression="TIPO_UBICACION" />  
                                                        <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cód. Ubicación" SortExpression="COD_ALMACEN" />
                                                        <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Descripción Ubicación" SortExpression="ALMACEN_NOMBRE"/>
                                                        <asp:BoundField DataField="SERIE_FECHA_ADQ" HeaderText="Fecha Adq." SortExpression="SERIE_FECHA_ADQ"/>
                                                        <asp:BoundField DataField="SERIE_SKU" HeaderText="SKU" SortExpression="SERIE_SKU"/>
                                                        <asp:BoundField DataField="SERIE_ORDEN_COMPRA" HeaderText="OC" SortExpression="SERIE_ORDEN_COMPRA"/>
                                                        <asp:BoundField DataField="SERIE_GUIA" HeaderText="GUIA" SortExpression="SERIE_GUIA"/>
                                                        <asp:BoundField DataField="ARTICULO_REFERENCIA" HeaderText="Descripcion Referencia" SortExpression="ARTICULO_REFERENCIA" /> 
                                                        <asp:BoundField DataField="ubicact_tipo" SortExpression="ubicact_tipo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ubicact_codigo" SortExpression="ubicact_codigo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Serie_Numerar" SortExpression="Serie_Numerar">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Art_Tipo" SortExpression="Art_Tipo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CS">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Fecha_Servicio" HeaderText="Fecha R/S" SortExpression="Fecha_Servicio" /> 
                                                        <asp:BoundField DataField="Centro_Costo" HeaderText="Centro Costo" SortExpression="Centro_Costo" /> 
                                                    </Columns>
                                                </asp:GridView>
                                            </div> 
                                        </div>                               
                                    <%--</div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </div> --%>




            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtPlaca" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtNroSerie" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="GvListaBienes" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnLimpiar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel> 

    </div>

    <div id="ModalBusqueda" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:Label ID="lblEtq_BusDestino" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Centro de Costos" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>                          
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="lblEtiq_Modal1" runat="server" Font-Bold="true"  Text="Código :" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-3 col-sx-5">
                                                    <asp:TextBox ID="txtBusCod" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-3">
                                                    <asp:Button ID="btnUbiCerrar" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="lblEtiq_Modal2" runat="server" Font-Bold="true"  Text="Descripción :" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-5 col-sx-5">
                                                    <asp:TextBox ID="txtBusDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-1">
                                                    <asp:Button ID="btnUbiListar" runat="server" Text="Listar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <asp:GridView ID="FlexUbicacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="CodInterno" HeaderText="Codigo" SortExpression="CodInterno" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
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

    <div id="ModalBuscaArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Búsqueda de Artículos</h4>
                </div>
                <div class="modal-body" style="padding: 20px 10px 0;">
                    <%--<div class="form-group">--%>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row espacio">
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_codArt">Código Artículo</label>
                                        <input class="form-control" id="TxtCodArticuloBA" type="text" runat="server" />
                                    </div>                                              
                                    <div class="col-md-6 col-xs-6 selectContainer">
                                        <label class="control-label" for="id_tipoArticuloBA">Tipo de Art :</label>
                                        <asp:DropDownList ID="DdlTipoBA" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row espacio">
                                    <div class="col-md-12 col-xs-10">
                                        <label class="control-label" for="id_descripcionBA">Descripción :</label>
                                        <input class="form-control" id="TxtDescripcionBA" name="Descripcion" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label ID="LblCodClasificacionBA" runat="server" CssClass="control-label" Visible="false" />
                                    <asp:Label ID="lblCodClas" runat="server" CssClass="control-label" Visible="false" />
                                </div>
                                <div class="row">
                                    <div class="col-md-9 col-xs-10">
                                        <label class="control-label" for="id_clasificacionBA">Clasificacíon</label>
                                        <input class="form-control" id="TxtClasificacionBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-2">
                                        <asp:Label ID="Label27" runat="server" CssClass="control-label" Text="Clasif" forecolor="White" />
                                        <asp:Button ID="BtnBuscaClasificacionBA" runat="server" Text="..." ControlStyle-CssClass="btn btn-default" />
                                    </div>
                                </div>
                                <div class="row espacio">
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_numParteBA">Número Parte</label>
                                        <input class="form-control" id="TxtNumParteBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_codEspecificoBA">Cod. Especif</label>
                                        <input class="form-control" id="TxtCodEspecificoBA" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row espacio">
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_numParteBA">SKU</label>
                                        <input class="form-control" id="TxtSku" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row espacio">
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="btn btn-default"/>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row espacio">
                                            <div class="col-sm-12">
                                                <asp:Label ID="LblCantArtReg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row espacio">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="GvBuscarArticulos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte"  SortExpression="ART_CODEQUIVA"/>
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo"  SortExpression="TIPO_ART"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                                             </ItemTemplate>
                                                        </asp:TemplateField>
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
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="GvListaNoEncontrados" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="GvListaCantidades" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="TreeNodePopulate" />
                            </Triggers>
                        </asp:UpdatePanel>
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div> 
    
    <div id="ModalBusquedaProv" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:Label ID="Label19" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Proveedores" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>                          
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="true"  Text="RUC:" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-3 col-sx-5">
                                                    <asp:TextBox ID="txtRucTipoPers" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-3">
                                                    <asp:Button ID="btnCerrar2" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="true"  Text="Razón Social:" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-5 col-sx-5">
                                                    <asp:TextBox ID="txtRazonSocialTipoPers" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-1">
                                                    <asp:Button ID="btnListaProveedor" runat="server" Text="Listar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row col-md-12">
                                                <asp:GridView ID="FlexTipoPers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="PERSONA_RUC" HeaderText="RUC" SortExpression="PERSONA_RUC" />
                                                        <asp:BoundField DataField="PERSONA_RAZON_SOCIAL" HeaderText="Razón Social" SortExpression="PERSONA_RAZON_SOCIAL" />
                                                        <asp:BoundField DataField="PERSONA_CODIGO" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexTipoPers" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCerrar2" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnListaProveedor" EventName="Click" />
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

</asp:Content>

