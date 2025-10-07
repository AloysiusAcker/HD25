<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Geolocalizacion_xBien.aspx.vb" Inherits="Inventario_Inventario_Geolocalizacion_xBien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

     <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Geolocalización de Bienes" CssClass="Titulos" />
            </div> 
        </div>
         <input type="text" id="txtLatitud" placeholder="Latitud"  />
        <input type="text" id="txtLongitud" placeholder="Longitud"  />

    </div> 
</asp:Content>

