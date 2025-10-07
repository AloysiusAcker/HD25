<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GoogleMaps_Prueba.aspx.vb" Inherits="EvaluacionProcesos_GoogleMaps_Prueba" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Usando la geolocalización con HTML5</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false&libraries=places&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"></script>
       
    <script type="text/javascript">
        //>>Inicia declaración de variables
        var map; //Objeto MAPA
        var infowindow; //Objeto para ventanas
        var latitud = 23.743338; //Latitud inicial
        var longitud = -99.143684; //Longitud Inicial
        var directionsService; //Objeto para el servicio de instrucciones
        var directionsDisplay; //Objeto para mostrar instrucciones
        var myCurrentPosition; //Posición Actual
        var markers = []; //Arreglo para agregar marcadores
        //<<Termina declaración de variables
        function initMap() {
            directionsDisplay = new google.maps.DirectionsRenderer();
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: latitud, lng: longitud },
                scrollwheel: false,
                zoom: 13
            });
            directionsDisplay.setMap(map);
            map.addListener("mousemove", function (event) {
                displayCoordinates(event.latLng);
            });
        }
        //Función para desplegar coordenadas del mouse
        function displayCoordinates(pnt) {
            var coordsLabel = document.getElementById("tdCursor");
            var lat = pnt.lat();
            lat = lat.toFixed(4);
            var lng = pnt.lng();
            lng = lng.toFixed(4);
            coordsLabel.innerHTML = "Lat: " + String(lat) + "  Lng: " + String(lng);
        }



        
        //Mostramos el mapa y marcamos nuestra posición
        $(document).ready(function () {
            $(window).load(function () {
                //Inicializar variables
                infowindow = new google.maps.InfoWindow({});
                directionsService = new google.maps.DirectionsService();
                //Inicializar mapa
                initMap();
                //Obtener la posición actual
                /*
                Nota Importante: Esta funcionalidad solo funciona de manera local.
                Para poder utilizarla en una página web se requiere conexión segura;
                es decir, se requiere la utiliación de un certificado SSL.
                */
                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(
                        function (position) {
                            /*Obtener coordenadas*/
                            console.log("Obteniendo coordenadas...");
                            var point = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                            myCurrentPosition = point;
                            map.setZoom(16);
                            map.setCenter(point);
                            google.maps.event.trigger(map, "resize");//actualizar mapa
                            var myPos = new google.maps.Marker({
                                position: point,
                                map: map,
                                icon: "../img/ico_green.png",
                                title: "Mi posición actual"
                            });
                            myPos.addListener('click', function () {
                                infowindow.setContent("<i class='glyphicon glyphicon-home'></i>" +
                                    "<h3>Esta es mi posición actual</h3>" +
                                    "<small><a href='www.tyrodeveloper.com'>www.tyrodeveloper.com</a></small>");
                                infowindow.open(map, myPos);
                            });
                        },
                        function (err) {
                            /*En caso de fallar al obtener las coordenadas*/
                            console.log('GPS Desactivado.');
                            /*Establecer la posición predeterminada*/
                            myCurrentPosition = new google.maps.LatLng(latitud, longitud);
                        }
                    );
                }

            });
        });

        //Función para buscar cuando se escriba algo en el Textbox
        $("#<%=txtBuscar.ClientID%>").autocomplete({
            source: function (request, response) {
                /*Aquí se configura el origen de datos*/
                $.ajax({
                    type: 'POST',
                    url: "autocomplete-google-maps.aspx/BuscarLugar",
                    data: "{textoBuscar: '" + request.term + "'}",
                    dataType: "json",
                    contentType: 'application/json',
                    async: false,
                    success: function (result) {
                        response($.parseJSON(result.d));
                    }
                });
            },
            search: function () {
                /*Este evento sucede mientras se escribe algo en el TextBox*/
                // Condicionar a menos 3 
                // caracteres en la búsqueda
                var term = this.value;
                if (term.length < 3) {
                    return false;
                }
            },
            focus: function () {
                /*Este evento sucede cuando el TextBox obtiene el foco*/
                // Evitar que el valor se inserte cuando 
                // el TextBox obtenga el foco
                return false;
            },
            select: function (event, ui) {
                /*Este evento sucede cuando se selecciona uno de los resultados*/
                // Asignar el valor al TextBox
                this.value = ui.item.value;
                // Mostrar el ID
                BuscarUbicacion(ui.item.id);//***ATENCION EN ESTA LINEA, LA MODIFICAREMOS MAS ADELANTE***/
                return false;
            }
        });

        //Función para obtener la ubicación
        function BuscarUbicacion(idUbicacion) {
            $.ajax({
                type: 'POST',
                url: "autocomplete-google-maps.aspx/BuscarUbicacion",
                data: "{idUbicacion: " + idUbicacion + "}",
                dataType: "json",
                contentType: 'application/json',
                async: false,
                success: function (result) {
                    //Obtener la ubicación devuelta
                    var ubicacion = $.parseJSON(result.d);
                    var marker = new google.maps.Marker({
                        position: new google.maps.LatLng(ubicacion.Lat, ubicacion.Lng),
                        title: ubicacion.Nombre,
                        icon: '../img/ico_red.png',
                        map: map
                    });
                    markers.push(marker);
                    marker.addListener('click', function () {
                        //Poner todos los marcadores de color rojo
                        for (var j = 0; j < markers.length; j++) {
                            markers[j].setIcon("../img/ico_red.png");
                        }
                        //Poner icono azul al marcador nuevo
                        marker.setIcon("../img/ico_blue.png");
                        //Diseñar la ventana que se va a mostrar
                        infowindow.setContent("<h3>" + ubicacion.Nombre + "</h3><hr />" +
                            "<img src='../img/" + ubicacion.Foto + "' height='150px' width='250px' alt='Foto' /><br /><br />" +
                            "<a href='#' onclick='CalcularRuta(" + ubicacion.Lat + "," + ubicacion.Lng + ");' class='btn btn-primary btn-sm'><i class='glyphicon glyphicon-road'></i> Mostrar ruta</a>");
                        infowindow.open(map, marker);
                    });
                    map.setZoom(16);
                    map.setCenter(marker.getPosition());
                    google.maps.event.trigger(map, 'resize');
                }
            });
        }

        //function iniciar()
        //{
        //	var boton=document.getElementById('obtener');	
        //	boton.addEventListener('click', obtener, false);
        //}

        //function obtener(){
        //	navigator.geolocation.getCurrentPosition(mostrar);
        //}

        //function mostrar(posicion)
        //{
        //	var ubicacion = document.getElementById('ubicacion');
        //	var datos="";
        //		datos+='Latitud: '  + posicion.coords.latitude + '<br/>';
        //		datos+='Longitud: ' + posicion.coords.longitude + '<br/>';
        //		datos+='Exactitud: ' + posicion.coords.accuracy + '<br/>';
        //		ubicacion.innerHTML=datos;
        //}
        //window.addEventListener('load', iniciar, false);

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Google Maps y jQuery Autocomplete</h1>
            <hr />
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtBuscar" placeholder="Lugar a buscar..." CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div id="map" style="width: 100%; height: 450px;"></div>
                            <div id="tdCursor">Lat: 0, Lng: 0</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
<%--<div id="ubicacion">
<button id="obtener">Obtener mi ubicación </button>
</div>--%>
</body>

</html>
