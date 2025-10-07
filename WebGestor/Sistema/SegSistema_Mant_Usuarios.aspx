<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SegSistema_Mant_Usuarios.aspx.vb" Inherits="SegSistema_Mant_Usuarios" title="Sistema - Usuarios" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container">
        <h1 class="Titulos">Usuarios del Sistema</h1>    
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <cc1:TabContainer ID="Ficha" runat="server" ActiveTabIndex="1" Width="100%" AutoPostBack="True" CssClass="MyTabStyle ajax__tab_header">
                    <cc1:TabPanel runat="server" HeaderText="Usuarios" ID="TabPanel4">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="row espacio">          
                                        <div class="col-lg-12">           
                                            <asp:Label ID="LblError" runat="server" CssClass="control-label-2" ForeColor="Maroon"></asp:Label>    
                                            <asp:Label id="lblMensaje" runat="server" ForeColor="Red" CssClass="control-label-2" ></asp:Label>         
                                        </div>
                                    </div> 
                                    <div class="row espacio">          
                                        <div class="col-lg-3">  
                                            <asp:Button ID="btnListar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Listar" />                    
                                        </div>
                                        <div class="col-lg-3">                       
                                            <asp:Button ID="btnNuevo" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Nuevo" />
                                        </div>     
                                    </div>
                                    <div id="fraDatosPersonales" runat="server" visible="false" >
                                        <div class="row espacio">          
                                            <div class="col-lg-12">  
                                                <asp:Label ID="lbl1" runat="server" CssClass="control-label-2" ForeColor="Maroon" Text="Datos Personales"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row espacio">          
                                            <div class="col-lg-1">  
                                                <asp:Label ID="lbl2" runat="server" CssClass="control-label-2" Text="Personal"></asp:Label>
                                                <asp:DropDownList ID="cboPerSN" runat="server" AutoPostBack="True" CssClass="form-control">
                                                    <asp:ListItem>SI</asp:ListItem>
                                                    <asp:ListItem>NO</asp:ListItem>
                                                    <asp:ListItem Selected="True">&lt; Seleccionar &gt;</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>       
                                            <div class="col-lg-2">  
                                                <asp:Label ID="lbl7" runat="server" CssClass="control-label-2" Text="Código"></asp:Label>
                                                <asp:TextBox ID="txtCodigoPers" runat="server" Enabled="False"  CssClass="form-control" ReadOnly="True">
                                                </asp:TextBox>
                                            </div>       
                                            <div class="col-lg-1">  
                                                <asp:Label ID="Label2" runat="server" CssClass="control-label-2" Text="Código" ForeColor="white"></asp:Label>
                                                <asp:Button ID="btnBuscar" runat="server" Text="..." CssClass="form-control btn btn-default" />
                                            </div>   
                                            <div class="col-lg-3">  
                                                <asp:Label ID="Label1" runat="server" CssClass="control-label-2" Text="Cod. Interno"></asp:Label>
                                                <asp:TextBox ID="lblCodInterno" runat="server"  CssClass="form-control" MaxLength="8"></asp:TextBox>
                                            </div> 
                                        </div>
                                        <div class="row espacio">         
                                            <div class="col-lg-3">  
                                                <asp:Label ID="lbl3" runat="server" CssClass="control-label-2" Text="Ap. Paterno"></asp:Label>
                                                <asp:TextBox ID="txtApepat" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            </div>       
                                            <div class="col-lg-3">  
                                                <asp:Label ID="lbl4" runat="server" CssClass="control-label-2" Text="Ap.Materno"></asp:Label>
                                                <asp:TextBox ID="txtApeMat" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            </div>       
                                            <div class="col-lg-3">  
                                                <asp:Label ID="lbl5" runat="server" CssClass="control-label-2" Text="Nombres"></asp:Label>
                                                <asp:TextBox ID="txtNombres" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row espacio">         
                                            <div class="col-lg-3"> 
                                                <asp:Label ID="lbl8" runat="server" CssClass="control-label-2" Text="Nacionalidad"></asp:Label>
                                                <asp:DropDownList ID="cboNacionalidad" runat="server" CssClass="form-control"></asp:DropDownList> 
                                            </div>       
                                            <div class="col-lg-3">  
                                                <asp:Label ID="lbl9" runat="server" CssClass="control-label-2" Text="Tipo de Doc."></asp:Label>
                                                <asp:DropDownList ID="cboTipoDoc" runat="server"  CssClass="form-control"></asp:DropDownList>
                                            </div>       
                                            <div class="col-lg-3">  
                                                <asp:Label ID="lbl10" runat="server" CssClass="control-label-2" Text="Número de Doc."></asp:Label>
                                                <asp:TextBox ID="txtNroDoc" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row espacio">          
                                            <div class="col-lg-9">  
                                                <asp:Label ID="lblEtiq2" runat="server" CssClass="control-label-2" Text="E - Mail"></asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                            </div>    
                                        </div>
                                        <div class="row espacio">          
                                            <div class="col-lg-2">  
                                                <asp:Label ID="lbl15" runat="server" CssClass="control-label-2" Text="Fecha Inicio"></asp:Label>
                                                <asp:TextBox ID="txtFechaIni" runat="server"  CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender id="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaIni" Format="dd/MM/yyyy" PopupButtonID="txtFechaIni" />
                                            </div>       
                                            <div class="col-lg-2">
                                                <asp:Label ID="lbl16" runat="server" CssClass="control-label-2" Text="Fecha Final"></asp:Label>
                                                <asp:TextBox ID="txtFechaFin" runat="server"  CssClass="form-control"></asp:TextBox>
                                                <cc1:CalendarExtender id="CalendarExtender4" runat="server" CssClass="custom-calendar" TargetControlID="txtFechaFin" Format="dd/MM/yyyy" PopupButtonID="txtFechaFin" />
                                            </div>                                       
                                            <div class="col-lg-1"> 
                                            </div>                                       
                                            <div class="col-lg-2">  
                                                <asp:Label ID="Label10" runat="server" CssClass="control-label-2" Text="Código" ForeColor="white"></asp:Label>
                                                <asp:Button ID="BtnGrabar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Grabar" />                    
                                            </div> 
                                            <div class="col-lg-2">   
                                                <asp:Label ID="Label11" runat="server" CssClass="control-label-2" Text="Código" ForeColor="white"></asp:Label>                    
                                                <asp:Button ID="BtnCancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar"  />
                                            </div> 
                                        </div>
                                        <div class="row espacio">    
                                        </div>
                                    </div>
                                    <div class="row espacio">          
                                        <div class="col-lg-12">  
                                            <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                        <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                    </asp:ButtonField> 
                                                    <asp:ButtonField CommandName="Asignar" Text="Perfil" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                        <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                    </asp:ButtonField>
                                                    <asp:ButtonField CommandName="Empresa" Text="Empresa" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                        <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                    </asp:ButtonField>
                                                    <asp:BoundField DataField="USUARI_CODIGO" HeaderText="Cód.">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERSON_COD_INTERNO" HeaderText="Usuario">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USUARI_APEPAT" HeaderText="Ape. Paterno">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USUARI_APEMAT" HeaderText="Ape. Materno">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USUARI_NOMBRES" HeaderText="Nombres">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USUARI_PERCED" HeaderText="Tipo">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USUARI_FECINI" HeaderText="Fecha Inicia">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="USUARI_FECFIN" HeaderText="Fecha Expira">
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle VerticalAlign="Middle"></PagerStyle>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate> 
                            </asp:UpdatePanel> 
                        </ContentTemplate>  
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Perfiles de Usuario" ID="TabPanel5">
                        <ContentTemplate>                            
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row espacio">          
                                        <div class="col-lg-2">                                           
                                            <asp:Label id="lblPU1" runat="server" CssClass="control-label-2" Text="Usuario" ></asp:Label> 
                                            <asp:TextBox id="txtCodUsuarioPU" runat="server" CssClass="form-control"></asp:TextBox> 
                                        </div>       
                                        <div class="col-lg-6">                                        
                                            <asp:Label id="Label3" runat="server" CssClass="control-label-2" Text="Usuario" ForeColor="white" ></asp:Label> 
                                            <asp:TextBox id="txtUsuarioPU" runat="server" CssClass="form-control" ></asp:TextBox> 
                                        </div>         
                                        <div class="col-lg-2">                                           
                                            <asp:Label id="Label4" runat="server" CssClass="control-label-2" Text="Usuario" ForeColor="white" ></asp:Label>  
                                            <asp:Button ID="btnRegresar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Regresar" />    
                                        </div>          
                                        <div class="col-lg-2">                                           
                                            <asp:Label id="Label5" runat="server" CssClass="control-label-2" Text="Usuario" ForeColor="white" ></asp:Label>  
                                            <asp:Button ID="btnPUAsignar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Asignar" />    
                                        </div> 
                                    </div>                              
                                    <div class="row espacio">          
                                        <div class="col-lg-2">    
                                            <asp:Label id="lblPUError" runat="server" CssClass="control-label-2" ForeColor="Red" ></asp:Label>                                       
                                        </div> 
                                    </div>                                
                                    <div id="lblAsignarPerfil" runat="server" visible="false" >                                        
                                        <div class="row espacio">          
                                            <div class="col-lg-2">    
                                                <asp:Label id="lblPU2" runat="server" CssClass="control-label-2" ForeColor="Maroon" Text="Asignar Perfil"></asp:Label>
                                            </div> 
                                        </div>                                      
                                        <div class="row espacio">          
                                            <div class="col-lg-6">  
                                                <asp:Label id="lblPU3" runat="server" CssClass="control-label-2" Text="Grupo Empresa"></asp:Label> 
                                                <asp:DropDownList id="cboGrpoEmp" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>                                         
                                            </div>   
                                            <div class="col-lg-6">    
                                                <asp:Label id="lblPU5" runat="server" CssClass="control-label-2" Text="Empresa" ></asp:Label>
                                                <asp:DropDownList id="cboEmp" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:DropDownList>                                        
                                            </div>   
                                        </div>                                      
                                        <div class="row espacio">          
                                            <div class="col-lg-6">     
                                                <asp:Label id="lblPU4" runat="server" CssClass="control-label-2" Text="Módulo Integ."></asp:Label> 
                                                <asp:DropDownList id="cboModInteg" runat="server" CssClass="form-control" AutoPostBack="True" ></asp:DropDownList>                                       
                                            </div>   
                                            <div class="col-lg-6">       
                                                <asp:Label id="Label6" runat="server" CssClass="control-label-2" Text="Perfil de Usuario"></asp:Label> 
                                                <asp:DropDownList id="cboPerfil" runat="server" CssClass="form-control" ></asp:DropDownList>                                     
                                            </div>   
                                        </div>                                      
                                        <div class="row espacio">               
                                            <div class="col-lg-2">    
                                                <asp:Button id="btnPUGuardar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Guardar"></asp:Button> 
                                            </div>  
                                            <div class="col-lg-2">
                                                <asp:Button id="btnPUCancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar" ></asp:Button>                 
                                            </div> 
                                        </div>   
                                    </div>
                                    <div class="row espacio">          
                                        <div class="col-lg-12"> 
                                            <asp:GridView id="FlexPU" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                        <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                    </asp:ButtonField> 
                                                    <asp:BoundField DataField="GE_NOMBRE" HeaderText="Grupo de Empresa">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GEE_NOMBRE" HeaderText="Empresa">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MODINTEG_NOMBRE" HeaderText="M&#243;dulo de Integraci&#243;n">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERFIL_CODIGO" HeaderText="Perfil">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERFIL_DES" HeaderText="Descripci&#243;n del Perfil">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GRPOEMPRESA_CODIGO">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EMPRESA_CODIGO">
                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MODINTEG_CODIGO">
                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PERFIL_CODUNICO">
                                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Left" VerticalAlign="Top"></PagerStyle>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            </asp:GridView> 
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate> 
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText=" Define Accesos a Empresa" ID="TabPanel2">
                        <ContentTemplate>                            
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>                  
                                    <div class="row espacio">          
                                        <div class="col-lg-2"> 
                                            <asp:Label id="lblAE1" runat="server" CssClass="control-label-2" Text="Usuario"></asp:Label>
                                            <asp:TextBox id="txtCodUsuarioAE" runat="server" CssClass="form-control" ></asp:TextBox>
                                        </div>   
                                        <div class="col-lg-6">  
                                            <asp:Label id="Label7" runat="server" CssClass="control-label-2" Text="Usuario" ForeColor="white" ></asp:Label> 
                                            <asp:TextBox id="txtUsuarioAE" runat="server" CssClass="form-control" ></asp:TextBox>
                                        </div>   
                                        <div class="col-lg-2">   
                                            <asp:Label id="Label8" runat="server" CssClass="control-label-2" Text="Regresar" ForeColor="white" ></asp:Label> 
                                            <asp:Button id="btnAERegresar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Regresar"></asp:Button>
                                        </div>   
                                        <div class="col-lg-2">    
                                            <asp:Label id="Label9" runat="server" CssClass="control-label-2" Text="Asignar" ForeColor="white" ></asp:Label> 
                                            <asp:Button id="btnAEAsignar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Asignar"></asp:Button>
                                        </div>
                                    </div>
                                    <div id="lblAccesoEmpresa" runat="server" visible="false" >                                        
                                        <div class="row espacio">          
                                            <div class="col-lg-12">    
                                                <asp:Label id="lblAEEtiqueta" runat="server" CssClass="control-label-2" Text="Agregar Acceso" ForeColor="Maroon"></asp:Label>
                                            </div> 
                                        </div>  
                                        <div class="row espacio">          
                                            <div class="col-lg-12"> 
                                                <asp:Label id="lblAEUser" CssClass="control-label-2" runat="server" Text="Empresas que el usuario tiene acceso :"></asp:Label>
                                            </div> 
                                        </div> 
                                        <div class="row espacio">          
                                            <div class="col-lg-6"> 
                                                <asp:Label id="lblAE2" runat="server" CssClass="control-label-2" text="Grupo Empresa"></asp:Label>
                                                <asp:DropDownList id="cboAEGrpoEmp" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                                            </div>        
                                            <div class="col-lg-6"> 
                                                <asp:Label id="lblAE3" CssClass="control-label-2" runat="server" Text="Empresa"></asp:Label>
                                                <asp:DropDownList id="cboAEEmp" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div> 
                                        </div> 
                                        <div class="row espacio">          
                                            <div class="col-lg-2"> 
                                                <asp:Button id="btnAEGuardar" ControlStyle-CssClass="form-control btn btn-default"  Text="Guardar" runat="server"></asp:Button>
                                            </div>        
                                            <div class="col-lg-2"> 
                                                <asp:Button id="btnAECancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar"></asp:Button>
                                            </div> 
                                        </div> 
                                    </div> 
                                    <div class="row espacio">          
                                        <div class="col-lg-12"> 
                                            <asp:GridView id="FlexAE" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                        <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                    </asp:ButtonField> 
                                                    <asp:BoundField DataField="GE_NOMBRE" HeaderText="Grupo de Empresa">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GEE_NOMBRE" HeaderText="Empresa">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GRPOEMPRESA_CODIGO">
                                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EMPRESA_CODIGO">
                                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            </asp:GridView>
                                        </div> 
                                    </div> 
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate> 
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Ficha" EventName="ActiveTabChanged" />
            </Triggers>
        </asp:UpdatePanel> 
    </div> 

    
    <div id="ModalPersonal" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="TituloPopup" runat="server" Text="Relación de Personal de la Empresa" CssClass="Titulos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row espacio">
                                                <div class="col-md-3">
                                                    <asp:Button ID="BtnCerrar" ControlStyle-CssClass="form-control btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                            <div class="row espacio">
                                                <div class="col-md-3">
                                                    <asp:GridView ID="FlexP" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                                <ItemStyle Height="10px" Width="10px" />
                                                            </asp:ButtonField>
                                                            <asp:BoundField DataField="PERSON_CODIGO" HeaderText="Código">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EsUsuario" HeaderText="Usuario">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PERSON_APEPAT" HeaderText="Ap. Paterno">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PERSON_APEMAT" HeaderText="Ap. Materno">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PERSON_NOMBRES" HeaderText="Nombres">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TIPO_DOC" HeaderText="Tipo Doc.">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PERSON_NUMDOCIDE" HeaderText="Numero Doc.">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PERSON_COD_INTERNO" HeaderText="Cod. Interno">
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="TIPO_CODDOC">
                                                                <ItemStyle Width="0px" ForeColor="White" BorderColor="White"></ItemStyle>
                                                                <HeaderStyle Width="0px" BorderColor="White"></HeaderStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </div> 
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexP" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
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

