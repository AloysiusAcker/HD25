<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MapaWeb.aspx.vb" Inherits="MapaWeb" %>

<!DOCTYPE html>
<html  xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gogle Map Picker</title>
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap-theme.min.css"/>
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false&libraries=places&key=AIzaSyBBnFbOLJzvM1gkDIi7COjRTjI-djBS-AI"></script>
    <script src="../Js/locationpicker.jquery.js"></script>
    <script type="text/javascript">
        var map = null;
        function showlocation() {
            // One-shot position request.
            navigator.geolocation.getCurrentPosition(callback);
        }

        function callback(position) {

            var lat = position.coords.latitude;
            var lon = position.coords.longitude;

            document.getElementById('latitude').innerHTML = lat;
            document.getElementById('longitude').innerHTML = lon;

            var latLong = new google.maps.LatLng(lat, lon);

            var marker = new google.maps.Marker({
                position: latLong
            });

            marker.setMap(map);
            map.setZoom(8);
            map.setCenter(marker.getPosition());
        }

        google.maps.event.addDomListener(window, 'load', initMap);
        function initMap() {
            var mapOptions = {
                center: new google.maps.LatLng(0, 0),
                zoom: 1,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("map-canvas"),
                                              mapOptions);

        }
    </script>
  </head>
  <body>
    <form id="form1" runat="server">
        <div class="container">
            <input type="button" value="Show my location on Map"
                    onclick="javascript: showlocation()" />   <br/>
            Latitude: <span id="latitude"></span>       <br/>
            Longitude: <span id="longitude"></span>
        <br/><br/>
        <div id="map-canvas"/>
        </div>
    </form> 
  </body>
</html>