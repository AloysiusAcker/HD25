<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Recepcion_Listado.aspx.vb" Inherits="Inventario_Inventario_Recepcion_Listado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    
      <style>
            /*input[type="file"] {
                width: 0.1px;
                height: 0.1px;
                opacity: 0;
            }*/
            .estiloImagen {
                width: 200px;
                height: 150px;
            }  
        </style>
    

     <script type="text/javascript">
        function showimagepreview(input) {
            var fileInput = document.getElementById('FileUpload1');
            var NombreImg = document.getElementById('TxtNombreImagen');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                if (!allowedExtensions.exec(filePath)) {
                    alert('Seleccione una imagen');
                    fileInput.value = '';
                    document.getElementById("imagenCarga2").setAttribute("src", "");                    
                    return false;
                } else {
                    reader.onload = function (e) {
                        NombreImg = fileInput.files[0]
                        document.getElementById("imagenCarga2").setAttribute("src", e.target.result);                        
                    }
                    reader.readAsDataURL(input.files[0]);                    
                    return false;
                }
            } else {
                document.getElementById("imagenCarga2").setAttribute("src", "");                
            }
        }
        
        function MostrarImagenBien(input) {
            var fileInput = document.getElementById('FileUpload2');
            var NombreImg = document.getElementById('TxtNombreImagenGuia');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                if (!allowedExtensions.exec(filePath)) {
                    alert('Seleccione una imagen');
                    fileInput.value = '';
                    document.getElementById("imagenCargaGuia2").setAttribute("src", "");                    
                    return false;
                } else {
                    reader.onload = function (e) {
                        NombreImg = fileInput.files[0]
                        document.getElementById("imagenCargaGuia2").setAttribute("src", e.target.result);                        
                    }
                    reader.readAsDataURL(input.files[0]);                    
                    return false;
                }
            } else {
                document.getElementById("imagenCargaGuia2").setAttribute("src", "");                
            }
        }


    </script>


    <div class="container-fluid" >
        <div class="row top" >
            <div class="col-lg-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Recepciones en Almacén" CssClass="Titulos"></asp:Label>
            </div>
        </div> 
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblError" runat="server" Text="" CssClass="control-label-2"></asp:Label>
                    </div>
                </div>          
                <div class="row espacio">
                    <div class="col-lg-2 col-md-6 col-sm-6">
                        <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="form-control btn btn-default"/>
                    </div> 
                    <div class="col-lg-2 col-md-6 col-sm-6">
                        <asp:Button ID="BtnListar" runat="server" Text="Listar"  CssClass="form-control btn btn-default"/>
                    </div> 
                </div>          
                <div class="row espacio">
                    <div class="col-md-5">
                        <asp:Label ID="LblEtiq" runat="server" Text="Almacén :" CssClass="control-label-2" />
                        <asp:DropDownList ID="cboBusAlmacen" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-5">
                        <asp:Label ID="LblEtiq2" runat="server" Text="Estado :" CssClass="control-label-2" />
                        <asp:DropDownList ID="cboBusEstado" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row espacio">
                </div>
                <div class="row espacio">                
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkBusArticulo" CssClass="checkbox checkbox-inline" Text="Artículo" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="TxtBusArtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnBusArt" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox ID="TxtBusArtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">                
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkBusProveedor" CssClass="checkbox checkbox-inline" Text="Proveedor" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="TxtBusProvRuc" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnBusProv" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox ID="TxtBusProvNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
        
                <div class="row espacio">                
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkBusMotivo" CssClass="checkbox checkbox-inline" Text="Motivo" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-5">
                        <asp:DropDownList ID="CboBusMotivo" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>            
                </div>
                <div class="row espacio">   
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkClasificacion" CssClass="checkbox checkbox-inline" Text="Clasificación" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-5">
                        <asp:TextBox ID="TxtClasificacion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="BtnBuscarClas" runat="server" Text="..." CssClass="form-control btn btn-default" />
                    </div> 
                </div>
                <div class="row espacio">                
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkBusFecha" CssClass="checkbox checkbox-inline" Text="Fecha" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtBusFecIni" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtBusFecIni" PopupButtonID="txtBusFecIni" Format="dd/MM/yyyy"></cc1:CalendarExtender> 
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtBusFecFin" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender id="CalendarExtender2" runat="server" TargetControlID="txtBusFecFin" PopupButtonID="txtBusFecFin" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                    </div>              
                    <div class="col-md-2">
                        <asp:CheckBox ID="ChkOC" CssClass="checkbox checkbox-inline" Text="Orden de Compra" Font-Bold ="true" runat="server" AutoPostBack="True" />
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="TxtNroOC" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">  
                </div>
                <div class="row">
                    <asp:TextBox ID="txtBusProvCodigo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="lblCodClas" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>            
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="chkBusArticulo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkBusFecha" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkBusMotivo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkBusProveedor" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="ChkOC" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
            <ContentTemplate>          
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="lblCodigo" runat="server" Text="Cod. Recepcion:" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtCodRecepcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblImagenNroOC" runat="server" Text="Nro. Orden Compra:" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="TxtImagenNroOC" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:Label ID="lblNombreimg" runat="server" Text="Nombre de la imagen" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="TxtNombreImagen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 
                <asp:Label ID="lblCodigoRecep" runat="server" Text="" CssClass="control-label-2" Visible ="false" ></asp:Label>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblImagen" runat="server" Text="Imagen de la O/C:" CssClass="control-label-2"></asp:Label>                            
                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" Font-Names="file" runat="server" ClientIDMode="Static"
                            onchange="showimagepreview(this)" onclick="Ayuda" />
                    </div>
                </div> 
                <div class="row col-md-12" id="div_imagen" runat="server" visible ="false"  >
                    <label class="control-label col-sm-3 col-xs-12" for="id_observacion"></label>
                    <div class="col-sm-6 col-xs-7">
                        <asp:Image ID="imagenCarga" runat="server" class="estiloImagen" visible="false"  />
                    </div>
                </div>  
                <div class="row espacio">
                    <div class="col-md-6">
                        <img id="imagenCarga2" alt="" src="" />
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnGuargarImg" />
                <asp:PostBackTrigger ControlID="BtnCancelar" />
                <asp:PostBackTrigger ControlID="BtnCerrarImg" />
            </Triggers>
        </asp:UpdatePanel>    
        
   
        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Button ID="BtnGuargarImg" runat="server" Text="Guardar" class="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-3">
                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="form-control btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="flex" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>



        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
            <ContentTemplate>          
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="LblNroGuia" runat="server" Text="Nro. Guía:" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="TxtNroGuia" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:Label ID="LblNombreImagenGuia" runat="server" Text="Nombre de la imagen" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="TxtNombreImagenGuia" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="flex" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblImagen2" runat="server" Text="Imagen de la guía:" CssClass="control-label-2"></asp:Label>                            
                        <asp:FileUpload ID="FileUpload2" CssClass="form-control"  Font-Names="file" runat="server" ClientIDMode="Static"
                            onchange="MostrarImagenBien(this)" onclick="AyudaGuia" />
                    </div>
                </div> 
                <div class="row col-md-12" id="div_OC" runat="server" visible ="false"  >
                    <label class="control-label col-sm-3 col-xs-12" for="id_observacion"></label>
                    <div class="col-sm-6 col-xs-7">
                        <asp:Image ID="imageCargaGuia" runat="server" class="estiloImagen" visible="false"  />
                    </div>
                </div>  
                <div class="row espacio">
                    <div class="col-md-6">
                        <img id="imagenCargaGuia2" alt="" src="" />
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnGuargarImgGuia" />
                <asp:PostBackTrigger ControlID="BtnCancelarGuia" />
                <asp:PostBackTrigger ControlID="BtnCerrarImg" />
            </Triggers>
        </asp:UpdatePanel>    

        
   
        <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Button ID="BtnGuargarImgGuia" runat="server" Text="Guardar" class="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-3">
                        <asp:Button ID="BtnCancelarGuia" runat="server" Text="Cancelar" class="form-control btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="flex" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        
        <div class="row espacio">
            <div class="col-lg-3">
                <asp:Button ID="BtnCerrarImg" runat="server" Text="Cerrar" class="form-control btn btn-default" />
            </div> 
        </div> 

        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblReg" runat="server" Text="" CssClass="control-label-2" />                
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" AllowSorting="true" >
                            <Columns>
                                <asp:ButtonField CommandName="Anular" Text="Anular" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Ver" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Imagen" Text="Imagen" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="RECEP_CODIGO" HeaderText="C&#243;digo"  SortExpression="RECEP_CODIGO" ></asp:BoundField>
                                <asp:BoundField DataField="MOTIVO" HeaderText="Motivo"  SortExpression="MOTIVO" ></asp:BoundField>
                                <asp:BoundField DataField="FECHA_REG" HeaderText="Fec. Reg."  SortExpression="FECHA_REG" ></asp:BoundField>
                                <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc."  SortExpression="TIPO_DOC" ></asp:BoundField>
                                <asp:BoundField DataField="NRO_DOC" HeaderText="N° Doumento"  SortExpression="NRO_DOC" ></asp:BoundField>
                                <asp:BoundField DataField="RECEP_NRO_OC" HeaderText="N° Orden Compra"  SortExpression="RECEP_NRO_OC" ></asp:BoundField>
                                <asp:BoundField DataField="FECHA_RECEPCION" HeaderText="Fec. Recep."  SortExpression="FECHA_RECEPCION" ></asp:BoundField>
                                <asp:BoundField DataField="Tipo_Origen" HeaderText="Origen"  SortExpression="Tipo_Origen" />
                                <asp:BoundField DataField="RUC" HeaderText="Código"  SortExpression="RUC" ></asp:BoundField>
                                <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Descripción"  SortExpression="RAZON_SOCIAL" ></asp:BoundField>
                                <asp:BoundField DataField="Tipo_Destino" HeaderText="Destino"  SortExpression="Tipo_Destino" />
                                <asp:BoundField DataField="Destino_Cod" HeaderText="Código"  SortExpression="Destino_Cod" />
                                <asp:BoundField DataField="Destino" HeaderText="Descripción"  SortExpression="Destino" />
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" ></asp:BoundField>
                                <asp:BoundField DataField="ITEM" HeaderText="N&#176; Items" SortExpression="ITEM" ></asp:BoundField>
                                <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. x Rec." SortExpression="CANT_XREC" ></asp:BoundField>
                                <asp:BoundField DataField="CANT_REC" HeaderText="Cant. Rec." SortExpression="CANT_REC" ></asp:BoundField>
                                <asp:BoundField DataField="CANT_FALTA" HeaderText="Cant. Falta " SortExpression="CANT_FALTA" ></asp:BoundField>
                                <asp:BoundField DataField="RECEP_OBSERVACION" HeaderText="Observaci&#243;n" SortExpression="RECEP_OBSERVACION" ></asp:BoundField>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="OC">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"RecepcionHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("RECEP_CODIGO") IsNot DBNull.Value, Eval("RECEP_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderText="Guía">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#"RecepGuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("RECEP_CODIGO") IsNot DBNull.Value, Eval("RECEP_CODIGO"), Nothing))) %>' Width="100" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RECEP_IMAGEN_OC_NOMBRE" HeaderText="Nombre imagen OC" SortExpression="RECEP_IMAGEN_OC_NOMBRE" ></asp:BoundField>
                                <asp:BoundField DataField="RECEP_IMAGEN_GUIA_NOMBRE" HeaderText="Nombre Imagen Guia" SortExpression="RECEP_IMAGEN_GUIA_NOMBRE" ></asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                        </asp:GridView>
                    </div> 
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="chkBusArticulo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkBusFecha" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkBusMotivo" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkBusProveedor" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="ChkOC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
            </Triggers>
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">SKU</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-4 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                    <%--<asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="btn btn-default"/>--%>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row form-group col-md-12">
                                                        <div class="col-lg-12">
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


    <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
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
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
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


    <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
             <%--   <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">--%>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> 
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
     <%--           </div>--%>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                    <%--    <div class="panel-group" id="step4">--%>
                            <div class="panel panel-default">
                                <div class="panel-body">  
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="LblTituloModal" runat="server" class="ModalTitulos" Text="" /> 
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-4">
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:Button ID="btnCerrar" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="btnCerrar_Click" />
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="lblRegDet" runat="server" CssClass="EstiloLabel" Font-Bold="True" Font-Italic="False" ForeColor="Maroon"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="FlexDet" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Ver" Text="Detalle" ButtonType="Button" >
                                                                <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="ITEM" HeaderText="Item" />
                                                            <asp:BoundField DataField="ART_COD" HeaderText="Art. Cod." />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" />
                                                            <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                                            <asp:BoundField DataField="CANT_XREC" HeaderText="Cant. xRec." />
                                                            <asp:BoundField DataField="CANT_REC" HeaderText="Cant. Rec." />
                                                            <%-- <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" />
                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="LblRegistroDetItem" runat="server" CssClass="EstiloLabel" Font-Bold="True" Font-Italic="False" ForeColor="Maroon"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="GvDetalleItem" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="ART_COD" HeaderText="Art. Cod." />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" />
                                                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                                            <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" />
                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="FlexDet" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                 <%--       </div>--%>
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
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
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
                                            <asp:AsyncPostBackTrigger ControlID="btnModalBuscarClas" EventName="Click" />
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

    <div id="myModalAnular" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-12 col-sm-6" >
                                <asp:Label ID="Label3" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                            </div> 
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-6 col-sm-3">
                                            <asp:Button ID="btnAnularCompleto" runat="server" class="form-control btn btn-default" Text="Anular por completo" OnClick="btnAnularCompleto_Click" />
                                        </div>
                                        <div class="col-md-6 col-sm-3 ">
                                            <asp:Button ID="btnCambiarEstado" runat="server" class="form-control btn btn-default" Text="Volver a Estado Anterior" OnClick="btnCambiarEstado_Click" />
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
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Click" />
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


