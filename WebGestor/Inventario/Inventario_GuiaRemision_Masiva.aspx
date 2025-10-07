<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_GuiaRemision_Masiva.aspx.vb" Inherits="Inventario_Inventario_GuiaRemision_Masiva" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Guía Remisión - Masiva" CssClass="Titulos"></asp:Label>
            </div>
        </div><br /><br />
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-9">
                        <asp:Label ID="LblError" runat="server" ForeColor="red"></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnLeerArchivo" />
                </Triggers>
            </asp:UpdatePanel>  
        </div>
        
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha:"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-4">
                <asp:Label ID="Label18" CssClass="control-label-2" runat="server" Text="Hora:"></asp:Label>
                <asp:TextBox ID="TxtHora" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>                
            </div>
            <div class="col-md-3">
                <asp:Label ID="Lbl1" CssClass="control-label-2" runat="server" Text="# Col - Centro de Costo:"></asp:Label>
                <asp:TextBox ID="TxtColCC" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Lbl2" CssClass="control-label-2" runat="server" Text="# Col - Observacion Guia"></asp:Label>
                <asp:TextBox ID="TxtColObsGuia" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
 
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-2">
                        <asp:Label ID="Lbletiqueta4" CssClass="control-label-2" runat="server" Text="Guia Serie"></asp:Label>
                        <asp:TextBox ID="TxtGuiaSerie" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="Lbl3" CssClass="control-label-2" runat="server" Text="Guia Numero"></asp:Label>
                        <asp:TextBox ID="TxtGuiaNumero" runat="server" CssClass="form-control"></asp:TextBox>
                    </div> 
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="TxtGuiaSerie" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>  
            <div class="col-md-3">
                <asp:Label ID="Lbl4" CssClass="control-label-2" runat="server" Text="Fila donde empieza"></asp:Label>
                <asp:TextBox ID="TxtIni" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Lbl5" CssClass="control-label-2" runat="server" Text="Fila donde termina"></asp:Label>
                <asp:TextBox ID="Txtfin" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-md-6">
                        <asp:Label ID="Label11" CssClass="control-label-2" runat="server" Text="Archivo"></asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnLeerArchivo" />
                </Triggers>
            </asp:UpdatePanel> 
            <div class="col-md-3">
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label12" CssClass="control-label-2" runat="server" Text="Archivo" ForeColor="white"></asp:Label>
                <asp:Button ID="btnLeerArchivo" runat="server" Text="Generar Guías de Remisión" CssClass="form-control btn btn-default" OnClick="btnLeerArchivo_Click" />                        
            </div>  
        </div>
        
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="Label13" CssClass="control-label-2" runat="server" Text="Modalidad Transporte:"></asp:Label>
                <asp:DropDownList ID="DdlModTransporte" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>       
            <div class="col-md-3">
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label6"  CssClass="control-label-2" runat="server" Text="P" ForeColor="white" ></asp:Label>
                <asp:Button ID="BtnBuscarArt" runat="server"  CssClass="form-control btn btn-default" Text="Agregar Producto" />                    
            </div>      
        </div>

        <div class="row">
            <div class="col-md-12"> 
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="Lbl6" CssClass="control-label-2" runat="server" Text="Origen"></asp:Label>
                        <asp:RadioButton ID="optOrigen" text="Almacén" Checked="false"  runat="server" Class="form-control2"  GroupName="ubicacion"  AutoPostBack="True" />
                        <asp:RadioButton ID="optOrigen2" text="Centro Costo" Checked="false"  runat="server" Class="form-control2" GroupName="ubicacion"  AutoPostBack="True" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="optOrigen" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="optOrigen2" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label15"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="txtCodOrigen" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label16"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnBuscarOrigen" runat="server"  CssClass="btn btn-default" Enabled="false" Text="..." />
                    </div> 
                    <div class="col-md-9">
                        <asp:Label ID="Label17"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>        
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="Label19"  CssClass="control-label-2" runat="server" Text="Pto. de Partida"></asp:Label>
                        <asp:TextBox ID="TxtPuntoPartida" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Ubigeo"></asp:Label>
                        <asp:TextBox ID="TxtUbigeo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>          
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblEtiqueta5"  CssClass="control-label-3" runat="server" Text="Datos del Transportista" Font-Bold="true" ></asp:Label>
                    </div>  
                </div>                

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblPlaca"  CssClass="control-label-2" runat="server" Text="Placa Vehículo" ></asp:Label>
                        <asp:TextBox ID="TxtNroPlaca" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Lbl7"  CssClass="control-label-2" runat="server" Text="Placa" ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnVehiculo" runat="server"  CssClass="btn btn-default" Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="Lbl8"  CssClass="control-label-2" runat="server" Text="Marca Vehículo" ></asp:Label>
                        <asp:TextBox ID="TxtMarca" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label1"  CssClass="control-label-2" runat="server" Text="Conf. Vehicular" ></asp:Label>
                        <asp:TextBox ID="TxtconfVehicular" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Lbl11"  CssClass="control-label-2" runat="server" Text="R.U.C." ></asp:Label>
                        <asp:TextBox ID="TxtRucTrasnportista" runat="server" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Lbl12"  CssClass="control-label-2" runat="server" Text="Currier" ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnTransporte" runat="server"  CssClass="btn btn-default"  Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="Lbl13"  CssClass="control-label-2" runat="server" Text="Razon social" ></asp:Label>
                        <asp:TextBox ID="TxtRazonTransportista" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Lbl14"  CssClass="control-label-2" runat="server" Text="Cert. Inscripción" ></asp:Label>
                        <asp:TextBox ID="TxtCertInscripcion" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label2"  CssClass="control-label-2" runat="server" Text="Chofer DNI" ></asp:Label>
                        <asp:TextBox ID="TxtChoferDNI" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label3"  CssClass="control-label-2" runat="server" Text="Chofer" ForeColor="white" ></asp:Label>
                        <asp:Button ID="BtnChofer" runat="server"  CssClass="btn btn-default" Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="Label4"  CssClass="control-label-2" runat="server" Text="Apellidos y Nombres" ></asp:Label>
                        <asp:TextBox ID="TxtChoferNombre" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label5"  CssClass="control-label-2" runat="server" Text="Nro. Licencia" ></asp:Label>
                        <asp:TextBox ID="TxtLicencia" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div> 

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvTransporte" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvVehiculo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvChofer" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="optOrigen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="optOrigen2" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnBuscarArt" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
        
        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-9">
                        <asp:Label ID="Label7"  CssClass="control-label-2" runat="server" Text="Lista de producto"></asp:Label>                
                        <asp:GridView ID="GvListaArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="QuitarArt" ImageUrl="~/icono/delete2_opt.png" Text="Eliminar">
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Código" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="TIPO_ART" HeaderText="tipo" SortExpression="TIPO_ART" />
                                <asp:BoundField DataField="CANT_COL_EXCEL" HeaderText="Nro. Col. Cant." SortExpression="CANT_COL_EXCEL" />   
                                <asp:TemplateField>                                                                       
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCant" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                    <div class="col-md-3">
                        <asp:Label ID="Label8"  CssClass="control-label-2" runat="server" Text="Centro de Costos sin direcciones"></asp:Label>                
                        <asp:GridView ID="GvListaCC" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Código" SortExpression="CECOSE_COD_INTERNO" />
                                <asp:BoundField DataField="CECOSE_DESCRIPCION" HeaderText="Descripción" SortExpression="CECOSE_DESCRIPCION" />
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>         
        
                <div class="form-group">
                    <div class="col-md-9">
                        <asp:Label ID="lblCodOrigen" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodTrasporte" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodVehiculo" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodChofer" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvTransporte" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvVehiculo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvChofer" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>

        <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="TituloPopup" runat="server" Text="Buscar" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarOrigen" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step1">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2">
                                                        <asp:Button ID="btnCancelar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                               <%--             <div class="col-lg-12">--%>
                                                <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CodInterno" HeaderText="Código" SortExpression="CodInterno" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                                <asp:BoundField DataField="Direccion"  HeaderText="Dirección" SortExpression="Codigo"/>
                                                                <asp:BoundField DataField="Ubigeo"  HeaderText="Ubigeo" SortExpression="Codigo"/>
                                                                <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                   <%--         </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ModalTransporte" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:Label ID="LblPopudTransporte" runat="server" Text="Busqueda Empresa Transporte" />
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step2">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label"  for="id_descripcion">Razón social :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusTransRazon" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnBusTransporte" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label" for="id_codigo">RUC :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusTransRUC" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnCancelarTrans" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvTransporte" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCancelarTrans" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                                            <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvTransporte" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CodInterno" HeaderText="Código" SortExpression="CodInterno" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                                <asp:BoundField DataField="CertInscripcion" HeaderText="Cert. Inscripción" SortExpression="CertInscripcion"/>
                                                                <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnBusTransporte" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnCancelarTrans" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ModalVehiculo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:Label ID="Label9" runat="server" Text="Busqueda Vehículo" />
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step3">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label"  for="id_descripcion">Nro. Placa :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusPlaca" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnVehiculoBuscar" CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label" for="id_codigo">Marca :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusMarca" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnVehiculoCerrar" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvVehiculo" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnVehiculoCerrar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                                            <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvVehiculo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="VEHICU_PLACA" HeaderText="Placa" SortExpression="VEHICU_PLACA" />
                                                                <asp:BoundField DataField="VEHICU_MARCA" HeaderText="Marca" SortExpression="VEHICU_MARCA" />
                                                                <asp:BoundField DataField="VEHICU_CERTIF_INSCRIP" HeaderText="Cert. Inscripción" SortExpression="VEHICU_CERTIF_INSCRIP"/>
                                                                <asp:BoundField DataField="RUCTRANSPORTISTA" HeaderText="RUC" SortExpression="RUCTRANSPORTISTA" />
                                                                <asp:BoundField DataField="NOMTRANSPORTISTA" HeaderText="Razón Social" SortExpression="NOMTRANSPORTISTA" />
                                                                <asp:BoundField DataField="CERTRANSPORTISTA" HeaderText="Cert. Inscripcion" SortExpression="CERTRANSPORTISTA" />
                                                                <asp:BoundField DataField="VEHICU_CODIGO" SortExpression="VEHICU_CODIGO">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnVehiculoBuscar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnVehiculoCerrar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ModalChofer" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:Label ID="Label10" runat="server" Text="Busqueda Chofer" />
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step4">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label"  for="id_descripcion">DNI :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusChoferDni" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnChoferBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                    </div>
                                                </div>
                                                <div class="row form-group col-md-12">
                                                    <label class="col-sm-3 control-label" for="id_codigo">Nombres :</label>
                                                    <div class="col-sm-6 col-xs-5">
                                                        <input class="form-control" id="TxtBusChoferNombres" type="text" runat="server" />
                                                    </div>
                                                    <div class="col-sm-3 col-xs-2 ">
                                                        <asp:Button ID="BtnChoferCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvChofer" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnChoferCerrar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row form-group col-md-12">
                                            <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="GvChofer" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="CHOFER_DNI" HeaderText="DNI" SortExpression="CHOFER_DNI" />
                                                                <asp:BoundField DataField="NOMBRES" HeaderText="Apellidos y Nombres" SortExpression="NOMBRES" />
                                                                <asp:BoundField DataField="CHOFER_BREVETE" HeaderText="Nº Licencia" SortExpression="CHOFER_BREVETE"/>
                                                                <asp:BoundField DataField="CHOFER_CODIGO" SortExpression="CHOFER_CODIGO">
                                                                    <ItemStyle ForeColor="White" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="BtnChoferBuscar" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="BtnChoferCerrar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="ModalArticulo" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
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
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
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
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <%--<div class="col-lg-7 col-lg-offset-1">--%>
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GvBusArticulo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Código" SortExpression="ART_CODIGO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción" SortExpression="ART_DESCRIPCION" />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nº Parte" SortExpression="ART_CODEQUIVA"/>
                                                            <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo" SortExpression="TIPO_ART"/>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
         

    </div> 

<%--    <div class="container">
            <div class="accordion" id="accordionExample">
                <div class="card">
                    <div class="card-header" id="headingOne">
                        <h2 class="mb-0">
                            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">                                
                                columna 1
                            </button>
                        </h2>
                    </div>
                    <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                        <div class="card-body">
                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" />
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header" id="headingTwo">
                        <h2 class="mb-0">
                            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                Column 2
                            </button>
                        </h2>
                    </div>
                    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                        <div class="card-body">
                            <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control" />
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header" id="headingThree">
                        <h2 class="mb-0">
                            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                Column 3
                            </button>
                        </h2>
                    </div>
                    <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                        <div class="card-body">
                            <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
</asp:Content>

