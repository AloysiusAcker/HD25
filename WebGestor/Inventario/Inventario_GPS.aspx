<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_GPS.aspx.vb" Inherits="Inventario_Inventario_GPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--    <style type="text/css">
        /* Personalizar el estilo del botón de carga */
        #fileUpload {
            padding: 5px;
            border: 1px solid #ccc;
            background-color: #f9f9f9;
            cursor: pointer;
        }

        /* Opcional: Estilo para el botón de subida */
        #btnUpload {
            padding: 5px;
            background-color: #808080;
            color: #fff;
            border: none;
            cursor: pointer;
        }
    </style>--%>

        <div class="row">
            <div class="col-lg-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Informe Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <br />

        <div class="row">            
            <div class="col-lg-4">
                <asp:Button ID="BtnCargarInvOk" runat="server" Text="Cargar Bienes Inv. Ok" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        <div class="row">
            <div class="col-lg-2">
                <asp:Button ID="BtnListaInf" runat="server" Text="Listar 501 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnListarMob" runat="server" Text="Listar 501 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnGenerar" runat="server" Text="Txt 501 Mob" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="BtnGenerarInf" runat="server" Text="Txt 501 Inf" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="BtnExcelInf" runat="server" Text="Excel 501 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnExcelMob" runat="server" Text="Excel 501 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        <div class="row">
            <div class="col-lg-2">
                <asp:Button ID="BtnLista201I" runat="server" Text="Listar 201 Inf." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="BtnLista201M" runat="server" Text="Listar 201 Mob." ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Button ID="Btn201Inf" runat="server" Text="Txt 201 Inf" CssClass="form-control btn btn-default"/>
            </div>
            <div class="col-lg-2">
                <asp:Button ID="Btn201Mob" runat="server" Text="Txt 201 Mob" CssClass="form-control btn btn-default"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Lbl4" CssClass="control-label-2" runat="server" Text="Fila empieza"></asp:Label>
                <asp:TextBox ID="TxtIni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Lbl5" CssClass="control-label-2" runat="server" Text="Fila termina"></asp:Label>
                <asp:TextBox ID="Txtfin" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-2">
                <asp:Button ID="btnUpload" runat="server" Text="Subir Archivo"  CssClass="form-control btn btn-default" OnClick="btnUpload_Click" />
            </div>
            <div class="col-lg-6">
                <asp:FileUpload ID="fileUpload" runat="server"   CssClass="form-control"/>
            </div> 
        </div>
        <div class="row">
            <div class="col-lg-12">
                <asp:PlaceHolder runat="server" ID="phEtiquetas"></asp:PlaceHolder>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row col-lg-12">
                    <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red"/>
                </div> 
                <div class="row">
                    <div class="col-lg-9">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RbTodos" runat="server" Text="Todos"  Checked="true" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-lg-6">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-lg-12">
                         <asp:Label ID="LblUbicaCodigo" runat="server" Text="" visible="false" ></asp:Label>
                         <asp:Label ID="LblUbicaCodigoInv" runat="server" Text="" visible="false" ></asp:Label>
                    </div>
                </div>
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistro2" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="gvListaGps" runat="server" AutoGenerateColumns="False" width="100%" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="GPS_SERIE_EQUIPO" HeaderText="Serie Equipo" SortExpression="GPS_SERIE_EQUIPO" />
                                <asp:BoundField DataField="GPS_PLACA_NRO" HeaderText="Placa Nro" SortExpression="GPS_PLACA_NRO" />
                                <asp:BoundField DataField="GPS_SERIE_NRO" HeaderText="Serie Nro" SortExpression="GPS_SERIE_NRO" />
                                <asp:BoundField DataField="GPS_MATERIAL" HeaderText="Material" SortExpression="GPS_MATERIAL" />
                                <asp:BoundField DataField="GPS_DESCRIPCION" HeaderText="Descripcion" SortExpression="GPS_DESCRIPCION" />
                                <asp:BoundField DataField="GPS_FECHA_MOV" HeaderText="Fecha Mov." SortExpression="GPS_FECHA_MOV" />
                                <asp:BoundField DataField="GPS_ALMACEN" HeaderText="Almacen" SortExpression="GPS_ALMACEN" />
                                <asp:BoundField DataField="GPS_CENTRO_COSTO" HeaderText="Centro Costo" SortExpression="GPS_CENTRO_COSTO" />
                                <asp:BoundField DataField="GPS_STATUS_USU" HeaderText="Stat Usu" SortExpression="GPS_STATUS_USU" />
                                <asp:BoundField DataField="GPS_TIPO_EQUIPO" HeaderText="Tipo Equipo" SortExpression="GPS_TIPO_EQUIPO" />
                                <asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Ubicacion CC" SortExpression="CECOSE_COD_INTERNO" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RbTodos" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

     <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="RbTodos" EventName="CheckedChanged" />
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
                                                        <asp:BoundField DataField="CodUbi" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" Width="" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="RbTodos" EventName="CheckedChanged" /> 
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

