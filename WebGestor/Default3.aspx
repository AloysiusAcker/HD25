<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default3.aspx.vb" Inherits="Default3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Geolocalización ASP.NET</title>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Verifica si el navegador soporta la API de geolocalización
            if ("geolocation" in navigator) {
                // Obtiene la posición actual del usuario
                navigator.geolocation.getCurrentPosition(function (position) {
                    // Accede a las coordenadas de la posición
                    var latitud = position.coords.latitude;
                    var longitud = position.coords.longitude;

                    // Muestra las coordenadas en cajas de texto
                    $("#txtLatitud").val(latitud);
                    $("#txtLongitud").val(longitud);

                   
                }, function (error) {
                    // Manejo de errores
                    console.error("Error al obtener la geolocalización: " + error.message);
                });
            } else {
                console.log("La geolocalización no está soportada por este navegador.");
            }
        });
    </script>
</head>
<body>
    <!-- Contenido de tu página ASPX -->
    <input type="text" id="txtLatitud" placeholder="Latitud"  />
    <input type="text" id="txtLongitud" placeholder="Longitud"  />
</body>
</html>
