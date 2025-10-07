<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Eventos_xParticipantes.aspx.vb" Inherits="Eventos_Eventos_xParticipantes" %>

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

       let stream = null; // Variable global para la cámara

        // Función para abrir la cámara
        function abrirCamara() {
            if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
                navigator.mediaDevices.getUserMedia({ video: true })
                    .then(function (mediaStream) {
                        stream = mediaStream;
                        const video = document.getElementById("video");
                        video.srcObject = stream;
                        video.play();
                    })
                    .catch(function (err) {
                        console.error("Error al abrir la cámara:", err);
                        alert("No se pudo acceder a la cámara. Verifica permisos.");
                    });
            } else {
                alert("Tu navegador no soporta la cámara.");
            }
        }

        // Función para tomar la foto y guardarla automáticamente
        function tomarFoto() {
            if (!stream) {
                alert("La cámara no está activa.");
                return;
            }
            const canvas = document.getElementById("canvas");
            const video = document.getElementById("video");
            const contexto = canvas.getContext("2d");

            // Dibujar el video en el canvas
            contexto.drawImage(video, 0, 0, canvas.width, canvas.height);

            // Convertir la imagen a Base64
            const dataURL = canvas.toDataURL("image/png");
            document.getElementById("<%= hfImagen.ClientID %>").value = dataURL;

            // Cerrar la cámara
            cerrarCamara();

            // Enviar automáticamente la imagen al servidor
            document.getElementById("<%= btnGuardar.ClientID %>").click();
        }

        // Función para cerrar la cámara
        function cerrarCamara() {
            if (stream) {
                stream.getTracks().forEach(track => track.stop());
                stream = null;
                document.getElementById("video").srcObject = null;
            }
        }

        // Función que se ejecuta al hacer clic en el botón
        function abrirYTomarFoto() {
            abrirCamara();
            setTimeout(tomarFoto, 3000); // Esperar 3 segundos antes de tomar la foto
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
         //window.onload = iniciarCaptura();
        // Llamar a la función al cargar la página
       window.onload = obtenerGeolocalizacion;

    </script>


    <div class="container">
        <h1 class="Titulos">Eventos del Personal</h1>
        
        <div class="row espacio">
            <div class="col-md-2">
                <asp:Label ID="lblFecha" runat="server" CssClass="control-label-2"  Text="Fecha Sistema"></asp:Label>
                <asp:TextBox ID="txtFSistema" runat="server"  CssClass="form-control" ReadOnly="True"></asp:TextBox>
            </div> 
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="1000"></asp:Timer>
                    <div class="col-md-2">
                        <asp:Label ID="lblHora" runat="server"  CssClass="control-label-2"  Text="Hora Sistema" ></asp:Label>
                        <asp:TextBox id="txtHSistema" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox> 
                    </div> 
                </ContentTemplate>
                <Triggers>                        
                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="GvEventos" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div runat="server" class="col-md-2">
                <asp:Label ID="Label5" runat="server"  CssClass="control-label-2"  Text="Latitud" ></asp:Label>
                <input type="text" id="txtGeoInfoLat" class="form-control"  disabled="disabled" />
            </div> 
            <div runat="server" class="col-md-2">
                <asp:Label ID="Label6" runat="server"  CssClass="control-label-2"  Text="Longitud" ></asp:Label>
                <input type="text" id="txtGeoInfoLon" class ="form-control" disabled="disabled"  />
            </div>
            <div runat="server" visible ="false" class="col-md-2">
                <input type="text" id="txtGeoInfo"  />
            </div> 
            <asp:HiddenField ID="hfLatitud" runat="server" />
            <asp:HiddenField ID="hfLongitud" runat="server" />
        </div>  
        <div class="row espacio">            
            <div class="col-lg-2">
                <asp:Label id="LblEtiq1" runat="server" CssClass="control-label-2" text="Personal" ></asp:Label>
                <asp:TextBox ID="TxtPersonalCodigo" runat="server"  CssClass="form-control" ReadOnly="true"  ></asp:TextBox>
            </div>
            <div class="col-lg-8">
                <asp:Label id="LblEtiq9" runat="server" CssClass="control-label-2" text="Nombres" ></asp:Label>
                <asp:TextBox ID="TxtPersonalNombres" runat="server" CssClass="form-control" ReadOnly="true"  ></asp:TextBox>
            </div>
            <div class="col-lg-2">
                <asp:Label id="LblEtiq21" runat="server" CssClass="control-label-2" text="listar" forecolor="white"></asp:Label>
                <asp:Button ID="BtnListar" runat="server" text="Listar" CssClass="form-control btn-default" /> 
            </div>
        </div>           
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
                <div id="DivEvento" runat="server" visible="false" >                    
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label id="LblEtiq20" runat="server" CssClass="control-label-2" text="Datos del Evento" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </div>
                    </div>                
                    <div class="row espacio">
                        <div class="col-lg-1">
                            <asp:Label id="LblEtiq2" runat="server" CssClass="control-label-2" text="Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvCodigo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                        </div>
                        <div class="col-lg-5">
                            <asp:Label id="LblEtiq4" runat="server" CssClass="control-label-2" text="Nombre" ></asp:Label>
                            <asp:TextBox ID="TxtEvNombre" runat="server" CssClass="form-control" MaxLength="100" ReadOnly="true" ></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq3" runat="server" CssClass="control-label-2" text="Tipo del Evento" ></asp:Label>
                            <asp:Textbox ID="TxtEvTipo" runat="server" CssClass="form-control"  ReadOnly="true"  ></asp:Textbox>
                        </div>
                    </div>          
                    <div class="row espacio">
                    </div>      
                    <div class="row espacio">
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq5" runat="server" CssClass="control-label-2" text="Objetivo del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvObjetivo" runat="server" CssClass="form-control"  TextMode="MultiLine"  ReadOnly ="true"  ></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq6" runat="server" CssClass="control-label-2" text="Descripción del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"  ReadOnly ="true"  ></asp:TextBox>
                        </div>
                    </div>   
                    <div class="row espacio">                        
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq13" runat="server" CssClass="control-label-2" text="Responsable del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvResponsable" runat="server" CssClass="form-control"  ReadOnly ="true" ></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label id="LblEtiq7" runat="server" CssClass="control-label-2" text="Contacto" ></asp:Label>
                            <asp:TextBox ID="TxtEvContacto" runat="server" CssClass="form-control"   ReadOnly ="true" ></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq8" runat="server" CssClass="control-label-2" text="Teléfono" ></asp:Label>
                            <asp:TextBox ID="TxtEvContactoTelef" runat="server" CssClass="form-control"   ReadOnly ="true"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" CssClass="control-label-2"  Text="Fecha Inicia"></asp:Label>
                            <asp:TextBox ID="TxtFechaIni" runat="server"  CssClass="form-control"  ReadOnly ="true"  ></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq10" runat="server" CssClass="control-label-2"  Text="Fecha Termina"></asp:Label>
                            <asp:TextBox ID="TxtFechaFin" runat="server"  CssClass="form-control"  ReadOnly ="true"   ></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq11" runat="server" CssClass="control-label-2"  Text="Hora Inicia"></asp:Label>
                            <asp:TextBox ID="TxtHoraIni" runat="server"  CssClass="form-control" ReadOnly ="true"  ></asp:TextBox>
                       </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq12" runat="server" CssClass="control-label-2"  Text="Hora Termina"></asp:Label>
                            <asp:TextBox ID="TxtHoraFin" runat="server"  CssClass="form-control"  ReadOnly ="true"   ></asp:TextBox>
                        </div> 
                    </div>     
                    
                    <div class="row espacio" runat="server" id="Asiste" visible ="false" >
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq14" runat="server" CssClass="control-label-2"  Text="Fecha Asiste"></asp:Label>
                            <asp:TextBox ID="TxtFechaAsiste" runat="server"  CssClass="form-control"  ReadOnly ="true"   ></asp:TextBox>
                        </div> 
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq15" runat="server" CssClass="control-label-2" text="Latitud" ></asp:Label>
                            <asp:TextBox ID="TxtLatitud" runat="server" CssClass="form-control"   ReadOnly ="true"  ></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq16" runat="server" CssClass="control-label-2" text="Longitud" ></asp:Label>
                            <asp:TextBox ID="TxtLongitud" runat="server" CssClass="form-control"   ReadOnly ="true"  ></asp:TextBox>
                        </div>
                    </div> 
                    
                    <div class="row espacio">
                        <asp:TextBox ID="TxtRegistroFechas" runat="server"  CssClass="form-control"  visible="false"   ></asp:TextBox>
                    </div> 
                    
                    <div class="row espacio">
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq17" runat="server" CssClass="control-label-2"  Text="Fecha Asiste"></asp:Label>
                            <asp:TextBox ID="TxtFirmaFecha" runat="server"  CssClass="form-control"  ReadOnly ="true"  ></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq18" runat="server" CssClass="control-label-2"  Text="Hora Entrada"></asp:Label>
                            <asp:TextBox ID="TxtFirmaHoraEnt" runat="server"  CssClass="form-control" ReadOnly ="true"  ></asp:TextBox>
                       </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq19" runat="server" CssClass="control-label-2"  Text="Hora Salida"></asp:Label>
                            <asp:TextBox ID="TxtFirmaHoraSal" runat="server"  CssClass="form-control"  ReadOnly ="true"   ></asp:TextBox>
                        </div> 
                    </div>   
                    
                    <div class="row espacio">
                        <div class="col-md-2">
                            <button type="button" class ="form-control" onclick="abrirYTomarFoto()">Firmar Ingreso</button>
                        </div> 
                        <div class="col-lg-2">
                            <asp:Button ID="BtnCancelar" runat="server" text="Cancelar" CssClass="form-control btn-default" /> 
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar Foto" OnClick="btnGuardar_Click" Style="display:none;" />
                        </div>
                    </div>   

                   <%-- <div class="row espacio">
                        <div class="col-md-2 col-xs-6">
                            <div class="imagen">
                                <asp:Image ID="imgMostrada" runat="server" Width="150px" Height="150px"  CssClass="imagen-perfil" />
                            </div>                        
                        </div>
                    </div> --%>

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
                            <div class="col-md-3">
                                <!-- Campo oculto para guardar la imagen en base64 -->
                                <asp:HiddenField ID="hfImagen" runat="server" />
                            </div>
                        </div>
                    </div>                     

                </div>
         <%--   </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvEventos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
            <ContentTemplate>   --%>     
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label id="LblRegistro" runat="server" CssClass="control-label-2" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </div>
                </div>      
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView id="GvEventos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="EVENTO" HeaderText="Evento">
                                    <ItemStyle HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_EVENTO" HeaderText="Tipo">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_NOMBRE" HeaderText="Nombre del Evento">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_INICIA_EVENTO" HeaderText="Evento Inicia">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_TERMINA_EVENTO" HeaderText="Evento Termina">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_OBJETIVO" HeaderText="Objetivo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_DESCRIPCION" HeaderText="Descripción">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RESPONSABLE" HeaderText="Responsable">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_PARTICIPANTE" HeaderText="Fecha Asiste">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_INGRESO_REAL" HeaderText="Hora Ing.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_SALIDA_REAL" HeaderText="Hora Sal.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVEPART_REGISTRO" HeaderText="Registro">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                               <%--EVEPART_REGISTRO--%>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 
    </div> 


</asp:Content>

