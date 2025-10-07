<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Eventos.aspx.vb" Inherits="Eventos_Eventos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <h1 class="Titulos">Definición de Eventos</h1>
        <div class="row espacio">
            <div class="col-lg-2">
                <asp:Button ID="BtnListar" runat="server" text="Listar" CssClass="form-control btn-default" /> 
            </div>
            <div class="col-lg-2">
                <asp:Button ID="BtnNuevo" runat="server" text="Nuevo Evento" CssClass="form-control btn-default" /> 
            </div>
        </div>        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="DivEvento" runat="server" visible="false" >                    
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label id="LblEtiq1" runat="server" CssClass="control-label-2" text="Datos del Evento" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </div>
                    </div>                
                    <div class="row espacio">
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq2" runat="server" CssClass="control-label-2" text="Código del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvCodigo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label id="LblEtiq3" runat="server" CssClass="control-label-2" text="Tipo del Evento" ></asp:Label>
                            <asp:DropDownList ID="DdlTipo" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq4" runat="server" CssClass="control-label-2" text="Nombre del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvNombre" runat="server" CssClass="form-control" MaxLength="100" ></asp:TextBox>
                        </div>
                    </div>    
                    <div class="row espacio">
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq5" runat="server" CssClass="control-label-2" text="Objetivo del Evento"></asp:Label>
                            <asp:TextBox ID="TxtEvObjetivo" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq6" runat="server" CssClass="control-label-2" text="Descripción del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq9" runat="server" CssClass="control-label-2" Text="Fecha Inicia"></asp:Label>
                            <asp:TextBox ID="TxtFechaIni" runat="server"  CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq10" runat="server" CssClass="control-label-2" Text="Fecha Termina"></asp:Label>
                            <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq11" runat="server" CssClass="control-label-2"  Text="Hora Inicia"></asp:Label>
                            <asp:TextBox ID="TxtHoraIni" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                       </div> 
                        <div class="col-md-2">
                            <asp:Label ID="LblEtiq12" runat="server" CssClass="control-label-2" Text="Hora Termina"></asp:Label>
                            <asp:TextBox ID="TxtHoraFin" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                        </div> 
                    </div>          
                    <div class="row espacio">
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq14" runat="server" CssClass="control-label-2" text="Dirección del Evento" ></asp:Label>
                            <asp:TextBox ID="TxtEvDireccion" runat="server" CssClass="form-control"  TextMode="MultiLine" MaxLength="500" ></asp:TextBox>
                        </div>
                    </div>    
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label id="LblEtiq18" runat="server" CssClass="control-label-2" text="País" ></asp:Label>
                            <asp:DropDownList ID="DdlEvPais" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label id="LblEtiq15" runat="server" CssClass="control-label-2" text="Departamento" ></asp:Label>
                            <asp:DropDownList ID="DdlEvDpto" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label id="LblEtiq16" runat="server" CssClass="control-label-2" text="Provincia" ></asp:Label>
                            <asp:DropDownList ID="DdlEvProv" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label id="LblEtiq17" runat="server" CssClass="control-label-2" text="Distrito" ></asp:Label>
                            <asp:DropDownList ID="DdlEvDist" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                    </div>    
                    <div class="row espacio">                        
                        <div class="col-lg-6">
                            <asp:Label id="LblEtiq13" runat="server" CssClass="control-label-2" text="Responsable del Evento" ></asp:Label>
                            <asp:DropDownList ID="DdlResponsable" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <asp:Label id="LblEtiq7" runat="server" CssClass="control-label-2" text="Contacto" ></asp:Label>
                            <asp:TextBox ID="TxtEvContacto" runat="server" CssClass="form-control"  MaxLength="150" ></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <asp:Label id="LblEtiq8" runat="server" CssClass="control-label-2" text="Teléfono" ></asp:Label>
                            <asp:TextBox ID="TxtEvContactoTelef" runat="server" CssClass="form-control"  TextMode="Phone"  MaxLength="50" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row espacio">
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-2">
                            <asp:Button ID="BtnGuardar" runat="server" text="Guardar" CssClass="form-control btn-default" /> 
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="BtnCancelar" runat="server" text="Cancelar" CssClass="form-control btn-default" /> 
                        </div>
                    </div> 
                </div>
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:Label id="LblRegistro" runat="server" CssClass="control-label-2" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </div>
                </div>     
                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView id="GvEventos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Edita" Text="Editar" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="Participantes" Text="Participantes" ButtonType="Button">
                                    <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                    <ItemStyle Height="10px" Width="10px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="EVENTO" HeaderText="Evento">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TIPO_EVENTO" HeaderText="Tipo">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_NOMBRE" HeaderText="Nombre del Evento">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_INICIA_EVENTO" HeaderText="Evento Inicia">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_TERMINA_EVENTO" HeaderText="Evento Termina">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_INICIA_EVENTO" HeaderText="Hora Inicia">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="HORA_TERMINA_EVENTO" HeaderText="Hora Termina">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_OBJETIVO" HeaderText="Objetivo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_DESCRIPCION" HeaderText="Descripción">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_CONTACTO" HeaderText="Contacto">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_CONTACTO_TELEFONO" HeaderText="Contacto Teléfono">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RESPONSABLE" HeaderText="Responsable">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ESTADO" HeaderText="Estado">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_DIRECCION" HeaderText="Dirección">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_ESTADO" HeaderText="">
                                    <ItemStyle ForeColor="white"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EVENTO_RESPONSABLE" HeaderText="">
                                    <ItemStyle ForeColor="white"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GvEventos" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnNuevo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="BtnGuardar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel> 
    </div>

    <div id="ModalUsuario" class="modal fade" role="dialog" data-backdrop="static"  style="position: fixed; top: 0%;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <div class="col-lg-12 col-sm-6" >
                        <asp:Label ID="LblTituloModal" runat="server" class="ModalTitulos" Text="Relación de Participantes" />
                    </div> 
                </div> 
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row espacio">
                                                <div class="col-lg-2">
                                                    <asp:Label ID="LblEM1" runat="server" CssClass="control-label2" Text="Evento" />
                                                    <asp:TextBox ID="TxtMEventoCodigo" runat="server" CssClass="form-control"  ReadOnly ="true" />
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:Label ID="LblEM2" runat="server"  CssClass="control-label2" Text="Tipo"/>
                                                    <asp:TextBox ID="TxtMEvnetoTipo" runat="server" CssClass="form-control" ReadOnly ="true"  />
                                                </div>
                                                <div class="col-lg-8">
                                                    <asp:Label ID="LblEM3" runat="server" CssClass="control-label2" Text="Nombre" />
                                                    <asp:TextBox ID="TxtMEventoNombre" runat="server" CssClass="form-control"  ReadOnly ="true" />
                                                </div>
                                            </div> 
                                            <div class="row espacio">                    
                                                <div class="col-lg-6">
                                                    <asp:Label ID="LblEM4" runat="server" CssClass="control-label2" Text="Ojetivo" />
                                                    <asp:TextBox ID="TxtMEventoObjetivo" runat="server" CssClass="form-control" ReadOnly ="true"  />
                                                </div>               
                                                <div class="col-lg-6">
                                                    <asp:Label ID="LblEM5" runat="server" CssClass="control-label2" Text="Descripción" />
                                                    <asp:TextBox ID="TxtMEventoDescripcion" runat="server" CssClass="form-control"  ReadOnly ="true" />
                                                </div>
                                            </div> 
                                            <div class="row espacio">                    
                                                <div class="col-lg-2">
                                                    <asp:Label ID="LblEM6" runat="server" CssClass="control-label2" Text="Inicia" />
                                                    <asp:TextBox ID="TxtMEventoFechaInicia" runat="server" CssClass="form-control"  ReadOnly ="true"  />
                                                </div>               
                                                <div class="col-lg-2">
                                                    <asp:Label ID="LblEM7" runat="server" CssClass="control-label2" Text="Finaliza" />
                                                    <asp:TextBox ID="TxtMEventoFechaTermina" runat="server" CssClass="form-control"  ReadOnly ="true" />
                                                </div>            
                                                <div class="col-lg-2">
                                                    <asp:Label ID="LblEM8" runat="server" CssClass="control-label2" Text="Hora Empieza" />
                                                    <asp:TextBox ID="TxtMEventoHoraInicia" runat="server" CssClass="form-control"  ReadOnly ="true" />
                                                </div>       
                                                <div class="col-lg-2">
                                                    <asp:Label ID="LblEM9" runat="server" CssClass="control-label2" Text="Hora Termina" />
                                                    <asp:TextBox ID="TxtMEventoHoraFin" runat="server" CssClass="form-control" ReadOnly ="true"  />
                                                </div>                                  
                                            </div> 
                                            <div class="row espacio">                    
                                                <div class="col-lg-12">
                                                    <asp:Label ID="LblEM10" runat="server" CssClass="control-label2" Text="Dirección" />
                                                    <asp:TextBox ID="TxtMEventoDireccion" runat="server" CssClass="form-control"  ReadOnly ="true" />
                                                </div>                                        
                                            </div> 
                                            <div class="row espacio">                    
                                                <div class="col-lg-10">
                                                    <asp:Label ID="LblEM11" runat="server" CssClass="control-label2" Text="Contacto" />
                                                    <asp:TextBox ID="TxtMEventoContacto" runat="server" CssClass="form-control" ReadOnly ="true"  />
                                                </div>                     
                                                <div class="col-lg-2">
                                                    <asp:Label ID="Label6" runat="server" CssClass="control-label2" Text="Teléfono" />
                                                    <asp:TextBox ID="TxtMEventoContactoTelef" runat="server" CssClass="form-control" ReadOnly ="true"  />
                                                </div>                                     
                                            </div> 
                                            <div class="row espacio">                    
                                                <div class="col-lg-8">
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:Button ID="BtnParticipantes" runat="server" Text="Ingresar" CssClass="form-control btn btn-default" />
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:Button ID="BtnRelacionCerrar" runat="server" Text="Cerrar" CssClass="form-control btn btn-default" />
                                                </div>
                                            </div>
                                            <div id="DivParticipantes" runat="server" visible="false" >
                                                <div class="row espacio">    
                                                    <div class="col-lg-10">
                                                        <asp:Label id="LblEtq21" runat="server" CssClass="control-label-2" text="Personal del Evento" ></asp:Label>
                                                        <asp:DropDownList ID="DdlUsuario" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Label ID="Label5" runat="server" CssClass="control-label-2"  Text="Guardar" ForeColor="white"></asp:Label>
                                                        <asp:Button ID="BtnMGuardar" runat="server" Text="Guardar" CssClass="form-control btn btn-default" />
                                                    </div>
                                                </div>
                                                <div class="row espacio">
                                                    <div class="col-lg-3">
                                                        <asp:Label ID="Label1" runat="server" CssClass="control-label-2"  Text="Fecha Inicia"></asp:Label>
                                                        <asp:TextBox ID="TxtMFechaIni" runat="server"  CssClass="form-control" TextMode="Date" ></asp:TextBox>
                                                    </div> 
                                                    <div class="col-lg-3">
                                                        <asp:Label ID="Label3" runat="server" CssClass="control-label-2"  Text="Hora Ingresa"></asp:Label>
                                                        <asp:TextBox ID="TxtMHoraIni" runat="server"  CssClass="form-control" TextMode="Time" ></asp:TextBox>
                                                   </div> 
                                                    <div class="col-lg-3">
                                                        <asp:Label ID="Label4" runat="server" CssClass="control-label-2"  Text="Hora Salida"></asp:Label>
                                                        <asp:TextBox ID="TxtMHoraFin" runat="server"  CssClass="form-control"  TextMode="Time"   ></asp:TextBox>
                                                    </div> 
                                                    <div class="col-lg-1">
                                                    </div> 
                                                    <div class="col-lg-2">
                                                        <asp:Label ID="Label2" runat="server" CssClass="control-label-2"  Text="Cancelar" ForeColor="white"></asp:Label>
                                                        <asp:Button ID="BtnMCancelar" runat="server" Text="Cancelar" CssClass="form-control btn btn-default" />
                                                    </div>
                                                </div>    
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="LblRegParticipantes" runat="server" CssClass="control-label-2" Text="" ForeColor="Maroon" ></asp:Label>
                                                </div>
                                            </div> 
                                            <div class="row espacio">
                                                <div class="col-lg-12">
                                                    <asp:GridView ID="gvUsuario"  runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button">
                                                                <ControlStyle CssClass="Form-control btn btn-default"></ControlStyle>
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="EVEPART_PERSONAL" HeaderText="Código" SortExpression="EVEPART_PERSONAL" />
                                                            <asp:BoundField DataField="PARTICIPANTE" HeaderText="Nombres y Apellidos" SortExpression="PARTICIPANTE" />
                                                            <asp:BoundField DataField="FECHA" HeaderText="Fecha" SortExpression="FECHA" />
                                                            <asp:BoundField DataField="HORA_INGRESA" HeaderText="Hora Ingreso" SortExpression="HORA_INGRESA" />
                                                            <asp:BoundField DataField="HORA_SALIDA" HeaderText="Hora Salida" SortExpression="HORA_SALIDA" />                                                        </Columns>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvEventos" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="gvUsuario" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnMGuardar" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnSi" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnNo" EventName="Click" />
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

    
    <div id="ModalMensaje" class="modal fade" role="dialog" data-backdrop="static" style="position: fixed; top: 0%;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row espacio">
                                <div class="col-md-12">
                                    <asp:Label runat="server" ID="Mensaje" class="col-lg-12" />
                                </div>
                            </div>
                            <div class="row espacio">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="BtnSi" CssClass="btn btn-default" runat="server" Text="Si" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="BtnNo" CssClass="btn btn-default" runat="server" Text="No" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="BtnMGuardar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

