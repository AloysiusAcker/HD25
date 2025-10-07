<%@ Page Language="VB" AutoEventWireup="false" CodeFile="camara.aspx.vb" Inherits="camara" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Tomar Foto</title>
    <script src="Script/jquery-2.1.4.min.js"></script>
    <script src="Script/upclick-min.js"></script>
    <script lang="es-pe" type="text/javascript">
        function btnCamara_onclick() {
            var vDNI = $.trim(document.getElementById('<%=hndQR.ClientID %>').value);
            var cad = "";
            cad = prompt("Escriba un DNI", vDNI);
            if (cad != null) {
                if ($.isNumeric(cad) && cad.length >= 8) {
                    var frm = document.forms[0];

                    var hndDNI = frm.getElementsByTagName("input")[0];
                    hndDNI.value = cad;
                    //hndDNI.value = 100000;

                    document.getElementById('btnCamara').style.display = "none";

                    var afuFoto = document.getElementById("afuFoto");
                    afuFoto.style.display = "block";
                } else {
                    alert('Es un DNI incorrecto')
                }
            }
        }

        function FileUpload() {
            upclick(
                {
                    element: "afuFoto",
                    action: "camaraUpload.ashx",
                    action_params: {
                        'hndDNI': ''
                    },
                    onstar:
                        function (filename) {
                            var imgLoader = document.getElementById('imgLoader');
                            imgLoader.style.display = "block";
                        },
                    oncomplete:
                        function (response_data) {
                            var imgLoader = document.getElementById('imgLoader');
                            imgLoader.style.display = "none";

                            document.getElementById('<%=btnAgregar.ClientID %>').click();
                        }
                });
        }

        window.onload = function () {
            FileUpload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script lang="es-pe" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(FileUpload);
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <input type="button" id="afuFoto" value="FOTO" style="width: 117px;height: 43px; display:none; " />
        <input type="button" id="btnCamara" value="DNI" onclick="btnCamara_onclick();" style="width: 117px;height: 43px;" />
        <asp:Button ID="btnAgregar" runat="server" style="display:none" />
        <img id="imgLoader" src="Fotos/loading.gif" alt="" style="display:none;" />
        
        <figure style="margin:1em 40px;">
            <asp:Repeater ID="repFotos" runat="server">
            <ItemTemplate>
                <div style="display:inline-block; position:relative; width:220px; margin-bottom:5px">
                    <img id="imgFotos" src="Fotos/persona.jpg" alt="" runat="server" style="height:245px;" />
                    <div id="objDescrip" runat="server" style="color:rgb(0, 0, 0); background-color:rgb(76, 255, 0); text-align:center;">43059906</div>
                </div>
            </ItemTemplate>
            </asp:Repeater>
            <input type="hidden" id="hndQR" runat="server" />
        </figure>

        
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>