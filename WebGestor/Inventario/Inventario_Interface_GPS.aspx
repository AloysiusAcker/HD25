<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Interface_GPS.aspx.vb" Inherits="Inventario_Inventario_Interface_GPS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="container">

    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Interface GPS " CssClass="Titulos" />
        </div> 
    </div>
    <div class="row">
        <div class="col-lg-2">
            <asp:Label ID="Label10" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
            <asp:TextBox ID="TxtFechaIniExportar" runat="server" CssClass="form-control" Text=""></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaIniExportar" Format="dd/MM/yyyy" PopupButtonID="TxtFechaIniExportar" ></cc1:CalendarExtender>
        </div>
        <div class="col-lg-2">
            <asp:Label ID="Label11" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
            <asp:TextBox ID="TxtFechaFinExportar" runat="server" CssClass="form-control" Text=""></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFinExportar" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFinExportar" ></cc1:CalendarExtender>
        </div>
    </div> 
    <br />     
    <div class="row">     
        <div class="col-lg-2">
            <asp:Button ID="BtnListar" runat="server" Text="Listar Movimiento" ControlStyle-CssClass="form-control btn btn-default" />
        </div>              
        <div class="col-lg-2">
            <asp:Button ID="BtnExportar" runat="server" Text="Exportar Listado" ControlStyle-CssClass="form-control btn btn-default"  />
        </div>      
        <div class="col-md-4">
            <asp:Button ID="BtnExportarInvOk" runat="server" Text="Exportar Bienes Inventariado Ok" ControlStyle-CssClass="form-control btn btn-default" />
        </div>     
        <div class="col-lg-2">
            <asp:Button ID="BtnDefinirMov" runat="server" Text="Definir Movimiento" ControlStyle-CssClass="form-control btn btn-default" />
        </div>  
    </div>
    <br /> 
    <div class="row">         
        <div class="col-lg-2">
            <asp:Button ID="BtnUpload" runat="server" Text="Carga bienes GPS"  CssClass="form-control btn btn-default" visible="false"  />
        </div> 
        <div class="col-lg-2">
            <asp:Button ID="BtnCargaBienes" runat="server" Text="Cargar Archivo a mover" ControlStyle-CssClass="form-control btn btn-default" visible="false"  />
        </div> 
        <div class="col-lg-2">
            <asp:Button ID="BtnBienesNoMover" runat="server" Text="Placas no mover" ControlStyle-CssClass="form-control btn btn-default" visible="false"  />
        </div> 
        <div class="col-lg-2">
            <asp:Button ID="BtnListaBNoMover" runat="server" Text="Lista Placas no mover" ControlStyle-CssClass="form-control btn btn-default" visible="false"  />
        </div>   
        <div class="col-lg-2">
            <asp:Button ID="BtnCancelarMov" runat="server" Text="Cancelar Movimiento" ControlStyle-CssClass="form-control btn btn-default" visible="false"  />
        </div> 
    </div>
    <br />  
    <div id="Mov" runat ="server" visible ="false" >
        <div class="row">    
            <div class="col-lg-2">                        
                <asp:Label ID="LblEtiqMov" runat="server" Text="Nro Movimiento :" CssClass="control-label-2" />
                <asp:TextBox ID="TxtMovNro" runat="server"  CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-lg-2">                        
                <asp:Label ID="Label1" runat="server" Text="Tipo Movimiento :" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlMovTipo" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>                    
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="col-lg-5">    
                        <asp:Label ID="Label4" runat="server" Text="Centro Costo :" CssClass="control-label-2" />
                        <asp:TextBox ID="TxtCCosto" runat="server" visible="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label5" runat="server" Text="CC" CssClass="control-label-2" ForeColor="White" />
                        <asp:Button ID="BtnBusca" runat="server" Text="..." visible="False"  ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                </ContentTemplate>      
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                </Triggers>
            </asp:UpdatePanel>
        </div> 
        <div class="row">    
            <div class="col-lg-9">                        
                <asp:Label ID="Label2" runat="server" Text="Descripción" CssClass="control-label-2" />
                <asp:TextBox ID="txtMovDescripcion" runat="server"  CssClass="form-control"></asp:TextBox>
            </div>
        </div> 
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
        <ContentTemplate>
            <div id="MovCCostos" runat ="server" visible ="false" >
                <div class="row">
                    <div class="col-lg-2">
                        <asp:Label ID="Label6" runat="server" Text="Descripción" CssClass="control-label-2" ForeColor="white" />
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:Label ID="Label7" runat="server" Text="Descripción" CssClass="control-label-2"  ForeColor="white" />
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:Label ID="lblCodCCostos" runat="server" Text="" visible="false" ></asp:Label>
                        <asp:Label ID="LblUbicaCodigoInv" runat="server" Text="" visible="false" ></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>        
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
           
    <div id="MovFilas" runat ="server" visible ="false" >
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Lbl4" CssClass="control-label-2" runat="server" Text="Fila empieza"></asp:Label>
                <asp:TextBox ID="TxtIni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Lbl5" CssClass="control-label-2" runat="server" Text="Fila termina"></asp:Label>
                <asp:TextBox ID="Txtfin" runat="server" CssClass="form-control"></asp:TextBox>
            </div>             
            <div class="col-lg-5">
                <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="Fila termina" ForeColor="white"></asp:Label>
                <asp:FileUpload ID="fileUpload" runat="server"  CssClass="form-control" />
            </div> 
            <div class="col-lg-2">
                <asp:Label ID="Label13" CssClass="control-label-2" runat="server" Text="Carga" ForeColor="White"></asp:Label>
                <asp:Button ID="BtnCargaArchivo" runat="server" Text="Carga bienes Archivo" ControlStyle-CssClass="form-control btn btn-default" Visible ="false"  />
            </div>  
        </div>
    </div> 
    <div id="File" runat ="server" visible ="false" >  
    </div> 
    <div id="Mov2" runat ="server" visible ="false" >       
        <div class="row">
            <div class="col-lg-2">
                <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label8" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
            </div>
            <div class="col-lg-5">
                <asp:Label ID="Label9" CssClass="control-label-2" runat="server" Text="Carga" ForeColor="White"></asp:Label>
                <asp:Button ID="BtnCargarInvOk" runat="server" Text="Cargar Bienes Inv. Ok" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                    <asp:Label ID="Label12" CssClass="control-label-2" runat="server" Text="Carga" ForeColor="White"></asp:Label>
                <asp:Button ID="BtnCargarTablaMov" runat="server" Text="Carga Tabla Movi." ControlStyle-CssClass="form-control btn btn-default" />
            </div>  
        </div>                      
        <div class ="row">
        </div>  
        <br /> 
        <div class="row">
            <div class="col-lg-2">
                <asp:Button ID="Btn201Inf" runat="server" Text="Txt 201 Inf" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="Btn201Mob" runat="server" Text="Txt 201 Mob" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="btn201InfExcel" runat="server" Text="Excel 201 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="btn201MobExcel" runat="server" Text="Excel 201 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnGenerar" runat="server" Text="Txt 501 Mob" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="BtnGenerarInf" runat="server" Text="Txt 501 Inf" CssClass="form-control btn btn-default"/>
            </div> 
        </div>
        <br />  
        <div class="row">
            <div class="col-lg-2">
                <asp:Button ID="BtnListaInf" runat="server" Text="Listar 501 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnListarMob" runat="server" Text="Listar 501 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnLista201I" runat="server" Text="Listar 201 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnLista201M" runat="server" Text="Listar 201 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnExcelInf" runat="server" Text="Excel 501 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnExcelMob" runat="server" Text="Excel 501 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
    </div> 
    <br />

    <div id="Lista" runat ="server" visible ="false" class="row" >                   
        <div class="col-lg-12">
            <asp:GridView ID="gvListaMoviimentos" runat="server" AutoGenerateColumns="False" width="100%" CssClass="table table-bordered GridView">
                <Columns>
                    <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                        <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                        <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                    </asp:ButtonField>
                    <asp:BoundField DataField="GPS_MOV_NRO" HeaderText="Nro. Mov." SortExpression="GPS_MOV_NRO" />
                    <asp:BoundField DataField="TIPO_MOV" HeaderText="Tipo Mov." SortExpression="TIPO_MOV" />
                    <asp:BoundField DataField="GPS_MOV_DESCRIPCION" HeaderText="Descripción Mov." SortExpression="GPS_MOV_DESCRIPCION" />
                    <asp:BoundField DataField="Fecha_Mov" HeaderText="Fecha" SortExpression="Fecha_Mov" />
                    <asp:BoundField DataField="HORA_Mov" HeaderText="Hora" SortExpression="HORA_Mov" />
                    <asp:BoundField DataField="CANT" HeaderText="Cant. Bienes" SortExpression="CANT" />
                    <asp:BoundField DataField="ESTADO_MOV" HeaderText="Estado" SortExpression="ESTADO_MOV" />
                </Columns>
            </asp:GridView>
        </div>    
           
    </div> 
    <div class="row">
        <div class="col-lg-12">                   
            <div id="accordion" role="tablist" aria-multiselectable="true" runat="server" visible="false" >
                <div class="card">
                    <div class="card-header" role="tab" id="section1HeaderId">
                        <h5 class="mb-0">                            
                            <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="false" aria-controls="section1ContentId">
                                Lista Placa que no se mueven
                            </a>
                        </h5>
                    </div>
                    <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                        <div class="card-body">                                 
                            <div class="row">                    
                                <div class="col-md-12">
                                    <asp:Label ID="lblRegistros" runat="server" class="control-label-2" Text="" ></asp:Label>
                                </div> 
                            </div> 
                            <div class="row">                    
                                <div class="col-md-12">
                                    <asp:GridView ID="GvListaPlacas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:BoundField DataField="GPS_MOV_NRO" HeaderText="Nro. Mov." SortExpression="GPS_MOV_NRO" />
                                            <asp:BoundField DataField="GPSDET_CENTRO_COSTO" HeaderText="Centro Costos" SortExpression="GPSDET_CENTRO_COSTO" />
                                            <asp:BoundField DataField="GPSDET_CENTRO_COSTO_FINAL" HeaderText="Centro Costos Final" SortExpression="GPSDET_CENTRO_COSTO_FINAL" />
                                            <asp:TemplateField HeaderText="Desc. Artículo">
                                                <ItemTemplate>
                                                    <div class="two-lines-cell">
                                                        <%# Eval("GPSDET_DESCRIPCION") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="GPSDET_PLACA_NRO" HeaderText="Nro. Placa" SortExpression="GPSDET_PLACA_NRO" />
                                            <asp:BoundField DataField="GPSDET_SERIE_EQUIPO" HeaderText="Serie Equipo" SortExpression="GPSDET_SERIE_EQUIPO" />
                                            <asp:BoundField DataField="GPSDET_SERIE_NRO" HeaderText="Nro. Serie" SortExpression="GPSDET_SERIE_NRO" />
                                            <asp:BoundField DataField="GPSDET_STATUS_USU" HeaderText="Status" SortExpression="GPSDET_STATUS_USU" />
                                        </Columns>
                                    </asp:GridView>
                                </div> 
                            </div>       

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>  
        
        <div class="row">                    
            <div class="col-lg-12">
                <asp:Label ID="LblRegistro2" runat="server" class="control-label-2" Text="" ></asp:Label>
            </div> 
        </div>  

        <div class="col-lg-12">
            <asp:GridView ID="gvListaGps" runat="server" AutoGenerateColumns="False" width="100%" CssClass="table table-bordered GridView">
                <Columns>
                    <asp:BoundField DataField="GPSDET_CENTRO_COSTO" HeaderText="Centro Costo Actual" SortExpression="GPSDET_CENTRO_COSTO" />
                    <asp:BoundField DataField="GPSDET_CENTRO_COSTO_FINAL" HeaderText="Centro Costo Final" SortExpression="GPSDET_CENTRO_COSTO_FINAL" />
                    <asp:BoundField DataField="GPSDET_SERIE_EQUIPO" HeaderText="Serie Equipo" SortExpression="GPSDET_SERIE_EQUIPO" />
                    <asp:BoundField DataField="GPSDET_PLACA_NRO" HeaderText="Placa Nro" SortExpression="GPSDET_PLACA_NRO" />
                    <asp:BoundField DataField="GPSDET_SERIE_NRO" HeaderText="Serie Nro" SortExpression="GPSDET_SERIE_NRO" />
                    <asp:BoundField DataField="GPSDET_MATERIAL" HeaderText="Material" SortExpression="GPSDET_MATERIAL" />
                    <asp:BoundField DataField="GPSDET_DESCRIPCION" HeaderText="Descripcion" SortExpression="GPS_DESCRIPCION" />
                    <asp:BoundField DataField="Fecha_Mov" HeaderText="Fecha Mov." SortExpression="Fecha_Mov" />
                    <asp:BoundField DataField="GPSDET_ALMACEN" HeaderText="Almacen" SortExpression="GPSDET_ALMACEN" />
                    <asp:BoundField DataField="GPSDET_STATUS_USU" HeaderText="Stat Usu" SortExpression="GPSDET_STATUS_USU" />
                    <asp:BoundField DataField="GPSDET_TIPO_EQUIPO" HeaderText="Tipo Equipo" SortExpression="GPSDET_TIPO_EQUIPO" />
                    <asp:BoundField DataField="GPSDET_MOV501" HeaderText="Mov. 501" SortExpression="GPSDET_MOV501" />
                    <asp:BoundField DataField="Fecha_Mov501" HeaderText="Fecha Mov. 501" SortExpression="Fecha_Mov501" />
                    <asp:BoundField DataField="GPSDET_MOV201" HeaderText="Mov. 201" SortExpression="GPSDET_MOV201" />
                    <asp:BoundField DataField="Fecha_Mov201" HeaderText="Fecha Mov. 501" SortExpression="Fecha_Mov201" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                    <asp:BoundField DataField="NOMOVER" HeaderText="NO MOVER" SortExpression="NOMOVER" />
                </Columns>
            </asp:GridView>
        </div>

    
  </div>    

    <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">         
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda Sección de Centro de Costo" />
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
                                                        <asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Codigo" SortExpression="CodInterno" />
                                                        <asp:BoundField DataField="CECOSE_DESCRIPCION" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="CECOSE_CODIGO" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
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

