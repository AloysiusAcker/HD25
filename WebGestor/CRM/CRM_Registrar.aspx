<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="CRM_Registrar.aspx.vb" Inherits="CRM_CRM_Registrar" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <style type="text/css">
    .four-lines-cell {
            white-space: normal; /* Permite que el texto se ajuste a dos líneas */
            word-wrap: break-word; /* Rompe palabras largas */
            overflow: hidden; /* Oculta el texto que no cabe */
            text-overflow: ellipsis; /* Muestra puntos suspensivos si el texto es demasiado largo */
            max-height: 3em; /* Altura máxima de dos líneas */
            line-height: 1.5em; /* Altura de línea para dos líneas */
        }
    </style>

    <script type="text/javascript" lang="javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';
    </script>

    <div class="container">
        <h1 class="Titulos">Registro de Tickets</h1>    
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row espacio">          
                    <div class="col-lg-12">           
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Maroon"></asp:Label>             
                    </div>
                </div> 
                <div class="row espacio">          
                    <div class="col-lg-12">           
                        <asp:Label ID="lblErrorInc" runat="server" ForeColor="Red"></asp:Label>             
                    </div>
                </div> 
                <div class="row espacio">
                    <div class="col-lg-2">
                        <asp:Label ID="lbl1" runat="server" CssClass="control-label-2" ForeColor="Red" Text="Nº de Ticket"></asp:Label>
                       <asp:TextBox ID="txtIncidente" runat="server" CssClass="form-control" ReadOnly="True" ></asp:TextBox>
                    </div>
                    <div class="col-lg-6">

                    </div>
                    <div class="col-lg-2">
                        <asp:Label ID="Label24" runat="server" CssClass="control-label-2" Text="Fecha Apertura"></asp:Label>
                        <asp:TextBox ID="txtAperturaFecha" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="txtAperturaFecha" Format="dd/MM/yyyy" PopupButtonID="txtAperturaFecha" ></cc1:CalendarExtender>
                    </div>
                    <div class="col-lg-2">                        
                        <asp:Label ID="Label16" runat="server"  CssClass="control-label-2" Text="Hora"></asp:Label>
                        <asp:TextBox ID="txtHoraApertura" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="Label1" runat="server" CssClass="control-label-2" Text="Cliente" ></asp:Label>
                        <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control" AutoPostBack="True" MaxLength="11"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Label ID="Label27" runat="server" CssClass="control-label-2" Text="Cliente" ForeColor="White" ></asp:Label>
                        <asp:Button ID="btnDatos" runat="server" CssClass="form-control btn btn-default" Text="..."  />
                    </div>
                    <div class="col-lg-8">
                        <asp:Label ID="Label28" runat="server" CssClass="control-label-2" Text="Cliente" ForeColor="White"></asp:Label>
                        <asp:TextBox ID="txtRazon" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div>                    
                   <asp:Label ID="lblCodCliente" runat="server" CssClass="control-label-2" visible="false"  ></asp:Label>
                    <asp:Label ID="LblCodCCosto" runat="server" CssClass="control-label-2" visible="false"   ></asp:Label>
                </div>
                <div class="row espacio">
                    <div class="col-lg-9">
                        <asp:Label ID="Label2" runat="server" CssClass="control-label-2" Text="Contacto" ></asp:Label>
                        <asp:DropDownList ID="DdlContacto" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label4" runat="server" CssClass="control-label-2" Text="Teléfono" ></asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">                        
                        <asp:Label ID="Label22" runat="server" CssClass="control-label-2" Text="Correo"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-1">                        
                        <asp:CheckBox ID="ChkCosto" runat="server" text="C.Costo" CssClass="form-control checkbox-inline" BorderWidth="0px" AutoPostBack="True" />
                    </div>
                    <div class="col-lg-2">
                        <asp:TextBox ID="TxtCodInternoCC" runat="server" CssClass="form-control" Enabled="false" AutoPostBack="True" MaxLength="11"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:Button ID="BtnBuscarCC" runat="server" Text="..." CssClass="form-control btn btn-default" Enabled="false" />
                    </div>
                    <div class="col-lg-8">
                        <asp:TextBox ID="TxtDescripcionCC" runat="server" CssClass="form-control" ReadOnly="True" Enabled="false" ></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-6">
                        <asp:Label ID="Label3" runat="server" CssClass="control-label-2" Text="Proceso"></asp:Label>
                        <asp:DropDownList ID="DdlProceso" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label5" runat="server" CssClass="control-label-2" Text="Canal"></asp:Label>
                        <asp:DropDownList ID="DdlCanal" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label32" runat="server" CssClass="control-label-2" text="Correo" ForeColor="white"></asp:Label>
                        <asp:Button ID="BtnCorreo" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Correo Entrante" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="Label33" runat="server" CssClass="control-label-2" text="btnEvento" ForeColor="white"></asp:Label>
                        <asp:Button ID="btnEvento" runat="server" style="display:none" visible="true" OnClick="btnEvento_Click"/>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="Label7" runat="server" CssClass="control-label-2" Text="Tipo Petición"></asp:Label>
                        <asp:DropDownList ID="cboComponente" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label23" runat="server" CssClass="control-label-2" Text="Elemento"></asp:Label>
                        <asp:DropDownList ID="cboElemento" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label9" runat="server" CssClass="control-label-2" Text="Elemento" ForeColor ="white"></asp:Label>
                        <asp:DropDownList ID="cboElemento2" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">         
                        <asp:Label ID="Label29" runat="server" CssClass="control-label-2" Text="Tabla" ForeColor="white"></asp:Label>              
                        <asp:Button ID="BtnNuevaTE" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Nueva Tabla" />
                    </div> 
                </div>
                <div class="row espacio">
                    <div class="col-lg-9">
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="cmdBuscar" runat="server" ControlStyle-CssClass="form-control btn btn-default" onkeypress="javascript:if(event.keyCode==13){retur n false;}"
                            Text="Buscar" />
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="Label8" runat="server" CssClass="control-label-2" Text="Motivo"></asp:Label>
                        <asp:TextBox ID="txtMotivo" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="Label14" runat="server" CssClass="control-label-2" Text="Descripción del Problema"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label ID="Label34" runat="server" CssClass="control-label-2" Text="Descripción del Problema"></asp:Label>
                        <asp:TextBox ID="txtSolucion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Label ID="Label10" runat="server" CssClass="control-label-2" Text="Criticidad"></asp:Label>
                        <asp:DropDownList ID="DdlCriticidad" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="Label11" runat="server" CssClass="control-label-2" Text="Estado"></asp:Label>
                        <asp:DropDownList ID="DdlEstado" runat="server" AutoPostBack="True" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblImpacto" runat="server" CssClass="control-label-2" Text="Estado Cliente"></asp:Label>    
                        <asp:TextBox ID="txtEstadoCliente" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                </div>
                <div class="row espacio">
                    <div class="col-lg-3">
                        <asp:Button ID="cmdResolver" runat="server" Text="Guardar" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                    </div>
                    <div class="col-lg-3">
                        <asp:Button ID="cmdBorrar" runat="server" Text="Borrar Descripción y Solución" ControlStyle-CssClass="form-control btn btn-default"></asp:Button>
                    </div>
                </div>
                <div class="row">                    
                    <asp:TextBox ID="lblElemento" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="lblElemento2" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="lblComponente" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="lblCodOficina" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="lblCodEstado" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="lblCodConsulta" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                </div>
                <div id="Trackings" class="form-horizontal" style="margin-top: 10px; padding-left: 25px; width: 650px" runat="server" visible="false">
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                    <cc1:TabPanel runat="server" HeaderText="Tracking de Correos Enviados" ID="TabPanel8">
                        <ContentTemplate>
                            <div class="row form-group-lg">
                                <div class="col-lg-12">
                                    <asp:GridView ID="GvTrackingCorreo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:BoundField DataField="TICKET_NRO" HeaderText="Nro Ticket" SortExpression="TICKET_NRO" />
                                            <asp:BoundField DataField="RAZON_SOCIAL" HeaderText="Razón Social" SortExpression="RAZON_SOCIAL" />
                                            <asp:BoundField DataField="CONTACTO" HeaderText="Contacto" SortExpression="CONTACTO" />
                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                            <asp:BoundField DataField="HORA" HeaderText="Hora" SortExpression="HORA" />
                                            <asp:BoundField DataField="APROB_ESTADO" HeaderText="Estado" SortExpression="APROB_ESTADO" />
                                            <asp:BoundField DataField="NRO_VEZ" HeaderText="N° Veces" SortExpression="NRO_VEZ" />
                                            <asp:BoundField DataField="PERSONA_ENVIADA" HeaderText="Persona Env." SortExpression="PERSONA_ENVIADA" />
                                            <asp:BoundField DataField="PUESTO" HeaderText="Cargo" SortExpression="PUESTO" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Tracking de Acciones" ID="TabPanel9">
                        <ContentTemplate>
                            <div class="row form-group col-md-12">
                                <div class="col-lg-12">
                                    <asp:GridView ID="GvTrackingAcciones" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                        <Columns>
                                            <asp:BoundField DataField="TICKET_CODIGO" HeaderText="N° Ticket" SortExpression="TICKET_CODIGO" />
                                            <asp:BoundField DataField="ACCION" HeaderText="Acción" SortExpression="ACCION" />
                                            <asp:BoundField DataField="ACCION_FECHA" HeaderText="Fecha" SortExpression="ACCION_FECHA" />
                                            <asp:BoundField DataField="ACCION_HORA" HeaderText="Hora" SortExpression="ACCION_HORA" />
                                            <asp:BoundField DataField="ACCION_USER" HeaderText="Usuario" SortExpression="ACCION_USER" />
                                            <asp:BoundField DataField="ETIQUETA_REFERENCIA" HeaderText="Referencia" SortExpression="ETIQUETA_REFERENCIA" />
                                            <asp:BoundField DataField="COD_REFERENCIA" HeaderText="Cod. Referencia" SortExpression="COD_REFERENCIA" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>


                <div runat="server" id="ProcedimientosSeguir" visible="false" style="padding-left: 25px;">
                    <label style="font-size: medium; text-align: center">______________________________________________________________________</label>
                    <div class="form-group" style="align-items: center">
                        <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                            <label style="font-size: medium; text-align: center">Procedimientos a Seguir</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row form-group col-md-12">
                            <asp:GridView ID="GvProcedimientosSeguir" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/Icono/details_opt.png">
                                        <ItemStyle Height="10px" Width="10px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="PROCESO_NOMBRE" HeaderText="Proceso" SortExpression="PROCESO_NOMBRE" />
                                    <asp:BoundField DataField="TAREA_NOMBRE" HeaderText="Actividades" SortExpression="TAREA_NOMBRE" />
                                    <asp:BoundField DataField="TAREA_CODIGO" SortExpression="TAREA_CODIGO">
                                        <ItemStyle ForeColor="White" Width="0.1px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="modal-header" style="text-align: center; background-color: white; padding: 2px; margin-bottom: 15px;">
                            <label runat="server" visible="false" id="TareasRealizar" style="font-size: medium; text-align: center">Tareas A Realizar</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row form-group col-md-12">
                            <asp:GridView ID="GvTareasRealizar" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:BoundField DataField="TAREADET_NOMBRE" HeaderText="Tarea" SortExpression="TAREADET_NOMBRE" />
                                    <asp:BoundField DataField="TAREADET_DESCRIPCION" HeaderText="Descripción" SortExpression="TAREADET_DESCRIPCION" />
                                    <asp:BoundField DataField="TAREADET_VALOR_MINIMO" HeaderText="Valor Min." SortExpression="TAREADET_VALOR_MINIMO" />
                                    <asp:BoundField DataField="TAREADET_VALOR_MAXIMO" HeaderText="Valor Max." SortExpression="TAREADET_VALOR_MAXIMO" />
                                    <asp:BoundField DataField="TAREADET_ESTADO" HeaderText="Estado" SortExpression="TAREADET_ESTADO" />
                                    <asp:BoundField DataField="TAREADET_OPERADOR" HeaderText="Observación" SortExpression="TAREADET_OPERADOR" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
                
                <asp:Panel ID="panelUpdateProgress" runat="server" Width="200px" CssClass="updateProgress">
                    <asp:UpdateProgress ID="UpdateProg1" runat="server" DisplayAfter="0">
                        <ProgressTemplate>
                            <div style="position: relative; top: 30%; text-align: center;">
                                <img src="/Fotos/5.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </asp:Panel>

                <div style="text-align: left" >
                    <asp:Panel ID="Panel3" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 350px; border-right: black 1px outset; border-top: black 1px outset; border-left: black 1px outset; border-bottom: black 1px outset;" cancelcontrolid="btnCerrarTI">
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray; height: 25px;" valign="top">
                                </td>
                                <td align="left" colspan="3" style="background-color: darkgray; vertical-align: middle; height: 25px; text-align: center; " valign="top">
                                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Datos del Correo"></asp:Label></td>
                                <td align="left" style="width: 25px; background-color: darkgray; height: 25px;" valign="top">
                                </td>
                            </tr>
                            <tr>                    
                                <td align="left" style="width: 25px; background-color: darkgray; height: 22px;" valign="top"></td>
                                <td align="left" style="width: 100px; background-color: darkgray; height: 22px;" valign="middle">
                                        <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Contacto"></asp:Label></td>
                                <td align="left" style="width: 400px; background-color: darkgray; height: 22px;" valign="middle" colspan="2">
                                    <asp:TextBox id="TxtContactocorreo" runat="server" Width="150px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="17px"></asp:TextBox> 
                                </td>
                                <td align="left" style="width: 25px; background-color: darkgray; height: 22px;" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 100px;" valign="middle">
                                    <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Enviado"></asp:Label></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 200px;" valign="top" colspan="2">
                                    <asp:TextBox id="txtCorreoFecha" runat="server" Width="350px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="17px"></asp:TextBox> 
                                </td>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 100px;" valign="middle">
                                    <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Para"></asp:Label></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 200px;" valign="top" colspan="2">
                                    <asp:TextBox id="txtCorreoFrom" runat="server" Width="350px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="17px"></asp:TextBox> 
                                </td>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 100px;" valign="middle">
                                    <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Asunto"></asp:Label></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 200px;" valign="top" colspan="2">
                                    <asp:TextBox id="txtCorreoAsunto" runat="server" Width="350px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="17px"></asp:TextBox> 
                                </td>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 100px;" valign="middle">
                                    <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Mensaje"></asp:Label></td>
                                <td align="left" style="height: 22px; background-color: darkgray; width: 200px;" valign="top" colspan="2">
                                    <asp:TextBox id="txtCorreoBody" runat="server" Width="350px" Font-Size="8pt" Font-Names="Arial" BorderWidth="1px" BorderStyle="Outset" BorderColor="Black" Height="106px" TextMode="MultiLine"></asp:TextBox> 
                                </td>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                                <td align="left" colspan="3" style="vertical-align: middle; height: 22px; background-color: darkgray; text-align: left;"
                                    valign="top">
                                    <asp:Button ID="btnCerrarCorreo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                        Text="Cerrar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" />
                                    <asp:Button ID="btnAceptarCorreo" runat="server" BackColor="LightGray" BorderColor="Gray"
                                        BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                        Text="Aceptar" Width="52px" CssClass="EstiloBoton" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'" />
                                    </td>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                                <td align="left" colspan="3" style="height: 25px; background-color: darkgray; width: 500px;" valign="top"></td>
                                <td align="left" style="width: 25px; background-color: darkgray" valign="top"></td>
                            </tr>
                        </table>
                                          
                        <cc1:ModalPopupExtender 
                            id="ModalPopupExtender3" 
                                            runat="server" 
                                            TargetControlID="btnEvento"
                                            CancelControlID ="btnCerrarCorreo"
                                            PopupControlID ="Panel3" 
                                            CacheDynamicResults="True" 
                                            BackgroundCssClass="modalBackground" X="400" Y="400" >
                            </cc1:ModalPopupExtender> 
                        </asp:Panel>
                </div>          

                <cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
                    BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="DdlProceso" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DdlContacto" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboComponente" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboElemento" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtRuc" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnCerrarCorreo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>         
    </div>    

    <div id="ModalCliente" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="Label6" runat="server" Text="Busqueda de Cliente" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step3">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="txtBusRazon" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnListarTI" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigo">RUC :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="txtBusRuc" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnCerrarTI" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexTI" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="FlexTI" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField CommandName="AceptarTI" Text="Aceptar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                            <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="TBTICKET_CLIENTE_CIF" HeaderText="RUC">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TBTICKET_CLIENTE_NOMBRE" HeaderText="Razón Social">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TBTICKET_CLIENTE_CODIGO">
                                                            <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TBTICKET_CLIENTE_ESTADO">
                                                            <ItemStyle ForeColor="DarkGray" HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnListarTI" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCerrarTI" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalBuscar" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="TituloPopup1" runat="server" Text="Busqueda Base de Datos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step2">
                            <div class="panel panel-default">
                                <div class="panel-body">                                    
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                                        <ContentTemplate>
                                            <div class="row espacio">
                                                <div class="col-lg-9 col-xs-6">                                                    
                                                    <asp:Label ID="LblUbicacion" runat="server" Text="Modo de Busqueda :" CssClass="control-label-2" />
                                                    <asp:RadioButton GroupName="optModoBus" ID="rd0" runat="server" Text="A ó B" Checked="true" AutoPostBack="True" />
                                                    <asp:RadioButton GroupName="optModoBus" ID="rd1" runat="server" Text="A y B" AutoPostBack="True" />
                                                </div>
                                                <div class="col-lg-3 col-xs-6"> 
                                                    <asp:Button ID="btnListar" runat="server"  Text="Listar" ControlStyle-CssClass="form-control btn btn-default"  />
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-9 col-xs-6"> 
                                                    <asp:Label id="lbl2" runat="server" CssClass="control-label-2" Text="Palabras a Buscar:"></asp:Label>
                                                    <asp:CheckBox id="chkFiltros" runat="server" text="Sin filtros" CssClass="checkbox-inline" BorderWidth="0px"/>
                                                </div>
                                                <div class="col-lg-3 col-xs-6"> 
                                                    <asp:Button ID="btnCerrar" runat="server"  Text="Cerrar" ControlStyle-CssClass="form-control btn btn-default"  />
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-9 col-xs-6"> 
                                                    <asp:Label id="Label20" runat="server" CssClass="control-label-2" ForeColor="#0000C0"
                                                               Text="** Palabras a buscar separadas por una coma. Si no lo busca como una frase completa."></asp:Label>
                                                    <asp:TextBox id="txtBuscador" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3 col-xs-12"> 
                                                    <asp:Button ID="BtnNuevo" runat="server"  Text="Nuevo" ControlStyle-CssClass="form-control btn btn-default"  />
                                                </div>
                                            </div>
                                            <div id="lblIngreso" runat="server" visible="false" >
                                                <div class="row espacio">          
                                                    <div class="col-lg-3">  
                                                        <asp:Button ID="btnMGuardar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" />                    
                                                    </div>
                                                    <div class="col-lg-3">                       
                                                        <asp:Button ID="BtnMCancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar" />
                                                    </div>
                                                </div>
                                                <div class="row espacio">          
                                                    <div class="col-lg-12"> 
                                                        <asp:Label ID="lblError" runat="server" CssClass="control-label-2" ForeColor="red"></asp:Label>                  
                                                    </div>          
                                                </div>
                                                <div class="row espacio">          
                                                    <div class="col-lg-12"> 
                                                        <asp:Label ID="lblEtiqueta" runat="server" CssClass="control-label-2" ForeColor="Maroon"></asp:Label>                  
                                                    </div>          
                                                </div>
                                                <div class="row espacio">     
                                                    <div class="col-lg-3">   
                                                        <asp:Label ID="lblEtiqueta1" runat="server" CssClass="control-label-2" Text="Aplicativo"></asp:Label>
                                                        <asp:DropDownList ID="cboAplicativo" runat="server"  CssClass="form-control" AutoPostBack="True">
                                                        </asp:DropDownList>                    
                                                    </div>   
                                                    <div class="col-lg-3">   
                                                        <asp:Label ID="lblEtiqueta2" runat="server" CssClass="control-label-2" Text="Producto"></asp:Label>
                                                        <asp:DropDownList id="cboProducto" runat="server"  CssClass="form-control" AutoPostBack="True"></asp:DropDownList>                          
                                                    </div>
                                                    <div class="col-lg-3">   
                                                        <asp:Label ID="lblEtiqueta3" runat="server" CssClass="control-label-2" Text="Sub-Producto"></asp:Label>
                                                        <asp:DropDownList id="cboSubProd" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>                   
                                                    </div>
                                                </div>
                                                <div class="row espacio">     
                                                    <div class="col-lg-12">  
                                                        <asp:Label ID="lblEtiqueta4" runat="server" CssClass="control-label-2" Text="Transacción"></asp:Label>
                                                        <asp:TextBox ID="txtTransaccion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>                     
                                                    </div>   
                                                </div>
                                                <div class="row espacio">     
                                                    <div class="col-lg-12"> 
                                                        <asp:Label ID="lblEtiqueta5" runat="server" CssClass="control-label-2" Text="Consulta"></asp:Label>
                                                        <asp:TextBox ID="txtConsulta" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>                      
                                                    </div>   
                                                </div>
                                                <div class="row espacio">     
                                                    <div class="col-lg-12"> 
                                                        <asp:Label ID="lblEtiqueta6" runat="server" CssClass="control-label-2" Text="Solución"></asp:Label>
                                                        <asp:TextBox ID="txtMSolucion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>                      
                                                    </div>   
                                                </div>
                                                <div class="row">                        
                                                    <asp:TextBox ID="txtCodConsulta" runat="server" Text="" CssClass="form-control" visible="false"></asp:TextBox>
                                                </div>                                                
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" DataKeyNames="CARCON_APLICATIVO,CARCON_PRODUCTO,CARCON_SUBPRODUCTO" >
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Aceptar" Text="Aceptar" ButtonType="Image" ImageUrl="~/icono/ok.png">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" ></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_APLICATIVO">
                                                                <ItemStyle Width="0px" ForeColor="White" BorderColor="White"></ItemStyle>
                                                                <HeaderStyle Width="0px" BorderColor="White"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_PRODUCTO">
                                                                <ItemStyle Width="0px" ForeColor="White" BorderColor="White"></ItemStyle>
                                                                <HeaderStyle Width="0px" BorderColor="White"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_SUBPRODUCTO">
                                                                <ItemStyle Width="0px" ForeColor="White" BorderColor="White"></ItemStyle>
                                                                <HeaderStyle Width="0px" BorderColor="White"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CARCON_CODIGO">
                                                                <ItemStyle ForeColor="White" Width="0px"  BorderColor="White"/>
                                                                <HeaderStyle Width="0px" BorderColor="White"></HeaderStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalUbicacion" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="TituloPopup" runat="server" Text="Busqueda de Centro de Costos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="BuscarDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnBuscar" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="BuscarCodigo" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnCancelar" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvBusqueda" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="GvBusqueda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="CodInterno" HeaderText="Código" SortExpression="CodInterno" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion" />
                                                        <asp:BoundField DataField="Direccion"  HeaderText="Dirección" SortExpression="Codigo"/>
                                                        <asp:BoundField DataField="Ubigeo"  HeaderText="Ubigeo" SortExpression="Codigo"/>
                                                        <asp:BoundField DataField="Codigo" SortExpression="Codigo">
                                                            <ItemStyle ForeColor="White" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalTablaEsp" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="Label12" runat="server" Font-Size="14px" class="control-label2" Text="Elementos de Tablas Especiales" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step5">
                            <div class="panel panel-default">
                                <div class="panel-body">                  
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>     
                                            <div class="row espacio">          
                                                <div class="col-lg-3">                
                                                    <asp:DropDownList ID="cboTabla" runat="server"  CssClass="form-control" AutoPostBack="True" ></asp:DropDownList>
                                                </div>        
                                                <div class="col-lg-2">  
                                                    <asp:Button ID="btnTENuevo" runat="server" Text="Nuevo" CssClass="form-control btn btn-default" />                    
                                                </div>     
                                                <div class="col-lg-2">
                                                    <asp:Button ID="BtnTECerrar" runat="server" class="form-control btn btn-default" Text="Cerrar"/>
                                                </div>    
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btnTEGuardar" runat="server" Text="Guardar" CssClass="form-control btn btn-default" />
                                                </div>   
                                                <div class="col-lg-2">
                                                    <asp:Button ID="btnTECancelar" runat="server" Text="Cancelar" CssClass="form-control btn btn-default" />
                                                </div>       
                                            </div>   
                                            <asp:Label ID="lblTabla3" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                                            <asp:Label ID="lblTabla2" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                                            <asp:Label ID="lblTabla1" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                                            <div id="lblIngresoTE" runat="server" visible ="False" >                                
                                                <div class="row espacio">           
                                                    <div class="col-lg-12">
                                                        <asp:Label ID="lblEtiquetaTE" runat="server" CssClass="control-label-2" ForeColor="Maroon" Font-Bold="True"  ></asp:Label>
                                                    </div>  
                                                </div>                                   
                                                <div class="row espacio">           
                                                    <div class="col-lg-12">                                        
                                                    <asp:Label ID="Label13" runat="server" CssClass="control-label-2" Text="Nivel 1"></asp:Label>       
                                                    <asp:DropDownList ID="cboNivel1" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                    </div>  
                                                </div>                               
                                                <div class="row espacio">           
                                                    <div class="col-lg-12">                                        
                                                        <asp:Label ID="Label15" runat="server" CssClass="control-label-2" Text="Nivel 2"></asp:Label>       
                                                        <asp:DropDownList ID="cboNivel2" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                    </div>  
                                                </div>             
                                                <div class="row espacio">           
                                                    <div class="col-lg-2">
                                                        <asp:Label ID="lblTE7" runat="server" Text="Código" CssClass="control-label-2"></asp:Label>
                                                        <asp:TextBox ID="txtTECodigo" runat="server"  CssClass="form-control"></asp:TextBox>
                                                    </div>                               
                                                    <div class="col-lg-4">
                                                        <asp:Label ID="lblTE3" runat="server" Text="Nombre" CssClass="control-label-2"></asp:Label>
                                                        <asp:TextBox ID="txtTEDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>      
                                                </div>   
                                                <div class="row espacio">           
                                                    <div class="col-lg-12">
                                                        <asp:TextBox ID="txtTEDescripcionE" runat="server" CssClass="form-control" Visible="False" ></asp:TextBox>
                                                    </div>  
                                                </div>   
                                            </div>  
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="FlexTE" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="C1" />
                                                            <asp:BoundField DataField="C2" >
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="C3"  >
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="C4"  >
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </div>
                                            </div>       
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cboNivel1" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="cboTabla" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="btnTENuevo" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnTEGuardar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnTECancelar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>     
                                 </div> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

