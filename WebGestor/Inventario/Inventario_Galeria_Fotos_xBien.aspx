<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inventario_Galeria_Fotos_xBien.aspx.vb" Inherits="Inventario_Inventario_Galeria_Fotos_xBien" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">
            <h1 class="mt-5">Galería de Fotos</h1>
            <div class="row mt-4">
                <asp:Repeater ID="rptPhotos" runat="server">
                    <ItemTemplate>
                        <div class="photo-thumbnail" data-toggle="modal" data-target="#photoModal" data-photo='<%# Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())) %>'>
                        <img src='data:image/png;base64,<%# Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())) %>' alt='<%# Eval("Descripcion") %>' class="img-thumbnail" />                            
                           
                    </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
