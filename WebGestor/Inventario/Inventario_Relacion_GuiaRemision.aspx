<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Relacion_GuiaRemision.aspx.vb" Inherits="Inventario_Inventario_Relacion_GuiaRemision" %>
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
        
              

    </script>

    <div class="container-fluid">

        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblTitulo" runat="server" Text="Inventario - Guías de Remisión" CssClass="Titulos"></asp:Label>
            </div>
        </div>

        <div class="row espacio">
            <div class="col-md-12">
                <asp:Label ID="LblError" runat="server" Text="" ForeColor="red"></asp:Label>
            </div>
        </div>        

        <div class="row espacio" >
            <div class="col-md-2">
                <asp:Label ID="LblEtiq6" CssClass="control-label-2" runat="server" Text="Guia Serie"></asp:Label>
                <asp:TextBox ID="TxtGuiaSerie" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="LblEtiq7" CssClass="control-label-2" runat="server" Text="Guia Numero"></asp:Label>
                <asp:TextBox ID="TxtGuiaNumero" runat="server" CssClass="form-control"></asp:TextBox>
            </div> 
            <div class="col-md-2">
            </div> 
            <div class="col-md-3">
            </div> 
            <div class="col-md-3">
                <asp:Label ID="LblEtiq10"  CssClass="control-label-2" runat="server" Text="Listar" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnListar" runat="server" Text="Listar" CssClass="form-control btn btn-default"/>
            </div> 
        </div> 
        <div class="row espacio">
            <div class="col-md-2">
                <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label4"  CssClass="control-label-2" runat="server" Text="Carga" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnCargaArchivo" runat="server" CssClass="form-control btn btn-default" Text="Carga Guias" OnClick="BtnCargaArchivo_Click" />
            </div> 
            <div class="col-md-3">
                <asp:Label ID="Label2"  CssClass="control-label-2" runat="server" Text="Enviar Sunat" ForeColor="white"></asp:Label>
                <asp:Button ID="BtnEnviarTodo" runat="server" Text="Enviar Sunat" CssClass="form-control btn btn-default"/>
            </div> 
        </div>
        <div class="row">
        </div>
        

        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
            <div class="row espacio">
                <div class="col-md-2">
                    <asp:Label ID="LblEtiq8" CssClass="control-label-2" runat="server" Text="Remitente:"></asp:Label>
                    <asp:DropDownList ID="DdlRemitente" runat="server" CssClass="form-control"  AutoPostBack="True">
                        <asp:ListItem Text="< Seleccionar >" Selected="True" />
                        <asp:ListItem Text="Almacén" Value="1" />
                        <asp:ListItem Text="Sessión CC" Value="2" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="LblEtiq9"  CssClass="control-label-2" runat="server" Text="Código"></asp:Label>
                    <asp:TextBox ID="TxtRemCodigo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="Label3"  CssClass="control-label-2" runat="server" Text="..." ForeColor="white"></asp:Label>
                    <asp:Button ID="BtnRemitente" runat="server"  CssClass="form-control btn btn-default"  Text="..." />
                </div> 
                <div class="col-md-7">
                    <asp:Label ID="LblEtiq11"  CssClass="control-label-2" runat="server" Text="Descripción"></asp:Label>
                    <asp:TextBox ID="txtRemDescripcion" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
            </div>     
            <div class="row">
                <div class="col-md-12">                
                    <asp:Label ID="lblCodRemitente" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>

                        

            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="DdlRemitente" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="row espacio">
            <div class="col-md-9">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                        <asp:Label ID="LblRegistro" runat="server" Text="" ></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>    
        
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
                <div class="row espacio">
                    <div class="col-md-12">
                        <asp:Label ID="LblQr" runat="server" Text="" ></asp:Label>
                    </div>
                </div>           
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="lblCodigo" runat="server" Text="Cod. Guia :" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="txtCodGuia" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-6">
                        <asp:Label ID="lblNombreimg" runat="server" Text="Nombre de la imagen" CssClass="control-label-2"></asp:Label>
                        <asp:TextBox ID="TxtNombreImagen" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div> 
                <asp:Label ID="lblCodigoGuia" runat="server" Text="" CssClass="control-label-2" Visible ="false" ></asp:Label>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblImagen" runat="server" Text="Imagen de la Guía:" CssClass="control-label-2"></asp:Label>                            
                        <asp:FileUpload ID="FileUpload1" Font-Names="file" runat="server" ClientIDMode="Static"
                            onchange="showimagepreview(this)" onclick="Ayuda" />
                        <label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" >Seleccionar Imagen</label>
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
            </Triggers>
        </asp:UpdatePanel>        
     
   
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
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
                <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>

        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate> 
                    <asp:Image ID="imgQRCode" runat="server" />
                    <asp:GridView ID="gridGuia" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkPag" runat="server" Height="20px" Width="1px" />                                                                      
                                </ItemTemplate>
                                <ControlStyle Width="20px"></ControlStyle>
                                <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="CrearXml" Text="Xml" ButtonType="Button" ControlStyle-CssClass=" btn btn-default" >
                                <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="QR" runat="server" Text="PDF" CommandName="QR"  ControlStyle-CssClass=" btn btn-default" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Imagen" Text="Imagen" ButtonType="Button" ControlStyle-CssClass=" btn btn-default" >
                                <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="Codigo_Guia" HeaderText="Codigo" SortExpression="Codigo_Guia" />
                            <asp:BoundField DataField="GUIREM_SERIE" HeaderText="Serie" SortExpression="GUIREM_SERIE" />
                            <asp:BoundField DataField="GUIREM_NUMERO" HeaderText="Numeración" SortExpression="GUIREM_NUMERO" />
                            <asp:BoundField DataField="Fecha_Guia" HeaderText="Fecha" SortExpression="Fecha_Guia" />
                            <asp:BoundField DataField="Hora_Guia" HeaderText="Hora" SortExpression="Hora_Guia" />
                            <asp:BoundField DataField="REMITENTE_CODINTERNO" HeaderText="Cod. Remitente" SortExpression="REMITENTE_CODINTERNO" />
                            <asp:BoundField DataField="REMITENTE_NOMBRE" HeaderText="Remitente" SortExpression="REMITENTE_NOMBRE" />
                            <asp:BoundField DataField="DESTINATARIO_CODINTERNO" HeaderText="Cod. Destinatario" SortExpression="DESTINATARIO_CODINTERNO" />
                            <asp:BoundField DataField="DESTINATARIO_NOMBRE" HeaderText="Destinatario" SortExpression="DESTINATARIO_NOMBRE" />
                            <asp:BoundField DataField="NUM_TICKET" HeaderText="Numero Ticket" SortExpression="NUM_TICKET" />
                            <asp:BoundField DataField="FECHA_RECEPCION_TICKET" HeaderText="Fecha Recep. Ticket" SortExpression="FECHA_RECEPCION_TICKET" />
                            <asp:BoundField DataField="GUIREM_OBSERVACION_SUNAT" HeaderText="Obs. SUNAT" SortExpression="GUIREM_OBSERVACION_SUNAT" />
                            <asp:TemplateField HeaderText="Archivo PDF">
                                <ItemTemplate>
                                   <asp:HyperLink ID="lnkPDF2" runat="server" Text="PDF" NavigateUrl='<%# Eval("GUIREM_ARCHIVO", "~/Inventario/GuiaRemision/{0}") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%#"GuiaHandler.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("GUIREM_CODIGO") IsNot DBNull.Value, Eval("GUIREM_CODIGO"), Nothing))) %>' Width="100" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="GUIA_IMG_NOMBRE" HeaderText="Nombre Imagen" SortExpression="GUIA_IMG_NOMBRE" />
                        </Columns>
                    </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div> 
    </div>
    
     <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate> 
                                <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="-" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step4">
                                <div class="panel panel-default">
                                    <div class="panel-body">   
                                        <div class="row">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnCerrar" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="btnCerrar_Click" />
                                            </div>
                                            <div class="col-md-4">
                                             </div>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row form-group col-md-12">
                                                    <asp:Label ID="lblRegDet" runat="server" CssClass="EstiloLabel" Font-Bold="True" Font-Italic="False" ForeColor="Maroon"></asp:Label>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-9">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate> 
                                                                <asp:GridView ID="gridDetalle" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                                        <asp:BoundField DataField="SERIE_NRO" HeaderText="Nro. Serie" SortExpression="SERIE_NRO" />
                                                                        <asp:BoundField DataField="PLACA_NRO" HeaderText="Nro. Placa" SortExpression="PLACA_NRO" />
                                                                        <asp:BoundField DataField="Cod_Articulo" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                                        <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Hora" SortExpression="Hora_Guia" />
                                                                        <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                                        <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                                   </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div> 
                                                <div class="row">
                                                    <div class="col-md-9">
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate> 
                                                            <asp:GridView ID="gridDetalleAcc" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Item" HeaderText="Item" SortExpression="Item" />
                                                                    <asp:BoundField DataField="Cod_Articulo" HeaderText="Cod. Artículo" SortExpression="Cod_Articulo" />
                                                                    <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="Nro. Parte" SortExpression="ART_CODEQUIVA" />
                                                                    <asp:BoundField DataField="ART_SKU" HeaderText="Sku"  SortExpression="ART_SKU"></asp:BoundField>
                                                                    <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripción del Artículo" SortExpression="ART_DESCRIPCION" />
                                                                    <asp:BoundField DataField="Cant" HeaderText="Cantidad" SortExpression="Cant" />
                                                                </Columns>
                                                            </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
                                                                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gridGuia" EventName="RowCommand" />
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


    <div id="ModalUbicacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="TituloPopup" runat="server" Text="Buscar" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnRemitente" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-horizontal">
                        <div class="modal-body" style="padding: 20px 10px 0;">
                            <div class="panel-group" id="step1">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
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
                                                        <asp:Button ID="BtnBusCancelar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="BtnBusCancelar" EventName="Click" />
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
                                                        <asp:AsyncPostBackTrigger ControlID="BtnBusCancelar" EventName="Click" />
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

</asp:Content>

