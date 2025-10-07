<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CRM/PagPrincipal_CRM.Master" CodeFile="Inventario_Verificar.aspx.vb" Inherits="Inventario_Verificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        input[type="file"] {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
        }
        .estiloImagen {
            width: 200px;
            height: 150px;
        }  
        
        .espacio{
            padding: 3px 0px 3px 0px
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
            var NombreImg = document.getElementById('TxtNombreImagen');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                if (!allowedExtensions.exec(filePath)) {
                    alert('Seleccione una imagen');
                    fileInput.value = '';
                    document.getElementById("imagenCarga3").setAttribute("src", "");                    
                    return false;
                } else {
                    reader.onload = function (e) {
                        NombreImg = fileInput.files[0]
                        document.getElementById("imagenCarga3").setAttribute("src", e.target.result);                        
                    }
                    reader.readAsDataURL(input.files[0]);                    
                    return false;
                }
            } else {
                document.getElementById("imagenCarga3").setAttribute("src", "");                
            }
        }

        function VerImg(input, name) {
            var img = document.getElementById("imagenCarga2").getAttribute("src");
            if (name.toString() != "") {
                if (img.toString() == "") {
                    $('#ModalImagen').modal('show');
                    document.getElementById("imagenVisualizar").setAttribute("src", input);
                }
            }
        } 
        
       
        $(document).ready(function () {
            $('#photoModal').on('shown.bs.modal', function (e) {
                var modal = $(this);
                var clickedThumbnail = $(e.relatedTarget);
                var photoPath = clickedThumbnail.data('photo');
                
                // Lógica para cargar la foto grande en el modal
                modal.find('.carousel-inner').html('<div class="carousel-item active"><img src="' + photoPath + '" class="d-block w-100" alt="Photo"></div>');


                $('#ModalArticulos').modal('show');
            });
        });
              

    </script>
         
    <asp:TextBox ID="txtSearch" runat="server" visible="false"  />
    <asp:Button ID="btnSearch" runat="server" Text="Buscar" OnClick="btnSearch_Click" visible="false" />
    <asp:Label ID="lblResults" runat="server" visible="false" />

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Verificación de Inventario" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="control-label-2" ForeColor="red" />
                    </div> 
                </div>
            </ContentTemplate>
            <Triggers>
                
                <asp:AsyncPostBackTrigger ControlID="txtBusArticulo" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-9 col-xs-6">
                        <asp:Label ID="LblInventario" runat="server" Text="Inventario :" CssClass="control-label-2" />
                        <asp:DropDownList ID="DdlInventario" runat="server" CssClass="form-control" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6">
                       <asp:Label ID="Label11" runat="server" class="control-label-2" Text="Listar" forecolor="White" ></asp:Label>
                       <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="LblUbicacion" runat="server" Text="Ubicación :" CssClass="control-label-2" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBAlmacen" runat="server" Text="Almacén" Checked="true" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBCentroC" runat="server" Text="Centro de Costo" AutoPostBack="True" />
                        <asp:RadioButton GroupName="ubicacion" ID="RBUbicaciones" runat="server" Text="Ubicaciones" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-xs-12">
                        <asp:TextBox ID="TxtCodigo" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 col-xs-11">
                        <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                    </div>
                    <div class="col-md-6 col-xs-12">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-12">                    
                        <input id="btnOpen" type="button" value="Datos Oficina" runat="server" class="form-control btn btn-default" />
                    </div> 
                </div>

                <div class="row">                    
                    <div class="col-md-12">
                        <asp:TextBox ID="TxtCodigoAyudaUbicacion" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TxtCodigoAyuda" runat="server" Width="102px" Visible="false"></asp:TextBox>
                    </div> 
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnCerrarInv" runat="server" Text="Cerrar Inventario" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnGuardarNoEncontrados" runat="server" Text="Guardar No Encontrados" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                    </div> 
                    <div class="col-md-3">
                        <asp:Button ID="BtnAcceso" runat="server" Text="Sin Acceso" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                    </div> 
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnNuevoEquipo" runat="server" Text="Nuevo" ControlStyle-CssClass="form-control btn btn-default" visible="false" />
                    </div> 
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnIniciarVerificacion" runat="server" Text="Iniciar Verificación" ControlStyle-CssClass="form-control btn btn-default"  Visible="false" />
                    </div>
                </div>
                <%--<br />--%>
                <div class="row">
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnCargaArchivo" runat="server" Text="Cargar Archivo" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-3 col-xs-6">
                        <asp:Label ID="LblNroPlaca" runat="server" class="control-label-2" Text="Nro. Placa :" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtNroPlaca" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-6">
                        <asp:Label ID="LblNroSerie" runat="server" class="control-label-2" Text="Nro. Serie :" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtNroSerie" runat="server" CssClass="form-control" Visible="false" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-6">
                        <asp:Label ID="Label1" runat="server" class="control-label-2" Text="Nro. ATM :" Visible="false"></asp:Label>
                        <asp:TextBox ID="TxtNroAtm" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-6">                    
                        <%--<asp:Button ID="BtnMostrarModal" runat="server" Text="Datos Oficina" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />--%>
                        <asp:Label ID="lblBusArticulo" runat="server" class="control-label-2" Text="Cód. Artículo :" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtBusArticulo" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                    </div> 
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblEtiquetaUbi" runat="server" Text="Ubicación :" CssClass="control-label-2"  Visible="false" />
                        <asp:DropDownList ID="ddlUbicacion" runat="server"  CssClass="form-control autocomplete" AutoPostBack="true"  Visible="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblIngObs"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnIngObs" runat="server" Text="Ing. Observación" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                    </div> 
                    <div class="col-md-3">
                        <asp:Label ID="lblCancelar"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                    </div> 
                </div>
                <div class="row" runat ="server" visible ="false" >          
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-md-6">
                                <asp:Label ID="Label6" CssClass="control-label-2" runat="server" Text="Archivo"></asp:Label>
                                <!-- Contenido dentro del UpdatePanel -->
                                <div class="mb-3">
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                                </div>
                            </div> 
                            <div class="col-md-2">
                                <asp:Label ID="Label10"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white"></asp:Label>
                                <asp:Button ID="BtnCargarArchivo" runat="server" CssClass="form-control btn btn-default" Text="Carga Placas" OnClick="BtnCargarArchivo_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnCargarArchivo" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div> 
                <div class="row">
                    <div class="col-md-12">
                    <asp:Label ID="TxtMensajeVerificar" runat="server" Visible="False" CssClass="control-label-2"></asp:Label>
                    </div>
                </div> 
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnGuardarNoEncontrados" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="DdlInventario" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBAlmacen" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBCentroC" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBUbicaciones" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="BtnNuevoEquipo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCerrarInv" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>        
            
            <div class="row">
                <div class="col-md-9">
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="LbltotalEquipos" runat="server"  CssClass="form-control" Enabled ="false" visible="false"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-9">
                    <asp:Label ID="lblObsArticulo" runat="server" class="control-label-2" Text="Observación Artículo" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtObsArticulo" runat="server" CssClass="form-control" Visible="false" AutoPostBack="True"></asp:TextBox>
                </div> 
                <div class="col-md-3">
                    <asp:Label ID="lblGuardar"  CssClass="control-label-2" runat="server" Text="Ubi" ForeColor="white" Visible="false" ></asp:Label>
                    <asp:Button ID="BtnGuardarObs" runat="server" Text="Guardar" ControlStyle-CssClass="form-control btn btn-default" Visible="false" />
                </div> 
            </div>            
                                    
            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="lblRegistro3" runat="server" class="control-label-2" Text="" ></asp:Label>
                </div> 
            </div>    
            <div class="row">                    
                <div class="col-md-12">
                    <asp:GridView ID="GvListaVerificarInventarioNuevos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                            <asp:TemplateField HeaderText="Desc. Artículo">
                                <ItemTemplate>
                                    <div class="two-lines-cell">
                                        <%# Eval("ART_DESCRIPCION") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                            <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                            <asp:TemplateField HeaderText="Est. Inventario">
                                <ItemTemplate>
                                    <div class="two-lines-cell">
                                        <%# Eval("ESTADO_INVENTARIO") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                            <asp:BoundField DataField="SERIE_CON_CAJA" HeaderText="Con Caja" SortExpression="SERIE_CON_CAJA" />
                            <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>  
            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="LblContador" runat="server" class="control-label-2" Text="" ></asp:Label>
                </div> 
            </div> 
            <div class="row">                    
                <div class="col-md-12">
                    <asp:GridView ID="gvListaTop5" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                            <%--<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />--%>
                            <asp:TemplateField HeaderText="Desc. Artículo">
                                <ItemTemplate>
                                    <div class="two-lines-cell">
                                        <%# Eval("ART_DESCRIPCION") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                            <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                            <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                            <asp:TemplateField HeaderText="Est. Inventario">
                                <ItemTemplate>
                                    <div class="two-lines-cell">
                                        <%# Eval("ESTADO_INVENTARIO") %>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                            <asp:BoundField DataField="SERIE_CON_CAJA" HeaderText="Con Caja" SortExpression="SERIE_CON_CAJA" />
                            <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                        </Columns>
                    </asp:GridView>
                </div> 
            </div>   
            
            
            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="lblRegistro2" runat="server" class="control-label-2" Text="" visible="false" ></asp:Label>
                </div> 
            </div>                          
            <div class="row">                    
                <div class="col-md-12">
                    <asp:GridView ID="GvListaVerificarInventarioOtros" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" Visible ="false" >
                        <Columns>
                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />
                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                                <asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />
                            <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                            <asp:BoundField DataField="SERIE_CON_CAJA" HeaderText="Con Caja" SortExpression="SERIE_CON_CAJA" />
                            <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                        </Columns>
                    </asp:GridView>
                </div> 
            </div>                                         
            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="lblRegistroCant" runat="server" class="control-label-2" Text="" ></asp:Label>
                </div> 
            </div>                  
            <div class="row">                    
                <div class="col-md-12">
                    <asp:Label ID="lblCodArticuloBus" runat="server" class="control-label-2" Text="" visible="false" ></asp:Label>
                </div> 
            </div> 
            <div class="row">
                <div class="col-md-12">                    
                    <asp:GridView ID="GvListaCantxArt" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:ButtonField CommandName="Detalle" Text="Detalle" />
                            <asp:BoundField DataField="Articulo" HeaderText="Cód. Artículo" SortExpression="Articulo" />
                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripcíon" SortExpression="ART_DESCRIPCION" />
                            <asp:BoundField HeaderText="Inventariado"  />
                            <asp:BoundField HeaderText="No Iventariado"  />
                            <asp:BoundField DataField="CANT" HeaderText="Cantidad" SortExpression="CANT" />
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
                                   Inventariado Ok
                                </a>
                            </h5>
                        </div>
                        <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                            <div class="card-body">                                 
                               <div class="row">                    
                                    <div class="col-md-12">
                                        <asp:Label ID="lblRegistraTodo" runat="server" class="control-label-2" Text="" ></asp:Label>
                                    </div> 
                                </div> 
                                <div class="row">                    
                                    <div class="col-md-12">
                                        <asp:GridView ID="GvListaVerificarInventario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                            <Columns>
                                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                <%--<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />--%>
                                                <asp:TemplateField HeaderText="Desc. Artículo">
                                                    <ItemTemplate>
                                                        <div class="two-lines-cell">
                                                            <%# Eval("ART_DESCRIPCION") %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                                <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                                                <asp:TemplateField HeaderText="Est. Inventario">
                                                    <ItemTemplate>
                                                        <div class="two-lines-cell">
                                                            <%# Eval("ESTADO_INVENTARIO") %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="AREA_NOMBRE" HeaderText="Ubicacion" SortExpression="AREA_NOMBRE" />
                                                <asp:BoundField DataField="SERIE_STATUSU" HeaderText="Stat. Sist." SortExpression="SERIE_STATUSU" />
                                                <asp:BoundField DataField="SERIE_CON_CAJA" HeaderText="Con Caja" SortExpression="SERIE_CON_CAJA" />
                                                <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
                                            </Columns>
                                        </asp:GridView>
                                    </div> 
                                </div>       

                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header" role="tab" id="section1HeaderId2">
                            <h5 class="mb-0">
                                <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId2" aria-expanded="false" aria-controls="section1ContentId2">
                                    No Inventariado
                                </a>
                            </h5>
                        </div>
                        <div id="section1ContentId2" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId2">
                            <div class="card-body">  
                                <div class="row">                    
                                    <div class="col-md-12">
                                        <asp:Label ID="lblContador2" runat="server" class="control-label-2" Text="" ></asp:Label>
                                    </div> 
                                </div> 
                                <div class="row">                    
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvListaNoInventariado" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                            <Columns>
                                                <asp:BoundField DataField="ART_CODIGO" HeaderText="Cod. Artículo" SortExpression="ART_CODIGO" />
                                                <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                <%--<asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Desc. Artículo" SortExpression="ART_DESCRIPCION" />--%>
                                                <asp:TemplateField HeaderText="Desc. Artículo">
                                                    <ItemTemplate>
                                                        <div class="two-lines-cell">
                                                            <%# Eval("ART_DESCRIPCION") %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                                                <asp:BoundField DataField="VERIFICAR" HeaderText="Verificado" SortExpression="VERIFICAR" />
                                                <%--<asp:BoundField DataField="ESTADO_INVENTARIO" HeaderText="Estado del Inventario" SortExpression="ESTADO_INVENTARIO" />--%>
                                                <asp:TemplateField HeaderText="Est. Inventario">
                                                    <ItemTemplate>
                                                        <div class="two-lines-cell">
                                                            <%# Eval("ESTADO_INVENTARIO") %>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SERIE_STATUSU" HeaderText="Stat. Sist." SortExpression="SERIE_STATUSU" />
                                                <asp:BoundField DataField="SERIE_CON_CAJA" HeaderText="Con Caja" SortExpression="SERIE_CON_CAJA" />
                                                <asp:BoundField DataField="SERIE_CAJA_NRO" HeaderText="Nro Caja" SortExpression="SERIE_CAJA_NRO" />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="TxtNroAtm" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnIniciarVerificacion" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </div>

    <div id="Modal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnBusca" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaAreaM" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaUbicacionM" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaMarcaBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnBuscaModeloBA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnNuevoEquipo" EventName="Click" />
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
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label class="control-label" for="id_codigo">Código :</label>
                                                    <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-md-9">
                                                    <label class="control-label" for="id_descripcion">Descripción :</label>
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                            </div>
                                            <div class="row espacio">     
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnBuscar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>                                           
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaU" EventName="RowCommand" />
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
                                                            <asp:BoundField DataField="CodUbi" SortExpression="CodUbi">
                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GvBusquedaU" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="CodInterno" HeaderText="Codigo" SortExpression="CodInterno" />
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                            <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                                <ItemStyle ForeColor="White" Width="0.1px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GvBusquedaM" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
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
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="BtnBuscaUbicacionM" EventName="Click" />
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

    <div id="ModalPregunta" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 25%;">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloPregunta" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="BtnNuevoEquipo" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="col-sm-8 col-xs-2 col-lg-offset-3" style="padding-left: 12%">
                                            <asp:Button ID="BtnSi" CssClass="btn btn-info" runat="server" Text="Sí" />
                                            <asp:Button ID="BtnNo" ControlStyle-CssClass="btn btn-info" runat="server" Text="No" />
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
    
    <div id="ModalBuscaArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Búsqueda de Artículos</h4>
                </div>
                <div class="modal-body" style="padding: 20px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_codArt">Código Artículo</label>
                                        <input class="form-control" id="TxtCodArticuloBA" type="text" runat="server" />
                                    </div>                                              
                                    <div class="col-md-6 col-xs-6 selectContainer">
                                        <label class="control-label" for="id_tipoArticuloBA">Tipo de Art :</label>
                                        <asp:DropDownList ID="DdlTipoBA" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
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
                                    <div class="col-md-10 col-xs-10">
                                        <label class="control-label" for="id_clasificacionBA">Clasificacíon</label>
                                        <input class="form-control" id="TxtClasificacionBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-xs-2">
                                        <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="C" forecolor="White" />
                                        <asp:Button ID="BtnBuscaClasificacionBA" runat="server" Text="..." ControlStyle-CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <div class="row">      
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_numParteBA">Número Parte</label>
                                        <input class="form-control" id="TxtNumParteBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_codEspecificoBA">Cod. Especif</label>
                                        <input class="form-control" id="TxtCodEspecificoBA" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-10 col-xs-10">
                                    <label class="control-label" for="id_marcaBA">Marca</label>
                                        <input class="form-control" id="TxtMarcaBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-xs-2">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Mar" forecolor="White" />
                                        <asp:Button ID="BtnBuscaMarcaBA" runat="server" Text="..." ControlStyle-CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <div class="row">            
                                    <div class="col-md-10 col-xs-10">
                                        <label class="control-label" for="id_modeloBA">Modelo</label>
                                        <input class="form-control" id="TxtModeloBA" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-xs-2">
                                        <asp:Label ID="Label5" runat="server" CssClass="control-label" Text="Mar" forecolor="White" />
                                        <asp:Button ID="BtnBuscaModeloBA" runat="server" Text="..." ControlStyle-CssClass="form-group btn btn-default" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Label ID="LblCodMarcaBA" runat="server" CssClass="control-label" Visible="false" />
                                    <asp:Label ID="LblCodModeloBA" runat="server" CssClass="control-label" Visible="false" />
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnBuscarBA" runat="server" Text="Buscar" CssClass="form-group btn btn-default" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnCerrarBA" runat="server" Text="Cerrar" CssClass="form-group btn btn-default" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnNuevoBA" runat="server" Text="Grabar" CssClass="form-group btn btn-default"/>
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnListarNE" runat="server" Text="De Otras Oficinas" CssClass="form-group btn btn-default"/>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblNroEqNoInv" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="gvEqNoInventariado" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                        <asp:BoundField DataField="SERIE_NUMERAR" SortExpression="SERIE_NUMERAR">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>                                                                        
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:Label ID="LblCantNE" runat="server" class="control-label-2" Text="" ></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row">                    
                                            <div class="col-md-12">
                                                <asp:GridView ID="gvNEUsuario" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                        <asp:BoundField DataField="CodUbicacion" HeaderText="Cod. Oficina" SortExpression="CodUbicacion" />
                                                        <asp:BoundField DataField="UBICACION" HeaderText="Oficina" SortExpression="UBICACION" />
                                                        <asp:BoundField DataField="SERIE_NUMERAR" SortExpression="SERIE_NUMERAR">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="INVNE_INVUBICA_CODIGO" SortExpression="INVNE_INVUBICA_CODIGO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="INVNE_UBICA_TIPO" SortExpression="INVNE_UBICA_TIPO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="INVNE_UBICA_CODIGO" SortExpression="INVNE_UBICA_CODIGO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div> 
                                        </div>   
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblCantArtReg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:GridView ID="GvBuscarArticulos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Art. Descripción" SortExpression="ART_DESCRIPCION" />
                                                        <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo"  SortExpression="TIPO_ART"></asp:BoundField>
                                                        <asp:BoundField DataField="ART_TIPO" SortExpression="ART_TIPO">
                                                            <ItemStyle ForeColor="White" Width="1px" />
                                                        </asp:BoundField>
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
                                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="SelectedNodeChanged" />
                                <asp:AsyncPostBackTrigger ControlID="GvBusquedaM" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscarBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnNuevoBA" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnListarNE" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="gvEqNoInventariado" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="gvNEUsuario" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="TxtDescripcion" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
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

    <div id="ModalArticulos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="TituloArticulo" Class="subTitulos" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnSi" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-3 col-xs-6">
                                                    <input id="Galeria" type="button" value="Abrir Galería" runat="server" class="form-control btn btn-success" />
                                                </div>
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Button ID="BtnDatosAd" ControlStyle-CssClass="form-control btn btn-success" runat="server" Text="Datos Adicionales" />
                                                </div>
                                                <div class="col-sm-3 col-xs-6">
                                                    <asp:Button ID="BtnAgregarArticulo" ControlStyle-CssClass="form-control btn btn-success" runat="server" Text="Agregar" />
                                                </div>
                                                <div class="col-sm-3 col-xs-6">
                                                    <asp:Button ID="BtnCerrarArticulo" ControlStyle-CssClass="form-control btn btn-success" runat="server" Text="Cerrar" />
                                                </div>
                                            </div> 
                                            <br />
                                            <div class="row form-group col-md-12">         
                                            </div>
                                            <br />
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_3">Nro. Atm :</label>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtNroAtmM" runat="server" class="form-control" ></asp:TextBox>
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_4">Cant. Equipos :</label>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="txtCantEq" runat="server" text="1" class="form-control" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
<%--                                                <label class="control-label col-sm-3 col-xs-12" for="id_3">Latitud :</label>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="TxtLatitud" runat="server" class="form-control" ></asp:TextBox>
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_4">longitud :</label>
                                                <div class="col-sm-3 col-xs-12">
                                                    <asp:TextBox ID="TxtLongitud" runat="server" text="1" class="form-control" ></asp:TextBox>
                                                </div> --%>               
<%--                                                <div>
                                                    <input type="text" id="txtLatitud" placeholder="Latitud"  />
                                                    <input type="text" id="txtLongitud" placeholder="Longitud"  />
                                                </div>--%>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_placaNro">Placa Nro :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtPlacaNroM" type="text" runat="server" />
                                                </div>
                                                <label class="control-label col-sm-2 col-xs-12" for="id_serieNro">Serie Nro :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <asp:TextBox ID="TxtSerieNroM" runat="server" class="form-control" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_estado">Con Caja:</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <asp:DropDownList ID="DdlEstadoCaja" runat="server" AutoPostBack="True" CssClass="form-control" />
                                                </div>  
                                                <label class="control-label col-sm-2 col-xs-12" for="id_serieNro">Nro Caja :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <asp:TextBox ID="TxtCajaNro" runat="server" class="form-control" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_estado">Estado :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <asp:DropDownList ID="DdlEstadoM" runat="server" AutoPostBack="True" CssClass="form-control" />
                                                </div>
                                                <div class="col-lg-3 col-xs-6">
                                                    <asp:CheckBox ID="ChkPlaca" CssClass="checkbox checkbox-inline" Text="Placa correlativa" Font-Bold ="true" runat="server" AutoPostBack="True" />
                                                </div>
                                                <div class="col-lg-3 col-xs-6">
                                                    <asp:CheckBox ID="ChkBienObs" CssClass="checkbox checkbox-inline" Text="Bien Observado" Font-Bold ="true" runat="server" AutoPostBack="True" />
                                                </div>  
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_codRelacionado">Nro. Parte :</label>
                                                <div class="col-sm-3 col-xs-7">
                                                    <input class="form-control" id="TxtCodRelacionadoM" type="text" runat="server"  disabled="disabled" />
                                                </div>
                                                <div class="col-lg-3 col-xs-6">
                                                    <asp:CheckBox ID="ChkInvOk" CssClass="checkbox checkbox-inline" Text="Placado" Font-Bold ="true" runat="server" AutoPostBack="True" />
                                                </div>     
                                                <div class="col-sm-3 col-xs-6">
                                                    <asp:CheckBox ID="ChkSinPlaca" CssClass="checkbox checkbox-inline" Text="Sin Placa" Font-Bold ="true" runat="server" AutoPostBack="True" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_responsable">Responsable :</label>
                                                <div class="col-sm-9 col-xs-7 selectContainer">
                                                     <input class="form-control" id="TxtResponsable" type="text" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_articulo">Artículo :</label>
                                                <div class="col-sm-2 col-xs-7">
                                                    <input class="form-control" id="TxtCodArticuloM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaArticuloM" runat="server" Text="..."
                                                        ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input class="form-control" id="TxtDescArticuloM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <asp:Label ID="LblArticuloM" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <div class="col-lg-5 col-lg-offset-3">
                                                    <asp:RadioButton GroupName="area" ID="RBAlmacenArea" runat="server" Text="Almacén" Checked="true" />
                                                    <asp:RadioButton GroupName="area" ID="RBCentroCArea" runat="server" Text="Centro de Costo" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_area">Area :</label>
                                                <div class="col-sm-2 col-xs-7">
                                                    <input class="form-control" id="TxtCodAreaM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaAreaM" runat="server" Text="..." ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input class="form-control" id="TxtDescAreaM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <asp:Label ID="LblCodAreaM" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_ubicacion">Ubicación :</label>
                                                <div class="col-sm-2 col-xs-7">
                                                    <input class="form-control" id="TxtCodUbicacionM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="col-sm-1 col-xs-2">
                                                    <asp:Button ID="BtnBuscaUbicacionM" runat="server" Text="..." ControlStyle-CssClass="btn btn-block" />
                                                </div>
                                                <div class="col-sm-6 col-xs-7">
                                                    <input class="form-control" id="TxtDescUbicacionM" type="text" runat="server" disabled="disabled" />
                                                </div>
                                                <asp:Label ID="LblCodUbicacionM" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_observacion">Observación :</label>
                                                <div class="col-sm-9 col-xs-7">
                                                    <input class="form-control" id="TxtObservacionM" type="text" runat="server" />
                                                </div>
                                                <asp:Label ID="lblSerieNumerar" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblSerieReal" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblPlacaReal" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblArtCodReal" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblEqxPlacar" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblEstadoInventario" runat="server" Visible="false"></asp:Label>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <asp:Label ID="lblNombreimg"  CssClass="control-label col-sm-3 col-xs-12" runat="server" Text="Nombre de la imagen"></asp:Label>
                                                <div class="col-sm-9 col-xs-7">
                                                    <input class="form-control" id="TxtNombreImagen" type="text" runat="server" />
                                                </div>  
                                             </div>
                                            <div class="row form-group col-md-12" id="div_imagen" runat="server" visible ="false"  >
                                                <label class="control-label col-sm-3 col-xs-12" for="id_observacion"></label>
                                                <div class="col-sm-6 col-xs-7">
                                                    <asp:Image ID="imagenCarga" runat="server" class="estiloImagen" visible="false"  />
                                                </div>
                                            </div>  
                                            <div class="row form-group col-md-12">
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="lblImagen" runat="server" Text="Imagen del Artículo :" Class="control-label col-sm-3 col-xs-12" ></asp:Label>
                                                        <div class="col-md-6 col-xs-7">
                                                            <asp:FileUpload ID="FileUpload1" Font-Names="file" runat="server" ClientIDMode="Static"
                                                                onchange="showimagepreview(this)" onclick="Ayuda" />
                                                            <label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" >Seleccionar Imagen</label>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="BtnGuardarImg" />
                                                    </Triggers>
                                                </asp:UpdatePanel>                                
                                                <div class="col-md-3 col-xs-7">
                                                    <asp:Button ID="BtnGuardarImg" ControlStyle-CssClass="btn btn-success" runat="server" Text="Guardar Imagen"/>
                                                </div>
                                            </div> 
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_observacion2"></label>
                                                <div class="col-sm-9 col-xs-7">
                                                    <img id="imagenCarga2" src="" alt=""/>
                                                </div>
                                            </div> 
                                            <div class="row form-group col-md-12">
                                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text="Imagen del Bien :" Class="control-label col-sm-3 col-xs-12" ></asp:Label>
                                                        <div class="col-md-6 col-xs-7">
                                                            <asp:FileUpload ID="FileUpload2" Font-Names="file" runat="server" ClientIDMode="Static"
                                                                onchange="MostrarImagenBien(this)" onclick="Ayuda" />
                                                            <label id="FileNombre2" runat="server" class="btn btn-default" for="FileUpload2" >Seleccionar Imagen</label>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="BtnGuardarImg2" />
                                                    </Triggers>
                                                </asp:UpdatePanel>                                
                                                <div class="col-md-3 col-xs-7"> 
                                                    <asp:Button ID="BtnGuardarImg2" ControlStyle-CssClass="btn btn-success" runat="server" Text="Guardar Imagen" />
                                                </div>
                                            </div> 
                                            <div class="row form-group col-md-12">
                                                <label class="control-label col-sm-3 col-xs-12" for="id_observacion5"></label>
                                                <div class="col-sm-9 col-xs-7">
                                                    <img id="imagenCarga3" src="" alt=""/>
                                                </div>
                                            </div> 
                                            <div class="row form-group col-md-12">
                                                <div class="col-lg-5">
                                                    <input class="form-control" id="TxtBuscarArticulo" type="text" runat="server"  Visible="false" />
                                                </div>
                                                <div class="col-lg-2">
                                                    <input class="form-control" id="TxtBuscarSerie" type="text" runat="server"  Visible="false" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="GvArticulo1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView"  visible="false"   >
                                                        <Columns>
                                                            <asp:BoundField DataField="ART_CODIGO" HeaderText="Art. Código" SortExpression="ART_CODIGO" />
                                                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                            <asp:BoundField DataField="ART_TIPO" HeaderText="Tipo" SortExpression="ART_TIPO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="GvArticulo2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" visible="false"  >
                                                        <Columns>
                                                            <asp:BoundField HeaderText=" " />
                                                            <asp:BoundField DataField="COD_ARTICULO" HeaderText="Art. Código" SortExpression="COD_ARTICULO" />
                                                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                            <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                            <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                            <asp:BoundField DataField="SERIE_COD_RELACIONADO" HeaderText="Cod. Relacionado" SortExpression="SERIE_COD_RELACIONADO" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="txtBusArticulo" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="TxtNroAtm" EventName="TextChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="GvBusquedaU" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnSi" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnNuevoEquipo" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="chkPlaca" EventName="CheckedChanged" />     
                                            <asp:AsyncPostBackTrigger ControlID="gvEqNoInventariado" EventName="RowCommand"/>   
                                            <asp:AsyncPostBackTrigger ControlID="gvNEUsuario" EventName="RowCommand" />    
                                            <asp:AsyncPostBackTrigger ControlID="TrvClasificacion" EventName="SelectedNodeChanged" /> 
                                            <asp:AsyncPostBackTrigger ControlID="DdlEstadoCaja" EventName="SelectedIndexChanged" />            
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

    <div id="myModalError" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-8">
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="LblMensajeError" runat="server" class="control-label2" Text=""/>
                                                </ContentTemplate>
                                                <Triggers>                                                        
                                                    <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 col-sm-3 ">
                                        </div>
                                        <div class="col-md-4 col-sm-3">
                                            <asp:Button ID="btnCerrarModal" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="btnCerrarModal_Click" />
                                        </div>
                                        <div class="col-md-4 col-sm-3 ">
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

    <div id="miModalDatos" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="lblTitulo" runat="server" Text="Datos de la Oficina" CssClass="Titulos"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblEtq1" runat="server" Text="Código" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCCod" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-9 col-xs-6">
                                    <asp:Label ID="lblEtq2" runat="server" Text="Descripción" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblEtq3" runat="server" Text="Cargo" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCCargo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-9 col-xs-6">
                                    <asp:Label ID="lblEtq4" runat="server" Text="Nombre" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblEtq9" runat="server" Text="Tipo" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCTipo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblEtq5" runat="server" Text="Anexo" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCAnexo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblEtq6" runat="server" Text="Teléfono" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblEtq7" runat="server" Text="Celular" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-xs-12">
                                    <asp:Label ID="lblEtq8" runat="server" Text="Correo Electrónico" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtCCCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>                                
                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <!-- Pie Modal -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>     
                </div>
            </div>
        </div>
    </div>

    <div id="ModalObservaciones" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label3" runat="server" Text="Observaciones de la Oficina" CssClass="Titulos"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="lblObsEtiq1" runat="server" Text="Nro. Observación" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtObsNro" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label8" runat="server" Text="Observación" CssClass="control-label" ForeColor="white"></asp:Label>
                                    <asp:Button ID="BtnObsCancelar" runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Cerrar" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9 col-xs-6">
                                    <asp:Label ID="lblObsEtiq2" runat="server" Text="Observación" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtObsDetalle" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label4" runat="server" Text="Observación" CssClass="control-label" ForeColor="white"></asp:Label>
                                    <asp:Button ID="BtnObsGuardar" runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvObservacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                        <Columns>
                                            <asp:BoundField DataField="OBSERV_NRO" HeaderText="Nro. Observacion" SortExpression="OBSERV_NRO" />
                                            <asp:BoundField DataField="OBSERV_DETALLE" HeaderText="Detalle de la Observación" SortExpression="OBSERV_DETALLE" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>                                
                            <asp:AsyncPostBackTrigger ControlID="BtnIngObs" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnObsGuardar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnObsCancelar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalSinAcceso" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label12" runat="server" Text="No acceso a ubicaciones" CssClass="Titulos"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label13" runat="server" Text="Nro. " CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="TxtSinAcceso_codigo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label14" runat="server" Text="..." CssClass="control-label" ForeColor="white"></asp:Label>
                                    <asp:Button ID="BtnSinAcceso_Cerrar" runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Cerrar" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9 col-xs-6">
                                    <asp:Label ID="Label15" runat="server" Text="Ubicación" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="TxtSinAcceso_Descripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label16" runat="server" Text="..." CssClass="control-label" ForeColor="white"></asp:Label>
                                    <asp:Button ID="BtnSinAcceso_Guardar" runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:GridView ID="GvSinAcceso" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                        <Columns>
                                            <asp:BoundField DataField="INVUBICA_CORRELATIVO" HeaderText="Nro. " SortExpression="INVUBICA_CORRELATIVO" />
                                            <asp:BoundField DataField="INVUBICA_DESCRIPCION" HeaderText="Detalle de la Ubicación" SortExpression="INVUBICA_DESCRIPCION" />
                                            <asp:BoundField DataField="INVUBICA_CODIGO" HeaderText="" SortExpression="INVUBICA_CODIGO" />

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>                                
                            <asp:AsyncPostBackTrigger ControlID="BtnAcceso" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSinAcceso_Guardar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnSinAcceso_Cerrar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="photoModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Galería de Fotos</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <!-- Contenido de la galería se agregará dinámicamente aquí -->
            <div id="photoGallery" class="carousel slide" data-ride="carousel">
              <div class="carousel-inner">
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="rptPhotos" runat="server">
                            <ItemTemplate>
                                <div class="photo-thumbnail" data-toggle="modal" data-target="#photoModal" data-photo='<%# Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())) %>'>
                                    <img src='data:image/png;base64,<%# Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())) %>' alt='<%# Eval("Descripcion") %>' class="img-thumbnail" />                            
                           
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                    <Triggers>                                
                        <asp:AsyncPostBackTrigger ControlID="GvBuscarArticulos" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrarClasificacion" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="TxtNroPlaca" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="TxtNroSerie" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtBusArticulo" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="TxtNroAtm" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="GvBusquedaU" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrarBA" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnSi" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnNuevoEquipo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnAgregarArticulo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="chkPlaca" EventName="CheckedChanged" />       
                        <asp:AsyncPostBackTrigger ControlID="gvEqNoInventariado" EventName="RowCommand"/>   
                    </Triggers>
                </asp:UpdatePanel>
              </div>
              <a class="carousel-control-prev" href="#photoGallery" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
              </a>
              <a class="carousel-control-next" href="#photoGallery" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div id="ModalDatosAdicionales" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Datos Adicionales del Equipo</h4>
                </div>

                <div class="modal-body" style="padding: 20px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">  
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_Usuario">Usuario</label>
                                        <input class="form-control" id="InputUsuario" name="Usuario" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_Sede">Sede</label>
                                        <input class="form-control" id="InputSede" name="Sede" type="text" runat="server" />
                                    </div>  
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_JefeArea">Jefe</label>
                                        <input class="form-control" id="InputJefe" name="Jefe" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9 col-xs-6">
                                        <label class="control-label" for="id_ubicacion2">Ubicacion</label>
                                        <input class="form-control" id="InputUbicacion2" name="Ubicacion" type="text" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-xs-12">
                                        <label class="control-label" for="id_Serie">N° Serie</label>
                                        <input class="form-control" id="InputSerie" name="Serie" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_Marca">Marca</label>
                                        <input class="form-control" id="InputMarca" name="Marca" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_Modelo">Modelo</label>
                                        <input class="form-control" id="InputModelo" name="Modelo" type="text" runat="server" />
                                    </div>
                                </div>

                                <div class="row">  
                                </div>

                                <div class="row">
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Perfil">Perfil</label>
                                        <input class="form-control" id="InputPerfil" name="Descripcion" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_hostName">Hostname</label>
                                        <input class="form-control" id="InputHostName" name="Modelo" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Procesador">Procesador</label>
                                        <input class="form-control" id="InputProcesador" name="Procesador" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Ram">RAM</label>
                                        <input class="form-control" id="InputRam" name="Ram" type="text" runat="server" />
                                    </div> 
                                </div>

                                <div class="row">     
                                </div>

                                <div class="row">
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Hdd">HDD</label>
                                        <input class="form-control" id="InputHdd" name="HDD" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_SO">SO</label>
                                        <input class="form-control" id="InputSO" name="SO" type="text" runat="server" />
                                    </div>  
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Situacion">Situación</label>
                                        <input class="form-control" id="InputSituacion" name="Situacion" type="text" runat="server" />
                                    </div>  
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Estado">Estado</label>
                                        <input class="form-control" id="InputEstado" name="Estado" type="text" runat="server" />
                                    </div> 
                                </div>
                	
                                <div class="row">
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Area">Area</label>
                                        <input class="form-control" id="InputArea" name="area" type="text" runat="server" />
                                    </div>
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Ceco">CECO</label>
                                        <input class="form-control" id="InputCeco" name="ceco" type="text" runat="server" />
                                    </div>  
                                    <div class="col-md-12">
                                        <label class="control-label" for="id_Ubicacion">Ubicación técnica</label>
                                        <input class="form-control" id="InputUbicacion" name="Ubicacion" type="text" runat="server" />
                                    </div>  
                                </div>

                                <div class="row">
                                </div>

                                <div class="row">
                                </div>

                                <div class="row">
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Teclado">Teclado N° Serie</label>
                                        <input class="form-control" id="InputTecladoSerie" name="TecladoSerie" type="text" runat="server" />
                                    </div>  
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Mouse">Mouse N° Serie</label>
                                        <input class="form-control" id="InputMouseSerie" name="MouseSerie" type="text" runat="server" />
                                    </div> 
                                </div>
                                
                                <div class="row">
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_SericeTag">Serice Tag</label>
                                        <input class="form-control" id="InputSericeTag" name="SericeTag" type="text" runat="server" />
                                    </div>  
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_EquipoSuperior">Eq. Superior</label>
                                        <input class="form-control" id="InputEquipoSuperior" name="EquipoSuperior" type="text" runat="server" />
                                    </div> 
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6 col-xs-3">
                                        <asp:Button ID="BtnCerrarDatosAd" runat="server" Text="Cerrar" ControlStyle-CssClass="form-control btn btn-success" />
                                    </div>
                                    <div class="col-md-6 col-xs-3">
                                        <asp:Button ID="BtnGuardarDatosAd" runat="server" Text="Grabar" ControlStyle-CssClass="form-control btn btn-success"/>
                                    </div>
                                </div>   
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnCerrarDatosAd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnGuardarDatosAd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnDatosAd" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
