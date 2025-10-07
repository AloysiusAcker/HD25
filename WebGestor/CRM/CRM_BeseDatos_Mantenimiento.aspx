<%--<%@ Page Title="" Language="VB" MasterPageFile="~/PagMaestra_DevExpress.master" AutoEventWireup="false" CodeFile="CRM_BeseDatos_Mantenimiento.aspx.vb" Inherits="CRM_CRM_BeseDatos_Mantenimiento" %>--%>
<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="FALSE" CodeFile="CRM_BeseDatos_Mantenimiento.aspx.vb" Inherits="CRM_CRM_BeseDatos_Mantenimiento" title="GestorPlus"  %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register assembly="DevExpress.Web.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>--%>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="Head" Runat="Server">
    <link href="../Css_Gestor/FormLayout.css" rel="stylesheet" />
    <link href="../Css_Gestor/Layout.css" rel="stylesheet" />
    <link href="../Css_Gestor/Content.css" rel="stylesheet" />
    <script src="../Css_Gestor/Script.js"></script>
           <script type="text/javascript">
               function OnGridFocusedRowChanged() {
                   // Query the server for the "EmployeeID" and "Notes" fields from the focused row
                   // The values will be returned to the OnGetRowValues() function
                   grid.GetRowValues(grid.GetFocusedRowIndex(), 'TICKET_codigo', OnGetRowValues);
               }
               function OnGetRowValues(values) {
                   Listar_Ticket(values[1]);
               }
               // Value array contains "EmployeeID" and "Notes" field values returned from the server
               function OnGridEstadoFocusedRowChanged() {
                   // Query the server for the "EmployeeID" and "Notes" fields from the focused row
                   // The values will be returned to the OnGetRowValues() function
                   gridestado.GetRowValues(gridestado.GetFocusedRowIndex(), 'TICKET_ESTADO', OnGetRowValues);
               }
               function OnCloseUp(s, e) {
                   btnShowHide.SetVisible(true);
               }
               function OnShowHideClick(s, e) {
                   dockPanel.Show();
                   btnShowHide.SetVisible(false);
               }
    </script>
</asp:Content>--%>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="LeftPanelContent" Runat="Server">
     <dx:aspxmenu ID="ASPxMenu2" runat="server" Orientation="Vertical" cssclass="header-menu application-menu" ></dx:aspxmenu>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <h1 class="Titulos">Base de Conocimientos</h1>
        
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                
                <div class="row espacio">
                    <div class="col-lg-12">                                         
                        <asp:Label ID="LblError" runat="server" CssClass="control-label-2" Text="" ForeColor="red"></asp:Label>
                    </div> 
                </div> 
                <div class="row espacio">
                    <div class="col-lg-3">                                         
                        <asp:Label ID="Label1" runat="server" CssClass="control-label-2" Text="Aplicativo"></asp:Label>
                        <asp:DropDownList ID="cboBusAplicativo" runat="server" CssClass="form-control" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">                       
                        <asp:Label ID="Label2" runat="server" CssClass="control-label-2" Text="Producto"></asp:Label>
                        <asp:DropDownList id="cboBusProducto" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList> 
                    </div>
                    <div class="col-lg-3"> 
                        <asp:Label ID="Label3" runat="server" CssClass="control-label-2" Text="Sub-Producto"></asp:Label>
                        <asp:DropDownList id="cboBusSubProd" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList> 
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
                <div id="lblIngreso" runat="server" visible="false" >
                    <div class="row espacio">          
                        <div class="col-lg-3">  
                            <asp:Button ID="btnGuardar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Guardar" />                    
                        </div>
                        <div class="col-lg-3">                       
                            <asp:Button ID="btnCancelar" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Cancelar" />
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
                        <div class="col-lg-3">         
                            <asp:Label ID="Label10" runat="server" CssClass="control-label-2" Text="Tabla" ForeColor="white"></asp:Label>              
                            <asp:Button ID="BtnNuevaTE" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Nueva Tabla" />
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
                            <asp:TextBox ID="txtSolucion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>                   
                        </div>  
                    </div>  
                    <div class="row">                        
                        <asp:TextBox ID="txtCodConsulta" runat="server" Text="" CssClass="form-control" visible="false"></asp:TextBox>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <div class="row espacio">     
                                <div class="col-lg-9">   
                                    <asp:FileUpload ID="FileUpload1" runat="server"  CssClass ="form-control" />              
                                </div>   
                                <div class="col-lg-3">      
                                    <asp:Button ID="BtnArchivo" runat="server" ControlStyle-CssClass="form-control btn btn-default" Text="Adjuntar" />                
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnArchivo" />
                        </Triggers>
                    </asp:UpdatePanel>  
                    <div class="row espacio">          
                        <div class="col-lg-12">  
                            <asp:GridView ID="GvArchivo" runat="server"  AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                        <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                        <ItemStyle Width="50px"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div id="Doc" runat="server" ></div>                                    
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ARCHIVO" HeaderText="Archivo">
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARCON_CODIGO" HeaderText="Numero" >
                                        <ItemStyle Width="0px"></ItemStyle>
                                        <HeaderStyle Width="0px"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CODIGO" HeaderText="Codigo" >
                                        <ItemStyle Width="0px"></ItemStyle>
                                        <HeaderStyle Width="0px"></HeaderStyle>
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="row espacio">          
                    <div class="col-lg-12">  
                        <asp:Label id="lblCount" runat="server" ForeColor="Maroon" CssClass="control-label-2" Font-Bold="True" Text="Total de Registros : 0"></asp:Label>
                    </div>
                </div>
                <div class="row espacio">          
                    <div class="col-lg-12">  
                        <asp:GridView id="Flex" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                            <Columns>
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                </asp:ButtonField>     
                                <asp:ButtonField CommandName="Archivos" Text="Archivos" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                    <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                </asp:ButtonField>        
                                <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="Aplicativo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCTO" HeaderText="Producto">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="subproducto" HeaderText="Sub-Producto">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CARCON_TRANSACCION" HeaderText="Transacci&#243;n">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CARCON_CONSULTA" HeaderText="Consulta">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CARCON_SOLUCION" HeaderText="Soluci&#243;n">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CARCON_CODIGO">
                                    <ItemStyle Width="0px" ForeColor="White" BorderColor="White"></ItemStyle>
                                    <HeaderStyle Width="0px" BorderColor="White"></HeaderStyle>
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
                            </Columns>
                        </asp:GridView> 
                    </div>
                </div>
                <div id="DivArchivo" runat="server" class="row espacio" visible="false" >   
                    <div class="row espacio">          
                        <div class="col-lg-12">
                        </div>
                    </div>
                </div> 
            </ContentTemplate>
            <triggers>
                <asp:AsyncPostBackTrigger ControlID="cboBusAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboBusProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboBusSubProd" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="FlexDetalle" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboAplicativo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboProducto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="cboSubProd" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
            </triggers>
        </asp:UpdatePanel>
    </div> 

    <div id="ModalDetalle" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="LblTituloModal" runat="server" Font-Size="14px" class="control-label2" Text="Lista de Archivos" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step4">
                            <div class="panel panel-default">
                                <div class="panel-body">   
                                    <div class="row espacio">
                                        <div class="col-lg-4">
                                            <asp:Button ID="btnCerrar" runat="server" class="form-control btn btn-default" Text="Cerrar"/>
                                        </div>
                                    </div>           
                                    <div class="row espacio">
                                        <div class="col-lg-4">
                                            <asp:Label ID="Label7" runat="server" CssClass="control-label-2" Text="Aplicativo"></asp:Label>
                                            <asp:TextBox ID="TxtMAplicativo" runat="server" CssClass="form-control" ReadOnly ="true" ></asp:TextBox> 
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label ID="Label8" runat="server" CssClass="control-label-2" Text="Producto"></asp:Label>
                                            <asp:TextBox ID="TxtMProducto" runat="server" CssClass="form-control" ReadOnly ="true"></asp:TextBox> 
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Label ID="Label9" runat="server" CssClass="control-label-2" Text="Sub-Producto"></asp:Label>
                                            <asp:TextBox ID="TxtMSubProducto" runat="server" CssClass="form-control" ReadOnly ="true"></asp:TextBox> 
                                        </div>
                                    </div>        
                                    <div class="row espacio">
                                        <div class="col-lg-12">
                                            <asp:Label ID="Label4" runat="server" CssClass="control-label-2" Text="Transacción"></asp:Label>
                                            <asp:TextBox ID="TxtMTransac" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly ="true"></asp:TextBox>    
                                        </div>
                                    </div>          
                                    <div class="row espacio">
                                        <div class="col-lg-12">
                                            <asp:Label ID="Label5" runat="server" CssClass="control-label-2" Text="Consulta"></asp:Label>
                                            <asp:TextBox ID="TxtMConsulta" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly ="true"></asp:TextBox>    
                                        </div>
                                    </div>         
                                    <div class="row espacio">
                                        <div class="col-lg-12">
                                            <asp:Label ID="Label6" runat="server" CssClass="control-label-2" Text="Solucion"></asp:Label>
                                            <asp:TextBox ID="TxtMSolucion" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly ="true"></asp:TextBox>    
                                        </div>
                                    </div>                              
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>                
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:Label ID="LblEtiq35"  CssClass="control-label-2" runat="server" Text=""></asp:Label>    
                                                    <asp:GridView ID="FlexDetalle" runat="server"  AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" CssClass="table table-bordered GridView">
                                                        <Columns>
                                                            <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                                                <ControlStyle CssClass="btn btn-default"></ControlStyle>
                                                                <ItemStyle Width="50px" />
                                                            </asp:ButtonField> 
                                                            <asp:TemplateField HeaderText="Nombre del Archivo">
                                                                <ItemTemplate>
                                                                    <div id="Doc" runat="server" ></div>                                    
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left"  VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CARCON_CODIGO" HeaderText="BC" >
                                                                <ItemStyle Width="0px"></ItemStyle>
                                                                <HeaderStyle Width="0px"></HeaderStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="codigo" HeaderText="Código" >
                                                                <ItemStyle Width="0px"></ItemStyle>
                                                                <HeaderStyle Width="0px"></HeaderStyle>
                                                            </asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div> 
                                            </div>       
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand" />
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

    <div id="ModalTablaEsp" class="modal fade" role="dialog" data-backdrop="static" style="overflow-y: scroll;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <asp:Label ID="Label11" runat="server" Font-Size="14px" class="control-label2" Text="Elementos de Tablas Especiales" />
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step5">
                            <div class="panel panel-default">
                                <div class="panel-body">                  
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>     
                                            <div class="row espacio">          
                                                <div class="col-lg-2">                
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
                                                    <asp:Label ID="Label12" runat="server" CssClass="control-label-2" Text="Nivel 1"></asp:Label>       
                                                    <asp:DropDownList ID="cboNivel1" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                    </div>  
                                                </div>                               
                                                <div class="row espacio">           
                                                    <div class="col-lg-12">                                        
                                                        <asp:Label ID="Label13" runat="server" CssClass="control-label-2" Text="Nivel 2"></asp:Label>       
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
