<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Gastos.aspx.vb" Inherits="Inventario_Inventario_Gastos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style>
        /*input[type="file"] {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
        }*/
        
        .input {
            width: 0.1px;
            height: 0.1px;
            opacity: 0;
        }
        .estiloImagen {
            width: 200px;
            height: 250px;
        }  
        .espacio{
            padding: 3px 0px 3px 0px
        }
    </style>    

    <script type="text/javascript"> 
        
        function openModal()
        {
            $('#ModalGasto').modal('show');
        }

         function showimagepreview(input) {
            var fileInput = document.getElementById('FileUpload1');
            var NombreImg = document.getElementById('TxtNombreArchivo');
            var filePath = fileInput.value;
            var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
            var file = input.files[0];
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                
                if (!allowedExtensions.exec(filePath)) {
                    NombreImg = fileInput.files[0]
                    NombreImg.value = file.name;
                    //alert('Seleccione una imagen');
                    //fileInput.value = '';
                    //document.getElementById("imagenCarga2").setAttribute("src", "");
                    document.getElementById("imagenCarga2").setAttribute("src", "path/to/generic/file/icon.png");
                    return false;
                } else {

                    reader.onload = function (e) {
                        NombreImg = fileInput.files[0]
                        var img = new Image();
                        img.src = e.target.result;
                        img.onload = function () {
                            var canvas = document.createElement('canvas');
                            var ctx = canvas.getContext('2d');

                            // Redimensionar la imagen
                            var maxWidth = 600; // Anchura máxima
                            var maxHeight = 400; // Altura máxima
                            var width = img.width;
                            var height = img.height;

                            if (width > height) {
                                if (width > maxWidth) {
                                    height *= maxWidth / width;
                                    width = maxWidth;
                                }
                            } else {
                                if (height > maxHeight) {
                                    width *= maxHeight / height;
                                    height = maxHeight;
                                }
                            }
                            canvas.width = width;
                            canvas.height = height;
                            ctx.drawImage(img, 0, 0, width, height);

                            // Convertir el canvas a una imagen comprimida
                            var compressedImage = canvas.toDataURL('image/jpeg', 0.7); // Calidad de 0.7

                            // Mostrar la imagen comprimida
                             document.getElementById("imagenCarga2").setAttribute("src", compressedImage);
                        };
                    }
                    reader.readAsDataURL(file);
                    NombreImg.value = file.name; // Actualizar el campo de texto con el nombre del archivo
                }
            } else {
                document.getElementById("imagenCarga2").setAttribute("src", "");
            }
        }

//        function showimagepreview(input) {
//    var fileInput = document.getElementById('FileUpload1');
//    var NombreImg = document.getElementById('TxtNombreArchivo');
//    var filePath = fileInput.value;
//    var allowedExtensions = /(.jpg|.jpeg|.png|.gif|.jp|.jfif)$/i;
//    var file = input.files[0];

//    if (file) {
//        var reader = new FileReader();

//        if (!allowedExtensions.exec(filePath)) {
//            // Si el archivo no es una imagen, manejarlo de manera diferente
//            alert('Cargando un archivo no imagen');
//            NombreImg.value = file.name;

//            // Si deseas manejar el archivo de alguna manera específica, puedes hacerlo aquí
//            // Por ejemplo, podrías mostrar un ícono de archivo genérico

//            document.getElementById("imagenCarga2").setAttribute("src", "path/to/generic/file/icon.png");
//        } else {
//            // Si el archivo es una imagen
//            reader.onload = function (e) {
//                NombreImg = fileInput.files[0]
//                var img = new Image();
//                img.src = e.target.result;
//                img.onload = function () {
//                    var canvas = document.createElement('canvas');
//                    var ctx = canvas.getContext('2d');

//                    // Redimensionar la imagen
//                    var maxWidth = 600; // Anchura máxima
//                    var maxHeight = 400; // Altura máxima
//                    var width = img.width;
//                    var height = img.height;

//                    if (width > height) {
//                        if (width > maxWidth) {
//                            height *= maxWidth / width;
//                            width = maxWidth;
//                        }
//                    } else {
//                        if (height > maxHeight) {
//                            width *= maxHeight / height;
//                            height = maxHeight;
//                        }
//                    }
//                    canvas.width = width;
//                    canvas.height = height;
//                    ctx.drawImage(img, 0, 0, width, height);

//                    // Convertir el canvas a una imagen comprimida
//                    var compressedImage = canvas.toDataURL('image/jpeg', 0.7); // Calidad de 0.7

//                    // Mostrar la imagen comprimida
//                    document.getElementById("imagenCarga2").setAttribute("src", compressedImage);
//                    NombreImg.value = file.name;
//                };
//            }
//            reader.readAsDataURL(file);
//        }
//    } else {
//        document.getElementById("imagenCarga2").setAttribute("src", "");
//    }
//}

        function showimageprevieweeeee(input) {
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

    </script>

    <div class="container">

        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Inventario - Gastos" CssClass="Titulos" />
            </div> 
        </div>
        
        <div class="row espacio">
            <div class="col-md-6">
                <asp:Label ID="Label3" runat="server" Text="Usuario" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlBusUsuario" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
            </div>
            <div class="col-lg-2">
                <asp:Label ID="Label6" runat="server" Text="Usuario" CssClass="control-label-2" ForeColor="White" />
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-lg-2">
                <asp:Label ID="Label9" runat="server" Text="Usuario" CssClass="control-label-2" ForeColor="White" />
                <asp:Button ID="BtnIngresarGastos" runat="server" Text="Ingresar Gastos" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
        </div>
        
        <div class="row espacio">
            <div class="col-md-3">
                <asp:Label ID="Label8" runat="server" Text="Tipo Gasto" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlBusTipo" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label ID="Label24" runat="server" Text="Tipo Movilidad" CssClass="control-label-2" />
                <asp:DropDownList ID="DdlBusTipoMov" runat="server" CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>          
        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Label ID="Label5" runat="server" Text="Fecha Registro" CssClass="control-label-2" />
                <asp:TextBox ID="TxtBusFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtBusFecha" Format="dd/MM/yyyy" PopupButtonID="TxtBusFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2 col-xs-6">
                <asp:Label ID="Label25" runat="server" Text="Fecha Registro Hasta" CssClass="control-label-2" />
                <asp:TextBox ID="TxtBusFecha2" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="TxtBusFecha2" Format="dd/MM/yyyy" PopupButtonID="TxtBusFecha2" ></cc1:CalendarExtender>
            </div>
        </div> 

        
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:Label ID="LblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div> 
                <div class="row">                    
                    <div class="col-lg-12">
                        <asp:GridView ID="GvGastos" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" />
                                <asp:BoundField DataField="INVGASTOS_REGISTRO" HeaderText="Nro. Reg." SortExpression="INVGASTOS_REGISTRO"/>
                                <asp:BoundField DataField="FECHA_REG" HeaderText="Fecha Reg." SortExpression="FECHA_REG"/>
                                <asp:BoundField DataField="nombre" HeaderText="Personal" SortExpression="nombre"/>
                                <asp:BoundField DataField="GASTO_FECHA" HeaderText="Fecha Gasto" SortExpression="GASTO_FECHA" />
                                <asp:BoundField DataField="CECOSE_COD_INTERNO" HeaderText="Of. Código" SortExpression="CECOSE_COD_INTERNO" />
                                <asp:BoundField DataField="CECOSE_DESCRIPCION" HeaderText="Of. Descripción" SortExpression="CECOSE_DESCRIPCION" />
                                <asp:BoundField DataField="GASTO_TIPO" HeaderText="Tipo Gasto" SortExpression="GASTO_TIPO" />
                                <asp:BoundField DataField="GASTO_TIPO_MOV" HeaderText="Tipo Movilidad" SortExpression="GASTO_TIPO_MOV" />
                                <asp:BoundField DataField="GASTO_TIPO_DOC" HeaderText="Tipo Documento" SortExpression="GASTO_TIPO_DOC" />
                                <asp:BoundField DataField="INVGASTOS_DOC_SERIE" HeaderText="Serie " SortExpression="INVGASTOS_DOC_SERIE" />
                                <asp:BoundField DataField="INVGASTOS_DOC_NUMERO" HeaderText="Número " SortExpression="INVGASTOS_DOC_NUMERO" />
                                <asp:BoundField DataField="GASTO_MONEDA" HeaderText="Moneda" SortExpression="GASTO_MONEDA" />
                                <asp:BoundField DataField="INVGASTOS_IMPORTE" HeaderText="Importe" SortExpression="INVGASTOS_IMPORTE" />
                                <asp:TemplateField HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkPDF2" runat="server" Text="Ver"  NavigateUrl='<%# Eval("INVGASTOS_ARCHIVO", "~/Inventario/Gastos/{0}") %>' ></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#"ImagenGasto.ashx?Ruta=" + Session("Ruta_Emp") + "&id=" + HttpUtility.UrlEncode(Convert.ToString(If(Eval("INVGASTOS_REGISTRO") IsNot DBNull.Value, Eval("INVGASTOS_REGISTRO"), Nothing))) %>' Width="100" />
                                        </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div> 
                </div>           
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
    <div id="ModalObservaciones" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label ID="Label14" runat="server" Text="Observaciones del Registro de Gastos" CssClass="Titulos"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-9 col-xs-6">
                                    <asp:Label ID="lblObsEtiq2" runat="server" Text="Observación" CssClass="control-label"></asp:Label>
                                    <asp:TextBox ID="txtObsDetalle" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                </div>
                                <div class="col-md-3 col-xs-6">
                                    <asp:Label ID="Label16" runat="server" Text="Observación" CssClass="control-label" ForeColor="white"></asp:Label>
                                    <asp:Button ID="BtnOK" runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Ok" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>                                
                            <asp:AsyncPostBackTrigger ControlID="BtnGuardar2" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnOK" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
     <div id="ModalGasto" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label runat="server" ID="LblModalGasto" Text="Registro de Gastos" />
                        </ContentTemplate> 
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="BtnIngresarGastos" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnGuardar2" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="GvGastos" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step2">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <asp:Label ID="lblError" runat="server" ForeColor="Red" />
                                            </div>
                                            <div class="row estilo">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label15" runat="server" Text="Nro. Registro" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtNroRegistro" runat="server" CssClass="form-control" Text="" ReadOnly="true" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label4" runat="server" Text="Fecha Registro" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtFechaReg" runat="server" CssClass="form-control" Text="" ReadOnly="true" ></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaReg" Format="dd/MM/yyyy" PopupButtonID="TxtFechaReg" ></cc1:CalendarExtender>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="BtnGuardar2" runat="server" Text="Guardar" visible ="false" ControlStyle-CssClass="form-control btn btn-default"/>
                                                </div> 
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label22" runat="server" Text="Compra" CssClass="control-label-2" forecolor="white"  />
                                                     <asp:Button ID="BtnGuardar" runat="server" Text="Guardar"  ControlStyle-CssClass="form-control btn btn-default" OnClick="BtnGuardar_Click"/>
                                               </div> 
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label23" runat="server" Text="Compra" CssClass="control-label-2"  forecolor="white"   />
                                                    <asp:Button ID="BtnLimpiar" runat="server" Text="Cerrar" ControlStyle-CssClass="form-control btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row estilo">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Label ID="Label2" runat="server" Text="Usuario" CssClass="control-label-2" />
                                                    <asp:DropDownList ID="DdlUsuario" runat="server" CssClass="form-control" >
                                                    </asp:DropDownList>
                                                </div>
                                            </div>   
                                            <%--  <div class="row">
                                                <div class="col-lg-3 col-xs-6">
                                                    <asp:CheckBox ID="ChkCCostos" CssClass="checkbox checkbox-inline" Text="Centro de Costos" Font-Bold ="true" runat="server" AutoPostBack="True" />
                                                </div> 
                                            </div>  --%>
                                            <div class="row estilo">
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label19" runat="server" Text="Centro Costos" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtRuc" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1 col-xs-6">
                                                    <asp:Label ID="Label20" runat="server" Text="Persona" CssClass="control-label-2" ForeColor="White"  />
                                                    <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                                                </div>
                                                <div class="col-md-9 col-xs-6">
                                                    <asp:Label ID="Label21" runat="server" Text="Persona" CssClass="control-label-2" ForeColor="White"  />
                                                    <asp:TextBox ID="TxtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div> 
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12">
                                                    <asp:TextBox ID="TxtCodPersona" runat="server" visible="False" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row estilo">
                                                <div class="col-md-6 col-xs-6">
                                                    <asp:Label ID="Label1" runat="server" Text="Tipo de gastos" CssClass="control-label-2" />
                                                    <asp:DropDownList ID="DdlTipo" runat="server" CssClass="form-control" >
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-6 col-xs-6">
                                                    <asp:Label ID="LblMoneda" runat="server" Text="Tipo Movilidad" CssClass="control-label-2" />
                                                    <asp:DropDownList ID="DdlTipoMov" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div> 
                                            <div class="row estilo">
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label11" runat="server" Text="Documento" CssClass="control-label-2" />
                                                    <asp:DropDownList ID="DdlDoc" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label12" runat="server" Text="Serie" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtDocSerie" runat="server" Enabled ="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4 col-xs-6">
                                                    <asp:Label ID="Label13" runat="server" Text="Número" CssClass="control-label-2"  />
                                                    <asp:TextBox ID="TxtDocNumero" runat="server" Enabled ="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div> 
                                            <div class="row estilo">     
                                                <div class="col-md-3 col-xs-6">
                                                    <asp:Label ID="Label17" runat="server" Text="Moneda" CssClass="control-label-2" />
                                                    <asp:DropDownList ID="DdlMoneda" runat="server" CssClass="form-control" >
                                                    </asp:DropDownList>
                                                </div> 
                                                <div class="col-md-2 col-xs-6">
                                                    <asp:Label ID="Label7" runat="server" Text="Fecha del Gasto" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtFechaGasto" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaGasto" Format="dd/MM/yyyy" PopupButtonID="TxtFechaGasto" ></cc1:CalendarExtender>
                                                </div>
                                                <div class="col-md-4 col-xs-6">
                                                    <asp:Label ID="Label18" runat="server" Text="importe" CssClass="control-label-2"  />
                                                    <asp:TextBox ID="TxtImporte" runat="server"  CssClass="form-control" ></asp:TextBox>
                                                </div>
                                            </div> 
                                            <div class="row estilo">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Label ID="Label10" runat="server" Text="Glosa" CssClass="control-label-2" />
                                                    <asp:TextBox ID="TxtGlosa" runat="server" CssClass="form-control" Text="" TextMode="MultiLine"  ></asp:TextBox>
                                                </div>
                                            </div>    
                                            <br />
                                            <div class="row form-group col-md-12" id="div_imagen" runat="server" visible ="false"  >
                                                <label class="control-label col-sm-3 col-xs-12" for="id_observacion"></label>
                                                <div class="col-sm-6 col-xs-7">
                                                    <asp:Image ID="imagenCarga" runat="server" class="estiloImagen" visible="false"  />
                                                </div>
                                            </div>  
                                            <div class="row estilo">
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label26" CssClass="control-label-2" runat="server" Text="Carga Archivo"></asp:Label>
                                                    <asp:FileUpload ID="fileUpload" runat="server"  CssClass="form-control" />
                                                </div> 
                                            </div> 
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <div class="row espacio">
                                                        <div class="col-md-12 col-xs-12">
                                                            <asp:Label ID="lblImagen" runat="server" Text="Cargar Imagen:" Class="control-label" ></asp:Label> 
                                                            <asp:FileUpload ID="FileUpload1" Font-Names="file" runat="server" ClientIDMode="Static"
                                                                onchange="showimagepreview(this)" onclick="Ayuda" CssClass ="input"/>
                                                            <label id="FileNombre" runat="server" class="btn btn-default" for="FileUpload1" >Seleccionar Imagen</label>
                                                        </div>
                                                    </div> 
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="BtnGuardar" />
                                                </Triggers>
                                            </asp:UpdatePanel>  
                                            <div class="row espacio">
                                                <div class="col-sm-6 col-xs-7">
                                                    <img id="imagenCarga2" src="" alt=""/>
                                                </div>
                                            </div>                                              
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnIngresarGastos" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnGuardar2" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="GvGastos" EventName="RowCommand" />
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
                    <asp:Label runat="server" ID="TituloPopup" Text="Búsqueda de Centro de Costos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
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
                                                        <asp:BoundField DataField="CodUbi" SortExpression="CodUbi">
                                                            <ItemStyle ForeColor="White" Width="0.1px" />
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

