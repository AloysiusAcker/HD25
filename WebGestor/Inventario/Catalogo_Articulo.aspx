<%@ Page Title="" Language="VB" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Catalogo_Articulo.aspx.vb" Inherits="Catalogo_Articulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        input[type="file"] {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
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
      
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Catálogo de Artículos" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                <asp:Button ID="BtnListarArticulos" runat="server" Text="Listar Todos los Artículos" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-3">
                <button type="button" id="mostrarBusqPrinc" class="form-control btn btn-default" data-toggle="modal" data-target="#ModalBusqueda" runat="server">
                    <span>Buscar</span>
                </button>
            </div> 
            <div class="col-md-3">
                <asp:Button ID="BtnNuevoArticulo" runat="server" Text="Nuevo Ingreso Artículo" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>        

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblImagen" runat="server" Text="Imagen :" Class="control-label-2" Visible="false"></asp:Label>
                        <asp:FileUpload ID="FileUpload1" Font-Names="file" runat="server" ClientIDMode="Static"
                            onchange="showimagepreview(this)" onclick="Ayuda" />
                        <label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" visible="false">Seleccionar Imagen</label>
                    </div>
                    <div class="col-md-3">
                        <input id="btnTomarFoto" type="button" value="Tomar Foto" runat="server" class="form-control btn btn-default" visible ="false"  />
                    </div>
                </div> 
                <div class="row">
                    <div class="col-md-6">
                        <img id="imagenCarga" alt="" src="" />
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnAgregarArticulo" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="lblCodigo" runat="server" Text="Cod. Artículo :" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtCodArt" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo de Artículo :" CssClass="control-label-2"></asp:Label>
                        <asp:DropDownList ID="DdlTipo" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="lblDetraccion" runat="server" Text=" % de Detracción :" CssClass="control-label-2" Visible="false"></asp:Label>
                        <asp:DropDownList ID="DdlDetraccion" runat="server" CssClass="form-control" Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblTipoBien" runat="server" Text="Tipo Bien :" CssClass="control-label-2" Visible="false"></asp:Label>
                        <asp:DropDownList ID="DdlTipoBien" runat="server" CssClass="form-control" Visible="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <asp:Label ID="lblCodClas" runat="server" Class="control-label" Visible="false"></asp:Label>
                        <asp:Label ID="lblCodMar" runat="server" Class="control-label" Visible="false" />
                        <asp:Label ID="lblCodMo" runat="server" Class="control-label" Visible="false"></asp:Label>
                    <asp:Label ID="lblCodDetaMod" runat="server" Class="col-lg-2 control-label" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="lblClasificacion" runat="server" Text="Clasificación :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtClasificacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label1" runat="server" Text="BusClas" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnBuscarClas" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="..." Visible="true" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblMarca" runat="server" Text="Marca :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label2" runat="server" Text="BusMarca" Class="control-label-2" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnBuscarMar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="..." Visible="true" />
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="lblModelo" runat="server" Text="Modelo :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label3" runat="server" Text="BusModelo" Class="control-label" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnBuscarMod" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="..." Visible="true" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblDetalleModelo" runat="server" Text="Detalle Modelo :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtDetalleModelo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label4" runat="server" Text="BusMod" Class="control-label" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnModeloDetalleMod" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="..." Visible="true" />
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                    <div class="col-lg-8">
                        <asp:Label ID="lblDescr" runat="server" Text="Descripción :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtDesc" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8">
                        <asp:Label ID="lblAbrev" runat="server" Text="Abreviatura :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtAbreviatura" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblNro" runat="server" Text="Nro. Parte :" Class="control-label"></asp:Label>
                        <asp:TextBox ID="txtNumP" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label ID="lblCodE" runat="server" Text="Cod. Específico :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtCodEsp" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="LblSku" runat="server" Text="SKU :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="TxtArtSku" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblUnidad" runat="server" Text="U. de Medida :" Class="control-label-2"></asp:Label>
                        <asp:DropDownList ID="DdlMedida" runat="server" Class="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label ID="lblPeso" runat="server" Text="Peso :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtPeso" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblVolumen" runat="server" Text="Volumen :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label ID="Label5" runat="server" Text="Volumen" Class="control-label" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnCalcularVolumen" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Calcular Volumen" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblAlto" runat="server" Text="Alto :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtAlto" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label ID="lblLargo" runat="server" Text="Largo :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtLargo" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblAncho" runat="server" Text="Ancho :" Class="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtAncho" runat="server" Class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
                    <div class="col-lg-3">
                        <asp:Label ID="Label6" runat="server" Text="Volumen" Class="control-label" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnAgregarArticulo" runat="server" Text="Agregar" class="form-control btn btn-default" />
                    </div> 
                    <div class="col-lg-3">
                        <asp:Label ID="Label7" runat="server" Text="Volumen" Class="control-label" ForeColor="White" ></asp:Label>
                        <asp:Button ID="BtnCancelarArituclo" runat="server" Text="Cancelar" class="form-control btn btn-default" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="SelectedNodeChanged" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="GvListaArticulos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="DdlTipo" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistro"  CssClass="control-label-2" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                    </div>
                </div> 
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="GvListaArticulos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="~/Icono/Editar_opt.png"></asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" CommandName="Eliminar" Text="Eliminar" ImageUrl="~/Icono/delete2_opt.png"></asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" CommandName="Imagen" Text="Imagen" ImageUrl="~/Icono/Image_opt.png"></asp:ButtonField>
                                <asp:ButtonField ButtonType="Image" CommandName="EliminarImagen" Text="EliminarImagen" ImageUrl="~/Icono/Delete_Image_opt.png"></asp:ButtonField>
                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Codigo Art." SortExpression="ART_CODIGO" />
                                <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo de Artículo" SortExpression="TIPO_ART" />
                                <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción Artículo" SortExpression="ART_DESCRIPCION" />
                                <asp:BoundField DataField="ART_SKU" HeaderText="SKU" SortExpression="ART_SKU" />
                                <asp:BoundField DataField="CLASIFICACION" HeaderText="Clasificacion" SortExpression="CLASIFICACION" />
                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="N° Parte" SortExpression="ART_CODEQUIVA" />
                                <asp:BoundField DataField="ART_UNIDAD_MEDIDA" HeaderText="Unidad de Medida" SortExpression="ART_UNIDAD_MEDIDA" />
                                <asp:BoundField DataField="ART_STOCK_MINIMO" HeaderText="Stock Mín." SortExpression="ART_STOCK_MINIMO" />
                                <asp:BoundField DataField="MARCA" HeaderText="Marca" SortExpression="MARCA" />
                                <asp:BoundField DataField="modelo" HeaderText="Modelo" SortExpression="modelo" />
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
                    <asp:AsyncPostBackTrigger ControlID="GvListaArticulos" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="BtnListarArticulos" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

    </div> 

    <div id="ModalBusqueda" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
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
                                                <label class="control-label col-sm-3 col-xs-12" for="id_numParteBA">SKU :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtSku" type="text" runat="server" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="trvClasificacion" EventName="SelectedNodeChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancela" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <div class="col-sm-5 col-xs-2 col-lg-offset-3">
                                            <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="btn btn-default" />
                                            <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="btn btn-default" />
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

    <div id="ModalImagen" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="form-group col-lg-10">
                                                <img id="imagenVisualizar" style="width: 100%; margin-left: 25px" src="#" />
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-sm-5 col-xs-2 col-lg-offset-5">
                                                    <asp:Button ID="BtnCerrarImagen" runat="server" Text="Cerrar" CssClass="btn btn-default" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaArticulos" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarImagen" EventName="Click" />
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarcaBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaModeloBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarMar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarMod" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnModeloDetalleMod" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcionM">Descripción :</label>
                                                <div class="col-sm-5 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2 col-lg-offset-1">
                                                    <asp:Button ID="btnBuscar" class="btn btn-default" runat="server" Text="Buscar" />
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
                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarMar" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscarMod" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnModeloDetalleMod" EventName="Click" />
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
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopupp" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarClas" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaClasificacionBA" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row form-group col-md-12">
                                        <div class="col-lg-6">
                                            <asp:Button ID="BtnSalirClas" class="btn btn-default" runat="server" Text="Cerrar" />
                                        </div>
                                        <div class="col-lg-6">
                                            <asp:Button ID="BtnCerrarClasificacion" class="btn btn-default" runat="server" Text="Listar" />
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
                                            <asp:AsyncPostBackTrigger ControlID="BtnBuscarClas" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
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

</asp:Content>

