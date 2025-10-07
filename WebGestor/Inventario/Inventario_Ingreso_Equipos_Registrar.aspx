<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Ingreso_Equipos_Registrar.aspx.vb" Inherits="Inventario_Inventario_Ingreso_Equipos_Registrar" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Recepción" CssClass="subTitulos"></asp:Label>
            </div>
        </div> 
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                    <cc1:TabPanel runat="server" HeaderText="Nueva Recepción" ID="TabPanel3">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>     
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-2">
                                            <asp:Label ID="lbl1" runat="server" Text="Nro. Recepción" CssClass="control-label2"></asp:Label>
                                            <asp:TextBox ID="txtNroRecepcion" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                        </div> 
                                        <div class="col-md-2">
                                            <asp:Label ID="LblEtiq_3" CssClass="control-label-2" runat="server" Text="Fecha Registro"></asp:Label>
                                            <asp:TextBox ID="txtFecRegistra" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                        <div class="col-md-2">
                                            <asp:Label ID="Label24" CssClass="control-label-2" runat="server" Text="Hora Registro"></asp:Label>
                                            <asp:TextBox ID="txtHoraRegistra" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                        <div class="col-md-2">
                                        </div> 
                                        <div class="col-md-2">
                                            <asp:Label ID="LblEtiq_2" runat="server" class="control-label-2" Text="Listar" forecolor="White" ></asp:Label>
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ControlStyle-CssClass="form-control btn btn-default" />
                                        </div> 
                                        <div class="col-md-2">
                                            <asp:Label ID="Label12" runat="server" class="control-label-2" Text="Listar" forecolor="White" ></asp:Label>
                                            <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" ControlStyle-CssClass="form-control btn btn-default" />
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Usuario Registro"></asp:Label>
                                            <asp:TextBox ID="txtUserRegistra" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-6">
                                            <asp:Label ID="LblEtiq_1" runat="server" Text="Almacén:" CssClass="control-label-2" />
                                            <asp:DropDownList ID="cboAlmacen" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-md-6">
                                            <asp:Label ID="Label3" runat="server" Text="Motivo:" CssClass="control-label-2" />
                                            <asp:DropDownList ID="cboMotivo" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label1" runat="server" Text="Tipo Documento:" CssClass="control-label-2" />
                                            <asp:DropDownList ID="cboTipoDoc" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-md-2">
                                            <asp:Label ID="Label4" CssClass="control-label-2" runat="server" Text="Serie Documento"></asp:Label>
                                            <asp:TextBox ID="txtSerieDoc" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                        <div class="col-md-5">
                                            <asp:Label ID="Label25" CssClass="control-label-2" runat="server" Text="Nro. Documento"></asp:Label>
                                            <asp:TextBox ID="txtNroDoc" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label5" CssClass="control-label-2" runat="server" Text="Fecha Recepción"></asp:Label>
                                            <asp:TextBox ID="txtFecRecepcion" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="txtFecRecepcion" Format="dd/MM/yyyy" PopupButtonID="txtFecRecepcion" ></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:Label ID="Label10" CssClass="control-label-2" runat="server" Text="Nro. Orden Compra"></asp:Label>
                                            <asp:TextBox ID="txtNroOC" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label6" CssClass="control-label-2" runat="server" Text="Proveedor"></asp:Label>
                                            <asp:TextBox ID="txtProvRuc" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="Label11" runat="server" class="control-label-2" Text="..." forecolor="White" ></asp:Label>
                                            <asp:Button ID="btnBus" runat="server" Text="..." CssClass="form-control btn btn-default" />
                                        </div>
                                        <div class="col-md-5">
                                            <asp:Label ID="Label26" runat="server" class="control-label-2" Text="..." forecolor="White" ></asp:Label>
                                            <asp:TextBox ID="txtProvNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row espacio">
                                        <asp:TextBox ID="txtProvCodigo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>         
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-9">
                                            <asp:Label ID="Label7" runat="server" Text="Propietario" CssClass="control-label-2" />
                                            <asp:DropDownList ID="cboPropietario" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label8" runat="server" Text="¿Desea ingresar la cantidad de accesorios y/o series de los equipos?" CssClass="control-label-2" ForeColor="Maroon"  />
                                        </div> 
                                        <div class="col-md-6">
                                            <asp:RadioButtonList ID="optIngreso" runat="server" RepeatDirection="Horizontal" CssClass ="form-control">
                                                <asp:ListItem>Si</asp:ListItem>
                                                <asp:ListItem>No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnAgregar" runat="server" Text="..." CssClass="form-control btn btn-default" />
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                            <asp:GridView id="FlexItem" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="true" >
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Button" CommandName="Quitar" Text="Quitar">
                                                        <ControlStyle CssClass="EstiloBoton_Ac" Width="50px" />
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="c0" HeaderText="Item">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="c1" HeaderText="Cod. Art.">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="c2" HeaderText="Nro. Parte">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="c3" HeaderText="Descripción">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="c4" HeaderText="Tipo">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Cant. x Recibir">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCant" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Garantía">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtGarantia" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtGarantia" TargetControlID="txtGarantia">
                                                            </cc1:CalendarExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="c5">
                                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="c6">
                                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="c7">
                                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            </asp:GridView>
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label15" runat="server" Text="Proyecto Año" CssClass="control-label-2" />
                                            <asp:DropDownList ID="cboAño" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="col-md-6">
                                            <asp:Label ID="Label27" runat="server" Text="Proyecto" CssClass="control-label-2" />
                                            <asp:DropDownList ID="cboProyecto" runat="server" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                            <asp:Label ID="Label28" runat="server" class="control-label-2" Text="Referencia" ></asp:Label>
                                            <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel> 

                        </ContentTemplate>  
                    </cc1:TabPanel> 
                    <cc1:TabPanel runat="server" HeaderText="Ingresar Series y Cantidades" ID="TabPanel5">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>     
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblErrort" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_5" CssClass="control-label-2" runat="server" Text="Nro. Recepción"></asp:Label>
                                            <asp:TextBox ID="txtIngRecepcion" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_6" CssClass="control-label-2" runat="server" Text="Almacén"></asp:Label>
                                            <asp:TextBox ID="txtIngCodAlmacen" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label16" CssClass="control-label-2" runat="server" Text="Almacén"></asp:Label>
                                            <asp:TextBox ID="txtIngAlmacen" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div> 
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="..." ForeColor ="White" ></asp:Label>
                                            <asp:Button ID="BtnRegresar2" Text="Regresar" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-10">
                                            <asp:Label ID="Label29" CssClass="control-label-2" runat="server" Text="Proveedor"></asp:Label>
                                            <asp:TextBox ID="txtIngProveedor" runat="server" CssClass="form-control" Text=""></asp:TextBox>                                                                                                           
                                        </div> 
                                        <div class="col-md-2 col-xs-6">
                                            <asp:Label ID="LblEtiq_9" CssClass="control-label-2" runat="server" Text="Almacén"></asp:Label>
                                            <asp:Button ID="btnEjecutar" Text="Ejecutar Recepción" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="row espacio">                    
                                        <div class="col-md-9">
                                            <asp:GridView ID="FlexItemSerie" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                <Columns>
                                                    <asp:ButtonField CommandName="IngSerie" Text="Ing. Series" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                        <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="ITEM" HeaderText="Nro. Item" SortExpression="ITEM" />
                                                    <asp:BoundField DataField="ART_COD" HeaderText="Art. Código" SortExpression="ART_COD" />
                                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Srt. Descripción" SortExpression="DESCRIPCION" />
                                                    <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. x Rec." SortExpression="CANT_XREC" />
                                                    <asp:BoundField DataField="CANT_REC" HeaderText="Cant. Rec." SortExpression="CANT_REC" />
                                                    <asp:BoundField DataField="CANT_FALTA" HeaderText="Cant. Falta" SortExpression="CANT_FALTA" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>   
                                    </div>   
                                    <div class="row espacio" id ="IngSeries" runat="server" visible="false" >
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label9" CssClass="control-label-2" runat="server" Text="Artículo"></asp:Label>
                                            <asp:TextBox ID="txtIngArtCodigo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-lg-8">
                                            <asp:Label ID="Label17" CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                                            <asp:TextBox ID="txtIngArticulo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label22" CssClass="control-label-2" runat="server" Text="..." ForeColor="White" ></asp:Label>
                                            <asp:Button ID="BtnBorrar" Text ="Borrar Series"  runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                        </div>
                                    </div> 
                                    <div class="row espacio">
                                        <div class="col-lg-10">
                                            <asp:Label ID="Label18" CssClass="control-label-2" runat="server" Text="..." ForeColor="White" ></asp:Label>
                                            <asp:GridView ID="FlexSeries" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Ing. Nro Serie">
                                                        <ItemTemplate>
                                                            <asp:TextBox id="txtSerie" runat="server" CssClass="form-control" Text="">  
                                                            </asp:TextBox> 
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ing. Nro Placa">
                                                        <ItemTemplate>
                                                            <asp:TextBox id="txtPlaca" runat="server" CssClass="form-control" Text="">  
                                                            </asp:TextBox> 
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SERIE_NUMERAR">
                                                        <ItemStyle Wrap="True" ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div> 
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label19" CssClass="control-label-2" runat="server" Text="..." ForeColor="White" ></asp:Label>
                                            <asp:Button ID="BtnGuardarS" Text ="Guardar Series"  runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                        </div>
                                    </div>
                                    <h4>Accesorios x recibir: Ingreso de cantidades</h4>
                                    <div class="row espacio"> 
                                        <div class="col-md-3">
                                            <asp:CheckBox ID="ChkRecibirAcc" CssClass="checkbox checkbox-inline" Text="Recibir Todo" Font-Bold ="true" runat="server" AutoPostBack="True" Visible ="false"  />
                                        </div>
                                        <div class="col-md-1">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="BtnGuardarAccCant" Text="Guardar Cantidades" runat="server" ControlStyle-CssClass="form-control btn btn-default" Visible ="false" ></asp:Button>                                
                                        </div>
                                    </div>
                                    <div class="row espacio"> 
                                        <div class="col-md-3">
                                            <asp:GridView ID="FlexItemAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >                            
                                                <Columns>
                                                    <asp:BoundField DataField="ITEM" HeaderText="Nro. Item"></asp:BoundField>
                                                    <asp:BoundField DataField="ART_COD" HeaderText="Art. Codigo"></asp:BoundField>
                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte"></asp:BoundField>
                                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Art. Descripción"></asp:BoundField>
                                                    <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. x Recibir"></asp:BoundField>
                                                    <asp:BoundField DataField="CANT_REC" HeaderText="Cant. Recibida"></asp:BoundField>
                                                    <asp:BoundField DataField="CANT_FALTA" HeaderText="Falta Recibir"></asp:BoundField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                                            </asp:GridView>                                
                                        </div>
                                    </div>
                                    <div class="row espacio"> 
                                        <div class="col-md-2">
                                            <asp:Label ID="Label20" CssClass="control-label-2" runat="server" Text="Guía Serie"></asp:Label>
                                            <asp:TextBox ID="txtGuiaSerie" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="Label21" CssClass="control-label-2" runat="server" Text="Guía Número"></asp:Label>
                                            <asp:TextBox ID="txtGuiaNro" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label30" CssClass="control-label-2" runat="server" Text="Fecha Guía"></asp:Label>
                                            <asp:TextBox ID="txtIngFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="txtIngFecha" Format="dd/MM/yyyy" PopupButtonID="txtIngFecha" ></cc1:CalendarExtender>
                                        </div>
                                    </div>                   
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                    <div class="row espacio">
                                        <div class="col-md-12">
                                        </div> 
                                        <div class="col-md-12">
                                        </div> 
                                    </div>
                                </ContentTemplate> 
                            </asp:UpdatePanel> 
                        </ContentTemplate> 
                    </cc1:TabPanel>
                </cc1:TabContainer> 
            </ContentTemplate>
        </asp:UpdatePanel> 
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
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
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
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
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
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
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

    <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarcaBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaModeloBA" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcionM">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="BtnBuscaMarca" class="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigoM">Código :</label>
                                                <div class="col-sm-3 col-xs-5">
                                                    <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="btnCancela" class="btn btn-default" runat="server" Text="Cancelar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancela" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-5 col-lg-offset-1">
                                            <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
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
                                                    <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarca" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnCancela" EventName="Click" />
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
    </div>

    <div id="ModalClasificacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label runat="server" ID="TituloPopupp" Text="Buscar Clasificación" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-6 col-lg-offset-4">
                                            <asp:Button ID="btnModalBuscarClas" class="btn btn-default" runat="server" Text="Buscar" />
                                            <asp:Button ID="BtnCerrarClasificacion" class="btn btn-default" runat="server" Text="Cancelar" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TreeView ID="trvClasificacion" runat="server" ShowExpandCollapse="true"
                                                ShowLines="True" PopulateNodesFromClient="true" ExpandDepth="0">
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <Nodes>
                                                </Nodes>
                                                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                                <ParentNodeStyle Font-Bold="False" />
                                                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
                                            </asp:TreeView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="TreeNodePopulate" />
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

    <div id="ModalBusqueda" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:Label ID="lblEtq_BusDestino" runat="server" Font-Size="14px" class="control-label2" Text="Busqueda de Proveedores" />
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
                                                <asp:Label ID="lblEtiq_Modal1" runat="server" Font-Bold="true"  Text="RUC:" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
                                                <div class="col-md-3 col-sx-5">
                                                    <asp:TextBox ID="txtRucTipoPers" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sx-2 col-lg-offset-3">
                                                    <asp:Button ID="btnCerrar2" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="lblEtiq_Modal2" runat="server" Font-Bold="true"  Text="Razón Social:" CssClass="col-md-3 col-sx-3 control-label"></asp:Label>
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

</asp:Content>

