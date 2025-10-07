<%--<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Finanzas_Registro.aspx.vb" Inherits="Finanzas_Finanzas_Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link rel="stylesheet" href="../css/bootstrap-theme.css" />
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <link rel="stylesheet" href="../css/bootstrapValidator.css" />
    <link rel="stylesheet" href="../css/CSSWeb.css" />
    <link rel="stylesheet" href="../EstiloWebTec.css"/>
    <link rel="stylesheet"  href="../Css_Tab.css" />
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/webcamjs/1.0.26/webcam.min.js"></script>       
    <script src="../js/PopupArticulos.js" type="text/javascript"></script>
    <script src="../js/Popup.js" type="text/javascript"></script>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script src="../js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../js/bootstrapValidator.js" type="text/javascript"></script>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <style type="text/css">
        
        .two-lines-cell {
            white-space: normal; /* Permite que el texto se ajuste a dos líneas */
            word-wrap: break-word; /* Rompe palabras largas */
            overflow: hidden; /* Oculta el texto que no cabe */
            text-overflow: ellipsis; /* Muestra puntos suspensivos si el texto es demasiado largo */
            max-height: 3em; /* Altura máxima de dos líneas */
            line-height: 1.5em; /* Altura de línea para dos líneas */
        }

        .custom-file-upload {
          display: inline-block;
          padding: 6px 12px;
          cursor: pointer;
          background-color: #337ab7;
          color: #fff;
          border: none;
          border-radius: 4px;
        }

        /* Ocultar el control de archivo nativo */
        .custom-file-upload input[type="file"] {
          display: none;
        }
        .it .btn-orange
        {
          background-color: blue;
          border-color: #777!important;
          color: #777;
          text-align: left;
          width:100%;
        }
        .it input.form-control
        {
  
          border:none;
          margin-bottom:0px;
          border-radius: 0px;
          border-bottom: 1px solid #ddd;
          box-shadow: none;
        }
        .it .form-control:focus
        {
          border-color: #ff4d0d;
          box-shadow: none;
          outline: none;
        }
        .fileUpload {
            position: relative;
            overflow: hidden;
        }
        .fileUpload input.upload {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            padding: 0;
            font-size: 20px;
            cursor: pointer;
            opacity: 0;
            filter: alpha(opacity=0);
        }
        .fileUpload .upload {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            padding: 0;
            font-size: 20px;
            cursor: pointer;
            opacity: 0;
            filter: alpha(opacity=0);
        }


    </style>
    
    <style type="text/css">
        /* Estilo personalizado para el CalendarExtender */
        .ajax__calendar_container {
            position: absolute;
            z-index: 1000; /* Puedes ajustar este valor según tus necesidades */
        }
        .custom-calendar .ajax__calendar_container {
            background-color: #f2f2f2; /* Color de fondo del calendario */
            border: 1px solid #ccc; /* Borde del calendario */
        }

        .custom-calendar .ajax__calendar_header {
            background-color: #333; /* Color de fondo del encabezado del calendario */
            color: #fff; /* Color del texto del encabezado del calendario */
        }

        .custom-calendar .ajax__calendar_dayname {
            background-color: #eee; /* Color de fondo de los días de la semana */
            color: #666; /* Color del texto de los días de la semana */
        }

        .custom-calendar .ajax__calendar_day {
            background-color: #fff; /* Color de fondo de los días */
            color: #333; /* Color del texto de los días */
        }

        .custom-calendar .ajax__calendar_hover {
            background-color: #ddd; /* Color de fondo al pasar el mouse por encima de un día */
            color: #333; /* Color del texto al pasar el mouse por encima de un día */
        }

        .custom-calendar .ajax__calendar_active {
            background-color: #007bff; /* Color de fondo de un día seleccionado */
            color: #fff; /* Color del texto de un día seleccionado */
        }

        .custom-calendar .ajax__calendar_other {
            color: #999; /* Color del texto de los días de otros meses */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Finanzas_Registro.aspx.vb" Inherits="Finanzas_Finanzas_Registro" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="LblEtiq1" runat="server" Text="Registro de Ingresos y Egregos" CssClass="Titulos" />
                </div> 
            </div>
            <br />
            <div class="row">  
            </div>
            <div class="row">
                <div class="col-md-3 col-xs-6">
                     <asp:Label ID="Label2" runat="server" Text="Tipo de Cambio" CssClass="control-label-2" />
                </div>  
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="TxtVenta" runat="server" Text="Venta" CssClass="control-label-2" />
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="TxtCompra" runat="server" Text="Compra" CssClass="control-label-2"   />
                </div> 
            </div>         
            <div class="row">
                <div class="col-md-2 col-xs-6">  
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label22" runat="server" Text="Compra" CssClass="control-label-2" forecolor="white"  />
                    <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" ControlStyle-CssClass="form-control btn btn-default" />
                </div> 
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label23" runat="server" Text="Compra" CssClass="control-label-2"  forecolor="white"   />
                    <asp:Button ID="BtnLimpiar" runat="server" Text="Limpiar" ControlStyle-CssClass="form-control btn btn-default"/>
                </div>
            </div>   
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="LblAño" runat="server" Text="Año" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlAño" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="Fecha Registro" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtFechaReg" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaReg" Format="dd/MM/yyyy" PopupButtonID="TxtFechaReg" ></cc1:CalendarExtender>
                </div>
            </div>   
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="Módulo" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlModulo" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="LblMoneda" runat="server" Text="Tipo Mov." CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlTipoMov" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="Caja" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlCaja" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="Label6" runat="server" Text=".." CssClass="control-label-2" ForeColor ="White"  />
                    <asp:TextBox ID="TxtCaja" runat="server" CssClass="form-control" Text=""  ></asp:TextBox>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label7" runat="server" Text="Motivo" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlMotivo" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label8" runat="server" Text="Cta. Origen" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlCtaOrigen" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label9" runat="server" Text="Cta. Destino" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlCtaDestino" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label10" runat="server" Text="Glosa" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtGlosa" runat="server" CssClass="form-control" Text=""  ></asp:TextBox>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-2 col-xs-12">
                    <asp:Label ID="Label19" runat="server" Text="Persona" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtRuc" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1 col-xs-11">
                    <asp:Label ID="Label20" runat="server" Text="Persona" CssClass="control-label-2" ForeColor="White"  />
                    <asp:Button ID="BtnBusca" runat="server" Text="..." ControlStyle-CssClass="form-control btn btn-default" />
                </div>
                <div class="col-md-6 col-xs-12">
                    <asp:Label ID="Label21" runat="server" Text="Persona" CssClass="control-label-2" ForeColor="White"  />
                    <asp:TextBox ID="TxtRazonSocial" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-6 col-xs-12">
                    <asp:TextBox ID="TxtCodPersona" runat="server" visible="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label11" runat="server" Text="Documento" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlDoc" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label12" runat="server" Text="Serie" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtDocSerie" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-6 col-xs-6">
                    <asp:Label ID="Label13" runat="server" Text="Número" CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtDocNumero" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-3 col-xs-6">
                    <asp:Label ID="Label14" runat="server" Text="Documento Ref." CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlDocRef" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label15" runat="server" Text="Serie Ref." CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtDocRefSerie" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3 col-xs-6">
                    <asp:Label ID="Label16" runat="server" Text="Numero Ref" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtDocRefNumero" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label17" runat="server" Text="Moneda" CssClass="control-label-2" />
                    <asp:DropDownList ID="DdlMoneda" runat="server" CssClass="form-control" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label26" runat="server" Text="Fec. Emi.doc. Ref" CssClass="control-label-2" />
                    <asp:TextBox ID="TxtDocRefFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtDocRefFecha" Format="dd/MM/yyyy" PopupButtonID="TxtDocRefFecha" ></cc1:CalendarExtender>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label18" runat="server" Text="importe" CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtImporte" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label3" runat="server" Text="IGV" CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtIgv" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label24" runat="server" Text="Total" CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtTotal" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label25" runat="server" Text="Tipo Cambio" CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtTipoCambio" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
            </div> 
            <div class="row">
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label27" runat="server" Text="Renta 4ta " CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtRenta4" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label28" runat="server" Text="Inafecto" CssClass="control-label-2"  />
                    <asp:TextBox ID="TxtInafecto" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2 col-xs-6">
                    <asp:Label ID="Label29" runat="server" Text="Inafecto" CssClass="control-label-2"  />
                    <asp:DropDownList ID="DdlInafecto" runat="server" Enabled="False" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-lg-3 col-xs-6">
                    <asp:CheckBox ID="ChkSinIgv" CssClass="checkbox checkbox-inline" Text="Sin IGV" Font-Bold ="true" runat="server" AutoPostBack="True" />
                </div> 
            </div> 
            <br />



        </div>


</asp:Content>
<%--    </form>
</body>
</html>--%>
