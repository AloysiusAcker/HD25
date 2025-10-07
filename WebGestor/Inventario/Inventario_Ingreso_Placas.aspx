<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="Inventario_Ingreso_Placas.aspx.vb" Inherits="Inventario_Ingreso_Placas" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1"  runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="Ficha" runat="server" AutoPostBack="true" ActiveTabIndex="0" Width="100%" CssClass="MyTabStyle ajax__tab_header">
        <cc1:TabPanel ID="PnLista" runat="server" HeaderText="Lista Recepcion">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="Lista Recepciones" CssClass="Titulos"></asp:Label><br/><br/>

                <div class="form-group">
				    <asp:Label CssClass="col-lg-2 control-label-2" runat ="server" Text ="Almacen:"></asp:Label>
				    <div class="col-lg-4">
                        <asp:DropDownList ID="DdlAlmacén" runat="server" CssClass="form-control col-lg-2">
                        </asp:DropDownList>
                    </div>
                        <div class="col-lg-4">
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    </div>
                <div class="form-group">
                    <div class="col-lg-offset-0">
                        <asp:Button ID="BtnListar" runat="server" Text="Listar"  CssClass="btn btn-default"/>
                        </div>
				</div>
                <br/><br/>
                 <asp:GridView ID="GridView_Lista_Recepción" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                <Columns>
                    <asp:ButtonField CommandName="Detalle" Text="Detalle" ButtonType="Image" ImageUrl="~/icono/details_opt.png">
                    <ControlStyle CssClass=" btn btn-default" />
                    <ItemStyle Width="10px" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="RECEP_CODIGO" HeaderText="N° RECEPCIÓN" SortExpression="RECEP_CODIGO" />
                    <asp:BoundField DataField="MOTIVO_GRAL" HeaderText="MOTIVO" SortExpression="MOTIVO_GRAL" />
                    <asp:BoundField DataField="RECEP_FECHA_REG" HeaderText="F. REGISTRO" SortExpression="RECEP_FECHA_REG" />
                    <asp:BoundField DataField="TIPODOC" HeaderText="TIPO DOCUMENTO" SortExpression="TIPODOC"></asp:BoundField>
                    <asp:BoundField DataField="RECEP_DOC_NUMERACION" HeaderText="N° DOCUMENTO" SortExpression="RECEP_DOC_NUMERACION" >
                    <ItemStyle Height="10px" Width="10px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RECEP_FEC_EMI_DOC" HeaderText="F. RECEPCIÓN" SortExpression="RECEP_FEC_EMI_DOC" />
                    <asp:BoundField DataField="RUC" HeaderText="PROVEEDOR RUC" SortExpression="RUC" />
                    <asp:BoundField DataField="RAZONSOCIAL" HeaderText="PROVEEDOR RAZÓN SOCIAL" SortExpression="RAZONSOCIAL" />
                    <asp:BoundField DataField="RECEP_ESTADO" HeaderText="ESTADO RECEPCIÓN" SortExpression="RECEP_ESTADO" />
                    <asp:BoundField DataField="RECEP_NRO_ITEM" HeaderText="N° ITEMS" SortExpression="RECEP_NRO_ITEM" />
                    <asp:BoundField DataField="RECEP_CANT_REC" HeaderText="CANT. TOTAL RECIBIDA" SortExpression="RECEP_CANT_REC" />
                </Columns>
                </asp:GridView>

            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="PnIngreso" runat="server" HeaderText="Ingreso de Placas">
            <ContentTemplate>
                <asp:Label ID="Label2" runat="server" Text="Ingreso de Placas" CssClass="Titulos"></asp:Label><br/><br/>
                
                <div class="form-group"> 
                <asp:Label CssClass="col-lg-2 control-label-2" runat ="server" Text ="N° Recepción:"></asp:Label>
                    <div class="col-lg-3">
                        <asp:TextBox ID="txtNumRecepcion" runat="server"  CssClass="form-control" Enabled="False"></asp:TextBox>
                    </div>
                </div>

                <br />

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <asp:Button ID="BtnIngreso" runat="server" Text="Ingresar Placa"   CssClass="btn btn-default"/>
                            <asp:Button ID="BtnBorrar" runat="server" Text="Borrar Placa" CssClass="btn btn-default"/>
                            <asp:Button ID="BtnCerrar" runat="server" Text="Regresar" CssClass="btn btn-default"/>
                            <asp:Button ID="BtnGenerar" runat="server" Text="Generar Placa" Visible="False"  CssClass="btn btn-default"/>
                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" Visible="False" CssClass="btn btn-default"/>
                        </div>
                        <div id="FramePlaca" runat="server" visible="false" >
                            <div class="form-group">
                                <asp:Label CssClass="col-lg-2 control-label-2" runat ="server" Text ="Tipo de Placa:"></asp:Label>
                                <div class="col-lg-3">
                                    <asp:DropDownList ID="DdlTipoPlaca" runat="server" CssClass="form-control col-lg-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <asp:CheckBox ID="ChkMarcarTodo" runat="server" text="MarcarTodo" AutoPostBack="True"  />
                                </div>
                            </div>
                            <div class="form-group">
				                <asp:Label ID="LblUltima" runat="server" Text="Última Placa:" CssClass="col-lg-2 control-label-2"></asp:Label>
                                <div class="col-lg-4">                               
                                    <asp:TextBox ID="UltimaPlaca" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox> 				
                                </div>
                            </div>	       
                            <div class="form-group">         
                                <asp:Label ID="LblIniciar" runat="server" Text="Iniciar Placa:" CssClass="col-lg-2 control-label-2" ></asp:Label>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="IniciarPlaca" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div> 
                            </div>
                        </div>
                    <asp:GridView ID="GridView_Detalle_Recepción" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Check" runat="server"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ART_CODIGO" HeaderText="CODIGO" SortExpression="ART_CODIGO" />
                            <asp:BoundField DataField="ART_CODEQUIVA" HeaderText="N° PArte" SortExpression="ART_CODEQUIVA" />
                            <asp:BoundField DataField="ART_DESCRIPCION" HeaderText="Descripcion" SortExpression="ART_DESCRIPCION" />
                            <asp:BoundField DataField="SERIE_NRO" HeaderText="N° Serie" SortExpression="SERIE_NRO" />
                            <asp:BoundField DataField="PLACA_NRO" HeaderText="N° Placa" SortExpression="PLACA_NRO" />
                            <asp:BoundField DataField="ART_TIPO" HeaderText="" SortExpression="ART_TIPO">
                            <ItemStyle ForeColor="White" Height="10px" Width="10px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="SERIE_NUMERAR" HeaderText="" SortExpression="SERIE_NUMERAR">
                            <ItemStyle ForeColor="White" Height="10px" Width="10px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>

                    </ContentTemplate>
                             
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DdlTipoPlaca" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="BtnIngreso" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnBorrar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="BtnCerrar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ChkMarcarTodo" EventName="CheckedChanged" />
                    </Triggers>
                             
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>