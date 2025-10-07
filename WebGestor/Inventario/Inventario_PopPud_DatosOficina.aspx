<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inventario_PopPud_DatosOficina.aspx.vb" Inherits="Inventario_Inventario_PopPud_DatosOficina" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <title>Gestor</title>
    <link rel="stylesheet" href="../css/bootstrap-theme.css" />
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <link rel="stylesheet" href="../css/bootstrapValidator.css" />
    <link rel="stylesheet" href="../css/CSSWeb.css" />
    <link rel="stylesheet" href="../EstiloWebTec.css"/>
    <link rel="stylesheet"  href="../Css_Tab.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/webcamjs/1.0.26/webcam.min.js"></script>
    <script src="../js/PopupArticulos.js" type="text/javascript"></script>
    <script src="../js/Popup.js" type="text/javascript"></script>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../js/bootstrapValidator.js" type="text/javascript"></script>
    <link href="../EstiloWebTec.css" rel="stylesheet" />

    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </div>

        <div class="container mt-5">
            <!-- El Modal -->
            <div id="miModalDatos" class="columnEmergente">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <!-- Encabezado Modal -->
                        <div class="modal-header">
                            <asp:Label ID="lblTitulo" runat="server" Text="Datos de la Oficina" CssClass="Titulos"></asp:Label>
                        </div>

                        <!-- Cuerpo Modal -->
                        <div class="modal-body">    
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>                        
                                    <div class="row">
                                        <div class="col-md-12 col-xs-12">
                                            <asp:Label ID="lblEtq1" runat="server" Text="Código" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCCod" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">                                
                                        <div class="col-md-12 col-xs-12">
                                            <asp:Label ID="lblEtq2" runat="server" Text="Descripción" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">                                
                                        <div class="col-md-12 col-xs-12">
                                            <asp:Label ID="lblEtq10" runat="server" Text="Dirección" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-xs-12">
                                            <asp:Label ID="lblEtq3" runat="server" Text="Cargo" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCCargo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-xs-12">
                                            <asp:Label ID="lblEtq4" runat="server" Text="Nombre" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-xs-6">
                                            <asp:Label ID="lblEtq9" runat="server" Text="Tipo de Oficina" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCTipo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6 col-xs-6">
                                            <asp:Label ID="lblEtq5" runat="server" Text="Anexo" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCAnexo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6 col-xs-6">
                                            <asp:Label ID="lblEtq6" runat="server" Text="Teléfono" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6 col-xs-6">
                                            <asp:Label ID="lblEtq7" runat="server" Text="Celular" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 col-xs-12">
                                            <asp:Label ID="lblEtq8" runat="server" Text="Correo Electrónico" CssClass="control-label"></asp:Label>
                                            <asp:TextBox ID="txtCCCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <!-- Pie Modal -->
                        <div class="modal-footer">
                            <asp:button ID="Cerrar" runat="server" Font-Names="Arial" Text="Cerrar" CssClass="form-control btn btn-default"></asp:button>  
                        </div>
                    </div>
                </div>
            </div>
        </div>
            </div> 
    </form>
</body>
</html>
