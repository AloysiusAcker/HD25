<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default4.aspx.vb" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Gestor Plus</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/css/bootstrap.min.css" integrity="sha384-HSMxcRTRxnN+Bdg0JdbxYKrThecOKuH5zCYotlSAcp1+c8xmyTe9GYg1l9a69psu" crossorigin="anonymous"/>

    <link href="css/CSSWeb.css" rel="stylesheet" />
    <link href="EstiloWebTec.css" rel="stylesheet" type="text/css" />
    <link href="Css_WebGestor.css" rel="stylesheet" type="text/css" />

              
     <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
    
        <script type="text/javascript">
        function MantenSesion() {
            var CONTROLADOR = 'refresh_session.ashx';
            var head = document.getElementsByTagName('head').item(0);
            script = document.createElement('script');
            script.src = CONTROLADOR;
            script.setAtribute('type', 'text/javascript');
            script.defer = true;
            head.appendChild(script);
        }
    </script> 
    <style type="text/css">
        html {
          position: relative;
          min-height: 100%;
        }
        body {
         margin-bottom: 60px;
        }
        .auto-style1 {
            width:1100px;
        }
        .auto-style2 {
            width: 200px;
            height: 160px;
        }
        .auto-style3 {
            width: 289px;
            height: 160px;
        }
        .auto-style4 {
            width: 200px;
            height: 17px;
        }
        .auto-style5 {
            width: 200px;
        }
        .auto-style6 {
            width:289px;
        }
        .auto-style7 {
            width: 200px;
            height: 15px;
        }
        .auto-style8 {
            width:289px;
            height: 15px;
        }
        .auto-style9 {
            height: 15px;
            width: 504px;
        }
         
       
        .sidebar {
            height: 100%;
            width: 0;
            position: fixed;
            top: 65px;
            left: 0;
            background-color: #f8f9fa;
            overflow-x: hidden;
            transition: 0.5s;
            
            z-index: 2;
        }

        .sidebar a {
            padding: 8px 8px 8px 32px;
            text-decoration: none;
            font-size: 12px;
            color: #000;
            display: block;
            transition: 0.3s;
        }

        .sidebar a:hover {
            color: #007bff;
        }

        .sidebar .close-btn {
            position: absolute;
            top: 0;
            right: 25px;
            font-size: 30px;
            margin-left: 50px;
        }

        .sidebar .close-btn:hover {
            color: #007bff;
        }

        .sidebar-visible {
            width: 250px;
        }
        .content {
          /* 
          margin-left: 250px; 
              Ajusta según el ancho del sidebar */
        }
        .container .text-muted {
          margin: 20px 0;
        }
        .footer {
          position: absolute;
          bottom: 0;
          width: 100%;
          height: 60px; /* Set the fixed height of the footer here */
          background-color: #f5f5f5;
        }
        #nav-mobile .objLi {
            position: relative;
            display: inline-block;
            vertical-align: middle;
        }
        #nav-mobile .objLi>a{
            padding: 15px 8px;
            box-sizing: border-box;
        }
        @media (max-width: 767px){
            .navbar-nav .open .dropdown-menu {
                position: absolute;
                float: initial;
                width: initial;
                margin-top: 0;
                left : 0px;
                background-color: #fff;
                border: 1px solid #ccc;
                -webkit-box-shadow: none;
                box-shadow: 0 6px 12px rgba(0,0,0,.175);
            }
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
            EnableScriptLocalization="True">
        </asp:ScriptManager>
        
        <header>
            <nav class="navbar navbar-inverse">
                <div class="container-fluid">
                    <div class="navbar-header" style="width: 100%">

                        <a id="btn-trigger-menu" href="#" data-target="" class="navbar-link" 
                                style="float: left; padding: 13px 20px 13px 0px;">
                            <i class="material-icons">menu</i>
                        </a>
                        <a href="#" class="navbar-brand"><img src="~/Fotos/logo_white.png" runat="server"
                            style="height: 21px;"/></a>
                            <ul id="nav-mobile" class="nav navbar-nav navbar-right" style="float:right; margin: 0px;">
                                <li><asp:LinkButton ID="Inicio" runat="server" PostBackUrl="~/Default4.aspx" style="display: inline-block; vertical-align: middle">Inicio</asp:LinkButton></li>
                                <li class="objLi"><asp:LinkButton ID="PaginaP" runat="server" 
                                    PostBackUrl="~/PaginaPrincipal.aspx" style="padding-top: 18px;" >PáginaPrincipal</asp:LinkButton></li>   
                                <li class="objLi dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" 
                                       aria-haspopup="true" aria-expanded="false">  
                                        <span id="userNameHeader" runat="server" 
                                            style="display: inline-block; vertical-align: middle"></span>
                                        <i class="material-icons" 
                                            style="display: inline-block; vertical-align: middle">person</i></a>
                                    <ul class="dropdown-menu">
                                        <li><a id="username" runat="server"></a></li>
                                        <li><asp:LinkButton ID="btnCambioPass" runat="server" 
                                                PostBackUrl="~/Sistema/SegSistema_CambioContraseña.aspx" >Cambiar Contraseña</asp:LinkButton></li>                    
                                        <li><asp:LinkButton ID="Cerrar" runat="server" PostBackUrl="~/Salida.aspx" >Cerrar Sesión</asp:LinkButton></li>                                        
                                    </ul>
                                </li>
                            </ul>
                    </div>
                </div>
            </nav>
        </header>
        
        <div class="content">
            <div class="container">
                <div id="lblFecha" runat="server" style="font-weight: normal; font-size: 8pt; text-transform: capitalize;
                                width: 350px; color: seagreen; font-family: Arial; height: 16px; text-align: right; font-style: italic; display: inline;"></div>
                <div id="lblAgrup" runat="server" style="width: 700px; color: seagreen; font-family: Arial;
                        height: 17px; text-align: right; font-size: 8pt; font-style: italic; display: inline;"></div>

                <h1>Elegir Servidor</h1>
                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" font-color="black" Font-Names="Arial" Font-Size="10pt" GridLines="None">
                        <Columns>
                            <asp:ButtonField CommandName="Entrar" Text="Entrar" />
                            <asp:BoundField DataField="Empresa" />
                            <asp:BoundField DataField="nombre">
                            <ItemStyle ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Sigla">
                            <ItemStyle ForeColor="White" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>     
            </div>
        </div>
        <footer class="footer">
            <div class="container">
            <p class="text-muted">Derechos Reservados: HAC DATA.</p>
            </div>
        </footer>
    </form>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="https://code.jquery.com/jquery-1.12.4.min.js" integrity="sha384-nvAa0+6Qg9clwYCGGPpDQLVpLNn0fRaROjHqs13t4Ggj3Ez50XnGQqc/r8MhnRDZ" crossorigin="anonymous"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@3.4.1/dist/js/bootstrap.min.js" integrity="sha384-aJ21OjlMXNL5UyIl/XNwTMqvzeRMZH2w8c5cRVpzpU8Y5bApTppSuUkhZXN0VxHd" crossorigin="anonymous"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
           
            asignarDataTarget()

            $("#btn-trigger-menu").click(function () {
                $(".sidebar").toggleClass("sidebar-visible");
            });
        });
         function closeSidebar() {
            $(".sidebar").removeClass("sidebar-visible");
        }
        function asignarDataTarget() {
            // Obtener el sidenav por su clase
            var sidenav = document.querySelector('.custom-menu');

            // Obtener el botón por su ID
            var btnTriggerMenu = document.getElementById('btn-trigger-menu');

            // Asignar dinámicamente el ID del sidenav al atributo data-target del botón
            if (btnTriggerMenu && sidenav) {
              btnTriggerMenu.setAttribute('data-target', sidenav.id);
            }
        }
    </script>
</html>
