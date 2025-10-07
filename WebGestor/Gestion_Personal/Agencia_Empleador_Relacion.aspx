<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Agencia_Empleador_Relacion.aspx.vb" Inherits="Agencia_Empleador_Relacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        /* Estilos para el GridView */
            .gridview-container {
                font-family:  Roboto, arial, sans-serif;
                border: 1px solid #ddd;
                border-radius: 5px;
                overflow: hidden;
            }
        .gridview {    
            font-family: Roboto, arial, sans-serif;
            font-size: 8pt;
            border-collapse: collapse;
            border:1px solid #ddd;
            width: 100%;
        }

        .gridview th, .gridview td {
            padding: 3px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .gridview th {
            background-color: #f2f2f2;
        }

        .gridview tr:hover {
            background-color: #f5f5f5;
        }
    </style>


    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="LblEtiq1" runat="server" Text="Agencia - Relación de Empleadores" CssClass="Titulos" />
            </div> 
        </div>
        <br />
        <div class="row">
            <div class="col-md-9">
                <div class="row">
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnListar" runat="server" Text="Listar" ControlStyle-CssClass="form-control btn btn-default" />
                    </div> 
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnNuevo" runat="server" Text="Nuevo" ControlStyle-CssClass="form-control btn btn-default"/>
                    </div> 
                    <div class="col-md-3 col-xs-6">
                        <asp:Button ID="BtnExportar" runat="server" Text="Exportar" ControlStyle-CssClass="form-control btn btn-default"/>
                    </div> 
                </div>
            </div>
        </div>    
        
        <div class="row">

        </div> 
  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate> 
        
                <div class="row">
                    <div class="col-md-9">               
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label1" CssClass="control-label-2" runat="server" Text="Nro.Doc."></asp:Label>
                                <asp:TextBox ID="TxtNrodoc" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                            </div>   
                            <div class="col-md-9">
                                <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Apellido"></asp:Label>
                                <asp:TextBox ID="TxtApellido" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                            </div>                  
                        </div>                 
                        <div class="row"> 
                            <div class="col-md-3">
                                <asp:Label ID="Label14" CssClass="control-label-2" runat="server" Text="Sexo"></asp:Label>
                                <asp:DropDownList ID="DdlSexo" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>   
                            <div class="col-md-3">
                                <asp:Label ID="Label3" CssClass="control-label-2" runat="server" Text="Est. civil"></asp:Label>
                                <asp:DropDownList ID="DdlEstCivil" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>               
                        </div>
                        <div class="row">
                            <div class="col-md-3">                                
                            <asp:CheckBox ID="ChkFecha" CssClass="checkbox checkbox-inline" Text="Fecha Ing." Font-Bold ="true" runat="server" AutoPostBack="True" />
                            </div>             
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text="" Enabled ="false" ></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text="" Enabled ="false" ></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                            </div>  
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="LblEtiq" CssClass="control-label-2" runat="server" Text="Estado"></asp:Label>
                            </div>
                        </div>     
                        <div class="row">
                            <div class="col-md-3">
                                <div id="divCheckBoxList" style="height: 200px;width:200px; overflow-y: auto;">
                                <asp:CheckBoxList ID="ChkLisEstado" runat="server" CssClass="checkbox checkbox-inline" ></asp:CheckBoxList>
                                </div> 
                            </div>
                        </div>  
                    </div>
                </div>         
                      
        

                <div class="row">                    
                    <div class="col-md-12">
                        <asp:Label ID="lblRegistro" runat="server" class="control-label-2" Text="" ></asp:Label>
                    </div> 
                </div>    
                <div class="row">                    
                    <div class="col-md-12">
                        <asp:GridView ID="GvListaEmpleadores" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                            <Columns>
                                <asp:ButtonField CommandName="Reque" Text="Requerimiento" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                </asp:ButtonField>
                                <%--<asp:BoundField DataField="Estado" HeaderText="Nro." SortExpression="Estado" />--%>
                                <asp:BoundField DataField="NRO_EMPLEADOR" HeaderText="Nro." SortExpression="NRO_EMPLEADOR" />
                                <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc." SortExpression="TIPO_DOC" />
                                <asp:BoundField DataField="Nro_Doc" HeaderText="Nro. Doc." SortExpression="Nro_Doc" />
                                <asp:BoundField DataField="EMPLEADOR" HeaderText="Empleador" SortExpression="EMPLEADOR" />
                                <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" SortExpression="DIRECCION" />
                                <asp:BoundField DataField="DISTRITO" HeaderText="Distrito" SortExpression="DISTRITO" />
                                <asp:BoundField DataField="Sexo" HeaderText="Sexo" SortExpression="Sexo" />
                                <asp:BoundField DataField="ESTADO_CIVIL" HeaderText="Est. Civil" SortExpression="ESTADO_CIVIL" />
                                <asp:BoundField DataField="ESTADO_EMPLEADOR" HeaderText="Estado" SortExpression="ESTADO_EMPLEADOR" />
                                <asp:BoundField DataField="Fecha_Nac" HeaderText="Fecha Nac." SortExpression="Fecha_Nac" />
                                <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Registro" SortExpression="fecha_registro" />
                                <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>  
            </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ChkFecha" EventName="CheckedChanged" />
                
            </Triggers>
        </asp:UpdatePanel>        
    </div> 

    <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                        <asp:Label ID="LblTituloModal" runat="server" CssClass="subTitulos" Text="-" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GvListaEmpleadores" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step4">
                            <div class="panel panel-default">
                                <div class="panel-body">   
                                    <div class="row">
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnCerrar" runat="server" class="form-control btn btn-default" Text="Cerrar" OnClick="btnCerrar_Click" />
                                        </div>
                                    </div>                                       
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>      
                                            
                                            <div class="row">                    
                                                <div class="col-md-12">
                                                    <asp:Label ID="LblRegistroDetalle" runat="server" class="control-label-2" Text="" ></asp:Label>
                                                </div> 
                                            </div>   
                                            <div class="row">
                                                <div class="table-responsive col-md-12">
                                                    <asp:Label ID="LblEtiq35"  CssClass="control-label-2" runat="server" Text=""></asp:Label>                
                                                    <asp:GridView ID="GvRequerimiento" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                                                        <Columns>
                                                            <asp:BoundField DataField="NRO_REQ" HeaderText="Nro." SortExpression="NRO_REQ" />
                                                            <asp:BoundField DataField="REQ_SERVICIO" HeaderText="Servicio" SortExpression="REQ_SERVICIO" />
                                                            <asp:BoundField DataField="Actividad" HeaderText="Actividad" SortExpression="Actividad" /> 
                                                            <asp:BoundField DataField="REQUE_DIA_DESCANSO" HeaderText="Día Descanso" SortExpression="REQUE_DIA_DESCANSO" />
                                                            <asp:BoundField DataField="REQUE_DIA_DESCANSO_EXTRA" HeaderText="Día Extra" SortExpression="REQUE_DIA_DESCANSO_EXTRA" />
                                                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                                                            <asp:BoundField DataField="REQUE_DISTRITO" HeaderText="Distrito" SortExpression="REQUE_DISTRITO" />
                                                            <asp:BoundField DataField="REQUE_POSTULANTE" HeaderText="Postulante" SortExpression="REQUE_POSTULANTE" />
                                                            <asp:BoundField DataField="REQUE_CATEGORIA" HeaderText="Categoria" SortExpression="REQUE_CATEGORIA" />
                                                            <asp:BoundField DataField="rango_edad" HeaderText="Rango" SortExpression="rango_edad" />
                                                            <asp:BoundField DataField="FECHA_REG" HeaderText="Fecha Reg." SortExpression="FECHA_REG" />
<%--                                                            <asp:BoundField DataField="REQUE_DATOS_ADICIONALES" HeaderText="Datos Adicionales" SortExpression="REQUE_DATOS_ADICIONALES" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div> 
                                            </div>        
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="GvListaEmpleadores" EventName="RowCommand" />
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


        <div id="ModalEmpleador" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Ingresar Empleador</h4>
                </div>
                <div class="modal-body" style="padding: 20px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnECerrar" runat="server" Text="Cerrar" CssClass="form-control btn-success" />
                                    </div>
                                    <div class="col-md-3 col-xs-3">
                                        <asp:Button ID="BtnEGuardar" runat="server" Text="Grabar" CssClass="form-control btn-success"/>
                                    </div>
                                </div>     
                                <h5>Datos del Empleador</h5>
                                <div class="row">                                     
                                    <div class="col-md-3 col-xs-6 selectContainer">
                                        <label class="control-label" for="id_ETipoDoc">Doc.Tipo</label>
                                        <asp:DropDownList ID="DdlEDocTipo" runat="server" CssClass="form-control" />
                                    </div>          
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_ENroDoc">Doc.Nro.</label>
                                        <input class="form-control" id="txtEDocNro" name="Descripcion" type="text" runat="server" />
                                    </div> 
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_EEstado">Estado</label>
                                        <asp:DropDownList ID="DdlEEstado" runat="server" CssClass="form-control" />
                                    </div>      
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_EFechaReg">Fecha Registro</label>
                                        <input class="form-control" id="TxtEFechaReg" name="Descripcion" type="text" runat="server" readonly="true"/>
                                    </div>        
                                </div>        
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="control-label" for="id_EApePat">Ape. Paterno</label>
                                        <input class="form-control" id="TxtEApePat" name="Descripcion" type="text" runat="server" />
                                    </div>    
                                    <div class="col-md-3">
                                        <label class="control-label" for="id_EApeMat">Ape. Materno</label>
                                        <input class="form-control" id="TxtEApeMat" name="Descripcion" type="text" runat="server" />
                                    </div>   
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_ENombre">Nombres</label>
                                        <input class="form-control" id="TxtENombres" name="Descripcion" type="text" runat="server" />
                                    </div> 
                                </div>
                                <div class="row"> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_ENombre">Razón Social</label>
                                        <input class="form-control" id="TxtERazonSocial" name="Descripcion" type="text" runat="server" />
                                    </div> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_EEMail">Correo Electrónico</label>
                                        <input class="form-control" id="TxtEEmail" name="Descripcion" type="text" runat="server" />
                                    </div> 
                                </div>
                                <div class="row"> 
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_ESeEntero">¿Cómo se enteró?</label>
                                        <asp:DropDownList ID="DdlESeEntero" runat="server" CssClass="form-control" />
                                    </div>     
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_ERecomienda">¿Quién recomendó?</label>
                                        <input class="form-control" id="TxtERecomienda" type="text" runat="server" />
                                    </div> 
                                </div>
                                <div class="row"> 
                                </div>   
                                <h5>Dirección del Empleador</h5>
                                <div class="row"> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_EDpto">Departamento</label>
                                        <asp:DropDownList ID="DdlEDpto" runat="server" CssClass="form-control no-collapse" AutoPostBack="True"  />
                                    </div> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_EProv">Provincia</label>
                                        <asp:DropDownList ID="DdlEProv" runat="server" CssClass="form-control" AutoPostBack="True" />
                                    </div> 
                                </div>  
                                <div class="row"> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_EDistrito">Distrito</label>
                                        <asp:DropDownList ID="DdlEDist" runat="server" CssClass="form-control" />
                                    </div> 
                                    <div class="col-md-6">
                                        <label class="control-label" for="id_ETipo">Tipo</label>
                                        <asp:DropDownList ID="DdlETipo" runat="server" CssClass="form-control" />
                                    </div>
                                </div>   
                                <div class="row"> 
                                    <div class="col-md-12">
                                        <label class="control-label" for="id_EDireccion">Dirección</label>
                                        <asp:Textbox ID="TxtEDireccion" runat="server" CssClass="form-control" />
                                    </div> 
                                </div>
                                <h5>Relación de Teléfonos</h5>
                                <div class="row"> 
                                    <div class="col-md-3 col-xs-6">
                                        <label class="control-label" for="id_ETelefono">Número</label>
                                        <asp:Textbox ID="TxtETelefono" runat="server" CssClass="form-control" />
                                    </div> 
                                    <div class="col-md-3 col-xs-3">
                                        <label class="control-label" for="id_Segui3" style="color:white;" >agregar</label>
                                        <asp:Button ID="BtnETelef" runat="server" Text="Agregar" CssClass="form-control btn-success"/>
                                    </div>                  
                                    <div class="col-md-3 col-xs-12">
                                        <asp:GridView ID="GvTelefonos" runat="server" AutoGenerateColumns="False"  CssClass="gridview">
                                            <Columns>
                                                <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                    <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div> 
                                <div class="row">  
                                </div>  
                                 <div class="row">
                                    <div class="col-lg-12">                   
                                        <div id="Div1" role="tablist" aria-multiselectable="true" runat="server"  >
                                            <div class="card">
                                                <div class="card-header" role="tab" id="section1HeaderId">
                                                    <h5 class="mb-0">                            
                                                        <a data-toggle="collapse" data-parent="#accordion" href="#section1ContentId" aria-expanded="false" aria-controls="section1ContentId" >
                                                            
                                                        </a>
                                                    </h5>
                                                </div>
                                                <div id="section1ContentId" class="collapse" role="tabpanel" aria-labelledby="section1HeaderId">
                                                    <div class="card-body">                                 
                                                        
                                                    </div>
                                                </div>
                                            </div>                                           
                                        </div>
                                    </div>
                                </div> 
                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="BtnECerrar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnEGuardar" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnNuevo" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="BtnETelef" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DdlEDpto" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="DdlEProv" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="GvTelefonos" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div> 

    
</asp:Content>

