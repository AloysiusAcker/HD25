<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Ingreso_Series.aspx.vb" Inherits="Inventario_Inventario_Ingreso_Series" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">        
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Ingreso de N° de Series de Equipos a Recibir" CssClass="subTitulos"></asp:Label>
            </div>
            </div> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc1:TabContainer ID="ficha" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                        <cc1:TabPanel runat="server" HeaderText="Recepciones Generadas" ID="TabPanel1">
                            <ContentTemplate>       
                                <div class="row espacio">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label6" runat="server" Text="Recepciones Generadas" CssClass="subTitulos"></asp:Label>
                                    </div>
                                </div> 
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                        <div class="row espacio">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                                            </div> 
                                        </div>
                                        <div class="row espacio">
                                            <div class="col-md-9 col-xs-6">
                                                <asp:Label ID="LblEtiq_1" runat="server" Text="Almacén :" CssClass="control-label-2" />
                                                <asp:DropDownList ID="cboBusAlmacen" runat="server" CssClass="form-control" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-xs-6">
                                               <asp:Label ID="LblEtiq_2" runat="server" class="control-label-2" Text="Listar" forecolor="White" ></asp:Label>
                                               <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                                            </div> 
                                        </div>
                                        <div class="row espacio">
                                            <div class="col-md-3">
                                                <asp:Label ID="LblEtiq_3" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                                                <asp:TextBox ID="txtBusFecIni" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="txtBusFecIni" Format="dd/MM/yyyy" PopupButtonID="txtBusFecIni" ></cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="LblEtiq_4" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                                                <asp:TextBox ID="txtBusFecFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="txtBusFecFin" Format="dd/MM/yyyy" PopupButtonID="txtBusFecFin" ></cc1:CalendarExtender>
                                            </div>
                                            <div class="col-md-3 col-xs-6">
                                                <asp:TextBox ID="txtBusProvCodigo" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row espacio">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div>    
                                        <div class="row espacio">                    
                                            <div class="col-md-12">
                                                <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Ingreso" Text="Ingresar Series" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                            <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                        </asp:ButtonField>
                                                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                            <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="RECEP_CODIGO" HeaderText="Cod. Recep." SortExpression="RECEP_CODIGO" />
                                                        <asp:BoundField DataField="MOTIVO" HeaderText="Motivo" SortExpression="MOTIVO" />
                                                        <asp:BoundField DataField="FECHA_REG" HeaderText="F. Registro" SortExpression="FECHA_REG" />
                                                        <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc." SortExpression="TIPO_DOC" />
                                                        <asp:BoundField DataField="NRO_DOC" HeaderText="Nro. doc" SortExpression="NRO_DOC" />
                                                        <asp:BoundField DataField="FECHA_RECEPCION" HeaderText="F. Recepción" SortExpression="FECHA_RECEPCION" />
                                                        <asp:BoundField DataField="RUC" HeaderText="RUC" SortExpression="RUC" />
                                                        <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razón social" SortExpression="RAZON_SOCIAL" />
                                                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" SortExpression="ESTADO" />
                                                        <asp:BoundField DataField="ITEM" HeaderText="ESTADO" SortExpression="ITEM" />
                                                        <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. x Rec." SortExpression="CANT_XREC" />
                                                        <asp:BoundField DataField="CANT_REC" HeaderText="cant. Rec." SortExpression="CANT_REC" />
                                                        <asp:BoundField DataField="CANT_FALTA" HeaderText="Cant. Falta" SortExpression="CANT_FALTA" />
                                                        <asp:BoundField DataField="RECEP_OBSERVACION" HeaderText="Observación" SortExpression="RECEP_OBSERVACION" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>  
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel runat="server" HeaderText="Ingreso de Series" ID="TabPanel2">
                            <ContentTemplate>   
                                <div class="row espacio">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label8" runat="server" Text="Ingresar Series y Cantidades" CssClass="subTitulos"></asp:Label>
                                    </div>
                                </div>    
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>                         
                                        <div class="row espacio">
                                            <div class="col-md-12">
                                                <asp:Label ID="LblErrort" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
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
                                            <div class="col-md-6 col-xs-6">
                                                <asp:Label ID="LblEtiq_9" CssClass="control-label-2" runat="server" Text="Almacén"></asp:Label>
                                                <asp:TextBox ID="txtIngAlmacen" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-xs-6">
                                                <asp:Label ID="Label10" CssClass="control-label-2" runat="server" Text="..." ForeColor ="White" ></asp:Label>
                                                <asp:Button ID="btnRegresar" Text="Regresar" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                                            </div>
                                        </div>   
                                        <div class="row">
                                            <div class="col-md-10">
                                                <asp:Label ID="LblEtiq_10" CssClass="control-label-2" runat="server" Text="Proveedor"></asp:Label>
                                                <asp:TextBox ID="txtIngProveedor" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-xs-6">
                                                <asp:Label ID="Label9" CssClass="control-label-2" runat="server" Text="..." ForeColor ="White" ></asp:Label>
                                                <asp:Button  ID="btnEjecutar" text="Ejecutar" runat ="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
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
                                        <div id ="IngSeries" runat="server" visible="false" >                                           
                                            <div class="row espacio">
                                                <div class="col-lg-2">
                                                    <asp:Button ID="BtnCancelarIng" Text="Cancelar Ing." runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                                </div>
                                                <div class="col-lg-2">     
                                                    <asp:Button ID="btnBorrar" Text="Borrar" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btnGuardarS" Text ="Guardar Series"  runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btnExportar" Text="Exportar Excel" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button> 
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                        <ContentTemplate>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" >Seleccionar Imagen</label>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnGuardarExcel" />
                                                        </Triggers>
                                                    </asp:UpdatePanel> 
                                                    <asp:Button ID="btnGuardarExcel" Text="Guardar Excel" runat="server" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>  
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-2">
                                                    <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Artículo"></asp:Label>
                                                    <asp:TextBox ID="txtIngArtCodigo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                                                    <asp:TextBox ID="txtIngArticulo" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                </div>
                                            </div>      
                                            <div class="row espacio">
                                                <div class="col-lg-8">
                                                    <asp:GridView ID="FlexSeries" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Ing. Nro Serie">
                                                                <ItemTemplate>
                                                                    <asp:TextBox id="txtSerie" runat="server" CssClass="form-control" Text="">  
                                                                    </asp:TextBox> 
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
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
                                            </div>  
                                        </div> 
                                        <div class="row espacio"> 
                                            <div class="col-md-3">
                                                <asp:CheckBox ID="chkRecibirAcc" CssClass="checkbox checkbox-inline" Text="Recibir Todo" Font-Bold ="true" runat="server" AutoPostBack="True" Visible ="false"  />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Button ID="btnGuardarAccCant" Text="Guardar Cantidades" runat="server" ControlStyle-CssClass="form-control btn btn-default" Visible ="false" ></asp:Button>                                
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
                                            <div class="col-md-3">
                                                <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="Guía Serie"></asp:Label>
                                                <asp:TextBox ID="txtGuiaSerie" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="Label4" CssClass="control-label-2" runat="server" Text="Guía Número"></asp:Label>
                                                <asp:TextBox ID="txtGuiaNro" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="Label5" CssClass="control-label-2" runat="server" Text="Fecha Guía"></asp:Label>
                                                <asp:TextBox ID="txtIngFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="txtIngFecha" Format="dd/MM/yyyy" PopupButtonID="txtIngFecha" ></cc1:CalendarExtender>
                                            </div>
                                        </div>                    
                                        <div class="row espacio"> 
                                            <div class="col-md-3">
                                                <asp:GridView ID="FlexExportar" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView"> 
                                                   <Columns>
                                                        <asp:BoundField DataField="NRO_RECEPCION" HeaderText="Nro. Recepción"></asp:BoundField>
                                                        <asp:BoundField DataField="PROVEEDOR" HeaderText="Proveedor"></asp:BoundField>
                                                        <asp:BoundField DataField="ARTICULO_CODIGO" HeaderText="Cod. Artículo"></asp:BoundField>
                                                        <asp:BoundField DataField="ARTICULO" HeaderText="Artículo"></asp:BoundField>
                                                        <asp:BoundField DataField="SERIE_NUMERAR" HeaderText="Serie Numerar"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Nro. Serie"></asp:BoundField>
                                                        <asp:BoundField HeaderText="Nro. Placa"></asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>                    
                                            </div>            
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="FlexItemSerie" EventName="RowCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="btnEjecutar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnRegresar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnBorrar" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGuardarS" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnGuardarAccCant" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
                </Triggers>
            </asp:UpdatePanel>
    </div>
</asp:Content>

