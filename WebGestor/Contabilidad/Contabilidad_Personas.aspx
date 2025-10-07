<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Contabilidad_Personas.aspx.vb" Inherits="Contabilidad_Personas" Title="GestorPlus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row espacio">          
                    <div class="col-lg-12"> 
                        <asp:Label runat="server" id="lblTitulo" text="Personas" class="Titulos"></asp:Label>
                    </div>
                </div> 

                <div class="row espacio">          
                    <div class="col-lg-12">           
                        <asp:Label ID="lblError" runat="server" CssClass="control-label control-label-2" ForeColor="Red"></asp:Label>             
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

                <div class="row espacio">          
                    <div class="col-lg-3">
                        <asp:Label ID="lblRegistro" runat="server" ForeColor="Maroon" Font-Size="8pt" Font-Names="Arial"
                            Font-Bold="True"></asp:Label>
                    </div> 
                </div> 
        
                <div id="FraIngreso" runat="server" visible="false" >
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Button ID="BtnSunat"  runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="SUNAT"></asp:Button>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btnGuardar"  runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" ></asp:Button>
                        </div>
                        <div class="col-lg-3">
                            <asp:Button ID="btnRegresar"   runat="server"  ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar"></asp:Button>
                        </div>
                    </div>

                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label ID="lblEtiqueta" runat="server" ForeColor="Maroon" CssClass="control-label-2"></asp:Label>
                        </div> 
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl1" runat="server" CssClass="control-label-2" Text="R.U.C."></asp:Label>
                            <asp:TextBox ID="txtRuc" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:RadioButton GroupName="optDoc" ID="RbDni" runat="server" Text="DNI" Checked="true" AutoPostBack="True" />
                            <asp:RadioButton GroupName="optDoc" ID="Rbruc" runat="server" Text="RUC" AutoPostBack="True" />
                        </div>
                    </div>
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl2" runat="server" CssClass="control-label-2" Text="Tipo Persona"></asp:Label>
                            <asp:DropDownList ID="cboTipoPer" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label5" runat="server" CssClass="control-label-2" Text="Tipo" ForeColor="white"></asp:Label>
                            <asp:DropDownList ID="CboTipo1" runat="server" CssClass="form-control" AutoPostBack="True">
                                <asp:ListItem Selected="True">(Seleccionar)</asp:ListItem>
                                <asp:ListItem Value="1">Persona Natural</asp:ListItem>
                                <asp:ListItem Value="2">Persona Juridica</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl3" runat="server" CssClass="control-label-2" Text="Tipo Cliente"></asp:Label>
                            <asp:DropDownList ID="cboTipoCliente" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label1" runat="server" CssClass="control-label-2" Text="Forma Pago"></asp:Label>
                            <asp:DropDownList ID="cboFormaPago" runat="server" CssClass="form-control" >
                                <asp:ListItem Selected="True">(Seleccionar)</asp:ListItem>
                                <asp:ListItem Value="1">Contado</asp:ListItem>
                                <asp:ListItem Value="2">Credito</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label ID="lbl4" runat="server" CssClass="control-label-2" Text="Razón Social"></asp:Label>
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl9" runat="server" CssClass="control-label-2" Text="Ap. Paterno"></asp:Label>
                            <asp:TextBox ID="txtApepat" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lbl10" runat="server" CssClass="control-label-2" Text="Ap. Materno"></asp:Label>
                            <asp:TextBox ID="txtApemat" runat="server"  CssClass="form-control" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label ID="lbl6" runat="server" CssClass="control-label-2" Text="Nombres"></asp:Label>
                            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" MaxLength="80"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl5" runat="server" CssClass="control-label-2" Text="Cert. Inscr."></asp:Label>
                            <asp:TextBox ID="txtCertInscrip" runat="server"  CssClass="form-control" MaxLength="20"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label runat="server" CssClass="control-label-2" Text="Inicio Actividades"></asp:Label>
                            <asp:TextBox ID="txtFechaInicioActividades" runat="server" CssClass="form-control" MaxLength="200" ></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label runat="server"  CssClass="control-label-2" Text="Nombre Comercial"></asp:Label>
                            <asp:TextBox ID="txtNomComercial" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label ID="lbl12" runat="server"  CssClass="control-label-2" Text="Dirección"></asp:Label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" MaxLength="150" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl13" runat="server" CssClass="control-label-2" Text="País"></asp:Label>
                            <asp:DropDownList ID="cboPais" runat="server"  CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">                    
                            <asp:Label ID="Label6" runat="server" CssClass="control-label-2" Text="Departamento"></asp:Label>
                            <asp:DropDownList ID="cboDpto" runat="server"  CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">                    
                            <asp:Label ID="Label7" runat="server" CssClass="control-label-2" Text="Provincia"></asp:Label>
                            <asp:DropDownList ID="cboProv" runat="server"  CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3">                    
                            <asp:Label ID="Label8" runat="server" CssClass="control-label-2" Text="Distrito"></asp:Label>
                            <asp:DropDownList ID="cboDist" runat="server"  CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-3">                    
                            <asp:Label ID="lbl7" runat="server" CssClass="control-label-2" Text="Telefonos"></asp:Label>
                            <asp:TextBox ID="txtTelef1" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label9" runat="server" CssClass="control-label-2" Text="Telefonos" ForeColor="white"></asp:Label>
                            <asp:TextBox ID="txtTelef2" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-6">
                            <asp:Label ID="lbl8" runat="server" CssClass="control-label-2" Text="Correo"></asp:Label>
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" MaxLength="150"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:Label ID="lbl11" runat="server" CssClass="control-label-2" Text="Página Web"></asp:Label>
                            <asp:TextBox ID="txtPagWeb" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                        </div>
                    </div> 
                    <div class="row espacio">
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-3">
                            <asp:Label ID="lbl17" runat="server" CssClass="control-label-2" Text="Puesto"></asp:Label>
                            <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label4" runat="server" CssClass="control-label-2" Text="DNI Contacto"></asp:Label>
                            <asp:TextBox ID="txtDniContacto" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label runat="server" CssClass="control-label-2" Text="Estado"></asp:Label>
                            <asp:TextBox ID="txtEstadoContribuyente" runat="server"  CssClass="form-control" MaxLength="8"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="Label2" runat="server" CssClass="control-label-2" Text="Rubro"></asp:Label>
                            <asp:DropDownList ID="CboRubro" runat="server"  CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="row espacio">
                    </div> 
                    <div class="row espacio">
                        <div class="col-lg-12">
                            <asp:Label ID="Label3" runat="server" CssClass="control-label-2" Text="S. Economico"></asp:Label>
                            <asp:DropDownList ID="CboSectorEconomico" runat="server"  CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div> 
                    <div class="row espacio">                
                        <asp:TextBox ID="txtCodPersona" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtNroTicket" runat="server" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
                    </div>
                </div>

                <div class="row espacio">
                    <div class="col-lg-12">
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderWidth="1px" BorderStyle="Outset" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Gray" Width="40px"></ControlStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="PTIPO" HeaderText="Tipo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Font-Names="Arial" Font-Size="8pt" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSONA_RUC" HeaderText="R.U.C.">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSONA_RAZON_SOCIAL" HeaderText="Nombre / Razón Social">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DIRECCION" HeaderText="Direcci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TELEF" HeaderText="Telefonos">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSONA_NOMBRE_CONTACTO" HeaderText="Persona Contacto">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CORREO" HeaderText="Correo Electr&#243;nico">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PAGWEB" HeaderText="Pagina Web">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PCATEG" HeaderText="Categoria">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PERSONA_CERT_INSCR" HeaderText="Inscripci&#243;n"></asp:BoundField>
                                <asp:BoundField DataField="PERSONA_CODIGO">
                                    <ItemStyle ForeColor="White" Width="0px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Names="Arial" Font-Size="8pt"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btnRegresar" EventName="Click"></asp:AsyncPostBackTrigger>
                <%--<asp:AsyncPostBackTrigger ControlID="BtnSunat" EventName="Click"></asp:AsyncPostBackTrigger>--%>
                <asp:AsyncPostBackTrigger ControlID="cboPais" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboDpto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboProv" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboTipoPer" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="RbRuc" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="Rbdni" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="CboTipo1" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
    </div> 


  
</asp:Content>

