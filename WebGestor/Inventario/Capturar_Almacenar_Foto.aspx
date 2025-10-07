<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Capturar_Almacenar_Foto.aspx.vb" Inherits="Capturar_Almacenar_Foto" %>

<!DOCTYPE html>
<html>
<head>
    <title>Foto</title>
    <link rel="stylesheet" href="../css/CSSWeb.css" />
    <link rel="stylesheet" href="../EstiloWebTec.css"/>
    <link href="../Css_WebGestor.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    
</head>
<body>
    <div class="container">
        <h1>Captura y Visualización de Foto</h1>
        <div class="row">
            <div class="col-md-12">
                <video id="video" autoplay></video>
                <button id="captureBtn" class="botoncito">Capturar Foto</button>
                <img id="capturedImg" style="display: none; max-width: 100px;" />
                <button id="uploadBtn" style="display: none;">Subir Foto</button>
            </div>
        </div>
        <div class="row" style="align-content:flex-start">
            <div class="col-md-12">
                <h5>Nombre de la Imagen :</h5>
                <input id="txtNomImg" runat="server"  type="text" CssClass="form-control" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"/>
            </div>
        </div>
        <div class="row">
            <div class="container">
                <h5>Cód. Articulos :</h5>
                <input id="txtCodArt" runat="server"  type="text" CssClass="form-control" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h5>Nombre Articulos :</h5>
                <input id="txtNomArt" runat="server"  type="text" CssClass="form-control" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"/>
            </div>
        </div>
        <div class="row" style="color:white">
            <div class="col-md-12">
                <input id="TxtRutaServidor" runat="server"  type="text" CssClass="form-control" Font-Names="Roboto,arial,sans-serif" Font-Size="8pt"/>
            </div>
        </div>
    </div>
    <script>
        const video = document.getElementById('video');
        const captureBtn = document.getElementById('captureBtn');
        const capturedImg = document.getElementById('capturedImg');
        const uploadBtn = document.getElementById('uploadBtn');

        if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
          // Solicitar permisos para acceder a la cámara
          navigator.mediaDevices.getUserMedia({ video: true })
            .then(function(stream) {
                video.srcObject = stream;
                captureBtn.style.display = 'block';
            })
            .catch(function(error) {
              // El permiso fue denegado o ocurrió un error
              console.error('Error al acceder a la cámara:', error);
            });
        } else {
          console.error('El navegador no admite getUserMedia');
        }


        //navigator.mediaDevices.getUserMedia({ video: true })
        //    .then(stream => {
        //        video.srcObject = stream;
        //        captureBtn.style.display = 'block';
        //    })
        //    .catch(error => console.error('Error accessing camera:', error));

        captureBtn.addEventListener('click', () => {

            const canvas = document.createElement('canvas');
            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;
            canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
            const imageDataURL = canvas.toDataURL('image/jpeg');
            capturedImg.src = imageDataURL;
            capturedImg.style.display = 'block';
            uploadBtn.style.display = 'block';
        });

        uploadBtn.addEventListener('click', () => {
            
            var valorTextBox = document.getElementById('<%= txtCodArt.ClientID %>').value;
            var valorNomImg = document.getElementById('<%= txtNomImg.ClientID %>').value;
            var valorSession = document.getElementById('<%= TxtRutaServidor.ClientID %>').value;

            const capturedData = capturedImg.src;
            const xhr = new XMLHttpRequest();
            xhr.open('POST', 'Capturar_Almacenar_Foto.aspx/GuardarImagen', true);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.onreadystatechange = () => {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    alert('Imagen subida con éxito.');
                    window.close();
                }
            };
            xhr.send(JSON.stringify({ imageData: capturedData, paraCodArt: valorTextBox, paraRuta: valorSession, paraNomImg: valorNomImg }));
            window.close();
        });
    </script>
</body>
</html>
