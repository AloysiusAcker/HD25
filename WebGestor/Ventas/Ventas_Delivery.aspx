<%@ Page Title="" Language="VB" MasterPageFile="~/Ventas/PagPrincipal_Nuevo.master" AutoEventWireup="false" CodeFile="Ventas_Delivery.aspx.vb" Inherits="Ventas_Ventas_Delivery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
        .ajax__calendar_container { z-index : 1000 ; border: 1px solid #646464; background-color: White; color: Navy; width: 200px; }
        .ajax__calendar_body { width: 200px; font-size : 1em; color: black;}
             .ajax__calendar_header,
             .ajax__calendar_title,
             .ajax__calendar_dayname,
             .ajax__calendar_day { font-size : 1em; color: black;}
             .ajax__calendar_hover .ajax__calendar_day,
             .ajax__calendar_hover .ajax__calendar_month,
             .ajax__calendar_hover .ajax__calendar_year { font-size : 1em; color: red;}
        .ajax__calendar {
            position: relative;
            left: 0px !important;
            top: 0px !important;
            visibility: visible; display: block;
            }
        .ajax__calendar iframe
        {
            left: 0px !important;
            top: 0px !important;
        }
        
        /*.ajax__calendar_container { border: 1px solid #646464; background-color: White; color: Navy; width: 200px;}*/
            /* cuerpo */
            /* formato de la información mostrada */
            /* cuando colocamos el mouse en algún campo */

    </style>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-horizontal">                
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <asp:Label ID="Label5" runat="server" Text="Delivery" CssClass="Titulos"></asp:Label>
                </div>
            </div> 
            
            <div class="form-group col-lg-12">
                <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
            </div> 
            .
            <div class="form-horizontal">
                <div class="row">
                    <div class="col-lg-6">
                        <div id="DivDatosCliente" runat="server">              
                            <div class="form-group">
                                <asp:Label ID="LblEtq_1" runat="server" CssClass="col-lg-3 control-label-2" Text="Telefono"></asp:Label>                                
                                <div class="col-lg-5">
                                    <asp:TextBox ID="TxtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Button ID="BtnTelefono" runat="server" ControlStyle-CssClass="btn btn-default" Text="Telefonos"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblEtq_2" runat="server" CssClass="col-lg-3 control-label-2" Text="DNI"></asp:Label>                                    
                                <div class="col-lg-5">
                                    <asp:TextBox ID="TxtRuc" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <asp:RadioButtonList ID="RbDniRuc" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                                        <asp:ListItem class="radio-inline" Value="0" Selected="True" >DNI</asp:ListItem>
                                        <asp:ListItem class="radio-inline" Value="1">RUC</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <asp:Label ID="LblEtq_3" runat="server" CssClass="col-lg-3 control-label-2" Text="Nombres"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtNombres" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">                         
                                <asp:Label ID="LblEtq_4" runat="server" CssClass="col-lg-3 control-label-2" Text="Ap.Paterno"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtApePat" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
            
                            <div class="form-group">                    
                                <asp:Label ID="LblEtq_5" runat="server" CssClass="col-lg-3 control-label-2" Text="Ap.Materno"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtApeMat" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                                
                            <div class="form-group">
                                <asp:Label ID="LblEtq_6" runat="server" CssClass="col-lg-3 control-label-2" Text="Fec. Nac."></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtFecNac" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFecNac" Format="dd/MM/yyyy" PopupButtonID="TxtFecNac"></cc1:CalendarExtender>
                            </div>
            
                            <div class="form-group">
                                <asp:Label ID="LblEtq_7" runat="server" CssClass="col-lg-3 control-label-2" Text="E-Mail"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>        
                            
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="LblEtq_8" runat="server" CssClass="col-lg-3 control-label-2" Text="Razon Social"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEtq_9" runat="server" CssClass="col-lg-3 control-label-2" Text="Direccion"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtDireccion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEtq_10" runat="server" CssClass="col-lg-3 control-label-2" Text="Dpto"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlDpto"  CssClass="form-control" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEtq_11" runat="server" CssClass="col-lg-3 control-label-2" Text="Provincia"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlProvincia"  CssClass="form-control" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEtq_12" runat="server" CssClass="col-lg-3 control-label-2" Text="Distrito"></asp:Label>
                            <div class="col-lg-9">                        
                                <asp:DropDownList ID="DdlDistrito"  CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="LblEtq_13" runat="server" CssClass="col-lg-3 control-label-2" Text="Referencia"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtReferencia" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>

                    </div>                     
                </div>                
            </div>
            <div class="form-horizontal">
                <div class="row">
                    <div class="col-lg-6">
                        <div id="DivDatosDelivery" runat="server">        
                            <div class="form-group">
                                <asp:Label ID="LblEtq_15" runat="server" CssClass="col-lg-3 control-label-2" Text="Nro. Delivery"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtNroDelivery" runat="server" CssClass="form-control" ReadOnly ="true" ></asp:TextBox>
                                </div>
                            </div>
                        
                            <div class="form-group">
                                <asp:Label ID="LblEtq_16" runat="server" CssClass="col-lg-3 control-label-2" Text="Fecha"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtDFecha" runat="server" CssClass="form-control" ReadOnly ="true" ></asp:TextBox>
                                </div>
                            </div> 

                            <div class="form-group">
                                <asp:Label ID="LblEtq_17" runat="server" CssClass="col-lg-3 control-label-2" Text="Documento"></asp:Label>
                                <div class="col-lg-9">                        
                                    <asp:DropDownList ID="DdlTipoDoc" CssClass="form-control" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="LblEtq_18" runat="server" CssClass="col-lg-3 control-label-2" Text="Razon Social"/>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtBusRuc" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:Button ID="btnBusRuc" runat="server" ControlStyle-CssClass="btn btn-block" Text="..." />
                                </div>
                                <div class="col-lg-5">
                                    <asp:TextBox ID="txtBusRazon" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                   
                            <div class="form-group">
                                <asp:Label ID="LblEtq_19" runat="server" CssClass="col-lg-3 control-label-2" Text="Forma Pago"></asp:Label>
                                <div class="col-lg-9">                       
                                    <asp:DropDownList ID="DdlFormaPago" CssClass="form-control" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="LblEtq_20" runat="server" CssClass="col-lg-3 control-label-2" Text="Efectivo" Visible="false" ></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtEfectivo" runat="server" CssClass="form-control" Visible="false" ></asp:TextBox>                  
                                    <asp:DropDownList ID="DdlTipoTarj" CssClass="form-control" runat="server" Visible="false" >
                                    </asp:DropDownList>
                                </div>       
                            </div>                
                        </div>    
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <asp:Label ID="LblEtq_14" runat="server" CssClass="col-lg-3 control-label-2" Text="Tiempo Aprox."></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtTimeAprox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" >                            
                            <asp:Label ID="Label2" runat="server" CssClass="col-lg-3 control-label-2" Text="Sub-Total"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtDSubTotal" runat="server" style="text-align: right;" CssClass="form-control" ReadOnly ="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" >                            
                            <asp:Label ID="Label3" runat="server" CssClass="col-lg-3 control-label-2" Text="IGV"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtDIgv" runat="server" style="text-align: right;" CssClass="form-control" ReadOnly ="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" >                            
                            <asp:Label ID="Label1" runat="server" CssClass="col-lg-3 control-label-2" Text="Total"></asp:Label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="TxtDTotal" runat="server" style="text-align: right;" CssClass="form-control" ReadOnly ="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" >    
                            <asp:TextBox ID="TxtDCant" runat="server" style="text-align: right;" CssClass="form-control" visible="false"  ></asp:TextBox>
                        </div>
                    </div> 
                </div>                
            </div>
                
            <div class="form-group">
                <asp:Label ID="lblCodCliente" runat="server" CssClass="col-lg-2 control-label-2" Text=""  Visible="False"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblFCodCliente" runat="server" CssClass="col-lg-2 control-label-2" Text=""  Visible="False"></asp:Label>
            </div>

            <div class="form-group">
                <div class="col-lg-9">
                    <asp:Button ID="BtnLimpiar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Limpiar" />
                    <asp:Button ID="BtnRegistrar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Registrar" />
                    <asp:Button ID="Btnagregar" runat="server" ControlStyle-CssClass="btn btn-default" Text="Agregar" />
                </div> 
            </div>    
            
            <div class="form-group">
                <asp:Label ID="LblMensajeError" runat="server" ForeColor="Red" text=""></asp:Label>
            </div>
            
            <div class="form-group">
                <asp:Label ID="LblRegistro" runat="server" ForeColor="Maroon"  text=""></asp:Label>
            </div>

            <div class="form-horizontal">
                <div id="DivFlex" runat="server"  class="row form-group-lg">
                    <div class="col-lg-12">
                        <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="Quitar" Text="Quitar" ImageUrl="~/Icono/delete2_opt.png">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:ButtonField>
                                                                
                                <asp:BoundField DataField="art_descripcion" HeaderText="Descripcion Articulo">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="art_codigo" HeaderText="Codigo" >
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCant" runat="server" BorderWidth="0px" style="text-align: right;" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("cant") %>' AutoPostBack="True" OnTextChanged="txtCant_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Precio" HeaderText="Precio">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="Total" HeaderText="Total">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right"  VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>   

                                <asp:BoundField DataField="Precio_SinIgv" HeaderText="">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="Precio_Igv" HeaderText="">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField DataField="ART_COMPUESTO" HeaderText="">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                </asp:BoundField>

                            </Columns>
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                        </asp:GridView>
                    </div>                   
                   
                </div> 
            </div>                   

            <div id="DivStock" runat="server" visible ="false" >
                <asp:GridView id="GvStock" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>           
                        <asp:BoundField DataField="art_descripcion" HeaderText="Descripcion Articulo">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="art_codigo" HeaderText="Codigo" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                                
                        <asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="STOCK_UTILIZADO" HeaderText="Cantidad">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                                
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                </asp:GridView>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DdlDpto" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlProvincia" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlFormaPago" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DdlTipoDoc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="TxtTelefono" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="BtnLimpiar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnRegistrar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GvBusArt" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowDataBound" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="ModalBuscar" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">  
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>                                                     
                            <div class="form-group">
                                <asp:Label runat="server" ID="TituloPopupp" text="Busqueda de Articulos"/>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblBusCodigo" runat="server" CssClass="col-lg-3 control-label-2" Text="Codigo"></asp:Label>
                                <div class="col-lg-6">
                                    <asp:TextBox ID="TxtBusArtCodigo" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <asp:Button ID="BtnBuscarArt" runat="server" ControlStyle-CssClass="btn btn-default" Text="Buscar" />
                                </div>   
                            </div>
                            <div class="form-group">
                                <asp:Label ID="LblBusArt" runat="server" CssClass="col-lg-3 control-label-2" Text="Descripcion"></asp:Label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="TxtBusArt" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>                                                   
                            <div class="form-group">
                                <asp:Label runat="server" ID="LblMensajeError2" text=""/>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Btnagregar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="GvBusArt" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-6 col-lg-offset-5">
                                            <asp:Button ID="BtnCerrarModal" class="btn btn-default" runat="server" Text="Cerrar" />
                                        </div>
                                    </div>
                                    <div id="DivBusArticulo" >
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView id="GvBusArt" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>                                
                                                        <asp:ButtonField ButtonType="Image" CommandName="Seleccionar" Text="Seleccionar" ImageUrl="~/Icono/ok.png">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        </asp:ButtonField>
                                                                
                                                        <asp:BoundField DataField="Art_descripcion" HeaderText="Descripcion Articulo">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="art_codigo" HeaderText="Codigo" >
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="STOCK_ACTUAL" HeaderText="Stock">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="TIPOART" HeaderText="Tipo Art.">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                                        </asp:BoundField>                                

                                                        <asp:BoundField DataField="ART_COMPUESTO" >
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                                        <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ART_TIPO" >
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                                        <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                                        </asp:BoundField>

                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"></PagerStyle>
                                                </asp:GridView>                                                
                                            </ContentTemplate>                                            
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarArt" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarModal" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="Btnagregar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="GvBusArt" EventName="RowCommand" />
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

    <div id="ModalTelefonos" class="modal fade" data-backdrop="static" role="dialog" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="LblModalTelefono" Text=" Lista de Telefonos" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnTelefono" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <input type="hidden" name="metodo" value="registrarP" />
                        <div id="step9" class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2  col-lg-offset-8">
                                                    <asp:Button ID="BtnCerrarTelefono" runat="server" CssClass="btn btn-group" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <p id="LblTotalTelefonos" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros : </p>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <br />
                                                    <div id="TablaListaDocumentos" runat="server">
                                                        <asp:GridView ID="GvListaTelefono" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                            <Columns>
                                                                <asp:ButtonField CommandName="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                    <ItemStyle Height="10px" Width="10px" />
                                                                </asp:ButtonField>
                                                                <asp:BoundField DataField="PERSONA_RAZON_SOCIAL" HeaderText="Razon Social" />
                                                                <asp:BoundField DataField="DIRECCION_C_TELEFONO" HeaderText="Telefono"/>
                                                                <asp:BoundField DataField="DIRECCION_C_DESCRIPCION" HeaderText="Direccion"/>
                                                                <asp:BoundField DataField="PDPTO" HeaderText="Dpto."/>
                                                                <asp:BoundField DataField="PPROV" HeaderText="Provincia"/>
                                                                <asp:BoundField DataField="PDIST" HeaderText="Distrito"/>
                                                                <asp:BoundField DataField="PERSONA_RUC" HeaderText="RUC"/>
                                                                <asp:BoundField DataField="DIRECCION_C_REFERENCIA" HeaderText="Referencia"/>
                                                                <asp:BoundField DataField="PERSONA_APEPAT" HeaderText="Ap. Paterno"/>
                                                                <asp:BoundField DataField="PERSONA_APEMAT" HeaderText="Ap. Materno"/>
                                                                <asp:BoundField DataField="PERSONA_NOMBRES" HeaderText="Nombres"/>
                                                                <asp:BoundField DataField="PERSONA_EMAIL" HeaderText="Correo"/>
                                                                <asp:BoundField DataField="PERSONA_FECHANAC" HeaderText="">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                                                <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PERSONA_CODIGO" HeaderText="">
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="White"></HeaderStyle>
                                                                <ItemStyle ForeColor="White" Width="0px" BorderColor="White"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnTelefono" EventName="Click" />
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

