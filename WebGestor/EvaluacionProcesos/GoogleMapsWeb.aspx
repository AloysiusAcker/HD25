<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GoogleMapsWeb.aspx.vb" Inherits="EvaluacionProcesos_GoogleMapsWeb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gogle Map Picker</title>
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap-theme.min.css"/>
    <script src="https://code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false&libraries=places&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"></script>
   <%-- <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false&libraries=places&key=AIzaSyBBnFbOLJzvM1gkDIi7COjRTjI-djBS-AI"></script>--%>
    <script src="../Js/locationpicker.jquery.js"></script>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false&libraries=places&key=AIzaSyDOVmgMrLBpBMmHRlu7hqX7Ti3g-mmhiEE&callback=initialize"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <button type="button" data-toggle="modal" data-target="#ModalMap" class="btn btn-default">
                <span class="glyphicon glyphicon-map-marker"></span><span id="ubicacion">Seleccionar ubicacion</span>
            </button>

            <style>
                .pac-container{
                    z-index:99999;
                }
            </style>
            <div class="modal fade" id="ModalMap" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Ubicacion:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="ModalMapAddress" CssClass="form-control" runat="server" Text="av. mariategui 1236 jesus maria, lima, peru"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                </div>
                                <div id="ModalMapPreview" style="width:100%; height:400px;"></div>
                                <div class="clearfix"> &nbsp;</div>
                                <div class="m-t-small">
                                    <label class="p-r-small col-sm-1 control-label">lat.:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ModalMapLat" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="p-r-small col-sm-1 control-label">long.:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="ModalMapLon" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <button type="button" class="btn btn-primary btn-block" data-dismiss="modal">Aceptar</button>
                                    </div>
                                </div>
                                <div class="clearfix"> &nbsp;</div>
                                <script>

                                    $('#ModalMapPreview').locationpicker({
                                        radius: 0,
                                        location: {
                                            latitude: -11.4990268,
                                            longitude: -77.2034841,
                                            name: 'av. mariategui 1236 jesus maria lima peru'
                                        },
                                        enableAutocomplete: true,
                                        inputBinding: {
                                            latitudeInput: $('#<%=ModalMapLat.ClientID%>'),
                                            longitudeInput: $('#<%=ModalMapLon.ClientID%>'),
                                            locationNameInput:$('#<%=ModalMapAddress.ClientID%>')
                                        }
                                    });

                                    $('#ModalMap').on('shown.bs.modal', function () {
                                        $('#ModalMapPreview').locationpicker('autosize');
                                    });

                                </script>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
