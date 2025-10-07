<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Definiciones_Tablas.aspx.vb" Inherits="Inventario_Inventario_Definiciones_Tablas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%" AutoPostBack="True" BorderStyle="None" CssClass="MyTabStyle ajax__tab_header"  >
        <cc1:TabPanel ID="Panel1" runat="server" HeaderText="Almacen">            
            <ContentTemplate>  
             </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel2" runat="server" HeaderText="Marca" BorderWidth="0" BorderColor="White" BorderStyle="None">
            <ContentTemplate>  
                <asp:Label ID="LblDescMarca" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="TxtDescMarca" runat="server" Width="222px"></asp:TextBox>
                <asp:Button ID="BtnListarMarca" runat="server" Text="Listar" CssClass=" btn btn-default"/>
                <asp:Button ID="BtnNuevaMarca" runat="server" Text="Nuevo" CssClass=" btn btn-default"/>
                <br />
                <br />
                <asp:Label ID="LblCodigoMarca" runat="server" Text="Código" Visible="False"></asp:Label>
                <asp:TextBox ID="TxtCodigoMarca" runat="server" Visible="False"></asp:TextBox>
                <asp:Button ID="BtnAgregarMarca" runat="server" Text="Agregar" Visible="False" CssClass=" btn btn-default"/>
                <br />
                <asp:Label ID="LblDescripcionMarca" runat="server" Text="Descripción" Visible="False"></asp:Label>
                <asp:TextBox ID="TxtDescripcionMarca" runat="server" Visible="False"></asp:TextBox>
                <asp:Button ID="BtnCancelarMarca" runat="server" Text="Cancelar" Visible="False" CssClass=" btn btn-default"/>
                <br />
                <asp:GridView ID="GvListaMarcas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="EditaMarca" Text="Editar">
                            <ControlStyle CssClass=" btn btn-default" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="EliminaMarca" Text="Eliminar">
                            <ControlStyle CssClass=" btn btn-default" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="DetalleMarca" Text="Detalle">
                            <ControlStyle CssClass=" btn btn-default" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="ARTMAR_CODIGO" HeaderText="Codigo" SortExpression="ARTMAR_CODIGO" />
                        <asp:BoundField DataField="ARTMAR_DESCRIPCION" HeaderText="Descripcion" SortExpression="ARTMAR_DESCRIPCION" />
                    </Columns>
                </asp:GridView>
                <br />
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel3" runat="server" HeaderText="Modelo">
            <ContentTemplate>   
				<div class="form-group col-lg-10">
					<label class="col-lg-2 control-label" for="id_Cod_Marca_Modelo">Marca: </label>
					<div class="col-lg-6">
                        <asp:DropDownList ID="DdlMarca" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="BtnNuevoModelo" runat="server" Text="Nuevo" CssClass="btn control-button"/>
				</div>
                <div class="col-lg-6">
						<input class="form-control" id="codigoModelo"
							 type="text" runat="server" visible="False"/>
					</div>
				<div class="form-group col-lg-10">
					<label class="col-lg-2 control-label" for="id_correo" id="LblDescripcionModelo" runat="server" visible="False">Descripción:</label>
					<div class="col-lg-6">
						<input class="form-control" id="TxtDescripcionModelo"
							 type="text" runat="server" visible="False"/>
					</div>
			    </div>
				<div class="form-group col-lg-10">
                    <asp:Button ID="BtnCancelarModelo" runat="server" Text="Cancelar" CssClass="btn control-button" visible="False"/>
                    <asp:Button ID="BtnAgregarModelo" runat="server" Text="Agregar" CssClass="btn control-button" visible="False"/>
				</div>
				<div class="form-group">
                    <asp:GridView runat="server" AutoGenerateColumns="False" ID="GvListaModelo" CssClass="table table-bordered">
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="EditaModelo" Text="Editar">
                                <ControlStyle CssClass=" btn btn-default" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="EliminaModelo" Text="Eliminar">
                                <ControlStyle CssClass=" btn btn-default" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="ARTMAR_CODIGO" HeaderText="Cod. Marca" SortExpression="ARTMAR_CODIGO" />
                            <asp:BoundField DataField="ARTMOD_CODIGO" HeaderText="Cod. Modelo" SortExpression="ARTMOD_CODIGO" />
                            <asp:BoundField DataField="ARTMOD_DESCRIPCION" HeaderText="Descripcion" SortExpression="ARTMOD_DESCRIPCION" />
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel4" runat="server" HeaderText="Propietario">
            <ContentTemplate> 
                <asp:Label ID="LblDescPropietario" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="TxtDescPropietario" runat="server" Width="222px"></asp:TextBox>
                <asp:Button ID="BtnListarPropietario" runat="server" Text=" Listar" Width="76px" CssClass=" btn btn-default"/>
                <asp:Button ID="BtnNuevoPropietario" runat="server" Text="Nuevo" Width="74px" CssClass=" btn btn-default"/>
                <asp:GridView ID="GvListaPropietario" runat="server" AutoGenerateColumns="False" Height="16px" Width="448px" CssClass="table table-bordered">
                    <Columns>
                        <asp:ButtonField CommandName="EditaPropietario" Text="Editar" ButtonType="Button">
                        <ControlStyle CssClass="btn btn-default" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="EliminaPropietario" Text="Eliminar" ButtonType="Button" >
                        <ControlStyle CssClass="btn btn-default" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="ALTIBI_CODIGO" HeaderText="Codigo" SortExpression="ALTIBI_CODIGO" />
                        <asp:BoundField DataField="ALTIBI_DESCRIPCION" HeaderText="Descripcion" SortExpression="INVENT_DESCRIPCION" />
                        <asp:BoundField DataField="ALTIBI_PLACABILIDAD" HeaderText="Placabilidad" SortExpression="ALTIBI_PLACABILIDAD" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="LblCodigoPropietario" runat="server" Text="Código" Visible="False" />
                <asp:TextBox ID="TxtCodigoPropietario" runat="server" Visible="False" Width="130px" Enabled="False" /><br />
                <asp:Label ID="LblDescripcionPropietario" runat="server" Text="Descripción" Visible="False" />
                <asp:TextBox ID="TxtDescripcionPropietario" runat="server" Visible="False" Width="139px" />
                <asp:Button ID="BtnAgregarPropietario" runat="server" Text="Agregar" Visible="False" /><br />
                <asp:Label ID="LblPlacabilidadPropietario" runat="server" Text="Placabilidad" Visible="False" />
                <asp:TextBox ID="TxtPlacabilidadPropietario" runat="server" Visible="False" Width="96px" />
                <asp:Button ID="BtnCancelarPropietario" runat="server" Text="Cancelar" Visible="False" />
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="Panel5" runat="server" HeaderText="Proyectos">
          <ContentTemplate> 
            <asp:DropDownList ID="DdlAño" runat="server" AutoPostBack="True"></asp:DropDownList>
   
            <asp:Button ID="btnListar_Proyectos" runat="server" Text="Listar" CssClass=" btn btn-default"/>
            <asp:Button ID="btnNuevo_Proyectos" runat="server" Text="Nuevo" CssClass=" btn btn-default"/>

            <br />
            <asp:GridView ID="GridView_Proyectos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button">
                    <ControlStyle CssClass=" btn btn-default" />
                    </asp:ButtonField>
                    <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                    <ControlStyle CssClass=" btn btn-default" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="PROYECTO_CODIGO" HeaderText="Codigo" SortExpression="PROYECTO_CODIGO" />
                    <asp:BoundField DataField="PROYECTO_AÑO" HeaderText="Año" SortExpression="PROYECTO_AÑO" />
                    <asp:BoundField DataField="PROYECTO_DESCRIPCION" HeaderText="Descripcion" SortExpression="PROYECTO_DESCRIPCION" />
                </Columns>
                </asp:GridView>
                <asp:Label ID="LblAño_Proy" runat="server" Text="Año" Visible="False"></asp:Label>

                <asp:DropDownList ID="DdlAñoNuevo" runat="server" AutoPostBack="True" Visible="False"></asp:DropDownList>

                <br />
                <asp:Label ID="LblCodigo_Proy" runat="server" Text="Código" Visible="False"></asp:Label>
                <asp:TextBox ID="txtCodigo_Proy" runat="server" Width="106px" Visible="False" Enabled="False"></asp:TextBox>

                <br />
                <asp:Label ID="LblDescripción_Proy" runat="server" Text="Descripción" Visible="False"></asp:Label>
                <asp:TextBox ID="txtDescripcion_Proy" runat="server" Visible="False" Width="283px"></asp:TextBox>

                <asp:Button ID="BtnGrabar_Proyectos" runat="server" Text="Grabar" Visible="False" />
                <asp:Button ID="BtnCancelar_Proyectos" runat="server" Text="Cancelar" Visible="False" />
           </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>

</asp:Content>

