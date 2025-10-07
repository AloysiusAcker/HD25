<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Relacion_Movimientos.aspx.vb" Inherits="Inventario_Inventario_Relacion_Movimientos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <script type="text/javascript">
        function validateDecimalInput(textBox) {
            var valid = /^-?\d+(\.\d*)?$/.test(textBox.value);
            if (!valid) {
                textBox.value = textBox.value.replace(/[^0-9.]+/g, '');
            }
        }
    </script>

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lbltitulo" runat="server" Text="Relación de Movimientos con pesos y volumenes" CssClass="Titulos"></asp:Label>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-3 col-md-6 col-xs-12">
                    <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="form-control btn btn-default" />
                </div>
                <div class="col-lg-3 col-md-6 col-xs-12">
                    <asp:Button ID="BtnExportar" runat="server" Text="Exportar" CssClass="form-control btn btn-default" />
                </div>
            </div>
            <div class="row">                
                <div class="col-md-3 col-xs-6">
                    <asp:Label ID="Lbletiqueta2" CssClass="control-label-2" runat="server" Text="Fecha de"></asp:Label>
                    <asp:TextBox ID="TxtFecha" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="custom-calendar" TargetControlID="TxtFecha" Format="dd/MM/yyyy" PopupButtonID="TxtFecha" ></cc1:CalendarExtender>
                </div>
                <div class="col-md-3 col-xs-6">
                    <asp:Label ID="Label2" CssClass="control-label-2" runat="server" Text="Fecha hasta"></asp:Label>
                    <asp:TextBox ID="TxtFechaFin" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="custom-calendar" TargetControlID="TxtFechaFin" Format="dd/MM/yyyy" PopupButtonID="TxtFechaFin" ></cc1:CalendarExtender>
                </div>
            </div>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>   
	                <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblEtiq1" runat="server" Text="Tipo Ubicación" CssClass="control-label-2" ></asp:Label>
                            <asp:DropDownList id="cboUbica" runat="server" CssClass="form-control" AutoPostBack="True">
                                        <asp:ListItem Selected="True" Value="&lt; Seleccionar &gt;">&lt; Seleccionar &gt;</asp:ListItem>
                                        <asp:ListItem Value="1">Almac&#233;n</asp:ListItem>
                                        <asp:ListItem Value="2">Secci&#243;n</asp:ListItem>
                            </asp:DropDownList>
                        </div>   
                        <div class="col-md-2">
                            <asp:Label ID="lblEtiqUbicacion" runat="server" Text="Búsqueda" CssClass="control-label-2"  ></asp:Label>
                            <asp:TextBox ID="txtUbicaCodInterno" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblEtiq3" runat="server" Text="Buscar" Class="control-label-2" ForeColor="White" ></asp:Label>
                            <asp:Button ID="btnBusUbicacion" runat="server" Text="..." CssClass="form-control btn btn-default" Enabled="false" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblEtiq4" runat="server" Text="NombreArt" Class="control-label-2" ForeColor="White" ></asp:Label>
                            <asp:TextBox ID="txtUbicaDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>            
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="txtUbicaCodigo" runat="server" Class="control-label" Visible="false"></asp:Label>
                        </div>
                    </div>        
            
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="LblRegistro" runat="server"  Font-Size="8pt" Font-Names="Arial" ForeColor="Maroon" ></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView id="Flex" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView">
                                <Columns>
                                    <asp:ButtonField CommandName="Detalle" Text="Volumen y Peso" ButtonType="Button" ControlStyle-CssClass="btn btn-default" >
                                        <ControlStyle CssClass=" btn btn-default"></ControlStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="k_art_codigo" HeaderText="Cod. Art.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_nombre" HeaderText="Descripción">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_fecha" HeaderText="Fecha">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_tipo_mov" HeaderText="Tipo Mov.">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_cant" HeaderText="Cantidad">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_peso_total" HeaderText="Peso Total">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_volumen_total" HeaderText="Volumen Total">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_peso_unit" HeaderText="Peso Unit.">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_alto" HeaderText="Alto">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_ancho" HeaderText="Ancho">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_largo" HeaderText="Largo">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="k_art_volumen" HeaderText="Volumen">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"/>
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView> 
                        </div>
                    </div> 
                </ContentTemplate>
                <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
                
                <asp:AsyncPostBackTrigger ControlID="cboUbica" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
            </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>

    <div class="modal" id="miModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="padding: 8px 10px; text-align: center; background-color: white;">
                    <h4 class="modal-title">Ingresar Volumenes y Peso</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        <span>&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="padding: 20px 10px 0;">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-3 col-xs-12">
                                        <label class="control-label2" for="id_ArtCodigo">Artículo</label>
                                        <input class="form-control" id="txtArtCodigo" type="text" runat="server" />
                                    </div>                                          
                                    <div class="col-md-9 col-xs-12">
                                        <label class="control-label" for="id_ArtDescripcion">Descripción</label>
                                        <input class="form-control" id="txtArtDescripcion" type="text" runat="server" />
                                    </div>
                                </div>   
                                <hr />
                                <div class="row">                                                
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Alto">Alto</label>
                                        <asp:TextBox ID="txtVolAlto" runat="server" CssClass="form-control" text="0" ClientIDMode="Static" oninput="validateDecimalInput(this)" ></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Ancho">Ancho</label>
                                        <asp:TextBox ID="txtVolAncho" runat="server" CssClass="form-control" text="0" ClientIDMode="Static" oninput="validateDecimalInput(this)" ></asp:TextBox>
                                    </div>     
                                </div>
                                <div class="row">                                         
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Largo">Largo</label>
                                        <asp:TextBox ID="txtVolLargo" runat="server" CssClass="form-control" text="0" ClientIDMode="Static" oninput="validateDecimalInput(this)" ></asp:TextBox>
                                    </div>     
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Peso">Peso Unit.</label>
                                        <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control" text="0" ClientIDMode="Static" oninput="validateDecimalInput(this)" ></asp:TextBox>
                                    </div>         
                                </div>
                                <div class="row">                                                           
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Volumen">Volumen</label>
                                        <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control" ReadOnly="true"  text="0" ClientIDMode="Static" oninput="validateDecimalInput(this)" ></asp:TextBox>
                                    </div>                                                       
                                    <div class="col-md-6 col-xs-6">
                                        <label class="control-label" for="id_Volumen"></label>
                                        <asp:Button ID="BtnCalcular" runat="server" Text="Calcular Volumen"  ControlStyle-CssClass="btn btn-default"/>
                                    </div>   
                                </div>
                            </ContentTemplate>
                            <Triggers>                                
                                <asp:AsyncPostBackTrigger ControlID="BtnCalcular" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>                    
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-md-6 col-xs-6">
                            <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" ControlStyle-CssClass="btn btn-default"/>
                        </div>
                        <div class="col-md-6 col-xs-6">
                            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" ControlStyle-CssClass="btn btn-default"/>
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
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblEtiqUbicacion2" runat="server" Text="Buscar" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBusUbicacion" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="form-horizontal">
                    <div class="modal-body" style="padding: 20px 10px 0;">
                        <div class="panel-group" id="step1">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_descripcion">Descripción :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="txtBusUbicDescripcion" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnUbicListar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Buscar" />
                                                </div>
                                            </div>
                                            <div class="row form-group col-md-12">
                                                <label class="col-lg-3 control-label" for="id_codigo">Código :</label>
                                                <div class="col-sm-6 col-xs-5">
                                                    <input class="form-control" id="txtBusUbicCodInterno" type="text" runat="server" />
                                                </div>
                                                <div class="col-sm-3 col-xs-2">
                                                    <asp:Button ID="btnUbicCerrar" ControlStyle-CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnUbicCerrar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <div class="row form-group col-md-12">
                                        <asp:UpdatePanel ID="upSetSession" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView id="FlexUbicacion" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered GridView" >
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Image" CommandName="Aceptar" ImageUrl="~/icono/ok.png" Text="Aceptar">
                                                            <ItemStyle Height="10px" Width="10px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CODIGO">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" ForeColor="DarkGray" Width="0px"></ItemStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnUbicListar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnUbicCerrar" EventName="Click" />
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


</asp:Content>

