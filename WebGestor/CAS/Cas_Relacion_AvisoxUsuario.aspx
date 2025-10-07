<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Relacion_AvisoxUsuario.aspx.vb" Inherits="CAS_Cas_Relacion_AvisoxUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .ajax__calendar_container { z-index : 1000 ; border: 1px solid #646464; background-color: White; color: Navy; width: 200px; }
        .ajax__calendar_body { width: 200px; font-size : 1em; color: black;}
             .ajax__calendar_header,
             .ajax__calendar_title,
             .ajax__calendar_dayname,
             .ajax__calendar_day { font-size : 1em; color: black;}
             .ajax__calendar_hover .ajax__calendar_day,
             .ajax__calendar_hover .ajax__calendar_month,
             .ajax__calendar_hover .ajax__calendar_year { font-size : 1em; color: red;}
        .ajax__calendar {
            position: relative;
            left: 0px !important;
            top: 0px !important;
            visibility: visible; display: block;
            }
        .ajax__calendar iframe
        {
            left: 0px !important;
            top: 0px !important;
        }
        
        /*.ajax__calendar_container { border: 1px solid #646464; background-color: White; color: Navy; width: 200px;}*/
            /* cuerpo */
            /* formato de la información mostrada */
            /* cuando colocamos el mouse en algún campo */

    </style>


    <div class="container contenedor">
        <div class="row mt-4 border rounded border-white">
            <!--Zona 1-->
            <div class="col-12 col-md-4 p-0">
                <!--Zona 1.1 (1)-->
                <div class="row p-2 m-1 border rounded">
                    <div class="col-12 mb-1">
                        <label class="h5 m-0" for="busqueda">Busqueda: </label>
                    </div>
                    <div class="col-12 overflow-hidden mb-1">
                        <input type="text" name="txtBusqueda" class="form-control" id="busqueda" placeholder="Ingresar palabra"/>
                    </div>
                    <div class="col-12 mt-1">
                        <p class="lead m-0">Filtros: </p>
                        <div class="row">
                            <div class="col-12 mb-1">
                                <asp:TextBox ID="TxtFecha" runat="server" CssClass="w-100" PopupButtonID="TxtFecha"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" TargetControlID="TxtFecha"></cc1:CalendarExtender>
                            </div>
                            <div class="col-12 mb-1">                    
                                <asp:DropDownList ID="DdlEstado"  CssClass="custom-select" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-12 mb-1">                    
                                <asp:DropDownList ID="DdlTipo" CssClass="custom-select" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Zona 1.2 (2)-->
                <div class="row p-2 m-1 border rounded">
                    <div class="col-12">
                        <p class="h5">Listado: </p>
                        <p class="lead">@ViewBag.listaAvisos</p>
                        <dl class="row">
                            @foreach (var item in Model)
                            {
                                <dd class="p-0 col-5 overflow-hidden border-top d-flex align-items-center mt-1 ml-1">@item.AVISO_DESCRIPCION</dd>
                                <dd class="p-0 col-6 overflow-hidden border-top d-flex align-items-center mt-1">@item.AVISO_DETALLE</dd>
                                <button></button>
                                <a href="~/Hacdata/Index?empresa=@item.EMPRESA_CODIGO&aviso=@item.AVISO_NRO" class="btn btn-success float-right">Listar</a> <!--buscar-->
                            }
                        </dl>
                    </div>
                </div>
            </div>

            <!--Zona 2-->
            <div class="col-12 col-md-8 p-0">
                <!--Zona 2.1 (3)-->
                <div class="row p-2 m-1 border rounded">
                    <div class="col-12 col-md-8">
                        <h6 class="d-inline-block">Asunto</h6>
                        <!--
                            <p>Quien lo publica</p>
                            <p>Nro aviso: 1234</p>
                        -->
                        <p class="lead text-center">
                            <em>No hay data</em>
                            <img src="../Fotos/nodatafound.png" class="img-fluid" />
                        </p>
                    </div>
                    <div class="col-12 col-md-4">
                        <button class="d-inline-block float-right btn btn-success">Realizar comentario</button>
                    </div>
                </div>

                <!--Tabla invertida opinion-->
                <!--Zona 2.2 (4)-->
                <div class="row p-2 m-1 border rounded">
                    <div class="col">
                        <h6 class="d-inline-block">DETALLE</h6>
                        <div class="row">
                            <div class="col">
                                <p class="lead text-center">
                                    <em>No hay data</em>
                                    <img src="../Fotos/nodatafound.png" class="img-fluid" />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Zona 2.3 (5)-->
                <div class="row p-2 m-1 border rounded">
                    <div class="col-12">
                        <p class="h5 m-0">Listado de comentarios</p>
                        <div class="row">
                            <div class="col-12 mt-2">
                                <p class="lead text-center">
                                    <em>No hay data</em>
                                    <img src="../Fotos/nodatafound.png" class="img-fluid" />
                                </p>
                            </div>

                            <!--
                            <div class="col-12 border-top mt-2">
                                <label class="m-0" for="comentario">NombrePersona</label>
                                <p class="float-right m-0">19/08/2020 19:47</p>
                                <input type="text" disabled name="txtComentario" id="comentario" class="w-100" value="comentario de persona">
                            </div>
                                -->
                        </div>

                    </div>
                </div>

                <!--Zona 2.4 (6)-->
                <div class="row p-2 m-1 border rounded">
                    <div class="col-12">
                        <p class="h5">Listado adjuntos: </p>
                        <div class="row">
                            <div class="col-12 mt-2">
                                <p class="lead text-center">
                                    <em>No hay data</em>
                                    <img src="../Fotos/nodatafound.png" class="img-fluid" />
                                </p>
                            </div>
                        </div>
                        <!--
                        <dl class="row text-center">
                            <dt class="col-2"><img src="#" alt="ico"></dt>
                            <dd class="col-10"><a href="#">Link</a></dd>
                            <dt class="col-2"><img src="#" alt="ico"></dt>
                            <dd class="col-10"><a href="#">Link</a></dd>
                        </dl>
                            -->
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

