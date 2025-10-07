<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Person_Control_EntSal.aspx.vb" Inherits="Person_Control_EntSal" title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">

        .imagen {
            flex-shrink: 0;  /* Evita que la imagen se reduzca demasiado */
        }

        .imagen-perfil {
            border-radius: 10px;  /* Opcional: Bordes redondeados */
            box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.2);  /* Sombra elegante */
        }     
    </style>

    <script type="text/javascript">

       let stream = null; // Variable global para almacenar el flujo de la cámara

        // Función para abrir la cámara
        function abrirCamara() {
            if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
                return navigator.mediaDevices.getUserMedia({ video: true })
                    .then(function (mediaStream) {
                        stream = mediaStream; // Guardar el flujo para detenerlo después
                        const video = document.getElementById("video");
                        video.srcObject = stream; // Asignar el flujo al elemento <video>
                        video.play();
                    })
                    .catch(function (err) {
                        console.error("Error al abrir la cámara:", err);
                        alert("No se pudo acceder a la cámara. Detalles: " + err.message);
                    });
            } else {
                alert("Tu navegador no soporta la cámara.");
                return Promise.reject("No se puede abrir la cámara.");
            }
        }

        // Función para capturar una foto automáticamente
        function tomarFoto() {
            if (!stream) {
                alert("No se puede tomar la foto porque la cámara no está activa.");
                return;
            }
            const canvas = document.getElementById("canvas");
            const video = document.getElementById("video");
            const contexto = canvas.getContext("2d");

            // Dibujar el contenido del video en el canvas
            contexto.drawImage(video, 0, 0, canvas.width, canvas.height);

            // Convertir la imagen a Base64 y enviarla al servidor
            const dataURL = canvas.toDataURL("image/png");
            document.getElementById("<%= hfImagen.ClientID %>").value = dataURL;

            cerrarCamara(); // Cerrar la cámara después de tomar la foto
        }

        // Función para cerrar la cámara
        function cerrarCamara() {
            if (stream) {
                stream.getTracks().forEach(function (track) {
                    track.stop(); // Detener cada pista del flujo
                });
                stream = null;
                const video = document.getElementById("video");
                video.srcObject = null; // Limpiar el video
            }
        }

        // Función principal que se ejecuta al cargar la página
        function iniciarCaptura() {
            abrirCamara().then(() => {
                // Esperar 2 segundos para que la cámara esté lista antes de capturar la foto
                setTimeout(tomarFoto, 2000);
            });
        }

        function obtenerGeolocalizacion() {

            if (navigator.geolocation) {
                try {
                    navigator.geolocation.getCurrentPosition(
                        function (position) {
                                var geoInfoLatitud = position.coords.latitude;
                                document.getElementById("txtGeoInfoLat").value = geoInfoLatitud;
                                var geoInfoLongitud = position.coords.longitude;
                                document.getElementById("txtGeoInfoLon").value = geoInfoLongitud;

                            document.getElementById('<%= hfLatitud.ClientID %>').value = geoInfoLatitud;
                            document.getElementById('<%= hfLongitud.ClientID %>').value = geoInfoLongitud;
                        },
                        function (error) {
                            console.log(error);
                            document.getElementById("txtGeoInfo").value = "Error al obtener la geolocalización.";
                        }
                    );
                } catch (e) {
                    console.log(e);
                    document.getElementById("txtGeoInfo").value = "Error al obtener la geolocalización.";
                }
            } else {
                document.getElementById("txtGeoInfo").value = "La geolocalización no está soportada por este navegador.";
            }
                   
        }
        window.onload = iniciarCaptura();
        // Llamar a la función al cargar la página
        window.onload = obtenerGeolocalizacion;





    </script>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="LblEtiq1" runat="server" Text="Control del Personal" CssClass="Titulos" />
                </div> 
            </div>
            <div class="row espacio">
                <div class="col-md-12">   
                    <asp:Label ID="LblError" runat="server"  CssClass="control-label-2"  Text="" forecolor="Red"></asp:Label>
                </div> 
            </div>  
            <div class="row espacio">
                <div class="col-md-2">
                    <asp:Label ID="lblFecha" runat="server" CssClass="control-label-2"  Text="Fecha Sistema"></asp:Label>
                    <asp:TextBox ID="txtFSistema" runat="server"  CssClass="form-control" ReadOnly="True"></asp:TextBox>
                </div> 
                <div class="col-md-2">
                    <asp:Label ID="lblHora" runat="server"  CssClass="control-label-2"  Text="Hora Sistema" ></asp:Label>
                    <asp:TextBox id="txtHSistema" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox> 
                </div> 
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server"  CssClass="control-label-2"  Text="Latitud" ></asp:Label>
                        <input type="text" id="txtGeoInfoLat" class="form-control" />
                </div> 
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server"  CssClass="control-label-2"  Text="Longitud" ></asp:Label>
                    <input type="text" id="txtGeoInfoLon"  class="form-control" />
                </div>
                <div runat="server" visible ="false"  class="col-md-2">
                    <input type="text" id="txtGeoInfo"  />
                </div> 
                <asp:HiddenField ID="hfLatitud" runat="server" />
                <asp:HiddenField ID="hfLongitud" runat="server" />
            </div>     
                

            <div class="row espacio">
                <div class="col-md-3">
                    <!-- Canvas para mostrar la foto capturada -->
                    <canvas id="canvas" width="200" style="border: 0px"></canvas>
                </div>
                <div class="col-md-3">
                    <video id="video" width="200" autoplay></video>
                </div>
            </div>      

            <div id="dvFoto" runat="server">
                <div class="row espacio">
                </div>
                <div class="row espacio">
                    <div class="col-md-3">
                        <!-- Campo oculto para guardar la imagen en base64 -->
                        <asp:HiddenField ID="hfImagen" runat="server" />
                    </div>
                </div>
            </div> 
                
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row espacio">
                        <div class="col-md-2 col-xs-6">
                            <div class="imagen">
                                <asp:Image ID="imgUsuario" runat="server" Width="150px" Height="150px"  CssClass="imagen-perfil" />
                            </div>                        
                        </div>
                    </div> 

                    <div class="row espacio">
                        <div class="col-md-2">
                            <asp:Label ID="Label6" runat="server"  CssClass="control-label-2"  Text="Cod. Personal" ></asp:Label>
                            <asp:TextBox id="txtCodigo" runat="server" class="form-control"></asp:TextBox> 
                        </div> 
                        <div class="col-md-1">
                            <asp:Label ID="Label4" runat="server"  CssClass="control-label-2"  Text="Bus" forecolor="White"></asp:Label>
                            <asp:Button ID="btnBuscar" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" Font-Overline="True" />
                        </div> 
                    </div>
                    <div class="row espacio">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server"  CssClass="control-label-2"  Text="Password" ></asp:Label>
                            <asp:TextBox id="txtPassword" runat="server" class="form-control" TextMode="Password"></asp:TextBox> 
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server"  CssClass="control-label-2"  Text="Verifica"  forecolor="White"></asp:Label>
                            <asp:Button ID="btnVerificar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Verificar" />
                        </div> 
                    </div>
                    <div class="row espacio">
                        <div class="col-md-5">
                            <asp:Label ID="lblNombres" runat="server"  CssClass="control-label-2"  Text="Nombres y Ape." ></asp:Label>
                            <asp:TextBox id="txtNombApe" runat="server" class="form-control" Text=""></asp:TextBox> 
                        </div> 
                    </div>
                    <div class="row espacio">
                        <div class="col-md-3">
                            <asp:Label ID="Label8" runat="server"  CssClass="control-label-2"  Text="Permiso" forecolor="White"></asp:Label>
                            <asp:RadioButtonList id="lstPermiso" runat="server"  OnSelectedIndexChanged="lstPermiso_SelectedIndexChanged" AutoPostBack="True"></asp:RadioButtonList>
                        </div> 
                    </div>
                    <div class="row espacio">
                        <div class="col-md-2 col-xs-6">
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" ControlStyle-CssClass="form-control btn btn-default"/>
                        </div> 
                        <div class="col-md-2 col-xs-6">
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" ControlStyle-CssClass="form-control btn btn-default"/>
                        </div>
                        <div class="col-md-2 col-xs-6">
                            <asp:Button ID="btnNuevoPermiso" runat="server" Text="Nuevo Permiso" ControlStyle-CssClass="form-control btn btn-default"/>
                        </div>
                    </div>

                    <div class="row espacio">
                        <div class="col-md-6">
                            <asp:GridView id="FlexHora" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:BoundField DataField="ENTSAL_TIPO" HeaderText="Tipo">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField>
                                        <HeaderStyle Width="70px"></HeaderStyle>
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="INGRESO_HORA" HeaderText="Hora de Ingreso">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SALIDA_HORA" HeaderText="Hora de Salida">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ENTSAL_CONTAR_TIPO">
                                        <HeaderStyle Width="0px"></HeaderStyle>
                                        <ItemStyle Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="S">
                                        <HeaderStyle Width="0px"></HeaderStyle>
                                        <ItemStyle Width="0px"></ItemStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView> 
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:GridView id="FlexPermiso"  runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:BoundField DataField="ENTSAL_TIPO" HeaderText="Tipo">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERMISO" HeaderText="Permiso">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERMISO_SALIDA_HORA" HeaderText="Hora de Salida">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PERMISO_INGRESO_HORA" HeaderText="Hora de Ingreso">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ENTSAL_CONTAR_TIPO">
                                        <ItemStyle Width="0px"></ItemStyle>
                                        <HeaderStyle Width="0px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="S">
                                        <ItemStyle Width="0px"></ItemStyle>
                                        <HeaderStyle Width="0px"></HeaderStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView> 
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="FlexBus" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnVerificar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="BtnBuscar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnNuevoPermiso" EventName="Click" />

                </Triggers>
            </asp:UpdatePanel>
        </div> 

     <div id="ModalBusqueda" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-md-12 col-sm-6" >
                        <asp:Label ID="lblEtq_BusDestino" runat="server" Font-Size="14px" class="control-label2" Text="Lista de Personal" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>                          
                                            <div class="row espacio">
                                                <div class="col-md-3">
                                                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-12">
                                                    <asp:GridView ID="FlexBus" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="PERSON_CODIGO" HeaderText="RUC" SortExpression="PERSON_CODIGO" />
                                                            <asp:BoundField DataField="NOMBRE_PERSONAL" HeaderText="Razón Social" SortExpression="NOMBRE_PERSONAL" />
                                                         </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexBus" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="Click" />
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

