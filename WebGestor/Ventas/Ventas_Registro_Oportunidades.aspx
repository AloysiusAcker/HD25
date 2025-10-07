<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Ventas_Registro_Oportunidades.aspx.vb" Inherits="Ventas_Ventas_Registro_Oportunidades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Ventas - Registro Oportunidades" CssClass="Titulos" />
            </div> 
        </div>
        <br />

        <div class="row">
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
            </div> 
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" ControlStyle-CssClass="form-control btn btn-default"/>
            </div> 
            <div class="col-md-2 col-xs-6">
                <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default"/>
            </div> 
        </div>
        
        <div class="row">            
            <div class="col-md-6">
                <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Vendedor"></asp:Label>
                <asp:DropDownList ID="DdlVendedor" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
            </div>   
        </div> 
        <div class="row">
            <div class="col-md-2">                                
            <asp:CheckBox ID="ChkFecha" CssClass="checkbox checkbox-inline" Text="Fecha" Font-Bold ="true" runat="server" AutoPostBack="True" />
            </div>             
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text="" Enabled ="false" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text="" Enabled ="false" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
            </div>  
        </div>
        <br />
        <div class="row">                    
            <div class="col-md-12">
                <asp:Label ID="lblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
            </div> 
        </div>    
        <div class="row">                    
            <div class="col-md-12">
                <asp:GridView ID="GvListaOportunidades" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                    <Columns>
                        <asp:ButtonField CommandName="Ingresar" Text="Seguimiento" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                            <ControlStyle CssClass="btn btn-default"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                            <ControlStyle CssClass="btn btn-default"></ControlStyle>
                        </asp:ButtonField>
                        <asp:BoundField DataField="OPP_CODIGO" HeaderText="Nro." SortExpression="OPP_CODIGO" />
                        <asp:BoundField DataField="OPP_FECHA" HeaderText="Fecha" SortExpression="OPP_FECHA" />
                        <asp:BoundField DataField="OPP_CLIENTE_RUC" HeaderText="RUC" SortExpression="OPP_CLIENTE_RUC" />
                        <asp:BoundField DataField="OPP_CLIENTE_RAZON" HeaderText="Razón social" SortExpression="OPP_CLIENTE_RAZON" />
                        <asp:BoundField DataField="OPP_CLIENTE_CONTACTO_APELLIDOS" HeaderText="Apellidos" SortExpression="OPP_CLIENTE_CONTACTO_APELLIDOS" />
                        <asp:BoundField DataField="OPP_CLIENTE_CONTACTO_NOMBRES" HeaderText="Nombres" SortExpression="OPP_CLIENTE_CONTACTO_NOMBRES" />
                        <asp:BoundField DataField="OPP_CLIENTE_CONTACTO_EMAIL" HeaderText="Email" SortExpression="OPP_CLIENTE_CONTACTO_EMAIL" />
                        <asp:BoundField DataField="OPP_CLIENTE_CONTACTO_TELEFONO_1" HeaderText="Teléfono" SortExpression="OPP_CLIENTE_CONTACTO_TELEFONO_1" />
                        <asp:BoundField DataField="OPP_OPORTUNIDAD" HeaderText="Oportunidad" SortExpression="OPP_OPORTUNIDAD" />
                        <asp:BoundField DataField="Requerimiento" HeaderText="Requerimiento" SortExpression="Requerimiento" />
                        <asp:BoundField DataField="OPP_COMENTARIO" HeaderText="Comentario" SortExpression="OPP_COMENTARIO" />
                        <asp:BoundField DataField="pEstado" HeaderText="Estado" SortExpression="pEstado" />
                        <asp:BoundField DataField="VENDEDOR" HeaderText="Vendedor" SortExpression="VENDEDOR" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>  
    </div> 

    <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                        <asp:Label ID="LblTituloModal" runat="server" CssClass="subTitulos" Text="-" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvListaOportunidades" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step4">
                            <div class="panel panel-default">
                                <div class="panel-body">   
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="BtnCerrarSeg" runat="server" class="form-control btn btn-default" Text="Cerrar" />
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>                                       
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>                
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblEtiq35"  CssClass="control-label-2" runat="server" Text=""></asp:Label>                
                                                    <asp:GridView ID="GvSeguimiento" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:BoundField DataField="OPPSEG_CODIGO" HeaderText="Seg." SortExpression="OPPSEG_CODIGO" />
                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
                                                            <asp:BoundField DataField="hora" HeaderText="Hora" SortExpression="hora" /> 
                                                            <asp:BoundField DataField="TIPO_SEG" HeaderText="Tipo" SortExpression="TIPO_SEG" />
                                                            <asp:BoundField DataField="OPPSEG_DESCRIPCION" HeaderText="Descripción" SortExpression="OPPSEG_DESCRIPCION" />
                                                            <asp:BoundField DataField="TIPO_ACCION" HeaderText="Próxima Acción" SortExpression="TIPO_ACCION" />
                                                            <asp:BoundField DataField="FECHA_ACC" HeaderText="Prox. Fecha acc." SortExpression="FECHA_ACC" />
                                                            <asp:BoundField DataField="HORA_ACC" HeaderText="Prox. Hora Acc." SortExpression="HORA_ACC" />
                                                            <asp:BoundField DataField="FECHA_REG" HeaderText="Fecha Registro" SortExpression="FECHA_REG" />
                                                            <asp:BoundField DataField="HORA_REG" HeaderText="Hora Registro" SortExpression="HORA_REG" />
                                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div> 
                                            </div>        
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaOportunidades" EventName="RowCommand" />
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


    <div id="ModalOportunidad" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Ingresar Nueva Oportunidad</h4>
                </div>
                <div class="modal-body" style="padding: 20px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>   
                                <h4>Datos de la oportunidad</h4>
                                <div class="row">        
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_ENroDoc">Nro. Registro</label>
                                        <input class="form-control" id="txtNroReg" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>  
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_ENroDoc">Fecha Registro</label>
                                        <asp:TextBox ID="TxtFechaReg" runat="server" CssClass="form-control" Text="" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaReg" Format="dd/MM/yyyy" PopupButtonID="TxtFechaReg" ></cc1:CalendarExtender>
                                    </div>      
                                    <div class="col-md-3 col-xs-3">
                                        <label class="control-label" for="id_ENroDoc" style="color:white;" >Cerrar</label>
                                        <asp:Button ID="BtnECerrar" runat="server" Text="Cerrar" CssClass="form-control btn-success" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <label class="control-label" for="id_ENroDoc" style="color:white;" >Guardar</label>
                                        <asp:Button ID="BtnEGuardar" runat="server" Text="Grabar" CssClass="form-control btn-success"/>
                                    </div>     
                                </div>        
                                <div class="row">
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Ruc">RUC</label>
                                        <input class="form-control" id="TxtRUC" name="Descripcion" type="text" runat="server" />
                                    </div>   
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_ENroDoc" style="color:white;" >Bus</label>
                                        <asp:Button ID="BtnBuscaRuc" runat="server" Text="..." ControlStyle-CssClass="btn btn-block" />
                                    </div>
                                </div>
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_RazonSocial">Razón Social</label>
                                        <input class="form-control" id="TxtRazonScial" name="Descripcion" type="text" runat="server" />
                                    </div> 
                                </div>             
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_EDireccion">Dirección</label>
                                        <textarea id="TxtDireccion" cols="20" rows="3" class="form-control" runat="server" ></textarea>
                                    </div> 
                                </div>                                                     
                                <div class="row"> 
                                    <div class="col-md-6 col-xs-12">
                                        <label class="control-label" for="id_EPais">Pais</label>
                                        <asp:DropDownList ID="DdlPais" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-6 col-xs-12">
                                        <label class="control-label" for="id_EDpto">Departamento</label>
                                        <asp:DropDownList ID="DdlDpto" runat="server" CssClass="form-control" AutoPostBack="True"  />
                                    </div> 
                                </div>  
                                <div class="row"> 
                                    <div class="col-md-6 col-xs-12">
                                        <label class="control-label" for="id_EProv">Provincia</label>
                                        <asp:DropDownList ID="DdlProv" runat="server" CssClass="form-control" AutoPostBack="True" />
                                    </div> 
                                    <div class="col-md-6 col-xs-12">
                                        <label class="control-label" for="id_EDistrito">Distrito</label>
                                        <asp:DropDownList ID="DdlDist" runat="server" CssClass="form-control" />
                                    </div> 
                                </div>   
                                
                                <h4>Datos del Contacto</h4>

                                <div class="row">
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_CApePat">Ape. Paterno</label>
                                        <input class="form-control" id="TxtCApePat" name="Descripcion" type="text" runat="server" />
                                    </div>    
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_CApeMat">Ape. Materno</label>
                                        <input class="form-control" id="TxtCApeMat" name="Descripcion" type="text" runat="server" />
                                    </div>   
                                </div>
                                <div class="row"> 
                                    <div class="col-md-12">
                                        <label class="control-label" for="id_CNombre">Nombres</label>
                                        <input class="form-control" id="TxtCNombres" name="Descripcion" type="text" runat="server" />
                                    </div> 
                                </div>
                                <div class="row"> 
                                    <div class="col-md-3">
                                        <label class="control-label" for="id_CTelefono">Teléfono</label>
                                        <asp:Textbox ID="TxtCTelef" runat="server" CssClass="form-control" />
                                    </div> 
                                    <div class="col-md-3">
                                        <label class="control-label" for="id_CTelefono2">Teléfono</label>
                                        <asp:Textbox ID="TxtCTelef2" runat="server" CssClass="form-control" />
                                    </div> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_EDireccion">Correo Electrónico</label>
                                        <asp:Textbox ID="TxtCEmail" runat="server" CssClass="form-control" />
                                    </div> 
                                </div>
                                <div class="row"> 
                                </div>

                                <h4>Datos del Requerimiento</h4>

                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_RVendedor">Vendedor</label>
                                        <asp:DropDownList ID="DDlRVendedor" runat="server" CssClass="form-control" />
                                    </div>
                                </div>  
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_Requerimiento">Requerimiento</label>
                                        <asp:DropDownList ID="DDlReque" runat="server" CssClass="form-control" />
                                    </div> 
                                </div>  
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_RDetalle">Detalle</label>
                                        <textarea id="TxtRDetalle" cols="20" rows="3" class="form-control" runat="server" ></textarea>
                                    </div> 
                                </div>                             
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnECerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnEGuardar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnNuevo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DdlDpto" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="DdlProv" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="BtnBuscaRuc" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div> 

    <div id="ModalSeguimiento" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Seguimiento de la Oportunidad</h4>
                </div>
                <div class="modal-body" style="padding: 10px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate> 
                                <h4>Datos del Seguimiento</h4>
                                
                                <div class="row">
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Segui6">Fecha</label>
                                        <asp:TextBox ID="TxtSeguiFecha" runat="server" CssClass="form-control" ></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="custom-calendar" TargetControlID="TxtSeguiFecha" Format="dd/MM/yyyy" PopupButtonID="TxtSeguiFecha" ></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-9 col-xs-6">
                                        <label class="control-label" for="id_Segui5">Tipo</label>
                                        <asp:DropDownList ID="DdlSeguiTipo" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_Segui4">Descripción</label>
                                        <textarea id="TxtSeguiDescripcion" cols="20" rows="3" class="form-control" runat="server" ></textarea>
                                    </div> 
                                </div> 
                                <div class="row">   
                                    <div class="col-md-6 col-xs-6">                                
                                        <asp:CheckBox ID="ChkProxAccion" CssClass="checkbox checkbox-inline" Text="Siguiente Acción" Font-Bold ="true" runat="server" AutoPostBack="True" />
                                                   
                                    </div> 
                                </div> 
                                <div id="DivAccion" runat="server" visible="false" >
                                    <div class="row">  
                                        <div class="col-md-6 col-xs-6">
                                            <label class="control-label" for="id_Segui5">Próxima Acción</label>
                                            <asp:DropDownList ID="DdlProxAcc" runat="server" CssClass="form-control" />
                                        </div>      
                                        <div class="col-md-3 col-xs-6">
                                            <label class="control-label" for="id_Segui6">Fecha Acción</label>
                                            <asp:TextBox ID="TxtFechaAcc" runat="server" CssClass="form-control" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaAcc" Format="dd/MM/yyyy" PopupButtonID="TxtFechaAcc" ></cc1:CalendarExtender>
                                        </div> 
                                        <div class="col-md-3 col-xs-6">
                                            <label class="control-label" for="id_Segui6">Hora Acción</label>
                                            <asp:TextBox ID="TxtHoraAcc" runat="server" CssClass="form-control" ></asp:TextBox>
                                         </div> 
                                    </div>       
                                    <div class="row"> 
                                    </div> 
                                </div>
                                <div class="row"> 
                                    <div class="col-md-3 col-xs-3">
                                        <label class="control-label" for="id_Segui3" style="color:white;" >Cerrar</label>
                                        <asp:Button ID="BtnSeguiCerrar" runat="server" Text="Cerrar" CssClass="form-control btn-success" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <label class="control-label" for="id_Segui2" style="color:white;" >Guardar</label>
                                        <asp:Button ID="BtnSeguiGuardar" runat="server" Text="Grabar" CssClass="form-control btn-success"/>
                                    </div>  
                                </div>    
                                <h4>Datos de la oportunidad</h4>
                                <div class="row">        
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Opo_Nro">Nro.</label>
                                        <input class="form-control" id="TxtOpoNro" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>           
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_Opo_Fecha">Fecha</label>
                                        <input class="form-control" id="TxtOpoFecha" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>  
                                </div>        
                                <div class="row">
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_OpoRuc">RUC</label>
                                        <input class="form-control" id="TxtOpoRuc" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>   
                                    <div class="col-md-9 col-xs-12">
                                        <label class="control-label" for="id_OpoRazonSocial">Razón Social</label>
                                        <input class="form-control" id="TxtOpoRazonSocial" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div> 
                                </div>
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_OpoDireccion">Dirección</label>
                                        <input class="form-control" id="TxtOpoDireccion" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                </div>                                        
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_OpoContacto">Nombres y Apellidos del Contacto</label>
                                        <input class="form-control" id="TxtOpoContacto" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                </div>   
                                <div class="row"> 
                                    <div class="col-md-6 col-xs-12">
                                        <label class="control-label" for="id_OpoEmail">Correo Electrónico del Contacto</label>
                                        <input class="form-control" id="TxtOpoEmail" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_OpoTelef">Teléfono</label>
                                        <input class="form-control" id="TxtOpoTelef" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label" for="id_OpoTelef">teléfono 2</label>
                                        <input class="form-control" id="TxtOpoTelef2" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                </div>   
                                <div class="row"> 
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_OpoRequeri">Requerimiento</label>
                                        <input class="form-control" id="TxtOpoRequerimiento" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                    <div class="col-md-12 col-xs-12">
                                        <label class="control-label" for="id_OpoDetalle">Detalle</label>
                                        <input class="form-control" id="TxtOpoDetalle" name="Descripcion" type="text" runat="server" readonly ="true"  />
                                    </div>
                                </div>   
                            
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnSeguiCerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="GvListaOportunidades" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="ChkProxAccion" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="BtnSeguiGuardar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div> 

</asp:Content>

