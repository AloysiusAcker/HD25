<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_CargaFoto_xBiene.aspx.vb" Inherits="Inventario_Inventario_CargaFoto_xBiene" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    
      <style>
        input[type="file"] {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
        }

        .estiloImagen {
            width: 300px;
            height: 300px;
        }   

        .custom-image-container {
            margin-bottom: 20px;
        }

        .custom-image-container img {
            max-width: 100%;
            height: auto;
        }
    </style>

    <script type="text/javascript">
        function showimagepreview(input) {
            var fileInput = document.getElementById('FileUpload1');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                if (!allowedExtensions.exec(filePath)) {
                    alert('Seleccione una imagen');
                    fileInput.value = '';
                    document.getElementById("imagenCarga").setAttribute("src", "");
                    return false;
                } else {
                    reader.onload = function (e) {
                        document.getElementById("imagenCarga").setAttribute("src", e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                    return false;
                }
            } else {
                document.getElementById("imagenCarga").setAttribute("src", "");
            }
        }

        function VerImg(input, name) {
            var img = document.getElementById("imagenCarga").getAttribute("src");
            if (name.toString() != "") {
                if (img.toString() == "") {
                    $('#ModalImagen').modal('show');
                    document.getElementById("imagenVisualizar").setAttribute("src", input);
                }
            }
        } 
    </script>

     <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Imagenes de los bienes" CssClass="Titulos" />
            </div> 
        </div>
        <div class="row">
            <div class="col-md-3">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div>
        </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>    
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Tipo Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RbTodos" runat="server" Text="Todos" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén"  AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
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
                    <div class="col-md-3">
                        <asp:Label ID="Label4"  CssClass="control-label-2" runat="server" Text="Serie Nro"></asp:Label>
                        <asp:TextBox ID="TxtSerieNro" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicaCodigo" runat="server" Text="" visible="false" />
                        <asp:Label ID="LblUbicaCodigoInv" runat="server" Text="" visible="false" />
                        <asp:Label ID="LblSerieNumerar" runat="server" Text="" visible="false" />
                    </div> 
                </div>
                 <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="LblEtiq2"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                        <asp:TextBox ID="TxtArtCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="LblEtiq3"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnArtBuscar" runat="server"  CssClass="form-control btn btn-default" Text="..." />
                    </div> 
                    <div class="col-md-6">
                        <asp:Label ID="LblEtiq4"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                        <asp:TextBox ID="TxtArtDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                 </div>
                 <br />
                 <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" class="form-control btn btn-default" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblNombreimg"  CssClass="control-label-2" runat="server" Text="Nombre de la imagen"></asp:Label>
                        <asp:TextBox ID="TxtNombreImagen" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblImagen" runat="server" Text="Imagen :" Class="control-label-2" Visible="false"></asp:Label>
                                <asp:FileUpload ID="FileUpload1" Font-Names="file" runat="server" ClientIDMode="Static"
                                    onchange="showimagepreview(this)" onclick="Ayuda" />
                                <label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" visible="false">Seleccionar Imagen</label>
                            </div>
                        </div> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnAgregarArticulo" />
                    </Triggers>
                </asp:UpdatePanel>  
                <div class="row" id="div_imagen" runat="server" visible ="false"  >
                    <div class="col-md-6">
                        <img id="imagenCarga" src="" alt=""/>
                    </div>
                </div> 
                 <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="BtnAgregarArticulo" runat="server" Text="Guardar Imagen" class="form-control btn btn-default" />
                    </div> 
                    <div class="col-md-3">
                        <asp:Button ID="BtnCancelarArituclo" runat="server" Text="Cancelar" class="form-control btn btn-default" />
                    </div>
                </div>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvBusArticulo" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvListaBienes" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
             </Triggers>
         </asp:UpdatePanel>
      

         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
             <ContentTemplate>

                <div class="row">
                    <div class="col-lg-12">                                            
                        <asp:Label ID="lblRegistroInv" runat="server"  CssClass="control-label-2" />
                    </div>
                </div>  
                 <div class="row">
                    <div class="col-lg-12">
                        <asp:GridView ID="GvListaBienes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:ButtonField CommandName="Imagen" Text="Ingresar Imagen" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Lista" Text="Lista Imagenes" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="COD_ALMACEN" HeaderText="Cod. Interno" SortExpression="Oficina_Codigo" />
                                <asp:BoundField DataField="ALMACEN_NOMBRE" HeaderText="Oficina" SortExpression="Oficina" />
                                <asp:BoundField DataField="COD_ARTICULO" HeaderText="Cod. Artículo" SortExpression="COD_ARTICULO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Serie Nro." SortExpression="SERIE_NRO" />
                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Placa Nro." SortExpression="PLACA_NRO" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="SERIE_NUMERAR" SortExpression="SERIE_NUMERAR">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div> 
                 <div class="row">
                    <div class="col-lg-12">
                        <asp:GridView ID="GvImagenes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:ButtonField CommandName="Quitar" Text="Quitar Imagen" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="nro" HeaderText="Nro." SortExpression="nro" />
                                <asp:BoundField DataField="nro_serie" HeaderText="Nro." SortExpression="nro_serie" />
                                <asp:BoundField DataField="SERIE_IMAGEN_NOM" HeaderText="Nombre" SortExpression="SERIE_IMAGEN_NOM" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"SerieImagen.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("SERIE_IMAGEN_NRO") IsNot DBNull.Value, Eval("SERIE_IMAGEN_NRO"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div> 
             </ContentTemplate>
             <Triggers>                 
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvListaBienes" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvImagenes" EventName="RowCommand" />
             </Triggers>
         </asp:UpdatePanel>
         
         <asp:UpdatePanel ID="UpdatePanel5" runat="server">
             <ContentTemplate>

                <div class="row">
                    <div class="col-lg-12">                                            
                        <asp:Label ID="lblCantArt" runat="server"  CssClass="control-label-2" />
                    </div>
                </div>  
                 <div class="row">
                    <div class="col-lg-12">
                        <asp:GridView ID="gvArtPlacadosOf" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:ButtonField CommandName="Imagen" Text="Ingresar Imagen" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="Oficina_Codigo" HeaderText="Cod. Interno" SortExpression="Oficina_Codigo" />
                                <asp:BoundField DataField="Oficina" HeaderText="Oficina" SortExpression="Oficina" />
                                <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="cant" HeaderText="Cantidad" SortExpression="cant" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="ART_IMG_NOM" HeaderText="Nombre Imagen" SortExpression="ART_IMG_NOM" />
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="INVDET_INVENTUBIC_CODIGO" SortExpression="INVDET_INVENTUBIC_CODIGO">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UBICACION_CODIGO" SortExpression="UBICACION_CODIGO">
                                    <ItemStyle ForeColor="White" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div> 
                 <div class="row">
                    <div class="col-lg-12">
                        <asp:GridView ID="gvArtPlacados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                            <Columns>
                                <asp:ButtonField CommandName="Imagen" Text="Ingresar Imagen" >
                                <ItemStyle Width="10%" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="cant" HeaderText="Cantidad" SortExpression="cant" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                <asp:BoundField DataField="ART_IMG_NOM" HeaderText="Nombre Imagen" SortExpression="ART_IMG_NOM" />
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
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvListaBienes" EventName="RowCommand" />
             </Triggers>
         </asp:UpdatePanel>

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
                                                        ControlStyle-CssClass="form-control btn btn-block" />
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
                                                        ControlStyle-CssClass="form-control btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                                <label class="control-label col-sm-2 col-xs-12" for="id_modeloBA">Modelo :</label>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..."
                                                        ControlStyle-CssClass="form-control btn btn-block" />
                                                </div>
                                                <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                            </div>     
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">SKU :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                                    <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                    <asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
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
                                                        <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_IMG_NOM" HeaderText="Nombre Imagen" SortExpression="ART_IMG_NOM" />
                                                       <%-- <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("art_codigo") IsNot DBNull.Value, Eval("art_codigo"), Nothing))) %>' Width="100" />
                                                             </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnLimpiar" EventName="Click" />
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
    
     <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                            <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
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

